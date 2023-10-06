
namespace Iso
{
    partial class FormAddLoadColumn
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
            this.labelPosition = new Iso.LabelText();
            this.labelVectY = new Iso.LabelText();
            this.tbPosition = new Iso.DoubleTextBox();
            this.tbVectY = new Iso.DoubleTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAdd)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.Location = new System.Drawing.Point(12, 113);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 31);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.TabStop = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.MouseEnter += new System.EventHandler(this.btnCancel_MouseEnter);
            this.btnCancel.MouseLeave += new System.EventHandler(this.btnCancel_MouseLeave);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.Location = new System.Drawing.Point(157, 113);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(68, 31);
            this.btnAdd.TabIndex = 21;
            this.btnAdd.TabStop = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.btnAdd.MouseEnter += new System.EventHandler(this.btnAdd_MouseEnter);
            this.btnAdd.MouseLeave += new System.EventHandler(this.btnAdd_MouseLeave);
            // 
            // labelTitle
            // 
            this.labelTitle.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.labelTitle.Location = new System.Drawing.Point(12, 9);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(213, 22);
            this.labelTitle.TabIndex = 20;
            this.labelTitle.Text = "Adicionar carregamento";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelPosition
            // 
            this.labelPosition.AutoSize = true;
            this.labelPosition.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.labelPosition.Location = new System.Drawing.Point(50, 77);
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.Size = new System.Drawing.Size(63, 15);
            this.labelPosition.TabIndex = 19;
            this.labelPosition.Text = "Altura (m):";
            // 
            // labelVectY
            // 
            this.labelVectY.AutoSize = true;
            this.labelVectY.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.labelVectY.Location = new System.Drawing.Point(15, 51);
            this.labelVectY.Name = "labelVectY";
            this.labelVectY.Size = new System.Drawing.Size(98, 15);
            this.labelVectY.TabIndex = 17;
            this.labelVectY.Text = "Força em Y (kN):";
            // 
            // tbPosition
            // 
            this.tbPosition.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.tbPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPosition.Location = new System.Drawing.Point(135, 76);
            this.tbPosition.Name = "tbPosition";
            this.tbPosition.Size = new System.Drawing.Size(86, 20);
            this.tbPosition.TabIndex = 16;
            this.tbPosition.Text = "0,00";
            this.tbPosition.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbVectY
            // 
            this.tbVectY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.tbVectY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbVectY.Location = new System.Drawing.Point(135, 50);
            this.tbVectY.Name = "tbVectY";
            this.tbVectY.Size = new System.Drawing.Size(86, 20);
            this.tbVectY.TabIndex = 13;
            this.tbVectY.Text = "0,00";
            this.tbVectY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FormAddLoadColumn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 157);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.labelPosition);
            this.Controls.Add(this.labelVectY);
            this.Controls.Add(this.tbPosition);
            this.Controls.Add(this.tbVectY);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAddLoadColumn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAdd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox btnCancel;
        private System.Windows.Forms.PictureBox btnAdd;
        private LabelText labelTitle;
        private LabelText labelPosition;
        private LabelText labelVectY;
        private DoubleTextBox tbPosition;
        private DoubleTextBox tbVectY;
    }
}