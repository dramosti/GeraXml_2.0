using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using NfeGerarXml.NFes;
using HLP.bel;
using FirebirdSql.Data.FirebirdClient;
using System.Deployment.Application;
using HLP.Util;
using NfeGerarXml.Config;
using System.Xml.Linq;
using NfeGerarXml.CCe;
using ComponentFactory.Krypton.Toolkit;
using HLP.bel.Static;
using Proshot.UtilityLib.CommonDialogs;
using HLP.bel.NFe.GeraXml;
using HLP.bel.CTe;

namespace NfeGerarXml
{
    public partial class frmGerarXml : KryptonForm
    {
        public string sStatusSefaz
        {
            get { return lblStatusSefaz.Text; }
            set { lblStatusSefaz.Text = value; }
        }
        frmGerarXml objfrmPrincipal;
        private static string _VISUAL = "VisualGeraXml";
        belConnection cx;

        public frmGerarXml()
        {
            InitializeComponent();
            try
            {
                belStaticPastas.Pasta_StartupPath = Application.StartupPath;
                belStatic.bClickOnce = false;
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    belStatic.bClickOnce = true;
                }
                string sVisualGera = "";
                if (belStatic.bClickOnce)
                {
                    belIsolated objIsolated = new belIsolated();
                    sVisualGera = objIsolated.BuscarArquivo(_VISUAL, belIsolated.Lugar.Local);
                }
                else
                {
                    sVisualGera = belRegedit.BuscaNomeSkin();
                }
                SetVisualandBackColor(sVisualGera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void frmGerarXmlNfe_Load(object sender, EventArgs e)
        {
            try
            {
                foreach (Control ctl in this.Controls)
                {
                    if ((ctl) is MdiClient)
                    {
                        ctl.BackColor = Color.White;
                        break;
                    }
                }
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;
                    lblVersao.Text = "Versão Atual: " + ad.CurrentVersion.ToString();
                    belStatic.sVersaoAtual = ad.CurrentVersion.ToString();
                    belVersionamento objbelVersion = new belVersionamento();
                    if (objbelVersion.VerificaPublicacaoDisponivel())
                    {
                        frmPopup popup = new frmPopup(PopupSkins.InfoSkin);
                        popup.ShowPopup("Atualização", "Uma nova versão do Sistema já está Disponível!", 200, 4000, 2000);
                        tsAtualizacao.Visible = true;
                    }
                    else
                    {
                        tsAtualizacao.Visible = false;
                    }
                    versãoHlpToolStripMenuItem.Visible = false;
                }

                //Carrega os arquivos de configuração
                if (!Util.VerificaConfiguracaoPastasXml())
                {
                    frmLocalXml objfrm = new frmLocalXml("");
                    objfrm.ShowDialog();
                }
                else
                {
                    DirectoryInfo dinfo = new DirectoryInfo(belStatic.Pasta_xmls_Configs);
                    if (!dinfo.Exists)
                    {
                        KryptonMessageBox.Show(null, "O caminho configurado abaixo não foi encontrado!! "
                            + Environment.NewLine
                            + Environment.NewLine
                            + belStatic.Pasta_xmls_Configs, "A V I S O", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmLocalXml objfrm = new frmLocalXml(belStatic.Pasta_xmls_Configs);
                        objfrm.ShowDialog();
                    }
                }

                int iCountFiles = 0;
                DirectoryInfo dPastaData = new DirectoryInfo(belStatic.Pasta_xmls_Configs);
                if (!dPastaData.Exists)
                {
                    dPastaData.Create();
                }
                else
                {
                    FileInfo[] finfo = dPastaData.GetFiles("*.xml");
                    foreach (FileInfo item in finfo)
                    {
                        iCountFiles++;
                    }
                }


                belStatic.IPrimeiroLoad = 1;
                if (iCountFiles != 0)
                {
                    frmSelecionaConfigs objFrmSeleciona = new frmSelecionaConfigs();
                    objFrmSeleciona.ShowDialog();
                    if (objFrmSeleciona.bFecharApp)
                    {
                        throw new Exception("Fechar");
                    }
                    if (!objFrmSeleciona.bESCRITA)
                    {
                        objFrmSeleciona.Hide();
                        belStatic.IPrimeiroLoad = 1;
                        frmLogin objfrm = new frmLogin();
                        objfrm.ShowDialog();
                        CarregaDadosEmpresa();
                        VerificaAcessoUserEmprersa(sender, e);
                        belStatic.IPrimeiroLoad = 0;
                        lblUsuario.Text = "Usuário: " + belStatic.SUsuario;
                        lblEmpresa.Text = belStatic.sNomeEmpresa;
                        gerarAquivosXmlsToolStripMenuItem.Visible = true;
                        tsNfe.Enabled = true;
                        tsNfes.Enabled = true;
                        headerMenuLateral.Visible = true;
                        cx = new belConnection();
                    }
                    else
                    {
                        gerarAquivosXmlsToolStripMenuItem.Visible = false;
                        tsNfe.Enabled = false;
                        tsNfes.Enabled = false;
                        headerMenuLateral.Visible = false;
                    }

                }
                else
                {
                    if (KryptonMessageBox.Show(null, "Não existe nenhum arquivo de configuração na pasta Selecionada."
                         + Environment.NewLine
                         + Environment.NewLine
                         + "Deseja selecionar uma outra Pasta ?",
                         "A V I S O",
                         MessageBoxButtons.YesNo,
                         MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        frmLocalXml objfrm = new frmLocalXml(belStatic.Pasta_xmls_Configs);
                        objfrm.ShowDialog();
                        Application.Restart();
                        this.Close();
                    }


                    belStatic.BSemArquivo = true;
                    frmLoginConfig objFrm = new frmLoginConfig();
                    objFrm.ShowDialog();

                    lblUsuario.Text = "   Usuário : " + belStatic.SUsuario;
                }
                CarregaStatuModoSistema();

                //carrega Logotipo
                Globais LeRegWin = new Globais();
                LeRegWin.CarregaInfStaticas(); // INICIALIZA AS CONFIGURAÇÕES PADRÕES
                Byte[] bimagem = belUtil.carregaImagem(LeRegWin.LeRegConfig("Logotipo"));

                if (bimagem != null)
                {
                    pictureBox1.BackgroundImage = belUtil.byteArrayToImage(bimagem);
                }

                HLP.Dao.daoEmailContador objdaoemailCont = new HLP.Dao.daoEmailContador();
                if (objdaoemailCont.VerificaDiaParaEnviarEmail())
                {
                    try
                    {
                        KryptonMessageBox.Show("Hoje é dia de enviar Email para o Contador, Verifique suas Pendências!!", "A V I S O", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        NfeGerarXml.NFe.frmEmailContadorNfe objfrm = new NfeGerarXml.NFe.frmEmailContadorNfe();
                        objfrm.MdiParent = this;
                        objfrm.Show();
                    }
                    catch (Exception ex)
                    {
                        KryptonMessageBox.Show(null, ex.Message, "A V I S O", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

                if (belStatic.RAMO == "TRANSPORTE")
                {
                    btnNfe.Enabled = false;
                    tsNfe.Enabled = false;

                    btnNfes.Enabled = false;
                    tsNfes.Enabled = false;

                    btnCte.Enabled = true;
                    tsCte.Enabled = true;

                    btnCce.Enabled = false;
                    tsCce.Enabled = false;

                    //btnEmail.Enabled = false;
                    //tsEmail.Enabled = false;

                    tsOrganizarPasta.Enabled = false;
                    tsProtocolos.Enabled = false;
                    tsImportarXmlEscritor.Enabled = false;
                }
                else
                {
                    btnNfe.Enabled = true;
                    tsNfe.Enabled = true;

                    btnNfes.Enabled = true;
                    tsNfes.Enabled = true;

                    btnCte.Enabled = false;
                    tsCte.Enabled = false;

                    btnCce.Enabled = true;
                    tsCce.Enabled = true;

                    btnEmail.Enabled = true;
                    tsEmail.Enabled = true;

                    tsOrganizarPasta.Enabled = true;
                    tsProtocolos.Enabled = true;
                    tsImportarXmlEscritor.Enabled = true;
                }




            }
            catch (FbException fbx)
            {
                KryptonMessageBox.Show(null, "Ocorreu uma falha ao montar a string de Conexão!"
                    + Environment.NewLine
                    + "Verifique se o arquivo está configurado corretamente!"
                    + Environment.NewLine,
                    "A V I S O", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmLocalXml objfrm = new frmLocalXml(belStatic.Pasta_xmls_Configs);

                frmLoginConfig objFrm = new frmLoginConfig();
                objFrm.ShowDialog();
                lblUsuario.Text = "   Usuário : " + belStatic.SUsuario;
                Application.Restart();

            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("Fechar"))
                {
                    this.Close();
                }
                else
                {
                    KryptonMessageBox.Show(ex.Message);
                }
            }
        }

        private void VerificaAcessoUserEmprersa(object sender, EventArgs e)
        {
            try
            {
                StringBuilder sQuery = new StringBuilder();

                sQuery.Append("SELECT count( acesso.cd_operado)contador FROM acesso");

                FbCommand command = new FbCommand(sQuery.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                command.ExecuteNonQuery();
                FbDataReader dr = command.ExecuteReader();
                dr.Read();

                if (Convert.ToInt32(dr["contador"].ToString()) > 0)
                {
                    sQuery = new StringBuilder();
                    sQuery.Append("SELECT count( acessoem.cd_operado)contador FROM acessoem ");
                    sQuery.Append("where acessoem.cd_operado = '" + belStatic.SUsuario + "' and ");
                    sQuery.Append("acessoem.cd_empresa = '" + belStatic.codEmpresaNFe + "'");

                    command = new FbCommand(sQuery.ToString(), cx.get_Conexao());
                    command.ExecuteNonQuery();
                    dr = command.ExecuteReader();
                    dr.Read();
                    int icount = Convert.ToInt32(dr["contador"].ToString());

                    if (icount == 0)
                    {
                        KryptonMessageBox.Show(null, "Usuário " + belStatic.SUsuario
                            + " não tem acesso permitido a Empresa "
                            + belStatic.codEmpresaNFe + "!!"
                            + Environment.NewLine
                            + Environment.NewLine, "A L E R T A", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmGerarXmlNfe_Load(sender, e);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cx.Close_Conexao();
            }

        }

        public void CarregaDadosEmpresa()
        {
            try
            {
                cx = new belConnection();
                string sQuery = "select empresa.nm_cidnor, empresa.cd_ufnor from empresa where empresa.cd_empresa ='" + belStatic.codEmpresaNFe + "'";
                FbCommand command = new FbCommand(sQuery, cx.get_Conexao());
                cx.Open_Conexao();
                command.ExecuteNonQuery();
                FbDataReader dr = command.ExecuteReader();
                dr.Read();
                List<string> ObjListCidadesNFse = new List<string>();
                ObjListCidadesNFse.Add("itu");
                ObjListCidadesNFse.Add("jundiai");
                belStatic.sNmCidadeEmpresa = dr["nm_cidnor"].ToString().ToUpper().Trim().ToLower();
                belStatic.sUF = dr["cd_ufnor"].ToString().ToUpper().Trim().ToUpper();
                belStatic.Sigla_uf = belStatic.sUF;
                belStatic.CodEmpresaCte = belStatic.codEmpresaNFe;
            }
            catch (Exception)
            {
                throw;
            }
            finally { cx.Close_Conexao(); }

        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (KryptonMessageBox.Show("Gostaria de sair do sistema?", "S A I R", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Application.Exit();
        }

        private void importarXMLPEscritorToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Boolean ok = false;
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is frmImportaEscritorNfe)
                {
                    frm.BringToFront();
                    ok = true;
                }
            }
            if (ok == false)
            {
                frmImportaEscritorNfe objfrm = new frmImportaEscritorNfe();
                objfrm.MdiParent = this;
                objfrm.Show();
            }
            else
            {
                KryptonMessageBox.Show(null, "A Tela de Importação de Notas já se encontra aberta", "A V I S O", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void enviadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OrganizaPastas(2);
        }

        private void envioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OrganizaPastas(1);
        }

        /// <summary>
        /// 1 - Envio / 2 - Enviados
        /// </summary>
        /// <param name="sPasta"></param>
        private void OrganizaPastas(int iPasta)
        {
            try
            {
                HLP.bel.NFe.GeraXml.Globais objbelGlobais = new Globais();
                System.IO.DirectoryInfo diretorio = new System.IO.DirectoryInfo((iPasta == 1 ? belStaticPastas.ENVIO : belStaticPastas.ENVIADOS));
                System.IO.FileSystemInfo[] arquivos = diretorio.GetFileSystemInfos();
                int icount = 0;

                foreach (System.IO.FileSystemInfo item in arquivos)
                {
                    if ((item.Extension.Equals(".xml")) && (item.Name.ToString().Length == 52))
                    {
                        System.IO.DirectoryInfo pasta = new System.IO.DirectoryInfo(diretorio.FullName + "/" + item.Name.Substring(2, 4));
                        if (!pasta.Exists) { pasta.Create(); }

                        if (!File.Exists(@pasta.FullName + "/" + item))
                        {
                            File.Move(@item.FullName, @pasta.FullName + "/" + item);
                            icount++;
                        }
                    }
                }
                if (icount > 0)
                {
                    KryptonMessageBox.Show(null, icount.ToString() + " Arquivo(s) encontrado(s) e organizado(s) ", "ORGANIZAÇÃO DE ARQUIVOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    KryptonMessageBox.Show(null, "Nenhum arquivo .xml encontrado no caminho:  " + Environment.NewLine + Environment.NewLine + "-->" + diretorio.FullName, "ORGANIZAÇÃO DE ARQUIVOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void protocolosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Boolean ok = false;
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is frmProtocolosNfe)
                {
                    frm.BringToFront();
                    ok = true;
                }
            }
            if (!ok)
            {
                frmProtocolosNfe objfrm = new frmProtocolosNfe(this);
                objfrm.MdiParent = this;
                objfrm.Show();
            }
        }

        private void nFeServiçoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (belStatic.sNmCidadeEmpresa.ToUpper().Equals("ITU") || belStatic.sNmCidadeEmpresa.ToUpper().Equals("JUNDIAI"))
            {
                MinimizarTudo();
                try
                {
                    Boolean ok = false;
                    foreach (Form frm in this.MdiChildren)
                    {
                        if (frm is frmEnviaNfs)
                        {
                            frm.BringToFront();
                            ok = true;
                        }

                    }
                    if (ok == false)
                    {
                        frmEnviaNfs objfrm = new frmEnviaNfs();
                        objfrm.MdiParent = this;
                        objfrm.WindowState = FormWindowState.Minimized;
                        objfrm.Show();
                        objfrm.WindowState = FormWindowState.Maximized;

                    }
                    else
                    {
                        KryptonMessageBox.Show(null, "A Tela de Envio de Notas já se encontra aberta", "A V I S O", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                catch (Exception ex)
                {
                    if (ex.Message.ToString() != "m_safeCertContext é um identificador inválido.")
                    {
                        KryptonMessageBox.Show(null, "Erro na configurações das pastas - " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Boolean ok = false;
                        foreach (Form frm in this.MdiChildren)
                        {
                            if (frm is frmLoginConfig)
                            {
                                frm.BringToFront();
                                ok = true;
                            }
                        }
                        if (!ok)
                        {
                            frmLoginConfig objfrm = new frmLoginConfig(this);
                            objfrm.MdiParent = this;
                            objfrm.Show();
                        }
                    }
                }
            }
            else
            {
                hlpMessageBox.ShowAviso("Módulo de Nota fiscal de serviço não liberado para a Cidade de " + belStatic.sNmCidadeEmpresa);
            }

        }

        private void nFeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MinimizarTudo();
                Boolean ok = false;
                foreach (Form frm in this.MdiChildren)
                {
                    if (frm is frmArquivosXmlNfe)
                    {
                        frm.BringToFront();
                        ok = true;
                    }
                }
                if (ok == false)
                {
                    frmArquivosXmlNfe objfrm = new frmArquivosXmlNfe(this);
                    objfrm.MdiParent = this;
                    objfrm.WindowState = FormWindowState.Minimized;
                    belGerarXML objbelGeraXml = new belGerarXML();
                    objfrm.Show();
                    objfrm.WindowState = FormWindowState.Maximized;
                }
                else
                {
                    hlpMessageBox.ShowAviso("A Tela de Visualização de Notas já se encontra aberta");
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.ToString() != "m_safeCertContext é um identificador inválido.")
                {
                    hlpMessageBox.ShowErro("Erro na configurações das pastas - " + ex.Message);
                    Boolean ok = false;
                    foreach (Form frm in this.MdiChildren)
                    {
                        if (frm is frmLoginConfig)
                        {
                            frm.BringToFront();
                            ok = true;
                        }
                    }
                    if (!ok)
                    {
                        frmLoginConfig objfrm = new frmLoginConfig(this);
                        objfrm.MdiParent = this;
                        objfrm.Show();
                    }
                }
            }
        }

        private void btnCte_Click(object sender, EventArgs e)
        {
            try
            {
                MinimizarTudo();
                Boolean ok = false;
                foreach (Form frm in this.MdiChildren)
                {
                    if (frm is frmGerarArquivosCte)
                    {
                        frm.BringToFront();
                        ok = true;
                    }
                }
                if (!ok)
                {
                    frmGerarArquivosCte objfrm = new frmGerarArquivosCte();
                    objfrm.MdiParent = this;
                    objfrm.WindowState = FormWindowState.Minimized;
                    if (objfrm.VerificaStatusServico())
                    {
                        objfrm.Show();
                        objfrm.WindowState = FormWindowState.Maximized;
                    }

                }
                else
                {
                    hlpMessageBox.ShowAviso("A Tela de Conhecimento já se encontra aberta");
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void cTeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MinimizarTudo();
                Boolean ok = false;
                foreach (Form frm in this.MdiChildren)
                {
                    if (frm is frmGerarArquivosCte)
                    {
                        frm.BringToFront();
                        ok = true;
                    }
                }
                if (!ok)
                {
                    frmGerarArquivosCte objfrm = new frmGerarArquivosCte();
                    objfrm.MdiParent = this;
                    objfrm.Show();
                    ActivateMdiChild(null);
                    ActivateMdiChild(objfrm);
                }
                else
                {
                    hlpMessageBox.ShowAviso("A Tela de Conhecimento já se encontra aberta");
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void btnSelecionaConfig_Click(object sender, EventArgs e)
        {
            frmGerarXmlNfe_Load(sender, e);
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }

        }

        private void btnNfe_Click(object sender, EventArgs e)
        {
            try
            {
                objfrmPrincipal = new frmGerarXml();
                Boolean ok = false;
                foreach (Form frm in this.MdiChildren)
                {
                    if (frm is frmArquivosXmlNfe)
                    {
                        frm.BringToFront();
                        ok = true;
                    }
                }
                if (ok == false)
                {
                    frmArquivosXmlNfe objfrm = new frmArquivosXmlNfe(this);
                    objfrm.MdiParent = this;
                    belGerarXML objbelGeraXml = new belGerarXML();
                    objfrm.Show();
                }
                else
                {
                    KryptonMessageBox.Show(null, "A Tela de Visualização de Notas já se encontra aberta", "A V I S O", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.ToString() != "m_safeCertContext é um identificador inválido.")
                {
                    KryptonMessageBox.Show(null, "Erro na configurações das pastas - " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Boolean ok = false;
                    foreach (Form frm in this.MdiChildren)
                    {
                        if (frm is frmLoginConfig)
                        {
                            frm.BringToFront();
                            ok = true;
                        }
                    }
                    if (!ok)
                    {
                        frmLoginConfig objfrm = new frmLoginConfig(this);
                        objfrm.MdiParent = this;
                        objfrm.Show();
                    }
                }
            }
        }

        private void btnNfeServico_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean ok = false;
                foreach (Form frm in this.MdiChildren)
                {
                    if (frm is frmEnviaNfs)
                    {
                        frm.BringToFront();
                        ok = true;
                    }

                }
                if (ok == false)
                {
                    frmEnviaNfs objfrm = new frmEnviaNfs();
                    objfrm.MdiParent = this;
                    objfrm.Show();

                }
                else
                {
                    KryptonMessageBox.Show(null, "A Tela de Envio de Notas já se encontra aberta", "A V I S O", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.ToString() != "m_safeCertContext é um identificador inválido.")
                {
                    KryptonMessageBox.Show(null, "Erro na configurações das pastas - " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Boolean ok = false;
                    foreach (Form frm in this.MdiChildren)
                    {
                        if (frm is frmLoginConfig)
                        {
                            frm.BringToFront();
                            ok = true;
                        }
                    }
                    if (!ok)
                    {
                        frmLoginConfig objfrm = new frmLoginConfig(this);
                        objfrm.MdiParent = this;
                        objfrm.Show();
                    }
                }
            }

        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            Boolean ok = false;
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is frmLoginConfig)
                {
                    frm.BringToFront();
                    ok = true;
                }
            }
            if (!ok)
            {
                frmLoginConfig objfrm = new frmLoginConfig(this);
                objfrm.MdiParent = this;
                objfrm.Show();
            }
        }

        private void btnUsuario_Click(object sender, EventArgs e)
        {
            bool ok = false;
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is frmLogin)
                {
                    frm.BringToFront();
                    ok = true;
                }
            }
            if (!ok)
            {
                frmLogin objfrm = new frmLogin();
                objfrm.MdiParent = this;
                objfrm.Show();
            }
        }



        private void configuraçõesDoSistemaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MinimizarTudo();
            Boolean ok = false;
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is frmLoginConfig)
                {
                    frm.BringToFront();
                    ok = true;
                }
            }
            if (!ok)
            {
                frmLoginConfig objfrm = new frmLoginConfig(this);
                objfrm.MdiParent = this;
                objfrm.Show();
            }
        }

        private void pastaConfigXmlsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void enviarEmailXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MinimizarTudo();
                NfeGerarXml.NFe.frmEmailContadorNfe objfrm = new NfeGerarXml.NFe.frmEmailContadorNfe();
                objfrm.MdiParent = this;
                objfrm.Show();
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, ex.Message, "A V I S O", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ts = (ToolStripMenuItem)sender;

            if (ts.Name == "tsContingencia")
            {
                tsContingencia.Checked = !tsContingencia.Checked;
                tsNormal.Checked = false;
                tsScan.Checked = false;
            }
            else if (ts.Name == "tsNormal")
            {
                tsNormal.Checked = !tsNormal.Checked;
                tsScan.Checked = false;
                tsContingencia.Checked = false;
            }
            else if (ts.Name == "tsScan")
            {
                tsScan.Checked = !tsScan.Checked;
                tsContingencia.Checked = false;
                tsNormal.Checked = false;
            }

            SalvarModoSistema();
        }

        private void SalvarModoSistema()
        {
            try
            {
                XDocument xdoc = XDocument.Load(belStatic.Pasta_xmls_Configs + belStatic.sConfig);
                xdoc.Element("nfe_configuracoes").Element("AtivaModuloScan").Value = tsScan.Checked.ToString();
                xdoc.Element("nfe_configuracoes").Element("AtivaModuloContingencia").Value = tsContingencia.Checked.ToString();
                xdoc.Save(belStatic.Pasta_xmls_Configs + belStatic.sConfig);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void CarregaStatuModoSistema()
        {
            try
            {
                XDocument xdoc = XDocument.Load(belStatic.Pasta_xmls_Configs + belStatic.sConfig);
                bool bScan = Convert.ToBoolean(xdoc.Element("nfe_configuracoes").Element("AtivaModuloScan").Value);
                bool bContingencia = Convert.ToBoolean(xdoc.Element("nfe_configuracoes").Element("AtivaModuloContingencia").Value);

                if (bScan == false && bContingencia == false)
                {
                    tsNormal.Checked = true;
                }
                else if (bScan)
                {
                    tsScan.Checked = true;
                }
                else if (bContingencia)
                {
                    tsContingencia.Checked = true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void versãoHlpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Version version = this.GetType().Assembly.GetName().Version;
            string versionstring = version.Major + "." + version.Minor +
           "." + version.Build + "." + version.Revision;
            KryptonMessageBox.Show(string.Format("Versão atual do Sistema: {0}",
                            versionstring), "Versão", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cCeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MinimizarTudo();
            try
            {
                Boolean ok = false;
                foreach (Form frm in this.MdiChildren)
                {
                    if (frm is frmBuscaCCe)
                    {
                        frm.BringToFront();
                        ok = true;
                    }
                }
                if (ok == false)
                {
                    frmBuscaCCe objfrm = new frmBuscaCCe();
                    objfrm.MdiParent = this;
                    belGerarXML objbelGeraXml = new belGerarXML();
                    objfrm.WindowState = FormWindowState.Minimized;
                    objfrm.Show();
                    objfrm.WindowState = FormWindowState.Maximized;
                }
                else
                {
                    KryptonMessageBox.Show(null, "A Tela de Visualização de Notas já se encontra aberta", "A V I S O", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void toolStripButton_MouseEnter(object sender, EventArgs e)
        {
            ToolStripButton tsButton = (ToolStripButton)sender;
            tsButton.Font = new Font(tsButton.Font.Name, 16, FontStyle.Bold, GraphicsUnit.Pixel);
            tsButton.ForeColor = Color.NavajoWhite;
        }

        private void toolStripButton_MouseLeave(object sender, EventArgs e)
        {
            ToolStripButton tsButton = (ToolStripButton)sender;
            tsButton.Font = new Font(tsButton.Font.Name, 12, FontStyle.Regular, GraphicsUnit.Pixel);
            tsButton.ForeColor = Color.White;
        }

        private void VisualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem menu = (ToolStripMenuItem)sender;
                belIsolated objIsolated = new belIsolated();
                SetVisualandBackColor(menu.Text);
                if (belStatic.bClickOnce)
                {
                    objIsolated.SalvarArquivo(_VISUAL, menu.Text, belIsolated.Lugar.Local);
                }
                else
                {
                    belRegedit.SalvarRegistro("Skin", _VISUAL, menu.Text);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void SetVisualandBackColor(string sCor)
        {
            try
            {
                foreach (Control ctl in this.Controls)
                {
                    if ((ctl) is MdiClient)
                    {
                        if (sCor.Equals("Preto"))
                        {
                            kryptonManager1.GlobalPaletteMode = PaletteModeManager.Office2010Black;
                            menuPreto.Checked = true;
                            menuAzul.Checked = false;
                            menuAzul2.Checked = false;
                        }
                        else if (sCor.Equals("Azul"))
                        {
                            kryptonManager1.GlobalPaletteMode = PaletteModeManager.Office2007Blue;
                            menuPreto.Checked = false;
                            menuAzul.Checked = true;
                            menuAzul2.Checked = false;
                        }
                        else if (sCor.Equals("Azul 2"))
                        {
                            kryptonManager1.GlobalPaletteMode = PaletteModeManager.Office2010Blue;
                            menuPreto.Checked = false;
                            menuAzul.Checked = false;
                            menuAzul2.Checked = true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        private void fecharTodasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void MinimizarTudo()
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.WindowState = FormWindowState.Minimized;
            }
        }

        private void kryptonSliderBar1_ValueChanged(AC.ExtendedRenderer.Toolkit.KryptonSlider Sender, AC.ExtendedRenderer.Toolkit.KryptonSlider.SliderEventArgs e)
        {
            object Tag;
            if (Convert.ToInt32(kryptonSliderBar1.Value) > 0)
            {
                Tag = (Convert.ToInt32(kryptonSliderBar1.Value) + 15).ToString();

            }
            else
            {
                Tag = (-1 * ((Convert.ToInt32(kryptonSliderBar1.Value) * -1) - 15)).ToString();
            }

            decimal bla = Math.Round(Convert.ToDecimal((70 + Convert.ToDecimal(Tag.ToString())) / 100), 2);

            this.Opacity = Convert.ToDouble(bla);
        }



        private void tsAtualizacao_Click(object sender, EventArgs e)
        {
            try
            {
                tsAtualizacao.Visible = true;
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    tsAtualizacao.Text = "Aguarde...";
                    frmAviso objfrm = new frmAviso();
                    objfrm.Show();
                    tsAtualizacao.Text = "";
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(ex.Message);
            }

        }

        private void localDeInstalaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (KryptonMessageBox.Show(null, Application.StartupPath + Environment.NewLine + "Deseja abrir o local ?", "Local", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(Application.StartupPath);
            }
        }

        private void btnMinimiza_Click(object sender, EventArgs e)
        {
            if (headerMenuLateral.HeaderPositionPrimary == VisualOrientation.Top)
            {
                headerMenuLateral.HeaderPositionPrimary = VisualOrientation.Left;
                headerMenuLateral.Size = new Size(22, headerMenuLateral.Height);
            }
            else
            {
                headerMenuLateral.HeaderPositionPrimary = VisualOrientation.Top;
                headerMenuLateral.Size = new Size(141, headerMenuLateral.Height);
            }
        }

        private void horaServidorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(HLP.Util.Util.GetDateServidor().ToString());
        }

        private void conexãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                belConnection objboCon = new belConnection();
                MessageBox.Show(objboCon.get_Conexao().ConnectionString);
            }
            catch (Exception)
            {
                
                throw;
            }

        }

    }
}