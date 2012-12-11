using ComponentFactory.Krypton.Toolkit;
namespace NfeGerarXml.NFe
{
    partial class frmDeclaracaoImportacaoNfe
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDeclaracaoImportacaoNfe));
            this.bsDI = new System.Windows.Forms.BindingSource(this.components);
            this.bsAdicoes = new System.Windows.Forms.BindingSource(this.components);
            this.lblProduto = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.listBoxProdutos = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
            this.lblObs = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonGroupBox2 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.dgvDI = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.kryptonGroupBox1 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.dgvADI = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nDIDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dDIDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xLocDesembDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uFDesembDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dDesembDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cExportadorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nAdicaoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nSeqAdicDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cFabricanteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vDescDIDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maskedEditColumn1 = new NfeGerarXml.MaskedEditColumn(this.components);
            this.maskedEditColumn2 = new NfeGerarXml.MaskedEditColumn(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bsDI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsAdicoes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).BeginInit();
            this.kryptonGroupBox2.Panel.SuspendLayout();
            this.kryptonGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvADI)).BeginInit();
            this.SuspendLayout();
            // 
            // bsDI
            // 
            this.bsDI.DataSource = typeof(HLP.bel.belDI);
            // 
            // bsAdicoes
            // 
            this.bsAdicoes.DataSource = typeof(HLP.bel.beladi);
            // 
            // lblProduto
            // 
            this.lblProduto.Font = new System.Drawing.Font("Papyrus", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProduto.Location = new System.Drawing.Point(3, 9);
            this.lblProduto.Name = "lblProduto";
            this.lblProduto.Size = new System.Drawing.Size(55, 20);
            this.lblProduto.TabIndex = 202;
            this.lblProduto.Values.Text = "Produto";
            // 
            // listBoxProdutos
            // 
            this.listBoxProdutos.DisplayMember = "xProd";
            this.listBoxProdutos.FormattingEnabled = true;
            this.listBoxProdutos.Location = new System.Drawing.Point(8, 46);
            this.listBoxProdutos.Name = "listBoxProdutos";
            this.listBoxProdutos.Size = new System.Drawing.Size(190, 342);
            this.listBoxProdutos.TabIndex = 203;
            this.listBoxProdutos.SelectedIndexChanged += new System.EventHandler(this.listBoxProdutos_SelectedIndexChanged);
            // 
            // lblObs
            // 
            this.lblObs.Font = new System.Drawing.Font("Papyrus", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblObs.Location = new System.Drawing.Point(601, 217);
            this.lblObs.Name = "lblObs";
            this.lblObs.Size = new System.Drawing.Size(30, 20);
            this.lblObs.TabIndex = 204;
            this.lblObs.Values.Text = "obs";
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonGroupBox2);
            this.kryptonPanel1.Controls.Add(this.kryptonGroupBox1);
            this.kryptonPanel1.Controls.Add(this.lblProduto);
            this.kryptonPanel1.Controls.Add(this.lblObs);
            this.kryptonPanel1.Controls.Add(this.listBoxProdutos);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(902, 399);
            this.kryptonPanel1.TabIndex = 205;
            // 
            // kryptonGroupBox2
            // 
            this.kryptonGroupBox2.Location = new System.Drawing.Point(221, 36);
            this.kryptonGroupBox2.Name = "kryptonGroupBox2";
            // 
            // kryptonGroupBox2.Panel
            // 
            this.kryptonGroupBox2.Panel.Controls.Add(this.dgvDI);
            this.kryptonGroupBox2.Size = new System.Drawing.Size(669, 160);
            this.kryptonGroupBox2.TabIndex = 206;
            this.kryptonGroupBox2.Text = "adi (adições)";
            this.kryptonGroupBox2.Values.Heading = "adi (adições)";
            // 
            // dgvDI
            // 
            this.dgvDI.AllowUserToAddRows = false;
            this.dgvDI.AllowUserToDeleteRows = false;
            this.dgvDI.AllowUserToOrderColumns = true;
            this.dgvDI.AutoGenerateColumns = false;
            this.dgvDI.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nDIDataGridViewTextBoxColumn,
            this.dDIDataGridViewTextBoxColumn,
            this.xLocDesembDataGridViewTextBoxColumn,
            this.uFDesembDataGridViewTextBoxColumn,
            this.dDesembDataGridViewTextBoxColumn,
            this.cExportadorDataGridViewTextBoxColumn});
            this.dgvDI.DataSource = this.bsDI;
            this.dgvDI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDI.Location = new System.Drawing.Point(0, 0);
            this.dgvDI.Name = "dgvDI";
            this.dgvDI.RowTemplate.Height = 18;
            this.dgvDI.Size = new System.Drawing.Size(665, 136);
            this.dgvDI.TabIndex = 1;
            // 
            // kryptonGroupBox1
            // 
            this.kryptonGroupBox1.Location = new System.Drawing.Point(223, 208);
            this.kryptonGroupBox1.Name = "kryptonGroupBox1";
            // 
            // kryptonGroupBox1.Panel
            // 
            this.kryptonGroupBox1.Panel.Controls.Add(this.dgvADI);
            this.kryptonGroupBox1.Size = new System.Drawing.Size(372, 180);
            this.kryptonGroupBox1.TabIndex = 205;
            this.kryptonGroupBox1.Text = "Declaração de Importação";
            this.kryptonGroupBox1.Values.Heading = "Declaração de Importação";
            // 
            // dgvADI
            // 
            this.dgvADI.AllowUserToAddRows = false;
            this.dgvADI.AllowUserToDeleteRows = false;
            this.dgvADI.AllowUserToOrderColumns = true;
            this.dgvADI.AutoGenerateColumns = false;
            this.dgvADI.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nAdicaoDataGridViewTextBoxColumn,
            this.nSeqAdicDataGridViewTextBoxColumn,
            this.cFabricanteDataGridViewTextBoxColumn,
            this.vDescDIDataGridViewTextBoxColumn});
            this.dgvADI.DataSource = this.bsAdicoes;
            this.dgvADI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvADI.Location = new System.Drawing.Point(0, 0);
            this.dgvADI.Name = "dgvADI";
            this.dgvADI.RowTemplate.Height = 18;
            this.dgvADI.Size = new System.Drawing.Size(368, 156);
            this.dgvADI.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "nDI";
            this.dataGridViewTextBoxColumn1.HeaderText = "nDI";
            this.dataGridViewTextBoxColumn1.MaxInputLength = 10;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ToolTipText = "Número do Documento de Importação DI/DSI/DA";
            this.dataGridViewTextBoxColumn1.Width = 60;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "xLocDesemb";
            this.dataGridViewTextBoxColumn2.HeaderText = "xLocDesemb";
            this.dataGridViewTextBoxColumn2.MaxInputLength = 60;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn2.ToolTipText = "Data de Registro da DI/DSI/DA";
            this.dataGridViewTextBoxColumn2.Width = 70;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "UFDesemb";
            this.dataGridViewTextBoxColumn3.HeaderText = "UFDesemb";
            this.dataGridViewTextBoxColumn3.MaxInputLength = 2;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ToolTipText = "Local de desembaraço";
            this.dataGridViewTextBoxColumn3.Width = 150;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "cExportador";
            this.dataGridViewTextBoxColumn4.HeaderText = "cExportador";
            this.dataGridViewTextBoxColumn4.MaxInputLength = 60;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ToolTipText = "Sigla da UF onde ocorreu o Desembaraço Aduaneiro";
            this.dataGridViewTextBoxColumn4.Width = 70;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "dDesemb";
            this.dataGridViewTextBoxColumn5.HeaderText = "dDesemb";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn5.ToolTipText = "Data do Desembaraço Aduaneiro";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "cExportador";
            this.dataGridViewTextBoxColumn6.HeaderText = "cExportador";
            this.dataGridViewTextBoxColumn6.MaxInputLength = 60;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ToolTipText = "Código do exportador";
            this.dataGridViewTextBoxColumn6.Width = 120;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "nAdicao";
            this.dataGridViewTextBoxColumn7.HeaderText = "nAdicao";
            this.dataGridViewTextBoxColumn7.MaxInputLength = 3;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.ToolTipText = "Numero da adição";
            this.dataGridViewTextBoxColumn7.Width = 70;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "nSeqAdic";
            this.dataGridViewTextBoxColumn8.HeaderText = "nSeqAdic";
            this.dataGridViewTextBoxColumn8.MaxInputLength = 3;
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ToolTipText = "Numero da adição";
            this.dataGridViewTextBoxColumn8.Visible = false;
            this.dataGridViewTextBoxColumn8.Width = 70;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "cFabricante";
            this.dataGridViewTextBoxColumn9.HeaderText = "cFabricante";
            this.dataGridViewTextBoxColumn9.MaxInputLength = 60;
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ToolTipText = "Código do fabricante Estrangeiro";
            this.dataGridViewTextBoxColumn9.Width = 150;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "vDescDI";
            dataGridViewCellStyle2.Format = "n2";
            this.dataGridViewTextBoxColumn10.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn10.HeaderText = "vDescDI";
            this.dataGridViewTextBoxColumn10.MaxInputLength = 17;
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ToolTipText = "Valor do desconto do item da  DI – Adição";
            this.dataGridViewTextBoxColumn10.Width = 70;
            // 
            // nDIDataGridViewTextBoxColumn
            // 
            this.nDIDataGridViewTextBoxColumn.DataPropertyName = "nDI";
            this.nDIDataGridViewTextBoxColumn.HeaderText = "nº_DI (*)";
            this.nDIDataGridViewTextBoxColumn.MaxInputLength = 10;
            this.nDIDataGridViewTextBoxColumn.Name = "nDIDataGridViewTextBoxColumn";
            this.nDIDataGridViewTextBoxColumn.ToolTipText = "Número do Documento de Importação DI/DSI/DA";
            this.nDIDataGridViewTextBoxColumn.Width = 60;
            // 
            // dDIDataGridViewTextBoxColumn
            // 
            this.dDIDataGridViewTextBoxColumn.DataPropertyName = "DDI";
            this.dDIDataGridViewTextBoxColumn.HeaderText = "data DI (*)";
            this.dDIDataGridViewTextBoxColumn.Name = "dDIDataGridViewTextBoxColumn";
            this.dDIDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dDIDataGridViewTextBoxColumn.ToolTipText = "Data de Registro da DI/DSI/DA";
            this.dDIDataGridViewTextBoxColumn.Width = 70;
            // 
            // xLocDesembDataGridViewTextBoxColumn
            // 
            this.xLocDesembDataGridViewTextBoxColumn.DataPropertyName = "xLocDesemb";
            this.xLocDesembDataGridViewTextBoxColumn.HeaderText = "Local Desembaraço(*)";
            this.xLocDesembDataGridViewTextBoxColumn.MaxInputLength = 60;
            this.xLocDesembDataGridViewTextBoxColumn.Name = "xLocDesembDataGridViewTextBoxColumn";
            this.xLocDesembDataGridViewTextBoxColumn.ToolTipText = "Local de desembaraço";
            this.xLocDesembDataGridViewTextBoxColumn.Width = 150;
            // 
            // uFDesembDataGridViewTextBoxColumn
            // 
            this.uFDesembDataGridViewTextBoxColumn.DataPropertyName = "UFDesemb";
            this.uFDesembDataGridViewTextBoxColumn.HeaderText = "UF_Desemb(*)";
            this.uFDesembDataGridViewTextBoxColumn.MaxInputLength = 2;
            this.uFDesembDataGridViewTextBoxColumn.Name = "uFDesembDataGridViewTextBoxColumn";
            this.uFDesembDataGridViewTextBoxColumn.ToolTipText = "Sigla da UF onde ocorreu o Desembaraço Aduaneiro";
            this.uFDesembDataGridViewTextBoxColumn.Width = 105;
            // 
            // dDesembDataGridViewTextBoxColumn
            // 
            this.dDesembDataGridViewTextBoxColumn.DataPropertyName = "dDesemb";
            this.dDesembDataGridViewTextBoxColumn.HeaderText = "data Desemb(*)";
            this.dDesembDataGridViewTextBoxColumn.Name = "dDesembDataGridViewTextBoxColumn";
            this.dDesembDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dDesembDataGridViewTextBoxColumn.ToolTipText = "Data do Desembaraço Aduaneiro";
            // 
            // cExportadorDataGridViewTextBoxColumn
            // 
            this.cExportadorDataGridViewTextBoxColumn.DataPropertyName = "cExportador";
            this.cExportadorDataGridViewTextBoxColumn.HeaderText = "cód. Exportador(*)";
            this.cExportadorDataGridViewTextBoxColumn.MaxInputLength = 60;
            this.cExportadorDataGridViewTextBoxColumn.Name = "cExportadorDataGridViewTextBoxColumn";
            this.cExportadorDataGridViewTextBoxColumn.ToolTipText = "Código do exportador";
            this.cExportadorDataGridViewTextBoxColumn.Width = 120;
            // 
            // nAdicaoDataGridViewTextBoxColumn
            // 
            this.nAdicaoDataGridViewTextBoxColumn.DataPropertyName = "nAdicao";
            this.nAdicaoDataGridViewTextBoxColumn.HeaderText = "nº_Adicao(*)";
            this.nAdicaoDataGridViewTextBoxColumn.MaxInputLength = 3;
            this.nAdicaoDataGridViewTextBoxColumn.Name = "nAdicaoDataGridViewTextBoxColumn";
            this.nAdicaoDataGridViewTextBoxColumn.ToolTipText = "Numero da adição";
            this.nAdicaoDataGridViewTextBoxColumn.Width = 90;
            // 
            // nSeqAdicDataGridViewTextBoxColumn
            // 
            this.nSeqAdicDataGridViewTextBoxColumn.DataPropertyName = "nSeqAdic";
            this.nSeqAdicDataGridViewTextBoxColumn.HeaderText = "nSeqAdic(*)";
            this.nSeqAdicDataGridViewTextBoxColumn.MaxInputLength = 3;
            this.nSeqAdicDataGridViewTextBoxColumn.Name = "nSeqAdicDataGridViewTextBoxColumn";
            this.nSeqAdicDataGridViewTextBoxColumn.Visible = false;
            this.nSeqAdicDataGridViewTextBoxColumn.Width = 70;
            // 
            // cFabricanteDataGridViewTextBoxColumn
            // 
            this.cFabricanteDataGridViewTextBoxColumn.DataPropertyName = "cFabricante";
            this.cFabricanteDataGridViewTextBoxColumn.HeaderText = "cFabricante(*)";
            this.cFabricanteDataGridViewTextBoxColumn.MaxInputLength = 60;
            this.cFabricanteDataGridViewTextBoxColumn.Name = "cFabricanteDataGridViewTextBoxColumn";
            this.cFabricanteDataGridViewTextBoxColumn.ToolTipText = "Código do fabricante Estrangeiro";
            this.cFabricanteDataGridViewTextBoxColumn.Width = 150;
            // 
            // vDescDIDataGridViewTextBoxColumn
            // 
            this.vDescDIDataGridViewTextBoxColumn.DataPropertyName = "vDescDI";
            dataGridViewCellStyle1.Format = "n2";
            this.vDescDIDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.vDescDIDataGridViewTextBoxColumn.HeaderText = "vDescDI";
            this.vDescDIDataGridViewTextBoxColumn.MaxInputLength = 17;
            this.vDescDIDataGridViewTextBoxColumn.Name = "vDescDIDataGridViewTextBoxColumn";
            this.vDescDIDataGridViewTextBoxColumn.ToolTipText = "Valor do desconto do item da  DI – Adição";
            this.vDescDIDataGridViewTextBoxColumn.Width = 70;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "xPed";
            this.dataGridViewTextBoxColumn11.HeaderText = "xPed";
            this.dataGridViewTextBoxColumn11.MaxInputLength = 15;
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.Visible = false;
            this.dataGridViewTextBoxColumn11.Width = 70;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "nItemPed";
            this.dataGridViewTextBoxColumn12.HeaderText = "nItemPed";
            this.dataGridViewTextBoxColumn12.MaxInputLength = 6;
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.Visible = false;
            this.dataGridViewTextBoxColumn12.Width = 70;
            // 
            // maskedEditColumn1
            // 
            this.maskedEditColumn1.DataPropertyName = "dDI";
            this.maskedEditColumn1.HeaderText = "dDI";
            this.maskedEditColumn1.Mask = null;
            this.maskedEditColumn1.Name = "maskedEditColumn1";
            this.maskedEditColumn1.PromptChar = '_';
            this.maskedEditColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.maskedEditColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.maskedEditColumn1.ValidatingType = null;
            // 
            // maskedEditColumn2
            // 
            this.maskedEditColumn2.DataPropertyName = "dDesemb";
            this.maskedEditColumn2.HeaderText = "dDesemb";
            this.maskedEditColumn2.Mask = null;
            this.maskedEditColumn2.Name = "maskedEditColumn2";
            this.maskedEditColumn2.PromptChar = '_';
            this.maskedEditColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.maskedEditColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.maskedEditColumn2.ValidatingType = null;
            // 
            // frmDeclaracaoImportacaoNfe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(902, 399);
            this.Controls.Add(this.kryptonPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDeclaracaoImportacaoNfe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmDeclaracaoImportacao_FormClosed);
            this.Load += new System.EventHandler(this.frmDeclaracaoImportacao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bsDI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsAdicoes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.kryptonGroupBox2.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).EndInit();
            this.kryptonGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDI)).EndInit();
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvADI)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsDI;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private MaskedEditColumn maskedEditColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private MaskedEditColumn maskedEditColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.BindingSource bsAdicoes;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private KryptonLabel lblProduto;
        private KryptonListBox listBoxProdutos;
        private KryptonLabel lblObs;
        private System.Windows.Forms.DataGridViewTextBoxColumn xPedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nItemPedDataGridViewTextBoxColumn;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox2;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox1;
        private KryptonDataGridView dgvDI;
        private System.Windows.Forms.DataGridViewTextBoxColumn nDIDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dDIDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn xLocDesembDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn uFDesembDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dDesembDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cExportadorDataGridViewTextBoxColumn;
        private KryptonDataGridView dgvADI;
        private System.Windows.Forms.DataGridViewTextBoxColumn nAdicaoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nSeqAdicDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cFabricanteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vDescDIDataGridViewTextBoxColumn;
    }
}