using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HLP.bel.CCe;
using HLP.bel;
using FirebirdSql.Data.FirebirdClient;
using System.Xml.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Schema;
using System.Xml;
using System.IO;
using HLP.bel.Static;
using System.Windows.Forms;
using HLP.bel.CTe;
using HLP.bel.NFe.GeraXml;

namespace HLP.Dao.CCe
{


    public class daoGeraCCe
    {
        private string _VERSAO = "1.00";
        private string _TPEVENTO = "110110";
        X509Certificate2 cert;
        public string sXMLfinal = "";
        private List<belPesquisaCCe> objlItensPesquisa = new List<belPesquisaCCe>();
        belConnection cx = new belConnection();

        public belEnvEvento objEnvEvento = new belEnvEvento();

        /// <summary>
        /// chave, xml
        /// </summary>
        public Dictionary<string, string> objXmlsSeparados = new Dictionary<string, string>();

        public daoGeraCCe()
        {
        }

        public daoGeraCCe(List<belPesquisaCCe> _objlItensPesquisa, X509Certificate2 _cert)
        {
            objlItensPesquisa = _objlItensPesquisa;
            cert = _cert;

            objEnvEvento = new belEnvEvento();

            foreach (belPesquisaCCe obj in _objlItensPesquisa)
            {
                CarregaItensEventoCartaCorrecao(obj);
            }
        }

        private void CarregaItensEventoCartaCorrecao(belPesquisaCCe objbelPesquisa)
        {
            try
            {
                objEnvEvento.versao = this._VERSAO;
                objEnvEvento.id = objbelPesquisa.CD_NRLANC;
                belEvento objEvento = new belEvento();
                objEvento.versao = _VERSAO;
                objEvento.infEvento = new _infEvento();
                objEvento.infEvento.CNPJ = objbelPesquisa.CNPJ;
                objEvento.infEvento.CPF = objbelPesquisa.CPF;
                objEvento.infEvento.dhEvento = HLP.Util.Util.GetDateServidor().ToString("yyyy-MM-ddTHH:mm:ss" + belStatic.sFuso);//objbelPesquisa.DT_LANC;
                objEvento.infEvento.verEvento = _VERSAO;
                objEvento.infEvento.tpAmb = belStatic.TpAmb;
                objEvento.infEvento.chNFe = objbelPesquisa.CHNFE;
                objEvento.infEvento.cOrgao = belStatic.cUF.ToString();
                objEvento.infEvento.detEvento = new _detEvento
                  {
                      versao = _VERSAO
                  };
                objEvento.infEvento.detEvento.xCorrecao = BuscaCorrecoes(objbelPesquisa.CD_NRLANC);
                objEvento.infEvento.nSeqEvento = objbelPesquisa.QT_ENVIO + 1;
                objEvento.idLote = "ID" + objEvento.infEvento.tpEvento + objEvento.infEvento.chNFe + objEvento.infEvento.nSeqEvento.ToString().PadLeft(2, '0');
                objEnvEvento.evento.Add(objEvento);
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

        public string BuscaCorrecoes(string sNR_LANC)
        {
            try
            {
                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("SELECT i.ds_item, i.ds_correto FROM ITCARTAC i ");
                sQuery.Append("where i.cd_carta = '" + sNR_LANC + "' ");
                sQuery.Append("and i.cd_empresa = '" + belStatic.codEmpresaNFe + "' ");
                sQuery.Append("and coalesce(i.ds_correto,'') <> '' ");
                FbCommand comand = new FbCommand(sQuery.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                FbDataReader dr = comand.ExecuteReader();
                string sXcorrecao = "";
                while (dr.Read())
                {
                    sXcorrecao += string.Format("IRREGULARIDADE: {0} - RETIFICAÇÃO: {1} |",
                                                 dr["ds_item"].ToString().ToUpper().Trim(),
                                                 dr["ds_correto"].ToString().ToUpper().Trim());
                }
                if (sXcorrecao.Length > 1)
                {
                    sXcorrecao = sXcorrecao.Remove(sXcorrecao.Length - 1, 1).Trim();
                }
                return sXcorrecao;
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

        /// <summary>
        /// Pulando de Linha
        /// </summary>
        /// <param name="sNR_LANC"></param>
        /// <returns></returns>
        public string BuscaCorrecoesPulandoLinha(string sNR_LANC)
        {
            try
            {

                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("SELECT i.ds_item, i.ds_correto FROM ITCARTAC i ");
                sQuery.Append("where i.cd_carta = '" + sNR_LANC + "' ");
                sQuery.Append("and i.cd_empresa = '" + belStatic.codEmpresaNFe + "' ");
                sQuery.Append("and coalesce(i.ds_correto,'') <> '' ");
                FbCommand comand = new FbCommand(sQuery.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                FbDataReader dr = comand.ExecuteReader();
                string sXcorrecao = "";
                while (dr.Read())
                {
                    sXcorrecao += string.Format("IRREGULARIDADE: {0} - RETIFICAÇÃO: {1} " + Environment.NewLine,
                                                 dr["ds_item"].ToString().ToUpper().Trim(),
                                                 dr["ds_correto"].ToString().ToUpper().Trim());
                }
                if (sXcorrecao.Length > 1)
                {
                    sXcorrecao = sXcorrecao.Remove(sXcorrecao.Length - 1, 1).Trim();
                }
                return sXcorrecao;

            }
            catch (Exception)
            {

                throw;
            }
            finally { cx.Close_Conexao(); }
        }

        public void GeraXmlEnvio()
        {
            try
            {
                AssinaNFeXml Assinatura = new AssinaNFeXml();
                XDocument xdoc = new XDocument();
                XNamespace pf = "http://www.portalfiscal.inf.br/nfe";
                XContainer xContEnvEvento = new XElement(pf + "envEvento", new XAttribute("xmlns", "http://www.portalfiscal.inf.br/nfe"),
                                                                    new XAttribute("versao", objEnvEvento.versao),
                                                                    new XElement(pf + "idLote", objEnvEvento.id));

                //List<string> ListaEventos = new List<string>();
                objXmlsSeparados = new Dictionary<string, string>();
                foreach (var obj in objEnvEvento.evento)
                {
                    XContainer xConEvento = new XElement(pf + "evento", new XAttribute("xmlns", "http://www.portalfiscal.inf.br/nfe"),
                                                                   new XAttribute("versao", obj.versao),
                                                                   new XElement(pf + "infEvento", new XAttribute("Id", obj.idLote),
                                                                       new XElement(pf + "cOrgao", obj.infEvento.cOrgao),
                                                                       new XElement(pf + "tpAmb", obj.infEvento.tpAmb),
                                                                       new XElement(pf + (obj.infEvento.CNPJ != "" ? "CNPJ" : "CPF"), (obj.infEvento.CNPJ != "" ? obj.infEvento.CNPJ : obj.infEvento.CPF)),
                                                                       new XElement(pf + "chNFe", obj.infEvento.chNFe),
                                                                       new XElement(pf + "dhEvento", obj.infEvento.dhEvento),//-02:00
                                                                       new XElement(pf + "tpEvento", obj.infEvento.tpEvento),
                                                                       new XElement(pf + "nSeqEvento", obj.infEvento.nSeqEvento),
                                                                       new XElement(pf + "verEvento", obj.infEvento.verEvento),
                                                                       new XElement(pf + "detEvento", new XAttribute("versao", obj.versao),
                                                                                    new XElement(pf + "descEvento", obj.infEvento.detEvento.descEvento),
                                                                                    new XElement(pf + "xCorrecao", obj.infEvento.detEvento.xCorrecao),
                                                                                    new XElement(pf + "xCondUso", obj.infEvento.detEvento.xCondUso))));

                    //   ListaEventos.Add(Assinatura.ConfigurarArquivo(xConEvento.ToString(), "infEvento", cert));

                    objXmlsSeparados.Add(obj.infEvento.chNFe, Assinatura.ConfigurarArquivo(xConEvento.ToString(), "infEvento", cert));
                }


                string sEventos = "";
                foreach (KeyValuePair<string, string> sInfEvento in objXmlsSeparados)
                {
                    sEventos += sInfEvento.Value;
                    //xContEnvEvento.Add(XElement.Parse(sInfEvento, LoadOptions.PreserveWhitespace));
                }

                sXMLfinal = "<?xml version=\"1.0\" encoding=\"utf-8\"?><envEvento xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"1.00\"><idLote>" + objEnvEvento.id.ToString()
                                + "</idLote>" + sEventos + "</envEvento>";

                #region Valida_Xml|

                HLP.bel.NFe.GeraXml.Globais getschema = new bel.NFe.GeraXml.Globais();

                XmlValidatingReader reader;
                try
                {
                    XmlSchemaCollection myschema = new XmlSchemaCollection();
                    myschema.Add("http://www.portalfiscal.inf.br/nfe", belStaticPastas.SCHEMA_CCE + "\\envCCe_v1.00.xsd");

                    XmlParserContext context = new XmlParserContext(null, null, "", XmlSpace.None);
                    reader = new XmlValidatingReader(sXMLfinal, XmlNodeType.Element, context);
                    reader.ValidationType = ValidationType.Schema;
                    reader.Schemas.Add(myschema);
                    while (reader.Read())
                    {

                    }
                }
                catch (XmlException x)
                {
                    // File.Delete(sPathLote);
                    throw new Exception(x.Message);
                }
                catch (XmlSchemaException x)
                {
                    // File.Delete(sPathLote);
                    throw new Exception(x.Message);
                }
                #endregion




            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string AnalisaRetornoEnvio(string sRet)
        {
            try
            {
                string sCabec = "";
                string sDetalhes = "";
                XElement xDoc = XElement.Parse(sRet, LoadOptions.None);
                List<Motivos> objListMotivos = new List<Motivos>();

                foreach (XElement item in xDoc.Descendants())
                {
                    switch (item.Name.LocalName.ToString())
                    {
                        case "xMotivo":
                            {
                                if (sCabec == "")
                                {
                                    sCabec = string.Format("{0} {1}{1}", item.Value.ToString(), Environment.NewLine);
                                }
                            }
                            break;
                    }
                    if (item.Name.LocalName.ToString().Equals("retEvento"))
                    {
                        Motivos objMotivo = new Motivos();
                        foreach (XElement st in item.Descendants())
                        {
                            switch (st.Name.LocalName.ToString())
                            {
                                case "xMotivo": { objMotivo.xMotivo = st.Value.ToString(); }
                                    break;
                                case "chNFe": { objMotivo.chave = st.Value.ToString(); }
                                    break;
                                case "cStat": { objMotivo.Status = st.Value.ToString(); }
                                    break;
                            }
                        }
                        objListMotivos.Add(objMotivo);

                    }
                }

                foreach (Motivos mot in objListMotivos)
                {
                    string sNota = objlItensPesquisa.FirstOrDefault(c => c.CHNFE == mot.chave).CD_NOTAFIS;
                    int iQT_ENVIO = objlItensPesquisa.FirstOrDefault(c => c.CHNFE == mot.chave).QT_ENVIO;
                    string sNR_LANC = objlItensPesquisa.FirstOrDefault(c => c.CHNFE == mot.chave).CD_NRLANC;
                    if (mot.Status == "135")
                    {
                        AtualizaContadorCCe(sNR_LANC, iQT_ENVIO);
                        SaveXmlPastaCCe(mot.chave);


                    }
                    sDetalhes += string.Format("CCe da NFe: {0} - '{1}'{2}", sNota, mot.xMotivo, Environment.NewLine);
                }

                return sCabec + sDetalhes;

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SaveXmlPastaCCe(string sChave)
        {
            try
            {
                DirectoryInfo dPastaData = new DirectoryInfo(belStaticPastas.CCe + "\\" + sChave.Substring(2, 4));
                if (!dPastaData.Exists) { dPastaData.Create(); }
                XDocument xml = XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf-8\"?>" + objXmlsSeparados.First(c => c.Key == sChave).Value, LoadOptions.PreserveWhitespace);
                xml.Save(belStaticPastas.CCe + "\\" + sChave.Substring(2, 4) + "\\" + sChave + "-cce.xml");
            }
            catch (Exception ex)
            {
                throw new Exception("O Caminho para salvar os arquivos Xmls de CCe não foi configurado!"); ;
            }
        }

        private void AtualizaContadorCCe(string _sNR_LANC, int _iQT_ENVIO)
        {
            try
            {
                string sQuery = string.Format("update cartacor set qt_envio = '{0}' where cartacor.nr_lanc = '{1}' and cartacor.cd_empresa = '{2}'", _iQT_ENVIO + 1, _sNR_LANC, belStatic.codEmpresaNFe);
                FbCommand cmd = new FbCommand(sQuery, cx.get_Conexao());
                cx.Open_Conexao();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { cx.Close_Conexao(); }
        }


       



        private struct Motivos
        {
            public string chave { get; set; }
            public string xMotivo { get; set; }
            public string Status { get; set; }

        }
    }
}

