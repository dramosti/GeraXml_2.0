using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Deployment.Application;
using ComponentFactory.Krypton.Toolkit;

namespace NfeGerarXml.Config
{
    public partial class frmAtualizacao : KryptonForm
    {
        ApplicationDeployment ad;
        public bool bcancela = false;
        public frmAtualizacao(ApplicationDeployment ad)
        {
            InitializeComponent();
            this.ad = ad;
        }

        private void frmAtualizacao_Load(object sender, EventArgs e)
        {

        }


        private void btnSim_Click(object sender, EventArgs e)
        {
            try
            {
                this.Controls.Remove(panelConfirmacao);
                lblStatus.Text = "Aguarde enquanto o Sistema está sendo atualizado...";
                lblStatus.Refresh();
                ad.Update() ;
                this.Hide();
                KryptonMessageBox.Show("Atualização realizada com sucesso. O sistema será reiniciado.", "Atualização", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Restart();
            }
            catch (DeploymentDownloadException dde)
            {
                KryptonMessageBox.Show("Atualização não foi instalada. \n\nVerifique sua conexão com a Internet ou tente novamente mais tarde. Erro: " + dde);
                return;
            }
        }

        private void btnNao_Click(object sender, EventArgs e)
        {
            bcancela = true;
            this.Close();
        }
    }
}
