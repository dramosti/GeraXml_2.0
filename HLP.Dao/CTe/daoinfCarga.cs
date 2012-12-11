using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HLP.bel.CTe;
using FirebirdSql.Data.FirebirdClient;
using HLP.bel.Static;
using HLP.bel;

namespace HLP.Dao.CTe
{
    public class daoinfCarga
    {
        public void PopulainfCarga(belinfCte objbelinfCte, FbConnection conn, string sCte)
        {
            try
            {
                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("Select ");
                sQuery.Append("coalesce(conhecim.ds_prodpred,'')proPred, ");
                sQuery.Append("coalesce(nfconhec.vl_nf,'')vMerc ");
                sQuery.Append("from conhecim ");
                sQuery.Append("join nfconhec on  conhecim.nr_lanc = nfconhec.nr_lancconhecim ");
                sQuery.Append("join empresa on conhecim.cd_empresa = empresa.cd_empresa ");
                sQuery.Append("where   conhecim.nr_lanc ='" + sCte + "' ");
                sQuery.Append("and empresa.cd_empresa ='" + belStatic.CodEmpresaCte + "'");



                FbCommand fbConn = new FbCommand(sQuery.ToString(), conn);
                fbConn.ExecuteNonQuery();
                FbDataReader dr = fbConn.ExecuteReader();

                objbelinfCte.infCTeNorm = new belinfCTeNorm();
                objbelinfCte.infCTeNorm.infCarga = new belinfCarga();
                while (dr.Read())
                {
                    objbelinfCte.infCTeNorm.infCarga.vCarga += Convert.ToDecimal(dr["vMerc"].ToString().Replace(".", ","));
                    objbelinfCte.infCTeNorm.infCarga.proPred =belUtil.TiraSimbolo( dr["proPred"].ToString(),"");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
