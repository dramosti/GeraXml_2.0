using System;
using System.Collections.Generic;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Linq;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using HLP.Util;

namespace HLP.bel
{
    public struct strucDevolucoes
    {
        public string scdNotafis { get; set; }
        public DateTime dDtEmi { get; set; }
        public string sSaldo { get; set; }
        public string dValorNF { get; set; }
    }
    public class belGerarXML
    {
        public FbConnection Conn;
        public string nm_Cliente;
        public string psNM_Banco { get; set; }
        private FbDataAdapter Da;
        private DataSet ds;
        public Boolean bIndustria = true;
        private string sConn = sMontaStringConexao();
        public string sTabela = String.Empty;
        public string sCampos = String.Empty;
        public string sWhere = String.Empty;
        public string sOrder = String.Empty;
        public StringBuilder sInner;
        string sSQL = String.Empty;
        FbCommand SelCmd = new FbCommand();
        public string sTipoIndustrializacao = string.Empty;

        public belGerarXML()
        {
            FbCommand InsertCmd = new FbCommand();
            FbCommand UpDate = new FbCommand();
            FbCommand DelCmd = new FbCommand();
            InicializeConnection();
            using (FbCommand cmd = new FbCommand("select control.cd_conteud from control where control.cd_nivel = '0016'", Conn))
            {
                Conn.Open();
                nm_Cliente = Convert.ToString(cmd.ExecuteScalar()).Trim();
                Conn.Close();
            }
            Globais LeRegWin = new Globais();
            string psNM_Banco = LeRegWin.LeRegConfig("BancoDados");
            sTipoIndustrializacao = LeRegWin.LeRegConfig("Industrializacao");
        }

        public string MontaStringConexao()
        {
            Globais MontaStringConexao = new Globais();
            StringBuilder sbConexao = new StringBuilder();

            sbConexao.Append("User =");
            sbConexao.Append(MontaStringConexao.LeRegConfig("Usuario"));
            sbConexao.Append(";");
            sbConexao.Append("Password=");
            sbConexao.Append(MontaStringConexao.LeRegConfig("Senha"));
            sbConexao.Append(";");
            sbConexao.Append("Database=");
            string sdatabase = MontaStringConexao.LeRegConfig("BancoDados");
            sbConexao.Append(sdatabase);
            sbConexao.Append(";");
            sbConexao.Append("DataSource=");
            sbConexao.Append(MontaStringConexao.LeRegConfig("Servidor"));
            sbConexao.Append(";");
            sbConexao.Append("Port=3050;Dialect=3; Charset=NONE;Role=;Connection lifetime=15;Pooling=true; MinPoolSize=0;MaxPoolSize=50;Packet Size=8192;ServerType=0;");


            return (string)sbConexao.ToString();
        }
        public static string sMontaStringConexao()
        {
            Globais MontaStringConexao = new Globais();
            StringBuilder sbConexao = new StringBuilder();

            sbConexao.Append("User =");
            sbConexao.Append(MontaStringConexao.LeRegConfig("Usuario"));
            sbConexao.Append(";");
            sbConexao.Append("Password=");
            sbConexao.Append(MontaStringConexao.LeRegConfig("Senha"));
            sbConexao.Append(";");
            sbConexao.Append("Database=");
            string sdatabase = MontaStringConexao.LeRegConfig("BancoDados");
            sbConexao.Append(sdatabase);
            sbConexao.Append(";");
            sbConexao.Append("DataSource=");
            sbConexao.Append(MontaStringConexao.LeRegConfig("Servidor"));
            sbConexao.Append(";");
            sbConexao.Append("Port=3050;Dialect=3; Charset=NONE;Role=;Connection lifetime=15;Pooling=true; MinPoolSize=0;MaxPoolSize=2000;Packet Size=8192;ServerType=0;");


            return (string)sbConexao.ToString();
        }

        public DataTable BuscaDadosNF()
        {
            Conn.Open();

            sSQL = "SELECT " + this.sCampos + " FROM " + this.sTabela;

            if (!(this.sInner.Equals(String.Empty)))
                sSQL += this.sInner.ToString();

            if (!(this.sWhere.Equals(String.Empty)))
                sSQL += " WHERE " + this.sWhere + " ";

            if (!(this.sOrder.Equals(String.Empty)))
                sSQL += " ORDER BY " + this.sOrder;

            /*
            SelCmd = new FbCommand(sSQL, Conn);
            ,SelCmd.CommandType = CommandType.Text;
             */

            DataTable dt = new DataTable();

            FbDataAdapter Da = new FbDataAdapter(sSQL, Conn);

            dt.Clear();
            Da.Fill(dt);
            Da.Dispose();

            Conn.Close();

            return dt;
        }
        public string RetornaGenString(string sGen, int Tamanho)
        {
            string sNumArq = "";
            try
            {
                FbCommand sSql = new FbCommand();
                sSql.Connection = Conn;
                sSql.CommandText = "SP_CHAVEPRI";
                sSql.CommandType = CommandType.StoredProcedure;
                sSql.Parameters.Clear();

                Conn.Open();

                sSql.Parameters.Add("@SNOMEGENERATOR", FbDbType.VarChar, 31).Value = "GEN_NOMEARQXML";

                sNumArq = sSql.ExecuteScalar().ToString();

            }
            catch (FbException Ex)
            {
                Console.WriteLine("Erro.: ", Ex.Message);
            }
            finally
            {
                Conn.Close();
            }



            return sNumArq.PadLeft(Tamanho, '0');
        }
        public void SetCamposSelect(StringBuilder Lista)
        {
            this.sCampos = Lista.ToString().Trim();
        }
        public void SetWhereSelect(StringBuilder Where)
        {
            this.sWhere = Where.ToString().Trim();
        }
        public void SetOrderSelect(string Order)
        {
            this.sOrder = Order.Trim();
        }
        public void SetTabelaSelect(string Tabela)
        {
            this.sTabela = Tabela.Trim();
        }
        public void SetInnerSelec(StringBuilder Inner)
        {
            this.sInner = new StringBuilder();
            this.sInner = Inner;
        }

        private void InicializeConnection()
        {
            Conn = new FbConnection(sConn);
        }
        public DataSet GetData()
        {
            ds = new DataSet();
            Da.Fill(ds);
            return ds;
        }
    }
    public class Globais
    {
        public string LeRegConfig(string NomeChave)
        {
            string Retorno = "";
            try
            {
                string path = this.GetType().Assembly.Location.Replace(this.GetType().Assembly.ManifestModule.Name, "") + "\\Config\\" + HLP.bel.belStatic.sConfig;
                if (File.Exists(path))
                {
                    XmlTextReader reader = new XmlTextReader(path);
                    while (reader.Read())
                    {
                        if ((reader.NodeType != XmlNodeType.Element) || !(reader.Name == "nfe_configuracoes"))
                        {
                            continue;
                        }
                        while (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Element)
                            {
                                if (reader.Name == NomeChave)
                                {
                                    reader.Read();
                                    Retorno = reader.Value;
                                    continue;
                                }
                            }
                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(string.Format("Erro ao tentar abrir o xml de configuração do sistema.: {0}",
                                    ex.Message));
            }
            return Retorno;
        }
    }
    public class GeraXMLExp
    {
        bool bModoSCAN = false;
        int iSerieSCAN = 0;
        int iStatusAtualSistema;
        string sFormaEmiNFe = "";
        belGerarXML objbelGeraXml;
        FbConnection Conn;
        public string sSimplesNac;
        public bool pbIndustri;
        public decimal dTotPis = 0;
        public decimal dTotCofins = 0;
        public decimal dTotPisISS = 0;
        public decimal dTotCofinsISS = 0;
        public decimal dTotServ = 0;
        public decimal dTotBCISS = 0;
        public decimal dTotISS = 0;
        public decimal dTotbaseICMS = 0;
        public decimal dTotValorICMS = 0;
        public string sExecao = string.Empty;
        public string sFaturamento;
        public List<string> nfes = new List<string>();
        public string sXmlfull;
        public string sTipoIndustrializacao = string.Empty;
        public List<List<object>> lTotNota = new List<List<object>>();

        public GeraXMLExp(int iStatusAtualSistema)
        {
            this.iStatusAtualSistema = iStatusAtualSistema; // NFe_2.0
        }

        public GeraXMLExp()
        {
        }

        public XmlWriterSettings ConfiguraXML()
        {
            XmlWriterSettings ConfiguraXml = new XmlWriterSettings();
            ConfiguraXml.Indent = true;
            ConfiguraXml.IndentChars = "";
            ConfiguraXml.NewLineOnAttributes = false;
            ConfiguraXml.OmitXmlDeclaration = false;
            return ConfiguraXml;

        }

        /// <summary>
        /// Gera a Estrutura do XML da NF-e
        /// </summary>
        /// <param name="sNF"></param>
        /// <param name="cert"></param>
        /// <param name="sEmp"></param>
        /// <param name="sNomeArq"></param>
        /// <param name="iStatusAtualSistema"</param>
        public void geraArquivoNFE(List<string> sNF, X509Certificate2 cert, string sEmp, string sNomeArq)
        {
            objbelGeraXml = new belGerarXML();
            Conn = objbelGeraXml.Conn;
            Conn.Open();
            try
            {
                string sPath = "";
                sPath = (sFormaEmiNFe.Equals("2") ? CarregarDadosXml("PastaContingencia").ToString() + @sNomeArq : CarregarDadosXml("PastaXmlEnvio").ToString() + @sNomeArq);
                Globais glob = new Globais();
                int iCount = 0;

                if (File.Exists(sPath))
                {
                    File.Delete(sPath);
                }


                foreach (var i in lTotNota)
                {
                    string sNota = sNF[iCount];
                    string sNFe = "NFe" + GeraChave(sEmp, sNota, Conn);
                    XDocument xdoc = new XDocument();

                    #region XML_Principal
                    XNamespace pf = "http://www.portalfiscal.inf.br/nfe";
                    XContainer conenv = new XElement(pf + "enviNFe", new XAttribute("xmlns", "http://www.portalfiscal.inf.br/nfe"),
                                                               new XAttribute("versao", "2.00"),
                                                               new XElement(pf + "idLote", sNomeArq.Substring(7, 15)));
                    #endregion

                    #region XML_Cabeçalho


                    XContainer concabec = (new XElement(pf + "NFe", new XAttribute("xmlns", "http://www.portalfiscal.inf.br/nfe")));
                    XContainer coninfnfe = (new XElement(pf + "infNFe", new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"), new XAttribute("Id", sNFe),
                                                                        new XAttribute("versao", "2.00")));


                    #endregion

                    #region Ide
                    XContainer conide;
                    try
                    {
                        belIde objide = i[0] as belIde;
                        #region XML_ide
                        conide = (new XElement(pf + "ide", new XElement(pf + "cUF", objide.Cuf.ToString()),
                                                                    new XElement(pf + "cNF", objide.Cnf.ToString()),
                                                                    new XElement(pf + "natOp", objide.Natop.ToString()),
                                                                    new XElement(pf + "indPag", objide.Indpag.ToString()),
                                                                    new XElement(pf + "mod", objide.Mod.ToString()),
                                                                    new XElement(pf + "serie", objide.Serie.ToString()),
                                                                    new XElement(pf + "nNF", objide.Nnf.ToString()),
                                                                    new XElement(pf + "dEmi", objide.Demi.ToString("yyyy-MM-dd")),
                                                                    new XElement(pf + "dSaiEnt", objide.Dsaient.ToString("yyyy-MM-dd")),
                                                                    new XElement(pf + "hSaiEnt", objide.HSaiEnt.ToString("HH:mm:ss")), // NFe_2.0
                                                                    new XElement(pf + "tpNF", objide.Tpnf.ToString()),
                                                                    new XElement(pf + "cMunFG", objide.Cmunfg.ToString()),
                                                                    (objide.belNFref != null ?
                                                                    (from c in objide.belNFref
                                                                     select new XElement(pf + "NFref",
                                                                                (c.RefNFe != null ? new XElement(pf + "refNFe", c.RefNFe) : null),
                                                                               (c.cUF != null ? (new XElement(pf + "refNF",
                                                                                (c.cUF != null ? new XElement(pf + "cUF", c.cUF) : null),
                                                                                 (c.AAMM != null ? new XElement(pf + "AAMM", c.AAMM) : null),
                                                                                 (c.CNPJ != null ? new XElement(pf + "CNPJ", c.CNPJ) : null),
                                                                                 (c.mod != null ? new XElement(pf + "mod", c.mod) : null),
                                                                                 (c.serie != null ? new XElement(pf + "serie", c.serie) : null),
                                                                                 (c.nNF != null ? new XElement(pf + "nNF", c.nNF) : null))) : null))) : null),//NFe_2.0_Verificar ID B14 - B20a - B20i - 
                                                                    new XElement(pf + "tpImp", objide.Tpimp.ToString()),
                                                                    new XElement(pf + "tpEmis", objide.Tpemis.ToString()),
                                                                    new XElement(pf + "cDV", objide.Cdv.ToString()),
                                                                    new XElement(pf + "tpAmb", objide.Tpamb.ToString()),
                                                                    new XElement(pf + "finNFe", objide.Finnfe.ToString()),
                                                                    new XElement(pf + "procEmi", objide.Procemi.ToString()),
                                                                    new XElement(pf + "verProc", objide.Verproc.ToString()),

                                                                    ((objide.Tpemis.Equals("2")) || (objide.Tpemis.Equals("3")) ?
                                                                                          new XElement(pf + "dhCont", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")) : null), // NFe_2.0
                                                                    ((objide.Tpemis.Equals("2")) || (objide.Tpemis.Equals("3")) ?
                                                                                          new XElement(pf + "xJust", (iStatusAtualSistema == 2 ? "FALHA DE CONEXÃO COM INTERNET" : "FALHA COM WEB SERVICE DO ESTADO")) : null)));// NFe_2.0
                        #endregion
                    }
                    catch (Exception x)
                    {
                        throw new Exception("Nota de Sequência - " + sNota + "Erro na geração do XML, Regiao XML_ide - " + x.Message);
                    }
                    #endregion

                    #region Emit

                    XContainer conemit;
                    try
                    {
                        belEmit objemit = i[1] as belEmit;
                        #region XML_Emit

                        conemit = (new XElement(pf + "emit", (new XElement(pf + "CNPJ", objemit.Cnpj.ToString())),
                                                  new XElement(pf + "xNome", objemit.Xnome.ToString()),
                                                  new XElement(pf + "xFant", objemit.Xfant.ToString()),
                                                          new XElement(pf + "enderEmit",
                                                                  new XElement(pf + "xLgr", objemit.Xlgr.ToString()),
                                                                  new XElement(pf + "nro", objemit.Nro.ToString()),
                                                                  (objemit.Xcpl != null ? new XElement(pf + "xCpl", objemit.Xcpl.ToString()) : null),
                                                                  new XElement(pf + "xBairro", objemit.Xbairro.ToString()),
                                                                  new XElement(pf + "cMun", objemit.Cmun.ToString()),
                                                                  new XElement(pf + "xMun", objemit.Xmun.ToString()),
                                                                  new XElement(pf + "UF", objemit.Uf.ToString()),
                                                                  new XElement(pf + "CEP", objemit.Cep.ToString()),
                                                                  new XElement(pf + "cPais", objemit.Cpais.ToString()),
                                                                  new XElement(pf + "xPais", objemit.Xpais.ToString()),
                                                                  (objemit.Fone != null ? new XElement(pf + "fone", objemit.Fone.ToString()) : null)),
                                                  (objemit.Ie != null ? new XElement(pf + "IE", objemit.Ie.ToString()) : null),
                                                  (objemit.Iest != null ? new XElement(pf + "IEST", objemit.Iest.ToString()) : null),
                                                  (objemit.Im != null ? new XElement(pf + "IM", objemit.Im.ToString()) : null),
                                                  (objemit.Cnae != null ? new XElement(pf + "CNAE", objemit.Cnae.ToString()) : null),
                                                  new XElement(pf + "CRT", objemit.CRT.ToString()))); // NFe_2.0

                        #endregion
                    }
                    catch (Exception x)
                    {
                        throw new Exception("Nota de Sequência - " + sNota + "Erro na geração do XML, Regiao XML_Emit - " + x.Message);
                    }
                    #endregion

                    #region Dest
                    XContainer condest;
                    try
                    {
                        belDest objdest = i[2] as belDest;
                        #region XML_Dest
                        objdest.Ie = (objdest.Ie == null ? "" : objdest.Ie);

                        condest = (new XElement(pf + "dest",
                                                  (objdest.Cnpj == "EXTERIOR" ? new XElement(pf + "CNPJ") :
                                                     (objdest.Cnpj != null ? new XElement(pf + "CNPJ", objdest.Cnpj) :
                                                                            new XElement(pf + "CPF", objdest.Cpf))),
                                                  new XElement(pf + "xNome", objdest.Xnome.ToString()),
                                                  new XElement(pf + "enderDest",
                                                      new XElement(pf + "xLgr", objdest.Xlgr.ToString()),
                                                      new XElement(pf + "nro", (objdest.Nro != null ? objdest.Nro.ToString() : "0")),
                                                      (objdest.Xcpl != null ? new XElement(pf + "xCpl", objdest.Xcpl.ToString()) : null),
                                                      new XElement(pf + "xBairro", objdest.Xbairro.ToString()),
                                                      new XElement(pf + "cMun", objdest.Cmun.ToString()),
                                                      new XElement(pf + "xMun", objdest.Xmun.ToString()),
                                                      new XElement(pf + "UF", objdest.Uf.ToString()),
                                                      (objdest.Cep != null ? new XElement(pf + "CEP", objdest.Cep.ToString()) : null),
                                                      new XElement(pf + "cPais", objdest.Cpais.ToString()),
                                                      (objdest.Xpais != null ? new XElement(pf + "xPais", objdest.Xpais.ToString()) : null),
                                                     (objdest.Fone != null ? new XElement(pf + "fone", objdest.Fone.ToString()) : null)),
                                                  ((objdest.Ie != null) ? (objdest.Ie != "EXTERIOR" ? new XElement(pf + "IE", objdest.Ie.ToString()) : new XElement(pf + "IE")) : null), //Claudinei - o.s. sem - 11/02/2010
                                                  (objdest.Isuf != null ? new XElement(pf + "ISUF", objdest.Isuf.ToString()) : null)));


                        #endregion
                    }
                    catch (Exception x)
                    {
                        throw new Exception("Nota de Sequência - " + sNota + "Erro na geração do XML, Regiao XML_Dest - " + x.Message);
                    }

                    #endregion

                    #region Det
                    List<XElement> lcondet = new List<XElement>();
                    try
                    {
                        List<belDet> objdet = new List<belDet>();
                        objdet = i[4] as List<belDet>;
                        #region XML_Detalhes

                        foreach (var det in objdet)
                        {
                            XElement condet = (new XElement(pf + "det", new XAttribute("nItem", det.Nitem),
                                               new XElement(pf + "prod",
                                                   (det.belProd.Cprod != null ? new XElement(pf + "cProd", det.belProd.Cprod.ToString()) : null),
                                                   (det.belProd.Cean != null ? new XElement(pf + "cEAN", det.belProd.Cean.ToString()) : null),
                                                   (det.belProd.Xprod != null ? new XElement(pf + "xProd", det.belProd.Xprod.ToString()) : null),
                                                   (det.belProd.Ncm != null ? new XElement(pf + "NCM", det.belProd.Ncm.ToString()) : new XElement(pf + "NCM", det.belProd.Ncm = "00000000")), //Claudinei - o.s. 24200 - 01/03/2010
                                                   (det.belProd.Extipi != null ? new XElement(pf + "EXTIPI", det.belProd.Extipi.ToString()) : null),
                                                   (det.belProd.Genero != null ? new XElement(pf + "genero", det.belProd.Genero.ToString()) : null),
                                                   (det.belProd.Cfop != null ? new XElement(pf + "CFOP", det.belProd.Cfop.ToString()) : null),
                                                   (det.belProd.Ucom != null ? new XElement(pf + "uCom", det.belProd.Ucom.ToString()) : null),
                                                   (det.belProd.Qcom != null ? new XElement(pf + "qCom", det.belProd.Qcom.ToString("#0.0000").Replace(",", ".")) : null),
                                                   (det.belProd.Vuncom != null ? new XElement(pf + "vUnCom", det.belProd.Vuncom.ToString("#0.0000").Replace(",", ".")) : null),
                                                   (det.belProd.Vprod != null ? new XElement(pf + "vProd", det.belProd.Vprod.ToString("#0.00").Replace(",", ".")) : null),
                                                   (det.belProd.Ceantrib != null ? new XElement(pf + "cEANTrib", det.belProd.Ceantrib.ToString().PadLeft(8, '0')) : null),
                                                   (det.belProd.Utrib != null ? new XElement(pf + "uTrib", det.belProd.Utrib.ToString()) : null),
                                                   (det.belProd.Qtrib != null ? new XElement(pf + "qTrib", det.belProd.Qtrib.ToString("#0.0000").Replace(",", ".")) : null),
                                                   (det.belProd.Vuntrib != null ? new XElement(pf + "vUnTrib", det.belProd.Vuntrib.ToString("#0.0000").Replace(",", ".")) : null),
                                                   ((det.belProd.Vfrete != null) && (det.belProd.Vfrete != 0) ? new XElement(pf + "vFrete", det.belProd.Vfrete.ToString("#0.00").Replace(",", ".")) : null),
                                                   ((det.belProd.Vseg != null) && (det.belProd.Vseg != 0) ? new XElement(pf + "vSeg", det.belProd.Vseg.ToString("#0.00").Replace(",", ".")) : null),
                                                   (((det.belProd.Vdesc != null) && (det.belProd.Vdesc != 0)) ? new XElement(pf + "vDesc", det.belProd.Vdesc.ToString("#0.00").Replace(",", ".")) : null),
                                                   ((det.belProd.VOutro != null) && (det.belProd.VOutro != 0) ? new XElement(pf + "vOutro", det.belProd.VOutro.ToString("#0.00").Replace(",", ".")) : null), //NFe_2.0 
                                                   (det.belProd.IndTot != null) ? new XElement(pf + "indTot", det.belProd.IndTot.ToString()) : null), //NFe_2.0

                                               new XElement(pf + "imposto",

                                                   //---------------ICMS-----------------//

                                                   new XElement(pf + "ICMS",

                                                       //-------------ICMS00-------------//

                                                       (det.belImposto.belIcms.belIcms00 != null ?
                                                       new XElement(pf + "ICMS00",
                                                            (det.belImposto.belIcms.belIcms00.Orig != null ? new XElement(pf + "orig", det.belImposto.belIcms.belIcms00.Orig.ToString()) : null),
                                                            (det.belImposto.belIcms.belIcms00.Cst != null ? new XElement(pf + "CST", det.belImposto.belIcms.belIcms00.Cst.ToString()) : null),
                                                            (det.belImposto.belIcms.belIcms00.Modbc != null ? new XElement(pf + "modBC", det.belImposto.belIcms.belIcms00.Modbc.ToString()) : null),
                                                            (det.belImposto.belIcms.belIcms00.Vbc != null ? new XElement(pf + "vBC", det.belImposto.belIcms.belIcms00.Vbc.ToString("#0.00").Replace(",", ".")) : null),
                                                            (det.belImposto.belIcms.belIcms00.Picms != null ? new XElement(pf + "pICMS", det.belImposto.belIcms.belIcms00.Picms.ToString("#0.00").Replace(",", ".")) : null),
                                                            (det.belImposto.belIcms.belIcms00.Vicms != null ? new XElement(pf + "vICMS", det.belImposto.belIcms.belIcms00.Vicms.ToString("#0.00").Replace(",", ".")) : null)) : null),

                                                        //-------------ICMS10-------------//

                                                        (det.belImposto.belIcms.belIcms10 != null ?
                                                        new XElement(pf + "ICMS10",
                                                            (det.belImposto.belIcms.belIcms10.Orig != null ? new XElement(pf + "orig", det.belImposto.belIcms.belIcms10.Orig.ToString()) : null),
                                                            (det.belImposto.belIcms.belIcms10.Cst != null ? new XElement(pf + "CST", det.belImposto.belIcms.belIcms10.Cst.ToString()) : null),
                                                            (det.belImposto.belIcms.belIcms10.Modbc != null ? new XElement(pf + "modBC", det.belImposto.belIcms.belIcms10.Modbc.ToString()) : null),
                                                            (det.belImposto.belIcms.belIcms10.Vbc != null ? new XElement(pf + "vBC", det.belImposto.belIcms.belIcms10.Vbc.ToString("#0.00").Replace(",", ".")) : null),
                                                            (det.belImposto.belIcms.belIcms10.Picms != null ? new XElement(pf + "pICMS", det.belImposto.belIcms.belIcms10.Picms.ToString("#0.00").Replace(",", ".")) : null),
                                                            (det.belImposto.belIcms.belIcms10.Vicms != null ? new XElement(pf + "vICMS", det.belImposto.belIcms.belIcms10.Vicms.ToString("#0.00").Replace(",", ".")) : null),
                                                            (det.belImposto.belIcms.belIcms10.Modbcst != null ? new XElement(pf + "modBCST", det.belImposto.belIcms.belIcms10.Modbcst.ToString()) : null),
                                                            (det.belImposto.belIcms.belIcms10.Pmvast != 0 ? new XElement(pf + "pMVAST", det.belImposto.belIcms.belIcms10.Pmvast.ToString("#0.00").Replace(",", ".")) : null), //Claudinei - o.s. sem - 11/03/2010
                                                            (det.belImposto.belIcms.belIcms10.Predbcst.ToString() != "0" ? new XElement(pf + "pRedBCST", det.belImposto.belIcms.belIcms10.Predbcst.ToString("#0.00").Replace(",", ".")) : null),
                                                            (det.belImposto.belIcms.belIcms10.Vbcst != null ? new XElement(pf + "vBCST", det.belImposto.belIcms.belIcms10.Vbcst.ToString("#0.00").Replace(",", ".")) : null),
                                                            (det.belImposto.belIcms.belIcms10.Picmsst != null ? new XElement(pf + "pICMSST", det.belImposto.belIcms.belIcms10.Picmsst.ToString("#0.00").Replace(",", ".")) : null),
                                                            (det.belImposto.belIcms.belIcms10.Vicmsst != null ? new XElement(pf + "vICMSST", det.belImposto.belIcms.belIcms10.Vicmsst.ToString("#0.00").Replace(",", ".")) : null)) : null),

                                                        //-------------ICMS20-------------//

                                                        (det.belImposto.belIcms.belIcms20 != null ?
                                                        new XElement(pf + "ICMS20",
                                                            (det.belImposto.belIcms.belIcms20.Orig != null ? new XElement(pf + "orig", det.belImposto.belIcms.belIcms20.Orig.ToString()) : null),
                                                            (det.belImposto.belIcms.belIcms20.Cst != null ? new XElement(pf + "CST", det.belImposto.belIcms.belIcms20.Cst.ToString()) : null),
                                                            (det.belImposto.belIcms.belIcms20.Modbc != null ? new XElement(pf + "modBC", det.belImposto.belIcms.belIcms20.Modbc.ToString()) : null),
                                                            (det.belImposto.belIcms.belIcms20.Predbc != null ? new XElement(pf + "pRedBC", det.belImposto.belIcms.belIcms20.Predbc.ToString("#0.00").Replace(",", ".")) : null),
                                                            (det.belImposto.belIcms.belIcms20.Vbc != null ? new XElement(pf + "vBC", det.belImposto.belIcms.belIcms20.Vbc.ToString("#0.00").Replace(",", ".")) : null),
                                                            (det.belImposto.belIcms.belIcms20.Picms != null ? new XElement(pf + "pICMS", det.belImposto.belIcms.belIcms20.Picms.ToString("#0.00").Replace(",", ".")) : null),
                                                            (det.belImposto.belIcms.belIcms20.Vicms != null ? new XElement(pf + "vICMS", det.belImposto.belIcms.belIcms20.Vicms.ToString("#0.00").Replace(",", ".")) : null)) : null),

                                                        //-------------ICMS30-------------//

                                                        (det.belImposto.belIcms.belIcms30 != null ?
                                                        new XElement(pf + "ICMS30",
                                                            (det.belImposto.belIcms.belIcms30.Orig != null ? new XElement(pf + "orig", det.belImposto.belIcms.belIcms30.Orig.ToString()) : null),
                                                            (det.belImposto.belIcms.belIcms30.Cst != null ? new XElement(pf + "CST", det.belImposto.belIcms.belIcms30.Cst.ToString()) : null),
                                                            (det.belImposto.belIcms.belIcms30.Modbcst != null ? new XElement(pf + "modBCST", det.belImposto.belIcms.belIcms30.Modbcst.ToString()) : null),
                                                            (det.belImposto.belIcms.belIcms30.Pmvast != 0 ? new XElement(pf + "pMVAST", det.belImposto.belIcms.belIcms30.Pmvast.ToString("#0.00").Replace(",", ".")) : null), //Claudinei - o.s. sem - 12/03/2010
                                                            (det.belImposto.belIcms.belIcms30.Predbcst.ToString() != "0" ? new XElement(pf + "pRedBCST", det.belImposto.belIcms.belIcms30.Predbcst.ToString("#0.00").Replace(",", ".")) : null),
                                                            (det.belImposto.belIcms.belIcms30.Vbcst != null ? new XElement(pf + "vBCST", det.belImposto.belIcms.belIcms30.Vbcst.ToString("#0.00").Replace(",", ".")) : null),
                                                            (det.belImposto.belIcms.belIcms30.Picmsst != null ? new XElement(pf + "pICMSST", det.belImposto.belIcms.belIcms30.Picmsst.ToString("#0.00").Replace(",", ".")) : null),
                                                            (det.belImposto.belIcms.belIcms30.Vicmsst != null ? new XElement(pf + "vICMSST", det.belImposto.belIcms.belIcms30.Vicmsst.ToString("#0.00").Replace(",", ".")) : null)) : null),

                                                        //-------------ICMS40-------------//

                                                        (det.belImposto.belIcms.belIcms40 != null ?
                                                        new XElement(pf + "ICMS40",
                                                            (det.belImposto.belIcms.belIcms40.Orig != null ? new XElement(pf + "orig", det.belImposto.belIcms.belIcms40.Orig.ToString()) : null),
                                                            (det.belImposto.belIcms.belIcms40.Cst != null ? new XElement(pf + "CST", det.belImposto.belIcms.belIcms40.Cst.ToString()) : null),
                                                            (det.belImposto.belIcms.belIcms40.Vicms != null ? new XElement(pf + "vICMS", det.belImposto.belIcms.belIcms40.Vicms.ToString()) : null), //NFe_2.0
                                                            (det.belImposto.belIcms.belIcms40.motDesICMS != null ? new XElement(pf + "motDesICMS", det.belImposto.belIcms.belIcms40.motDesICMS.ToString()) : null)) : null),//NFe_2.0

                                                        //-------------ICMS41-------------//

                                                        (det.belImposto.belIcms.belIcms41 != null ?
                                                        new XElement(pf + "ICMS41",
                                                            (det.belImposto.belIcms.belIcms41.Orig != null ? new XElement(pf + "orig", det.belImposto.belIcms.belIcms41.Orig.ToString()) : null),
                                                            (det.belImposto.belIcms.belIcms41.Cst != null ? new XElement(pf + "CST", det.belImposto.belIcms.belIcms41.Cst.ToString()) : null),
                                                            (det.belImposto.belIcms.belIcms41.Vicms != null ? new XElement(pf + "vICMS", det.belImposto.belIcms.belIcms41.Vicms.ToString()) : null),//NFe_2.0
                                                            (det.belImposto.belIcms.belIcms41.motDesICMS != null ? new XElement(pf + "motDesICMS", det.belImposto.belIcms.belIcms41.motDesICMS.ToString()) : null)) : null),//NFe_2.0

                                                        //-------------ICMS50-------------//

                                                        (det.belImposto.belIcms.belIcms50 != null ?
                                                        new XElement(pf + "ICMS50",
                                                            (det.belImposto.belIcms.belIcms50.Orig != null ? new XElement(pf + "orig", det.belImposto.belIcms.belIcms50.Orig.ToString()) : null),
                                                            (det.belImposto.belIcms.belIcms50.Cst != null ? new XElement(pf + "CST", det.belImposto.belIcms.belIcms50.Cst.ToString()) : null),
                                                                (det.belImposto.belIcms.belIcms50.Vicms != null ? new XElement(pf + "vICMS", det.belImposto.belIcms.belIcms50.Vicms.ToString()) : null),//NFe_2.0
                                                            (det.belImposto.belIcms.belIcms50.motDesICMS != null ? new XElement(pf + "motDesICMS", det.belImposto.belIcms.belIcms50.motDesICMS.ToString()) : null)) : null),//NFe_2.0

                                                        //-------------ICMS51-------------//

                                                        (det.belImposto.belIcms.belIcms51 != null ?
                                                        new XElement(pf + "ICMS51",
                                                            (det.belImposto.belIcms.belIcms51.Orig != null ? new XElement(pf + "orig", det.belImposto.belIcms.belIcms51.Orig.ToString()) : null),
                                                            (det.belImposto.belIcms.belIcms51.Cst != null ? new XElement(pf + "CST", det.belImposto.belIcms.belIcms51.Cst.ToString()) : null),
                                                            (det.belImposto.belIcms.belIcms51.Modbc != null ? new XElement(pf + "modBC", det.belImposto.belIcms.belIcms51.Modbc.ToString()) : null),
                                                            (det.belImposto.belIcms.belIcms51.Predbc != null ? new XElement(pf + "pRedBC", det.belImposto.belIcms.belIcms51.Predbc.ToString("#0.00").Replace(",", ".")) : null),
                                                            (det.belImposto.belIcms.belIcms51.Vbc != null ? new XElement(pf + "vBC", det.belImposto.belIcms.belIcms51.Vbc.ToString("#0.00").Replace(",", ".")) : null),
                                                            (det.belImposto.belIcms.belIcms51.Picms != null ? new XElement(pf + "pICMS", det.belImposto.belIcms.belIcms51.Picms.ToString("#0.00").Replace(",", ".")) : null),
                                                            (det.belImposto.belIcms.belIcms51.Vicms != null ? new XElement(pf + "vICMS", det.belImposto.belIcms.belIcms51.Vicms.ToString("#0.00").Replace(",", ".")) : null)) : null),

                                                        //-------------ICMS60-------------//

                                                        (det.belImposto.belIcms.belIcms60 != null ?
                                                        new XElement(pf + "ICMS60",//Danner - o.s. sem - 12/03/2010
                                                            (det.belImposto.belIcms.belIcms60.Orig != null ? new XElement(pf + "orig", det.belImposto.belIcms.belIcms60.Orig.ToString()) : null),
                                                            (det.belImposto.belIcms.belIcms60.Cst != null ? new XElement(pf + "CST", det.belImposto.belIcms.belIcms60.Cst.ToString()) : null),
                                                            (det.belImposto.belIcms.belIcms60.Vbcst != null ? new XElement(pf + "vBCSTRet", det.belImposto.belIcms.belIcms60.Vbcst.ToString("#0.00").Replace(",", ".")) : null),//NFe_2.0 - Mudança de nome de Tag
                                                            (det.belImposto.belIcms.belIcms60.Vicmsst != null ? new XElement(pf + "vICMSSTRet", det.belImposto.belIcms.belIcms60.Vicmsst.ToString("#0.00").Replace(",", ".")) : null)) : null),//NFe_2.0 Mudança de nome de Tag

                                                        //-------------ICMS70-------------//

                                                        (det.belImposto.belIcms.belIcms70 != null ?
                                                        new XElement(pf + "ICMS70",
                                                            (det.belImposto.belIcms.belIcms70.Orig != null ? new XElement(pf + "orig", det.belImposto.belIcms.belIcms70.Orig.ToString()) : null),
                                                            (det.belImposto.belIcms.belIcms70.Cst != null ? new XElement(pf + "CST", det.belImposto.belIcms.belIcms70.Cst.ToString()) : null),
                                                            (det.belImposto.belIcms.belIcms70.Modbc != null ? new XElement(pf + "modBC", det.belImposto.belIcms.belIcms70.Modbc.ToString()) : null),
                                                            (det.belImposto.belIcms.belIcms70.Predbc != null ? new XElement(pf + "pRedBC", det.belImposto.belIcms.belIcms70.Predbc.ToString("#0.00").Replace(',', '.')) : null), //Danner - o.s. 24091 - 06/02/2010
                                                            (det.belImposto.belIcms.belIcms70.Vbc != null ? new XElement(pf + "vBC", det.belImposto.belIcms.belIcms70.Vbc.ToString("#0.00").Replace(",", ".")) : null),
                                                            (det.belImposto.belIcms.belIcms70.Picms != null ? new XElement(pf + "pICMS", det.belImposto.belIcms.belIcms70.Picms.ToString("#0.00").Replace(",", ".")) : null),
                                                            (det.belImposto.belIcms.belIcms70.Vicms != null ? new XElement(pf + "vICMS", det.belImposto.belIcms.belIcms70.Vicms.ToString("#0.00").Replace(",", ".")) : null),
                                                            (det.belImposto.belIcms.belIcms70.Modbcst != null ? new XElement(pf + "modBCST", det.belImposto.belIcms.belIcms70.Modbcst.ToString()) : null),
                                                            (det.belImposto.belIcms.belIcms70.Pmvast != 0 ? new XElement(pf + "pMVAST", det.belImposto.belIcms.belIcms70.Pmvast.ToString("#0.00").Replace(",", ".")) : null), //Claudinei - o.s. sem - 12/03/2010
                                                            (det.belImposto.belIcms.belIcms70.Predbcst.ToString() != "0" ? new XElement(pf + "pRedBCST", det.belImposto.belIcms.belIcms70.Predbcst.ToString("#0.00").Replace(",", ".")) : null),
                                                            (det.belImposto.belIcms.belIcms70.Vbcst != null ? new XElement(pf + "vBCST", det.belImposto.belIcms.belIcms70.Vbcst.ToString("#0.00").Replace(",", ".")) : null),
                                                            (det.belImposto.belIcms.belIcms70.Picmsst != null ? new XElement(pf + "pICMSST", det.belImposto.belIcms.belIcms70.Picmsst.ToString("#0.00").Replace(",", ".")) : null),
                                                            (det.belImposto.belIcms.belIcms70.Vicmsst != null ? new XElement(pf + "vICMSST", det.belImposto.belIcms.belIcms70.Vicmsst.ToString("#0.00").Replace(",", ".")) : null)) : null),

                                                        //-------------ICMS90-------------//

                                                        (det.belImposto.belIcms.belIcms90 != null ?
                                                        new XElement(pf + "ICMS90",
                                                            (det.belImposto.belIcms.belIcms90.Orig != null ? new XElement(pf + "orig", det.belImposto.belIcms.belIcms90.Orig.ToString()) : null),
                                                            (det.belImposto.belIcms.belIcms90.Cst != null ? new XElement(pf + "CST", det.belImposto.belIcms.belIcms90.Cst.ToString()) : null),
                                                            (det.belImposto.belIcms.belIcms90.Modbc != null ? new XElement(pf + "modBC", det.belImposto.belIcms.belIcms90.Modbc.ToString()) : null),
                                                            (det.belImposto.belIcms.belIcms90.Vbc != null ? new XElement(pf + "vBC", det.belImposto.belIcms.belIcms90.Vbc.ToString("#0.00").Replace(",", ".")) : null),
                                                            (det.belImposto.belIcms.belIcms90.Predbc != 0 ? new XElement(pf + "pRedBC", det.belImposto.belIcms.belIcms90.Predbc.ToString("#0.00").Replace(',', '.')) : null), //Danner - o.s. 24091 - 06/02/2010 //Claudinei - o.s. sem - 24/02/2010
                                                            (det.belImposto.belIcms.belIcms90.Picms != null ? new XElement(pf + "pICMS", det.belImposto.belIcms.belIcms90.Picms.ToString("#0.00").Replace(",", ".")) : null),
                                                            (det.belImposto.belIcms.belIcms90.Vicms != null ? new XElement(pf + "vICMS", det.belImposto.belIcms.belIcms90.Vicms.ToString("#0.00").Replace(",", ".")) : null),
                                                            (det.belImposto.belIcms.belIcms90.Modbcst != null ? new XElement(pf + "modBCST", det.belImposto.belIcms.belIcms90.Modbcst.ToString()) : null),
                                                            (det.belImposto.belIcms.belIcms90.Pmvast != 0 ? new XElement(pf + "pMVAST", det.belImposto.belIcms.belIcms90.Pmvast.ToString("#0.00").Replace(",", ".")) : null), //Claudinei - o.s. 24076 - 01/02/2010
                                                            (det.belImposto.belIcms.belIcms90.Predbcst.ToString() != "0" ? new XElement(pf + "pRedBCST", det.belImposto.belIcms.belIcms90.Predbcst.ToString("#0.00").Replace(",", ".")) : null), //Claudinei - o.s. 24076 - 01/02/2010
                                                            (det.belImposto.belIcms.belIcms90.Vbcst != null ? new XElement(pf + "vBCST", det.belImposto.belIcms.belIcms90.Vbcst.ToString("#0.00").Replace(",", ".")) : null),
                                                            (det.belImposto.belIcms.belIcms90.Picmsst != null ? new XElement(pf + "pICMSST", det.belImposto.belIcms.belIcms90.Picmsst.ToString("#0.00").Replace(",", ".")) : null),
                                                            (det.belImposto.belIcms.belIcms90.Vicmsst != null ? new XElement(pf + "vICMSST", det.belImposto.belIcms.belIcms90.Vicmsst.ToString("#0.00").Replace(",", ".")) : null)) : null),

                                                       //-------------ICMSSN101--------------//
                                                       (det.belImposto.belIcms.belICMSSN101 != null ?
                                                            new XElement(pf + "ICMSSN101",
                                                               (det.belImposto.belIcms.belICMSSN101.orig != null ? new XElement(pf + "orig", det.belImposto.belIcms.belICMSSN101.orig.ToString()) : null),
                                                               new XElement(pf + "CSOSN", det.belImposto.belIcms.belICMSSN101.CSOSN.ToString()),
                                                               (det.belImposto.belIcms.belICMSSN101.pCredSN != null ? new XElement(pf + "pCredSN", det.belImposto.belIcms.belICMSSN101.pCredSN.ToString("#0.00").Replace(",", ".")) : null),
                                                               (det.belImposto.belIcms.belICMSSN101.vCredICMSSN != null ? new XElement(pf + "vCredICMSSN", det.belImposto.belIcms.belICMSSN101.vCredICMSSN.ToString("#0.00").Replace(",", ".")) : null)) : null),

                                                      //-------------ICMSSN102--------------//
                                                       (det.belImposto.belIcms.belICMSSN102 != null ?
                                                            new XElement(pf + "ICMSSN102",
                                                               (det.belImposto.belIcms.belICMSSN102.orig != null ? new XElement(pf + "orig", det.belImposto.belIcms.belICMSSN102.orig.ToString()) : null),
                                                               new XElement(pf + "CSOSN", det.belImposto.belIcms.belICMSSN102.CSOSN.ToString())) : null),

                                                      //-------------ICMSSN201--------------//
                                                       (det.belImposto.belIcms.belICMSSN201 != null ?
                                                            new XElement(pf + "ICMSSN201",
                                                               (det.belImposto.belIcms.belICMSSN201.orig != null ? new XElement(pf + "orig", det.belImposto.belIcms.belICMSSN201.orig.ToString()) : null),
                                                               new XElement(pf + "CSOSN", det.belImposto.belIcms.belICMSSN201.CSOSN.ToString()),
                                                               (det.belImposto.belIcms.belICMSSN201.modBCST != null ? new XElement(pf + "modBCST", det.belImposto.belIcms.belICMSSN201.modBCST.ToString()) : null),
                                                               (det.belImposto.belIcms.belICMSSN201.pMVAST != null ? new XElement(pf + "pMVAST", det.belImposto.belIcms.belICMSSN201.pMVAST.ToString("#0.00").Replace(",", ".")) : null),
                                                               (det.belImposto.belIcms.belICMSSN201.pRedBCST != null ? new XElement(pf + "pRedBCST", det.belImposto.belIcms.belICMSSN201.pRedBCST.ToString("#0.00").Replace(",", ".")) : null),
                                                               (det.belImposto.belIcms.belICMSSN201.vBCST != null ? new XElement(pf + "vBCST", det.belImposto.belIcms.belICMSSN201.vBCST.ToString("#0.00").Replace(",", ".")) : null),
                                                               (det.belImposto.belIcms.belICMSSN201.pICMSST != null ? new XElement(pf + "pICMSST", det.belImposto.belIcms.belICMSSN201.pICMSST.ToString("#0.00").Replace(",", ".")) : null),
                                                               (det.belImposto.belIcms.belICMSSN201.vICMSST != null ? new XElement(pf + "vICMSST", det.belImposto.belIcms.belICMSSN201.vICMSST.ToString("#0.00").Replace(",", ".")) : null),
                                                               (det.belImposto.belIcms.belICMSSN201.pCredSN != null ? new XElement(pf + "pCredSN", det.belImposto.belIcms.belICMSSN201.pCredSN.ToString("#0.00").Replace(",", ".")) : null),
                                                               (det.belImposto.belIcms.belICMSSN201.vCredICMSSN != null ? new XElement(pf + "vCredICMSSN", det.belImposto.belIcms.belICMSSN201.vCredICMSSN.ToString("#0.00").Replace(",", ".")) : null)) : null),

                                                      //-------------ICMSSN500--------------//
                                                       (det.belImposto.belIcms.belICMSSN500 != null ?
                                                            new XElement(pf + "ICMSSN500",
                                                               (det.belImposto.belIcms.belICMSSN500.orig != null ? new XElement(pf + "orig", det.belImposto.belIcms.belICMSSN500.orig.ToString()) : null),
                                                               new XElement(pf + "CSOSN", det.belImposto.belIcms.belICMSSN500.CSOSN.ToString()),
                                                               (det.belImposto.belIcms.belICMSSN500.vBCSTRet != null ? new XElement(pf + "vBCSTRet", det.belImposto.belIcms.belICMSSN500.vBCSTRet.ToString("#0.00").Replace(",", ".")) : null),
                                                               (det.belImposto.belIcms.belICMSSN500.vICMSSTRet != null ? new XElement(pf + "vICMSSTRet", det.belImposto.belIcms.belICMSSN500.vICMSSTRet.ToString("#0.00").Replace(",", ".")) : null)) : null),

                                                      //-------------ICMSSN900--------------//
                                                       (det.belImposto.belIcms.belICMSSN900 != null ?
                                                            new XElement(pf + "ICMSSN900",
                                                               (det.belImposto.belIcms.belICMSSN900.orig != null ? new XElement(pf + "orig", det.belImposto.belIcms.belICMSSN900.orig.ToString()) : null),
                                                               new XElement(pf + "CSOSN", det.belImposto.belIcms.belICMSSN900.CSOSN.ToString())) : null)),
                                //(det.belImposto.belIcms.belICMSSN900.modBC != null ? new XElement(pf + "modBC", det.belImposto.belIcms.belICMSSN900.modBC.ToString()) : null),
                                //(det.belImposto.belIcms.belICMSSN900.vBC != null ? new XElement(pf + "vBC", det.belImposto.belIcms.belICMSSN900.vBC.ToString("#0.00").Replace(",", ".")) : null),
                                //(det.belImposto.belIcms.belICMSSN900.pRedBC != null ? new XElement(pf + "pRedBC", det.belImposto.belIcms.belICMSSN900.pRedBC.ToString("#0.00").Replace(",", ".")) : null),
                                //(det.belImposto.belIcms.belICMSSN900.pICMS != null ? new XElement(pf + "pICMS", det.belImposto.belIcms.belICMSSN900.pICMS.ToString("#0.00").Replace(",", ".")) : null),
                                //(det.belImposto.belIcms.belICMSSN900.vICMS != null ? new XElement(pf + "vICMS", det.belImposto.belIcms.belICMSSN900.vICMS.ToString("#0.00").Replace(",", ".")) : null),
                                //(det.belImposto.belIcms.belICMSSN900.modBCST != null ? new XElement(pf + "modBCST", det.belImposto.belIcms.belICMSSN900.modBCST.ToString()) : null),
                                //(det.belImposto.belIcms.belICMSSN900.pMVAST != null ? new XElement(pf + "pMVAST", det.belImposto.belIcms.belICMSSN900.pMVAST.ToString("#0.00").Replace(",", ".")) : null),
                                //(det.belImposto.belIcms.belICMSSN900.pRedBCST != null ? new XElement(pf + "pRedBCST", det.belImposto.belIcms.belICMSSN900.pRedBCST.ToString("#0.00").Replace(",", ".")) : null),
                                //(det.belImposto.belIcms.belICMSSN900.vBCST != null ? new XElement(pf + "vBCST", det.belImposto.belIcms.belICMSSN900.vBCST.ToString("#0.00").Replace(",", ".")) : null),
                                //(det.belImposto.belIcms.belICMSSN900.pICMSST != null ? new XElement(pf + "pICMSST", det.belImposto.belIcms.belICMSSN900.pICMSST.ToString("#0.00").Replace(",", ".")) : null),
                                //(det.belImposto.belIcms.belICMSSN900.vICMSST != null ? new XElement(pf + "vICMSST", det.belImposto.belIcms.belICMSSN900.vICMSST.ToString("#0.00").Replace(",", ".")) : null),
                                //(det.belImposto.belIcms.belICMSSN900.vBCSTRet != null ? new XElement(pf + "vBCSTRet", det.belImposto.belIcms.belICMSSN900.vBCSTRet.ToString("#0.00").Replace(",", ".")) : null),
                                //(det.belImposto.belIcms.belICMSSN900.vICMSSTRet != null ? new XElement(pf + "vICMSSTRet", det.belImposto.belIcms.belICMSSN900.vICMSSTRet.ToString("#0.00").Replace(",", ".")) : null),
                                //(det.belImposto.belIcms.belICMSSN900.pCredSN != null ? new XElement(pf + "pCredSN", det.belImposto.belIcms.belICMSSN900.pCredSN.ToString("#0.00").Replace(",", ".")) : null),
                                //(det.belImposto.belIcms.belICMSSN900.vCredICMSSN != null ? new XElement(pf + "vCredICMSSN", det.belImposto.belIcms.belICMSSN900.vCredICMSSN.ToString("#0.00").Replace(",", ".")) : null)) : null)),

                                        //---------------IPI-------------//
                                            (det.belImposto.belIpi != null ?
                                            new XElement(pf + "IPI",
                                                (det.belImposto.belIpi.Clenq != null ?
                                                    new XElement(pf + "clEnq", det.belImposto.belIpi.Clenq.ToString()) : null),
                                                (det.belImposto.belIpi.Cnpjprod != null ?
                                                    new XElement(pf + "CNPJProd", det.belImposto.belIpi.Cnpjprod.ToString()) : null),
                                                (det.belImposto.belIpi.Cselo != null ?
                                                    new XElement(pf + "cSelo", det.belImposto.belIpi.Cselo.ToString()) : null),
                                                (det.belImposto.belIpi.Qselo != null ?
                                                    new XElement(pf + "qSelo", det.belImposto.belIpi.Qselo.ToString()) : null),
                                                (det.belImposto.belIpi.Cenq != null ?
                                                    new XElement(pf + "cEnq", det.belImposto.belIpi.Cenq.ToString()) : null),

                                                //-----------IPITrib-----------//    

                                                (det.belImposto.belIpi.belIpitrib != null ?
                                                new XElement(pf + "IPITrib",
                                                    (det.belImposto.belIpi.belIpitrib.Cst != null ? new XElement(pf + "CST", det.belImposto.belIpi.belIpitrib.Cst.ToString()) : null),
                                                    (det.belImposto.belIpi.belIpitrib.Vbc != null ? new XElement(pf + "vBC", det.belImposto.belIpi.belIpitrib.Vbc.ToString("#0.00").Replace(",", ".")) : null),
                                                    (det.belImposto.belIpi.belIpitrib.Qunid != null ? new XElement(pf + "qUnid", det.belImposto.belIpi.belIpitrib.Qunid.ToString()) : null),
                                                    (det.belImposto.belIpi.belIpitrib.Vunid != 0 ? new XElement(pf + "vUnid", det.belImposto.belIpi.belIpitrib.Vunid.ToString("#0.0000").Replace(",", ".")) : null),
                                                    (det.belImposto.belIpi.belIpitrib.Pipi != null ? new XElement(pf + "pIPI", det.belImposto.belIpi.belIpitrib.Pipi.ToString("#0.00").Replace(",", ".")) : null),
                                                    (det.belImposto.belIpi.belIpitrib.Vipi != null ? new XElement(pf + "vIPI", det.belImposto.belIpi.belIpitrib.Vipi.ToString("#0.00").Replace(",", ".")) : null)) : null),

                                                //-----------IPINT-----------//

                                                (det.belImposto.belIpi.belIpint != null ?
                                                new XElement(pf + "IPINT",
                                                    (det.belImposto.belIpi.belIpint != null ? new XElement(pf + "CST", det.belImposto.belIpi.belIpint.Cst.ToString()) : null)) : null)) : null),




                                       //--------------II--------------//             
                                       (det.belImposto.belIi != null ?
                                       new XElement(pf + "II",
                                           (det.belImposto.belIi.Vbc != null ? new XElement(pf + "vBC", det.belImposto.belIi.Vbc.ToString("#0.00").Replace(',', '.')) : null),
                                           (det.belImposto.belIi.Vdespadu != null ? new XElement(pf + "vDespAdu", det.belImposto.belIi.Vdespadu.ToString()) : null),
                                           (det.belImposto.belIi.Vii != null ? new XElement(pf + "vII", det.belImposto.belIi.Vii.ToString("0.00").Replace(',', '.')) : null),
                                           (det.belImposto.belIi.Viof != null ? new XElement(pf + "vIOF", det.belImposto.belIi.Viof.ToString("#0.00").Replace(',', '.')) : null)) : null),



                               //----------------PIS------------//

                                 (det.belImposto.belPis != null ?
                                 new XElement(pf + "PIS",

                                     //-----------PISAliq----------//

                                     (det.belImposto.belPis.belPisaliq != null ?
                                     new XElement(pf + "PISAliq",
                                         (det.belImposto.belPis.belPisaliq.Cst != null ? new XElement(pf + "CST", det.belImposto.belPis.belPisaliq.Cst.ToString()) : null),
                                         (det.belImposto.belPis.belPisaliq.Vbc != null ? new XElement(pf + "vBC", det.belImposto.belPis.belPisaliq.Vbc.ToString("#0.00").Replace(",", ".")) : null),
                                         (det.belImposto.belPis.belPisaliq.Ppis != null ? new XElement(pf + "pPIS", det.belImposto.belPis.belPisaliq.Ppis.ToString("#0.00").Replace(",", ".")) : null),
                                         (det.belImposto.belPis.belPisaliq.Vpis != null ? new XElement(pf + "vPIS", det.belImposto.belPis.belPisaliq.Vpis.ToString("#0.00").Replace(",", ".")) : null)) : null),

                                    //-----------PISQtde-----------//                               
                                     (det.belImposto.belPis.belPisqtde != null ?
                                     new XElement(pf + "PISQtde",
                                         (det.belImposto.belPis.belPisqtde.Cst != null ? new XElement(pf + "CST", det.belImposto.belPis.belPisqtde.Cst.ToString()) : null),
                                         (det.belImposto.belPis.belPisqtde.Qbcprod != 0 ? new XElement(pf + "qBCProd", det.belImposto.belPis.belPisqtde.Qbcprod.ToString()) : null),
                                         (det.belImposto.belPis.belPisqtde.Valiqprod != null ? new XElement(pf + "vAliqProd", det.belImposto.belPis.belPisqtde.Valiqprod.ToString("#0.00").Replace(",", ".")) : null),
                                         (det.belImposto.belPis.belPisqtde.Vpis != null ? new XElement(pf + "vPIS", det.belImposto.belPis.belPisqtde.Vpis.ToString("#0.00").Replace(",", ".")) : null)) : null),

                                     //----------PISNT------------//

                                     (det.belImposto.belPis.belPisnt != null ?
                                     new XElement(pf + "PISNT",
                                         (det.belImposto.belPis.belPisnt.Cst != null ? new XElement(pf + "CST", det.belImposto.belPis.belPisnt.Cst.ToString()) : null)) : null),

                                     //----------PISOutr-----------//

                                     (det.belImposto.belPis.belPisoutr != null ?
                                     new XElement(pf + "PISOutr",
                                         (det.belImposto.belPis.belPisoutr.Cst != null ? new XElement(pf + "CST", det.belImposto.belPis.belPisoutr.Cst.ToString()) : null),
                                         (det.belImposto.belPis.belPisoutr.Vbc != null ? new XElement(pf + "vBC", det.belImposto.belPis.belPisoutr.Vbc.ToString("#0.00").Replace(",", ".")) : null),
                                         (det.belImposto.belPis.belPisoutr.Ppis != null ? new XElement(pf + "pPIS", det.belImposto.belPis.belPisoutr.Ppis.ToString("#0.00").Replace(",", ".")) : null),
                                         (det.belImposto.belPis.belPisoutr.Qbcprod != 0 ? new XElement(pf + "qBCProd", det.belImposto.belPis.belPisoutr.Qbcprod.ToString()) : null),
                                         (det.belImposto.belPis.belPisoutr.Valiqprod != 0 ? new XElement(pf + "vAliqProd", det.belImposto.belPis.belPisoutr.Valiqprod.ToString("#0.0000").Replace(",", ".")) : null),
                                         (det.belImposto.belPis.belPisoutr.Vpis != null ? new XElement(pf + "vPIS", det.belImposto.belPis.belPisoutr.Vpis.ToString("#0.00").Replace(",", ".")) : null)) : null)) : null),







                                 //---------------COFINS---------------//
                                 (det.belImposto.belCofins != null ?
                                 new XElement(pf + "COFINS",

                                     //-----------COFINSAliq------------//

                                     (det.belImposto.belCofins.belCofinsaliq != null ?
                                     new XElement(pf + "COFINSAliq",
                                         new XElement(pf + "CST", det.belImposto.belCofins.belCofinsaliq.Cst.ToString()),
                                         new XElement(pf + "vBC", det.belImposto.belCofins.belCofinsaliq.Vbc.ToString("#0.00").Replace(",", ".")),
                                         new XElement(pf + "pCOFINS", det.belImposto.belCofins.belCofinsaliq.Pcofins.ToString("#0.00").Replace(",", ".")),
                                         new XElement(pf + "vCOFINS", det.belImposto.belCofins.belCofinsaliq.Vcofins.ToString("#0.00").Replace(",", "."))) : null),

                                     //------------COFINSQtde------------//

                                     (det.belImposto.belCofins.belCofinsqtde != null ?
                                     new XElement(pf + "COFINSQtde",
                                         new XElement(pf + "CST", det.belImposto.belCofins.belCofinsqtde.Cst.ToString()),
                                         new XElement(pf + "pBCProd", det.belImposto.belCofins.belCofinsqtde.Qbcprod.ToString()),
                                         new XElement(pf + "vAliqProd", det.belImposto.belCofins.belCofinsqtde.Valiqprod.ToString("#0.00").Replace(",", ".")),
                                         new XElement(pf + "vCOFINS", det.belImposto.belCofins.belCofinsqtde.Vcofins.ToString("#0.00").Replace(",", "."))) : null),

                                     //------------COFINSNT--------------//

                                     (det.belImposto.belCofins.belCofinsnt != null ?
                                     new XElement(pf + "COFINSNT",
                                         (det.belImposto.belCofins.belCofinsnt.Cst != null ? new XElement(pf + "CST", det.belImposto.belCofins.belCofinsnt.Cst.ToString()) : null)) : null),

                                     //------------COFINSOutr--------------//

                                     (det.belImposto.belCofins.belCofinsoutr != null ?
                                     new XElement(pf + "COFINSOutr",
                                         new XElement(pf + "CST", det.belImposto.belCofins.belCofinsoutr.Cst.ToString()),
                                         new XElement(pf + "vBC", det.belImposto.belCofins.belCofinsoutr.Vbc.ToString("#0.00").Replace(",", ".")),
                                         new XElement(pf + "pCOFINS", det.belImposto.belCofins.belCofinsoutr.Pcofins.ToString("#0.00").Replace(",", ".")),
                                         new XElement(pf + "vCOFINS", det.belImposto.belCofins.belCofinsoutr.Vcofins.ToString("#0.00").Replace(",", "."))) : null)) : null),


                                 //----------------ISSQN-----------------//

                                 (det.belImposto.belIss != null ?
                                 new XElement(pf + "ISSQN",
                                     (det.belImposto.belIss.Vbc != null ? new XElement(pf + "vBC", det.belImposto.belIss.Vbc.ToString("#0.00").Replace(",", ".")) : null),
                                     (det.belImposto.belIss.Valiq != null ? new XElement(pf + "vAliq", det.belImposto.belIss.Valiq.ToString("#0.00").Replace(",", ".")) : null),
                                     (det.belImposto.belIss.Vissqn != null ? new XElement(pf + "vISSQN", det.belImposto.belIss.Vissqn.ToString("#0.00").Replace(",", ".")) : null),
                                     (det.belImposto.belIss.Cmunfg != null ? new XElement(pf + "cMunFG", det.belImposto.belIss.Cmunfg.ToString()) : null),
                                     (det.belImposto.belIss.Clistserv != null ? new XElement(pf + "cListServ", det.belImposto.belIss.Clistserv.ToString()) : null),
                                     (det.belImposto.belIss.cSitTrib != null ? new XElement(pf + "cSitTrib", det.belImposto.belIss.cSitTrib.ToString()) : null)) : null)), // NFe_2.0 Tratar item


                                 //-----------INFADPROD-------------//

                                 (det.belInfadprod != null ?
                                 (det.belInfadprod.Infadprid != null ? new XElement(pf + "infAdProd", det.belInfadprod.Infadprid.ToString()) : null) : null))); //Danner - o.s. sem -21/12/2009

                            lcondet.Add(condet);
                        }
                        #endregion
                    }
                    catch (Exception x)
                    {
                        throw new Exception("Nota de Sequência - " + sNota + "Erro na geração do XML, Regiao XML_Detalhes - " + x.Message);
                    }
                    #endregion

                    //Total
                    XContainer contotal;

                    try
                    {
                        belTotal objtotal = i[5] as belTotal;
                        #region XML_Total

                        contotal = (new XElement(pf + "total",
                                                (objtotal.belIcmstot != null ? new XElement(pf + "ICMSTot",
                                                      new XElement(pf + "vBC", objtotal.belIcmstot.Vbc.ToString("#0.00").Replace(",", ".")),//Danner - o.s. 24271 - 15/03/2010
                                                      new XElement(pf + "vICMS", objtotal.belIcmstot.Vicms.ToString("#0.00").Replace(",", ".")),
                                                      new XElement(pf + "vBCST", objtotal.belIcmstot.Vbcst.ToString("#0.00").Replace(",", ".")),
                                                      new XElement(pf + "vST", objtotal.belIcmstot.Vst.ToString("#0.00").Replace(",", ".")),
                                                      new XElement(pf + "vProd", objtotal.belIcmstot.Vprod.ToString("#0.00").Replace(",", ".")),
                                                      new XElement(pf + "vFrete", objtotal.belIcmstot.Vfrete.ToString("#0.00").Replace(",", ".")),
                                                      new XElement(pf + "vSeg", objtotal.belIcmstot.Vseg.ToString("#0.00").Replace(",", ".")),
                                                      new XElement(pf + "vDesc", objtotal.belIcmstot.Vdesc.ToString("#0.00").Replace(",", ".")),
                                                      new XElement(pf + "vII", objtotal.belIcmstot.Vii.ToString("#0.00").Replace(",", ".")),
                                                      new XElement(pf + "vIPI", objtotal.belIcmstot.Vipi.ToString("#0.00").Replace(",", ".")),
                                                      new XElement(pf + "vPIS", objtotal.belIcmstot.Vpis.ToString("#0.00").Replace(",", ".")),
                                                      new XElement(pf + "vCOFINS", objtotal.belIcmstot.Vcofins.ToString("#0.00").Replace(",", ".")),
                                                      new XElement(pf + "vOutro", objtotal.belIcmstot.Voutro.ToString("#0.00").Replace(",", ".")),
                                                      new XElement(pf + "vNF", objtotal.belIcmstot.Vnf.ToString("#0.00").Replace(",", "."))) : null),



                                                (objtotal.belIssqntot != null ? new XElement(pf + "ISSQNtot",
                                                    new XElement(pf + "vServ", objtotal.belIssqntot.Vserv.ToString("#0.00").Replace(",", ".")),
                                                    new XElement(pf + "vBC", objtotal.belIssqntot.Vbc.ToString("#0.00").Replace(",", ".")),
                                                    new XElement(pf + "vISS", objtotal.belIssqntot.Viss.ToString("#0.00").Replace(",", ".")),
                                                    (objtotal.belIssqntot.Vpis != 0 ? new XElement(pf + "vPIS", objtotal.belIssqntot.Vpis.ToString("#0.00").Replace(",", ".")) : null),
                                                    (objtotal.belIssqntot.Vcofins != 0 ? new XElement(pf + "vCOFINS", objtotal.belIssqntot.Vcofins.ToString("#0.00").Replace(",", ".")) : null)) : null)));
                        //(objtotal.belRetTrib != null ? new XElement(pf + "retTrib",
                        //                                   new XElement(pf + "vRetPIS", objtotal.belRetTrib.Vretpis.ToString("#0.00").Replace(",", ".")),
                        //                                   new XElement(pf + "vRetCOFINS", objtotal.belRetTrib.Vretcofins.ToString("#0.00").Replace(",", ".")),
                        //                                   new XElement(pf + "vRetCSLL", objtotal.belRetTrib.Vretcsll.ToString("#0.00").Replace(",", ".")),
                        //                                   new XElement(pf + "vBCIRRF", objtotal.belRetTrib.Vbcirrf.ToString("#0.00"),
                        //                                   new XElement(pf + "vIRRF", objtotal.belRetTrib.Virrf.ToString("#0.00").Replace(",", ".")),
                        //                                   new XElement(pf + "vBCRetPrev", objtotal.belRetTrib.Vbcretprev.ToString("#0.00").Replace(",", ".")),
                        //                                   new XElement(pf + "vRetPrev", objtotal.belRetTrib.Vretprev.ToString("#0.00").Replace(",", "."))) : null)));

                        #endregion
                    }
                    catch (Exception x)
                    {
                        throw new Exception("Nota de Sequência - " + sNota + "Erro na geração do XML, Regiao XML_Total - " + x.Message);
                    }
                    //Fim - Total

                    //Frete
                    XContainer contransp;
                    belTransp objtransp;
                    try
                    {
                        objtransp = i[6] as belTransp;
                        #region XML_Transporte

                        contransp = (new XElement(pf + "transp",
                                                   new XElement(pf + "modFrete", objtransp.Modfrete.ToString()),
                                                   new XElement(pf + "transporta",
                                                       (objtransp.belTransportadora.Cnpj != null ? new XElement(pf + "CNPJ", objtransp.belTransportadora.Cnpj.ToString()) :
                                                                                                   (objtransp.belTransportadora.Cpf != null ? new XElement(pf + "CPF", objtransp.belTransportadora.Cpf.ToString()) : null)),
                                                       (objtransp.belTransportadora.Xnome != null ? new XElement(pf + "xNome", objtransp.belTransportadora.Xnome.ToString()) : null),
                                                       (objtransp.belTransportadora.Ie != null ? new XElement(pf + "IE", objtransp.belTransportadora.Ie.ToString()) : null),
                                                       (objtransp.belTransportadora.Xender != null ? new XElement(pf + "xEnder", objtransp.belTransportadora.Xender.ToString()) : null),
                                                       (objtransp.belTransportadora.Xmun != null ? new XElement(pf + "xMun", objtransp.belTransportadora.Xmun.ToString()) : null),
                                                       (objtransp.belTransportadora.Uf != null ? new XElement(pf + "UF", objtransp.belTransportadora.Uf.ToString()) : null)),
                (objtransp.belRetTransp != null ? (new XElement(pf + "retTransp",
                                                       new XElement(pf + "vServ", objtransp.belRetTransp.Vserv.ToString("#0.00").Replace(',', '.')),
                                                       new XElement(pf + "vBCRet", objtransp.belRetTransp.Vbvret.ToString("#0.00").Replace(',', '.')),
                                                       new XElement(pf + "pICMSRet", objtransp.belRetTransp.Picmsret.ToString()),
                                                       new XElement(pf + "vICMSRet", objtransp.belRetTransp.Vicmsret.ToString("#0.00").Replace(',', '.')),
                                                       new XElement(pf + "CFOP", objtransp.belRetTransp.Cfop.ToString()),
                                                       new XElement(pf + "cMunFG", objtransp.belRetTransp.Cmunfg.ToString()))) : null),
                (objtransp.belVeicTransp != null ? (new XElement(pf + "veicTransp",
                                                       (objtransp.belVeicTransp.Placa != null ? new XElement(pf + "placa", objtransp.belVeicTransp.Placa.ToString()) : null),
                                                       (objtransp.belVeicTransp.Uf != null ? new XElement(pf + "UF", objtransp.belVeicTransp.Uf.ToString()) : null),
                                                       (objtransp.belVeicTransp.Rntc != null ? new XElement(pf + "RNTC", objtransp.belVeicTransp.Rntc.ToString()) : null))) : null),
                   (objtransp.belReboque != null ? new XElement(pf + "reboque",
                                                       (objtransp.belReboque.Placa != null ? new XElement(pf + "placa", objtransp.belReboque.Placa.ToString()) : null),
                                                       (objtransp.belReboque.Uf != null ? new XElement(pf + "UF", objtransp.belReboque.Uf.ToString()) : null),
                                                       (objtransp.belReboque.Rntc != null ? new XElement(pf + "RNTC", objtransp.belReboque.Rntc.ToString()) : null)) : null),
                       (objtransp.belVol != null ? new XElement(pf + "vol",
                                                       new XElement(pf + "qVol", objtransp.belVol.Qvol.ToString("#")),
                                                       new XElement(pf + "esp", objtransp.belVol.Esp.ToString()),
                                                       new XElement(pf + "marca", objtransp.belVol.Marca.ToString()),
                                                       (objtransp.belVol.Nvol != null ? new XElement(pf + "nVol", objtransp.belVol.Nvol.ToString()) : null),//Danner - o.s. 24385 - 26/04/2010
                                                       new XElement(pf + "pesoL", objtransp.belVol.PesoL.ToString("#0.000").Replace(",", ".")),
                                                       new XElement(pf + "pesoB", objtransp.belVol.PesoB.ToString("#0.000").Replace(",", "."))) : null),
                   (objtransp.belLacres != null ? new XElement(pf + "lacres",
                                                       new XElement(pf + "nLacre", "")) : null)));



                        #endregion
                    }
                    catch (Exception x)
                    {
                        throw new Exception("Nota de Sequência - " + sNota + "Erro na geração do XML, Regiao XML_Transporte - " + x.Message);
                    }
                    //Fim - Frete

                    //Duplicata
                    XContainer concobr;
                    belCobr objcob;
                    try
                    {
                        objcob = i[7] as belCobr;
                        belGerarXML BuscaConexao = new belGerarXML();
                        #region XML_Cobrança
                        concobr = (new XElement(pf + "cobr",
                                                  new XElement(pf + "fat",
                                                      new XElement(pf + "nFat", objcob.belFat.Nfat.ToString()),
                                                      (objcob.belFat.Vorig != 0 ? new XElement(pf + "vOrig", objcob.belFat.Vorig.ToString("#0.00").Replace(",", ".")) : null),
                                                      (objcob.belFat.Vdesc != null && objcob.belFat.Vdesc != 0 ? new XElement(pf + "vDesc", objcob.belFat.Vdesc.ToString("#0.00").Replace(",", ".")) : null),
                                                      (objcob.belFat.Vliq != 0 ? new XElement(pf + "vLiq", objcob.belFat.Vliq.ToString("#0.00").Replace(",", ".")) : null)),
                                                      (objcob.belFat.belDup != null ? from dup in objcob.belFat.belDup
                                                                                      select new XElement(pf + "dup", new XElement(pf + "nDup", dup.Ndup.ToString()),
                                                                                             new XElement(pf + "dVenc", dup.Dvenc.ToString("yyyy-MM-dd")),
                                                                                             (BuscaConexao.nm_Cliente != "LORENZON" ? new XElement(pf + "vDup", dup.Vdup.ToString("#0.00").Replace(",", ".")) : null)) : null)));

                        #endregion
                    }
                    catch (Exception x)
                    {
                        throw new Exception("Nota de Sequência - " + sNota + "Erro na geração do XML, Region XML_Cobrança - " + x.Message);
                    }
                    //Fim - Duplicata

                    //Obs
                    XContainer conobs;
                    belInfAdic objobs;
                    try
                    {
                        objobs = i[8] as belInfAdic;
                        #region XML_Obs
                        conobs = new XElement(pf + "infAdic",
                                                    (objobs.Infcpl != null ?
                                                    new XElement(pf + "infCpl", objobs.Infcpl.ToString()) : null));
                        #endregion
                    }
                    catch (Exception x)
                    {
                        throw new Exception("Nota de Sequência - " + sNota + "Erro na geração do XML,Regiao XML_Obs - " + x.Message);
                    }
                    //Fim - Obs

                    //Uniao dos Containers
                    try
                    {
                        concabec.Add(coninfnfe);
                        coninfnfe.Add(conide);
                        conide.AddAfterSelf(conemit);
                        conemit.AddAfterSelf(condest);
                        condest.AddAfterSelf(contotal);
                        contotal.AddAfterSelf(contransp);

                        if (concobr != null)
                        {
                            contransp.AddAfterSelf(concobr);
                            if (objobs.Infcpl != null)
                                concobr.AddAfterSelf(conobs);
                        }
                        else
                        {
                            if (objobs.Infcpl != null)
                                contransp.AddAfterSelf(conobs);
                        }

                        foreach (XElement x in lcondet)
                        {
                            contotal.AddBeforeSelf(x);
                        }
                    }
                    catch (Exception x)
                    {
                        throw new Exception("Nota de Sequência - " + sNota + "Erro na montagem do XML, União dos Containers - " + x.Message);
                    }
                    try
                    {
                        AssinaNFeXml Assinatura = new AssinaNFeXml();
                        string nfe = Assinatura.ConfigurarArquivo(concabec.ToString(), "infNFe", cert);
                        nfes.Add(nfe);
                        XElement xnfe = XElement.Parse(nfe);
                        XDocument xdocsalvanfesemlot = new XDocument(xnfe);

                        DirectoryInfo dPastaData = new DirectoryInfo(glob.LeRegConfig("PastaXmlEnvio").ToString() + "\\" + sNFe.Substring(5, 4));
                        if (!dPastaData.Exists) { dPastaData.Create(); }
                        if (sFormaEmiNFe.Equals("2"))
                        {
                            xdocsalvanfesemlot.Save(glob.LeRegConfig("PastaContingencia").ToString() + "\\" + sNFe.Replace("NFe", "") + "-nfe.xml");
                        }
                        else
                        {
                            xdocsalvanfesemlot.Save(glob.LeRegConfig("PastaXmlEnvio").ToString() + "\\" + sNFe.Substring(5, 4) + "\\" + sNFe.Replace("NFe", "") + "-nfe.xml"); // OS_25024
                        }
                        //StreamWriter swnfe = new StreamWriter(glob.LeRegWin("PastaXmlEnvio").ToString() + "\\" + sNFe.Replace("NFe", "") + "-nfe.xml");
                        //swnfe.Write(nfe);
                        //swnfe.Close();
                    }
                    catch (Exception x)
                    {
                        throw new Exception("Nota de Sequência - " + sNota + "Erro ao assinar a nfe de sequencia " + sNota + x.Message);
                    }

                    iCount++;
                }

                //Concatenando as NFes.
                string sXmlComp = "";
                //Junta todos os XML's em texto 
                foreach (var i in nfes)
                {
                    sXmlComp = sXmlComp + i;
                }
                //Estou inserindo o enviNFe pois se eu transformar o arquivo xml assinado em xml e depois em testo usando um toString,
                //a assinatura se torna invalida. Então depois de assinado do trabalho em forma de testo como xml ateh envia-lo pra fazenda.                        
                sXmlfull = "<?xml version=\"1.0\" encoding=\"utf-8\"?><enviNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"2.00\"><idLote>" +
                            sNomeArq.Substring(7, 15) + "</idLote>" + sXmlComp + "</enviNFe>";

                //Grava
                StreamWriter sw = new StreamWriter(sPath);
                sw.Write(sXmlfull);
                sw.Close();

                #region Valida_Xml

                Globais getschema = new Globais();

                //string sXml = File.OpenText(sPath).ReadToEnd();

                XmlSchemaCollection myschema = new XmlSchemaCollection();

                XmlValidatingReader reader;

                //Danner - o.s. 23732 - 06/11/2009

                try
                {
                    XmlParserContext context = new XmlParserContext(null, null, "", XmlSpace.None);

                    reader = new XmlValidatingReader(sXmlfull, XmlNodeType.Element, context);

                    myschema.Add("http://www.portalfiscal.inf.br/nfe", getschema.LeRegConfig("PastaSchema") + "\\enviNFe_v2.00.xsd");

                    reader.ValidationType = ValidationType.Schema;

                    reader.Schemas.Add(myschema);

                    while (reader.Read())
                    {

                    }

                }
                catch (XmlException x)
                {
                    File.Delete(sPath);
                    throw new Exception(x.Message);
                }
                catch (XmlSchemaException x)
                {
                    File.Delete(sPath);
                    throw new Exception(x.Message);
                }

                //Fim - Danner - o.s. 23732 - 06/11/2009

                #endregion

            }
            catch (Exception ex)
            {
                Conn.Close();
                throw ex;
            }
            finally { Conn.Close(); }
        }


        /// <summary>
        /// Popula as Classes da NF-e.
        /// </summary>
        /// <param name="sEmp"></param>
        /// <param name="sNF"></param>
        /// <param name="sNomeArq"></param>
        /// <param name="sFormaEmissao"></param>
        /// <param name="sForDanfe"></param>
        /// <param name="tp_amb"></param>
        /// <param name="cd_ufnro"></param>
        /// <param name="cert"></param>
        public void NFe(string sEmp, List<string> sNF, string sNomeArq, string sFormaEmissao, string sForDanfe, int tp_amb, string cd_ufnro, X509Certificate2 cert, bool bModoSCAN, int iSerieSCAN, string sFormaEmiNFe, Version versao)
        {
            objbelGeraXml = new belGerarXML();
            Conn = objbelGeraXml.Conn;
            Conn.Open();
            this.sFormaEmiNFe = sFormaEmiNFe;
            this.bModoSCAN = bModoSCAN;
            this.iSerieSCAN = iSerieSCAN;

            string sPath = "";
            sPath = CarregarDadosXml("PastaXmlEnvio").ToString() + @sNomeArq;
            sTipoIndustrializacao = CarregarDadosXml("Industrializacao")[0].ToString();
            if (File.Exists(sPath))
            {
                File.Delete(sPath);
            }

            XDocument xdoc = new XDocument();
            #region XML_Principal
            XNamespace pf = "http://www.portalfiscal.inf.br/nfe";
            XContainer conenv = new XElement(pf + "enviNFe", new XAttribute("xmlns", "http://www.portalfiscal.inf.br/nfe"),
                                                     new XAttribute("versao", "1.10"),
                                                     new XElement(pf + "idLote", sNomeArq.Substring(7, 15)));
            #endregion


            Globais glob = new Globais();
            int notaindex = 0;

            try
            {
                foreach (string sNota in sNF)
                {
                    List<object> lObjNotas = new List<object>();
                    notaindex++;
                    string sNFe = "NFe" + GeraChave(sEmp, sNota, Conn);


                    // infNFE                
                    // Começa a Popular as Classes
                    //Ide               
                    belIde objide;
                    objide = BuscaIde(sEmp, sNota, sForDanfe, sFormaEmissao, versao);
                    lObjNotas.Add(objide);//Danner - o.s. 24092 - 03/02/2010
                    //Fim - Ide

                    //Emit
                    belEmit objemit;
                    objemit = BuscaEmit(sEmp, sForDanfe, sForDanfe, sNota); //Claudinei - o.s. 24222 - 08/03/2010
                    lObjNotas.Add(objemit);//Danner - o.s. 24092 - 03/02/2010
                    //Fim - Emit

                    //dest
                    belDest objdest;
                    objdest = BuscaDest(sEmp, sNota);
                    lObjNotas.Add(objdest);//Danner - o.s. 24092 - 03/02/2010
                    //Fim - dest

                    //Endereço de entrega
                    belEndEnt objendent;
                    objendent = BuscaEndEnt(sEmp, sNota);
                    lObjNotas.Add(objendent);
                    //Fim - Endereço de entrega

                    //Itens da NFe
                    List<belDet> objdet = new List<belDet>();
                    objdet = BuscaItem(sEmp, sNota, objdest);
                    lObjNotas.Add(objdet);

                    //Totais               
                    belTotal objtotal;
                    objtotal = BuscaTotais(sEmp, sNota, objdest);
                    lObjNotas.Add(objtotal);
                    //Fim - Totais

                    //Frete
                    belTransp objtransp;
                    objtransp = BuscaFrete(sEmp, sNota);
                    lObjNotas.Add(objtransp);

                    //Fim - Frete

                    //Duplicatas                
                    belCobr objcob;
                    objcob = BuscaFat(sEmp, sNota);
                    lObjNotas.Add(objcob);
                    //Fim - Duplicatas


                    //OBS
                    belInfAdic objobs;
                    objobs = BuscaObs(sEmp, sNota, objdest, objdet, objbelGeraXml);
                    lObjNotas.Add(objobs);
                    //Fim - OBS

                    lTotNota.Add(lObjNotas);

                    // Update para Gravar o numero da Chave no banco da nota corrente.
                    for (int i = 0; i < 4; i++)
                    {
                        if (GravaNumeroChaveNota(sEmp, sNota, sNFe))
                        {
                            break;
                        }
                    }
                }
                Conn.Close();
            }
            catch (Exception ex)
            {
                Conn.Close();
                throw ex;
            }
        }

        #region BuscaDados

        public belDest BuscaDest(string sEmp,
                                string sNF)
        {
            belDest objdest = new belDest();
            try
            {
                StringBuilder sSql = new StringBuilder();

                //Campos do Select
                sSql.Append("Select ");
                sSql.Append("case when clifor.cd_ufnor <> 'EX' then clifor.cd_cgc else 'EXTERIOR' END CNPJ, ");
                sSql.Append("case when clifor.cd_ufnor <> 'EX' then clifor.cd_cpf else 'EXTERIOR' end CPF, ");
                sSql.Append("clifor.st_pessoaj, ");
                sSql.Append("clifor.nm_clifor xNome, ");
                sSql.Append("clifor.nm_guerra xFant, ");
                sSql.Append("clifor.ds_endnor xlgr, ");
                sSql.Append("clifor.nr_endnor nro, ");
                sSql.Append("clifor.cd_email email, "); // NFe_2.0
                sSql.Append("clifor.nm_bairronor xBairro, ");
                sSql.Append("case when clifor.cd_ufnor <> 'EX' then cidades.cd_municipio else '9999999' END cMun, ");
                sSql.Append("case when clifor.cd_ufnor <> 'EX' then cidades.nm_cidnor else 'EXTERIOR' END xMun, ");
                sSql.Append("clifor.cd_ufnor uf, ");
                sSql.Append("clifor.cd_cepnor cep, ");
                sSql.Append("case when pais.cd_pais is null then ");
                sSql.Append("(select cd_pais from pais where pais.ds_pais = 'BRASIL') ");
                sSql.Append("else ");
                sSql.Append("pais.cd_pais END ");
                sSql.Append(" cPais, ");
                sSql.Append("pais.ds_pais xPais, ");
                sSql.Append("clifor.cd_fonenor fone, ");
                sSql.Append("case when clifor.cd_ufnor <> 'EX' then clifor.cd_insest else 'EXTERIOR' END IE, ");
                sSql.Append("clifor.cd_suframa ");
                //Tabela
                sSql.Append("From nf ");

                //Relacionamentos
                sSql.Append("inner join clifor on (clifor.cd_clifor = nf.cd_clifor) ");
                sSql.Append("left join cidades on (cidades.nm_cidnor = clifor.nm_cidnor) ");
                sSql.Append(" and (cidades.cd_ufnor = clifor.cd_ufnor) ");
                sSql.Append("left join pais on (pais.cd_pais = clifor.cd_pais) ");

                //Where
                sSql.Append("Where ");
                sSql.Append("(nf.cd_empresa ='");
                sSql.Append(sEmp);
                sSql.Append("') ");
                sSql.Append(" and ");
                sSql.Append("(nf.cd_nfseq ='");
                sSql.Append(sNF);
                sSql.Append("') ");

                //belGerarXML BuscaConexao = new belGerarXML();
                //FbConnection Conn = BuscaConexao.Conn;
                //Conn.Open();
                FbCommand cmdDest = new FbCommand(sSql.ToString(), Conn);
                cmdDest.ExecuteNonQuery();
                FbDataReader drDest = cmdDest.ExecuteReader();
                drDest.Read();
                if (drDest["st_pessoaj"].ToString() == "S")
                {
                    objdest.Cnpj = TiraSimbolo(drDest["CNPJ"].ToString(), "");
                }
                else
                {
                    objdest.Cpf = TiraSimbolo(drDest["CPF"].ToString(), "");
                }
                if (drDest["xNome"].ToString() != "")
                {
                    string sNome = TiraSimbolo(drDest["xNome"].ToString().Trim(), "");
                    if (sNome.Length > 60)
                    {
                        sNome = sNome.Substring(0, 60);
                    }
                    objdest.Xnome = sNome;
                }
                if (drDest["xlgr"].ToString() != "")
                {
                    objdest.Xlgr = TiraSimbolo(drDest["xlgr"].ToString(), "");
                }
                if (drDest["email"].ToString() != "")
                {
                    objdest.email = drDest["email"].ToString().Trim(); // NFe_2.0
                }
                if (drDest["nro"].ToString() != "")
                {
                    objdest.Nro = drDest["nro"].ToString();
                }
                if (drDest["xBairro"].ToString() != "")
                {
                    objdest.Xbairro = TiraSimbolo(drDest["xBairro"].ToString(), "");
                }
                if (drDest["cMun"].ToString() != "")
                {
                    objdest.Cmun = drDest["cMun"].ToString();
                }
                if (drDest["cMun"].ToString() != "")
                {
                    objdest.Xmun = TiraSimbolo(drDest["xMun"].ToString(), "");
                }
                if (drDest["uf"].ToString() != "")
                {
                    objdest.Uf = drDest["uf"].ToString();
                }
                if (drDest["cep"].ToString() != "")
                {
                    objdest.Cep = TiraSimbolo(drDest["cep"].ToString(), "");
                }

                if (drDest["cPais"].ToString() != "")
                {
                    objdest.Cpais = drDest["cPais"].ToString();
                }
                if (drDest["xPais"].ToString() != "")
                {
                    objdest.Xpais = drDest["xPais"].ToString();
                }
                if (drDest["fone"].ToString() != "")
                {
                    string sFone = Convert.ToInt64(TiraSimbolo(drDest["fone"].ToString().Replace(" ", ""), "").Replace("[", "").Replace("]", "")).ToString().Trim(); //Claudinei - o.s. 24067 - 29/01/2010
                    if (sFone.Trim().Length > 10)
                    {
                        throw new Exception("Telefone com formato inválido no Destinátário, tamanho maior que 10 caracteres!");
                    }
                    objdest.Fone = sFone;
                }
                if (drDest["IE"].ToString() != "")
                {
                    objdest.Ie = TiraSimbolo(drDest["IE"].ToString(), "");
                }
                else
                {
                    if (drDest["st_pessoaj"].ToString() != "S")
                    {
                        objdest.Ie = "ISENTO";
                    }
                }
                if (drDest["cd_suframa"].ToString() != "")
                {
                    objdest.Isuf = TiraSimbolo(drDest["cd_suframa"].ToString(), "");
                }
            }
            catch (Exception Ex)
            {
                sExecao = " - Problemas na Busca do Destinatário da Nota";
                throw new Exception(Ex.Message + sExecao);
            }
            return objdest;
        }

        public belEndEnt BuscaEndEnt(string sEmp,
                                        string sNF)
        {
            belEndEnt objendent = new belEndEnt();
            try
            {
                StringBuilder sSql = new StringBuilder();

                //Campos do Select
                sSql.Append("Select ");
                sSql.Append("case when endentr.cd_cgcent is not null then ");
                sSql.Append("endentr.cd_cgcent ");
                sSql.Append("else ");
                sSql.Append("nf.cd_cgc ");
                sSql.Append("end CNPJ, ");
                sSql.Append("endentr.ds_endent xLgr, ");
                sSql.Append("endentr.nr_endent nro, ");
                sSql.Append("endentr.nm_bairroent xBairro, ");
                sSql.Append("cidades.cd_municipio cMun, ");
                sSql.Append("cidades.nm_cidnor xMun, ");
                sSql.Append("endentr.cd_ufent UF ");
                //Tabela
                sSql.Append("From nf ");

                //Relacionamentos
                sSql.Append("inner join endentr on (endentr.cd_cliente = nf.cd_clifor) ");
                sSql.Append("and ");
                sSql.Append(" (endentr.cd_endent = nf.cd_endent) ");
                sSql.Append("inner join cidades on (cidades.nm_cidnor = endentr.nm_cident) ");
                sSql.Append("and ");
                sSql.Append("(cidades.cd_ufnor = endentr.cd_ufent) ");

                //Where
                sSql.Append("Where ");
                sSql.Append("(nf.cd_empresa ='");
                sSql.Append(sEmp);
                sSql.Append("') ");
                sSql.Append(" and ");
                sSql.Append("(nf.cd_nfseq ='");
                sSql.Append(sNF);
                sSql.Append("') ");


                //Claudinei - o.s 23507 - 25/05/2009
                //belGerarXML BuscaConexao = new belGerarXML();

                //FbConnection Conn = BuscaConexao.Conn;

                //Conn.Open();

                FbCommand cmdEndent = new FbCommand(sSql.ToString(), Conn);
                cmdEndent.ExecuteNonQuery();



                FbDataReader drEndent = cmdEndent.ExecuteReader();
                while (drEndent.Read())
                {

                    //Montagem do XML
                    //XmlElement noEndent, no;
                    //XmlText noText;
                    //noEndent = Doc.CreateElement("entrega");
                    //noInfNFe.AppendChild(noEndent);

                    if (drEndent["CNPJ"].ToString() != "")
                    {
                        objendent.Cnpj = TiraSimbolo(drEndent["CNPJ"].ToString().PadLeft(14, '0'), "");

                        //no = Doc.CreateElement("CNPJ");
                        //noText = Doc.CreateTextNode(TiraSimbolo(drEndent["CNPJ"].ToString().PadLeft(14,'0'), ""));
                        //no.AppendChild(noText);
                        //noEndent.AppendChild(no);
                    }

                    if (drEndent["xLgr"].ToString() != "")
                    {
                        objendent.Xlgr = TiraSimbolo(drEndent["xLgr"].ToString().Trim(), "");

                        ////no = Doc.CreateElement("xLgr");
                        ////noText = Doc.CreateTextNode(TiraSimbolo(drEndent["xLgr"].ToString().Trim(), ""));
                        ////no.AppendChild(noText);
                        ////noEndent.AppendChild(no);
                    }

                    if (drEndent["nro"].ToString() != "")
                    {
                        objendent.Nro = TiraSimbolo(drEndent["nro"].ToString().Trim(), "");

                        //no = Doc.CreateElement("nro");
                        //noText = Doc.CreateTextNode(TiraSimbolo(drEndent["nro"].ToString().Trim(), ""));
                        //no.AppendChild(noText);
                        //noEndent.AppendChild(no);
                    }

                    if (drEndent["xBairro"].ToString() != "")
                    {
                        objendent.Xbairro = TiraSimbolo(drEndent["xBairro"].ToString().Trim(), "");

                        //no = Doc.CreateElement("xBairro");
                        //noText = Doc.CreateTextNode(TiraSimbolo(drEndent["xBairro"].ToString().Trim(), ""));
                        //no.AppendChild(noText);
                        //noEndent.AppendChild(no);
                    }

                    if (drEndent["cMun"].ToString() != "")
                    {
                        objendent.Cmun = TiraSimbolo(drEndent["cMun"].ToString().Trim(), "");

                        //no = Doc.CreateElement("cMun");
                        //noText = Doc.CreateTextNode(TiraSimbolo(drEndent["cMun"].ToString().Trim(), ""));
                        //no.AppendChild(noText);
                        //noEndent.AppendChild(no);
                    }

                    if (drEndent["xMun"].ToString() != "")
                    {
                        objendent.Xmun = TiraSimbolo(drEndent["xMun"].ToString().Trim(), "");

                        //no = Doc.CreateElement("xMun");
                        //noText = Doc.CreateTextNode(TiraSimbolo(drEndent["xMun"].ToString().Trim(), ""));
                        //no.AppendChild(noText);
                        //noEndent.AppendChild(no);
                    }

                    if (drEndent["UF"].ToString() != "")
                    {
                        objendent.Uf = TiraSimbolo(drEndent["UF"].ToString().Trim(), "");
                        //no = Doc.CreateElement("UF");
                        //noText = Doc.CreateTextNode(TiraSimbolo(drEndent["UF"].ToString().Trim(), ""));
                        //no.AppendChild(noText);
                        //noEndent.AppendChild(no);
                    }
                }
            }
            //Fim - Montagem do XML
            catch (Exception Ex)
            {
                sExecao = " - Problemas na Busca do Endereço de Entrega da Nota";
                throw new Exception(Ex.Message + sExecao);
            }


            return objendent;


        }

        public belEmit BuscaEmit(string sEmp,
                                    string sForDanfe,
                                    string sFormaEmissao,
                                    string sCdNfseq)
        {

            //FbConnection Conn = BuscaConexao.Conn;
            belEmit objemit = new belEmit();


            try
            {
                //Conn.Open();
                StringBuilder sSql = new StringBuilder();

                //Campos do Select
                sSql.Append("Select ");
                sSql.Append("coalesce(empresa.cd_regime_tributacao,0) CRT, "); //NFe_2.0
                sSql.Append("empresa.cd_cgc CNPJ, ");
                sSql.Append("empresa.nm_empresa xNome, ");
                sSql.Append("empresa.nm_guerra xFant, ");
                sSql.Append("empresa.ds_endnor xLgr, ");
                sSql.Append("empresa.ds_endcomp nro, ");
                sSql.Append("empresa.nm_bairronor xBairro, ");
                sSql.Append("cidades.cd_municipio cMun, ");
                sSql.Append("cidades.nm_cidnor xMun, ");
                sSql.Append("empresa.cd_ufnor uf, ");
                sSql.Append("empresa.cd_cepnor cep, ");
                sSql.Append("pais.cd_pais cPais, ");
                sSql.Append("pais.ds_pais xPais, ");
                sSql.Append("empresa.cd_fonenor fone, ");
                sSql.Append("empresa.cd_insest IE ");
                sSql.Append(", (select NF.CD_INSC_SUBSTITUTO from nf where nf.cd_empresa = '");
                sSql.Append(sEmp);
                sSql.Append("' and nf.cd_nfseq = '");
                sSql.Append(sCdNfseq);
                sSql.Append("') IEST ");

                //Tabela
                sSql.Append("From Empresa ");

                //Relacionamentos
                sSql.Append("left join cidades on (cidades.nm_cidnor = empresa.nm_cidnor) ");
                sSql.Append(" and ");
                sSql.Append("(cidades.cd_ufnor = empresa.cd_ufnor) ");
                sSql.Append("left join pais on (pais.cd_pais = empresa.cd_pais) ");

                //Where
                sSql.Append("Where ");
                sSql.Append("(Empresa.cd_empresa ='");
                sSql.Append(sEmp);
                sSql.Append("')");

                FbCommand cmdEmit = new FbCommand(sSql.ToString(), Conn);
                cmdEmit.ExecuteNonQuery();
                FbDataReader drEmit = cmdEmit.ExecuteReader();
                drEmit.Read();

                //Montagem do XML                
                if (drEmit["CNPJ"].ToString() != "")
                {
                    objemit.Cnpj = TiraSimbolo(drEmit["CNPJ"].ToString(), "");
                }
                if (drEmit["xNome"].ToString() != "")
                {
                    objemit.Xnome = TiraSimbolo(drEmit["xNome"].ToString(), "");
                }
                if (drEmit["xFant"].ToString() != "")
                {
                    objemit.Xfant = drEmit["xFant"].ToString();
                }
                if (drEmit["xlgr"].ToString() != "")
                {
                    objemit.Xlgr = TiraSimbolo(drEmit["xLgr"].ToString(), "");
                }
                if (drEmit["nro"].ToString() != "")
                {
                    objemit.Nro = drEmit["nro"].ToString();
                }
                if (drEmit["xBairro"].ToString() != "")
                {
                    objemit.Xbairro = TiraSimbolo(drEmit["xBairro"].ToString(), "");
                }
                else
                {
                    throw new Exception("Bairro do Emitente não esta preenchido !");
                }
                if (drEmit["cMun"].ToString() != "")
                {
                    objemit.Cmun = drEmit["cMun"].ToString();
                }
                if (drEmit["xMun"].ToString() != "")
                {
                    objemit.Xmun = TiraSimbolo(drEmit["xMun"].ToString(), "");
                }
                if (drEmit["uf"].ToString() != "")
                {
                    objemit.Uf = drEmit["uf"].ToString();
                }
                if (drEmit["cep"].ToString() != "")
                {
                    objemit.Cep = TiraSimbolo(drEmit["cep"].ToString(), "");
                }
                if (drEmit["cPais"].ToString() != "")
                {
                    objemit.Cpais = drEmit["cPais"].ToString();
                }
                if (drEmit["xPais"].ToString() != "")
                {
                    objemit.Xpais = drEmit["xPais"].ToString();
                }
                string sFone = Convert.ToInt64(TiraSimbolo(drEmit["fone"].ToString().Replace(" ", ""), "")).ToString().Trim();
                if (sFone != "")
                {
                    objemit.Fone = sFone;
                }
                if (drEmit["IE"].ToString() != "")
                {
                    objemit.Ie = TiraSimbolo(drEmit["IE"].ToString(), "");
                }
                if (drEmit["IEST"].ToString() != "")
                {
                    objemit.Iest = TiraSimbolo(drEmit["IEST"].ToString(), "");
                }
                objemit.CRT = Convert.ToInt16(drEmit["CRT"].ToString());//NFe_2.0
                //Fim - Montagem do XML
            }
            catch (Exception Ex)
            {
                sExecao = " - Problemas na Busca do Emitente da Nota";
                throw new Exception(Ex.Message + sExecao);
            }
            //finally
            //{
            //    Conn.Close();
            //}
            return objemit;
        }

        public belIde BuscaIde(string sEmp,
                                    string sNF,
                                    string sForDanfe,
                                    string sFormaEmissao,
                                    Version versao)
        {
            //belGerarXML BuscaConexao = new belGerarXML();
            // FbConnection Conn = BuscaConexao.Conn;
            belIde objide = new belIde();
            try
            {
                StringBuilder sSql = new StringBuilder();
                //Campos do Select
                sSql.Append("Select First 1 ");
                sSql.Append("coalesce(tpdoc.st_ref_outra_nf,'S') st_ref_outra_nf, ");
                sSql.Append("uf.nr_ufnfe cUF, ");
                sSql.Append("nf.cd_nfseq cNF, ");
                sSql.Append("nf.ds_cfop natop, ");
                sSql.Append("coalesce(PRAZOS.ST_FPAGNFE,0) indPag, ");
                sSql.Append("'55' mod, ");
                sSql.Append("coalesce(nf.cd_serie, 1) serie, ");
                sSql.Append("nf.cd_notafis nNF, ");
                sSql.Append("nf.dt_emi dEmi, ");
                sSql.Append("nf.dt_sainf dSaiEnt, ");
                sSql.Append("case when tpdoc.tp_doc = 'NS' then '1' else '0' end tpNF, ");
                sSql.Append("cidades.cd_municipio cMunFg, ");
                sSql.Append("'1' tpImp, ");
                sSql.Append("'1' tpEmis, ");
                sSql.Append("coalesce(empresa.st_ambiente, '2') tpAmb, ");
                sSql.Append("case when tpdoc.st_nfcompl = 'S' then '2' else '1' end finNFe, ");
                sSql.Append("'0' procEmi, ");
                Globais LeRegWin = new Globais();
                string versionstring = versao.Major + "." + versao.Minor + "." + versao.Build + "." + versao.Revision;
                sSql.Append("'");
                sSql.Append(versionstring);
                sSql.Append("' verProc ");
                //Tabela
                sSql.Append("From NF ");
                //Relacionamentos
                sSql.Append("left join tpdoc on (tpdoc.cd_tipodoc = nf.cd_tipodoc)");
                sSql.Append("inner join empresa on (empresa.cd_empresa = nf.cd_empresa)");
                sSql.Append("left join uf on (uf.cd_uf = empresa.cd_ufnor)");
                sSql.Append("left join cidades on (cidades.nm_cidnor = empresa.nm_cidnor)");
                sSql.Append(" and ");
                sSql.Append("(cidades.cd_ufnor = empresa.cd_ufnor) ");
                sSql.Append("left join  prazos on (prazos.cd_prazo = nf.cd_prazo)");
                //Where
                sSql.Append("Where ");
                sSql.Append("(nf.cd_empresa ='");
                sSql.Append(sEmp);
                sSql.Append("')");
                sSql.Append(" and ");
                sSql.Append("(nf.cd_nfseq = '");
                sSql.Append(sNF);
                sSql.Append("') ");
                // Conn.Open();

                FbCommand cmdIde = new FbCommand(sSql.ToString(), Conn);
                cmdIde.ExecuteNonQuery();
                FbDataReader drIde = cmdIde.ExecuteReader();
                drIde.Read();
                string sNFe = "NFe" + GeraChave(sEmp, sNF, Conn);
                objide = new belIde();
                objide.Cuf = drIde["cUF"].ToString();
                Int32 icNF = Convert.ToInt32(drIde["cNF"].ToString());
                objide.Cnf = icNF.ToString().PadLeft(8, '0'); // NFe_2.0
                string snatop = drIde["natop"].ToString();
                if (snatop.Length > 60)
                {
                    snatop = snatop.Substring(0, 59);
                }
                objide.Natop = TiraSimbolo(snatop, "");
                objide.Indpag = drIde["indPag"].ToString();
                objide.Mod = drIde["mod"].ToString();
                objide.Serie = (bModoSCAN == false ? drIde["serie"].ToString().Replace("U", "1") : iSerieSCAN.ToString());//Diego - OS_24580
                Int32 inNF = Convert.ToInt32(drIde["nNF"].ToString()); //Claudinei - o.s. 23630
                objide.Nnf = inNF.ToString().Trim();
                objide.Demi = System.DateTime.Parse(drIde["dEmi"].ToString());
                if (drIde["dSaiEnt"].ToString() != "")
                {
                    objide.Dsaient = System.DateTime.Parse(drIde["dSaiEnt"].ToString());
                    objide.HSaiEnt = System.DateTime.Parse(drIde["dSaiEnt"].ToString());
                }
                else
                {
                    objide.Dsaient = DateTime.Now;
                    objide.HSaiEnt = DateTime.Now;
                }
                objide.Tpnf = drIde["tpNF"].ToString();
                objide.Cmunfg = TiraSimbolo(drIde["cMunFG"].ToString(), "");
                objide.Tpimp = sForDanfe;
                objide.Tpemis = sFormaEmissao;
                objide.Cdv = sNFe.Substring((sNFe.Length - 1), 1);
                objide.Tpamb = drIde["tpAmb"].ToString();
                objide.Finnfe = drIde["finNFe"].ToString();
                objide.Procemi = drIde["procEmi"].ToString();
                objide.Verproc = drIde["verProc"].ToString();
                objide.bReferenciaNF = (drIde["st_ref_outra_nf"].ToString().Equals("N") ? false : true); //25360
            }
            catch (Exception Ex)
            {
                sExecao = " - Problemas na Identificação da Nota";
                throw new Exception(Ex.Message + sExecao);
            }
            //finally
            //{
            //    Conn.Close();
            //}
            return objide;


        }

        public List<belDet> BuscaItem(string sEmp,
                                   string sNF, belDest objdest)
        {
            // belGerarXML BuscaConexao = new belGerarXML();
            //FbConnection Conn = BuscaConexao.Conn;
            List<belDet> dets = new List<belDet>();
            Globais LeRegWin = new Globais();
            string psNM_Banco = LeRegWin.LeRegConfig("BancoDados");

            bool bAgrupa = VerificaSeAgrupaItens(Conn);

            try
            {
                StringBuilder sSql = new StringBuilder();

                #region Campos do Select

                sSql.Append("Select ");
                sSql.Append("coalesce(opereve.st_servico,'') st_servico,movitem.cd_oper, coalesce(nf.st_soma_dev_tot_nf,'N') st_soma_dev_tot_nf, ");//NFe_2.0
                if ((psNM_Banco.ToUpper().IndexOf("COMERCIOC") == -1) && (psNM_Banco.ToUpper().IndexOf("CERAMICAC") == -1)) //SE FOR INDUSTRIA
                {
                    sSql.Append("coalesce(tpdoc.st_compoe_vl_totprod_nf,'A') st_compoe_vl_totprod_nf, ");
                }

                if (objbelGeraXml.nm_Cliente != "MOGPLAST")
                {
                    sSql.Append("case when empresa.st_codprodnfe = 'C' then ");
                    sSql.Append("produto.cd_prod else ");
                    sSql.Append("produto.cd_alter end cProd, ");
                }
                else
                {
                    sSql.Append("case when empresa.nm_empresa containing 'MOGPLAST' then ");
                    sSql.Append("produto.ds_detalhe ");
                    sSql.Append("else ");
                    sSql.Append("case when empresa.st_codprodnfe = 'C' then ");
                    sSql.Append("movitem.cd_prod else ");
                    sSql.Append("movitem.cd_alter end ");
                    sSql.Append("End cProd, ");
                }
                if (LeRegWin.LeRegConfig("CodBarrasXml") == "True")
                {
                    sSql.Append("produto.cd_barras cEAN, ");
                }
                else
                {
                    sSql.Append("produto.cd_alter cEAN, ");
                }
                if (objbelGeraXml.nm_Cliente != "NAVE_THERM")
                {
                    sSql.Append("movitem.ds_prod xProd, ");
                }
                else
                {
                    sSql.Append("case when produto.ds_prod_compl is not null then ");
                    sSql.Append("substring(produto.ds_prod_compl from 1 for 120) ");
                    sSql.Append("else ");
                    sSql.Append("produto.ds_prod ");
                    sSql.Append("end xProd, ");
                }
                sSql.Append("substring(clas_fis.ds_clasfis from 1 for 8) NCM, ");// Diego - 21/10 Lorenzon
                sSql.Append("movitem.cd_cfop CFOP, ");
                sSql.Append("unidades.cd_unfat uCom, "); // Diego - OS_ 25/08/10
                sSql.Append((bAgrupa == false ? "movitem.qt_prod qCom, " : "sum (movitem.qt_prod) qCom, "));   // Diego - OS_24870  
                sSql.Append((bAgrupa == false ? "movitem.vl_comprimento vl_comprimento, " : "sum (movitem.vl_comprimento) vl_comprimento, "));   // Diego - OS_25550  

                if (objdest.Uf != "EX")
                {
                    sSql.Append("movitem.vl_uniprod vUnCom, ");
                }
                else
                {
                    sSql.Append("(case when movitem.vl_uniprod_ii = 0 then movitem.vl_uniprod else movitem.vl_uniprod_ii end) vUnCom, ");
                }
                if (objdest.Uf == "EX") //DIEGO - OS_24730
                {
                    sSql.Append("(case when movitem.vl_uniprod_ii = 0 then movitem.vl_totbruto else (movitem.vl_uniprod_ii * movitem.qt_prod) end) vProd, ");
                }
                else
                {
                    sSql.Append(bAgrupa == false ? "movitem.vl_totbruto vProd, " : " sum(movitem.vl_totbruto) vProd, ");
                }//DIEGO - OS_24730 - FIM                
                sSql.Append(bAgrupa == false ? "movitem.vl_totliq vl_totliq," : "sum(movitem.vl_totliq) vl_totliq, "); // Diego 0S_24595                

                if (LeRegWin.LeRegConfig("CodBarrasXml") == "True")
                {
                    sSql.Append("produto.cd_barras cEANTrib, ");
                }
                else
                {
                    sSql.Append("produto.cd_alter cEANTrib, ");
                }
                if (bAgrupa)
                {
                    sSql.Append("sum(coalesce(movitem.vl_descsuframa,0) + coalesce(movitem.vl_desccofinssuframa,0) + coalesce(movitem.vl_descpissuframa,0)) vDescSuframa ,");
                }
                else
                {
                    sSql.Append("coalesce(movitem.vl_descsuframa,0) + coalesce(movitem.vl_desccofinssuframa,0) + coalesce(movitem.vl_descpissuframa,0) vDescSuframa ,");
                }
                sSql.Append("coalesce(nf.st_desc,'U')st_desc , ");
                sSql.Append("movitem.cd_tpunid uTrib, ");
                sSql.Append(bAgrupa == false ? "movitem.qt_prod qTrib, " : "sum(movitem.qt_prod) qTrib, ");
                sSql.Append(objdest.Uf != "EX" ? (bAgrupa == false ? "movitem.vl_uniprod vUnTrib, " : "sum(movitem.vl_uniprod) vUnTrib, ") : "movitem.vl_uniprod_ii vUnTrib, "); // DIEGO OS_24730
                sSql.Append("movitem.cd_sittrib CST, ");
                sSql.Append("movitem.vl_alicredicms pCredSN, ");//NFe_2.0
                sSql.Append("movitem.vl_credicms vCredICMSSN, ");//NFe_2.0
                if (objdest.Uf != "EX")
                {
                    sSql.Append(bAgrupa == false ? "" : "sum(");
                    sSql.Append("case when coalesce(tpdoc.st_nfcompl, 'N') = 'N' then ");

                    if (objbelGeraXml.nm_Cliente != "TORCETEX")
                    {
                        sSql.Append("case when coalesce(tpdoc.st_nfcompl, 'N') = 'N' then ");
                        sSql.Append("case when coalesce(nf.st_ipi,'N') = 'N' then ");
                        sSql.Append("CASE when (SELECT SUM(VL_TOTBRUTO) FROM movitem where ((movitem.cd_empresa = nf.cd_empresa) and (MOVITEM.cd_nfseq = NF.cd_nfseq))) > NF.vl_totprod then ");
                        sSql.Append("((case when movitem.vl_redbase <> 0 then ");
                        sSql.Append("case when  coalesce((select first 1 ST_ESTTERC from opereve where ((ST_ESTTERC = 'S') and ((TPDOC.cd_operval) containing cd_oper  ))),'N') = 'N' then ");
                        sSql.Append("(movitem.vl_totliq - ((SELECT SUM(VL_TOTBRUTO) FROM movitem where ((movitem.cd_empresa = nf.cd_empresa) and (MOVITEM.cd_nfseq = NF.cd_nfseq))) - NF.vl_totprod) / ((SELECT COUNT(NR_LANC) FROM movitem where movitem.cd_empresa = nf.cd_empresa and MOVITEM.cd_nfseq = NF.cd_nfseq) )) - (((100-coalesce(movitem.vl_redbase, 100)) * (movitem.vl_totliq - ((SELECT SUM(VL_TOTBRUTO) FROM movitem where ((movitem.cd_empresa = nf.cd_empresa) and (MOVITEM.cd_nfseq = NF.cd_nfseq))) - NF.vl_totprod) / ((SELECT COUNT(NR_LANC) FROM movitem where movitem.cd_empresa = nf.cd_empresa and MOVITEM.cd_nfseq = NF.cd_nfseq) )))/ 100) ");
                        sSql.Append("else ");
                        sSql.Append("movitem.vl_totliq - (((100-coalesce(movitem.vl_redbase, 100)) * movitem.vl_totliq)/ 100) ");
                        sSql.Append("end ");
                        sSql.Append("else ");
                        sSql.Append("movitem.vl_totliq ");
                        sSql.Append("end + (case when (coalesce(tpdoc.ST_FRETE_ENTRA_ICMS_S,'N') <> 'N') AND (UF.VL_ALIICMSFRETE > 0) then  movitem.vl_frete else 0 end))) "); //OS 25385
                        sSql.Append("else ");
                        sSql.Append("((case when movitem.vl_redbase <> 0 then ");
                        sSql.Append("movitem.vl_totliq - (((100-coalesce(movitem.vl_redbase, 100)) * movitem.vl_totliq)/ 100) ");
                        sSql.Append("else ");
                        sSql.Append("movitem.vl_totliq ");
                        sSql.Append("end + (case when (coalesce(tpdoc.ST_FRETE_ENTRA_ICMS_S,'N') <> 'N') AND (UF.VL_ALIICMSFRETE > 0) then  movitem.vl_frete else 0 end))) ");//25385
                        sSql.Append("end ");
                        sSql.Append("else ");
                        sSql.Append("(CASE when (SELECT SUM(VL_TOTBRUTO) FROM movitem where movitem.cd_empresa = nf.cd_empresa and MOVITEM.cd_nfseq = NF.cd_nfseq) > NF.vl_totprod then ");
                        sSql.Append("((case when movitem.vl_redbase <> 0 then ");
                        sSql.Append("(movitem.vl_totliq - ((SELECT SUM(VL_TOTBRUTO) FROM movitem where movitem.cd_empresa = nf.cd_empresa and MOVITEM.cd_nfseq = NF.cd_nfseq) - NF.vl_totprod) / ((SELECT COUNT(NR_LANC) FROM movitem where movitem.cd_empresa = nf.cd_empresa and MOVITEM.cd_nfseq = NF.cd_nfseq) )) - (((100-coalesce(movitem.vl_redbase, 100)) * (movitem.vl_totliq - ((SELECT SUM(VL_TOTBRUTO) FROM movitem where movitem.cd_empresa = nf.cd_empresa and MOVITEM.cd_nfseq = NF.cd_nfseq) - NF.vl_totprod) / ((SELECT COUNT(NR_LANC) FROM movitem where movitem.cd_EMPRESA = NF.CD_EMPRESA AND MOVITEM.cd_nfseq = NF.cd_nfseq) )))/ 100) ");
                        sSql.Append("else ");
                        sSql.Append("movitem.vl_totliq ");
                        sSql.Append("end + ");
                        sSql.Append("(case when (coalesce(tpdoc.ST_FRETE_ENTRA_ICMS_S,'N') <> 'N') AND (UF.VL_ALIICMSFRETE > 0) then  movitem.vl_frete else 0 end))) "); //25385
                        sSql.Append("else ");
                        sSql.Append("((case when movitem.vl_redbase <> 0 then ");
                        sSql.Append("movitem.vl_totliq - (((100-coalesce(movitem.vl_redbase, 100)) * movitem.vl_totliq)/ 100) ");
                        sSql.Append("else ");
                        sSql.Append("movitem.vl_totliq ");
                        sSql.Append("end + (case when (coalesce(tpdoc.ST_FRETE_ENTRA_ICMS_S,'N') <> 'N') AND (UF.VL_ALIICMSFRETE > 0) then  movitem.vl_frete else 0 end))) "); //25385
                        sSql.Append("end) + movitem.vl_ipi ");
                        sSql.Append("end ");
                    }
                    else
                    {
                        sSql.Append("case when coalesce(tpdoc.st_nfcompl, 'N') = 'N' then ");
                        sSql.Append("case when coalesce(nf.st_ipi,'N') = 'N' then ");
                        sSql.Append("((movitem.vl_totliq * movitem.vl_coefdesc) + (case when (coalesce(tpdoc.ST_FRETE_ENTRA_ICMS_S,'N') <> 'N') AND (UF.VL_ALIICMSFRETE > 0) then  movitem.vl_frete else 0 end))  "); //25385
                        sSql.Append("else ");
                        sSql.Append("((movitem.vl_totliq * movitem.vl_coefdesc) + (case when (coalesce(tpdoc.ST_FRETE_ENTRA_ICMS_S,'N') <> 'N') AND (UF.VL_ALIICMSFRETE > 0) then  movitem.vl_frete else 0 end)) + movitem.vl_ipi "); //25385
                        sSql.Append("end ");
                    }

                    sSql.Append("end ");
                    sSql.Append("else ");
                    sSql.Append("nf.vl_baseicm ");
                    sSql.Append("end ");
                    sSql.Append(bAgrupa == false ? "" : ")");
                    sSql.Append("vBC, ");
                }
                else
                {
                    sSql.Append("movitem.vl_baseicms_ii vBC, "); // Diego OS_24730
                }
                sSql.Append("movitem.vl_bicmproprio_subst vBCProp, "); // Diego OS_25278

                sSql.Append("movitem.vl_aliicms pICMS, ");//Diego - OS_24730
                if (objbelGeraXml.nm_Cliente == "PAVAX")
                {
                    if (objdest.Uf == "EX")
                    {
                        sSql.Append("coalesce(MOVITEM.vl_icms_ii,0) vICMS, ");
                    }
                    else
                    {
                        sSql.Append("movitem.vl_icms vICMS, ");
                    }
                }
                else
                {
                    sSql.Append(bAgrupa == false ? "(movitem.vl_icms + movitem.vl_icmproprio_subst)vICMS, " : "sum(movitem.vl_icms + movitem.vl_icmproprio_subst) vICMS, ");
                } //Diego - OS_24730 - FIM
                sSql.Append((bAgrupa == false ? "" : "sum("));
                sSql.Append("case when tpdoc.st_nfcompl = 'N' then ");
                sSql.Append("coalesce(movitem.vl_bicmssubst, 0) ");
                sSql.Append("else ");
                sSql.Append("nf.VL_BICMSSU ");
                sSql.Append("end ");
                sSql.Append((bAgrupa == false ? "" : ")"));
                sSql.Append("vBCST, ");
                //sSql.Append("coalesce(movitem.vl_aliicms_subst, 0) pICMSST, ");
                sSql.Append("coalesce(icm.vl_aliinte, 0) pICMSST, ");
                sSql.Append((bAgrupa == false ? "" : "sum("));
                sSql.Append("case when tpdoc.st_nfcompl = 'N' then ");
                sSql.Append("coalesce(movitem.vl_icmretsubst, 0) ");
                sSql.Append("else ");
                sSql.Append("nf.VL_ICMSSUB ");
                sSql.Append("end ");
                sSql.Append((bAgrupa == false ? "" : ")"));
                sSql.Append("vICMSST, ");
                sSql.Append(bAgrupa == false ? "(100-coalesce(movitem.vl_redbase, 0)) pRedBC, " : "sum((100-coalesce(movitem.vl_redbase, 0))) pRedBC, ");
                sSql.Append("coalesce(icm.vl_alisubs, 0) pMVAST, ");
                sSql.Append("(100-coalesce(movitem.vl_redbase, 0)) pRedBCST, ");
                sSql.Append("coalesce(movitem.vl_aliipi, 0) pIPI, ");
                sSql.Append(bAgrupa == false ? "coalesce(movitem.vl_ipi, 0) vIPI, " : "sum(coalesce(movitem.vl_ipi, 0)) vIPI, ");
                sSql.Append(bAgrupa == false ? "nf.ds_anota infAdProd, " : "'' infAdProd, ");
                sSql.Append("coalesce(clas_fis.st_tributacao, '1') Tributa_ipi, ");
                sSql.Append("tpdoc.tp_doc, ");
                sSql.Append("case when tpdoc.tp_doc = 'NS' then ");
                sSql.Append("opereve.ST_CALCIPI_FA ");
                sSql.Append("else ");
                sSql.Append("opereve.st_ipi ");
                sSql.Append("end Calcula_IPI, ");
                sSql.Append("coalesce(opereve.st_hefrete, 'N') st_hefrete, ");
                sSql.Append("opereve.st_piscofins, ");
                sSql.Append(bAgrupa == false ? "coalesce(movitem.vl_frete, 0) vFrete, " : "sum(coalesce(movitem.vl_frete, 0)) vFrete, ");
                sSql.Append((bAgrupa == false ? "movitem.nr_lanc, " : "'' nr_lanc, "));
                if (objdest.Uf.Equals("EX"))
                {
                    sSql.Append("coalesce(movitem.vl_aliqcofins_cif , 0) vl_aliqcofins_suframa, "); //DIEGO - 24730 - 02/08
                    sSql.Append("coalesce(movitem.VL_ALIQPIS_CIF  , 0) vl_aliqpis_suframa ");
                }
                else
                {
                    sSql.Append("coalesce(empresa.vl_aliqcofins_suframa, 0) vl_aliqcofins_suframa, ");
                    sSql.Append("coalesce(empresa.vl_aliqpis_suframa, 0) vl_aliqpis_suframa ");
                }
                sSql.Append(", endentr.ds_endent xLgr, ");
                sSql.Append("endentr.nr_endent nro, ");
                sSql.Append("endentr.nm_bairroent xBairro, ");
                sSql.Append("endentr.nm_cident cMun, ");
                sSql.Append("endentr.cd_ufent UF ");
                sSql.Append(", listaserv.ds_codigo cListserv, ");
                sSql.Append("coalesce(movitem.vl_aliqserv, 0) vAliqISS, ");
                if (bAgrupa == false)
                {
                    sSql.Append("(movitem.vl_totliq * coalesce(movitem.vl_aliqserv, 0))/100 vIssqn, ");
                }
                else
                {
                    sSql.Append("sum((movitem.vl_totliq * coalesce(movitem.vl_aliqserv, 0))/100) vIssqn, ");
                }
                sSql.Append((bAgrupa == false ? "movitem.vl_totliq vBCISS, " : "sum(movitem.vl_totliq) vBCISS, "));
                sSql.Append("cidades.cd_municipio cMunFG ");
                if (objbelGeraXml.nm_Cliente == "TORCETEX")
                {
                    sSql.Append(", movitem.VL_COEFDESC, ");
                    sSql.Append("movitem.vl_cofins  vl_cofins, ");
                    sSql.Append("movitem.vl_pis vl_pis , ");
                    sSql.Append("((cast((cast((movitem.qt_prod * movitem.vl_uniprod) as numeric(15,4)) * movitem.vl_coefdesc) as numeric(15,2)) + movitem.vl_frete)) vl_basePisCofins ");
                }
                else
                {
                    sSql.Append(", movitem.VL_COEF,  ");
                    if (objdest.Uf.Equals("EX"))  //Diego - 02/08 - 24730
                    {
                        sSql.Append("movitem.VL_COFINS_CIF  vl_cofins, ");
                        sSql.Append("movitem.VL_PIS_CIF vl_pis , ");
                        sSql.Append("coalesce(movitem.VL_BASECOFINS_CIF, 0) vl_basePisCofins ");
                    }
                    else
                    {
                        sSql.Append((bAgrupa == false ? "movitem.vl_cofins  vl_cofins, " : " sum(movitem.vl_cofins)  vl_cofins, "));
                        sSql.Append((bAgrupa == false ? "movitem.vl_pis vl_pis , " : "sum(movitem.vl_pis) vl_pis , "));
                        if (bAgrupa == false)
                        {
                            sSql.Append("((cast((cast((movitem.qt_prod * movitem.vl_uniprod) as numeric(15,2))*movitem.vl_coef) as numeric(15,2)) + movitem.vl_frete)) vl_basePisCofins "); // Diego - 24730 - 02/08                            
                        }
                        else
                        {
                            sSql.Append("sum(((cast((cast((movitem.qt_prod * movitem.vl_uniprod) as numeric(15,2))* movitem.vl_coef) as numeric(15,2)) + movitem.vl_frete))) vl_basePisCofins "); // Diego - 24730 - 02/08
                        }
                    }
                }
                string sBanco = LeRegWin.LeRegConfig("BancoDados");
                if ((sBanco.ToUpper().IndexOf("COMERCIOC") == -1) && (sBanco.ToUpper().IndexOf("CERAMICAC") == -1)) //Claudinei - o.s. - 25/09/2009
                {
                    sSql.Append(", case when empresa.ST_RASTREABILIDADE = '1' ");
                    sSql.Append("then ");
                    sSql.Append("movitem.nr_lote ");
                    sSql.Append("else null ");
                    sSql.Append("end nr_lote ");
                    sSql.Append(", movitem.cd_pedcli, ");
                    sSql.Append("movitem.cd_prodcli ");
                }
                if (objbelGeraXml.nm_Cliente == "MARPA")
                {
                    sSql.Append(", nf.vl_desccomer Desconto_Valor ");
                    sSql.Append(", ((nf.vl_desccomer / nf.vl_totnf)*100) Desconto_Percentual ");
                }
                sSql.Append(", movitem.CD_SITTRIBCOF ");
                sSql.Append(", movitem.CD_SITTRIBIPI ");
                sSql.Append(", movitem.CD_SITTRIBPIS ");
                sSql.Append(", coalesce(movitem.vl_outras,'0') vOutro"); // NFe_2.0
                //sSql.Append(", case when opereve.st_tpoper = '0' then 1 else 0 end indTot "); // NFe_2.0
                //sSql.Append(", coalesce(tpdoc.st_compoe_vl_totprod_nf,'A')st_compoe_vl_totpro "); //NFe_2.0 OS_25346
                sSql.Append(", coalesce(opereve.st_tpoper,'0')st_tpoper ");//NFe_2.0 OS_25346
                sSql.Append(", coalesce(opereve.ST_ESTTERC,'N')ST_ESTTERC ");//NFe_2.0 OS_25346
                sSql.Append(", tpdoc.cd_operval ");
                sSql.Append(", coalesce(Empresa.st_imp_cdpedcli, 'N') st_imp_cdpedcli ");
                sSql.Append(",transpor.nm_trans Redespacho, ");
                sSql.Append("transpor.ds_endnor xLgrRedes, ");
                sSql.Append("transpor.nr_endnor nroRedes, ");
                sSql.Append("transpor.ds_bairronor xBairroRedes, ");
                sSql.Append("transpor.nm_cidnor cmunRedes, ");
                sSql.Append("transpor.cd_ufnor UFRedes ");
                #endregion

                #region Tabelas
                //Tabelas
                sSql.Append("From MOVITEM ");

                //Relacionamentos
                sSql.Append("inner join nf on (nf.cd_empresa = movitem.cd_empresa)");
                sSql.Append(" and ");
                sSql.Append("(nf.cd_nfseq = movitem.cd_nfseq) ");
                sSql.Append("inner join empresa on (empresa.cd_empresa = movitem.cd_empresa) ");
                sSql.Append("inner join unidades on (movitem.cd_tpunid = unidades.cd_tpunid) "); // Diego - OS_ 25/08/10
                sSql.Append("left join clas_fis on (clas_fis.cd_empresa = movitem.cd_empresa)");
                sSql.Append(" and ");
                sSql.Append("(clas_fis.cd_cf = movitem.cd_cf) ");
                sSql.Append("left join icm on (icm.cd_ufnor = nf.cd_ufnor) ");
                sSql.Append("And ");
                sSql.Append("(icm.cd_aliicms = movitem.cd_aliicms) ");
                sSql.Append("left join opereve on (opereve.cd_oper = movitem.cd_oper) ");
                sSql.Append("left join tpdoc on (tpdoc.cd_tipodoc = nf.cd_tipodoc) ");
                sSql.Append("left join produto ");
                sSql.Append("on (produto.cd_empresa = movitem.cd_empresa) ");
                sSql.Append("and ");
                sSql.Append("(produto.cd_prod = movitem.cd_prod) ");
                sSql.Append("left join linhapro ");
                sSql.Append("on (linhapro.cd_empresa = produto.cd_empresa) ");
                sSql.Append("and ");
                sSql.Append("(linhapro.cd_linha = produto.cd_linha) ");
                sSql.Append("left join listaserv ");
                sSql.Append("on (listaserv.nr_lanc = linhapro.nr_lanclistaserv) ");
                sSql.Append("inner join clifor ");
                sSql.Append("on (clifor.cd_clifor = nf.cd_clifor) ");
                sSql.Append("left join cidades ");
                sSql.Append("on (cidades.nm_cidnor = clifor.nm_cidnor) ");
                sSql.Append("and ");
                sSql.Append("(cidades.cd_ufnor = clifor.cd_ufnor) ");
                sSql.Append("inner join uf on (clifor.cd_ufnor = uf.cd_uf) ");//25385
                sSql.Append("left join endentr on (endentr.cd_cliente = nf.cd_clifor) ");
                sSql.Append("and ");
                sSql.Append(" (endentr.cd_endent = nf.cd_endent) ");
                if ((objbelGeraXml.nm_Cliente == "NAVE_THERM") || (objbelGeraXml.nm_Cliente == "MOGPLAST"))
                {
                    sSql.Append("left join produto on (produto.cd_empresa = movitem.cd_empresa) ");
                    sSql.Append("And ");
                    sSql.Append("(produto.cd_prod = movitem.cd_prod)");
                }
                sSql.Append("left join transpor on (transpor.cd_trans = nf.cd_redes) ");
                #endregion

                #region Where
                sSql.Append("Where ");
                sSql.Append("(movitem.cd_empresa ='");
                sSql.Append(sEmp);
                sSql.Append("')");
                sSql.Append(" and ");
                sSql.Append("(nf.cd_nfseq = '");
                sSql.Append(sNF);
                sSql.Append("') ");
                sSql.Append((bAgrupa == false ? "Order by movitem.nr_lanc" : ""));
                #endregion

                #region GrouBy

                if (bAgrupa)
                {
                    sSql.Append(" group by ");
                    sSql.Append("coalesce(opereve.st_servico,''), "); //NFe_2.0
                    sSql.Append("movitem.cd_oper, "); //NFe_2.0
                    sSql.Append("coalesce(nf.st_soma_dev_tot_nf,'N'), ");//NFe_2.0
                    if ((psNM_Banco.ToUpper().IndexOf("COMERCIOC") == -1) && (psNM_Banco.ToUpper().IndexOf("CERAMICAC") == -1)) //SE FOR INDUSTRIA
                    {
                        sSql.Append("coalesce(tpdoc.st_compoe_vl_totprod_nf,'A'), ");  //NFe_2.0
                    }
                    sSql.Append(" case when empresa.st_codprodnfe = 'C' then produto.cd_prod else produto.cd_alter end ");
                    if (LeRegWin.LeRegConfig("CodBarrasXml") == "True")
                    {
                        sSql.Append(" ,produto.cd_barras ");
                    }
                    else
                    {
                        sSql.Append(" ,produto.cd_alter ");
                    }
                    sSql.Append(" ,movitem.ds_prod ");
                    sSql.Append(" ,substring(clas_fis.ds_clasfis from 1 for 8)");
                    sSql.Append(" ,movitem.cd_cfop ");
                    sSql.Append(" ,unidades.cd_unfat ");
                    sSql.Append(" ,movitem.vl_uniprod");
                    if (LeRegWin.LeRegConfig("CodBarrasXml") == "True")
                    {
                        sSql.Append(" ,produto.cd_barras ");
                    }
                    else
                    {
                        sSql.Append(" ,produto.cd_alter ");
                    }
                    sSql.Append(" ,coalesce(nf.st_desc,'U') ");
                    sSql.Append(" ,movitem.cd_tpunid ");
                    sSql.Append(" ,movitem.cd_sittrib ");
                    sSql.Append(" ,movitem.vl_alicredicms "); //NFe_2.0
                    sSql.Append(" ,movitem.vl_credicms "); //NFe_2.0
                    sSql.Append(" ,movitem.vl_bicmproprio_subst ");
                    sSql.Append(" ,movitem.vl_aliicms ");
                    sSql.Append(" ,coalesce(icm.vl_aliinte, 0) ");//NFe_2.0
                    sSql.Append(" ,coalesce(icm.vl_alisubs, 0) ");
                    sSql.Append(" ,movitem.vl_redbase ");
                    sSql.Append(" ,coalesce(movitem.vl_aliipi, 0) ");
                    sSql.Append(" ,coalesce(clas_fis.st_tributacao, '1')");
                    sSql.Append(" ,tpdoc.tp_doc");
                    sSql.Append(" ,case when tpdoc.tp_doc = 'NS' then opereve.ST_CALCIPI_FA else opereve.st_ipi end ");
                    sSql.Append(" ,coalesce(opereve.st_hefrete, 'N')");
                    sSql.Append(" ,opereve.st_piscofins");
                    sSql.Append(" ,coalesce(empresa.vl_aliqcofins_suframa, 0) ");
                    sSql.Append(" ,coalesce(empresa.vl_aliqpis_suframa, 0) ");
                    sSql.Append(" ,endentr.ds_endent ");
                    sSql.Append(" ,endentr.nr_endent ");
                    sSql.Append(" ,endentr.nm_bairroent ");
                    sSql.Append(" ,endentr.nm_cident ");
                    sSql.Append(" ,endentr.cd_ufent");
                    sSql.Append(" ,listaserv.ds_codigo ");
                    sSql.Append(" ,coalesce(movitem.vl_aliqserv, 0) ");
                    sSql.Append(" ,cidades.cd_municipio ");
                    sSql.Append(" ,movitem.VL_COEF");
                    sSql.Append(" ,movitem.CD_SITTRIBCOF ");
                    sSql.Append(" ,movitem.CD_SITTRIBIPI ");
                    sSql.Append(" ,movitem.CD_SITTRIBPIS ");
                    sSql.Append(" ,coalesce(movitem.vl_outras,'0') "); //NFe_2.0
                    //sSql.Append(" ,case when opereve.st_tpoper = '0' then 1 else 0 end ");//NFe_2.0
                    sSql.Append(", coalesce(opereve.st_tpoper,'0')");//NFe_2.0 OS_25346
                    sSql.Append(", coalesce(opereve.ST_ESTTERC,'N')");//NFe_2.0 OS_25346
                    sSql.Append(" ,tpdoc.cd_operval ");
                    sSql.Append(" ,coalesce(Empresa.st_imp_cdpedcli, 'N')");
                    sSql.Append(" ,transpor.nm_trans ");
                    sSql.Append(" ,transpor.ds_endnor ");
                    sSql.Append(" ,transpor.nr_endnor ");
                    sSql.Append(" ,transpor.ds_bairronor ");
                    sSql.Append(" ,transpor.nm_cidnor ");
                    sSql.Append(" ,transpor.cd_ufnor");
                    sSql.Append((bAgrupa == false ? "" : " order by case when empresa.st_codprodnfe = 'C' then produto.cd_prod else produto.cd_alter end"));
                }
                #endregion

                string sNr_Lanc;
                string sql = "select max(nr_lanc) from movitem where (movitem.cd_empresa ='" +
                                                     sEmp +
                                                     "') and " +
                                                     "(movitem.cd_nfseq = '" +
                                                     sNF +
                                                     "') ";
                using (FbCommand cmd = new FbCommand(sql, Conn))
                {
                    //Conn.Open();
                    sNr_Lanc = cmd.ExecuteScalar().ToString();
                    //Conn.Close();
                }
                //Conn.Open();
                FbCommand cmdItem = new FbCommand(sSql.ToString(), Conn);
                cmdItem.ExecuteNonQuery();
                FbDataReader drIItem = cmdItem.ExecuteReader();
                int iSeqItem = 0;
                dTotPis = 0;
                dTotCofins = 0;

                //ITem
                while (drIItem.Read())
                {
                    int indTot = 1;
                    //indTot = (VerificaItemEntraTotalNf(drIItem["st_servico"].ToString(),
                    //                                  drIItem["cd_oper"].ToString(),
                    //                                  drIItem["st_soma_dev_tot_nf"].ToString(),
                    //                                  drIItem["st_compoe_vl_totprod_nf"].ToString(),
                    //                                  Conn) == true ? 1 : 0);

                    //OS_25346 INICIO

                    if ((psNM_Banco.ToUpper().IndexOf("COMERCIOC") == -1) && (psNM_Banco.ToUpper().IndexOf("CERAMICAC") == -1))
                    {
                        if (drIItem["st_compoe_vl_totprod_nf"].ToString().Equals("A")) //Verifica se ambos os produtos vão entrar no total da nota
                        {
                            indTot = 1;
                        }
                        else if (drIItem["st_compoe_vl_totprod_nf"].ToString().Equals("D")) // verifica se movimenta estoque terceiro!! S - SIM / N-NÃO
                        {
                            indTot = (drIItem["ST_ESTTERC"].ToString().Equals("S") ? 1 : 0);
                        }
                        else if (drIItem["st_compoe_vl_totprod_nf"].ToString().Equals("P"))
                        {
                            indTot = (drIItem["st_tpoper"].ToString().Equals("0") ? 1 : 0);  // verifica se representa faturamento!! 0- SIM - 1 -NÃO
                        }
                        //OS_25346 INICIO - FIM 
                    }
                    else
                    {
                        indTot = 1;
                    }

                    belDet objdet = new belDet();
                    belImposto objimp = new belImposto();
                    belProd objprod = new belProd();
                    iSeqItem++;
                    objdet.Nitem = Convert.ToDecimal(iSeqItem.ToString().Trim());
                    objprod.Cprod = TiraSimbolo(drIItem["cProd"].ToString().Trim(), "");
                    objprod.Cean = (Util.Util.IsNumeric(drIItem["cEAN"].ToString()) ? (Util.Util.ValidacEAN(drIItem["cEAN"].ToString()) ? drIItem["cEAN"].ToString() : "00000000") : "00000000");
                    if (objbelGeraXml.nm_Cliente == "ESTACAHC")
                    {
                        objprod.Xprod = drIItem["qCom"].ToString() + "  " + drIItem["xProd"].ToString().Trim();
                    }
                    else
                    {
                        objprod.Xprod = drIItem["xProd"].ToString().Trim();
                    }
                    if (drIItem["NCM"].ToString() != "")
                    {
                        objprod.Ncm = TiraSimbolo(drIItem["NCM"].ToString(), "");
                    }
                    objprod.Cfop = drIItem["CFOP"].ToString();
                    objprod.Ucom = TiraSimbolo(drIItem["uCom"].ToString(), "");
                    if (objbelGeraXml.nm_Cliente.Equals("ESTACAHC"))
                    {
                        decimal dqCom = Math.Round(Convert.ToDecimal(drIItem["qCom"].ToString()) * Convert.ToDecimal(drIItem["vl_coef"].ToString()), 4);
                        objprod.Qcom = dqCom;
                    }
                    else if (objbelGeraXml.nm_Cliente.Equals("MAD_STA_RITA"))
                    {
                        decimal dqCom = Math.Round(Convert.ToDecimal(drIItem["qCom"].ToString()), 4);
                        decimal dComprimento = Math.Round(Convert.ToDecimal(drIItem["vl_comprimento"].ToString()), 4);
                        if (dComprimento == 0)
                        {
                            objprod.Qcom = dqCom;
                        }
                        else
                        {
                            objprod.Qcom = dqCom * dComprimento;
                        }
                    }
                    else
                    {
                        decimal dqCom = Math.Round(Convert.ToDecimal(drIItem["qCom"].ToString()), 4); //Claudinei - o.s. 24248 - 26/03/2010
                        objprod.Qcom = dqCom;
                    }
                    decimal dvUnCom = Math.Round(Convert.ToDecimal(drIItem["vUnCom"].ToString()), 4); //Claudinei - o.s. 24248 - 26/03/2010
                    objprod.Vuncom = dvUnCom;
                    decimal dvProd = 0;
                    decimal vl_prodDesc = 0;
                    decimal vl_desconto = 0;
                    if (objbelGeraXml.nm_Cliente == "ESTACAHC") // Diego - OS_24595
                    {
                        dvProd = Math.Round(Convert.ToDecimal(drIItem["vl_totliq"].ToString()), 2);
                    }
                    else
                    {
                        dvProd = Math.Round(Convert.ToDecimal(drIItem["vProd"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                    }
                    if (objbelGeraXml.nm_Cliente == "TORCETEX")
                    {
                        vl_prodDesc = Math.Round(dvProd * Convert.ToDecimal(drIItem["VL_COEFDESC"].ToString()), 2); //OS_25339 - DIEGO
                    }
                    else
                    {
                        if (drIItem["st_desc"].ToString().Equals("U")) //25368
                        {
                            vl_prodDesc = dvProd; // Math.Round(dvProd * Convert.ToDecimal(drIItem["VL_COEF"].ToString()), 2);
                        }
                        else
                        {
                            vl_prodDesc = Math.Round(dvProd * Convert.ToDecimal(drIItem["VL_COEF"].ToString()), 2);
                        }
                    }
                    pbIndustri = bIndustrializacao(drIItem["cd_operval"].ToString(), Conn);

                    if (VerificaNotaComSuframa(sEmp, sNF, Conn)) //NFe_2.0
                    {
                        vl_desconto = Convert.ToDecimal(drIItem["vDescSuframa"].ToString());
                    }
                    else
                    {
                        vl_desconto = dvProd - vl_prodDesc;
                    }

                    if (vl_desconto == 0)
                    {
                        vl_desconto = BuscaDescTotal(sEmp, sNF, Conn);
                    }
                    if (drIItem["st_hefrete"].ToString() == "S")
                    {
                        dvProd = 0;
                    }
                    if (objbelGeraXml.nm_Cliente == "TORCETEX")//OS_25339 - DIEGO
                    {
                        objprod.Vprod = vl_prodDesc;
                    }
                    else
                    {
                        objprod.Vprod = dvProd;
                    }
                    objprod.Ceantrib = objprod.Cean;
                    objprod.Utrib = TiraSimbolo(drIItem["uTrib"].ToString(), "");
                    objprod.VOutro = Convert.ToDecimal(drIItem["vOutro"].ToString()); // NFe_2.0



                    objprod.IndTot = indTot;//Convert.ToInt16(drIItem["indTot"].ToString()); // NFe_2.0
                    if (objbelGeraXml.nm_Cliente.Equals("ESTACAHC"))
                    {
                        decimal dvqCom = Math.Round(Convert.ToDecimal(drIItem["qCom"].ToString()) * Convert.ToDecimal(drIItem["vl_coef"].ToString()), 4);
                        objprod.Qtrib = dvqCom;
                    }
                    else if (objbelGeraXml.nm_Cliente.Equals("MAD_STA_RITA"))
                    {
                        objprod.Qtrib = objprod.Qcom;
                    }
                    else
                    {
                        decimal dvqCom = Math.Round(Convert.ToDecimal(drIItem["qCom"].ToString()), 4); // o.s. 24248 - 26/03/2010
                        objprod.Qtrib = dvqCom;
                    }
                    decimal dvUnTrib = Math.Round(Convert.ToDecimal(drIItem["vUnTrib"].ToString()), 4); //o.s. 24248 - 26/03/2010
                    objprod.Vuntrib = dvUnTrib;
                    if (drIItem["vFrete"].ToString() != "0")
                    {
                        decimal dvFrete = Math.Round(Convert.ToDecimal(drIItem["vFrete"].ToString()), 2); // o.s. 24248 - 26/03/2010
                        objprod.Vfrete = dvFrete;
                    }
                    if (vl_desconto > 0)
                    {
                        objprod.Vdesc = vl_desconto;
                    }
                    objdet.belProd = objprod;

                    //Impostos

                    #region ICMS
                    belIcms objicms = new belIcms();
                    string sCST = drIItem["CST"].ToString();
                    string sSimplesNac = VerificaEmpresaSimplesNac(sEmp, Conn); // Diego - OS 24918 - 14/09/2010
                    decimal dvBC = 0;
                    decimal dvBCProp = 0; //25278
                    if (sSimplesNac == "N" || sSimplesNac == "")
                    {
                        dvBC = Math.Round(Convert.ToDecimal(drIItem["vBC"].ToString()), 2);
                        dvBCProp = Math.Round(Convert.ToDecimal(drIItem["vBCProp"].ToString()), 2); //25278

                    }
                    decimal dvICMS = Math.Round(Convert.ToDecimal(drIItem["vICMS"].ToString()), 2); //o.s. 24248 - 26/03/2010




                    if (!Util.Util.VerificaNovaST(sCST))
                    {
                        #region CST_ANTIGAS
                        switch (sCST.Substring(1, 2))
                        {
                            case "00":
                                {
                                    #region 00
                                    belIcms00 obj00 = new belIcms00();
                                    obj00.Orig = sCST.ToString().Substring(0, 1);
                                    obj00.Cst = sCST.ToString().Substring(1, 2);
                                    obj00.Modbc = "3";
                                    obj00.Vbc = dvBC;
                                    decimal dpICMS = Math.Round(Convert.ToDecimal(drIItem["pICMS"].ToString()), 2); //o.s. 24248 - 26/03/2010
                                    obj00.Picms = dpICMS;
                                    obj00.Vicms = dvICMS;
                                    objicms.belIcms00 = obj00;
                                    #endregion

                                    break;
                                }
                            case "10":
                                {
                                    #region 010
                                    belIcms10 obj10 = new belIcms10();
                                    obj10.Orig = sCST.ToString().Substring(0, 1);
                                    obj10.Cst = sCST.ToString().Substring(1, 2);
                                    obj10.Modbc = "0";
                                    obj10.Vbc = (dvBCProp == 0 ? (dvICMS == 0 ? 0 : dvBC) : dvBCProp); // 25278
                                    decimal dpICMS = Math.Round(Convert.ToDecimal(drIItem["pICMS"].ToString()), 2);
                                    obj10.Picms = dpICMS;
                                    //dvICMS = (dvBC * dpICMS) / 100;
                                    obj10.Vicms = dvICMS;
                                    obj10.Modbcst = 4;
                                    decimal dpMVAST = Math.Round(Convert.ToDecimal(drIItem["pMVAST"].ToString()), 2);
                                    obj10.Pmvast = dpMVAST;
                                    decimal dvBCST = Math.Round(Convert.ToDecimal(drIItem["vBCST"].ToString()), 2);
                                    obj10.Vbcst = dvBCST;
                                    decimal dpICMSST = Math.Round(Convert.ToDecimal(drIItem["pICMSST"].ToString()), 2);
                                    obj10.Picmsst = dpICMSST;
                                    decimal dvICMSST = Math.Round(Convert.ToDecimal(drIItem["vICMSST"].ToString()), 2);
                                    obj10.Vicmsst = dvICMSST;
                                    objicms.belIcms10 = obj10;
                                    break;
                                    #endregion
                                }
                            case "20":
                                {
                                    #region 020
                                    belIcms20 obj20 = new belIcms20();
                                    obj20.Orig = sCST.ToString().Substring(0, 1);
                                    obj20.Cst = sCST.ToString().Substring(1, 2);
                                    obj20.Modbc = "3";
                                    decimal dpRedBC = Math.Round(Convert.ToDecimal(drIItem["pRedBC"].ToString()), 2); // o.s. 24248 - 26/03/2010
                                    if (dpRedBC == 100)
                                    {
                                        dpRedBC = 0;
                                    }
                                    obj20.Predbc = dpRedBC;
                                    obj20.Vbc = dvBC;
                                    decimal dpICMS = Math.Round(Convert.ToDecimal(drIItem["pICMS"].ToString()), 2); // o.s. 24248 - 26/03/2010
                                    obj20.Picms = dpICMS;
                                    obj20.Vicms = dvICMS;
                                    objicms.belIcms20 = obj20;
                                    break;
                                    #endregion
                                }
                            case "30":
                                {
                                    #region 030
                                    belIcms30 obj30 = new belIcms30();
                                    obj30.Orig = sCST.ToString().Substring(0, 1);
                                    obj30.Cst = sCST.ToString().Substring(1, 2);
                                    obj30.Modbcst = 3;
                                    decimal dpMVAST = Math.Round(Convert.ToDecimal(drIItem["pMVAST"].ToString()), 2); // o.s. 24248 - 26/03/2010
                                    obj30.Pmvast = dpMVAST;
                                    decimal dpRedBCST = Math.Round(Convert.ToDecimal(drIItem["pRedBCST"].ToString()), 2); // o.s. 24248 - 26/03/2010
                                    obj30.Predbcst = dpRedBCST;
                                    decimal dvBCST = Math.Round(Convert.ToDecimal(drIItem["vBCST"].ToString()), 2); // o.s. 24248 - 26/03/2010
                                    obj30.Vbcst = dvBCST;
                                    decimal dpICMSST = Math.Round(Convert.ToDecimal(drIItem["pICMSST"].ToString()), 2); // o.s. 24248 - 26/03/2010
                                    obj30.Picmsst = dpICMSST;
                                    decimal dvICMSST = Math.Round(Convert.ToDecimal(drIItem["vICMSST"].ToString()), 2); // o.s. 24248 - 26/03/2010
                                    obj30.Vicmsst = dvICMSST;
                                    objicms.belIcms30 = obj30;
                                    break;
                                    #endregion
                                }
                            case "40":
                                {
                                    #region 040
                                    belIcms40 obj40 = new belIcms40();
                                    obj40.Orig = sCST.ToString().Substring(0, 1);
                                    obj40.Cst = sCST.ToString().Substring(1, 2);
                                    obj40.Vicms = dvICMS; // NFe_2.0
                                    obj40.motDesICMS = (VerificaNotaComSuframa(sEmp, sNF, Conn) == false ? 9 : 7); // NFe_2.0
                                    dvBC = 0;
                                    objicms.belIcms40 = obj40;
                                    break;
                                    #endregion
                                }
                            case "41":
                                {
                                    #region 041
                                    belIcms40 obj40 = new belIcms40();
                                    obj40.Orig = sCST.ToString().Substring(0, 1);
                                    obj40.Cst = sCST.ToString().Substring(1, 2);
                                    obj40.Vicms = dvICMS; // NFe_2.0
                                    obj40.motDesICMS = (VerificaNotaComSuframa(sEmp, sNF, Conn) == false ? 9 : 7); // NFe_2.0
                                    dvBC = 0;
                                    objicms.belIcms40 = obj40;
                                    break;
                                    #endregion
                                }
                            case "50":
                                {
                                    #region 050
                                    belIcms40 obj40 = new belIcms40();
                                    obj40.Orig = sCST.ToString().Substring(0, 1);
                                    obj40.Cst = sCST.ToString().Substring(1, 2);
                                    obj40.Vicms = dvICMS; // NFe_2.0
                                    obj40.motDesICMS = (VerificaNotaComSuframa(sEmp, sNF, Conn) == false ? 9 : 7); // NFe_2.0
                                    dvBC = 0;
                                    objicms.belIcms40 = obj40;
                                    break;
                                    #endregion
                                }
                            case "51":
                                {
                                    #region 051
                                    belIcms51 obj51 = new belIcms51();
                                    obj51.Orig = sCST.ToString().Substring(0, 1);
                                    obj51.Cst = sCST.ToString().Substring(1, 2);
                                    obj51.Modbc = "3";
                                    decimal dpRedBC = Math.Round(Convert.ToDecimal(drIItem["pRedBC"].ToString()), 2); // o.s. 24248 - 26/03/2010
                                    if (dpRedBC == 100)
                                    {
                                        dpRedBC = 0;
                                    }
                                    obj51.Predbc = dpRedBC;
                                    obj51.Vbc = 0;// Math.Round(Convert.ToDecimal(drIItem["vBC"].ToString()), 2); // DIEGO- OS_24591 - 26/06/2010 INICIO E FIM
                                    decimal dpICMS = Math.Round(Convert.ToDecimal(drIItem["pICMS"].ToString()), 2); // o.s. 24248 - 26/03/2010
                                    obj51.Picms = dpICMS;
                                    obj51.Vicms = dvICMS;
                                    objicms.belIcms51 = obj51;
                                    break;
                                    #endregion
                                }
                            case "60":
                                {
                                    #region 060
                                    belIcms60 obj60 = new belIcms60();
                                    obj60.Orig = sCST.ToString().Substring(0, 1);
                                    obj60.Cst = sCST.ToString().Substring(1, 2);
                                    decimal dvBCST = Math.Round(Convert.ToDecimal(drIItem["vBCST"].ToString()), 2); // o.s. 24248 - 26/03/2010
                                    obj60.Vbcst = dvBCST;
                                    decimal dvICMSST = Math.Round(Convert.ToDecimal(drIItem["vICMSST"].ToString()), 2); // o.s. 24248 - 26/03/2010
                                    obj60.Vicmsst = dvICMSST;
                                    objicms.belIcms60 = obj60;
                                    break;
                                    #endregion
                                }

                            case "70":
                                {
                                    #region 070
                                    belIcms70 obj70 = new belIcms70();
                                    obj70.Orig = sCST.ToString().Substring(0, 1);
                                    obj70.Cst = sCST.ToString().Substring(1, 2);
                                    obj70.Modbc = "3";
                                    decimal dpRedBC = Math.Round(Convert.ToDecimal(drIItem["pRedBC"].ToString()), 2); // o.s. 24248 - 26/03/2010
                                    if (dpRedBC == 100)
                                    {
                                        dpRedBC = 0;
                                    }
                                    obj70.Predbc = dpRedBC;
                                    obj70.Vbc = dvBC;
                                    decimal dpICMS = Math.Round(Convert.ToDecimal(drIItem["pICMS"].ToString()), 2); // o.s. 24248 - 26/03/20103
                                    obj70.Picms = dpICMS;
                                    dvICMS = (dvBC * dpICMS) / 100;
                                    obj70.Vicms = dvICMS;
                                    obj70.Modbcst = 0;
                                    decimal dpMVAST = Math.Round(Convert.ToDecimal(drIItem["pMVAST"].ToString()), 2); // o.s. 24248 - 26/03/2010
                                    obj70.Pmvast = dpMVAST;
                                    decimal dpRedBCST = Math.Round(Convert.ToDecimal(drIItem["pRedBCST"].ToString()), 2); // o.s. 24248 - 26/03/2010
                                    if (dpRedBCST == 100)
                                    {
                                        dpRedBCST = 0;
                                    }
                                    obj70.Predbcst = dpRedBCST;
                                    if (!drIItem["vBCST"].Equals(string.Empty))
                                    {
                                        decimal dvBCST = Math.Round(Convert.ToDecimal(drIItem["vBCST"].ToString()), 2); // o.s. 24248 - 26/03/2010
                                        obj70.Vbcst = dvBCST;
                                    }
                                    if (!drIItem["pICMSST"].Equals(string.Empty))
                                    {
                                        decimal dpICMSST = Math.Round(Convert.ToDecimal(drIItem["pICMSST"].ToString()), 2); // o.s. 24248 - 26/03/2010
                                        obj70.Picmsst = dpICMSST;
                                    }
                                    if (!drIItem["vICMSST"].Equals(string.Empty))
                                    {
                                        decimal dvICMSST = Math.Round(Convert.ToDecimal(drIItem["vICMSST"].ToString()), 2); ;
                                        obj70.Vicmsst = dvICMSST;
                                    }
                                    objicms.belIcms70 = obj70;
                                    break;
                                    #endregion
                                }

                            case "90":
                                {
                                    #region 090
                                    belIcms90 obj90 = new belIcms90();
                                    obj90.Orig = sCST.ToString().Substring(0, 1);
                                    obj90.Cst = sCST.ToString().Substring(1, 2);
                                    obj90.Modbc = "3";
                                    dvBC = 0;
                                    obj90.Vbc = dvBC;
                                    decimal dpRedBC = Math.Round(Convert.ToDecimal(drIItem["pRedBC"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                                    dpRedBC = 0;
                                    if (dpRedBC != 0)
                                    {
                                        obj90.Predbc = dpRedBC;
                                    }
                                    decimal dpICMS = Math.Round(Convert.ToDecimal(drIItem["pICMS"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                                    dpICMS = 0;
                                    obj90.Picms = dpICMS;

                                    dvICMS = 0;
                                    obj90.Vicms = dvICMS;
                                    obj90.Modbcst = 3;
                                    if (drIItem["pMVAST"].ToString() != "0")
                                    {
                                        decimal dpMVAST = Math.Round(Convert.ToDecimal(drIItem["pMVAST"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                                        dpMVAST = 0;
                                        obj90.Pmvast = dpMVAST;
                                    }
                                    decimal dpRedBCST = Math.Round(Convert.ToDecimal(drIItem["pRedBCST"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                                    dpRedBCST = 0;
                                    if (dpRedBCST != 0)
                                    {
                                        obj90.Predbcst = dpRedBCST;
                                    }
                                    decimal dvBCST = Math.Round(Convert.ToDecimal(drIItem["vBCST"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                                    dvBCST = 0;
                                    obj90.Vbcst = dvICMS;
                                    decimal dpICMSST = Math.Round(Convert.ToDecimal(drIItem["pICMSST"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                                    dpICMSST = 0;
                                    obj90.Picmsst = dpICMSST;
                                    decimal dvICMSST = Math.Round(Convert.ToDecimal(drIItem["vICMSST"].ToString()), 2);
                                    dvICMSST = 0;
                                    obj90.Vicmsst = dvICMSST;
                                    objicms.belIcms90 = obj90;
                                    break;
                                    #endregion
                                }
                        }

                        #endregion
                    }
                    else
                    {
                        #region CTS_NOVAS
                        switch ((Util.Util.RetornaSTnovaAserUsada(sCST)))
                        {
                            case "101":
                                {
                                    #region 101
                                    belICMSSN101 obj101 = new belICMSSN101();
                                    obj101.orig = (objdest.Uf.Equals("EX") ? "1" : "0");
                                    obj101.CSOSN = sCST.ToString();
                                    obj101.pCredSN = Math.Round(Convert.ToDecimal(drIItem["pCredSN"].ToString()), 2);//NFe_2.0
                                    obj101.vCredICMSSN = Math.Round(Convert.ToDecimal(drIItem["vCredICMSSN"].ToString()), 2); //NFe_2.0
                                    objicms.belICMSSN101 = obj101;
                                    #endregion
                                }
                                break;

                            case "102":
                                {
                                    #region 102
                                    belICMSSN102 obj102 = new belICMSSN102();
                                    obj102.orig = (objdest.Uf.Equals("EX") ? "1" : "0");
                                    obj102.CSOSN = sCST.ToString();
                                    objicms.belICMSSN102 = obj102;
                                    #endregion

                                }
                                break;
                            case "201":
                                {
                                    #region 201
                                    belICMSSN201 obj201 = new belICMSSN201();
                                    decimal dpRedBCST = Math.Round(Convert.ToDecimal(drIItem["pRedBCST"].ToString()), 2);
                                    decimal dpICMSST = Math.Round(Convert.ToDecimal(drIItem["pICMSST"].ToString()), 2);
                                    decimal dvICMSST = Math.Round(Convert.ToDecimal(drIItem["vICMSST"].ToString()), 2);

                                    obj201.orig = (objdest.Uf.Equals("EX") ? "1" : "0");
                                    obj201.CSOSN = sCST.ToString();
                                    obj201.modBCST = 3;
                                    obj201.pMVAST = Math.Round(Convert.ToDecimal(drIItem["pMVAST"].ToString()), 2);
                                    obj201.vBCST = Math.Round(Convert.ToDecimal(drIItem["vBCST"].ToString()), 2);
                                    obj201.pICMSST = dpICMSST;
                                    obj201.vICMSST = dvICMSST;
                                    obj201.pCredSN = Math.Round(Convert.ToDecimal(drIItem["pCredSN"].ToString()), 2);//NFe_2.0
                                    obj201.vCredICMSSN = Math.Round(Convert.ToDecimal(drIItem["vCredICMSSN"].ToString()), 2); //NFe_2.0                                   
                                    if (dpRedBCST != 0)
                                    {
                                        obj201.pRedBCST = dpRedBCST;
                                    }
                                    objicms.belICMSSN201 = obj201;
                                    #endregion
                                }
                                break;
                            case "500":
                                {
                                    #region 500
                                    belICMSSN500 obj500 = new belICMSSN500();
                                    obj500.orig = (objdest.Uf.Equals("EX") ? "1" : "0");
                                    obj500.CSOSN = sCST.ToString();
                                    decimal dvBCSTRet = Math.Round(Convert.ToDecimal(drIItem["vBCST"].ToString()), 2);
                                    decimal dvICMSSTRet = Math.Round(Convert.ToDecimal(drIItem["vICMSST"].ToString()), 2);
                                    obj500.vBCSTRet = dvBCSTRet;
                                    obj500.vICMSSTRet = dvICMSSTRet;
                                    objicms.belICMSSN500 = obj500;
                                    #endregion
                                }
                                break;
                            case "900":
                                {
                                    #region 900
                                    belICMSSN900 obj900 = new belICMSSN900();
                                    decimal dpRedBCST = Math.Round(Convert.ToDecimal(drIItem["pRedBCST"].ToString()), 2);
                                    decimal dpICMSST = Math.Round(Convert.ToDecimal(drIItem["pICMSST"].ToString()), 2);
                                    decimal dvICMSST = Math.Round(Convert.ToDecimal(drIItem["vICMSST"].ToString()), 2);
                                    decimal dvBCSTRet = Math.Round(Convert.ToDecimal(drIItem["vBCST"].ToString()), 2);
                                    decimal dvICMSSTRet = Math.Round(Convert.ToDecimal(drIItem["vICMSST"].ToString()), 2);

                                    obj900.orig = (objdest.Uf.Equals("EX") ? "1" : "0");
                                    obj900.CSOSN = sCST.ToString();
                                    obj900.modBC = 3;
                                    obj900.vBC = dvBC;
                                    decimal dpRedBC = Math.Round(Convert.ToDecimal(drIItem["pRedBC"].ToString()), 2);
                                    if (dpRedBC != 0)
                                    {
                                        obj900.pRedBC = dpRedBC;
                                    }
                                    obj900.vICMS = dvICMS;
                                    obj900.modBCST = 3;
                                    obj900.pMVAST = Math.Round(Convert.ToDecimal(drIItem["pMVAST"].ToString()), 2);
                                    if (dpRedBCST != 0)
                                    {
                                        obj900.pRedBCST = dpRedBCST;
                                    }
                                    decimal dvBCST = Math.Round(Convert.ToDecimal(drIItem["vBCST"].ToString()), 2);
                                    obj900.vBCST = dvBCST;
                                    obj900.pICMSST = dpICMSST;
                                    obj900.vICMSST = dvICMSST;
                                    obj900.vBCSTRet = dvBCSTRet;
                                    obj900.vICMSSTRet = dvICMSSTRet;
                                    obj900.pCredSN = Math.Round(Convert.ToDecimal(drIItem["pCredSN"].ToString()), 2);//NFe_2.0
                                    obj900.vCredICMSSN = Math.Round(Convert.ToDecimal(drIItem["vCredICMSSN"].ToString()), 2); //NFe_2.0                                    

                                    // Alteração feita por motivo de NFe para a Lorenzon

                                    obj900.modBC = null;
                                    obj900.vBC = null;
                                    obj900.pRedBC = null;
                                    obj900.pICMS = null;
                                    obj900.vICMS = null;
                                    obj900.modBCST = null;
                                    obj900.pMVAST = null;
                                    obj900.pRedBCST = null;
                                    obj900.vBCST = null;
                                    obj900.pICMSST = null;
                                    obj900.vICMSST = null;
                                    obj900.vBCSTRet = null;
                                    obj900.vICMSSTRet = null;
                                    obj900.pCredSN = null;//NFe_2.0
                                    obj900.vCredICMSSN = null; //NFe_2.0                                    

                                    objicms.belICMSSN900 = obj900;
                                    #endregion
                                }
                                break;
                        }
                        #endregion
                    }

                    if ((dvBC != 0) && (Convert.ToDecimal(drIItem["pICMS"].ToString()) != 0))
                    {
                        dTotbaseICMS += dvBC;
                        dTotValorICMS += dvICMS;
                    }
                    objimp.belIcms = objicms;
                    #endregion

                    #region IPI
                    belIpi objipi = new belIpi();
                    if (drIItem["CD_SITTRIBIPI"].ToString() == "")
                        throw new Exception("Situação Tributária do IPI está vazia na NF");

                    string sTributaIPI = drIItem["cd_sittribipi"].ToString().PadLeft(2, '0');
                    if ((sTributaIPI == "49") || (sTributaIPI == "00") || (sTributaIPI == "50") || (sTributaIPI == "99"))
                    {
                        belIpitrib objipitrib = new belIpitrib();
                        objipi.Cenq = "999";
                        objipitrib.Cst = sTributaIPI;
                        if (!drIItem["vBC"].Equals(string.Empty))
                        {
                            if (objdest.Uf.Equals("EX"))
                            {
                                decimal ddvBC = Math.Round((Convert.ToDecimal(drIItem["vUnCom"]) + Convert.ToDecimal(drIItem["pIPI"])) * (Convert.ToDecimal(drIItem["qCom"])), 2);  // Diego - 24730 - 02/08/10
                                objipitrib.Vbc = ddvBC;
                            }
                            else
                            {
                                decimal ddvBC = Math.Round(Convert.ToDecimal(drIItem["vBC"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                                objipitrib.Vbc = ddvBC;
                            }
                        }
                        if (!drIItem["pIPI"].Equals(string.Empty))
                        {
                            decimal dpIPI = Math.Round(Convert.ToDecimal(drIItem["pIPI"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                            objipitrib.Pipi = dpIPI;
                        }
                        if (!drIItem["vIPI"].Equals(string.Empty))
                        {
                            decimal dvIPI = Math.Round(Convert.ToDecimal(drIItem["vIPI"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                            objipitrib.Vipi = dvIPI;
                        }
                        objipi.belIpitrib = objipitrib;
                    }
                    else
                    {
                        belIpint objipint = new belIpint();
                        objipi.Cenq = "999";
                        objipint.Cst = sTributaIPI;
                        objipi.belIpint = objipint;
                    }
                    objimp.belIpi = objipi;
                    #endregion

                    #region II
                    //Imposto de importação
                    belIi objii = new belIi();
                    objii.Vbc = 0;
                    objii.Vdespadu = 0;
                    objii.Vii = 0;
                    objii.Viof = 0;
                    objimp.belIi = objii;
                    //Fim - II                    
                    #endregion

                    #region PIS

                    if (drIItem["CD_SITTRIBPIS"].ToString() == "")
                    {
                        throw new Exception("Situação Tributária do PIS está vazia na NF");
                    }
                    string sCst = drIItem["CD_SITTRIBPIS"].ToString().PadLeft(2, '0'); //o.s. 23672 - 10/09/2009
                    //PIS
                    belPis objpis = new belPis();
                    if ((drIItem["st_piscofins"].ToString() == "S") && (Convert.ToInt16(sCst) < 4))
                    {
                        belPisaliq objpisaliq = new belPisaliq();
                        objpisaliq.Cst = sCst;
                        decimal dvlBasepis = Math.Round(Convert.ToDecimal(drIItem["vl_basePisCofins"].ToString()), 2);
                        objpisaliq.Vbc = (objpisaliq.Cst.ToString().Equals("99") ? 0 : dvlBasepis); //24872
                        decimal dpPis = Math.Round(Convert.ToDecimal(drIItem["vl_aliqpis_suframa"].ToString()), 2);
                        objpisaliq.Ppis = (objpisaliq.Cst.ToString().Equals("99") ? 0 : dpPis);
                        decimal dvPIS = Math.Round(Convert.ToDecimal(drIItem["vl_pis"].ToString()), 2);
                        dTotPis += dvPIS;
                        objpisaliq.Vpis = (objpisaliq.Cst.ToString().Equals("99") ? 0 : dvPIS);
                        objpis.belPisaliq = objpisaliq;
                    }
                    else if (Convert.ToInt16(sCst) == 99)
                    {
                        // Diego - OS_24585 - 25/06/2010
                        sSimplesNac = VerificaEmpresaSimplesNac(sEmp, Conn);
                        belPisoutr objpisoutr = new belPisoutr();
                        objpisoutr.Cst = sCst;
                        objpisoutr.Vbc = 0;
                        objpisoutr.Ppis = 0;
                        dTotPis += 0;
                        objpisoutr.Vpis = 0;
                        objpis.belPisoutr = objpisoutr;
                        // Diego - OS_24585 - 25/06/2010 - FIM
                    }
                    else
                    {
                        belPisnt objpisnt = new belPisnt();
                        objpisnt.Cst = sCst;
                        objpis.belPisnt = objpisnt;
                    }
                    objimp.belPis = objpis;
                    //Fim PIS

                    #endregion

                    #region Cofins
                    //Cofins
                    if (drIItem["cd_sittribcof"].ToString() == "")
                    {
                        throw new Exception("Situação Tributária do COFINS está vazia na NF");
                    }
                    belCofins objcofins = new belCofins();
                    if ((drIItem["st_piscofins"].ToString() == "S") && (Convert.ToInt16(drIItem["cd_sittribcof"].ToString()) < 4))
                    {
                        belCofinsaliq objcofinsaliq = new belCofinsaliq();
                        objcofinsaliq.Cst = drIItem["cd_sittribcof"].ToString().PadLeft(2, '0');
                        decimal dvlBaseCofins = Math.Round(Convert.ToDecimal(drIItem["vl_basePisCofins"].ToString()), 2); //o.s. 24248 - 26/03/2010
                        objcofinsaliq.Vbc = dvlBaseCofins;
                        decimal dpCofins = Math.Round(Convert.ToDecimal(drIItem["vl_aliqcofins_suframa"].ToString()), 2); //o.s. 24248 - 26/03/2010
                        objcofinsaliq.Pcofins = dpCofins;
                        decimal dvCofins = Math.Round(Convert.ToDecimal(drIItem["vl_cofins"].ToString()), 2); //o.s. 24248 - 26/03/2010
                        objcofinsaliq.Vcofins = dvCofins;
                        objcofins.belCofinsaliq = objcofinsaliq;
                    }
                    else if (Convert.ToInt16(drIItem["cd_sittribcof"].ToString()) == 99)
                    {
                        belCofinsoutr objcofinsoutr = new belCofinsoutr();
                        objcofinsoutr.Cst = drIItem["cd_sittribcof"].ToString().PadLeft(2, '0');
                        objcofinsoutr.Vbc = 0;
                        objcofinsoutr.Pcofins = 0;
                        dTotCofins += 0;
                        objcofinsoutr.Vcofins = dTotCofins;
                        objcofins.belCofinsoutr = objcofinsoutr;
                    } // Diego - OS_24585 - 25/06/2010 - FIM
                    else
                    {
                        belCofinsnt objcofinsnt = new belCofinsnt();
                        objcofinsnt.Cst = drIItem["cd_sittribcof"].ToString().PadLeft(2, '0');
                        objcofins.belCofinsnt = objcofinsnt;
                    }
                    objimp.belCofins = objcofins;
                    //Fim - Cofins                    
                    #endregion

                    #region ISS
                    if ((drIItem["vAliqISS"].ToString() != "") && (drIItem["vAliqISS"].ToString() != "0"))
                    {
                        belIss objiss = new belIss();
                        decimal dvBCISS = Math.Round(Convert.ToDecimal(drIItem["vBCISS"].ToString()), 2); //o.s. 24248 - 26/03/2010
                        dTotServ += dvBCISS;
                        dTotBCISS = dTotServ;
                        objiss.Vbc = dvBCISS;
                        decimal dvAliqISS = Math.Round(Convert.ToDecimal(drIItem["vAliqISS"].ToString()), 2); //o.s. 24248 - 26/03/2010
                        objiss.Valiq = dvAliqISS;
                        decimal dvISSQN = Convert.ToDecimal(drIItem["vIssqn"].ToString());
                        dTotISS += dvISSQN;
                        dTotPisISS += Math.Round(Convert.ToDecimal(drIItem["vl_pis"].ToString()), 2); //o.s. 24248 - 26/03/2010
                        dTotCofinsISS += Math.Round(Convert.ToDecimal(drIItem["vl_cofins"].ToString()), 2);  //o.s. 24248 - 26/03/2010
                        objiss.Vissqn = dvISSQN;
                        objiss.Cmunfg = drIItem["cMunFG"].ToString();
                        if (drIItem["cListserv"].ToString() != "")
                        {
                            Int64 icListServ = Convert.ToInt64(drIItem["cListserv"].ToString());
                            objiss.Clistserv = icListServ;
                        }
                        objimp.belIss = objiss;
                    }
                    #endregion

                    #region Obs
                    //Obs
                    belInfadprod objinf = new belInfadprod();
                    string sObsItem = "";
                    if (objbelGeraXml.nm_Cliente == "HELENGE")
                    {
                        sObsItem += (BuscaContratoOBS(sEmp, drIItem["nr_lanc"].ToString(), Conn)).Replace(Environment.NewLine, "-");
                    }
                    if (objbelGeraXml.nm_Cliente == "FORMINGP")                    //Diego - O.S 24028 - 22/01/2010
                    {
                        sObsItem += BuscaSerieProd(sEmp, drIItem["nr_lanc"].ToString(), Conn);
                    }//Fim - Diego - O.S 24028 - 22/01/2010

                    sObsItem = BuscaObsItemSimples(sEmp, drIItem["nr_lanc"].ToString(), Conn) + sObsItem; //17/11/2010


                    if ((sBanco.ToUpper().IndexOf("COMERCIOC") == -1) && (sBanco.ToUpper().IndexOf("CERAMICAC") == -1))
                    {
                        if (drIItem["st_imp_cdpedcli"].ToString() != "N") //Claudinei - o.s. sem - 21/12/2009
                        {
                            if (drIItem["nr_lote"].ToString() != "")
                            {
                                sObsItem = string.Format("Lote: {0}", drIItem["nr_lote"].ToString());
                            }
                            if (drIItem["cd_pedcli"].ToString() != "")
                            {
                                if (sObsItem == "")
                                {
                                    sObsItem += string.Format("SEU PEDIDO.: {0}",
                                                              drIItem["cd_pedcli"].ToString().Trim());
                                }
                                else
                                {
                                    sObsItem += string.Format(" SEU PEDIDO.: {0}",
                                                              drIItem["cd_pedcli"].ToString().Trim());
                                }
                            }

                            if (drIItem["cd_prodcli"].ToString() != "")
                            {
                                if (sObsItem == "")
                                {
                                    sObsItem += string.Format("PRD_CLI.: {0}",
                                                              drIItem["cd_prodcli"].ToString().Trim());

                                }
                                else
                                {
                                    sObsItem += string.Format(" PRD_CLI.: {0}",
                                                              drIItem["cd_prodcli"].ToString().Trim());
                                }
                            }

                        }
                    }

                    if (objbelGeraXml.nm_Cliente == "MARPA")
                    {
                        sObsItem = MontaObsItem(sEmp, drIItem["nr_lanc"].ToString(), Conn);
                        if (sObsItem != "")
                        {
                            if (drIItem["nr_lanc"].ToString() == sNr_Lanc)
                            {
                                if (drIItem["xLgr"].ToString().Trim() != "")
                                {
                                    sObsItem += string.Format(" - Endereco de Entrega.: {0} {1} - Bairro.: {2} - Cidade.: {3} - UF.: {4} ",
                                                              TiraSimbolo(drIItem["xLgr"].ToString().Trim(), ""),
                                                              TiraSimbolo(drIItem["nro"].ToString().Trim(), ""),
                                                              TiraSimbolo(drIItem["xBairro"].ToString().Trim(), ""),
                                                              RetiraCaracterEsquerda(TiraSimbolo(drIItem["cMun"].ToString().Trim(), ""), '0'),
                                                              TiraSimbolo(drIItem["UF"].ToString().Trim(), ""));
                                }
                                if (drIItem["Desconto_Valor"].ToString() != "0")
                                {
                                    decimal dDesconto_Valor = Convert.ToDecimal(drIItem["Desconto_Valor"].ToString());
                                    decimal dDesconto_Percentual = (Convert.ToDecimal(drIItem["Desconto_Percentual"].ToString()) / 100);

                                    sObsItem += string.Format(" - Desconto.: ({0:p2}) {1:f2}",
                                                              dDesconto_Percentual,
                                                              dDesconto_Valor);


                                }
                            }
                            objinf.Infadprid = TiraSimbolo(sObsItem.Trim(), "");
                        }
                        else
                        {
                            if (drIItem["nr_lanc"].ToString() == sNr_Lanc)
                            {
                                if (drIItem["Desconto_Valor"].ToString() != "0")
                                {
                                    decimal dDesconto_Valor = Convert.ToDecimal(drIItem["Desconto_Valor"].ToString());
                                    decimal dDesconto_Percentual = (Convert.ToDecimal(drIItem["Desconto_Percentual"].ToString()) / 100);

                                    sObsItem = string.Format("Desconto.: ({0:p2}) {1:f2}",
                                                              dDesconto_Percentual,
                                                              dDesconto_Valor);
                                    objinf.Infadprid = TiraSimbolo(sObsItem.Trim(), "");
                                }
                            }
                        }
                    }
                    else
                    {
                        if (drIItem["nr_lanc"].ToString() == sNr_Lanc)
                        {
                            if (drIItem["xLgr"].ToString().Trim() != "")
                            {
                                if (sObsItem == "")
                                {

                                    sObsItem = string.Format("Endereco de Entrega.: {0}, {1} - Bairro.: {2} - Cidade.: {3} - UF.: {4} ",
                                                             drIItem["xLgr"].ToString().Trim(),
                                                             drIItem["nro"].ToString().Trim(),
                                                             drIItem["xBairro"].ToString().Trim(),
                                                             RetiraCaracterEsquerda(drIItem["cMun"].ToString().Trim(), '0'),
                                                             drIItem["UF"].ToString().Trim());
                                }
                                else
                                {
                                    sObsItem += string.Format(" - Endereco de Entrega.: {0}, {1} - Bairro.: {2} - Cidade.: {3} - UF.: {4} ",
                                                             drIItem["xLgr"].ToString().Trim(),
                                                             drIItem["nro"].ToString().Trim(),
                                                             drIItem["xBairro"].ToString().Trim(),
                                                             RetiraCaracterEsquerda(drIItem["cMun"].ToString().Trim(), '0'),
                                                             drIItem["UF"].ToString().Trim());

                                }
                                if (sObsItem != "")
                                {
                                    objinf.Infadprid = TiraSimbolo(sObsItem.Trim(), "");
                                }
                            }
                            else
                            {
                                if (sObsItem != "")
                                {
                                    objinf.Infadprid = TiraSimbolo(sObsItem.Trim(), "").Replace(Environment.NewLine, "-");
                                }
                            }
                        }
                        else
                        {
                            if (sObsItem != "")
                            {
                                objinf.Infadprid = TiraSimbolo(sObsItem.Trim(), "");
                            }

                        }
                    }
                    if (sObsItem != "")
                    {
                        if (drIItem["nr_lanc"].ToString() == sNr_Lanc)
                        {
                            if (drIItem["xLgrRedes"].ToString().Trim() != "")
                            {
                                sObsItem += string.Format(" - Redespacho.: {5} - {0} {1} - Bairro.: {2} - Cidade.: {3} - UF.: {4} ",
                                                          TiraSimbolo(drIItem["xLgrRedes"].ToString().Trim(), ""),
                                                          TiraSimbolo(drIItem["nroRedes"].ToString().Trim(), ""),
                                                          TiraSimbolo(drIItem["xBairroRedes"].ToString().Trim(), ""),
                                                          RetiraCaracterEsquerda(TiraSimbolo(drIItem["cmunRedes"].ToString().Trim(), ""), '0'),
                                                          TiraSimbolo(drIItem["UFRedes"].ToString().Trim(), ""),
                                                          TiraSimbolo(drIItem["redespacho"].ToString().Trim(), ""));
                                objinf.Infadprid = TiraSimbolo(sObsItem.Trim(), "-");
                            }
                        }
                    }
                    else
                    {
                        if (drIItem["nr_lanc"].ToString() == sNr_Lanc)
                        {
                            if ((drIItem["xLgrRedes"].ToString().Trim() != "-") && (drIItem["xLgrRedes"].ToString().Trim() != "")) //Claudinei - o.s. sem - 01/03/2010
                            {
                                sObsItem += string.Format("Redespacho.: {5} - {0} {1} - Bairro.: {2} - Cidade.: {3} - UF.: {4} ",
                                                          TiraSimbolo(drIItem["xLgrRedes"].ToString().Trim(), ""),
                                                          TiraSimbolo(drIItem["nroRedes"].ToString().Trim(), ""),
                                                          TiraSimbolo(drIItem["xBairroRedes"].ToString().Trim(), ""),
                                                          RetiraCaracterEsquerda(TiraSimbolo(drIItem["cmunRedes"].ToString().Trim(), ""), '0'),
                                                          TiraSimbolo(drIItem["UFRedes"].ToString().Trim(), ""),
                                                          TiraSimbolo(drIItem["redespacho"].ToString().Trim(), ""));
                                objinf.Infadprid = TiraSimbolo(sObsItem.Trim(), "-");
                            }
                        }
                    }
                    if (objinf.Infadprid != null)
                    {
                        if (objinf.Infadprid.Length > 500)
                        {
                            objinf.Infadprid = objinf.Infadprid.Substring(0, 500);
                        }
                    }
                    //Fim - Obs                    
                    #endregion

                    objdet.belImposto = objimp;
                    objdet.belInfadprod = objinf;
                    dets.Add(objdet);
                    //Fim - Impostos
                }
            }
            catch (Exception Ex)
            {
                sExecao = " - Problemas ao tentar gerar os Itens da Nota de Seq.: " + sNF;  //OS 24738
                throw new Exception(Ex.Message + sExecao);
            }
            //finally
            //{
            //    Conn.Close();
            //}
            return dets;
        }

        public belTotal BuscaTotais(string sEmp,
                                    string sNF, belDest objdest)
        {
            //belGerarXML BuscaConexao = new belGerarXML();
            //FbConnection Conn = BuscaConexao.Conn;
            belTotal objtot = new belTotal();


            try
            {
                //Conn.Open();
                //Claudinei - o.s. 24352 - 09/04/2010                
                string sStImpICMS_Em_Subst_NF = BuscaSTICMSEMSUBSTNF(sEmp, sNF, Conn);
                //Fim - Claudinei - o.s. 24352 - 09/04/2010
                string nm_Cliente = string.Empty;
                using (FbCommand cmd = new FbCommand("select control.cd_conteud from control where control.cd_nivel = '0016'", Conn))
                {
                    nm_Cliente = Convert.ToString(cmd.ExecuteScalar()).Trim();
                }
                StringBuilder sSql = new StringBuilder();

                //Campos do Select
                sSql.Append("Select ");
                sSql.Append("First 1 ");

                if ((nm_Cliente == "PAVAX") && (objdest.Uf.Equals("EX"))) // DIEGO - OS_24730
                {
                    sSql.Append("coalesce(nf.vl_toticms_ii,0)  vICMS, ");
                    sSql.Append("coalesce(nf.vl_baseicms_ii,0) vBC, ");
                }   // DIEGO - OS_24730 - 11/08/2010              
                else
                {
                    if ((pbIndustri) || (nm_Cliente == "CMENDES"))
                    {
                        if (sStImpICMS_Em_Subst_NF == "S")
                        {
                            sSql.Append("(coalesce(nf.vl_baseicm,0) + coalesce(nf.VL_BICMPROPRIO_SUBST,0)) vBC, ");
                        }
                        else
                        {
                            sSql.Append("coalesce(nf.vl_baseicm,0) vBC, ");
                        }
                    }

                    if (sStImpICMS_Em_Subst_NF == "S")
                    {
                        sSql.Append("(COALESCE(nf.vl_toticms,0) + COALESCE(NF.VL_TICMPROPRIO_SUBST,0)) vICMS, "); //Claudinei - o.s. 24200 - 01/03/2010
                        sSql.Append("(coalesce(nf.vl_baseicm,0) + coalesce(nf.VL_BICMPROPRIO_SUBST,0)) vBC, ");
                    }
                    else
                    {
                        sSql.Append("COALESCE(nf.vl_toticms,0)  vICMS, ");
                        sSql.Append("coalesce(nf.vl_baseicm,0) vBC, ");
                    }
                }

                sSql.Append("nf.vl_bicmssu vBCST, ");
                sSql.Append("nf.vl_icmssub vST, ");
                if (pbIndustri)
                {
                    if (sTipoIndustrializacao == "1")
                    {
                        if (objdest.Uf != "EX")
                        {
                            sSql.Append("(select sum(movitem.vl_totbruto) from movitem inner join opereve on (opereve.cd_oper = movitem.cd_oper) where ((movitem.cd_empresa = nf.cd_empresa) and (movitem.cd_nfseq = nf.cd_nfseq)) and (opereve.ST_ESTTERC <> 'S')) vProd, ");
                        }
                        else
                        {
                            sSql.Append("((select sum(movitem.vl_totbruto) from movitem inner join opereve on (opereve.cd_oper = movitem.cd_oper) where ((movitem.cd_empresa = nf.cd_empresa) and (movitem.cd_nfseq = nf.cd_nfseq)) and (opereve.ST_ESTTERC <> 'S'))+(coalesce( nf.vl_impimport,0))) vProd, ");
                        }
                    }
                    else
                    {
                        if (objdest.Uf != "EX")
                        {
                            sSql.Append("coalesce(nf.vl_totprod, 0) vProd, ");
                        }
                        else
                        {
                            sSql.Append("(coalesce(nf.vl_totprod, 0)+(coalesce( nf.vl_impimport,0))) vProd, ");
                        }
                    }
                }
                else
                {
                    if (nm_Cliente != "NAVE_THERM")
                    {
                        if (sTipoIndustrializacao == "1")
                        {
                            if (objdest.Uf != "EX")
                            {
                                sSql.Append(" case when coalesce(opereve.st_hefrete, 'N') = 'N' then ");
                                sSql.Append(" (nf.vl_totprod + nf.vl_desccomer + nf.vl_servico) ");
                                sSql.Append("else ");
                                sSql.Append("(nf.vl_totprod + nf.vl_desccomer ) ");
                                sSql.Append("end vProd,");
                            }
                            else
                            {
                                sSql.Append(" case when coalesce(opereve.st_hefrete, 'N') = 'N' then ");
                                sSql.Append(" (nf.vl_totprod + nf.vl_desccomer + nf.vl_servico + coalesce( nf.vl_impimport,0)) ");
                                sSql.Append("else ");
                                sSql.Append("(nf.vl_totprod + nf.vl_desccomer + coalesce( nf.vl_impimport,0)) ");
                                sSql.Append("end vProd,");
                            }
                        }
                        else
                        {
                            if (objdest.Uf != "EX")
                            {
                                sSql.Append("coalesce(nf.vl_totprod, 0) vProd,");
                            }
                            else
                            {
                                sSql.Append("(coalesce(nf.vl_totprod, 0)+coalesce( nf.vl_impimport,0)) vProd,");
                            }
                        }
                    }
                    else
                    {
                        sSql.Append("(nf.vl_totprod + nf.vl_servico) vProd, ");

                        if (sStImpICMS_Em_Subst_NF == "S")
                        {
                            if (objdest.Uf != "EX")
                            {
                                sSql.Append("(coalesce(nf.vl_baseicm,0) + coalesce(nf.VL_BICMPROPRIO_SUBST,0)) vBC, ");
                            }
                            else
                            {
                                sSql.Append("(coalesce(nf.vl_baseicm,0) + coalesce(nf.VL_BICMPROPRIO_SUBST,0) + coalesce( nf.vl_impimport,0)) vBC, ");
                            }
                        }
                        else
                        {
                            if (objdest.Uf != "EX")
                            {
                                sSql.Append("coalesce(nf.vl_baseicm,0) vBC, ");
                            }
                            else
                            {
                                sSql.Append("(coalesce(nf.vl_baseicm,0)+ (coalesce( nf.vl_impimport,0)) vBC, ");
                            }
                        }
                    }
                }
                sSql.Append("nf.vl_frete vFrete, ");
                sSql.Append("nf.vl_seg vSeg, ");
                bool bNfSuframa = VerificaNotaComSuframa(sEmp, sNF, Conn);
                sSql.Append(bNfSuframa == false ? "nf.vl_desccomer vDesc, " : "(coalesce(nf.vl_cofins_suframa,0)+ coalesce(nf.vl_pis_suframa,0)+ coalesce(nf.vl_desc_suframa,0)) vDesc, ");
                sSql.Append("nf.vl_impimport vII, ");
                sSql.Append("nf.vl_totipi vIPI, ");
                sSql.Append("nf.vl_pis vPIS, ");
                sSql.Append("nf.vl_cofins vCOFINS, ");
                sSql.Append("nf.vl_outras vOutro, ");
                if (sTipoIndustrializacao == "1")
                {
                    sSql.Append("(select sum(movitem.vl_totbruto) from movitem where ((movitem.cd_empresa = nf.cd_empresa) and (movitem.cd_nfseq = nf.cd_nfseq))) + coalesce(nf.vl_frete,0) vNF, "); //Claudinei - 24162 - 22/02/2010 
                }
                else
                {
                    sSql.Append("nf.vl_totnf vNF, ");
                }
                sSql.Append("coalesce(tpdoc.st_pauta,'N') st_pauta ");
                sSql.Append(", nf.vl_servico vServ ");
                sSql.Append(", nf.vl_iss Viss ");
                sSql.Append(", nf.vl_pis_serv PisIss ");
                sSql.Append(", nf.vl_cofins_serv cofinsIss ");
                //Tabela
                sSql.Append("From NF ");
                //Relacionamentos
                sSql.Append("inner join movitem on (movitem.cd_empresa = nf.cd_empresa) and (movitem.cd_nfseq = nf.cd_nfseq) ");
                sSql.Append("inner join opereve on (opereve.cd_oper = movitem.cd_oper)");
                sSql.Append("inner join tpdoc on (tpdoc.cd_tipodoc = nf.cd_tipodoc) ");
                //Where
                sSql.Append("Where ");
                sSql.Append("(NF.cd_empresa ='");
                sSql.Append(sEmp);
                sSql.Append("')");
                sSql.Append(" and ");
                sSql.Append("(nf.cd_nfseq = '");
                sSql.Append(sNF);
                sSql.Append("') ");
                FbCommand cmdTotais = new FbCommand(sSql.ToString(), Conn);
                cmdTotais.ExecuteNonQuery();
                FbDataReader drTotais = cmdTotais.ExecuteReader();
                drTotais.Read();
                if ((pbIndustri) && (nm_Cliente != "TECNOZ"))
                {
                    dTotbaseICMS = Math.Round(Convert.ToDecimal(drTotais["vBCST"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                }
                if ((pbIndustri) && (nm_Cliente == "MOGPLAST"))
                {
                    dTotbaseICMS = Math.Round(Convert.ToDecimal(drTotais["vBC"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                }
                belIcmstot objicmstot = new belIcmstot();
                dTotbaseICMS = Math.Round(Convert.ToDecimal(drTotais["vBC"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                objicmstot.Vbc = Math.Round(Convert.ToDecimal(dTotbaseICMS.ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                dTotValorICMS = Math.Round(Convert.ToDecimal(drTotais["vICMS"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                objicmstot.Vicms = Math.Round(Convert.ToDecimal(dTotValorICMS.ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                if (!drTotais["vBCST"].Equals(string.Empty))
                {
                    decimal dvBCST = Math.Round(Convert.ToDecimal(drTotais["vBCST"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                    objicmstot.Vbcst = dvBCST;
                }
                if (!drTotais["vST"].Equals(string.Empty))
                {
                    decimal dvST = Math.Round(Convert.ToDecimal(drTotais["vST"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                    objicmstot.Vst = dvST;
                }
                if (drTotais["vProd"].ToString() != "")
                {
                    decimal dvProd = Math.Round(Convert.ToDecimal(drTotais["vProd"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                    objicmstot.Vprod = dvProd;
                }
                else
                {
                    objicmstot.Vprod = 0;
                }
                if (!drTotais["vFrete"].Equals(string.Empty))
                {
                    decimal dvFrete = Math.Round(Convert.ToDecimal(drTotais["vFrete"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                    objicmstot.Vfrete = dvFrete;
                }
                if (!drTotais["vSeg"].Equals(string.Empty))
                {
                    decimal dvSeg = Math.Round(Convert.ToDecimal(drTotais["vSeg"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                    objicmstot.Vseg = dvSeg;
                }
                decimal dvDesc = Math.Round(Convert.ToDecimal(drTotais["vDesc"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                objicmstot.Vdesc = dvDesc;
                if (!drTotais["vII"].Equals(string.Empty))
                {
                    decimal dvII = Math.Round(Convert.ToDecimal(drTotais["vII"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                    objicmstot.Vii = dvII;
                }
                if (!drTotais["vIPI"].Equals(string.Empty))
                {
                    decimal dvIPI = Math.Round(Convert.ToDecimal(drTotais["vIPI"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                    objicmstot.Vipi = dvIPI;
                }
                if (sTipoIndustrializacao == "2")
                {
                    dTotPis = Math.Round(Convert.ToDecimal(drTotais["vPIS"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                }
                objicmstot.Vpis = Math.Round(Convert.ToDecimal(drTotais["vPIS"].ToString()), 2); //Claudinei - o.s. 24292 - 24/03/2010 //Claudinei - o.s. 24248 - 26/03/2010
                if (sTipoIndustrializacao == "2")
                {
                    dTotCofins = Math.Round(Convert.ToDecimal(drTotais["vCOFINS"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                }
                objicmstot.Vcofins = Math.Round(Convert.ToDecimal(dTotCofins.ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                if (!drTotais["vOutro"].Equals(string.Empty))
                {
                    decimal dvOutro = Math.Round(Convert.ToDecimal(drTotais["vOutro"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                    objicmstot.Voutro = dvOutro;
                }
                if (!drTotais["vNF"].Equals(string.Empty))
                {
                    decimal dvNF = Math.Round(Convert.ToDecimal(drTotais["vNF"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                    objicmstot.Vnf = dvNF;
                }
                objtot.belIcmstot = objicmstot;
                if (sTipoIndustrializacao == "2")
                {
                    dTotServ = Math.Round(Convert.ToDecimal(drTotais["vServ"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                }
                if (dTotServ != 0)
                {
                    belIssqntot objisstot = new belIssqntot();
                    if (dTotServ != 0)
                    {
                        objisstot.Vserv = Math.Round(Convert.ToDecimal(dTotServ.ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                    }
                    if (sTipoIndustrializacao == "2")
                    {
                        dTotBCISS = Math.Round(Convert.ToDecimal(drTotais["vServ"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                    }
                    if (dTotBCISS != 0)
                    {
                        objisstot.Vbc = Math.Round(Convert.ToDecimal(dTotBCISS.ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                    }
                    if (sTipoIndustrializacao == "2")
                    {
                        dTotISS = Math.Round(Convert.ToDecimal(drTotais["Viss"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                    }
                    if (dTotISS != 0)
                    {
                        objisstot.Viss = Math.Round(Convert.ToDecimal(dTotISS.ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                    }
                    if (sTipoIndustrializacao == "2")
                    {
                        dTotPisISS = Math.Round(Convert.ToDecimal(drTotais["PisIss"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                    }
                    if (dTotPisISS != 0)
                    {
                        objisstot.Vpis = Math.Round(Convert.ToDecimal(dTotPisISS.ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                    }
                    if (sTipoIndustrializacao == "2")
                    {
                        dTotCofinsISS = Math.Round(Convert.ToDecimal(drTotais["cofinsIss"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                    }
                    if (dTotCofinsISS != 0)
                    {
                        objisstot.Vcofins = Math.Round(Convert.ToDecimal(dTotCofinsISS.ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                    }
                    objtot.belIssqntot = objisstot;
                }
            }
            catch (Exception Ex)
            {
                sExecao = " - Problemas nos Totais da Nota";
                throw new Exception(Ex.Message + sExecao);
            }
            finally
            {
                //Conn.Close();
            }

            return objtot;
        }

        public belTransp BuscaFrete(string sEmp,
                                    string sNF)
        {
            //belGerarXML BuscaConexao = new belGerarXML();
            //FbConnection Conn = BuscaConexao.Conn;
            belTransp objtransp = new belTransp();

            try
            {
                //Conn.Open();
                StringBuilder sSql = new StringBuilder();

                //Campos do Select
                sSql.Append("Select ");
                sSql.Append("coalesce(nf.st_frete, 1) modFrete, ");
                //Clauidnei - o.s. 23507 - 10-07-2009
                sSql.Append("transpor.st_pessoaj, ");
                sSql.Append("transpor.cd_cpf, ");
                //Fim - Clauidnei - o.s. 23507 - 10-07-2009
                sSql.Append("transpor.cd_cgc CNPJ, ");
                sSql.Append("transpor.nm_trans xNome, ");
                sSql.Append("transpor.cd_insest IE, ");
                sSql.Append("transpor.ds_endnor xEnder, ");
                sSql.Append("transpor.nm_cidnor xMun, ");
                sSql.Append("transpor.cd_ufnor UF, ");
                sSql.Append("coalesce(nf.qt_volumes, 0) qVol, ");
                sSql.Append("coalesce(nf.ds_especie, 'VOLUME') esp, ");
                sSql.Append("COALESCE(nf.vl_pesoliq, 0) pesoL, ");
                sSql.Append("COALESCE(nf.vl_pesobru,0) pesoB, ");
                sSql.Append("COALESCE(nf.ds_marca, 'MARCA') marca, ");

                //Claudinei - o.s. 23674 - 10/09/2009

                //Danner - o.s. 24322 - 31/03/2010
                //sSql.Append(", transpor.cd_placa placa, ");
                sSql.Append("CASE WHEN COALESCE (NF.cd_placa, '') <> '' then NF.cd_placa ELSE TRANSPOR.cd_placa END placa,");
                sSql.Append("transpor.cd_ufvei UF, ");
                sSql.Append("transpor.ds_rntc RNTC ");

                //Fim - Claudinei - o.s. 23674 - 10/09/2009

                //Danner - o.s. 24385 - 26/04/2010

                sSql.Append(", coalesce(nf.ds_numero,'') nVol ");

                //Fim - Danner - o.s. 24385 - 26/04/2010

                //Tabela
                sSql.Append("From NF ");

                //Relacionamentos
                sSql.Append("inner join transpor on (transpor.cd_trans = nf.cd_trans) ");

                //Where
                sSql.Append("Where ");
                sSql.Append("(NF.cd_empresa ='");
                sSql.Append(sEmp);
                sSql.Append("')");
                sSql.Append(" and ");
                sSql.Append("(nf.cd_nfseq = '");
                sSql.Append(sNF);
                sSql.Append("') ");
                //Claudinei - o.s 23507 - 25/05/2009

                FbCommand cmdTranspor = new FbCommand(sSql.ToString(), Conn);
                cmdTranspor.ExecuteNonQuery();



                FbDataReader drTranspor = cmdTranspor.ExecuteReader();

                if (drTranspor.Read()) //Claudinei - o.s. 24210 - 03/03/2010
                {
                    StringBuilder sSql2 = new StringBuilder();
                    sSql2.Append("Select ");

                    sSql2.Append("transpor.nm_trans xNome ");




                    //Tabela
                    sSql2.Append("From NF ");

                    //Relacionamentos
                    sSql2.Append("inner join transpor on (transpor.cd_trans = nf.cd_trans) ");

                    //Where
                    sSql2.Append("Where ");
                    sSql2.Append("(NF.cd_empresa ='");
                    sSql2.Append(sEmp);
                    sSql2.Append("')");
                    sSql2.Append(" and ");
                    sSql2.Append("(nf.cd_nfseq = '");
                    sSql2.Append(sNF);
                    sSql2.Append("') ");
                    string razao_transp = string.Empty;
                    using (FbCommand cmd = new FbCommand(sSql2.ToString(), Conn))
                    {

                        razao_transp = Convert.ToString(cmd.ExecuteScalar()).Trim();

                    }
                    if (drTranspor["modFrete"].ToString() == "1") // Remetnete(Emitente)
                    {
                        objtransp.Modfrete = "0";
                    }
                    else if (drTranspor["modFrete"].ToString() == "2") // destinatario
                    {
                        objtransp.Modfrete = "1";
                    }
                    else if (drTranspor["modFrete"].ToString() == "3")
                    {
                        objtransp.Modfrete = "2";
                    }
                    else
                    {
                        objtransp.Modfrete = "9";
                    }
                    belTransportadora objtransportadora = new belTransportadora();

                    if (drTranspor["st_pessoaj"].ToString() == "S")
                    {
                        if (drTranspor["CNPJ"].ToString() != "")
                        {
                            objtransportadora.Cnpj = TiraSimbolo(drTranspor["CNPJ"].ToString().PadLeft(14, '0'), "");
                        }
                    }
                    else
                    {
                        if (drTranspor["CD_CPF"].ToString() != "")
                        {
                            objtransportadora.Cpf = TiraSimbolo(drTranspor["CD_CPF"].ToString().PadLeft(11, '0'), "");

                            //no = Doc.CreateElement("CPF");
                            //noText = Doc.CreateTextNode(TiraSimbolo(drTranspor["CD_CPF"].ToString().PadLeft(11, '0'), ""));
                            //no.AppendChild(noText);
                            //noTransportadora.AppendChild(no);
                        }

                    }

                    /*
                    if (!drTranspor["cnpj"].Equals(string.Empty))
                    {
                        no = Doc.CreateElement("CNPJ");
                        noText = Doc.CreateTextNode(TiraSimbolo(drTranspor["cnpj"].ToString(), ""));
                        no.AppendChild(noText);
                        noTransportadora.AppendChild(no);
                    }
                     */

                    if (drTranspor["xnome"] != null)
                    {
                        razao_transp = drTranspor["xNome"].ToString();
                    }
                    if (razao_transp != "")
                    {
                        int iTamanho = razao_transp.Length - 1;
                        if (iTamanho > 59)
                        {
                            iTamanho = 59;
                            // Diego - 0S 24039 - 26-01-10
                            objtransportadora.Xnome = TiraSimbolo(razao_transp.ToString().Substring(0, iTamanho), "");
                        }
                        else
                        {
                            objtransportadora.Xnome = TiraSimbolo(razao_transp.ToString().Substring(0, razao_transp.Length), "");
                        }
                        // Diego - 0S 24039 - 26-01-10 - FIM

                        //no = Doc.CreateElement("xNome");
                        //noText = Doc.CreateTextNode(TiraSimbolo(drTranspor["xNome"].ToString().Substring(0, iTamanho), ""));
                        //no.AppendChild(noText);
                        //noTransportadora.AppendChild(no);
                    }


                    if (drTranspor["IE"].ToString() != "")
                    {
                        objtransportadora.Ie = TiraSimbolo(drTranspor["IE"].ToString(), "");

                        //no = Doc.CreateElement("IE");
                        //noText = Doc.CreateTextNode(TiraSimbolo(drTranspor["IE"].ToString(), ""));
                        //no.AppendChild(noText);
                        //noTransportadora.AppendChild(no);
                    }

                    if (drTranspor["xEnder"].ToString() != "")
                    {
                        objtransportadora.Xender = TiraSimbolo(drTranspor["xEnder"].ToString(), "");

                        //no = Doc.CreateElement("xEnder");
                        //noText = Doc.CreateTextNode(TiraSimbolo(drTranspor["xEnder"].ToString(), ""));
                        //no.AppendChild(noText);
                        //noTransportadora.AppendChild(no);
                    }


                    if (drTranspor["xMun"].ToString() != "")
                    {
                        objtransportadora.Xmun = TiraSimbolo(drTranspor["xMun"].ToString(), "");

                        //no = Doc.CreateElement("xMun");
                        //noText = Doc.CreateTextNode(TiraSimbolo(drTranspor["xMun"].ToString(), ""));
                        //no.AppendChild(noText);
                        //noTransportadora.AppendChild(no);
                    }

                    if (drTranspor["UF"].ToString() != "")
                    {
                        objtransportadora.Uf = drTranspor["UF"].ToString();

                        //no = Doc.CreateElement("UF");
                        //noText = Doc.CreateTextNode(drTranspor["UF"].ToString());
                        //no.AppendChild(noText);
                        //noTransportadora.AppendChild(no);
                    }
                    objtransp.belTransportadora = objtransportadora;

                    //Claudinei - o.s. 23674 - 10/09/2009
                    if ((drTranspor["placa"].ToString() != "") && (drTranspor["placa"].ToString() != null))
                    {

                        //noVeic = Doc.CreateElement("veicTransp");
                        belVeicTransp objveictransp = new belVeicTransp();

                        //noTranspor.AppendChild(noVeic);

                        objveictransp.Placa = TiraSimbolo(drTranspor["placa"].ToString().Replace(" ", "").Trim(), "");//Danner - o.s. sem - 30/03/2010
                        //no = Doc.CreateElement("placa");
                        //noText = Doc.CreateTextNode(drTranspor["placa"].ToString().Replace(" ", "").Trim());
                        //no.AppendChild(noText);
                        //noVeic.AppendChild(no);


                        if (drTranspor["UF"].ToString() != "")
                        {
                            objveictransp.Uf = drTranspor["UF"].ToString().Trim();

                            //no = Doc.CreateElement("UF");
                            //noText = Doc.CreateTextNode(drTranspor["UF"].ToString().Trim());
                            //no.AppendChild(noText);
                            //noVeic.AppendChild(no);
                        }

                        if (drTranspor["RNTC"].ToString() != "")//Tag não Obrigatoria
                        {
                            objveictransp.Rntc = drTranspor["RNTC"].ToString().Trim();

                            //no = Doc.CreateElement("RNTC");
                            //noText = Doc.CreateTextNode(drTranspor["RNTC"].ToString().Trim());
                            //no.AppendChild(noText);
                            //noVeic.AppendChild(no);
                        }
                        //Danner - o.s. sem - 05/03/2010
                        else
                        {
                            //throw new Exception("RNTC do Transportador não pode estar vazio!");
                            objveictransp.Rntc = "00";
                        }
                        objtransp.belVeicTransp = objveictransp;
                    }

                    //Fim - Claudinei - o.s. 23674 - 10/09/2009

                    //noVol = Doc.CreateElement("vol");
                    //noTranspor.AppendChild(noVol);
                    belVol objvol = new belVol();



                    if (drTranspor["qVol"].ToString() != "")
                    {

                        //no = Doc.CreateElement("qVol");
                        //Claudinei - o.s. 23776 - 22/10/2009
                        try
                        {
                            decimal dqVol = Convert.ToDecimal(drTranspor["qVol"].ToString());
                            //Claudinei - o.s. 24276 - 26/03/2010
                            if ((drTranspor["qVol"].ToString() == "") || (drTranspor["qVol"].ToString() == "0"))
                            {
                                dqVol = 1;
                            }
                            //Fim - Claudinei - o.s. 24276 - 26/03/2010

                            objvol.Qvol = dqVol;

                            //noText = Doc.CreateTextNode(Convert.ToInt64(dqVol).ToString());
                            ////Claudinei - o.s. 23642 - 25/09/2009
                            //no.AppendChild(noText);
                            //noVol.AppendChild(no);

                        }
                        catch (Exception ex)
                        {

                            throw new Exception(string.Format("{0} - Campo de Quantidade de Volumes na tela de Montar NF",
                                                ex.Message));
                        }
                        //Fim - Claudinei - o.s. 23776 - 22/10/2009

                    }

                    if (drTranspor["nVol"].ToString() != "")
                    {
                        objvol.Nvol = drTranspor["nVol"].ToString();//Danner - o.s. 24432 - 04/05/2010
                    }



                    if (drTranspor["esp"].ToString() != "")
                    {
                        objvol.Esp = TiraSimbolo(drTranspor["esp"].ToString(), "");

                        //no = Doc.CreateElement("esp");
                        //noText = Doc.CreateTextNode(TiraSimbolo(drTranspor["esp"].ToString(), ""));
                        //no.AppendChild(noText);
                        //noVol.AppendChild(no);
                    }

                    if (drTranspor["marca"].ToString() != "")
                    {
                        objvol.Marca = drTranspor["marca"].ToString();

                        //no = Doc.CreateElement("marca");
                        //noText = Doc.CreateTextNode(drTranspor["marca"].ToString());
                        //no.AppendChild(noText);
                        //noVol.AppendChild(no);
                    }


                    if (drTranspor["pesoL"].ToString() != "")
                    {
                        try
                        {
                            decimal dpesoL = Math.Round(Convert.ToDecimal(drTranspor["pesoL"].ToString()), 3); //Claudinei - o.s. 24248 - 26/03/2010
                            objvol.PesoL = dpesoL;

                            //no = Doc.CreateElement("pesoL");
                            //noText = Doc.CreateTextNode(dpesoL.ToString("#0.000").Replace(',', '.'));
                            //no.AppendChild(noText);
                            //noVol.AppendChild(no);

                        }
                        catch (Exception ex)
                        {

                            throw new Exception(string.Format("{0} - Campo Peso Liquido",
                                                              ex.Message));
                        }

                    }

                    if (drTranspor["pesoB"].ToString() != "")
                    {
                        try
                        {
                            decimal dpesoB = Math.Round(Convert.ToDecimal(drTranspor["pesoB"].ToString()), 3); //Claudinei - o.s. 24248 - 26/03/2010
                            objvol.PesoB = dpesoB;

                            //no = Doc.CreateElement("pesoB");
                            //noText = Doc.CreateTextNode(dpesoB.ToString("#0.000").Replace(',', '.'));
                            //no.AppendChild(noText);
                            //noVol.AppendChild(no);

                        }
                        catch (Exception ex)
                        {

                            throw new Exception(string.Format("{0} - Campo Peso Bruto",
                                                             ex.Message));
                        }
                    }
                    objtransp.belVol = objvol;

                }
                //Claudinei - o.s. 24210 - 03/03/2010
                else
                {
                    throw new Exception("Transportadora não selecionada na nota!");
                }
                //Fim - Claudinei - o.s. 24210 - 03/03/2010
                //Fim - Montagem do XML
            }
            catch (Exception Ex)
            {
                sExecao = " - Problemas no Frete da Nota";
                throw new Exception(Ex.Message + sExecao);
            }
            finally
            {
                //Conn.Close();
            }

            return objtransp;


        }

        public belInfAdic BuscaObs(string sEmp,
                           string sNF,
                           belDest objbeldest,
                           List<belDet> objbelDet, belGerarXML objbelGeraXml)
        {
            belInfAdic objinfadic = new belInfAdic();

            try
            {
                StringBuilder sSql = new StringBuilder();

                //Campos do Select
                sSql.Append("Select ");
                sSql.Append("nf.ds_anota ");

                if (((objbelGeraXml.nm_Cliente == "MOGPLAST") || (objbelGeraXml.nm_Cliente == "TSA")) && (sEmp == "003"))
                {
                    sSql.Append(", nf.cd_nfseq_fat_origem ");
                }
                if (objbelGeraXml.nm_Cliente == "MACROTEX")
                {
                    sSql.Append(", vendedor.nm_vend, ");
                    sSql.Append("nf.DS_DOCORIG ");
                }
                //Tabela
                sSql.Append("From NF ");
                //Relacionamentos
                sSql.Append("left join vendedor on (vendedor.cd_vend = nf.cd_vend1) ");
                //Where
                sSql.Append("Where ");
                sSql.Append("(NF.cd_empresa ='");
                sSql.Append(sEmp);
                sSql.Append("')");
                sSql.Append(" and ");
                sSql.Append("(nf.cd_nfseq = '");
                sSql.Append(sNF);
                sSql.Append("') ");
                string sObs = "";
                sObs = RetornaBlob(sSql, sEmp, Conn, objbelGeraXml);
                if (sObs.IndexOf("\\fs") != -1)// DIEGO - OS_24854 
                {
                    sObs = sObs.Substring((sObs.IndexOf("\\fs") + 6), sObs.Length - (sObs.IndexOf("\\fs") + 6));
                }
                if (objbelGeraXml.nm_Cliente == "MARPA")
                {
                    sObs += MontaObsAgrup(sEmp, sNF, Conn);
                }
                Globais LeRegWin = new Globais();
                string sBanco = LeRegWin.LeRegConfig("BancoDados");
                {
                    //Fim - Danner - o.s. 24383 - 22/04/2010
                    //belGerarXML BuscaConexao = new belGerarXML();
                    //FbConnection Conn = BuscaConexao.Conn;
                    #region OS_24245

                    try
                    {
                        if (sBanco.ToUpper().IndexOf("CERAMICAC0") == -1) //Claudinei - o.s. 24245 - 08/03/2010
                        {
                            StringBuilder sSuframa = new StringBuilder();
                            sSuframa.Append("Select First 1 ");
                            sSuframa.Append("nf.ds_anota, ");
                            sSuframa.Append("clifor.st_descsuframa, ");
                            sSuframa.Append("clifor.cd_suframa, ");
                            sSuframa.Append("clifor.ST_PISCOFINS_SUFRAMA, ");
                            sSuframa.Append("nf.vl_aliqcofins_suframa, ");
                            sSuframa.Append("nf.vl_aliqpis_suframa, ");
                            sSuframa.Append("nf.vl_cofins_suframa, ");
                            sSuframa.Append("NF.vl_pis_suframa, ");

                            //Claudinei - o.s. 23683 - 11/09/2009
                            sSuframa.Append("(select Sum(movitem.vl_descsuframa) from movitem where (movitem.cd_empresa = nf.cd_empresa) and (movitem.cd_nfseq = nf.cd_nfseq)) vl_suframa, ");
                            sSuframa.Append("icm.vl_aliquot vl_persuframa ");
                            //Fim - Claudinei - o.s. 23683 - 11/09/2009

                            //Claudinei - o.s. 23827 - 17/11/2009

                            sSuframa.Append(", ");
                            sSuframa.Append("case when empresa.vl_aliqfatcred > 0 then ");
                            sSuframa.Append("(nf.vl_totnf * empresa.vl_aliqfatcred)/100 ");
                            sSuframa.Append("else ");
                            sSuframa.Append("0 ");
                            sSuframa.Append("end aliq, ");
                            sSuframa.Append("empresa.vl_aliqfatcred, ");
                            sSuframa.Append("coalesce(tpdoc.st_hevenda,'N') st_hevenda ");

                            //Fim - Claudinei - o.s. 23827 - 17/11/2009

                            //Tabela
                            sSuframa.Append("From NF ");

                            //Relacionamentos
                            sSuframa.Append("left join clifor on (clifor.cd_clifor = nf.cd_clifor) ");
                            sSuframa.Append("left join icm on (icm.cd_ufnor = clifor.cd_ufnor) ");
                            sSuframa.Append("left join movitem on (movitem.cd_empresa = nf.cd_empresa) ");
                            sSuframa.Append("and ");
                            sSuframa.Append("(movitem.cd_nfseq = nf.cd_nfseq) ");

                            //Claudinei - o.s. 23827 - 17/11/2009
                            sSuframa.Append("Inner join ");
                            sSuframa.Append("Empresa on ");
                            sSuframa.Append("(Empresa.cd_empresa = nf.cd_empresa) ");

                            sSuframa.Append("Left join ");
                            sSuframa.Append("TPDoc on ");
                            sSuframa.Append("(TPDoc.cd_tipodoc = nf.cd_tipodoc) ");

                            //Fim - Claudinei - o.s. 23827 - 17/11/2009


                            //Where
                            sSuframa.Append("Where ");
                            sSuframa.Append("(NF.cd_empresa ='");
                            sSuframa.Append(sEmp);
                            sSuframa.Append("')");
                            sSuframa.Append(" and ");
                            sSuframa.Append("(nf.cd_nfseq = '");
                            sSuframa.Append(sNF);
                            sSuframa.Append("') ");

                            //Conn.Open();

                            FbCommand cmdSuframa = new FbCommand(sSuframa.ToString(), Conn);
                            cmdSuframa.ExecuteNonQuery();

                            FbDataReader drSuframa = cmdSuframa.ExecuteReader();
                            drSuframa.Read();

                            if (drSuframa["st_descsuframa"].ToString() == "S")
                            {
                                //Claudinei - o.s. 23683 - 11/09/2009
                                decimal dvlSuframa = Math.Round(Convert.ToDecimal(drSuframa["vl_suframa"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                                decimal dvlPerSuframa = Math.Round(Convert.ToDecimal(drSuframa["vl_persuframa"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                                //Fim - Claudinei - o.s. 23683 - 11/09/2009

                                if (sObs.Trim() != "")
                                {
                                    sObs += string.Format(" - DESCONTO DE {0:C2} REF. AO ICMS {1:f2}% CODIGO SUFRAMA: {2}",
                                                          dvlSuframa,
                                                          dvlPerSuframa,
                                                          drSuframa["cd_suframa"].ToString());
                                }
                                else
                                {
                                    sObs += string.Format("DESCONTO DE {0:C2} REF. AO ICMS 7.00% CODIGO SUFRAMA: {1}",
                                                          dvlSuframa,
                                                          drSuframa["cd_suframa"].ToString());
                                }
                            }

                            //Claudinei - o.s. sem - 01/09/2009

                            if (drSuframa["ST_PISCOFINS_SUFRAMA"].ToString() == "S")
                            {
                                decimal dvl_aliqcofins_suframa = Math.Round(Convert.ToDecimal(drSuframa["vl_aliqcofins_suframa"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                                decimal dvl_cofins_suframa = Math.Round(Convert.ToDecimal(drSuframa["vl_cofins_suframa"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                                decimal dvl_aliqpis_suframa = Math.Round(Convert.ToDecimal(drSuframa["vl_aliqpis_suframa"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                                decimal dvl_pis_suframa = Math.Round(Convert.ToDecimal(drSuframa["vl_pis_suframa"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010

                                if (sObs.Trim() != "")
                                {
                                    sObs += string.Format(" - ABATIMENTO COFINS ({0}%) - VALOR R$ {1} - ABATIMENTO PIS ({2}%) - VALOR R$ {3} ",
                                                          dvl_aliqcofins_suframa.ToString("#0.00").Replace(',', '.'),
                                                          dvl_cofins_suframa.ToString("#0.00").Replace(',', '.'),
                                                          dvl_aliqpis_suframa.ToString("#0.00").Replace(',', '.'),
                                                          dvl_pis_suframa.ToString("#0.00").Replace(',', '.'));
                                }
                                else
                                {
                                    sObs += string.Format("ABATIMENTO COFINS ({0}%) - VALOR R$ {1} - ABATIMENTO PIS ({2}%) - VALOR R$ {3} ",
                                                          dvl_aliqcofins_suframa.ToString("#0.0000").Replace(',', '.'),
                                                          dvl_cofins_suframa.ToString("#0.0000").Replace(',', '.'),
                                                          dvl_aliqpis_suframa.ToString("#0.0000").Replace(',', '.'),
                                                          dvl_pis_suframa.ToString("#0.0000").Replace(',', '.'));
                                }
                            }


                            decimal dvlnf = 0;
                            if (drSuframa["aliq"].ToString() != "")
                            {
                                dvlnf = Math.Round(Convert.ToDecimal(drSuframa["aliq"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                            }

                        }
                        StringBuilder sDevolucao = new StringBuilder();


                        //Tabela
                        sDevolucao.Append("Select ");
                        sDevolucao.Append("movitem.cd_doc, ");
                        sDevolucao.Append("nf.cd_clifor, ");
                        sDevolucao.Append("nf.dt_emi, ");
                        sDevolucao.Append("movensai.dt_emi EmissaoEntrada, ");
                        sDevolucao.Append("movitem.vl_totbruto ");
                        sDevolucao.Append("From Movitem ");
                        sDevolucao.Append("inner join NF on ");
                        sDevolucao.Append("(nf.cd_empresa = Movitem.cd_empresa) ");
                        sDevolucao.Append("and ");
                        sDevolucao.Append("(nf.cd_nfseq = movitem.cd_nfseq) ");
                        sDevolucao.Append("inner join opereve on ");
                        sDevolucao.Append("(opereve.cd_oper = movitem.cd_oper) ");
                        sDevolucao.Append("left join movensai on (movensai.cd_empresa = movitem.cd_empresa) ");
                        sDevolucao.Append("and ");
                        sDevolucao.Append("(movensai.cd_doc = movitem.cd_doc) ");
                        sDevolucao.Append("and ");
                        sDevolucao.Append("(movensai.cd_clifor = nf.cd_clifor) ");

                        //Where
                        sDevolucao.Append("Where ");
                        sDevolucao.Append("(Movitem.cd_empresa ='");
                        sDevolucao.Append(sEmp);
                        sDevolucao.Append("')");
                        sDevolucao.Append(" and ");
                        sDevolucao.Append("(Movitem.cd_nfseq = '");
                        sDevolucao.Append(sNF);
                        sDevolucao.Append("') ");
                        sDevolucao.Append("and ");
                        sDevolucao.Append("(opereve.ST_ESTTERC = 'S') ");
                        sDevolucao.Append("and ");
                        sDevolucao.Append("Movitem.cd_oper <> '202' ");
                        sDevolucao.Append("and ");
                        sDevolucao.Append("Movitem.cd_oper <> '227' ");//TESTE-DANI
                        sDevolucao.Append("Order by movitem.cd_doc");

                        //if (Conn.State != ConnectionState.Open)
                        {
                            //Conn.Open();
                        }
                        FbCommand cmdDevolucoes = new FbCommand(sDevolucao.ToString(), Conn);
                        cmdDevolucoes.ExecuteNonQuery();
                        FbDataReader drDevolucoes = cmdDevolucoes.ExecuteReader();
                        List<strucDevolucoes> Devolucoes = new List<strucDevolucoes>();
                        decimal dvlTotBruto = 0;
                        string scdDoc = string.Empty;
                        while (drDevolucoes.Read())
                        {
                            if (scdDoc != drDevolucoes["cd_doc"].ToString())
                            {
                                dvlTotBruto = 0;

                            }

                            dvlTotBruto += Math.Round(Convert.ToDecimal(drDevolucoes["vl_totbruto"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010

                            StringBuilder sStore = new StringBuilder();
                            sStore.Append("SELECT ");
                            sStore.Append("QT_SALDOEN ");
                            sStore.Append("FROM SP_SALDOTER('");
                            sStore.Append(sEmp);
                            sStore.Append("', '");
                            sStore.Append(drDevolucoes["cd_clifor"].ToString());
                            sStore.Append("', '");
                            sStore.Append("       "); //Claudinei - o.s. 24075 - 29/01/2010
                            sStore.Append("', '");
                            sStore.Append("|||||||"); //Claudinei - o.s. 24075 - 29/01/2010
                            sStore.Append("', '");
                            sStore.Append("X");
                            sStore.Append("', '");
                            sStore.Append("N");
                            sStore.Append("', '");
                            sStore.Append(sNF);
                            sStore.Append("', '");
                            sStore.Append(Convert.ToDateTime(drDevolucoes["dt_emi"]).ToString("dd.MM.yyyy"));
                            sStore.Append("') ");
                            sStore.Append("where SP_SALDOTER.cd_doc ='");
                            sStore.Append(drDevolucoes["cd_doc"].ToString().Trim());
                            sStore.Append("'");
                            sStore.Append(" and ");
                            sStore.Append("SP_SALDOTER.qt_saldoen > 0");

                            FbCommand cmd = new FbCommand();
                            cmd.Connection = Conn;
                            cmd.CommandText = sStore.ToString();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Clear();

                            //if (Conn.State != ConnectionState.Open)
                            {
                                //Conn.Open();
                            }

                            Int32 iSaldoTer = Convert.ToInt32(cmd.ExecuteScalar());
                            strucDevolucoes Devolucao = new strucDevolucoes();
                            if (drDevolucoes["cd_doc"].ToString() != "")
                            {
                                Devolucao.scdNotafis = drDevolucoes["cd_doc"].ToString();
                                if (drDevolucoes["EmissaoEntrada"] != System.DBNull.Value)
                                {
                                    Devolucao.dDtEmi = Convert.ToDateTime(drDevolucoes["EmissaoEntrada"]);
                                }
                                Devolucao.dValorNF = dvlTotBruto.ToString("#0.00");
                                Devolucao.sSaldo = (iSaldoTer > 0 ? "Parcial" : "Total");
                                if (!Devolucoes.Exists(c => c.scdNotafis == Devolucao.scdNotafis))
                                {
                                    Devolucoes.Add(Devolucao);
                                }
                                else
                                {
                                    for (int i = 0; i < Devolucoes.Count; i++)
                                    {
                                        if ((Devolucoes[i].scdNotafis == Devolucao.scdNotafis) && (Devolucoes[i].dDtEmi == Devolucao.dDtEmi)) //OS_25220
                                        {
                                            Devolucoes[i] = Devolucao;
                                            break;
                                        }
                                    }
                                }
                            }
                            scdDoc = drDevolucoes["cd_doc"].ToString();
                        }

                        for (int i = 0; i < Devolucoes.Count; i++)
                        {

                            if (sObs.Trim().Length > 0)
                            {
                                sObs += string.Format(" - Devolucao {0} ref. sua NF {1} de {2} de valor R$ {3}", //Claudinei - o.s. 24043 - 25/01/2010
                                                      Devolucoes[i].sSaldo,
                                                      Devolucoes[i].scdNotafis,
                                                      Devolucoes[i].dDtEmi.ToString("dd/MM/yyyy"),
                                                      Devolucoes[i].dValorNF); //Claudinei - o.s. 24043 - 25/01/2010
                            }
                            else
                            {
                                sObs += string.Format("Devolucao {0} ref. sua NF {1} de {2} de valor R$ {3}", //Claudinei - o.s. 24043 - 25/01/2010
                                                      Devolucoes[i].sSaldo,
                                                      Devolucoes[i].scdNotafis,
                                                      Devolucoes[i].dDtEmi.ToString("dd/MM/yyyy"),
                                                      Devolucoes[i].dValorNF); //Claudinei - o.s. 24043 - 25/01/2010

                            }
                        }

                        string sMensagemSuperSimples = MensagemSuperSimples(sEmp, sNF, Conn);
                        bool cfopsValidos = (objbelDet.Count(p => (p.belProd.Cfop.Equals("5101"))
                                                                   || (p.belProd.Cfop.Equals("6107"))
                                                                   || (p.belProd.Cfop.Equals("6101"))) > 0 ? true : false);

                        if (sMensagemSuperSimples != ""
                           && (objbeldest.Cnpj != null && objbeldest.Cnpj != "")
                           || (objbelGeraXml.nm_Cliente == "TERRAVIS")
                            && cfopsValidos) // OS_25182 
                        {

                            if (sObs.Trim().Length > 0)
                            {
                                sObs += " - " + sMensagemSuperSimples;
                            }
                            else
                            {
                                sObs += sMensagemSuperSimples;

                            }
                        }
                        //Claudinei - o.s. 24118 - 11/02/2010
                    }
                    catch (Exception Ex)
                    {
                        sExecao = " - Problemas ao Buscar Suframa";
                        throw new Exception(Ex.Message + sExecao);
                    }
                    finally
                    {
                        //Conn.Close();
                    }

                    #endregion
                }
                // Diego - 22/06/2010 - OS_24576
                //Monta Mensagem de PIS, COFINS
                try
                {
                    //belGerarXML BuscaConexao = new belGerarXML();
                    //FbConnection Conn = BuscaConexao.Conn;
                    //if (Conn.State != ConnectionState.Open)
                    {
                        //Conn.Open();
                    }
                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append("SELECT ");
                    sQuery.Append("NF.VL_COFINS, NF.VL_PIS, ");
                    sQuery.Append("empresa.vl_aliqpis_suframa,empresa.vl_aliqcofins_suframa, ");
                    sQuery.Append("CLIFOR.st_desc_piscofins_dupl ");
                    sQuery.Append("from nf inner join empresa ");
                    sQuery.Append("on (nf.cd_empresa = empresa.cd_empresa)");
                    sQuery.Append("inner join clifor ");
                    sQuery.Append("on (nf.cd_clifor = clifor.cd_clifor) ");
                    sQuery.Append("where (empresa.cd_empresa = '");
                    sQuery.Append(sEmp);
                    sQuery.Append("') ");
                    sQuery.Append("and ( nf.cd_nfseq = '");
                    sQuery.Append(sNF);
                    sQuery.Append("') ");


                    FbCommand cmd = new FbCommand(sQuery.ToString(), Conn);
                    FbDataReader drPisCofins = cmd.ExecuteReader();

                    while (drPisCofins.Read())
                    {
                        if ((drPisCofins["st_desc_piscofins_dupl"].ToString() == "S"))
                        {
                            if ((drPisCofins["vl_aliqpis_suframa"].ToString() != "") && (drPisCofins["vl_aliqcofins_suframa"].ToString() != ""))
                            {
                                string sMensagemPisCofins = "(PIS e COFINS retido conforme artigo 3º paragrafo 4º da lei 10.485/02, PIS "
                                                            + drPisCofins["vl_aliqpis_suframa"].ToString()
                                                            + "% R$" + drPisCofins["VL_PIS"].ToString()
                                                            + " , COFINS "
                                                            + drPisCofins["vl_aliqcofins_suframa"].ToString()
                                                            + "% R$"
                                                            + drPisCofins["VL_COFINS"].ToString()
                                                            + " Total R$" + ((Convert.ToDouble(drPisCofins["VL_PIS"].ToString())) + (Convert.ToDouble(drPisCofins["VL_COFINS"].ToString()))).ToString() + ")";
                                sObs += (sObs != "" ? " - " : "") + sMensagemPisCofins;
                            }
                        }
                    }
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }// Diego - 22/06/2010 - OS_24576 - FIM


                // Diego - 15/07/2010 - OS_24665
                //Obs de ICMS Recolhido por Substituição
                try
                {
                    //belGerarXML BuscaConexao = new belGerarXML();
                    //FbConnection Conn = BuscaConexao.Conn;


                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append("SELECT ");
                    sQuery.Append("movitem.cd_sittrib, EMPRESA.ST_SUPERSIMPLES, EMPRESA.ST_IMP_SUPERSIMPLES ");
                    sQuery.Append("from empresa INNER JOIN movitem ON (EMPRESA.cd_empresa = movitem.cd_empresa)");
                    sQuery.Append("where (empresa.cd_empresa = '");
                    sQuery.Append(sEmp);
                    sQuery.Append("') ");
                    sQuery.Append("and ( movitem.cd_nfseq = '");
                    sQuery.Append(sNF);
                    sQuery.Append("') ");


                    FbCommand cmd = new FbCommand(sQuery.ToString(), Conn);
                    FbDataReader drIcmsRecolhido = cmd.ExecuteReader();
                    string sMensagemIcmsRecolhido = "";

                    while (drIcmsRecolhido.Read())
                    {
                        if ((drIcmsRecolhido["ST_SUPERSIMPLES"].ToString() == "S") && (drIcmsRecolhido["ST_IMP_SUPERSIMPLES"].ToString() == "S"))
                        {
                            if ((drIcmsRecolhido["cd_sittrib"].ToString().Equals("010")) ||
                                        (drIcmsRecolhido["cd_sittrib"].ToString().Equals("030")) ||
                                        (drIcmsRecolhido["cd_sittrib"].ToString().Equals("060")) ||
                                        (drIcmsRecolhido["cd_sittrib"].ToString().Equals("070")))
                            {
                                sMensagemIcmsRecolhido = "ICMS RECOLHIDO POR SUBSTITUICAO TRIBUTARIA CONFORME DECRETO 54251/09 ART 313 RICMS/2000";
                                break;
                            }
                        }
                    }
                    if (sMensagemIcmsRecolhido != "")
                    {
                        sObs += (sObs.Trim() != "" ? " - " : "") + sMensagemIcmsRecolhido;
                    }
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
                // Diego - 15/07/2010 - OS_24665 - FIM
                //OS_25201 - DIEGO
                if (LeRegWin.LeRegConfig("TotalizaCFOP").Equals("True"))
                {
                    sObs += " " + MessagemTotalizaCFOP(sEmp, sNF, Conn);
                }//OS_25201 - FIM

                //OS_25224 - INICIO

                string sCNPJdest = (objbeldest.Cnpj != null ? Util.Util.RetiraCaracterCNPJ(objbeldest.Cnpj) : "");

                try
                {
                    //belGerarXML BuscaConexao = new belGerarXML();
                    //FbConnection Conn = BuscaConexao.Conn;

                    if (objbelGeraXml.Equals("JAMAICA"))
                    {
                        StringBuilder sQuery = new StringBuilder();
                        sQuery.Append("select coalesce(clifor.cd_alter2,'') cd_alter2 from clifor ");
                        sQuery.Append("where clifor.cd_cgc ='" + sCNPJdest + "'");
                        FbCommand cmd = new FbCommand(sQuery.ToString(), Conn);
                        FbDataReader dr = cmd.ExecuteReader();
                        string sMesgCodDest = "";
                        while (dr.Read())
                        {
                            sMesgCodDest = dr["cd_alter2"].ToString();
                        }
                        if (sMesgCodDest != "")
                        {
                            sObs = "<<COD FORNECEDOR " + sMesgCodDest + ">> " + sObs;
                        }
                    }
                    if (objbelGeraXml.nm_Cliente.Equals("LORENZON"))
                    {
                        StringBuilder sQuery = new StringBuilder();
                        sQuery.Append("select prazos.ds_prazo, vendedor.nm_vend , clifor.cd_clifor from nf ");
                        sQuery.Append("inner join clifor on nf.cd_clifor = clifor.cd_clifor ");
                        sQuery.Append("inner join prazos on nf.cd_prazo = prazos.cd_prazo ");
                        sQuery.Append(" inner join vendedor  on nf.cd_vendint = vendedor.cd_vend ");
                        sQuery.Append("where nf.cd_nfseq = '" + sNF + "' ");
                        sQuery.Append("and nf.cd_empresa = '" + sEmp + "' ");
                        FbCommand cmd = new FbCommand(sQuery.ToString(), Conn);
                        FbDataReader dr = cmd.ExecuteReader();
                        string sMsgLorenzon = "";


                        while (dr.Read())
                        {
                            sMsgLorenzon = "COND.PGTO = " + dr["ds_prazo"].ToString() + " | VENDEDOR = " + dr["nm_vend"].ToString() + " | COD. CLIENTE = " + dr["cd_clifor"].ToString();
                        }
                        if (sMsgLorenzon != "")
                        {
                            sObs = "<< " + sMsgLorenzon + " >> " + sObs;
                        }
                    }



                }
                catch (Exception ex)
                {
                    throw ex;
                }

                //OS_25224 - FIM
                if (sObs.Trim() != "")
                {
                    objinfadic.Infcpl = sObs.Trim();
                }
                //Fim - Montagem do XML
            }
            catch (Exception Ex)
            {
                sExecao = " - Problemas nas Obs´s da Nota";
                throw new Exception(Ex.Message + sExecao);
            }

            return objinfadic; //Danner - o.s. sem - 16/11/2009


        }

        public belCobr BuscaFat(string sEmp,
                                string sNF)
        {
            belCobr objcobr = new belCobr();
            List<belFat> objfats = new List<belFat>();

            try
            {
                StringBuilder sSql = new StringBuilder();

                //Campos do Select
                sSql.Append("Select ");
                sSql.Append("doc_ctr.cd_dupli, ");
                sSql.Append("DOC_CTR.dt_venci, ");
                sSql.Append("DOC_CTR.vl_doc ,");
                sSql.Append("doc_ctr.vl_totdesc, ");
                sSql.Append("doc_ctr.cd_documento ");

                //Tabela
                sSql.Append("From doc_ctr ");

                //Relacionamentos
                sSql.Append("INNER JOIN NF ON (NF.cd_empresa = DOC_CTR.cd_empresa) AND ");
                sSql.Append("(NF.cd_nfseq = DOC_CTR.cd_nfseq) ");

                //Where
                sSql.Append("Where ");
                sSql.Append("(nf.cd_empresa ='");
                sSql.Append(sEmp);
                sSql.Append("') ");
                sSql.Append(" and ");
                sSql.Append("(nf.cd_nfseq ='");
                sSql.Append(sNF);
                sSql.Append("') ");
                //belGerarXML BuscaConexao = new belGerarXML();
                //FbConnection Conn = BuscaConexao.Conn;
                //Conn.Open();
                FbCommand cmdFat = new FbCommand(sSql.ToString(), Conn);
                cmdFat.ExecuteNonQuery();
                FbDataReader drFat = cmdFat.ExecuteReader();
                sSql.Remove(0, sSql.Length);
                sSql.Append("Select ");
                sSql.Append("nf.cd_notafis nFat, ");
                sSql.Append("coalesce((nf.vl_totnf + nf.vl_desccomer ), 0) vOrig, ");
                sSql.Append("nf.vl_desccomer vDesc, ");
                sSql.Append("nf.vl_totnf vLiq ");
                sSql.Append("from nf ");
                sSql.Append("Where ");
                sSql.Append("(nf.cd_empresa ='");
                sSql.Append(sEmp);
                sSql.Append("') ");
                sSql.Append(" and ");
                sSql.Append("(nf.cd_nfseq ='");
                sSql.Append(sNF);
                sSql.Append("') ");
                FbCommand cmdCob = new FbCommand(sSql.ToString(), Conn);
                cmdCob.ExecuteNonQuery();
                FbDataReader drCob = cmdCob.ExecuteReader();
                belFat objfat = new belFat();
                while (drCob.Read())
                {
                    decimal dvDup = Math.Round(Convert.ToDecimal(drCob["vOrig"].ToString()), 2);
                    decimal dDesc = Math.Round(Convert.ToDecimal(drCob["vDesc"].ToString()), 2);
                    if (drCob["nFat"].ToString().Trim() != "")
                    {
                        objfat.Nfat = TiraSimbolo(drCob["nFat"].ToString(), "");
                    }
                    objfat.Vorig = dvDup;
                    if (drCob["vDesc"].ToString() != "0")
                    {
                        objfat.Vdesc = Math.Round(Convert.ToDecimal(dDesc.ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                    }
                    if (drCob["vLiq"].ToString() != "0")
                    {
                        decimal dLiq = Math.Round(Convert.ToDecimal(drCob["vLiq"].ToString()), 2);
                        objfat.Vliq = dLiq;
                    }
                }
                List<belDup> objdups = new List<belDup>();
                while (drFat.Read())
                {
                    belDup objdup = new belDup();
                    if (drFat["cd_dupli"].ToString() != "")
                    {
                        objdup.Ndup = TiraSimbolo(drFat["cd_dupli"].ToString(), "");
                    }
                    if (!drFat["dt_venci"].Equals(string.Empty))
                    {
                        objdup.Dvenc = System.DateTime.Parse(drFat["dt_venci"].ToString());
                    }
                    decimal dvDup = Math.Round(Convert.ToDecimal(drFat["vl_doc"].ToString()), 2); //Claudinei - o.s. 24248 - 26/03/2010
                    if (dvDup != 0)
                    {
                        objdup.Vdup = dvDup;
                    }
                    objdups.Add(objdup);
                }
                objfat.belDup = objdups;
                objcobr.belFat = objfat;
            }
            catch (Exception Ex)
            {
                sExecao = " - Problemas nas Faturas da Nota";
                throw new Exception(Ex.Message + sExecao);
            }
            return objcobr;
        }

        public XmlDocument BuscaAssinatura(XmlDocument Doc,
                                           XmlElement noInfNFe)
        {


            try
            {


                //Montagem do XML
                XmlElement noAssinatura;

                noAssinatura = Doc.CreateElement("Signature");
                noInfNFe.AppendChild(noAssinatura);

                /*
                no = Doc.CreateElement("infCpl");
                noText = Doc.CreateTextNode(TiraSimbolo(sObs, ""));
                no.AppendChild(noText);
                noObs.AppendChild(no);
                 */


                //Fim - Montagem do XML
            }
            catch (Exception Ex)
            {
                sExecao = " - Problemas na Assinatura do XML";
                throw new Exception(Ex.Message + sExecao);
            }

            return Doc;


        }

        #endregion

        #region Métodos

        private static bool GravaNumeroChaveNota(string sEmp, string sNota, string sNFe)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append("update nf set cd_chavenfe = '");
            sSql.Append(sNFe.Replace("NFe", ""));
            sSql.Append("'");
            sSql.Append(" Where nf.cd_empresa = '");
            sSql.Append(sEmp);
            sSql.Append("' and ");
            sSql.Append("nf.cd_nfseq = '");
            sSql.Append(sNota);
            sSql.Append("'");

            belGerarXML BuscaConexao = new belGerarXML();
            try
            {
                using (FbConnection Conn = BuscaConexao.Conn)
                {
                    using (FbCommand cmdUpdate = new FbCommand(sSql.ToString(), Conn))
                    {
                        Conn.Open();
                        cmdUpdate.ExecuteNonQuery();
                        Conn.Close();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static bool VerificaSeAgrupaItens(FbConnection Conn)
        {

            bool bAgrupa = false;
            using (FbCommand cmdAgrupa = new FbCommand("select control.cd_conteud from control where control.cd_nivel = '1350'", Conn))
            {
                bAgrupa = (cmdAgrupa.ExecuteScalar().ToString().Equals("S") ? true : false);
            }
            return bAgrupa;
        }

        private static string VerificaEmpresaSimplesNac(string sEmp, FbConnection Conn)
        {
            StringBuilder sqlSimplesNac = new StringBuilder();
            sqlSimplesNac.Append("Select ");
            sqlSimplesNac.Append("empresa.st_supersimples from empresa where (empresa.cd_empresa = '");
            sqlSimplesNac.Append(sEmp);
            sqlSimplesNac.Append("')");
            FbCommand cmdSimplesNac = new FbCommand(sqlSimplesNac.ToString(), Conn);
            cmdSimplesNac.ExecuteNonQuery();
            FbDataReader drSimpNac = cmdSimplesNac.ExecuteReader();
            string sSimpNac = "N";
            while (drSimpNac.Read())
            {
                sSimpNac = drSimpNac["st_supersimples"].ToString();
            }
            return sSimpNac;
        }

        private string BuscaContratoOBS(string psEmp, string psNrLanc, FbConnection Conn)
        {
            string sOBS = string.Empty;
            try
            {
                StringBuilder sSql = new StringBuilder();
                sSql.Append("Select ");
                sSql.Append("cd_contrato, ");
                sSql.Append("ds_obs ");
                sSql.Append("From Movitem ");
                sSql.Append("where cd_empresa = '");
                sSql.Append(psEmp);
                sSql.Append("'");
                sSql.Append(" and ");
                sSql.Append("nr_lanc = '");
                sSql.Append(psNrLanc);
                sSql.Append("'");
                using (FbCommand cmd = new FbCommand(sSql.ToString(), Conn))
                {
                    FbDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        if (dr["cd_contrato"].ToString() != "")
                        {
                            sOBS += string.Format("Contrato {0}",
                                                 dr["cd_contrato"].ToString());

                        }
                        if (dr["ds_obs"].ToString() != "")
                        {
                            sOBS += string.Format("{0}OBS {1}",
                                                  (sOBS != "" ? " - " : ""),
                                                  dr["ds_obs"].ToString());
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possivel gerar a OBS de Itens do Contrato/OBS, Erro.: {0}",
                                                  ex.Message));
            }
            return sOBS;
        }

        private string BuscaObsItemSimples(string psEmp, string psNrLanc, FbConnection Conn)
        {
            string sOBS = string.Empty;
            try
            {
                StringBuilder sSql = new StringBuilder();
                sSql.Append("Select ");
                sSql.Append("ds_obs ");
                sSql.Append("From Movitem ");
                sSql.Append("where cd_empresa = '");
                sSql.Append(psEmp);
                sSql.Append("'");
                sSql.Append(" and ");
                sSql.Append("nr_lanc = '");
                sSql.Append(psNrLanc);
                sSql.Append("'");
                using (FbCommand cmd = new FbCommand(sSql.ToString(), Conn))
                {
                    FbDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        if (dr["ds_obs"].ToString() != "")
                        {
                            sOBS += string.Format("{0}OBS {1}",
                                                  (sOBS != "" ? " - " : ""),
                                                  dr["ds_obs"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possivel gerar a OBS dos Itens , Erro.: {0}",
                                                  ex.Message));
            }
            return sOBS;
        }

        public string RetiraCaracterEsquerda(string sCOnteudo, char sCaracter)
        {
            string Retorno = sCOnteudo;
            int iQtCaracter = 0;

            for (int i = 0; i < Retorno.Length; i++)
            {
                if (Retorno[i] == sCaracter)
                {
                    iQtCaracter++;
                }
                else
                {
                    Retorno = Retorno.Remove(0, iQtCaracter);
                    break;
                }
            }

            return Retorno.Trim();
        }

        public bool bIndustrializacao(string sOperacoesValidas, FbConnection Conn)
        {
            bool bIndustrializacao = false;

            string sOperValid = "'" + sOperacoesValidas.Replace(",", "','");
            string sSql = "select first 1 ST_ESTTERC from opereve where ((ST_ESTTERC = 'S') and (cd_oper in (" + sOperValid + "')))";
            using (FbCommand cmd = new FbCommand(sSql, Conn))
            {
                string sScalar = Convert.ToString(cmd.ExecuteScalar()).Trim();
                if (sScalar.Equals("S"))
                {
                    bIndustrializacao = true;
                }
            }
            return bIndustrializacao;
        }

        private decimal BuscaDescTotal(string sEmp, string sNF, FbConnection Conn)
        {
            decimal dvlTotProd = 0;
            decimal dvlTotItens = 0;
            decimal vl_Desconto = 0;
            decimal dQTItens = 0;

            StringBuilder SqlTotProd = new StringBuilder();
            SqlTotProd.Append("Select (vl_totprod + vl_servico) vl_totprod ");
            SqlTotProd.Append("from nf ");
            SqlTotProd.Append("where ");
            SqlTotProd.Append("(nf.cd_empresa ='");
            SqlTotProd.Append(sEmp);
            SqlTotProd.Append("') and ");
            SqlTotProd.Append("(nf.cd_nfseq = '");
            SqlTotProd.Append(sNF);
            SqlTotProd.Append("') ");

            using (FbCommand cmd = new FbCommand(SqlTotProd.ToString(), Conn))
            {
                dvlTotProd = Math.Round(Convert.ToDecimal(cmd.ExecuteScalar()), 2); //Claudinei - o.s. 24248 - 26/03/2010
            }

            StringBuilder SqlTotItens = new StringBuilder();
            SqlTotItens.Append("Select sum(vl_totbruto) ");
            SqlTotItens.Append("from movitem ");
            SqlTotItens.Append("where ");
            SqlTotItens.Append("(movitem.cd_empresa ='");
            SqlTotItens.Append(sEmp);
            SqlTotItens.Append("') and ");
            SqlTotItens.Append("(movitem.cd_nfseq = '");
            SqlTotItens.Append(sNF);
            SqlTotItens.Append("') ");

            using (FbCommand cmd = new FbCommand(SqlTotItens.ToString(), Conn))
            {
                dvlTotItens = Math.Round(Convert.ToDecimal(cmd.ExecuteScalar()), 2); //Claudinei - o.s. 24248 - 26/03/2010
            }

            StringBuilder SqlQtItens = new StringBuilder();
            SqlQtItens.Append("Select count(nr_lanc) ");
            SqlQtItens.Append("from movitem ");
            SqlQtItens.Append("where ");
            SqlQtItens.Append("(movitem.cd_empresa ='");
            SqlQtItens.Append(sEmp);
            SqlQtItens.Append("') and ");
            SqlQtItens.Append("(movitem.cd_nfseq = '");
            SqlQtItens.Append(sNF);
            SqlQtItens.Append("') ");

            using (FbCommand cmd = new FbCommand(SqlQtItens.ToString(), Conn))
            {
                dQTItens = Math.Round(Convert.ToDecimal(cmd.ExecuteScalar()), 2); //Claudinei - o.s. 24248 - 26/03/2010
            }

            if (dvlTotProd < dvlTotItens)
            {
                vl_Desconto = ((dvlTotItens - dvlTotProd) / dQTItens);

            }
            belGerarXML BuscaConexao = new belGerarXML();
            if (pbIndustri)
            {
                vl_Desconto = 0;
            }
            else if (BuscaConexao.nm_Cliente == "EMEB")
            {
                vl_Desconto = 0;
            }
            return vl_Desconto;
        }

        public string MontaObsItem(string sEmp, string sNrLanc, FbConnection Conn)
        {
            string sObs = "";
            double vl_BaseIcmsRet = 0.00;
            double vl_IcmsRet = 0.00;

            StringBuilder sSql = new StringBuilder();

            sSql.Append("Select ");
            sSql.Append("First 1 ");
            sSql.Append("MOVITEM.vl_bicmssubst, ");
            sSql.Append("MOVITEM.vl_icmretsubst, ");
            sSql.Append("PRODUTO.DS_CERTIFICADONF ");
            //Tabela
            sSql.Append("From MOVITEM ");

            //Relacionamentos

            sSql.Append("inner join Produto on (Produto.cd_empresa = movitem.cd_empresa)");
            sSql.Append(" and ");
            sSql.Append("(Produto.cd_prod = movitem.cd_prod) ");

            //Where
            sSql.Append("Where ");
            sSql.Append("(movitem.cd_empresa ='");
            sSql.Append(sEmp);
            sSql.Append("')");
            sSql.Append(" and ");
            sSql.Append("(movitem.nr_lanc = '");
            sSql.Append(sNrLanc);
            sSql.Append("') ");


            FbCommand cmdObsItem = new FbCommand(sSql.ToString(), Conn);
            cmdObsItem.ExecuteNonQuery();

            FbDataReader drObsItem = cmdObsItem.ExecuteReader();

            drObsItem.Read();
            if ((drObsItem["vl_bicmssubst"].ToString() != "0") ||
                (drObsItem["vl_icmretsubst"].ToString() != "0"))
            {

                vl_BaseIcmsRet = Convert.ToDouble(drObsItem["vl_bicmssubst"].ToString());
                vl_IcmsRet = Convert.ToDouble(drObsItem["vl_icmretsubst"].ToString());

                sObs = string.Format("Base ICMS Retido: {0:C2} - Valor ICMS Retido: {1:C2}",
                                     vl_BaseIcmsRet.ToString("#0.00"),
                                     vl_IcmsRet.ToString("#0.00"));
            }
            if (drObsItem["DS_CERTIFICADONF"].ToString() != "")
            {
                if (sObs != "")
                {
                    sObs += " - " + TiraSimbolo(drObsItem["DS_CERTIFICADONF"].ToString(), "-");
                }
                else
                {
                    sObs = TiraSimbolo(drObsItem["DS_CERTIFICADONF"].ToString(), "-");
                }
            }

            return sObs;
        }

        public string MontaObsAgrup(string sEmp, string sNota, FbConnection Conn)
        {

            string sObs = "";
            StringBuilder sSql = new StringBuilder();

            //Campos do Select
            sSql.Append("Select ");
            sSql.Append("MOVITEM.cd_cfop, ");
            sSql.Append("cast(MOVITEM.vl_aliicms as numeric(15,2)) vl_aliicms, ");
            sSql.Append("cast(case when movitem.vl_redbase = 0 then ");
            sSql.Append("cast(sum(movitem.vl_totliq) as numeric(15,2)) ");
            sSql.Append("else ");
            sSql.Append("sum((movitem.vl_totliq * movitem.vl_redbase)/100) ");
            sSql.Append("end as numeric(15,2)) vl_base, ");
            sSql.Append("cast(case when movitem.vl_redbase = 0 then ");
            sSql.Append("movitem.vl_redbase ");
            sSql.Append("else ");
            sSql.Append("sum(movitem.vl_totliq - ((movitem.vl_totliq * movitem.vl_redbase)/100) ) ");
            sSql.Append("end as numeric(15,2)) Reducao, ");
            sSql.Append("cast((case when movitem.vl_redbase = 0 then ");
            sSql.Append("cast(sum(movitem.vl_totliq) as numeric(15,2)) ");
            sSql.Append("else ");
            sSql.Append("sum((movitem.vl_totliq * movitem.vl_redbase)/100) ");
            sSql.Append("end * movitem.vl_aliicms)/100 as numeric(15,2)) icms, ");
            sSql.Append("cast(case when (case when movitem.vl_redbase = 0 then ");
            sSql.Append("movitem.vl_redbase ");
            sSql.Append("else ");
            sSql.Append("sum(movitem.vl_totliq - ((movitem.vl_totliq * movitem.vl_redbase)/100) ) ");
            sSql.Append("end) = 0 then ");
            sSql.Append("0 ");
            sSql.Append("else ");
            sSql.Append("(case when movitem.vl_redbase = 0 then ");
            sSql.Append("cast(sum(movitem.vl_totliq) as numeric(15,2)) ");
            sSql.Append("else ");
            sSql.Append("sum((movitem.vl_totliq * movitem.vl_redbase)/100) ");
            sSql.Append("end) + ");
            sSql.Append("(case when movitem.vl_redbase = 0 then ");
            sSql.Append("movitem.vl_redbase ");
            sSql.Append("else ");
            sSql.Append("sum(movitem.vl_totliq - ((movitem.vl_totliq * movitem.vl_redbase)/100) ) ");
            sSql.Append("end) ");
            sSql.Append("end as numeric(15,2)) basecalcred ");

            //Tabela
            sSql.Append("From MOVITEM ");

            //Relacionamentos

            sSql.Append("INNER JOIN NF ON (NF.CD_EMPRESA = MOVITEM.CD_EMPRESA)");
            sSql.Append(" and ");
            sSql.Append("(NF.cd_nfseq = MOVITEM.cd_nfseq) ");

            //Where
            sSql.Append("Where ");
            sSql.Append("(nf.cd_empresa ='");
            sSql.Append(sEmp);
            sSql.Append("')");
            sSql.Append(" and ");
            sSql.Append("(nf.cd_nfseq = '");
            sSql.Append(sNota);
            sSql.Append("') ");

            //Group By
            sSql.Append("Group By ");
            sSql.Append("MOVITEM.cd_cfop, ");
            sSql.Append("MOVITEM.vl_aliicms, ");
            sSql.Append("MOVITEM.vl_redbase, ");
            sSql.Append("nf.st_impicms_em_subst_nf");

            FbCommand cmdObsRes = new FbCommand(sSql.ToString(), Conn);
            cmdObsRes.ExecuteNonQuery();

            FbDataReader drObsRes = cmdObsRes.ExecuteReader();

            while (drObsRes.Read())
            {
                sObs += string.Format("CFOP {0} - Aliquota {1:P2} - Base Calculo {2:n2} - Reducao {3:n2} - ICMS {4:n2} - Base Calc Red {5:n2} ",
                                      drObsRes["cd_cfop"].ToString(),
                                      drObsRes["vl_aliicms"].ToString(),
                                      drObsRes["vl_base"].ToString(),
                                      drObsRes["Reducao"].ToString(),
                                      drObsRes["icms"].ToString(),
                                      drObsRes["BaseCalcRed"].ToString());

            }
            return sObs.Trim();
        }

        public string BuscaSitTriIPI(string STrib,
                                     string sTpDoc,
                                     string sCalcula,
                                     string sVlIPI)
        {
            string sRetorno = "00";
            if (sTpDoc == "NS")
            {
                if (sCalcula == "N")
                {
                    sRetorno = "53";
                }
                else
                {
                    switch (STrib)
                    {
                        case "1":
                            {
                                if (sVlIPI != "0")
                                {
                                    sRetorno = "50";
                                }
                                else
                                {
                                    sRetorno = "51";
                                }
                                break;
                            }
                        case "2":
                            {
                                sRetorno = "52";
                                break;
                            }
                        case "3":
                            {
                                sRetorno = "99";
                                break;
                            }

                    }
                }

            }
            else
            {
                if (sCalcula == "N")
                {
                    sRetorno = "03";
                }
                else
                {
                    switch (STrib)
                    {
                        case "1":
                            {
                                sRetorno = "00";
                                break;
                            }
                        case "2":
                            {
                                sRetorno = "02";
                                break;
                            }
                        case "3":
                            {
                                sRetorno = "49";
                                break;
                            }

                    }

                }
            }
            return sRetorno;
        }

        /// <summary>
        ///  A NF SUFRAMA, OU SEJA NA TABELA "CLIFOR" OS CAMPOS "CD_SUFRAMA" IS NOT NULL E "ST_DESCSUFRAMA" = S
        /// </summary>
        /// <param name="sEmp"></param>
        /// <param name="sNF"></param>
        /// <param name="Conn"></param>
        /// <returns></returns>
        private bool VerificaNotaComSuframa(string sEmp, string sNF, FbConnection Conn)
        {
            try
            {
                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("select nf.cd_nfseq, clifor.cd_suframa,clifor.st_descsuframa ");
                sQuery.Append("from nf inner join clifor on ");
                sQuery.Append("nf.cd_clifor = clifor.cd_clifor ");
                sQuery.Append("where  nf.cd_nfseq = '" + sNF + "' and ");
                sQuery.Append("nf.cd_empresa = '" + sEmp + "'");

                using (FbCommand cmd = new FbCommand(sQuery.ToString(), Conn))
                {
                    FbDataReader dr = cmd.ExecuteReader();
                    bool bValida = false;
                    while (dr.Read())
                    {
                        if ((dr["st_descsuframa"].ToString().Equals("S")) && (dr["cd_suframa"] != null))
                        {
                            bValida = true;
                        }
                        else
                        {
                            bValida = false;
                        }
                    }
                    return bValida;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string BuscaSTICMSEMSUBSTNF(string sEmp, string sNF, FbConnection Conn)
        {
            string sRetorno = "N";

            try
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("select ");
                sb.Append("Coalesce(TPDOC.ST_IMPICMS_EM_SUBST_NF, 'N') ");
                sb.Append("from ");
                sb.Append("nf ");
                sb.Append("inner join tpdoc ");
                sb.Append("on (tpdoc.cd_tipodoc = nf.cd_tipodoc) ");
                sb.Append("Where ");
                sb.Append("nf.cd_empresa = '");
                sb.Append(sEmp);
                sb.Append("' and ");
                sb.Append("nf.cd_nfseq = '");
                sb.Append(sNF);
                sb.Append("'");

                using (FbCommand cmd = new FbCommand(sb.ToString(), Conn))
                {
                    sRetorno = cmd.ExecuteScalar().ToString();
                }


            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível buscar o conteúdo do campo ST_IMPICMS_EM_SUBST_NF, erro: ",
                                                  ex.Message));
            }

            return sRetorno;
        }

        public string BuscaSerieProd(string sEmp, string sNRLanc, FbConnection Conn)
        {
            StringBuilder sSerieProd = new StringBuilder();
            string sNrSerieProd = string.Empty;

            //Campos do Select
            sSerieProd.Append("Select ");
            sSerieProd.Append("cd_NRSerie ");

            //Tabela
            sSerieProd.Append("From SERIEPROD ");

            //Where
            sSerieProd.Append("Where ");
            sSerieProd.Append("(SERIEPROD.cd_empresa ='");
            sSerieProd.Append(sEmp);
            sSerieProd.Append("')");
            sSerieProd.Append(" and ");
            sSerieProd.Append("(SERIEPROD.nr_lanc = '");
            sSerieProd.Append(sNRLanc.Trim());
            sSerieProd.Append("') ");

            FbCommand cmdSerieProd = new FbCommand(sSerieProd.ToString(), Conn);
            cmdSerieProd.ExecuteNonQuery();

            FbDataReader drSerieProd = cmdSerieProd.ExecuteReader();

            while (drSerieProd.Read())
            {
                sNrSerieProd += drSerieProd["cd_NRSerie"].ToString().Trim() + ", ";
            }

            if (sNrSerieProd != "")
            {
                sNrSerieProd = string.Format("Numero de Serie.: {0}",
                                            sNrSerieProd.Substring(0, sNrSerieProd.Trim().Length - 1));
            }
            return sNrSerieProd;
        }

        private string MessagemTotalizaCFOP(string sEmp, string sCd_nfseq, FbConnection Conn)
        {
            try
            {
                //Conn.Open();
                string sCfopsTot = "";
                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("SELECT SUM(MOVITEM.vl_totliq) vl_totliq, MOVITEM.cd_cfop FROM MOVITEM ");
                sQuery.Append("WHERE MOVITEM.cd_nfseq = '" + sCd_nfseq + "'  AND MOVITEM.cd_empresa = '" + sEmp + "'");
                sQuery.Append("GROUP BY MOVITEM.cd_cfop");

                FbCommand cmd = new FbCommand(sQuery.ToString(), Conn);
                FbDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    sCfopsTot += "CFOP " + dr["cd_cfop"].ToString() + " = " + dr["vl_totliq"].ToString() + " - ";
                }

                if (sCfopsTot != "")
                {
                    sCfopsTot = sCfopsTot.Remove(sCfopsTot.Length - 2, 2);
                }
                return sCfopsTot;

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public string MensagemSuperSimples(string sEmp, string scdnfseq, FbConnection Conn)
        {
            string sMensagem = string.Empty;
            string StImpCredIcms = string.Empty;

            using (FbCommand cmd = new FbCommand("select Coalesce(Empresa.st_imp_credicms, 'N') st_imp_credicms from Empresa where Empresa.cd_empresa = '" + sEmp + "'", Conn))
            {
                if (Conn.State != ConnectionState.Open)
                {
                    Conn.Open();
                }
                StImpCredIcms = Convert.ToString(cmd.ExecuteScalar()).Trim();
            }

            if (StImpCredIcms == "S")
            {
                StringBuilder sSql = new StringBuilder();

                //Campos do Select
                sSql.Append("Select ");
                sSql.Append("movitem.vl_alicredicms, ");
                sSql.Append("sum(movitem.vl_credicms) vl_credicms ");
                //Tabela
                sSql.Append("from movitem ");

                //Relacionamentos

                //Where
                sSql.Append("Where ");
                sSql.Append("(movitem.cd_empresa ='");
                sSql.Append(sEmp);
                sSql.Append("') ");
                sSql.Append(" and ");
                sSql.Append("(movitem.cd_nfseq ='");
                sSql.Append(scdnfseq);
                sSql.Append("') ");
                sSql.Append("group by movitem.vl_alicredicms");

                FbCommand cmd = new FbCommand(sSql.ToString(), Conn);
                cmd.ExecuteNonQuery();

                FbDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if ((dr["vl_credicms"] != null) && (dr["vl_alicredicms"] != null)) //os_25182
                    {
                        sMensagem = string.Format("PERMITE O APROVEITAMENTO DE CREDITO DE ICMS NO VALOR DE R$ {0:C2}, CORRESPONDENTE ALIQUOTA DE {1:P2}% NOS TERMOS DO ARTIGO 23 DA LEI No 123/06",
                                                  dr["vl_credicms"].ToString(),
                                                  dr["vl_alicredicms"].ToString());
                    }
                }
            }

            return sMensagem;
        }

        private bool VerificaItemEntraTotalNf(string st_servico, string cd_oper, string st_soma_dev_tot_nf, string st_compoe_vl_totprod_nf, FbConnection Conn)
        {
            bool bEntra = false;
            try
            {
                string clr1301 = "";
                string clr1364 = "";
                using (FbCommand cmd = new FbCommand("select coalesce(control.cd_conteud,'')cd_conteudn from control where control.cd_nivel = '1301'", Conn))
                {
                    clr1301 = Convert.ToString(cmd.ExecuteScalar()).Trim();
                }
                using (FbCommand cmd = new FbCommand("select coalesce(control.cd_conteud,'') from control where control.cd_nivel = '1364'", Conn))
                {
                    clr1364 = Convert.ToString(cmd.ExecuteScalar()).Trim();
                }

                //(SOMA DEVOLUCAO NO TOTAL DA NOTA FISCAL)
                if ((st_soma_dev_tot_nf.Equals("S")) && (cd_oper != clr1301))
                {
                    bEntra = true;
                }
                else if ((st_compoe_vl_totprod_nf.Equals("P")) && (cd_oper != clr1364) && (cd_oper != clr1301) && (st_servico.Equals("N")))
                {
                    bEntra = true;
                }
                else if ((st_compoe_vl_totprod_nf.Equals("D")) && (cd_oper.Equals(clr1301)))
                {
                    bEntra = true;
                }
                else if ((st_compoe_vl_totprod_nf.Equals("A")) && (st_servico.Equals("N")))
                {
                    bEntra = true;
                }
                else if ((st_servico.Equals("S")))
                {
                    bEntra = true;
                }
                else
                {
                    bEntra = false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bEntra;

        }

        public string GeraChaveDadosNfe(int i, XmlDocument xml)
        {
            string scUF = ""; //--------------------------------emit> ok
            string stpEmis = ""; // --------------------------- <ide>  ok
            string sCNPJ = ""; // -----------------------------<emit> ok
            string svNF = "";  //---------------------------- <total> ok
            string sICMSp = "0"; //verificar
            string sICMSs = "0"; // verificar
            string sDD = ""; // ------------------------------- <ide> ok 
            string sDV = "";

            string sDadosNfe = "";

            XmlNodeList xNdIde = xml.GetElementsByTagName("ide");
            XmlNodeList xNdEmit = xml.GetElementsByTagName("emit");
            XmlNodeList xNdenderEmit = xml.GetElementsByTagName("enderEmit");
            XmlNodeList xndICMStot = xml.GetElementsByTagName("ICMSTot");
            XmlNodeList xNdImposto = xml.GetElementsByTagName("imposto");
            XmlNodeList xNdICMS = null;

            // verifico qtas tags existem do mesmo tipo dentro do XML

            for (int j = 0; j < xNdIde[i].ChildNodes.Count; j++)
            {
                switch (xNdIde[i].ChildNodes[j].LocalName)
                {
                    case "tpEmis": stpEmis = xNdIde[i].ChildNodes[j].InnerText;
                        break;
                    case "dEmi": sDD = Convert.ToDateTime(xNdIde[i].ChildNodes[j].InnerText).Day.ToString();
                        break;
                    case "cUF": scUF = xNdIde[i].ChildNodes[j].InnerText;
                        break;
                }
            }

            for (int j = 0; j < xNdEmit[i].ChildNodes.Count; j++)
            {
                switch (xNdEmit[i].ChildNodes[j].LocalName)
                {
                    case "CNPJ": { sCNPJ = xNdEmit[i].ChildNodes[j].InnerText; }
                        break;
                }
            }

            for (int j = 0; j < xndICMStot[i].ChildNodes.Count; j++)
            {
                switch (xndICMStot[i].ChildNodes[j].LocalName)
                {
                    case "vNF":
                        {
                            svNF = xndICMStot[i].ChildNodes[j].InnerText.Replace(".", "").PadLeft(14, '0');
                        }
                        break;
                }
            }



            switch (xNdImposto[i].ChildNodes[i].FirstChild.Name.Replace("ICMS", ""))
            {
                case "00":
                    {
                        sICMSp = "1";
                        sICMSs = "0";

                    }
                    break;
                case "10":
                    {
                        sICMSp = "1";
                        sICMSs = "1";
                    }
                    break;
                case "20":
                    {
                        sICMSp = "1";
                        sICMSs = "0";
                    }
                    break;
                case "30":
                    {
                        sICMSp = "0";
                        sICMSs = "1";
                    }
                    break;
                case "40":
                    {
                        sICMSp = "0";
                        sICMSs = "0";
                    }
                    break;
                case "51":
                    {
                        sICMSp = "1";
                        sICMSs = "0";
                    }
                    break;
                case "60":
                    {
                        xNdICMS = xml.GetElementsByTagName(xNdImposto[i].ChildNodes[i].FirstChild.Name);

                        for (int j = 0; j < xNdICMS[i].ChildNodes.Count; j++)
                        {
                            switch (xNdICMS[i].ChildNodes[j].LocalName)
                            {
                                case "vICMS":
                                    {
                                        sICMSp = xNdICMS[i].ChildNodes[j].InnerText;
                                    }
                                    break;
                                case "vICMSST":
                                    {
                                        sICMSs = xNdICMS[i].ChildNodes[j].InnerText;
                                    }
                                    break;
                            }
                        }
                        sICMSs = (sICMSs != "0" ? "1" : "0");
                        sICMSp = (sICMSp != "0" ? "1" : "0");
                    }
                    break;
                case "70":
                    {
                        sICMSp = "1";
                        sICMSs = "1";
                    }
                    break;
                case "90":
                    {
                        xNdICMS = xml.GetElementsByTagName(xNdImposto[i].ChildNodes[i].FirstChild.Name);

                        for (int j = 0; j < xNdICMS[i].ChildNodes.Count; j++)
                        {
                            switch (xNdICMS[i].ChildNodes[j].LocalName)
                            {
                                case "vICMS":
                                    {
                                        sICMSp = xNdICMS[i].ChildNodes[j].InnerText;
                                    }
                                    break;
                                case "vICMSST":
                                    {
                                        sICMSs = xNdICMS[i].ChildNodes[j].InnerText;
                                    }
                                    break;
                            }
                        }
                        sICMSs = (sICMSs != "0" ? "1" : "0");
                        sICMSp = (sICMSp != "0" ? "1" : "0");
                    }
                    break;
            }
            sDadosNfe = scUF + stpEmis + sCNPJ + svNF + sICMSp + sICMSs + sDD;

            string sDig = CalculaDig11(sDadosNfe).ToString();

            return (sDadosNfe + sDig).Trim();
        }

        public string GeraChave(string sEmp, string sNF, FbConnection Conn)
        {

            StringBuilder sSql = new StringBuilder();
            sSql.Append("Select ");
            sSql.Append("uf.nr_ufnfe, ");
            sSql.Append("coalesce(nf.cd_serie, 1) serie, ");
            sSql.Append("nf.cd_notafis nNF, ");
            sSql.Append("nf.dt_emi dEmi, ");
            sSql.Append("empresa.cd_cgc CNPJ, ");
            sSql.Append("nf.cd_nfseq cNF ");
            sSql.Append(" From ");
            sSql.Append("NF ");
            sSql.Append("inner join empresa on (empresa.cd_empresa = nf.cd_empresa) ");
            sSql.Append("left join uf on (uf.cd_uf = empresa.cd_ufnor) ");
            sSql.Append("where ");
            sSql.Append("(nf.cd_empresa ='");
            sSql.Append(sEmp);
            sSql.Append("')");
            sSql.Append(" and ");
            sSql.Append("(nf.cd_nfseq = '");
            sSql.Append(sNF);
            sSql.Append("')");

            FbCommand sqlConsulta = new FbCommand(sSql.ToString(), Conn);
            sqlConsulta.ExecuteNonQuery();
            FbDataReader drChave = sqlConsulta.ExecuteReader();
            drChave.Read();

            string scUF, sAAmM, sCNPJ, sMod, sSerie, snNF, tpemis, scNF;
            scUF = drChave["nr_ufnfe"].ToString().PadLeft(2, '0');
            sAAmM = drChave["demi"].ToString().Replace("/", "").Substring(6, 2).ToString() +
                    drChave["demi"].ToString().Replace("/", "").Substring(2, 2).ToString();
            sCNPJ = TiraSimbolo(drChave["cnpj"].ToString(), "");
            sCNPJ = sCNPJ.PadLeft(14, '0');
            sMod = "55";
            tpemis = this.sFormaEmiNFe;

            Globais objGlobais = new Globais();

            if (Convert.ToBoolean(objGlobais.LeRegConfig("AtivaModuloScan")))
            {
                sSerie = objGlobais.LeRegConfig("SerieScan");
            }
            else if (IsNumeric(drChave["serie"].ToString()))
            {
                sSerie = drChave["serie"].ToString().PadLeft(3, '0');
            }
            else
            {
                sSerie = "001";
            }
            snNF = drChave["nNF"].ToString().PadLeft(9, '0');
            scNF = drChave["cNF"].ToString().PadLeft(8, '0');

            string sChaveantDig = "";
            string sChave = "";
            string sDig = "";

            sChaveantDig = scUF.Trim() + sAAmM.Trim() + sCNPJ.Trim() + sMod.Trim() + sSerie.Trim() + snNF.Trim() + iStatusAtualSistema + scNF.Trim();//+ tpemis + scNF.Trim();
            sDig = CalculaDig11(sChaveantDig).ToString();

            sChave = sChaveantDig + sDig;

            return sChave;
        }

        public int CalculaDig11(string sChave)
        {

            int iDig = 0;
            int iMult = 2;
            int iTotal = 0;

            for (int i = (sChave.Length - 1); i >= 0; i--)
            {
                iTotal += Convert.ToInt32(sChave[i].ToString()) * iMult;
                iMult++;
                if (iMult > 9)//Danner - o.s. 29/10/2009
                {
                    iMult = 2;
                }

            }
            int iresto = (iTotal % 11);
            if ((iresto == 0) || (iresto == 1))
            {
                iDig = 0;
            }
            else
            {
                iDig = 11 - iresto;
            }
            return iDig;
        }

        static bool IsNumeric(object Expression)
        {
            // Variable to collect the Return value of the TryParse method.
            bool isNum;

            // Define variable to collect out parameter of the TryParse method. If the conversion fails, the out parameter is zero.
            double retNum;

            // The TryParse method converts a string in a specified style and culture-specific format to its double-precision floating point number equivalent.
            // The TryParse method does not generate an exception if the conversion fails. If the conversion passes, True is returned. If it does not, False is returned.
            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        public string CarregarDadosXml(string sarq)
        {
            string path = this.GetType().Assembly.Location.Replace(this.GetType().Assembly.ManifestModule.Name, "") + "\\Config\\" + HLP.bel.belStatic.sConfig;
            string scaminho = "";

            if (File.Exists(path))
            {
                XmlTextReader reader = new XmlTextReader(path);
                while (reader.Read())
                {
                    if ((reader.NodeType != XmlNodeType.Element) || !(reader.Name == "nfe_configuracoes"))
                    {
                        continue;
                    }
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            if (reader.Name == sarq)
                            {
                                reader.Read();
                                scaminho = reader.Value;
                                break;
                            }
                        }
                    }
                    reader.Close();
                }
            }
            return scaminho + "\\";
        }

        /// <summary>
        /// Gera o numero do Lote.
        /// </summary>
        /// <param name="sEmp"></param>
        /// <returns></returns>
        public string NomeArqNFe(string sEmp)
        {
            string sNomeArqNfe = "";

            belGerarXML BuscaGen = new belGerarXML();
            sNomeArqNfe = "Nfe_" + sEmp + BuscaGen.RetornaGenString("SP_CHAVEPRI", 15) + ".xml";


            return sNomeArqNfe;
        }

        public string TiraSimbolo(string sString, string sIgnorar)
        {
            sString = sString.Replace("\\viewkind4\\uc1\\pard\\f0\\fs16 ", "");
            sString = sString.Replace("\\f1\\'c7", "C");
            sString = sString.Replace("\\'c3", "A");
            sString = sString.Replace("\\f0 ", "");
            sString = sString.Replace("\\par", " ");
            sString = sString.Replace("}\0", "");
            sString = sString.Replace("\\f0", "");

            //Claudinei - o.s. 23615 - 07/08/2009
            sString = sString.Replace("{\\colortbl ;\\red0\\green0\\blue255;}\\viewkind4\\uc1 d\\cf1\\lang1046\\fs16   ", "");
            sString = sString.Replace("\\'ba", "o");
            sString = sString.Replace("\\f1", "");
            //Fim - Claudinei - o.s. 23615 - 07/08/2009

            //Claudinei - o.s. sem - 28-08-2009
            sString = sString.Replace("\\'cd", "I");
            //Fim Claudinei - o.s. sem - 28-08-2009

            //Claudinei - o.s. sem - 24/08/2009
            sString = sString.Replace("\\viewkind4\\uc1 d\\b\\fs16 ", "");
            sString = sString.Replace("\\'aa", "a");
            sString = sString.Replace("\\'e1", "a");
            sString = sString.Replace("\\'e7\\'e3", "ca");
            sString = sString.Replace("\\b0", ".");

            //Fim - Claudinei - o.s. sem - 24/08/2009

            //Claudinei - o.s. sem - 01/09/2009

            sString = sString.Replace("\\'e3", "a");
            sString = sString.Replace("\\'ea", "e");
            //Fim - Claudinei - o.s. sem - 01/09/2009

            sString = sString.Replace("\\c9", "E");


            string[,] sSimbolos = {   
                                    { "á", "a" },
                                    { "Á", "A" },
                                    { "é", "e" },
                                    { "É", "E" },
                                    { "í", "i" },
                                    { "Í", "I" },
                                    { "ó", "o" },
                                    { "Ó", "O" },
                                    { "ú", "u" },
                                    { "Ú", "U" },
                                    { "ã", "a" },
                                    { "Ã", "A" },
                                    { "õ", "o" },
                                    { "Õ", "O" },
                                    { "â", "a" },
                                    { "Â", "A" },
                                    { "ê", "e" },
                                    { "Ê", "E" },
                                    { "ô", "o" },
                                    { "Ô", "O" },
                                    { "/", "" },
                                    { "ç", "c" },
                                    { "Ç", "C" },
                                    { "-", "" },
                                    { "  ", "" },
                                    { ".", "" },
                                    { "(", "" },
                                    { ")", "" },
                                    { "°", "o" },
                                    { "�", " "},
                                    { "&", "E"},
                                    { "*", ""},
                                    { "º", "o"},
                                    { "\"", ""},
                                    { "Ø", ""},
                                    { "'", ""}                                    
                                };

            string Resultado = "";
            string sCar = "";

            for (int i = 0; i <= (sString.Length - 1); i++)
            {
                sCar = sString[i].ToString();
                for (int y = 0; y <= (sSimbolos.GetLength(0) - 1); y++)
                {
                    if ((sCar == sSimbolos[y, 0]) && (sCar != sIgnorar))
                    {
                        sString = sString.Replace(sCar, sSimbolos[y, 1]);
                    }
                }

            }

            //Claudinei - o.s. sem - 02/03/2010
            while (sString.Contains("  "))
            {
                sString = sString.Replace("  ", " ");

            }
            //Fim - Claudinei - o.s. sem - 02/03/2010

            Resultado = sString;

            return Resultado;
        }

        public string TiraSimbolo(string sString, string sIgnorar_1, string sIgnorar_2)
        {
            sString = sString.Replace("\\viewkind4\\uc1\\pard\\f0\\fs16 ", "");
            sString = sString.Replace("\\f1\\'c7", "C");
            sString = sString.Replace("\\'c3", "A");
            sString = sString.Replace("\\f0 ", "");
            sString = sString.Replace("\\par", " ");
            sString = sString.Replace("}\0", "");
            sString = sString.Replace("\\f0", "");

            //Claudinei - o.s. 23615 - 07/08/2009
            sString = sString.Replace("{\\colortbl ;\\red0\\green0\\blue255;}\\viewkind4\\uc1 d\\cf1\\lang1046\\fs16   ", "");
            sString = sString.Replace("\\'ba", "o");
            sString = sString.Replace("\\f1", "");
            //Fim - Claudinei - o.s. 23615 - 07/08/2009

            //Claudinei - o.s. sem - 28-08-2009
            sString = sString.Replace("\\'cd", "I");
            //Fim Claudinei - o.s. sem - 28-08-2009

            //Claudinei - o.s. sem - 24/08/2009
            sString = sString.Replace("\\viewkind4\\uc1 d\\b\\fs16 ", "");
            sString = sString.Replace("\\'aa", "a");
            sString = sString.Replace("\\'e1", "a");
            sString = sString.Replace("\\'e7\\'e3", "ca");
            sString = sString.Replace("\\b0", ".");

            //Fim - Claudinei - o.s. sem - 24/08/2009

            //Claudinei - o.s. sem - 01/09/2009

            sString = sString.Replace("\\'e3", "a");
            sString = sString.Replace("\\'ea", "e");
            //Fim - Claudinei - o.s. sem - 01/09/2009

            sString = sString.Replace("\\c9", "E");


            string[,] sSimbolos = {   
                                    { "á", "a" },
                                    { "Á", "A" },
                                    { "é", "e" },
                                    { "É", "E" },
                                    { "í", "i" },
                                    { "Í", "I" },
                                    { "ó", "o" },
                                    { "Ó", "O" },
                                    { "ú", "u" },
                                    { "Ú", "U" },
                                    { "ã", "a" },
                                    { "Ã", "A" },
                                    { "õ", "o" },
                                    { "Õ", "O" },
                                    { "â", "a" },
                                    { "Â", "A" },
                                    { "ê", "e" },
                                    { "Ê", "E" },
                                    { "ô", "o" },
                                    { "Ô", "O" },
                                    { "/", "" },
                                    { "ç", "c" },
                                    { "Ç", "C" },
                                    { "-", "" },
                                    { "  ", "" },
                                    { ".", "" },
                                    { "(", "" },
                                    { ")", "" },
                                    { "°", "o" },
                                    { "�", " "},
                                    { "&", "E"},
                                    { "*", ""},
                                    { "º", "o"},
                                    { "\"", ""},
                                    { "Ø", ""},
                                    { "'", ""}                                    
                                };

            string Resultado = "";
            string sCar = "";

            for (int i = 0; i <= (sString.Length - 1); i++)
            {
                sCar = sString[i].ToString();
                for (int y = 0; y <= (sSimbolos.GetLength(0) - 1); y++)
                {
                    if ((sCar == sSimbolos[y, 0]) && ((sCar != sIgnorar_1) && (sCar != sIgnorar_2)))
                    {
                        sString = sString.Replace(sCar, sSimbolos[y, 1]);
                    }
                }

            }

            //Claudinei - o.s. sem - 02/03/2010
            while (sString.Contains("  "))
            {
                sString = sString.Replace("  ", " ");
            }
            //Fim - Claudinei - o.s. sem - 02/03/2010

            Resultado = sString;

            return Resultado;
        }

        public string RetornaBlob(StringBuilder sComando, string sEmp, FbConnection Conn, belGerarXML objbelGeraXml)
        {
            string texto = "";
            FbCommand comando = new FbCommand(sComando.ToString(), Conn);
            FbDataReader Reader = comando.ExecuteReader();
            Byte[] blob = null;
            MemoryStream ms = new MemoryStream();
            while (Reader.Read())
            {
                blob = new Byte[(Reader.GetBytes(0, 0, null, 0, int.MaxValue))];
                try
                {
                    Reader.GetBytes(0, 0, blob, 0, blob.Length);
                }
                catch
                {
                    texto = "";
                    //return texto;
                }


                ms = new MemoryStream(blob);

            }

            StreamReader Ler = new StreamReader(ms);
            Ler.ReadLine();
            while (Ler.Peek() != -1)
            {
                texto += Ler.ReadLine();
            }


            //Claudinei - o.s. 24078 - 04/03/2010
            if (objbelGeraXml.nm_Cliente == "MACROTEX")
            {
                string sVendedor = string.Empty;
                string sPedidoCliente = string.Empty;

                if (Conn.State != ConnectionState.Open)
                {
                    Conn.Open();
                }
                FbCommand cmd = new FbCommand(sComando.ToString().Replace("nf.ds_anota ,", ""), Conn);
                cmd.ExecuteNonQuery();
                FbDataReader dr = cmd.ExecuteReader();
                dr.Read();

                sVendedor = dr["nm_vend"].ToString();
                sPedidoCliente = dr["DS_DOCORIG"].ToString();



                if (texto == "")
                {
                    texto += string.Format("Vendedor.: {0} Pedido N.: {1}",
                                           sVendedor,
                                           sPedidoCliente);

                }
                else
                {
                    texto += string.Format(" Vendedor.: {0} Pedido N.: {1}",
                                           sVendedor,
                                           sPedidoCliente);
                }
            }
            if (((objbelGeraXml.nm_Cliente == "MOGPLAST") || (objbelGeraXml.nm_Cliente == "TSA")) && (sEmp == "003"))
            {
                string sNFOrigem = string.Empty;
                string sEmiOrigem = string.Empty;


                if (Conn.State != ConnectionState.Open)
                {
                    Conn.Open();
                }
                FbCommand cmd = new FbCommand(sComando.ToString().Replace("nf.ds_anota ,", ""), Conn);
                cmd.ExecuteNonQuery();
                FbDataReader dr = cmd.ExecuteReader();
                dr.Read();

                //Claudinei - o.s. sem - 02/03/2010
                if (dr["cd_nfseq_fat_origem"].ToString() != "")
                {
                    //Fim - Claudinei - o.s. sem - 02/03/2010
                    StringBuilder sSqlNFOrigem = new StringBuilder();
                    sSqlNFOrigem.Append("Select ");
                    sSqlNFOrigem.Append("cd_notafis, ");
                    sSqlNFOrigem.Append("dt_emi ");
                    sSqlNFOrigem.Append("From NF ");
                    sSqlNFOrigem.Append("Where nf.cd_empresa = '");
                    sSqlNFOrigem.Append("001");
                    sSqlNFOrigem.Append("'");
                    sSqlNFOrigem.Append(" and ");
                    sSqlNFOrigem.Append("cd_nfseq = '");
                    sSqlNFOrigem.Append(dr["cd_nfseq_fat_origem"].ToString());
                    sSqlNFOrigem.Append("'");

                    FbCommand cmdNFOrigem = new FbCommand(sSqlNFOrigem.ToString(), Conn);
                    cmdNFOrigem.ExecuteNonQuery();

                    FbDataReader drNFOrigem = cmdNFOrigem.ExecuteReader();

                    drNFOrigem.Read();

                    sNFOrigem = drNFOrigem["cd_notafis"].ToString();
                    sEmiOrigem = System.DateTime.Parse(drNFOrigem["dt_emi"].ToString()).ToString("dd/MM/yyyy");

                    if (texto == "")
                    {
                        texto += string.Format("DEV TOTAL REF A NF {0} DE {1}",
                                               sNFOrigem,
                                               sEmiOrigem);

                    }
                    else
                    {
                        texto += string.Format(" DEV TOTAL REF A NF {0} DE {1}",
                                               sNFOrigem,
                                               sEmiOrigem);
                    }
                }
            }
            return Util.Util.TiraCaracterEstranho(texto);
        }

        #endregion




    }



    public class AssinaNFeXml
    {
        private string msgResultado;
        private XmlDocument XMLDoc;

        public XmlDocument XMLDocAssinado
        {
            get { return XMLDoc; }
        }

        public string XMLStringAssinado
        {
            get { return XMLDoc.OuterXml; }
        }

        public string mensagemResultado
        {
            get { return msgResultado; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sPathXml"></param>
        /// <param name="sTagBusca"></param>
        /// <param name="cert"></param>
        /// <returns></returns>
        public string ConfigurarArquivo(string sPathXml, string sTagBusca, X509Certificate2 cert)
        {

            string _arquivo = sPathXml;

            if (_arquivo == null)
            {
                Console.WriteLine("\rNome de arquivo não informado...");
            }
            else
            {
                string _uri = sTagBusca;
                if (_uri == null)
                {
                    Console.WriteLine("\rURI não informada...");
                }
                else
                {
                    int resultado = Assinar(sPathXml, _uri, cert);
                    if (resultado == 0)
                    {
                    }
                    else
                    {
                        throw new Exception(mensagemResultado);
                    }
                }
            } return XMLStringAssinado;
        }
        /// <summary>
        /// Gera assinatura Digital do XML
        /// </summary>
        /// <param name="XMLString"></param>
        /// <param name="RefUri"></param>
        /// <param name="X509Cert"></param>
        /// <returns></returns>
        public int Assinar(string XMLString, string RefUri, X509Certificate2 X509Cert)
        {


            int resultado = 0;
            msgResultado = "Assinatura realizada com sucesso";
            try
            {
                //   certificado para ser utilizado na assinatura
                //
                string _xnome = "";

                bool bX509Cert = false;

                if (X509Cert != null)
                {
                    _xnome = X509Cert.Subject.ToString();
                }
                else
                {
                    bX509Cert = true;
                }
                X509Certificate2 _X509Cert = new X509Certificate2();
                X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
                X509Certificate2Collection collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindBySubjectDistinguishedName, (object)_xnome, true);

                //if (collection1.Count == 0)
                if (bX509Cert)
                {
                    resultado = 2;
                    msgResultado = "Problemas no certificado digital";
                }
                else
                {
                    // certificado ok
                    //_X509Cert = collection1[0];

                    _X509Cert = X509Cert;
                    string x;
                    x = _X509Cert.GetKeyAlgorithm().ToString();
                    // Create a new XML document.

                    XmlDocument doc = new XmlDocument();

                    // Format the document to ignore white spaces.
                    doc.PreserveWhitespace = false;

                    // Load the passed XML file using it's name.
                    try
                    {
                        doc.LoadXml(XMLString);

                        // Verifica se a tag a ser assinada existe é única
                        int qtdeRefUri = doc.GetElementsByTagName(RefUri).Count;

                        if (qtdeRefUri == 0)
                        {
                            //  a URI indicada não existe
                            resultado = 4;
                            msgResultado = "A tag de assinatura " + RefUri.Trim() + " inexiste";
                        }
                        // Exsiste mais de uma tag a ser assinada
                        else
                        {

                            if (qtdeRefUri > 1)
                            {
                                // existe mais de uma URI indicada
                                resultado = 5;
                                msgResultado = "A tag de assinatura " + RefUri.Trim() + " não é unica";

                            }
                            else
                            {
                                try
                                {
                                    //Claudinei - o.s. 23615 - 10/08/2009
                                    //for (int i = 0; i < qtdeRefUri; i++)
                                    {
                                        //Fim - Claudinei - o.s. 23615 - 10/08/2009

                                        // Create a SignedXml object.
                                        SignedXml signedXml = new SignedXml(doc);

                                        //sTipoAssinatura = _X509Cert.PrivateKey.KeySize.ToString();
                                        // Add the key to the SignedXml document 
                                        signedXml.SigningKey = _X509Cert.PrivateKey;

                                        // Create a reference to be signed
                                        Reference reference = new Reference();
                                        // pega o uri que deve ser assinada
                                        XmlAttributeCollection _Uri = doc.GetElementsByTagName(RefUri).Item(0).Attributes; //Claudinei - o.s. 23615 - 10/08/2009
                                        foreach (XmlAttribute _atributo in _Uri)
                                        {
                                            if (_atributo.Name == "Id")
                                            {
                                                reference.Uri = "#" + _atributo.InnerText;
                                            }
                                        }

                                        // Add an enveloped transformation to the reference.
                                        XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
                                        reference.AddTransform(env);

                                        XmlDsigC14NTransform c14 = new XmlDsigC14NTransform();
                                        reference.AddTransform(c14);

                                        // Add the reference to the SignedXml object.
                                        signedXml.AddReference(reference);

                                        // Create a new KeyInfo object
                                        KeyInfo keyInfo = new KeyInfo();

                                        // Load the certificate into a KeyInfoX509Data object
                                        // and add it to the KeyInfo object.
                                        keyInfo.AddClause(new KeyInfoX509Data(_X509Cert));

                                        // Add the KeyInfo object to the SignedXml object.
                                        signedXml.KeyInfo = keyInfo;

                                        signedXml.ComputeSignature();

                                        // Get the XML representation of the signature and save
                                        // it to an XmlElement object.
                                        XmlElement xmlDigitalSignature = signedXml.GetXml();

                                        // Append the element to the XML document.

                                        //Claudinei - o.s. 23581 - 07/07/2009
                                        /*
                                        string teste = "";
                                        //XmlNode xmlno = new XmlNode();
                                        foreach (XmlNode xmlno in doc)
                                        {
                                            teste = xmlno.Name.ToString();
                                        }
                                         */
                                        //Fim - Claudinei - o.s. 23581 - 07/07/2009

                                        //Danner - o.s. 23732 - 11/11/2009
                                        doc.DocumentElement.AppendChild(doc.ImportNode(xmlDigitalSignature, true));
                                        //Fim - Danner - o.s. 23732 - 11/11/2009
                                        XMLDoc = new XmlDocument();
                                        XMLDoc.PreserveWhitespace = false;
                                        XMLDoc = doc;
                                    } //Claudinei - o.s. 23615 - 10/08/2009
                                }

                                catch (Exception caught)
                                {
                                    resultado = 7;
                                    msgResultado = "Erro: Ao assinar o documento - " + caught.Message;
                                }

                            }
                        }
                    }

                    catch (Exception caught)
                    {
                        resultado = 3;
                        msgResultado = "Erro: XML mal formado - " + caught.Message;
                    }
                }
            }
            catch (Exception caught)
            {
                resultado = 1;
                msgResultado = "Erro: Problema ao acessar o certificado digital" + caught.Message;
            }

            return resultado;
        }
        public X509Certificate2 BuscaNome(string Nome)
        {
            X509Certificate2 _X509Cert = new X509Certificate2();
            try
            {
                X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
                X509Certificate2Collection collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
                X509Certificate2Collection collection2 = (X509Certificate2Collection)collection.Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, false);
                if (Nome == "")
                {
                    X509Certificate2Collection scollection = X509Certificate2UI.SelectFromCollection(collection2, "Certificado(s) Digital(is) disponível(is)", "Selecione o Certificado Digital para uso no aplicativo", X509SelectionFlag.SingleSelection);
                    if (scollection.Count == 0)
                    {
                        _X509Cert.Reset();
                        Console.WriteLine("Nenhum certificado escolhido", "Atenção");
                    }
                    else
                    {
                        _X509Cert = scollection[0];
                    }
                }
                else
                {
                    X509Certificate2Collection scollection = (X509Certificate2Collection)collection2.Find(X509FindType.FindBySubjectDistinguishedName, Nome, false);
                    if (scollection.Count == 0)
                    {
                        Console.WriteLine("Nenhum certificado válido foi encontrado com o nome informado: " + Nome, "Atenção");
                        _X509Cert.Reset();
                    }
                    else
                    {
                        _X509Cert = scollection[0];
                    }
                }
                store.Close();
                return _X509Cert;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return _X509Cert;
            }
        }
        public X509Certificate2 BuscaNroSerie(string NroSerie)
        {
            X509Certificate2 _X509Cert = new X509Certificate2();
            try
            {

                X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
                X509Certificate2Collection collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindByTimeValid, DateTime.Now, true);
                X509Certificate2Collection collection2 = (X509Certificate2Collection)collection1.Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, true);
                if (NroSerie == "")
                {
                    X509Certificate2Collection scollection = X509Certificate2UI.SelectFromCollection(collection2, "Certificados Digitais", "Selecione o Certificado Digital para uso no aplicativo", X509SelectionFlag.SingleSelection);
                    if (scollection.Count == 0)
                    {
                        _X509Cert.Reset();
                        Console.WriteLine("Nenhum certificado válido foi encontrado com o número de série informado: " + NroSerie, "Atenção");
                    }
                    else
                    {
                        _X509Cert = scollection[0];
                    }
                }
                else
                {
                    X509Certificate2Collection scollection = (X509Certificate2Collection)collection2.Find(X509FindType.FindBySerialNumber, NroSerie, true);
                    if (scollection.Count == 0)
                    {
                        _X509Cert.Reset();
                        Console.WriteLine("Nenhum certificado válido foi encontrado com o número de série informado: " + NroSerie, "Atenção");
                    }
                    else
                    {
                        _X509Cert = scollection[0];
                    }
                }
                store.Close();
                return _X509Cert;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return _X509Cert;
            }
        }



    }
}
