
namespace Iso
{
    partial class FormAddDistLoad
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
            this.labelP2 = new Iso.LabelText();
            this.labelP1 = new Iso.LabelText();
            this.labelValue = new Iso.LabelText();
            this.tbP2 = new Iso.DoubleTextBox();
            this.tbP1 = new Iso.DoubleTextBox();
            this.tbValue = new Iso.DoubleTextBox();
            this.labelTitle = new Iso.LabelText();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAdd)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.Location = new System.Drawing.Point(12, 162);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 31);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.TabStop = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.MouseEnter += new System.EventHandler(this.btnCancel_MouseEnter);
            this.btnCancel.MouseLeave += new System.EventHandler(this.btnCancel_MouseLeave);
            // 
            // btnAdd
            // 
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.Location = new System.Drawing.Point(189, 162);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(68, 31);
            this.btnAdd.TabIndex = 11;
            this.btnAdd.TabStop = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.btnAdd.MouseEnter += new System.EventHandler(this.btnAdd_MouseEnter);
            this.btnAdd.MouseLeave += new System.EventHandler(this.btnAdd_MouseLeave);
            // 
            // labelP2
            // 
            this.labelP2.AutoSize = true;
            this.labelP2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.labelP2.Location = new System.Drawing.Point(102, 113);
            this.labelP2.Name = "labelP2";
            this.labelP2.Size = new System.Drawing.Size(49, 15);
            this.labelP2.TabIndex = 18;
            this.labelP2.Text = "Até (m):";
            // 
            // labelP1
            // 
            this.labelP1.AutoSize = true;
            this.labelP1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.labelP1.Location = new System.Drawing.Point(103, 86);
            this.labelP1.Name = "labelP1";
            this.labelP1.Size = new System.Drawing.Size(48, 15);
            this.labelP1.TabIndex = 17;
            this.labelP1.Text = "De (m):";
            // 
            // labelValue
            // 
            this.labelValue.AutoSize = true;
            this.labelValue.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.labelValue.Location = new System.Drawing.Point(21, 61);
            this.labelValue.Name = "labelValue";
            this.labelValue.Size = new System.Drawing.Size(130, 15);
            this.labelValue.TabIndex = 16;
            this.labelValue.Text = "Carregamento (kN/m):";
            // 
            // tbP2
            // 
            this.tbP2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.tbP2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbP2.Location = new System.Drawing.Point(171, 112);
            this.tbP2.Name = "tbP2";
            this.tbP2.Size = new System.Drawing.Size(86, 20);
            this.tbP2.TabIndex = 15;
            this.tbP2.Text = "0,00";
            this.tbP2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbP1
            // 
            this.tbP1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.tbP1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbP1.Location = new System.Drawing.Point(171, 86);
            this.tbP1.Name = "tbP1";
            this.tbP1.Size = new System.Drawing.Size(86, 20);
            this.tbP1.TabIndex = 14;
            this.tbP1.Text = "0,00";
            this.tbP1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbValue
            // 
            this.tbValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.tbValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbValue.Location = new System.Drawing.Point(171, 60);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(86, 20);
            this.tbValue.TabIndex = 13;
            this.tbValue.Text = "0,00";
            this.tbValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelTitle
            // 
            this.labelTitle.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.labelTitle.Location = new System.Drawing.Point(23, 13);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(233, 22);
            this.labelTitle.TabIndex = 19;
            this.labelTitle.Text = "Adicionar carga distribuída";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormAddDistLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 214);
            this.ControlBox = false;
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.labelP2);
            this.Controls.Add(this.labelP1);
            this.Controls.Add(this.labelValue);
            this.Controls.Add(this.tbP2);
            this.Controls.Add(this.tbP1);
            this.Controls.Add(this.tbValue);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAdd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAddDistLoad";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
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
        private LabelText labelValue;
        private DoubleTextBox tbP2;
        private DoubleTextBox tbP1;
        private DoubleTextBox tbValue;
        private LabelText labelTitle;
    }
}