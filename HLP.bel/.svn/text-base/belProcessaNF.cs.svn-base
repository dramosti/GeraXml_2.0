using System;
using System.Collections.Generic;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using System.Data;

namespace HLP.bel
{
    public class belProcessaNF
    {
        public string pEmpresa { get; set; }
        public string pCdNfseq { get; set; }
        public FbConnection pConn { get; set; }
        private bool pbGravarCdDupli { get; set; }
        public string psNM_Cliente { get; set; }
        public string psNM_Banco { get; set; }
        public List<string> plsNotas { get; set; }

        public string GerarNumeroNF(string sEmpresa, string sGrupoNF)
        {
            string sNumeroNF = string.Empty;

            StringBuilder SsqlNumeroNF = new StringBuilder();
            SsqlNumeroNF.Append("Select ");
            SsqlNumeroNF.Append("MAX(NF.CD_NOTAFIS) AS CD_NOTAFIS, ");
            SsqlNumeroNF.Append("MAX(NF.CD_RPS) AS CD_RPS ");
            SsqlNumeroNF.Append("from NF ");
            SsqlNumeroNF.Append("where ");
            SsqlNumeroNF.Append("(NF.cd_empresa ='");
            SsqlNumeroNF.Append(sEmpresa);
            SsqlNumeroNF.Append("') and ");
            if ((sGrupoNF == "") || (sGrupoNF == "0"))
            {
                SsqlNumeroNF.Append("(NF.CD_GRUPONF IS NULL OR NF.CD_GRUPONF='' OR NF.CD_GRUPONF='0') ");
            }
            else
            {
                SsqlNumeroNF.Append("(CD_GRUPONF='" + sGrupoNF + "') ");
            }

            try
            {
                using (FbCommand cmd = new FbCommand(SsqlNumeroNF.ToString(), pConn))
                {
                    if (pConn.State != ConnectionState.Open)
                    {
                        pConn.Open();
                    }
                    sNumeroNF = Convert.ToString(cmd.ExecuteScalar());
                }
            }
            catch (FbException Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally
            {
                pConn.Close();
            }

            return sNumeroNF;
        }

        private bool BuscaCdDupli()
        {
            bool bGravarCdDupli = false;

            StringBuilder sSql = new StringBuilder();

            sSql.Append("Select ");
            sSql.Append("control.cd_conteud ");
            sSql.Append("from control ");
            sSql.Append("where ");
            sSql.Append("(control.cd_nivel = '");
            sSql.Append("1355");
            sSql.Append("')");

            if ((psNM_Banco.ToUpper().IndexOf("COMERCIOC") == -1) && (psNM_Banco.ToUpper().IndexOf("CERAMICAC") == -1))
            {
                using (FbCommand cmd = new FbCommand(sSql.ToString(), pConn))
                {
                    if (pConn.State != ConnectionState.Open)
                    {
                        pConn.Open();
                    }
                    bGravarCdDupli = (cmd.ExecuteScalar().ToString() == "S" ? true : false);

                    pConn.Close();
                }
            }
            else
            {
                bGravarCdDupli = true;
            }

            return bGravarCdDupli;

        }

        public void RegravaDuplicata(string sEmp, List<string> sNotas)
        {


        }

    }
}
