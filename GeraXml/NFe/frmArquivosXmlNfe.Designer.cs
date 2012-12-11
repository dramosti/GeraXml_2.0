using ComponentFactory.Krypton.Toolkit;
using System.Windows.Forms;
namespace NfeGerarXml
{
    partial class frmArquivosXmlNfe
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmArquivosXmlNfe));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cbxFormDanfe = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.timerWebServices = new System.Windows.Forms.Timer(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.kryptonPanel2 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonSplitContainer1 = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.headerMenuLateral = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.btnMinimiza = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.kryptonSplitContainer2 = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.kryptonPanel5 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.rbtAmbas = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.label9 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.rbtEnviadas = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.rbtNaoEnviadas = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.kryptonPanel6 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonSplitContainer3 = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.dtpIni = new ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker();
            this.kryptonHeader1 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.dtpFim = new ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker();
            this.rdbData = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.rdbNF = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.label16 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtNfIni = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtNfFim = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.label15 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonPanel7 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.kryptonPanel3 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.button1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.txtMensagemContingencia = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.lblAvisoContingencia = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.label17 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonHeader2 = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.label5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.label19 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.label6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.dgvNF = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.ASSINANF = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewCheckBoxColumn();
            this.CD_NOTAFIS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cd_nfseq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DT_EMI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NM_GUERRA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vl_totnf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ST_NFE = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewCheckBoxColumn();
            this.CANCELADA = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewCheckBoxColumn();
            this.Imprime = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewCheckBoxColumn();
            this.st_contingencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cd_recibocanc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnBuscaNotas = new System.Windows.Forms.ToolStripButton();
            this.btnUsuario = new System.Windows.Forms.ToolStripButton();
            this.btnEnviar = new System.Windows.Forms.ToolStripButton();
            this.btnGerarContingencia = new System.Windows.Forms.ToolStripButton();
            this.btnCancelanebto = new System.Windows.Forms.ToolStripButton();
            this.btnImpressao = new System.Windows.Forms.ToolStripButton();
            this.btnInutilizacao = new System.Windows.Forms.ToolStripButton();
            this.btnConsultaStatus = new System.Windows.Forms.ToolStripButton();
            this.btnBuscaRetorno = new System.Windows.Forms.ToolStripButton();
            this.btnSituacaoNFe = new System.Windows.Forms.ToolStripButton();
            this.tsConsultaCadastro = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.sslStatusEnvio = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusContingencia = new System.Windows.Forms.ToolStripStatusLabel();
            this.nmEmpresa = new System.Windows.Forms.ToolStripStatusLabel();
            this.nmStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timerInicializacao = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.cbxFormDanfe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel1)).BeginInit();
            this.kryptonSplitContainer1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel2)).BeginInit();
            this.kryptonSplitContainer1.Panel2.SuspendLayout();
            this.kryptonSplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.headerMenuLateral)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.headerMenuLateral.Panel)).BeginInit();
            this.headerMenuLateral.Panel.SuspendLayout();
            this.headerMenuLateral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer2.Panel1)).BeginInit();
            this.kryptonSplitContainer2.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer2.Panel2)).BeginInit();
            this.kryptonSplitContainer2.Panel2.SuspendLayout();
            this.kryptonSplitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel5)).BeginInit();
            this.kryptonPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel6)).BeginInit();
            this.kryptonPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer3.Panel1)).BeginInit();
            this.kryptonSplitContainer3.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer3.Panel2)).BeginInit();
            this.kryptonSplitContainer3.Panel2.SuspendLayout();
            this.kryptonSplitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel7)).BeginInit();
            this.kryptonPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).BeginInit();
            this.kryptonPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNF)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // cbxFormDanfe
            // 
            this.cbxFormDanfe.AlwaysActive = false;
            this.cbxFormDanfe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFormDanfe.DropDownWidth = 140;
            this.cbxFormDanfe.FormattingEnabled = true;
            this.cbxFormDanfe.Items.AddRange(new object[] {
            "Retrato",
            "Paisagem"});
            this.cbxFormDanfe.Location = new System.Drawing.Point(3, 26);
            this.cbxFormDanfe.Name = "cbxFormDanfe";
            this.cbxFormDanfe.Size = new System.Drawing.Size(140, 21);
            this.cbxFormDanfe.StateActive.ComboBox.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cbxFormDanfe.StateCommon.ComboBox.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.cbxFormDanfe.StateCommon.ComboBox.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.cbxFormDanfe.StateDisabled.ComboBox.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.cbxFormDanfe.StateDisabled.ComboBox.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.cbxFormDanfe.TabIndex = 0;
            this.toolTip1.SetToolTip(this.cbxFormDanfe, "Tipo de Impressão da DANFE");
            // 
            // timerWebServices
            // 
            this.timerWebServices.Interval = 10000000;
            this.timerWebServices.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.Icon = ((System.Drawing.Icon)(resources.GetObject("errorProvider1.Icon")));
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.kryptonSplitContainer1);
            this.kryptonPanel2.Controls.Add(this.toolStrip1);
            this.kryptonPanel2.Controls.Add(this.statusStrip1);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(1079, 657);
            this.kryptonPanel2.TabIndex = 202;
            // 
            // kryptonSplitContainer1
            // 
            this.kryptonSplitContainer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.kryptonSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonSplitContainer1.Location = new System.Drawing.Point(0, 25);
            this.kryptonSplitContainer1.Name = "kryptonSplitContainer1";
            // 
            // kryptonSplitContainer1.Panel1
            // 
            this.kryptonSplitContainer1.Panel1.Controls.Add(this.headerMenuLateral);
            this.kryptonSplitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(0, 15, 0, 0);
            // 
            // kryptonSplitContainer1.Panel2
            // 
            this.kryptonSplitContainer1.Panel2.Controls.Add(this.dgvNF);
            this.kryptonSplitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(0, 15, 3, 3);
            this.kryptonSplitContainer1.Size = new System.Drawing.Size(1079, 610);
            this.kryptonSplitContainer1.SplitterDistance = 186;
            this.kryptonSplitContainer1.TabIndex = 233;
            this.kryptonSplitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.kryptonSplitContainer1_SplitterMoved);
            // 
            // headerMenuLateral
            // 
            this.headerMenuLateral.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup[] {
            this.btnMinimiza});
            this.headerMenuLateral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerMenuLateral.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.headerMenuLateral.HeaderVisibleSecondary = false;
            this.headerMenuLateral.Location = new System.Drawing.Point(0, 15);
            this.headerMenuLateral.Name = "headerMenuLateral";
            // 
            // headerMenuLateral.Panel
            // 
            this.headerMenuLateral.Panel.Controls.Add(this.kryptonSplitContainer2);
            this.headerMenuLateral.Panel.Padding = new System.Windows.Forms.Padding(2);
            this.headerMenuLateral.Size = new System.Drawing.Size(186, 595);
            this.headerMenuLateral.StateNormal.HeaderPrimary.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.headerMenuLateral.TabIndex = 230;
            this.headerMenuLateral.ValuesPrimary.Heading = "Status";
            this.headerMenuLateral.ValuesPrimary.Image = null;
            // 
            // btnMinimiza
            // 
            this.btnMinimiza.Type = ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.ArrowLeft;
            this.btnMinimiza.UniqueName = "266203CDA201483E72BF18ACC2C492DD";
            this.btnMinimiza.Click += new System.EventHandler(this.btnMinimiza_Click);
            // 
            // kryptonSplitContainer2
            // 
            this.kryptonSplitContainer2.Cursor = System.Windows.Forms.Cursors.Default;
            this.kryptonSplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonSplitContainer2.Location = new System.Drawing.Point(2, 2);
            this.kryptonSplitContainer2.Name = "kryptonSplitContainer2";
            this.kryptonSplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // kryptonSplitContainer2.Panel1
            // 
            this.kryptonSplitContainer2.Panel1.Controls.Add(this.kryptonPanel5);
            // 
            // kryptonSplitContainer2.Panel2
            // 
            this.kryptonSplitContainer2.Panel2.Controls.Add(this.kryptonPanel6);
            this.kryptonSplitContainer2.Size = new System.Drawing.Size(180, 567);
            this.kryptonSplitContainer2.SplitterDistance = 128;
            this.kryptonSplitContainer2.TabIndex = 230;
            // 
            // kryptonPanel5
            // 
            this.kryptonPanel5.Controls.Add(this.rbtAmbas);
            this.kryptonPanel5.Controls.Add(this.label9);
            this.kryptonPanel5.Controls.Add(this.rbtEnviadas);
            this.kryptonPanel5.Controls.Add(this.rbtNaoEnviadas);
            this.kryptonPanel5.Controls.Add(this.cbxFormDanfe);
            this.kryptonPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel5.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel5.Name = "kryptonPanel5";
            this.kryptonPanel5.Size = new System.Drawing.Size(180, 128);
            this.kryptonPanel5.TabIndex = 232;
            // 
            // rbtAmbas
            // 
            this.rbtAmbas.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
            this.rbtAmbas.Location = new System.Drawing.Point(3, 104);
            this.rbtAmbas.Name = "rbtAmbas";
            this.rbtAmbas.Size = new System.Drawing.Size(60, 20);
            this.rbtAmbas.TabIndex = 2;
            this.rbtAmbas.Values.Text = "&Ambas";
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
            this.label9.Location = new System.Drawing.Point(3, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 20);
            this.label9.TabIndex = 190;
            this.label9.Values.Text = "Impressão";
            // 
            // rbtEnviadas
            // 
            this.rbtEnviadas.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
            this.rbtEnviadas.Location = new System.Drawing.Point(3, 52);
            this.rbtEnviadas.Name = "rbtEnviadas";
            this.rbtEnviadas.Size = new System.Drawing.Size(70, 20);
            this.rbtEnviadas.TabIndex = 0;
            this.rbtEnviadas.Values.Text = "&Enviadas";
            // 
            // rbtNaoEnviadas
            // 
            this.rbtNaoEnviadas.Checked = true;
            this.rbtNaoEnviadas.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
            this.rbtNaoEnviadas.Location = new System.Drawing.Point(3, 78);
            this.rbtNaoEnviadas.Name = "rbtNaoEnviadas";
            this.rbtNaoEnviadas.Size = new System.Drawing.Size(96, 20);
            this.rbtNaoEnviadas.TabIndex = 1;
            this.rbtNaoEnviadas.Values.Text = "&Não Enviadas";
            // 
            // kryptonPanel6
            // 
            this.kryptonPanel6.Controls.Add(this.kryptonSplitContainer3);
            this.kryptonPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel6.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel6.Name = "kryptonPanel6";
            this.kryptonPanel6.Size = new System.Drawing.Size(180, 434);
            this.kryptonPanel6.TabIndex = 233;
            // 
            // kryptonSplitContainer3
            // 
            this.kryptonSplitContainer3.Cursor = System.Windows.Forms.Cursors.Default;
            this.kryptonSplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonSplitContainer3.Location = new System.Drawing.Point(0, 0);
            this.kryptonSplitContainer3.Name = "kryptonSplitContainer3";
            this.kryptonSplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // kryptonSplitContainer3.Panel1
            // 
            this.kryptonSplitContainer3.Panel1.Controls.Add(this.dtpIni);
            this.kryptonSplitContainer3.Panel1.Controls.Add(this.kryptonHeader1);
            this.kryptonSplitContainer3.Panel1.Controls.Add(this.dtpFim);
            this.kryptonSplitContainer3.Panel1.Controls.Add(this.rdbData);
            this.kryptonSplitContainer3.Panel1.Controls.Add(this.rdbNF);
            this.kryptonSplitContainer3.Panel1.Controls.Add(this.label16);
            this.kryptonSplitContainer3.Panel1.Controls.Add(this.txtNfIni);
            this.kryptonSplitContainer3.Panel1.Controls.Add(this.txtNfFim);
            this.kryptonSplitContainer3.Panel1.Controls.Add(this.label15);
            // 
            // kryptonSplitContainer3.Panel2
            // 
            this.kryptonSplitContainer3.Panel2.Controls.Add(this.kryptonPanel7);
            this.kryptonSplitContainer3.Size = new System.Drawing.Size(180, 434);
            this.kryptonSplitContainer3.SplitterDistance = 157;
            this.kryptonSplitContainer3.TabIndex = 231;
            // 
            // dtpIni
            // 
            this.dtpIni.AlwaysActive = false;
            this.dtpIni.CustomFormat = "dd/MM/yyyy";
            this.dtpIni.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpIni.Location = new System.Drawing.Point(39, 89);
            this.dtpIni.Name = "dtpIni";
            this.dtpIni.Size = new System.Drawing.Size(82, 21);
            this.dtpIni.StateActive.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dtpIni.StateActive.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.dtpIni.StateActive.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.dtpIni.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.dtpIni.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.dtpIni.TabIndex = 199;
            // 
            // kryptonHeader1
            // 
            this.kryptonHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeader1.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeader1.Name = "kryptonHeader1";
            this.kryptonHeader1.Size = new System.Drawing.Size(180, 23);
            this.kryptonHeader1.StateNormal.Content.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonHeader1.StateNormal.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonHeader1.TabIndex = 231;
            this.kryptonHeader1.Values.Description = "";
            this.kryptonHeader1.Values.Heading = "Filtro";
            this.kryptonHeader1.Values.Image = null;
            // 
            // dtpFim
            // 
            this.dtpFim.AlwaysActive = false;
            this.dtpFim.CustomFormat = "dd/MM/yyyy";
            this.dtpFim.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFim.Location = new System.Drawing.Point(39, 114);
            this.dtpFim.Name = "dtpFim";
            this.dtpFim.Size = new System.Drawing.Size(82, 21);
            this.dtpFim.StateActive.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dtpFim.StateActive.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.dtpFim.StateActive.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.dtpFim.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.dtpFim.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.dtpFim.TabIndex = 6;
            // 
            // rdbData
            // 
            this.rdbData.Checked = true;
            this.rdbData.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
            this.rdbData.Location = new System.Drawing.Point(3, 29);
            this.rdbData.Name = "rdbData";
            this.rdbData.Size = new System.Drawing.Size(70, 20);
            this.rdbData.TabIndex = 2;
            this.rdbData.Values.Text = "Por Data";
            this.rdbData.CheckedChanged += new System.EventHandler(this.rdbFiltros_CheckedChanged);
            // 
            // rdbNF
            // 
            this.rdbNF.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
            this.rdbNF.Location = new System.Drawing.Point(3, 55);
            this.rdbNF.Name = "rdbNF";
            this.rdbNF.Size = new System.Drawing.Size(100, 20);
            this.rdbNF.TabIndex = 3;
            this.rdbNF.Values.Text = "Por Sequência";
            this.rdbNF.CheckedChanged += new System.EventHandler(this.rdbFiltros_CheckedChanged);
            // 
            // label16
            // 
            this.label16.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
            this.label16.Location = new System.Drawing.Point(3, 116);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(29, 20);
            this.label16.TabIndex = 198;
            this.label16.Values.Text = "Até";
            // 
            // txtNfIni
            // 
            this.txtNfIni.AlwaysActive = false;
            this.txtNfIni.Location = new System.Drawing.Point(39, 89);
            this.txtNfIni.MaxLength = 9;
            this.txtNfIni.Name = "txtNfIni";
            this.txtNfIni.Size = new System.Drawing.Size(81, 20);
            this.txtNfIni.StateActive.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtNfIni.StateActive.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.txtNfIni.StateActive.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtNfIni.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtNfIni.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.txtNfIni.TabIndex = 0;
            this.txtNfIni.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNfIni.Visible = false;
            this.txtNfIni.Enter += new System.EventHandler(this.txtNfFim_Enter);
            this.txtNfIni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNfIni_KeyPress);
            this.txtNfIni.Validated += new System.EventHandler(this.txtNfIni_Validated);
            // 
            // txtNfFim
            // 
            this.txtNfFim.AlwaysActive = false;
            this.txtNfFim.Location = new System.Drawing.Point(39, 114);
            this.txtNfFim.MaxLength = 9;
            this.txtNfFim.Name = "txtNfFim";
            this.txtNfFim.Size = new System.Drawing.Size(82, 20);
            this.txtNfFim.StateActive.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtNfFim.StateActive.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.txtNfFim.StateActive.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtNfFim.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtNfFim.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.txtNfFim.TabIndex = 1;
            this.txtNfFim.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNfFim.Visible = false;
            this.txtNfFim.Enter += new System.EventHandler(this.txtNfFim_Enter);
            this.txtNfFim.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNfFim_KeyPress);
            this.txtNfFim.Validated += new System.EventHandler(this.txtNfFim_Validated);
            // 
            // label15
            // 
            this.label15.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
            this.label15.Location = new System.Drawing.Point(6, 89);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(26, 20);
            this.label15.TabIndex = 197;
            this.label15.Values.Text = "De";
            // 
            // kryptonPanel7
            // 
            this.kryptonPanel7.Controls.Add(this.panel1);
            this.kryptonPanel7.Controls.Add(this.panel2);
            this.kryptonPanel7.Controls.Add(this.panel5);
            this.kryptonPanel7.Controls.Add(this.panel6);
            this.kryptonPanel7.Controls.Add(this.kryptonPanel3);
            this.kryptonPanel7.Controls.Add(this.label17);
            this.kryptonPanel7.Controls.Add(this.kryptonHeader2);
            this.kryptonPanel7.Controls.Add(this.label5);
            this.kryptonPanel7.Controls.Add(this.label19);
            this.kryptonPanel7.Controls.Add(this.label6);
            this.kryptonPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel7.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel7.Name = "kryptonPanel7";
            this.kryptonPanel7.Size = new System.Drawing.Size(180, 272);
            this.kryptonPanel7.TabIndex = 233;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Red;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(6, 110);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(21, 20);
            this.panel1.TabIndex = 236;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Aquamarine;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Location = new System.Drawing.Point(6, 84);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(21, 20);
            this.panel2.TabIndex = 237;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Khaki;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Location = new System.Drawing.Point(6, 58);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(21, 20);
            this.panel5.TabIndex = 238;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.White;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel6.Location = new System.Drawing.Point(6, 32);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(21, 20);
            this.panel6.TabIndex = 239;
            // 
            // kryptonPanel3
            // 
            this.kryptonPanel3.Controls.Add(this.button1);
            this.kryptonPanel3.Controls.Add(this.txtMensagemContingencia);
            this.kryptonPanel3.Controls.Add(this.lblAvisoContingencia);
            this.kryptonPanel3.Location = new System.Drawing.Point(6, 139);
            this.kryptonPanel3.Name = "kryptonPanel3";
            this.kryptonPanel3.Size = new System.Drawing.Size(142, 138);
            this.kryptonPanel3.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 100);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 24);
            this.button1.TabIndex = 2;
            this.button1.Values.Text = "Buscar Pendências";
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // txtMensagemContingencia
            // 
            this.txtMensagemContingencia.AlwaysActive = false;
            this.txtMensagemContingencia.Location = new System.Drawing.Point(3, 29);
            this.txtMensagemContingencia.Multiline = true;
            this.txtMensagemContingencia.Name = "txtMensagemContingencia";
            this.txtMensagemContingencia.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMensagemContingencia.Size = new System.Drawing.Size(132, 65);
            this.txtMensagemContingencia.StateActive.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtMensagemContingencia.StateActive.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.txtMensagemContingencia.StateActive.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtMensagemContingencia.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtMensagemContingencia.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.txtMensagemContingencia.TabIndex = 1;
            this.txtMensagemContingencia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblAvisoContingencia
            // 
            this.lblAvisoContingencia.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvisoContingencia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblAvisoContingencia.Location = new System.Drawing.Point(42, 3);
            this.lblAvisoContingencia.Name = "lblAvisoContingencia";
            this.lblAvisoContingencia.Size = new System.Drawing.Size(59, 20);
            this.lblAvisoContingencia.TabIndex = 0;
            this.lblAvisoContingencia.Values.Text = "A V I S O";
            // 
            // label17
            // 
            this.label17.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
            this.label17.Location = new System.Drawing.Point(30, 58);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(72, 20);
            this.label17.TabIndex = 201;
            this.label17.Values.Text = "Canceladas";
            // 
            // kryptonHeader2
            // 
            this.kryptonHeader2.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeader2.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeader2.Name = "kryptonHeader2";
            this.kryptonHeader2.Size = new System.Drawing.Size(180, 23);
            this.kryptonHeader2.StateNormal.Content.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonHeader2.StateNormal.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonHeader2.TabIndex = 232;
            this.kryptonHeader2.Values.Description = "";
            this.kryptonHeader2.Values.Heading = "Legenda";
            this.kryptonHeader2.Values.Image = null;
            // 
            // label5
            // 
            this.label5.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
            this.label5.Location = new System.Drawing.Point(30, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 20);
            this.label5.TabIndex = 18;
            this.label5.Values.Text = "Pendentes";
            // 
            // label19
            // 
            this.label19.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
            this.label19.Location = new System.Drawing.Point(30, 32);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(78, 20);
            this.label19.TabIndex = 199;
            this.label19.Values.Text = "NF. Normais";
            // 
            // label6
            // 
            this.label6.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
            this.label6.Location = new System.Drawing.Point(30, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 20);
            this.label6.TabIndex = 20;
            this.label6.Values.Text = "Contingência";
            // 
            // dgvNF
            // 
            this.dgvNF.AllowUserToAddRows = false;
            this.dgvNF.AllowUserToDeleteRows = false;
            this.dgvNF.AllowUserToOrderColumns = true;
            this.dgvNF.ColumnHeadersHeight = 22;
            this.dgvNF.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ASSINANF,
            this.CD_NOTAFIS,
            this.cd_nfseq,
            this.DT_EMI,
            this.NM_GUERRA,
            this.vl_totnf,
            this.ST_NFE,
            this.CANCELADA,
            this.Imprime,
            this.st_contingencia,
            this.cd_recibocanc});
            this.dgvNF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNF.Location = new System.Drawing.Point(0, 15);
            this.dgvNF.Name = "dgvNF";
            this.dgvNF.RowHeadersVisible = false;
            this.dgvNF.RowHeadersWidth = 42;
            this.dgvNF.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvNF.RowTemplate.Height = 18;
            this.dgvNF.Size = new System.Drawing.Size(885, 592);
            this.dgvNF.TabIndex = 4;
            this.dgvNF.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNF_CellClick);
            this.dgvNF.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvNF_CellFormatting);
            this.dgvNF.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvNF_ColumnHeaderMouseClick);
            this.dgvNF.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmArquivosXml_KeyDown);
            // 
            // ASSINANF
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.NullValue = false;
            this.ASSINANF.DefaultCellStyle = dataGridViewCellStyle1;
            this.ASSINANF.FalseValue = null;
            this.ASSINANF.FillWeight = 75F;
            this.ASSINANF.HeaderText = "Enviar";
            this.ASSINANF.IndeterminateValue = null;
            this.ASSINANF.Name = "ASSINANF";
            this.ASSINANF.ReadOnly = true;
            this.ASSINANF.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ASSINANF.TrueValue = null;
            this.ASSINANF.Width = 40;
            // 
            // CD_NOTAFIS
            // 
            this.CD_NOTAFIS.DataPropertyName = "CD_NOTAFIS";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomLeft;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CD_NOTAFIS.DefaultCellStyle = dataGridViewCellStyle2;
            this.CD_NOTAFIS.FillWeight = 75F;
            this.CD_NOTAFIS.HeaderText = "NF";
            this.CD_NOTAFIS.Name = "CD_NOTAFIS";
            this.CD_NOTAFIS.ReadOnly = true;
            this.CD_NOTAFIS.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CD_NOTAFIS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CD_NOTAFIS.Width = 50;
            // 
            // cd_nfseq
            // 
            this.cd_nfseq.DataPropertyName = "CD_NFSEQ";
            this.cd_nfseq.FillWeight = 75F;
            this.cd_nfseq.HeaderText = "Sequência";
            this.cd_nfseq.Name = "cd_nfseq";
            this.cd_nfseq.ReadOnly = true;
            this.cd_nfseq.Width = 70;
            // 
            // DT_EMI
            // 
            this.DT_EMI.DataPropertyName = "DT_EMI";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DT_EMI.DefaultCellStyle = dataGridViewCellStyle3;
            this.DT_EMI.FillWeight = 80F;
            this.DT_EMI.HeaderText = "Emissão";
            this.DT_EMI.Name = "DT_EMI";
            this.DT_EMI.ReadOnly = true;
            this.DT_EMI.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DT_EMI.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DT_EMI.Width = 80;
            // 
            // NM_GUERRA
            // 
            this.NM_GUERRA.DataPropertyName = "NM_GUERRA";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomLeft;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NM_GUERRA.DefaultCellStyle = dataGridViewCellStyle4;
            this.NM_GUERRA.HeaderText = "Cliente";
            this.NM_GUERRA.Name = "NM_GUERRA";
            this.NM_GUERRA.ReadOnly = true;
            this.NM_GUERRA.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.NM_GUERRA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.NM_GUERRA.Width = 200;
            // 
            // vl_totnf
            // 
            this.vl_totnf.DataPropertyName = "vl_totnf";
            this.vl_totnf.FillWeight = 90F;
            this.vl_totnf.HeaderText = "Valor";
            this.vl_totnf.Name = "vl_totnf";
            this.vl_totnf.ReadOnly = true;
            this.vl_totnf.ToolTipText = "Valor total da Nota";
            this.vl_totnf.Width = 75;
            // 
            // ST_NFE
            // 
            this.ST_NFE.DataPropertyName = "ST_NFE";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.NullValue = false;
            this.ST_NFE.DefaultCellStyle = dataGridViewCellStyle5;
            this.ST_NFE.FalseValue = null;
            this.ST_NFE.FillWeight = 60F;
            this.ST_NFE.HeaderText = "Enviado";
            this.ST_NFE.IndeterminateValue = null;
            this.ST_NFE.Name = "ST_NFE";
            this.ST_NFE.ReadOnly = true;
            this.ST_NFE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ST_NFE.TrueValue = null;
            this.ST_NFE.Width = 60;
            // 
            // CANCELADA
            // 
            this.CANCELADA.DataPropertyName = "CANCELADA";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.NullValue = false;
            this.CANCELADA.DefaultCellStyle = dataGridViewCellStyle6;
            this.CANCELADA.FalseValue = null;
            this.CANCELADA.FillWeight = 70F;
            this.CANCELADA.HeaderText = "Cancelada";
            this.CANCELADA.IndeterminateValue = null;
            this.CANCELADA.Name = "CANCELADA";
            this.CANCELADA.ReadOnly = true;
            this.CANCELADA.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CANCELADA.TrueValue = null;
            this.CANCELADA.Width = 70;
            // 
            // Imprime
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.NullValue = false;
            this.Imprime.DefaultCellStyle = dataGridViewCellStyle7;
            this.Imprime.FalseValue = null;
            this.Imprime.FillWeight = 80F;
            this.Imprime.HeaderText = "Imprime";
            this.Imprime.IndeterminateValue = null;
            this.Imprime.Name = "Imprime";
            this.Imprime.ReadOnly = true;
            this.Imprime.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Imprime.TrueValue = null;
            this.Imprime.Width = 60;
            // 
            // st_contingencia
            // 
            this.st_contingencia.DataPropertyName = "st_contingencia";
            this.st_contingencia.HeaderText = "Envio Contingencia";
            this.st_contingencia.Name = "st_contingencia";
            this.st_contingencia.ReadOnly = true;
            this.st_contingencia.Visible = false;
            // 
            // cd_recibocanc
            // 
            this.cd_recibocanc.DataPropertyName = "cd_recibocanc";
            this.cd_recibocanc.HeaderText = "cd_recibocanc";
            this.cd_recibocanc.Name = "cd_recibocanc";
            this.cd_recibocanc.ReadOnly = true;
            this.cd_recibocanc.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnBuscaNotas,
            this.btnUsuario,
            this.btnEnviar,
            this.btnGerarContingencia,
            this.btnCancelanebto,
            this.btnImpressao,
            this.btnInutilizacao,
            this.btnConsultaStatus,
            this.btnBuscaRetorno,
            this.btnSituacaoNFe,
            this.tsConsultaCadastro});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1079, 25);
            this.toolStrip1.TabIndex = 232;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnBuscaNotas
            // 
            this.btnBuscaNotas.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnBuscaNotas.Image = global::GeraXml.Properties.Resources.pesquisar;
            this.btnBuscaNotas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscaNotas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBuscaNotas.Name = "btnBuscaNotas";
            this.btnBuscaNotas.Size = new System.Drawing.Size(77, 22);
            this.btnBuscaNotas.Text = "Pesquisar";
            this.btnBuscaNotas.ToolTipText = "Pesquisar";
            this.btnBuscaNotas.Click += new System.EventHandler(this.btnPesquisa_Click);
            // 
            // btnUsuario
            // 
            this.btnUsuario.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnUsuario.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnUsuario.Image = global::GeraXml.Properties.Resources.users;
            this.btnUsuario.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUsuario.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUsuario.Name = "btnUsuario";
            this.btnUsuario.Size = new System.Drawing.Size(57, 22);
            this.btnUsuario.Text = "Login";
            this.btnUsuario.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // btnEnviar
            // 
            this.btnEnviar.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnEnviar.Image = global::GeraXml.Properties.Resources.send;
            this.btnEnviar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEnviar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(59, 22);
            this.btnEnviar.Text = "Enviar";
            this.btnEnviar.ToolTipText = "Enviar";
            this.btnEnviar.Click += new System.EventHandler(this.btnGerarXml_Click);
            // 
            // btnGerarContingencia
            // 
            this.btnGerarContingencia.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnGerarContingencia.Image = global::GeraXml.Properties.Resources.xml;
            this.btnGerarContingencia.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGerarContingencia.Name = "btnGerarContingencia";
            this.btnGerarContingencia.Size = new System.Drawing.Size(152, 22);
            this.btnGerarContingencia.Text = "Gerar Xml Contingência";
            this.btnGerarContingencia.Click += new System.EventHandler(this.btnGerarContingencia_Click);
            // 
            // btnCancelanebto
            // 
            this.btnCancelanebto.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancelanebto.Image = global::GeraXml.Properties.Resources.cancel__3_;
            this.btnCancelanebto.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelanebto.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelanebto.Name = "btnCancelanebto";
            this.btnCancelanebto.Size = new System.Drawing.Size(96, 22);
            this.btnCancelanebto.Text = "Cancelar NFe";
            this.btnCancelanebto.ToolTipText = "Cancelar NFe";
            this.btnCancelanebto.Click += new System.EventHandler(this.btnCancNFe_Click);
            // 
            // btnImpressao
            // 
            this.btnImpressao.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnImpressao.Image = global::GeraXml.Properties.Resources.print;
            this.btnImpressao.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImpressao.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImpressao.Name = "btnImpressao";
            this.btnImpressao.Size = new System.Drawing.Size(76, 22);
            this.btnImpressao.Text = "Imprimir ";
            this.btnImpressao.ToolTipText = "Imprimir ";
            this.btnImpressao.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnInutilizacao
            // 
            this.btnInutilizacao.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnInutilizacao.Image = global::GeraXml.Properties.Resources.inutilzar;
            this.btnInutilizacao.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInutilizacao.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnInutilizacao.Name = "btnInutilizacao";
            this.btnInutilizacao.Size = new System.Drawing.Size(90, 22);
            this.btnInutilizacao.Text = "Inutilizar Nº";
            this.btnInutilizacao.ToolTipText = "Inutilizar Nº";
            this.btnInutilizacao.Click += new System.EventHandler(this.btnInutilizaNFe_Click);
            // 
            // btnConsultaStatus
            // 
            this.btnConsultaStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnConsultaStatus.Image = global::GeraXml.Properties.Resources.status;
            this.btnConsultaStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConsultaStatus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnConsultaStatus.Name = "btnConsultaStatus";
            this.btnConsultaStatus.Size = new System.Drawing.Size(66, 22);
            this.btnConsultaStatus.Text = "Serviço";
            this.btnConsultaStatus.ToolTipText = "Serviço";
            this.btnConsultaStatus.Click += new System.EventHandler(this.btnConsultaServico_Click);
            // 
            // btnBuscaRetorno
            // 
            this.btnBuscaRetorno.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnBuscaRetorno.Image = global::GeraXml.Properties.Resources.retorno;
            this.btnBuscaRetorno.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscaRetorno.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBuscaRetorno.Name = "btnBuscaRetorno";
            this.btnBuscaRetorno.Size = new System.Drawing.Size(107, 22);
            this.btnBuscaRetorno.Text = "Buscar Retorno";
            this.btnBuscaRetorno.ToolTipText = "Buscar Retorno";
            this.btnBuscaRetorno.Click += new System.EventHandler(this.btnBuscaRetFazenda_Click);
            // 
            // btnSituacaoNFe
            // 
            this.btnSituacaoNFe.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnSituacaoNFe.Image = global::GeraXml.Properties.Resources.situacao;
            this.btnSituacaoNFe.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSituacaoNFe.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSituacaoNFe.Name = "btnSituacaoNFe";
            this.btnSituacaoNFe.Size = new System.Drawing.Size(101, 22);
            this.btnSituacaoNFe.Text = "Consultar NFe";
            this.btnSituacaoNFe.ToolTipText = "Consultar Situação";
            this.btnSituacaoNFe.Click += new System.EventHandler(this.btnConsultaNF_Click);
            // 
            // tsConsultaCadastro
            // 
            this.tsConsultaCadastro.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.tsConsultaCadastro.Image = global::GeraXml.Properties.Resources.cliente;
            this.tsConsultaCadastro.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsConsultaCadastro.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsConsultaCadastro.Name = "tsConsultaCadastro";
            this.tsConsultaCadastro.Size = new System.Drawing.Size(73, 22);
            this.tsConsultaCadastro.Text = "Situação";
            this.tsConsultaCadastro.ToolTipText = "Serviço";
            this.tsConsultaCadastro.Click += new System.EventHandler(this.tsConsultaCadastro_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Transparent;
            this.statusStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.statusStrip1.Location = new System.Drawing.Point(0, 635);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip1.Size = new System.Drawing.Size(1079, 22);
            this.statusStrip1.TabIndex = 200;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // sslStatusEnvio
            // 
            this.sslStatusEnvio.BackColor = System.Drawing.Color.Transparent;
            this.sslStatusEnvio.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sslStatusEnvio.Name = "sslStatusEnvio";
            this.sslStatusEnvio.Size = new System.Drawing.Size(0, 0);
            // 
            // lblStatusContingencia
            // 
            this.lblStatusContingencia.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusContingencia.ForeColor = System.Drawing.Color.Red;
            this.lblStatusContingencia.Name = "lblStatusContingencia";
            this.lblStatusContingencia.Size = new System.Drawing.Size(0, 0);
            // 
            // nmEmpresa
            // 
            this.nmEmpresa.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nmEmpresa.Name = "nmEmpresa";
            this.nmEmpresa.Size = new System.Drawing.Size(0, 0);
            // 
            // nmStatus
            // 
            this.nmStatus.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nmStatus.Name = "nmStatus";
            this.nmStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "CD_NOTAFIS";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomLeft;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn1.FillWeight = 75F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Nota Fiscal";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 75;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "CD_NFSEQ";
            this.dataGridViewTextBoxColumn2.FillWeight = 75F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Sequência";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 75;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "DT_EMI";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn3.FillWeight = 80F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Emissão";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 80;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "NM_GUERRA";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomLeft;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewTextBoxColumn4.HeaderText = "Cliente";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn4.Width = 255;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "vl_totnf";
            this.dataGridViewTextBoxColumn5.FillWeight = 90F;
            this.dataGridViewTextBoxColumn5.HeaderText = "Valor";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.ToolTipText = "Valor total da Nota";
            this.dataGridViewTextBoxColumn5.Width = 90;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "st_contingencia";
            this.dataGridViewTextBoxColumn6.HeaderText = "cl_Contingencia";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Visible = false;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "cd_recibocanc";
            this.dataGridViewTextBoxColumn7.HeaderText = "cd_recibocanc";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Visible = false;
            // 
            // timerInicializacao
            // 
            this.timerInicializacao.Interval = 10;
            this.timerInicializacao.Tick += new System.EventHandler(this.timerInicializacao_Tick);
            // 
            // frmArquivosXmlNfe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1079, 657);
            this.Controls.Add(this.kryptonPanel2);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmArquivosXmlNfe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gerar NF-e";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmArquivosXml_FormClosed);
            this.Load += new System.EventHandler(this.frmArquivosXml_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmArquivosXml_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.cbxFormDanfe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.kryptonPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel1)).EndInit();
            this.kryptonSplitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel2)).EndInit();
            this.kryptonSplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1)).EndInit();
            this.kryptonSplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.headerMenuLateral.Panel)).EndInit();
            this.headerMenuLateral.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.headerMenuLateral)).EndInit();
            this.headerMenuLateral.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer2.Panel1)).EndInit();
            this.kryptonSplitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer2.Panel2)).EndInit();
            this.kryptonSplitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer2)).EndInit();
            this.kryptonSplitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel5)).EndInit();
            this.kryptonPanel5.ResumeLayout(false);
            this.kryptonPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel6)).EndInit();
            this.kryptonPanel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer3.Panel1)).EndInit();
            this.kryptonSplitContainer3.Panel1.ResumeLayout(false);
            this.kryptonSplitContainer3.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer3.Panel2)).EndInit();
            this.kryptonSplitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer3)).EndInit();
            this.kryptonSplitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel7)).EndInit();
            this.kryptonPanel7.ResumeLayout(false);
            this.kryptonPanel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).EndInit();
            this.kryptonPanel3.ResumeLayout(false);
            this.kryptonPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNF)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

}

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.Timer timerWebServices;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.Timer timer1;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel sslStatusEnvio;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusContingencia;
        public KryptonDataGridView dgvNF;
        private KryptonButton button1;
        private KryptonTextBox txtMensagemContingencia;
        private KryptonLabel lblAvisoContingencia;
        private KryptonLabel label9;
        private KryptonLabel label17;
        private KryptonLabel label19;
        private KryptonLabel label6;
        private KryptonLabel label5;
        public KryptonComboBox cbxFormDanfe;
        public KryptonDateTimePicker dtpFim;
        private KryptonRadioButton rdbData;
        private KryptonTextBox txtNfFim;
        private KryptonTextBox txtNfIni;
        private KryptonRadioButton rdbNF;
        private KryptonLabel label15;
        private KryptonLabel label16;
        private KryptonRadioButton rbtAmbas;
        private KryptonRadioButton rbtNaoEnviadas;
        private KryptonRadioButton rbtEnviadas;
        private ToolStripStatusLabel nmEmpresa;
        private ToolStripStatusLabel nmStatus;
        private KryptonDataGridViewCheckBoxColumn ASSINANF;
        private DataGridViewTextBoxColumn CD_NOTAFIS;
        private DataGridViewTextBoxColumn cd_nfseq;
        private DataGridViewTextBoxColumn DT_EMI;
        private DataGridViewTextBoxColumn NM_GUERRA;
        private DataGridViewTextBoxColumn vl_totnf;
        private KryptonDataGridViewCheckBoxColumn ST_NFE;
        private KryptonDataGridViewCheckBoxColumn CANCELADA;
        private KryptonDataGridViewCheckBoxColumn Imprime;
        private DataGridViewTextBoxColumn st_contingencia;
        private DataGridViewTextBoxColumn cd_recibocanc;
        public KryptonDateTimePicker dtpIni;
       
        private ToolStripSeparator toolStripSeparator1;
      
        private ToolStripSeparator toolStripSeparator3;
      
        private ToolStripSeparator toolStripSeparator2;
       
        private ToolStripSeparator toolStripSeparator4;
       
        private ToolStripSeparator toolStripSeparator5;
         
        private ToolStripSeparator toolStripSeparator6;
       
        private ToolStripSeparator toolStripSeparator7;
     
        private ToolStripSeparator toolStripSeparator8;
       
        public KryptonHeaderGroup headerMenuLateral;
        private ButtonSpecHeaderGroup btnMinimiza;
        private KryptonSplitContainer kryptonSplitContainer2;
        private KryptonPanel kryptonPanel5;
        private KryptonPanel kryptonPanel6;
        private KryptonSplitContainer kryptonSplitContainer3;
        private KryptonHeader kryptonHeader1;
        private KryptonPanel kryptonPanel7;
        private KryptonHeader kryptonHeader2;
        private KryptonPanel kryptonPanel3;
        private KryptonSplitContainer kryptonSplitContainer1;
        private Panel panel1;
        private Panel panel2;
        private Panel panel5;
        private Panel panel6;
        private ToolStrip toolStrip1;
        private ToolStripButton btnBuscaNotas;
        private ToolStripButton btnUsuario;
        private ToolStripButton btnEnviar;
        private ToolStripButton btnGerarContingencia;
        private ToolStripButton btnCancelanebto;
        private ToolStripButton btnImpressao;
        private ToolStripButton btnInutilizacao;
        private ToolStripButton btnConsultaStatus;
        private ToolStripButton btnBuscaRetorno;
        private ToolStripButton btnSituacaoNFe;
        private ToolStripButton tsConsultaCadastro;
        private Timer timerInicializacao;

    }
}