using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HLP.bel.CTe;
using FirebirdSql.Data.FirebirdClient;

namespace HLP.bel.NFe
{
    public class belLorenzon
    {
        public void AlteraDuplicataLorenzon(List<belNumeroNF> objNumeroNfs, int i)
        {
            belConnection cx = new belConnection();
            try
            {

                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("UPDATE dupnotar SET dupnotar.cd_notafis = '" + objNumeroNfs[i].Cdnotafis + "' ");
                sQuery.Append("where dupnotar.cd_empresa = '" + Static.belStatic.codEmpresaNFe + "' ");
                sQuery.Append("and dupnotar.cd_nfseq = '" + objNumeroNfs[i].Nfseq + "'");
                FbCommand cmdLorenzon = new FbCommand(sQuery.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                cmdLorenzon.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw;
            }
            finally { cx.Close_Conexao(); }
          
        }
    }
}
