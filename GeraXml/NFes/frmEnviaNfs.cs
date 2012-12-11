using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HLP.bel;
using HLP.Util;
using FirebirdSql.Data.FirebirdClient;
using HLP.Dao.NFes;
using HLP.bel.NFes;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using ComponentFactory.Krypton.Toolkit;
using HLP.bel.Static;
using HLP.bel.NFe.GeraXml;
using HLP.bel.CTe;

namespace NfeGerarXml.NFes
{
    public partial class frmEnviaNfs : KryptonForm
    {
        frmGerarXml objfrmPrincipal;
        /// <summary>
        /// Sequencias das Notas a serem enviadas;
        /// </summary>
        List<string> sListaNotas = new List<string>();
        belConnection cx = new belConnection();
        public frmEnviaNfs(frmGerarXml objfrmPrincipal)
        {
            InitializeComponent();
            belStatic.bNotaServico = true;
            this.objfrmPrincipal = objfrmPrincipal;
            VerificaGeneratorLote();
            rbtNaoEnviadas.Checked = true;

        }
        public frmEnviaNfs()
        {
            InitializeComponent();
            belStatic.bNotaServico = true;
            VerificaGeneratorLote();
            rbtNaoEnviadas.Checked = true;

        }

        private void VerificaGeneratorLote()
        {
            try
            {
                daoUtil objdaoUtil = new daoUtil();
                if (objdaoUtil.VerificaExistenciaGenerator("GEN_LOTE_NFES"))
                {
                    lblNumLote.Text = "ÚLTIMO LOTE: " + objdaoUtil.RetornaUltimoValorGenerator("GEN_LOTE_NFES");
                }
                else
                {
                    objdaoUtil.CreateGenerator("GEN_LOTE_NFES", 0);
                    lblNumLote.Text = "ÚLTIMO LOTE: " + objdaoUtil.RetornaUltimoValorGenerator("GEN_LOTE_NFES");
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, string.Format(Msg_Padrao.CException, Environment.NewLine) + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "AVISO ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            PopulaDataGridView();
        }


        public void PopulaDataGridView()
        {
            try
            {
                StringBuilder strCampos = new StringBuilder();
                strCampos.Append("nf.cd_notafis, ");
                strCampos.Append("nf.cd_nfseq, ");
                strCampos.Append("cd_recibonfe, ");
                strCampos.Append("nf.dt_emi, ");
                strCampos.Append("nf.nm_guerra, ");
                strCampos.Append("nf.vl_totnf, ");
                strCampos.Append("coalesce(nf.cd_numero_nfse, '')cd_numero_nfse , ");
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
                strWhere.Append(belStatic.codEmpresaNFe);
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
                strWhere.Append("(coalesce(nf.st_nf_prod,'S') = 'N') ");
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
                for (int i = 0; i < dgvNF.RowCount; i++)
                {
                    dgvNF["ASSINANF", i].Value = false;

                    if (dgvNF["cd_recibocanc", i].Value.ToString() != "")
                    {
                        dgvNF.Rows[i].DefaultCellStyle.BackColor = Color.Khaki;
                    }
                    else if ((dgvNF["cd_recibonfe", i].Value.ToString() != "")
                        && (dgvNF["ST_NFE", i].Value.ToString().Equals("0")))
                    {
                        dgvNF.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, string.Format(Msg_Padrao.CException, Environment.NewLine) + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "AVISO ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
                cx.Close_Conexao();
                return "";
                KryptonMessageBox.Show(null, string.Format(Msg_Padrao.CException, Environment.NewLine) + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "AVISO ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally { cx.Close_Conexao(); }

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            PopulaDataGridView();
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
                foreach (var sNfseq in sListaNotas)
                {
                    iCont++;
                    sSqlSeqValidas.Append(sNfseq);
                    if (sListaNotas.Count > iCont)
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

        private bool ValidaCertificado(X509Certificate2 cert)
        {
            try
            {
                int IvalidaCertificado = Convert.ToInt32(cert.Version);
                return true;
            }
            catch (Exception)
            {
                return false;
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

        private void MarcadesmarcaTodos(int coluna)
        {
            for (int i = 0; i < dgvNF.RowCount; i++)
            {
                dgvNF.Rows[i].Cells[coluna].Value = !Convert.ToBoolean(dgvNF.Rows[i].Cells[coluna].Value);
                SendKeys.Send("{right}");
                SendKeys.Send("{left}");
            }
        }

        private void dgvNF_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (((e.ColumnIndex == 0)) && (dgvNF.DataSource != null))
            {
                MarcadesmarcaTodos(e.ColumnIndex);
            }
        }


        private void frmEnviaNfes_Load(object sender, EventArgs e)
        {
            lblCodEmpresa.Text = belStatic.codEmpresaNFe.ToString() + " - " + belStatic.sNomeEmpresa;

            DirectoryInfo dinfo = new DirectoryInfo(@"G:\CSharp\Desenvolvimento");
            DirectoryInfo dinfo2 = new DirectoryInfo(@"J:\D6\Industri");
            StringBuilder sSql = new StringBuilder();
            try
            {
                cx.Open_Conexao();

                if ((dinfo.Exists) && (dinfo.Exists))
                {
                    belStatic.tpAmbNFse = 2;
                    KryptonMessageBox.Show(null, "VOCÊ ESTÁ TRABALHANDO NA HLP, VERIFIQUE O AMBIENTE DO SISTEMA DA NFE", "CUIDADO, ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    sSql.Append("update empresa set ST_AMBIENTE_NFSE = '2' ");
                    sSql.Append("Where empresa.cd_empresa = '");
                    sSql.Append(belStatic.codEmpresaNFe);
                    sSql.Append("'");

                    using (FbCommand cmdUpdate = new FbCommand(sSql.ToString(), cx.get_Conexao()))
                    {
                        cmdUpdate.ExecuteNonQuery();
                    }

                }
                else
                {
                    sSql = new StringBuilder();
                    sSql.Append("SELECT coalesce(EMPRESA.st_ambiente_nfse,'2')st_ambiente_nfse ");
                    sSql.Append("FROM EMPRESA ");
                    sSql.Append("Where empresa.cd_empresa = '");
                    sSql.Append(belStatic.codEmpresaNFe);
                    sSql.Append("'");
                    using (FbCommand cmdUpdate = new FbCommand(sSql.ToString(), cx.get_Conexao()))
                    {
                        belStatic.tpAmbNFse = Convert.ToInt16(cmdUpdate.ExecuteScalar());
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally { cx.Close_Conexao(); }
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            frmStatusEnvioNfs objfrmStatus = null;
            try
            {
                Globais objGlobais = new Globais();
                if (objGlobais.LeRegConfig("GrupoServico").Equals(""))
                {
                    throw new Exception("Parametrize o Grupo Padrão de Faturamento (Serviço) no Config do Sistema antes de Enviar Notas");
                }

                for (int i = 0; i < dgvNF.RowCount; i++)
                {
                    if (dgvNF["ASSINANF", i].Value != null)
                    {
                        if (dgvNF["ASSINANF", i].Value.ToString().Equals("True"))
                        {
                            if (VerificaCampoReciboPreenchido(belStatic.codEmpresaNFe, dgvNF["CD_NFSEQ", i].Value.ToString()) != "")
                            {
                                throw new Exception("A Nota de Sequencia = " + dgvNF["CD_NFSEQ", i].Value.ToString() + " Já tem um retorno Salvo no Banco de Dados, tente Buscar Retorno");
                            }
                        }
                    }
                }
                #region Busca Notas Selecionadas na Grid
                string sNfCancelada = string.Empty;
                sListaNotas = new List<string>();
                for (int i = 0; i < dgvNF.RowCount; i++)
                {
                    if (((dgvNF["ASSINANF", i].Value != null) && (dgvNF["ASSINANF", i].Value.ToString().Equals("True"))) && ((dgvNF["CANCELADA", i].Value == null) || (dgvNF["CANCELADA", i].Value.ToString() == "0"))) //Danner - o.s. SEM - 17/12/2009
                    {
                        sListaNotas.Add((string)dgvNF["CD_NFSEQ", i].Value);
                    }
                    if ((dgvNF["CANCELADA", i].Value != null) && (dgvNF["CANCELADA", i].Value.ToString() == "1"))
                    {
                        sNfCancelada += "Nota Fiscal " + dgvNF["CD_NOTAFIS", i].Value.ToString() + " - Esta Cancelada e não é Permitido o Reenvio da mesma Nota!" + Environment.NewLine + Environment.NewLine;
                    }
                }

                if (sListaNotas.Count == 0)
                {
                    KryptonMessageBox.Show("Nenhuma nota Valida foi Selecionada!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (sNfCancelada != "")
                    {
                        KryptonMessageBox.Show(sNfCancelada, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return;
                }

                #endregion
                daoLoteRps objdaoLoteRps = new daoLoteRps();

                if (ValidaSeqNoBanco())
                {
                    tcLoteRps objLoteRps = objdaoLoteRps.BuscaDadosNFes(sListaNotas);
                    //Chamar Form para visualizar;
                    frmVisualizaNfs objFrmVisualiza = new frmVisualizaNfs(objLoteRps);
                    objFrmVisualiza.ShowDialog();
                    if (objFrmVisualiza.bCancela)
                    {
                        MessageBoxIcon _msgIcon = MessageBoxIcon.Information;
                        throw new Exception("Envio da(s) Nota(s) Cancelado");
                    }

                    AssinaNFeXml Assinatura = new AssinaNFeXml();
                    X509Certificate2 cert = Assinatura.BuscaNome("");
                    if (!ValidaCertificado(cert))
                    {
                        throw new Exception("Certificado não Selecionado!!");
                    }
                    objfrmStatus = new frmStatusEnvioNfs();
                    objfrmStatus.Show();
                    objfrmStatus.lblMsg.Text = "Montando XML de Acordo com os dados inseridos!!";
                    objfrmStatus.lblMsg.Refresh();
                    objfrmStatus.Refresh();

                    HLP.bel.NFes.belCreateXml objCreateXml = new belCreateXml(cert);
                    objCreateXml.GerarAqruivoXml(objFrmVisualiza.objLoteRpsAlter);

                    objfrmStatus.lblMsg.Text = "Enviando Lote para o Webservice!!";
                    objfrmStatus.lblMsg.Refresh();

                    //Envia Lote
                    belRecepcao objBelRecepcao = new belRecepcao(objCreateXml.sXmlLote, cert, objFrmVisualiza.objLoteRpsAlter);
                    if (objBelRecepcao.sMsgTransmissao != "")
                    {
                        throw new Exception(objBelRecepcao.sMsgTransmissao);
                    }
                    daoRecepcao objdaoRecepcao = new daoRecepcao(objBelRecepcao.Protocolo, objFrmVisualiza.objLoteRpsAlter);
                    objfrmStatus.lblMsg.Text = "Gravando recibo na base de dados!!";
                    objfrmStatus.lblMsg.Refresh();
                    objdaoRecepcao.GravaRecibo();
                    string sMsgErro = objBelRecepcao.BuscaRetorno(objFrmVisualiza.objLoteRpsAlter.Rps[0].InfRps.Prestador, objfrmStatus.lblMsg, objfrmStatus.progressBarStatus);

                    if (objBelRecepcao.sCodigoRetorno.Equals("E4"))
                    {
                        objfrmStatus.Close();
                        KryptonMessageBox.Show(null, sMsgErro + Environment.NewLine + Environment.NewLine + "IMPORTANTE: Tente Buscar Retorno da NFse pois o serviço do Web service está demorando para responder; ", "MENSAGEM DE RETORNO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (objBelRecepcao.objListaNfseRetorno.Count > 0) //Lote Enviado
                    {
                        objfrmStatus.lblMsg.Text = "Alterando Status da Nota para Enviada!!";
                        objfrmStatus.lblMsg.Refresh();
                        objdaoRecepcao.AlteraStatusDaNota(objBelRecepcao.objListaNfseRetorno);
                        objfrmStatus.Close();
                        objdaoRecepcao.VerificaNotasParaCancelar(objBelRecepcao.objListaNfseRetorno);

                        for (int i = 0; i < dgvNF.RowCount; i++)
                        {
                            //CD_NFSEQ
                            int ienviado = objBelRecepcao.objListaNfseRetorno.Count(lote => lote.IdentificacaoRps.Nfseq == dgvNF["CD_NFSEQ", i].Value.ToString());
                            if (ienviado > 0)
                            {
                                dgvNF["ST_NFE", i].Value = true;
                            }
                        }
                        KryptonMessageBox.Show(null, objBelRecepcao.MontaMsgDeRetornoParaCliente(), "MENSAGEM DE RETORNO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        EnviaEmail(objBelRecepcao.objListaNfseRetorno);
                    }
                    else
                    {
                        objdaoRecepcao.LimpaRecibo(objFrmVisualiza.objLoteRpsAlter);
                        objfrmStatus.Close();
                        KryptonMessageBox.Show(null, sMsgErro + Environment.NewLine, "MENSAGEM DE RETORNO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }

                VerificaGeneratorLote();
                PopulaDataGridView();
            }
            catch (Exception ex)
            {
                if (objfrmStatus != null)
                {
                    objfrmStatus.Close();
                }
                VerificaGeneratorLote();
                KryptonMessageBox.Show(null, string.Format(Msg_Padrao.CException, Environment.NewLine) + (ex.InnerException != null ? ex.InnerException.Message + Environment.NewLine + ex.Message : ex.Message).ToString(), "AVISO ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally { cx.Close_Conexao(); }
        }

        private void EnviaEmail(List<TcInfNfse> bjListaNfseRetorno)
        {
            Globais LeRegWin = new Globais();
            string hostservidor = LeRegWin.LeRegConfig("HostServidor").ToString().Trim();
            string porta = LeRegWin.LeRegConfig("PortaServidor").ToString().Trim();
            string remetente = LeRegWin.LeRegConfig("EmailRemetente").ToString().Trim();
            string senha = LeRegWin.LeRegConfig("SenhaRemetente").ToString().Trim();
            bool autentica = Convert.ToBoolean(LeRegWin.LeRegConfig("RequerSSL").ToString().Trim());

            List<belEmail> objlbelEmail = new List<belEmail>();

            //OS_25285
            daoPrestador objdaoPrestador = new daoPrestador();
            string sMsgPadraoPrestador = objdaoPrestador.RetPrestadorEmail();


            if ((hostservidor != "") && (porta != "0") && (remetente != "") && (senha != ""))
            {
                for (int e = 0; e < bjListaNfseRetorno.Count; e++)
                {
                    belEmail objemail = new belEmail(bjListaNfseRetorno[e], sMsgPadraoPrestador, hostservidor, porta, remetente, senha, "", autentica);
                    objlbelEmail.Add(objemail);

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

        private void btnBuscaRetorno_Click(object sender, EventArgs e)
        {
            frmStatusEnvioNfs objfrmStatus = null;
            try
            {
                #region Busca Notas Selecionadas na Grid
                string sNfCancelada = string.Empty;
                sListaNotas = new List<string>();
                for (int i = 0; i < dgvNF.RowCount; i++)
                {
                    if (((dgvNF["ASSINANF", i].Value != null) && (dgvNF["ASSINANF", i].Value.ToString().Equals("True"))) && ((dgvNF["CANCELADA", i].Value == null) || (dgvNF["CANCELADA", i].Value.ToString() == "0"))) //Danner - o.s. SEM - 17/12/2009
                    {
                        sListaNotas.Add((string)dgvNF["CD_NFSEQ", i].Value);
                    }
                    if ((dgvNF["CANCELADA", i].Value != null) && (dgvNF["CANCELADA", i].Value.ToString() == "1"))
                    {
                        sNfCancelada += "Nota Fiscal " + dgvNF["CD_NOTAFIS", i].Value.ToString() + " - Esta Cancelada e não é Permitido o Reenvio da mesma Nota!" + Environment.NewLine + Environment.NewLine;
                    }
                }

                if (sListaNotas.Count == 0)
                {
                    KryptonMessageBox.Show("Nenhuma nota Valida foi Selecionada!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (sNfCancelada != "")
                    {
                        KryptonMessageBox.Show(sNfCancelada, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return;
                }

                #endregion

                if (sListaNotas.Count == 1)
                {
                    belRecepcao objBelRecepcao = new belRecepcao();
                    daoPrestador objdaoPrestador = new daoPrestador();
                    daoRecepcao objdaoRecepcao = new daoRecepcao();
                    //Buscar Protocolo no banco
                    objBelRecepcao.Protocolo = objdaoRecepcao.BuscaNumProtocolo(sListaNotas[0]);

                    //Busca Retorno do lote
                    AssinaNFeXml Assinatura = new AssinaNFeXml();
                    X509Certificate2 cert = Assinatura.BuscaNome("");
                    if (!ValidaCertificado(cert))
                    {
                        throw new Exception("Certificado não Selecionado!!");
                    }
                    else
                    {
                        objBelRecepcao.cert = cert;
                    }

                    objfrmStatus = new frmStatusEnvioNfs();
                    objfrmStatus.Show();
                    objfrmStatus.Refresh();
                    string sMsgErro = objBelRecepcao.BuscaRetorno(objdaoPrestador.RettcIdentificacaoPrestador(cx.get_Conexao(), sListaNotas[0]), objfrmStatus.lblMsg, objfrmStatus.progressBarStatus);

                    if (objBelRecepcao.sCodigoRetorno.Equals("E4"))
                    {
                        objfrmStatus.Close();
                        KryptonMessageBox.Show(null, sMsgErro + Environment.NewLine + Environment.NewLine + "IMPORTANTE: Tente Buscar Retorno da NFse pois o serviço do Web service está demorando para responder; ", "MENSAGEM DE RETORNO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (objBelRecepcao.objListaNfseRetorno.Count > 0) //Lote Enviado
                    {
                        objfrmStatus.lblMsg.Text = "Alterando Status da Nota para Enviada!!";
                        objfrmStatus.lblMsg.Refresh();
                        objdaoRecepcao.AlteraStatusDaNota(objBelRecepcao.objListaNfseRetorno);
                        objdaoRecepcao.VerificaNotasParaCancelar(objBelRecepcao.objListaNfseRetorno);
                        for (int i = 0; i < dgvNF.RowCount; i++)
                        {
                            //CD_NFSEQ
                            int ienviado = objBelRecepcao.objListaNfseRetorno.Count(lote => lote.IdentificacaoRps.Nfseq == dgvNF["CD_NFSEQ", i].Value.ToString());
                            if (ienviado > 0)
                            {
                                dgvNF["ST_NFE", i].Value = true;
                            }
                        }
                        KryptonMessageBox.Show(null, objBelRecepcao.MontaMsgDeRetornoParaCliente(), "MENSAGEM DE RETORNO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        EnviaEmail(objBelRecepcao.objListaNfseRetorno);
                    }
                    else
                    {
                        objdaoRecepcao.LimpaRecibo();
                        objfrmStatus.Close();
                        KryptonMessageBox.Show(null, sMsgErro + Environment.NewLine, "MENSAGEM DE RETORNO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    objfrmStatus.Close();
                }
                else
                {
                    throw new Exception("Selecione apenas uma Nota e o lote ref. a éssa nota será consultado!!");
                }
                VerificaGeneratorLote();
                PopulaDataGridView();
            }
            catch (Exception ex)
            {
                if (objfrmStatus != null)
                {
                    objfrmStatus.Close();
                }
                KryptonMessageBox.Show(null, string.Format(Msg_Padrao.CException, Environment.NewLine) + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "AVISO ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally { cx.Close_Conexao(); }
        }

        private void btnCancelamento_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> sNotacanc = new List<string>();
                frmCancelamentoNfs objfrmCanc = new frmCancelamentoNfs();
                daoCancelamento objdaoCanc = new daoCancelamento();
                belCancelamentoNFse objbelCance = new belCancelamentoNFse();
                cx.Open_Conexao();

                for (int i = 0; i < dgvNF.RowCount; i++)
                {
                    if ((((dgvNF["cd_recibocanc", i].Value.ToString() == "") && (dgvNF["ASSINANF", i].Value != null) && dgvNF["ASSINANF", i].Value.ToString().Equals("True")) && (dgvNF["ST_NFE", i].Value.ToString() != "0")))//&& (dgvNF["CANCELADA", i].Value.ToString() != "0")))
                    {
                        sNotacanc.Add((string)dgvNF["CD_NFSEQ", i].Value);
                    }
                }
                if (sNotacanc.Count == 1)
                {
                    objfrmCanc.ShowDialog();
                    AssinaNFeXml Assinatura = new AssinaNFeXml();
                    X509Certificate2 cert = Assinatura.BuscaNome("");
                    if (!ValidaCertificado(cert))
                    {
                        throw new Exception("Certificado não Selecionado!!");
                    }

                    TcPedidoCancelamento objTcPediCanc = objdaoCanc.BuscaDadosParaCancelamento(cx.get_Conexao(), objfrmCanc.sErro, sNotacanc[0]);
                    string sXmlRet = objbelCance.CancelaNfes(objTcPediCanc, cert); //Cancela a Nota
                    string sMsgRet = objbelCance.ConfiguraMsgRetornoCancelamento(sXmlRet); // Configura Msg e verifica se a nota foi cancelada
                    if (objbelCance.bNotaCancelada)
                    {
                        objdaoCanc.CancelarNFseSistema(objTcPediCanc.InfPedidoCancelamento.IdentificacaoNfse.Numero, cx.get_Conexao()); //Update no cd_recibocanc
                    }
                    KryptonMessageBox.Show(null, sMsgRet, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, string.Format(Msg_Padrao.CException, Environment.NewLine) + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "AVISO ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally { cx.Close_Conexao(); }
        }

        private void btnVisualizar_Click(object sender, EventArgs e)
        {
            try
            {
                List<belimpressao> sListImpressao = new List<belimpressao>();
                belimpressao obj;
                for (int i = 0; i < dgvNF.RowCount; i++)
                {
                    if ((((dgvNF["Print", i].Value != null) && dgvNF["Print", i].Value.ToString().Equals("True")) && (dgvNF["ST_NFE", i].Value.ToString() != "0")))//&& (dgvNF["CANCELADA", i].Value.ToString() != "0")))
                    {
                        obj = new belimpressao();
                        obj.sNfSeq = (string)dgvNF["CD_NFSEQ", i].Value;
                        sListImpressao.Add(obj);
                    }
                }

                if (sListImpressao.Count > 0)
                {
                    daoImpressao objdaoImp = new daoImpressao();

                    sListImpressao = objdaoImp.BuscaDadosParaImpressao(sListImpressao, cx.get_Conexao());
                    cx.Open_Conexao();
                    for (int i = 0; i < sListImpressao.Count; i++)
                    {
                        // System.Diagnostics.Process.Start("iexplore", "http://" + belStatic.sNmCidadeEmpresa + ".ginfes" + (belStatic.tpAmbNFse == 2 ? "h" : "") + ".com.br/birt/frameset?__report=nfs_novo.rptdesign&cdVerificacao=" + sListImpressao[i].sVerificacao + "&numNota=" + sListImpressao[i].sNota);
                        System.Diagnostics.Process.Start("iexplore", "http://" + belStatic.sNmCidadeEmpresa + ".ginfes" + (belStatic.tpAmbNFse == 2 ? "h" : "") + ".com.br/birt/frameset?__report=nfs_ver4.rptdesign&cdVerificacao=" + sListImpressao[i].sVerificacao + "&numNota=" + sListImpressao[i].sNota);
                        //http://itu.                                ginfes.                                             com.br/birt/frameset?__report=nfs_ver4.rptdesign&cdVerificacao=515980147&numNota=119
                    }
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, string.Format(Msg_Padrao.CException, Environment.NewLine) + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "AVISO ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally { cx.Close_Conexao(); }
        }

        private void dgvNF_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            lblCod_nfse.Text = (dgvNF["cd_numero_nfse", e.RowIndex].Value.ToString() != "" ? "Nº NFse: " + dgvNF["cd_numero_nfse", e.RowIndex].Value.ToString() : "");

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

        private void rdbData_CheckedChanged(object sender, EventArgs e)
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
            else if (radio.Name.Equals("rdbSeq"))
            {
                dtpFim.Visible = false;
                dtpIni.Visible = false;

                txtNfFim.Visible = true;
                txtNfIni.Visible = true;
                txtNfIni.Focus();
            }
        }

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








    }
}
