using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using HLP.bel.CTe;
using System.Data;
using HLP.bel.Static;
using HLP.bel;

namespace HLP.Dao.CTe
{
    public class daoBuscaDadosGerais
    {
        belConnection cx = new belConnection();

        public DataTable PesquisaGridView(string sCampos, string sWhere)
        {
            try
            {
                DataTable dt = new DataTable();
                string sQuery = "Select "
                                + sCampos
                                + " from conhecim c inner join remetent r on c.cd_remetent = r.cd_remetent"
                                + " Where " + sWhere;

                FbDataAdapter da = new FbDataAdapter(sQuery, cx.get_Conexao());
                dt.Clear();
                cx.Open_Conexao();
                da.Fill(dt);
                da.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cx.Close_Conexao();
            }
        }

        public DataTable PesquisaGridViewContingencia(string sCampos)
        {
            try
            {
                DataTable dt = new DataTable();
                string sQuery = "Select "
                                + sCampos
                                + " from conhecim c inner join remetent r on c.cd_remetent = r.cd_remetent"
                                + " where conhecim.st_contingencia ='S'  and (conhecim.st_cte='N' or  conhecim.st_cte is null)";

                FbDataAdapter da = new FbDataAdapter(sQuery, cx.get_Conexao());
                dt.Clear();
                cx.Open_Conexao();
                da.Fill(dt);
                da.Dispose();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cx.Close_Conexao();
            }
        }


      


        public List<string> VerificaPendenciasdeEnvio()
        {
            belConnection cx = new belConnection();
            try
            {
                List<string> ListaConhecimento = new List<string>();
                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("Select conhecim.nr_lanc from conhecim ");
                sQuery.Append("where conhecim.st_contingencia ='S' ");
                sQuery.Append("and (conhecim.st_cte='N' or  conhecim.st_cte is null)");

                FbCommand fbConn = new FbCommand(sQuery.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                FbDataReader dr = fbConn.ExecuteReader();
                while (dr.Read())
                {
                    ListaConhecimento.Add(dr["nr_lanc"].ToString());
                }
                return ListaConhecimento;
            }
            catch (Exception ex)
            {
                cx.Close_Conexao();
                throw ex;
            }
            finally
            {
                cx.Close_Conexao();
            }
        }

        public string VerificaCampoReciboPreenchido(string sEmp, string sSeq)
        {
            try
            {
                string sValidaRecibo = "";
                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("Select cd_recibocte from conhecim ");
                sQuery.Append("where ");
                sQuery.Append("cd_empresa ='");
                sQuery.Append(sEmp);
                sQuery.Append("' ");
                sQuery.Append("and ");
                sQuery.Append("nr_lanc ='");
                sQuery.Append(sSeq);
                sQuery.Append("'");

                FbCommand fbConn = new FbCommand(sQuery.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                fbConn.ExecuteNonQuery();
                FbDataReader dr = fbConn.ExecuteReader();
                dr.Read();
                return sValidaRecibo = dr["cd_recibocte"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cx.Close_Conexao();
            }

        }

        public string VerificaCampoProtocoloEnvio(string sNumCte)
        {
            belConnection objbelConn = new belConnection();
            try
            {

                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("Select coalesce(conhecim.cd_nprotcte,'')cd_nprotcte ");
                sQuery.Append("from conhecim  ");
                sQuery.Append("where conhecim.cd_conheci ='" + sNumCte + "' ");
                sQuery.Append("and conhecim.cd_empresa ='" + belStatic.CodEmpresaCte + "' AND CONHECIM.st_cte = 'S'");
                FbCommand fbConn = new FbCommand(sQuery.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                fbConn.ExecuteNonQuery();
                FbDataReader dr = fbConn.ExecuteReader();
                dr.Read();
                string sProt = dr["cd_nprotcte"].ToString();
                return sProt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cx.Close_Conexao();
            }

        }

        public string BuscaChaveRetornoCte(string sNumCte)
        {
            try
            {
                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("Select coalesce(conhecim.cd_chavecte,'')cd_chavecte ");
                sQuery.Append("from conhecim  ");
                sQuery.Append("where conhecim.cd_conheci ='" + sNumCte + "' ");
                sQuery.Append("and conhecim.cd_empresa ='" + belStatic.CodEmpresaCte + "' AND CONHECIM.st_cte = 'S'");
                FbCommand fbConn = new FbCommand(sQuery.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                fbConn.ExecuteNonQuery();
                FbDataReader dr = fbConn.ExecuteReader();
                dr.Read();

                return dr["cd_chavecte"].ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cx.Close_Conexao();
            }

        }

        public void LiberaNotaParaReenvio(string sNR_LANC) 
        {
            try
            {
                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("UPDATE CONHECIM C  SET C.st_cte = 'N' WHERE ");
                sQuery.Append("C.nr_lanc = '" + sNR_LANC + "' AND C.cd_empresa = '" + belStatic.CodEmpresaCte + "'  ");
                FbCommand fbConn = new FbCommand(sQuery.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                fbConn.ExecuteNonQuery();
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        public string BuscaChaveRetornoCteSeq(string sNumSeq)
        {
            try
            {
                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("Select coalesce(conhecim.cd_chavecte,'')cd_chavecte ");
                sQuery.Append("from conhecim  ");
                sQuery.Append("where conhecim.nr_lanc ='" + sNumSeq + "' ");
                sQuery.Append("and conhecim.cd_empresa ='" + belStatic.CodEmpresaCte + "'");
                FbCommand fbConn = new FbCommand(sQuery.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                fbConn.ExecuteNonQuery();
                FbDataReader dr = fbConn.ExecuteReader();
                dr.Read();
                return dr["cd_chavecte"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cx.Open_Conexao();
            }

        }

        public string BuscaNumeroConhecimento(string sSeq)
        {
            belConnection cx = new belConnection();
            try
            {
                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("Select  conhecim.cd_conheci ");
                sQuery.Append("from conhecim  ");
                sQuery.Append("where conhecim.nr_lanc ='" + sSeq + "' ");
                sQuery.Append("and conhecim.cd_empresa ='" + belStatic.CodEmpresaCte + "'");
                FbCommand fbConn = new FbCommand(sQuery.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                fbConn.ExecuteNonQuery();
                FbDataReader dr = fbConn.ExecuteReader();
                dr.Read();
                return dr["cd_conheci"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cx.Close_Conexao();
            }
        }

        public List<string> ValidaSeqNoBanco(string sEmpresa, List<string> sListSeq)
        {
            try
            {

                StringBuilder sSqlSeqValidas = new StringBuilder();
                sSqlSeqValidas.Append("select ");
                sSqlSeqValidas.Append("c.nr_lanc ");
                sSqlSeqValidas.Append("From conhecim c ");
                sSqlSeqValidas.Append("where ");
                sSqlSeqValidas.Append("((c.cd_conheci is null) or (c.cd_conheci = '')) and (");
                sSqlSeqValidas.Append("c.cd_empresa ='");
                sSqlSeqValidas.Append(sEmpresa);
                sSqlSeqValidas.Append("') and (");
                sSqlSeqValidas.Append("c.nr_lanc in('");

                int iCont = 0;
                foreach (var seq in sListSeq)
                {
                    iCont++;
                    sSqlSeqValidas.Append(seq);
                    if (sListSeq.Count > iCont)
                    {
                        sSqlSeqValidas.Append("','");
                    }
                }
                sSqlSeqValidas.Append("')) ");
                sSqlSeqValidas.Append("Order by c.cd_empresa, c.nr_lanc ");

                FbCommand cmd = new FbCommand(sSqlSeqValidas.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                cmd.ExecuteNonQuery();

                FbDataReader dr = cmd.ExecuteReader();

                List<string> lsSeqValidos = new List<string>();

                while (dr.Read())
                {
                    lsSeqValidos.Add(dr["nr_lanc"].ToString());
                }

                return lsSeqValidos;

            }
            catch (Exception)
            {

                throw;
            }
            finally { cx.Close_Conexao(); }

        }

        public string BuscaNumProtocolo(string sNR_LANC)
        {
            try
            {
                string squery = "select conhecim.cd_nprotcte from conhecim where conhecim.cd_conheci = '{0}' and conhecim.cd_empresa = '{1}'";

                squery = string.Format(squery, sNR_LANC, belStatic.codEmpresaNFe);
                FbCommand fbConn = new FbCommand(squery, cx.get_Conexao());
                cx.Open_Conexao();
                fbConn.ExecuteNonQuery();
                FbDataReader dr = fbConn.ExecuteReader();
                dr.Read();

                string sretorno = dr["cd_nprotcte"].ToString();
                return sretorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cx.Close_Conexao();
            }

        }


    }
}
