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
    public class daoCancelaCte
    {
        belConnection cx = new belConnection();
        public belCancelaCte objBelCancelaCte = new belCancelaCte();

        public void PopulaDadosCancelamento(string sCodConhecimento, string sJustificativa)
        {
            try
            {

                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("Select ");
                sQuery.Append("conhecim.cd_chavecte chCTe, ");
                sQuery.Append("conhecim.cd_nprotcte nProt ");
                sQuery.Append("from conhecim ");
                sQuery.Append("where conhecim.cd_conheci ='" + sCodConhecimento + "' ");
                sQuery.Append("and conhecim.cd_empresa = '" + belStatic.CodEmpresaCte + "'");


                FbCommand fbConn = new FbCommand(sQuery.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                fbConn.ExecuteNonQuery();
                FbDataReader dr = fbConn.ExecuteReader();
                dr.Read();
                objBelCancelaCte.versao = "1.03";
                objBelCancelaCte.Id = "ID" + dr["chCTe"].ToString();
                objBelCancelaCte.tpAmb = belStatic.TpAmb.ToString();
                objBelCancelaCte.xServ = "CANCELAR";
                objBelCancelaCte.chCTe = dr["chCTe"].ToString();
                objBelCancelaCte.nProt = dr["nProt"].ToString();
                objBelCancelaCte.xJust = sJustificativa;
            }
            catch (Exception)
            {
                throw;
            }
            finally { cx.Close_Conexao(); }
        }
    }
}
