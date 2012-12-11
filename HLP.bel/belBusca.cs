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


namespace HLP.bel
{
    public class belBusRetFazenda
    {
        private bool bModoSCAN = false;
        private string _emp;
        private List<string> _seqs;

        private List<string> _nfes;
        private string _loteres;
        public string Loteres
        {
            get { return _loteres; }
        }
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

        /// <summary>
        /// Utilizado na Classe belGeraXML.
        /// </summary>
        /// <param name="sEmp"></param>
        /// <param name="sSeqs"></param>
        /// <param name="sNfes"></param>
        /// <param name="tp_amb"></param>
        /// <param name="cd_ufnro"></param>
        /// <param name="sRecibo"></param>
        /// <param name="xCert"></param>
        /// <param name="glob"></param>
        /// <param name="pf"></param>
        public belBusRetFazenda(string sEmp, List<string> sSeqs, List<string> sNfes, int tp_amb, string cd_ufnro,
                                string sRecibo, X509Certificate2 xCert, Globais glob, string UF_Empresa, bool bModoSCAN)
        {
            XNamespace pf = "http://www.portalfiscal.inf.br/nfe";
            this.bModoSCAN = bModoSCAN;
            _emp = sEmp;
            _seqs = sSeqs;
            _nfes = sNfes;

            string qtdeTentativas = glob.LeRegConfig("QtdeTentativas").ToString();

            bool parar = false;

            //Retorno da fazenda com Status de cada NFe do lote.
            belNfeRetRecepcao objnferetrecepcao = new belNfeRetRecepcao("1.02", tp_amb, sRecibo, "2.00", xCert, bModoSCAN);
            XmlDocument xret = new XmlDocument();

            //Variavel pra sabe se alguma nota possui erro
            XmlNodeList nodescStat;
            XmlNodeList nodesxMotivo;
            //contador de tentativas
            int countTentativas = 0;

            for (; ; )
            {

                xret = objnferetrecepcao.Retornaxml(UF_Empresa);
                nodescStat = xret.GetElementsByTagName("cStat");
                nodesxMotivo = xret.GetElementsByTagName("xMotivo");

                if (nodescStat[0].InnerText == "105")
                {

                    //Verifica o status do serviço e faz o calculo de tempo médio para a próxima tentativa do recebimento do retorno da fazenda
                    belnfeStatusServicoNF objstausserv = new belnfeStatusServicoNF("2.00", tp_amb, cd_ufnro, xCert, UF_Empresa);
                    System.Threading.Thread.Sleep((objstausserv.Tmed * 1000) + 3000);

                    

                    //Compara se a quantidade de tentativas dadas é igual ao limite estipuládo na configuração do sistema.
                    if (qtdeTentativas == countTentativas.ToString())
                    {

                        throw new Exception("Quantidade de Tentativas de Conexão com a secretaria da Fazenta Excedidas " + Environment.NewLine + 
                                            "Lote foi enviado a fazenda, porem esta em processamento" + Environment.NewLine +
                                            "Espere alguns segundos antes de tentar buscar o retorno novamente");

                    }

                    //Conta tentativa
                    countTentativas++;

                    continue;

                }
                else if (nodescStat[0].InnerText != "104")
                {
                    throw new Exception("Erro " + nodescStat[0].InnerText + " - " + nodesxMotivo[0].InnerText);
                }
                else
                {
                    for (int i = 0; i < sNfes.Count ; i++)
                    {
                        //Variavel que gera a mensagem de resposta
                        _loteres = _loteres + "Nota de número de sequência: " + sSeqs[i] + " - " + nodesxMotivo[i + 1].InnerText + " Cod. " + nodescStat[i + 1].InnerText + Environment.NewLine;

                        if (nodescStat[i + 1].InnerText == "100")
                        {
                            _nfeautorizadas.Add(sSeqs[i]);
                            XmlNode xRetUni = xret.GetElementsByTagName("infProt")[i]; // Diego OS_24777

                            //Método responsavel por gravar o nProt e chNfe no banco e gerar o xml protocolado.
                            geraProcNFe(sEmp, sSeqs[i], sNfes[i], xret.GetElementsByTagName("chNFe")[i].InnerText,
                                                xRetUni["nProt"].InnerText, xret.GetElementsByTagName("protNFe")[i], glob, pf);

                            xret.Save(glob.LeRegConfig("PastaProtocolos").ToString() + "\\" + sRecibo + "-pro-rec.xml");                           

                        }
                        else
                        {
                            if (nodescStat[i + 1].InnerText != "105")
                            {
                                LimpaCampoRecibo(_emp, sSeqs[i]);
                            }
                        }

                        parar = true;

                    }
                }
                if (parar == true)
                    break;
            }


        }

        public void LimpaCampoRecibo(string sEmp, string sSeq)
        {
            belGerarXML BuscaConexao = new belGerarXML();
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
                //sSql.Append(" and cd_recibonfe is null");



                using (FbConnection Conn = BuscaConexao.Conn)
                {
                    using (FbCommand cmdUpdate = new FbCommand(sSql.ToString(), Conn))
                    {
                        if (Conn.State != ConnectionState.Open)
                        {
                            Conn.Open();
                        }
                        cmdUpdate.ExecuteNonQuery();
                        Conn.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public belBusRetFazenda()
        {
        }
        //Fim - Danner - o.s. 24191 - 26/02/2010
        /// <summary>
        /// Utilizado no botão para buscar o retorno da fazenda
        /// </summary>
        /// <param name="sEmp"></param>
        /// <param name="sSeq"></param>
        /// <param name="sVersao"></param>
        /// <param name="sVersaoAplic"></param>
        /// <param name="sNFe"></param>
        /// <param name="sRecibo"></param>
        /// <param name="tp_amb"></param>
        /// <param name="glob"></param>
        /// <param name="?"></param>
        public belBusRetFazenda(string sEmp, string sSeq, string sVersao, string sVersaoAplic, string sNFe,
                                string sRecibo, string sChnfe, int tp_amb, Globais glob, X509Certificate2 xCert, string UF_Empresa)
            
        {
            XNamespace pf = "http://www.portalfiscal.inf.br/nfe";

            //Retorno da fazenda com Status de cada NFe do lote.
            belNfeRetRecepcao objnferetrecepcao = new belNfeRetRecepcao("1.02", tp_amb, sRecibo, "2.00", xCert, bModoSCAN);
            XmlDocument xret = new XmlDocument();
            xret = objnferetrecepcao.Retornaxml(UF_Empresa);

            //Variavel pra sabe se alguma nota possui erro
            XmlNodeList nodeschNFe = xret.GetElementsByTagName("chNFe");
            XmlNodeList nodecStat = xret.GetElementsByTagName("cStat");
            XmlNodeList nodesxMotivo = xret.GetElementsByTagName("xMotivo");

            try
            {
                if (nodecStat[0].InnerText != "104")
                {
                    throw new Exception("Cód. do Status: " + nodecStat[0].InnerText + " " +
                                                  "Status: " + nodesxMotivo[0].InnerText);
                }
                else
                {
                    for (int i = 0; i < nodeschNFe.Count; i++)
                    {
                        if (nodeschNFe[i].InnerText == sChnfe)
                        {
                            //Monta a mensagem de resposta
                            _loteres = _loteres + "Cód. do Status: " + nodecStat[i + 1].InnerText + " " +
                                                  "Status: " + nodesxMotivo[i + 1].InnerText;

                            if (nodecStat[i + 1].InnerText == "100")
                            {
                                _nfeunicaAut = sSeq;
                             
                                geraProcNFe(sEmp, sSeq, sNFe, xret.GetElementsByTagName("chNFe")[i].InnerText,
                                    xret.GetElementsByTagName("nProt")[i].InnerText, xret.GetElementsByTagName("protNFe")[i], glob, pf); //Claudinei - o.s. 24126 - 11/02/2010
                              
                                xret.Save(glob.LeRegConfig("PastaProtocolos").ToString() + "\\" + sRecibo + "-pro-rec.xml");                        
                            }//Diego - OS_24610
                            else if (nodecStat[i + 1].InnerText == "101")
                            {
                                _nfeunicaAut = sSeq;

                                geraProcNFe(sEmp, sSeq, sNFe, xret.GetElementsByTagName("chNFe")[i].InnerText,
                                            xret.GetElementsByTagName("nProt")[i].InnerText, xret.GetElementsByTagName("protNFe")[i], glob, pf);
                                DirectoryInfo dinfo = new DirectoryInfo(glob.LeRegConfig("PastaXmlEnviado") + "\\" + xret.GetElementsByTagName("chNFe")[i].InnerText.Substring(2,4));
                                string path = "";
                                string nome = "";
                                FileInfo[] finfo = dinfo.GetFiles();
                                foreach (var item in finfo)
                                {
                                    if (item.Name.Contains(sChnfe))
                                    {
                                        path = item.FullName;
                                        nome = item.Name;
                                        break;
                                    }
                                }
                                File.Move(path, glob.LeRegConfig("PastaXmlCancelados").ToString() + "\\" + nome.Replace("nfe", "can"));//+ ".xml");
                            }//Diego - OS_24610 - FIM
                        }
                    }
                }
            }
            catch (Exception x)
            {
                throw new Exception("Erro na BuscaRetFazenda, contrutor para o botão - " + x.Message);
            }

            

        }
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
        public void geraProcNFe(string sEmp, string sSeq, string sNFe, string sChNfe, string sNProt,
                                XmlNode xret, Globais glob, XNamespace pf)//Danner - o.s. 24435 - 05/05/2010
        {
            XDocument retcab = new XDocument();
             
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
                sSql.Append(sEmp);
                sSql.Append("' and ");
                sSql.Append("nf.cd_nfseq ='");
                sSql.Append(sSeq);
                sSql.Append("'");


                belGerarXML BuscaConexao = new belGerarXML();

                using (FbConnection Conn = BuscaConexao.Conn)
                {
                    using (FbCommand cmdUpdate = new FbCommand(sSql.ToString(), Conn))
                    {
                        Conn.Open();
                        cmdUpdate.ExecuteNonQuery();
                    }
                }
                //Geração do Xml da nfe Autorizado, incluindo a TAG infProc onde consta as informaçoes de retorno da nfe.
                XContainer retproc = new XElement(pf + "nfeProc", new XAttribute("versao", "2.00"),
                                                                     new XAttribute("xmlns", "http://www.portalfiscal.inf.br/nfe"));
                XElement retxml = XElement.Parse(sNFe);
                string sCodificacao = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
                string sRetProc = "<nfeProc versao=\"2.00\" xmlns=\"http://www.portalfiscal.inf.br/nfe\">";
                XmlDocument xmldocteste = new XmlDocument();
                xmldocteste.LoadXml(sNFe);                
                DirectoryInfo dPastaData = new DirectoryInfo(glob.LeRegConfig("PastaXmlEnviado") + "\\" + sChNfe.Substring(2, 4));
                if (!dPastaData.Exists) { dPastaData.Create(); }


                StreamWriter sw = new StreamWriter(glob.LeRegConfig("PastaXmlEnviado") +"\\"+ sChNfe.Substring(2,4)+ "\\" + sChNfe + "-nfe.xml"); //OS_25024
       
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

        }


        //Danner - o.s. 24191 - 26/02/2010
        //public void geraProcNFe(Globais glob, string sEmp, string sSeq, string sNFe, XmlDocument xret)
        //{
        //    XDocument retcab = new XDocument();
        //    XNamespace pf = "http://www.portalfiscal.inf.br/nfe";
        //    try
        //    {
        //        //String sql que dará o update no campos: cd_recibonfe, cd_chavenferet, cd_nprotnfe.
        //        StringBuilder sSql = new StringBuilder();

        //        sSql.Append("update nf set cd_chavenferet ='");
        //        sSql.Append(xret.GetElementsByTagName("chNFe")[0].InnerText);
        //        sSql.Append("', ");
        //        sSql.Append("cd_nprotnfe ='");
        //        sSql.Append(xret.GetElementsByTagName("nProt")[0].InnerText);
        //        sSql.Append("' ");
        //        sSql.Append("Where nf.cd_empresa = '");
        //        sSql.Append(sEmp);
        //        sSql.Append("' and ");
        //        sSql.Append("nf.cd_nfseq ='");
        //        sSql.Append(sSeq);
        //        sSql.Append("'");


        //        belGerarXML BuscaConexao = new belGerarXML();

        //        using (FbConnection Conn = BuscaConexao.Conn)
        //        {
        //            using (FbCommand cmdUpdate = new FbCommand(sSql.ToString(), Conn))
        //            {
        //                Conn.Open();
        //                cmdUpdate.ExecuteNonQuery();
        //            }
        //        }
        //        //Geração do Xml da nfe Autorizado, incluindo a TAG infProc onde consta as informaçoes de retorno da nfe.
        //        XContainer retproc = new XElement(pf + "nfeProc", new XAttribute("versao", "1.10"),
        //                                                              new XAttribute("xmlns", "http://www.portalfiscal.inf.br/nfe"));
        //        XElement retxml = XElement.Parse(sNFe);
        //        XElement protnfe = new XElement(pf + "protNFe",
        //                                new XElement(pf + "infProt",
        //                                    new XElement(pf + "nProt", xret.GetElementsByTagName("nProt")[0].InnerText)));
        //        retcab.Add(retproc);
        //        retproc.Add(retxml);
        //        retxml.AddAfterSelf(protnfe);
        //        retcab.Save(glob.LeRegWin("PastaXmlEnviado") + "\\" + xret.GetElementsByTagName("chNFe")[0].InnerText + "-nfe.xml");
        //    }
        //    catch (Exception x)
        //    {
        //        throw new Exception("Erro na geração do XML Protocolado, infProt - " + x.Message);
        //    }
        //}
        //Fim - Danner - o.s. 24191 - 26/02/2010


        //Fim - Danner - o.s. 23847 - 16/11/2009

    }
}
