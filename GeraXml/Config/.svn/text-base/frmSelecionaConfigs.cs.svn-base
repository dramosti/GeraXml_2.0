using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using NfeGerarXml.NFes;
using HLP.bel;
using System.Xml;
using Microsoft.Win32;
using NfeGerarXml.Config;
using System.Deployment.Application;
using ComponentFactory.Krypton.Toolkit;
using HLP.bel.Static;
using HLP.bel.NFe.GeraXml;

namespace NfeGerarXml
{
    public partial class frmSelecionaConfigs : KryptonForm
    {
        public bool bESCRITA = false;
        string sTipoNFe = "G";
        frmGerarXml objfrmPrincipal;
        belVersionamento objBelVersionamento = new belVersionamento();

        public bool bFecharApp = false;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objfrmPrincipal">Form Principal</param>
        /// <param name="sTipoNFe">N-> NORMAL / S-> SERVIÇO / I-> IMPORTAÇÃO</param>
        public frmSelecionaConfigs(frmGerarXml objfrmPrincipal, string sTipoNFe)
        {
            this.objfrmPrincipal = objfrmPrincipal;
            this.sTipoNFe = sTipoNFe;
            InitializeComponent();

        }
        public frmSelecionaConfigs()
        {
            InitializeComponent();

        }

        private void frmSelecionaConfigs_Load(object sender, EventArgs e)
        {
            try
            {
                DirectoryInfo dinfo = new DirectoryInfo(belStatic.Pasta_xmls_Configs);
                FileInfo[] finfo = dinfo.GetFiles();

                RegistryKey key = Registry.CurrentConfig.OpenSubKey("hlp\\nivel0006");
                string sCodEmpresaPadrao = (key != null ? key.GetValue("Código da firma digitado no início do Sistema", "").ToString() : "");
                string sAquivoPadrao = "";

                XmlDocument xml = new XmlDocument();

                foreach (FileInfo item in finfo)
                {
                    if (Path.GetExtension(item.FullName).ToUpper().Equals(".XML"))
                    {
                        cbxArquivos.Items.Add(item.Name);

                        xml.Load(item.FullName);
                        XmlNodeList Xnode = xml.GetElementsByTagName("nfe_configuracoes");

                        belStatic.sConfig = item.Name;

                        Globais objGlobais = new Globais();
                        if (sCodEmpresaPadrao == objGlobais.LeRegConfig("Empresa"))
                        {
                            sAquivoPadrao = item.Name;
                        }
                        belStatic.sConfig = "";
                    }
                }


                if ((belStatic.sConfig != "") && (belStatic.sConfig != null))
                {
                    cbxArquivos.Text = belStatic.sConfig;
                }
                else if ((cbxArquivos.Items.Count > 0) && (sAquivoPadrao == ""))
                {
                    cbxArquivos.SelectedIndex = 0;
                }
                else if (sAquivoPadrao != "")
                {
                    cbxArquivos.Text = sAquivoPadrao;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                lblVerificacao.Text = "Verificando atualizações. Por favor, aguarde..";
                statusStrip.Refresh();
                if (cbxArquivos.Items.Count > 0)
                {
                    belStatic.sConfig = cbxArquivos.SelectedItem.ToString();
                    Globais LeRegWin = new Globais();                    
                    belStatic.codEmpresaNFe = LeRegWin.LeRegConfig("Empresa");                   

                    if (sTipoNFe.Equals("G"))
                    {
                        belStatic.IPrimeiroLoad = 0;
                        if (cbxArquivos.SelectedItem.ToString().Replace(".xml", "").ToUpper().Equals("ESCRITA"))
                        {
                            bESCRITA = true;
                        }
                        this.Close();
                    }
                    else
                    {
                        this.Hide();
                        if (sTipoNFe.Equals("N"))
                        {
                            frmArquivosXmlNfe objfrm = new frmArquivosXmlNfe(objfrmPrincipal);
                            objfrm.MdiParent = objfrmPrincipal;
                            objfrm.Show();
                        }
                        else if (sTipoNFe.Equals("S"))
                        {
                            frmEnviaNfs objfrm = new frmEnviaNfs(objfrmPrincipal);
                            objfrm.MdiParent = objfrmPrincipal;
                            objfrm.Show();

                        }
                        else if (sTipoNFe.Equals("I"))
                        {
                            frmImportaEscritorNfe objfrm = new frmImportaEscritorNfe();
                            objfrm.MdiParent = objfrmPrincipal;
                            objfrm.Show();
                        }
                        this.Close();
                    }                    
                }
                else
                {
                    KryptonMessageBox.Show(null, "Não existem arquivos na pasta de Config.", "A V I S O", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, "Ocorreu uma Exceção não tratada, Informe a Mensagem abaixo ao suporte HLP."
                    + Environment.NewLine
                    + ex.Message, "E R R O", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }


        //private void Atualizacao()
        //{
        //    UpdateCheckInfo info = null;
        //    if (ApplicationDeployment.IsNetworkDeployed)
        //    {
        //        ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;
        //        try
        //        {
        //            info = ad.CheckForDetailedUpdate();
        //        }
        //        catch (DeploymentDownloadException dde)
        //        {
        //            KryptonMessageBox.Show("A nova versão não pode ser baixada agora. \n\nVerifique sua conexão com a Internet ou tente novamente mais tarde. Erro: " + dde.Message);
        //            return;
        //        }
        //        catch (InvalidDeploymentException ide)
        //        {
        //            KryptonMessageBox.Show("O arquivo está indisponível ou corrompido. Erro: " + ide.Message);
        //            return;
        //        }
        //        catch (InvalidOperationException ioe)
        //        {
        //            KryptonMessageBox.Show("This application cannot be updated. It is likely not a ClickOnce application. Error: " + ioe.Message);
        //            return;
        //        }

        //        if (info.UpdateAvailable)
        //        {
        //            if (!info.IsUpdateRequired)
        //            {
        //                this.Hide();
        //                frmAtualizacao objfrmAtualizacao = new frmAtualizacao(ad);
        //                objfrmAtualizacao.lblCabecalho.Text = "A versão " + info.AvailableVersion.ToString() + " já está disponível.";
        //                string sVersaoAtual = ad.CurrentVersion.ToString();
        //                objfrmAtualizacao.txtCorrecoes.Text = objBelVersionamento.BuscaInformacaoAtualizacao(sVersaoAtual);
        //                objfrmAtualizacao.ShowDialog();
        //                if (objfrmAtualizacao.bcancela == true)
        //                {
        //                    this.Show();
        //                }
        //            }
        //            else
        //            {
        //                // Display a message that the app MUST reboot. Display the minimum required version.
        //                KryptonMessageBox.Show("This application has detected a mandatory update from your current " +
        //                    "version to version " + info.MinimumRequiredVersion.ToString() +
        //                    ". The application will now install the update and restart.",
        //                    "Update Available", MessageBoxButtons.OK,
        //                    MessageBoxIcon.Information);
        //            }
        //        }
        //    }
        //}

        private void cbxArquivos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                SendKeys.Send("{tab}");
            }
        }

        private void frmSelecionaConfigs_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (belStatic.IPrimeiroLoad == 1)
            {
                bFecharApp = true;
            }
        }

        private void btnCaminhoXml_Click(object sender, EventArgs e)
        {
            string scaminho = belStatic.Pasta_xmls_Configs;
            frmLocalXml objfrm = new frmLocalXml(belStatic.Pasta_xmls_Configs);
            objfrm.ShowDialog();
            if (scaminho != belStatic.Pasta_xmls_Configs)
            {
                Application.Restart();
                Application.Exit();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


    }
}
