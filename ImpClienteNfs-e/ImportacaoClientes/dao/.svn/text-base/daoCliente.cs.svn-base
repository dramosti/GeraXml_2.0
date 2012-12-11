using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using ImportacaoClientes.bel;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ImportacaoClientes.dao
{
    public class daoCliente
    {

        public List<belLogErro> objListBelErros = new List<belLogErro>();
        public belLogErro objErro = null;


        public List<Identificacao> RetornaTodosClientes(string sConexao, ProgressBar pbCliente)
        {
            FbConnection Conn = new FbConnection(sConexao);
            try
            {

                List<Identificacao> objListIdentificacao = new List<Identificacao>();
                StringBuilder sQuery = new StringBuilder();

                Conn.Open();
                sQuery.Append("Select Count(clifor.cd_clifor) from clifor Where coalesce(clifor.st_inativo,'N') = 'N'  ");
                FbCommand Command = new FbCommand(sQuery.ToString(), Conn);
                pbCliente.Maximum = Convert.ToInt32(Command.ExecuteScalar());

                sQuery = new StringBuilder();

                //Identificacao
                sQuery.Append("Select ");
                sQuery.Append(" clifor.cd_clifor,  ");
                sQuery.Append("coalesce (clifor.cd_inscrmu,'') cd_inscrmu, ");
                sQuery.Append("coalesce (clifor.nm_guerra ,'')RazaoSocial, ");
                sQuery.Append("coalesce (clifor.cd_email, '') cd_email , ");
                sQuery.Append("coalesce (clifor.cd_cgc,'')cd_cgc,  ");
                sQuery.Append("coalesce (clifor.cd_cpf,'')cd_cpf,  ");

                //Dados da Grid
                sQuery.Append("coalesce (clifor.nm_cidnor,'')nm_cidnor, ");

                //Endereco
                sQuery.Append("coalesce (clifor.ds_endnor ,'')Logradouro,  ");
                sQuery.Append("coalesce (clifor.nr_endnor,'') Numero, ");
                sQuery.Append("coalesce (clifor.nm_bairronor,'') Bairro, ");
                sQuery.Append("coalesce (cidades.cd_municipio,'')  CodigoMunicipio, ");
                sQuery.Append("clifor.nm_cidcom, ");
                sQuery.Append("coalesce (uf.nr_ufnfe,'') CodigoUf, ");
                sQuery.Append("coalesce (clifor.cd_fonenor,'') Telefone, ");
                sQuery.Append("coalesce (clifor.ds_contato,'')ds_contato, ");
                sQuery.Append("coalesce (clifor.cd_cepnor,'') Cep, ");
                sQuery.Append("coalesce (clifor.cd_pais,'')cd_pais ");
                sQuery.Append("from clifor ");
                sQuery.Append("inner join cidades on  clifor.nm_cidnor = cidades.nm_cidnor ");
                sQuery.Append("inner join uf on clifor.cd_ufnor = uf.cd_uf ");
                sQuery.Append("Where coalesce(clifor.st_inativo,'N') = 'N'  ");


                Command = new FbCommand(sQuery.ToString(), Conn);
                FbDataReader dr = Command.ExecuteReader();

                while (dr.Read())
                {
                    pbCliente.PerformStep();
                    string sRazao = dr["RazaoSocial"].ToString();
                    string sCodCliente = dr["cd_clifor"].ToString();

                    bool bCpf = true;
                    bool bCnpj = true;
                    bool bEmail = true;
                    bool bTelefone = true;
                    bool bLogradouro = true;
                    bool bNumero = true;
                    bool bBairro = true;
                    bool bCodMun = true;
                    bool bCodUf = true;
                    bool bCodPais = true;
                    bool bCep = true;

                    objErro = new belLogErro();
                    objErro.CodCliente = sCodCliente;
                    objErro.Razao = sRazao;

                    Identificacao objIdentificacao = new Identificacao();

                    #region DadosGrid

                    if (dr["nm_cidnor"].ToString() != "") { objIdentificacao.Nome_Cidade = dr["nm_cidnor"].ToString(); }
                    if (dr["cd_clifor"].ToString() != "") { objIdentificacao.Cod_Cliente = dr["cd_clifor"].ToString(); }

                    #endregion

                    #region Identificacao

                    if (dr["cd_inscrmu"].ToString() != "") { objIdentificacao.DsIm = dr["cd_inscrmu"].ToString(); }
                    if (dr["RazaoSocial"].ToString() != "") { objIdentificacao.DsRazaoSocial = dr["RazaoSocial"].ToString(); }
                    if (dr["cd_email"].ToString() != "")
                    {
                        objIdentificacao.DsEmail = dr["cd_email"].ToString();
                        bEmail = ValidaEmail(objIdentificacao.DsEmail);
                        if (bEmail == false)
                        {
                            objErro.Motivo = objErro.Motivo + "E-mail Inválido; ";
                        }
                    }
                    if (dr["cd_cgc"].ToString() != "")
                    {
                        objIdentificacao.CdConsumidor = TiraSimbolo(dr["cd_cgc"].ToString());
                        bCnpj = ValidaCnpj(objIdentificacao.CdConsumidor);
                        objIdentificacao.CdTipoEmpresa = "1";
                        if (bCnpj == false)
                        {
                            objErro.Motivo = objErro.Motivo + "Cnpj Inválido; ";
                        }

                    }
                    if (dr["cd_cpf"].ToString() != "")
                    {
                        objIdentificacao.CdTipoEmpresa = "0";
                        objIdentificacao.CdConsumidor = TiraSimbolo(dr["cd_cpf"].ToString());
                        bCpf = ValidaCpf(objIdentificacao.CdConsumidor);

                        if (bCpf == false)
                        {
                            objErro.Motivo = objErro.Motivo + "Cpf Inválido; ";
                        }
                    }


                    #endregion

                    # region Endereco

                    if (dr["Logradouro"].ToString() != "") { objIdentificacao.DsLogradouro = dr["Logradouro"].ToString(); }
                    else
                    {
                        bLogradouro = false;
                        objErro.Motivo = objErro.Motivo + "Logradouro é Obrigatório; ";
                    }

                    if (dr["Numero"].ToString() != "")
                    {
                        objIdentificacao.DsNumero = dr["Numero"].ToString();
                        bNumero = ValidaNumero(objIdentificacao.DsNumero);
                        if (bNumero == false)
                        {
                            objErro.Motivo = objErro.Motivo + "Número do Endereço Inválido; ";
                        }
                    }
                    else
                    {
                        bNumero = false;
                        objErro.Motivo = objErro.Motivo + "Número do Endereço não pode estar em branco; ";
                    }

                    if (dr["Bairro"].ToString() != "") { objIdentificacao.DsBairro = dr["Bairro"].ToString(); }
                    else
                    {
                        bBairro = false;
                        objErro.Motivo = objErro.Motivo + "Bairro é Obrigatório; ";
                    }
                    if (dr["CodigoMunicipio"].ToString() != "")
                    {
                        string sMunicipio = dr["CodigoMunicipio"].ToString();
                        if (sMunicipio.Length == 7)
                        {
                            objIdentificacao.CdMunicipioIbge = sMunicipio.Substring(2, 5);
                        }
                        else if (sMunicipio.Length == 5)
                        {
                            objIdentificacao.CdMunicipioIbge = sMunicipio;
                        }
                    }
                    else
                    {
                        bCodMun = false;
                        objErro.Motivo = objErro.Motivo + "Código do Município é Obrigatório; ";
                    }

                    if (dr["CodigoUf"].ToString() != "") { objIdentificacao.CdUfIbge = dr["CodigoUf"].ToString(); }
                    else
                    {
                        bCodUf = false;
                        objErro.Motivo = objErro.Motivo + "Código do Estado é Obrigatório;";
                    }
                    if (dr["Telefone"].ToString() != "")
                    {
                        string Telefone = TiraSimbolo(dr["Telefone"].ToString());
                        if (Telefone.Length == 10)
                        {
                            objIdentificacao.DsTelefone = Telefone.Substring(2, 8);
                            objIdentificacao.DsTelefoneDdd = Telefone.Substring(0, 2);
                        }
                        else
                        {
                            bTelefone = false;
                            objErro.Motivo = objErro.Motivo + "Telefone Inválido; ";
                        }
                    }

                    if (dr["ds_contato"].ToString() != "") { objIdentificacao.DsContato = dr["ds_contato"].ToString(); }

                    if (dr["Cep"].ToString() != "")
                    {
                        string Cep = TiraSimbolo(dr["Cep"].ToString());
                        if (Cep.Length == 8)
                        {
                            objIdentificacao.CdCepPrefixo = Cep.Substring(0, 5);
                            objIdentificacao.CdCepSufixo = Cep.Substring(5, 3);
                        }
                    }
                    else
                    {
                        bCep = false;
                        objErro.Motivo = objErro.Motivo + "Cep é Obrigatório; ";
                    }

                    if (dr["cd_pais"].ToString() != "") { objIdentificacao.CdPais = dr["cd_pais"].ToString(); }
                    else
                    {
                        bCodPais = false;
                        objErro.Motivo = objErro.Motivo + "Código do País é Obrigatório; ";
                    }

                    if (objErro.Motivo != "")
                    {
                        objListBelErros.Add(objErro);
                    }

                    if (bCnpj == true && bCpf == true && bEmail == true && bTelefone == true && bLogradouro == true && bNumero == true && bBairro == true && bCodMun == true
                        && bCodUf == true && bCodPais == true && bCep == true)
                    {
                        objListIdentificacao.Add(objIdentificacao);
                    }

                    #endregion
                }
                return objListIdentificacao;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Conn.Close();
            }

        }

        public bool CriarXml(List<Identificacao> objListIdentificacao, string sNomeArquivo, ProgressBar pbCliente)
        {
            try
            {
                pbCliente.Maximum = objListIdentificacao.Count();

                // XNamespace xsi = "Cliente.xsd";
                XNamespace ns = "http://www.w3.org/2001/XMLSchema-instance";
                XContainer Cliente = null;
                XContainer Enderecos = null;
                XContainer Endereco = null;
                XContainer Identificacao = null;


                XContainer Clientes = (new XElement("Clientes", new XAttribute(ns + "noNamespaceSchemaLocation", "C:\\Cliente.xsd"),
                                                              new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance")));

                int iIDArquivo = 1;
                int iCountArquivos = 0;
                for (int i = 0; i < objListIdentificacao.Count; i++)
                {
                    pbCliente.PerformStep();

                    Cliente = (new XElement("Cliente"));

                    Identificacao = (new XElement("Identificacao", (new XElement("cdConsumidor", objListIdentificacao[i].CdConsumidor)),
                                                                          (objListIdentificacao[i].DsIm != "" ? new XElement("dsIm", objListIdentificacao[i].DsIm) : null),
                                                                          new XElement("dsRazaoSocial", objListIdentificacao[i].DsRazaoSocial),
                                                                          (objListIdentificacao[i].DsEmail != "" ? new XElement("dsEmail", objListIdentificacao[i].DsEmail) : null),
                                                                          new XElement("cdTipoEmpresa", objListIdentificacao[i].CdTipoEmpresa)));

                    Cliente.Add(Identificacao);


                    Enderecos = (new XElement("Enderecos"));


                    Endereco = (new XElement("Endereco", new XElement("dsLogradouro", objListIdentificacao[i].DsLogradouro),
                                                                                 new XElement("dsNumero", objListIdentificacao[i].DsNumero),
                                                                                (objListIdentificacao[i].DsComplemento != "" ? new XElement("dsComplemento", objListIdentificacao[i].DsComplemento) : null),
                                                                                 new XElement("dsBairro", objListIdentificacao[i].DsBairro),
                                                                                 new XElement("cdMunicipioIbge", objListIdentificacao[i].CdMunicipioIbge),
                                                                                 new XElement("cdUfIbge", objListIdentificacao[i].CdUfIbge),
                                                                                 (objListIdentificacao[i].DsTelefone != "" ? new XElement("dsTelefone", objListIdentificacao[i].DsTelefone) : null),
                                                                                 (objListIdentificacao[i].DsContato != "" ? new XElement("dsContato", objListIdentificacao[i].DsContato) : null),
                                                                                 new XElement("cdCepPrefixo", objListIdentificacao[i].CdCepPrefixo),
                                                                                 new XElement("cdCepSufixo", objListIdentificacao[i].CdCepSufixo),
                                                                                 new XElement("cdPais", objListIdentificacao[i].CdPais),
                                                                                 (objListIdentificacao[i].DsTelefoneDdd != "" ? new XElement("dsTelefoneDdd", objListIdentificacao[i].DsTelefoneDdd) : null),
                                                                                 (objListIdentificacao[i].DsTelefoneDdi != "" ? new XElement("dsTelefoneDdi", objListIdentificacao[i].DsTelefoneDdi) : null)));

                    Enderecos.Add(Endereco);


                    Cliente.Add(Enderecos);
                    Clientes.Add(Cliente);

                    iCountArquivos++;

                    if (iCountArquivos == 100)
                    {
                        iCountArquivos = 0;
                        XmlSchemaCollection myschema = new XmlSchemaCollection();
                        XmlValidatingReader reader;

                        XmlParserContext context = new XmlParserContext(null, null, "", XmlSpace.None);

                        reader = new XmlValidatingReader(Clientes.ToString(), XmlNodeType.Element, context);
                        reader.ValidationType = ValidationType.Schema;
                        reader.Schemas.Add(myschema);

                        while (reader.Read())
                        { }

                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + Clientes.ToString());
                        doc.Save(sNomeArquivo + "_" + iIDArquivo + ".xml");

                        iIDArquivo++;
                        Clientes = (new XElement("Clientes", new XAttribute(ns + "noNamespaceSchemaLocation", Application.StartupPath + "\\Schema\\Cliente.xsd"),
                                                                  new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance")));
                    }
                }

                if (Clientes.ToString() != null)
                {
                    XmlSchemaCollection myschema = new XmlSchemaCollection();
                    XmlValidatingReader reader;

                    XmlParserContext context = new XmlParserContext(null, null, "", XmlSpace.None);

                    reader = new XmlValidatingReader(Clientes.ToString(), XmlNodeType.Element, context);
                    reader.ValidationType = ValidationType.Schema;
                    reader.Schemas.Add(myschema);

                    while (reader.Read())
                    { }

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + Clientes.ToString());
                    doc.Save(sNomeArquivo + "_" + iIDArquivo + ".xml");
                }
                return true;
            }
            catch (XmlException x)
            {
                return false;
                throw new Exception(x.Message.ToString());
            }
            catch (XmlSchemaException x)
            {
                return false;
                throw new Exception(x.Message.ToString());
            }




        }

        public bool ValidaCpf(string sCpj)
        {
            bool Retorno = true;
            Match valida = Regex.Match(sCpj, @"^[0-9]{11}$");
            if (!valida.Success) { Retorno = false; };

            return Retorno;
        }
        public bool ValidaCnpj(string sCnpj)
        {
            bool Retorno = true;
            Match valida = Regex.Match(sCnpj, @"^[0-9]{14}$");
            if (!valida.Success) { Retorno = false; };

            return Retorno;
        }
        public bool ValidaEmail(string sEmail)
        {
            bool Retorno = true;
            Match valida = Regex.Match(sEmail, @"^[0-9]{10}$");
            if (!valida.Success) { Retorno = false; };

            return Retorno;

        }
        public bool ValidaNumero(string sNumero)
        {
            bool Retorno = true;
            Match valida = Regex.Match(sNumero, @"^[0-9]+$");
            if (!valida.Success) { Retorno = false; };
            return Retorno;
        }
        public string TiraSimbolo(string sString)
        {
            sString = sString.Replace("\\viewkind4\\uc1\\pard\\f0\\fs16 ", "");
            sString = sString.Replace("{\\colortbl ;\\red0\\green0\\blue255;}\\viewkind4\\uc1 d\\cf1\\lang1046\\fs16   ", "");
            sString = sString.Replace("\\viewkind4\\uc1 d\\b\\fs16 ", "");
            sString = sString.Replace("\\f1\\'c7", "C");
            sString = sString.Replace("\\'c3", "A");
            sString = sString.Replace("\\f0 ", "");
            sString = sString.Replace("\\par", " ");
            sString = sString.Replace("}\0", "");
            sString = sString.Replace("\\f0", "");
            sString = sString.Replace("\\'ba", "o");
            sString = sString.Replace("\\f1", "");
            sString = sString.Replace("\\'cd", "I");
            sString = sString.Replace("\\'aa", "a");
            sString = sString.Replace("\\'e1", "a");
            sString = sString.Replace("\\'e7\\'e3", "ca");
            sString = sString.Replace("\\b0", ".");
            sString = sString.Replace("\\'e3", "a");
            sString = sString.Replace("\\'ea", "e");
            sString = sString.Replace("\\c9", "E");
            sString = sString.Replace(" ", "");
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
                    if ((sCar == sSimbolos[y, 0]))
                    {
                        sString = sString.Replace(sCar, sSimbolos[y, 1]);
                    }
                }

            }
            Resultado = sString;
            return Resultado;
        }

    }
}
