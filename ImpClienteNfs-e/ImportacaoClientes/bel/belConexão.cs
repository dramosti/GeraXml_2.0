using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using FirebirdSql.Data.FirebirdClient;

namespace ImportacaoClientes.bel
{
    public class belConexão
    {

        public string sMontaStringConexao(string sCaminhoBanco, string sServidor)
        {
            StringBuilder sbConexao = new StringBuilder();

            sbConexao.Append("User =");
            sbConexao.Append("SYSDBA");
            sbConexao.Append(";");
            sbConexao.Append("Password=");
            sbConexao.Append("masterkey");
            sbConexao.Append(";");
            sbConexao.Append("Database=");
            string sdatabase = sCaminhoBanco;
            sbConexao.Append(sdatabase);
            sbConexao.Append(";");
            sbConexao.Append("DataSource=");
            sbConexao.Append(sServidor);
            sbConexao.Append(";");
            sbConexao.Append("Port=3050;Dialect=3; Charset=NONE;Role=;Connection lifetime=60;Pooling=true; MinPoolSize=0;MaxPoolSize=20000;Packet Size=8192;ServerType=0;");

            return (string)sbConexao.ToString();
        }

    }
}
