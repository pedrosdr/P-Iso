
namespace Iso
{
    partial class FormInit
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
            this.components = new System.ComponentModel.Container();
            this.timerInit = new System.Windows.Forms.Timer(this.components);
            this.labelInit = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timerInit
            // 
            this.timerInit.Enabled = true;
            this.timerInit.Interval = 1200;
            this.timerInit.Tick += new System.EventHandler(this.timerInit_Tick);
            // 
            // labelInit
            // 
            this.labelInit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelInit.BackColor = System.Drawing.Color.Transparent;
            this.labelInit.ForeColor = System.Drawing.Color.White;
            this.labelInit.Location = new System.Drawing.Point(635, 418);
            this.labelInit.Name = "labelInit";
            this.labelInit.Size = new System.Drawing.Size(153, 23);
            this.labelInit.TabIndex = 0;
            this.labelInit.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // FormInit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.labelInit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormInit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerInit;
        private System.Windows.Forms.Label labelInit;
    }
}