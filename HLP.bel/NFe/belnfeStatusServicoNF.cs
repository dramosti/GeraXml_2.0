using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Web.Services;
using HLP.WebService;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using HLP.bel.Static;
using HLP.bel.NFe.GeraXml;


namespace HLP.bel
{
    public class belnfeStatusServicoNF
    {
        private string _retconsstatserv;

        public int iTentativasWebServices { get; set; }

        private string _versao;
        public string Versao
        {
            get { return _versao; }
            set { _versao = value; }
        }
        private int _tpamb;

        public int Tpamb
        {
            get { return _tpamb; }
            set { _tpamb = value; }
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
        private string _cuf;

        public string Cuf
        {
            get { return _cuf; }
            set { _cuf = value; }
        }
        private DateTime _dhrecibo;

        public DateTime Dhrecibo
        {
            get { return _dhrecibo; }
            set { _dhrecibo = value; }
        }
        private int _tmed;

        public int Tmed
        {
            get { return _tmed; }
            set { _tmed = value; }
        }
        private DateTime _dhretorno;

        public DateTime Dhretorno
        {
            get { return _dhretorno; }
            set { _dhretorno = value; }
        }
        private string _xobs;

        public string Xobs
        {
            get { return _xobs; }
            set { _xobs = value; }
        }

        public X509Certificate2 Cert { get; set; }

        public belnfeStatusServicoNF(string sVersao, string spcUF,
                                     X509Certificate2 xcert, string UF_Empresa)
        {
            belUF objbelUf = new belUF();
            Versao = sVersao;
            Tpamb = belStatic.tpAmb;
            Cuf = objbelUf.RetornaCUF(spcUF);
            Cert = xcert;


            try
            {
                switch (UF_Empresa)
                {
                    case "SP":
                        {
                            ConsultaServico_SP();
                        }
                        break;
                    case "MS":
                        {
                            ConsultaServico_MS();
                        }
                        break;
                    case "RS":
                        {
                            ConsultaServico_RS();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                iTentativasWebServices = iTentativasWebServices + 1;

                if (iTentativasWebServices == 1)
                {
                    ConsultaServicoSCAN();
                }
                else
                {
                    throw ex;
                }
            }
        }


        private string NfeCabecMsg()
        {
            XNamespace nome = "http://www.portalfiscal.inf.br/nfe";
            XDocument doc = new XDocument(new XElement(nome + "cabecMsg", new XAttribute("versao", ""), new XAttribute("xmlns", "http://www.portalfiscal.inf.br/nfe"),
                                          new XElement(nome + "versaoDados", _pversaoaplic)));
            string RetXmlString = doc.ToString();
            RetXmlString = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + RetXmlString;
            return RetXmlString;
        }

        private string _pversaoaplic;

        private XmlDocument NfeDadosMsg()
        {
            try
            {
                string NFeNamespace = "http://www.portalfiscal.inf.br/nfe";
                XmlDocument doc = new XmlDocument();
                XmlElement xraiz, xtpAmb, xcUF, xServ;
                XmlAttribute vs, xmlns;

                xraiz = doc.CreateElement("consStatServ");

                vs = doc.CreateAttribute("versao");
                vs.Value = this.Versao;
                xraiz.Attributes.Append(vs);

                xmlns = doc.CreateAttribute("xmlns");
                xmlns.Value = NFeNamespace;
                xraiz.Attributes.Append(xmlns);

                xtpAmb = doc.CreateElement("tpAmb");
                xtpAmb.InnerText = Tpamb.ToString(); ;
                xraiz.AppendChild(xtpAmb);

                xcUF = doc.CreateElement("cUF");
                xcUF.InnerText = Cuf;
                xraiz.AppendChild(xcUF);

                xServ = doc.CreateElement("xServ");
                xServ.InnerText = "STATUS";
                xraiz.AppendChild(xServ);

                doc.AppendChild(xraiz);
                XmlProcessingInstruction pi = doc.CreateProcessingInstruction("xml", "version=\"1.0\" encoding=\"UTF-8\"");
                doc.InsertBefore(pi, xraiz);
                return doc;
            }
            catch (Exception ex)
            {
                throw ex;
            }




        }

        private void ConsultaServico_RS()
        {
            XmlDocument xdDadosMsg = NfeDadosMsg();
            AssinaNFeXml BC = new AssinaNFeXml();

            try
            {
                if (Tpamb == 2)
                {
                    HLP.WebService.v2_Homologacao_NFeStatusServico_RS.NfeStatusServico2 ws2 = new HLP.WebService.v2_Homologacao_NFeStatusServico_RS.NfeStatusServico2();
                    HLP.WebService.v2_Homologacao_NFeStatusServico_RS.nfeCabecMsg cabec = new HLP.WebService.v2_Homologacao_NFeStatusServico_RS.nfeCabecMsg();
                    cabec.cUF = Cuf;
                    cabec.versaoDados = _versao;
                    ws2.nfeCabecMsgValue = cabec;
                    ws2.ClientCertificates.Add(Cert);

                    XmlNode xmlDados = null;
                    xmlDados = xdDadosMsg.DocumentElement;

                    string resp = ws2.nfeStatusServicoNF2(xmlDados).OuterXml;

                    XElement Elemento = XElement.Parse(resp);

                    XNamespace xname = "http://www.portalfiscal.inf.br/nfe";

                    // Busca do status da conexao
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
                    //

                    //Mostra o tempo medio de resposta do site.                
                    var tMed =
                        from b in Elemento.Elements(xname + "tMed")

                        select new  // Depois da query adicionamos propriedades ao var Filme para estarem acessiveis no foreach
                        {
                            TempoMedio = (string)b.Value
                        };
                    foreach (var TempoMedio in tMed)
                    {
                        this.Tmed = Convert.ToInt32(TempoMedio.TempoMedio);
                    }
                    //

                    //Mostra o data e hora do recibo.                
                    var dhRecibo =
                        from b in Elemento.Elements(xname + "dhRecbto")

                        select new  // Depois da query adicionamos propriedades ao var Filme para estarem acessiveis no foreach
                        {
                            datahoraRecibo = (string)b.Value
                        };
                    foreach (var dhrec in dhRecibo)
                    {
                        this.Dhrecibo = Convert.ToDateTime(dhrec.datahoraRecibo);
                    }
                    //

                    //Mostra o data e hora do recibo.                
                    var dhRetorno =
                        from b in Elemento.Elements(xname + "dhRetorno")

                        select new  // Depois da query adicionamos propriedades ao var Filme para estarem acessiveis no foreach
                        {
                            datahoraRetorno = (string)b.Value
                        };
                    foreach (var dhret in dhRetorno)
                    {
                        this.Dhretorno = Convert.ToDateTime(dhret.datahoraRetorno);
                    }
                    // Versão do Aplicativo que processou a consulta
                    var verAplic =
                       from b in Elemento.Elements(xname + "dhRetorno")

                       select new  // Depois da query adicionamos propriedades ao var Filme para estarem acessiveis no foreach
                       {
                           verAplicativo = (string)b.Value
                       };
                    foreach (var v in verAplic)
                    {
                        this.Versao = Convert.ToString(v.verAplicativo);
                    }
                }
                else
                {
                    XNamespace xname = "http://www.portalfiscal.inf.br/nfe";

                    HLP.WebService.v2_Producao_NFeStatusServico_RS.NfeStatusServico2 ws2 = new HLP.WebService.v2_Producao_NFeStatusServico_RS.NfeStatusServico2();
                    HLP.WebService.v2_Producao_NFeStatusServico_RS.nfeCabecMsg cabec = new HLP.WebService.v2_Producao_NFeStatusServico_RS.nfeCabecMsg();
                    cabec.cUF = Cuf;
                    cabec.versaoDados = _versao;
                    ws2.nfeCabecMsgValue = cabec;
                    ws2.ClientCertificates.Add(Cert);

                    XmlNode xmlDados = null;
                    xmlDados = xdDadosMsg.DocumentElement;

                    string resp = ws2.nfeStatusServicoNF2(xmlDados).OuterXml;

                    XElement Elemento = XElement.Parse(resp);


                    // Busca do status da conexao
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
                    //

                    //Mostra o tempo medio de resposta do site.                
                    var tMed =
                        from b in Elemento.Elements(xname + "tMed")

                        select new  // Depois da query adicionamos propriedades ao var Filme para estarem acessiveis no foreach
                        {
                            TempoMedio = (string)b.Value
                        };
                    foreach (var TempoMedio in tMed)
                    {
                        this.Tmed = Convert.ToInt32(TempoMedio.TempoMedio);
                    }
                    //

                    //Mostra o data e hora do recibo.                
                    var dhRecibo =
                        from b in Elemento.Elements(xname + "dhRecbto")

                        select new  // Depois da query adicionamos propriedades ao var Filme para estarem acessiveis no foreach
                        {
                            datahoraRecibo = (string)b.Value
                        };
                    foreach (var dhrec in dhRecibo)
                    {
                        this.Dhrecibo = Convert.ToDateTime(dhrec.datahoraRecibo);
                    }
                    //

                    //Mostra o data e hora do recibo.                
                    var dhRetorno =
                        from b in Elemento.Elements(xname + "dhRetorno")

                        select new  // Depois da query adicionamos propriedades ao var Filme para estarem acessiveis no foreach
                        {
                            datahoraRetorno = (string)b.Value
                        };
                    foreach (var dhret in dhRetorno)
                    {
                        this.Dhretorno = Convert.ToDateTime(dhret.datahoraRetorno);
                    }
                }
            }
            catch (Exception x)
            {
                throw new Exception("Erro no Teste de Conexão - " + x.Message);
            }
            //Fim - Danner - o.s.23732 - 11/11/2009

        }

        private void ConsultaServicoSCAN()
        {
            XmlDocument xdDadosMsg = NfeDadosMsg();
            AssinaNFeXml BC = new AssinaNFeXml();

            try
            {
                if (Tpamb == 2)
                {
                    HLP.WebService.v2_SCAN_Homologacao_NFeStatusServico.NfeStatusServico2 ws2 = new HLP.WebService.v2_SCAN_Homologacao_NFeStatusServico.NfeStatusServico2();
                    HLP.WebService.v2_SCAN_Homologacao_NFeStatusServico.nfeCabecMsg cabec = new HLP.WebService.v2_SCAN_Homologacao_NFeStatusServico.nfeCabecMsg();
                    cabec.cUF = Cuf;
                    cabec.versaoDados = _versao;
                    ws2.nfeCabecMsgValue = cabec;
                    ws2.ClientCertificates.Add(Cert);

                    XmlNode xmlDados = null;
                    xmlDados = xdDadosMsg.DocumentElement;

                    string resp = ws2.nfeStatusServicoNF2(xmlDados).OuterXml;

                    XElement Elemento = XElement.Parse(resp);

                    XNamespace xname = "http://www.portalfiscal.inf.br/nfe";

                    // Busca do status da conexao
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
                            Motivo = "SCAN - " + (string)b.Value
                        };
                    foreach (var xMotivo in Motivo)
                    {
                        this.Xmotivo = xMotivo.Motivo;
                    }
                    //

                    //Mostra o tempo medio de resposta do site.                
                    var tMed =
                        from b in Elemento.Elements(xname + "tMed")

                        select new  // Depois da query adicionamos propriedades ao var Filme para estarem acessiveis no foreach
                        {
                            TempoMedio = (string)b.Value
                        };
                    foreach (var TempoMedio in tMed)
                    {
                        this.Tmed = Convert.ToInt32(TempoMedio.TempoMedio);
                    }
                    //

                    //Mostra o data e hora do recibo.                
                    var dhRecibo =
                        from b in Elemento.Elements(xname + "dhRecbto")

                        select new  // Depois da query adicionamos propriedades ao var Filme para estarem acessiveis no foreach
                        {
                            datahoraRecibo = (string)b.Value
                        };
                    foreach (var dhrec in dhRecibo)
                    {
                        this.Dhrecibo = Convert.ToDateTime(dhrec.datahoraRecibo);
                    }
                    //

                    //Mostra o data e hora do recibo.                
                    var dhRetorno =
                        from b in Elemento.Elements(xname + "dhRetorno")

                        select new  // Depois da query adicionamos propriedades ao var Filme para estarem acessiveis no foreach
                        {
                            datahoraRetorno = (string)b.Value
                        };
                    foreach (var dhret in dhRetorno)
                    {
                        this.Dhretorno = Convert.ToDateTime(dhret.datahoraRetorno);
                    }
                    // Versão do Aplicativo que processou a consulta
                    var verAplic =
                       from b in Elemento.Elements(xname + "dhRetorno")

                       select new  // Depois da query adicionamos propriedades ao var Filme para estarem acessiveis no foreach
                       {
                           verAplicativo = (string)b.Value
                       };
                    foreach (var v in verAplic)
                    {
                        this.Versao = Convert.ToString(v.verAplicativo);
                    }
                }
                else
                {
                    XNamespace xname = "http://www.portalfiscal.inf.br/nfe";

                    HLP.WebService.v2_SCAN_Producao_NFeStatusServico.NfeStatusServico2 ws2 = new HLP.WebService.v2_SCAN_Producao_NFeStatusServico.NfeStatusServico2();
                    HLP.WebService.v2_SCAN_Producao_NFeStatusServico.nfeCabecMsg cabec = new HLP.WebService.v2_SCAN_Producao_NFeStatusServico.nfeCabecMsg();
                    cabec.cUF = Cuf;
                    cabec.versaoDados = _versao;
                    ws2.nfeCabecMsgValue = cabec;
                    ws2.ClientCertificates.Add(Cert);

                    XmlNode xmlDados = null;
                    xmlDados = xdDadosMsg.DocumentElement;

                    string resp = ws2.nfeStatusServicoNF2(xmlDados).OuterXml;

                    XElement Elemento = XElement.Parse(resp);


                    // Busca do status da conexao
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
                            Motivo = "SCAN - " + (string)b.Value
                        };
                    foreach (var xMotivo in Motivo)
                    {
                        this.Xmotivo = xMotivo.Motivo;
                    }
                    //

                    //Mostra o tempo medio de resposta do site.                
                    var tMed =
                        from b in Elemento.Elements(xname + "tMed")

                        select new  // Depois da query adicionamos propriedades ao var Filme para estarem acessiveis no foreach
                        {
                            TempoMedio = (string)b.Value
                        };
                    foreach (var TempoMedio in tMed)
                    {
                        this.Tmed = Convert.ToInt32(TempoMedio.TempoMedio);
                    }
                    //

                    //Mostra o data e hora do recibo.                
                    var dhRecibo =
                        from b in Elemento.Elements(xname + "dhRecbto")

                        select new  // Depois da query adicionamos propriedades ao var Filme para estarem acessiveis no foreach
                        {
                            datahoraRecibo = (string)b.Value
                        };
                    foreach (var dhrec in dhRecibo)
                    {
                        this.Dhrecibo = Convert.ToDateTime(dhrec.datahoraRecibo);
                    }
                    //

                    //Mostra o data e hora do recibo.                
                    var dhRetorno =
                        from b in Elemento.Elements(xname + "dhRetorno")

                        select new  // Depois da query adicionamos propriedades ao var Filme para estarem acessiveis no foreach
                        {
                            datahoraRetorno = (string)b.Value
                        };
                    foreach (var dhret in dhRetorno)
                    {
                        this.Dhretorno = Convert.ToDateTime(dhret.datahoraRetorno);
                    }
                }
            }
            catch (Exception x)
            {
                throw new Exception("Erro no Teste de Conexão - " + x.Message);
            }
        }

        private void ConsultaServico_SP()
        {
            string snfeCabecMsg = NfeCabecMsg();
            XmlDocument xdDadosMsg = NfeDadosMsg();
            AssinaNFeXml BC = new AssinaNFeXml();
            try
            {
                if (Tpamb == 2)
                {
                    HLP.WebService.v2_Homologacao_NFeStatusServico_SP.NfeStatusServico2 ws2 = new HLP.WebService.v2_Homologacao_NFeStatusServico_SP.NfeStatusServico2();
                    HLP.WebService.v2_Homologacao_NFeStatusServico_SP.nfeCabecMsg cabec = new HLP.WebService.v2_Homologacao_NFeStatusServico_SP.nfeCabecMsg();
                    cabec.cUF = Cuf;
                    cabec.versaoDados = _versao;
                    ws2.nfeCabecMsgValue = cabec;
                    ws2.ClientCertificates.Add(Cert);

                    XmlNode xmlDados = null;
                    xmlDados = xdDadosMsg.DocumentElement;

                    string resp = ws2.nfeStatusServicoNF2(xmlDados).OuterXml;

                    XElement Elemento = XElement.Parse(resp);

                    XNamespace xname = "http://www.portalfiscal.inf.br/nfe";

                    // Busca do status da conexao
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
                    //

                    //Mostra o tempo medio de resposta do site.                
                    var tMed =
                        from b in Elemento.Elements(xname + "tMed")

                        select new  // Depois da query adicionamos propriedades ao var Filme para estarem acessiveis no foreach
                        {
                            TempoMedio = (string)b.Value
                        };
                    foreach (var TempoMedio in tMed)
                    {
                        this.Tmed = Convert.ToInt32(TempoMedio.TempoMedio);
                    }
                    //

                    //Mostra o data e hora do recibo.                
                    var dhRecibo =
                        from b in Elemento.Elements(xname + "dhRecbto")

                        select new  // Depois da query adicionamos propriedades ao var Filme para estarem acessiveis no foreach
                        {
                            datahoraRecibo = (string)b.Value
                        };
                    foreach (var dhrec in dhRecibo)
                    {
                        this.Dhrecibo = Convert.ToDateTime(dhrec.datahoraRecibo);
                    }
                    //

                    //Mostra o data e hora do recibo.                
                    var dhRetorno =
                        from b in Elemento.Elements(xname + "dhRetorno")

                        select new  // Depois da query adicionamos propriedades ao var Filme para estarem acessiveis no foreach
                        {
                            datahoraRetorno = (string)b.Value
                        };
                    foreach (var dhret in dhRetorno)
                    {
                        this.Dhretorno = Convert.ToDateTime(dhret.datahoraRetorno);
                    }
                    // Versão do Aplicativo que processou a consulta
                    var verAplic =
                       from b in Elemento.Elements(xname + "dhRetorno")

                       select new  // Depois da query adicionamos propriedades ao var Filme para estarem acessiveis no foreach
                       {
                           verAplicativo = (string)b.Value
                       };
                    foreach (var v in verAplic)
                    {
                        this.Versao = Convert.ToString(v.verAplicativo);
                    }
                }
                else
                {
                    XNamespace xname = "http://www.portalfiscal.inf.br/nfe";

                    HLP.WebService.v2_Producao_NFeStatusServico_SP.NfeStatusServico2 ws2 = new HLP.WebService.v2_Producao_NFeStatusServico_SP.NfeStatusServico2();
                    HLP.WebService.v2_Producao_NFeStatusServico_SP.nfeCabecMsg cabec = new HLP.WebService.v2_Producao_NFeStatusServico_SP.nfeCabecMsg();
                    cabec.cUF = Cuf;
                    cabec.versaoDados = _versao;
                    ws2.nfeCabecMsgValue = cabec;
                    ws2.ClientCertificates.Add(Cert);

                    XmlNode xmlDados = null;
                    xmlDados = xdDadosMsg.DocumentElement;

                    string resp = ws2.nfeStatusServicoNF2(xmlDados).OuterXml;

                    XElement Elemento = XElement.Parse(resp);


                    // Busca do status da conexao
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
                    //

                    //Mostra o tempo medio de resposta do site.                
                    var tMed =
                        from b in Elemento.Elements(xname + "tMed")

                        select new  // Depois da query adicionamos propriedades ao var Filme para estarem acessiveis no foreach
                        {
                            TempoMedio = (string)b.Value
                        };
                    foreach (var TempoMedio in tMed)
                    {
                        this.Tmed = Convert.ToInt32(TempoMedio.TempoMedio);
                    }
                    //

                    //Mostra o data e hora do recibo.                
                    var dhRecibo =
                        from b in Elemento.Elements(xname + "dhRecbto")

                        select new  // Depois da query adicionamos propriedades ao var Filme para estarem acessiveis no foreach
                        {
                            datahoraRecibo = (string)b.Value
                        };
                    foreach (var dhrec in dhRecibo)
                    {
                        this.Dhrecibo = Convert.ToDateTime(dhrec.datahoraRecibo);
                    }
                    //

                    //Mostra o data e hora do recibo.                
                    var dhRetorno =
                        from b in Elemento.Elements(xname + "dhRetorno")

                        select new  // Depois da query adicionamos propriedades ao var Filme para estarem acessiveis no foreach
                        {
                            datahoraRetorno = (string)b.Value
                        };
                    foreach (var dhret in dhRetorno)
                    {
                        this.Dhretorno = Convert.ToDateTime(dhret.datahoraRetorno);
                    }
                }
            }
            catch (Exception x)
            {
                throw new Exception("Erro no Teste de Conexão - " + x.Message);
            }
        }

        private void ConsultaServico_MS()
        {
            string snfeCabecMsg = NfeCabecMsg();
            XmlDocument xdDadosMsg = NfeDadosMsg();
            AssinaNFeXml BC = new AssinaNFeXml();
            try
            {
                if (Tpamb == 2)
                {
                    HLP.WebService.v2_Homologacao_NFeStatusServico_MS.NfeStatusServico2 ws2 = new HLP.WebService.v2_Homologacao_NFeStatusServico_MS.NfeStatusServico2();
                    HLP.WebService.v2_Homologacao_NFeStatusServico_MS.nfeCabecMsg cabec = new HLP.WebService.v2_Homologacao_NFeStatusServico_MS.nfeCabecMsg();
                    cabec.cUF = Cuf;
                    cabec.versaoDados = _versao;
                    ws2.nfeCabecMsgValue = cabec;
                    ws2.ClientCertificates.Add(Cert);

                    XmlNode xmlDados = null;
                    xmlDados = xdDadosMsg.DocumentElement;

                    string resp = ws2.nfeStatusServicoNF2(xmlDados).OuterXml;

                    XElement Elemento = XElement.Parse(resp);

                    XNamespace xname = "http://www.portalfiscal.inf.br/nfe";

                    // Busca do status da conexao
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
                    //

                    //Mostra o tempo medio de resposta do site.                
                    var tMed =
                        from b in Elemento.Elements(xname + "tMed")

                        select new  // Depois da query adicionamos propriedades ao var Filme para estarem acessiveis no foreach
                        {
                            TempoMedio = (string)b.Value
                        };
                    foreach (var TempoMedio in tMed)
                    {
                        this.Tmed = Convert.ToInt32(TempoMedio.TempoMedio);
                    }
                    //

                    //Mostra o data e hora do recibo.                
                    var dhRecibo =
                        from b in Elemento.Elements(xname + "dhRecbto")

                        select new  // Depois da query adicionamos propriedades ao var Filme para estarem acessiveis no foreach
                        {
                            datahoraRecibo = (string)b.Value
                        };
                    foreach (var dhrec in dhRecibo)
                    {
                        this.Dhrecibo = Convert.ToDateTime(dhrec.datahoraRecibo);
                    }
                    //

                    //Mostra o data e hora do recibo.                
                    var dhRetorno =
                        from b in Elemento.Elements(xname + "dhRetorno")

                        select new  // Depois da query adicionamos propriedades ao var Filme para estarem acessiveis no foreach
                        {
                            datahoraRetorno = (string)b.Value
                        };
                    foreach (var dhret in dhRetorno)
                    {
                        this.Dhretorno = Convert.ToDateTime(dhret.datahoraRetorno);
                    }
                    // Versão do Aplicativo que processou a consulta
                    var verAplic =
                       from b in Elemento.Elements(xname + "dhRetorno")

                       select new  // Depois da query adicionamos propriedades ao var Filme para estarem acessiveis no foreach
                       {
                           verAplicativo = (string)b.Value
                       };
                    foreach (var v in verAplic)
                    {
                        this.Versao = Convert.ToString(v.verAplicativo);
                    }
                }
                else
                {
                    XNamespace xname = "http://www.portalfiscal.inf.br/nfe";

                    HLP.WebService.v2_Producao_NFeStatusServico_MS.NfeStatusServico2 ws2 = new HLP.WebService.v2_Producao_NFeStatusServico_MS.NfeStatusServico2();
                    HLP.WebService.v2_Producao_NFeStatusServico_MS.nfeCabecMsg cabec = new HLP.WebService.v2_Producao_NFeStatusServico_MS.nfeCabecMsg();
                    cabec.cUF = Cuf;
                    cabec.versaoDados = _versao;
                    ws2.nfeCabecMsgValue = cabec;
                    ws2.ClientCertificates.Add(Cert);

                    XmlNode xmlDados = null;
                    xmlDados = xdDadosMsg.DocumentElement;

                    string resp = ws2.nfeStatusServicoNF2(xmlDados).OuterXml;

                    XElement Elemento = XElement.Parse(resp);


                    // Busca do status da conexao
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
                    //

                    //Mostra o tempo medio de resposta do site.                
                    var tMed =
                        from b in Elemento.Elements(xname + "tMed")

                        select new  // Depois da query adicionamos propriedades ao var Filme para estarem acessiveis no foreach
                        {
                            TempoMedio = (string)b.Value
                        };
                    foreach (var TempoMedio in tMed)
                    {
                        this.Tmed = Convert.ToInt32(TempoMedio.TempoMedio);
                    }
                    //

                    //Mostra o data e hora do recibo.                
                    var dhRecibo =
                        from b in Elemento.Elements(xname + "dhRecbto")

                        select new  // Depois da query adicionamos propriedades ao var Filme para estarem acessiveis no foreach
                        {
                            datahoraRecibo = (string)b.Value
                        };
                    foreach (var dhrec in dhRecibo)
                    {
                        this.Dhrecibo = Convert.ToDateTime(dhrec.datahoraRecibo);
                    }
                    //

                    //Mostra o data e hora do recibo.                
                    var dhRetorno =
                        from b in Elemento.Elements(xname + "dhRetorno")

                        select new  // Depois da query adicionamos propriedades ao var Filme para estarem acessiveis no foreach
                        {
                            datahoraRetorno = (string)b.Value
                        };
                    foreach (var dhret in dhRetorno)
                    {
                        this.Dhretorno = Convert.ToDateTime(dhret.datahoraRetorno);
                    }
                    //              

                }
            }
            catch (Exception x)
            {
                throw new Exception("Erro no Teste de Conexão - " + x.Message);
            }
        }



    }
}
