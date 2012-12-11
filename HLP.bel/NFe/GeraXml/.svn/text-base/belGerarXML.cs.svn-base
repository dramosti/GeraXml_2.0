using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using System.Data;
using HLP.bel.Static;

namespace HLP.bel.NFe.GeraXml
{
    public class belGerarXML 
    {
        public belConnection cx = new belConnection();
        public string psNM_Banco { get; set; }
        private FbDataAdapter Da = new FbDataAdapter();
        private DataSet ds = new DataSet();
        public Boolean bIndustria = true;
        public string sTabela = String.Empty;
        public string sCampos = String.Empty;
        public string sWhere = String.Empty;
        public string sOrder = String.Empty;
        public StringBuilder sInner = new StringBuilder();
        string sSQL = String.Empty;
        FbCommand SelCmd = new FbCommand();

        public belGerarXML()
        {  
        }

    
   

        public DataTable BuscaDadosNF()
        {
            DataTable dt = new DataTable();
            try
            {
                sSQL = "SELECT " + this.sCampos + " FROM " + this.sTabela;
                if (!(this.sInner.Equals(String.Empty)))
                    sSQL += this.sInner.ToString();

                if (!(this.sWhere.Equals(String.Empty)))
                    sSQL += " WHERE " + this.sWhere + " ";

                if (!(this.sOrder.Equals(String.Empty)))
                    sSQL += " ORDER BY " + this.sOrder;
                FbDataAdapter Da = new FbDataAdapter(sSQL, cx.get_Conexao());
                cx.Open_Conexao();
                dt.Clear();
                Da.Fill(dt);
                Da.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally { cx.Close_Conexao(); }
            return dt;
        }
        public string RetornaGenString(string sGen, int Tamanho)
        {
            string sNumArq = "";
            try
            {
                FbCommand sSql = new FbCommand();
                sSql.Connection = cx.get_Conexao();
                cx.Open_Conexao();
                sSql.CommandText = "SP_CHAVEPRI";
                sSql.CommandType = CommandType.StoredProcedure;
                sSql.Parameters.Clear();

                sSql.Parameters.Add("@SNOMEGENERATOR", FbDbType.VarChar, 31).Value = "GEN_NOMEARQXML";

                sNumArq = sSql.ExecuteScalar().ToString();

            }
            catch (FbException Ex)
            {               
                Console.WriteLine("Erro.: ", Ex.Message);
            }
            finally
            {
                cx.Close_Conexao();
            }
            return sNumArq.PadLeft(Tamanho, '0');
        }
        public void SetCamposSelect(StringBuilder Lista)
        {
            this.sCampos = Lista.ToString().Trim();
        }
        public void SetWhereSelect(StringBuilder Where)
        {
            this.sWhere = Where.ToString().Trim();
        }
        public void SetOrderSelect(string Order)
        {
            this.sOrder = Order.Trim();
        }
        public void SetTabelaSelect(string Tabela)
        {
            this.sTabela = Tabela.Trim();
        }
        public void SetInnerSelec(StringBuilder Inner)
        {
            this.sInner = new StringBuilder();
            this.sInner = Inner;
        }     
        public DataSet GetData()
        {
            ds = new DataSet();
            Da.Fill(ds);
            return ds;
        }
    }
}
