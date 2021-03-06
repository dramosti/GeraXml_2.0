﻿using ComponentFactory.Krypton.Toolkit;
using AC.ExtendedRenderer.Navigator;
namespace NfeGerarXml
{
    partial class frmImportaEscritorNfe
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImportaEscritorNfe));
            this.ofdXml = new System.Windows.Forms.OpenFileDialog();
            this.fbdImportar = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonButton3 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonButton2 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.label1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cbxEmpresas = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.label3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtXml = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.buttonSpecAny1 = new ComponentFactory.Krypton.Toolkit.ButtonSpecAny();
            this.dgvXmls = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.Selecionar = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewCheckBoxColumn();
            this.Nota = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Destinatario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Emitente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Emissao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NFE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Arquivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selecionarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.pgbNF = new System.Windows.Forms.ToolStripProgressBar();
            this.lblStatusScrituracao = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblXmlNaoValidado = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblarquivosEscriturados = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new AC.ExtendedRenderer.Navigator.KryptonTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvXmlNaoValidado = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxEmpresas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvXmls)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvXmlNaoValidado)).BeginInit();
            this.SuspendLayout();
            // 
            // ofdXml
            // 
            this.ofdXml.Filter = "Arquivos Xml (*.xml)|*.xml";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.kryptonButton3);
            this.panel1.Controls.Add(this.kryptonButton2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cbxEmpresas);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtXml);
            this.panel1.Location = new System.Drawing.Point(0, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(789, 68);
            this.panel1.TabIndex = 25;
            // 
            // kryptonButton3
            // 
            this.kryptonButton3.Location = new System.Drawing.Point(607, 34);
            this.kryptonButton3.Name = "kryptonButton3";
            this.kryptonButton3.Size = new System.Drawing.Size(56, 21);
            this.kryptonButton3.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.kryptonButton3.TabIndex = 39;
            this.kryptonButton3.Values.Text = "Importar";
            this.kryptonButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // kryptonButton2
            // 
            this.kryptonButton2.Location = new System.Drawing.Point(545, 34);
            this.kryptonButton2.Name = "kryptonButton2";
            this.kryptonButton2.Size = new System.Drawing.Size(56, 21);
            this.kryptonButton2.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.kryptonButton2.TabIndex = 38;
            this.kryptonButton2.Values.Text = "Pesquisar";
            this.kryptonButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
            this.label1.Location = new System.Drawing.Point(10, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 20);
            this.label1.TabIndex = 32;
            this.label1.Values.Text = "Pasta (XML)";
            this.label1.Paint += new System.Windows.Forms.PaintEventHandler(this.label1_Paint);
            // 
            // cbxEmpresas
            // 
            this.cbxEmpresas.DropDownWidth = 396;
            this.cbxEmpresas.FormattingEnabled = true;
            this.cbxEmpresas.Location = new System.Drawing.Point(108, 7);
            this.cbxEmpresas.Name = "cbxEmpresas";
            this.cbxEmpresas.Size = new System.Drawing.Size(396, 21);
            this.cbxEmpresas.TabIndex = 0;
            this.toolTip1.SetToolTip(this.cbxEmpresas, "Empresa");
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
            this.label3.Location = new System.Drawing.Point(27, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 20);
            this.label3.TabIndex = 31;
            this.label3.Values.Text = "Empresa";
            // 
            // txtXml
            // 
            this.txtXml.BackColor = System.Drawing.Color.White;
            this.txtXml.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecAny[] {
            this.buttonSpecAny1});
            this.txtXml.Location = new System.Drawing.Point(108, 34);
            this.txtXml.Name = "txtXml";
            this.txtXml.Size = new System.Drawing.Size(396, 20);
            this.txtXml.TabIndex = 29;
            this.toolTip1.SetToolTip(this.txtXml, "Pasta  onde se Encontra os XML\'s a Serem Escriturados");
            // 
            // buttonSpecAny1
            // 
            this.buttonSpecAny1.Image = global::GeraXml.Properties.Resources.pesq_verd;
            this.buttonSpecAny1.Style = ComponentFactory.Krypton.Toolkit.PaletteButtonStyle.InputControl;
            this.buttonSpecAny1.UniqueName = "71B34DD60F7244EE5897D019403939D4";
            this.buttonSpecAny1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // dgvXmls
            // 
            this.dgvXmls.AllowUserToAddRows = false;
            this.dgvXmls.AllowUserToDeleteRows = false;
            this.dgvXmls.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvXmls.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvXmls.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selecionar,
            this.Nota,
            this.Destinatario,
            this.Emitente,
            this.Emissao,
            this.NFE,
            this.Arquivo});
            this.dgvXmls.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvXmls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvXmls.Location = new System.Drawing.Point(0, 0);
            this.dgvXmls.Name = "dgvXmls";
            this.dgvXmls.ReadOnly = true;
            this.dgvXmls.RowHeadersVisible = false;
            this.dgvXmls.RowHeadersWidth = 35;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvXmls.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvXmls.RowTemplate.Height = 18;
            this.dgvXmls.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvXmls.Size = new System.Drawing.Size(781, 346);
            this.dgvXmls.TabIndex = 26;
            this.dgvXmls.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvXmls_CellClick);
            this.dgvXmls.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvXmls_ColumnHeaderMouseClick);
            // 
            // Selecionar
            // 
            this.Selecionar.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Selecionar.DataPropertyName = "Selecionar";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = false;
            this.Selecionar.DefaultCellStyle = dataGridViewCellStyle2;
            this.Selecionar.FalseValue = null;
            this.Selecionar.Frozen = true;
            this.Selecionar.HeaderText = "Selecionar";
            this.Selecionar.IndeterminateValue = null;
            this.Selecionar.Name = "Selecionar";
            this.Selecionar.ReadOnly = true;
            this.Selecionar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Selecionar.TrueValue = null;
            this.Selecionar.Width = 71;
            // 
            // Nota
            // 
            this.Nota.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Nota.DataPropertyName = "Nota";
            this.Nota.Frozen = true;
            this.Nota.HeaderText = "Nota";
            this.Nota.Name = "Nota";
            this.Nota.ReadOnly = true;
            this.Nota.Width = 62;
            // 
            // Destinatario
            // 
            this.Destinatario.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Destinatario.DataPropertyName = "Destinatario";
            this.Destinatario.Frozen = true;
            this.Destinatario.HeaderText = "Destinatário";
            this.Destinatario.Name = "Destinatario";
            this.Destinatario.ReadOnly = true;
            this.Destinatario.Width = 99;
            // 
            // Emitente
            // 
            this.Emitente.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Emitente.DataPropertyName = "Emitente";
            this.Emitente.HeaderText = "Emitente";
            this.Emitente.Name = "Emitente";
            this.Emitente.ReadOnly = true;
            this.Emitente.Width = 83;
            // 
            // Emissao
            // 
            this.Emissao.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Emissao.DataPropertyName = "Emissao";
            this.Emissao.HeaderText = "Emissão";
            this.Emissao.Name = "Emissao";
            this.Emissao.ReadOnly = true;
            this.Emissao.Width = 79;
            // 
            // NFE
            // 
            this.NFE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.NFE.DataPropertyName = "NFE";
            this.NFE.HeaderText = "NF-e";
            this.NFE.Name = "NFE";
            this.NFE.ReadOnly = true;
            this.NFE.Width = 62;
            // 
            // Arquivo
            // 
            this.Arquivo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Arquivo.DataPropertyName = "Arquivo";
            this.Arquivo.HeaderText = "Arquivo";
            this.Arquivo.Name = "Arquivo";
            this.Arquivo.ReadOnly = true;
            this.Arquivo.Visible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selecionarToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(129, 26);
            // 
            // selecionarToolStripMenuItem
            // 
            this.selecionarToolStripMenuItem.Image = global::GeraXml.Properties.Resources.confirmar;
            this.selecionarToolStripMenuItem.Name = "selecionarToolStripMenuItem";
            this.selecionarToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.selecionarToolStripMenuItem.Text = "Selecionar";
            this.selecionarToolStripMenuItem.Click += new System.EventHandler(this.selecionarToolStripMenuItem_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pgbNF,
            this.lblStatusScrituracao,
            this.lblXmlNaoValidado,
            this.lblarquivosEscriturados});
            this.statusStrip1.Location = new System.Drawing.Point(0, 454);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.statusStrip1.Size = new System.Drawing.Size(789, 22);
            this.statusStrip1.TabIndex = 27;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // pgbNF
            // 
            this.pgbNF.MarqueeAnimationSpeed = 0;
            this.pgbNF.Maximum = 0;
            this.pgbNF.Name = "pgbNF";
            this.pgbNF.Size = new System.Drawing.Size(100, 16);
            this.pgbNF.Step = 1;
            // 
            // lblStatusScrituracao
            // 
            this.lblStatusScrituracao.Name = "lblStatusScrituracao";
            this.lblStatusScrituracao.Size = new System.Drawing.Size(0, 17);
            // 
            // lblXmlNaoValidado
            // 
            this.lblXmlNaoValidado.Name = "lblXmlNaoValidado";
            this.lblXmlNaoValidado.Size = new System.Drawing.Size(0, 17);
            // 
            // lblarquivosEscriturados
            // 
            this.lblarquivosEscriturados.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblarquivosEscriturados.Name = "lblarquivosEscriturados";
            this.lblarquivosEscriturados.Size = new System.Drawing.Size(0, 17);
            this.lblarquivosEscriturados.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabControl1
            // 
            this.tabControl1.AllowCloseButton = false;
            this.tabControl1.AllowContextButton = false;
            this.tabControl1.AllowNavigatorButtons = false;
            this.tabControl1.AllowSelectedTabHigh = false;
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.BorderWidth = 1;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.CornerRoundRadiusWidth = 12;
            this.tabControl1.CornerSymmetry = AC.ExtendedRenderer.Navigator.KryptonTabControl.CornSymmetry.Both;
            this.tabControl1.CornerType = AC.ExtendedRenderer.Toolkit.Drawing.DrawingMethods.CornerType.Rounded;
            this.tabControl1.CornerWidth = AC.ExtendedRenderer.Navigator.KryptonTabControl.CornWidth.Thin;
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.HotTrack = true;
            this.tabControl1.Location = new System.Drawing.Point(0, 76);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.PreserveTabColor = false;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(789, 375);
            this.tabControl1.TabIndex = 28;
            this.tabControl1.UseExtendedLayout = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvXmls);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(781, 346);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Tag = false;
            this.tabPage1.Text = "Arquivos Validos";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvXmlNaoValidado);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(781, 346);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Tag = false;
            this.tabPage2.Text = "Arquivos Não Validados";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvXmlNaoValidado
            // 
            this.dgvXmlNaoValidado.AllowUserToAddRows = false;
            this.dgvXmlNaoValidado.AllowUserToDeleteRows = false;
            this.dgvXmlNaoValidado.AllowUserToOrderColumns = true;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvXmlNaoValidado.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvXmlNaoValidado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvXmlNaoValidado.Location = new System.Drawing.Point(0, 0);
            this.dgvXmlNaoValidado.Name = "dgvXmlNaoValidado";
            this.dgvXmlNaoValidado.ReadOnly = true;
            this.dgvXmlNaoValidado.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvXmlNaoValidado.RowTemplate.Height = 18;
            this.dgvXmlNaoValidado.Size = new System.Drawing.Size(781, 346);
            this.dgvXmlNaoValidado.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Arquivo";
            this.dataGridViewTextBoxColumn1.Frozen = true;
            this.dataGridViewTextBoxColumn1.HeaderText = "Arquivo";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "NFE";
            this.dataGridViewTextBoxColumn2.Frozen = true;
            this.dataGridViewTextBoxColumn2.HeaderText = "NFE";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Nota";
            this.dataGridViewTextBoxColumn3.HeaderText = "Nota";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Emissao";
            this.dataGridViewTextBoxColumn4.HeaderText = "Emissao";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Destinatario";
            this.dataGridViewTextBoxColumn5.HeaderText = "Destinatario";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Emitente";
            this.dataGridViewTextBoxColumn6.HeaderText = "Emitente";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Visible = false;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Xml Não Validado";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Width = 500;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "Motivo";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Width = 300;
            // 
            // frmImportaEscritorNfe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(789, 476);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmImportaEscritorNfe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Escritor Fiscal";
            this.Load += new System.EventHandler(this.frmImportaEscritor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxEmpresas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvXmls)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvXmlNaoValidado)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdXml;
        private System.Windows.Forms.FolderBrowserDialog fbdImportar;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private KryptonPanel panel1;
        private KryptonComboBox cbxEmpresas;
        private KryptonTextBox txtXml;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private KryptonDataGridView dgvXmls;
        private System.Windows.Forms.ToolTip toolTip1;
        private KryptonLabel label1;
        private KryptonLabel label3;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar pgbNF;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusScrituracao;
        private AC.ExtendedRenderer.Navigator.KryptonTabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private KryptonDataGridView dgvXmlNaoValidado;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.ToolStripStatusLabel lblXmlNaoValidado;
        private System.Windows.Forms.ToolStripStatusLabel lblarquivosEscriturados;
        private KryptonButton kryptonButton3;
        private KryptonButton kryptonButton2;
        private KryptonDataGridViewCheckBoxColumn Selecionar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nota;
        private System.Windows.Forms.DataGridViewTextBoxColumn Destinatario;
        private System.Windows.Forms.DataGridViewTextBoxColumn Emitente;
        private System.Windows.Forms.DataGridViewTextBoxColumn Emissao;
        private System.Windows.Forms.DataGridViewTextBoxColumn NFE;
        private System.Windows.Forms.DataGridViewTextBoxColumn Arquivo;
        private ButtonSpecAny buttonSpecAny1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem selecionarToolStripMenuItem;
    }
}