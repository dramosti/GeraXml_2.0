using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HLP.bel.NFes;
using FirebirdSql.Data.FirebirdClient;
using HLP.bel;
using HLP.bel.Static;

namespace HLP.Dao.NFes
{
    public class daotcIdentificacaoRps
    {
        tcIdentificacaoRps objtcIdentificacaoRps;

        public tcIdentificacaoRps BuscatcIdentificacaoRps(FbConnection Conn, string sNfseq)
        {
            try
            {
                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("select nf.cd_notafis, coalesce(nf.cd_serie,'00001')cd_serie from nf ");
                sQuery.Append("where nf.cd_nfseq = '" + sNfseq + "' and ");
                sQuery.Append("nf.cd_empresa = '" + belStatic.codEmpresaNFe + "'");
                
                FbCommand Comand = new FbCommand(sQuery.ToString(), Conn);
                Comand.ExecuteNonQuery();
                FbDataReader dr = Comand.ExecuteReader();
                dr.Read();

                objtcIdentificacaoRps = new tcIdentificacaoRps();
                objtcIdentificacaoRps.Nfseq = sNfseq;
                objtcIdentificacaoRps.Numero = dr["cd_notafis"].ToString();
                objtcIdentificacaoRps.Serie = dr["cd_serie"].ToString();
                objtcIdentificacaoRps.Tipo = 1; //Tratar;
                
                return objtcIdentificacaoRps;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
