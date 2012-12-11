using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Deployment.Application;
using HLP.bel;
using HLP.bel.Static;

namespace NfeGerarXml.Config
{
    public partial class frmAviso : KryptonForm
    {
        belVersionamento objBelVersionamento = new belVersionamento();
        UpdateCheckInfo info = null;
        public frmAviso()
        {
            InitializeComponent();
            this.Location = new Point(Control.MousePosition.X - 150, Control.MousePosition.Y - this.Size.Height - 20);
        }
        public frmAviso(int x, int y)
        {
            InitializeComponent();
            this.Location = new Point(x, y - this.Size.Height);
        }
        private void frmAviso_Load(object sender, EventArgs e)
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;
                #region Check se tem update
                try
                {
                    info = ad.CheckForDetailedUpdate();
                }
                catch (DeploymentDownloadException dde)
                {
                    throw new Exception("A nova versão não pode ser baixada agora. \n\nVerifique sua conexão com a Internet ou tente novamente mais tarde. Erro: " + dde.Message);

                }
                catch (InvalidDeploymentException ide)
                {
                    throw new Exception("O arquivo está indisponível ou corrompido. Erro: " + ide.Message);

                }
                catch (InvalidOperationException ioe)
                {
                    throw new Exception("This application cannot be updated. It is likely not a ClickOnce application. Error: " + ioe.Message);
                }
                #endregion
                if (info.UpdateAvailable)
                {
                    string sVersaoAtual = ad.CurrentVersion.ToString();
                    lblDisponivel.Text = string.Format(lblDisponivel.Text, sVersaoAtual);

                    if (!objBelVersionamento.VersaoDisponivelIgualLiberada(sVersaoAtual))
                    {
                        if (objBelVersionamento.VerificaAtualizacaoDisponivel())
                        {
                            this.Size = new Size(250, 115);
                            lblAviso.Text = Environment.NewLine + "Mantenha seu sistema atualizado!";
                        }
                        else
                        {
                            this.Size = new Size(250, 144);
                            btnAtualizar.Visible = false;
                            lblAviso.Text = Environment.NewLine
                                            + "A versão acima já foi publicada, "
                                            + "mas é necessário algumas atualizações "
                                            + "na estrutura do sistema.!"
                                            + Environment.NewLine
                                            + "Favor entrar em contato com o suporte para liberação da versão!";
                        }
                    }
                }
                else
                {
                    lblDisponivel.Text = string.Format(lblDisponivel.Text, belStatic.sVersaoAtual.ToString());
                    this.Size = new Size(250, 100);
                    btnAtualizar.Visible = false;
                    lblAviso.Text = Environment.NewLine + "Sistema Atualizado !!";
                }
            }
            btnAtualizar.Focus();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (info != null)
                {
                    if (info.UpdateAvailable)
                    {
                        ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;
                        frmAtualizacao objfrmAtualizacao = new frmAtualizacao(ad);
                        objfrmAtualizacao.lblCabecalho.Text = "A versão " + info.AvailableVersion.ToString() + " já está disponível.";
                        MessageBox.Show(ad.CurrentVersion.ToString());
                        objfrmAtualizacao.txtCorrecoes.Text = objBelVersionamento.BuscaInformacaoAtualizacao(info.AvailableVersion.ToString());
                        objfrmAtualizacao.ShowDialog();
                        if (objfrmAtualizacao.bcancela == true)
                        {
                            this.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("info null");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void frmAviso_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
