using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using HLP.bel.CTe;
using System.Data;
using HLP.bel;

namespace HLP.Dao.CTe
{
    public class daoGenerator
    {
        belConnection cx = new belConnection();

        public bool VerificaExistenciaGenerator(string sNomeGen)
        {
            try
            {
                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("SELECT RDB$GENERATORS.RDB$GENERATOR_NAME ");
                sQuery.Append("FROM RDB$GENERATORS ");
                sQuery.Append("WHERE (RDB$GENERATORS.RDB$GENERATOR_NAME = '" + sNomeGen + "')");
                FbCommand command = new FbCommand(sQuery.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                return (command.ExecuteScalar().ToString().Trim() != "" ? true : false);
            }
            catch (Exception)
            {
                cx.Close_Conexao();
                return false;
            }
            finally { cx.Close_Conexao(); }
        }

        public void CreateGenerator(string sNomeGen, int iValorIni)
        {
            try
            {
                StringBuilder sQuery = new StringBuilder();
                sQuery.Append(" CREATE GENERATOR " + sNomeGen);
                FbCommand Command = new FbCommand(sQuery.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                Command.ExecuteNonQuery();
                sQuery = new StringBuilder();
                sQuery.Append(" SET GENERATOR " + sNomeGen + " TO " + iValorIni.ToString());
                Command = new FbCommand(sQuery.ToString(), cx.get_Conexao());
                Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally { cx.Close_Conexao(); }
        }

        public string RetornaUltimoValorGenerator(string sNomeGernerator)
        {
            try
            {
                StringBuilder sSql = new StringBuilder();
                sSql.Append("Select ");
                sSql.Append("gen_id(" + sNomeGernerator.ToUpper() + ", 0) ");
                sSql.Append("from rdb$database");
                string sValor;
                using (FbCommand cmd = new FbCommand(sSql.ToString(), cx.get_Conexao()))
                {
                    cx.Open_Conexao();
                    sValor = cmd.ExecuteScalar().ToString();
                }

                return sValor;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally { cx.Close_Conexao(); }
        }

        public string RetornaProximoValorGenerator(string sNomeGernerator)
        {
            FbConnection Conn = new FbConnection();
            try
            {
                string sNumArq = "";                
                FbCommand sSql = new FbCommand();
                sSql.Connection = cx.get_Conexao(); ;
                sSql.CommandText = "SP_CHAVEPRI";
                sSql.CommandType = CommandType.StoredProcedure;
                sSql.Parameters.Clear();

                sSql.Parameters.Add("@SNOMEGENERATOR", FbDbType.VarChar, 31).Value = sNomeGernerator;
                cx.Open_Conexao();
                sNumArq = sSql.ExecuteScalar().ToString();
                return sNumArq;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally { cx.Close_Conexao(); }
        }
    }
}
