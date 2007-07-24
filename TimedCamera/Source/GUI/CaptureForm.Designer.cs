namespace RobotAgent.GUI
{
    partial class CaptureForm
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
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.ptcCamera = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // txtDescription
            // 
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.Location = new System.Drawing.Point(0, 0);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(240, 294);
            this.txtDescription.TabIndex = 0;
            // 
            // ptcCamera
            // 
            this.ptcCamera.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ptcCamera.Location = new System.Drawing.Point(0, 0);
            this.ptcCamera.Name = "ptcCamera";
            this.ptcCamera.Size = new System.Drawing.Size(240, 294);
            this.ptcCamera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // CaptureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.ptcCamera);
            this.Controls.Add(this.txtDescription);
            this.MinimizeBox = false;
            this.Name = "CaptureForm";
            this.Text = "Capture";
            this.Load += new System.EventHandler(this.CaptureForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.PictureBox ptcCamera;

    }
}

