using ComponentFactory.Krypton.Toolkit;
namespace NfeGerarXml
{
    partial class frmPesquisaNfe
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPesquisaNfe));
            this.dgvPesquisa = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.cbxCampoPesquisa = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.txtPesquisa = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.lblUF = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblCampoValor = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnPesquisa = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.label1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cbxOperador = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.label2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cbxTipoNf = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.cbxModelo = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.label3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.label4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cbxCampoPesquisaEntrada = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonGroupBox1 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SeqNF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sCli_For = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChaveAcesso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cUF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AAMM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CNPJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPesquisa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxCampoPesquisa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxOperador)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxTipoNf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxModelo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxCampoPesquisaEntrada)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvPesquisa
            // 
            this.dgvPesquisa.AllowUserToAddRows = false;
            this.dgvPesquisa.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvPesquisa.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPesquisa.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.SeqNF,
            this.sCli_For,
            this.ChaveAcesso,
            this.cUF,
            this.AAMM,
            this.CNPJ,
            this.serie});
            this.dgvPesquisa.Location = new System.Drawing.Point(3, 153);
            this.dgvPesquisa.Name = "dgvPesquisa";
            this.dgvPesquisa.ReadOnly = true;
            this.dgvPesquisa.RowTemplate.Height = 19;
            this.dgvPesquisa.Size = new System.Drawing.Size(923, 321);
            this.dgvPesquisa.TabIndex = 0;
            this.dgvPesquisa.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPesquisa_CellDoubleClick);
            // 
            // cbxCampoPesquisa
            // 
            this.cbxCampoPesquisa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCampoPesquisa.DropDownWidth = 147;
            this.cbxCampoPesquisa.FormattingEnabled = true;
            this.cbxCampoPesquisa.Items.AddRange(new object[] {
            "Número NF",
            "Sequência NF",
            "Chave Acesso"});
            this.cbxCampoPesquisa.Location = new System.Drawing.Point(145, 33);
            this.cbxCampoPesquisa.Name = "cbxCampoPesquisa";
            this.cbxCampoPesquisa.Size = new System.Drawing.Size(147, 21);
            this.cbxCampoPesquisa.TabIndex = 1;
            this.cbxCampoPesquisa.SelectionChangeCommitted += new System.EventHandler(this.cbxCampoPesquisa_SelectionChangeCommitted);
            // 
            // txtPesquisa
            // 
            this.txtPesquisa.Location = new System.Drawing.Point(448, 63);
            this.txtPesquisa.Name = "txtPesquisa";
            this.txtPesquisa.Size = new System.Drawing.Size(297, 20);
            this.txtPesquisa.TabIndex = 2;
            this.txtPesquisa.Enter += new System.EventHandler(this.txtPesquisa_Enter);
            this.txtPesquisa.Validating += new System.ComponentModel.CancelEventHandler(this.txtPesquisa_Validating);
            this.txtPesquisa.Validated += new System.EventHandler(this.txtPesquisa_Validated);
            // 
            // lblUF
            // 
            this.lblUF.BackColor = System.Drawing.Color.Lavender;
            this.lblUF.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUF.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
            this.lblUF.Location = new System.Drawing.Point(48, 34);
            this.lblUF.Name = "lblUF";
            this.lblUF.Size = new System.Drawing.Size(91, 20);
            this.lblUF.TabIndex = 168;
            this.lblUF.Values.Text = "Pesquisa Saída";
            // 
            // lblCampoValor
            // 
            this.lblCampoValor.BackColor = System.Drawing.Color.Lavender;
            this.lblCampoValor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCampoValor.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
            this.lblCampoValor.Location = new System.Drawing.Point(352, 62);
            this.lblCampoValor.Name = "lblCampoValor";
            this.lblCampoValor.Size = new System.Drawing.Size(90, 20);
            this.lblCampoValor.TabIndex = 169;
            this.lblCampoValor.Values.Text = "Valor Pesquisa";
            // 
            // btnPesquisa
            // 
            this.btnPesquisa.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPesquisa.BackgroundImage")));
            this.btnPesquisa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPesquisa.Location = new System.Drawing.Point(751, 62);
            this.btnPesquisa.Name = "btnPesquisa";
            this.btnPesquisa.Size = new System.Drawing.Size(28, 21);
            this.btnPesquisa.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.btnPesquisa.TabIndex = 170;
            this.btnPesquisa.Values.Text = ". . .";
            this.btnPesquisa.Click += new System.EventHandler(this.btnPesquisa_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Lavender;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
            this.label1.Location = new System.Drawing.Point(379, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 20);
            this.label1.TabIndex = 172;
            this.label1.Values.Text = "Operador";
            // 
            // cbxOperador
            // 
            this.cbxOperador.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOperador.DropDownWidth = 147;
            this.cbxOperador.FormattingEnabled = true;
            this.cbxOperador.Items.AddRange(new object[] {
            "Igual a",
            "Maior que",
            "Maior Igual que",
            "Menor que",
            "Menor Igual que",
            "Na Frase"});
            this.cbxOperador.Location = new System.Drawing.Point(448, 33);
            this.cbxOperador.Name = "cbxOperador";
            this.cbxOperador.Size = new System.Drawing.Size(147, 21);
            this.cbxOperador.TabIndex = 171;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Lavender;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
            this.label2.Location = new System.Drawing.Point(24, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 20);
            this.label2.TabIndex = 175;
            this.label2.Values.Text = "Tipo de Nota Fiscal";
            // 
            // cbxTipoNf
            // 
            this.cbxTipoNf.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTipoNf.DropDownWidth = 147;
            this.cbxTipoNf.FormattingEnabled = true;
            this.cbxTipoNf.Items.AddRange(new object[] {
            "Saída",
            "Entrada"});
            this.cbxTipoNf.Location = new System.Drawing.Point(145, 3);
            this.cbxTipoNf.Name = "cbxTipoNf";
            this.cbxTipoNf.Size = new System.Drawing.Size(147, 21);
            this.cbxTipoNf.TabIndex = 176;
            this.cbxTipoNf.SelectedIndexChanged += new System.EventHandler(this.cbxTipoNf_SelectedIndexChanged);
            // 
            // cbxModelo
            // 
            this.cbxModelo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxModelo.DropDownWidth = 147;
            this.cbxModelo.FormattingEnabled = true;
            this.cbxModelo.Items.AddRange(new object[] {
            "NF - Eletrônica",
            "Modelo 1/1A"});
            this.cbxModelo.Location = new System.Drawing.Point(448, 3);
            this.cbxModelo.Name = "cbxModelo";
            this.cbxModelo.Size = new System.Drawing.Size(147, 21);
            this.cbxModelo.TabIndex = 178;
            this.cbxModelo.SelectedIndexChanged += new System.EventHandler(this.cbxModelo_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Lavender;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
            this.label3.Location = new System.Drawing.Point(389, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 20);
            this.label3.TabIndex = 177;
            this.label3.Values.Text = "Modelo";
            this.label3.Paint += new System.Windows.Forms.PaintEventHandler(this.label3_Paint);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Lavender;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
            this.label4.Location = new System.Drawing.Point(36, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 20);
            this.label4.TabIndex = 180;
            this.label4.Values.Text = "Pesquisa Entrada";
            // 
            // cbxCampoPesquisaEntrada
            // 
            this.cbxCampoPesquisaEntrada.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCampoPesquisaEntrada.DropDownWidth = 147;
            this.cbxCampoPesquisaEntrada.FormattingEnabled = true;
            this.cbxCampoPesquisaEntrada.Items.AddRange(new object[] {
            "Número NF",
            "Lancamento",
            "Chave Acesso"});
            this.cbxCampoPesquisaEntrada.Location = new System.Drawing.Point(145, 61);
            this.cbxCampoPesquisaEntrada.Name = "cbxCampoPesquisaEntrada";
            this.cbxCampoPesquisaEntrada.Size = new System.Drawing.Size(147, 21);
            this.cbxCampoPesquisaEntrada.TabIndex = 179;
            this.cbxCampoPesquisaEntrada.SelectionChangeCommitted += new System.EventHandler(this.cbxCampoPesquisaEntrada_SelectionChangeCommitted);
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonGroupBox1);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Controls.Add(this.dgvPesquisa);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(929, 477);
            this.kryptonPanel1.TabIndex = 182;
            // 
            // kryptonGroupBox1
            // 
            this.kryptonGroupBox1.Location = new System.Drawing.Point(6, 3);
            this.kryptonGroupBox1.Name = "kryptonGroupBox1";
            // 
            // kryptonGroupBox1.Panel
            // 
            this.kryptonGroupBox1.Panel.Controls.Add(this.label2);
            this.kryptonGroupBox1.Panel.Controls.Add(this.label4);
            this.kryptonGroupBox1.Panel.Controls.Add(this.label1);
            this.kryptonGroupBox1.Panel.Controls.Add(this.cbxCampoPesquisa);
            this.kryptonGroupBox1.Panel.Controls.Add(this.cbxOperador);
            this.kryptonGroupBox1.Panel.Controls.Add(this.cbxCampoPesquisaEntrada);
            this.kryptonGroupBox1.Panel.Controls.Add(this.btnPesquisa);
            this.kryptonGroupBox1.Panel.Controls.Add(this.txtPesquisa);
            this.kryptonGroupBox1.Panel.Controls.Add(this.cbxTipoNf);
            this.kryptonGroupBox1.Panel.Controls.Add(this.cbxModelo);
            this.kryptonGroupBox1.Panel.Controls.Add(this.lblCampoValor);
            this.kryptonGroupBox1.Panel.Controls.Add(this.lblUF);
            this.kryptonGroupBox1.Panel.Controls.Add(this.label3);
            this.kryptonGroupBox1.Size = new System.Drawing.Size(911, 118);
            this.kryptonGroupBox1.TabIndex = 183;
            this.kryptonGroupBox1.Text = "Filtros";
            this.kryptonGroupBox1.Values.Heading = "Filtros";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.BackColor = System.Drawing.Color.Lavender;
            this.kryptonLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonLabel1.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
            this.kryptonLabel1.Location = new System.Drawing.Point(6, 127);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(103, 20);
            this.kryptonLabel1.TabIndex = 182;
            this.kryptonLabel1.Values.Text = "Pesquisa Entrada";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "NumeroNF";
            this.dataGridViewTextBoxColumn1.HeaderText = "Número NF";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 110;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "SeqNF";
            this.dataGridViewTextBoxColumn2.HeaderText = "Sequência NF";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 110;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "ChaveAcesso";
            this.dataGridViewTextBoxColumn3.HeaderText = "Chave de Acesso";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 300;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "ChaveAcesso";
            this.dataGridViewTextBoxColumn4.HeaderText = "Chave de Acesso";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 350;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "cUF";
            this.dataGridViewTextBoxColumn5.HeaderText = "cUF";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Visible = false;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "AAMM";
            this.dataGridViewTextBoxColumn6.HeaderText = "AAMM";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Visible = false;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "CNPJ";
            this.dataGridViewTextBoxColumn7.HeaderText = "CNPJ";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Visible = false;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "serie";
            this.dataGridViewTextBoxColumn8.HeaderText = "serie";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Visible = false;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "NumeroNF";
            this.Column1.HeaderText = "Número NF";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 110;
            // 
            // SeqNF
            // 
            this.SeqNF.DataPropertyName = "SeqNF";
            this.SeqNF.HeaderText = "Sequência NF";
            this.SeqNF.Name = "SeqNF";
            this.SeqNF.ReadOnly = true;
            this.SeqNF.Width = 110;
            // 
            // sCli_For
            // 
            this.sCli_For.DataPropertyName = "sCli_For";
            this.sCli_For.HeaderText = "Cliente / Fornecedor";
            this.sCli_For.Name = "sCli_For";
            this.sCli_For.ReadOnly = true;
            this.sCli_For.Width = 300;
            // 
            // ChaveAcesso
            // 
            this.ChaveAcesso.DataPropertyName = "ChaveAcesso";
            this.ChaveAcesso.HeaderText = "Chave de Acesso";
            this.ChaveAcesso.Name = "ChaveAcesso";
            this.ChaveAcesso.ReadOnly = true;
            this.ChaveAcesso.Width = 350;
            // 
            // cUF
            // 
            this.cUF.DataPropertyName = "cUF";
            this.cUF.HeaderText = "cUF";
            this.cUF.Name = "cUF";
            this.cUF.ReadOnly = true;
            this.cUF.Visible = false;
            // 
            // AAMM
            // 
            this.AAMM.DataPropertyName = "AAMM";
            this.AAMM.HeaderText = "AAMM";
            this.AAMM.Name = "AAMM";
            this.AAMM.ReadOnly = true;
            this.AAMM.Visible = false;
            // 
            // CNPJ
            // 
            this.CNPJ.DataPropertyName = "CNPJ";
            this.CNPJ.HeaderText = "CNPJ";
            this.CNPJ.Name = "CNPJ";
            this.CNPJ.ReadOnly = true;
            this.CNPJ.Visible = false;
            // 
            // serie
            // 
            this.serie.DataPropertyName = "serie";
            this.serie.HeaderText = "serie";
            this.serie.Name = "serie";
            this.serie.ReadOnly = true;
            this.serie.Visible = false;
            // 
            // frmPesquisaNfe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(929, 477);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPesquisaNfe";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PESQUISAR NF";
            this.Load += new System.EventHandler(this.PesquisaNF_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPesquisa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxCampoPesquisa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxOperador)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxTipoNf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxModelo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxCampoPesquisaEntrada)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            this.kryptonGroupBox1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonComboBox cbxCampoPesquisa;
        private KryptonTextBox txtPesquisa;
        private KryptonLabel lblUF;
        private KryptonLabel lblCampoValor;
        private KryptonDataGridView dgvPesquisa;
        private KryptonButton btnPesquisa;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private KryptonLabel label1;
        private KryptonComboBox cbxOperador;
        private KryptonLabel label2;
        private KryptonComboBox cbxTipoNf;
        private KryptonComboBox cbxModelo;
        private KryptonLabel label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private KryptonLabel label4;
        private KryptonComboBox cbxCampoPesquisaEntrada;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SeqNF;
        private System.Windows.Forms.DataGridViewTextBoxColumn sCli_For;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChaveAcesso;
        private System.Windows.Forms.DataGridViewTextBoxColumn cUF;
        private System.Windows.Forms.DataGridViewTextBoxColumn AAMM;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNPJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn serie;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private KryptonGroupBox kryptonGroupBox1;
        private KryptonLabel kryptonLabel1;
    }
}