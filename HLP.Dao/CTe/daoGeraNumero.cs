using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using HLP.bel.CTe;
using System.Data;
using HLP.bel;
using HLP.bel.Static;

namespace HLP.Dao.CTe
{
    public class daoGeraNumero
    {
        belConnection cx = new belConnection();

        public string BuscaUltimoNumeroConhecimento(string sEmp)
        {
            try
            {
                string sQuery = "";
                if (belStatic.sNomeEmpresa.ToUpper().Equals("SICUPIRA") || belStatic.sNomeEmpresa.ToUpper().Equals("TRANSLILO"))  
                {
                    string sGenerator = "CONHECIM_CTE" + belStatic.CodEmpresaCte;
                    sQuery = "SELECT GEN_ID(" + sGenerator + ", 0 ) FROM RDB$DATABASE";
                    using (FbCommand cmd = new FbCommand(sQuery, cx.get_Conexao()))
                    {
                        cx.Open_Conexao();
                        return Convert.ToString(cmd.ExecuteScalar());
                    }
                }
                else
                {
                    sQuery = "select max(c.cd_conheci) from conhecim c where c.cd_empresa = '" + sEmp + "'";
                    using (FbCommand cmd = new FbCommand(sQuery, cx.get_Conexao()))
                    {
                        cx.Open_Conexao();
                        return Convert.ToString(cmd.ExecuteScalar());
                    }
                }
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

        public List<belNumeroConhec> GeraNumerosConhecimentos(List<string> lsSeq, string sNumAserEmiti, string sEmp)
        {
            try
            {
                List<belNumeroConhec> objlbelNumConhec = new List<belNumeroConhec>();
                belNumeroConhec objbelNumConhec = null;

                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("select c.nr_lanc from conhecim c ");
                sQuery.Append("where ((c.cd_conheci is null) or (c.cd_conheci = '')) ");
                sQuery.Append("and");
                sQuery.Append("((c.cd_empresa = '" + sEmp + "') and ");
                sQuery.Append("(c.nr_lanc in ('");
                int iCont = 0;
                foreach (var sSeq in lsSeq)
                {
                    iCont++;
                    sQuery.Append(sSeq);
                    if (lsSeq.Count > iCont)
                    {
                        sQuery.Append("','");
                    }
                }
                sQuery.Append("')))");
                sQuery.Append(" order by  c.nr_lanc");

                int iCdConhec = Convert.ToInt32(sNumAserEmiti);
                FbCommand cmd = new FbCommand(sQuery.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                cmd.ExecuteNonQuery();
                FbDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    objbelNumConhec = new belNumeroConhec();
                    objbelNumConhec.nfSeq = dr["nr_lanc"].ToString();
                    objbelNumConhec.cdConhec = iCdConhec.ToString();
                    objlbelNumConhec.Add(objbelNumConhec);
                    iCdConhec++;
                }
                return objlbelNumConhec;
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

        public void GravaConhec(string sEmp, belNumeroConhec objbel)
        {
            try
            {
                string sQuery = "";

                if (belStatic.sNomeEmpresa.ToUpper().Equals("SICUPIRA") || belStatic.sNomeEmpresa.ToUpper().Equals("TRANSLILO"))
                {
                    sQuery = "update conhecim set  cd_conheci = '" + objbel.cdConhec.PadLeft(6, '0') + "' "
                                + "where cd_empresa = '" + sEmp + "' "
                                + "and nr_lanc = '" + objbel.nfSeq.PadLeft(7, '0') + "'";
                }
                else
                {
                    sQuery = "update conhecim set  cd_conheci = '" + objbel.cdConhec.PadLeft(6, '7') + "' "
                                  + "where cd_empresa = '" + sEmp + "' "
                                  + "and nr_lanc = '" + objbel.nfSeq.PadLeft(7, '0') + "'";
                }


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

        public void AtualizaGenerator(string sValue)
        {
            try
            {
                string sGenerator = "";
                if (belStatic.sNomeEmpresa.ToUpper().Equals("SICUPIRA") || belStatic.sNomeEmpresa.ToUpper().Equals("TRANSLILO"))
                {
                    sGenerator = "CONHECIM_CTE" + belStatic.CodEmpresaCte; ;
                }
                else
                {
                    sGenerator = "CONHECIM_CTE";
                }

                string sQuery = "SET GENERATOR " + sGenerator + " TO " + sValue;

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


    }
}
