namespace NfeGerarXml.Config
{
    partial class frmAviso
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
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.lblAviso = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.lblDisponivel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnAtualizar = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.lblAviso);
            this.kryptonPanel1.Controls.Add(this.lblDisponivel);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(291, 89);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // lblAviso
            // 
            this.lblAviso.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAviso.Location = new System.Drawing.Point(0, 20);
            this.lblAviso.Multiline = true;
            this.lblAviso.Name = "lblAviso";
            this.lblAviso.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.lblAviso.ReadOnly = true;
            this.lblAviso.Size = new System.Drawing.Size(291, 69);
            this.lblAviso.StateNormal.Back.Color1 = System.Drawing.Color.Transparent;
            this.lblAviso.TabIndex = 1;
            this.lblAviso.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblDisponivel
            // 
            this.lblDisponivel.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDisponivel.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
            this.lblDisponivel.Location = new System.Drawing.Point(0, 0);
            this.lblDisponivel.Name = "lblDisponivel";
            this.lblDisponivel.Size = new System.Drawing.Size(291, 20);
            this.lblDisponivel.TabIndex = 0;
            this.lblDisponivel.Values.Text = "Versão disponível: {0}";
            // 
            // btnAtualizar
            // 
            this.btnAtualizar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnAtualizar.Location = new System.Drawing.Point(0, 89);
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.Size = new System.Drawing.Size(291, 24);
            this.btnAtualizar.TabIndex = 1;
            this.btnAtualizar.Values.Text = "Visualizar alterações";
            this.btnAtualizar.Click += new System.EventHandler(this.btnAtualizar_Click);
            // 
            // frmAviso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 113);
            this.ControlBox = false;
            this.Controls.Add(this.kryptonPanel1);
            this.Controls.Add(this.btnAtualizar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAviso";
            this.Opacity = 0.6D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Deactivate += new System.EventHandler(this.frmAviso_Deactivate);
            this.Load += new System.EventHandler(this.frmAviso_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnAtualizar;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblDisponivel;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox lblAviso;
    }
}