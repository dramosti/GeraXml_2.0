using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using FirebirdSql.Data.FirebirdClient;
using System.Globalization;
using HLP.Dao;
using HLP.bel;
using DANFE;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ComponentFactory.Krypton.Toolkit;
using HLP.bel.CCe;
using HLP.bel.Static;
using HLP.bel.NFe.GeraXml;
using HLP.bel.CTe;
using HLP.bel.NFe;
using System.Collections;

namespace NfeGerarXml
{
    public partial class frmArquivosXmlNfe : KryptonForm
    {
        #region Propriedades
        X509Certificate2 cert = new X509Certificate2();
        frmGerarXml objfrmPrincipal = null;
        belnfeStatusServicoNF objnfeStatusServicoNF = null;
        frmLogin objfrmInicializacao = null;
        List<string> lNotasPendentes = new List<string>();
        belConnection cx = new belConnection();
        public string cd_ufnor;
        public string sVersao;
        public volatile bool deveparar;
        dsDanfe dsdanfe = new dsDanfe();
        HLP.bel.NFe.GeraXml.Globais LeRegWin = new HLP.bel.NFe.GeraXml.Globais();
        string sMesageErro = string.Empty;
        bool bTodosImprimir = false;
        bool bTodosEnviar = false;
        public string sNomeArq = "";
        public string sFormaEmissao;
        public string sForDanfe;
        List<string> sNotas = new List<string>();
        public string sFormaEmiNFe;
        public string sFormDanfe;
        public string Uf_Empresa;
        private MessageBoxIcon _msgIcon = MessageBoxIcon.Error;
        Version version = null;



        #endregion

        public frmArquivosXmlNfe(frmGerarXml objfrmPrincipal)
        {
            InitializeComponent();
            this.objfrmPrincipal = objfrmPrincipal;
        }
        public frmArquivosXmlNfe()
        {
            InitializeComponent();
        }
        private void frmArquivosXml_Load(object sender, EventArgs e)
        {
            try
            {
                Inicializacao();
            }
            catch (Exception ex)
            {
                new HLPexception(ex.Message, ex);
            }
        }
        private void Inicializacao()
        {
            try
            {
                belStatic.bNotaServico = false;
                version = objfrmPrincipal.GetType().Assembly.GetName().Version;
                HabilitaBotoes(true);
                AssinaNFeXml objbuscanome = new AssinaNFeXml();
                cert = new X509Certificate2();
                cert = objbuscanome.BuscaNome("");

                if (belUtil.ValidaCertificado(cert))
                {

                    belStatic.bModoContingencia = Convert.ToBoolean(LeRegWin.LeRegConfig("AtivaModuloContingencia"));
                    belStatic.bModoSCAN = Convert.ToBoolean(LeRegWin.LeRegConfig("AtivaModuloScan"));
                    belStatic.iSerieSCAN = (belStatic.bModoSCAN == true ? Convert.ToInt32(LeRegWin.LeRegConfig("SerieScan")) : 0);

                    DirectoryInfo info = new DirectoryInfo(belStaticPastas.CBARRAS);
                    LimparPasta(info);
                    if (belUtil.VerificaSeEstaNaHLP())
                    {
                        belStatic.bDentroHlp = true;
                        KryptonMessageBox.Show(null, "VOCÊ ESTÁ TRABALHANDO NA HLP, VERIFIQUE O AMBIENTE DO SISTEMA DA NFE", "CUIDADO, ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        belRegras objbelRegras = new belRegras();
                        objbelRegras.AlteraEmpresaParaHomologacao();
                    }
                    nmEmpresa.Text = belStatic.sNomeEmpresa + " - " + belStatic.codEmpresaNFe;
                    nmStatus.Text = (belStatic.tpAmb == 1 ? "Produção" : "Homologação");

                    this.sVersao = LeRegWin.LeRegConfig("Empresa");
                    CarregaConfiguracoesIniciais(belStatic.codEmpresaNFe);
                    string sTipoImpressao = LeRegWin.LeRegConfig("TipoImpressao");
                    cbxFormDanfe.SelectedIndex = 0;
                    if (sTipoImpressao != "")
                    {
                        cbxFormDanfe.Text = sTipoImpressao;
                    }
                    dtpIni.Text = HLP.Util.Util.GetDateServidor().ToString("dd/MM/yyyy");
                    dtpFim.Text = HLP.Util.Util.GetDateServidor().ToString("dd/MM/yyyy");
                    rbtNaoEnviadas.Checked = true;
                    belStatic.iStatusAtualSistema = 0; // Passa o zero para entrar no método novamente!
                    VerificaStatusSefaz();
                    timerWebServices.Enabled = true; // Ativa a Verificação do Web Service do Estado.                
                    if ((belStatic.iStatusAtualSistema == 1))
                    {
                        VerificaNotasPendentesEnvio();
                    }
                }
                else
                {
                    timerInicializacao.Start();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        #region Botões
        private void btnGerarContingencia_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dgvNF.RowCount; i++)
                {
                    if (dgvNF["ASSINANF", i].Value != null)
                    {
                        if (dgvNF["ASSINANF", i].Value.ToString().Equals("True"))
                        {
                            string sRetorno = VerificaCampoReciboPreenchido(belStatic.codEmpresaNFe, dgvNF["CD_NFSEQ", i].Value.ToString());
                            if (sRetorno == "denegada")
                            {
                                throw new Exception("A Nota de Sequencia = " + dgvNF["CD_NFSEQ", i].Value.ToString() + " foi Denegada pelo Sefaz e não podera ser excluída, cancelada e nem alterada.");
                            }
                            else
                            {
                                throw new Exception("A Nota de Sequencia = " + dgvNF["CD_NFSEQ", i].Value.ToString() + " Já tem um retorno Salvo no Banco de Dados, tente Buscar Retorno");
                            }
                        }
                    }
                }

                sslStatusEnvio.Text = "Acessando Banco de Dados ...";


                sFormaEmiNFe = "";

                if (belStatic.bModoSCAN && ((belStatic.iStatusAtualSistema == 3)))
                {
                    sFormaEmiNFe = "3";
                }
                else if (belStatic.bModoContingencia && (belStatic.iStatusAtualSistema == 2))
                {
                    sFormaEmiNFe = "2";
                }
                else
                {
                    sFormaEmiNFe = "1";
                }

                sFormDanfe = "";
                if (cbxFormDanfe.SelectedIndex == 0)
                {
                    sFormDanfe = "1";
                }
                else
                {
                    sFormDanfe = "2";
                }
                string sNfCancelada = "";
                GeraXMLExp Export = new GeraXMLExp(belStatic.iStatusAtualSistema);

                for (int i = 0; i < dgvNF.RowCount; i++)
                {
                    if (((dgvNF["ASSINANF", i].Value != null) && (dgvNF["ASSINANF", i].Value.ToString().Equals("True"))) && ((dgvNF["CANCELADA", i].Value == null) || (dgvNF["CANCELADA", i].Value.ToString() == "0"))) //Danner - o.s. SEM - 17/12/2009
                    {
                        //Verifico se a Nota Já foi Enviada em Modo de Contingencia
                        if ((dgvNF["st_contingencia", i].Value.ToString().Equals("S"))
                                   && (dgvNF["ST_NFE", i].Value.ToString().Equals("0"))
                                   && (dgvNF["CD_NOTAFIS", i].Value.ToString() != ""))
                        {
                            KryptonMessageBox.Show(null, "Nfe já Gerada em Modo de Contingência - Seq: " + dgvNF["CD_NOTAFIS", i].Value.ToString(), "A V I S O", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }
                        else
                        {
                            sNotas.Add((string)dgvNF["CD_NFSEQ", i].Value);
                        }
                    }
                    if ((dgvNF["CANCELADA", i].Value != null) && (dgvNF["CANCELADA", i].Value.ToString() == "1"))
                    {
                        sNfCancelada += "Nota Fiscal " + dgvNF["CD_NOTAFIS", i].Value.ToString() + " - Esta Cancelada e não é Permitido o Reenvio da mesma Nota!" + Environment.NewLine + Environment.NewLine;
                    }
                }
                Export.sExecao = string.Empty;

                if (sNotas.Count == 0)
                {
                    KryptonMessageBox.Show("Nenhuma nota Valida foi Selecionada!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (sNfCancelada != "")
                    {
                        KryptonMessageBox.Show(sNfCancelada, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return;
                }
                else if (sNotas.Count > 1)
                {
                    throw new Exception("Só é Possível Enviar uma Nota de Cada vez em Modo de Contingência");
                }
                //verifica no banco se as sequencias são existentes.
                if (ValidaSeqNoBanco())
                {
                    Export.NFe(belStatic.codEmpresaNFe, sNotas, Export.NomeArqNFe(belStatic.codEmpresaNFe), sFormaEmiNFe, sFormDanfe, cd_ufnor, cert, belStatic.bModoSCAN, belStatic.iSerieSCAN, sFormaEmiNFe, version);

                    frmVisualizaNotasNfe objFrm = new frmVisualizaNotasNfe(Export.lTotNota, belStatic.codEmpresaNFe);
                    objFrm.ShowDialog();
                    if (objFrm.Cancel == true)
                    {
                        sNotas.Clear();
                        _msgIcon = MessageBoxIcon.Information;
                        PopulaDataGridView();
                        throw new Exception("Envio da(s) Nota(s) Cancelado");
                    }
                    sslStatusEnvio.Text = "Gerando XML de Contingência ...";

                    Export.lTotNota = objFrm.lObjTotNotasFinal;

                    // Gera a Estrutura do XML
                    Export.geraArquivoNFE(sNotas, cert, belStatic.codEmpresaNFe, Export.NomeArqNFe(belStatic.codEmpresaNFe));
                    sslStatusEnvio.Text = "Arquivos Gerados...";
                    string sNotasGeradas = "Sequencia(as) de Nota(as) Gerada(as)  ";
                    foreach (string item in sNotas)
                    {
                        sNotasGeradas += "- " + item.ToString();
                    }
                    for (int i = 0; i < objFrm.lObjTotNotasFinal.Count; i++)
                    {
                        belIde objbelIde = objFrm.lObjTotNotasFinal[i][0] as belIde;

                        string sSqlAtualizaNF = "update NF set st_contingencia = '" + "S" +
                                         "' where cd_empresa = '" + belStatic.codEmpresaNFe +
                                         "' and cd_nfseq = '" + objbelIde.Cnf.Substring(2, 6) + "'";

                        using (FbCommand fbc = new FbCommand(sSqlAtualizaNF, cx.get_Conexao()))
                        {
                            cx.Open_Conexao();
                            fbc.ExecuteNonQuery();
                            cx.Close_Conexao();
                        }
                    }
                    KryptonMessageBox.Show(null, Environment.NewLine + sNotasGeradas, "I M P O R T A N T E", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    sslStatusEnvio.Text = "";
                    PopulaDataGridView();
                    sNotas.Clear();
                }
                else
                {
                    PopulaDataGridViewPendenciaContingencia();
                }
            }
            catch (Exception ex)
            {
                sNotas.Clear();
                new HLPexception(ex.Message, ex);
            }
            finally
            {
                cx.Close_Conexao();
            }
        }

        private void btnGerarXml_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dgvNF.RowCount; i++)
                {
                    if (dgvNF["ASSINANF", i].Value != null)
                    {
                        if (dgvNF["ASSINANF", i].Value.ToString().Equals("True"))
                        {
                            string sRetorno = VerificaCampoReciboPreenchido(belStatic.codEmpresaNFe, dgvNF["CD_NFSEQ", i].Value.ToString());
                            if (sRetorno == "denegada")
                            {
                                throw new Exception("A Nota de Sequencia = " + dgvNF["CD_NFSEQ", i].Value.ToString() + " foi Denegada pelo Sefaz e não podera ser excluída, cancelada e nem alterada.");
                            }
                            else if (sRetorno != "")
                            {
                                throw new Exception("A Nota de Sequencia = " + dgvNF["CD_NFSEQ", i].Value.ToString() + " Já tem um retorno Salvo no Banco de Dados, tente Buscar Retorno");
                            }
                        }
                    }
                }

                #region Verificações
                sslStatusEnvio.Text = "Gerando e Assinando XML...";
                sFormaEmiNFe = "";

                if (belStatic.bModoSCAN && ((belStatic.iStatusAtualSistema == 3)))
                {
                    sFormaEmiNFe = "3";
                }
                else if (belStatic.bModoContingencia && (belStatic.iStatusAtualSistema == 2))
                {
                    sFormaEmiNFe = "2";
                }
                else
                {
                    sFormaEmiNFe = "1";
                }

                sFormDanfe = "";
                if (cbxFormDanfe.SelectedIndex == 0)
                {
                    sFormDanfe = "1";
                }
                else
                {
                    sFormDanfe = "2";
                }
                string sNfCancelada = "";
                #endregion

                #region Seleciona Notas Selecionadas na Grid
                bool bEnviaContingencia = false;
                GeraXMLExp Export = new GeraXMLExp(belStatic.iStatusAtualSistema);
                for (int i = 0; i < dgvNF.RowCount; i++)
                {
                    if (((dgvNF["ASSINANF", i].Value != null) && (dgvNF["ASSINANF", i].Value.ToString().Equals("True")))
                                && ((dgvNF["CANCELADA", i].Value == null) || (dgvNF["CANCELADA", i].Value.ToString() == "0"))
                                && (dgvNF["ST_NFE", i].Value.ToString().Equals("0")))
                    {
                        if ((dgvNF["st_contingencia", i].Value.ToString().Equals("S"))
                            && (dgvNF["ST_NFE", i].Value.ToString().Equals("0"))
                            && (dgvNF["CD_NOTAFIS", i].Value.ToString() != ""))
                        {
                            if (sNotas.Count > 0)
                            {
                                throw new Exception("Obs: As Notas Pendentes de Envio devem ser Enviadas Uma de cada vez." + Environment.NewLine + Environment.NewLine);
                            }
                            else
                            {
                                bEnviaContingencia = true;
                            }
                        }

                        sNotas.Add((string)dgvNF["CD_NFSEQ", i].Value);
                    }
                    if ((dgvNF["CANCELADA", i].Value != null) && (dgvNF["CANCELADA", i].Value.ToString() == "1"))
                    {
                        sNfCancelada += "Nota Fiscal " + dgvNF["CD_NOTAFIS", i].Value.ToString() + " - Esta Cancelada e não é Permitido o Reenvio da mesma Nota!" + Environment.NewLine + Environment.NewLine;
                    }
                }

                Export.sExecao = string.Empty;

                if (sNotas.Count == 0)
                {
                    KryptonMessageBox.Show("Nenhuma nota Valida foi Selecionada!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (sNfCancelada != "")
                    {
                        KryptonMessageBox.Show(sNfCancelada, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return;
                }

                #endregion
                //verifica no banco se as sequencias são existentes.
                belBusRetFazenda objbusretfazenda = new belBusRetFazenda();
                if (ValidaSeqNoBanco())
                {
                    if (bEnviaContingencia == false)
                    {

                        #region Visualiza Nota
                        Export.NFe(belStatic.codEmpresaNFe, sNotas, Export.NomeArqNFe(belStatic.codEmpresaNFe), sFormaEmiNFe, sFormDanfe, cd_ufnor, cert, belStatic.bModoSCAN, belStatic.iSerieSCAN, sFormaEmiNFe, version);
                        frmVisualizaNotasNfe objFrm = new frmVisualizaNotasNfe(Export.lTotNota, belStatic.codEmpresaNFe);
                        objFrm.ShowDialog();
                        if (objFrm.Cancel == true)
                        {
                            _msgIcon = MessageBoxIcon.Information;
                            throw new Exception("Envio da(s) Nota(s) Cancelado");
                        }

                        Export.lTotNota = objFrm.lObjTotNotasFinal;
                        #endregion

                        #region Envia Nfe Normal
                        // Gera a Estrutura do XML
                        Export.geraArquivoNFE(sNotas, cert, belStatic.codEmpresaNFe, Export.NomeArqNFe(belStatic.codEmpresaNFe));
                        sslStatusEnvio.Text = "Enviando o Lote a Ministério da Fazenda...";
                        belNfeRecepcao y = new belNfeRecepcao(Export.sXmlfull, "2.00", cert, Uf_Empresa, belStatic.bModoSCAN, belStatic.iSerieSCAN);
                        if (y.Recibo.ToUpper().Contains("VERSAO"))
                        {
                            throw new Exception("Ocorreu uma exceção com o webservice de Recepção. Favor verificar se os serviços estão estáveis.");
                        }


                        for (int i = 0; i < sNotas.Count; i++)
                        {
                            //Grava o número do recibo nas notas que foram enviadas a Fazenda
                            gravaRecibo(y.Recibo, belStatic.codEmpresaNFe, sNotas[i]);

                            sslStatusEnvio.Text = "Gravando recibo do Lote";
                        }
                        sslStatusEnvio.Text = "Buscando retorno do Ministério da Fazenda";

                        objbusretfazenda = new belBusRetFazenda(sNotas, Export.nfes, y.Recibo, cert, Uf_Empresa);
                        GeraXml.NFe.frmBuscaRetorno objfrmAvisoRet = new GeraXml.NFe.frmBuscaRetorno(objbusretfazenda, GeraXml.NFe.frmBuscaRetorno.tipoBusca.ENVIO);
                        objfrmAvisoRet.ShowDialog();

                        //Se nenhuma nota foi autorizada entao, ele nem continua para não dar erro nas proximas rotinas.
                        if (objbusretfazenda.Nfeautorizadas.Count == 0)
                        {
                            sslStatusEnvio.Text = "Nenhuma Nota Autorizada ...";
                            throw new Exception("Nenhuma nota do Lote enviado a Fazenda foi Autorizada" + Environment.NewLine + objbusretfazenda.Loteres);
                        }

                        sMesageErro = Export.sExecao;
                        StringBuilder sSql = new StringBuilder();
                        foreach (string sNota in objbusretfazenda.Nfeautorizadas)
                        {
                            if (sSql.ToString().Length > 0)
                            {
                                sSql.Append(string.Format(", '{0}'", sNota));
                            }
                            else
                            {
                                sSql.Append(string.Format("'{0}'", sNota));
                            }
                        }
                        /// Update para mudar o estado da nota dizendo que a nota foi autorizada ou não.
                        sSql.Insert(0, "update nf set st_nfe = 'S' Where cd_nfseq in (");
                        sSql.Append(")");
                        sSql.Append("and ");
                        sSql.Append("cd_empresa = '");
                        sSql.Append(belStatic.codEmpresaNFe);
                        sSql.Append("'");


                        using (FbCommand cmdUpdate = new FbCommand(sSql.ToString(), cx.get_Conexao()))
                        {
                            cx.Open_Conexao();
                            cmdUpdate.ExecuteNonQuery();
                            cx.Close_Conexao();
                        }
                        for (int i = 0; i < dgvNF.RowCount; i++)
                        {
                            if (dgvNF["ASSINANF", i].Value != null)
                            {
                                if (dgvNF["ASSINANF", i].Value.ToString().Equals("True"))
                                {
                                    dgvNF["ST_NFE", i].Value = true;
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region Envio de Notas de Contingencia Pendentes !!

                        XmlDocument xml = new XmlDocument();
                        string sQuery = "SELECT NF.cd_chavenfe from nf " +
                                        "Where nf.cd_empresa = '" + belStatic.codEmpresaNFe +
                                        "' and nf.cd_nfseq = '" + sNotas[0].ToString() + "'";
                        string sID = string.Empty;

                        using (FbCommand cmd = new FbCommand(sQuery, cx.get_Conexao()))
                        {
                            cx.Open_Conexao();
                            sID = cmd.ExecuteScalar().ToString();
                            cx.Close_Conexao();
                        }
                        DirectoryInfo dinfo = new DirectoryInfo(belStaticPastas.CONTINGENCIA); ;
                        FileInfo[] finfo = dinfo.GetFiles();
                        foreach (FileInfo item in finfo)
                        {
                            if (Path.GetExtension(item.FullName).ToUpper().Equals(".XML"))
                            {
                                if (item.Name.ToString().Length == 26)
                                {
                                    xml.Load(@item.FullName);
                                    if (xml.GetElementsByTagName("infNFe")[0].Attributes["Id"].Value.ToString().Replace("NFe", "").Equals(sID))
                                    {
                                        Export.sXmlfull = xml.InnerXml.ToString();
                                        string sPathDest = belStaticPastas.ENVIO + "\\" + item.Name;
                                        string sPathOrigem = belStaticPastas.CONTINGENCIA + "\\" + item.Name;
                                        if (File.Exists(sPathDest))
                                        {
                                            File.Delete(sPathDest);
                                        }
                                        File.Copy(sPathOrigem, sPathDest);
                                    }
                                }
                                else if (item.Name.ToString().Length == 52)
                                {
                                    xml.Load(@item.FullName);
                                    if (xml.GetElementsByTagName("infNFe")[0].Attributes["Id"].Value.ToString().Replace("-nfe", "").Replace("NFe", "").Equals(sID))
                                    {
                                        Export.nfes.Add(xml.InnerXml.ToString());
                                        string sPathDest = belStaticPastas.ENVIO + "\\" + item.Name.Substring(2, 4) + "\\" + item.Name; //OS_25024
                                        string sPathOrigem = belStaticPastas.CONTINGENCIA + "\\" + item.Name;
                                        if (File.Exists(sPathDest))
                                        {
                                            File.Delete(sPathDest);
                                        }
                                        File.Copy(sPathOrigem, sPathDest);
                                    }
                                }
                                if ((Export.nfes.Count > 0) && (Export.sXmlfull != null))
                                {
                                    belNfeRecepcao y = new belNfeRecepcao(Export.sXmlfull, "2.00", cert, Uf_Empresa, belStatic.bModoSCAN, belStatic.iSerieSCAN);
                                    for (int i = 0; i < sNotas.Count; i++)
                                    {
                                        //Grava o número do recibo nas notas que foram enviadas a Fazenda
                                        gravaRecibo(y.Recibo, belStatic.codEmpresaNFe, sNotas[i]);
                                        sslStatusEnvio.Text = "Gravando recibo do Lote";
                                    }

                                    objbusretfazenda = new belBusRetFazenda(sNotas, Export.nfes, y.Recibo, cert, Uf_Empresa);
                                    GeraXml.NFe.frmBuscaRetorno objfrmAvisoRet = new GeraXml.NFe.frmBuscaRetorno(objbusretfazenda, GeraXml.NFe.frmBuscaRetorno.tipoBusca.ENVIO);
                                    objfrmAvisoRet.ShowDialog();

                                    //Se nenhuma nota foi autorizada entao, ele nem continua para não dar erro nas proximas rotinas.
                                    if (objbusretfazenda.Nfeautorizadas.Count == 0)
                                    {
                                        sslStatusEnvio.Text = "Nenhuma Nota Autorizada ...";
                                        throw new Exception("Nenhuma nota do Lote enviado a Fazenda foi Autorizada" + Environment.NewLine + objbusretfazenda.Loteres);
                                    }

                                    sMesageErro = Export.sExecao;
                                    StringBuilder sSql = new StringBuilder();
                                    foreach (string sNota in objbusretfazenda.Nfeautorizadas)
                                    {
                                        if (sSql.ToString().Length > 0)
                                        {
                                            sSql.Append(string.Format(", '{0}'", sNota));
                                        }
                                        else
                                        {
                                            sSql.Append(string.Format("'{0}'", sNota));
                                        }
                                    }
                                    /// Update para mudar o estado da nota dizendo que a nota foi autorizada ou não.
                                    sSql.Insert(0, "update nf set st_nfe = 'S' Where cd_nfseq in (");
                                    sSql.Append(")");
                                    sSql.Append("and ");
                                    sSql.Append("cd_empresa = '");
                                    sSql.Append(belStatic.codEmpresaNFe);
                                    sSql.Append("'");

                                    using (FbCommand cmdUpdate = new FbCommand(sSql.ToString(), cx.get_Conexao()))
                                    {
                                        cx.Open_Conexao();
                                        cmdUpdate.ExecuteNonQuery();
                                        cx.Close_Conexao();
                                    }
                                    for (int i = 0; i < dgvNF.RowCount; i++)
                                    {
                                        if (dgvNF["ASSINANF", i].Value != null)
                                        {
                                            if (dgvNF["ASSINANF", i].Value.ToString().Equals("True"))
                                            {
                                                dgvNF.Rows[i].DefaultCellStyle.BackColor = Color.Aquamarine;
                                                dgvNF["ST_NFE", i].Value = true;
                                            }
                                        }
                                    }
                                    VerificaNotasPendentesEnvio();
                                    break;
                                }
                            }
                        }

                        #endregion
                    }
                    sslStatusEnvio.Text = "Arquivos Gerados...";
                    KryptonMessageBox.Show("Arquivo Gerado" + Environment.NewLine + objbusretfazenda.Loteres);
                    sslStatusEnvio.Text = "";
                    for (int i = 0; i < dgvNF.RowCount; i++)
                    {
                        foreach (var item in objbusretfazenda.Nfeautorizadas) //Danner  - o.s. 23851 - 21/11/2009
                        {
                            if (dgvNF["cd_nfseq", i].Value.ToString() == item) //Danner - o.s. 23851 - 19/11/2009
                            {
                                if (dgvNF["ASSINANF", i].Value != null)
                                {
                                    dgvNF["ST_NFE", i].Value = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    PopulaDataGridView();
                }
            }

            catch (Exception ex)
            {
                KryptonMessageBox.Show(ex.Message + Environment.NewLine + "XML não Foi gerado com Sucesso!!!", "A V I S O", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _msgIcon = MessageBoxIcon.Error;
                PopulaDataGridView();
            }
            finally
            {
                cx.Close_Conexao();
                sNotas.Clear();
            }
        }


        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            PopulaDataGridView();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.objfrmPrincipal.sStatusSefaz = "";
            this.Close();
        }

        private void btnCancNFe_Click(object sender, EventArgs e)
        {
            belGerarXML objbelGeraXml = new belGerarXML();
            if (belStatic.sNomeEmpresa.Equals("LORENZON"))
            {
                if (belStatic.BAlteraDadosNfe)
                {
                    CancelaNFe();
                }
                else
                {
                    KryptonMessageBox.Show(null, "O usuário Ativo não tem autorização para realizar esse procedimento! "
                        + Environment.NewLine + Environment.NewLine + "Entre em contato com o Administrador do Sistema!", " A V I S O ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                CancelaNFe();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                List<String> sChaves = new List<string>();
                List<string> lCaminhosXml = new List<string>();
                List<string> lCaminhosXmlContingencia = new List<string>();
                List<string> lCaminhosXmlCancelados = new List<string>();
                StringBuilder sbSeq = new StringBuilder();
                string nao_autorizada = "";
                string cancelada = "";
                string arquivoInexist = "";
                int cont_1 = 0;
                int cont = 0;
                DirectoryInfo dinfo = null;
                FileInfo[] finfo = null;
                XmlDocument xml = new XmlDocument();
                List<belIde> objlbelIde = new List<belIde>(); // Lista de Notas Para envio de email

                string select = "";
                for (int i = 0; i < dgvNF.RowCount; i++)
                {
                    bool bImprime = false;

                    if (dgvNF["Imprime", i].Value == null)
                    {
                        bImprime = false;
                    }
                    else if (dgvNF["Imprime", i].Value.ToString() == "False")
                    {
                        bImprime = false;
                    }
                    else
                    {
                        bImprime = true;
                    }

                    if (((dgvNF["ST_NFE", i].Value.ToString().Equals("1")) || (dgvNF["st_contingencia", i].Value.ToString().Equals("S"))) && (bImprime == true))//&& (dgvNF["CANCELADA", i].Value.ToString().Equals("0")))
                    {

                        select = "Select cd_chavenfe from nf where cd_empresa = '"
                               + belStatic.codEmpresaNFe.Trim() + "' and CD_NFSEQ ='" + dgvNF["CD_NFSEQ", i].Value.ToString() + "'";

                        try
                        {
                            using (FbCommand cmd = new FbCommand(select, cx.get_Conexao()))
                            {
                                cx.Open_Conexao();
                                sChaves.Add(cmd.ExecuteScalar().ToString());

                                try
                                {
                                    if ((dgvNF["st_contingencia", i].Value.ToString().Equals("S"))
                                                         && (dgvNF["ST_NFE", i].Value.ToString().Equals("0"))
                                                         && (dgvNF["CD_NOTAFIS", i].Value.ToString() != ""))
                                    {
                                        #region Imprime Contingência
                                        dinfo = new DirectoryInfo(belStaticPastas.CONTINGENCIA); ;
                                        finfo = dinfo.GetFiles();
                                        foreach (FileInfo item in finfo)
                                        {
                                            if (Path.GetExtension(item.FullName).ToUpper().Equals(".XML"))
                                            {
                                                if (item.Name.ToString().Length == 26)
                                                {
                                                    xml.Load(@item.FullName);

                                                    if (xml.GetElementsByTagName("infNFe")[0].Attributes["Id"].Value.ToString().Replace("NFe", "").Equals(cmd.ExecuteScalar().ToString()))
                                                    {
                                                        lCaminhosXmlContingencia.Add(belStaticPastas.CONTINGENCIA + "\\" + (item.ToString()));
                                                    }
                                                }
                                            }
                                        }
                                        #endregion
                                    }
                                    else if (dgvNF["cd_recibocanc", i].Value.ToString() != "")
                                    {
                                        dinfo = new DirectoryInfo(belStaticPastas.CANCELADOS); ;
                                        finfo = dinfo.GetFiles();
                                        bool bAchouArquivo = false;

                                        foreach (FileInfo item in finfo)
                                        {
                                            if (Path.GetExtension(item.FullName).ToUpper().Equals(".XML"))
                                            {
                                                if (item.Name.ToString().Length == 56)
                                                {
                                                    if (item.Name.Replace("-can.xml.xml", "").Equals(cmd.ExecuteScalar().ToString()))
                                                    {
                                                        lCaminhosXmlCancelados.Add(dinfo.FullName + "\\" + (item.ToString()));
                                                        belIde objbelide = new belIde();
                                                        objbelide.Cnf = dgvNF["CD_NOTAFIS", i].Value.ToString();
                                                        objbelide.Nnf = dgvNF["CD_NFSEQ", i].Value.ToString();
                                                        objlbelIde.Add(objbelide);
                                                        bAchouArquivo = true;
                                                    }
                                                }
                                            }
                                        }
                                        if (bAchouArquivo == false)
                                        {
                                            foreach (DirectoryInfo diretorio in dinfo.GetDirectories())
                                            {
                                                foreach (FileInfo item in diretorio.GetFiles("*.xml"))
                                                {
                                                    if (item.Name.ToString().Length == 56)
                                                    {
                                                        if (item.Name.Replace("-can.xml.xml", "").Equals(cmd.ExecuteScalar().ToString()))
                                                        {
                                                            lCaminhosXmlCancelados.Add(diretorio.FullName + "\\" + (item.ToString()));
                                                            belIde objbelide = new belIde();
                                                            objbelide.Cnf = dgvNF["CD_NOTAFIS", i].Value.ToString();
                                                            objbelide.Nnf = dgvNF["CD_NFSEQ", i].Value.ToString();
                                                            objlbelIde.Add(objbelide);
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        string sArquivo = belStaticPastas.ENVIADOS + cmd.ExecuteScalar().ToString().Substring(2, 4) + "\\" + cmd.ExecuteScalar().ToString() + "-nfe.xml";

                                        if (File.Exists(sArquivo))
                                        {
                                            lCaminhosXml.Add(sArquivo);
                                            belIde objbelide = new belIde();
                                            objbelide.Cnf = dgvNF["CD_NOTAFIS", i].Value.ToString();
                                            objbelide.Nnf = dgvNF["CD_NFSEQ", i].Value.ToString();
                                            objlbelIde.Add(objbelide);

                                        }
                                        else
                                        {
                                            dinfo = new DirectoryInfo(belStaticPastas.ENVIADOS + "/" + cmd.ExecuteScalar().ToString().Substring(2, 4)); ;
                                            finfo = dinfo.GetFiles();

                                            foreach (FileInfo item in finfo)
                                            {
                                                if (Path.GetExtension(item.FullName).ToUpper().Equals(".XML"))
                                                {
                                                    if (item.Name.ToString().Length == 52)
                                                    {
                                                        // xml.Load(@item.FullName);
                                                        if (item.Name.Replace("-nfe.xml", "").Equals(cmd.ExecuteScalar().ToString()))
                                                        {
                                                            lCaminhosXml.Add(dinfo.FullName + "\\" + (item.ToString()));
                                                            belIde objbelide = new belIde();
                                                            objbelide.Cnf = dgvNF["CD_NOTAFIS", i].Value.ToString();
                                                            objbelide.Nnf = dgvNF["CD_NFSEQ", i].Value.ToString();
                                                            objlbelIde.Add(objbelide);
                                                            // EnviaEmail(i, item);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    new HLPexception("Verifique se não existe nenhum arquivo Corrompido ou que não seja de Enviados na Pasta Correspondente", ex);
                                    break;
                                }
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        finally { cx.Close_Conexao(); }
                    }
                    else if ((bImprime == true) && (dgvNF["CANCELADA", i].Value.ToString().Equals("0")))
                    {
                        if (cont > 0)
                        {
                            nao_autorizada = nao_autorizada + ", " + dgvNF["CD_NFSEQ", i].Value.ToString();
                        }
                        else
                        {
                            nao_autorizada = dgvNF["CD_NFSEQ", i].Value.ToString();
                        }
                        cont++;
                    }
                }

                // Diego - OS_23834 18/12/2009 
                if ((lCaminhosXml.Count > 0) || (lCaminhosXmlContingencia.Count > 0) || (lCaminhosXmlCancelados.Count > 0))
                {
                    if (lCaminhosXmlCancelados.Count > 0)
                    {
                        //Impressão de Danfe Normal
                        dsdanfe = new dsDanfe();
                        for (int i = 0; i < lCaminhosXmlCancelados.Count; i++)
                        {
                            InformaStatusEnvio("Gerando PDF da DANFE ", i, lCaminhosXml.Count);
                            PopulaDataSetXML(dsdanfe, lCaminhosXmlCancelados[i].ToString(), (i + 1).ToString());
                            dsDanfe dsPDF = new dsDanfe();
                            PopulaDataSetXML(dsPDF, lCaminhosXmlCancelados[i].ToString(), 1.ToString());
                            GeraPDF_Danfe(dsPDF, TipoPDF.CANCELADO, false);
                        }
                        if (LeRegWin.LeRegConfig("EmailAutomatico").ToString() == "True")
                        {
                            EnviaEmail(lCaminhosXml, objlbelIde);
                        }
                        GeraPDF_Danfe(dsdanfe, TipoPDF.CANCELADO, true);
                    }
                    if (lCaminhosXml.Count > 0)
                    {
                        //Impressão de Danfe Normal
                        dsdanfe = new dsDanfe();
                        for (int i = 0; i < lCaminhosXml.Count; i++)
                        {
                            InformaStatusEnvio("Gerando PDF da DANFE ", i, lCaminhosXml.Count);
                            statusStrip1.Refresh();
                            PopulaDataSetXML(dsdanfe, lCaminhosXml[i].ToString(), (i + 1).ToString());
                            dsDanfe dsPDF = new dsDanfe();
                            PopulaDataSetXML(dsPDF, lCaminhosXml[i].ToString(), 1.ToString());
                            GeraPDF_Danfe(dsPDF, TipoPDF.ENVIADO, false);
                        }
                        if (LeRegWin.LeRegConfig("EmailAutomatico").ToString() == "True")
                        {
                            EnviaEmail(lCaminhosXml, objlbelIde);
                        }
                        GeraPDF_Danfe(dsdanfe, TipoPDF.ENVIADO, true);
                    }

                    sslStatusEnvio.Text = "";

                    #region Contingencia
                    if ((lCaminhosXmlContingencia.Count > 0))
                    {
                        string sQueryEmpresa = "select empresa.nm_empresa from empresa " +
                                        "where empresa.cd_empresa = '" + belStatic.codEmpresaNFe + "'";
                        FbCommand fbComEmp = new FbCommand(sQueryEmpresa, cx.get_Conexao());
                        fbComEmp.ExecuteNonQuery();
                        FbDataReader drEmp = fbComEmp.ExecuteReader();
                        cx.Close_Conexao();
                        drEmp.Read();
                        frmContratoContingenciaNfe objfrmAviso = new frmContratoContingenciaNfe(drEmp["nm_empresa"].ToString());
                        objfrmAviso.ShowDialog();

                        if (objfrmAviso.bImprime)
                        {
                            //Impressão de Danfe em Contingencia
                            dsdanfe = new dsDanfe();
                            for (int i = 0; i < lCaminhosXmlContingencia.Count; i++)
                            {
                                PopulaDataSetXML(dsdanfe, lCaminhosXmlContingencia[i].ToString(), (i + 1).ToString());
                            }// OS.23999 - DIEGO - 02/02/2010                            
                            PrintReport(TipoPDF.CONTINGENCIA);
                        }
                        else
                        {
                            KryptonMessageBox.Show(null, "Impressão Cancelada!!", "IMPRESSÃO DE DANFE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    #endregion
                }
                else
                {
                    KryptonMessageBox.Show(null, "Nenhuma nota selecionada para Impressão!", "IMPRESSÃO DE DANFE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                if (nao_autorizada != "")
                {
                    KryptonMessageBox.Show(null, "Sequencia de Notas não autorizadas para a impressão do Danfe: " + nao_autorizada, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                if (arquivoInexist != "")
                {
                    KryptonMessageBox.Show(null, "XML da sequencia a seguir não foi encontrada na Pasta: " + arquivoInexist, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                if (cancelada != "")
                {
                    KryptonMessageBox.Show(null, "XML da Sequencia a seguir está Cancelado e não pode ser impresso: " + cancelada, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                for (int i = 0; i < dgvNF.RowCount; i++)
                {
                    dgvNF["Imprime", i].Value = false;
                }
                PopulaDataGridView();
            }
            catch (DirectoryNotFoundException)
            {
                KryptonMessageBox.Show(null, "Pasta de Arquivos não encontrados", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                new HLPexception(ex.Message, ex);
            }
            finally
            {
                cx.Close_Conexao();
            }

        }
        public enum TipoPDF { ENVIADO, CANCELADO, CONTINGENCIA };
        private void GeraPDF_Danfe(dsDanfe ds, TipoPDF tpPdf, bool bVisualiza)
        {
            try
            {
                string sRelImpressao = "";

                if (tpPdf == TipoPDF.CANCELADO)
                {
                    sRelImpressao = "RelDanfeCancelados.rpt";
                }

                if (tpPdf == TipoPDF.ENVIADO)
                {
                    if (cbxFormDanfe.SelectedIndex == 0)
                    {
                        string simplificado = LeRegWin.LeRegConfig("UsaDanfeSimplificada");
                        if (simplificado.ToUpper() == "TRUE")
                        {
                            sRelImpressao = "RelDanfeSimplificada.rpt";
                        }
                        else
                        {
                            sRelImpressao = "RelDanfe.rpt";
                        }
                    }
                    else
                    {
                        sRelImpressao = "RelDanfePaisagem.rpt";
                    }
                }

                ReportDocument rpt = new ReportDocument();
                if (LeRegWin.LeRegConfig("UsaRelatorioEspecifico") == "True")
                {
                    string sCaminho = LeRegWin.LeRegConfig("CaminhoRelatorioEspecifico") + "\\" + sRelImpressao;
                    rpt.Load(sCaminho);
                }
                else
                {
                    rpt.Load(Application.StartupPath + "\\Relatorios" + "\\" + sRelImpressao);
                }
                rpt.SetDataSource(ds);
                rpt.Refresh();

                DirectoryInfo dinfo = new DirectoryInfo(belStaticPastas.ENVIADOS + "\\PDF");
                if (!dinfo.Exists)
                {
                    dinfo.Create();
                }
                string sNmPdfVisualizacao = Environment.MachineName + "_Grupo_Danfes";

                string sCaminhoSave = belStaticPastas.ENVIADOS + "\\PDF\\" + (bVisualiza == false ? (ds.infNFe[0].ideRow.nNF.ToString().PadLeft(6, '0') + (tpPdf.ToString().Equals("ENVIADO") ? "_enviado" : "_cancelado")) : sNmPdfVisualizacao) + ".pdf";

                ExportPDF(rpt, sCaminhoSave);

                if (bVisualiza)
                {
                    //  System.Diagnostics.Process.Start(sCaminhoSave);
                    frmPreviwDanfe objfrmDanfe = new frmPreviwDanfe(rpt);
                    objfrmDanfe.Show();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        private static void ExportPDF(ReportDocument rpt, string sCaminhoSave)
        {
            CrystalDecisions.Windows.Forms.CrystalReportViewer cryView = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            ExportOptions CrExportOptions;
            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
            CrDiskFileDestinationOptions.DiskFileName = sCaminhoSave;
            CrExportOptions = rpt.ExportOptions;
            {
                CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                CrExportOptions.FormatOptions = CrFormatTypeOptions;
            }
            rpt.Export();

        }


        private void PrintReport(TipoPDF tpPdf)
        {
            ReportDocument rpt = new ReportDocument();

            string sRelatorio = "RelDanfe.rpt"; //padrão
            if (tpPdf.ToString().Equals("ENVIADO")) { sRelatorio = (cbxFormDanfe.SelectedIndex == 0 ? "RelDanfe.rpt" : "RelDanfePaisagem.rpt"); }
            else if (tpPdf.ToString().Equals("CANCELADO")) { sRelatorio = "RelDanfeCancelados.rpt"; }
            else if (tpPdf.ToString().Equals("CONTINGENCIA")) { sRelatorio = "RelDanfeContingencia.rpt"; }



            if (LeRegWin.LeRegConfig("UsaRelatorioEspecifico") == "True")
            {
                string sCaminho = LeRegWin.LeRegConfig("CaminhoRelatorioEspecifico") + "\\" + sRelatorio;
                rpt.Load(sCaminho);
            }
            else
            {
                rpt.Load(Application.StartupPath + "\\Relatorios" + "\\" + sRelatorio);
            }
            //if (cbxFormDanfe.SelectedIndex != 0)
            //{
            //    rpt.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
            //    rpt.PrintOptions.PaperSize = PaperSize.PaperA4;
            //    rpt.PrintOptions.PrinterName = @"\\hlp030\Samsung ML-2010 Series";                 
            //}
            rpt.SetDataSource(dsdanfe);
            rpt.Refresh();
            // rpt.PrintToPrinter(1, false, 0, 0);
            frmPreviwDanfe frm = new frmPreviwDanfe(rpt);
            frm.ShowDialog();
        }

        private void btnInutilizaNFe_Click(object sender, EventArgs e)
        {
            belGerarXML objbelGeraXml = new belGerarXML();
            if (belStatic.sNomeEmpresa.Equals("LORENZON"))
            {
                if (belStatic.BAlteraDadosNfe == true)
                {
                    frmInutilizaNFecs frm = new frmInutilizaNFecs(belStatic.codEmpresaNFe, Uf_Empresa, this);
                    frm.ShowDialog();
                }
                else
                {
                    KryptonMessageBox.Show(null, "O usuário Ativo não tem autorização para realizar esse procedimento! "
                        + Environment.NewLine + Environment.NewLine + "Entre em contato com o Administrador do Sistema!", " A V I S O ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                frmInutilizaNFecs frm = new frmInutilizaNFecs(belStatic.codEmpresaNFe, Uf_Empresa, this);
                frm.ShowDialog();
            }
        }

        private void btnConsultaServico_Click(object sender, EventArgs e)
        {
            try
            {
                belnfeStatusServicoNF objnfeStatusServicoNF = new belnfeStatusServicoNF("2.00", this.cd_ufnor, cert, Uf_Empresa);

                string sTempo = string.Empty;
                if (objnfeStatusServicoNF.Tmed > 1)
                {
                    sTempo = "Segundos";
                }
                else
                {
                    sTempo = "Segundo";
                }
                string sMenssagem = string.Format("Status do Servidor: {0}{1}Descrição: {2}{3}Tempo Médio de Resposta: {4} {5}",
                                              objnfeStatusServicoNF.Cstat,
                                              Environment.NewLine,
                                              objnfeStatusServicoNF.Xmotivo,
                                              Environment.NewLine,
                                              objnfeStatusServicoNF.Tmed,
                                              sTempo);

                if ((objnfeStatusServicoNF.Cstat == 107))
                {
                    KryptonMessageBox.Show(null, sMenssagem, "Status do Servidor da Secretaria da Fazenda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string sMensagemScan = Environment.NewLine + Environment.NewLine +
                                            "Detalhes: " +
                                            Environment.NewLine +
                                            "O Serviço da Sefaz do Estado de " + Uf_Empresa + " Está Inoperante no momento" +
                                            Environment.NewLine +
                                            "Para o Envio da NFe." +
                                             Environment.NewLine +
                                            "O Sistema deverá acessar o Servidor Federal (SCAN)." +
                                            Environment.NewLine +
                                            "Para que isso seja Possível, será necessário Ativar o Ambiente de SCAN nas Configurações e " +
                                            "escolher uma série para a NFe no Intervalo de 900 a 999;" +
                                            Environment.NewLine + Environment.NewLine +
                                            "Para Ativar o Ambiente de SCAN agora, pressione 'SIM'. Caso contrário, o sistema irá " +
                                            "verificar e informar no rodapé do Menu Principal quando o Servidor do Estado já estiver Online";

                    if (KryptonMessageBox.Show(null, sMenssagem + sMensagemScan, "Status do Servidor da Secretaria da Fazenda", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        frmConfiguracao objfrm = new frmConfiguracao(4);
                        objfrm.MdiParent = objfrmPrincipal;
                        objfrm.Show();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                new HLPexception(ex.Message +
                                         Environment.NewLine +
                                         "O Teste de Conexão com a Internet Falhou!! "
                                         + Environment.NewLine + Environment.NewLine
                                         + "O Sistema já está Configurado para Imprimir DANFE em Modo de Contingência."
                                         + Environment.NewLine, ex);
            }
        }

        private void btnBuscaRetFazenda_Click(object sender, EventArgs e)
        {
            //Instancia o objeto xml que vai carregar o xml certo
            XmlDocument xml = new XmlDocument();

            //variavel prox utilizada para parar de procurar o xml e ir para a proxima chave
            bool prox = false;

            //Variavel para formar a mesangem de resposta da fazenda das notas selecionadas na grid
            string Loteres = "";

            //Variavel que vai armazenar o select pra buscar a chavenfe, e o recibo.
            string select = "";

            //Objeto onde contarao as listas autorizadas
            List<string> NotasAutorizadas = new List<string>();

            try
            {

                for (int i = 0; i < dgvNF.RowCount; i++)
                {
                    if (dgvNF["ASSINANF", i].Value != null)
                    {
                        if (dgvNF["ASSINANF", i].Value.ToString().Equals("True"))
                        {
                            string sRetorno = VerificaCampoReciboPreenchido(belStatic.codEmpresaNFe, dgvNF["CD_NFSEQ", i].Value.ToString());
                            if (sRetorno == "denegada")
                            {
                                throw new Exception("A Nota de Sequencia = " + dgvNF["CD_NFSEQ", i].Value.ToString() + " foi Denegada pelo Sefaz e não podera ser excluída, cancelada e nem alterada.");
                            }
                        }
                    }
                }

                for (int i = 0; i < dgvNF.RowCount; i++)
                {
                    List<string> sChaves = new List<string>();
                    if ((dgvNF["ASSINANF", i].Value != null) && (dgvNF["ASSINANF", i].Value.ToString().Equals("True")) && (dgvNF["ST_NFE", i].Value != null))
                    {
                        select = "Select cd_chavenfe,cd_recibonfe from nf where cd_empresa = '"
                               + belStatic.codEmpresaNFe.Trim() + "' and CD_NFSEQ ='" + dgvNF["CD_NFSEQ", i].Value.ToString() + "'";

                        using (FbCommand cmd = new FbCommand(select, cx.get_Conexao()))
                        {
                            cx.Open_Conexao();
                            cmd.ExecuteNonQuery();


                            FbDataReader dr = cmd.ExecuteReader();
                            dr.Read();

                            //Pega o diretorio onde estão os xmls

                            DirectoryInfo dinfo = new DirectoryInfo(belStaticPastas.ENVIO + "\\" + dr["cd_chavenfe"].ToString().Substring(2, 4));
                            FileInfo[] finfo = dinfo.GetFiles();



                            foreach (var item in finfo)
                            {
                                //xml.Load(@item.FullName);
                                if (item.Name.Replace("-nfe.xml", "").Equals((dr["cd_chavenfe"].ToString())))
                                {
                                    //Carega o xml encontrado
                                    xml.Load(item.FullName);

                                    belBusRetFazenda objbuscaret = new belBusRetFazenda(
                                        dgvNF["CD_NFSEQ", i].Value.ToString(),
                                        xml.OuterXml.Remove(0, xml.OuterXml.IndexOf('>') + 1),
                                        dr["cd_recibonfe"].ToString(),
                                        dr["cd_chavenfe"].ToString(), cert, Uf_Empresa);

                                    GeraXml.NFe.frmBuscaRetorno objfrmAvisoRet = new GeraXml.NFe.frmBuscaRetorno(objbuscaret, GeraXml.NFe.frmBuscaRetorno.tipoBusca.RETORNO);
                                    objfrmAvisoRet.ShowDialog();

                                    //Concatena as Mensagens de cada nota de retorno da fazenda
                                    Loteres = Loteres + objbuscaret.Loteres + Environment.NewLine;

                                    //If responsavel por nao fazer o add se o valor da propriedade for nullo
                                    if (objbuscaret.NfeunicaAut != "")
                                    {
                                        NotasAutorizadas.Add(objbuscaret.NfeunicaAut);
                                    }

                                    prox = true;
                                }
                                if (prox == true)
                                {
                                    prox = false;
                                    break;
                                }
                            }
                        }
                    }
                }
                if (NotasAutorizadas.Count <= 0)
                {
                    KryptonMessageBox.Show("Nenhuma nota foi autorizada - " + Loteres);
                }
                //Danenr - o.s. 24030 - 20/01/2010
                else
                {
                    //Efetua os updates nas notas que realmente foram autorizadas
                    StringBuilder sSql = new StringBuilder();
                    foreach (string sNota in NotasAutorizadas) //Danner - o.s. SEM - 30/10/2009
                    {

                        if (sSql.ToString().Length > 0)
                        {
                            sSql.Append(string.Format(", '{0}'", sNota));
                        }
                        else
                        {
                            sSql.Append(string.Format("'{0}'", sNota));
                        }

                    }
                    sSql.Insert(0, "update nf set st_nfe = 'S' Where cd_nfseq in (");
                    sSql.Append(")");
                    sSql.Append("and ");
                    sSql.Append("cd_empresa = '");
                    sSql.Append(belStatic.codEmpresaNFe);
                    sSql.Append("'");

                    using (FbCommand cmdUpdate = new FbCommand(sSql.ToString(), cx.get_Conexao()))
                    {
                        cx.Open_Conexao();
                        cmdUpdate.ExecuteNonQuery();
                        cx.Close_Conexao();
                    }

                    KryptonMessageBox.Show("Arquivos Gerados" + Environment.NewLine + Environment.NewLine + Loteres);


                    for (int i = 0; i < dgvNF.RowCount; i++)
                    {
                        foreach (var item in NotasAutorizadas)
                        {
                            if (dgvNF["cd_nfseq", i].Value.ToString() == item)
                            {
                                if (dgvNF["ASSINANF", i].Value != null)
                                {
                                    dgvNF["ST_NFE", i].Value = true;
                                }
                            }
                        }
                    }
                    dgvNF.Refresh();
                }
            }
            catch (Exception x)
            {
                KryptonMessageBox.Show(x.Message);
            }
            finally { cx.Close_Conexao(); }

        }

        private void tsConsultaCadastro_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> sSeqs = new List<string>();

                for (int i = 0; i < dgvNF.RowCount; i++)
                {

                    if (dgvNF["ASSINANF", i].Value != null)
                    {
                        if ((bool)dgvNF["ASSINANF", i].Value != false)
                        {
                            sSeqs.Add(dgvNF["CD_NFSEQ", i].Value.ToString());
                        }
                    }
                }
                if (sSeqs.Count > 1)
                {
                    KryptonMessageBox.Show(null, "Só é possivel pesquisar um Contribuinte de cada Vez ", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (sSeqs.Count <= 0)
                {
                    KryptonMessageBox.Show(null, "Nenhuma nota foi selecionada!! - ", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append("SELECT clifor.cd_cgc, clifor.cd_insest , clifor.cd_cpf , clifor.cd_ufnor, empresa.cd_ufnor UFEMP from ");
                    sQuery.Append(" nf inner join clifor on nf.cd_clifor = clifor.cd_clifor ");
                    sQuery.Append(" inner join empresa on nf.cd_empresa = empresa.cd_empresa ");
                    sQuery.Append("where nf.cd_empresa = '");
                    sQuery.Append(belStatic.codEmpresaNFe + "' and nf.cd_nfseq = '");
                    sQuery.Append(sSeqs[0] + "'");

                    FbCommand cmb = new FbCommand(sQuery.ToString(), cx.get_Conexao());
                    cx.Open_Conexao();

                    FbDataReader dr = cmb.ExecuteReader();

                    string sUFEMP = "";
                    string sUF = "";
                    string sIE = "";
                    string sCNPJ = "";
                    string sCPF = "";


                    while (dr.Read())
                    {
                        sUF = dr["cd_ufnor"].ToString();
                        sIE = dr["cd_insest"].ToString();
                        sCNPJ = dr["cd_cgc"].ToString();
                        sCPF = dr["cd_cpf"].ToString();
                        sUFEMP = dr["UFEMP"].ToString();
                    }
                    belConsultaCadastro objConsCad = new belConsultaCadastro(sUFEMP, sUF, sIE, sCNPJ, sCPF, cert);

                    string bla = objConsCad.ConsultaCadastro();
                    KryptonMessageBox.Show(null, bla, "Consulta Cadastro Contribuinte.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(ex.Message);
            }
            finally { cx.Close_Conexao(); }
        }

        private void btnConsultaNF_Click(object sender, EventArgs e)
        {
            List<string> sNota = new List<string>();
            List<string> sNumero = new List<string>();
            string Mensagem = "";

            for (int i = 0; i < dgvNF.RowCount; i++)
            {
                if (dgvNF["ASSINANF", i].Value != null)
                {
                    if ((bool)dgvNF["ASSINANF", i].Value != false)
                    {
                        if (dgvNF["ST_NFE", i].Value.ToString().Equals("0"))
                        {
                            KryptonMessageBox.Show(null, "Nota Fiscal ainda não liberada para consulta de Status!!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            sNota.Add(dgvNF["CD_NFSEQ", i].Value.ToString());
                            sNumero.Add(dgvNF["CD_NOTAFIS", i].Value.ToString());
                        }
                    }
                }
            }
            if (sNota.Count > 1)
            {
                KryptonMessageBox.Show(null, "Só é possivel pesquisar uma nota de uma vez - ", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (sNota.Count == 1)
            {

                string sChave = buscaChNFe(belStatic.codEmpresaNFe, sNota[0]);
                belNfeConsultaNF objnfeconsultanf = new belNfeConsultaNF("1.02", "2.00", sChave, "CONSULTAR");
                XmlDocument xmlret = new XmlDocument();
                try
                {
                    if ((belStatic.bModoSCAN) && (belStatic.iStatusAtualSistema == 3))
                    {
                        xmlret.LoadXml(objnfeconsultanf.buscaRetornoSCAN(Uf_Empresa));
                    }
                    else
                    {
                        xmlret.LoadXml(objnfeconsultanf.buscaRetorno(Uf_Empresa));
                    }
                    if ((xmlret.GetElementsByTagName("cStat")[0].InnerText == "100") ||
                        (xmlret.GetElementsByTagName("cStat")[0].InnerText == "101") ||
                        (xmlret.GetElementsByTagName("cStat")[0].InnerText == "110"))
                    {

                        //Criado para separar a Data da Hora retirando o T, assim podendo formatar o formato da data.
                        string[] split = xmlret.GetElementsByTagName("dhRecbto")[0].InnerText.Split('T');

                        string datahoranfe = "";

                        int i = 0;
                        foreach (var item in split)
                        {
                            i++;
                            if (i == 1)
                            {
                                datahoranfe = datahoranfe + Convert.ToDateTime(item).ToString("dd-MM-yyyy") + " ";
                            }
                            else
                            {
                                datahoranfe = datahoranfe + item;
                            }

                        }

                        Mensagem = Mensagem + "Nota Fiscal Eletronica de Sequência: " + sNota[0] + Environment.NewLine +
                                              "Número da Nota: " + sNumero[0] + Environment.NewLine + Environment.NewLine +
                                              "Cód. do Status - " + xmlret.GetElementsByTagName("cStat")[0].InnerText + Environment.NewLine +
                                              "Status - " + xmlret.GetElementsByTagName("xMotivo")[0].InnerText + Environment.NewLine +
                                              "Chave de Acesso - " + xmlret.GetElementsByTagName("chNFe")[0].InnerText + Environment.NewLine +
                                              "Data do Recebimento - " + datahoranfe + Environment.NewLine +
                                              "Número do Protocolo - " + xmlret.GetElementsByTagName("nProt")[0].InnerText + Environment.NewLine +
                                              "Valor do Digest - " + xmlret.GetElementsByTagName("digVal")[0].InnerText;
                    }

                }
                catch (XmlException x)
                {

                    KryptonMessageBox.Show(null, "Erro ao carregar o xml ou ao geral a mensagem - " + x.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception x)
                {
                    KryptonMessageBox.Show(null, "Erro - " + x.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //Danner - o.s. 24152 - 18/02/2010
                if (Mensagem == "")
                {
                    Mensagem = "Nota não existe na sefaz ou WebService não funcionando normalmente";
                }

                KryptonMessageBox.Show(Mensagem);
                dgvNF.Refresh();
            }
        }

        public struct stEmails
        {
            public string scaminho { get; set; }
            public bool bemail { get; set; }
            public string sdest { get; set; }
            public string semail { get; set; }
        }

        #endregion

        #region Métodos

        private void CancelaNFe()
        {
            List<string> sNotacanc = new List<string>();

            try
            {

                for (int i = 0; i < dgvNF.RowCount; i++)
                {
                    if ((((dgvNF["cd_recibocanc", i].Value.ToString() == "") && (dgvNF["ASSINANF", i].Value != null) && dgvNF["ASSINANF", i].Value.ToString().Equals("True")) && (dgvNF["ST_NFE", i].Value.ToString() != "0")))//&& (dgvNF["CANCELADA", i].Value.ToString() != "0")))
                    {
                        sNotacanc.Add((string)dgvNF["CD_NFSEQ", i].Value);
                    }
                }
                if (sNotacanc.Count == 1)
                {
                    frmCancJustNfe frmcanjust = new frmCancJustNfe(sNotacanc, belStatic.codEmpresaNFe, Uf_Empresa, this);
                    frmcanjust.ShowDialog();
                    if ((frmcanjust.status == "101"))
                    {
                        if (KryptonMessageBox.Show(null, "Nota Cancelada com Sucesso !!"
                                     + Environment.NewLine
                                     + Environment.NewLine
                                     + "Deseja enviar email informando o Cancelamento ao Destinatário?",
                                     "Cancelamento", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            EnviaEmailCancelamento(sNotacanc[0]);
                        }
                        for (int i = 0; i < dgvNF.RowCount; i++)
                        {
                            if (dgvNF["CD_NFSEQ", i].Value.ToString().Equals(sNotacanc[0]))
                            {
                                dgvNF.Rows[i].DefaultCellStyle.BackColor = Color.Khaki;
                            }
                        }
                    }
                }
                else if (sNotacanc.Count > 1)
                {
                    KryptonMessageBox.Show(null, "Não é possível cancelar varias notas de uma só vez.!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    KryptonMessageBox.Show(null, "Nenhuma Nota válida foi selecionada para o Cancelamento.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception x)
            {
                KryptonMessageBox.Show(null, "Erro ao Cancelar a nota - " + x.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EnviaEmailCancelamento(string sSeq) //NFe_2.0
        {
            string hostservidor = LeRegWin.LeRegConfig("HostServidor").ToString().Trim();
            string porta = LeRegWin.LeRegConfig("PortaServidor").ToString().Trim();
            string remetente = LeRegWin.LeRegConfig("EmailRemetente").ToString().Trim();
            string senha = LeRegWin.LeRegConfig("SenhaRemetente").ToString().Trim();
            bool autentica = Convert.ToBoolean(LeRegWin.LeRegConfig("RequerSSL").ToString().Trim());
            List<belEmail> objlbelEmail = new List<belEmail>();
            if ((hostservidor != "") && (porta != "0") && (remetente != "") && (senha != ""))
            {
                belEmail objemail = new belEmail(sSeq, belStatic.codEmpresaNFe, hostservidor, porta, remetente, senha, "", autentica);
                objlbelEmail.Add(objemail);
            }
            else
            {
                if (KryptonMessageBox.Show(null, "Campos para o envio de e-Mail automático não estão preenchidos corretamente!" +
                                Environment.NewLine + Environment.NewLine +
                                "Deseja Preencher os campos corretamente agora ?", "E-Mail não pode ser enviado", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    frmConfiguracao objconfiguracao = new frmConfiguracao(2);
                    objconfiguracao.ShowDialog();
                }
            }
            if (objlbelEmail.Count > 0)
            {
                frmEmailNfe objfrmEmail = new frmEmailNfe(objlbelEmail);
                objfrmEmail.sTipo = "C";
                objfrmEmail.ShowDialog();
                for (int i = 0; i < objfrmEmail.objLbelEmail.Count; i++)
                {
                    if ((objfrmEmail.objLbelEmail[i]._envia == true) && (objfrmEmail.objLbelEmail[i]._para != "" || objfrmEmail.objLbelEmail[i]._paraTransp != ""))
                    {
                        try
                        {
                            objfrmEmail.objLbelEmail[i].enviaEmail();
                            KryptonMessageBox.Show(null, "Procedimento de Envio de E-mail Finalizado!"
                            + Environment.NewLine
                            + Environment.NewLine,
                            "A V I S O",
                             MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            KryptonMessageBox.Show(null, ex.Message + Environment.NewLine + Environment.NewLine + "E-mail: " + objfrmEmail.objLbelEmail[i]._para + "   - Seq: " + objfrmEmail.objLbelEmail[i]._sSeq, "E R R O - E N V I O", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }

        }

        private void EnviaEmail(List<string> lCaminhosXml, List<belIde> objlbelIde)
        {
            string hostservidor = LeRegWin.LeRegConfig("HostServidor").ToString().Trim();
            string porta = LeRegWin.LeRegConfig("PortaServidor").ToString().Trim();
            string remetente = LeRegWin.LeRegConfig("EmailRemetente").ToString().Trim();
            string senha = LeRegWin.LeRegConfig("SenhaRemetente").ToString().Trim();
            bool autentica = Convert.ToBoolean(LeRegWin.LeRegConfig("RequerSSL").ToString().Trim());

            List<belEmail> objlbelEmail = new List<belEmail>();

            //OS_25285
            for (int i = 0; i < lCaminhosXml.Count; i++)
            {

                if ((hostservidor != "") && (porta != "0") && (remetente != "") && (senha != ""))
                {
                    for (int e = 0; e < objlbelIde.Count; e++)
                    {
                        if (lCaminhosXml[i].Substring(lCaminhosXml[i].IndexOf("-nfe") - 7, 6).Equals(objlbelIde[e].Nnf))
                        {
                            InformaStatusEnvio("Estruturando Email", i, lCaminhosXml.Count);
                            belEmail objemail = new belEmail(objlbelIde[e].Nnf, objlbelIde[e].Cnf, belStatic.codEmpresaNFe, hostservidor, porta, remetente, senha, @lCaminhosXml[i], "", autentica);
                            objlbelEmail.Add(objemail);
                        }
                    }
                }
                else
                {
                    if (KryptonMessageBox.Show(null, "Campos para o envio de e-Mail automático não estão preenchidos corretamente!" +
                                    Environment.NewLine + Environment.NewLine +
                                    "Deseja Preencher os campos corretamente agora ?", "E-Mail não pode ser enviado", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        frmConfiguracao objconfiguracao = new frmConfiguracao(2);
                        objconfiguracao.ShowDialog();
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (objlbelEmail.Count > 0)
            {
                frmEmailNfe objfrmEmail = new frmEmailNfe(objlbelEmail);
                objfrmEmail.ShowDialog();
                int icount = 0;
                for (int i = 0; i < objfrmEmail.objLbelEmail.Count; i++)
                {
                    if ((objfrmEmail.objLbelEmail[i]._envia == true) && (objfrmEmail.objLbelEmail[i]._para != "" || objfrmEmail.objLbelEmail[i]._paraTransp != ""))
                    {
                        try
                        {
                            InformaStatusEnvio("Enviando Email", i, lCaminhosXml.Count);
                            objfrmEmail.objLbelEmail[i].enviaEmail();
                            icount++;
                        }
                        catch (Exception ex)
                        {
                            KryptonMessageBox.Show(null, ex.Message + Environment.NewLine + Environment.NewLine + "E-mail: " + objfrmEmail.objLbelEmail[i]._para + "   - Seq: " + objfrmEmail.objLbelEmail[i]._sSeq, "E R R O - E N V I O", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                if (icount > 0)
                {
                    KryptonMessageBox.Show(null, "Procedimento de Envio de E-mail Finalizado!"
                           + Environment.NewLine
                           + Environment.NewLine,
                           "A V I S O",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        private void InformaStatusEnvio(string sMessage, int i, int iCount)
        {
            sslStatusEnvio.Text = sMessage + "( " + (i + 1).ToString() + " de " + iCount.ToString() + " )";
            statusStrip1.Refresh();
        }

        private void MarcadesmarcaTodos(bool Marca, int coluna)
        {
            for (int i = 0; i < dgvNF.RowCount; i++)
            {
                dgvNF.Rows[i].Cells[coluna].Value = Marca;
            }
        }

        public void PopulaDataGridView()
        {
            sslStatusEnvio.Text = "";
            StringBuilder strCampos = new StringBuilder();
            strCampos.Append("nf.cd_notafis, ");
            strCampos.Append("nf.cd_nfseq, ");
            strCampos.Append("nf.dt_emi, ");
            strCampos.Append("nf.nm_guerra, ");
            strCampos.Append("nf.vl_totnf, ");
            strCampos.Append("coalesce(nf.st_contingencia,'N') as st_contingencia ,");
            strCampos.Append("cast(case when nf.st_nfe = 'S' then ");
            strCampos.Append("1 ");
            strCampos.Append("else ");
            strCampos.Append("0 ");
            strCampos.Append("end as smallint) st_nfe, ");
            strCampos.Append("cast(case when coalesce(nf.st_cannf, 'N') ='C' then 1 else 0 end as smallint) Cancelada ");
            strCampos.Append(" , coalesce(nf.cd_recibocanc,'') cd_recibocanc");//24752


            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" ((nf.cd_empresa = '");
            strWhere.Append(belStatic.codEmpresaNFe.Trim());
            strWhere.Append("')");
            if (rdbData.Checked)
            {
                strWhere.Append(" and ");
                strWhere.Append(" (nf.dt_emi between '");
                strWhere.Append(dtpIni.Value.ToString("dd.MM.yyyy"));
                strWhere.Append("' and '");
                strWhere.Append(dtpFim.Value.ToString("dd.MM.yyyy"));
                strWhere.Append("')");
            }
            else
            {
                strWhere.Append(" and ");
                strWhere.Append(" (nf.cd_nfseq between '");
                strWhere.Append(txtNfIni.Text.ToString());
                strWhere.Append("' and '");
                strWhere.Append(txtNfFim.Text.ToString());
                strWhere.Append("')");
            }
            strWhere.Append(")");


            if (rbtNaoEnviadas.Checked == true)
            {
                strWhere.Append(" and ");
                strWhere.Append("(nf.st_nfe = 'N' or nf.st_nfe is null) ");
            }
            if (rbtEnviadas.Checked == true)
            {
                strWhere.Append(" and ");
                strWhere.Append("(nf.st_nfe = 'S') ");
            }
            // Diego - OS_24688 - 21/07/10
            strWhere.Append(" and ");
            strWhere.Append("(coalesce(nf.st_nf_prod,'S') = 'S') ");
            // Diego - OS_24688 - 21/07/10 - FIM

            belGerarXML BuscaDados = new belGerarXML();
            BuscaDados.SetTabelaSelect("NF");
            BuscaDados.SetWhereSelect(strWhere);
            BuscaDados.SetOrderSelect(" nf.cd_notafis ");
            BuscaDados.SetCamposSelect(strCampos);
            StringBuilder strRelacionamento = new StringBuilder();
            strRelacionamento.Append("");
            BuscaDados.SetInnerSelec(strRelacionamento);
            dgvNF.DataSource = BuscaDados.BuscaDadosNF();
            dgvNF.Refresh();


            //Diego - OS - 23723 - 11/03/2009
            for (int i = 0; i < dgvNF.RowCount; i++)
            {
                dgvNF["Imprime", i].Value = false;
                dgvNF["ASSINANF", i].Value = false;

                if ((dgvNF["st_contingencia", i].Value.ToString().Equals("S"))
                            && (dgvNF["ST_NFE", i].Value.ToString().Equals("0"))
                            && (dgvNF["CD_NOTAFIS", i].Value.ToString() != ""))
                {
                    dgvNF.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
                else if ((dgvNF["st_contingencia", i].Value.ToString().Equals("S"))
                            && (dgvNF["CD_NOTAFIS", i].Value.ToString() != "")
                            && (dgvNF["ST_NFE", i].Value.ToString().Equals("1")))
                {
                    dgvNF.Rows[i].DefaultCellStyle.BackColor = Color.Aquamarine;
                }
                if (dgvNF["cd_recibocanc", i].Value.ToString() != "")
                {
                    dgvNF.Rows[i].DefaultCellStyle.BackColor = Color.Khaki;
                }
            }
            //Fim - Diego - OS - 23723 - 11/03/2009
        }

        public void PopulaDataGridViewPendenciaContingencia()
        {
            StringBuilder strCampos = new StringBuilder();
            strCampos.Append("nf.cd_notafis, ");
            strCampos.Append("nf.cd_nfseq, ");
            strCampos.Append("nf.dt_emi, ");
            strCampos.Append("nf.nm_guerra, ");
            strCampos.Append("nf.vl_totnf, ");
            strCampos.Append("coalesce(nf.st_contingencia,'N') as st_contingencia ,");
            strCampos.Append("cast(case when nf.st_nfe = 'S' then ");
            strCampos.Append("1 ");
            strCampos.Append("else ");
            strCampos.Append("0 ");
            strCampos.Append("end as smallint) st_nfe, ");
            strCampos.Append("cast(case when coalesce(nf.st_cannf, 'N') ='C' then 1 else 0 end as smallint) Cancelada ");
            strCampos.Append(" , coalesce(nf.cd_recibocanc,'') cd_recibocanc");//24752


            StringBuilder strWhere = new StringBuilder();
            strWhere.Append("nf.cd_empresa ='");
            strWhere.Append(belStatic.codEmpresaNFe);
            strWhere.Append("'");
            strWhere.Append(" and nf.st_contingencia = 'S'");
            strWhere.Append(" and ((nf.st_nfe = 'N') or (nf.st_nfe is null))");
            strWhere.Append(" and nf.cd_notafis is not null");
            // Diego - OS_24688 - 21/07/10
            strWhere.Append(" and ");
            strWhere.Append("(coalesce(nf.st_nf_prod,'S') = 'S') ");
            // Diego - OS_24688 - 21/07/10 - FIM

            belGerarXML BuscaDados = new belGerarXML();
            BuscaDados.SetTabelaSelect("NF");
            BuscaDados.SetWhereSelect(strWhere);
            BuscaDados.SetOrderSelect(" nf.cd_notafis ");
            BuscaDados.SetCamposSelect(strCampos);
            StringBuilder strRelacionamento = new StringBuilder();
            strRelacionamento.Append("");
            BuscaDados.SetInnerSelec(strRelacionamento);
            dgvNF.DataSource = BuscaDados.BuscaDadosNF();
            dgvNF.Refresh();


            //Diego - OS - 23723 - 11/03/2009
            for (int i = 0; i < dgvNF.RowCount; i++)
            {
                dgvNF.Rows[i].DefaultCellStyle.BackColor = Color.Red;
            }
            //Fim - Diego - OS - 23723 - 11/03/2009
        }

        private void PopulaDataSetXML(dsDanfe dsdanfe, string caminho, string codigo)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(@caminho);

            int ihoraImpDanfe = (LeRegWin.LeRegConfig("VisualizaHoraDanfe") == "True" ? 1 : 0);
            int idataImpDanfe = (LeRegWin.LeRegConfig("VisualizaDataDanfe") == "True" ? 1 : 0);

            PopulaDs populads = new PopulaDs();

            populads.populaTagInfNFe(dsdanfe, xml, codigo, ihoraImpDanfe, idataImpDanfe);
            populads.populaTagIDE(dsdanfe, xml, codigo);
            populads.populaTagEmit(dsdanfe, xml, codigo);
            populads.populaTagDest(dsdanfe, xml, codigo);
            populads.populaTagdet(dsdanfe, xml, codigo);
            populads.populaTagTotal(dsdanfe, xml, codigo);
            populads.PopulaTagTransp(dsdanfe, xml, codigo);
            populads.PopulaTagCobr(dsdanfe, xml, codigo);
            populads.PopulaTagInfAdic(dsdanfe, xml, codigo);
            populads.PopulaTagExporta(dsdanfe, xml, codigo);
            populads.PopulaTagCompra(dsdanfe, xml, codigo);
            populads.PopulaTagEntrega(dsdanfe, xml, codigo);
            populads.PopulaTagRetirada(dsdanfe, xml, codigo);
            populads.PopulaTagInfProt(dsdanfe, xml, codigo);

            if ((LeRegWin.LeRegConfig("Logotipo") != "\r\n"))
            {
                Byte[] bimagem = belUtil.carregaImagem(LeRegWin.LeRegConfig("Logotipo"));

                if (bimagem != null)
                {
                    if (bimagem.Length <= 100000)
                    {
                        populads.PopulaImagem(dsdanfe, bimagem);
                    }
                    else
                    {
                        KryptonMessageBox.Show(null, "Logotipo muito Grande, Favor reduzir os para menos de 100KB"
                            + Environment.NewLine + Environment.NewLine
                            + "A Danfe não sairá com logotipo néssa visualização!", "A V I S O", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void CarregaConfiguracoesIniciais(string sEmp)
        {
            try
            {


                string sRetorno = string.Empty;

                StringBuilder sSql = new StringBuilder();
                sSql.Append("Select ");
                sSql.Append("empresa.nm_empresa, ");
                sSql.Append("empresa.cd_ufnor, "); //Danner - o.s. 23984 - 07/01/2010
                sSql.Append("uf.nr_ufnfe cd_ufnor, ");
                sSql.Append("coalesce(empresa.st_ambiente, '2') tpAmb ");
                sSql.Append("from empresa ");
                sSql.Append("left join uf on (uf.cd_uf = empresa.cd_ufnor) ");
                sSql.Append("where ");
                sSql.Append("cd_empresa ='");
                sSql.Append(sEmp);
                sSql.Append("'");

                using (FbCommand cmd = new FbCommand(sSql.ToString(), cx.get_Conexao()))
                {
                    cx.Open_Conexao();
                    cmd.ExecuteNonQuery();

                    FbDataReader dr = cmd.ExecuteReader();
                    dr.Read();

                    this.cd_ufnor = dr["cd_ufnor"].ToString();
                    belStatic.tpAmb = Convert.ToInt16(dr["tpAmb"]);

                    sRetorno = string.Format("Sistema Manager XML - MAGNIFICUS - {0}",
                                              dr["nm_empresa"].ToString());
                    this.Uf_Empresa = dr["cd_ufnor"].ToString();

                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { cx.Close_Conexao(); }
        }

        public void gravaRecibo(string sRecibo, string sEmp, string sSeq)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {

                sSql.Append("update nf ");
                sSql.Append("set cd_recibonfe ='");
                sSql.Append(sRecibo);
                sSql.Append("' ");
                sSql.Append("where ");
                sSql.Append("cd_empresa ='");
                sSql.Append(sEmp);
                sSql.Append("' ");
                sSql.Append("and ");
                sSql.Append("cd_nfseq ='");
                sSql.Append(sSeq);
                sSql.Append("'");
                sSql.Append(" and coalesce(cd_recibonfe, '') = ''");


                using (FbCommand cmdUpdate = new FbCommand(sSql.ToString(), cx.get_Conexao()))
                {
                    cx.Open_Conexao();
                    cmdUpdate.ExecuteNonQuery();
                }


            }
            catch (Exception)
            {
                throw;
            }
            finally { cx.Close_Conexao(); }
        }

        private string buscaChNFe(string sEmp, string sSeq)
        {
            StringBuilder sSql = new StringBuilder();
            string chavenfe = string.Empty;
            try
            {
                sSql.Append("select ");
                sSql.Append("cd_chavenfe ");
                sSql.Append("from nf ");
                sSql.Append("where ");
                sSql.Append("cd_empresa ='");
                sSql.Append(sEmp);
                sSql.Append("' ");
                sSql.Append("and ");
                sSql.Append("cd_nfseq ='");
                sSql.Append(sSeq);
                sSql.Append("'");

                FbCommand cmd = new FbCommand(sSql.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                cmd.ExecuteNonQuery();
                FbDataReader dr = cmd.ExecuteReader();
                dr.Read();
                chavenfe = dr["cd_chavenfe"].ToString();
            }
            catch (Exception x)
            {
                throw new Exception("Erro no buscaChNFe - " + x.Message);
            }
            finally { cx.Close_Conexao(); }
            return chavenfe;
        }

        public void LimparPasta(DirectoryInfo pasta)
        {
            // Obtém a lista de arquivos dessa subpasta
            var arquivos = pasta.GetFiles();

            // Percorre a lista de arquivos da subpasta
            foreach (var a in arquivos)
            {
                // O arquivo está marcado como ReadOnly?
                if ((a.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    // Sim! Então remove esse atributo...
                    a.Attributes ^= FileAttributes.ReadOnly;
                }
                // Apaga o arquivo
                if (belFecharJanela.IsFileOpen(a.FullName) == false)
                {
                    a.Delete();
                }
            }
        }

        private void VerificaStatusSefaz()
        {
            try
            {
                InternetCS objVerificaInternet = new InternetCS();

                if (objVerificaInternet.Conexao())
                {

                    objnfeStatusServicoNF = new belnfeStatusServicoNF("2.00", this.cd_ufnor, cert, Uf_Empresa);
                    string sTempo = string.Empty;
                    if (objnfeStatusServicoNF.Tmed > 1)
                    {
                        sTempo = "Segundos";
                    }
                    else
                    {
                        sTempo = "Segundo";
                    }
                    string sMenssagem = string.Format("Status do servidor: {0} - {1} " + " - Layout: {2}",
                                                          objnfeStatusServicoNF.Cstat,
                                                          objnfeStatusServicoNF.Xmotivo,
                                                          objnfeStatusServicoNF.Versao
                                                          );

                    if ((objnfeStatusServicoNF.Cstat == 108) || (objnfeStatusServicoNF.Cstat == 109) || (objnfeStatusServicoNF.iTentativasWebServices == 1)) // Serviço do Estado Inoperante
                    {
                        #region Tratamento SCAN
                        if (belStatic.iStatusAtualSistema != 3)
                        {
                            belStatic.iStatusAtualSistema = 3;
                            if (belStatic.bModoSCAN)
                            {
                                KryptonMessageBox.Show(null, "Ocorreu uma Excessão com o Serviço do SEFAZ." +
                                        Environment.NewLine +
                                        sMenssagem +
                                        Environment.NewLine +
                                        Environment.NewLine +
                                        "O Sistema já está configurado para Enviar Notas no Modo Contingencia SCAN" +
                                        Environment.NewLine +
                                        Environment.NewLine +
                                        "Série SCAN : " + belStatic.iSerieSCAN.ToString(), "I N F O R M A Ç Ã O", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                lblStatusContingencia.Text = "Série SCAN: " + belStatic.iSerieSCAN.ToString();
                                objfrmPrincipal.sStatusSefaz = "Sistema em Modo de Contingência ( SCAN )";
                            }
                            else
                            {
                                KryptonMessageBox.Show(null, "Ocorreu uma Excessão com o Serviço do SEFAZ." +
                                        Environment.NewLine +
                                        sMenssagem +
                                        Environment.NewLine +
                                        Environment.NewLine +
                                        "O Sistema não está Configurado Para Enviar Notas no Modo Contingência SCAN " +
                                        Environment.NewLine +
                                        "E O GeraXML não será Inicializado!" +
                                        Environment.NewLine, "I N F O R M A Ç Ã O", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                timerWebServices.Enabled = false;
                                frmConfiguracao objfrm = new frmConfiguracao(4);
                                objfrm.ShowDialog();
                                Inicializacao();
                                Object sender = new object();
                                EventArgs e = new EventArgs();
                                this.frmArquivosXml_Load(sender, e);
                            }
                        }
                        #endregion
                    }
                    else if ((objnfeStatusServicoNF.Cstat == 107))
                    {

                        if (belStatic.iStatusAtualSistema != 1)
                        {
                            if (belStatic.bModoSCAN == false)
                            {

                                string sMensg = string.Format("Verificação Web Service: {0}{1}" + "Status do servidor: {2}{3}Motivo: {4}",
                                                              Environment.NewLine,
                                                              Environment.NewLine,
                                                              objnfeStatusServicoNF.Cstat,
                                                              Environment.NewLine,
                                                              objnfeStatusServicoNF.Xmotivo);

                                KryptonMessageBox.Show(null, sMensg +
                                            Environment.NewLine, "I N F O R M A Ç Ã O", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                if (belStatic.iStatusAtualSistema != 0)
                                {
                                    Inicializacao();
                                    Object sender = new object();
                                    EventArgs e = new EventArgs();
                                    this.frmArquivosXml_Load(sender, e);
                                }
                                this.objfrmPrincipal.sStatusSefaz = sMenssagem;
                                lblStatusContingencia.Text = "";
                                VerificaNotasPendentesEnvio();
                                belStatic.iStatusAtualSistema = 1;
                            }
                            else
                            {
                                belStatic.iStatusAtualSistema = 3;
                                sFormaEmissao = "3";
                                string sMensg = string.Format("Verificação Web Service. :{0}{1}" + "Status do servidor: {2}{3}Motivo: {4}",
                                                              Environment.NewLine,
                                                              Environment.NewLine,
                                                              objnfeStatusServicoNF.Cstat,
                                                              Environment.NewLine,
                                                              objnfeStatusServicoNF.Xmotivo
                                                              + Environment.NewLine + Environment.NewLine
                                                              + "Sistema configurado para Emitir NF-e em Contingência SCAN !");
                                KryptonMessageBox.Show(null, sMensg, "I N F O R M A Ç Ã O", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.objfrmPrincipal.sStatusSefaz = sMenssagem;
                                lblStatusContingencia.Text = "";
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception("A internet parece estar indisponível !");
                }
            }
            catch (HLPexception hlp)
            {
            }
            catch (Exception ex)
            {
                FalhaInternet(ex);
            }
        }

        private void FalhaInternet(Exception ex)
        {
            this.objfrmPrincipal.sStatusSefaz = "A internet parece estar indisponível !";
            if (belStatic.bModoContingencia)
            {
                lblStatusContingencia.Text = "Sistema Em Modo de Contingência !";
            }
            if (belStatic.iStatusAtualSistema != 2)
            {
                belStatic.iStatusAtualSistema = 2;
                if (belStatic.bModoContingencia && belStatic.iStatusAtualSistema == 2)
                {
                    HabilitaBotoes(false);
                    KryptonMessageBox.Show(null, ex.Message +
                                       Environment.NewLine +
                                       "O Teste de Conexão com a Internet Falhou!! "
                                       + Environment.NewLine + Environment.NewLine
                                       + "O Sistema já está Configurado para Imprimir DANFE em Modo de Contingência."
                                       + Environment.NewLine,
                                       "FALHA DE CONEXÃO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.objfrmPrincipal.sStatusSefaz = "A internet parece estar indisponível !";
                    lblStatusContingencia.Text = "Sistema Em Modo de Contingência !";
                }
                else
                {
                    if (objfrmInicializacao != null)
                    {
                        objfrmInicializacao.Close();
                    }
                    KryptonMessageBox.Show(null, ex.Message +
                                       Environment.NewLine +
                                       "O Teste de Conexão com a Internet Falhou!! "
                                       + Environment.NewLine + Environment.NewLine
                                       + "O Sistema não está Configurado para entrar em Modo de Contingência."
                                       + Environment.NewLine
                                       + "O GeraXml não será Iniciado",
                                       "FALHA DE CONEXÃO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmConfiguracao objfrm = new frmConfiguracao(4);
                    objfrm.ShowDialog();
                    Inicializacao();
                    Object sender = new object();
                    EventArgs e = new EventArgs();
                    this.frmArquivosXml_Load(sender, e);
                }
            }
        }

        private void HabilitaBotoes(bool bHabilita)
        {
            btnEnviar.Visible = bHabilita;
            btnCancelanebto.Visible = bHabilita;
            btnInutilizacao.Visible = bHabilita;
            btnBuscaRetorno.Visible = bHabilita;
            btnSituacaoNFe.Visible = bHabilita;
            btnGerarContingencia.Visible = !bHabilita;
        }

        private void VerificaNotasPendentesEnvio()
        {
            try
            {
                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("SELECT cd_nfseq, cd_notafis from nf ");
                sQuery.Append("Where nf.cd_empresa ='");
                sQuery.Append(belStatic.codEmpresaNFe);
                sQuery.Append("'");
                sQuery.Append(" and nf.st_contingencia = 'S'");
                sQuery.Append(" and ((nf.st_nfe = 'N') or (nf.st_nfe is null))");
                sQuery.Append(" and nf.cd_notafis is not null");

                using (FbCommand cmdSelect = new FbCommand(sQuery.ToString(), cx.get_Conexao()))
                {
                    cx.Open_Conexao();
                    cmdSelect.ExecuteNonQuery();
                    FbDataReader drPendencias = cmdSelect.ExecuteReader();
                    String sMessage = " * Existem Notas Pendentes de Envio *" + Environment.NewLine +
                            "É essencial Enviá-las !!";
                    string sNotas = string.Empty;
                    while (drPendencias.Read())
                    {
                        kryptonPanel3.Visible = true;
                        errorProvider1.SetError(lblAvisoContingencia, "NOTAS PENDENTES DE ENVIO !");
                        sNotas += "NF: " + drPendencias["cd_notafis"].ToString() + Environment.NewLine;
                        lNotasPendentes.Add(drPendencias["cd_notafis"].ToString());
                    }
                    if (sNotas != "")
                    {
                        errorProvider1.SetError(lblAvisoContingencia, "NOTAS PENDENTES DE ENVIO !");
                        txtMensagemContingencia.Text = sMessage + Environment.NewLine + sNotas;
                    }
                    else
                    {
                        kryptonPanel3.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { cx.Close_Conexao(); }
        }

        public string VerificaCampoReciboPreenchido(string sEmp, string sSeq)
        {
            try
            {
                string sValidaRecibo = "";
                StringBuilder sQueryVerifica = new StringBuilder();
                sQueryVerifica.Append("Select cd_recibonfe from nf ");
                sQueryVerifica.Append("where ");
                sQueryVerifica.Append("cd_empresa ='");
                sQueryVerifica.Append(sEmp);
                sQueryVerifica.Append("' ");
                sQueryVerifica.Append("and ");
                sQueryVerifica.Append("cd_nfseq ='");
                sQueryVerifica.Append(sSeq);
                sQueryVerifica.Append("'");


                using (FbCommand cmdSelect = new FbCommand(sQueryVerifica.ToString(), cx.get_Conexao()))
                {
                    cx.Open_Conexao();
                    FbDataReader dr = cmdSelect.ExecuteReader();
                    while (dr.Read())
                    {
                        sValidaRecibo = dr["cd_recibonfe"].ToString();
                    }
                }

                return sValidaRecibo;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { cx.Close_Conexao(); }

        }

        private bool ValidaSeqNoBanco()
        {
            try
            {
                StringBuilder sSqlSeqValidas = new StringBuilder();
                sSqlSeqValidas.Append("select ");
                sSqlSeqValidas.Append("nf.cd_nfseq ");
                sSqlSeqValidas.Append("From nf ");
                sSqlSeqValidas.Append("where ");
                sSqlSeqValidas.Append("((nf.cd_notafis is null) or (nf.cd_notafis = '')) and (");
                sSqlSeqValidas.Append("nf.cd_empresa ='");
                sSqlSeqValidas.Append(belStatic.codEmpresaNFe);
                sSqlSeqValidas.Append("') and (");
                sSqlSeqValidas.Append("nf.cd_nfseq in('");
                int iCont = 0;
                foreach (var sNfseq in sNotas)
                {
                    iCont++;
                    sSqlSeqValidas.Append(sNfseq);
                    if (sNotas.Count > iCont)
                    {
                        sSqlSeqValidas.Append("','");
                    }
                }
                sSqlSeqValidas.Append("')) ");
                sSqlSeqValidas.Append("Order by nf.cd_empresa, nf.cd_nfseq ");

                FbCommand cmd = new FbCommand(sSqlSeqValidas.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                cmd.ExecuteNonQuery();

                FbDataReader dr = cmd.ExecuteReader();

                List<string> lsNFSeqValidos = new List<string>();

                while (dr.Read())
                {
                    lsNFSeqValidos.Add(dr["cd_nfseq"].ToString());
                }
                frmGerarNumeroNfe frm = new frmGerarNumeroNfe(this);
                if (lsNFSeqValidos.Count > 0)
                {
                    frm.psNM_Banco = belStatic.psNM_Banco;
                    frm.psNM_Cliente = belStatic.sNomeEmpresa;
                    frm.plNotas = lsNFSeqValidos;
                    frm.ShowDialog();
                }
                return frm.bValida;

            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                cx.Close_Conexao();
            }
        }


        #endregion

        #region Eventos

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void dgvNF_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            dgvNF.Columns["vl_totnf"].DefaultCellStyle.Format = "C";
        }

        private void txtNfIni_Validated(object sender, EventArgs e)
        {
            txtNfIni.Text = txtNfIni.Text.PadLeft(6, '0');
            txtNfFim.Text = txtNfIni.Text;
            txtNfFim.Focus();
            txtNfFim.SelectAll();
        }

        private void txtNfFim_Validated(object sender, EventArgs e)
        {
            txtNfFim.Text = txtNfFim.Text.PadLeft(6, '0');
        }

        private void txtNfFim_Enter(object sender, EventArgs e)
        {

        }
        private void dgvNF_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //bTodosEnviar

            if (((e.ColumnIndex == 1)) && (dgvNF.DataSource != null))
            {
                if (bTodosImprimir == false)
                {
                    MarcadesmarcaTodos(true, e.ColumnIndex);
                    bTodosImprimir = true;
                }
                else
                {
                    MarcadesmarcaTodos(false, e.ColumnIndex);
                    bTodosImprimir = false;
                }
            }
            if (((e.ColumnIndex == 0)) && (dgvNF.DataSource != null))
            {
                if (bTodosEnviar == false)
                {
                    MarcadesmarcaTodos(true, e.ColumnIndex);
                    bTodosEnviar = true;
                }
                else
                {
                    MarcadesmarcaTodos(false, e.ColumnIndex);
                    bTodosEnviar = false;
                }
            }

            //Diego - OS - 23723 - 11/03/2009
            for (int i = 0; i < dgvNF.RowCount; i++)
            {
                //dgvNF["Imprime", i].Value = false;
                //dgvNF["ASSINANF", i].Value = false;

                if ((dgvNF["st_contingencia", i].Value.ToString().Equals("S"))
                            && (dgvNF["ST_NFE", i].Value.ToString().Equals("0"))
                            && (dgvNF["CD_NOTAFIS", i].Value.ToString() != ""))
                {
                    dgvNF.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
                else if ((dgvNF["st_contingencia", i].Value.ToString().Equals("S"))
                            && (dgvNF["CD_NOTAFIS", i].Value.ToString() != "")
                            && (dgvNF["ST_NFE", i].Value.ToString().Equals("1")))
                {
                    dgvNF.Rows[i].DefaultCellStyle.BackColor = Color.Aquamarine;
                }


            }
            //Fim - Diego - OS - 23723 - 11/03/2009
        }

        private void frmArquivosXml_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.L))
            {
                btnPesquisa_Click(sender, e);
            }
            if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.E))
            {
                btnGerarXml_Click(sender, e);
            }
            if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.P))
            {
                btnImprimir_Click(sender, e);
            }
        }

        private void dgvNF_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if ((e.RowIndex > -1) && (e.ColumnIndex == 0))
            {
                if ((dgvNF[0, e.RowIndex].Value == null))
                {
                    dgvNF[0, e.RowIndex].Value = true;
                    SendKeys.Send("{right}");
                    SendKeys.Send("{left}");
                }
                else
                {
                    if (dgvNF[0, e.RowIndex].Value.ToString() == "False")
                    {
                        dgvNF[0, e.RowIndex].Value = true;
                        SendKeys.Send("{right}");
                        SendKeys.Send("{left}");
                    }
                    else
                    {
                        dgvNF[0, e.RowIndex].Value = false;
                        SendKeys.Send("{right}");
                        SendKeys.Send("{left}");
                    }
                }
            }
            if ((e.RowIndex > -1) && (e.ColumnIndex == 1))
            {
                if ((dgvNF[1, e.RowIndex].Value == null))
                {
                    dgvNF[1, e.RowIndex].Value = true;
                    SendKeys.Send("{left}");
                    SendKeys.Send("{right}");
                }
                else
                {
                    if (dgvNF[1, e.RowIndex].Value.ToString() == "False")
                    {
                        dgvNF[1, e.RowIndex].Value = true;
                        SendKeys.Send("{left}");
                        SendKeys.Send("{right}");


                    }
                    else
                    {
                        dgvNF[1, e.RowIndex].Value = false;
                        SendKeys.Send("{left}");
                        SendKeys.Send("{right}");
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            VerificaStatusSefaz();
        }

        private void frmArquivosXml_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.objfrmPrincipal.sStatusSefaz = "";
            //objfrmPrincipal.lblUsuario.Text = "";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            PopulaDataGridViewPendenciaContingencia();
        }

        private void txtNfIni_Enter(object sender, EventArgs e)
        {
            KryptonTextBox txt = (KryptonTextBox)sender;
            txt.SelectAll();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                objfrmInicializacao = new frmLogin();
                objfrmInicializacao.ShowDialog();

                if (!objfrmInicializacao.bFechaAplicativo)
                {
                    //objfrmPrincipal.bAlteraDados = objfrmInicializacao.bAlteraDadosNfe;
                    objfrmPrincipal.lblUsuario.Text = "   USUÁRIO : " + belStatic.SUsuario;
                    //objfrmPrincipal.bAlteraDados = objfrmInicializacao.bAlteraDadosNfe;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void rdbFiltros_CheckedChanged(object sender, EventArgs e)
        {
            KryptonRadioButton radio = (KryptonRadioButton)sender;

            if (radio.Name.Equals("rdbData"))
            {
                dtpFim.Visible = true;
                dtpIni.Visible = true;

                txtNfFim.Visible = false;
                txtNfIni.Visible = false;

                dtpIni.Focus();
            }
            else if (radio.Name.Equals("rdbNF"))
            {
                dtpFim.Visible = false;
                dtpIni.Visible = false;

                txtNfFim.Visible = true;
                txtNfIni.Visible = true;
                txtNfIni.Focus();
            }
        }

        private void txtNfIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                KryptonMessageBox.Show("Somente Números", "Campo Númerico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Handled = true;
            }
        }

        private void txtNfFim_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                KryptonMessageBox.Show("Somente Números", "Campo Númerico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Handled = true;
            }
        }
        #endregion

        private int _cacheWidth;
        private void btnMinimiza_Click(object sender, EventArgs e)
        {
            try
            {
                kryptonSplitContainer1.SuspendLayout();
                if (kryptonSplitContainer1.FixedPanel == FixedPanel.None)
                {
                    kryptonSplitContainer1.FixedPanel = FixedPanel.Panel1;
                    kryptonSplitContainer1.IsSplitterFixed = true;

                    _cacheWidth = headerMenuLateral.Width;
                    int newWidth = headerMenuLateral.PreferredSize.Height;

                    kryptonSplitContainer1.Panel1MinSize = newWidth;
                    kryptonSplitContainer1.SplitterDistance = newWidth;

                    headerMenuLateral.HeaderPositionPrimary = VisualOrientation.Right;
                    headerMenuLateral.ButtonSpecs[0].Edge = PaletteRelativeEdgeAlign.Near;
                    for (int i = 0; i < kryptonSplitContainer1.Panel1.Controls.Count; i++)
                    {
                        if (kryptonSplitContainer1.Panel1.Controls[i].GetType() == typeof(KryptonButton) || kryptonSplitContainer1.Panel1.Controls[i].GetType() == typeof(KryptonSeparator))
                        {
                            kryptonSplitContainer1.Panel1.Controls[i].Visible = false;
                        }
                        else if (kryptonSplitContainer1.Panel1.Controls[i].GetType() == typeof(KryptonTextBox))
                        {
                            kryptonSplitContainer1.Panel1.Controls[i].Visible = false;
                        }
                        else if (kryptonSplitContainer1.Panel1.Controls[i].GetType() == typeof(KryptonHeader))
                        {
                            kryptonSplitContainer1.Panel1.Controls[i].Visible = false;
                        }
                    }
                }
                else
                {
                    kryptonSplitContainer1.FixedPanel = FixedPanel.None;
                    kryptonSplitContainer1.IsSplitterFixed = false;
                    kryptonSplitContainer1.Panel1MinSize = 25;
                    kryptonSplitContainer1.SplitterDistance = _cacheWidth;

                    headerMenuLateral.HeaderPositionPrimary = VisualOrientation.Top;
                    headerMenuLateral.ButtonSpecs[0].Edge = PaletteRelativeEdgeAlign.Far;

                    for (int i = 0; i < kryptonSplitContainer1.Panel1.Controls.Count; i++)
                    {
                        if (kryptonSplitContainer1.Panel1.Controls[i].GetType() == typeof(KryptonButton) || kryptonSplitContainer1.Panel1.Controls[i].GetType() == typeof(KryptonSeparator))
                        {
                            kryptonSplitContainer1.Panel1.Controls[i].Visible = true;
                        }
                        else if (kryptonSplitContainer1.Panel1.Controls[i].GetType() == typeof(KryptonTextBox))
                        {
                            kryptonSplitContainer1.Panel1.Controls[i].Visible = true;
                        }
                        else if (kryptonSplitContainer1.Panel1.Controls[i].GetType() == typeof(KryptonHeader))
                        {
                            kryptonSplitContainer1.Panel1.Controls[i].Visible = true;
                        }
                    }
                }
                kryptonSplitContainer1.ResumeLayout();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void kryptonSplitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void timerInicializacao_Tick(object sender, EventArgs e)
        {
            timerInicializacao.Stop();
            KryptonMessageBox.Show("Nota Fiscal Elêtronica não será iniciado pois nenhum certificado foi selecionado", "A V I S O", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }





    }
}
