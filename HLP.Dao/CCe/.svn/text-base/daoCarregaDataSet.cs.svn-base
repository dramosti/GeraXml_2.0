using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DANFE;
using HLP.bel.CCe;
using HLP.bel;
using FirebirdSql.Data.FirebirdClient;
using HLP.bel.Static;
using HLP.bel.CTe;

namespace HLP.Dao.CCe
{
    public class daoCarregaDataSet
    {
        public dsCCe objDS = new dsCCe();
        public List<dsCCe> objListaDS = new List<dsCCe>();

        private List<belPesquisaCCe> objListCCe;
        belConnection cx = new belConnection();

        public daoCarregaDataSet(List<belPesquisaCCe> _objListCCe)
        {
            this.objListCCe = _objListCCe;
            CarregaInformacoes();
        }

        private void CarregaInformacoes()
        {
            dsCCe.CCeRow drCCe;
            HLP.bel.NFe.GeraXml.Globais LeRegWin = new HLP.bel.NFe.GeraXml.Globais();
            Byte[] bimagem = belUtil.carregaImagem(LeRegWin.LeRegConfig("Logotipo"));
            try
            {
                for (int i = 0; i < objListCCe.Count; i++)
                {
                    drCCe = objDS.CCe.NewCCeRow();
                    drCCe = CarregaLinha(drCCe, bimagem, i);
                    objDS.CCe.AddCCeRow(drCCe);

                    dsCCe objDSlista = new dsCCe();
                    objDSlista.CCe.AddCCeRow(CarregaLinha(objDSlista.CCe.NewCCeRow(), bimagem, 0));
                    objListaDS.Add(objDSlista);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private dsCCe.CCeRow CarregaLinha(dsCCe.CCeRow drCCe, Byte[] bimagem, int i)
        {
            drCCe.ID = i;
            drCCe.DADOS_EMPRESA = BuscaDadosEmpresa();
            drCCe.DADOS_CLIENTE = BuscaDadosCliente(objListCCe[i].CD_CLIFOR);
            drCCe.CHAVE = objListCCe[i].CHNFE;
            drCCe.NFE = objListCCe[i].CD_NOTAFIS;
            drCCe.DT_EMISSAO = objListCCe[i].DT_EMI.ToString("dd/MM/yyyy");
            Dao.CCe.daoGeraCCe objdaoGeraCCe = new daoGeraCCe();
            drCCe.RETIFICACAO = objdaoGeraCCe.BuscaCorrecoesPulandoLinha(objListCCe[i].CD_NRLANC);
            drCCe.LOGO = bimagem;
            Byte[] bCodBarras = belUtil.SalvaCodBarras(drCCe.CHAVE);
            drCCe.BARRAS = bCodBarras;
            return drCCe;
        }

        private string BuscaDadosEmpresa()
        {
            try
            {
                string sQuery = "select e.cd_cgc, e.ds_endnor, e.nm_bairronor,e.nm_cidnor,e.cd_ufnor from empresa e " +
                                "where e.cd_empresa = '" + belStatic.codEmpresaNFe + "'";

                FbCommand command = new FbCommand(sQuery, cx.get_Conexao());
                cx.Open_Conexao();

                FbDataReader dr = command.ExecuteReader();

                StringBuilder sDados = new StringBuilder();

                while (dr.Read())
                {
                    sDados.Append(belStatic.sNomeEmpresaCompleto + Environment.NewLine);
                    sDados.Append(dr["cd_cgc"].ToString() + Environment.NewLine);
                    sDados.Append(dr["ds_endnor"].ToString() + ", " + dr["nm_bairronor"].ToString() + Environment.NewLine + dr["nm_cidnor"].ToString() + "/" + dr["cd_ufnor"].ToString());
                }
                return sDados.ToString(); ;
            }
            catch (Exception)
            {
                throw;
            }
            finally { cx.Close_Conexao(); }
        }

        private string BuscaDadosCliente(string sCD_CLIFOR)
        {
            try
            {
                string sQuery = "select e.nm_clifor,(case when e.cd_cpf is null then e.cd_cgc else e.cd_cpf end)cd_cgc, " +
                                " coalesce(e.ds_endnor,'')ds_endnor, coalesce(e.nm_bairronor,'')nm_bairronor,coalesce(e.nm_cidnor,'')nm_cidnor,coalesce(e.cd_ufnor,'')cd_ufnor from clifor e " +
                                "where e.cd_clifor = '" + sCD_CLIFOR + "'";

                FbCommand command = new FbCommand(sQuery, cx.get_Conexao());
                cx.Open_Conexao();

                FbDataReader dr = command.ExecuteReader();

                StringBuilder sDados = new StringBuilder();

                while (dr.Read())
                {
                    sDados.Append(dr["nm_clifor"].ToString() + Environment.NewLine);
                    sDados.Append(dr["cd_cgc"].ToString() + Environment.NewLine);
                    sDados.Append(dr["ds_endnor"].ToString() + " ," + dr["nm_bairronor"].ToString() + Environment.NewLine + dr["nm_cidnor"].ToString() + "/" + dr["cd_ufnor"].ToString());
                }
                return sDados.ToString(); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { cx.Close_Conexao(); }
        }

    }
}
