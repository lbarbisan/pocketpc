namespace CameraClient
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pctCamera = new System.Windows.Forms.PictureBox();
            this.btnCapture = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pctCamera)).BeginInit();
            this.SuspendLayout();
            // 
            // pctCamera
            // 
            this.pctCamera.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pctCamera.Location = new System.Drawing.Point(0, 0);
            this.pctCamera.Name = "pctCamera";
            this.pctCamera.Size = new System.Drawing.Size(292, 266);
            this.pctCamera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pctCamera.TabIndex = 0;
            this.pctCamera.TabStop = false;
            // 
            // btnCapture
            // 
            this.btnCapture.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnCapture.Location = new System.Drawing.Point(0, 220);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(292, 46);
            this.btnCapture.TabIndex = 1;
            this.btnCapture.Text = "Capture";
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.btnCapture);
            this.Controls.Add(this.pctCamera);
            this.Name = "MainForm";
            this.Text = "PocketPc Camera";
            ((System.ComponentModel.ISupportInitialize)(this.pctCamera)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pctCamera;
        private System.Windows.Forms.Button btnCapture;
    }
}

