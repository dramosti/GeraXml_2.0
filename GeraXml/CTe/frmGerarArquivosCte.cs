using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HLP.Dao.CTe;
using HLP.bel.CTe;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Xml;
using HLP.bel;
using HLP.bel.Static;
using DANFE;
using CrystalDecisions.CrystalReports.Engine;
using ComponentFactory.Krypton.Toolkit;
using System.Threading;
using System.Xml.Linq;
using CrystalDecisions.Shared;

namespace NfeGerarXml
{
    public partial class frmGerarArquivosCte : KryptonForm
    {
        public string _sMessageException = string.Format("Ocorreu uma Exceção ao Manipular essa Ação : {0}{0}Verifique a Mensagem abaixo: {0}________________________________{0}{0}", Environment.NewLine);

        X509Certificate2 cert = null;
        bool bTodosImprimir = false;
        bool bTodosEnviar = false;
        List<string> slistaConhec = new List<string>();
        belGlobais objbelGlobais = new belGlobais();
        daoGravaDadosRetorno objGravaDadosRetorno = new daoGravaDadosRetorno();
        daoBuscaDadosGerais objGerais = new daoBuscaDadosGerais();
        daoEmpresa objdaoEmpresa = new daoEmpresa();
        belUF objbelUfEmp = new belUF();
        belCriaXml objCriaXml = null;
        List<string> Pendencias = new List<string>();
        bool Operacao = true;


        public frmGerarArquivosCte()
        {
            InitializeComponent();
            VerificaGeneratorLote();
            objdaoEmpresa.BuscaUFeAmb();
        }
        private void frmGerarArquivos_Load(object sender, EventArgs e)
        {
            try
            {
                lblAmbiente.Text = (belStatic.tpAmb == 1 ? "Produção" : "Homologação");
                objbelUfEmp.SiglaUF = belStatic.Sigla_uf;
                DirectoryInfo info = new DirectoryInfo(belStaticPastas.CBARRAS);
                LimparPasta(info);
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, _sMessageException + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                this.Close();
            }
        }



        private void VerificaPendenciasContingencia()
        {
            try
            {
                if (!belStatic.bModoContingencia)
                {
                    Pendencias = objGerais.VerificaPendenciasdeEnvio();
                    if (Pendencias.Count > 0)
                    {
                        txtPendencias.Text = "";
                        txtPendencias.Visible = true;
                        btnPendencias.Visible = true;

                        foreach (string item in Pendencias)
                        {
                            txtPendencias.Text += "Seq. " + item + Environment.NewLine;
                        }
                        if (Pendencias.Count > 1)
                        {
                            KryptonMessageBox.Show("Existem " + Pendencias.Count.ToString() + " Conhecimentos Pendentes de Envio!", "P E N D Ê N C I A S", MessageBoxButtons.OK, MessageBoxIcon.Warning); ;
                        }
                        else if (Pendencias.Count == 1)
                        {
                            KryptonMessageBox.Show("Existe " + Pendencias.Count.ToString() + " Conhecimento Pendente de Envio!", "P E N D Ê N C I A S", MessageBoxButtons.OK, MessageBoxIcon.Warning); ;
                        }

                    }
                    else
                    {
                        txtPendencias.Visible = false;
                        btnPendencias.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, _sMessageException + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            }
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
                a.Delete();
            }
        }



        private void VerificaGeneratorLote()
        {
            try
            {
                daoGenerator objdaoGenerator = new daoGenerator();
                if (objdaoGenerator.VerificaExistenciaGenerator("GEN_LOTE_CTE"))
                {
                    lblNumLote.Text = "Último Lote: " + objdaoGenerator.RetornaUltimoValorGenerator("GEN_LOTE_CTE");
                }
                else
                {
                    objdaoGenerator.CreateGenerator("GEN_LOTE_CTE", 0);
                    lblNumLote.Text = "Último Lote: " + objdaoGenerator.RetornaUltimoValorGenerator("GEN_LOTE_CTE");
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, _sMessageException + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            }
        }
        public bool VerificaStatusServico()
        {
            bool ret = true;
            try
            {
                InternetCS objVerificaInternet = new InternetCS();
                if (objVerificaInternet.Conexao())
                {
                    cert = new X509Certificate2();
                    cert = belCertificadoDigital.BuscaNome("");
                    if (!belCertificadoDigital.ValidaCertificado(cert))
                    {
                        lblStatus.Text = "";
                        ret = false;
                        throw new InvalidOperationException("Certificado não Selecionado.");
                    }
                    if (belCertificadoDigital.ValidaCertificado(cert))
                    {
                        objCriaXml = new belCriaXml(cert);
                        List<belStatusCte> ListaStatus = objCriaXml.GerarXmlConsultaStatus();
                        KryptonMessageBox.Show(belTrataMensagem.RetornaMensagem(ListaStatus, belTrataMensagem.Tipo.Status), "STATUS WEBSERVICE ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        foreach (belStatusCte status in ListaStatus)
                        {
                            if (status.CodRetorno == "107")
                            {
                                lblStatusSefaz.Text = "Sistema em Operação";
                                belStatic.bModoContingencia = false;
                                VerificaPendenciasContingencia();
                                Operacao = true;
                            }
                            else
                            {
                                if (KryptonMessageBox.Show("O Sistema está Indisponível" + Environment.NewLine + "Deseja imprimir DACTE em Modo de Contingência?", "STATUS WEBSERVICE ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    belStatic.bModoContingencia = true;
                                    lblStatusSefaz.Text = "Sistema em Modo de Contingência";
                                }
                                else
                                {
                                    lblStatusSefaz.Text = "Sistema Inoperante";
                                }
                                Operacao = false;
                            }
                        }
                    }
                }
                else
                {
                    FalhaInternet();
                }
            }
            catch (InvalidOperationException io)
            {
                KryptonMessageBox.Show(null, _sMessageException + (io.InnerException != null ? io.InnerException.Message : io.Message).ToString(), "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                FalhaInternet();
            }
            return ret;

        }

        private void FalhaInternet()
        {
            if (!belStatic.bModoContingencia)
            {
                if (KryptonMessageBox.Show("A internet parece estar indisponível!" + Environment.NewLine + "Deseja imprimir DACTE em Modo de Contingência?", "STATUS WEBSERVICE ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    belStatic.bModoContingencia = true;
                    lblStatusSefaz.Text = "Sistema em Modo de Contingência";
                }
                else
                {
                    lblStatusSefaz.Text = "Sistema Inoperante";
                }
                Operacao = false;
            }
            else
            {
                KryptonMessageBox.Show("A internet parece estar indisponível!" + Environment.NewLine + "Sistema já está em Modo de Contingência.", "STATUS WEBSERVICE ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        #region Botoes Form
        public class DadosImpressao
        {
            public string sCaminhoXml;
            public bool bArquivoEncontrado = false;
            public bool Cancelado = false;
            public string sNumeroCte;
            public string sProtocolo;
        }


        private void btnImpressao_Click(object sender, EventArgs e)
        {
            try
            {
                List<DadosImpressao> objListDados = new List<DadosImpressao>();

                belGlobais objGlobais = new belGlobais();
                DirectoryInfo dPasta = null;
                FileInfo[] fArquivoImprimir = null;

                if (!belStatic.bModoContingencia)
                {
                    #region Verifica Selecionadas

                    for (int i = 0; i < dgvArquivos.RowCount; i++)
                    {
                        if (dgvArquivos["cl_imprime", i].Value != null)
                        {
                            if (dgvArquivos["cl_imprime", i].Value.ToString().Equals("True"))
                            {
                                //string sProtEnvio = objGerais.VerificaCampoProtocoloEnvio(dgvArquivos["cd_conheci", i].Value.ToString());
                                //if (sProtEnvio != "")
                                //{
                                DadosImpressao objDados = new DadosImpressao();
                                objDados.sNumeroCte = dgvArquivos["cd_conheci", i].Value.ToString();
                                objDados.sProtocolo = "";// sProtEnvio;
                                if (!dgvArquivos["ds_cancelamento", i].Value.ToString().Equals(""))
                                {
                                    objDados.Cancelado = true;
                                }
                                objListDados.Add(objDados);
                                //}

                            }
                        }
                    }

                    #endregion
                }
                else
                {
                    #region Verifica Notas em Contingencia

                    for (int i = 0; i < dgvArquivos.RowCount; i++)
                    {
                        if (dgvArquivos["cl_imprime", i].Value != null)
                        {
                            if (dgvArquivos["cl_imprime", i].Value.ToString().Equals("True") && Convert.ToBoolean(dgvArquivos["st_cte", i].Value) == false && Convert.ToBoolean(dgvArquivos["st_contingencia", i].Value) == true)
                            {
                                DadosImpressao objDados = new DadosImpressao();
                                objDados.sNumeroCte = dgvArquivos["cd_conheci", i].Value.ToString();
                                objListDados.Add(objDados);
                            }
                        }
                    }

                    #endregion
                }

                #region Popula Dataset com Cte Validos

                if (objListDados.Count > 0)
                {
                    for (int i = 0; i < objListDados.Count; i++)
                    {
                        if (!belStatic.bModoContingencia)
                        {
                            string sChaveCteRet = objGerais.BuscaChaveRetornoCte(objListDados[i].sNumeroCte);
                            string sPasta = sChaveCteRet.Substring(4, 2) + "-" + sChaveCteRet.Substring(2, 2);

                            if (!objListDados[i].Cancelado)
                            {
                                dPasta = new DirectoryInfo(belStaticPastas.ENVIADOS + @"\\" + sPasta);
                            }
                            else
                            {
                                dPasta = new DirectoryInfo(belStaticPastas.CANCELADOS + @"\\" + sPasta);
                            }
                            if (dPasta.Exists)
                            {
                                fArquivoImprimir = dPasta.GetFiles("Cte_" + sChaveCteRet + ".xml");
                                if (fArquivoImprimir.Count() == 1)
                                {
                                    objListDados[i].bArquivoEncontrado = true;
                                    objListDados[i].sCaminhoXml = dPasta.ToString() + "\\Cte_" + sChaveCteRet + ".xml";
                                }
                                else
                                {
                                    throw new Exception("Arquivo Xml não Encontrado");
                                }
                            }
                        }
                        else
                        {
                            XmlDocument doc = new XmlDocument();

                            string sChaveCteRet = objGerais.BuscaChaveRetornoCte(objListDados[i].sNumeroCte);
                            string sPasta = sChaveCteRet.Substring(4, 2) + "-" + sChaveCteRet.Substring(2, 2);

                            dPasta = new DirectoryInfo(belStaticPastas.CONTINGENCIA + @"\\" + sPasta);
                            fArquivoImprimir = dPasta.GetFiles("*.xml", SearchOption.AllDirectories);

                            foreach (FileInfo arq in fArquivoImprimir)
                            {
                                doc.Load(@arq.FullName);
                                if (doc.GetElementsByTagName("infCte")[0].Attributes["Id"].Value.ToString().Replace("CTe", "").Equals(sChaveCteRet))
                                {
                                    objListDados[i].bArquivoEncontrado = true;
                                    objListDados[i].sCaminhoXml = arq.FullName;
                                    break;
                                }
                            }

                        }
                    }

                    belPopulaDataSet objDataSet = new belPopulaDataSet();

                    dsCTe dsPadrao = new dsCTe();
                    dsCTe dsLotacao = new dsCTe();
                    dsCTe dsPadraoCancelado = new dsCTe();
                    dsCTe dsLotacaoCancelado = new dsCTe();

                    for (int i = 0; i < objListDados.Count; i++)
                    {
                        dsCTe dsPDF = new dsCTe();
                        if (objListDados[i].bArquivoEncontrado == true)
                        {
                            if (objDataSet.VerificaLotacao(objListDados[i].sCaminhoXml))
                            {
                                if (!objListDados[i].Cancelado)
                                {
                                    objDataSet.PopulaDataSet(dsLotacao, objListDados[i].sCaminhoXml, i + 1, objListDados[i].sProtocolo);
                                    objDataSet.PopulaDataSet(dsPDF, objListDados[i].sCaminhoXml, 1, objListDados[i].sProtocolo);
                                    GeraPDF(dsPDF, TipoPDF.LOTACAO, objListDados[i].sNumeroCte);

                                }
                                else
                                {
                                    objDataSet.PopulaDataSet(dsLotacaoCancelado, objListDados[i].sCaminhoXml, i + 1, objListDados[i].sProtocolo);
                                    objDataSet.PopulaDataSet(dsPDF, objListDados[i].sCaminhoXml, 1, objListDados[i].sProtocolo);
                                    GeraPDF(dsPDF, TipoPDF.LOTACAO_CANCELADO, objListDados[i].sNumeroCte);
                                }
                            }
                            else
                            {
                                if (!objListDados[i].Cancelado)
                                {
                                    objDataSet.PopulaDataSet(dsPadrao, objListDados[i].sCaminhoXml, i + 1, objListDados[i].sProtocolo);
                                    objDataSet.PopulaDataSet(dsPDF, objListDados[i].sCaminhoXml, 1, objListDados[i].sProtocolo);
                                    GeraPDF(dsPDF, TipoPDF.PADRAO, objListDados[i].sNumeroCte);
                                }
                                else
                                {
                                    objDataSet.PopulaDataSet(dsPadraoCancelado, objListDados[i].sCaminhoXml, i + 1, objListDados[i].sProtocolo);
                                    objDataSet.PopulaDataSet(dsPDF, objListDados[i].sCaminhoXml, 1, objListDados[i].sProtocolo);
                                    GeraPDF(dsPDF, TipoPDF.PADRAO_CANCELADO, objListDados[i].sNumeroCte);
                                }
                            }
                        }
                    }

                    if (objbelGlobais.LeRegWin("EmailAutomatico").ToString() == "True")
                    {
                        EnviaEmail(objListDados);
                    }
                    if (dsPadrao.infCte.Count() > 0)
                    {
                        ReportDocument rpt = new ReportDocument();
                        rpt.Load(Application.StartupPath + "\\Relatorios\\rptCtePadrao.rpt");
                        rpt.SetDataSource(dsPadrao);
                        rpt.Refresh();

                        frmRelatorioCte frm = new frmRelatorioCte(rpt, "Impressão de DACTE - Carga Fracionada");
                        frm.Show();
                    }
                    if (dsPadraoCancelado.infCte.Count() > 0)
                    {
                        ReportDocument rpt = new ReportDocument();
                        rpt.Load(Application.StartupPath + "\\Relatorios\\rptCtePadraoCancelado.rpt");
                        rpt.SetDataSource(dsPadraoCancelado);
                        rpt.Refresh();

                        frmRelatorioCte frm = new frmRelatorioCte(rpt, "Impressão de DACTE - Carga Fracionada(Cancelados)");
                        frm.Show();
                    }
                    if (dsLotacao.infCte.Count() > 0)
                    {
                        ReportDocument rpt = new ReportDocument();
                        rpt.Load(Application.StartupPath + "\\Relatorios\\rptCteLotacao.rpt");
                        rpt.SetDataSource(dsLotacao);
                        rpt.Refresh();

                        frmRelatorioCte frm = new frmRelatorioCte(rpt, "Impressão de DACTE - Lotação");
                        frm.Show();
                    }
                    if (dsLotacaoCancelado.infCte.Count() > 0)
                    {
                        ReportDocument rpt = new ReportDocument();
                        rpt.Load(Application.StartupPath + "\\Relatorios\\rptCteLotacaoCancelado.rpt");
                        rpt.SetDataSource(dsLotacaoCancelado);
                        rpt.Refresh();

                        frmRelatorioCte frm = new frmRelatorioCte(rpt, "Impressão de DACTE - Lotação(Cancelados)");
                        frm.Show();
                    }


                }
                else
                {
                    KryptonMessageBox.Show("Nenhum Conhecimento Válido foi Selecionado para Impressão", "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                }

            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, _sMessageException + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            }


                #endregion

            CarregaGrid();
        }
        private void btnGeraPdf_Click(object sender, EventArgs e)
        {
            try
            {
                List<DadosImpressao> objListDados = new List<DadosImpressao>();
                FileInfo[] fArquivoImprimir = null;
                belGlobais objGlobais = new belGlobais();
                DirectoryInfo dPasta = null;

                if (!belStatic.bModoContingencia)
                {
                    #region Verifica Selecionadas

                    for (int i = 0; i < dgvArquivos.RowCount; i++)
                    {
                        if (dgvArquivos["cl_imprime", i].Value != null)
                        {
                            if (dgvArquivos["cl_imprime", i].Value.ToString().Equals("True"))
                            {
                                string sProtEnvio = objGerais.VerificaCampoProtocoloEnvio(dgvArquivos["cd_conheci", i].Value.ToString());
                                if (sProtEnvio != "")
                                {
                                    DadosImpressao objDados = new DadosImpressao();
                                    objDados.sNumeroCte = dgvArquivos["cd_conheci", i].Value.ToString();
                                    objDados.sProtocolo = sProtEnvio;
                                    if (!(dgvArquivos["ds_cancelamento", i].Value).ToString().Equals(""))
                                    {
                                        objDados.Cancelado = true;
                                    }
                                    objListDados.Add(objDados);
                                }

                            }
                        }
                    }

                    #endregion
                }
                else
                {
                    #region Verifica Notas em Contingencia

                    for (int i = 0; i < dgvArquivos.RowCount; i++)
                    {
                        if (dgvArquivos["cl_imprime", i].Value != null)
                        {
                            if (dgvArquivos["cl_imprime", i].Value.ToString().Equals("True") && Convert.ToBoolean(dgvArquivos["st_cte", i].Value) == false && Convert.ToBoolean(dgvArquivos["st_contingencia", i].Value) == true)
                            {
                                DadosImpressao objDados = new DadosImpressao();
                                objDados.sNumeroCte = dgvArquivos["cd_conheci", i].Value.ToString();
                                objListDados.Add(objDados);
                            }
                        }
                    }

                    #endregion
                }
                if (objListDados.Count() > 0)
                {
                    if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                    {
                        #region Busca Arquivos XML
                        for (int i = 0; i < objListDados.Count; i++)
                        {
                            if (!belStatic.bModoContingencia)
                            {
                                string sChaveCteRet = objGerais.BuscaChaveRetornoCte(objListDados[i].sNumeroCte);
                                string sPasta = sChaveCteRet.Substring(4, 2) + "-" + sChaveCteRet.Substring(2, 2);

                                if (!objListDados[i].Cancelado)
                                {
                                    dPasta = new DirectoryInfo(belStaticPastas.ENVIADOS + @"\\" + sPasta);
                                }
                                else
                                {
                                    dPasta = new DirectoryInfo(belStaticPastas.CANCELADOS + @"\\" + sPasta);
                                }
                                if (dPasta.Exists)
                                {
                                    fArquivoImprimir = dPasta.GetFiles("Cte_" + sChaveCteRet + ".xml");
                                    if (fArquivoImprimir.Count() == 1)
                                    {
                                        objListDados[i].bArquivoEncontrado = true;
                                        objListDados[i].sCaminhoXml = dPasta.ToString() + "\\Cte_" + sChaveCteRet + ".xml";
                                    }
                                    else
                                    {
                                        throw new Exception("Arquivo Xml não Encontrado");
                                    }
                                }
                            }
                            else
                            {
                                XmlDocument doc = new XmlDocument();

                                string sChaveCteRet = objGerais.BuscaChaveRetornoCte(objListDados[i].sNumeroCte);
                                string sPasta = sChaveCteRet.Substring(4, 2) + "-" + sChaveCteRet.Substring(2, 2);

                                dPasta = new DirectoryInfo(belStaticPastas.CONTINGENCIA + @"\\" + sPasta);
                                fArquivoImprimir = dPasta.GetFiles("*.xml", SearchOption.AllDirectories);

                                foreach (FileInfo arq in fArquivoImprimir)
                                {
                                    doc.Load(@arq.FullName);
                                    if (doc.GetElementsByTagName("infCte")[0].Attributes["Id"].Value.ToString().Replace("CTe", "").Equals(sChaveCteRet))
                                    {
                                        objListDados[i].bArquivoEncontrado = true;
                                        objListDados[i].sCaminhoXml = arq.FullName;
                                        break;
                                    }
                                }

                            }
                        }
                        #endregion

                        belPopulaDataSet objDataSet = new belPopulaDataSet();

                        dsCTe dsPadrao = new dsCTe();
                        dsCTe dsLotacao = new dsCTe();
                        dsCTe dsPadraoCancelado = new dsCTe();
                        dsCTe dsLotacaoCancelado = new dsCTe();

                        int iCount = 0;


                        for (int i = 0; i < objListDados.Count; i++)
                        {
                            if (objListDados[i].bArquivoEncontrado == true)
                            {
                                if (objDataSet.VerificaLotacao(objListDados[i].sCaminhoXml))
                                {
                                    if (!objListDados[i].Cancelado)
                                    {
                                        dsLotacao = new dsCTe();
                                        objDataSet.PopulaDataSet(dsLotacao, objListDados[i].sCaminhoXml, 1, objListDados[i].sProtocolo);
                                        GeraPDF(dsLotacao, TipoPDF.LOTACAO, objListDados[i].sNumeroCte, folderBrowserDialog1.SelectedPath);
                                        iCount++;
                                    }
                                    else
                                    {
                                        dsLotacaoCancelado = new dsCTe();
                                        objDataSet.PopulaDataSet(dsLotacaoCancelado, objListDados[i].sCaminhoXml, 1, objListDados[i].sProtocolo);
                                        GeraPDF(dsLotacaoCancelado, TipoPDF.LOTACAO_CANCELADO, objListDados[i].sNumeroCte, folderBrowserDialog1.SelectedPath);
                                        iCount++;
                                    }
                                }
                                else
                                {
                                    if (!objListDados[i].Cancelado)
                                    {
                                        dsPadrao = new dsCTe();
                                        objDataSet.PopulaDataSet(dsPadrao, objListDados[i].sCaminhoXml, 1, objListDados[i].sProtocolo);
                                        GeraPDF(dsPadrao, TipoPDF.PADRAO, objListDados[i].sNumeroCte, folderBrowserDialog1.SelectedPath);
                                        iCount++;
                                    }
                                    else
                                    {
                                        dsPadraoCancelado = new dsCTe();
                                        objDataSet.PopulaDataSet(dsPadraoCancelado, objListDados[i].sCaminhoXml, 1, objListDados[i].sProtocolo);
                                        GeraPDF(dsPadraoCancelado, TipoPDF.PADRAO_CANCELADO, objListDados[i].sNumeroCte, folderBrowserDialog1.SelectedPath);
                                        iCount++;
                                    }
                                }
                            }
                        }
                        if (iCount > 0)
                        {
                            KryptonMessageBox.Show("Arquivos PDF gerados com sucesso!", "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, _sMessageException + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            }
        }

        public enum TipoPDF { PADRAO, LOTACAO, PADRAO_CANCELADO, LOTACAO_CANCELADO };
        private void GeraPDF(dsCTe ds, TipoPDF tpPdf, string sNumCte)
        {
            try
            {

                ReportDocument rpt = new ReportDocument();
                DirectoryInfo dinfo = null;

                if (tpPdf == TipoPDF.PADRAO)
                {
                    rpt.Load(Application.StartupPath + "\\Relatorios\\rptCtePadrao.rpt");
                    dinfo = new DirectoryInfo(belStaticPastas.ENVIADOS + "\\PDF");
                }
                else if (tpPdf == TipoPDF.PADRAO_CANCELADO)
                {
                    rpt.Load(Application.StartupPath + "\\Relatorios\\rptCtePadraoCancelado.rpt");
                    dinfo = new DirectoryInfo(belStaticPastas.CANCELADOS + "\\PDF");
                }
                else if (tpPdf == TipoPDF.LOTACAO)
                {
                    rpt.Load(Application.StartupPath + "\\Relatorios\\rptCteLotacao.rpt");
                    dinfo = new DirectoryInfo(belStaticPastas.ENVIADOS + "\\PDF");
                }
                else if (tpPdf == TipoPDF.LOTACAO_CANCELADO)
                {
                    rpt.Load(Application.StartupPath + "\\Relatorios\\rptCteLotacaoCancelado.rpt");
                    dinfo = new DirectoryInfo(belStaticPastas.CANCELADOS + "\\PDF");
                }
                rpt.SetDataSource(ds);
                rpt.Refresh();

                if (!dinfo.Exists)
                {
                    dinfo.Create();
                }

                ExportPDF(rpt, dinfo.FullName + "\\Cte_" + sNumCte + ".pdf");

            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, _sMessageException + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            }


        }
        private void GeraPDF(dsCTe ds, TipoPDF tpPdf, string sNumCte, string sCaminhoSalvar)
        {
            try
            {

                ReportDocument rpt = new ReportDocument();

                if (tpPdf == TipoPDF.PADRAO)
                {
                    rpt.Load(Application.StartupPath + "\\Relatorios\\rptCtePadrao.rpt");
                }
                else if (tpPdf == TipoPDF.PADRAO_CANCELADO)
                {
                    rpt.Load(Application.StartupPath + "\\Relatorios\\rptCtePadraoCancelado.rpt");
                }
                else if (tpPdf == TipoPDF.LOTACAO)
                {
                    rpt.Load(Application.StartupPath + "\\Relatorios\\rptCteLotacao.rpt");
                }
                else if (tpPdf == TipoPDF.LOTACAO_CANCELADO)
                {
                    rpt.Load(Application.StartupPath + "\\Relatorios\\rptCteLotacaoCancelado.rpt");
                }
                rpt.SetDataSource(ds);
                rpt.Refresh();



                ExportPDF(rpt, sCaminhoSalvar + "\\Cte_" + sNumCte + ".pdf");

            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, _sMessageException + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
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
        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            CarregaGrid();

        }

        private void CarregaGrid()
        {
            try
            {
                daoBuscaDadosGerais objdaoGerais = new daoBuscaDadosGerais();

                StringBuilder strCampos = new StringBuilder();
                strCampos.Append("c.nr_lanc, ");
                strCampos.Append("coalesce(c.cd_conheci, '') cd_conheci , ");
                strCampos.Append("cast(case when coalesce(c.st_contingencia, 'N') = 'S' then 1 else 0 end as smallint) st_contingencia , ");
                strCampos.Append("c.dt_emi, ");
                strCampos.Append("r.nm_social, ");
                strCampos.Append("c.vl_total, ");
                strCampos.Append("cast(case when coalesce(c.st_cte, 'N') = 'S' then 1 else 0 end as smallint ) st_cte, ");
                strCampos.Append("coalesce(c.cd_recibocanc, '') cancelado ");



                StringBuilder strWhere = new StringBuilder();
                strWhere.Append(" ((c.cd_empresa = '");
                strWhere.Append(belStatic.CodEmpresaCte);
                strWhere.Append("')");
                if (rdbData.Checked)
                {
                    strWhere.Append(" and ");
                    strWhere.Append(" (c.dt_emi between '");
                    strWhere.Append(dtpIni.Value.ToString("dd.MM.yyyy"));
                    strWhere.Append("' and '");
                    strWhere.Append(dtpFim.Value.ToString("dd.MM.yyyy"));
                    strWhere.Append("')");
                }
                else
                {
                    strWhere.Append(" and ");
                    strWhere.Append(" (c.nr_lanc between '");
                    strWhere.Append(txtNfIni.Text.ToString());
                    strWhere.Append("' and '");
                    strWhere.Append(txtNfFim.Text.ToString());
                    strWhere.Append("')");
                }
                strWhere.Append(")");

                if (rbdNaoEnviadas.Checked == true)
                {
                    strWhere.Append(" and ");
                    strWhere.Append("(c.st_cte = 'N' or c.st_cte is null) ");
                }
                if (rbdEnviadas.Checked == true)
                {
                    strWhere.Append(" and ");
                    strWhere.Append("(c.st_cte = 'S') ");
                    strWhere.Append("or (c.st_contingencia = 'S') ");
                }

                dgvArquivos.DataSource = objdaoGerais.PesquisaGridView(strCampos.ToString(), strWhere.ToString());
                for (int i = 0; i < dgvArquivos.RowCount; i++)
                {
                    if (dgvArquivos["ds_cancelamento", i].Value.ToString() != "")
                    {
                        dgvArquivos.Rows[i].DefaultCellStyle.BackColor = Color.Khaki;
                    }
                    else if (Convert.ToBoolean(dgvArquivos["st_cte", i].Value) == true)
                    {
                        dgvArquivos.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                    else if (Convert.ToBoolean(dgvArquivos["st_cte", i].Value) == false && Convert.ToBoolean(dgvArquivos["st_contingencia", i].Value) == true)
                    {
                        dgvArquivos.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    }

                    dgvArquivos["cl_assina", i].Value = false;
                    dgvArquivos["cl_imprime", i].Value = false;


                }

            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, _sMessageException + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            }
        }
        private void btnPendencias_Click(object sender, EventArgs e)
        {
            try
            {
                daoBuscaDadosGerais objdaoGerais = new daoBuscaDadosGerais();

                StringBuilder strCampos = new StringBuilder();
                strCampos.Append("c.nr_lanc, ");
                strCampos.Append("coalesce(c.cd_conheci, '') cd_conheci , ");
                strCampos.Append("cast(case when coalesce(c.st_contingencia, 'N') = 'S' then 1 else 0 end as smallint) st_contingencia , ");
                strCampos.Append("c.dt_emi, ");
                strCampos.Append("r.nm_social, ");
                strCampos.Append("c.vl_total, ");
                strCampos.Append("cast(case when coalesce(c.st_cte, 'N') = 'S' then 1 else 0 end as smallint ) st_cte, ");
                strCampos.Append("cast(case when coalesce(c.ds_cancelamento, 'N') = 'N' then 0 else 1 end as smallint) ds_cancelamento ");

                dgvArquivos.DataSource = objdaoGerais.PesquisaGridViewContingencia(strCampos.ToString());

                for (int i = 0; i < dgvArquivos.RowCount; i++)
                {
                    if (dgvArquivos["ds_cancelamento", i].Value.ToString() != "")
                    {
                        dgvArquivos.Rows[i].DefaultCellStyle.BackColor = Color.Khaki;
                    }
                    else if (Convert.ToBoolean(dgvArquivos["st_cte", i].Value) == true)
                    {
                        dgvArquivos.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                    else if (Convert.ToBoolean(dgvArquivos["st_cte", i].Value) == false && Convert.ToBoolean(dgvArquivos["st_contingencia", i].Value) == true)
                    {
                        dgvArquivos.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    }

                }
                VerificaPendenciasContingencia();
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, _sMessageException + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            }
        }



        private void btnEnvio_Click(object sender, EventArgs e)
        {
            try
            {
                objCriaXml = new belCriaXml();
                bool bContingencia = false;


                if (!belStatic.bModoContingencia)
                {
                    if (Operacao)
                    {
                        #region Verifica se Item Selecionado já  foi enviado

                        for (int i = 0; i < dgvArquivos.RowCount; i++)
                        {
                            if (dgvArquivos["cl_assina", i].Value != null)
                            {
                                if (dgvArquivos["cl_assina", i].Value.ToString().Equals("True"))
                                {
                                    if (objGerais.VerificaCampoReciboPreenchido(belStatic.CodEmpresaCte, dgvArquivos["nr_lanc", i].Value.ToString()) != "")
                                    {
                                        throw new Exception("O Conhecimento de Sequência " + dgvArquivos["nr_lanc", i].Value.ToString() + " Já tem um recibo Salvo no Banco de Dados, tente Buscar Retorno.");
                                    }
                                }
                            }
                        }
                        #endregion

                        lblStatus.Text = "Carregando Informações...";

                        #region Pega Notas Selecionadas na Grid

                        string sCanceladas = "";
                        slistaConhec = new List<string>();
                        for (int i = 0; i < dgvArquivos.RowCount; i++)
                        {
                            try
                            {
                                if (dgvArquivos["cl_assina", i].Value != null)
                                {
                                    if (((dgvArquivos["cl_assina", i].Value != null) && (dgvArquivos["cl_assina", i].Value.ToString().Equals("True")))
                                                && ((dgvArquivos["ds_cancelamento", i].Value.ToString() == ""))
                                                && (dgvArquivos["st_cte", i].Value.ToString().Equals("0")))
                                    {
                                        if (Convert.ToBoolean(dgvArquivos["st_cte", i].Value) == false && Convert.ToBoolean(dgvArquivos["st_contingencia", i].Value) == true)
                                        {
                                            bContingencia = true;
                                            if (slistaConhec.Count() > 0)
                                            {
                                                throw new Exception("Os Conhecimentos Pendentes devem ser Enviados um por vez.");
                                            }
                                        }

                                        slistaConhec.Add((string)dgvArquivos["nr_lanc", i].Value);
                                    }
                                    if (dgvArquivos["ds_cancelamento", i].Value.ToString() != "")
                                    {
                                        if (Convert.ToBoolean(dgvArquivos["cl_assina", i].Value.ToString()) == true)
                                        {
                                            sCanceladas += "Conhecimento de Transp. " + dgvArquivos["cd_conheci", i].Value.ToString() + " - Esta Cancelado e não é Permitido o Reenvio do mesmo!" + Environment.NewLine + Environment.NewLine;
                                        }
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                        }
                        #endregion
                        if (slistaConhec.Count == 0)
                        {
                            KryptonMessageBox.Show("Nenhuma nota Válida foi Selecionada!", "A T E N Ç Ã O", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            if (sCanceladas != "")
                            {
                                throw new Exception(sCanceladas);
                            }
                            lblStatus.Text = "";
                        }
                        else
                        {
                            if (!bContingencia)
                            {
                                #region Envio Normal

                                //verifica no banco se as sequencias são existentes.
                                List<string> objLGerarSeq = objGerais.ValidaSeqNoBanco(belStatic.CodEmpresaCte, slistaConhec);
                                if (objLGerarSeq.Count > 0)
                                {
                                    frmGerarNumeroCte objfrmGerarNum = new frmGerarNumeroCte(belStatic.CodEmpresaCte, objLGerarSeq);
                                    objfrmGerarNum.ShowDialog();
                                }

                                cert = new X509Certificate2();
                                cert = belCertificadoDigital.BuscaNome("");
                                if (!belCertificadoDigital.ValidaCertificado(cert))
                                {
                                    lblStatus.Text = "";
                                    throw new Exception("Certificado não Selecionado.");
                                }

                                #region Popula as Classes e abre form Visualização

                                belPopulaObjetos objObjetos = new belPopulaObjetos(belStatic.CodEmpresaCte, slistaConhec, objbelUfEmp.CUF, cert);

                                daoInfCte objdaoInfCte = new daoInfCte();
                                objdaoInfCte.ImportaConhecInfCte(objObjetos, belStatic.CodEmpresaCte);


                                lblStatus.Text = "Aguardando Envio";
                                frmVisualizaCte objFrm = new frmVisualizaCte(objObjetos);
                                objFrm.ShowDialog();

                                #endregion
                                if (objFrm.bCancela)
                                {
                                    lblStatus.Text = "";
                                    throw new Exception("Envio do(s) Conhecimento(s) Cancelado");
                                }
                                else
                                {
                                    #region Envia Lote WebService

                                    lblStatus.Text = "Enviando Lote para WebService...";
                                    daoGenerator objGerator = new daoGenerator();
                                    int iNumLote = Convert.ToInt32(objGerator.RetornaProximoValorGenerator("GEN_LOTE_CTE"));

                                    objGravaDadosRetorno.GravarChave(objFrm.objObjetosAlter);


                                    string sRecibo = objCriaXml.GerarXml(objFrm.objObjetosAlter, iNumLote);


                                    List<belStatusCte> ListaStatus = objCriaXml.ConsultaLoteEnviado(sRecibo);
                                    // if (sRecibo != "") // sicupira
                                    {
                                        objGravaDadosRetorno.GravarRecibo(objFrm.objObjetosAlter, sRecibo);
                                    }
                                    foreach (belStatusCte cte in ListaStatus.Where(C => C.Enviado == true))
                                    {
                                        if (cte.CodRetorno != "218"
                                         && cte.CodRetorno != "101"
                                         && cte.CodRetorno != "103"
                                         && cte.CodRetorno != "104"
                                         && cte.CodRetorno != "105"
                                         && cte.CodRetorno != "100"
                                         && cte.CodRetorno != "204")
                                        {
                                            objGravaDadosRetorno.ApagarRecibo(sRecibo);
                                        }
                                        else
                                        {
                                            objGravaDadosRetorno.GravarProtocoloEnvio(cte);
                                        }
                                    }
                                    KryptonMessageBox.Show(belTrataMensagem.RetornaMensagem(ListaStatus, belTrataMensagem.Tipo.Envio), "CT-e - Retorno WebService", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    objGerais = new daoBuscaDadosGerais();
                                    foreach (belStatusCte cte in ListaStatus.Where(C => C.Enviado == true))
                                    {
                                        lblStatus.Text = "Salvando Arquivos";
                                        objGravaDadosRetorno.AlterarStatusCte(cte);
                                        string sChave = objGravaDadosRetorno.BuscaChave(cte.NumeroSeq);
                                        objCriaXml.SalvaArquivoPastaEnviado(objGerais.BuscaNumeroConhecimento(cte.NumeroSeq), sChave);
                                        if (cte.CodRetorno == "218" || cte.CodRetorno == "101")
                                        {
                                            objCriaXml.SalvaArquivoPastaCancelado(sChave);
                                        }
                                    }
                                    lblStatus.Text = "";

                                    Pendencias = objGerais.VerificaPendenciasdeEnvio();
                                    if (Pendencias.Count > 0)
                                    {
                                        txtPendencias.Text = "";
                                        txtPendencias.Visible = true;
                                        btnPendencias.Visible = true;
                                        foreach (string item in Pendencias)
                                        {
                                            txtPendencias.Text += "Seq. " + item + Environment.NewLine;
                                        }
                                    }
                                    else
                                    {
                                        txtPendencias.Visible = false;
                                        btnPendencias.Visible = false;
                                    }
                                    btnPesquisa_Click(sender, e);
                                    #endregion
                                }

                                #endregion
                            }
                            else
                            {
                                #region Envio Contingencia

                                cert = new X509Certificate2();
                                cert = belCertificadoDigital.BuscaNome("");
                                if (!belCertificadoDigital.ValidaCertificado(cert))
                                {
                                    lblStatus.Text = "";
                                    throw new Exception("Certificado não Selecionado.");
                                }
                                objCriaXml.cert = cert;

                                objGerais = new daoBuscaDadosGerais();
                                belGlobais objGlobais = new belGlobais();
                                XmlDocument doc = new XmlDocument();
                                string sChave = objGerais.BuscaChaveRetornoCteSeq(slistaConhec[0]);

                                DirectoryInfo dPastaContingencia = new DirectoryInfo(belStaticPastas.CONTINGENCIA);
                                FileInfo[] finfo = dPastaContingencia.GetFiles("*.xml", SearchOption.AllDirectories);

                                bool ArquivoPastaEnvio = false;
                                bool ArquivoPastaEnvioMesAtual = false;
                                string sCaminho = "";

                                foreach (FileInfo arq in finfo)
                                {
                                    if (arq.Name.Contains("Lote") && ArquivoPastaEnvio == false)
                                    {
                                        doc.Load(@arq.FullName);
                                        if (doc.GetElementsByTagName("infCte")[0].Attributes["Id"].Value.ToString().Replace("CTe", "").Equals(sChave))
                                        {
                                            sCaminho = @arq.FullName;
                                            string sPathDest = belStaticPastas.ENVIO + "\\" + arq.Name;
                                            string sPathOrigem = belStaticPastas.CONTINGENCIA + "\\" + arq.Name;

                                            if (File.Exists(sPathDest))
                                            {
                                                File.Delete(sPathDest);
                                            }
                                            File.Copy(sPathOrigem, sPathDest);
                                            ArquivoPastaEnvio = true;
                                        }
                                    }
                                    else if (!arq.Name.Contains("Lote") && ArquivoPastaEnvioMesAtual == false)
                                    {
                                        string sData = HLP.Util.Util.GetDateServidor().Date.ToString("dd-MM-yyyy");
                                        doc.Load(@arq.FullName);

                                        if (doc.GetElementsByTagName("infCte")[0].Attributes["Id"].Value.ToString().Replace("CTe", "").Equals(sChave))
                                        {
                                            string sPathDest = belStaticPastas.ENVIO + sData.Substring(3, 2) + "-" + sData.Substring(8, 2) + @"\\" + arq.Name;
                                            string sPathOrigem = belStaticPastas.CONTINGENCIA + sData.Substring(3, 2) + "-" + sData.Substring(8, 2) + @"\\" + arq.Name;

                                            if (File.Exists(sPathDest))
                                            {
                                                File.Delete(sPathDest);
                                            }
                                            File.Copy(sPathOrigem, sPathDest);
                                            ArquivoPastaEnvioMesAtual = true;
                                        }
                                    }

                                    if (ArquivoPastaEnvioMesAtual && ArquivoPastaEnvio)
                                    {
                                        lblStatus.Text = "Enviando Lote para WebService...";
                                        doc.Load(sCaminho);
                                        string sRetorno = objCriaXml.TransmitirLote(doc);
                                        string sRecibo = objCriaXml.BuscaReciboRetornoEnvio(sRetorno);


                                        List<belStatusCte> ListaStatus = objCriaXml.ConsultaLoteEnviado(sRecibo);
                                        if (sRecibo != "")
                                        {
                                            objGravaDadosRetorno.GravarRecibo(slistaConhec[0], sRecibo);
                                        }
                                        foreach (belStatusCte cte in ListaStatus)
                                        {
                                            if (cte.CodRetorno != "103" && cte.CodRetorno != "104" && cte.CodRetorno != "105" && cte.CodRetorno != "100")
                                            {
                                                objGravaDadosRetorno.ApagarRecibo(sRecibo);
                                            }
                                            else
                                            {
                                                objGravaDadosRetorno.GravarProtocoloEnvio(cte);
                                            }
                                        }
                                        KryptonMessageBox.Show(belTrataMensagem.RetornaMensagem(ListaStatus, belTrataMensagem.Tipo.Envio), "CT-e - Retorno WebService", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        objGerais = new daoBuscaDadosGerais();
                                        foreach (belStatusCte cte in ListaStatus.Where(C => C.Enviado == true))
                                        {
                                            lblStatus.Text = "Salvando Arquivos";
                                            objGravaDadosRetorno.AlterarStatusCte(cte);
                                            string sprot = objGerais.BuscaNumProtocolo(cte.NumeroSeq);
                                            objCriaXml.SalvaArquivoPastaEnviado(objGerais.BuscaNumeroConhecimento(cte.NumeroSeq), cte.Chave);
                                        }
                                        lblStatus.Text = "";
                                        btnPendencias_Click(sender, e);
                                        break;
                                    }
                                }
                                if (!ArquivoPastaEnvioMesAtual && !ArquivoPastaEnvio)
                                {
                                    lblStatus.Text = "";
                                    KryptonMessageBox.Show("Arquivo para Envio não Encontrado", "CONHECIMENTO DE TRANSP. ELETRÔNICO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                #endregion
                            }
                        }
                    }
                    else
                    {
                        KryptonMessageBox.Show("Sistema está Indisponível!", "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    KryptonMessageBox.Show("Sistema se encontra em Modo de Contingência", "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                lblStatus.Text = "";
                KryptonMessageBox.Show(null, _sMessageException + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            }
        }
        private void btnContingencia_Click(object sender, EventArgs e)
        {
            try
            {
                if (belStatic.bModoContingencia)
                {
                    #region Verifica se Item Selecionado já  foi enviado

                    for (int i = 0; i < dgvArquivos.RowCount; i++)
                    {
                        if (dgvArquivos["cl_assina", i].Value != null)
                        {
                            if (dgvArquivos["cl_assina", i].Value.ToString().Equals("True"))
                            {
                                if (objGerais.VerificaCampoReciboPreenchido(belStatic.CodEmpresaCte, dgvArquivos["nr_lanc", i].Value.ToString()) != "")
                                {
                                    throw new Exception("O Conhecimento de Sequência " + dgvArquivos["nr_lanc", i].Value.ToString() + " Já tem um recibo Salvo no Banco de Dados, tente Buscar Retorno.");
                                }
                                else if (Convert.ToBoolean(dgvArquivos["st_cte", i].Value) == false && Convert.ToBoolean(dgvArquivos["st_contingencia", i].Value) == true)
                                {
                                    throw new Exception("O Conhecimento de Sequência " + dgvArquivos["nr_lanc", i].Value.ToString() + " Já foi Gerado em Modo de Contingência.");
                                }
                            }
                        }
                    }
                    #endregion

                    lblStatus.Text = "Carregando Informações...";

                    #region Pega Notas Selecionadas na Grid

                    string sCanceladas = "";
                    slistaConhec = new List<string>();
                    for (int i = 0; i < dgvArquivos.RowCount; i++)
                    {
                        if (((dgvArquivos["cl_assina", i].Value != null) && (dgvArquivos["cl_assina", i].Value.ToString().Equals("True")))
                                    && ((dgvArquivos["ds_cancelamento", i].Value.ToString() == ""))
                                    && (dgvArquivos["st_cte", i].Value.ToString().Equals("0")))
                        {

                            slistaConhec.Add((string)dgvArquivos["nr_lanc", i].Value);
                        }
                        if ((dgvArquivos["ds_cancelamento", i].Value.ToString() != ""))
                        {
                            if (dgvArquivos["cl_assina", i].Value == "1")
                            {
                                sCanceladas += "Conhecimento de Transp. " + dgvArquivos["cd_conheci", i].Value.ToString() + " - Esta Cancelado e não é Permitido o Reenvio do mesmo!" + Environment.NewLine + Environment.NewLine;
                            }
                        }
                    }

                    if (slistaConhec.Count == 0)
                    {
                        KryptonMessageBox.Show("Nenhuma nota Válida foi Selecionada!", "A T E N Ç Ã O", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (sCanceladas != "")
                        {
                            throw new Exception(sCanceladas);
                        }
                        lblStatus.Text = "";
                    }
                    else if (slistaConhec.Count > 1)
                    {
                        throw new Exception("Só é Possível Gerar uma XML de Cada vez em Modo de Contingência");
                    }
                    else
                    {
                        //verifica no banco se as sequencias são existentes.
                        List<string> objLGerarSeq = objGerais.ValidaSeqNoBanco(belStatic.CodEmpresaCte, slistaConhec);
                        if (objLGerarSeq.Count > 0)
                        {
                            frmGerarNumeroCte objfrmGerarNum = new frmGerarNumeroCte(belStatic.CodEmpresaCte, objLGerarSeq);
                            objfrmGerarNum.ShowDialog();
                        }
                    #endregion

                        cert = new X509Certificate2();
                        cert = belCertificadoDigital.BuscaNome("");
                        if (!belCertificadoDigital.ValidaCertificado(cert))
                        {
                            lblStatus.Text = "";
                            throw new Exception("Certificado não Selecionado.");
                        }

                        #region Popula as Classes e abre form Visualização

                        belPopulaObjetos objObjetos = new belPopulaObjetos(belStatic.CodEmpresaCte, slistaConhec, objbelUfEmp.CUF, cert);

                        daoInfCte objdaoInfCte = new daoInfCte();
                        objdaoInfCte.ImportaConhecInfCte(objObjetos, belStatic.CodEmpresaCte);


                        lblStatus.Text = "Aguardando";
                        frmVisualizaCte objFrm = new frmVisualizaCte(objObjetos);
                        objFrm.ShowDialog();

                        #endregion
                        if (objFrm.bCancela)
                        {
                            lblStatus.Text = "";
                            throw new Exception("Geração do XML Cancelada");
                        }
                        else
                        {
                            #region Gera XML Contingencia

                            lblStatus.Text = "Gerando XML de Contingência...";
                            daoGenerator objGerator = new daoGenerator();
                            int iNumLote = Convert.ToInt32(objGerator.RetornaProximoValorGenerator("GEN_LOTE_CTE"));

                            objGravaDadosRetorno.GravarChave(objFrm.objObjetosAlter);

                            objCriaXml = new belCriaXml();
                            string sRecibo = objCriaXml.GerarXml(objFrm.objObjetosAlter, iNumLote);

                            objGravaDadosRetorno.AlterarStatusCteContingencia(objFrm.objObjetosAlter.objLinfCte[0].ide.nCT);
                            KryptonMessageBox.Show("Arquivo gravado na pasta Contingência com Sucesso!", "CONHECIMENTO DE TRANSP. ELETRÔNICO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            lblStatus.Text = "";
                            btnPesquisa_Click(sender, e);
                            #endregion
                        }
                    }
                }
                else
                {
                    KryptonMessageBox.Show("Sistema não se encontra em Modo de Contingência!", "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "";
                KryptonMessageBox.Show(null, _sMessageException + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            }
        }
        private void btnBuscaRetorno_Click(object sender, EventArgs e)
        {
            try
            {

                for (int i = 0; i < dgvArquivos.RowCount; i++)
                {
                    if (dgvArquivos["cl_assina", i].Value != null)
                    {
                        if (dgvArquivos["cl_assina", i].Value.ToString().Equals("True"))
                        {
                            string sRecibo = objGerais.VerificaCampoReciboPreenchido(belStatic.CodEmpresaCte, dgvArquivos["nr_lanc", i].Value.ToString());
                            if (sRecibo != "")
                            {
                                cert = new X509Certificate2();
                                cert = belCertificadoDigital.BuscaNome("");
                                if (!belCertificadoDigital.ValidaCertificado(cert))
                                {
                                    lblStatus.Text = "";
                                    throw new Exception("Certificado não Selecionado.");
                                }
                                lblStatus.Text = "Buscando Retorno...";
                                objCriaXml = new belCriaXml(cert);

                                List<belStatusCte> ListaStatus = objCriaXml.ConsultaLoteEnviado(sRecibo);
                                foreach (belStatusCte cte in ListaStatus)
                                {
                                    if (cte.CodRetorno != "218"
                                        && cte.CodRetorno != "101"
                                        && cte.CodRetorno != "103"
                                        && cte.CodRetorno != "104"
                                        && cte.CodRetorno != "105"
                                        && cte.CodRetorno != "100"
                                        && cte.CodRetorno != "204")
                                    {
                                        objGravaDadosRetorno.ApagarRecibo(sRecibo);
                                    }
                                    else
                                    {
                                        objGravaDadosRetorno.GravarProtocoloEnvio(cte);
                                        if (cte.CodRetorno == "218" || cte.CodRetorno == "101")
                                        {
                                            //if (string.IsNullOrEmpty(cte.NumeroSeq))
                                            {
                                                cte.NumeroSeq = dgvArquivos["nr_lanc", i].Value.ToString();
                                                cte.NumeroCte = dgvArquivos["cd_conheci", i].Value.ToString();
                                            }
                                            cte.Enviado = true;
                                            objGravaDadosRetorno.GravarReciboCancelamento(cte.NumeroCte, sRecibo, "ERRO DE SISTEMA");
                                        }
                                    }
                                }
                                objGerais = new daoBuscaDadosGerais();
                                foreach (belStatusCte cte in ListaStatus.Where(C => C.Enviado == true))
                                {
                                    objGravaDadosRetorno.AlterarStatusCte(cte);
                                    string sChave = objGravaDadosRetorno.BuscaChave(cte.NumeroSeq);
                                    objCriaXml.SalvaArquivoPastaEnviado(objGerais.BuscaNumeroConhecimento(cte.NumeroSeq), sChave);
                                    if (cte.CodRetorno == "218" || cte.CodRetorno == "101")
                                    {
                                        objCriaXml.SalvaArquivoPastaCancelado(sChave);
                                    }
                                }
                                belTrataMensagem.sNumCte = dgvArquivos["nr_lanc", i].Value.ToString();
                                KryptonMessageBox.Show(belTrataMensagem.RetornaMensagem(ListaStatus, belTrataMensagem.Tipo.Individual), "CT-e - Retorno WebService", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                lblStatus.Text = "";
                                btnPesquisa_Click(sender, e);
                            }
                            else
                            {
                                KryptonMessageBox.Show("Conhecimento selecionado ainda não Foi enviado!", "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, _sMessageException + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            }
        }
        private void btnCancelamento_Click(object sender, EventArgs e)
        {
            try
            {
                lblStatus.Text = "";
                List<string> sListCodConhec = new List<string>();

                for (int i = 0; i < dgvArquivos.RowCount; i++)
                {
                    if (dgvArquivos["cl_assina", i].Value != null)
                    {
                        if (dgvArquivos["cl_assina", i].Value.ToString().Equals("True"))
                        {
                            string sRecibo = objGerais.VerificaCampoReciboPreenchido(belStatic.CodEmpresaCte, dgvArquivos["nr_lanc", i].Value.ToString());
                            if (sRecibo != "")
                            {
                                sListCodConhec.Add(dgvArquivos["cd_conheci", i].Value.ToString());
                            }
                        }
                    }
                }
                if (sListCodConhec.Count == 1)
                {
                    frmCancJustCte objfrmCanc = new frmCancJustCte(sListCodConhec);
                    objfrmCanc.ShowDialog();
                    btnPesquisa_Click(sender, e);
                }
                else if (sListCodConhec.Count > 1)
                {
                    KryptonMessageBox.Show("Não é possível cancelar vários CT-e de uma vez!", "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                }
                else
                {
                    KryptonMessageBox.Show("Nenhum CT-e válido foi Selecionado para cancelamento.", "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                }

            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, _sMessageException + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            }
        }
        private void btnInutilizacao_Click(object sender, EventArgs e)
        {
            try
            {
                frmInutilizaFaixaCte frmInu = new frmInutilizaFaixaCte();
                frmInu.ShowDialog();
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, _sMessageException + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            }
        }
        private void btnConsultaStatus_Click(object sender, EventArgs e)
        {
            try
            {
                VerificaStatusServico();
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, _sMessageException + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            }
        }
        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #endregion


        private void EnviaEmail(List<DadosImpressao> objListDados)
        {

            try
            {
                string sHostServidor = objbelGlobais.LeRegWin("HostServidor").ToString().Trim();
                string sPorta = objbelGlobais.LeRegWin("PortaServidor").ToString().Trim();
                string sRemetente = objbelGlobais.LeRegWin("EmailRemetente").ToString().Trim();
                string sSenha = objbelGlobais.LeRegWin("SenhaRemetente").ToString().Trim();
                bool bAutentica = Convert.ToBoolean(objbelGlobais.LeRegWin("RequerSSL").ToString().Trim());

                List<belEmailCte> objlbelEmail = new List<belEmailCte>();


                for (int i = 0; i < objListDados.Count; i++)
                {

                    if ((sHostServidor != "") && (sPorta != "0") && (sRemetente != "") && (sSenha != ""))
                    {
                        string sCaminho = objListDados[i].sCaminhoXml;
                        belEmailCte objemail = new belEmailCte(sCaminho, objListDados[i].sNumeroCte, sHostServidor, sPorta, sRemetente, sSenha, bAutentica);
                        objlbelEmail.Add(objemail);

                    }
                    else
                    {
                        if (KryptonMessageBox.Show(null, "Campos para o envio de e-mail automático não estão preenchidos corretamente!" +
                                        Environment.NewLine + Environment.NewLine +
                                        "Deseja preencher os campos agora?", "E N V I O", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
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
                    frmEmailCte objfrmEmail = new frmEmailCte(objlbelEmail);
                    objfrmEmail.ShowDialog();
                    int icount = 0;
                    for (int i = 0; i < objfrmEmail.objLbelEmailCte.Count; i++)
                    {
                        if ((objfrmEmail.objLbelEmailCte[i]._envia == true) && (objfrmEmail.objLbelEmailCte[i]._para != ""))
                        {
                            try
                            {
                                objfrmEmail.objLbelEmailCte[i].enviaEmail(objfrmEmail.objLbelEmailCte[i]._NumCte);
                                icount++;
                            }
                            catch (Exception ex)
                            {
                                KryptonMessageBox.Show(null, ex.Message + Environment.NewLine + Environment.NewLine + "E-mail: " + objfrmEmail.objLbelEmailCte[i]._para + "   - Seq: " + objfrmEmail.objLbelEmailCte[i]._sSeq, "E R R O - E N V I O", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            txtNfFim.Text = "";
            txtNfIni.Text = "";
        }



        private void txtNfIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }
        private void txtNfIni_Enter(object sender, EventArgs e)
        {
            KryptonTextBox txt = (KryptonTextBox)sender;
            txt.SelectAll();
        }
        private void txtNfIni_Validated(object sender, EventArgs e)
        {
            KryptonTextBox txt = (KryptonTextBox)sender;
            txt.Text = txt.Text.PadLeft(7, '0');
        }



        private void dgvArquivos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((e.RowIndex > -1) && (e.ColumnIndex == 0))
                {
                    if ((dgvArquivos[0, e.RowIndex].Value == null))
                    {
                        dgvArquivos[0, e.RowIndex].Value = true;
                        SendKeys.Send("{right}");
                        SendKeys.Send("{left}");
                    }
                    else
                    {
                        if (dgvArquivos[0, e.RowIndex].Value.ToString() == "False")
                        {
                            dgvArquivos[0, e.RowIndex].Value = true;
                            SendKeys.Send("{right}");
                            SendKeys.Send("{left}");

                        }
                        else
                        {
                            dgvArquivos[0, e.RowIndex].Value = false;
                            SendKeys.Send("{right}");
                            SendKeys.Send("{left}");
                        }
                    }
                }
                if ((e.RowIndex > -1) && (e.ColumnIndex == 1))
                {
                    if ((dgvArquivos[1, e.RowIndex].Value == null))
                    {
                        dgvArquivos[1, e.RowIndex].Value = true;
                        SendKeys.Send("{left}");
                        SendKeys.Send("{right}");
                    }
                    else
                    {
                        if (dgvArquivos[1, e.RowIndex].Value.ToString() == "False")
                        {
                            dgvArquivos[1, e.RowIndex].Value = true;
                            SendKeys.Send("{left}");
                            SendKeys.Send("{right}");


                        }
                        else
                        {
                            dgvArquivos[1, e.RowIndex].Value = false;
                            SendKeys.Send("{left}");
                            SendKeys.Send("{right}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void dgvArquivos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (((e.ColumnIndex == 1)) && (dgvArquivos.DataSource != null))
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
                if (((e.ColumnIndex == 0)) && (dgvArquivos.DataSource != null))
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
                for (int i = 0; i < dgvArquivos.RowCount; i++)
                {
                    if (dgvArquivos["ds_cancelamento", i].Value.ToString() != "")
                    {
                        dgvArquivos.Rows[i].DefaultCellStyle.BackColor = Color.Khaki;
                    }
                    else if (Convert.ToBoolean(dgvArquivos["st_cte", i].Value) == true)
                    {
                        dgvArquivos.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                    else if (Convert.ToBoolean(dgvArquivos["st_cte", i].Value) == false && Convert.ToBoolean(dgvArquivos["st_contingencia", i].Value) == true)
                    {
                        dgvArquivos.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void MarcadesmarcaTodos(bool Marca, int coluna)
        {
            for (int i = 0; i < dgvArquivos.RowCount; i++)
            {
                dgvArquivos.Rows[i].Cells[coluna].Value = Marca;
                SendKeys.Send("{right}");
                SendKeys.Send("{left}");
            }
        }

        private void frmGerarArquivos_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.L))
            {
                btnPesquisa_Click(sender, e);
            }
            if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.E))
            {
                btnEnvio_Click(sender, e);
            }
            if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.P))
            {
                btnImpressao_Click(sender, e);
            }
        }

        private void btnConsultaSituacao_Click(object sender, EventArgs e)
        {
            try
            {
                lblStatus.Text = "";
                List<string> sListCodConhec = new List<string>();

                for (int i = 0; i < dgvArquivos.RowCount; i++)
                {
                    if (dgvArquivos["cl_assina", i].Value != null)
                    {
                        if (dgvArquivos["cl_assina", i].Value.ToString().Equals("True"))
                        {
                            string sRecibo = objGerais.VerificaCampoReciboPreenchido(belStatic.CodEmpresaCte, dgvArquivos["nr_lanc", i].Value.ToString());
                            if (sRecibo != "")
                            {
                                sListCodConhec.Add(dgvArquivos["cd_conheci", i].Value.ToString());
                            }
                        }
                    }
                }
                if (sListCodConhec.Count == 1)
                {
                    objCriaXml = new belCriaXml();
                    cert = new X509Certificate2();
                    cert = belCertificadoDigital.BuscaNome("");
                    if (!belCertificadoDigital.ValidaCertificado(cert))
                    {
                        throw new Exception("Certificado não Selecionado.");
                    }
                    else
                    {
                        objCriaXml.cert = cert;
                        objGerais = new daoBuscaDadosGerais();
                        string sChave = objGerais.BuscaChaveRetornoCte(sListCodConhec[0]);
                        List<belStatusCte> ListaStatus = objCriaXml.GerarXmlConsultaSituacao(sChave, false);

                        if (ListaStatus[0].NumeroSeq != null)
                        {
                            belTrataMensagem.sNumCte = objGerais.BuscaNumeroConhecimento(ListaStatus[0].NumeroSeq);
                        }
                        KryptonMessageBox.Show(belTrataMensagem.RetornaMensagem(ListaStatus, belTrataMensagem.Tipo.Situacao), "CT-e - Retorno WebService", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (sListCodConhec.Count > 1)
                {
                    KryptonMessageBox.Show("Não é possível consultar vários CT-e de uma vez!", "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                }
                else
                {
                    KryptonMessageBox.Show("Nenhum CT-e válido foi Selecionado.", "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                }


            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, _sMessageException + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            }
        }

        private int _cacheWidth;
        private void btnMinimiza_Click(object sender, EventArgs e)
        {
            try
            {
                splitContainerTela.SuspendLayout();
                if (splitContainerTela.FixedPanel == FixedPanel.None)
                {
                    splitContainerTela.FixedPanel = FixedPanel.Panel1;
                    splitContainerTela.IsSplitterFixed = true;

                    _cacheWidth = headerMenuLateral.Width;
                    int newWidth = headerMenuLateral.PreferredSize.Height;

                    splitContainerTela.Panel1MinSize = newWidth;
                    splitContainerTela.SplitterDistance = newWidth;

                    headerMenuLateral.HeaderPositionPrimary = VisualOrientation.Right;
                    headerMenuLateral.ButtonSpecs[0].Edge = PaletteRelativeEdgeAlign.Near;
                    for (int i = 0; i < splitContainerTela.Panel1.Controls.Count; i++)
                    {
                        if (splitContainerTela.Panel1.Controls[i].GetType() == typeof(KryptonButton) || splitContainerTela.Panel1.Controls[i].GetType() == typeof(KryptonSeparator))
                        {
                            splitContainerTela.Panel1.Controls[i].Visible = false;
                        }
                        else if (splitContainerTela.Panel1.Controls[i].GetType() == typeof(KryptonTextBox))
                        {
                            splitContainerTela.Panel1.Controls[i].Visible = false;
                        }
                        else if (splitContainerTela.Panel1.Controls[i].GetType() == typeof(KryptonHeader))
                        {
                            splitContainerTela.Panel1.Controls[i].Visible = false;
                        }
                    }
                }
                else
                {
                    splitContainerTela.FixedPanel = FixedPanel.None;
                    splitContainerTela.IsSplitterFixed = false;
                    splitContainerTela.Panel1MinSize = 25;
                    splitContainerTela.SplitterDistance = _cacheWidth;

                    headerMenuLateral.HeaderPositionPrimary = VisualOrientation.Top;
                    headerMenuLateral.ButtonSpecs[0].Edge = PaletteRelativeEdgeAlign.Far;

                    for (int i = 0; i < splitContainerTela.Panel1.Controls.Count; i++)
                    {
                        if (splitContainerTela.Panel1.Controls[i].GetType() == typeof(KryptonButton) || splitContainerTela.Panel1.Controls[i].GetType() == typeof(KryptonSeparator))
                        {
                            splitContainerTela.Panel1.Controls[i].Visible = true;
                        }
                        else if (splitContainerTela.Panel1.Controls[i].GetType() == typeof(KryptonTextBox))
                        {
                            splitContainerTela.Panel1.Controls[i].Visible = true;
                        }
                        else if (splitContainerTela.Panel1.Controls[i].GetType() == typeof(KryptonHeader))
                        {
                            splitContainerTela.Panel1.Controls[i].Visible = true;
                        }
                    }
                }
                splitContainerTela.ResumeLayout();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void liberarParaReenvioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvArquivos.RowCount; i++)
            {
                if (dgvArquivos["cl_assina", i].Value != null)
                {
                    if (dgvArquivos["cl_assina", i].Value.ToString().Equals("True"))
                    {
                        objGerais.LiberaNotaParaReenvio(dgvArquivos["nr_lanc", i].Value.ToString());
                    }
                }
            }
            CarregaGrid();
        }













    }
}
