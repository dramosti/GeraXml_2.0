using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Linq;

namespace HLP.bel
{
    public class belConsultaCadastro
    {
        public string sUF { get; set; }
        public string sIE { get; set; }
        public string sCNPJ { get; set; }
        public string sCPF { get; set; }
        public string sUFemp { get; set; }
        public X509Certificate2 cert { get; set; }
        

        public belConsultaCadastro(string sUFemp, string sUF, string sIE, string sCNPJ, string sCPF, X509Certificate2 cert)
        {
            
            this.sUF = sUF;
            this.sIE = sIE;
            this.sCNPJ = sCNPJ;
            this.sCPF = sCPF;
            this.sUFemp = sUFemp;
            this.cert = cert;
        }

        public string ConsultaCadastro()
        {
            try
            {
                StringBuilder sMsgRetorno = new StringBuilder();

                switch (sUFemp)
                {
                    case "SP":
                        {
                            HLP.WebService.v2_Producao_NFeConsultaCadastro_SP.CadConsultaCadastro2 ws2 = new HLP.WebService.v2_Producao_NFeConsultaCadastro_SP.CadConsultaCadastro2();
                            HLP.WebService.v2_Producao_NFeConsultaCadastro_SP.nfeCabecMsg cabec = new HLP.WebService.v2_Producao_NFeConsultaCadastro_SP.nfeCabecMsg();
                            belUF objbelUF = new belUF();
                            cabec.cUF = objbelUF.RetornaCUF(sUF);
                            cabec.versaoDados = "2.00";
                            ws2.nfeCabecMsgValue = cabec;
                            ws2.ClientCertificates.Add(cert);
                            XmlNode xDados = MontaMsg();
                            string sretorno = ws2.consultaCadastro2(xDados).OuterXml;
                            XmlDocument xRetorno = new XmlDocument();
                            xRetorno.LoadXml(sretorno);
                            MontaMsgRetorno(sMsgRetorno, xRetorno);

                        }
                        break;
                    case "RS":
                        {
                            HLP.WebService.v2_Producao_NFeConsultaCadastro_RS.CadConsultaCadastro2 ws2 = new HLP.WebService.v2_Producao_NFeConsultaCadastro_RS.CadConsultaCadastro2();
                            HLP.WebService.v2_Producao_NFeConsultaCadastro_RS.nfeCabecMsg cabec = new HLP.WebService.v2_Producao_NFeConsultaCadastro_RS.nfeCabecMsg();
                            belUF objbelUF = new belUF();
                            cabec.cUF = objbelUF.RetornaCUF(sUF);
                            cabec.versaoDados = "2.00";
                            ws2.nfeCabecMsgValue = cabec;
                            ws2.ClientCertificates.Add(cert);
                            XmlNode xDados = MontaMsg();
                            string sretorno = ws2.consultaCadastro2(xDados).OuterXml;
                            XmlDocument xRetorno = new XmlDocument();
                            xRetorno.LoadXml(sretorno);
                            MontaMsgRetorno(sMsgRetorno, xRetorno);
                        }
                        break;
                    case "MS":
                        {
                            HLP.WebService.v2_Producao_NFeConsultaCadastro_MS.CadConsultaCadastro2 ws2 = new HLP.WebService.v2_Producao_NFeConsultaCadastro_MS.CadConsultaCadastro2();
                            HLP.WebService.v2_Producao_NFeConsultaCadastro_MS.nfeCabecMsg cabec = new HLP.WebService.v2_Producao_NFeConsultaCadastro_MS.nfeCabecMsg();
                            belUF objbelUF = new belUF();
                            cabec.cUF = objbelUF.RetornaCUF(sUF);
                            cabec.versaoDados = "2.00";
                            ws2.nfeCabecMsgValue = cabec;
                            ws2.ClientCertificates.Add(cert);
                            XmlNode xDados = MontaMsg();
                            string sretorno = ws2.consultaCadastro2(xDados).OuterXml;
                            XmlDocument xRetorno = new XmlDocument();
                            xRetorno.LoadXml(sretorno);
                            MontaMsgRetorno(sMsgRetorno, xRetorno);
                        }
                        break;
                }

                return sMsgRetorno.ToString(); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void MontaMsgRetorno(StringBuilder sMsgRetorno, XmlDocument xRetorno)
        {
            XmlNodeList cStat = xRetorno.GetElementsByTagName("cStat");
            XmlNodeList xMotivo = xRetorno.GetElementsByTagName("xMotivo");
            XmlNodeList dhCons = xRetorno.GetElementsByTagName("dhCons");
            XmlNodeList cSit = xRetorno.GetElementsByTagName("cSit");
            XmlNodeList xNome = xRetorno.GetElementsByTagName("xNome");
            XmlNodeList xRegApur = xRetorno.GetElementsByTagName("xRegApur");
            XmlNodeList CNAE = xRetorno.GetElementsByTagName("CNAE");
            XmlNodeList dIniAtiv = xRetorno.GetElementsByTagName("dIniAtiv");
            XmlNodeList dUltSit = xRetorno.GetElementsByTagName("dUltSit");
            XmlNodeList dBaixa = xRetorno.GetElementsByTagName("dBaixa");

            if (cStat.Count > 0)
            {
                sMsgRetorno.Append("Cod. Status:  " + cStat[0].InnerText.ToString() + Environment.NewLine);
            }

            if (xMotivo.Count > 0)
            {
                sMsgRetorno.Append("Status: " + xMotivo[0].InnerText.ToString() + Environment.NewLine);
            }
            if (cSit.Count > 0)
            {
                sMsgRetorno.Append("Situação do contribuinte: " + (cSit[0].InnerText.ToString() == "0" ? "Não Habilitado" : "Habilitado.") + Environment.NewLine);
            }

            if (dhCons.Count > 0)
            {
                sMsgRetorno.Append("Data e hora de processamento da consulta: " + Convert.ToDateTime(dhCons[0].InnerText).ToString("dd/MM/yyyy HH:mm:ss") + Environment.NewLine);
            }

            if (xNome.Count > 0)
            {
                sMsgRetorno.Append("Razão Social: " + xNome[0].InnerText.ToString() + Environment.NewLine);
            }

            if (xRegApur.Count > 0)
            {
                sMsgRetorno.Append("Regime de Apuração do ICMS do Contribuinte: " + xRegApur[0].InnerText.ToString() + Environment.NewLine);
            }

            if (CNAE.Count > 0)
            {
                sMsgRetorno.Append("CNAE principal do contribuinte: " + CNAE[0].InnerText.ToString() + Environment.NewLine);
            }

            if (dIniAtiv.Count > 0)
            {
                sMsgRetorno.Append("Data de Início da Atividade do Contribuinte: " + Convert.ToDateTime(dIniAtiv[0].InnerText).ToString("dd/MM/yyyy") + Environment.NewLine);
            }

            if (dUltSit.Count > 0)
            {
                sMsgRetorno.Append("Data da última modificação da situação cadastral do contribuinte: " + Convert.ToDateTime(dUltSit[0].InnerText).ToString("dd/MM/yyyy") + Environment.NewLine);
            }

            if (dBaixa.Count > 0)
            {
                sMsgRetorno.Append("Data de ocorrência da baixa do contribuinte: " + Convert.ToDateTime(dBaixa[0].InnerText).ToString("dd/MM/yyyy") + Environment.NewLine);
            }
        }



        private XmlNode MontaMsg()
        {
            try
            {
                XmlSchemaCollection myschema = new XmlSchemaCollection();
                XmlValidatingReader reader;
                XNamespace pf = "http://www.portalfiscal.inf.br/nfe";

                XDocument xdoc = new XDocument(new XElement(pf + "ConsCad", new XAttribute("versao", "2.00"),
                                                    new XElement(pf + "infCons",
                                                               new XElement(pf + "xServ", "CONS-CAD"),
                                                               new XElement(pf + "UF", sUF),
                                                               (sIE != "" ? new XElement(pf + "IE", Util.Util.TiraSimbolo(sIE, "")) : null),
                                                               ((sCNPJ != "" && sIE == "") ? new XElement(pf + "CNPJ", Util.Util.TiraSimbolo(sCNPJ, "")) : null),
                                                               ((sCPF != "" && sIE == "" && sCNPJ == "") ? new XElement(pf + "CPF", Util.Util.TiraSimbolo(sCPF, "")) : null))));

              

                XmlParserContext context = new XmlParserContext(null, null, "", XmlSpace.None);

                reader = new XmlValidatingReader(xdoc.ToString(), XmlNodeType.Element, context);

                Globais Gb = new Globais();

                myschema.Add("http://www.portalfiscal.inf.br/nfe", Gb.LeRegConfig("PastaSchema").ToString() + "\\consCad_v2.00.xsd");

                reader.ValidationType = ValidationType.Schema;

                reader.Schemas.Add(myschema);

                while (reader.Read())
                { }
                string sDados = xdoc.ToString();
                XmlDocument Xmldoc = new XmlDocument();
                Xmldoc.LoadXml(sDados);
                XmlNode xNode = Xmldoc.DocumentElement;
                return xNode;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
