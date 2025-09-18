using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace DeepCamClient
{
    public partial class MainForm : Form
    {
        private WebCamSource _webCamSource;
        private List<CameraDevice> _availableDevices;
        private bool _isFormClosing = false;

        public MainForm()
        {
            InitializeComponent();
            InitializeWebCamSource();
        }

        private void InitializeWebCamSource()
        {
            _webCamSource = new WebCamSource();

            // Subscribe to events
            _webCamSource.FrameCaptured += OnFrameCaptured;
            _webCamSource.ErrorOccurred += OnErrorOccurred;
            _webCamSource.StatusChanged += OnStatusChanged;

            // Configure default settings
            _webCamSource.Settings.Width = 640;
            _webCamSource.Settings.Height = 480;
            _webCamSource.Settings.Fps = 5;
            _webCamSource.Settings.BufferSize = 1;
            _webCamSource.Settings.ContinueOnError = true;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadDevices();
            UpdateUI();
        }

        private void LoadDevices()
        {
            try
            {
                comboBoxDevices.Items.Clear();
                _availableDevices = _webCamSource.GetAvailableDevices();

                foreach (var device in _availableDevices)
                {
                    comboBoxDevices.Items.Add(device.Name);
                }

                if (comboBoxDevices.Items.Count > 0)
                {
                    comboBoxDevices.SelectedIndex = 0;
                }
                else
                {
                    ShowStatus("No video devices found.", true);
                }
            }
            catch (Exception ex)
            {
                ShowStatus($"Error loading devices: {ex.Message}", true);
            }
        }

        private void buttonRefreshDevices_Click(object sender, EventArgs e)
        {
            StopCameraCapture();
            LoadDevices();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (comboBoxDevices.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a camera.", "No Camera Selected",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            StartCameraCapture();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            StopCameraCapture();
        }

        private void buttonCapture_Click(object sender, EventArgs e)
        {
            CaptureStillImage();
        }

        private void buttonShowInfo_Click(object sender, EventArgs e)
        {
            ShowCameraInfo();
        }

        private void StartCameraCapture()
        {
            try
            {
                var selectedIndex = comboBoxDevices.SelectedIndex;
                var selectedDevice = _availableDevices[selectedIndex];

                // Open camera
                if (!_webCamSource.OpenCamera(selectedDevice.Index, selectedDevice.Name))
                {
                    return; // Error will be shown through event
                }

                // Apply current UI settings to camera
                ApplyUISettingsToCamera();

                // Start capture
                if (_webCamSource.StartCapture())
                {
                    UpdateUI();
                }
            }
            catch (Exception ex)
            {
                ShowStatus($"Error starting camera: {ex.Message}", true);
            }
        }

        private void StopCameraCapture()
        {
            try
            {
                _webCamSource.StopCapture();
                _webCamSource.CloseCamera();

                // Clear preview
                if (pictureBoxPreview != null)
                {
                    pictureBoxPreview.Image?.Dispose();
                    pictureBoxPreview.Image = null;
                }

                UpdateUI();
            }
            catch (Exception ex)
            {
                ShowStatus($"Error stopping camera: {ex.Message}", true);
            }
        }

        private void CaptureStillImage()
        {
            try
            {
                if (!_webCamSource.IsCapturing)
                {
                    MessageBox.Show("Camera is not capturing. Please start capture first.",
                                  "Camera Not Active", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "JPEG Image|*.jpg|PNG Image|*.png|Bitmap Image|*.bmp";
                    saveDialog.DefaultExt = "jpg";
                    saveDialog.FileName = $"capture_{DateTime.Now:yyyyMMdd_HHmmss}";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        if (_webCamSource.SaveCurrentFrame(saveDialog.FileName))
                        {
                            ShowStatus($"Image saved: {saveDialog.FileName}", false);
                        }
                        else
                        {
                            ShowStatus("Failed to save image.", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowStatus($"Error capturing image: {ex.Message}", true);
            }
        }

        private void ShowCameraInfo()
        {
            try
            {
                var info = _webCamSource.GetCameraInfo();
                if (info != null)
                {
                    MessageBox.Show(info.ToString(), "Camera Information",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Camera information not available.", "Camera Info",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                ShowStatus($"Error getting camera info: {ex.Message}", true);
            }
        }

        private void ApplyUISettingsToCamera()
        {
            var settings = _webCamSource.Settings;

            // Apply brightness and contrast from trackbars
            settings.ApplyBrightness = checkBoxBrightness.Checked;
            settings.Brightness = trackBarBrightness.Value;

            settings.ApplyContrast = checkBoxContrast.Checked;
            settings.Contrast = trackBarContrast.Value;

            settings.ApplyGaussianBlur = checkBoxBlur.Checked;
            settings.BlurKernelSize = (int)(trackBarBlur.Value / 2) * 2 + 1;

            // Apply processing settings from checkboxes
            settings.ConvertToGrayscale = checkBoxGrayscale.Checked;

            _webCamSource.UpdateSettings(settings);
        }

        // Event handlers for WebCamSource
        private void OnFrameCaptured(object sender, FrameCapturedEventArgs e)
        {
            if (_isFormClosing)
            {
                e.Frame?.Dispose();
                return;
            }

            // Update UI on main thread
            if (pictureBoxPreview != null && pictureBoxPreview.IsHandleCreated && !pictureBoxPreview.IsDisposed)
            {
                try
                {
                    pictureBoxPreview.BeginInvoke(new Action(() =>
                    {
                        if (!_isFormClosing && !pictureBoxPreview.IsDisposed)
                        {
                            // Dispose old image
                            pictureBoxPreview.Image?.Dispose();

                            // Set new image (PictureBox will handle scaling with SizeMode.Zoom)
                            pictureBoxPreview.Image = e.Frame;
                        }
                        else
                        {
                            e.Frame?.Dispose();
                        }
                    }));
                }
                catch (ObjectDisposedException)
                {
                    e.Frame?.Dispose();
                }
                catch (Exception ex)
                {
                    e.Frame?.Dispose();
                    Console.WriteLine($"UI update error: {ex.Message}");
                }
            }
            else
            {
                e.Frame?.Dispose();
            }
        }

        private void OnErrorOccurred(object sender, string message)
        {
            if (_isFormClosing) return;

            try
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() => ShowStatus(message, true)));
                }
                else
                {
                    ShowStatus(message, true);
                }
            }
            catch (ObjectDisposedException)
            {
                // Form is being disposed, ignore
            }
        }

        private void OnStatusChanged(object sender, CameraStatusChangedEventArgs e)
        {
            if (_isFormClosing) return;

            try
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() =>
                    {
                        ShowStatus($"Camera {e.Status}: {e.CameraName}", false);
                        UpdateUI();
                    }));
                }
                else
                {
                    ShowStatus($"Camera {e.Status}: {e.CameraName}", false);
                    UpdateUI();
                }
            }
            catch (ObjectDisposedException)
            {
                // Form is being disposed, ignore
            }
        }

        // UI control event handlers
        private void TrackBarBrightness_ValueChanged(object sender, EventArgs e)
        {
            if (_webCamSource.IsOpened)
            {
                ApplyUISettingsToCamera();
            }
        }

        private void TrackBarContrast_ValueChanged(object sender, EventArgs e)
        {
            if (_webCamSource.IsOpened)
            {
                ApplyUISettingsToCamera();
            }
        }

        private void CheckBoxGrayscale_CheckedChanged(object sender, EventArgs e)
        {
            if (_webCamSource.IsOpened)
            {
                ApplyUISettingsToCamera();
            }
        }

        private void CheckBoxBlur_CheckedChanged(object sender, EventArgs e)
        {
            if (_webCamSource.IsOpened)
            {
                UpdateUI();
                ApplyUISettingsToCamera();
            }
        }

        // Helper methods
        private void ShowStatus(string message, bool isError)
        {
            try
            {
                if (labelStatus != null && !labelStatus.IsDisposed)
                {
                    labelStatus.Text = message;
                    labelStatus.ForeColor = isError ? Color.Red : Color.Black;
                }

                // Also log to console
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {(isError ? "ERROR" : "INFO")}: {message}");

                // Show error in MessageBox for critical errors
                if (isError && message.Contains("Failed to open"))
                {
                    MessageBox.Show(message, "Camera Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating status: {ex.Message}");
            }
        }

        private void UpdateUI()
        {
            try
            {
                bool isCapturing = _webCamSource.IsCapturing;
                bool isOpened = _webCamSource.IsOpened;

                // Update button states
                buttonStart.Enabled = !isCapturing && comboBoxDevices.SelectedIndex >= 0;
                buttonStop.Enabled = isCapturing;
                buttonCapture.Enabled = isCapturing;
                buttonShowInfo.Enabled = isOpened;
                buttonRefreshDevices.Enabled = !isCapturing;
                comboBoxDevices.Enabled = !isCapturing;
                trackBarBrightness.Enabled = isOpened;
                trackBarContrast.Enabled = isOpened;
                trackBarBlur.Enabled = isOpened;

                // Update checkboxes
                checkBoxBrightness.Enabled = isOpened;
                checkBoxContrast.Enabled = isOpened;
                checkBoxGrayscale.Enabled = isOpened;
                checkBoxBlur.Enabled = isOpened;

                // Update trackbars
                trackBarBrightness.Enabled = checkBoxBrightness.Checked;
                trackBarContrast.Enabled = checkBoxContrast.Checked;
                trackBarBlur.Enabled = checkBoxBlur.Checked;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating UI: {ex.Message}");
            }
        }

        private Bitmap ResizeImageToFit(Bitmap originalImage, Size targetSize)
        {
            if (originalImage == null || targetSize.Width <= 0 || targetSize.Height <= 0)
                return originalImage;

            try
            {
                // Calculate scaling factor to maintain aspect ratio
                float scaleX = (float)targetSize.Width / originalImage.Width;
                float scaleY = (float)targetSize.Height / originalImage.Height;
                float scale = Math.Min(scaleX, scaleY);

                int newWidth = (int)(originalImage.Width * scale);
                int newHeight = (int)(originalImage.Height * scale);

                var resizedImage = new Bitmap(newWidth, newHeight);
                using (var graphics = Graphics.FromImage(resizedImage))
                {
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.DrawImage(originalImage, 0, 0, newWidth, newHeight);
                }

                return resizedImage;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Resize error: {ex.Message}");
                return originalImage;
            }
        }

        // Form event handlers
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _isFormClosing = true;

            try
            {
                // Stop capture and close camera
                _webCamSource?.StopCapture();
                _webCamSource?.CloseCamera();

                // Clean up preview image
                if (pictureBoxPreview?.Image != null)
                {
                    pictureBoxPreview.Image.Dispose();
                    pictureBoxPreview.Image = null;
                }

                // Dispose WebCamSource
                _webCamSource?.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cleanup error: {ex.Message}");
            }
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            // Handle form resize if needed
            UpdateUI();
        }

        // Additional utility methods
        private void SetCameraResolution(int width, int height)
        {
            if (_webCamSource.IsOpened)
            {
                var settings = _webCamSource.Settings;
                settings.Width = width;
                settings.Height = height;
                _webCamSource.UpdateSettings(settings);
            }
        }

        private void SetCameraFPS(double fps)
        {
            if (_webCamSource.IsOpened)
            {
                var settings = _webCamSource.Settings;
                settings.Fps = fps;
                _webCamSource.UpdateSettings(settings);
            }
        }

        // Menu event handlers (if you have menu items)
        private void menuItemResolution640x480_Click(object sender, EventArgs e)
        {
            SetCameraResolution(640, 480);
        }

        private void menuItemResolution1280x720_Click(object sender, EventArgs e)
        {
            SetCameraResolution(1280, 720);
        }

        private void menuItemResolution1920x1080_Click(object sender, EventArgs e)
        {
            SetCameraResolution(1920, 1080);
        }

        private void menuItemFPS15_Click(object sender, EventArgs e)
        {
            SetCameraFPS(15);
        }

        private void menuItemFPS30_Click(object sender, EventArgs e)
        {
            SetCameraFPS(30);
        }

        private void menuItemFPS60_Click(object sender, EventArgs e)
        {
            SetCameraFPS(60);
        }

        // Advanced features
        private void StartVideoRecording(string filename)
        {
            // This would require additional implementation with OpenCV VideoWriter
            // Left as a placeholder for future enhancement
            throw new NotImplementedException("Video recording not yet implemented");
        }

        private void StopVideoRecording()
        {
            // This would require additional implementation with OpenCV VideoWriter
            // Left as a placeholder for future enhancement
            throw new NotImplementedException("Video recording not yet implemented");
        }

        private void ApplyImageFilter(string filterType)
        {
            // Example of how to apply different filters
            var settings = _webCamSource.Settings;

            switch (filterType.ToLower())
            {
                case "grayscale":
                    settings.ConvertToGrayscale = true;
                    settings.ApplyGaussianBlur = false;
                    break;

                case "blur":
                    settings.ConvertToGrayscale = false;
                    settings.ApplyGaussianBlur = true;
                    settings.BlurKernelSize = 15;
                    break;

                case "none":
                default:
                    settings.ConvertToGrayscale = false;
                    settings.ApplyGaussianBlur = false;
                    break;
            }

            _webCamSource.UpdateSettings(settings);
        }

        // Example of how to handle custom processing
        private void EnableCustomProcessing()
        {
            // You could extend the WebCamSource class to support custom processing delegates
            // This is just an example of the pattern
            /*
            _webCamSource.CustomFrameProcessor = (inputFrame) =>
            {
                // Your custom OpenCV processing here
                var processedFrame = new Mat();
                // ... custom processing logic ...
                return processedFrame;
            };
            */
        }

        private void checkBoxBrightness_CheckedChanged(object sender, EventArgs e)
        {
            if (_webCamSource.IsOpened)
            {
                UpdateUI();
                ApplyUISettingsToCamera();
            }
        }

        private void checkBoxContrast_CheckedChanged(object sender, EventArgs e)
        {
            if (_webCamSource.IsOpened)

            {
                UpdateUI();
                ApplyUISettingsToCamera();
            }
        }

        private void trackBarBlur_ValueChanged(object sender, EventArgs e)
        {
            if (_webCamSource.IsOpened)
            {
                ApplyUISettingsToCamera();
            }
        }
    }

    // Extension class for additional WebCamSource functionality
    public static class WebCamSourceExtensions
    {
        public static void SetResolutionPreset(this WebCamSource webCamSource, string preset)
        {
            var settings = webCamSource.Settings;

            switch (preset.ToUpper())
            {
                case "VGA":
                    settings.Width = 640;
                    settings.Height = 480;
                    break;
                case "HD":
                    settings.Width = 1280;
                    settings.Height = 720;
                    break;
                case "FHD":
                    settings.Width = 1920;
                    settings.Height = 1080;
                    break;
                case "4K":
                    settings.Width = 3840;
                    settings.Height = 2160;
                    break;
                default:
                    return; // Unknown preset
            }

            webCamSource.UpdateSettings(settings);
        }
    }
}