namespace MetroForm
{
    partial class frmMetrolinkHost
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
            this.button2 = new System.Windows.Forms.Button();
            this.WPFelementHost = new System.Windows.Forms.Integration.ElementHost();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(77, 29);
            this.button2.TabIndex = 1;
            this.button2.Text = "Close";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // WPFelementHost
            // 
            this.WPFelementHost.AutoSize = true;
            this.WPFelementHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WPFelementHost.Location = new System.Drawing.Point(0, 0);
            this.WPFelementHost.Name = "WPFelementHost";
            this.WPFelementHost.Size = new System.Drawing.Size(284, 261);
            this.WPFelementHost.TabIndex = 2;
            this.WPFelementHost.Text = "WPFelementHost";
            this.WPFelementHost.DockChanged += new System.EventHandler(this.WPFelementHost_DockChanged);
            this.WPFelementHost.Child = null;
            // 
            // frmMetrolinkHost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.WPFelementHost);
            this.Controls.Add(this.button2);
            this.Name = "frmMetrolinkHost";
            this.Text = "MetrolinkHidden";
            this.Load += new System.EventHandler(this.frmMetroForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Integration.ElementHost WPFelementHost;
    }
}

