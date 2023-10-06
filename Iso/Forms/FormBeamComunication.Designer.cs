
namespace Iso
{
    partial class FormBeamComunication
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
            this.btnOk = new System.Windows.Forms.PictureBox();
            this.rbNoCom = new System.Windows.Forms.RadioButton();
            this.rbParentSupChild = new System.Windows.Forms.RadioButton();
            this.rbChildSupParent = new System.Windows.Forms.RadioButton();
            this.labelTitle = new Iso.LabelText();
            this.rbAuto = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOk)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.Location = new System.Drawing.Point(9, 162);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 31);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.TabStop = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.MouseEnter += new System.EventHandler(this.btnCancel_MouseEnter);
            this.btnCancel.MouseLeave += new System.EventHandler(this.btnCancel_MouseLeave);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.BackColor = System.Drawing.Color.Transparent;
            this.btnOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOk.Location = new System.Drawing.Point(129, 162);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(68, 31);
            this.btnOk.TabIndex = 14;
            this.btnOk.TabStop = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            this.btnOk.MouseEnter += new System.EventHandler(this.btnOk_MouseEnter);
            this.btnOk.MouseLeave += new System.EventHandler(this.btnOk_MouseLeave);
            // 
            // rbNoCom
            // 
            this.rbNoCom.AutoSize = true;
            this.rbNoCom.Location = new System.Drawing.Point(32, 76);
            this.rbNoCom.Name = "rbNoCom";
            this.rbNoCom.Size = new System.Drawing.Size(138, 17);
            this.rbNoCom.TabIndex = 17;
            this.rbNoCom.Text = "Nenhuma comunicação";
            this.rbNoCom.UseVisualStyleBackColor = true;
            // 
            // rbParentSupChild
            // 
            this.rbParentSupChild.AutoSize = true;
            this.rbParentSupChild.Location = new System.Drawing.Point(32, 99);
            this.rbParentSupChild.Name = "rbParentSupChild";
            this.rbParentSupChild.Size = new System.Drawing.Size(133, 17);
            this.rbParentSupChild.TabIndex = 18;
            this.rbParentSupChild.Text = "Parent supporting child";
            this.rbParentSupChild.UseVisualStyleBackColor = true;
            // 
            // rbChildSupParent
            // 
            this.rbChildSupParent.AutoSize = true;
            this.rbChildSupParent.Location = new System.Drawing.Point(32, 122);
            this.rbChildSupParent.Name = "rbChildSupParent";
            this.rbChildSupParent.Size = new System.Drawing.Size(133, 17);
            this.rbChildSupParent.TabIndex = 19;
            this.rbChildSupParent.Text = "Child supporting parent";
            this.rbChildSupParent.UseVisualStyleBackColor = true;
            // 
            // labelTitle
            // 
            this.labelTitle.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.labelTitle.Location = new System.Drawing.Point(6, 9);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(191, 22);
            this.labelTitle.TabIndex = 16;
            this.labelTitle.Text = "Comunicação entre as vigas";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rbAuto
            // 
            this.rbAuto.AutoSize = true;
            this.rbAuto.Checked = true;
            this.rbAuto.Location = new System.Drawing.Point(32, 53);
            this.rbAuto.Name = "rbAuto";
            this.rbAuto.Size = new System.Drawing.Size(78, 17);
            this.rbAuto.TabIndex = 20;
            this.rbAuto.TabStop = true;
            this.rbAuto.Text = "Automático";
            this.rbAuto.UseVisualStyleBackColor = true;
            // 
            // FormBeamComunication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(202, 205);
            this.ControlBox = false;
            this.Controls.Add(this.rbAuto);
            this.Controls.Add(this.rbChildSupParent);
            this.Controls.Add(this.rbParentSupChild);
            this.Controls.Add(this.rbNoCom);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormBeamComunication";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOk)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox btnCancel;
        private System.Windows.Forms.PictureBox btnOk;
        private LabelText labelTitle;
        private System.Windows.Forms.RadioButton rbNoCom;
        private System.Windows.Forms.RadioButton rbParentSupChild;
        private System.Windows.Forms.RadioButton rbChildSupParent;
        private System.Windows.Forms.RadioButton rbAuto;
    }
}