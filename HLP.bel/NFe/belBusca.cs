using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Security.Cryptography.X509Certificates;
using FirebirdSql.Data.FirebirdClient;
using System.IO;
using System.Data;
using HLP.bel.NFe.GeraXml;
using HLP.bel.CTe;
using HLP.bel.Static;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;


namespace HLP.bel
{
    public class belBusRetFazenda
    {
        private bool bModoSCAN = false;
        private string _emp;
        private List<string> _lsSeqs;
        private List<string> _lnfes;
        private string _loteres;
        public string Loteres
        {
            get { return _loteres; }
        }

        public string _sSeq { get; set; }
        public string _sNFe { get; set; }
        private string _cd_ufnro { get; set; }
        public string _sChnfe { get; set; }
        private string _sRecibo { get; set; }
        private X509Certificate2 _xCert { get; set; }
        private string _UF_Empresa { get; set; }
        public KryptonLabel _lblQtde { get; set; }

        int countTentativas = 0;



        /// <summary>
        /// Objeto responsavel por armazenara as nota que são autorizadas, somente utilizando na Classe belGeraXML
        /// </summary>
        private List<string> _nfeautorizadas = new List<string>();
        /// <summary>
        /// Propriedade responsavel por retornar a lista de nfs autorizadas, somente utilizando na Classe belGeraXML
        /// </summary>
        public List<string> Nfeautorizadas
        {
            get { return _nfeautorizadas; }
        }
        /// <summary>
        /// Objeto responsavel por armazenar a nf autorizada, somento utilizado no btnConsultaRetFazenda
        /// </summary>
        private string _nfeunicaAut;
        /// <summary>
        /// Propriedade responsavel por armazenar a nf autorizada, somente utilizado no btnConsultaRetFazenda
        /// </summary>
        public string NfeunicaAut
        {
            get { return _nfeunicaAut; }
        }
        public bool bStopRetorno = false;

        public void BusRetFazendaEnvio()
        {

            Globais glob = new Globais();
            XNamespace pf = "http://www.portalfiscal.inf.br/nfe";
            this.bModoSCAN = belStatic.bModoSCAN;
            _emp = belStatic.codEmpresaNFe;

            string qtdeTentativas = glob.LeRegConfig("QtdeTentativas").ToString();

            //Retorno da fazenda com Status de cada NFe do lote.
            belNfeRetRecepcao objnferetrecepcao = new belNfeRetRecepcao("1.02", _sRecibo, "2.00", _xCert, bModoSCAN);
            XmlDocument xret = new XmlDocument();

            //Variavel pra sabe se alguma nota possui erro
            XmlNodeList nodescStat;
            XmlNodeList nodesxMotivo;
            //contador de tentativas          

            while (!bStopRetorno)
            {
                _loteres = "";
                xret = objnferetrecepcao.Retornaxml(_UF_Empresa);
                nodescStat = xret.GetElementsByTagName("cStat");
                nodesxMotivo = xret.GetElementsByTagName("xMotivo");

                if (nodescStat[0].InnerText == "105")
                {
                    _loteres = nodescStat[0].InnerText + " - " + nodesxMotivo[0].InnerText;
                    countTentativas++;
                }
                else if (nodescStat[0].InnerText != "104")
                {
                    throw new Exception("Erro " + nodescStat[0].InnerText + " - " + nodesxMotivo[0].InnerText);
                }
                else
                {
                    for (int i = 0; i < _lnfes.Count; i++)
                    {
                        //Variavel que gera a mensagem de resposta
                        _loteres = _loteres + "Nota de número de sequência: " + _lsSeqs[i] + " - " + nodesxMotivo[i + 1].InnerText + " Cod. " + nodescStat[i + 1].InnerText + Environment.NewLine;

                        if (nodescStat[i + 1].InnerText == "100")
                        {
                            _nfeautorizadas.Add(_lsSeqs[i]);
                            XmlNode xRetUni = xret.GetElementsByTagName("infProt")[i]; // Diego OS_24777

                            //Método responsavel por gravar o nProt e chNfe no banco e gerar o xml protocolado.
                            geraProcNFe(_lsSeqs[i], _lnfes[i], xret.GetElementsByTagName("chNFe")[i].InnerText,
                                                xRetUni["nProt"].InnerText, xret.GetElementsByTagName("protNFe")[i], pf);

                            xret.Save(belStaticPastas.PROTOCOLOS + "\\" + _sRecibo + "-pro-rec.xml");
                            countTentativas++;
                        }
                        else if (nodescStat[i + 1].InnerText == "204")
                        {
                            string sRet = nodesxMotivo[i + 1].InnerText.Substring((nodesxMotivo[i + 1].InnerText.IndexOf("nRec") + 5), 15);
                            SalvaRetornoNotaDuplicada(sRet, _lsSeqs[i]);
                        }
                        else if ((nodescStat[i + 1].InnerText == "110") || (nodescStat[i + 1].InnerText == "302") || (nodescStat[i + 1].InnerText == "302"))
                        {
                            NotaDenegada(_lsSeqs[i]);
                        }
                        else if (nodescStat[i + 1].InnerText != "105") // Lote em processamento
                        {
                            LimpaCampoRecibo(_emp, _lsSeqs[i]);
                        }
                    }
                    bStopRetorno = true;
                }
                _lblQtde.Text = countTentativas.ToString() + " - " + _loteres;

            }

        }

        public void BusRetFazendaRetorno()
        {
            try
            {
                XNamespace pf = "http://www.portalfiscal.inf.br/nfe";
                //Retorno da fazenda com Status de cada NFe do lote.
                belNfeRetRecepcao objnferetrecepcao = new belNfeRetRecepcao("1.02", _sRecibo, "2.00", _xCert, bModoSCAN);
                XmlDocument xret = new XmlDocument();

                while (!bStopRetorno)
                {
                    xret = objnferetrecepcao.Retornaxml(_UF_Empresa);
                    _loteres = "";
                    //Variavel pra sabe se alguma nota possui erro
                    XmlNodeList nodeschNFe = xret.GetElementsByTagName("chNFe");
                    XmlNodeList nodecStat = xret.GetElementsByTagName("cStat");
                    XmlNodeList nodesxMotivo = xret.GetElementsByTagName("xMotivo");

                    if (nodecStat[0].InnerText == "105")
                    {
                        _loteres = nodecStat[0].InnerText + " - " + nodesxMotivo[0].InnerText;
                        countTentativas++;
                    }
                    else if (nodecStat[0].InnerText != "104")
                    {
                        throw new Exception("Cód. do Status: " + nodecStat[0].InnerText + " " +
                                                      "Status: " + nodesxMotivo[0].InnerText);
                    }
                    else
                    {
                        for (int i = 0; i < nodeschNFe.Count; i++)
                        {
                            if (nodeschNFe[i].InnerText == _sChnfe)
                            {
                                //Monta a mensagem de resposta
                                _loteres = _loteres + "Cód. do Status: " + nodecStat[i + 1].InnerText + " " +
                                                      "Status: " + nodesxMotivo[i + 1].InnerText;

                                if (nodecStat[i + 1].InnerText == "100")
                                {
                                    _nfeunicaAut = _sSeq;

                                    geraProcNFe(_sSeq, _sNFe, xret.GetElementsByTagName("chNFe")[i].InnerText,
                                        xret.GetElementsByTagName("nProt")[i].InnerText, xret.GetElementsByTagName("protNFe")[i], pf); //Claudinei - o.s. 24126 - 11/02/2010

                                    xret.Save(belStaticPastas.PROTOCOLOS + "\\" + _sRecibo + "-pro-rec.xml");
                                    bStopRetorno = true;
                                }//Diego - OS_24610
                                else if (nodecStat[i + 1].InnerText == "101")
                                {
                                    _nfeunicaAut = _sSeq;

                                    geraProcNFe(_sSeq, _sNFe, xret.GetElementsByTagName("chNFe")[i].InnerText,
                                                xret.GetElementsByTagName("nProt")[i].InnerText, xret.GetElementsByTagName("protNFe")[i], pf);
                                    DirectoryInfo dinfo = new DirectoryInfo(belStaticPastas.ENVIADOS + "\\" + xret.GetElementsByTagName("chNFe")[i].InnerText.Substring(2, 4));
                                    string path = "";
                                    string nome = "";
                                    FileInfo[] finfo = dinfo.GetFiles();
                                    foreach (var item in finfo)
                                    {
                                        if (item.Name.Contains(_sChnfe))
                                        {
                                            path = item.FullName;
                                            nome = item.Name;
                                            break;
                                        }
                                    }
                                    File.Move(path, belStaticPastas.CANCELADOS + "\\" + nome.Replace("nfe", "can"));//+ ".xml");
                                    bStopRetorno = true;
                                }//Diego - OS_24610 - FIM
                                else if ((nodecStat[i + 1].InnerText == "110") || (nodecStat[i + 1].InnerText == "302") || (nodecStat[i + 1].InnerText == "302"))
                                {
                                    NotaDenegada(_lsSeqs[i]);
                                }
                            }
                        }
                    }
                    _lblQtde.Text = countTentativas.ToString() + " - " + _loteres;
                    // _txtMsg.Text = _loteres;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void NotaDenegada(string sNfseq)
        {
            belConnection cx = new belConnection();

            try
            {
                string sSql = string.Format("UPDATE NF SET " +
                                                    "cd_recibonfe = 'denegada', " +
                                                    "st_nfe = 'S' " +
                                                    (belUtil.CampoExisteNaTabela("ST_DENEGADA", "NF") ? " ,ST_DENEGADA = 'S' " : "") +
                                                    (belUtil.CampoExisteNaTabela("CD_STDOC", "NF") ? " ,CD_STDOC = '04' " : "") +
                                                    "WHERE CD_NFSEQ = '{0}' AND CD_EMPRESA = '{1}'",
                                sNfseq,
                                belStatic.codEmpresaNFe);
                using (FbCommand cmdUpdate = new FbCommand(sSql.ToString(), cx.get_Conexao()))
                {
                    cx.Open_Conexao();
                    cmdUpdate.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            finally { cx.Close_Conexao(); }

        }
        public void SalvaRetornoNotaDuplicada(string sRecibo, string sSeq)
        {
            belConnection cx = new belConnection();
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.Append("UPDATE NF ");
                sSql.Append("set cd_recibonfe = '" + sRecibo + "' ");
                sSql.Append("where ");
                sSql.Append("cd_empresa ='");
                sSql.Append(belStatic.codEmpresaNFe);
                sSql.Append("' ");
                sSql.Append("and ");
                sSql.Append("cd_nfseq ='");
                sSql.Append(sSeq);
                sSql.Append("'");
                using (FbCommand cmdUpdate = new FbCommand(sSql.ToString(), cx.get_Conexao()))
                {
                    cx.Open_Conexao();
                    cmdUpdate.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally { cx.Close_Conexao(); }

        }

        public void LimpaCampoRecibo(string sEmp, string sSeq)
        {
            belConnection cx = new belConnection();
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.Append("update nf ");
                sSql.Append("set cd_recibonfe = null ");
                sSql.Append("where ");
                sSql.Append("cd_empresa ='");
                sSql.Append(sEmp);
                sSql.Append("' ");
                sSql.Append("and ");
                sSql.Append("cd_nfseq ='");
                sSql.Append(sSeq);
                sSql.Append("'");

                using (FbCommand cmdUpdate = new FbCommand(sSql.ToString(), cx.get_Conexao()))
                {
                    cx.Open_Conexao();
                    cmdUpdate.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cx.Close_Conexao();
            }
        }

        public belBusRetFazenda(List<string> lsSeqs, List<string> lsNfes, string sRecibo, X509Certificate2 xCert, string UF_Empresa)
        {
            _lsSeqs = lsSeqs;
            _lnfes = lsNfes;
            _sRecibo = sRecibo;
            _xCert = xCert;
            _UF_Empresa = UF_Empresa;
        }

        public belBusRetFazenda(string sSeq, string sNFe, string sRecibo, string sChnfe, X509Certificate2 xCert, string UF_Empresa)
        {
            _sSeq = sSeq;
            _sNFe = sNFe;
            _sRecibo = sRecibo;
            _xCert = xCert;
            _UF_Empresa = UF_Empresa;
            _sChnfe = sChnfe;
        }

        public belBusRetFazenda() { }

        /// <summary>
        /// Método Responsavel por gravar os campos cd_chavenferet e cd_nprotnfe. Responsavel tambem montar o TAG procNFe no xml e
        /// grava-lo na pasta de xml enviados.
        /// </summary>
        /// <param name="sEmp"></param>
        /// <param name="sSeq"></param>
        /// <param name="sNFe"></param>
        /// <param name="sChNfe"></param>
        /// <param name="sNProt"></param>
        /// <param name="xret"></param>
        /// <param name="glob"></param>
        /// <param name="pf"></param>
        public void geraProcNFe(string sSeq, string sNFe, string sChNfe, string sNProt,
                                XmlNode xret, XNamespace pf)//Danner - o.s. 24435 - 05/05/2010
        {
            Globais glob = new Globais();
            XDocument retcab = new XDocument();
            belConnection cx = new belConnection();
            try
            {
                //String sql que dará o update no campos: cd_recibonfe, cd_chavenferet, cd_nprotnfe.
                StringBuilder sSql = new StringBuilder();

                sSql.Append("update nf set cd_chavenferet ='");
                sSql.Append(sChNfe);
                sSql.Append("', ");
                sSql.Append("cd_nprotnfe ='");
                sSql.Append(sNProt);
                sSql.Append("' ");
                sSql.Append("Where nf.cd_empresa = '");
                sSql.Append(belStatic.codEmpresaNFe);
                sSql.Append("' and ");
                sSql.Append("nf.cd_nfseq ='");
                sSql.Append(sSeq);
                sSql.Append("'");

                using (FbCommand cmdUpdate = new FbCommand(sSql.ToString(), cx.get_Conexao()))
                {
                    cx.Open_Conexao();
                    cmdUpdate.ExecuteNonQuery();
                }
                //Geração do Xml da nfe Autorizado, incluindo a TAG infProc onde consta as informaçoes de retorno da nfe.
                XContainer retproc = new XElement(pf + "nfeProc", new XAttribute("versao", "2.00"),
                                                                     new XAttribute("xmlns", "http://www.portalfiscal.inf.br/nfe"));
                XElement retxml = XElement.Parse(sNFe);
                string sCodificacao = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
                string sRetProc = "<nfeProc versao=\"2.00\" xmlns=\"http://www.portalfiscal.inf.br/nfe\">";
                XmlDocument xmldocteste = new XmlDocument();
                xmldocteste.LoadXml(sNFe);
                DirectoryInfo dPastaData = new DirectoryInfo(belStaticPastas.ENVIADOS + "\\" + sChNfe.Substring(2, 4));
                if (!dPastaData.Exists) { dPastaData.Create(); }


                StreamWriter sw = new StreamWriter(belStaticPastas.ENVIADOS + "\\" + sChNfe.Substring(2, 4) + "\\" + sChNfe + "-nfe.xml"); //OS_25024

                if (@xmldocteste.FirstChild.Name.Equals("xml"))
                {
                    sw.Write(@sRetProc + @xmldocteste.OuterXml.Remove(0, 38) + @xret.OuterXml.ToString() + @"</nfeProc>");
                }
                else
                {
                    sw.Write(@sCodificacao + @sRetProc + @xmldocteste.OuterXml + @xret.OuterXml.ToString() + @"</nfeProc>");
                }
                sw.Close();

                //Geração do Xml da nfe Autorizado, incluindo a TAG infProc onde consta as informaçoes de retorno da nfe.
            }
            catch (Exception x)
            {
                throw new Exception("Erro na geração do XML Protocolado, infProt - " + x.Message);
            }
            finally
            {
                cx.Close_Conexao();
            }
        }

    }
}
