using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using FirebirdSql.Data.FirebirdClient;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;


namespace HLP.bel
{
    public class belNfeInutilizacao
    {
        private int _tpamp = 0;
        private string _cuf = "";
        private string _cnpj = "";
        private string _mod = "";
        private string _serie = "";
        private string _sjust = "";
        private string _nnfini = "";
        private string _nnffim = "";
        private string _pversao = "1.02";
        private string _pversaoaplic = "2.00";
        private string _id = "";
        private string _spath;
        private string _sschema;
        private string _retws;
        public string RetWs
        {
            get { return _retws; }
        }
        XNamespace pf;
        X509Certificate2 cert;
        private string _uf_empresa;

        private string NfeCabecMsg()
        {
            XmlSchemaCollection myschema = new XmlSchemaCollection();
            XmlValidatingReader reader;

            try
            {
                XNamespace nome = "http://www.portalfiscal.inf.br/nfe";
                XDocument xdoc = new XDocument(new XElement(nome + "cabecMsg", new XAttribute("versao", _pversao), new XAttribute("xmlns", "http://www.portalfiscal.inf.br/nfe"),
                                              new XElement(nome + "versaoDados", _pversaoaplic)));


                //Danner -  o.s. sem - 04/11/2009
                //  Globais getschema = new Globais();

                // XmlParserContext context = new XmlParserContext(null, null, "", XmlSpace.None);

                // reader = new XmlValidatingReader(xdoc.ToString(), XmlNodeType.Element, context);

                //myschema.Add("http://www.portalfiscal.inf.br/nfe", getschema.LeRegWin("PastaSchema") + "\\cabecMsg_v1.02.xsd");

                //reader.ValidationType = ValidationType.Schema;

                // reader.Schemas.Add(myschema);

                // while (reader.Read())
                // { }

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
        public belNfeInutilizacao(int sTpAmb, string sCoduf, string sCnpj, string sMod, string sSerie, string sJust,
                                  string sNNFini, string sNNFfim, X509Certificate2 cert, string uf_empresa, bool bModoSCAN, int iStatusAtualSistema)
        {
            Globais LeRegWin = new Globais();
            _tpamp = sTpAmb;
            _cuf = sCoduf;
            _cnpj = Util.Util.TiraSimbolo(sCnpj, "");
            _mod = sMod;
            _serie = sSerie;
            _sjust = sJust;
            _nnffim = sNNFfim;
            _nnfini = sNNFini;
            _uf_empresa = uf_empresa;
            _spath = LeRegWin.LeRegConfig("PastaProtocolos");
            _sschema = LeRegWin.LeRegConfig("PastaSchema");
            this.pf = "http://www.portalfiscal.inf.br/nfe";
            this.cert = cert;
            if ((bModoSCAN) && (iStatusAtualSistema == 3))
            {

            }
            else
            {
                buscaRetorno();
            }
        }
        private void NfeDadosMgs()
        {
            StreamReader ler = null; ;
            XmlSchemaCollection myschema = new XmlSchemaCollection();
            XmlValidatingReader reader;
            string _ano = DateTime.Now.ToString("yy");
            _id = _cuf + _ano + _cnpj + _mod + _serie + _nnfini + _nnffim;         
            try
            {

                XDocument xDados = new XDocument(new XElement(pf + "inutNFe", new XAttribute("versao", _pversaoaplic),
                                                    new XElement(pf + "infInut", new XAttribute("Id", "ID" + _id),
                                                        new XElement(pf + "tpAmb", _tpamp),
                                                        new XElement(pf + "xServ", "INUTILIZAR"),
                                                        new XElement(pf + "cUF", _cuf),
                                                        new XElement(pf + "ano", DateTime.Now.ToString("yy")),
                                                        new XElement(pf + "CNPJ", _cnpj),
                                                        new XElement(pf + "mod", _mod),
                                                        new XElement(pf + "serie",  Convert.ToInt16(_serie)),
                                                        new XElement(pf + "nNFIni", Convert.ToInt32(_nnfini)),
                                                        new XElement(pf + "nNFFin", Convert.ToInt32(_nnffim)),
                                                        new XElement(pf + "xJust", _sjust))));

                xDados.Save(_spath + "\\" + _id + "_ped_inu.xml");
                 AssinaNFeInutXml assinaInuti = new AssinaNFeInutXml();
                assinaInuti.ConfigurarArquivo(_spath + "\\" + _id + "_ped_inu.xml", "infInut", cert);

                ler = File.OpenText(_spath + "\\" + _id + "_ped_inu.xml");

                XmlParserContext context = new XmlParserContext(null, null, "", XmlSpace.None);

                reader = new XmlValidatingReader(ler.ReadToEnd().ToString(), XmlNodeType.Element, context);

                myschema.Add("http://www.portalfiscal.inf.br/nfe", _sschema + "\\inutNFe_v2.00.xsd");

                reader.ValidationType = ValidationType.Schema;

                reader.Schemas.Add(myschema);

                while (reader.Read())
                { }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                ler.Close();
            }
        }


        private void buscaRetorno()
        {
            NfeDadosMgs();
            string sNfeDados = File.OpenText(_spath + "\\" + _id + "_ped_inu.xml").ReadToEnd();

            // Diego - O.S 24489 - 26/05/2010
            switch (_uf_empresa)
            {
                case "SP":
                    {
                        if (_tpamp == 1)
                        {                     
                            HLP.WebService.v2_Producao_NFeInutilizacao_SP.nfeCabecMsg cabec = new HLP.WebService.v2_Producao_NFeInutilizacao_SP.nfeCabecMsg();
                            HLP.WebService.v2_Producao_NFeInutilizacao_SP.NfeInutilizacao2 ws2 = new HLP.WebService.v2_Producao_NFeInutilizacao_SP.NfeInutilizacao2();
                            belUF objbelUf = new belUF();
                            cabec.cUF = objbelUf.RetornaCUF(_uf_empresa);
                            cabec.versaoDados = "2.00";
                            ws2.ClientCertificates.Add(cert);
                            ws2.nfeCabecMsgValue = cabec;

                            XmlDocument xmlInut = new XmlDocument();
                            xmlInut.LoadXml(sNfeDados);
                            XmlNode xNodeInut = xmlInut.DocumentElement;
                            _retws = ws2.nfeInutilizacaoNF2(xNodeInut).OuterXml;


                        }
                        else
                        {
                            HLP.WebService.v2_Homologacao_NFeInutilizacao_SP.nfeCabecMsg cabec = new HLP.WebService.v2_Homologacao_NFeInutilizacao_SP.nfeCabecMsg();
                            HLP.WebService.v2_Homologacao_NFeInutilizacao_SP.NfeInutilizacao2 ws2 = new HLP.WebService.v2_Homologacao_NFeInutilizacao_SP.NfeInutilizacao2();
                            belUF objbelUf = new belUF();
                            cabec.cUF = objbelUf.RetornaCUF(_uf_empresa);
                            cabec.versaoDados = "2.00";
                            ws2.ClientCertificates.Add(cert);
                            ws2.nfeCabecMsgValue = cabec;

                            XmlDocument xmlInut = new XmlDocument();
                            xmlInut.LoadXml(sNfeDados);
                            XmlNode xNodeInut = xmlInut.DocumentElement;
                            _retws = ws2.nfeInutilizacaoNF2(xNodeInut).OuterXml;
                        }
                    }
                    break;
                case "MS":
                    {
                        if (_tpamp == 1)
                        {
                            HLP.WebService.v2_Producao_NFeInutilizacao_MS.nfeCabecMsg cabec = new HLP.WebService.v2_Producao_NFeInutilizacao_MS.nfeCabecMsg();
                            HLP.WebService.v2_Producao_NFeInutilizacao_MS.NfeInutilizacao2 ws2 = new HLP.WebService.v2_Producao_NFeInutilizacao_MS.NfeInutilizacao2();
                            belUF objbelUf = new belUF();
                            cabec.cUF = objbelUf.RetornaCUF(_uf_empresa);
                            cabec.versaoDados = "2.00";
                            ws2.ClientCertificates.Add(cert);
                            ws2.nfeCabecMsgValue = cabec;

                            XmlDocument xmlInut = new XmlDocument();
                            xmlInut.LoadXml(sNfeDados);
                            XmlNode xNodeInut = xmlInut.DocumentElement;
                            _retws = ws2.nfeInutilizacaoNF2(xNodeInut).OuterXml;
                        }
                        else
                        {
                            HLP.WebService.v2_Homologacao_NFeInutilizacao_MS.nfeCabecMsg cabec = new HLP.WebService.v2_Homologacao_NFeInutilizacao_MS.nfeCabecMsg();
                            HLP.WebService.v2_Homologacao_NFeInutilizacao_MS.NfeInutilizacao2 ws2 = new HLP.WebService.v2_Homologacao_NFeInutilizacao_MS.NfeInutilizacao2();
                            belUF objbelUf = new belUF();
                            cabec.cUF = objbelUf.RetornaCUF(_uf_empresa);
                            cabec.versaoDados = "2.00";
                            ws2.ClientCertificates.Add(cert);
                            ws2.nfeCabecMsgValue = cabec;

                            XmlDocument xmlInut = new XmlDocument();
                            xmlInut.LoadXml(sNfeDados);
                            XmlNode xNodeInut = xmlInut.DocumentElement;
                            _retws = ws2.nfeInutilizacaoNF2(xNodeInut).OuterXml;
                        }
                    }
                    break;
                case "RS":
                    {
                        if (_tpamp == 1)
                        {
                            HLP.WebService.v2_Producao_NFeInutilizacao_RS.nfeCabecMsg cabec = new HLP.WebService.v2_Producao_NFeInutilizacao_RS.nfeCabecMsg();
                            HLP.WebService.v2_Producao_NFeInutilizacao_RS.NfeInutilizacao2 ws2 = new HLP.WebService.v2_Producao_NFeInutilizacao_RS.NfeInutilizacao2();
                            belUF objbelUf = new belUF();
                            cabec.cUF = objbelUf.RetornaCUF(_uf_empresa);
                            cabec.versaoDados = "2.00";
                            ws2.ClientCertificates.Add(cert);
                            ws2.nfeCabecMsgValue = cabec;

                            XmlDocument xmlInut = new XmlDocument();
                            xmlInut.LoadXml(sNfeDados);
                            XmlNode xNodeInut = xmlInut.DocumentElement;
                            _retws = ws2.nfeInutilizacaoNF2(xNodeInut).OuterXml;
                        }
                        else
                        {
                            HLP.WebService.v2_Homologacao_NFeInutilizacao_RS.nfeCabecMsg cabec = new HLP.WebService.v2_Homologacao_NFeInutilizacao_RS.nfeCabecMsg();
                            HLP.WebService.v2_Homologacao_NFeInutilizacao_RS.NfeInutilizacao2 ws2 = new HLP.WebService.v2_Homologacao_NFeInutilizacao_RS.NfeInutilizacao2();
                            belUF objbelUf = new belUF();
                            cabec.cUF = objbelUf.RetornaCUF(_uf_empresa);
                            cabec.versaoDados = "2.00";
                            ws2.ClientCertificates.Add(cert);
                            ws2.nfeCabecMsgValue = cabec;

                            XmlDocument xmlInut = new XmlDocument();
                            xmlInut.LoadXml(sNfeDados);
                            XmlNode xNodeInut = xmlInut.DocumentElement;
                            _retws = ws2.nfeInutilizacaoNF2(xNodeInut).OuterXml;
                        }
                    }
                    break;
            }
        }

        private void buscaRetornoScan()
        {
            string sNfeCabec = NfeCabecMsg();
            NfeDadosMgs();
            string sNfeDados = File.OpenText(_spath + "\\" + _id + "_ped_inu.xml").ReadToEnd();
            if (_tpamp == 1)
            {
                HLP.WebService.v2_SCAN_Producao_NFeInutilizacao.nfeCabecMsg cabec = new HLP.WebService.v2_SCAN_Producao_NFeInutilizacao.nfeCabecMsg();
                HLP.WebService.v2_SCAN_Producao_NFeInutilizacao.NfeInutilizacao2 ws2 = new HLP.WebService.v2_SCAN_Producao_NFeInutilizacao.NfeInutilizacao2();
                belUF objbelUf = new belUF();
                cabec.cUF = objbelUf.RetornaCUF(_uf_empresa);
                cabec.versaoDados = "2.00";
                ws2.ClientCertificates.Add(cert);
                ws2.nfeCabecMsgValue = cabec;

                XmlDocument xmlInut = new XmlDocument();
                xmlInut.LoadXml(sNfeDados);
                XmlNode xNodeInut = xmlInut.DocumentElement;
                _retws = ws2.nfeInutilizacaoNF2(xNodeInut).OuterXml;
            }
            else
            {
                HLP.WebService.v2_SCAN_Homologacao_NFeInutilizacao.nfeCabecMsg cabec = new HLP.WebService.v2_SCAN_Homologacao_NFeInutilizacao.nfeCabecMsg();
                HLP.WebService.v2_SCAN_Homologacao_NFeInutilizacao.NfeInutilizacao2 ws2 = new HLP.WebService.v2_SCAN_Homologacao_NFeInutilizacao.NfeInutilizacao2();
                belUF objbelUf = new belUF();
                cabec.cUF = objbelUf.RetornaCUF(_uf_empresa);
                cabec.versaoDados = "2.00";
                ws2.ClientCertificates.Add(cert);
                ws2.nfeCabecMsgValue = cabec;

                XmlDocument xmlInut = new XmlDocument();
                xmlInut.LoadXml(sNfeDados);
                XmlNode xNodeInut = xmlInut.DocumentElement;
                _retws = ws2.nfeInutilizacaoNF2(xNodeInut).OuterXml;
            }
        }
    }
    public class AssinaNFeInutXml
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
