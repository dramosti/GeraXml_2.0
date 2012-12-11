using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using HLP.bel;

namespace HLP.bel.CTe
{
    public class belConnection
    {
        public FbConnection Conn;

        public string sBanco { get; set; }

        public belConnection()
        {
            try
            {
                Conn = new FbConnection(sMontaStringConexao());
                VerificaStatusConn();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static string sMontaStringConexao()
        {
            try
            {
                Globais LeRegWin = new Globais();
                belGlobais MontaStringConexao = new belGlobais();
                StringBuilder sbConexao = new StringBuilder();

                sbConexao.Append("User =");
                sbConexao.Append("SYSDBA");
                sbConexao.Append(";");
                sbConexao.Append("Password=");
                sbConexao.Append("masterkey");
                sbConexao.Append(";");
                sbConexao.Append("Database=");
                string sdatabase = LeRegWin.LeRegConfig("BancoDados");
                sbConexao.Append(sdatabase);
                sbConexao.Append(";");
                sbConexao.Append("DataSource=");
                sbConexao.Append(LeRegWin.LeRegConfig("Servidor"));
                sbConexao.Append(";");
                sbConexao.Append("Port=3050;Dialect=3; Charset=NONE;Role=;Connection lifetime=15;Pooling=true; MinPoolSize=0;MaxPoolSize=2000;Packet Size=8192;ServerType=0;");
                return (string)sbConexao.ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void VerificaStatusConn()
        {
            if (Conn.State == System.Data.ConnectionState.Closed)
            {
                Conn.Open();
            }
        }
    }
}
