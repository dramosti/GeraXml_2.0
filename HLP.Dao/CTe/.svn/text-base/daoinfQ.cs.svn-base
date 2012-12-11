using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HLP.bel.CTe;
using FirebirdSql.Data.FirebirdClient;
using HLP.bel.Static;

namespace HLP.Dao.CTe
{
    public class daoinfQ
    {
        public void PopulainfQ(belinfCte objbelinfCte, FbConnection conn, string sCte)
        {
            try
            {
                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("Select ");
                sQuery.Append("coalesce(nfconhec.cd_um,'')cUnid, ");
                sQuery.Append("coalesce(nfconhec.ds_especie,'') tpMed, ");
                sQuery.Append("sum(coalesce(nfconhec.vl_volume,'')) qCarga_Volume, ");
                sQuery.Append("sum(coalesce(nfconhec.vl_peso,'')) qCarga_Peso ");
                sQuery.Append("from nfconhec ");
                sQuery.Append("join empresa on nfconhec.cd_empresa = empresa.cd_empresa ");
                sQuery.Append("where nfconhec.nr_lancconhecim ='" + sCte + "'");
                sQuery.Append("and empresa.cd_empresa ='" + belStatic.CodEmpresaCte + "' ");
                sQuery.Append("group by  coalesce(nfconhec.cd_um,''), coalesce(nfconhec.ds_especie,'')");


                FbCommand fbConn = new FbCommand(sQuery.ToString(), conn);
                fbConn.ExecuteNonQuery();
                FbDataReader dr = fbConn.ExecuteReader();

                if (objbelinfCte.infCTeNorm == null)
                {
                    objbelinfCte.infCTeNorm = new belinfCTeNorm();
                    objbelinfCte.infCTeNorm.infCarga = new belinfCarga();
                }
                objbelinfCte.infCTeNorm.infCarga.infQ = new List<belinfQ>();

                while (dr.Read())
                {
                    belinfQ objinfQ = new belinfQ();
                    objinfQ.cUnid = "00"; 
                    objinfQ.tpMed = dr["tpMed"].ToString().ToUpper();
                    objinfQ.qCarga = Convert.ToDecimal(dr["qCarga_Volume"].ToString().Replace(".", ","));
                    objbelinfCte.infCTeNorm.infCarga.infQ.Add(objinfQ);

                    objinfQ = new belinfQ();
                    objinfQ.cUnid = dr["cUnid"].ToString();
                    objinfQ.tpMed = "PESO";
                    objinfQ.qCarga = Convert.ToDecimal(dr["qCarga_Peso"].ToString().Replace(".", ","));
                    objbelinfCte.infCTeNorm.infCarga.infQ.Add(objinfQ);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }
    }
}
