using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Schema;
using System.IO;
using HLP.bel.Static;
using HLP.bel.NFe.GeraXml;

namespace HLP.bel.NFes
{
    public class belCancelamentoNFse
    {
        public string cod { get; set; }
        public string msg { get; set; }
        public string solucao { get; set; }
        public bool bNotaCancelada = false;

        public List<belCancelamentoNFse> RetListaErros()
        {
            try
            {
                List<belCancelamentoNFse> objLista = new List<belCancelamentoNFse>();

                objLista.Add(new belCancelamentoNFse
                {
                    cod = "E1",
                    msg = "Assinatura do Hash não confere",
                    solucao = "Reenvie asssinatura do Hash conforme algoritmo estabelecido no Manual de Instrução da NFS-e"
                });
                objLista.Add(new belCancelamentoNFse
                {
                    cod = "E2",
                    msg = "Mês de competência superior ao de emissão do RPS ou da Nota",
                    solucao = "Informe um mês de competência inferior ou igual ao de emissão do RPS ou da Nota."
                });
                objLista.Add(new belCancelamentoNFse
                {
                    cod = "E3",
                    msg = "Natureza da operação não informada.",
                    solucao = "Utilize um dos tipos: 01 – Tributação no municipio; 02 – Tributação fora do municipio; 03 – Isenção; 04 – Imune; 05 – Exigibilidade suspensa por decisão judicial; 06 – Exigibilidade suspensa por procedimento administrativo."
                });
                objLista.Add(new belCancelamentoNFse
                {
                    cod = "E4",
                    msg = "Esse RPS não foi enviado para a nossa base de dados",
                    solucao = "Envie o RPS para emissão da NFS-e."
                });
                objLista.Add(new belCancelamentoNFse
                {
                    cod = "E5",
                    msg = "O número da NFS-E substituída informado não existe na base de dados do município.",
                    solucao = "Informe um número de NFS-E substituída que já tenha sido emitida."
                });
                objLista.Add(new belCancelamentoNFse
                {
                    cod = "E6",
                    msg = "Essa NFS-e não pode ser cancelada através desse serviço, pois há crédito informado",
                    solucao = "O cancelamento de uma NFS-e com crédito deve ser feito através de processo administrativo aberto em uma repartição fazendária."
                });
                objLista.Add(new belCancelamentoNFse
                {
                    cod = "E7",
                    msg = "Essa NFS-e já foi substituída",
                    solucao = "Confira e informe novamente os dados da NFS-e que deseja substituir."
                });
                objLista.Add(new belCancelamentoNFse
                {
                    cod = "E8",
                    msg = "Campo de optante pelo simples nacional não informado",
                    solucao = "Utilize um dos tipos: 1 – Sim; 2 - Não."
                });
                objLista.Add(new belCancelamentoNFse
                {
                    cod = "E9",
                    msg = "Campo de incentivador cultural não informado",
                    solucao = "Utilize um dos tipos: 1 – Sim; 2 - Não"
                });
                objLista.Add(new belCancelamentoNFse
                {
                    cod = "E10",
                    msg = "RPS já informado.",
                    solucao = "Para essa Inscrição Municipal/CNPJ já existe um RPS informado com o mesmo número, série e tipo."
                });
                objLista.Add(new belCancelamentoNFse
                {
                    cod = "E11",
                    msg = "Número do RPS não informado",
                    solucao = "Informe o número do RPS"
                });
                objLista.Add(new belCancelamentoNFse
                {
                    cod = "E12",
                    msg = "Tipo do RPS não informado",
                    solucao = "Informe o tipo do RPS"
                });


                return objLista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string CancelaNfes(TcPedidoCancelamento objPedCanc, X509Certificate2 cert)
        {
            try
            {
                string sRet = "";

                //Homologação
                if (belStatic.tpAmbNFse == 2)
                {
                    HLP.WebService.Itu_servicos_Homologacao.ServiceGinfesImplService objtrans = new HLP.WebService.Itu_servicos_Homologacao.ServiceGinfesImplService();
                    objtrans.ClientCertificates.Add(cert);
                    objtrans.Timeout = 60000;
                    sRet = objtrans.CancelarNfse(MontaXmlCancelamentoHomo(objPedCanc, cert));
                }
                else if (belStatic.tpAmbNFse == 1)
                {
                    HLP.WebService.Itu_servicos_Producao.ServiceGinfesImplService objtrans = new HLP.WebService.Itu_servicos_Producao.ServiceGinfesImplService();
                    objtrans.ClientCertificates.Add(cert);
                    objtrans.Timeout = 60000;
                    sRet = objtrans.CancelarNfse(MontaXmlCancelamento(objPedCanc, cert));
                    // sRet = objtrans.CancelarNfse(MontaXmlCancelamentoV3(objPedCanc, cert));

                }
                else
                {
                    throw new Exception("Cadastro de Empresa não configurado para enviar NFe-serviço");
                }
                //string sMsg = ConfiguraMsgRetornoCancelamento(sRet);
                // return sMsg;
                return sRet;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string ConfiguraMsgRetornoCancelamento(string sRet)
        {
            try
            {
                string sMsg = "";
                XmlDocument xmlRet = new XmlDocument();
                xmlRet.LoadXml(sRet);

                XmlNodeList xNodeListns2 = xmlRet.GetElementsByTagName("ns2:MensagemRetorno");
                XmlNodeList xNodeList = xmlRet.GetElementsByTagName("MensagemRetorno");
                XmlNodeList xNodeSucesso = xmlRet.GetElementsByTagName("ns2:Sucesso");
                if (xNodeSucesso.Count >0)
                {
                    if (xmlRet.GetElementsByTagName("ns2:Sucesso")[0].InnerText.Equals("true"))
                    {
                        bNotaCancelada = true;
                        sMsg = "{2}Sucesso: {0}{2}{2}Mensagem: {1}{2}{2}";
                        sMsg = string.Format(sMsg, xmlRet.GetElementsByTagName("ns2:Sucesso")[0].InnerText,
                                  xNodeListns2[0]["ns3:Mensagem"].InnerText,
                                  Environment.NewLine);

                    }                    
                }               
                else if (xNodeListns2.Count > 0)
                {
                    sMsg = "{3}Código: {0}{3}{3}Mensagem: {1}{3}{3}Correção: {2}{3}";
                    sMsg = string.Format(sMsg, xNodeListns2[0]["ns3:Codigo"].InnerText,
                              xNodeListns2[0]["ns3:Mensagem"].InnerText,
                              xNodeListns2[0]["ns3:Correcao"].InnerText, Environment.NewLine);
                }
                else if (xNodeList.Count > 0)
                {
                    sMsg = "{3}Código: {0}{3}{3}Mensagem: {1}{3}{3}Correção: {2}{3}";
                    sMsg = string.Format(sMsg, xNodeList[0]["Codigo"].InnerText,
                              xNodeList[0]["Mensagem"].InnerText,
                              xNodeList[0]["Correcao"].InnerText, Environment.NewLine);
                }
                return sMsg;

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        private string MontaXmlCancelamento(TcPedidoCancelamento objPedCanc, X509Certificate2 cert)
        {
            try
            {
                //    XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
                //    XNamespace tns = "http://www.ginfes.com.br/servico_cancelar_nfse_envio";
                //    XNamespace tipos = "http://www.ginfes.com.br/tipos";
                //    XNamespace ds = "http://www.w3.org/2000/09/xmldsig#";

                //    XContainer conCancelarNfseEnvio = null;
                //    conCancelarNfseEnvio = (new XElement(tns + "CancelarNfseEnvio", new XAttribute(xsi + "schemaLocation", "http://www.ginfes.com.br/servico_cancelar_nfse_envio servico_cancelar_nfse_envio_v02.xsd"),
                //                                                                   new XAttribute(XNamespace.Xmlns + "tns", "http://www.ginfes.com.br/servico_cancelar_nfse_envio"),
                //                                                                   new XAttribute(XNamespace.Xmlns + "tipos", "http://www.ginfes.com.br/tipos"),
                //                                                                   new XAttribute(XNamespace.Xmlns + "ds", "http://www.w3.org/2000/09/xmldsig#"),
                //                                                                   new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                //                                      new XElement(tipos + "Prestador", new XElement(tns + "Cnpj", objPedCanc.InfPedidoCancelamento.IdentificacaoNfse.Cnpj)),
                //                            new XElement(tipos + "NumeroNfse", objPedCanc.InfPedidoCancelamento.IdentificacaoNfse.Numero)));





                XNamespace pf = "http://www.ginfes.com.br/servico_cancelar_nfse_envio";
                XNamespace tipos = "http://www.ginfes.com.br/tipos";
                XContainer conCancelarNfseEnvio = null;
                conCancelarNfseEnvio = (new XElement(pf + "CancelarNfseEnvio", new XAttribute("xmlns", "http://www.ginfes.com.br/servico_cancelar_nfse_envio"),
                                            new XAttribute(XNamespace.Xmlns + "tipos", "http://www.ginfes.com.br/tipos"),
                                        new XElement(pf + "Prestador", new XElement(tipos + "Cnpj", objPedCanc.InfPedidoCancelamento.IdentificacaoNfse.Cnpj),
                                                                      ((objPedCanc.InfPedidoCancelamento.IdentificacaoNfse.InscricaoMunicipal != "") ? new XElement(tipos + "InscricaoMunicipal", objPedCanc.InfPedidoCancelamento.IdentificacaoNfse.InscricaoMunicipal) : null)),
                                        new XElement(pf + "NumeroNfse", objPedCanc.InfPedidoCancelamento.IdentificacaoNfse.Numero)));

                //Valida

                AssinaNFeXml Assinatura = new AssinaNFeXml();
                string sArquivo = Assinatura.ConfigurarArquivo(conCancelarNfseEnvio.ToString(), "NumeroNfse", cert);

                Globais glob = new Globais();
                DirectoryInfo dPastaData = new DirectoryInfo(belStaticPastas.PROTOCOLOS + "\\Servicos");
                if (!dPastaData.Exists) { dPastaData.Create(); }
                XmlDocument xdocCanc = new XmlDocument();
                xdocCanc.LoadXml(sArquivo);
                xdocCanc.Save(belStaticPastas.PROTOCOLOS + "\\Servicos\\ped_canc_" + objPedCanc.InfPedidoCancelamento.IdentificacaoNfse.Numero + ".xml");

                Globais getschema = new Globais();

                XmlSchemaCollection myschema = new XmlSchemaCollection();
                XmlValidatingReader reader;
                XmlParserContext context = new XmlParserContext(null, null, "", XmlSpace.None);

                sArquivo = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + sArquivo;
                //reader = new XmlValidatingReader(sArquivo, XmlNodeType.Element, context);

                //myschema.Add("http://www.ginfes.com.br/servico_cancelar_nfse_envio", getschema.LeRegConfig("PastaSchema") + "\\servico_cancelar_nfse_envio_v02.xsd");

                //reader.ValidationType = ValidationType.Schema;

                //reader.Schemas.Add(myschema);

                //while (reader.Read())
                //{ }


                return sArquivo;
            }
            catch (Exception ex)
            {
                return "";
                throw;
            }
        }

        private string MontaXmlCancelamentoHomo(TcPedidoCancelamento objPedCanc, X509Certificate2 cert)
        {
            try
            {
                XNamespace pf = "http://www.ginfes.com.br/servico_cancelar_nfse_envio";
                //XNamespace tipos = "http://www.ginfes.com.br/tipos";
                XContainer conCancelarNfseEnvio = null;
                conCancelarNfseEnvio = (new XElement(pf + "CancelarNfseEnvio", new XAttribute("xmlns", "http://www.ginfes.com.br/servico_cancelar_nfse_envio"),
                                           // new XAttribute(XNamespace.Xmlns + "tipos", "http://www.ginfes.com.br/tipos"),
                                        new XElement(pf + "Prestador", new XElement(pf + "Cnpj", objPedCanc.InfPedidoCancelamento.IdentificacaoNfse.Cnpj),
                                                                      ((objPedCanc.InfPedidoCancelamento.IdentificacaoNfse.InscricaoMunicipal != "") ? new XElement(pf + "InscricaoMunicipal", objPedCanc.InfPedidoCancelamento.IdentificacaoNfse.InscricaoMunicipal) : null)),
                                        new XElement(pf + "NumeroNfse", objPedCanc.InfPedidoCancelamento.IdentificacaoNfse.Numero)));

                //Valida

                AssinaNFeXml Assinatura = new AssinaNFeXml();
                string sArquivo = Assinatura.ConfigurarArquivo(conCancelarNfseEnvio.ToString(), "NumeroNfse", cert);

                Globais glob = new Globais();
                DirectoryInfo dPastaData = new DirectoryInfo(belStaticPastas.PROTOCOLOS + "\\Servicos");
                if (!dPastaData.Exists) { dPastaData.Create(); }
                XmlDocument xdocCanc = new XmlDocument();
                xdocCanc.LoadXml(sArquivo);
                xdocCanc.Save(belStaticPastas.PROTOCOLOS + "\\Servicos\\ped_canc_" + objPedCanc.InfPedidoCancelamento.IdentificacaoNfse.Numero + ".xml");

                Globais getschema = new Globais();

                XmlSchemaCollection myschema = new XmlSchemaCollection();
                XmlValidatingReader reader;
                XmlParserContext context = new XmlParserContext(null, null, "", XmlSpace.None);

                sArquivo = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + sArquivo;
                //reader = new XmlValidatingReader(sArquivo, XmlNodeType.Element, context);

                //myschema.Add("http://www.ginfes.com.br/servico_cancelar_nfse_envio", getschema.LeRegConfig("PastaSchema") + "\\servico_cancelar_nfse_envio_v02.xsd");

                //reader.ValidationType = ValidationType.Schema;

                //reader.Schemas.Add(myschema);

                //while (reader.Read())
                //{ }


                return sArquivo;
            }
            catch (Exception ex)
            {
                return "";
                throw;
            }
        }

        private string MontaXmlCancelamentoV3(TcPedidoCancelamento objPedCanc, X509Certificate2 cert)
        {
            try
            {
                XNamespace pf = "http://www.ginfes.com.br/servico_cancelar_nfse_envio_v03.xsd";
                XNamespace tipos = "http://www.ginfes.com.br/tipos_v03.xsd";
                XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";

                XContainer conCancelarNfseEnvio = null;
                conCancelarNfseEnvio = (new XElement(pf + "CancelarNfseEnvio", new XAttribute(xsi + "schemaLocation", "http://www.ginfes.com.br/servico_cancelar_nfse_envio_v03.xsd"),
                                                                              new XAttribute("xmlns", "http://www.ginfes.com.br/servico_cancelar_nfse_envio_v03.xsd"),
                                                                              new XAttribute(XNamespace.Xmlns + "tipos", "http://www.ginfes.com.br/tipos_v03.xsd"),
                                                                              new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                                            new XElement(pf + "Pedido",
                                                new XElement(tipos + "InfPedidoCancelamento",
                                                     new XElement(tipos + "IdentificacaoNfse",
                                                         new XElement(tipos + "Numero", objPedCanc.InfPedidoCancelamento.IdentificacaoNfse.Numero),
                                                         new XElement(tipos + "Cnpj", objPedCanc.InfPedidoCancelamento.IdentificacaoNfse.Cnpj),
                                                         new XElement(tipos + "InscricaoMunicipal", objPedCanc.InfPedidoCancelamento.IdentificacaoNfse.InscricaoMunicipal),
                                                         new XElement(tipos + "CodigoMunicipio", objPedCanc.InfPedidoCancelamento.IdentificacaoNfse.CodigoMunicipio)),
                                                     new XElement(tipos + "CodigoCancelamento", objPedCanc.InfPedidoCancelamento.CodigoCancelamento)))));

                //Valida

                AssinaNFeXml Assinatura = new AssinaNFeXml();
                string sArquivo = Assinatura.ConfigurarArquivo(conCancelarNfseEnvio.ToString(), "tipos:InfPedidoCancelamento", cert);

                Globais glob = new Globais();
                DirectoryInfo dPastaData = new DirectoryInfo(belStaticPastas.PROTOCOLOS + "\\Servicos");
                if (!dPastaData.Exists) { dPastaData.Create(); }
                XmlDocument xdocCanc = new XmlDocument();
                xdocCanc.LoadXml(sArquivo);
                xdocCanc.Save(belStaticPastas.PROTOCOLOS + "\\Servicos\\ped_canc_" + objPedCanc.InfPedidoCancelamento.IdentificacaoNfse.Numero + ".xml");

                Globais getschema = new Globais();

                XmlSchemaCollection myschema = new XmlSchemaCollection();
                XmlValidatingReader reader;
                XmlParserContext context = new XmlParserContext(null, null, "", XmlSpace.None);

                reader = new XmlValidatingReader(sArquivo, XmlNodeType.Element, context);

                myschema.Add("http://www.ginfes.com.br/servico_cancelar_nfse_envio_v03.xsd", belStaticPastas.SCHEMA_NFSE + "\\servico_cancelar_nfse_envio_v03.xsd");

                reader.ValidationType = ValidationType.Schema;

                reader.Schemas.Add(myschema);

                while (reader.Read())
                {
                }


                return "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + sArquivo;
            }
            catch (Exception ex)
            {
                return "";
                throw;
            }
        }


    }
}
