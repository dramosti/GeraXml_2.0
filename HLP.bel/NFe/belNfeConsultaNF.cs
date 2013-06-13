using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using HLP.bel.Static;
using HLP.bel.NFe.GeraXml;
//Danner - o.s. 23851 - 18/11/2009

namespace HLP.bel
{
    public class belNfeConsultaNF
    {
        /// <summary>
        /// Tipo do Ambiente da Empresa.
        /// </summary>
        private int _tpamb;
        /// <summary>
        /// Chave de Acesso da NFe.
        /// </summary>
        private string _chnfe;   
        /// <summary>
        /// Versão do Cabeçalho.
        /// </summary>
        private string _versao;
        /// <summary>
        /// Versão de Dados.
        /// </summary>
        private string _versaodados;
        /// <summary>
        /// Nome do Serviço.
        /// </summary>
        private string _xserv;

        public belNfeConsultaNF(string sVersao, string sVerDados,  string sChnfe, string sXServ)
        {
            Globais Gb = new Globais();
            _tpamb = belStatic.TpAmb;         
            _versao = sVersao;
            _versaodados = "2.01";
            _xserv = sXServ;
            _chnfe = sChnfe;

        }

        /// <summary>
        /// Busca Retorno do Web Service SCAN
        /// </summary>
        /// <returns></returns>
        public string buscaRetornoSCAN(string UF_Empresa)
        {
            string sCabec = NfeCabecMsg();
            string sDados = consultaNFe();
            AssinaNFeXml bc = new AssinaNFeXml();
            string xret = string.Empty;
            try
            {
                if (_tpamb == 2)
                {
                    HLP.WebService.v2_SCAN_Homologacao_NFeConsulta.NfeConsulta2 ws2 = new HLP.WebService.v2_SCAN_Homologacao_NFeConsulta.NfeConsulta2();
                    HLP.WebService.v2_SCAN_Homologacao_NFeConsulta.nfeCabecMsg cabec = new HLP.WebService.v2_SCAN_Homologacao_NFeConsulta.nfeCabecMsg();

                    cabec.versaoDados = _versaodados;
                    belUF objUf = new belUF();
                    cabec.cUF = objUf.RetornaCUF(UF_Empresa);
                    ws2.nfeCabecMsgValue = cabec;
                    ws2.ClientCertificates.Add(bc.BuscaNome(""));

                    XmlDataDocument xmlConsulta = new XmlDataDocument();
                    xmlConsulta.LoadXml(sDados);
                    XmlNode xNodeConsulta = xmlConsulta.DocumentElement;

                    xret = ws2.nfeConsultaNF2(xNodeConsulta).OuterXml;
                }
                else
                {
                    HLP.WebService.v2_SCAN_Producao_NFeConsulta.NfeConsulta2 ws2 = new HLP.WebService.v2_SCAN_Producao_NFeConsulta.NfeConsulta2();
                    HLP.WebService.v2_SCAN_Producao_NFeConsulta.nfeCabecMsg cabec = new HLP.WebService.v2_SCAN_Producao_NFeConsulta.nfeCabecMsg();

                    cabec.versaoDados = _versaodados;
                    belUF objUf = new belUF();
                    cabec.cUF = objUf.RetornaCUF(UF_Empresa);
                    ws2.nfeCabecMsgValue = cabec;
                    ws2.ClientCertificates.Add(bc.BuscaNome(""));

                    XmlDataDocument xmlConsulta = new XmlDataDocument();
                    xmlConsulta.LoadXml(sDados);
                    XmlNode xNodeConsulta = xmlConsulta.DocumentElement;

                    xret = ws2.nfeConsultaNF2(xNodeConsulta).OuterXml;
                }
                return xret;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Busca Retorno do Web Service SCAN
        /// </summary>
        /// <param name="UF_Empresa"></param>
        /// <returns></returns>
        public string buscaRetorno(string UF_Empresa)
        {
            string sCabec = NfeCabecMsg();
            string sDados = consultaNFe();
            AssinaNFeXml bc = new AssinaNFeXml();
            string xret = string.Empty;


            try
            {
                // Diego - O.S 24489 - 26/05/2010
                switch (UF_Empresa)
                {
                    case "SP":
                        {
                            if (_tpamb == 2)
                            {
                                HLP.WebService.v2_Homologacao_NFeConsulta_SP.NfeConsulta2 ws2 = new HLP.WebService.v2_Homologacao_NFeConsulta_SP.NfeConsulta2();
                                HLP.WebService.v2_Homologacao_NFeConsulta_SP.nfeCabecMsg cabec = new HLP.WebService.v2_Homologacao_NFeConsulta_SP.nfeCabecMsg();

                                cabec.versaoDados = _versaodados;
                                belUF objUf = new belUF();
                                cabec.cUF = objUf.RetornaCUF(UF_Empresa);
                                ws2.nfeCabecMsgValue = cabec;
                                ws2.ClientCertificates.Add(bc.BuscaNome(""));

                                XmlDataDocument xmlConsulta = new XmlDataDocument();
                                xmlConsulta.LoadXml(sDados);
                                XmlNode xNodeConsulta = xmlConsulta.DocumentElement;

                                xret = ws2.nfeConsultaNF2(xNodeConsulta).OuterXml;
                            }
                            else
                            {
                                HLP.WebService.v2_Producao_NFeConsulta_SP.NfeConsulta2 ws2 = new HLP.WebService.v2_Producao_NFeConsulta_SP.NfeConsulta2();
                                HLP.WebService.v2_Producao_NFeConsulta_SP.nfeCabecMsg cabec = new HLP.WebService.v2_Producao_NFeConsulta_SP.nfeCabecMsg();

                                cabec.versaoDados = _versaodados;
                                belUF objUf = new belUF();
                                cabec.cUF = objUf.RetornaCUF(UF_Empresa);
                                ws2.nfeCabecMsgValue = cabec;
                                ws2.ClientCertificates.Add(bc.BuscaNome(""));

                                XmlDataDocument xmlConsulta = new XmlDataDocument();
                                xmlConsulta.LoadXml(sDados);
                                XmlNode xNodeConsulta = xmlConsulta.DocumentElement;

                                xret = ws2.nfeConsultaNF2(xNodeConsulta).OuterXml;
                            }
                        }
                        break;
                    case "MS":
                        {
                            if (_tpamb == 2)
                            {
                                HLP.WebService.v2_Homologacao_NFeConsulta_MS.NfeConsulta2 ws2 = new HLP.WebService.v2_Homologacao_NFeConsulta_MS.NfeConsulta2();
                                HLP.WebService.v2_Homologacao_NFeConsulta_MS.nfeCabecMsg cabec = new HLP.WebService.v2_Homologacao_NFeConsulta_MS.nfeCabecMsg();

                                cabec.versaoDados = _versaodados;
                                belUF objUf = new belUF();
                                cabec.cUF = objUf.RetornaCUF(UF_Empresa);
                                ws2.nfeCabecMsgValue = cabec;
                                ws2.ClientCertificates.Add(bc.BuscaNome(""));

                                XmlDataDocument xmlConsulta = new XmlDataDocument();
                                xmlConsulta.LoadXml(sDados);
                                XmlNode xNodeConsulta = xmlConsulta.DocumentElement;

                                xret = ws2.nfeConsultaNF2(xNodeConsulta).OuterXml;

                            }
                            else
                            {
                                HLP.WebService.v2_Producao_NFeConsulta_MS.NfeConsulta2 ws2 = new HLP.WebService.v2_Producao_NFeConsulta_MS.NfeConsulta2();
                                HLP.WebService.v2_Producao_NFeConsulta_MS.nfeCabecMsg cabec = new HLP.WebService.v2_Producao_NFeConsulta_MS.nfeCabecMsg();

                                cabec.versaoDados = _versaodados;
                                belUF objUf = new belUF();
                                cabec.cUF = objUf.RetornaCUF(UF_Empresa);
                                ws2.nfeCabecMsgValue = cabec;
                                ws2.ClientCertificates.Add(bc.BuscaNome(""));

                                XmlDataDocument xmlConsulta = new XmlDataDocument();
                                xmlConsulta.LoadXml(sDados);
                                XmlNode xNodeConsulta = xmlConsulta.DocumentElement;

                                xret = ws2.nfeConsultaNF2(xNodeConsulta).OuterXml;
                            }
                        }
                        break;
                    case "RS":
                        {
                            if (_tpamb == 2)
                            {
                                HLP.WebService.v2_Homologacao_NFeConsulta_RS.NfeConsulta2 ws2 = new HLP.WebService.v2_Homologacao_NFeConsulta_RS.NfeConsulta2();
                                HLP.WebService.v2_Homologacao_NFeConsulta_RS.nfeCabecMsg cabec = new HLP.WebService.v2_Homologacao_NFeConsulta_RS.nfeCabecMsg();

                                cabec.versaoDados = _versaodados;
                                belUF objUf = new belUF();
                                cabec.cUF = objUf.RetornaCUF(UF_Empresa);
                                ws2.nfeCabecMsgValue = cabec;
                                ws2.ClientCertificates.Add(bc.BuscaNome(""));

                                XmlDataDocument xmlConsulta = new XmlDataDocument();
                                xmlConsulta.LoadXml(sDados);
                                XmlNode xNodeConsulta = xmlConsulta.DocumentElement;

                                xret = ws2.nfeConsultaNF2(xNodeConsulta).OuterXml;
                            }
                            else
                            {
                                HLP.WebService.v2_Producao_NFeConsulta_RS.NfeConsulta2 ws2 = new HLP.WebService.v2_Producao_NFeConsulta_RS.NfeConsulta2();
                                HLP.WebService.v2_Producao_NFeConsulta_RS.nfeCabecMsg cabec = new HLP.WebService.v2_Producao_NFeConsulta_RS.nfeCabecMsg();

                                cabec.versaoDados = _versaodados;
                                belUF objUf = new belUF();
                                cabec.cUF = objUf.RetornaCUF(UF_Empresa);
                                ws2.nfeCabecMsgValue = cabec;
                                ws2.ClientCertificates.Add(bc.BuscaNome(""));

                                XmlDataDocument xmlConsulta = new XmlDataDocument();
                                xmlConsulta.LoadXml(sDados);
                                XmlNode xNodeConsulta = xmlConsulta.DocumentElement;

                                xret = ws2.nfeConsultaNF2(xNodeConsulta).OuterXml;
                            }
                        }
                        break;
                }
                return xret;

            }
            catch (Exception)
            {

                throw;
            }

        }
        private string NfeCabecMsg()
        {
            XmlSchemaCollection myschema = new XmlSchemaCollection();
            XmlValidatingReader reader;


            try
            {
                XNamespace nome = "http://www.portalfiscal.inf.br/nfe";
                XDocument xdoc = new XDocument(new XElement(nome + "cabecMsg", new XAttribute("versao", _versao), new XAttribute("xmlns", "http://www.portalfiscal.inf.br/nfe"),
                                              new XElement(nome + "versaoDados", _versaodados)));

                //XmlParserContext context = new XmlParserContext(null, null, "", XmlSpace.None);

                //reader = new XmlValidatingReader(xdoc.ToString(), XmlNodeType.Element, context);

                //myschema.Add("http://www.portalfiscal.inf.br/nfe", _pathschemas + "\\cabecMsg_v1.02.xsd");

                //reader.ValidationType = ValidationType.Schema;

                //reader.Schemas.Add(myschema);

                //while (reader.Read())
                //{ }

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
        private string consultaNFe()
        {
            XmlSchemaCollection myschema = new XmlSchemaCollection();
            XmlValidatingReader reader;
            string sxdoc = "";
            XNamespace pf = "http://www.portalfiscal.inf.br/nfe";

            try
            {
                XDocument xdoc = new XDocument(new XElement(pf + "consSitNFe", new XAttribute("versao", _versaodados),
                                                  new XElement(pf + "tpAmb", _tpamb.ToString()),
                                                   new XElement(pf + "xServ", _xserv),
                                                   new XElement(pf + "chNFe", _chnfe)));

                //XmlParserContext context = new XmlParserContext(null, null, "", XmlSpace.None);

                //reader = new XmlValidatingReader(xdoc.ToString(), XmlNodeType.Element, context);

                //myschema.Add("http://www.portalfiscal.inf.br/nfe", belStaticPastas.SCHEMA_NFE + "\\consSitNFe_v2.00.xsd");

                //reader.ValidationType = ValidationType.Schema;

                //reader.Schemas.Add(myschema);

                //while (reader.Read())
                //{ }
                sxdoc = xdoc.ToString();

            }
            catch (XmlException x)
            {
                throw new Exception(x.Message.ToString());
            }
            catch (XmlSchemaException x)
            {
                throw new Exception(x.Message.ToString());
            }
            return sxdoc;
        }

    }
}
//Fim - Danner - o.s. 23851 - 18/11/2009