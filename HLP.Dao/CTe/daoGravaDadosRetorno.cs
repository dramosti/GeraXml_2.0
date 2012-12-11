using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HLP.bel.CTe;
using FirebirdSql.Data.FirebirdClient;
using HLP.bel.Static;
using HLP.bel;
using System.Windows.Forms;

namespace HLP.Dao.CTe
{
    public class daoGravaDadosRetorno
    {
        belConnection cx = new belConnection();

        public string BuscaChave(string sCD_SEQ)
        {
            try
            {
                string sQuery = "select c.cd_chavecte from conhecim c where c.nr_lanc = '{0}' and c.cd_empresa = '{1}'";

                sQuery = string.Format(sQuery, sCD_SEQ, belStatic.CodEmpresaCte);

                FbCommand cmd = new FbCommand(sQuery, cx.get_Conexao());
                cx.Open_Conexao();

                string sRet = cmd.ExecuteScalar().ToString();

                return sRet;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao Gravar a Chave no Banco de Dados.");
            }
            finally
            {
                cx.Close_Conexao();
            }

        }

        public void GravarChave(belPopulaObjetos objObjetos)
        {
            try
            {
                for (int i = 0; i < objObjetos.objLinfCte.Count; i++)
                {
                    string sChave = objObjetos.objLinfCte[i].id.Replace("CTe", "");

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append("Update conhecim ");
                    sQuery.Append("set conhecim.cd_chavecte='" + sChave + "' ");
                    sQuery.Append("where conhecim.cd_conheci='" + objObjetos.objLinfCte[i].ide.nCT.PadLeft(6, '0') + "' "); //sicupira
                    sQuery.Append("and conhecim.cd_empresa ='" + belStatic.CodEmpresaCte + "'");

                    FbCommand fbConn = new FbCommand(sQuery.ToString(), cx.get_Conexao());
                    cx.Open_Conexao();
                    fbConn.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao Gravar a Chave no Banco de Dados.");
            }
            finally
            {
                cx.Close_Conexao();
            }

        }

        public void GravarRecibo(belPopulaObjetos objObjetos, string sRecibo)
        {
            try
            {
                for (int i = 0; i < objObjetos.objLinfCte.Count; i++)
                {

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append("Update conhecim ");
                    sQuery.Append("set conhecim.cd_recibocte='" + sRecibo + "' ");
                    sQuery.Append("where conhecim.cd_conheci='" + objObjetos.objLinfCte[i].ide.nCT + "' ");
                    sQuery.Append("and conhecim.cd_empresa ='" + belStatic.CodEmpresaCte + "'");

                    FbCommand fbConn = new FbCommand(sQuery.ToString(), cx.get_Conexao());
                    cx.Open_Conexao();
                    fbConn.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao Gravar o Recibo no Banco de Dados.");
            }
            finally
            {
                cx.Close_Conexao();
            }
        }

        public void GravarRecibo(string SeqCte, string sRecibo)
        {
            try
            {
                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("Update conhecim ");
                sQuery.Append("set conhecim.cd_recibocte='" + sRecibo + "' ");
                sQuery.Append("where conhecim.nr_lanc='" + SeqCte + "' ");
                sQuery.Append("and conhecim.cd_empresa ='" + belStatic.CodEmpresaCte + "'");

                FbCommand fbConn = new FbCommand(sQuery.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                fbConn.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao Gravar o Recibo no Banco de Dados.");
            }
            finally
            {
                cx.Close_Conexao();
            }
        }

        public void GravarReciboCancelamento(string sCodConhec, string sReciboCancelamento, string sJustificativa)
        {
            try
            {
                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("Update conhecim ");
                sQuery.Append("set conhecim.cd_recibocanc ='" + sReciboCancelamento + "' ");
                if (sJustificativa != "")
                {
                    sQuery.Append(", conhecim.ds_cancelamento='" + sJustificativa + "' ");
                }
                sQuery.Append("where conhecim.cd_conheci='" + sCodConhec + "' ");
                sQuery.Append("and conhecim.cd_empresa ='" + belStatic.CodEmpresaCte + "'");
                FbCommand fbConn = new FbCommand(sQuery.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                fbConn.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cx.Close_Conexao();
            }
        }

        public void ApagarRecibo(string sRecibo)
        {
            try
            {
                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("Update conhecim ");
                sQuery.Append("set conhecim.cd_recibocte= null ");
                sQuery.Append("where conhecim.cd_recibocte='" + sRecibo + "' ");
                sQuery.Append("and conhecim.cd_empresa ='" + belStatic.CodEmpresaCte + "'");
                FbCommand fbConn = new FbCommand(sQuery.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                fbConn.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cx.Close_Conexao();
            }

        }

        public void GravarProtocoloEnvio(belStatusCte cte)
        {
            try
            {
                if (cte.Protocolo != null)
                {
                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append("Update conhecim ");
                    sQuery.Append("set conhecim.cd_nprotcte='" + cte.Protocolo + "' ");
                    sQuery.Append("where conhecim.nr_lanc='" + cte.NumeroSeq.PadLeft(6, '0') + "' ");
                    sQuery.Append("and conhecim.cd_empresa ='" + belStatic.CodEmpresaCte + "'");

                    FbCommand fbConn = new FbCommand(sQuery.ToString(), cx.get_Conexao());
                    cx.Open_Conexao();
                    fbConn.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cx.Close_Conexao();
            }

        }

        public void AlterarStatusCte(belStatusCte status)
        {
            try
            {
                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("Update conhecim ");
                sQuery.Append("set  conhecim.st_cte ='S' ");
                sQuery.Append("where conhecim.nr_lanc ='" + status.NumeroSeq + "' ");
                sQuery.Append("and conhecim.cd_empresa ='" + belStatic.CodEmpresaCte + "'");

                FbCommand fbConn = new FbCommand(sQuery.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                fbConn.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cx.Close_Conexao();
            }
        }

        public void AlterarStatusCteContingencia(string sNumCte)
        {
            try
            {
                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("Update conhecim ");
                sQuery.Append("set  conhecim.st_contingencia ='S' ");
                sQuery.Append("where conhecim.cd_conheci ='" + sNumCte + "' ");
                sQuery.Append("and conhecim.cd_empresa ='" + belStatic.CodEmpresaCte + "'");
                FbCommand fbConn = new FbCommand(sQuery.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                fbConn.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cx.Close_Conexao();
            }
        }
    }
}
