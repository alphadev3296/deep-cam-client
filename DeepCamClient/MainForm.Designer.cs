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
            this.groupBoxEffect = new System.Windows.Forms.GroupBox();
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
            this.groupBoxEffect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBlur)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarContrast)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxDevices
            // 
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
            this.buttonRefreshDevices.Size = new System.Drawing.Size(75, 23);
            this.buttonRefreshDevices.TabIndex = 1;
            this.buttonRefreshDevices.Text = "Refresh";
            this.buttonRefreshDevices.UseVisualStyleBackColor = true;
            this.buttonRefreshDevices.Click += new System.EventHandler(this.buttonRefreshDevices_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(44, 27);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(112, 23);
            this.buttonStart.TabIndex = 2;
            this.buttonStart.Text = "Start Capture";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // pictureBoxPreview
            // 
            this.pictureBoxPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxPreview.Location = new System.Drawing.Point(227, 0);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(757, 633);
            this.pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxPreview.TabIndex = 3;
            this.pictureBoxPreview.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBoxDetails);
            this.panel1.Controls.Add(this.groupBoxCapture);
            this.panel1.Controls.Add(this.groupBoxEffect);
            this.panel1.Controls.Add(this.buttonRefreshDevices);
            this.panel1.Controls.Add(this.comboBoxDevices);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(227, 633);
            this.panel1.TabIndex = 4;
            // 
            // groupBoxDetails
            // 
            this.groupBoxDetails.Controls.Add(this.buttonShowInfo);
            this.groupBoxDetails.Location = new System.Drawing.Point(12, 535);
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
            this.groupBoxCapture.Size = new System.Drawing.Size(200, 126);
            this.groupBoxCapture.TabIndex = 14;
            this.groupBoxCapture.TabStop = false;
            this.groupBoxCapture.Text = "Capture";
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(44, 56);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(112, 23);
            this.buttonStop.TabIndex = 3;
            this.buttonStop.Text = "Stop Capture";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonCapture
            // 
            this.buttonCapture.Location = new System.Drawing.Point(44, 85);
            this.buttonCapture.Name = "buttonCapture";
            this.buttonCapture.Size = new System.Drawing.Size(112, 23);
            this.buttonCapture.TabIndex = 4;
            this.buttonCapture.Text = "Capture Frame";
            this.buttonCapture.UseVisualStyleBackColor = true;
            this.buttonCapture.Click += new System.EventHandler(this.buttonCapture_Click);
            // 
            // groupBoxEffect
            // 
            this.groupBoxEffect.Controls.Add(this.trackBarBlur);
            this.groupBoxEffect.Controls.Add(this.checkBoxContrast);
            this.groupBoxEffect.Controls.Add(this.checkBoxBrightness);
            this.groupBoxEffect.Controls.Add(this.trackBarBrightness);
            this.groupBoxEffect.Controls.Add(this.trackBarContrast);
            this.groupBoxEffect.Controls.Add(this.checkBoxBlur);
            this.groupBoxEffect.Controls.Add(this.checkBoxGrayscale);
            this.groupBoxEffect.Location = new System.Drawing.Point(12, 202);
            this.groupBoxEffect.Name = "groupBoxEffect";
            this.groupBoxEffect.Size = new System.Drawing.Size(200, 289);
            this.groupBoxEffect.TabIndex = 13;
            this.groupBoxEffect.TabStop = false;
            this.groupBoxEffect.Text = "Effect";
            // 
            // trackBarBlur
            // 
            this.trackBarBlur.LargeChange = 6;
            this.trackBarBlur.Location = new System.Drawing.Point(6, 222);
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
            this.checkBoxContrast.Location = new System.Drawing.Point(6, 96);
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
            this.checkBoxBrightness.Location = new System.Drawing.Point(6, 22);
            this.checkBoxBrightness.Name = "checkBoxBrightness";
            this.checkBoxBrightness.Size = new System.Drawing.Size(81, 19);
            this.checkBoxBrightness.TabIndex = 11;
            this.checkBoxBrightness.Text = "Brightness";
            this.checkBoxBrightness.UseVisualStyleBackColor = true;
            this.checkBoxBrightness.CheckedChanged += new System.EventHandler(this.checkBoxBrightness_CheckedChanged);
            // 
            // trackBarBrightness
            // 
            this.trackBarBrightness.Location = new System.Drawing.Point(6, 45);
            this.trackBarBrightness.Maximum = 100;
            this.trackBarBrightness.Name = "trackBarBrightness";
            this.trackBarBrightness.Size = new System.Drawing.Size(188, 45);
            this.trackBarBrightness.TabIndex = 7;
            this.trackBarBrightness.Value = 50;
            this.trackBarBrightness.ValueChanged += new System.EventHandler(this.TrackBarBrightness_ValueChanged);
            // 
            // trackBarContrast
            // 
            this.trackBarContrast.Location = new System.Drawing.Point(6, 121);
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
            this.checkBoxBlur.Location = new System.Drawing.Point(6, 197);
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
            this.checkBoxGrayscale.Location = new System.Drawing.Point(6, 172);
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
            this.labelStatus.Location = new System.Drawing.Point(0, 633);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(984, 15);
            this.labelStatus.TabIndex = 5;
            this.labelStatus.Text = "Status";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 648);
            this.Controls.Add(this.pictureBoxPreview);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelStatus);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(1000, 521);
            this.Name = "MainForm";
            this.Text = "Deep Cam Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBoxDetails.ResumeLayout(false);
            this.groupBoxCapture.ResumeLayout(false);
            this.groupBoxEffect.ResumeLayout(false);
            this.groupBoxEffect.PerformLayout();
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
        private GroupBox groupBoxEffect;
        private GroupBox groupBoxDetails;
        private GroupBox groupBoxCapture;
        private Label labelStatus;
        private TrackBar trackBarBrightness;
        private TrackBar trackBarContrast;
        private CheckBox checkBoxContrast;
        private CheckBox checkBoxBrightness;
        private TrackBar trackBarBlur;
    }
}