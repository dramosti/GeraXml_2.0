using ComponentFactory.Krypton.Toolkit;
using AC.ExtendedRenderer.Navigator;
namespace NfeGerarXml.NFe
{
    partial class frmEmailContadorNfe
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEmailContadorNfe));
            this.dataGridView1 = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.bSelectDataGridViewCheckBoxColumn = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewCheckBoxColumn();
            this.Ienviado = new System.Windows.Forms.DataGridViewImageColumn();
            this.mesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sAnoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTotalArquivos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iEnviadoContador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.belEmailContadorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.kryptonPanel4 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.tabControl1 = new AC.ExtendedRenderer.Navigator.KryptonTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.kryptonPanel2 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.Contador = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.label1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtContador = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtCopia = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.lblStatus = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.kryptonPanel3 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.label2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonButton2 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.cbxDia = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.chkMensal = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.chkSemanal = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.belEmailContadorBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel4)).BeginInit();
            this.kryptonPanel4.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).BeginInit();
            this.kryptonPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxDia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeight = 20;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.bSelectDataGridViewCheckBoxColumn,
            this.Ienviado,
            this.mesDataGridViewTextBoxColumn,
            this.sAnoDataGridViewTextBoxColumn,
            this.iIdDataGridViewTextBoxColumn,
            this.iTotalArquivos,
            this.iEnviadoContador});
            this.dataGridView1.DataSource = this.belEmailContadorBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 43;
            this.dataGridView1.RowTemplate.Height = 19;
            this.dataGridView1.Size = new System.Drawing.Size(394, 255);
            this.dataGridView1.TabIndex = 0;
            // 
            // bSelectDataGridViewCheckBoxColumn
            // 
            this.bSelectDataGridViewCheckBoxColumn.DataPropertyName = "bSelect";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = false;
            this.bSelectDataGridViewCheckBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.bSelectDataGridViewCheckBoxColumn.FalseValue = null;
            this.bSelectDataGridViewCheckBoxColumn.HeaderText = "Enviar";
            this.bSelectDataGridViewCheckBoxColumn.IndeterminateValue = null;
            this.bSelectDataGridViewCheckBoxColumn.Name = "bSelectDataGridViewCheckBoxColumn";
            this.bSelectDataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.bSelectDataGridViewCheckBoxColumn.ToolTipText = "Marcar para enviar";
            this.bSelectDataGridViewCheckBoxColumn.TrueValue = null;
            this.bSelectDataGridViewCheckBoxColumn.Width = 45;
            // 
            // Ienviado
            // 
            this.Ienviado.DataPropertyName = "Ienviado";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle2.NullValue")));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            this.Ienviado.DefaultCellStyle = dataGridViewCellStyle2;
            this.Ienviado.HeaderText = "";
            this.Ienviado.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.Ienviado.Name = "Ienviado";
            this.Ienviado.ReadOnly = true;
            this.Ienviado.ToolTipText = "Status da Pasta";
            this.Ienviado.Width = 15;
            // 
            // mesDataGridViewTextBoxColumn
            // 
            this.mesDataGridViewTextBoxColumn.DataPropertyName = "Mes";
            this.mesDataGridViewTextBoxColumn.HeaderText = "Mês";
            this.mesDataGridViewTextBoxColumn.Name = "mesDataGridViewTextBoxColumn";
            this.mesDataGridViewTextBoxColumn.ReadOnly = true;
            this.mesDataGridViewTextBoxColumn.ToolTipText = "Mês";
            // 
            // sAnoDataGridViewTextBoxColumn
            // 
            this.sAnoDataGridViewTextBoxColumn.DataPropertyName = "sAno";
            this.sAnoDataGridViewTextBoxColumn.HeaderText = "Ano";
            this.sAnoDataGridViewTextBoxColumn.Name = "sAnoDataGridViewTextBoxColumn";
            this.sAnoDataGridViewTextBoxColumn.ReadOnly = true;
            this.sAnoDataGridViewTextBoxColumn.ToolTipText = "Ano";
            this.sAnoDataGridViewTextBoxColumn.Width = 60;
            // 
            // iIdDataGridViewTextBoxColumn
            // 
            this.iIdDataGridViewTextBoxColumn.DataPropertyName = "iId";
            this.iIdDataGridViewTextBoxColumn.HeaderText = "iId";
            this.iIdDataGridViewTextBoxColumn.Name = "iIdDataGridViewTextBoxColumn";
            this.iIdDataGridViewTextBoxColumn.ReadOnly = true;
            this.iIdDataGridViewTextBoxColumn.Visible = false;
            // 
            // iTotalArquivos
            // 
            this.iTotalArquivos.DataPropertyName = "iFaltantes";
            this.iTotalArquivos.HeaderText = "Faltantes";
            this.iTotalArquivos.Name = "iTotalArquivos";
            this.iTotalArquivos.ReadOnly = true;
            this.iTotalArquivos.Width = 60;
            // 
            // iEnviadoContador
            // 
            this.iEnviadoContador.DataPropertyName = "iEnviadoContador";
            this.iEnviadoContador.HeaderText = "Enviados";
            this.iEnviadoContador.Name = "iEnviadoContador";
            this.iEnviadoContador.ReadOnly = true;
            // 
            // belEmailContadorBindingSource
            // 
            this.belEmailContadorBindingSource.DataSource = typeof(HLP.bel.NFe.belEmailContador);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.kryptonPanel1);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(398, 434);
            this.panel1.TabIndex = 1;
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.dataGridView1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(394, 255);
            this.kryptonPanel1.TabIndex = 2;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.splitter1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 255);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(394, 3);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.kryptonPanel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 258);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(394, 172);
            this.panel2.TabIndex = 0;
            // 
            // kryptonPanel4
            // 
            this.kryptonPanel4.Controls.Add(this.tabControl1);
            this.kryptonPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel4.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel4.Name = "kryptonPanel4";
            this.kryptonPanel4.Size = new System.Drawing.Size(390, 168);
            this.kryptonPanel4.TabIndex = 10;
            // 
            // tabControl1
            // 
            this.tabControl1.AllowCloseButton = false;
            this.tabControl1.AllowContextButton = false;
            this.tabControl1.AllowNavigatorButtons = false;
            this.tabControl1.AllowSelectedTabHigh = false;
            this.tabControl1.BorderWidth = 1;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.CornerRoundRadiusWidth = 12;
            this.tabControl1.CornerSymmetry = AC.ExtendedRenderer.Navigator.KryptonTabControl.CornSymmetry.Both;
            this.tabControl1.CornerType = AC.ExtendedRenderer.Toolkit.Drawing.DrawingMethods.CornerType.Rounded;
            this.tabControl1.CornerWidth = AC.ExtendedRenderer.Navigator.KryptonTabControl.CornWidth.Thin;
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.HotTrack = true;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.PreserveTabColor = false;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(390, 168);
            this.tabControl1.TabIndex = 9;
            this.tabControl1.UseExtendedLayout = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.kryptonPanel2);
            this.tabPage1.Controls.Add(this.lblStatus);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(382, 139);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Tag = false;
            this.tabPage1.Text = "Envio";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.Contador);
            this.kryptonPanel2.Controls.Add(this.kryptonButton1);
            this.kryptonPanel2.Controls.Add(this.label1);
            this.kryptonPanel2.Controls.Add(this.txtContador);
            this.kryptonPanel2.Controls.Add(this.txtCopia);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel2.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(382, 139);
            this.kryptonPanel2.TabIndex = 10;
            // 
            // Contador
            // 
            this.Contador.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
            this.Contador.Location = new System.Drawing.Point(7, 5);
            this.Contador.Name = "Contador";
            this.Contador.Size = new System.Drawing.Size(99, 20);
            this.Contador.TabIndex = 4;
            this.Contador.Values.Text = "E-mail Contador";
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(10, 108);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(348, 21);
            this.kryptonButton1.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.kryptonButton1.TabIndex = 37;
            this.kryptonButton1.Values.Text = "Enviar E-mail";
            this.kryptonButton1.Click += new System.EventHandler(this.btnEnviarEmail_Click);
            // 
            // label1
            // 
            this.label1.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
            this.label1.Location = new System.Drawing.Point(11, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 20);
            this.label1.TabIndex = 6;
            this.label1.Values.Text = "Cc";
            // 
            // txtContador
            // 
            this.txtContador.BackColor = System.Drawing.SystemColors.Control;
            this.txtContador.Location = new System.Drawing.Point(10, 26);
            this.txtContador.MaxLength = 70;
            this.txtContador.Name = "txtContador";
            this.txtContador.ReadOnly = true;
            this.txtContador.Size = new System.Drawing.Size(348, 20);
            this.txtContador.StateActive.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtContador.TabIndex = 5;
            // 
            // txtCopia
            // 
            this.txtCopia.Location = new System.Drawing.Point(10, 66);
            this.txtCopia.MaxLength = 70;
            this.txtCopia.Name = "txtCopia";
            this.txtCopia.Size = new System.Drawing.Size(348, 20);
            this.txtCopia.TabIndex = 7;
            this.txtCopia.Validating += new System.ComponentModel.CancelEventHandler(this.txtCopia_Validating);
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(7, 123);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(39, 20);
            this.lblStatus.TabIndex = 9;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.kryptonPanel3);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(382, 139);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Tag = false;
            this.tabPage2.Text = "Configuração";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // kryptonPanel3
            // 
            this.kryptonPanel3.Controls.Add(this.label2);
            this.kryptonPanel3.Controls.Add(this.kryptonButton2);
            this.kryptonPanel3.Controls.Add(this.cbxDia);
            this.kryptonPanel3.Controls.Add(this.chkMensal);
            this.kryptonPanel3.Controls.Add(this.chkSemanal);
            this.kryptonPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel3.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel3.Name = "kryptonPanel3";
            this.kryptonPanel3.Size = new System.Drawing.Size(382, 139);
            this.kryptonPanel3.TabIndex = 38;
            // 
            // label2
            // 
            this.label2.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
            this.label2.Location = new System.Drawing.Point(3, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(312, 20);
            this.label2.TabIndex = 5;
            this.label2.Values.Text = "Deseja ser alertado do Envio para o Contador quando ?";
            // 
            // kryptonButton2
            // 
            this.kryptonButton2.Location = new System.Drawing.Point(6, 93);
            this.kryptonButton2.Name = "kryptonButton2";
            this.kryptonButton2.Size = new System.Drawing.Size(106, 21);
            this.kryptonButton2.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.kryptonButton2.TabIndex = 37;
            this.kryptonButton2.Values.Text = "Salvar Configuração";
            this.kryptonButton2.Click += new System.EventHandler(this.btnSalvarDia_Click);
            // 
            // cbxDia
            // 
            this.cbxDia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDia.DropDownWidth = 128;
            this.cbxDia.Enabled = false;
            this.cbxDia.FormattingEnabled = true;
            this.cbxDia.Location = new System.Drawing.Point(108, 31);
            this.cbxDia.Name = "cbxDia";
            this.cbxDia.Size = new System.Drawing.Size(128, 21);
            this.cbxDia.TabIndex = 0;
            // 
            // chkMensal
            // 
            this.chkMensal.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
            this.chkMensal.Location = new System.Drawing.Point(6, 58);
            this.chkMensal.Name = "chkMensal";
            this.chkMensal.Size = new System.Drawing.Size(97, 20);
            this.chkMensal.TabIndex = 7;
            this.chkMensal.Text = "Mensalmente";
            this.chkMensal.Values.Text = "Mensalmente";
            this.chkMensal.Click += new System.EventHandler(this.checkBox1_Click);
            // 
            // chkSemanal
            // 
            this.chkSemanal.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
            this.chkSemanal.Location = new System.Drawing.Point(6, 35);
            this.chkSemanal.Name = "chkSemanal";
            this.chkSemanal.Size = new System.Drawing.Size(104, 20);
            this.chkSemanal.TabIndex = 6;
            this.chkSemanal.Text = "Semanalmente";
            this.chkSemanal.Values.Text = "Semanalmente";
            this.chkSemanal.Click += new System.EventHandler(this.checkBox1_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Mes";
            this.dataGridViewTextBoxColumn1.HeaderText = "Mes";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.ToolTipText = "Mês";
            this.dataGridViewTextBoxColumn1.Width = 60;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "sAno";
            this.dataGridViewTextBoxColumn2.HeaderText = "Ano";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.ToolTipText = "Ano";
            this.dataGridViewTextBoxColumn2.Width = 60;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "iId";
            this.dataGridViewTextBoxColumn3.HeaderText = "iId";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Visible = false;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "sCaminho";
            this.dataGridViewTextBoxColumn4.HeaderText = "sCaminho";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Visible = false;
            this.dataGridViewTextBoxColumn4.Width = 230;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "iFaltantes";
            this.dataGridViewTextBoxColumn5.HeaderText = "Faltantes";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 60;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "iEnviadoContador";
            this.dataGridViewTextBoxColumn6.HeaderText = "Enviado Email";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // frmEmailContadorNfe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 434);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmEmailContadorNfe";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enviar Xml  p/ \'Contador\'";
            this.Load += new System.EventHandler(this.frmEmailContador_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmEmailContadorNfe_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.belEmailContadorBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel4)).EndInit();
            this.kryptonPanel4.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.kryptonPanel2.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).EndInit();
            this.kryptonPanel3.ResumeLayout(false);
            this.kryptonPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxDia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonDataGridView dataGridView1;
        private System.Windows.Forms.BindingSource belEmailContadorBindingSource;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel2;
        private KryptonTextBox txtCopia;
        private KryptonLabel label1;
        private KryptonTextBox txtContador;
        private KryptonLabel Contador;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.DataGridViewTextBoxColumn sCaminhoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private KryptonComboBox cbxDia;
        private AC.ExtendedRenderer.Navigator.KryptonTabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private KryptonLabel label2;
        private KryptonCheckBox chkMensal;
        private KryptonCheckBox chkSemanal;
        private KryptonLabel lblStatus;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton2;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel4;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel2;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel3;
        private KryptonDataGridViewCheckBoxColumn bSelectDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewImageColumn Ienviado;
        private System.Windows.Forms.DataGridViewTextBoxColumn mesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sAnoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTotalArquivos;
        private System.Windows.Forms.DataGridViewTextBoxColumn iEnviadoContador;
    }
}