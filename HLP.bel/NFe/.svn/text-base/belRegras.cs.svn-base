using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HLP.bel.Static;
using HLP.bel.CTe;
using FirebirdSql.Data.FirebirdClient;

namespace HLP.bel.NFe
{
    public class belRegras
    {
        belConnection cx = new belConnection();

        public void AlteraDuplicatasNfe(List<belNumeroNF> objNumeroNfs, ref string sSqlAtualizaNF, int i)
        {
            try
            {
                if ((belStatic.bModoSCAN) && (belStatic.iStatusAtualSistema == 3) && (belStatic.bNotaServico))
                {
                    sSqlAtualizaNF = "update NF set cd_notafis = '" + objNumeroNfs[i].Cdnotafis +
                                     "', st_contingencia = '" + "N" +
                                     "', cd_serie = '" + belStatic.iSerieSCAN.ToString() +
                                     "' where cd_empresa = '" + belStatic.codEmpresaNFe +
                                     "' and cd_nfseq = '" + objNumeroNfs[i].Nfseq + "'";
                }
                else if ((belStatic.bModoContingencia) && (belStatic.iStatusAtualSistema == 2) && (belStatic.bNotaServico))
                {
                    sSqlAtualizaNF = "update NF set cd_notafis = '" + objNumeroNfs[i].Cdnotafis +
                                     "', st_contingencia = '" + "S" +
                                     "' where cd_empresa = '" + belStatic.codEmpresaNFe +
                                     "' and cd_nfseq = '" + objNumeroNfs[i].Nfseq + "'";
                }
                else
                {

                    sSqlAtualizaNF = "update NF set cd_notafis = '" + objNumeroNfs[i].Cdnotafis +
                                     "', st_contingencia = '" + "N" +
                                     "' where cd_empresa = '" + belStatic.codEmpresaNFe +
                                     "' and cd_nfseq = '" + objNumeroNfs[i].Nfseq + "'";
                }

                FbCommand cmdUpdate = new FbCommand(sSqlAtualizaNF, cx.get_Conexao());
                cx.Open_Conexao();
                cmdUpdate.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw;
            }
            finally { cx.Close_Conexao(); }


        }

        public string OperacoesValidas(string sEmp, string scdnfseq)
        {
            try
            {
                StringBuilder sSql = new StringBuilder();
                string sOperacoesValidas = string.Empty;
                string sTipo131 = string.Empty;
                sSql.Append("select ");
                sSql.Append("distinct ");
                sSql.Append("movitem.cd_oper, ");
                sSql.Append("nf.cd_tipodoc ");
                sSql.Append("from nf ");
                sSql.Append("inner join movitem on (movitem.cd_empresa = nf.cd_empresa) and ");
                sSql.Append("(movitem.cd_nfseq = nf.cd_nfseq) ");
                sSql.Append("where ");
                sSql.Append("((nf.cd_empresa = '");
                sSql.Append(sEmp);
                sSql.Append("')");
                sSql.Append(" and ");
                sSql.Append("(nf.cd_nfseq = '");
                sSql.Append(scdnfseq);
                sSql.Append("'))");

                FbCommand cmdOperacoesValidas = new FbCommand(sSql.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                cmdOperacoesValidas.ExecuteNonQuery();
                FbDataReader drOperacoesValidas = cmdOperacoesValidas.ExecuteReader();
                while (drOperacoesValidas.Read())
                {
                    sOperacoesValidas += "," + drOperacoesValidas["cd_oper"].ToString().Trim();
                    if (drOperacoesValidas["cd_tipodoc"].ToString() == "131")
                    {
                        sTipo131 = "TIPO131";
                    }
                }
                sOperacoesValidas = sOperacoesValidas.Replace(",", "','");
                if (sTipo131 != "")
                {
                    sOperacoesValidas += "TIPO131";
                }
                return sOperacoesValidas;
            }
            catch (Exception)
            {
                throw;
            }
            finally { cx.Close_Conexao(); }
        }

        public string MontaFiltroOperacoesValidas(string sOperacoesValidas)
        {
            try
            {
                string sWhere = string.Empty;
                StringBuilder sSql = new StringBuilder();
                sSql = new StringBuilder();
                sSql.Append("select ");
                sSql.Append("opereve.cd_oper ");
                sSql.Append("from opereve ");
                sSql.Append("where ");
                sSql.Append("(opereve.cd_oper ");
                sSql.Append("in ('");
                sSql.Append(sOperacoesValidas);
                sSql.Append("') ");
                sSql.Append("and ");
                sSql.Append("((ST_ESTTERC = 'S') ");
                sSql.Append("and ");
                sSql.Append("(ST_OPER='0')))");
                FbCommand cmdOperacoesValidas = new FbCommand(sSql.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                cmdOperacoesValidas.ExecuteNonQuery();
                FbDataReader drOperacoesValidas = cmdOperacoesValidas.ExecuteReader();
                string sFiltro = string.Empty;
                while (drOperacoesValidas.Read())
                {
                    sFiltro += "," + drOperacoesValidas["cd_oper"].ToString();
                }
                if (sFiltro.Trim() != "")
                {
                    sFiltro = sFiltro.Substring(1, (sFiltro.Length - 1));
                    sFiltro = sFiltro.Replace(",", "','");
                    sWhere = " And (NOT(CD_OPER IN('" + sFiltro + "')))";
                }
                return sWhere;
            }
            catch (Exception)
            {
                throw;
            }
            finally { cx.Close_Conexao(); }

        }

        public string retGrupoFaturamento(string sNota)
        {
            string sSql = "select coalesce(tpdoc.cd_gruponf,'')cd_gruponf from nf inner join tpdoc on (tpdoc.cd_tipodoc = nf.cd_tipodoc) " +
                 "where nf.cd_empresa = '" + belStatic.codEmpresaNFe + "' and nf.cd_nfseq = '" + sNota + "'";
            string GrupFatur = "";
            try
            {
                using (FbCommand GpFat = new FbCommand(sSql, cx.get_Conexao()))
                {
                    cx.Open_Conexao();
                    GrupFatur = GpFat.ExecuteScalar().ToString();
                }
                return GrupFatur;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally { cx.Close_Conexao(); }


        }

        public string VerificaNumeracaoExistente(List<belNumeroNF> objNumeroNfs)
        {
            string sNumNotasInvalidas = "";
            for (int i = 0; i < objNumeroNfs.Count; i++)
            {
                string sQueryValida = "Select count(nf.cd_notafis) from nf where nf.cd_empresa = '" + belStatic.codEmpresaNFe +
                                    "' and  nf.cd_notafis = '" + objNumeroNfs[i].Cdnotafis.ToString() +
                                    "' and nf.cd_gruponf = " +
                                    " (select nf.cd_gruponf from nf where nf.cd_empresa = '" + belStatic.codEmpresaNFe +
                                    "' and  nf.cd_nfseq = '" + objNumeroNfs[i].Nfseq.ToString() + "')";
                FbCommand command = new FbCommand(sQueryValida, cx.get_Conexao());
                cx.Open_Conexao();

                if (Convert.ToInt16(command.ExecuteScalar()) > 0)
                {
                    sNumNotasInvalidas += objNumeroNfs[i].Cdnotafis + Environment.NewLine;
                }
            }
            return sNumNotasInvalidas;
        }

        public void AlteraEmpresaParaHomologacao()
        {
            try
            {

                StringBuilder sSql = new StringBuilder();
                sSql.Append("update empresa set st_ambiente = '2' ");// o.s.23984 - 07/01/2010
                sSql.Append("Where empresa.cd_empresa = '");
                sSql.Append(belStatic.codEmpresaNFe);
                sSql.Append("'");
                using (FbCommand cmdUpdate = new FbCommand(sSql.ToString(), cx.get_Conexao()))
                {
                    cx.Open_Conexao();
                    cmdUpdate.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally { cx.Close_Conexao(); }
        }

    }
}
