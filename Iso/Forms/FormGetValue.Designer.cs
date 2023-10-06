
namespace Iso
{
    partial class FormGetValue
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
            this.groupOptions = new System.Windows.Forms.GroupBox();
            this.rbDeformacao = new System.Windows.Forms.RadioButton();
            this.rbRotacao = new System.Windows.Forms.RadioButton();
            this.rbCortante = new System.Windows.Forms.RadioButton();
            this.rbMomento = new System.Windows.Forms.RadioButton();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.lblPoint = new Iso.LabelText();
            this.tbPoint = new Iso.DoubleTextBox();
            this.groupOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupOptions
            // 
            this.groupOptions.Controls.Add(this.rbDeformacao);
            this.groupOptions.Controls.Add(this.rbRotacao);
            this.groupOptions.Controls.Add(this.rbCortante);
            this.groupOptions.Controls.Add(this.rbMomento);
            this.groupOptions.Location = new System.Drawing.Point(32, 46);
            this.groupOptions.Name = "groupOptions";
            this.groupOptions.Size = new System.Drawing.Size(298, 118);
            this.groupOptions.TabIndex = 2;
            this.groupOptions.TabStop = false;
            this.groupOptions.Text = "Valor a ser encontrado";
            // 
            // rbDeformacao
            // 
            this.rbDeformacao.AutoSize = true;
            this.rbDeformacao.Location = new System.Drawing.Point(7, 89);
            this.rbDeformacao.Name = "rbDeformacao";
            this.rbDeformacao.Size = new System.Drawing.Size(120, 17);
            this.rbDeformacao.TabIndex = 3;
            this.rbDeformacao.TabStop = true;
            this.rbDeformacao.Text = "Deformação vertical";
            this.rbDeformacao.UseVisualStyleBackColor = true;
            this.rbDeformacao.CheckedChanged += new System.EventHandler(this.rbDeformacao_CheckedChanged);
            // 
            // rbRotacao
            // 
            this.rbRotacao.AutoSize = true;
            this.rbRotacao.Location = new System.Drawing.Point(7, 66);
            this.rbRotacao.Name = "rbRotacao";
            this.rbRotacao.Size = new System.Drawing.Size(66, 17);
            this.rbRotacao.TabIndex = 2;
            this.rbRotacao.TabStop = true;
            this.rbRotacao.Text = "Rotação";
            this.rbRotacao.UseVisualStyleBackColor = true;
            this.rbRotacao.CheckedChanged += new System.EventHandler(this.rbRotacao_CheckedChanged);
            // 
            // rbCortante
            // 
            this.rbCortante.AutoSize = true;
            this.rbCortante.Location = new System.Drawing.Point(7, 43);
            this.rbCortante.Name = "rbCortante";
            this.rbCortante.Size = new System.Drawing.Size(65, 17);
            this.rbCortante.TabIndex = 1;
            this.rbCortante.TabStop = true;
            this.rbCortante.Text = "Cortante";
            this.rbCortante.UseVisualStyleBackColor = true;
            this.rbCortante.CheckedChanged += new System.EventHandler(this.rbCortante_CheckedChanged);
            // 
            // rbMomento
            // 
            this.rbMomento.AutoSize = true;
            this.rbMomento.Location = new System.Drawing.Point(7, 20);
            this.rbMomento.Name = "rbMomento";
            this.rbMomento.Size = new System.Drawing.Size(69, 17);
            this.rbMomento.TabIndex = 0;
            this.rbMomento.TabStop = true;
            this.rbMomento.Text = "Momento";
            this.rbMomento.UseVisualStyleBackColor = true;
            this.rbMomento.CheckedChanged += new System.EventHandler(this.rbMomento_CheckedChanged);
            // 
            // tbResult
            // 
            this.tbResult.BackColor = System.Drawing.SystemColors.Window;
            this.tbResult.Enabled = false;
            this.tbResult.Location = new System.Drawing.Point(105, 184);
            this.tbResult.Name = "tbResult";
            this.tbResult.Size = new System.Drawing.Size(152, 20);
            this.tbResult.TabIndex = 3;
            // 
            // lblPoint
            // 
            this.lblPoint.AutoSize = true;
            this.lblPoint.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblPoint.Location = new System.Drawing.Point(68, 19);
            this.lblPoint.Name = "lblPoint";
            this.lblPoint.Size = new System.Drawing.Size(125, 14);
            this.lblPoint.TabIndex = 1;
            this.lblPoint.Text = "Ponto (valor de x):";
            // 
            // tbPoint
            // 
            this.tbPoint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.tbPoint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPoint.Location = new System.Drawing.Point(201, 18);
            this.tbPoint.Name = "tbPoint";
            this.tbPoint.Size = new System.Drawing.Size(68, 20);
            this.tbPoint.TabIndex = 0;
            this.tbPoint.Text = "0,00";
            this.tbPoint.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbPoint.TextChanged += new System.EventHandler(this.tbPoint_TextChanged);
            // 
            // FormGetValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 216);
            this.Controls.Add(this.tbResult);
            this.Controls.Add(this.groupOptions);
            this.Controls.Add(this.lblPoint);
            this.Controls.Add(this.tbPoint);
            this.Name = "FormGetValue";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Encontrar valor no ponto";
            this.groupOptions.ResumeLayout(false);
            this.groupOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DoubleTextBox tbPoint;
        private LabelText lblPoint;
        private System.Windows.Forms.GroupBox groupOptions;
        private System.Windows.Forms.RadioButton rbDeformacao;
        private System.Windows.Forms.RadioButton rbRotacao;
        private System.Windows.Forms.RadioButton rbCortante;
        private System.Windows.Forms.RadioButton rbMomento;
        private System.Windows.Forms.TextBox tbResult;
    }
}