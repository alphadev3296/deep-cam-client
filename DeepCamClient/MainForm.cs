using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace DeepCamClient
{
    public partial class MainForm : Form
    {
        private VideoCapture videoCapture;          // OpenCV video capture
        private CancellationTokenSource cancellationTokenSource;
        private Task captureTask;
        private bool isCapturing = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadDevices();
        }

        private void LoadDevices()
        {
            try
            {
                comboBoxDevices.Items.Clear();

                // OpenCV typically supports camera indices 0-9
                // We'll probe for available cameras
                int deviceCount = 0;
                for (int i = 0; i < 10; i++)
                {
                    using (var testCapture = new VideoCapture(i))
                    {
                        if (testCapture.IsOpened())
                        {
                            comboBoxDevices.Items.Add($"Camera {i}");
                            deviceCount++;
                        }
                    }
                }

                if (deviceCount > 0)
                {
                    comboBoxDevices.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("No video devices found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading devices: " + ex.Message);
            }
        }

        private void buttonRefrechDevices_Click(object sender, EventArgs e)
        {
            StopCapture();
            LoadDevices();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (comboBoxDevices.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a camera.");
                return;
            }

            StartCapture();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            StopCapture();
        }

        private async void StartCapture()
        {
            try
            {
                // Stop if already running
                StopCapture();

                // Clear background image
                pictureBoxPreview.Image?.Dispose();
                pictureBoxPreview.Image = null;

                // Get selected camera index
                int cameraIndex = comboBoxDevices.SelectedIndex;

                // Initialize video capture
                videoCapture = new VideoCapture(cameraIndex);

                if (!videoCapture.IsOpened())
                {
                    MessageBox.Show("Failed to open camera.");
                    return;
                }

                // Configure camera settings (optional)
                ConfigureCameraSettings();

                // Start capture task
                isCapturing = true;
                cancellationTokenSource = new CancellationTokenSource();
                captureTask = Task.Run(() => CaptureLoop(cancellationTokenSource.Token));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error starting capture: " + ex.Message);
                StopCapture();
            }
        }

        private void StopCapture()
        {
            try
            {
                isCapturing = false;

                // Cancel the capture task
                cancellationTokenSource?.Cancel();

                // Wait for task to complete
                captureTask?.Wait(1000);

                // Dispose resources
                videoCapture?.Release();
                videoCapture?.Dispose();
                videoCapture = null;

                cancellationTokenSource?.Dispose();
                cancellationTokenSource = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error stopping capture: " + ex.Message);
            }
        }

        private void ConfigureCameraSettings()
        {
            try
            {
                // Set camera properties (adjust as needed)
                videoCapture.Set(VideoCaptureProperties.FrameWidth, 640);
                videoCapture.Set(VideoCaptureProperties.FrameHeight, 480);
                videoCapture.Set(VideoCaptureProperties.Fps, 30);

                // Optional: Set buffer size to reduce latency
                videoCapture.Set(VideoCaptureProperties.BufferSize, 1);

                // Get actual values (camera might not support requested values)
                double actualWidth = videoCapture.Get(VideoCaptureProperties.FrameWidth);
                double actualHeight = videoCapture.Get(VideoCaptureProperties.FrameHeight);
                double actualFps = videoCapture.Get(VideoCaptureProperties.Fps);

                Console.WriteLine($"Camera configured: {actualWidth}x{actualHeight} @ {actualFps} FPS");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error configuring camera: " + ex.Message);
            }
        }

        private void CaptureLoop(CancellationToken cancellationToken)
        {
            using (var frame = new Mat())
            {
                while (isCapturing && !cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        // Read frame from camera
                        if (videoCapture.Read(frame) && !frame.Empty())
                        {
                            // Convert Mat to Bitmap
                            var bitmap = BitmapConverter.ToBitmap(frame);

                            // Update UI on main thread
                            if (pictureBoxPreview.IsHandleCreated && !pictureBoxPreview.IsDisposed)
                            {
                                pictureBoxPreview.BeginInvoke(new Action(() =>
                                {
                                    try
                                    {
                                        if (!pictureBoxPreview.IsDisposed)
                                        {
                                            pictureBoxPreview.Image?.Dispose();
                                            pictureBoxPreview.Image = bitmap;
                                        }
                                        else
                                        {
                                            bitmap?.Dispose();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("UI update error: " + ex.Message);
                                        bitmap?.Dispose();
                                    }
                                }));
                            }
                            else
                            {
                                bitmap?.Dispose();
                            }
                        }

                        // Small delay to prevent excessive CPU usage
                        Thread.Sleep(33); // ~30 FPS
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Frame capture error: " + ex.Message);
                        break;
                    }
                }
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                StopCapture();
                pictureBoxPreview.Image?.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cleanup error: " + ex.Message);
            }
        }

        // Additional helper methods for advanced features

        private void CaptureStillImage(string filename)
        {
            try
            {
                if (videoCapture != null && videoCapture.IsOpened())
                {
                    using (var frame = new Mat())
                    {
                        if (videoCapture.Read(frame) && !frame.Empty())
                        {
                            frame.SaveImage(filename);
                            MessageBox.Show($"Image saved: {filename}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error capturing still image: " + ex.Message);
            }
        }

        private void ShowCameraInfo()
        {
            try
            {
                if (videoCapture != null && videoCapture.IsOpened())
                {
                    double width = videoCapture.Get(VideoCaptureProperties.FrameWidth);
                    double height = videoCapture.Get(VideoCaptureProperties.FrameHeight);
                    double fps = videoCapture.Get(VideoCaptureProperties.Fps);
                    double brightness = videoCapture.Get(VideoCaptureProperties.Brightness);
                    double contrast = videoCapture.Get(VideoCaptureProperties.Contrast);

                    string info = $"Camera Information:\n" +
                                 $"Resolution: {width}x{height}\n" +
                                 $"FPS: {fps}\n" +
                                 $"Brightness: {brightness}\n" +
                                 $"Contrast: {contrast}";

                    MessageBox.Show(info, "Camera Info");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting camera info: " + ex.Message);
            }
        }

        // Method to apply basic image processing
        private Mat ProcessFrame(Mat inputFrame)
        {
            try
            {
                var processedFrame = new Mat();

                // Example: Convert to grayscale and back to color
                // Cv2.CvtColor(inputFrame, processedFrame, ColorConversionCodes.BGR2GRAY);
                // Cv2.CvtColor(processedFrame, processedFrame, ColorConversionCodes.GRAY2BGR);

                // Example: Apply Gaussian blur
                // Cv2.GaussianBlur(inputFrame, processedFrame, new Size(15, 15), 0);

                // For now, just return the original frame
                inputFrame.CopyTo(processedFrame);

                return processedFrame;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Frame processing error: " + ex.Message);
                return inputFrame.Clone();
            }
        }

        // Method to adjust camera properties
        private void SetCameraBrightness(double value)
        {
            try
            {
                if (videoCapture != null && videoCapture.IsOpened())
                {
                    videoCapture.Set(VideoCaptureProperties.Brightness, value);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error setting brightness: " + ex.Message);
            }
        }

        private void SetCameraContrast(double value)
        {
            try
            {
                if (videoCapture != null && videoCapture.IsOpened())
                {
                    videoCapture.Set(VideoCaptureProperties.Contrast, value);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error setting contrast: " + ex.Message);
            }
        }
    }
}