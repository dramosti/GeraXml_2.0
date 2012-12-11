using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using HLP.bel.NFes;
using HLP.bel.Static;
using HLP.bel;

namespace HLP.Dao.NFes
{
    public class daoRecepcao
    {
        string sReciboProt;
        tcLoteRps objLoteRpsAlter;
        belConnection cx = new belConnection();
        public daoRecepcao() { }
        public daoRecepcao(string sReciboProt, bel.NFes.tcLoteRps objLoteRpsAlter)
        {
            this.sReciboProt = sReciboProt;
            this.objLoteRpsAlter = objLoteRpsAlter;

        }

        public void GravaRecibo()
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                for (int i = 0; i < objLoteRpsAlter.Rps.Count; i++)
                {
                    sSql = new StringBuilder();
                    sSql.Append("update nf ");
                    sSql.Append("set cd_recibonfe ='");
                    sSql.Append(sReciboProt);
                    sSql.Append("' ");
                    sSql.Append("where ");
                    sSql.Append("cd_empresa ='");
                    sSql.Append(belStatic.codEmpresaNFe);
                    sSql.Append("' ");
                    sSql.Append("and ");
                    sSql.Append("cd_nfseq ='");
                    sSql.Append(objLoteRpsAlter.Rps[i].InfRps.IdentificacaoRps.Nfseq);
                    sSql.Append("'");
                    sSql.Append(" and coalesce(cd_recibonfe, '') = ''");

                    using (FbCommand cmdUpdate = new FbCommand(sSql.ToString(), cx.get_Conexao()))
                    {
                        cx.Open_Conexao();
                        cmdUpdate.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally { cx.Close_Conexao(); }

        }

        /// <summary>
        /// Limpa Recibo no Envio
        /// </summary>
        /// <param name="objListaLote"></param>
        public void LimpaRecibo(tcLoteRps objListaLote)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                for (int i = 0; i < objListaLote.Rps.Count; i++)
                {
                    sSql = new StringBuilder();
                    sSql.Append("update nf ");
                    sSql.Append("set cd_recibonfe ='' ");
                    sSql.Append("where ");
                    sSql.Append("cd_empresa ='");
                    sSql.Append(belStatic.codEmpresaNFe);
                    sSql.Append("' ");
                    sSql.Append("and ");
                    sSql.Append("cd_nfseq ='");
                    sSql.Append(objLoteRpsAlter.Rps[i].InfRps.IdentificacaoRps.Nfseq);
                    sSql.Append("'");

                    using (FbCommand cmdUpdate = new FbCommand(sSql.ToString(), cx.get_Conexao()))
                    {
                        cx.Open_Conexao();
                        cmdUpdate.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally { cx.Close_Conexao(); }
        }

        /// <summary>
        /// Limpa Recibo na Busca de Retorno
        /// </summary>
        /// <param name="objListaLote"></param>
        public void LimpaRecibo()
        {
            StringBuilder sSql = new StringBuilder();
            List<string> objListaNfseq = BuscaListaDeNotasLote();
            try
            {
                for (int i = 0; i < objListaNfseq.Count; i++)
                {
                    sSql = new StringBuilder();
                    sSql.Append("update nf ");
                    sSql.Append("set cd_recibonfe ='' ");
                    sSql.Append("where ");
                    sSql.Append("cd_empresa ='");
                    sSql.Append(belStatic.codEmpresaNFe);
                    sSql.Append("' ");
                    sSql.Append("and ");
                    sSql.Append("cd_nfseq ='");
                    sSql.Append(objListaNfseq[i]);
                    sSql.Append("'");

                    using (FbCommand cmdUpdate = new FbCommand(sSql.ToString(), cx.get_Conexao()))
                    {
                        cx.Open_Conexao();
                        cmdUpdate.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally { cx.Close_Conexao(); }
        }



        /// <summary>
        /// Altera Status da Nota e Salva Numero da NFse
        /// </summary>
        /// <param name="objListaNFse"></param>
        public void AlteraStatusDaNota(List<TcInfNfse> objListaNFse)
        {
            try
            {
                for (int i = 0; i < objListaNFse.Count; i++)
                {
                    StringBuilder sSql = new StringBuilder();
                    sSql.Append("update nf set st_nfe = 'S' ,");
                    sSql.Append("CD_NUMERO_NFSE ='" + objListaNFse[i].Numero + "' ,");
                    sSql.Append("CD_VERIFICACAO_NFSE  = '" + objListaNFse[i].CodigoVerificacao + "' ");
                    sSql.Append("Where cd_nfseq = '" + objListaNFse[i].IdentificacaoRps.Nfseq + "' ");
                    sSql.Append("and ");
                    sSql.Append("cd_empresa = '");
                    sSql.Append(belStatic.codEmpresaNFe);
                    sSql.Append("'");
                    using (FbCommand cmdUpdate = new FbCommand(sSql.ToString(), cx.get_Conexao()))
                    {
                        cx.Open_Conexao();
                        cmdUpdate.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally { cx.Close_Conexao(); }
        }


        public void VerificaNotasParaCancelar(List<TcInfNfse> objListaNFse)
        {
            try
            {
                for (int i = 0; i < objListaNFse.Count; i++)
                {
                    if (objListaNFse[i].NfseSubstituida != "")
                    {
                        StringBuilder sSql = new StringBuilder();
                        sSql.Append("update nf ");
                        sSql.Append("set cd_recibocanc = '");
                        sSql.Append(objListaNFse[i].NfseSubstituida);
                        sSql.Append("' ");
                        sSql.Append("where ");
                        sSql.Append("cd_empresa = '");
                        sSql.Append(belStatic.codEmpresaNFe);
                        sSql.Append("' ");
                        sSql.Append("and ");
                        sSql.Append("cd_numero_nfse = '");
                        sSql.Append(objListaNFse[i].NfseSubstituida);
                        sSql.Append("'");
                        using (FbCommand cmdUpdate = new FbCommand(sSql.ToString(), cx.get_Conexao()))
                        {
                            cx.Open_Conexao();
                            cmdUpdate.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally { cx.Close_Conexao(); }

        }


        public string BuscaNumProtocolo(string sSequencia)
        {
            try
            {
                StringBuilder sSql = new StringBuilder();
                sSql.Append("select nf.cd_recibonfe from nf ");
                sSql.Append("where ");
                sSql.Append("cd_empresa ='");
                sSql.Append(belStatic.codEmpresaNFe);
                sSql.Append("' ");
                sSql.Append("and ");
                sSql.Append("cd_nfseq ='");
                sSql.Append(sSequencia);
                sSql.Append("'");

                FbCommand comand = new FbCommand(sSql.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                this.sReciboProt = comand.ExecuteScalar().ToString();
                return sReciboProt;
            }
            catch (Exception)
            {
                throw;
            }
            finally { cx.Close_Conexao(); }

        }


        private List<string> BuscaListaDeNotasLote()
        {
            try
            {
                StringBuilder sSql = new StringBuilder();
                sSql.Append("select nf.cd_nfseq from nf ");
                sSql.Append("where ");
                sSql.Append("cd_empresa ='");
                sSql.Append(belStatic.codEmpresaNFe);
                sSql.Append("' ");
                sSql.Append("and ");
                sSql.Append("cd_recibonfe ='");
                sSql.Append(this.sReciboProt);
                sSql.Append("'");

                FbCommand comand = new FbCommand(sSql.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                comand.ExecuteNonQuery();
                FbDataReader dr = comand.ExecuteReader();

                List<string> objListaSequencias = new List<string>();
                while (dr.Read())
                {
                    objListaSequencias.Add(dr["cd_nfseq"].ToString());
                }
                return objListaSequencias;

            }
            catch (Exception)
            {
                throw;
            }
            finally { cx.Close_Conexao(); }

        }





    }
}
