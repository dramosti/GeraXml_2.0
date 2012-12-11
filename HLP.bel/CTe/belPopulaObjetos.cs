using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using FirebirdSql.Data.FirebirdClient;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace HLP.bel.CTe
{
    public class belPopulaObjetos
    {
        public List<belinfCte> objLinfCte = new List<belinfCte>();
        public string sEmp = "";
        public List<string> objlConhec;
        public string sFormEmiss = "";

        public string cUf = "";
        public X509Certificate2 cert;
        public string sPath = "";
        public string sNomeArq = "";

        public belPopulaObjetos(string sEmp, List<string> objlConhec,
                                  string cUf, X509Certificate2 cert)
        {
            this.sEmp = sEmp;
            this.objlConhec = objlConhec;

            this.cUf = cUf;
            this.cert = cert;
            belGlobais objGlobais = new belGlobais();
            sNomeArq = NomeArqCte();
            sPath = util.RetCaminhoPastaPadrao("Envio") + @sNomeArq;

        }




        private string NomeArqCte()
        {
            belConnection cx = new belConnection();
            try
            {
                string sNomeArq = "";
                FbCommand cmd = new FbCommand();
                cmd.Connection = cx.get_Conexao();
                cx.Open_Conexao();
                cmd.CommandText = "SP_CHAVEPRI";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();

                cmd.Parameters.Add("@SNOMEGENERATOR", FbDbType.VarChar, 31).Value = "GEN_NOMEARQXML";

                sNomeArq = cmd.ExecuteScalar().ToString();

                return "Cte_" + sEmp + sNomeArq.PadLeft(15, '0') + ".xml";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { cx.Close_Conexao(); }
        }
    }
}
