namespace NDde.Test.Forms
{
    partial class Form1
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
            this.grid = new System.Windows.Forms.DataGridView();
            this.Ativo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ultima = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantidade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Abertura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fechamento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Variacao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VolumeFinanceiro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Minimo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Maximo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumeroNegocios = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VolumeProjetado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataHora = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Ativo,
            this.Ultima,
            this.Quantidade,
            this.Abertura,
            this.Fechamento,
            this.Variacao,
            this.VolumeFinanceiro,
            this.Minimo,
            this.Maximo,
            this.NumeroNegocios,
            this.VolumeProjetado,
            this.DataHora});
            this.grid.Location = new System.Drawing.Point(12, 12);
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.Size = new System.Drawing.Size(1258, 406);
            this.grid.TabIndex = 0;
            // 
            // Ativo
            // 
            this.Ativo.HeaderText = "Ativo";
            this.Ativo.Name = "Ativo";
            this.Ativo.ReadOnly = true;
            // 
            // Ultima
            // 
            this.Ultima.HeaderText = "Ultima";
            this.Ultima.Name = "Ultima";
            this.Ultima.ReadOnly = true;
            // 
            // Quantidade
            // 
            this.Quantidade.HeaderText = "Quantidade";
            this.Quantidade.Name = "Quantidade";
            this.Quantidade.ReadOnly = true;
            // 
            // Abertura
            // 
            this.Abertura.HeaderText = "Abertura";
            this.Abertura.Name = "Abertura";
            this.Abertura.ReadOnly = true;
            // 
            // Fechamento
            // 
            this.Fechamento.HeaderText = "FechamentoAnterior";
            this.Fechamento.Name = "Fechamento";
            this.Fechamento.ReadOnly = true;
            // 
            // Variacao
            // 
            this.Variacao.HeaderText = "Variacao";
            this.Variacao.Name = "Variacao";
            this.Variacao.ReadOnly = true;
            // 
            // VolumeFinanceiro
            // 
            this.VolumeFinanceiro.HeaderText = "VolumeFinanceiro";
            this.VolumeFinanceiro.Name = "VolumeFinanceiro";
            this.VolumeFinanceiro.ReadOnly = true;
            // 
            // Minimo
            // 
            this.Minimo.HeaderText = "Minimo";
            this.Minimo.Name = "Minimo";
            this.Minimo.ReadOnly = true;
            // 
            // Maximo
            // 
            this.Maximo.HeaderText = "Maximo";
            this.Maximo.Name = "Maximo";
            this.Maximo.ReadOnly = true;
            // 
            // NumeroNegocios
            // 
            this.NumeroNegocios.HeaderText = "NumeroNegocios";
            this.NumeroNegocios.Name = "NumeroNegocios";
            this.NumeroNegocios.ReadOnly = true;
            // 
            // VolumeProjetado
            // 
            this.VolumeProjetado.HeaderText = "VolumeProjetado";
            this.VolumeProjetado.Name = "VolumeProjetado";
            this.VolumeProjetado.ReadOnly = true;
            // 
            // DataHora
            // 
            this.DataHora.HeaderText = "DataHora";
            this.DataHora.Name = "DataHora";
            this.DataHora.ReadOnly = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1282, 430);
            this.Controls.Add(this.grid);
            this.Name = "Form1";
            this.Text = "Teste XPPro";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ativo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ultima;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantidade;
        private System.Windows.Forms.DataGridViewTextBoxColumn Abertura;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fechamento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Variacao;
        private System.Windows.Forms.DataGridViewTextBoxColumn VolumeFinanceiro;
        private System.Windows.Forms.DataGridViewTextBoxColumn Minimo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Maximo;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumeroNegocios;
        private System.Windows.Forms.DataGridViewTextBoxColumn VolumeProjetado;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataHora;

    }
}

