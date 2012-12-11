using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using HLP.bel;
using HLP.bel.Static;
using HLP.bel.CTe;

namespace HLP.Dao.CCe
{
    public class daoPesquisaCCe
    {

        StringBuilder sQuery = new StringBuilder();
        public enum Campo { cd_notafis, cd_nfseq, cd_conheci, nr_lanc };
        public List<bel.CCe.belPesquisaCCe> objLPesquisa = new List<HLP.bel.CCe.belPesquisaCCe>();
        belConnection cx = new belConnection();

        private void AddCamposAquery()
        {
            sQuery = new StringBuilder();
            sQuery.Append("select ");
            sQuery.Append("cartacor.nr_lanc , ");
            sQuery.Append("cartacor.cd_clifor, ");
            sQuery.Append("clifor.nm_clifor, ");
            sQuery.Append("cartacor.cd_notafis, ");
            sQuery.Append("cartacor.cd_nfseq, ");
            sQuery.Append("cartacor.dt_emi, ");
            sQuery.Append("cartacor.dt_lanc, ");
            sQuery.Append("coalesce(cartacor.QT_ENVIO,0)QT_ENVIO, ");
            sQuery.Append("(case when coalesce(nf.cd_chavenferet,'') = '' then coalesce(nf.cd_chavenfe,'') else coalesce(nf.cd_chavenferet,'') end)CHNFE ");
            sQuery.Append("from cartacor inner join clifor on cartacor.cd_clifor = clifor.cd_clifor ");
            sQuery.Append("              inner join nf on nf.cd_nfseq = cartacor.cd_nfseq and nf.cd_notafis = cartacor.cd_notafis and nf.cd_empresa = cartacor.cd_empresa ");
        }

        private void AddCamposAqueryCte()
        {
            sQuery = new StringBuilder();
            sQuery.Append("select ");
            sQuery.Append("cartacor.nr_lanc , ");
            sQuery.Append("cartacor.cd_clifor, ");
            sQuery.Append("clifor.nm_clifor, ");
            sQuery.Append("cartacor.cd_conhecim cd_notafis, ");
            sQuery.Append("conhecim.nr_lanc cd_nfseq, ");
            sQuery.Append("cartacor.cd_nfseq, ");
            sQuery.Append("cartacor.dt_emi, ");
            sQuery.Append("cartacor.dt_lanc, ");
            sQuery.Append("coalesce(cartacor.QT_ENVIO,0)QT_ENVIO, ");
            sQuery.Append("conhecim.cd_chavecte CHNFE ");
            sQuery.Append("from cartacor left join clifor on cartacor.cd_clifor = clifor.cd_clifor ");
            sQuery.Append("              left join conhecim on  cartacor.cd_conhecim  = conhecim.cd_conheci ");
            sQuery.Append("              and conhecim.cd_empresa = cartacor.cd_empresa ");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtInicial"></param>
        /// <param name="dtFinal"></param>
        /// <param name="status">Enviado, Pendente</param>
        /// <returns></returns>
        public List<bel.CCe.belPesquisaCCe> BuscaCCe(DateTime dtInicial, DateTime dtFinal, string status)
        {
            try
            {

                if (belStatic.RAMO != "TRANSPORTE")
                {
                    AddCamposAquery();
                    sQuery.Append(string.Format("where coalesce(nf.cd_recibocanc,'') = '' and cartacor.dt_emi between '{0}' and '{1}' and cartacor.cd_empresa = '{2}' ", dtInicial.ToString("dd.MM.yyyy"), dtFinal.ToString("dd.MM.yyyy"), belStatic.codEmpresaNFe));
                }
                else
                {
                    AddCamposAqueryCte();
                    sQuery.Append(string.Format("where coalesce(conhecim.cd_recibocanc,'') = '' and cartacor.dt_emi between '{0}' and '{1}' and cartacor.cd_empresa = '{2}' ", dtInicial.ToString("dd.MM.yyyy"), dtFinal.ToString("dd.MM.yyyy"), belStatic.codEmpresaNFe));
                }

                AddWhereStatus(status);
                ExecuteQuery();

                return objLPesquisa;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<bel.CCe.belPesquisaCCe> BuscaCCe(string sVl_inicial, string sVl_final, Campo campo, string status)
        {
            try
            {
                if (belStatic.RAMO != "TRANSPORTE")
                {
                    AddCamposAquery();
                    sQuery.Append(string.Format("where coalesce(nf.cd_recibocanc,'') = '' and cartacor." + campo.ToString() + " between '{0}' and '{1}'  and cartacor.cd_empresa = '{2}'", sVl_inicial, sVl_final, belStatic.codEmpresaNFe));
                }
                else
                {
                    AddCamposAqueryCte();
                    sQuery.Append(string.Format("where coalesce(conhecim.cd_recibocanc,'') = '' and conhecim." + campo.ToString() + " between '{0}' and '{1}'  and cartacor.cd_empresa = '{2}'", sVl_inicial, sVl_final, belStatic.codEmpresaNFe));
                }

                AddWhereStatus(status);
                ExecuteQuery();


                return objLPesquisa;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ExecuteQuery()
        {
            try
            {
                sQuery.Append(" order by cartacor.nr_lanc ");
                objLPesquisa = new List<HLP.bel.CCe.belPesquisaCCe>();
                try
                {
                    FbCommand cmd = new FbCommand(sQuery.ToString(), cx.get_Conexao());
                    cx.Open_Conexao();
                    FbDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        objLPesquisa.Add(new HLP.bel.CCe.belPesquisaCCe
                        {
                            CD_CLIFOR = dr["cd_clifor"].ToString(),
                            CD_NFSEQ = dr["cd_nfseq"].ToString(),
                            CD_NOTAFIS = dr["cd_notafis"].ToString(),
                            CNPJ = belStatic.CNPJ_Empresa,
                            CPF = "",
                            CHNFE = dr["CHNFE"].ToString(),
                            DT_EMI = Convert.ToDateTime(dr["dt_emi"].ToString()),
                            DT_LANC = Convert.ToDateTime(dr["dt_lanc"].ToString()),
                            NM_CLIFOR = dr["nm_clifor"].ToString(),
                            CD_NRLANC = dr["nr_lanc"].ToString(),
                            QT_ENVIO = Convert.ToInt32(dr["QT_ENVIO"].ToString())
                        });
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddWhereStatus(string status)
        {
            if (status.Equals("Enviado"))
            {
                sQuery.Append("and coalesce(cartacor.qt_envio,0) <> 0 ");
            }
            else if (status.Equals("Pendente"))
            {
                sQuery.Append("and  coalesce(cartacor.qt_envio,0) = 0 ");
            }
        }
    }
}
