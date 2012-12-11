using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using HLP.bel.CTe;
using System.Windows.Forms;
using HLP.bel.Static;
using HLP.bel;


namespace HLP.Dao.CTe
{
    public class daoEmpresa
    {
        public List<string> BuscaCodigoEmpresas()
        {
            List<string> slCodigos = new List<string>();
            belConnection cx = new belConnection();

            try
            {
                using (FbCommand cmd = new FbCommand("Select empresa.cd_empresa from empresa order by empresa.cd_empresa", cx.get_Conexao()))
                {
                    cx.Open_Conexao();
                    cmd.ExecuteNonQuery();
                    FbDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        slCodigos.Add(dr["cd_empresa"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cx.Close_Conexao();
            }

            return slCodigos;
        }


        public void BuscaUFeAmb()
        {

            belConnection cx = new belConnection();
            try
            {

                StringBuilder sSql = new StringBuilder();
                sSql.Append("Select ");
                sSql.Append("empresa.nm_empresa, ");
                sSql.Append("empresa.cd_ufnor, ");
                sSql.Append("coalesce(empresa.st_ambiente, '2') tpAmb ");
                sSql.Append("from empresa ");
                sSql.Append("where ");
                sSql.Append("cd_empresa ='");
                sSql.Append(belStatic.CodEmpresaCte);
                sSql.Append("'");

                using (FbCommand cmd = new FbCommand(sSql.ToString(), cx.get_Conexao()))
                {
                    cx.Open_Conexao();
                    cmd.ExecuteNonQuery();

                    FbDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        belStatic.Sigla_uf = dr["cd_ufnor"].ToString();
                        belStatic.TpAmb = Convert.ToInt32(dr["tpAmb"].ToString());
                        //belStatic.sNomeEmpresa = dr["nm_empresa"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { cx.Close_Conexao(); }
        }



    }
}
