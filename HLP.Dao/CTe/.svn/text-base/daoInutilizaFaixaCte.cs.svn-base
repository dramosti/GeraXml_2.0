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
    public class daoInutilizaFaixaCte
    {
        belConnection cx = new belConnection();
        public belInutilizaFaixaCte objBelInutiliza = new belInutilizaFaixaCte();

        public void PopulaDadosInutilizacao(string sNumInicial, string sNumFinal, string sJustificativa)
        {
            try
            {
                objBelInutiliza = new belInutilizaFaixaCte();

                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("Select ");
                sQuery.Append("coalesce(empresa.cd_ufnor,'') cUF,");
                sQuery.Append("coalesce(empresa.cd_cgc,'')CNPJ ");
                sQuery.Append("from empresa ");
                sQuery.Append("where empresa.cd_empresa ='" + belStatic.CodEmpresaCte + "' ");
                FbCommand fbConn = new FbCommand(sQuery.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                fbConn.ExecuteNonQuery();
                FbDataReader dr = fbConn.ExecuteReader();
                dr.Read();

                bel.belUF objbelUf = new bel.belUF();
                objbelUf.SiglaUF = dr["cUF"].ToString();

                objBelInutiliza.versao = "1.03";
                objBelInutiliza.tpAmb = belStatic.TpAmb.ToString();
                objBelInutiliza.xServ = "INUTILIZAR";
                objBelInutiliza.cUF = objbelUf.CUF;
                objBelInutiliza.ano = HLP.Util.Util.GetDateServidor().ToString("yy");
                objBelInutiliza.CNPJ = util.TiraSimbolo(dr["CNPJ"].ToString());
                objBelInutiliza.mod = "57";
                objBelInutiliza.serie = "1";
                objBelInutiliza.nCTIni = sNumInicial;
                objBelInutiliza.nCTFin = sNumFinal;
                objBelInutiliza.xJust = sJustificativa;
                objBelInutiliza.Id = GeraChave(objBelInutiliza.cUF, objBelInutiliza.CNPJ, objBelInutiliza.nCTIni, objBelInutiliza.nCTFin);
            }
            catch (Exception)
            {
                throw;
            }
            finally { cx.Close_Conexao(); }

        }

        private string GeraChave(string sCodUf, string sCNPJ, string sNumInicial, string sNumFinal)
        {
            string sChave = "";
            try
            {

                sChave = "ID" + sCodUf + sCNPJ + "57" + "001" + sNumInicial.PadLeft(9, '0') + sNumFinal.PadLeft(9, '0');
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sChave;
        }
    }
}
