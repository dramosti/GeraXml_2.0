using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Xml;
using FirebirdSql.Data.FirebirdClient;
using HLP.bel;
using ComponentFactory.Krypton.Toolkit;
using HLP.bel.Static;
using HLP.bel.CTe;
using HLP.bel.NFe.GeraXml;

namespace NfeGerarXml
{
    public partial class frmConfiguracao : KryptonForm
    {
        /// <summary>
        ///  0 - Pasta
        ///  1 - Parametros Gerais
        ///  2 - Email
        ///  3 - Escrita Fiscal
        ///  4 - SCAN - Contingencia
        /// </summary>
        /// <param name="iTabControle"></param>
        /// 
        string spath = "";
        belConnection cx;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iTabControle">Tab Para Iniciar</param>
        /// <param name="path">Nome do Arquivo</param>
        /// 
        struct GruposFaturamento
        {
            public string ds_descvalor { get; set; }
            public string ds_valor { get; set; }
        }


        public frmConfiguracao(int iTabControle)
        {
            InitializeComponent();
            this.spath = belStatic.sConfig;
            //iTabControle controla qual a Tab que o tabControl1 irá iniciar toda a vez que o mesmo for instanciado.

            if (iTabControle == 0)
            {
                Configuracoes.SelectedTab = tbPasta;
            }
            else if (iTabControle == 1)
            {
                Configuracoes.SelectedTab = tbCertificado;
            }
            else if (iTabControle == 2)
            {
                Configuracoes.SelectedTab = tbEmail;
            }
            else if (iTabControle == 3)
            {
                Configuracoes.SelectedTab = tbpEscritor;
            }
            else if (iTabControle == 4)
            {
                Configuracoes.SelectedTab = tabContingencia;
            }
            //Fim - Danner - o.s. 23732 - 06/11/2009

            Configuracoes.TabPages.Remove(tbpEscritor);

        }



        protected void Pastas_Click(object sender, EventArgs e)
        {
            ButtonSpecAny btn = (ButtonSpecAny)sender;

            if (
                (btn.UniqueName.Equals("btnEnvioArquivo")) ||
                (btn.UniqueName.Equals("btnRetornoArquivo")) ||
                (btn.UniqueName.Equals("btnXmlEnviado")) ||
                (btn.UniqueName.Equals("btnSchemaXml")) ||
                (btn.UniqueName.Equals("btnXmlCancelados")) ||
                (btn.UniqueName.Equals("btnProtocolo")) ||
                (btn.UniqueName.Equals("btnCodigoBarrasTemp")) ||
                (btn.UniqueName.Equals("btnPesquisaContingencia")) ||
                (btn.UniqueName.Equals("btnCaminhoPadrao")) ||
                (btn.UniqueName.Equals("btnArquivoEscrituracao") ||
                (btn.UniqueName.Equals("btnPastaRelatorio")) ||
                (btn.UniqueName.Equals("btnPastaCCe"))))
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    switch (btn.UniqueName)
                    {
                        case "btnEnvioArquivo": txtEnvioArquivo.Text = folderBrowserDialog1.SelectedPath;
                            break;
                        case "btnXmlEnviado": txtXmlEnviados.Text = folderBrowserDialog1.SelectedPath;
                            break;
                        case "btnXmlCancelados": txtXmlCancelados.Text = folderBrowserDialog1.SelectedPath;
                            break;
                        case "btnProtocolo": txtProtocolo.Text = folderBrowserDialog1.SelectedPath;
                            break;
                        case "btnArquivoEscrituracao": txtEscrituracao.Text = folderBrowserDialog1.SelectedPath;
                            break;
                        case "btnCodigoBarrasTemp": txtCaminhoBarrasTemp.Text = folderBrowserDialog1.SelectedPath;
                            break;
                        case "btnPesquisaContingencia": txtContingencia.Text = folderBrowserDialog1.SelectedPath;
                            break;
                        case "btnPastaRelatorio": txtCaminhoPastaRelatorio.Text = folderBrowserDialog1.SelectedPath;
                            break;
                        case "btnCaminhoPadrao": txtCaminhoPadrao.Text = folderBrowserDialog1.SelectedPath;
                            break;
                        case "btnPastaCCe": txtCCe.Text = folderBrowserDialog1.SelectedPath;
                            break;
                    }
                }
            }
            else
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    switch (btn.UniqueName)
                    {
                        case "btnBancoDados": txtBancoDados.Text = openFileDialog1.FileName.ToString();
                            break;
                        case "btnLogo": txtCaminhoLogo.Text = openFileDialog1.FileName.ToString();
                            break;
                        //Claudinei - o.s. 24085 - 23/02/2010
                        case "btnBancoEscritor": txtBancoEscritor.Text = openFileDialog1.FileName.ToString();
                            break;
                        //Fim - Claudinei - o.s. 24085 - 23/02/2010

                    }
                }
            }
        }

        private void frmConfiguracao_FormClosing(object sender, FormClosingEventArgs e)
        {
            errorProvider1.Clear();
            if (verificaPastasPreenchidas() == 0)
            {

                if (txtCaminhoPadrao.Text != "")
                {
                    belStaticPastas.CAMINHO = txtCaminhoPadrao.Text.Trim();
                }
                SalvarXml();
                KryptonMessageBox.Show("Todas as alterações foram salvas.", "S A L V A R", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (belStatic.BSemArquivo == true)
                {
                    belStatic.BSemArquivo = false;
                    belStatic.IPrimeiroLoad = 1;
                    frmSelecionaConfigs objFrmSeleciona = new frmSelecionaConfigs();
                    objFrmSeleciona.ShowDialog();

                    frmLogin objfrm = new frmLogin();
                    objfrm.ShowDialog();
                    belStatic.IPrimeiroLoad = 0;
                }
                HLP.bel.NFe.belConfigInicial.CarregaConfiguracoesIniciais();
                Globais LeRegWin = new Globais();
                LeRegWin.CarregaInfStaticas(); // INICIALIZA AS PASTAS PADRÕES
            }
            else
            {
                KryptonMessageBox.Show(null, "Os Campos em Alerta são Obrigatórios para o Parametro!! ", "A L E R T A", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Cancel = true;
            }

            // Diego OS-24205 - 05/03/2010 - FIM
        }

        // Diego OS-24205 - 05/03/2010
        private int verificaPastasPreenchidas()
        {
            int count = 0;

            if (txtCaminhoPadrao.Text.Trim() == "")
            {
                DirectoryInfo info = new DirectoryInfo((this.txtEnvioArquivo.Text == "" ? "C:\\GERAHLPERRO" : this.txtEnvioArquivo.Text));
                DirectoryInfo info4 = new DirectoryInfo((this.txtXmlEnviados.Text == "" ? "C:\\GERAHLPERRO" : this.txtXmlEnviados.Text));
                DirectoryInfo info5 = new DirectoryInfo((this.txtXmlCancelados.Text == "" ? "C:\\GERAHLPERRO" : this.txtXmlCancelados.Text));
                DirectoryInfo info6 = new DirectoryInfo((this.txtProtocolo.Text == "" ? "C:\\GERAHLPERRO" : this.txtProtocolo.Text));
                DirectoryInfo info7 = new DirectoryInfo((this.txtContingencia.Text == "" ? "C:\\GERAHLPERRO" : this.txtContingencia.Text));
                DirectoryInfo info8 = new DirectoryInfo((this.txtCaminhoBarrasTemp.Text == "" ? "C:\\GERAHLPERRO" : this.txtCaminhoBarrasTemp.Text));
                DirectoryInfo info9 = new DirectoryInfo((this.txtCCe.Text == "" ? "C:\\GERAHLPERRO" : this.txtCCe.Text));



                if (!info.Exists)
                {
                    KryptonMessageBox.Show("Pasta ENVIO dos arquivos Xml informada n\x00e3o existe.", "Advert\x00eancia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    errorProvider1.SetError(txtEnvioArquivo, "Caminho Obrigatório");
                    count++;
                }
                else if (!info4.Exists)
                {
                    KryptonMessageBox.Show("Pasta ENVIADOS dos arquivos Xml informada n\x00e3o existe.", "Advert\x00eancia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    errorProvider1.SetError(txtXmlEnviados, "Caminho Obrigatório");
                    count++;
                }
                else if (!info5.Exists)
                {
                    KryptonMessageBox.Show("Pasta CANCELADOS dos arquivos Xml informada n\x00e3o existe.", "Advert\x00eancia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    errorProvider1.SetError(txtXmlCancelados, "Caminho Obrigatório");
                    count++;
                }
                else if (!info6.Exists)
                {
                    KryptonMessageBox.Show("Pasta PROTOCOLOS dos arquivos Xml informada n\x00e3o existe.", "Advert\x00eancia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    errorProvider1.SetError(txtProtocolo, "Caminho Obrigatório");
                    count++;
                }
                else if (!info7.Exists)
                {
                    KryptonMessageBox.Show("Pasta CONTINGENCIA dos arquivos Xml n\x00e3o existe.", "Advert\x00eancia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    errorProvider1.SetError(txtContingencia, "Caminho Obrigatório");
                    count++;
                }
                else if (!info8.Exists)
                {
                    KryptonMessageBox.Show("A pasta do Codigo de Barras Temp. n\x00e3o existe.", "Advert\x00eancia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    errorProvider1.SetError(txtCaminhoBarrasTemp, "Caminho Obrigatório");
                    count++;
                }
                else if (!info9.Exists)
                {
                    KryptonMessageBox.Show("Pasta CCe dos arquivos Xmls enviados informada n\x00e3o existe.", "Advert\x00eancia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    errorProvider1.SetError(txtCCe, "Caminho Obrigatório");
                    count++;
                }
                else if (cbxGruposServico.Items.Count > 0)
                {
                    if (cbxGruposServico.SelectedIndex == -1)
                    {
                        KryptonMessageBox.Show("Selecione um Grupo de Faturamento Padrão para Serviço", "Advert\x00eancia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        errorProvider1.SetError(cbxGruposServico, "Campo Obrigatório");
                        count++;
                    }
                }
                if (chkRelatorio.Checked)
                {
                    if (txtCaminhoPastaRelatorio.Text.Trim().Equals(""))
                    {
                        errorProvider1.SetError(txtCaminhoPastaRelatorio, "Caminho Obrigatório");
                        count++;
                    }

                    else
                    {
                        DirectoryInfo inf = new DirectoryInfo(txtCaminhoPastaRelatorio.Text.Trim());
                        if (!inf.Exists)
                        {
                            KryptonMessageBox.Show("A pasta do Caminho dos Relatórios Temp. n\x00e3o existe.", "Advert\x00eancia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            errorProvider1.SetError(txtCaminhoPastaRelatorio, "Caminho Obrigatório");
                            count++;
                        }
                    }

                }
            }
            else
            {
                DirectoryInfo inf = new DirectoryInfo(txtCaminhoPadrao.Text.Trim());
                if (!inf.Exists)
                {
                    KryptonMessageBox.Show("A pasta do Caminho Padrão Temp. n\x00e3o existe.", "Advert\x00eancia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    errorProvider1.SetError(txtCaminhoPadrao, "Pasta Inexistente");
                    count++;
                }
            }

            return count;
        }
        // Diego OS-24205 - 05/03/2010 - FIM

        private void SalvarXml()
        {
            if (verificaPastasPreenchidas() == 0)
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.IndentChars = "";
                settings.NewLineOnAttributes = false;
                settings.OmitXmlDeclaration = false;

                if ((File.Exists(belStatic.Pasta_xmls_Configs + spath)))
                    File.Delete(belStatic.Pasta_xmls_Configs + spath);

                XmlWriter writer = XmlWriter.Create(belStatic.Pasta_xmls_Configs + spath, settings);
                writer.WriteStartDocument();
                writer.WriteStartElement("nfe_configuracoes");
                writer.WriteElementString("PastaXmlEnvio", this.txtEnvioArquivo.Text);
                writer.WriteElementString("PastaXmlEnviado", this.txtXmlEnviados.Text);
                writer.WriteElementString("PastaXmlCancelados", this.txtXmlCancelados.Text);
                writer.WriteElementString("PastaProtocolos", this.txtProtocolo.Text);
                writer.WriteElementString("PastaContingencia", this.txtContingencia.Text);
                writer.WriteElementString("PastaArquivosEscritor", this.txtEscrituracao.Text.ToString().Trim());
                writer.WriteElementString("PastaCCe", this.txtCCe.Text.ToString().Trim());
                writer.WriteElementString("UsaRelatorioEspecifico", this.chkRelatorio.Checked.ToString());
                writer.WriteElementString("CaminhoRelatorioEspecifico", this.txtCaminhoPastaRelatorio.Text.Trim());
                writer.WriteElementString("CaminhoPadrao", this.txtCaminhoPadrao.Text.ToString().Trim());


                writer.WriteElementString("BancoDados", this.txtBancoDados.Text);
                writer.WriteElementString("Logotipo", this.txtCaminhoLogo.Text);
                writer.WriteElementString("QtdeCasasProdutos", this.nupCasasQtdeProd.Value.ToString());
                writer.WriteElementString("QtdeCasasVlUnit", this.nudQtd_Vl_unitario.Value.ToString());
                writer.WriteElementString("CodBarras", this.txtCaminhoBarrasTemp.Text);
                writer.WriteElementString("AtivaModuloScan", this.chkModuloScan.Checked.ToString());
                writer.WriteElementString("GravaNumNFseDupl", this.chkGravaNumNFseDup.Checked.ToString());
                writer.WriteElementString("SerieScan", this.nudScan.Text);
                writer.WriteElementString("AtivaModuloContingencia", this.chkContingencia.Checked.ToString());
                writer.WriteElementString("VisualizaHoraDanfe", this.ckbHoraSaidaDanfe.Checked.ToString());
                writer.WriteElementString("VisualizaDataDanfe", this.ckbDataSaidaDanfe.Checked.ToString());
                writer.WriteElementString("UsaDanfeSimplificada", this.ckbDanfeSimplificada.Checked.ToString());
                //cbxFusoHorario
                if (cbxFusoHorario.Text != "")
                {
                    writer.WriteElementString("Fuso", cbxFusoHorario.Text.ToString());
                }
                else
                {
                    writer.WriteElementString("Fuso", "-02:00");
                }

                writer.WriteElementString("CodBarrasXml", this.chkCodBarras.Checked.ToString()); // 24/08/10
                writer.WriteElementString("TotalizaCFOP", this.chkTotCfop.Checked.ToString()); // OS_25021
                writer.WriteElementString("Servidor", this.txtServidor.Text);
                writer.WriteElementString("Porta", this.txtPorta.Text.Trim());
                writer.WriteElementString("Usuario", this.txtUsuario.Text);
                writer.WriteElementString("Senha", this.txtSenha.Text);
                writer.WriteElementString("Empresa", this.txtEmpresa.Text);
                writer.WriteElementString("RequerSSL", this.ckbRequerSSL.Checked.ToString());
                writer.WriteElementString("DestacaImpTribMun", this.chkTribMunicipio.Checked.ToString());
                writer.WriteElementString("QtdeTentativas", this.nudQtdeTentativas.Value.ToString());
                if (cbxGruposServico.Items.Count > 0)
                {
                    if (cbxGruposServico.SelectedValue != null)
                    {
                        writer.WriteElementString("GrupoServico", cbxGruposServico.SelectedValue.ToString());
                    }
                }
                writer.WriteElementString("TipoImpressao", cbxFormDanfe.Text.ToString());
                writer.WriteElementString("HostServidor", this.txtHostServidor.Text.Trim());
                writer.WriteElementString("PortaServidor", this.numericUpDown1.Value.ToString().Trim());
                writer.WriteElementString("EmailRemetente", this.txtEmailRemetente.Text.Trim());
                writer.WriteElementString("SenhaRemetente", this.txtSenhaEmail.Text.Trim());
                writer.WriteElementString("EmailAutomatico", this.ckbEmailAutomatico.Checked.ToString());
                writer.WriteElementString("Thread", "0");
                writer.WriteElementString("Industrializacao", this.cbxIndustrializacao.Text[0].ToString());
                writer.WriteElementString("BancoEscritor", this.txtBancoEscritor.Text.ToString().Trim());
                writer.WriteElementString("ServidorEscritor", this.txtServidorEscritor.Text.ToString().Trim());
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
                writer.Close();
                base.Dispose();
            }
        }

        private void populaComboGruposFat()
        {
            try
            {
                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("select hlpstatus.ds_descvalor, hlpstatus.ds_valor FROM hlpstatus ");
                sQuery.Append("where hlpstatus.ds_referencia = 'CD_GRUPONF'");

                cx = new belConnection();
                FbCommand command = new FbCommand(sQuery.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                command.ExecuteNonQuery();
                FbDataReader dr = command.ExecuteReader();

                List<GruposFaturamento> objLista = new List<GruposFaturamento>();
                while (dr.Read())
                {
                    objLista.Add(new GruposFaturamento
                    {
                        ds_descvalor = dr["ds_valor"].ToString() + " - " + dr["ds_descvalor"].ToString(),
                        ds_valor = dr["ds_valor"].ToString()
                    });
                }
                cbxGruposServico.DataSource = objLista;
                cbxGruposServico.SelectedIndex = -1;
            }
            catch (Exception)
            {
                throw;
            }
            finally { cx.Close_Conexao(); }
        }

        public void CarregarDados()
        {
            if (File.Exists(belStatic.Pasta_xmls_Configs + spath))
            {
                if (!spath.ToUpper().Equals("ESCRITA.XML"))
                {
                    try
                    {
                        populaComboGruposFat();
                        StringBuilder sSql = new StringBuilder();
                        sSql.Append("Select ");
                        sSql.Append("gen_id(gen_nomearqxml, 0) ");
                        sSql.Append("from rdb$database");
                        cx = new belConnection();
                        using (FbCommand cmd = new FbCommand(sSql.ToString(), cx.get_Conexao()))
                        {
                            cx.Open_Conexao();
                            lblAtual.Text = cmd.ExecuteScalar().ToString();
                        }
                    }
                    catch (FbException ex)
                    {
                        KryptonMessageBox.Show(string.Format("Erro.: {0}", ex.Message), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblAtual.Text = "Generator Inexistente!";
                    }
                    finally { cx.Close_Conexao(); }
                }

                XmlTextReader reader = new XmlTextReader(belStatic.Pasta_xmls_Configs + spath);
                cbxFormDanfe.SelectedIndex = 0;
                while (reader.Read())
                {
                    if ((reader.NodeType != XmlNodeType.Element) || !(reader.Name == "nfe_configuracoes"))
                    {
                        continue;
                    }
                    #region Campos
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            if (reader.Name == "PastaXmlEnvio")
                            {
                                reader.Read();
                                txtEnvioArquivo.Text = reader.Value;
                            }
                            else
                            {
                                if (reader.Name == "PastaXmlEnviado")
                                {
                                    reader.Read();
                                    txtXmlEnviados.Text = reader.Value;
                                    continue;
                                }
                                //Danner - o.s. 23851 - 17/11/2009
                                if (reader.Name == "PastaXmlCancelados")
                                {
                                    reader.Read();
                                    txtXmlCancelados.Text = reader.Value;
                                    continue;
                                }
                                if (reader.Name == "PastaProtocolos")
                                {
                                    reader.Read();
                                    txtProtocolo.Text = reader.Value;
                                    continue;
                                }
                                if (reader.Name == "PastaContingencia")
                                {
                                    reader.Read();
                                    txtContingencia.Text = reader.Value;
                                    continue;
                                }
                                if (reader.Name == "PastaCCe")
                                {
                                    reader.Read();
                                    txtCCe.Text = reader.Value;
                                    continue;
                                }
                                if (reader.Name == "CaminhoPadrao")
                                {
                                    reader.Read();
                                    txtCaminhoPadrao.Text = reader.Value;
                                    continue;
                                }
                                if (reader.Name == "UsaRelatorioEspecifico")
                                {
                                    reader.Read();
                                    chkRelatorio.Checked = Convert.ToBoolean(reader.Value);
                                    continue;
                                }
                                if (reader.Name == "UsaDanfeSimplificada")
                                {
                                    reader.Read();
                                    ckbDanfeSimplificada.Checked = Convert.ToBoolean(reader.Value);
                                    continue;
                                }
                                if (reader.Name == "CaminhoRelatorioEspecifico")
                                {
                                    reader.Read();
                                    txtCaminhoPastaRelatorio.Text = reader.Value;
                                    continue;
                                }



                                //Claudinei - o.s. 23507 - 08/06/2009
                                if (reader.Name == "BancoDados")
                                {
                                    reader.Read();
                                    txtBancoDados.Text = reader.Value;
                                    continue;
                                }

                                if (reader.Name == "Servidor")
                                {
                                    reader.Read();
                                    txtServidor.Text = reader.Value;
                                    continue;
                                }
                                if (reader.Name == "Porta")
                                {
                                    reader.Read();
                                    txtPorta.Text = reader.Value;
                                    continue;
                                }

                                if (reader.Name == "Usuario")
                                {
                                    reader.Read();
                                    txtUsuario.Text = reader.Value;
                                    continue;
                                }

                                if (reader.Name == "Senha")
                                {
                                    reader.Read();
                                    txtSenha.Text = reader.Value;
                                    continue;
                                }
                                if (reader.Name == "Empresa")
                                {
                                    reader.Read();
                                    txtEmpresa.Text = reader.Value;
                                    continue;
                                }
                                //Danner - o.s. 23851 - 19/11/2009
                                if (reader.Name == "QtdeTentativas")
                                {
                                    reader.Read();
                                    nudQtdeTentativas.Value = Convert.ToInt16(reader.Value);
                                    continue;
                                }
                                if (reader.Name == "GravaNumNFseDupl")
                                {
                                    reader.Read();
                                    chkGravaNumNFseDup.Checked = Convert.ToBoolean(reader.Value);
                                    continue;
                                }
                                if (reader.Name == "GrupoServico")
                                {
                                    reader.Read();
                                    cbxGruposServico.SelectedValue = reader.Value.ToString();
                                    continue;
                                }

                                if (reader.Name == "Fuso")
                                {
                                    reader.Read();
                                    cbxFusoHorario.SelectedItem = reader.Value.ToString();
                                    continue;
                                }
                                if (reader.Name == "TipoImpressao")
                                {
                                    reader.Read();
                                    cbxFormDanfe.Text = reader.Value.ToString();
                                    continue;
                                }
                                if (reader.Name == "HostServidor")
                                {
                                    reader.Read();
                                    txtHostServidor.Text = reader.Value;
                                    continue;
                                }
                                if (reader.Name == "PortaServidor")
                                {
                                    reader.Read();
                                    numericUpDown1.Value = Convert.ToInt16(reader.Value);
                                    continue;
                                }

                                if (reader.Name == "EmailRemetente")
                                {
                                    reader.Read();
                                    txtEmailRemetente.Text = reader.Value;
                                    continue;
                                }
                                if (reader.Name == "SenhaRemetente")
                                {
                                    reader.Read();
                                    txtSenhaEmail.Text = reader.Value;
                                    continue;
                                }
                                //Danner - o.s. 24329 - 08/04/2010
                                if (reader.Name == "RequerSSL")
                                {
                                    reader.Read();
                                    ckbRequerSSL.Checked = (reader.Value == "True" ? true : false);
                                    continue;
                                }
                                //Fim - Danner - o.s. 24329 - 08/04/2010
                                if (reader.Name == "EmailAutomatico")
                                {
                                    reader.Read();
                                    ckbEmailAutomatico.Checked = (reader.Value == "True" ? true : false);
                                    continue;
                                }

                                if (reader.Name == "DestacaImpTribMun")
                                {
                                    reader.Read();
                                    chkTribMunicipio.Checked = (reader.Value == "True" ? true : false);
                                    continue;
                                }

                                if (reader.Name == "CodBarras")
                                {
                                    reader.Read();
                                    txtCaminhoBarrasTemp.Text = reader.Value;
                                    continue;
                                }

                                //Diego - o.s. 23723 - 06/11/2009
                                if (reader.Name == "Logotipo")
                                {
                                    reader.Read();
                                    txtCaminhoLogo.Text = reader.Value;
                                    continue;
                                }
                                //Fim - o.s. 23723 - 06/11/2009

                                // Diego - OS_24178 - 25/02/2010
                                if (reader.Name == "QtdeCasasProdutos")
                                {
                                    reader.Read();
                                    nupCasasQtdeProd.Value = Convert.ToInt16(reader.Value);
                                    continue;
                                }
                                if (reader.Name == "QtdeCasasVlUnit")
                                {
                                    reader.Read();
                                    nudQtd_Vl_unitario.Value = (Convert.ToInt16(reader.Value) > 1 ? Convert.ToInt16(reader.Value) : 2);
                                    continue;
                                }
                                // Diego - OS_24178 - 25/02/2010 - Fim

                                // Diego - O.S 24344 - 05/05/2010 
                                if (reader.Name == "VisualizaHoraDanfe")
                                {
                                    reader.Read();
                                    ckbHoraSaidaDanfe.Checked = (reader.Value == "True" ? true : false);
                                    continue;
                                }
                                if (reader.Name == "VisualizaDataDanfe")
                                {
                                    reader.Read();
                                    ckbDataSaidaDanfe.Checked = (reader.Value == "True" ? true : false);
                                    continue;
                                }
                                if (reader.Name == "CodBarrasXml")
                                {
                                    reader.Read();
                                    chkCodBarras.Checked = (reader.Value == "True" ? true : false);
                                    continue;
                                }
                                if (reader.Name == "TotalizaCFOP") //OS_25021
                                {
                                    reader.Read();
                                    chkTotCfop.Checked = (reader.Value == "True" ? true : false);
                                    continue;

                                }
                                if (reader.Name == "AtivaModuloScan")
                                {
                                    reader.Read();
                                    chkModuloScan.Checked = (reader.Value == "True" ? true : false);
                                    if (chkModuloScan.Checked)
                                    {
                                        nudScan.Enabled = true;
                                    }
                                    else
                                    {
                                        nudScan.Enabled = false;
                                    }
                                    continue;
                                } if (reader.Name == "SerieScan")
                                {
                                    reader.Read();
                                    nudScan.Value = Convert.ToDecimal(reader.Value);
                                    continue;
                                } if (reader.Name == "AtivaModuloContingencia")
                                {
                                    reader.Read();
                                    chkContingencia.Checked = (reader.Value == "True" ? true : false);
                                    continue;
                                }
                                if (reader.Name == "PastaArquivosEscritor")
                                {
                                    reader.Read();
                                    txtEscrituracao.Text = reader.Value.ToString().Trim();
                                }
                                if (reader.Name == "BancoEscritor")
                                {
                                    reader.Read();
                                    txtBancoEscritor.Text = reader.Value.ToString().Trim();
                                }
                                if (reader.Name == "ServidorEscritor")
                                {
                                    reader.Read();
                                    txtServidorEscritor.Text = reader.Value.ToString().Trim();
                                }
                            }
                        }
                    }
                    #endregion
                    reader.Close();
                }
            }

            if (cbxFusoHorario.Text == "")
            {
                cbxFusoHorario.SelectedItem = "-02:00";
            }

            // Diego - OS-24566 - 18/06/2010
            cbxIndustrializacao.SelectedIndex = 1;
            // Diego - OS-24566 - 18/06/2010 - FIM
        }

        private void frmConfiguracao_Load(object sender, EventArgs e)
        {
            CarregarDados();
        }

        private void btnAtualiza_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder sSql = new StringBuilder();
                sSql.Append("SET GENERATOR gen_nomearqxml TO ");
                sSql.Append(nudNovo_Lote.Value.ToString().Trim());
                cx = new belConnection();
                using (FbCommand cmd = new FbCommand(sSql.ToString(), cx.get_Conexao()))
                {
                    cx.Open_Conexao();
                    cmd.ExecuteNonQuery();
                }
                CarregarDados();
            }
            catch (Exception)
            {
                throw;
            }
            finally { cx.Close_Conexao(); }

        }

        private void chkModuloScan_CheckedChanged(object sender, EventArgs e)
        {
            KryptonCheckBox chk = (KryptonCheckBox)sender;

            if (chk.Name.ToString().Equals("chkModuloScan"))
            {

                if (chk.Checked)
                {
                    if (chkContingencia.Checked == false)
                    {
                        nudScan.Enabled = true;
                    }
                    else
                    {
                        KryptonMessageBox.Show(null, "O Sistema já está Configurado para o Módulo de Contingência."
                                        + Environment.NewLine
                                        + Environment.NewLine
                                        + "Caso queira Ativar o Módulo SCAN, primeiro desmarque a Opção de Contingência logo abaixo!", "I N F O R M Ç Ã O", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        chk.Checked = false;
                    }
                }
                else
                {
                    nudScan.Enabled = false;
                }
            }
            else if (chk.Name.ToString().Equals("chkContingencia"))
            {
                if (chk.Checked)
                {
                    if (chkModuloScan.Checked)
                    {
                        KryptonMessageBox.Show(null, "O Sistema já está Configurado para o Módulo de SCAN."
                                       + Environment.NewLine
                                       + Environment.NewLine
                                       + "Caso queira Ativar o Módulo de Contingência, primeiro desmarque a Opção de SCAN logo Acima!", "I N F O R M Ç Ã O", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        chk.Checked = false;
                    }
                }

            }
        }

        private void chkRelatorio_CheckedChanged(object sender, EventArgs e)
        {
            btnPastaRelatorio.Enabled = chkRelatorio.Checked == true ? ButtonEnabled.True : ButtonEnabled.False;
            txtCaminhoPastaRelatorio.Enabled = chkRelatorio.Checked;
        }

        private void label13_Paint(object sender, PaintEventArgs e)
        {

        }

        private void nudQtdeTentativas_ValueChanged(object sender, EventArgs e)
        {

        }

        private void frmConfiguracao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }


    }
}
