using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using HLP.WebService;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Security.Cryptography.X509Certificates;
using HLP.bel.Static;
using HLP.bel.NFe.GeraXml;


namespace HLP.bel
{
    public class belNfeRecepcao
    {
        string _recibo;
        public string Recibo
        {
            get { return _recibo; }
            set { _recibo = value; }
        }
        private int _ptpamb;

        public int Ptpamb
        {
            get { return _ptpamb; }
            set { _ptpamb = value; }
        }
        private X509Certificate2 _cert;

        /// <summary>
        /// Método que transmite para o WebService Estadual
        /// </summary>
        /// <param name="UF_Empresa"></param>
        private void TransmitirLote(string UF_Empresa)
        {
            string sRet = string.Empty;

            try
            {
                AssinaNFeXml BC = new AssinaNFeXml();
                XNamespace xname = "http://www.portalfiscal.inf.br/nfe";
                // Diego - O.S 24489 - 26/05/2010
                switch (UF_Empresa)
                {
                    case "SP":
                        {
                            #region Regiao_SP
                            if (_ptpamb == 2)
                            {
                                HLP.WebService.v2_Homologacao_NFeRecepcao_SP.nfeCabecMsg cabec = new HLP.WebService.v2_Homologacao_NFeRecepcao_SP.nfeCabecMsg();
                                HLP.WebService.v2_Homologacao_NFeRecepcao_SP.NfeRecepcao2 ws2 = new HLP.WebService.v2_Homologacao_NFeRecepcao_SP.NfeRecepcao2();
                                belUF objbelUf = new belUF();
                                cabec.cUF = objbelUf.RetornaCUF(UF_Empresa);
                                cabec.versaoDados = _pversaoaDados;
                                ws2.nfeCabecMsgValue = cabec;
                                ws2.ClientCertificates.Add(_cert);

                                XmlDocument _xmlxelem = new XmlDocument();
                                _xmlxelem.PreserveWhitespace = true;
                                _xmlxelem.LoadXml(_xelem);

                                XmlNode xNelem = null;
                                xNelem = _xmlxelem.DocumentElement;

                                sRet = ws2.nfeRecepcaoLote2(xNelem).OuterXml;

                                int lugar = sRet.IndexOf("<nRec>");

                                string pRec = sRet.Substring(lugar + 6, 15);

                                Recibo = pRec;

                                XElement Elemento = XElement.Parse(sRet);

                                // Busca do status da conexao
                                XContainer container = new XElement("infRec");

                                var Status =
                                    from b in Elemento.Elements(xname + "cStat")

                                    select new  // Depois da query adicionamos propriedades ao var Filme para estarem acessiveis no foreach
                                    {
                                        Status = (string)b.Value
                                    };
                                foreach (var Stat in Status)
                                {
                                    this.Cstat = Convert.ToInt16(Stat.Status);
                                }
                                //

                                // Busca do Descricao do Motivo do status                
                                var Motivo =
                                    from b in Elemento.Elements(xname + "xMotivo")

                                    select new  // Depois da query adicionamos propriedades ao var Filme para estarem acessiveis no foreach
                                    {
                                        Motivo = (string)b.Value
                                    };
                                foreach (var xMotivo in Motivo)
                                {
                                    this.Xmotivo = xMotivo.Motivo;
                                }

                                this.Nrec = sRet.Substring(sRet.IndexOf("<nRec>") + 6, ((sRet.IndexOf("</nRec>") - (sRet.IndexOf("<nRec>"))) - 6));
                                this.Dhrecbto = Convert.ToDateTime(sRet.Substring(sRet.IndexOf("<dhRecbto>") + 10, ((sRet.IndexOf("</dhRecbto>") - (sRet.IndexOf("<dhRecbto>"))) - 10)));
                                this.Tmed = Convert.ToInt16(sRet.Substring(sRet.IndexOf("<tMed>") + 6, ((sRet.IndexOf("</tMed>") - (sRet.IndexOf("<tMed>"))) - 6)));
                            }
                            else
                            {
                                if (_ptpamb == 1)
                                {
                                    HLP.WebService.v2_Producao_NFeRecepcao_SP.nfeCabecMsg cabec = new HLP.WebService.v2_Producao_NFeRecepcao_SP.nfeCabecMsg();
                                    HLP.WebService.v2_Producao_NFeRecepcao_SP.NfeRecepcao2 ws2 = new HLP.WebService.v2_Producao_NFeRecepcao_SP.NfeRecepcao2();
                                    belUF objbelUf = new belUF();
                                    cabec.cUF = objbelUf.RetornaCUF(UF_Empresa);
                                    cabec.versaoDados = _pversaoaDados;
                                    ws2.nfeCabecMsgValue = cabec;
                                    ws2.ClientCertificates.Add(_cert);

                                    XmlDocument _xmlxelem = new XmlDocument();
                                    _xmlxelem.PreserveWhitespace = true;
                                    _xmlxelem.LoadXml(_xelem);

                                    XmlNode xNelem = null;
                                    xNelem = _xmlxelem.DocumentElement;

                                    sRet = ws2.nfeRecepcaoLote2(xNelem).OuterXml;

                                    int lugar = sRet.IndexOf("<nRec>");

                                    string pRec = sRet.Substring(lugar + 6, 15);

                                    Recibo = pRec;

                                }
                                else
                                {
                                    throw new Exception("tpamb com valor incorreto");
                                }
                            }
                            #endregion
                        }
                        break;
                    case "MS":
                        {
                            #region Regiao_MS
                            if (_ptpamb == 2)
                            {
                                HLP.WebService.v2_Homologacao_NFeRecepcao_MS.nfeCabecMsg cabec = new HLP.WebService.v2_Homologacao_NFeRecepcao_MS.nfeCabecMsg();
                                HLP.WebService.v2_Homologacao_NFeRecepcao_MS.NfeRecepcao2 ws2 = new HLP.WebService.v2_Homologacao_NFeRecepcao_MS.NfeRecepcao2();
                                belUF objbelUf = new belUF();
                                cabec.cUF = objbelUf.RetornaCUF(UF_Empresa);
                                cabec.versaoDados = _pversaoaDados;
                                ws2.nfeCabecMsgValue = cabec;
                                ws2.ClientCertificates.Add(_cert);

                                XmlDocument _xmlxelem = new XmlDocument();
                                _xmlxelem.PreserveWhitespace = true;
                                _xmlxelem.LoadXml(_xelem);

                                XmlNode xNelem = null;
                                xNelem = _xmlxelem.DocumentElement;

                                sRet = ws2.nfeRecepcaoLote2(xNelem).OuterXml;

                                int lugar = sRet.IndexOf("<nRec>");

                                string pRec = sRet.Substring(lugar + 6, 15);

                                Recibo = pRec;

                                XElement Elemento = XElement.Parse(sRet);

                                // Busca do status da conexao
                                XContainer container = new XElement("infRec");

                                var Status =
                                    from b in Elemento.Elements(xname + "cStat")

                                    select new  // Depois da query adicionamos propriedades ao var Filme para estarem acessiveis no foreach
                                    {
                                        Status = (string)b.Value
                                    };
                                foreach (var Stat in Status)
                                {
                                    this.Cstat = Convert.ToInt16(Stat.Status);
                                }
                                //

                                // Busca do Descricao do Motivo do status                
                                var Motivo =
                                    from b in Elemento.Elements(xname + "xMotivo")

                                    select new  // Depois da query adicionamos propriedades ao var Filme para estarem acessiveis no foreach
                                    {
                                        Motivo = (string)b.Value
                                    };
                                foreach (var xMotivo in Motivo)
                                {
                                    this.Xmotivo = xMotivo.Motivo;
                                }

                                this.Nrec = sRet.Substring(sRet.IndexOf("<nRec>") + 6, ((sRet.IndexOf("</nRec>") - (sRet.IndexOf("<nRec>"))) - 6));
                                this.Dhrecbto = Convert.ToDateTime(sRet.Substring(sRet.IndexOf("<dhRecbto>") + 10, ((sRet.IndexOf("</dhRecbto>") - (sRet.IndexOf("<dhRecbto>"))) - 10)));
                                this.Tmed = Convert.ToInt16(sRet.Substring(sRet.IndexOf("<tMed>") + 6, ((sRet.IndexOf("</tMed>") - (sRet.IndexOf("<tMed>"))) - 6)));
                            }
                            else
                            {
                                if (_ptpamb == 1)
                                {
                                    HLP.WebService.v2_Producao_NFeRecepcao_MS.nfeCabecMsg cabec = new HLP.WebService.v2_Producao_NFeRecepcao_MS.nfeCabecMsg();
                                    HLP.WebService.v2_Producao_NFeRecepcao_MS.NfeRecepcao2 ws2 = new HLP.WebService.v2_Producao_NFeRecepcao_MS.NfeRecepcao2();
                                    belUF objbelUf = new belUF();
                                    cabec.cUF = objbelUf.RetornaCUF(UF_Empresa);
                                    cabec.versaoDados = _pversaoaDados;
                                    ws2.nfeCabecMsgValue = cabec;
                                    ws2.ClientCertificates.Add(_cert);

                                    XmlDocument _xmlxelem = new XmlDocument();
                                    _xmlxelem.PreserveWhitespace = true;
                                    _xmlxelem.LoadXml(_xelem);

                                    XmlNode xNelem = null;
                                    xNelem = _xmlxelem.DocumentElement;

                                    sRet = ws2.nfeRecepcaoLote2(xNelem).OuterXml;

                                    int lugar = sRet.IndexOf("<nRec>");

                                    string pRec = sRet.Substring(lugar + 6, 15);

                                    Recibo = pRec;
                                }
                                else
                                {
                                    throw new Exception("tpamb com valor incorreto");
                                }
                            }
                            #endregion
                        }
                        break;
                    case "RS":
                        {
                            #region Regiao_RS
                            if (_ptpamb == 2)
                            {
                                HLP.WebService.v2_Homologacao_NFeRecepcao_RS.nfeCabecMsg cabec = new HLP.WebService.v2_Homologacao_NFeRecepcao_RS.nfeCabecMsg();
                                HLP.WebService.v2_Homologacao_NFeRecepcao_RS.NfeRecepcao2 ws2 = new HLP.WebService.v2_Homologacao_NFeRecepcao_RS.NfeRecepcao2();
                                belUF objbelUf = new belUF();
                                cabec.cUF = objbelUf.RetornaCUF(UF_Empresa);
                                cabec.versaoDados = _pversaoaDados;
                                ws2.nfeCabecMsgValue = cabec;
                                ws2.ClientCertificates.Add(_cert);

                                XmlDocument _xmlxelem = new XmlDocument();
                                _xmlxelem.PreserveWhitespace = true;
                                _xmlxelem.LoadXml(_xelem);

                                XmlNode xNelem = null;
                                xNelem = _xmlxelem.DocumentElement;

                                sRet = ws2.nfeRecepcaoLote2(xNelem).OuterXml;

                                int lugar = sRet.IndexOf("<nRec>");

                                string pRec = sRet.Substring(lugar + 6, 15);

                                Recibo = pRec;

                                XElement Elemento = XElement.Parse(sRet);

                                //Elemento.Save(@"C:\Clientes\NAVETHERM\NFe\testerec.xml");

                                // Busca do status da conexao
                                XContainer container = new XElement("infRec");

                                var Status =
                                    from b in Elemento.Elements(xname + "cStat")

                                    select new  // Depois da query adicionamos propriedades ao var Filme para estarem acessiveis no foreach
                                    {
                                        Status = (string)b.Value
                                    };
                                foreach (var Stat in Status)
                                {
                                    this.Cstat = Convert.ToInt16(Stat.Status);
                                }
                                //

                                // Busca do Descricao do Motivo do status                
                                var Motivo =
                                    from b in Elemento.Elements(xname + "xMotivo")

                                    select new  // Depois da query adicionamos propriedades ao var Filme para estarem acessiveis no foreach
                                    {
                                        Motivo = (string)b.Value
                                    };
                                foreach (var xMotivo in Motivo)
                                {
                                    this.Xmotivo = xMotivo.Motivo;
                                }

                                this.Nrec = sRet.Substring(sRet.IndexOf("<nRec>") + 6, ((sRet.IndexOf("</nRec>") - (sRet.IndexOf("<nRec>"))) - 6));
                                this.Dhrecbto = Convert.ToDateTime(sRet.Substring(sRet.IndexOf("<dhRecbto>") + 10, ((sRet.IndexOf("</dhRecbto>") - (sRet.IndexOf("<dhRecbto>"))) - 10)));
                                this.Tmed = Convert.ToInt16(sRet.Substring(sRet.IndexOf("<tMed>") + 6, ((sRet.IndexOf("</tMed>") - (sRet.IndexOf("<tMed>"))) - 6)));
                            }
                            else
                            {
                                if (_ptpamb == 1)
                                {
                                    HLP.WebService.v2_Producao_NFeRecepcao_RS.nfeCabecMsg cabec = new HLP.WebService.v2_Producao_NFeRecepcao_RS.nfeCabecMsg();
                                    HLP.WebService.v2_Producao_NFeRecepcao_RS.NfeRecepcao2 ws2 = new HLP.WebService.v2_Producao_NFeRecepcao_RS.NfeRecepcao2();
                                    belUF objbelUf = new belUF();
                                    cabec.cUF = objbelUf.RetornaCUF(UF_Empresa);
                                    cabec.versaoDados = _pversaoaDados;
                                    ws2.nfeCabecMsgValue = cabec;
                                    ws2.ClientCertificates.Add(_cert);

                                    XmlDocument _xmlxelem = new XmlDocument();
                                    _xmlxelem.PreserveWhitespace = true;
                                    _xmlxelem.LoadXml(_xelem);

                                    XmlNode xNelem = null;
                                    xNelem = _xmlxelem.DocumentElement;

                                    sRet = ws2.nfeRecepcaoLote2(xNelem).OuterXml;

                                    int lugar = sRet.IndexOf("<nRec>");

                                    string pRec = sRet.Substring(lugar + 6, 15);

                                    Recibo = pRec;

                                }
                                else
                                {
                                    throw new Exception("tpamb com valor incorreto");
                                }
                            }
                            #endregion
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// Método que transmite Para o WebService Federal Scan
        /// </summary>
        /// <param name="iSerieScan"></param>
        private void TransmitirLote(int iSerieScanM, string cUF)
        {
            AssinaNFeXml BC = new AssinaNFeXml();
            string sRet = string.Empty;
            XNamespace xname = "http://www.portalfiscal.inf.br/nfe";

            #region ENVIO WEB SERVICE SCAN
            if (_ptpamb == 2)
            {
                HLP.WebService.v2_SCAN_Homologacao_NFeRecepcao.nfeCabecMsg cabec = new HLP.WebService.v2_SCAN_Homologacao_NFeRecepcao.nfeCabecMsg();
                HLP.WebService.v2_SCAN_Homologacao_NFeRecepcao.NfeRecepcao2 ws2 = new HLP.WebService.v2_SCAN_Homologacao_NFeRecepcao.NfeRecepcao2();
                belUF objbelUf = new belUF();
                cabec.cUF = objbelUf.RetornaCUF(cUF);
                cabec.versaoDados = _pversaoaDados;
                ws2.nfeCabecMsgValue = cabec;
                ws2.ClientCertificates.Add(_cert);

                XmlDocument _xmlxelem = new XmlDocument();
                _xmlxelem.PreserveWhitespace = true;
                _xmlxelem.LoadXml(_xelem);

                XmlNode xNelem = null;
                xNelem = _xmlxelem.DocumentElement;

                sRet = ws2.nfeRecepcaoLote2(xNelem).OuterXml;

                int lugar = sRet.IndexOf("<nRec>");

                string pRec = sRet.Substring(lugar + 6, 15);

                Recibo = pRec;

                XElement Elemento = XElement.Parse(sRet);

                // Busca do status da conexao
                XContainer container = new XElement("infRec");

                var Status =
                    from b in Elemento.Elements(xname + "cStat")

                    select new  // Depois da query adicionamos propriedades ao var Filme para estarem acessiveis no foreach
                    {
                        Status = (string)b.Value
                    };
                foreach (var Stat in Status)
                {
                    this.Cstat = Convert.ToInt16(Stat.Status);
                }
                //

                // Busca do Descricao do Motivo do status                
                var Motivo =
                    from b in Elemento.Elements(xname + "xMotivo")

                    select new  // Depois da query adicionamos propriedades ao var Filme para estarem acessiveis no foreach
                    {
                        Motivo = (string)b.Value
                    };
                foreach (var xMotivo in Motivo)
                {
                    this.Xmotivo = xMotivo.Motivo;
                }

                this.Nrec = sRet.Substring(sRet.IndexOf("<nRec>") + 6, ((sRet.IndexOf("</nRec>") - (sRet.IndexOf("<nRec>"))) - 6));
                this.Dhrecbto = Convert.ToDateTime(sRet.Substring(sRet.IndexOf("<dhRecbto>") + 10, ((sRet.IndexOf("</dhRecbto>") - (sRet.IndexOf("<dhRecbto>"))) - 10)));
                this.Tmed = Convert.ToInt16(sRet.Substring(sRet.IndexOf("<tMed>") + 6, ((sRet.IndexOf("</tMed>") - (sRet.IndexOf("<tMed>"))) - 6)));
            }
            else
            {
                if (_ptpamb == 1)
                {
                    HLP.WebService.v2_SCAN_Producao_NFeRecepcao.nfeCabecMsg cabec = new HLP.WebService.v2_SCAN_Producao_NFeRecepcao.nfeCabecMsg();
                    HLP.WebService.v2_SCAN_Producao_NFeRecepcao.NfeRecepcao2 ws2 = new HLP.WebService.v2_SCAN_Producao_NFeRecepcao.NfeRecepcao2();
                    belUF objbelUf = new belUF();
                    cabec.cUF = objbelUf.RetornaCUF(cUF);
                    cabec.versaoDados = _pversaoaDados;
                    ws2.nfeCabecMsgValue = cabec;
                    ws2.ClientCertificates.Add(_cert);

                    XmlDocument _xmlxelem = new XmlDocument();
                    _xmlxelem.PreserveWhitespace = true;
                    _xmlxelem.LoadXml(_xelem);

                    XmlNode xNelem = null;
                    xNelem = _xmlxelem.DocumentElement;

                    sRet = ws2.nfeRecepcaoLote2(xNelem).OuterXml;

                    int lugar = sRet.IndexOf("<nRec>");

                    string pRec = sRet.Substring(lugar + 6, 15);

                    Recibo = pRec;

                }
                else
                {
                    throw new Exception("tpamb com valor incorreto");
                }
            }
            #endregion
        }
        /// <summary>
        /// Envio do XML para a Fazenda.
        /// </summary>
        /// <param name="sCaminhoXml"></param>
        /// <param name="spVersao"></param>
        /// <param name="spVersaoaplic"></param>
        /// <param name="sptpAmb"></param>
        /// <param name="cert"></param>
        /// <param name="UF_Empresa"></param>
        public belNfeRecepcao(string sCaminhoXml,string VersaoDados, X509Certificate2 cert, string UF_Empresa, bool bModoSCAN, int iSerieSCAN)
        {
            _xelem = sCaminhoXml;
            _ptpamb = belStatic.tpAmb;
            _pversaoaDados = VersaoDados;
            _cert = cert;

            if (bModoSCAN)
            {
                TransmitirLote(iSerieSCAN, UF_Empresa);
            }
            else
            {
                TransmitirLote(UF_Empresa);
            }

        }

      

        private string _pversao;

        public string Pversao
        {
            get { return _pversao; }
            set { _pversao = value; }
        }
        private string _pversaoaDados;

        public string Pversaoaplic
        {
            get { return _pversaoaDados; }
            set { _pversaoaDados = value; }
        }

        private string _caminhoxml;

        public string Caminhoxml
        {
            get { return _caminhoxml; }
            set { _caminhoxml = value; }
        }

        private string _xelem;

        public string Xelem
        {
            get { return _xelem; }
            set { _xelem = value; }
        }

        private int _cstat;

        public int Cstat
        {
            get { return _cstat; }
            set { _cstat = value; }
        }
        private string _xmotivo;

        public string Xmotivo
        {
            get { return _xmotivo; }
            set { _xmotivo = value; }
        }
        private string _nrec;

        public string Nrec
        {
            get { return _nrec; }
            set { _nrec = value; }
        }
        private DateTime _dhrecbto;

        public DateTime Dhrecbto
        {
            get { return _dhrecbto; }
            set { _dhrecbto = value; }
        }
        private int _tmed;

        public int Tmed
        {
            get { return _tmed; }
            set { _tmed = value; }
        }

    }
}
