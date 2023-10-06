
namespace Iso
{
    partial class FormAddNUDistLoad
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
            this.btnCancel = new System.Windows.Forms.PictureBox();
            this.btnAdd = new System.Windows.Forms.PictureBox();
            this.labelTitle = new Iso.LabelText();
            this.labelP2 = new Iso.LabelText();
            this.labelP1 = new Iso.LabelText();
            this.labelValue1 = new Iso.LabelText();
            this.tbP2 = new Iso.DoubleTextBox();
            this.tbP1 = new Iso.DoubleTextBox();
            this.tbValue1 = new Iso.DoubleTextBox();
            this.labelValue2 = new Iso.LabelText();
            this.tbValue2 = new Iso.DoubleTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAdd)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.Location = new System.Drawing.Point(12, 188);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 31);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.TabStop = false;
            // 
            // btnAdd
            // 
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.Location = new System.Drawing.Point(187, 188);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(68, 31);
            this.btnAdd.TabIndex = 13;
            this.btnAdd.TabStop = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // labelTitle
            // 
            this.labelTitle.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.labelTitle.Location = new System.Drawing.Point(15, 9);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(233, 22);
            this.labelTitle.TabIndex = 25;
            this.labelTitle.Text = "Adicionar carga distribuída";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelP2
            // 
            this.labelP2.AutoSize = true;
            this.labelP2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.labelP2.Location = new System.Drawing.Point(93, 109);
            this.labelP2.Name = "labelP2";
            this.labelP2.Size = new System.Drawing.Size(49, 15);
            this.labelP2.TabIndex = 24;
            this.labelP2.Text = "Até (m):";
            // 
            // labelP1
            // 
            this.labelP1.AutoSize = true;
            this.labelP1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.labelP1.Location = new System.Drawing.Point(94, 56);
            this.labelP1.Name = "labelP1";
            this.labelP1.Size = new System.Drawing.Size(48, 15);
            this.labelP1.TabIndex = 23;
            this.labelP1.Text = "De (m):";
            // 
            // labelValue1
            // 
            this.labelValue1.AutoSize = true;
            this.labelValue1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.labelValue1.Location = new System.Drawing.Point(12, 83);
            this.labelValue1.Name = "labelValue1";
            this.labelValue1.Size = new System.Drawing.Size(130, 15);
            this.labelValue1.TabIndex = 22;
            this.labelValue1.Text = "Carregamento (kN/m):";
            // 
            // tbP2
            // 
            this.tbP2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.tbP2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbP2.Location = new System.Drawing.Point(162, 108);
            this.tbP2.Name = "tbP2";
            this.tbP2.Size = new System.Drawing.Size(86, 20);
            this.tbP2.TabIndex = 21;
            this.tbP2.Text = "0,00";
            this.tbP2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbP1
            // 
            this.tbP1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.tbP1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbP1.Location = new System.Drawing.Point(162, 56);
            this.tbP1.Name = "tbP1";
            this.tbP1.Size = new System.Drawing.Size(86, 20);
            this.tbP1.TabIndex = 20;
            this.tbP1.Text = "0,00";
            this.tbP1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbValue1
            // 
            this.tbValue1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.tbValue1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbValue1.Location = new System.Drawing.Point(162, 82);
            this.tbValue1.Name = "tbValue1";
            this.tbValue1.Size = new System.Drawing.Size(86, 20);
            this.tbValue1.TabIndex = 19;
            this.tbValue1.Text = "0,00";
            this.tbValue1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelValue2
            // 
            this.labelValue2.AutoSize = true;
            this.labelValue2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.labelValue2.Location = new System.Drawing.Point(12, 135);
            this.labelValue2.Name = "labelValue2";
            this.labelValue2.Size = new System.Drawing.Size(130, 15);
            this.labelValue2.TabIndex = 27;
            this.labelValue2.Text = "Carregamento (kN/m):";
            // 
            // tbValue2
            // 
            this.tbValue2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.tbValue2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbValue2.Location = new System.Drawing.Point(162, 134);
            this.tbValue2.Name = "tbValue2";
            this.tbValue2.Size = new System.Drawing.Size(86, 20);
            this.tbValue2.TabIndex = 26;
            this.tbValue2.Text = "0,00";
            this.tbValue2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FormAddNUDistLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 232);
            this.ControlBox = false;
            this.Controls.Add(this.labelValue2);
            this.Controls.Add(this.tbValue2);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.labelP2);
            this.Controls.Add(this.labelP1);
            this.Controls.Add(this.labelValue1);
            this.Controls.Add(this.tbP2);
            this.Controls.Add(this.tbP1);
            this.Controls.Add(this.tbValue1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAdd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormAddNUDistLoad";
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAdd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox btnCancel;
        private System.Windows.Forms.PictureBox btnAdd;
        private LabelText labelP2;
        private LabelText labelP1;
        private LabelText labelValue1;
        private DoubleTextBox tbP2;
        private DoubleTextBox tbP1;
        private DoubleTextBox tbValue1;
        private LabelText labelTitle;
        private LabelText labelValue2;
        private DoubleTextBox tbValue2;
    }
}