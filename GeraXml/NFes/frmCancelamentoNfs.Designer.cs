using AC.ExtendedRenderer.Toolkit;
using ComponentFactory.Krypton.Toolkit;
namespace NfeGerarXml.NFes
{
    partial class frmCancelamentoNfs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCancelamentoNfs));
            this.dgvTabErros = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.codDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.msgDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.solucaoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsCancelamento = new System.Windows.Forms.BindingSource(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblUF = new AC.ExtendedRenderer.Toolkit.KryptonLabel();
            this.txtFiltro = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtSolucao = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.label1 = new AC.ExtendedRenderer.Toolkit.KryptonLabel();
            this.label2 = new AC.ExtendedRenderer.Toolkit.KryptonLabel();
            this.btnCancelar = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.lblCoderro = new AC.ExtendedRenderer.Toolkit.KryptonLabel();
            this.label3 = new AC.ExtendedRenderer.Toolkit.KryptonLabel();
            this.lblErro = new AC.ExtendedRenderer.Toolkit.KryptonLabel();
            this.label4 = new AC.ExtendedRenderer.Toolkit.KryptonLabel();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.cbxFiltro = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTabErros)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCancelamento)).BeginInit();
            this.panel1.SuspendLayout();
            this.btnCancelar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxFiltro)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTabErros
            // 
            this.dgvTabErros.AllowUserToAddRows = false;
            this.dgvTabErros.AllowUserToDeleteRows = false;
            this.dgvTabErros.AutoGenerateColumns = false;
            this.dgvTabErros.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codDataGridViewTextBoxColumn,
            this.msgDataGridViewTextBoxColumn,
            this.solucaoDataGridViewTextBoxColumn});
            this.dgvTabErros.DataSource = this.bsCancelamento;
            this.dgvTabErros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTabErros.Location = new System.Drawing.Point(0, 0);
            this.dgvTabErros.Name = "dgvTabErros";
            this.dgvTabErros.ReadOnly = true;
            this.dgvTabErros.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTabErros.Size = new System.Drawing.Size(659, 337);
            this.dgvTabErros.TabIndex = 0;
            this.dgvTabErros.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTabErros_RowEnter);
            // 
            // codDataGridViewTextBoxColumn
            // 
            this.codDataGridViewTextBoxColumn.DataPropertyName = "cod";
            this.codDataGridViewTextBoxColumn.HeaderText = "Código";
            this.codDataGridViewTextBoxColumn.Name = "codDataGridViewTextBoxColumn";
            this.codDataGridViewTextBoxColumn.ReadOnly = true;
            this.codDataGridViewTextBoxColumn.Width = 80;
            // 
            // msgDataGridViewTextBoxColumn
            // 
            this.msgDataGridViewTextBoxColumn.DataPropertyName = "msg";
            this.msgDataGridViewTextBoxColumn.HeaderText = "Mensagem";
            this.msgDataGridViewTextBoxColumn.Name = "msgDataGridViewTextBoxColumn";
            this.msgDataGridViewTextBoxColumn.ReadOnly = true;
            this.msgDataGridViewTextBoxColumn.Width = 500;
            // 
            // solucaoDataGridViewTextBoxColumn
            // 
            this.solucaoDataGridViewTextBoxColumn.DataPropertyName = "solucao";
            this.solucaoDataGridViewTextBoxColumn.HeaderText = "solucao";
            this.solucaoDataGridViewTextBoxColumn.Name = "solucaoDataGridViewTextBoxColumn";
            this.solucaoDataGridViewTextBoxColumn.ReadOnly = true;
            this.solucaoDataGridViewTextBoxColumn.Visible = false;
            // 
            // bsCancelamento
            // 
            this.bsCancelamento.DataSource = typeof(HLP.bel.NFes.belCancelamentoNFse);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvTabErros);
            this.panel1.Location = new System.Drawing.Point(4, 118);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(659, 337);
            this.panel1.TabIndex = 1;
            // 
            // lblUF
            // 
            this.lblUF.BackColor = System.Drawing.Color.Transparent;
            this.lblUF.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUF.Location = new System.Drawing.Point(5, 39);
            this.lblUF.Name = "lblUF";
            this.lblUF.Size = new System.Drawing.Size(43, 21);
            this.lblUF.TabIndex = 169;
            this.lblUF.Text = "Filtro";
            this.lblUF.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblUF.UseAlternateForeColor = false;
            this.lblUF.UseKryptonFont = false;
            // 
            // txtFiltro
            // 
            this.txtFiltro.AlwaysActive = false;
            this.txtFiltro.Location = new System.Drawing.Point(171, 40);
            this.txtFiltro.Name = "txtFiltro";
            this.txtFiltro.Size = new System.Drawing.Size(490, 20);
            this.txtFiltro.StateActive.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.txtFiltro.StateActive.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtFiltro.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtFiltro.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.txtFiltro.TabIndex = 171;
            this.txtFiltro.TextChanged += new System.EventHandler(this.txtFiltro_TextChanged);
            // 
            // txtSolucao
            // 
            this.txtSolucao.AlwaysActive = false;
            this.txtSolucao.ForeColor = System.Drawing.Color.Blue;
            this.txtSolucao.Location = new System.Drawing.Point(6, 486);
            this.txtSolucao.Multiline = true;
            this.txtSolucao.Name = "txtSolucao";
            this.txtSolucao.ReadOnly = true;
            this.txtSolucao.Size = new System.Drawing.Size(655, 55);
            this.txtSolucao.StateActive.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.txtSolucao.StateActive.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtSolucao.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtSolucao.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.txtSolucao.TabIndex = 172;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 458);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 201;
            this.label1.Text = "Solução";
            this.label1.UseAlternateForeColor = false;
            this.label1.UseKryptonFont = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 202;
            this.label2.Text = "Tabela de Erros";
            this.label2.UseAlternateForeColor = false;
            this.label2.UseKryptonFont = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Controls.Add(this.lblCoderro);
            this.btnCancelar.Location = new System.Drawing.Point(496, 559);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(167, 30);
            this.btnCancelar.TabIndex = 203;
            this.btnCancelar.Values.Text = "Continuar o Cancelamento";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblCoderro
            // 
            this.lblCoderro.AutoSize = true;
            this.lblCoderro.BackColor = System.Drawing.Color.Transparent;
            this.lblCoderro.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCoderro.Location = new System.Drawing.Point(-65, 7);
            this.lblCoderro.Name = "lblCoderro";
            this.lblCoderro.Size = new System.Drawing.Size(0, 13);
            this.lblCoderro.TabIndex = 205;
            this.lblCoderro.UseAlternateForeColor = false;
            this.lblCoderro.UseKryptonFont = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(266, 566);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 204;
            this.label3.Text = "Erro Selecionado:";
            this.label3.UseAlternateForeColor = false;
            this.label3.UseKryptonFont = false;
            // 
            // lblErro
            // 
            this.lblErro.AutoSize = true;
            this.lblErro.BackColor = System.Drawing.Color.Transparent;
            this.lblErro.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErro.Location = new System.Drawing.Point(420, 566);
            this.lblErro.Name = "lblErro";
            this.lblErro.Size = new System.Drawing.Size(14, 13);
            this.lblErro.TabIndex = 205;
            this.lblErro.Text = "\' \'";
            this.lblErro.UseAlternateForeColor = false;
            this.lblErro.UseKryptonFont = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(228, 13);
            this.label4.TabIndex = 206;
            this.label4.Text = "Selecione um erro e continue o Cancelamento!";
            this.label4.UseAlternateForeColor = false;
            this.label4.UseKryptonFont = false;
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.cbxFiltro);
            this.kryptonPanel1.Controls.Add(this.label4);
            this.kryptonPanel1.Controls.Add(this.lblErro);
            this.kryptonPanel1.Controls.Add(this.lblUF);
            this.kryptonPanel1.Controls.Add(this.label3);
            this.kryptonPanel1.Controls.Add(this.btnCancelar);
            this.kryptonPanel1.Controls.Add(this.txtFiltro);
            this.kryptonPanel1.Controls.Add(this.label1);
            this.kryptonPanel1.Controls.Add(this.txtSolucao);
            this.kryptonPanel1.Controls.Add(this.label2);
            this.kryptonPanel1.Controls.Add(this.panel1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(679, 601);
            this.kryptonPanel1.TabIndex = 207;
            // 
            // cbxFiltro
            // 
            this.cbxFiltro.AlwaysActive = false;
            this.cbxFiltro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFiltro.DropDownWidth = 137;
            this.cbxFiltro.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxFiltro.FormattingEnabled = true;
            this.cbxFiltro.Items.AddRange(new object[] {
            "CODIGO",
            "MENSAGEM"});
            this.cbxFiltro.Location = new System.Drawing.Point(54, 39);
            this.cbxFiltro.Name = "cbxFiltro";
            this.cbxFiltro.Size = new System.Drawing.Size(108, 21);
            this.cbxFiltro.StateActive.ComboBox.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.cbxFiltro.StateActive.ComboBox.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.cbxFiltro.StateCommon.ComboBox.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.cbxFiltro.StateCommon.ComboBox.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.cbxFiltro.TabIndex = 207;
            this.cbxFiltro.Tag = "True";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "cod";
            this.dataGridViewTextBoxColumn1.HeaderText = "Codigo";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 80;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "msg";
            this.dataGridViewTextBoxColumn2.HeaderText = "Mensagem";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 450;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "solucao";
            this.dataGridViewTextBoxColumn3.HeaderText = "Solucao";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Visible = false;
            // 
            // frmCancelamentoNfs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(679, 601);
            this.Controls.Add(this.kryptonPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCancelamentoNfs";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tabela de Erros";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTabErros)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCancelamento)).EndInit();
            this.panel1.ResumeLayout(false);
            this.btnCancelar.ResumeLayout(false);
            this.btnCancelar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxFiltro)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonDataGridView dgvTabErros;
        private System.Windows.Forms.Panel panel1;
        private KryptonTextBox txtFiltro;
        private KryptonTextBox txtSolucao;
        private KryptonButton btnCancelar;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.BindingSource bsCancelamento;
        private System.Windows.Forms.DataGridViewTextBoxColumn codDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn msgDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn solucaoDataGridViewTextBoxColumn;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private AC.ExtendedRenderer.Toolkit.KryptonLabel lblUF;
        private AC.ExtendedRenderer.Toolkit.KryptonLabel label1;
        private AC.ExtendedRenderer.Toolkit.KryptonLabel label2;
        private AC.ExtendedRenderer.Toolkit.KryptonLabel lblCoderro;
        private AC.ExtendedRenderer.Toolkit.KryptonLabel label3;
        private AC.ExtendedRenderer.Toolkit.KryptonLabel lblErro;
        private AC.ExtendedRenderer.Toolkit.KryptonLabel label4;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbxFiltro;
    }
}