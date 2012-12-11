using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HLP.bel.NFes;
using FirebirdSql.Data.FirebirdClient;
using HLP.bel.Static;

namespace HLP.Dao.NFes
{
    public class daoConstrucaoCivil
    {

        tcDadosConstrucaoCivil objtcDadosConstrucaoCivil;
        public tcDadosConstrucaoCivil RettcDadosConstrucaoCivil(FbConnection Conn, string sNota)
        {

            try
            {
                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("select coalesce(clifor.cd_art,'') Art, coalesce(clifor.cd_obra,'')CodigoObra {0}");
                sQuery.Append("from nf inner join clifor on nf.cd_clifor = clifor.cd_clifor {0}");
                sQuery.Append("where nf.cd_nfseq = '{1}' and nf.cd_empresa = '{2}' {0}");

                string sQueryEnd = string.Format(sQuery.ToString(), Environment.NewLine, sNota, belStatic.codEmpresaNFe);

                FbCommand cmd = new FbCommand(sQueryEnd, Conn);
                Conn.Open();
                FbDataReader dr = cmd.ExecuteReader();
                objtcDadosConstrucaoCivil = new tcDadosConstrucaoCivil();
                while (dr.Read())
                {
                    objtcDadosConstrucaoCivil.Art = dr["Art"].ToString();
                    objtcDadosConstrucaoCivil.CodigoObra = dr["CodigoObra"].ToString();
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally { Conn.Close(); }


            return objtcDadosConstrucaoCivil;
        }

    }
}
