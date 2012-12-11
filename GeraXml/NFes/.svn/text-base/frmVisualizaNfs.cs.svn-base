using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HLP.bel.NFes;
using System.Text.RegularExpressions;
using HLP.bel;
using HLP.Util;
using ComponentFactory.Krypton.Toolkit;
using HLP.bel.Static;

namespace NfeGerarXml.NFes
{
    public partial class frmVisualizaNfs : KryptonForm
    {
        tcLoteRps objLoteRps = null;
        public tcLoteRps objLoteRpsAlter = null;
        public bool bCancela = false;
        int iIndex = 0;
        int iCountObj = 0;

        struct ErrosNotas
        {
            public string sNomeCampo { get; set; }
            public string sNota { get; set; }
        }


        List<ErrosNotas> objListaErroAtual = new List<ErrosNotas>();

        public frmVisualizaNfs(tcLoteRps objLoteRps)
        {

            InitializeComponent();
            this.objLoteRps = objLoteRps;
        }



        private bool VerificaNotas()
        {
            objListaErroAtual = new List<ErrosNotas>();
            bool retorno = true;
            int iCountErros = 0;

            for (int i = 0; i < objLoteRpsAlter.Rps.Count; i++)
            {

                string sNumNota = objLoteRpsAlter.Rps[i].InfRps.IdentificacaoRps.Numero;
                ErrosNotas objNotaAtual = new ErrosNotas();
                objNotaAtual.sNota = sNumNota;

                #region Tomador

                if (objLoteRpsAlter.Rps[i].InfRps.Tomador.Contato != null)
                {
                    string sTelefone = objLoteRpsAlter.Rps[i].InfRps.Tomador.Contato.Telefone;
                    if (sTelefone != "")
                    {
                        if (ValidaFone(sTelefone) == false)
                        {
                            objNotaAtual.sNomeCampo = objNotaAtual.sNomeCampo + "            Telefone Tomador Inválido!" + Environment.NewLine;
                            iCountErros++;
                        }
                    }
                }

                if (objLoteRpsAlter.Rps[i].InfRps.Tomador.IdentificacaoTomador != null)
                {
                    if (objLoteRpsAlter.Rps[i].InfRps.Tomador.IdentificacaoTomador.CpfCnpj != null)
                    {
                        if (objLoteRpsAlter.Rps[i].InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cnpj != "")
                        {
                            string sCnpj = TiraSimbolo(objLoteRpsAlter.Rps[i].InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cnpj);
                            if (ValidaCnpj(sCnpj) == false)
                            {
                                objNotaAtual.sNomeCampo = objNotaAtual.sNomeCampo + "            Cnpj Tomador Inválido!" + Environment.NewLine;
                                iCountErros++;
                            }
                        }
                        else if (objLoteRpsAlter.Rps[i].InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cpf != "")
                        {
                            string sCpf = TiraSimbolo(objLoteRpsAlter.Rps[i].InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cpf);
                            if (ValidaCpf(sCpf) == false)
                            {
                                objNotaAtual.sNomeCampo = objNotaAtual.sNomeCampo + "            Cpf Tomador Inválido!" + Environment.NewLine;
                                iCountErros++;
                            }
                        }
                    }
                }
                if (objLoteRpsAlter.Rps[i].InfRps.Tomador.Endereco != null)
                {
                    if (objLoteRpsAlter.Rps[i].InfRps.Tomador.Endereco.Cep != "")
                    {
                        string sCep = TiraSimbolo(Convert.ToString(objLoteRpsAlter.Rps[i].InfRps.Tomador.Endereco.Cep));
                        if (ValidaCep(sCep) == false)
                        {
                            objNotaAtual.sNomeCampo = objNotaAtual.sNomeCampo + "            Cep Tomador Inválido!" + Environment.NewLine;
                            iCountErros++;
                        }
                    }
                }
                #endregion

                #region Intemediario


                if (objLoteRpsAlter.Rps[i].InfRps.Prestador.Cnpj != null)
                {
                    string sCnpj = TiraSimbolo(objLoteRpsAlter.Rps[i].InfRps.Prestador.Cnpj);
                    if (ValidaCnpj(sCnpj) == false)
                    {
                        objNotaAtual.sNomeCampo = objNotaAtual.sNomeCampo + "            Cnpj Prestador de Serviço Inválido!" + Environment.NewLine;
                        iCountErros++;
                    }
                }


                if (objLoteRpsAlter.Rps[i].InfRps.IntermediarioServico != null)
                {
                    if (objLoteRpsAlter.Rps[i].InfRps.IntermediarioServico.CpfCnpj != null)
                    {
                        if (objLoteRpsAlter.Rps[i].InfRps.IntermediarioServico.CpfCnpj.Cnpj != "")
                        {
                            string sCnpj = TiraSimbolo(objLoteRpsAlter.Rps[i].InfRps.IntermediarioServico.CpfCnpj.Cnpj);
                            if (ValidaCnpj(sCnpj) == false)
                            {
                                objNotaAtual.sNomeCampo = objNotaAtual.sNomeCampo + "            Cnpj Intermediário de Serviço Inválido!" + Environment.NewLine;
                                iCountErros++;
                            }
                        }
                        else if (objLoteRpsAlter.Rps[i].InfRps.IntermediarioServico.CpfCnpj.Cpf != "")
                        {
                            string sCpf = TiraSimbolo(objLoteRpsAlter.Rps[i].InfRps.IntermediarioServico.CpfCnpj.Cpf);
                            if (ValidaCep(sCpf) == false)
                            {
                                objNotaAtual.sNomeCampo = objNotaAtual.sNomeCampo + "            Cpf Intermediário de Serviço Inválido!" + Environment.NewLine;
                                iCountErros++;
                            }
                        }
                    }
                }

                #endregion

                #region CamposObrigatórios
                if (objLoteRpsAlter.Rps[i].InfRps.IdentificacaoRps.Serie == "")
                {
                    objNotaAtual.sNomeCampo = objNotaAtual.sNomeCampo + "            Número de série Rps é Obrigatório!" + Environment.NewLine;
                    iCountErros++;
                }
                if (objLoteRpsAlter.Rps[i].InfRps.DataEmissao.ToString() == "")
                {
                    objNotaAtual.sNomeCampo = objNotaAtual.sNomeCampo + "            Data Emissão é Obrigatório!" + Environment.NewLine;
                    iCountErros++;
                }

                if (objLoteRpsAlter.Rps[i].InfRps.Servico.ItemListaServico == "")
                {
                    objNotaAtual.sNomeCampo = objNotaAtual.sNomeCampo + "            Item Lista Serviço é Obrigatório!" + Environment.NewLine;
                    iCountErros++;
                }

                if (objLoteRpsAlter.Rps[i].InfRps.Servico.CodigoMunicipio == "")
                {
                    objNotaAtual.sNomeCampo = objNotaAtual.sNomeCampo + "            Código do Municípo do Prestador de Serviço é Obrigatório!" + Environment.NewLine;
                    iCountErros++;
                }

                if (objLoteRpsAlter.Rps[i].InfRps.Servico.Discriminacao == "")
                {
                    objNotaAtual.sNomeCampo = objNotaAtual.sNomeCampo + "            Discriminação do Serviço é Obrigatório!" + Environment.NewLine;
                    iCountErros++;
                }
                if (objLoteRpsAlter.Rps[i].InfRps.Servico.CodigoTributacaoMunicipio == "" || objLoteRpsAlter.Rps[i].InfRps.Servico.CodigoTributacaoMunicipio == "0")
                {
                    objNotaAtual.sNomeCampo = objNotaAtual.sNomeCampo + "            Código do Município Inválido!" + Environment.NewLine;
                    iCountErros++;
                }
                if (objLoteRpsAlter.Rps[i].InfRps.NaturezaOperacao.ToString() == "")
                {
                    objNotaAtual.sNomeCampo = objNotaAtual.sNomeCampo + "            Natureza da Operação é Obrigatório!" + Environment.NewLine;
                    iCountErros++;
                }
                if (objLoteRpsAlter.Rps[i].InfRps.OptanteSimplesNacional.ToString() == "")
                {
                    objNotaAtual.sNomeCampo = objNotaAtual.sNomeCampo + "            Optante Simples Nacional é Obrigatório!" + Environment.NewLine;
                    iCountErros++;
                }
                if (objLoteRpsAlter.Rps[i].InfRps.IncentivadorCultural.ToString() == "")
                {
                    objNotaAtual.sNomeCampo = objNotaAtual.sNomeCampo + "            Incenticador Cultural é Obrigatório!" + Environment.NewLine;
                    iCountErros++;
                }
                if (objLoteRpsAlter.Rps[i].InfRps.Status.ToString() == "")
                {
                    objNotaAtual.sNomeCampo = objNotaAtual.sNomeCampo + "            Status é Obrigatório!" + Environment.NewLine;
                    iCountErros++;
                }
                if (objLoteRpsAlter.Rps[i].InfRps.IdentificacaoRps.Tipo.ToString() == "")
                {
                    objNotaAtual.sNomeCampo = objNotaAtual.sNomeCampo + "            Tipo Rps é Obrigatório!" + Environment.NewLine;
                    iCountErros++;
                }
                if (objLoteRpsAlter.Rps[i].InfRps.Servico.Valores.IssRetido.ToString() == "")
                {
                    objNotaAtual.sNomeCampo = objNotaAtual.sNomeCampo + "            ISS Retido é Obrigatório!" + Environment.NewLine;
                    iCountErros++;
                }

                if (belStatic.sNomeEmpresa.Equals("AENGE"))
                {
                    if (objLoteRpsAlter.Rps[i].InfRps.ConstrucaoCivil.CodigoObra.Equals(""))
                    {
                        objNotaAtual.sNomeCampo = objNotaAtual.sNomeCampo + "            Código da Obra é Obrigatório!" + Environment.NewLine;
                        iCountErros++;
                    }
                    if (objLoteRpsAlter.Rps[i].InfRps.ConstrucaoCivil.Art.Equals(""))
                    {
                        objNotaAtual.sNomeCampo = objNotaAtual.sNomeCampo + "            Art da Obra é Obrigatório!" + Environment.NewLine;
                        iCountErros++;
                    }

                }

                #endregion

                if (objNotaAtual.sNomeCampo != null)
                {
                    objListaErroAtual.Add(objNotaAtual);
                }
            }

            txtErros.Text = "";
            if (iCountErros > 0)
            {
                retorno = false;
                StringBuilder sErro = new StringBuilder();
                for (int i = 0; i < objListaErroAtual.Count; i++)
                {
                    sErro.Append("Nota Nº " + objListaErroAtual[i].sNota + Environment.NewLine);
                    sErro.Append(objListaErroAtual[i].sNomeCampo + Environment.NewLine);
                }
                txtErros.Text = sErro.ToString();
            }

            lblErro.Text = "Erros Encontrados: " + iCountErros;
            return retorno;
        }
        private string TiraSimbolo(string sString)
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


        private void CriaObjAlter()
        {
            try
            {

                tcLoteRps obj = new tcLoteRps();
                obj.Rps = new List<TcRps>();

                obj.NumeroLote = this.objLoteRps.NumeroLote;
                obj.Cnpj = this.objLoteRps.Cnpj;
                obj.InscricaoMunicipal = this.objLoteRps.InscricaoMunicipal;
                obj.QuantidadeRps = this.objLoteRps.QuantidadeRps;



                for (int i = 0; i < this.objLoteRps.Rps.Count; i++)
                {
                    TcRps objTcTcRps = new TcRps();
                    #region Identificacao
                    objTcTcRps.InfRps = new TcInfRps();
                    objTcTcRps.InfRps.DataEmissao = this.objLoteRps.Rps[i].InfRps.DataEmissao;
                    objTcTcRps.InfRps.NaturezaOperacao = this.objLoteRps.Rps[i].InfRps.NaturezaOperacao;
                    objTcTcRps.InfRps.RegimeEspecialTributacao = this.objLoteRps.Rps[i].InfRps.RegimeEspecialTributacao;
                    objTcTcRps.InfRps.OptanteSimplesNacional = this.objLoteRps.Rps[i].InfRps.OptanteSimplesNacional;
                    objTcTcRps.InfRps.IncentivadorCultural = this.objLoteRps.Rps[i].InfRps.IncentivadorCultural;
                    objTcTcRps.InfRps.Status = this.objLoteRps.Rps[i].InfRps.Status;


                    objTcTcRps.InfRps.IdentificacaoRps = new tcIdentificacaoRps();
                    objTcTcRps.InfRps.IdentificacaoRps.Numero = this.objLoteRps.Rps[i].InfRps.IdentificacaoRps.Numero;
                    objTcTcRps.InfRps.IdentificacaoRps.Serie = this.objLoteRps.Rps[i].InfRps.IdentificacaoRps.Serie;
                    objTcTcRps.InfRps.IdentificacaoRps.Tipo = this.objLoteRps.Rps[i].InfRps.IdentificacaoRps.Tipo;
                    objTcTcRps.InfRps.IdentificacaoRps.Nfseq = this.objLoteRps.Rps[i].InfRps.IdentificacaoRps.Nfseq;



                    if (this.objLoteRps.Rps[i].InfRps.RpsSubstituido != null)
                    {
                        objTcTcRps.InfRps.RpsSubstituido = new tcIdentificacaoRps();
                        objTcTcRps.InfRps.RpsSubstituido.Numero = this.objLoteRps.Rps[i].InfRps.RpsSubstituido.Numero;
                        objTcTcRps.InfRps.RpsSubstituido.Serie = this.objLoteRps.Rps[i].InfRps.RpsSubstituido.Serie;
                        objTcTcRps.InfRps.RpsSubstituido.Tipo = this.objLoteRps.Rps[i].InfRps.RpsSubstituido.Tipo;
                    }
                    #endregion

                    #region Serviço
                    objTcTcRps.InfRps.Servico = new TcDadosServico();
                    objTcTcRps.InfRps.Servico.Valores = new TcValores();

                    objTcTcRps.InfRps.Servico.Valores.ValorServicos = this.objLoteRps.Rps[i].InfRps.Servico.Valores.ValorServicos;
                    objTcTcRps.InfRps.Servico.Valores.ValorDeducoes = this.objLoteRps.Rps[i].InfRps.Servico.Valores.ValorDeducoes;
                    objTcTcRps.InfRps.Servico.Valores.ValorCsll = this.objLoteRps.Rps[i].InfRps.Servico.Valores.ValorCsll;
                    objTcTcRps.InfRps.Servico.Valores.ValorPis = this.objLoteRps.Rps[i].InfRps.Servico.Valores.ValorPis;
                    objTcTcRps.InfRps.Servico.Valores.ValorCofins = this.objLoteRps.Rps[i].InfRps.Servico.Valores.ValorCofins;
                    objTcTcRps.InfRps.Servico.Valores.IssRetido = this.objLoteRps.Rps[i].InfRps.Servico.Valores.IssRetido;
                    objTcTcRps.InfRps.Servico.Valores.ValorInss = this.objLoteRps.Rps[i].InfRps.Servico.Valores.ValorInss;
                    objTcTcRps.InfRps.Servico.Valores.ValorIr = this.objLoteRps.Rps[i].InfRps.Servico.Valores.ValorIr;
                    objTcTcRps.InfRps.Servico.Valores.ValorIss = this.objLoteRps.Rps[i].InfRps.Servico.Valores.ValorIss;
                    objTcTcRps.InfRps.Servico.Valores.OutrasRetencoes = this.objLoteRps.Rps[i].InfRps.Servico.Valores.OutrasRetencoes;
                    objTcTcRps.InfRps.Servico.Valores.BaseCalculo = this.objLoteRps.Rps[i].InfRps.Servico.Valores.BaseCalculo;
                    objTcTcRps.InfRps.Servico.Valores.Aliquota = this.objLoteRps.Rps[i].InfRps.Servico.Valores.Aliquota;
                    objTcTcRps.InfRps.Servico.Valores.ValorLiquidoNfse = this.objLoteRps.Rps[i].InfRps.Servico.Valores.ValorLiquidoNfse;
                    objTcTcRps.InfRps.Servico.Valores.ValorIssRetido = this.objLoteRps.Rps[i].InfRps.Servico.Valores.ValorIssRetido;
                    objTcTcRps.InfRps.Servico.Valores.DescontoCondicionado = this.objLoteRps.Rps[i].InfRps.Servico.Valores.DescontoCondicionado;
                    objTcTcRps.InfRps.Servico.Valores.DescontoIncondicionado = this.objLoteRps.Rps[i].InfRps.Servico.Valores.DescontoIncondicionado;

                    objTcTcRps.InfRps.Servico.ItemListaServico = this.objLoteRps.Rps[i].InfRps.Servico.ItemListaServico;
                    objTcTcRps.InfRps.Servico.CodigoTributacaoMunicipio = this.objLoteRps.Rps[i].InfRps.Servico.CodigoTributacaoMunicipio;
                    objTcTcRps.InfRps.Servico.CodigoCnae = this.objLoteRps.Rps[i].InfRps.Servico.CodigoCnae;
                    objTcTcRps.InfRps.Servico.CodigoMunicipio = this.objLoteRps.Rps[i].InfRps.Servico.CodigoMunicipio;
                    objTcTcRps.InfRps.Servico.Discriminacao = this.objLoteRps.Rps[i].InfRps.Servico.Discriminacao;

                    #endregion

                    #region Dados Adicionais
                    objTcTcRps.InfRps.Prestador = new tcIdentificacaoPrestador();
                    objTcTcRps.InfRps.Prestador.Cnpj = this.objLoteRps.Rps[i].InfRps.Prestador.Cnpj;

                    objTcTcRps.InfRps.Prestador.InscricaoMunicipal = this.objLoteRps.Rps[i].InfRps.Prestador.InscricaoMunicipal;

                    objTcTcRps.InfRps.Tomador = new tcDadosTomador();

                    objTcTcRps.InfRps.Tomador.IdentificacaoTomador = new tcIdentificacaoTomador();
                    if (this.objLoteRps.Rps[i].InfRps.Tomador.IdentificacaoTomador.CpfCnpj != null)
                    {
                        if (this.objLoteRps.Rps[i].InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cnpj != "")
                        {
                            objTcTcRps.InfRps.Tomador.IdentificacaoTomador.CpfCnpj = new TcCpfCnpj();
                            objTcTcRps.InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cnpj = this.objLoteRps.Rps[i].InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cnpj;
                        }
                        else if (this.objLoteRps.Rps[i].InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cpf != "")
                        {
                            objTcTcRps.InfRps.Tomador.IdentificacaoTomador.CpfCnpj = new TcCpfCnpj();
                            objTcTcRps.InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cpf = this.objLoteRps.Rps[i].InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cpf;
                        }
                    }
                    objTcTcRps.InfRps.Tomador.IdentificacaoTomador.InscricaoMunicipal = this.objLoteRps.Rps[i].InfRps.Tomador.IdentificacaoTomador.InscricaoMunicipal;
                    objTcTcRps.InfRps.Tomador.RazaoSocial = this.objLoteRps.Rps[i].InfRps.Tomador.RazaoSocial;

                    if (this.objLoteRps.Rps[i].InfRps.Tomador.Endereco != null)
                    {
                        objTcTcRps.InfRps.Tomador.Endereco = new TcEndereco();

                        objTcTcRps.InfRps.Tomador.Endereco.Endereco = this.objLoteRps.Rps[i].InfRps.Tomador.Endereco.Endereco;
                        objTcTcRps.InfRps.Tomador.Endereco.Numero = this.objLoteRps.Rps[i].InfRps.Tomador.Endereco.Numero;
                        objTcTcRps.InfRps.Tomador.Endereco.Complemento = this.objLoteRps.Rps[i].InfRps.Tomador.Endereco.Complemento;
                        objTcTcRps.InfRps.Tomador.Endereco.Bairro = this.objLoteRps.Rps[i].InfRps.Tomador.Endereco.Bairro;
                        objTcTcRps.InfRps.Tomador.Endereco.Uf = this.objLoteRps.Rps[i].InfRps.Tomador.Endereco.Uf;
                        objTcTcRps.InfRps.Tomador.Endereco.CodigoMunicipio = this.objLoteRps.Rps[i].InfRps.Tomador.Endereco.CodigoMunicipio;
                        objTcTcRps.InfRps.Tomador.Endereco.Cep = this.objLoteRps.Rps[i].InfRps.Tomador.Endereco.Cep;
                    }

                    if (this.objLoteRps.Rps[i].InfRps.Tomador.Contato != null)
                    {
                        objTcTcRps.InfRps.Tomador.Contato = new TcContato();

                        objTcTcRps.InfRps.Tomador.Contato.Telefone = this.objLoteRps.Rps[i].InfRps.Tomador.Contato.Telefone;
                        objTcTcRps.InfRps.Tomador.Contato.Email = this.objLoteRps.Rps[i].InfRps.Tomador.Contato.Email;
                    }


                    if (this.objLoteRps.Rps[i].InfRps.IntermediarioServico != null)
                    {
                        objTcTcRps.InfRps.IntermediarioServico = new TcIdentificacaoIntermediarioServico();
                        objTcTcRps.InfRps.IntermediarioServico.RazaoSocial = this.objLoteRps.Rps[i].InfRps.IntermediarioServico.RazaoSocial;
                        objTcTcRps.InfRps.IntermediarioServico.InscricaoMunicipal = this.objLoteRps.Rps[i].InfRps.IntermediarioServico.InscricaoMunicipal;


                        if (this.objLoteRps.Rps[i].InfRps.IntermediarioServico.CpfCnpj != null)
                        {
                            if (this.objLoteRps.Rps[i].InfRps.IntermediarioServico.CpfCnpj.Cnpj != "")
                            {
                                objTcTcRps.InfRps.IntermediarioServico.CpfCnpj = new TcCpfCnpj();
                                objTcTcRps.InfRps.IntermediarioServico.CpfCnpj.Cnpj = this.objLoteRps.Rps[i].InfRps.IntermediarioServico.CpfCnpj.Cnpj;
                            }
                            else if (this.objLoteRps.Rps[i].InfRps.IntermediarioServico.CpfCnpj.Cpf != "")
                            {
                                objTcTcRps.InfRps.IntermediarioServico.CpfCnpj = new TcCpfCnpj();
                                objTcTcRps.InfRps.IntermediarioServico.CpfCnpj.Cpf = this.objLoteRps.Rps[i].InfRps.IntermediarioServico.CpfCnpj.Cpf;
                            }
                        }
                    }

                    if (this.objLoteRps.Rps[i].InfRps.ConstrucaoCivil != null)
                    {
                        objTcTcRps.InfRps.ConstrucaoCivil = new tcDadosConstrucaoCivil();
                        objTcTcRps.InfRps.ConstrucaoCivil.Art = this.objLoteRps.Rps[i].InfRps.ConstrucaoCivil.Art;
                        objTcTcRps.InfRps.ConstrucaoCivil.CodigoObra = this.objLoteRps.Rps[i].InfRps.ConstrucaoCivil.CodigoObra;
                    }

                    #endregion

                    obj.Rps.Add(objTcTcRps);

                }
                this.objLoteRpsAlter = obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region ValidacaoCamposIndividuais

        private bool ValidaFone(string sFone)
        {
            Match valida = Regex.Match(sFone, @"^[0-9]{10}$");
            if (!valida.Success) { return false; } else { return true; }
        }
        private bool ValidaCnpj(string sCnpj)
        {
            Match valida = Regex.Match(sCnpj, @"^[0-9]{14}$");
            if (!valida.Success) { return false; } else { return true; }
        }
        private bool ValidaCpf(string sCpf)
        {
            Match valida = Regex.Match(sCpf, @"^[0-9]{11}$");
            if (!valida.Success) { return false; } else { return true; }
        }
        private bool ValidaCep(string sCep)
        {
            Match valida = Regex.Match(sCep, @"^[0-9]{8}$");
            if (!valida.Success) { return false; } else { return true; }
        }

        #endregion

        private bool VerificaErros()
        {
            bool Retorno = true;
            int iCountErros = 0;
            errErro.Dispose();
            string sObrigatorio = "Campo Obrigatório!";

            #region ValidaMascaras

            Match valida = Regex.Match(mskFoneToma.Text, @"^[0-9]{10}$");
            if (mskFoneToma.Text != "")
            {
                if (!valida.Success)
                {
                    errErro.SetError(mskFoneToma, "Campo Incompleto!");
                    iCountErros++;
                }
            }
            if (mskCpfCnpjToma.Text != "")
            {
                if (mskCpfCnpjToma.Mask == "00,000,000/0000-00")
                {
                    valida = Regex.Match(mskCpfCnpjToma.Text, @"^[0-9]{14}$");
                    if (!valida.Success)
                    {
                        errErro.SetError(mskCpfCnpjToma, "Campo Incompleto!");
                        iCountErros++;
                    }
                }
                else
                {
                    valida = Regex.Match(mskCpfCnpjToma.Text, @"^[0-9]{11}$");
                    if (!valida.Success)
                    {
                        errErro.SetError(mskCpfCnpjToma, "Campo Incompleto!");
                        iCountErros++;
                    }
                }
            }

            valida = Regex.Match(mskPrestCnpj.Text, @"^[0-9]{14}$");
            if (!valida.Success)
            {
                errErro.SetError(mskPrestCnpj, "Campo Incompleto!");
                iCountErros++;
            }

            if (mskCepToma.Text != "")
            {
                valida = Regex.Match(mskCepToma.Text, @"^[0-9]{8}$");
                if (!valida.Success)
                {
                    errErro.SetError(mskCepToma, "Campo Incompleto!");
                    iCountErros++;
                }
            }

            if (mskCnpjInterServ.Text != "")
            {
                if (mskCnpjInterServ.Mask == "00,000,000/0000-00")
                {
                    valida = Regex.Match(mskCnpjInterServ.Text, @"^[0-9]{14}$");
                    if (!valida.Success)
                    {
                        errErro.SetError(mskCnpjInterServ, "Campo Incompleto!");
                        iCountErros++;
                    }
                }
                else
                {
                    valida = Regex.Match(mskCnpjInterServ.Text, @"^[0-9]{11}$");
                    if (!valida.Success)
                    {
                        errErro.SetError(mskCnpjInterServ, "Campo Incompleto!");
                        iCountErros++;
                    }
                }
            }
            #endregion

            #region ValidaTextBox

            if (txtSerieRps.Text.Equals("")) { errErro.SetError(txtSerieRps, sObrigatorio); iCountErros++; }
            if (txtDataEmissao.Text.Equals("")) { errErro.SetError(txtDataEmissao, sObrigatorio); iCountErros++; }
            if (txtItemlServ.Text.Equals("")) { errErro.SetError(txtItemlServ, sObrigatorio); iCountErros++; }
            if (txtMunPrestServ.Text.Equals("")) { errErro.SetError(txtMunPrestServ, sObrigatorio); iCountErros++; }
            if (txtDiscriminacao.Text.Equals("")) { errErro.SetError(txtDiscriminacao, sObrigatorio); iCountErros++; }
            if ((txtCodTribMun.Text == "") || (txtCodTribMun.Text == "0")) { errErro.SetError(txtCodTribMun, sObrigatorio); iCountErros++; }

            #endregion

            #region ValidaComboBox

            if (cboNatOperacao.SelectedIndex == -1) { errErro.SetError(cboNatOperacao, sObrigatorio); iCountErros++; }
            if (cboSimplesnacional.SelectedIndex == -1) { errErro.SetError(cboSimplesnacional, sObrigatorio); iCountErros++; }
            if (cboInCultural.SelectedIndex == -1) { errErro.SetError(cboInCultural, sObrigatorio); iCountErros++; }
            if (cboStatus.SelectedIndex == -1) { errErro.SetError(cboStatus, sObrigatorio); iCountErros++; }
            if (cboTipoRps.SelectedIndex == -1) { errErro.SetError(cboTipoRps, sObrigatorio); iCountErros++; }
            if (cboIssRetido.SelectedIndex == -1) { errErro.SetError(cboIssRetido, sObrigatorio); iCountErros++; }

            #endregion

            if (iCountErros > 0)
            {
                Retorno = false;
            }

            return Retorno;
        }
        private void PopulaForm(int index)
        {
            try
            {
                LimpaCampos(this.Controls);
                #region Identificacao

                txtNumLote.Text = this.objLoteRpsAlter.NumeroLote;
                mskCnpjLote.Text = this.objLoteRpsAlter.Cnpj;
                txtImLote.Text = this.objLoteRpsAlter.InscricaoMunicipal;
                txtQtdeRpsLote.Text = this.objLoteRpsAlter.QuantidadeRps.ToString();

                TcRps rps = this.objLoteRpsAlter.Rps[index];

                txtDataEmissao.Text = rps.InfRps.DataEmissao.ToString();
                cboNatOperacao.SelectedIndex = rps.InfRps.NaturezaOperacao - 1;
                cboRegTributacao.SelectedIndex = rps.InfRps.RegimeEspecialTributacao - 1;
                cboSimplesnacional.SelectedIndex = rps.InfRps.OptanteSimplesNacional - 1;
                cboInCultural.SelectedIndex = rps.InfRps.IncentivadorCultural - 1;
                cboStatus.SelectedIndex = rps.InfRps.Status - 1;

                //Inicio Group Identificacao Rps
                txtNumRps.Text = rps.InfRps.IdentificacaoRps.Numero.ToString();
                txtSerieRps.Text = rps.InfRps.IdentificacaoRps.Serie;
                cboTipoRps.SelectedIndex = rps.InfRps.IdentificacaoRps.Tipo - 1;

                //Inicio Group Rps Substituido
                if (rps.InfRps.RpsSubstituido != null)
                {
                    txtNumRpsSubs.Text = rps.InfRps.RpsSubstituido.Numero.ToString();
                    txtSerieRpsSubs.Text = rps.InfRps.RpsSubstituido.Serie.ToString();
                    cboTipoRpsSubs.SelectedIndex = rps.InfRps.RpsSubstituido.Tipo - 1;
                }

                #endregion

                #region Serviço
                nudValorServ.Text = "0";
                nudValorDeducao.Text = "0";
                nudValorCSLL.Text = "0";
                nudValorPIS.Text = "0";
                nudValorCOFINS.Text = "0";
                nudValorInss.Text = "0";
                nudValorIr.Text = "0";
                nudValorISS.Text = "0";
                nudOutrasRetencoes.Text = "0";
                nudBaseCalc.Text = "0";
                nudAliquota.Text = "0";
                nudvalorNfse.Text = "0";
                nudIssRetido.Text = "0";
                nudDescCond.Text = "0";
                nudDescIncond.Text = "0";



                nudValorServ.Text = rps.InfRps.Servico.Valores.ValorServicos.ToString();
                nudValorDeducao.Value = rps.InfRps.Servico.Valores.ValorDeducoes;
                nudValorCSLL.Value = rps.InfRps.Servico.Valores.ValorCsll;
                nudValorPIS.Value = rps.InfRps.Servico.Valores.ValorPis;
                nudValorCOFINS.Value = rps.InfRps.Servico.Valores.ValorCofins;
                nudValorInss.Value = rps.InfRps.Servico.Valores.ValorInss;
                nudValorIr.Value = rps.InfRps.Servico.Valores.ValorIr;
                nudValorISS.Value = rps.InfRps.Servico.Valores.ValorIss;
                nudOutrasRetencoes.Value = rps.InfRps.Servico.Valores.OutrasRetencoes;
                nudBaseCalc.Value = rps.InfRps.Servico.Valores.BaseCalculo;
                nudAliquota.Value = rps.InfRps.Servico.Valores.Aliquota;
                nudvalorNfse.Value = rps.InfRps.Servico.Valores.ValorLiquidoNfse;
                nudIssRetido.Value = rps.InfRps.Servico.Valores.ValorIssRetido;
                nudDescCond.Value = rps.InfRps.Servico.Valores.DescontoCondicionado;
                nudDescIncond.Value = rps.InfRps.Servico.Valores.DescontoIncondicionado;

                cboIssRetido.SelectedIndex = rps.InfRps.Servico.Valores.IssRetido - 1;
                //Inicio Group Serviço
                txtItemlServ.Text = rps.InfRps.Servico.ItemListaServico;
                txtCodTribMun.Text = rps.InfRps.Servico.CodigoTributacaoMunicipio;
                txtCodCnae.Text = rps.InfRps.Servico.CodigoCnae;
                txtMunPrestServ.Text = rps.InfRps.Servico.CodigoMunicipio;
                txtDiscriminacao.Text = rps.InfRps.Servico.Discriminacao;

                #endregion

                #region Dados Adicionais

                //Inicio Group Prestador
                mskPrestCnpj.Text = rps.InfRps.Prestador.Cnpj;
                txtIM.Text = rps.InfRps.Prestador.InscricaoMunicipal;


                //Inicio Group Tomador
                if (rps.InfRps.Tomador.IdentificacaoTomador.CpfCnpj != null)
                {
                    if (rps.InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cnpj != "")
                    {
                        mskCpfCnpjToma.Mask = "00,000,000/0000-00";
                        mskCpfCnpjToma.Text = rps.InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cnpj;
                    }
                    else if (rps.InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cpf != "")
                    {
                        mskCpfCnpjToma.Mask = "000,000,000-00";
                        mskCpfCnpjToma.Text = rps.InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cpf;
                    }
                }
                txtImToma.Text = (rps.InfRps.Tomador.IdentificacaoTomador.InscricaoMunicipal.ToUpper().Equals("ISENTO") ? "" : rps.InfRps.Tomador.IdentificacaoTomador.InscricaoMunicipal);
                txtRazaoToma.Text = rps.InfRps.Tomador.RazaoSocial;
                if (rps.InfRps.Tomador.Endereco != null)
                {
                    txtEndToma.Text = rps.InfRps.Tomador.Endereco.Endereco;
                    txtNumtoma.Text = rps.InfRps.Tomador.Endereco.Numero;
                    txtCompleToma.Text = rps.InfRps.Tomador.Endereco.Complemento;
                    txtBairroToma.Text = rps.InfRps.Tomador.Endereco.Bairro;
                    txtEstadoToma.Text = rps.InfRps.Tomador.Endereco.Uf;
                    txtCidadeToma.Text = rps.InfRps.Tomador.Endereco.CodigoMunicipio.ToString();
                    mskCepToma.Text = rps.InfRps.Tomador.Endereco.Cep.ToString();
                }
                if (rps.InfRps.Tomador.Contato != null)
                {
                    mskFoneToma.Text = rps.InfRps.Tomador.Contato.Telefone.Replace(" ", "");
                    txtEmailToma.Text = rps.InfRps.Tomador.Contato.Email;
                }

                //Inicio Group Intermediario Serviço
                if (rps.InfRps.IntermediarioServico != null)
                {
                    txtRazaoInterServ.Text = rps.InfRps.IntermediarioServico.RazaoSocial;

                    if (rps.InfRps.IntermediarioServico.CpfCnpj != null)
                    {
                        if (rps.InfRps.IntermediarioServico.CpfCnpj.Cnpj != "")
                        {
                            mskCnpjInterServ.Mask = "00,000,000/0000-00";
                            mskCnpjInterServ.Text = rps.InfRps.IntermediarioServico.CpfCnpj.Cnpj;
                        }
                        else if (rps.InfRps.IntermediarioServico.CpfCnpj.Cpf != "")
                        {
                            mskCnpjInterServ.Mask = "000,000,000-00";
                            mskCnpjInterServ.Text = rps.InfRps.IntermediarioServico.CpfCnpj.Cnpj;
                        }
                    }
                    txtImInterServ.Text = rps.InfRps.IntermediarioServico.InscricaoMunicipal;
                }


                //Inicio Group Construçao Civil
                if (rps.InfRps.ConstrucaoCivil != null)
                {
                    txtCodObra.Text = rps.InfRps.ConstrucaoCivil.CodigoObra;
                    txtArt.Text = rps.InfRps.ConstrucaoCivil.Art;
                }


                #endregion
                VerificaErros();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void SalvarAlteracao(int index)
        {
            try
            {
                #region Identificacao


                TcRps rps = this.objLoteRpsAlter.Rps[index];

                rps.InfRps.DataEmissao = Convert.ToDateTime(txtDataEmissao.Text);
                rps.InfRps.NaturezaOperacao = cboNatOperacao.SelectedIndex + 1;
                rps.InfRps.RegimeEspecialTributacao = cboRegTributacao.SelectedIndex + 1;
                rps.InfRps.OptanteSimplesNacional = cboSimplesnacional.SelectedIndex + 1;
                rps.InfRps.IncentivadorCultural = cboInCultural.SelectedIndex + 1;
                rps.InfRps.Status = cboStatus.SelectedIndex + 1;

                //Inicio Group Identificacao Rps
                rps.InfRps.IdentificacaoRps.Numero = Convert.ToString(txtNumRps.Text);
                rps.InfRps.IdentificacaoRps.Serie = txtSerieRps.Text;
                rps.InfRps.IdentificacaoRps.Tipo = cboTipoRps.SelectedIndex + 1;

                //Inicio Group Rps Substituido
                if (txtNumRpsSubs.Text != "" && txtSerieRpsSubs.Text != "" && cboTipoRpsSubs.SelectedIndex != -1)
                {
                    rps.InfRps.RpsSubstituido = new tcIdentificacaoRps();
                    rps.InfRps.RpsSubstituido.Numero = Convert.ToString(txtNumRpsSubs.Text);
                    rps.InfRps.RpsSubstituido.Serie = txtSerieRpsSubs.Text;
                    rps.InfRps.RpsSubstituido.Tipo = cboTipoRpsSubs.SelectedIndex + 1;
                }



                #endregion

                #region Serviço

                rps.InfRps.Servico.Valores.ValorServicos = nudValorServ.Value;
                rps.InfRps.Servico.Valores.ValorDeducoes = nudValorDeducao.Value;
                rps.InfRps.Servico.Valores.ValorCsll = nudValorCSLL.Value;
                rps.InfRps.Servico.Valores.ValorPis = nudValorPIS.Value;
                rps.InfRps.Servico.Valores.ValorCofins = nudValorCOFINS.Value;
                rps.InfRps.Servico.Valores.IssRetido = cboIssRetido.SelectedIndex + 1;
                rps.InfRps.Servico.Valores.ValorInss = nudValorInss.Value;
                rps.InfRps.Servico.Valores.ValorIr = nudValorIr.Value;
                rps.InfRps.Servico.Valores.ValorIss = nudValorISS.Value;
                rps.InfRps.Servico.Valores.OutrasRetencoes = nudOutrasRetencoes.Value;
                rps.InfRps.Servico.Valores.BaseCalculo = nudBaseCalc.Value;
                rps.InfRps.Servico.Valores.Aliquota = nudAliquota.Value;
                rps.InfRps.Servico.Valores.ValorLiquidoNfse = nudvalorNfse.Value;
                rps.InfRps.Servico.Valores.ValorIssRetido = nudIssRetido.Value;
                rps.InfRps.Servico.Valores.DescontoCondicionado = nudDescCond.Value;
                rps.InfRps.Servico.Valores.DescontoIncondicionado = nudDescIncond.Value;

                //Inicio Group Serviço
                rps.InfRps.Servico.ItemListaServico = txtItemlServ.Text;
                rps.InfRps.Servico.CodigoTributacaoMunicipio = txtCodTribMun.Text;
                rps.InfRps.Servico.CodigoCnae = txtCodCnae.Text;
                rps.InfRps.Servico.CodigoMunicipio = txtMunPrestServ.Text;
                rps.InfRps.Servico.Discriminacao = txtDiscriminacao.Text;

                #endregion

                #region Dados Adicionais

                //Inicio Group Prestador
                rps.InfRps.Prestador.Cnpj = mskPrestCnpj.Text;
                rps.InfRps.Prestador.InscricaoMunicipal = txtIM.Text;


                //Inicio Group Tomador
                if (mskCpfCnpjToma.Text != "" || txtImToma.Text != "")
                {
                    rps.InfRps.Tomador.IdentificacaoTomador = new tcIdentificacaoTomador();
                    rps.InfRps.Tomador.IdentificacaoTomador.CpfCnpj = new TcCpfCnpj();
                    if (mskCpfCnpjToma.Mask == "00,000,000/0000-00")
                    {
                        rps.InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cnpj = mskCpfCnpjToma.Text;
                    }
                    else if (mskCpfCnpjToma.Mask == "000,000,000-00")
                    {
                        rps.InfRps.Tomador.IdentificacaoTomador.CpfCnpj.Cpf = mskCpfCnpjToma.Text;
                    }
                    rps.InfRps.Tomador.IdentificacaoTomador.InscricaoMunicipal = txtImToma.Text;
                }
                rps.InfRps.Tomador.RazaoSocial = txtRazaoToma.Text;

                if (txtEndToma.Text != "" || txtNumtoma.Text != "" || txtCompleToma.Text != "" || txtBairroToma.Text != ""
                    || txtEstadoToma.Text != "" || txtCidadeToma.Text != "" || mskCepToma.Text != "")
                {
                    rps.InfRps.Tomador.Endereco = new TcEndereco();
                    rps.InfRps.Tomador.Endereco.Endereco = txtEndToma.Text;
                    rps.InfRps.Tomador.Endereco.Numero = txtNumtoma.Text;
                    rps.InfRps.Tomador.Endereco.Complemento = txtCompleToma.Text;
                    rps.InfRps.Tomador.Endereco.Bairro = txtBairroToma.Text;
                    rps.InfRps.Tomador.Endereco.Uf = txtEstadoToma.Text;
                    if (txtCidadeToma.Text != "")
                    {
                        rps.InfRps.Tomador.Endereco.CodigoMunicipio = Convert.ToInt32(txtCidadeToma.Text);
                    }
                    if (mskCepToma.Text != "")
                    {
                        rps.InfRps.Tomador.Endereco.Cep = mskCepToma.Text;
                    }
                }

                if (mskFoneToma.Text != "" || txtEmailToma.Text != "")
                {
                    rps.InfRps.Tomador.Contato = new TcContato();
                    rps.InfRps.Tomador.Contato.Telefone = mskFoneToma.Text.Replace("-", "").Replace("(", "").Replace(")", "");
                    rps.InfRps.Tomador.Contato.Email = txtEmailToma.Text;
                }

                //Inicio Group Intermediario Serviço

                if (txtRazaoInterServ.Text != "" || mskCnpjInterServ.Text != "" || txtImInterServ.Text != "")
                {
                    rps.InfRps.IntermediarioServico = new TcIdentificacaoIntermediarioServico();
                    rps.InfRps.IntermediarioServico.CpfCnpj = new TcCpfCnpj();
                    rps.InfRps.IntermediarioServico.RazaoSocial = txtRazaoInterServ.Text;
                    if (mskCnpjInterServ.Mask == "00,000,000/0000-00")
                    {
                        rps.InfRps.IntermediarioServico.CpfCnpj.Cnpj = mskCnpjInterServ.Text;
                    }
                    else if (mskCnpjInterServ.Mask == "000,000,000-00")
                    {
                        rps.InfRps.IntermediarioServico.CpfCnpj.Cnpj = mskCnpjInterServ.Text;
                    }
                    rps.InfRps.IntermediarioServico.InscricaoMunicipal = txtImInterServ.Text;
                }


                //Inicio Group Construçao Civil
                if (txtCodObra.Text != "" || txtArt.Text != "")
                {
                    rps.InfRps.ConstrucaoCivil = new tcDadosConstrucaoCivil();
                    rps.InfRps.ConstrucaoCivil.CodigoObra = txtCodObra.Text;
                    rps.InfRps.ConstrucaoCivil.Art = txtArt.Text;
                }
                else
                {
                    rps.InfRps.ConstrucaoCivil = new tcDadosConstrucaoCivil();                 
                }


                #endregion

                VerificaNotas();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        private void EventoMouseEnter(object sender, EventArgs e)
        {
            ToolStripButton tsButton = (ToolStripButton)sender;
            tsButton.Font = new Font(tsButton.Font.Name, 14, FontStyle.Bold, GraphicsUnit.Pixel);
            tsButton.ForeColor = Color.NavajoWhite;
        }
        private void EventoMouseLeave(object sender, EventArgs e)
        {
            ToolStripButton tsButton = (ToolStripButton)sender;
            tsButton.Font = new Font(tsButton.Font.Name, 12, FontStyle.Regular, GraphicsUnit.Pixel);
            tsButton.ForeColor = Color.White;
        }
        private void btnNavegacao(object sender, EventArgs e)
        {

            ToolStripButton btn = (ToolStripButton)sender;
            switch (btn.Name)
            {
                case "btnProximo":
                    if (iIndex < iCountObj)
                    {
                        SalvarAlteracao(iIndex);
                        iIndex++;
                        PopulaForm(iIndex);
                        VerificaNotas();
                    }
                    break;

                case "btnAnterior":
                    if (iIndex > 0)
                    {
                        SalvarAlteracao(iIndex);
                        iIndex--;
                        PopulaForm(iIndex);
                        VerificaNotas();
                    }
                    break;

                case "btnUltimo":
                    SalvarAlteracao(iIndex);
                    iIndex = iCountObj;
                    PopulaForm(iIndex);
                    VerificaNotas();
                    break;

                case "btnPrimeiro":
                    SalvarAlteracao(iIndex);
                    iIndex = 0;
                    PopulaForm(iIndex);
                    VerificaNotas();
                    break;

            }
            lblContagemNotas.Text = (iIndex + 1) + " de " + (iCountObj + 1);

        }


        private void LimpaCampos(Control.ControlCollection ctrTela)
        {
            foreach (Control ctr in ctrTela)
            {
                if (ctr.HasChildren == true)
                {
                    LimpaCampos(ctr.Controls);
                }
                else
                {
                    foreach (Control ctrsub in ctrTela)
                    {
                        if (ctrsub is TextBox)
                        {
                            ctrsub.Text = "";
                        }
                        if (ctrsub is MaskedTextBox) { ctrsub.Text = ""; }
                        if (ctrsub is ComboBox)
                        {
                            ((ComboBox)ctrsub).SelectedIndex = -1;
                        }
                        if (ctrsub is NumericUpDown)
                        {
                            ((NumericUpDown)ctrsub).Value = 0;
                        }
                        if (ctrsub is NumericUpDown)
                        {
                            ((NumericUpDown)ctrsub).Text = "0";
                        }
                        if (ctrsub is DateTimePicker) { ((DateTimePicker)ctrsub).Value = HLP.Util.Util.GetDateServidor(); }
                        if (ctrsub is ListBox) { ((ListBox)ctrsub).Items.Clear(); }
                        if (ctrsub is CheckBox) { ((CheckBox)ctrsub).Checked = false; }
                        if (ctrsub is RichTextBox) { ((RichTextBox)ctrsub).Text = ""; }

                    }
                }
            }
        }
        public void HabilitaCampos(Control.ControlCollection ctrltela, bool blnHabilita)
        {
            try
            {
                foreach (Control ctr in ctrltela)
                {
                    if (ctr.HasChildren == true)
                    {
                        HabilitaCampos(ctr.Controls, blnHabilita);
                    }
                    else
                    {
                        if (ctr.Parent.GetType() == typeof(KryptonTextBox))
                        {
                            ctr.Enabled = blnHabilita;
                        }
                        else if (ctr.GetType().BaseType.Name == "MaskedTextBox")
                        {
                            ctr.Enabled = blnHabilita;
                        }
                        else if (ctr.GetType().BaseType.Name == "ComboBox")
                        {
                            ctr.Enabled = blnHabilita;
                        }
                        else if (ctr.GetType() == typeof(KryptonDateTimePicker))
                        {
                            ctr.Enabled = blnHabilita;
                        }
                        else if (ctr.GetType().BaseType.Name == "RadioButton")
                        {
                            ctr.Enabled = blnHabilita;
                        }
                        else if (ctr.GetType() == typeof(KryptonButton))
                        {
                            ctr.Enabled = blnHabilita;
                        }
                        else if (ctr.GetType().BaseType.Name == "ListBox")
                        {
                            ctr.Enabled = blnHabilita;
                        }
                        else if (ctr.GetType().BaseType.Name == "CheckBox")
                        {
                            ctr.Enabled = blnHabilita;
                        }
                        else if (ctr.GetType().BaseType.Name == "RichTextBox")
                        {
                            ctr.Enabled = blnHabilita;
                        }
                        else if (ctr.Parent.GetType().BaseType.Name == "NumericUpDown")
                        {
                            ctr.Parent.Enabled = blnHabilita;
                        }
                        else if (ctr.GetType().BaseType.Name == "UpDownButtons")
                        {
                            ctr.Enabled = blnHabilita;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void EmEdicao(bool bEmEdit)
        {
            btnAtualizar.Enabled = !bEmEdit;
            btnSalvar.Enabled = bEmEdit;
            btnCancelar.Enabled = bEmEdit;
            btnSair.Enabled = !bEmEdit;
            btnEnviar.Enabled = !bEmEdit;
        }


        private void btnSair_Click(object sender, EventArgs e)
        {
            if (KryptonMessageBox.Show("Nenhuma nota será enviada. Deseja realmente Sair?", "A L E R T A", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bCancela = true;
                this.Close();
            }
        }
        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (belStatic.BAlteraDadosNfe == true)
            {
                EmEdicao(true);
                HabilitaCampos(this.Controls, true);
            }
            else
            {
                if (KryptonMessageBox.Show(null, "Usuário não tem Acesso para Alterar dados da Nota Fiscal" +
                     Environment.NewLine +
                     Environment.NewLine +
                     "Deseja entrar com a Permissão de um Outro Usuário? ", "A V I S O",
                      MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    frmLogin objfrm = new frmLogin();
                    objfrm.ShowDialog();
                    if (!objfrm.bFechaAplicativo)
                    {
                        if (belStatic.BAlteraDadosNfe == true)
                        {
                            HabilitaCampos(this.Controls, true);
                            EmEdicao(true);
                        }
                        else
                        {
                            KryptonMessageBox.Show(null, "Usuário também não tem Permissão Para Alterar Dados da Nota Fiscal", "A V I S O", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (KryptonMessageBox.Show("Deseja cancelar as alterações realizadas?", "A L E R T A", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                HabilitaCampos(this.Controls, false);
                EmEdicao(false);

                CriaObjAlter();
                iIndex = 0;

                PopulaForm(iIndex);
                VerificaNotas();
                lblContagemNotas.Text = (iIndex + 1) + " de " + (iCountObj + 1);
            }
        }
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            SalvarAlteracao(iIndex);
            VerificaNotas();
            if (VerificaErros() == true && VerificaNotas() == true)
            {
                EmEdicao(false);
                HabilitaCampos(this.Controls, false);
            }
            else
            {
                KryptonMessageBox.Show("Verifique todos os erros antes de salvar as Alterações!", "A T E N Ç Ã O", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnEnviar_Click(object sender, EventArgs e)
        {
            if (VerificaErros() == true)
            {
                if (VerificaNotas() == true)
                {
                    SalvarAlteracao(iIndex);
                    bCancela = false;
                    this.Close();
                }
                else
                {
                    KryptonMessageBox.Show("Verifique todos os erros antes de enviar as Notas!", "A T E N Ç Ã O", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                KryptonMessageBox.Show("Verifique todos os erros antes de enviar as Notas!", "A T E N Ç Ã O", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }




        private void NumericUpDown_Enter(object sender, EventArgs e)
        {
            KryptonNumericUpDown nud = (KryptonNumericUpDown)sender;
            nud.Select(0, nud.ToString().Length);
        }

        private void txtNumRpsSubs_KeyPress(object sender, KeyPressEventArgs e)
        {
            KryptonTextBox txt = (KryptonTextBox)sender;
            if (txt.Text.Equals(""))
            {
                txtSerieRpsSubs.Text = "";
                cboTipoRpsSubs.SelectedIndex = -1;
            }
            else
            {
                txtSerieRpsSubs.Text = txtSerieRps.Text;
                cboTipoRpsSubs.SelectedIndex = cboTipoRps.SelectedIndex;
            }
        }
        private void SomenteNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void txtNumRpsSubs_Leave(object sender, EventArgs e)
        {
            if (txtNumRpsSubs.Text != "")
            {
                txtNumRpsSubs.Text = txtNumRpsSubs.Text.PadLeft(15, '0');
            }
        }

        private void frmVisualizaNfes_Load(object sender, EventArgs e)
        {
            CriaObjAlter();
            iCountObj = objLoteRps.Rps.Count() - 1;
            lblContagemNotas.Text = "1 de " + (iCountObj + 1);
            HabilitaCampos(this.Controls, false);
            EmEdicao(false);
            lblambiente.Text = (belStatic.tpAmbNFse == 1 ? "Produção" : "Homologação");
            PopulaForm(iIndex);
            VerificaNotas();
        }

        private void panelIdent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void kryptonLabel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void mskPrestCnpj_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
