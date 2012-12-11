using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HLP.bel.NFes;
using FirebirdSql.Data.FirebirdClient;
using HLP.bel;
using System.IO;
using HLP.bel.Static;
using HLP.bel.CTe;

namespace HLP.Dao.NFes
{
    public class daoServico
    {
        TcDadosServico objTcDadosServico;

        public TcDadosServico RetornaDadosServico(FbConnection Conn, string sNota, int iNaturezaOperacao)
        {
            try
            {
                objTcDadosServico = new TcDadosServico();
                objTcDadosServico.Valores = BuscaValores(Conn, sNota, iNaturezaOperacao);

                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("Select ");
                sQuery.Append("distinct movitem.ds_prod  ,movitem.vl_totbruto,coalesce(produto.cd_trib_municipio,'')cd_trib_municipio, " +
                              " coalesce(empresa.cd_lista_servico,'')cd_lista_servico_Emp, " +
                              " coalesce(produto.cd_lista_servico,'')cd_lista_servico_Prod from movitem ");
                sQuery.Append("left join produto on movitem.cd_prod = produto.cd_prod ");
                sQuery.Append("left join empresa on movitem.cd_empresa = empresa.cd_empresa ");
                sQuery.Append("where movitem.cd_nfseq = '" + sNota + "' and ");
                sQuery.Append("movitem.cd_empresa = '" + belStatic.codEmpresaNFe + "' and ");
                sQuery.Append("produto.cd_empresa = '" + belStatic.codEmpresaNFe + "'");

                FbCommand Command = new FbCommand(sQuery.ToString(), Conn);
                Command.ExecuteNonQuery();
                FbDataReader dr = Command.ExecuteReader();
                objTcDadosServico.Discriminacao = "Serviço(s) Realizado(s): ";
                while (dr.Read())
                {
                    if (objTcDadosServico.ItemListaServico.Equals(""))
                    {
                        string CodLista = "";
                        if (dr["cd_lista_servico_Prod"].ToString() != "")
                        {
                            CodLista = dr["cd_lista_servico_Prod"].ToString();
                        }
                        else if (dr["cd_lista_servico_Emp"].ToString() != "")
                        {
                            CodLista = dr["cd_lista_servico_Emp"].ToString();
                        }
                        else
                        {
                            throw new Exception("É Necessário Configurar o Código da lista de Serviço no Cadastro de Produto !");
                        }


                        objTcDadosServico.ItemListaServico = CodLista; // dr["cd_lista_servico_Emp"].ToString();
                    }
                    if (objTcDadosServico.CodigoTributacaoMunicipio.Equals(""))
                    {
                        objTcDadosServico.CodigoTributacaoMunicipio = dr["cd_trib_municipio"].ToString();
                    }
                    objTcDadosServico.Discriminacao += Environment.NewLine + "* " + dr["ds_prod"].ToString().ToUpper() + " R$ " + Convert.ToDecimal(dr["vl_totbruto"].ToString()).ToString("#0.00");
                }

                objTcDadosServico.Discriminacao += Environment.NewLine + Environment.NewLine + "Observação:" + Environment.NewLine
                                                + BuscaObs(sNota,Conn);

                if (objTcDadosServico.Discriminacao[objTcDadosServico.Discriminacao.Length - 1].ToString().Equals("}"))
                {
                    objTcDadosServico.Discriminacao = objTcDadosServico.Discriminacao.Remove(objTcDadosServico.Discriminacao.Length - 1);
                }

                sQuery = new StringBuilder();
                sQuery.Append(" select ");
                sQuery.Append(" cidades.cd_municipio ");
                sQuery.Append(" from  empresa ");
                sQuery.Append(" left join cidades on (cidades.nm_cidnor = empresa.nm_cidnor) ");
                sQuery.Append(" where empresa.cd_empresa = '" + belStatic.codEmpresaNFe + "'");


                Command = new FbCommand(sQuery.ToString(), Conn);
                objTcDadosServico.CodigoMunicipio = Command.ExecuteScalar().ToString();
                objTcDadosServico.CodigoCnae = ""; //não é obrigatório
            }
            catch (Exception ex)
            {
                throw;
            }

            return objTcDadosServico;
        }


        private TcValores BuscaValores(FbConnection Conn, string sNota, int iNaturezaOperacao)
        {
            TcValores objTcValores = new TcValores();

            try
            {
                StringBuilder sQuery = new StringBuilder();
                sQuery.Append(" select first 1 ");
                sQuery.Append(" nf.vl_servico ValorServicos, ");
                //sQuery.Append(" nf.vl_totnf ValorDeducoes, ");
                sQuery.Append(" nf.vl_pis_serv ValorPis, ");
                sQuery.Append(" nf.vl_cofins_serv ValorCofins, ");
                sQuery.Append(" nf.vl_inss ValorInss, ");
                sQuery.Append(" nf.vl_totir  ValorIr, ");
                sQuery.Append(" nf.vl_csll_serv vl_csll_serv, ");
                sQuery.Append(" COALESCE(TPDOC.st_retem_iss,'N')IssRetido  , ");//sim ou não
                sQuery.Append(" nf.vl_iss ValorIss, ");
                //sQuery.Append(" --  nf. OutrasRetencoes, ");
                sQuery.Append(" nf.vl_servico BaseCalculo, ");
                sQuery.Append(" movitem.vl_aliqserv Aliquota, ");
                sQuery.Append(" nf.vl_iss  ValorIssRetido ");
                //sQuery.Append(" -- nf.vl_d   DescontoCondicionado, ");
                //sQuery.Append(" -- nf.vl_ DescontoIncondicionado ");
                sQuery.Append(" from nf inner join movitem ");
                sQuery.Append(" on nf.cd_nfseq = movitem.cd_nfseq and ");
                sQuery.Append(" nf.cd_empresa  = movitem.cd_empresa ");
                sQuery.Append(" inner join tpdoc on nf.cd_tipodoc = tpdoc.cd_tipodoc ");
                sQuery.Append(" where nf.cd_nfseq = '" + sNota + "' and ");
                sQuery.Append(" nf.cd_empresa = '" + belStatic.codEmpresaNFe + "'");

                FbCommand Comand = new FbCommand(sQuery.ToString(), Conn);
                Comand.ExecuteNonQuery();
                FbDataReader dr = Comand.ExecuteReader();
                dr.Read();

                bool bNaoDestacaValor = false;
                HLP.bel.NFe.GeraXml.Globais LeRegWin = new HLP.bel.NFe.GeraXml.Globais();


                if ((iNaturezaOperacao == 1) && (Convert.ToBoolean((LeRegWin.LeRegConfig("DestacaImpTribMun") == "" ? "false" : LeRegWin.LeRegConfig("DestacaImpTribMun"))) == true))
                {
                    bNaoDestacaValor = true;
                }

                objTcValores.ValorServicos = Convert.ToDecimal(dr["ValorServicos"].ToString());
                objTcValores.ValorDeducoes = 0; //Convert.ToDecimal(dr["ValorDeducoes"].ToString());
                objTcValores.ValorPis = (bNaoDestacaValor == true ? 0 : Convert.ToDecimal(dr["ValorPis"].ToString())); //conceito passado pela lorenzon
                objTcValores.ValorCofins = (bNaoDestacaValor == true ? 0 : Convert.ToDecimal(dr["ValorCofins"].ToString())); //conceito passado pela lorenzon
                objTcValores.ValorInss = Convert.ToDecimal(dr["ValorInss"].ToString());
                objTcValores.ValorIr = (bNaoDestacaValor == true ? 0 : Convert.ToDecimal(dr["ValorIr"].ToString())); //conceito passado pela lorenzon
                objTcValores.ValorCsll = (bNaoDestacaValor == true ? 0 : Convert.ToDecimal(dr["vl_csll_serv"].ToString())); //conceito passado pela lorenzon
                objTcValores.IssRetido = (dr["IssRetido"].ToString() == "S" ? 1 : 2); //OS_26219
                objTcValores.ValorIss = (objTcValores.IssRetido == 2 ? Convert.ToDecimal(dr["ValorIss"].ToString()) : 0); // se não for retido joga no valor ISS //OS_26219
                objTcValores.OutrasRetencoes = 0;// Convert.ToDecimal(dr["OutrasRetencoes"].ToString());
                objTcValores.BaseCalculo = Convert.ToDecimal(dr["BaseCalculo"].ToString());
                objTcValores.Aliquota = Convert.ToDecimal(dr["Aliquota"].ToString());
                objTcValores.ValorIssRetido = (objTcValores.IssRetido == 1 ? Convert.ToDecimal(dr["ValorIssRetido"].ToString()) : 0); // ser for retido joga no valor iss retido //OS_26219
                objTcValores.DescontoCondicionado = 0;// Convert.ToDecimal(dr["DescontoCondicionado"].ToString());
                objTcValores.DescontoIncondicionado = 0;// Convert.ToDecimal(dr["DescontoIncondicionado"].ToString());
                objTcValores.ValorLiquidoNfse = objTcValores.CalculaValorLiquido();

                return objTcValores;
            }
            catch (Exception ex)
            {
                throw;
            }



        }


        public string BuscaObs(string sNF, FbConnection Conn)
        {
            string sObs = "";
            try
            {
                StringBuilder sSql = new StringBuilder();

                //Campos do Select
                sSql.Append("Select ");
                sSql.Append("nf.ds_anota ");

                if (((belStatic.sNomeEmpresa == "MOGPLAST") || (belStatic.sNomeEmpresa == "TSA")) && (belStatic.codEmpresaNFe == "003"))
                {
                    sSql.Append(", nf.cd_nfseq_fat_origem ");
                }
                if (belStatic.sNomeEmpresa == "MACROTEX")
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
                sSql.Append(belStatic.codEmpresaNFe);
                sSql.Append("')");
                sSql.Append(" and ");
                sSql.Append("(nf.cd_nfseq = '");
                sSql.Append(sNF);
                sSql.Append("') ");

                sObs = RetornaBlob(sSql, belStatic.codEmpresaNFe, Conn);
                if (sObs.IndexOf("\\fs") != -1)// DIEGO - OS_24854 
                {
                    sObs = sObs.Substring((sObs.IndexOf("\\fs") + 6), sObs.Length - (sObs.IndexOf("\\fs") + 6));
                }

                if (belStatic.sNomeEmpresa.Equals("LORENZON"))
                {
                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append("select prazos.ds_prazo, vendedor.nm_vend , clifor.cd_clifor from nf ");
                    sQuery.Append("inner join clifor on nf.cd_clifor = clifor.cd_clifor ");
                    sQuery.Append("inner join prazos on nf.cd_prazo = prazos.cd_prazo ");
                    sQuery.Append(" inner join vendedor  on nf.cd_vendint = vendedor.cd_vend ");
                    sQuery.Append("where nf.cd_nfseq = '" + sNF + "' ");
                    sQuery.Append("and nf.cd_empresa = '" + belStatic.codEmpresaNFe + "' ");
                    FbCommand cmd = new FbCommand(sQuery.ToString(), Conn);
                    FbDataReader dr = cmd.ExecuteReader();
                    string sMsgLorenzon = "";


                    while (dr.Read())
                    {
                        sMsgLorenzon = "COND.PGTO = " + dr["ds_prazo"].ToString() + " | VENDEDOR = " + dr["nm_vend"].ToString() + " | COD. CLIENTE = " + dr["cd_clifor"].ToString();
                    }

                    sMsgLorenzon = sMsgLorenzon + Environment.NewLine + Environment.NewLine;
                    if (sMsgLorenzon != "")
                    {
                        sObs = sMsgLorenzon + sObs;
                    }
                }

                return sObs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string RetornaBlob(StringBuilder sComando, string sEmp, FbConnection Conn)
        {
            try
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
                if (HLP.bel.Static.belStatic.sNomeEmpresa == "MACROTEX")
                {
                    string sVendedor = string.Empty;
                    string sPedidoCliente = string.Empty;

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
                if (((belStatic.sNomeEmpresa == "MOGPLAST") || (belStatic.sNomeEmpresa == "TSA")) && (sEmp == "003"))
                {
                    string sNFOrigem = string.Empty;
                    string sEmiOrigem = string.Empty;

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
            catch (Exception)
            {

                throw;
            }
        }        
    }
}
