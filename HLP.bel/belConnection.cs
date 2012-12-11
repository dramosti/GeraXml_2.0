using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using HLP.bel.NFe.GeraXml;

namespace HLP.bel
{
    public class belConnection
    {
        private FbConnection conexao;

        public belConnection()
        {
            try
            {
               
                conexao = new FbConnection(sMontaStringConexao());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string sMontaStringConexao()
        {
            try
            {
                Globais LeRegWin = new Globais();
                StringBuilder sbConexao = new StringBuilder();

                sbConexao.Append("User =");
                sbConexao.Append("SYSDBA");
                sbConexao.Append(";");
                sbConexao.Append("Password=");
                sbConexao.Append("masterkey");
                sbConexao.Append(";");
                string sPorta = LeRegWin.LeRegConfig("Porta");
                if (sPorta.Trim() != "")
                {
                    sbConexao.Append("Port=" + sPorta + ";");
                }
                sbConexao.Append("Database=");
                string sdatabase = LeRegWin.LeRegConfig("BancoDados");
                sbConexao.Append(sdatabase);
                sbConexao.Append(";");
                sbConexao.Append("DataSource=");
                sbConexao.Append(LeRegWin.LeRegConfig("Servidor"));
                sbConexao.Append(";");
                sbConexao.Append("Dialect=3; Charset=NONE;Role=;Connection lifetime=15;Pooling=true; MinPoolSize=0;MaxPoolSize=2000;Packet Size=8192;ServerType=0;");
                return (string)sbConexao.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <SUMMARY>
        /// Abre a conexão com o banco de dados
        /// </SUMMARY>
        public void Open_Conexao()
        {
            if (this.conexao.State == System.Data.ConnectionState.Closed)
            {
                this.conexao.Open();
            }
        }

        /// <SUMMARY>
        /// Fecha a conexão com o banco de dados
        /// </SUMMARY>
        public void Close_Conexao()
        {
            if (this.conexao.State == System.Data.ConnectionState.Open)
            {
                this.conexao.Close();
            }
        }

        /// <SUMMARY>
        /// Retorna uma conexao com o banco de dados
        /// </SUMMARY>
        /// <RETURNS>Conexão com o banco de dados</RETURNS>
        public FbConnection get_Conexao()
        {
            return this.conexao;
        }
    }
}
