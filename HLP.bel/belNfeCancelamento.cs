using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using FirebirdSql.Data.FirebirdClient;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
//Danner - o.s. SEM - 03/11/2009
namespace HLP.bel
{
    public class belNfeCancelamento
    {
        private string _versao;
        private string _id;
        private string _tpamb;
        private string _xserv;
        private string _chnfe;
        private string _xjust;
        private string _versaodados;
        private string _nprot;
        private string _emp;
        private string _seq;
        private string _spath;
        private string _sschema;
        private string _sret;
        public string Sret
        {
            get { return _sret; }
        }
        private X509Certificate2 _cert;

        public belNfeCancelamento(string versao, string tpamb, string xserv, string xjust, string versaodados, string sEmp, string sSeq,
                                  X509Certificate2 xCert, string UF_Empresa, bool bModoSCAN, int iStatusAtualSistema)
        {
            this._versao = versao;
            this._emp = sEmp;
            this._tpamb = tpamb;
            this._xserv = xserv;
            this._seq = sSeq;
            this._xjust = xjust;
            this._versaodados = versaodados;
            Globais caminho = new Globais();
            this._spath = caminho.LeRegConfig("PastaProtocolos");
            this._sschema = caminho.LeRegConfig("PastaSchema");
            this._cert = xCert; 

            if ((bModoSCAN)&&(iStatusAtualSistema == 3))
            {
                buscaRetornoSCAN(UF_Empresa);
            }
            else
            {
                buscaRetorno(UF_Empresa);
            }
        }
        private string geraChaveCanc(string sEmp, string sSeq)
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
            sSql.Append(sSeq);
            sSql.Append("')");

            belGerarXML BuscaConexao = new belGerarXML();

            FbConnection Conn = BuscaConexao.Conn;

            Conn.Open();

            FbCommand sqlConsulta = new FbCommand(sSql.ToString(), Conn);
            sqlConsulta.ExecuteNonQuery();



            FbDataReader drChave = sqlConsulta.ExecuteReader();
            drChave.Read();

            GeraXMLExp objgeraxmlexp = new GeraXMLExp();

            string scUF, sAAmM, sCNPJ, sMod, sSerie, snNF, scNF;
            scUF = drChave["nr_ufnfe"].ToString().PadLeft(2, '0');
            sAAmM = drChave["demi"].ToString().Replace("/", "").Substring(6, 2).ToString() +
                    drChave["demi"].ToString().Replace("/", "").Substring(2, 2).ToString();
            sCNPJ = objgeraxmlexp.TiraSimbolo(drChave["cnpj"].ToString(), "");
            sCNPJ = sCNPJ.PadLeft(14, '0');
            sMod = "55";

            if (IsNumeric(drChave["serie"].ToString()))
            {
                sSerie = drChave["serie"].ToString().PadLeft(3, '0');
            }
            else
            {
                sSerie = "001";
            }
            snNF = drChave["nNF"].ToString().PadLeft(9, '0');
            scNF = drChave["cNF"].ToString().PadLeft(9, '0');

            string sChaveantDig = "";
            string sChave = "";
            string sDig = "";

            sChaveantDig = scUF.Trim() + sAAmM.Trim() + sCNPJ.Trim() + sMod.Trim() + sSerie.Trim() + snNF.Trim() + scNF.Trim();
            sDig = objgeraxmlexp.CalculaDig11(sChaveantDig).ToString();

            sChave = sChaveantDig + sDig;

            Conn.Close();


            return sChave;
        }

        private bool IsNumeric(object Expression)
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
        /// <summary>
        /// Busca Retorna Web Service do Estado
        /// </summary>
        /// <param name="UF_Empresa"></param>
        private void buscaRetorno(string UF_Empresa)
        {
           
            AssinaNFeXml BC = new AssinaNFeXml();
            NfeDadosMsg();
            string sDados = File.OpenText(_spath + "\\" + _seq + "_ped-can.xml").ReadToEnd();

            string sRet = string.Empty;
           
            switch (UF_Empresa)
            {
                case "SP":
                    {
                        #region Regiao_SP
                        if (_tpamb == "1")
                        {
                            HLP.WebService.v2_Producao_NFeCancelamento_SP.NfeCancelamento2 ws2 = new HLP.WebService.v2_Producao_NFeCancelamento_SP.NfeCancelamento2();
                            HLP.WebService.v2_Producao_NFeCancelamento_SP.nfeCabecMsg cabec = new HLP.WebService.v2_Producao_NFeCancelamento_SP.nfeCabecMsg();

                            cabec.versaoDados = _versaodados;
                            belUF objbelUf = new belUF();
                            cabec.cUF = objbelUf.RetornaCUF(UF_Empresa);
                            ws2.ClientCertificates.Add(_cert);
                            ws2.nfeCabecMsgValue = cabec;
                            XmlDocument xmlCanc = new XmlDocument();
                            xmlCanc.LoadXml(sDados);
                            XmlNode xNodeCanc = xmlCanc.DocumentElement;
                            _sret = ws2.nfeCancelamentoNF2(xNodeCanc).OuterXml;

                        }
                        if (_tpamb == "2")
                        {
                            HLP.WebService.v2_Homologacao_NFeCancelamento_SP.NfeCancelamento2 ws2 = new HLP.WebService.v2_Homologacao_NFeCancelamento_SP.NfeCancelamento2();
                            HLP.WebService.v2_Homologacao_NFeCancelamento_SP.nfeCabecMsg cabec = new HLP.WebService.v2_Homologacao_NFeCancelamento_SP.nfeCabecMsg();

                            cabec.versaoDados = _versaodados;
                            belUF objbelUf = new belUF();
                            cabec.cUF = objbelUf.RetornaCUF(UF_Empresa);
                            ws2.ClientCertificates.Add(_cert);
                            ws2.nfeCabecMsgValue = cabec;
                            XmlDocument xmlCanc = new XmlDocument();
                            xmlCanc.LoadXml(sDados);
                            XmlNode xNodeCanc = xmlCanc.DocumentElement;
                            _sret = ws2.nfeCancelamentoNF2(xNodeCanc).OuterXml;

                        }
                        #endregion
                    }
                    break;
                case "MS":
                    {
                        #region Regiao_MS

                        if (_tpamb == "1")
                        {
                            HLP.WebService.v2_Producao_NFeCancelamento_MS.NfeCancelamento2 ws2 = new HLP.WebService.v2_Producao_NFeCancelamento_MS.NfeCancelamento2();
                            HLP.WebService.v2_Producao_NFeCancelamento_MS.nfeCabecMsg cabec = new HLP.WebService.v2_Producao_NFeCancelamento_MS.nfeCabecMsg();

                            cabec.versaoDados = _versaodados;
                            belUF objbelUf = new belUF();
                            cabec.cUF = objbelUf.RetornaCUF(UF_Empresa);
                            ws2.ClientCertificates.Add(_cert);
                            ws2.nfeCabecMsgValue = cabec;
                            XmlDocument xmlCanc = new XmlDocument();
                            xmlCanc.LoadXml(sDados);
                            XmlNode xNodeCanc = xmlCanc.DocumentElement;
                            _sret = ws2.nfeCancelamentoNF2(xNodeCanc).OuterXml;
                        }


                        if (_tpamb == "2")
                        {
                            HLP.WebService.v2_Homologacao_NFeCancelamento_MS.NfeCancelamento2 ws2 = new HLP.WebService.v2_Homologacao_NFeCancelamento_MS.NfeCancelamento2();
                            HLP.WebService.v2_Homologacao_NFeCancelamento_MS.nfeCabecMsg cabec = new HLP.WebService.v2_Homologacao_NFeCancelamento_MS.nfeCabecMsg();

                            cabec.versaoDados = _versaodados;
                            belUF objbelUf = new belUF();
                            cabec.cUF = objbelUf.RetornaCUF(UF_Empresa);
                            ws2.ClientCertificates.Add(_cert);
                            ws2.nfeCabecMsgValue = cabec;
                            XmlDocument xmlCanc = new XmlDocument();
                            xmlCanc.LoadXml(sDados);
                            XmlNode xNodeCanc = xmlCanc.DocumentElement;
                            _sret = ws2.nfeCancelamentoNF2(xNodeCanc).OuterXml;
                        }

                        #endregion
                    }
                    break;
                case "RS":
                    {
                        #region Regiao_RS

                        if (_tpamb == "1")
                        {
                            HLP.WebService.v2_Producao_NFeCancelamento_RS.NfeCancelamento2 ws2 = new HLP.WebService.v2_Producao_NFeCancelamento_RS.NfeCancelamento2();
                            HLP.WebService.v2_Producao_NFeCancelamento_RS.nfeCabecMsg cabec = new HLP.WebService.v2_Producao_NFeCancelamento_RS.nfeCabecMsg();

                            cabec.versaoDados = _versaodados;
                            belUF objbelUf = new belUF();
                            cabec.cUF = objbelUf.RetornaCUF(UF_Empresa);
                            ws2.ClientCertificates.Add(_cert);
                            ws2.nfeCabecMsgValue = cabec;
                            XmlDocument xmlCanc = new XmlDocument();
                            xmlCanc.LoadXml(sDados);
                            XmlNode xNodeCanc = xmlCanc.DocumentElement;
                            _sret = ws2.nfeCancelamentoNF2(xNodeCanc).OuterXml;
                        }


                        if (_tpamb == "2")
                        {
                            HLP.WebService.v2_Homologacao_NFeCancelamento_RS.NfeCancelamento2 ws2 = new HLP.WebService.v2_Homologacao_NFeCancelamento_RS.NfeCancelamento2();
                            HLP.WebService.v2_Homologacao_NFeCancelamento_RS.nfeCabecMsg cabec = new HLP.WebService.v2_Homologacao_NFeCancelamento_RS.nfeCabecMsg();

                            cabec.versaoDados = _versaodados;
                            belUF objbelUf = new belUF();
                            cabec.cUF = objbelUf.RetornaCUF(UF_Empresa);
                            ws2.ClientCertificates.Add(_cert);
                            ws2.nfeCabecMsgValue = cabec;
                            XmlDocument xmlCanc = new XmlDocument();
                            xmlCanc.LoadXml(sDados);
                            XmlNode xNodeCanc = xmlCanc.DocumentElement;
                            _sret = ws2.nfeCancelamentoNF2(xNodeCanc).OuterXml;
                        }
                        #endregion
                    }
                        
                    break;
                        
            }
        }


        /// <summary>
        /// Busca Retorno no Web Service SCAN
        /// </summary>
        private void buscaRetornoSCAN(string UF_Empresa)
        {
            AssinaNFeXml BC = new AssinaNFeXml();
            NfeDadosMsg();
            string sDados = File.OpenText(_spath + "\\" + _seq + "_ped-can.xml").ReadToEnd();

            string sRet = string.Empty;

            if (_tpamb == "1")
            {
                HLP.WebService.v2_SCAN_Producao_NFeCancelamento.NfeCancelamento2 ws2 = new HLP.WebService.v2_SCAN_Producao_NFeCancelamento.NfeCancelamento2();
                HLP.WebService.v2_SCAN_Producao_NFeCancelamento.nfeCabecMsg cabec = new HLP.WebService.v2_SCAN_Producao_NFeCancelamento.nfeCabecMsg();

                cabec.versaoDados = _versaodados;
                belUF objbelUf = new belUF();
                cabec.cUF = objbelUf.RetornaCUF(UF_Empresa);
                ws2.ClientCertificates.Add(_cert);
                ws2.nfeCabecMsgValue = cabec;
                XmlDocument xmlCanc = new XmlDocument();
                xmlCanc.LoadXml(sDados);
                XmlNode xNodeCanc = xmlCanc.DocumentElement;
                _sret = ws2.nfeCancelamentoNF2(xNodeCanc).OuterXml;
            }
            if (_tpamb == "2")
            {
                HLP.WebService.v2_SCAN_Homologacao_NFeCancelamento.NfeCancelamento2 ws2 = new HLP.WebService.v2_SCAN_Homologacao_NFeCancelamento.NfeCancelamento2();
                HLP.WebService.v2_SCAN_Homologacao_NFeCancelamento.nfeCabecMsg cabec = new HLP.WebService.v2_SCAN_Homologacao_NFeCancelamento.nfeCabecMsg();

                cabec.versaoDados = _versaodados;
                belUF objbelUf = new belUF();
                cabec.cUF = objbelUf.RetornaCUF(UF_Empresa);
                ws2.ClientCertificates.Add(_cert);
                ws2.nfeCabecMsgValue = cabec;
                XmlDocument xmlCanc = new XmlDocument();
                xmlCanc.LoadXml(sDados);
                XmlNode xNodeCanc = xmlCanc.DocumentElement;
                _sret = ws2.nfeCancelamentoNF2(xNodeCanc).OuterXml;
            }
        }
               
        private void NfeDadosMsg()
        {
            XmlSchemaCollection myschema = new XmlSchemaCollection();
            XmlValidatingReader reader;
            Globais sPath = new Globais();
            try
            {
                StringBuilder sSql = new StringBuilder();
                sSql.Append("select ");
                sSql.Append("cd_chavenferet, ");
                sSql.Append("cd_nprotnfe ");
                sSql.Append("from nf ");
                sSql.Append("where ");
                sSql.Append("(cd_empresa = '");
                sSql.Append(_emp);
                sSql.Append("') ");
                sSql.Append("and ");
                sSql.Append("(cd_nfseq = '");
                sSql.Append(_seq);
                sSql.Append("')");

                belGerarXML BuscaConexao = new belGerarXML();

                FbConnection Conn = BuscaConexao.Conn;

                Conn.Open();

                FbCommand cmdcanc = new FbCommand(sSql.ToString(), Conn);
                cmdcanc.ExecuteNonQuery();



                FbDataReader drCanc = cmdcanc.ExecuteReader();
                drCanc.Read();

                _chnfe = drCanc["cd_chavenferet"].ToString();
                _nprot = drCanc["cd_nprotnfe"].ToString();
                _id = "ID" + _chnfe;

                XNamespace xname = "http://www.portalfiscal.inf.br/nfe";
                XDocument xdoc = new XDocument(new XElement(xname + "cancNFe", new XAttribute("versao", _versaodados),
                                                   new XElement(xname + "infCanc", new XAttribute("Id", _id),
                                                       new XElement(xname + "tpAmb", _tpamb),
                                                       new XElement(xname + "xServ", _xserv),
                                                       new XElement(xname + "chNFe", _chnfe),
                                                       new XElement(xname + "nProt", _nprot),
                                                       new XElement(xname + "xJust", _xjust))));
                xdoc.Save(_spath + "\\" + _seq + "_ped-can.xml");

                AssinaNFeCancXml assinaCanc = new AssinaNFeCancXml();
                assinaCanc.ConfigurarArquivo(_spath + "\\" + _seq + "_ped-can.xml", "infCanc", _cert);

                StreamReader ler;

                ler = File.OpenText(_spath + "\\" + _seq + "_ped-can.xml");



                XmlParserContext context = new XmlParserContext(null, null, "", XmlSpace.None);

                reader = new XmlValidatingReader(ler.ReadToEnd().ToString(), XmlNodeType.Element, context);

                myschema.Add("http://www.portalfiscal.inf.br/nfe", _sschema + "\\cancNFe_v2.00.xsd");

                reader.ValidationType = ValidationType.Schema;

                reader.Schemas.Add(myschema);

                while (reader.Read())
                { }

            }
            catch (Exception x)
            {

                throw new Exception(x.Message);
            }
        }
    }
    public class AssinaNFeCancXml
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

        public void ConfigurarArquivo(string sPathXml, string sTagBusca, X509Certificate2 cert)
        {

            string _arquivo = sPathXml;

            if (_arquivo == null)
            {
                Console.WriteLine("\rNome de arquivo não informado...");
            }
            else if (!File.Exists(_arquivo))
            {
                Console.WriteLine("\rArquivo {0} inexistente...", _arquivo);
            }
            else
            {
                //Console.Write("URI a ser assinada (Ex.: infCanc, infNFe, infInut, etc.) :");

                string _uri = sTagBusca;
                if (_uri == null)
                {
                    Console.WriteLine("\rURI não informada...");
                }
                else
                {
                    //
                    //   le o arquivo xml
                    //
                    StreamReader SR;
                    string _stringXml;
                    SR = File.OpenText(_arquivo);
                    _stringXml = SR.ReadToEnd();

                    //Claudinei - o.s. sem - 24/08/2009
                    //Claudinei - o.s. sem - 24/08/2009

                    //Claudinei - o.s. 23581 - 06/07/2009
                    //_stringXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine + _stringXml;
                    //Fim - Claudinei - o.s. 23581 - 06/07/2009
                    //int iPos = _stringXml.IndexOf("xmlns=\"\"");
                    //string _stringXml1 = _stringXml.Substring(0, (iPos - 1));
                    //string _stringXml2 = _stringXml.Substring(iPos + 8);
                    //_stringXml = _stringXml1 + _stringXml2;


                    SR.Close();

                    //Danner - o.s. 23851 - 19/11/2009
                    //X509Certificate2 cert = new X509Certificate2();
                    //
                    //  seleciona certificado do repositório MY do windows
                    //

                    //cert = BuscaNome("");
                    //Fim - Danner - o.s. 23851 - 19/11/2009
                    int resultado = Assinar(_stringXml, _uri, cert);
                    if (resultado == 0)
                    {

                        //
                        //  grava arquivo assinado
                        //
                        string NomeNovo = _arquivo.Substring(0, _arquivo.Length - 4) + "01.xml"; //Claudinei - o.s. 23507 - 29/06/2009
                        StreamWriter SW;
                        //SW = File.CreateText(_arquivo.Substring(0, _arquivo.Length - 4) + "01.xml");
                        SW = File.CreateText(NomeNovo);
                        //SW.Write("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>" + XMLStringAssinado.Substring(136).Replace("</enviNFe>", "")); //Claudinei - o.s. sem - 24/08/2009
                        string sXMLStringAssinado = XMLStringAssinado;
                        //sXMLStringAssinado = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + sXMLStringAssinado.Substring(136).Replace("</enviNFe>", "");
                        SW.Write(sXMLStringAssinado); //Claudinei - o.s. sem - 24/08/2009
                        SW.Close();

                        //Claudinei - o.s. 23507 - 29/06/2009
                        File.Delete(_arquivo);
                        File.Move(NomeNovo, _arquivo);
                        //Fim - Claudinei - o.s. 23507 - 29/06/2009

                    }
                    else
                    {
                        throw new Exception(mensagemResultado);
                    }
                }
            }
        }
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

                                        doc.DocumentElement.AppendChild(doc.ImportNode(xmlDigitalSignature, true));
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
//FIM - Danner - o.s. SEM - 03/11/2009