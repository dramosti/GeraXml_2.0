using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HLP.bel.NFes;
using HLP.bel;
using FirebirdSql.Data.FirebirdClient;
using HLP.bel.Static;

namespace HLP.Dao.NFes
{
    public class daoCancelamento
    {

        public TcPedidoCancelamento BuscaDadosParaCancelamento(FbConnection Conn, string sCodCancelamento, string sSequencia)
        {
            try
            {
                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("select cidades.cd_municipio, empresa.cd_cgc Cnpj, empresa.cd_inscrmu, ");
                sQuery.Append("coalesce(nf.cd_numero_nfse,'')cd_numero_nfse ");
                sQuery.Append("from nf inner join empresa on nf.cd_empresa = empresa.cd_empresa ");
                sQuery.Append("inner join cidades on (cidades.nm_cidnor = empresa.nm_cidnor) ");
                sQuery.Append("where nf.cd_nfseq = '" + sSequencia + "' and ");
                sQuery.Append("nf.cd_empresa = '" + belStatic.codEmpresaNFe + "'");

                FbCommand Command = new FbCommand(sQuery.ToString(), Conn);
                FbDataReader dr = Command.ExecuteReader();
                dr.Read();

                TcPedidoCancelamento objCancelamento = new TcPedidoCancelamento();
                objCancelamento.InfPedidoCancelamento = new tcInfPedidoCancelamento();
                objCancelamento.InfPedidoCancelamento.CodigoCancelamento = sCodCancelamento;
                objCancelamento.InfPedidoCancelamento.IdentificacaoNfse = new tcIdentificacaoNfse();
                objCancelamento.InfPedidoCancelamento.IdentificacaoNfse.CodigoMunicipio = dr["cd_municipio"].ToString();
                objCancelamento.InfPedidoCancelamento.IdentificacaoNfse.Numero = dr["cd_numero_nfse"].ToString();
                objCancelamento.InfPedidoCancelamento.IdentificacaoNfse.Cnpj = dr["Cnpj"].ToString();
                objCancelamento.InfPedidoCancelamento.IdentificacaoNfse.InscricaoMunicipal = dr["cd_inscrmu"].ToString();

                return objCancelamento;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CancelarNFseSistema(string sNumNfse, FbConnection Conn)
        {
            try
            {
                StringBuilder sSql = new StringBuilder();
                sSql.Append("update nf ");
                sSql.Append("set cd_recibocanc = '");
                sSql.Append("CANCELADA");
                sSql.Append("' ");
                sSql.Append("where ");
                sSql.Append("cd_empresa = '");
                sSql.Append(belStatic.codEmpresaNFe);
                sSql.Append("' ");
                sSql.Append("and ");
                sSql.Append("cd_numero_nfse = '");
                sSql.Append(sNumNfse);
                sSql.Append("'");
                using (FbCommand cmdUpdate = new FbCommand(sSql.ToString(), Conn))
                {
                    cmdUpdate.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

    }
}
