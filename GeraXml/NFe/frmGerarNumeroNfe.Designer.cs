using ComponentFactory.Krypton.Toolkit;
using System.Windows.Forms;
namespace NfeGerarXml
{
    partial class frmGerarNumeroNfe
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGerarNumeroNfe));
            this.pnlDatas = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonGroupBox6 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.cmbGF = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonGroupBox3 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.dtpEmissao = new ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker();
            this.kryptonGroupBox5 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.dtpHoraSaida = new ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker();
            this.kryptonGroupBox4 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.dtpSaida = new ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker();
            this.panel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.grdNumeroASerEmi = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.txtNumeroASerEmi = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.grdNumeroUltNF = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.txtNumeroUltNF = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.btnGerar = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.ttpGF = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pnlDatas)).BeginInit();
            this.pnlDatas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox6)).BeginInit();
            this.kryptonGroupBox6.Panel.SuspendLayout();
            this.kryptonGroupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox3)).BeginInit();
            this.kryptonGroupBox3.Panel.SuspendLayout();
            this.kryptonGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox5)).BeginInit();
            this.kryptonGroupBox5.Panel.SuspendLayout();
            this.kryptonGroupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox4)).BeginInit();
            this.kryptonGroupBox4.Panel.SuspendLayout();
            this.kryptonGroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdNumeroASerEmi)).BeginInit();
            this.grdNumeroASerEmi.Panel.SuspendLayout();
            this.grdNumeroASerEmi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdNumeroUltNF)).BeginInit();
            this.grdNumeroUltNF.Panel.SuspendLayout();
            this.grdNumeroUltNF.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlDatas
            // 
            this.pnlDatas.Controls.Add(this.kryptonGroupBox6);
            this.pnlDatas.Controls.Add(this.kryptonGroupBox3);
            this.pnlDatas.Controls.Add(this.kryptonGroupBox5);
            this.pnlDatas.Controls.Add(this.kryptonGroupBox4);
            this.pnlDatas.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDatas.Location = new System.Drawing.Point(0, 0);
            this.pnlDatas.Name = "pnlDatas";
            this.pnlDatas.Size = new System.Drawing.Size(403, 71);
            this.pnlDatas.TabIndex = 0;
            // 
            // kryptonGroupBox6
            // 
            this.kryptonGroupBox6.Location = new System.Drawing.Point(337, 6);
            this.kryptonGroupBox6.Name = "kryptonGroupBox6";
            // 
            // kryptonGroupBox6.Panel
            // 
            this.kryptonGroupBox6.Panel.Controls.Add(this.cmbGF);
            this.kryptonGroupBox6.Size = new System.Drawing.Size(59, 59);
            this.kryptonGroupBox6.TabIndex = 8;
            this.kryptonGroupBox6.Text = "G.F";
            this.kryptonGroupBox6.Values.Heading = "G.F";
            // 
            // cmbGF
            // 
            this.cmbGF.AlwaysActive = false;
            this.cmbGF.DropDownWidth = 42;
            this.cmbGF.FormattingEnabled = true;
            this.cmbGF.Location = new System.Drawing.Point(5, 8);
            this.cmbGF.Name = "cmbGF";
            this.cmbGF.Size = new System.Drawing.Size(42, 21);
            this.cmbGF.StateActive.ComboBox.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmbGF.StateActive.ComboBox.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.cmbGF.StateActive.ComboBox.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.cmbGF.StateCommon.ComboBox.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.cmbGF.StateCommon.ComboBox.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.cmbGF.TabIndex = 0;
            this.cmbGF.TabIndexChanged += new System.EventHandler(this.cmbGF_TabIndexChanged);
            this.cmbGF.Validated += new System.EventHandler(this.cmbGF_Validated);
            // 
            // kryptonGroupBox3
            // 
            this.kryptonGroupBox3.Location = new System.Drawing.Point(10, 6);
            this.kryptonGroupBox3.Name = "kryptonGroupBox3";
            // 
            // kryptonGroupBox3.Panel
            // 
            this.kryptonGroupBox3.Panel.Controls.Add(this.dtpEmissao);
            this.kryptonGroupBox3.Size = new System.Drawing.Size(102, 59);
            this.kryptonGroupBox3.TabIndex = 5;
            this.kryptonGroupBox3.Text = "Data Emissão";
            this.kryptonGroupBox3.Values.Heading = "Data Emissão";
            // 
            // dtpEmissao
            // 
            this.dtpEmissao.AlwaysActive = false;
            this.dtpEmissao.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEmissao.Location = new System.Drawing.Point(6, 8);
            this.dtpEmissao.Name = "dtpEmissao";
            this.dtpEmissao.Size = new System.Drawing.Size(87, 21);
            this.dtpEmissao.StateActive.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dtpEmissao.StateActive.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.dtpEmissao.StateActive.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.dtpEmissao.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.dtpEmissao.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.dtpEmissao.TabIndex = 2;
            // 
            // kryptonGroupBox5
            // 
            this.kryptonGroupBox5.Location = new System.Drawing.Point(226, 6);
            this.kryptonGroupBox5.Name = "kryptonGroupBox5";
            // 
            // kryptonGroupBox5.Panel
            // 
            this.kryptonGroupBox5.Panel.Controls.Add(this.dtpHoraSaida);
            this.kryptonGroupBox5.Size = new System.Drawing.Size(102, 59);
            this.kryptonGroupBox5.TabIndex = 7;
            this.kryptonGroupBox5.Text = "Hora Saída";
            this.kryptonGroupBox5.Values.Heading = "Hora Saída";
            // 
            // dtpHoraSaida
            // 
            this.dtpHoraSaida.AlwaysActive = false;
            this.dtpHoraSaida.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpHoraSaida.Location = new System.Drawing.Point(4, 8);
            this.dtpHoraSaida.Name = "dtpHoraSaida";
            this.dtpHoraSaida.Size = new System.Drawing.Size(90, 21);
            this.dtpHoraSaida.StateActive.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dtpHoraSaida.StateActive.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.dtpHoraSaida.StateActive.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.dtpHoraSaida.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.dtpHoraSaida.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.dtpHoraSaida.TabIndex = 2;
            // 
            // kryptonGroupBox4
            // 
            this.kryptonGroupBox4.Location = new System.Drawing.Point(118, 6);
            this.kryptonGroupBox4.Name = "kryptonGroupBox4";
            // 
            // kryptonGroupBox4.Panel
            // 
            this.kryptonGroupBox4.Panel.Controls.Add(this.dtpSaida);
            this.kryptonGroupBox4.Size = new System.Drawing.Size(102, 59);
            this.kryptonGroupBox4.TabIndex = 6;
            this.kryptonGroupBox4.Text = "Data Saída";
            this.kryptonGroupBox4.Values.Heading = "Data Saída";
            // 
            // dtpSaida
            // 
            this.dtpSaida.AlwaysActive = false;
            this.dtpSaida.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpSaida.Location = new System.Drawing.Point(5, 8);
            this.dtpSaida.Name = "dtpSaida";
            this.dtpSaida.Size = new System.Drawing.Size(87, 21);
            this.dtpSaida.StateActive.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dtpSaida.StateActive.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.dtpSaida.StateActive.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.dtpSaida.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.dtpSaida.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.dtpSaida.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grdNumeroASerEmi);
            this.panel1.Controls.Add(this.grdNumeroUltNF);
            this.panel1.Controls.Add(this.btnGerar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 71);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(403, 131);
            this.panel1.TabIndex = 1;
            // 
            // grdNumeroASerEmi
            // 
            this.grdNumeroASerEmi.Location = new System.Drawing.Point(199, 6);
            this.grdNumeroASerEmi.Name = "grdNumeroASerEmi";
            // 
            // grdNumeroASerEmi.Panel
            // 
            this.grdNumeroASerEmi.Panel.Controls.Add(this.txtNumeroASerEmi);
            this.grdNumeroASerEmi.Size = new System.Drawing.Size(197, 59);
            this.grdNumeroASerEmi.TabIndex = 4;
            this.grdNumeroASerEmi.Text = "Número a ser Emitido";
            this.grdNumeroASerEmi.Values.Heading = "Número a ser Emitido";
            // 
            // txtNumeroASerEmi
            // 
            this.txtNumeroASerEmi.AlwaysActive = false;
            this.txtNumeroASerEmi.Location = new System.Drawing.Point(4, 9);
            this.txtNumeroASerEmi.MaxLength = 6;
            this.txtNumeroASerEmi.Name = "txtNumeroASerEmi";
            this.txtNumeroASerEmi.Size = new System.Drawing.Size(181, 20);
            this.txtNumeroASerEmi.StateActive.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtNumeroASerEmi.StateActive.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.txtNumeroASerEmi.StateActive.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtNumeroASerEmi.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtNumeroASerEmi.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.txtNumeroASerEmi.TabIndex = 0;
            // 
            // grdNumeroUltNF
            // 
            this.grdNumeroUltNF.Location = new System.Drawing.Point(10, 6);
            this.grdNumeroUltNF.Name = "grdNumeroUltNF";
            // 
            // grdNumeroUltNF.Panel
            // 
            this.grdNumeroUltNF.Panel.Controls.Add(this.txtNumeroUltNF);
            this.grdNumeroUltNF.Size = new System.Drawing.Size(183, 59);
            this.grdNumeroUltNF.TabIndex = 3;
            this.grdNumeroUltNF.Text = "Número da última NF";
            this.grdNumeroUltNF.Values.Heading = "Número da última NF";
            // 
            // txtNumeroUltNF
            // 
            this.txtNumeroUltNF.AlwaysActive = false;
            this.txtNumeroUltNF.Location = new System.Drawing.Point(4, 9);
            this.txtNumeroUltNF.MaxLength = 6;
            this.txtNumeroUltNF.Name = "txtNumeroUltNF";
            this.txtNumeroUltNF.Size = new System.Drawing.Size(163, 20);
            this.txtNumeroUltNF.StateActive.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtNumeroUltNF.StateActive.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.txtNumeroUltNF.StateActive.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtNumeroUltNF.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtNumeroUltNF.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.txtNumeroUltNF.TabIndex = 1;
            // 
            // btnGerar
            // 
            this.btnGerar.Location = new System.Drawing.Point(10, 71);
            this.btnGerar.Name = "btnGerar";
            this.btnGerar.Size = new System.Drawing.Size(386, 51);
            this.btnGerar.TabIndex = 2;
            this.btnGerar.Values.Text = "Gerar Número NF";
            this.btnGerar.Click += new System.EventHandler(this.btnGerar_Click);
            // 
            // ttpGF
            // 
            this.ttpGF.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // frmGerarNumeroNfe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(403, 202);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlDatas);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmGerarNumeroNfe";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Geração do Número da NF";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmGerarNumeroNF_FormClosing);
            this.Load += new System.EventHandler(this.frmGerarNumeroNF_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlDatas)).EndInit();
            this.pnlDatas.ResumeLayout(false);
            this.kryptonGroupBox6.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox6)).EndInit();
            this.kryptonGroupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbGF)).EndInit();
            this.kryptonGroupBox3.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox3)).EndInit();
            this.kryptonGroupBox3.ResumeLayout(false);
            this.kryptonGroupBox5.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox5)).EndInit();
            this.kryptonGroupBox5.ResumeLayout(false);
            this.kryptonGroupBox4.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox4)).EndInit();
            this.kryptonGroupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.grdNumeroASerEmi.Panel.ResumeLayout(false);
            this.grdNumeroASerEmi.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdNumeroASerEmi)).EndInit();
            this.grdNumeroASerEmi.ResumeLayout(false);
            this.grdNumeroUltNF.Panel.ResumeLayout(false);
            this.grdNumeroUltNF.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdNumeroUltNF)).EndInit();
            this.grdNumeroUltNF.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonPanel pnlDatas;
        private KryptonDateTimePicker dtpSaida;
        private KryptonDateTimePicker dtpEmissao;
        private KryptonDateTimePicker dtpHoraSaida;
        private KryptonPanel panel1;
        private KryptonTextBox txtNumeroASerEmi;
        private KryptonTextBox txtNumeroUltNF;
        private System.Windows.Forms.ToolTip ttpGF;
        private KryptonComboBox cmbGF;
        private KryptonGroupBox grdNumeroUltNF;
        private KryptonButton btnGerar;
        private KryptonGroupBox grdNumeroASerEmi;
        private KryptonGroupBox kryptonGroupBox6;
        private KryptonGroupBox kryptonGroupBox5;
        private KryptonGroupBox kryptonGroupBox4;
        private KryptonGroupBox kryptonGroupBox3;
    }
}