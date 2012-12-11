using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Xml.Linq;
using HLP.bel.Static;

namespace HLP.Dao.CCe
{
    public class daoEnviaCCe
    {

        public static string TransmiteLoteCCe(string sXmlLote, X509Certificate2 _cert)
        {
            try
            {
                bel.belUF objbelUF = new HLP.bel.belUF();
                string sUF = objbelUF.RetornaSiglaUF(belStatic.cUF.ToString());
                string sRet = "";
                switch (sUF)
                {
                    case "SP":
                        {
                            if (belStatic.tpAmb == 2)
                            {
                                #region sp_homologacao
                                HLP.WebService.v2_Homologacao_NFeRecepcaoEvento_SP.nfeCabecMsg cabec = new HLP.WebService.v2_Homologacao_NFeRecepcaoEvento_SP.nfeCabecMsg();
                                HLP.WebService.v2_Homologacao_NFeRecepcaoEvento_SP.RecepcaoEvento ws2 = new HLP.WebService.v2_Homologacao_NFeRecepcaoEvento_SP.RecepcaoEvento();

                                cabec.cUF = belStatic.cUF.ToString();
                                cabec.versaoDados = "1.00";
                                ws2.nfeCabecMsgValue = cabec;
                                ws2.ClientCertificates.Add(_cert);

                                XmlDocument _xmlxelem = new XmlDocument();
                                _xmlxelem.PreserveWhitespace = true;
                                _xmlxelem.LoadXml(sXmlLote);

                                XmlNode xNelem = null;
                                xNelem = _xmlxelem.DocumentElement;

                                sRet = ws2.nfeRecepcaoEvento(xNelem).OuterXml;
                                #endregion
                            }
                            else
                            {
                                #region sp_producao
                                HLP.WebService.v2_Producao_NFeRecepcaoEvento_SP.nfeCabecMsg cabec = new HLP.WebService.v2_Producao_NFeRecepcaoEvento_SP.nfeCabecMsg();
                                HLP.WebService.v2_Producao_NFeRecepcaoEvento_SP.RecepcaoEvento ws2 = new HLP.WebService.v2_Producao_NFeRecepcaoEvento_SP.RecepcaoEvento();

                                cabec.cUF = belStatic.cUF.ToString();
                                cabec.versaoDados = "1.00";
                                ws2.nfeCabecMsgValue = cabec;
                                ws2.ClientCertificates.Add(_cert);

                                XmlDocument _xmlxelem = new XmlDocument();
                                _xmlxelem.PreserveWhitespace = true;
                                _xmlxelem.LoadXml(sXmlLote);

                                XmlNode xNelem = null;
                                xNelem = _xmlxelem.DocumentElement;

                                sRet = ws2.nfeRecepcaoEvento(xNelem).OuterXml;

                                #endregion
                            }
                        }
                        break;
                    case "RS":
                        {
                            if (belStatic.tpAmb == 2)
                            {
                                #region RS_homologacao

                                HLP.WebService.v2_Homologacao_NFeRecepcaoEvento_RS.nfeCabecMsg cabec = new HLP.WebService.v2_Homologacao_NFeRecepcaoEvento_RS.nfeCabecMsg();
                                HLP.WebService.v2_Homologacao_NFeRecepcaoEvento_RS.RecepcaoEvento ws2 = new HLP.WebService.v2_Homologacao_NFeRecepcaoEvento_RS.RecepcaoEvento();

                                cabec.cUF = belStatic.cUF.ToString();
                                cabec.versaoDados = "1.00";
                                ws2.nfeCabecMsgValue = cabec;
                                ws2.ClientCertificates.Add(_cert);

                                XmlDocument _xmlxelem = new XmlDocument();
                                _xmlxelem.PreserveWhitespace = true;
                                _xmlxelem.LoadXml(sXmlLote);

                                XmlNode xNelem = null;
                                xNelem = _xmlxelem.DocumentElement;

                                sRet = ws2.nfeRecepcaoEvento(xNelem).OuterXml;
                                #endregion
                            }
                            else
                            {
                                #region RS_producao
                                HLP.WebService.v2_Producao_NFeRecepcaoEvento_RS.nfeCabecMsg cabec = new HLP.WebService.v2_Producao_NFeRecepcaoEvento_RS.nfeCabecMsg();
                                HLP.WebService.v2_Producao_NFeRecepcaoEvento_RS.RecepcaoEvento ws2 = new HLP.WebService.v2_Producao_NFeRecepcaoEvento_RS.RecepcaoEvento();

                                cabec.cUF = belStatic.cUF.ToString();
                                cabec.versaoDados = "1.00";
                                ws2.nfeCabecMsgValue = cabec;
                                ws2.ClientCertificates.Add(_cert);

                                XmlDocument _xmlxelem = new XmlDocument();
                                _xmlxelem.PreserveWhitespace = true;
                                _xmlxelem.LoadXml(sXmlLote);

                                XmlNode xNelem = null;
                                xNelem = _xmlxelem.DocumentElement;

                                sRet = ws2.nfeRecepcaoEvento(xNelem).OuterXml;
                                #endregion
                            }
                        }
                        break;
                    case "MS":
                        {
                            if (belStatic.tpAmb == 2)
                            {
                                HLP.WebService.v2_Homologacao_NFeRetRecepcao_MS.nfeCabecMsg cabec = new HLP.WebService.v2_Homologacao_NFeRetRecepcao_MS.nfeCabecMsg();
                                HLP.WebService.v2_Homologacao_NFeRetRecepcao_MS.NfeRetRecepcao2 ws2 = new HLP.WebService.v2_Homologacao_NFeRetRecepcao_MS.NfeRetRecepcao2();

                                cabec.cUF = belStatic.cUF.ToString();
                                cabec.versaoDados = "1.00";
                                ws2.nfeCabecMsgValue = cabec;
                                ws2.ClientCertificates.Add(_cert);

                                XmlDocument _xmlxelem = new XmlDocument();
                                _xmlxelem.PreserveWhitespace = true;
                                _xmlxelem.LoadXml(sXmlLote);

                                XmlNode xNelem = null;
                                xNelem = _xmlxelem.DocumentElement;

                                sRet = ws2.nfeRetRecepcao2(xNelem).OuterXml;
                            }
                            else
                            {
                                #region MS_producao
                                HLP.WebService.v2_Producao_NFeRecepcaoEvento_MS.nfeCabecMsg cabec = new HLP.WebService.v2_Producao_NFeRecepcaoEvento_MS.nfeCabecMsg();
                                HLP.WebService.v2_Producao_NFeRecepcaoEvento_MS.RecepcaoEvento ws2 = new HLP.WebService.v2_Producao_NFeRecepcaoEvento_MS.RecepcaoEvento();

                                cabec.cUF = belStatic.cUF.ToString();
                                cabec.versaoDados = "1.00";
                                ws2.nfeCabecMsgValue = cabec;
                                ws2.ClientCertificates.Add(_cert);

                                XmlDocument _xmlxelem = new XmlDocument();
                                _xmlxelem.PreserveWhitespace = true;
                                _xmlxelem.LoadXml(sXmlLote);

                                XmlNode xNelem = null;
                                xNelem = _xmlxelem.DocumentElement;

                                sRet = ws2.nfeRecepcaoEvento(xNelem).OuterXml;
                                #endregion
                            }

                        }
                        break;
                }

                return sRet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




    }
}
