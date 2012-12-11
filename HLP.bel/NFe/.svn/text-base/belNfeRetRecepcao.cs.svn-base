using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Web.Services;
using HLP.WebService;
using System.Xml;
using System.Xml.Schema;
using System.Security.Cryptography.X509Certificates;
using HLP.bel.Static;
using HLP.bel.NFe.GeraXml;

namespace HLP.bel
{
    public class belNfeRetRecepcao
    {
        private string _versao;
        private int _tpamb;

        public int Tpamb
        {
            get { return _tpamb; }
            set { _tpamb = value; }
        }
        private string _nrec;

        public string Nrec
        {
            get { return _nrec; }
            set { _nrec = value; }
        }
        private string _xmlRet;
        public string XmlRet
        {
            get { return _xmlRet; }
            set { _xmlRet = value; }
        }
        private X509Certificate2 _cert;
        private bool bModoSCAN;

        public belNfeRetRecepcao(string sVersao, string snRec, string sversaoaplic, X509Certificate2 xcert, bool bModoSCAN)
        {
            this.bModoSCAN = bModoSCAN;
            _versao = sVersao;
            _tpamb = belStatic.tpAmb;
            _nrec = snRec;
            _pversaoaplic = sversaoaplic;
            _cert = xcert;

        }


        /// <summary>
        /// BUSCA RETORNO WEB SERVICE ESTADUAL
        /// </summary>
        /// <param name="UF_Empresa"></param>
        private void BuscaRetorno(string UF_Empresa)
        {
            try
            {
                string snfeDadosMsg = NfeDadosMsg();

                AssinaNFeXml BC = new AssinaNFeXml();
                string sRet = string.Empty;


                if (belStatic.bModoSCAN)
                {
                    #region SCAN
                    if (_tpamb == 1)
                    {
                        HLP.WebService.v2_SCAN_Producao_NFeRetRecepcao.NfeRetRecepcao2 ws2 = new HLP.WebService.v2_SCAN_Producao_NFeRetRecepcao.NfeRetRecepcao2();
                        HLP.WebService.v2_SCAN_Producao_NFeRetRecepcao.nfeCabecMsg cabec = new HLP.WebService.v2_SCAN_Producao_NFeRetRecepcao.nfeCabecMsg();

                        belUF objbelUf = new belUF();
                        cabec.cUF = objbelUf.RetornaCUF(UF_Empresa);
                        cabec.versaoDados = _pversaoaplic;
                        ws2.nfeCabecMsgValue = cabec;
                        ws2.ClientCertificates.Add(_cert);

                        XmlDocument xmlNfeDadosMsg = new XmlDocument();
                        xmlNfeDadosMsg.LoadXml(snfeDadosMsg);
                        XmlNode xNodeRet = xmlNfeDadosMsg.DocumentElement;

                        _xmlRet = ws2.nfeRetRecepcao2(xNodeRet).OuterXml;
                    }
                    else if (_tpamb == 2)
                    {
                        HLP.WebService.v2_SCAN_Homologacao_NFeRetRecepcao.NfeRetRecepcao2 ws2 = new HLP.WebService.v2_SCAN_Homologacao_NFeRetRecepcao.NfeRetRecepcao2();
                        HLP.WebService.v2_SCAN_Homologacao_NFeRetRecepcao.nfeCabecMsg cabec = new HLP.WebService.v2_SCAN_Homologacao_NFeRetRecepcao.nfeCabecMsg();

                        belUF objbelUf = new belUF();
                        cabec.cUF = objbelUf.RetornaCUF(UF_Empresa);
                        cabec.versaoDados = _pversaoaplic;
                        ws2.nfeCabecMsgValue = cabec;
                        ws2.ClientCertificates.Add(_cert);

                        XmlDocument xmlNfeDadosMsg = new XmlDocument();
                        xmlNfeDadosMsg.LoadXml(snfeDadosMsg);
                        XmlNode xNodeRet = xmlNfeDadosMsg.DocumentElement;

                        _xmlRet = ws2.nfeRetRecepcao2(xNodeRet).OuterXml;

                    }
                    else
                    {
                        throw new Exception("tpamb com valor incorreto");
                    }
                    #endregion

                }
                else
                {

                    // Diego - O.S 24489 - 26/05/2010
                    switch (UF_Empresa)
                    {
                        case "SP":
                            {
                                #region Regiao_SP
                                if (_tpamb == 1)
                                {
                                    HLP.WebService.v2_Producao_NFeRetRecepcao_SP.NfeRetRecepcao2 ws2 = new HLP.WebService.v2_Producao_NFeRetRecepcao_SP.NfeRetRecepcao2();
                                    HLP.WebService.v2_Producao_NFeRetRecepcao_SP.nfeCabecMsg cabec = new HLP.WebService.v2_Producao_NFeRetRecepcao_SP.nfeCabecMsg();

                                    belUF objbelUf = new belUF();
                                    cabec.cUF = objbelUf.RetornaCUF(UF_Empresa);
                                    cabec.versaoDados = _pversaoaplic;
                                    ws2.nfeCabecMsgValue = cabec;
                                    ws2.ClientCertificates.Add(_cert);

                                    XmlDocument xmlNfeDadosMsg = new XmlDocument();
                                    xmlNfeDadosMsg.LoadXml(snfeDadosMsg);
                                    XmlNode xNodeRet = xmlNfeDadosMsg.DocumentElement;

                                    _xmlRet = ws2.nfeRetRecepcao2(xNodeRet).OuterXml;
                                }
                                else if (_tpamb == 2)
                                {
                                    HLP.WebService.v2_Homologacao_NfeRetRecepcao_SP.NfeRetRecepcao2 ws2 = new HLP.WebService.v2_Homologacao_NfeRetRecepcao_SP.NfeRetRecepcao2();
                                    HLP.WebService.v2_Homologacao_NfeRetRecepcao_SP.nfeCabecMsg cabec = new HLP.WebService.v2_Homologacao_NfeRetRecepcao_SP.nfeCabecMsg();

                                    belUF objbelUf = new belUF();
                                    cabec.cUF = objbelUf.RetornaCUF(UF_Empresa);
                                    cabec.versaoDados = _pversaoaplic;
                                    ws2.nfeCabecMsgValue = cabec;
                                    ws2.ClientCertificates.Add(_cert);

                                    XmlDocument xmlNfeDadosMsg = new XmlDocument();
                                    xmlNfeDadosMsg.LoadXml(snfeDadosMsg);
                                    XmlNode xNodeRet = xmlNfeDadosMsg.DocumentElement;

                                    _xmlRet = ws2.nfeRetRecepcao2(xNodeRet).OuterXml;

                                }
                                else
                                {
                                    throw new Exception("tpamb com valor incorreto");
                                }
                                #endregion
                            }
                            break;
                        case "MS":
                            {
                                #region Regiao_MS
                                if (_tpamb == 1)
                                {
                                    HLP.WebService.v2_Producao_NFeRetRecepcao_MS.NfeRetRecepcao2 ws2 = new HLP.WebService.v2_Producao_NFeRetRecepcao_MS.NfeRetRecepcao2();
                                    HLP.WebService.v2_Producao_NFeRetRecepcao_MS.nfeCabecMsg cabec = new HLP.WebService.v2_Producao_NFeRetRecepcao_MS.nfeCabecMsg();

                                    belUF objbelUf = new belUF();
                                    cabec.cUF = objbelUf.RetornaCUF(UF_Empresa);
                                    cabec.versaoDados = _pversaoaplic;
                                    ws2.nfeCabecMsgValue = cabec;
                                    ws2.ClientCertificates.Add(_cert);
                                    XmlDocument xmlNfeDadosMsg = new XmlDocument();
                                    xmlNfeDadosMsg.LoadXml(snfeDadosMsg);
                                    XmlNode xNodeRet = xmlNfeDadosMsg.DocumentElement;
                                    _xmlRet = ws2.nfeRetRecepcao2(xNodeRet).OuterXml;
                                }
                                else if (_tpamb == 2)
                                {
                                    HLP.WebService.v2_Homologacao_NFeRetRecepacao_RS.NfeRetRecepcao2 ws2 = new HLP.WebService.v2_Homologacao_NFeRetRecepacao_RS.NfeRetRecepcao2();
                                    HLP.WebService.v2_Homologacao_NFeRetRecepacao_RS.nfeCabecMsg cabec = new HLP.WebService.v2_Homologacao_NFeRetRecepacao_RS.nfeCabecMsg();

                                    belUF objbelUf = new belUF();
                                    cabec.cUF = objbelUf.RetornaCUF(UF_Empresa);
                                    cabec.versaoDados = _pversaoaplic;
                                    ws2.nfeCabecMsgValue = cabec;
                                    ws2.ClientCertificates.Add(_cert);
                                    XmlDocument xmlNfeDadosMsg = new XmlDocument();
                                    xmlNfeDadosMsg.LoadXml(snfeDadosMsg);
                                    XmlNode xNodeRet = xmlNfeDadosMsg.DocumentElement;
                                    _xmlRet = ws2.nfeRetRecepcao2(xNodeRet).OuterXml;
                                }
                                else
                                {
                                    throw new Exception("tpamb com valor incorreto");
                                }
                                #endregion
                            }
                            break;
                        case "RS":
                            {
                                #region Regiao_RS
                                if (_tpamb == 1)
                                {
                                    HLP.WebService.v2_Producao_NFeRetRecepcao_RS.NfeRetRecepcao2 ws2 = new HLP.WebService.v2_Producao_NFeRetRecepcao_RS.NfeRetRecepcao2();
                                    HLP.WebService.v2_Producao_NFeRetRecepcao_RS.nfeCabecMsg cabec = new HLP.WebService.v2_Producao_NFeRetRecepcao_RS.nfeCabecMsg();

                                    belUF objbelUf = new belUF();
                                    cabec.cUF = objbelUf.RetornaCUF(UF_Empresa);
                                    cabec.versaoDados = _pversaoaplic;
                                    ws2.nfeCabecMsgValue = cabec;
                                    ws2.ClientCertificates.Add(_cert);

                                    XmlDocument xmlNfeDadosMsg = new XmlDocument();
                                    xmlNfeDadosMsg.LoadXml(snfeDadosMsg);
                                    XmlNode xNodeRet = xmlNfeDadosMsg.DocumentElement;

                                    _xmlRet = ws2.nfeRetRecepcao2(xNodeRet).OuterXml;

                                }
                                else if (_tpamb == 2)
                                {
                                    HLP.WebService.v2_Homologacao_NFeRetRecepacao_RS.NfeRetRecepcao2 ws2 = new HLP.WebService.v2_Homologacao_NFeRetRecepacao_RS.NfeRetRecepcao2();
                                    HLP.WebService.v2_Homologacao_NFeRetRecepacao_RS.nfeCabecMsg cabec = new HLP.WebService.v2_Homologacao_NFeRetRecepacao_RS.nfeCabecMsg();

                                    belUF objbelUf = new belUF();
                                    cabec.cUF = objbelUf.RetornaCUF(UF_Empresa);
                                    cabec.versaoDados = _pversaoaplic;
                                    ws2.nfeCabecMsgValue = cabec;
                                    ws2.ClientCertificates.Add(_cert);

                                    XmlDocument xmlNfeDadosMsg = new XmlDocument();
                                    xmlNfeDadosMsg.LoadXml(snfeDadosMsg);
                                    XmlNode xNodeRet = xmlNfeDadosMsg.DocumentElement;

                                    _xmlRet = ws2.nfeRetRecepcao2(xNodeRet).OuterXml;
                                }
                                else
                                {
                                    throw new Exception("tpamb com valor incorreto");
                                }
                                #endregion
                            }
                            break;
                    }
                }
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


        public XmlDocument Retornaxml(string UF_Empresa)//Danner - o.s. 23984 - 07/01/2010
        {
            BuscaRetorno(UF_Empresa);
            XmlDocument xRet = new XmlDocument();
            xRet.LoadXml(_xmlRet);
            return xRet;
        }


        private string NfeDadosMsg()
        {
            XNamespace xnome = "http://www.portalfiscal.inf.br/nfe";
            XmlSchemaCollection myschema = new XmlSchemaCollection();
            XmlValidatingReader reader;
            try
            {
                XNamespace xname = "http://www.portalfiscal.inf.br/nfe";
                XDocument xdoc = new XDocument(new XElement(xname + "consReciNFe", new XAttribute("versao", "2.00"),
                                               new XElement(xname + "tpAmb", _tpamb.ToString()),
                                               new XElement(xname + "nRec", _nrec)));

                //Danner -  o.s. sem - 04/11/2009
                Globais xml = new Globais();

                XmlParserContext context = new XmlParserContext(null, null, "", XmlSpace.None);

                reader = new XmlValidatingReader(xdoc.ToString(), XmlNodeType.Element, context);

                myschema.Add("http://www.portalfiscal.inf.br/nfe", belStaticPastas.SCHEMA_NFE + "\\consReciNFe_v2.00.xsd");

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
            //Fim - Danner -  o.s. sem - 04/11/2009
        }

        private string _pversaoaplic;

        public string Pversaoaplic
        {
            get { return _pversaoaplic; }
            set { _pversaoaplic = value; }
        }

    }
}
