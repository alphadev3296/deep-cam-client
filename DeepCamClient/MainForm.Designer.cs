namespace DeepCamClient
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBoxDevices = new System.Windows.Forms.ComboBox();
            this.buttonRefreshDevices = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBoxDetails = new System.Windows.Forms.GroupBox();
            this.buttonShowInfo = new System.Windows.Forms.Button();
            this.groupBoxCapture = new System.Windows.Forms.GroupBox();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonCapture = new System.Windows.Forms.Button();
            this.groupBoxOutput = new System.Windows.Forms.GroupBox();
            this.labelResolution = new System.Windows.Forms.Label();
            this.comboBoxResolution = new System.Windows.Forms.ComboBox();
            this.labelFps = new System.Windows.Forms.Label();
            this.comboBoxFPS = new System.Windows.Forms.ComboBox();
            this.trackBarBlur = new System.Windows.Forms.TrackBar();
            this.checkBoxContrast = new System.Windows.Forms.CheckBox();
            this.checkBoxBrightness = new System.Windows.Forms.CheckBox();
            this.trackBarBrightness = new System.Windows.Forms.TrackBar();
            this.trackBarContrast = new System.Windows.Forms.TrackBar();
            this.checkBoxBlur = new System.Windows.Forms.CheckBox();
            this.checkBoxGrayscale = new System.Windows.Forms.CheckBox();
            this.labelStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBoxDetails.SuspendLayout();
            this.groupBoxCapture.SuspendLayout();
            this.groupBoxOutput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBlur)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarContrast)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxDevices
            // 
            this.comboBoxDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDevices.FormattingEnabled = true;
            this.comboBoxDevices.Location = new System.Drawing.Point(12, 41);
            this.comboBoxDevices.Name = "comboBoxDevices";
            this.comboBoxDevices.Size = new System.Drawing.Size(200, 23);
            this.comboBoxDevices.TabIndex = 0;
            // 
            // buttonRefreshDevices
            // 
            this.buttonRefreshDevices.Location = new System.Drawing.Point(12, 12);
            this.buttonRefreshDevices.Name = "buttonRefreshDevices";
            this.buttonRefreshDevices.Size = new System.Drawing.Size(122, 23);
            this.buttonRefreshDevices.TabIndex = 1;
            this.buttonRefreshDevices.Text = "Refresh Devices";
            this.buttonRefreshDevices.UseVisualStyleBackColor = true;
            this.buttonRefreshDevices.Click += new System.EventHandler(this.buttonRefreshDevices_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(10, 27);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(87, 23);
            this.buttonStart.TabIndex = 2;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // pictureBoxPreview
            // 
            this.pictureBoxPreview.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBoxPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxPreview.Location = new System.Drawing.Point(227, 0);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(757, 582);
            this.pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxPreview.TabIndex = 3;
            this.pictureBoxPreview.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBoxDetails);
            this.panel1.Controls.Add(this.groupBoxCapture);
            this.panel1.Controls.Add(this.groupBoxOutput);
            this.panel1.Controls.Add(this.buttonRefreshDevices);
            this.panel1.Controls.Add(this.comboBoxDevices);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(227, 582);
            this.panel1.TabIndex = 4;
            // 
            // groupBoxDetails
            // 
            this.groupBoxDetails.Controls.Add(this.buttonShowInfo);
            this.groupBoxDetails.Location = new System.Drawing.Point(12, 515);
            this.groupBoxDetails.Name = "groupBoxDetails";
            this.groupBoxDetails.Size = new System.Drawing.Size(200, 57);
            this.groupBoxDetails.TabIndex = 15;
            this.groupBoxDetails.TabStop = false;
            this.groupBoxDetails.Text = "Details";
            // 
            // buttonShowInfo
            // 
            this.buttonShowInfo.Location = new System.Drawing.Point(6, 22);
            this.buttonShowInfo.Name = "buttonShowInfo";
            this.buttonShowInfo.Size = new System.Drawing.Size(75, 23);
            this.buttonShowInfo.TabIndex = 5;
            this.buttonShowInfo.Text = "Show Info";
            this.buttonShowInfo.UseVisualStyleBackColor = true;
            this.buttonShowInfo.Click += new System.EventHandler(this.buttonShowInfo_Click);
            // 
            // groupBoxCapture
            // 
            this.groupBoxCapture.Controls.Add(this.buttonStart);
            this.groupBoxCapture.Controls.Add(this.buttonStop);
            this.groupBoxCapture.Controls.Add(this.buttonCapture);
            this.groupBoxCapture.Location = new System.Drawing.Point(12, 70);
            this.groupBoxCapture.Name = "groupBoxCapture";
            this.groupBoxCapture.Size = new System.Drawing.Size(200, 88);
            this.groupBoxCapture.TabIndex = 14;
            this.groupBoxCapture.TabStop = false;
            this.groupBoxCapture.Text = "Capture";
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(103, 27);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(91, 23);
            this.buttonStop.TabIndex = 3;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonCapture
            // 
            this.buttonCapture.Location = new System.Drawing.Point(10, 56);
            this.buttonCapture.Name = "buttonCapture";
            this.buttonCapture.Size = new System.Drawing.Size(184, 23);
            this.buttonCapture.TabIndex = 4;
            this.buttonCapture.Text = "Capture Frame";
            this.buttonCapture.UseVisualStyleBackColor = true;
            this.buttonCapture.Click += new System.EventHandler(this.buttonCapture_Click);
            // 
            // groupBoxOutput
            // 
            this.groupBoxOutput.Controls.Add(this.labelResolution);
            this.groupBoxOutput.Controls.Add(this.comboBoxResolution);
            this.groupBoxOutput.Controls.Add(this.labelFps);
            this.groupBoxOutput.Controls.Add(this.comboBoxFPS);
            this.groupBoxOutput.Controls.Add(this.trackBarBlur);
            this.groupBoxOutput.Controls.Add(this.checkBoxContrast);
            this.groupBoxOutput.Controls.Add(this.checkBoxBrightness);
            this.groupBoxOutput.Controls.Add(this.trackBarBrightness);
            this.groupBoxOutput.Controls.Add(this.trackBarContrast);
            this.groupBoxOutput.Controls.Add(this.checkBoxBlur);
            this.groupBoxOutput.Controls.Add(this.checkBoxGrayscale);
            this.groupBoxOutput.Location = new System.Drawing.Point(12, 164);
            this.groupBoxOutput.Name = "groupBoxOutput";
            this.groupBoxOutput.Size = new System.Drawing.Size(200, 345);
            this.groupBoxOutput.TabIndex = 13;
            this.groupBoxOutput.TabStop = false;
            this.groupBoxOutput.Text = "Output";
            // 
            // labelResolution
            // 
            this.labelResolution.AutoSize = true;
            this.labelResolution.Location = new System.Drawing.Point(6, 63);
            this.labelResolution.Name = "labelResolution";
            this.labelResolution.Size = new System.Drawing.Size(29, 15);
            this.labelResolution.TabIndex = 17;
            this.labelResolution.Text = "RES:";
            // 
            // comboBoxResolution
            // 
            this.comboBoxResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxResolution.FormattingEnabled = true;
            this.comboBoxResolution.Items.AddRange(new object[] {
            "VGA",
            "HD",
            "FHD",
            "4K"});
            this.comboBoxResolution.Location = new System.Drawing.Point(35, 60);
            this.comboBoxResolution.Name = "comboBoxResolution";
            this.comboBoxResolution.Size = new System.Drawing.Size(159, 23);
            this.comboBoxResolution.TabIndex = 16;
            this.comboBoxResolution.SelectedIndexChanged += new System.EventHandler(this.comboBoxResolution_SelectedIndexChanged);
            // 
            // labelFps
            // 
            this.labelFps.AutoSize = true;
            this.labelFps.Location = new System.Drawing.Point(6, 25);
            this.labelFps.Name = "labelFps";
            this.labelFps.Size = new System.Drawing.Size(29, 15);
            this.labelFps.TabIndex = 15;
            this.labelFps.Text = "FPS:";
            // 
            // comboBoxFPS
            // 
            this.comboBoxFPS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFPS.FormattingEnabled = true;
            this.comboBoxFPS.Items.AddRange(new object[] {
            "5",
            "10",
            "15",
            "30",
            "60"});
            this.comboBoxFPS.Location = new System.Drawing.Point(35, 22);
            this.comboBoxFPS.Name = "comboBoxFPS";
            this.comboBoxFPS.Size = new System.Drawing.Size(159, 23);
            this.comboBoxFPS.TabIndex = 14;
            this.comboBoxFPS.SelectedIndexChanged += new System.EventHandler(this.comboBoxFPS_SelectedIndexChanged);
            // 
            // trackBarBlur
            // 
            this.trackBarBlur.LargeChange = 6;
            this.trackBarBlur.Location = new System.Drawing.Point(6, 293);
            this.trackBarBlur.Maximum = 51;
            this.trackBarBlur.Minimum = 1;
            this.trackBarBlur.Name = "trackBarBlur";
            this.trackBarBlur.Size = new System.Drawing.Size(188, 45);
            this.trackBarBlur.SmallChange = 2;
            this.trackBarBlur.TabIndex = 13;
            this.trackBarBlur.Value = 1;
            this.trackBarBlur.ValueChanged += new System.EventHandler(this.trackBarBlur_ValueChanged);
            // 
            // checkBoxContrast
            // 
            this.checkBoxContrast.AutoSize = true;
            this.checkBoxContrast.Location = new System.Drawing.Point(6, 167);
            this.checkBoxContrast.Name = "checkBoxContrast";
            this.checkBoxContrast.Size = new System.Drawing.Size(71, 19);
            this.checkBoxContrast.TabIndex = 12;
            this.checkBoxContrast.Text = "Contrast";
            this.checkBoxContrast.UseVisualStyleBackColor = true;
            this.checkBoxContrast.CheckedChanged += new System.EventHandler(this.checkBoxContrast_CheckedChanged);
            // 
            // checkBoxBrightness
            // 
            this.checkBoxBrightness.AutoSize = true;
            this.checkBoxBrightness.Location = new System.Drawing.Point(6, 93);
            this.checkBoxBrightness.Name = "checkBoxBrightness";
            this.checkBoxBrightness.Size = new System.Drawing.Size(81, 19);
            this.checkBoxBrightness.TabIndex = 11;
            this.checkBoxBrightness.Text = "Brightness";
            this.checkBoxBrightness.UseVisualStyleBackColor = true;
            this.checkBoxBrightness.CheckedChanged += new System.EventHandler(this.checkBoxBrightness_CheckedChanged);
            // 
            // trackBarBrightness
            // 
            this.trackBarBrightness.Location = new System.Drawing.Point(6, 116);
            this.trackBarBrightness.Maximum = 100;
            this.trackBarBrightness.Name = "trackBarBrightness";
            this.trackBarBrightness.Size = new System.Drawing.Size(188, 45);
            this.trackBarBrightness.TabIndex = 7;
            this.trackBarBrightness.Value = 50;
            this.trackBarBrightness.ValueChanged += new System.EventHandler(this.TrackBarBrightness_ValueChanged);
            // 
            // trackBarContrast
            // 
            this.trackBarContrast.Location = new System.Drawing.Point(6, 192);
            this.trackBarContrast.Maximum = 100;
            this.trackBarContrast.Name = "trackBarContrast";
            this.trackBarContrast.Size = new System.Drawing.Size(188, 45);
            this.trackBarContrast.TabIndex = 8;
            this.trackBarContrast.Value = 50;
            this.trackBarContrast.ValueChanged += new System.EventHandler(this.TrackBarContrast_ValueChanged);
            // 
            // checkBoxBlur
            // 
            this.checkBoxBlur.AutoSize = true;
            this.checkBoxBlur.Location = new System.Drawing.Point(6, 268);
            this.checkBoxBlur.Name = "checkBoxBlur";
            this.checkBoxBlur.Size = new System.Drawing.Size(47, 19);
            this.checkBoxBlur.TabIndex = 10;
            this.checkBoxBlur.Text = "Blur";
            this.checkBoxBlur.UseVisualStyleBackColor = true;
            this.checkBoxBlur.CheckedChanged += new System.EventHandler(this.CheckBoxBlur_CheckedChanged);
            // 
            // checkBoxGrayscale
            // 
            this.checkBoxGrayscale.AutoSize = true;
            this.checkBoxGrayscale.Location = new System.Drawing.Point(6, 243);
            this.checkBoxGrayscale.Name = "checkBoxGrayscale";
            this.checkBoxGrayscale.Size = new System.Drawing.Size(80, 19);
            this.checkBoxGrayscale.TabIndex = 9;
            this.checkBoxGrayscale.Text = "Gray Scale";
            this.checkBoxGrayscale.UseVisualStyleBackColor = true;
            this.checkBoxGrayscale.CheckedChanged += new System.EventHandler(this.CheckBoxGrayscale_CheckedChanged);
            // 
            // labelStatus
            // 
            this.labelStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelStatus.Location = new System.Drawing.Point(0, 582);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(984, 15);
            this.labelStatus.TabIndex = 5;
            this.labelStatus.Text = "Status";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 597);
            this.Controls.Add(this.pictureBoxPreview);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelStatus);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(1000, 622);
            this.Name = "MainForm";
            this.Text = "Deep Cam Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBoxDetails.ResumeLayout(false);
            this.groupBoxCapture.ResumeLayout(false);
            this.groupBoxOutput.ResumeLayout(false);
            this.groupBoxOutput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBlur)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarContrast)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComboBox comboBoxDevices;
        private Button buttonRefreshDevices;
        private Button buttonStart;
        private PictureBox pictureBoxPreview;
        private Panel panel1;
        private Button buttonStop;
        private CheckBox checkBoxBlur;
        private CheckBox checkBoxGrayscale;
        private Button buttonShowInfo;
        private Button buttonCapture;
        private GroupBox groupBoxOutput;
        private GroupBox groupBoxDetails;
        private GroupBox groupBoxCapture;
        private Label labelStatus;
        private TrackBar trackBarBrightness;
        private TrackBar trackBarContrast;
        private CheckBox checkBoxContrast;
        private CheckBox checkBoxBrightness;
        private TrackBar trackBarBlur;
        private ComboBox comboBoxFPS;
        private Label labelFps;
        private ComboBox comboBoxResolution;
        private Label labelResolution;
    }
}