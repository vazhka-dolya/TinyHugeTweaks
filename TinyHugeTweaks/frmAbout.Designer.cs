namespace TinyHugeTweaks
{
    partial class frmAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
            this.label1 = new System.Windows.Forms.Label();
            this.lbVer = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.htmlCredits = new TheArtOfDev.HtmlRenderer.WinForms.HtmlLabel();
            this.labelLicense = new System.Windows.Forms.Label();
            this.pictureIcon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // lbVer
            // 
            resources.ApplyResources(this.lbVer, "lbVer");
            this.lbVer.Name = "lbVer";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            // 
            // htmlCredits
            // 
            this.htmlCredits.BackColor = System.Drawing.Color.Transparent;
            this.htmlCredits.BaseStylesheet = null;
            resources.ApplyResources(this.htmlCredits, "htmlCredits");
            this.htmlCredits.Name = "htmlCredits";
            // 
            // labelLicense
            // 
            resources.ApplyResources(this.labelLicense, "labelLicense");
            this.labelLicense.Name = "labelLicense";
            // 
            // pictureIcon
            // 
            this.pictureIcon.BackgroundImage = global::TinyHugeTweaks.Properties.Resources.tbk_icon_m;
            resources.ApplyResources(this.pictureIcon, "pictureIcon");
            this.pictureIcon.Name = "pictureIcon";
            this.pictureIcon.TabStop = false;
            // 
            // frmAbout
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelLicense);
            this.Controls.Add(this.pictureIcon);
            this.Controls.Add(this.htmlCredits);
            this.Controls.Add(this.lbVer);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAbout";
            ((System.ComponentModel.ISupportInitialize)(this.pictureIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbVer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private TheArtOfDev.HtmlRenderer.WinForms.HtmlLabel htmlCredits;
        private System.Windows.Forms.PictureBox pictureIcon;
        private System.Windows.Forms.Label labelLicense;
    }
}