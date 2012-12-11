using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using HLP.WebService.Itu_servicos_Producao;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml;
using System.IO;
using FirebirdSql.Data.FirebirdClient;
using System.Windows.Forms;
using HLP.bel.Static;
using HLP.bel.NFe.GeraXml;
using HLP.bel.CTe;
using ComponentFactory.Krypton.Toolkit;

namespace HLP.bel.NFes
{
    public class belRecepcao
    {
        private string sLoteXml;
        public X509Certificate2 cert;
        private tcLoteRps objLoteRpsAlter; // Objteto para guardar todas as notas do lote
        private TcInfNfse objNfseRetorno; // objteto para guardar o retorno da nota

        public List<TcInfNfse> objListaNfseRetorno = new List<TcInfNfse>(); //todas as notas que retornaram 
        public string sCodigoRetorno = "";
        public string NumeroLote { get; set; }
        public string Protocolo { get; set; }

        public string sMsgTransmissao { get; set; }

        public belRecepcao() { }

        public belRecepcao(string sLoteXml, X509Certificate2 cert, bel.NFes.tcLoteRps objLoteRpsAlter)
        {
            this.sLoteXml = sLoteXml;
            this.cert = cert;
            this.objLoteRpsAlter = objLoteRpsAlter;

            TransmitirLote();
        }

        private void TransmitirLote()
        {
            string sRet = "";
            sMsgTransmissao = "";
            try
            {
                //Homologação
                if (belStatic.tpAmbNFse == 2)
                {
                    HLP.WebService.Itu_servicos_Homologacao.ServiceGinfesImplService objtrans = new HLP.WebService.Itu_servicos_Homologacao.ServiceGinfesImplService();
                    objtrans.ClientCertificates.Add(cert);
                    objtrans.Timeout = 60000;
                    sRet = objtrans.RecepcionarLoteRpsV3(NfeCabecMsg(), sLoteXml);
                }
                else if (belStatic.tpAmbNFse == 1)
                {
                    HLP.WebService.Itu_servicos_Producao.ServiceGinfesImplService objtrans = new HLP.WebService.Itu_servicos_Producao.ServiceGinfesImplService();
                    objtrans.ClientCertificates.Add(cert);
                    objtrans.Timeout = 60000;
                    sRet = objtrans.RecepcionarLoteRpsV3(NfeCabecMsg(), sLoteXml);
                }
                else
                {
                    throw new Exception("Cadastro de Empresa não configurado para enviar NFe-serviço");
                }
                ConfiguraMsgdeTransmissao(sRet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ConfiguraMsgdeTransmissao(string sRet)
        {
            XmlDocument xmlRet = new XmlDocument();
            Globais glob = new Globais();
            xmlRet.LoadXml(sRet);

            XmlNodeList xNodeList = null;

            if (xmlRet.GetElementsByTagName("ns3:EnviarLoteRpsResposta").Count > 0)
            {
                xNodeList = xmlRet.GetElementsByTagName("ns3:EnviarLoteRpsResposta");
            }
            if (xmlRet.GetElementsByTagName("ns2:MensagemRetorno").Count > 0)
            {
                xNodeList = xmlRet.GetElementsByTagName("ns2:MensagemRetorno");
            }

            foreach (XmlNode node in xNodeList)
            {
                if (node["ns3:Protocolo"] != null)
                {
                    this.NumeroLote = node["ns3:NumeroLote"].InnerText;
                    this.Protocolo = node["ns3:Protocolo"].InnerText;

                    //Salva Protocolo do Lote
                    DirectoryInfo dPastaData = new DirectoryInfo(belStaticPastas.PROTOCOLOS + "\\Servicos\\");
                    if (!dPastaData.Exists) { dPastaData.Create(); }
                    xmlRet.Save(belStaticPastas.PROTOCOLOS + "\\Servicos\\" + "lote_" + this.NumeroLote.PadLeft(15, '0') + "_prot_" + this.Protocolo + ".xml");
                }
                else
                {
                    sMsgTransmissao = "{3}Código: {0}{3}{3}Mensagem: {1}{3}{3}Correção: {2}{3}";
                    sMsgTransmissao = string.Format(sMsgTransmissao, node["ns2:Codigo"].InnerText,
                                              node["ns2:Mensagem"].InnerText,
                                              node["ns2:Correcao"].InnerText, Environment.NewLine);

                }

            }
        }

        public string BuscaRetorno(tcIdentificacaoPrestador Prestador, KryptonLabel lblStatus, ProgressBar ProgresStatus)
        {

            bool parar = false;
            Globais glob = new Globais();
            string sMensagemErro = "";
            int iCountBuscaRet = 0;

            ProgresStatus.Step = 1;
            ProgresStatus.Minimum = 0;
            ProgresStatus.Maximum = 20;
            ProgresStatus.MarqueeAnimationSpeed = 20;
            ProgresStatus.Value = 0;

            try
            {
                for (; ; )
                {
                    ProgresStatus.PerformStep();
                    ProgresStatus.Refresh();
                    lblStatus.Text = "Sistema tentando buscar retorno!!" + Environment.NewLine + "Tentativas: " + iCountBuscaRet.ToString() + " de 21";
                    lblStatus.Refresh();
                    string sRetConsulta = BuscaRetornoWebService(Prestador);
                    XmlDocument xmlRet = new XmlDocument();
                    xmlRet.LoadXml(sRetConsulta);

                    XmlNodeList xNodeList = xmlRet.GetElementsByTagName("ns4:MensagemRetorno");

                    if (xNodeList.Count > 0)
                    {
                        sMensagemErro = "{3}Lote: " + NumeroLote + "{3}{3}Código: {0}{3}{3}Mensagem: {1}{3}{3}Correção: {2}{3}{3}Protocolo: " + Protocolo;

                        foreach (XmlNode node in xNodeList)
                        {
                            sCodigoRetorno = node["ns4:Codigo"].InnerText;

                            if (sCodigoRetorno.Equals("E4") && iCountBuscaRet <= 20)
                            {
                                iCountBuscaRet++;
                            }
                            else
                            {
                                sMensagemErro = string.Format(sMensagemErro, node["ns4:Codigo"].InnerText,
                                                      "Esse RPS ainda não se encontra em nossa base de dados.",
                                                      node["ns4:Correcao"].InnerText, Environment.NewLine);
                                parar = true;
                            }
                        }
                    }
                    else if (xmlRet.GetElementsByTagName("ns3:CompNfse").Count > 0)
                    {
                        this.sCodigoRetorno = "";
                        sMensagemErro = "";
                        Globais objGlobais = new Globais();
                        bool bAlteraDupl = Convert.ToBoolean(objGlobais.LeRegConfig("GravaNumNFseDupl"));


                        for (int i = 0; i < xmlRet.GetElementsByTagName("ns3:CompNfse").Count; i++)
                        {
                            #region Salva Arquivo por arquivo
                            string sPasta = Convert.ToDateTime(xmlRet.GetElementsByTagName("ns4:InfNfse")[i]["ns4:DataEmissao"].InnerText).ToString("MM/yy").Replace("/", "");
                            //Numero da nota no sefaz + numero da sequencia no sistema
                            string sNomeArquivo = sPasta + (xmlRet.GetElementsByTagName("ns4:InfNfse")[i]["ns4:Numero"].InnerText.PadLeft(6, '0'))
                                                 + (xmlRet.GetElementsByTagName("ns4:IdentificacaoRps")[i]["ns4:Numero"].InnerText.PadLeft(6, '0'));

                            XmlDocument xmlSaveNfes = new XmlDocument();
                            xmlSaveNfes.LoadXml(xmlRet.GetElementsByTagName("ns4:Nfse")[i].InnerXml);
                            DirectoryInfo dPastaData = new DirectoryInfo(belStaticPastas.ENVIADOS + "\\Servicos\\" + sPasta);
                            if (!dPastaData.Exists) { dPastaData.Create(); }
                            xmlSaveNfes.Save(belStaticPastas.ENVIADOS + "\\Servicos\\" + sPasta + "\\" + sNomeArquivo + "-nfes.xml");
                            #endregion

                            objNfseRetorno = new TcInfNfse();
                            objNfseRetorno.Numero = xmlRet.GetElementsByTagName("ns4:InfNfse")[i]["ns4:Numero"].InnerText;
                            objNfseRetorno.CodigoVerificacao = xmlRet.GetElementsByTagName("ns4:InfNfse")[i]["ns4:CodigoVerificacao"].InnerText;

                            tcIdentificacaoRps objIdentRps = BuscatcIdentificacaoRps(xmlRet.GetElementsByTagName("ns4:IdentificacaoRps")[i]["ns4:Numero"].InnerText.PadLeft(6, '0'));
                            belGerarXML objBelGeraXml = new belGerarXML();


                            if (belStatic.sNomeEmpresa == "LORENZON")
                            {
                                AlteraDuplicataNumNFse(objIdentRps, xmlRet.GetElementsByTagName("ns4:InfNfse")[i]["ns4:Numero"].InnerText);
                            }

                            if (xmlRet.GetElementsByTagName("ns4:SubstituicaoNfse")[i] != null)
                            {
                                objNfseRetorno.NfseSubstituida = xmlRet.GetElementsByTagName("ns4:SubstituicaoNfse")[i]["ns4:NfseSubstituidora"].InnerText;
                            }
                            objNfseRetorno.IdentificacaoRps = objIdentRps;
                            objListaNfseRetorno.Add(objNfseRetorno);

                        }
                        parar = true;
                    }

                    if (parar) break;
                }
                return sMensagemErro;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AlteraDuplicataNumNFse(tcIdentificacaoRps objIdentRps, string sNotaFis)
        {
            Globais objGlobais = new Globais();
            belConnection cx = new belConnection();

            try
            {
                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("UPDATE dupnotar SET dupnotar.cd_notafis = '" + sNotaFis + "' ");
                sQuery.Append("where dupnotar.cd_empresa = '" + belStatic.codEmpresaNFe + "' ");
                sQuery.Append("and dupnotar.cd_nfseq = '" + objIdentRps.Nfseq + "' ");
                sQuery.Append("and dupnotar.cd_gruponf = '" + objGlobais.LeRegConfig("GrupoServico") + "'");

                FbCommand cmd = new FbCommand(sQuery.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            { cx.Close_Conexao(); }
        }






        public tcIdentificacaoRps BuscatcIdentificacaoRps(string sNotaFis)
        {
            belConnection cx = new belConnection();
            Globais objGlobais = new Globais();
            try
            {
                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("select nf.cd_nfseq, nf.cd_notafis, coalesce(nf.cd_serie,'00001')cd_serie from nf ");
                sQuery.Append("where nf.cd_notafis = '" + sNotaFis + "' and ");
                sQuery.Append("nf.cd_empresa = '" + belStatic.codEmpresaNFe + "'");
                sQuery.Append(" and coalesce(nf.st_nf_prod,'S') = 'N'");
                sQuery.Append(" and nf.cd_gruponf = '" + objGlobais.LeRegConfig("GrupoServico") + "'");

                FbCommand Comand = new FbCommand(sQuery.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                Comand.ExecuteNonQuery();
                FbDataReader dr = Comand.ExecuteReader();
                dr.Read();

                tcIdentificacaoRps objtcIdentificacaoRps = new tcIdentificacaoRps();
                objtcIdentificacaoRps.Nfseq = dr["cd_nfseq"].ToString();
                objtcIdentificacaoRps.Numero = dr["cd_notafis"].ToString();
                objtcIdentificacaoRps.Serie = dr["cd_serie"].ToString();

                return objtcIdentificacaoRps;

            }
            catch (Exception)
            {
                throw new Exception("O Grupo de faturamento da nota " + sNotaFis + " deve ser igual ao Grupo de faturamento parametrizado no Config.");
            }
            finally { cx.Close_Conexao(); }
        }

        private string BuscaRetornoWebService(tcIdentificacaoPrestador Prestador)
        {
            try
            {
                //Homologação
                if (belStatic.tpAmbNFse == 2)
                {
                    HLP.WebService.Itu_servicos_Homologacao.ServiceGinfesImplService objtrans = new HLP.WebService.Itu_servicos_Homologacao.ServiceGinfesImplService();
                    objtrans.ClientCertificates.Add(cert);
                    objtrans.Timeout = 60000;
                    return objtrans.ConsultarLoteRpsV3(NfeCabecMsg(), MontaXmlConsultaLote(Prestador));

                }
                else if (belStatic.tpAmbNFse == 1)
                {
                    HLP.WebService.Itu_servicos_Producao.ServiceGinfesImplService objtrans = new HLP.WebService.Itu_servicos_Producao.ServiceGinfesImplService();
                    objtrans.ClientCertificates.Add(cert);
                    objtrans.Timeout = 60000;
                    return objtrans.ConsultarLoteRpsV3(NfeCabecMsg(), MontaXmlConsultaLote(Prestador));
                }
                else
                {
                    throw new Exception("Cadastro de Empresa não configurado para enviar NFe-serviço");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string MontaXmlConsultaLote(tcIdentificacaoPrestador objPrestador)
        {
            XmlSchemaCollection myschema = new XmlSchemaCollection();
            XmlValidatingReader reader;
            try
            {
                XNamespace tipos = "http://www.ginfes.com.br/tipos_v03.xsd";
                XNamespace pf = "http://www.ginfes.com.br/servico_consultar_lote_rps_envio_v03.xsd";
                XContainer conPrestador = null;
                XContainer conProtocolo = null;

                XContainer conConsultarLoteRpsEnvio = (new XElement(pf + "ConsultarLoteRpsEnvio", new XAttribute("xmlns", "http://www.ginfes.com.br/servico_consultar_lote_rps_envio_v03.xsd"),
                                                                        new XAttribute(XNamespace.Xmlns + "tipos", "http://www.ginfes.com.br/tipos_v03.xsd")));

                conPrestador = (new XElement(pf + "Prestador",
                    new XElement(tipos + "Cnpj", objPrestador.Cnpj),
                                                                     ((objPrestador.InscricaoMunicipal != "") ? new XElement(tipos + "InscricaoMunicipal", objPrestador.InscricaoMunicipal) : null)));

                conProtocolo = new XElement(pf + "Protocolo", Protocolo);


                conConsultarLoteRpsEnvio.Add(conPrestador);
                conConsultarLoteRpsEnvio.Add(conProtocolo);
                AssinaNFeXml Assinatura = new AssinaNFeXml();
                string sArquivo = Assinatura.ConfigurarArquivo(conConsultarLoteRpsEnvio.ToString(), "Protocolo", cert);

                //Valida
                Globais getschema = new Globais();

                XmlParserContext context = new XmlParserContext(null, null, "", XmlSpace.None);

                reader = new XmlValidatingReader(sArquivo, XmlNodeType.Element, context);

                myschema.Add("http://www.ginfes.com.br/servico_consultar_lote_rps_envio_v03.xsd", belStaticPastas.SCHEMA_NFSE + "\\servico_consultar_lote_rps_envio_v03.xsd");

                reader.ValidationType = ValidationType.Schema;

                reader.Schemas.Add(myschema);

                while (reader.Read())
                { }




                return sArquivo;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private string NfeCabecMsg()
        {
            try
            {
                XNamespace ns2 = "http://www.ginfes.com.br/cabecalho_v03.xsd";
                XContainer xdoc = (new XElement(ns2 + "cabecalho", new XAttribute("versao", "3"), new XAttribute(XNamespace.Xmlns + "ns2", "http://www.ginfes.com.br/cabecalho_v03.xsd"),
                                              new XElement("versaoDados", "3")));


                //<ns2:cabecalho versao="3" xmlns:ns2="http://www.ginfes.com.br/cabecalho_v03.xsd">
                //    <versaoDados>3</versaoDados>
                //</ns2:cabecalho>

                XmlSchemaCollection myschema = new XmlSchemaCollection();
                XmlValidatingReader reader;

                Globais getschema = new Globais();

                XmlParserContext context = new XmlParserContext(null, null, "", XmlSpace.None);

                reader = new XmlValidatingReader(xdoc.ToString(), XmlNodeType.Element, context);

                myschema.Add("http://www.ginfes.com.br/cabecalho_v03.xsd", belStaticPastas.SCHEMA_NFSE + "\\cabecalho_v03.xsd");

                reader.ValidationType = ValidationType.Schema;

                reader.Schemas.Add(myschema);

                while (reader.Read())
                { }

                return xdoc.ToString();

            }
            catch (XmlException x)
            {

                throw new Exception(x.Message.ToString());
            }
            catch (XmlSchemaException x)
            {
                throw new Exception(x.Message.ToString());
            }
        }

        public string MontaMsgDeRetornoParaCliente()
        {
            try
            {
                string sMsgNota = "Nota nº {0}  <->  Seq. Sistema: {1}{2}";
                string sMsgFinal = "Notas de Serviço Enviadas com Sucesso: " + Environment.NewLine
                    + "______________________________________"
                    + Environment.NewLine + Environment.NewLine;

                for (int i = 0; i < objListaNfseRetorno.Count; i++)
                {
                    sMsgFinal += string.Format(sMsgNota,
                                                objListaNfseRetorno[i].Numero.ToString()
                                                , objListaNfseRetorno[i].IdentificacaoRps.Nfseq
                                                , Environment.NewLine);
                }
                return sMsgFinal;

            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}
