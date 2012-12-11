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
    public class daoImpressao
    {

        public List<belimpressao> BuscaDadosParaImpressao(List<belimpressao> objLista, FbConnection Conn)
        {
            try
            {
                for (int i = 0; i < objLista.Count; i++)
                {
                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append("select nf.cd_verificacao_nfse, nf.cd_numero_nfse from nf ");
                    sQuery.Append("where nf.cd_nfseq = '" + objLista[i].sNfSeq + "' and ");
                    sQuery.Append("nf.cd_empresa = '" + belStatic.codEmpresaNFe + "'");

                    FbCommand Command = new FbCommand(sQuery.ToString(), Conn);
                    if (Conn.State == System.Data.ConnectionState.Closed)
                    {
                        Conn.Open();
                    }
                    FbDataReader dr = Command.ExecuteReader();
                    dr.Read();
                    objLista[i].sNota = dr["cd_numero_nfse"].ToString();
                    objLista[i].sVerificacao = dr["cd_verificacao_nfse"].ToString();
                }
                if (Conn.State == System.Data.ConnectionState.Open)
                {
                    Conn.Close();
                }
                return objLista;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
