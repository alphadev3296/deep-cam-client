using DirectShowLib;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace DeepCamClient
{
    public class WebCamSource : IDisposable
    {
        private VideoCapture _videoCapture;
        private CancellationTokenSource _cancellationTokenSource;
        private Task _captureTask;
        private bool _isCapturing;
        private bool _disposed;

        // Events
        public event EventHandler<FrameCapturedEventArgs> FrameCaptured;
        public event EventHandler<string> ErrorOccurred;
        public event EventHandler<CameraStatusChangedEventArgs> StatusChanged;

        // Properties
        public bool IsCapturing => _isCapturing;
        public bool IsOpened => _videoCapture?.IsOpened() ?? false;
        public int CurrentCameraIndex { get; private set; } = -1;
        public string CurrentCameraName { get; private set; } = string.Empty;

        // Camera settings
        public CameraSettings Settings { get; private set; }

        public WebCamSource()
        {
            Settings = new CameraSettings();
        }

        /// <summary>
        /// Gets list of available camera devices
        /// </summary>
        public List<CameraDevice> GetAvailableDevices()
        {
            var devices = new List<CameraDevice>();

            try
            {
                // Try to get device names using DirectShow
                var videoInputDevices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);

                int deviceIndex = 0;
                foreach (DsDevice device in videoInputDevices)
                {
                    // Test if camera is accessible via OpenCV
                    using (var testCapture = new VideoCapture(deviceIndex))
                    {
                        if (testCapture.IsOpened())
                        {
                            devices.Add(new CameraDevice
                            {
                                Index = deviceIndex,
                                Name = device.Name,
                                IsAvailable = true
                            });
                            deviceIndex++;
                        }
                    }
                }

                // Fallback: if DirectShow doesn't work, use generic names
                if (devices.Count == 0)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        using (var testCapture = new VideoCapture(i))
                        {
                            if (testCapture.IsOpened())
                            {
                                devices.Add(new CameraDevice
                                {
                                    Index = i,
                                    Name = $"Camera {i}",
                                    IsAvailable = true
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                OnErrorOccurred($"Error enumerating devices: {ex.Message}");
            }

            return devices;
        }

        /// <summary>
        /// Opens and initializes a camera
        /// </summary>
        public bool OpenCamera(int cameraIndex, string cameraName = "")
        {
            try
            {
                // Close existing camera if open
                CloseCamera();

                _videoCapture = new VideoCapture(cameraIndex);

                if (!_videoCapture.IsOpened())
                {
                    OnErrorOccurred($"Failed to open camera at index {cameraIndex}");
                    return false;
                }

                CurrentCameraIndex = cameraIndex;
                CurrentCameraName = string.IsNullOrEmpty(cameraName) ? $"Camera {cameraIndex}" : cameraName;

                // Apply camera settings
                ApplyCameraSettings();

                OnStatusChanged(new CameraStatusChangedEventArgs(CameraStatus.Opened, CurrentCameraName));
                return true;
            }
            catch (Exception ex)
            {
                OnErrorOccurred($"Error opening camera: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Starts video capture
        /// </summary>
        public bool StartCapture()
        {
            if (_videoCapture == null || !_videoCapture.IsOpened())
            {
                OnErrorOccurred("Camera not opened. Call OpenCamera first.");
                return false;
            }

            if (_isCapturing)
            {
                return true; // Already capturing
            }

            try
            {
                _isCapturing = true;
                _cancellationTokenSource = new CancellationTokenSource();
                _captureTask = Task.Run(() => CaptureLoop(_cancellationTokenSource.Token));

                OnStatusChanged(new CameraStatusChangedEventArgs(CameraStatus.Capturing, CurrentCameraName));
                return true;
            }
            catch (Exception ex)
            {
                OnErrorOccurred($"Error starting capture: {ex.Message}");
                StopCapture();
                return false;
            }
        }

        /// <summary>
        /// Stops video capture
        /// </summary>
        public void StopCapture()
        {
            if (!_isCapturing)
                return;

            try
            {
                _isCapturing = false;

                // Cancel the capture task
                _cancellationTokenSource?.Cancel();

                // Wait for task to complete with timeout
                _captureTask?.Wait(2000);

                _cancellationTokenSource?.Dispose();
                _cancellationTokenSource = null;
                _captureTask = null;

                OnStatusChanged(new CameraStatusChangedEventArgs(CameraStatus.Stopped, CurrentCameraName));
            }
            catch (Exception ex)
            {
                OnErrorOccurred($"Error stopping capture: {ex.Message}");
            }
        }

        /// <summary>
        /// Closes the camera and releases resources
        /// </summary>
        public void CloseCamera()
        {
            StopCapture();

            try
            {
                _videoCapture?.Release();
                _videoCapture?.Dispose();
                _videoCapture = null;

                CurrentCameraIndex = -1;
                CurrentCameraName = string.Empty;

                OnStatusChanged(new CameraStatusChangedEventArgs(CameraStatus.Closed, ""));
            }
            catch (Exception ex)
            {
                OnErrorOccurred($"Error closing camera: {ex.Message}");
            }
        }

        /// <summary>
        /// Captures a single frame as bitmap
        /// </summary>
        public Bitmap CaptureStillImage()
        {
            if (_videoCapture == null || !_videoCapture.IsOpened())
            {
                throw new InvalidOperationException("Camera not opened");
            }

            try
            {
                using (var frame = new Mat())
                {
                    if (_videoCapture.Read(frame) && !frame.Empty())
                    {
                        return BitmapConverter.ToBitmap(frame);
                    }
                }
            }
            catch (Exception ex)
            {
                OnErrorOccurred($"Error capturing still image: {ex.Message}");
            }

            return null;
        }

        /// <summary>
        /// Saves current frame to file
        /// </summary>
        public bool SaveCurrentFrame(string filename)
        {
            try
            {
                using (var bitmap = CaptureStillImage())
                {
                    if (bitmap != null)
                    {
                        bitmap.Save(filename);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                OnErrorOccurred($"Error saving frame: {ex.Message}");
            }

            return false;
        }

        /// <summary>
        /// Gets current camera information
        /// </summary>
        public CameraInfo GetCameraInfo()
        {
            if (_videoCapture == null || !_videoCapture.IsOpened())
                return null;

            try
            {
                return new CameraInfo
                {
                    Name = CurrentCameraName,
                    Index = CurrentCameraIndex,
                    Width = (int)_videoCapture.Get(VideoCaptureProperties.FrameWidth),
                    Height = (int)_videoCapture.Get(VideoCaptureProperties.FrameHeight),
                    Fps = _videoCapture.Get(VideoCaptureProperties.Fps),
                    Brightness = _videoCapture.Get(VideoCaptureProperties.Brightness),
                    Contrast = _videoCapture.Get(VideoCaptureProperties.Contrast),
                    Saturation = _videoCapture.Get(VideoCaptureProperties.Saturation),
                    Hue = _videoCapture.Get(VideoCaptureProperties.Hue)
                };
            }
            catch (Exception ex)
            {
                OnErrorOccurred($"Error getting camera info: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Updates camera settings
        /// </summary>
        public void UpdateSettings(CameraSettings newSettings)
        {
            Settings = newSettings ?? throw new ArgumentNullException(nameof(newSettings));

            if (_videoCapture != null && _videoCapture.IsOpened())
            {
                ApplyCameraSettings();
            }
        }

        private void ApplyCameraSettings()
        {
            if (_videoCapture == null || !_videoCapture.IsOpened())
                return;

            try
            {
                // Set resolution
                if (Settings.Width > 0 && Settings.Height > 0)
                {
                    _videoCapture.Set(VideoCaptureProperties.FrameWidth, Settings.Width);
                    _videoCapture.Set(VideoCaptureProperties.FrameHeight, Settings.Height);
                }

                // Set FPS
                if (Settings.Fps > 0)
                {
                    _videoCapture.Set(VideoCaptureProperties.Fps, Settings.Fps);
                }

                // Set buffer size for reduced latency
                if (Settings.BufferSize > 0)
                {
                    _videoCapture.Set(VideoCaptureProperties.BufferSize, Settings.BufferSize);
                }

                // Set image properties
                if (Settings.Brightness >= 0)
                    _videoCapture.Set(VideoCaptureProperties.Brightness, Settings.Brightness);

                if (Settings.Contrast >= 0)
                    _videoCapture.Set(VideoCaptureProperties.Contrast, Settings.Contrast);

                if (Settings.Saturation >= 0)
                    _videoCapture.Set(VideoCaptureProperties.Saturation, Settings.Saturation);

                if (Settings.Hue >= 0)
                    _videoCapture.Set(VideoCaptureProperties.Hue, Settings.Hue);
            }
            catch (Exception ex)
            {
                OnErrorOccurred($"Error applying camera settings: {ex.Message}");
            }
        }

        private void CaptureLoop(CancellationToken cancellationToken)
        {
            using (var frame = new Mat())
            {
                while (_isCapturing && !cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        if (_videoCapture.Read(frame) && !frame.Empty())
                        {
                            // Apply processing if needed
                            var processedFrame = Settings.EnableProcessing ? ProcessFrame(frame) : frame;

                            // Convert to bitmap
                            var bitmap = BitmapConverter.ToBitmap(processedFrame);

                            // Raise frame captured event
                            OnFrameCaptured(new FrameCapturedEventArgs(bitmap, DateTime.Now));

                            // Clean up processed frame if it's different from original
                            if (processedFrame != frame)
                            {
                                processedFrame?.Dispose();
                            }
                        }

                        // Frame rate control
                        var delayMs = Settings.Fps > 0 ? (int)(1000.0 / Settings.Fps) : 33;
                        Thread.Sleep(Math.Max(1, delayMs));
                    }
                    catch (Exception ex)
                    {
                        OnErrorOccurred($"Frame capture error: {ex.Message}");

                        if (!Settings.ContinueOnError)
                            break;

                        Thread.Sleep(100); // Brief pause before retry
                    }
                }
            }
        }

        private Mat ProcessFrame(Mat inputFrame)
        {
            if (!Settings.EnableProcessing)
                return inputFrame;

            try
            {
                var processedFrame = new Mat();

                // Apply processing based on settings
                if (Settings.ConvertToGrayscale)
                {
                    Cv2.CvtColor(inputFrame, processedFrame, ColorConversionCodes.BGR2GRAY);
                    Cv2.CvtColor(processedFrame, processedFrame, ColorConversionCodes.GRAY2BGR);
                }
                else if (Settings.ApplyGaussianBlur && Settings.BlurKernelSize > 0)
                {
                    var kernelSize = new OpenCvSharp.Size(Settings.BlurKernelSize, Settings.BlurKernelSize);
                    Cv2.GaussianBlur(inputFrame, processedFrame, kernelSize, 0);
                }
                else
                {
                    inputFrame.CopyTo(processedFrame);
                }

                return processedFrame;
            }
            catch (Exception ex)
            {
                OnErrorOccurred($"Frame processing error: {ex.Message}");
                return inputFrame.Clone();
            }
        }

        // Event handlers
        protected virtual void OnFrameCaptured(FrameCapturedEventArgs e)
        {
            FrameCaptured?.Invoke(this, e);
        }

        protected virtual void OnErrorOccurred(string message)
        {
            ErrorOccurred?.Invoke(this, message);
        }

        protected virtual void OnStatusChanged(CameraStatusChangedEventArgs e)
        {
            StatusChanged?.Invoke(this, e);
        }

        // IDisposable implementation
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                CloseCamera();
                _disposed = true;
            }
        }

        ~WebCamSource()
        {
            Dispose(false);
        }
    }

    // Supporting classes and enums
    public class CameraDevice
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
    }

    public class CameraSettings
    {
        public int Width { get; set; } = 640;
        public int Height { get; set; } = 480;
        public double Fps { get; set; } = 30;
        public int BufferSize { get; set; } = 1;

        // Image adjustment properties
        public double Brightness { get; set; } = -1; // -1 means don't set
        public double Contrast { get; set; } = -1;
        public double Saturation { get; set; } = -1;
        public double Hue { get; set; } = -1;

        // Processing options
        public bool EnableProcessing { get; set; } = false;
        public bool ConvertToGrayscale { get; set; } = false;
        public bool ApplyGaussianBlur { get; set; } = false;
        public int BlurKernelSize { get; set; } = 15;
        public bool ContinueOnError { get; set; } = true;
    }

    public class CameraInfo
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public double Fps { get; set; }
        public double Brightness { get; set; }
        public double Contrast { get; set; }
        public double Saturation { get; set; }
        public double Hue { get; set; }

        public override string ToString()
        {
            return $"Camera: {Name}\n" +
                   $"Resolution: {Width}x{Height}\n" +
                   $"FPS: {Fps:F1}\n" +
                   $"Brightness: {Brightness:F2}\n" +
                   $"Contrast: {Contrast:F2}\n" +
                   $"Saturation: {Saturation:F2}\n" +
                   $"Hue: {Hue:F2}";
        }
    }

    public enum CameraStatus
    {
        Closed,
        Opened,
        Capturing,
        Stopped,
        Error
    }

    // Event argument classes
    public class FrameCapturedEventArgs : EventArgs
    {
        public Bitmap Frame { get; }
        public DateTime Timestamp { get; }

        public FrameCapturedEventArgs(Bitmap frame, DateTime timestamp)
        {
            Frame = frame;
            Timestamp = timestamp;
        }
    }

    public class CameraStatusChangedEventArgs : EventArgs
    {
        public CameraStatus Status { get; }
        public string CameraName { get; }

        public CameraStatusChangedEventArgs(CameraStatus status, string cameraName)
        {
            Status = status;
            CameraName = cameraName;
        }
    }
}