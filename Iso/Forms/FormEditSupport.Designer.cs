
namespace Iso
{
    partial class FormEditSupport
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
            this.btnOk = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.PictureBox();
            this.labelTitle = new Iso.LabelText();
            this.rbMovel = new System.Windows.Forms.RadioButton();
            this.rbFixo = new System.Windows.Forms.RadioButton();
            this.rbEngaste = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.btnOk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.BackColor = System.Drawing.Color.Transparent;
            this.btnOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOk.Location = new System.Drawing.Point(135, 170);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(68, 31);
            this.btnOk.TabIndex = 1;
            this.btnOk.TabStop = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            this.btnOk.MouseEnter += new System.EventHandler(this.btnOk_MouseEnter);
            this.btnOk.MouseLeave += new System.EventHandler(this.btnOk_MouseLeave);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.Location = new System.Drawing.Point(12, 170);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 31);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.TabStop = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.MouseEnter += new System.EventHandler(this.btnCancel_MouseEnter);
            this.btnCancel.MouseLeave += new System.EventHandler(this.btnCancel_MouseLeave);
            // 
            // labelTitle
            // 
            this.labelTitle.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.labelTitle.Location = new System.Drawing.Point(12, 9);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(191, 22);
            this.labelTitle.TabIndex = 14;
            this.labelTitle.Text = "Editar suporte";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rbMovel
            // 
            this.rbMovel.AutoSize = true;
            this.rbMovel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbMovel.Location = new System.Drawing.Point(66, 70);
            this.rbMovel.Name = "rbMovel";
            this.rbMovel.Size = new System.Drawing.Size(96, 17);
            this.rbMovel.TabIndex = 15;
            this.rbMovel.TabStop = true;
            this.rbMovel.Text = "Apoio móvel";
            this.rbMovel.UseVisualStyleBackColor = true;
            // 
            // rbFixo
            // 
            this.rbFixo.AutoSize = true;
            this.rbFixo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbFixo.Location = new System.Drawing.Point(66, 93);
            this.rbFixo.Name = "rbFixo";
            this.rbFixo.Size = new System.Drawing.Size(82, 17);
            this.rbFixo.TabIndex = 16;
            this.rbFixo.TabStop = true;
            this.rbFixo.Text = "Apoio fixo";
            this.rbFixo.UseVisualStyleBackColor = true;
            // 
            // rbEngaste
            // 
            this.rbEngaste.AutoSize = true;
            this.rbEngaste.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbEngaste.Location = new System.Drawing.Point(66, 116);
            this.rbEngaste.Name = "rbEngaste";
            this.rbEngaste.Size = new System.Drawing.Size(70, 17);
            this.rbEngaste.TabIndex = 17;
            this.rbEngaste.TabStop = true;
            this.rbEngaste.Text = "Engaste";
            this.rbEngaste.UseVisualStyleBackColor = true;
            // 
            // FormEditSupport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(215, 213);
            this.ControlBox = false;
            this.Controls.Add(this.rbEngaste);
            this.Controls.Add(this.rbFixo);
            this.Controls.Add(this.rbMovel);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEditSupport";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.btnOk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox btnOk;
        private System.Windows.Forms.PictureBox btnCancel;
        private LabelText labelTitle;
        private System.Windows.Forms.RadioButton rbMovel;
        private System.Windows.Forms.RadioButton rbFixo;
        private System.Windows.Forms.RadioButton rbEngaste;
    }
}