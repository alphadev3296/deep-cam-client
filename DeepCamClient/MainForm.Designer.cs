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
            this.groupBoxEffect = new System.Windows.Forms.GroupBox();
            this.labelBrightness = new System.Windows.Forms.Label();
            this.labelContrast = new System.Windows.Forms.Label();
            this.trackBarBrightness = new System.Windows.Forms.TrackBar();
            this.trackBarContrast = new System.Windows.Forms.TrackBar();
            this.checkBoxBlur = new System.Windows.Forms.CheckBox();
            this.checkBoxGrayscale = new System.Windows.Forms.CheckBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.buttonShowInfo = new System.Windows.Forms.Button();
            this.buttonCapture = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.groupBoxCapture = new System.Windows.Forms.GroupBox();
            this.groupBoxDetails = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBoxEffect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarContrast)).BeginInit();
            this.groupBoxCapture.SuspendLayout();
            this.groupBoxDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxDevices
            // 
            this.comboBoxDevices.FormattingEnabled = true;
            this.comboBoxDevices.Location = new System.Drawing.Point(12, 12);
            this.comboBoxDevices.Name = "comboBoxDevices";
            this.comboBoxDevices.Size = new System.Drawing.Size(112, 23);
            this.comboBoxDevices.TabIndex = 0;
            // 
            // buttonRefreshDevices
            // 
            this.buttonRefreshDevices.Location = new System.Drawing.Point(137, 12);
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
            this.pictureBoxPreview.Size = new System.Drawing.Size(778, 535);
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
            this.panel1.Size = new System.Drawing.Size(227, 535);
            this.panel1.TabIndex = 4;
            // 
            // groupBoxEffect
            // 
            this.groupBoxEffect.Controls.Add(this.labelBrightness);
            this.groupBoxEffect.Controls.Add(this.labelContrast);
            this.groupBoxEffect.Controls.Add(this.trackBarBrightness);
            this.groupBoxEffect.Controls.Add(this.trackBarContrast);
            this.groupBoxEffect.Controls.Add(this.checkBoxBlur);
            this.groupBoxEffect.Controls.Add(this.checkBoxGrayscale);
            this.groupBoxEffect.Location = new System.Drawing.Point(12, 173);
            this.groupBoxEffect.Name = "groupBoxEffect";
            this.groupBoxEffect.Size = new System.Drawing.Size(200, 218);
            this.groupBoxEffect.TabIndex = 13;
            this.groupBoxEffect.TabStop = false;
            this.groupBoxEffect.Text = "Effect";
            // 
            // labelBrightness
            // 
            this.labelBrightness.AutoSize = true;
            this.labelBrightness.Location = new System.Drawing.Point(6, 27);
            this.labelBrightness.Name = "labelBrightness";
            this.labelBrightness.Size = new System.Drawing.Size(62, 15);
            this.labelBrightness.TabIndex = 11;
            this.labelBrightness.Text = "Brightness";
            // 
            // labelContrast
            // 
            this.labelContrast.AutoSize = true;
            this.labelContrast.Location = new System.Drawing.Point(12, 93);
            this.labelContrast.Name = "labelContrast";
            this.labelContrast.Size = new System.Drawing.Size(52, 15);
            this.labelContrast.TabIndex = 12;
            this.labelContrast.Text = "Contrast";
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
            this.trackBarContrast.Location = new System.Drawing.Point(6, 111);
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
            this.checkBoxBlur.Location = new System.Drawing.Point(6, 187);
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
            this.checkBoxGrayscale.Location = new System.Drawing.Point(6, 162);
            this.checkBoxGrayscale.Name = "checkBoxGrayscale";
            this.checkBoxGrayscale.Size = new System.Drawing.Size(80, 19);
            this.checkBoxGrayscale.TabIndex = 9;
            this.checkBoxGrayscale.Text = "Gray Scale";
            this.checkBoxGrayscale.UseVisualStyleBackColor = true;
            this.checkBoxGrayscale.CheckedChanged += new System.EventHandler(this.CheckBoxGrayscale_CheckedChanged);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(6, 48);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(39, 15);
            this.labelStatus.TabIndex = 6;
            this.labelStatus.Text = "Status";
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
            // groupBoxCapture
            // 
            this.groupBoxCapture.Controls.Add(this.buttonStart);
            this.groupBoxCapture.Controls.Add(this.buttonStop);
            this.groupBoxCapture.Controls.Add(this.buttonCapture);
            this.groupBoxCapture.Location = new System.Drawing.Point(12, 41);
            this.groupBoxCapture.Name = "groupBoxCapture";
            this.groupBoxCapture.Size = new System.Drawing.Size(200, 126);
            this.groupBoxCapture.TabIndex = 14;
            this.groupBoxCapture.TabStop = false;
            this.groupBoxCapture.Text = "Capture";
            // 
            // groupBoxDetails
            // 
            this.groupBoxDetails.Controls.Add(this.buttonShowInfo);
            this.groupBoxDetails.Controls.Add(this.labelStatus);
            this.groupBoxDetails.Location = new System.Drawing.Point(12, 397);
            this.groupBoxDetails.Name = "groupBoxDetails";
            this.groupBoxDetails.Size = new System.Drawing.Size(200, 100);
            this.groupBoxDetails.TabIndex = 15;
            this.groupBoxDetails.TabStop = false;
            this.groupBoxDetails.Text = "Details";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 535);
            this.Controls.Add(this.pictureBoxPreview);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Name = "MainForm";
            this.Text = "Deep Cam Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBoxEffect.ResumeLayout(false);
            this.groupBoxEffect.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarContrast)).EndInit();
            this.groupBoxCapture.ResumeLayout(false);
            this.groupBoxDetails.ResumeLayout(false);
            this.groupBoxDetails.PerformLayout();
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
        private TrackBar trackBarContrast;
        private TrackBar trackBarBrightness;
        private Label labelStatus;
        private Button buttonShowInfo;
        private Button buttonCapture;
        private GroupBox groupBoxEffect;
        private Label labelBrightness;
        private Label labelContrast;
        private GroupBox groupBoxDetails;
        private GroupBox groupBoxCapture;
    }
}