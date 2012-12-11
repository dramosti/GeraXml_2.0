using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using HLP.bel.Static;
using HLP.bel.NFe.GeraXml;
using HLP.bel.CTe;

namespace HLP.bel.NFe
{
    public static class belConfigInicial
    {
        public static void CarregaConfiguracoesIniciais()
        {
            try
            {
                string sRetorno = string.Empty;
                Globais g = new Globais();
                string sEmpresa = g.LeRegConfig("Empresa");

                belConnection cx = new belConnection();
                using (FbCommand cmd = new FbCommand("select control.cd_conteud from control where control.cd_nivel = '0016'", cx.get_Conexao()))
                {
                    cx.Open_Conexao();
                    belStatic.sNomeEmpresa = Convert.ToString(cmd.ExecuteScalar()).Trim();
                    cx.Close_Conexao();
                }

                belStatic.psNM_Banco = g.LeRegConfig("BancoDados");

                belStatic.sTipoIndustrializacao = g.LeRegConfig("Industrializacao");

                string[] sRamo = belStatic.psNM_Banco.Split('\\');
                if (sRamo[sRamo.Count() - 1].ToUpper().Contains("TRANSPOR"))
                {
                    belStatic.RAMO = "TRANSPORTE";
                }
                else if (sRamo[sRamo.Count() - 1].ToUpper().Contains("INDUSTRI"))
                {
                    belStatic.RAMO = "INDUSTRIA";
                }
                else if (sRamo[sRamo.Count() - 1].ToUpper().Contains("COMERCIO"))
                {
                    belStatic.RAMO = "COMERCIO";
                }
                else if (sRamo[sRamo.Count() - 1].ToUpper().Contains("CERAMICA"))
                {
                    belStatic.RAMO = "CERAMICA";
                }

                StringBuilder sSql = new StringBuilder();
                if (belStatic.RAMO != "TRANSPORTE" && belStatic.RAMO != "")
                {
                    sSql.Append("Select ");
                    sSql.Append("empresa.nm_empresa, ");
                    sSql.Append("coalesce(empresa.st_ambiente_nfse,'2')st_ambiente_nfse, ");
                    sSql.Append("UF.nr_ufnfe, ");
                    sSql.Append("empresa.cd_cgc, ");
                    sSql.Append("coalesce(empresa.st_ambiente, '2') tpAmb ");
                    sSql.Append("from empresa ");
                    sSql.Append("left join uf on (uf.cd_uf = empresa.cd_ufnor) ");
                    sSql.Append("where ");
                    sSql.Append("cd_empresa ='");
                    sSql.Append(sEmpresa);
                    sSql.Append("'");
                }
                else
                {
                    sSql.Append("Select ");
                    sSql.Append("empresa.nm_empresa, ");
                    sSql.Append("empresa.cd_cgc, ");
                    sSql.Append("empresa.cd_ufnor, ");
                    sSql.Append("coalesce(empresa.st_ambiente, '2') tpAmb ");
                    sSql.Append("from empresa ");
                    sSql.Append("where ");
                    sSql.Append("cd_empresa ='");
                    sSql.Append(sEmpresa);
                    sSql.Append("'");
                }

                using (FbCommand cmd = new FbCommand(sSql.ToString(), cx.get_Conexao()))
                {
                    cx.Open_Conexao();
                    cmd.ExecuteNonQuery();
                    FbDataReader dr = cmd.ExecuteReader();
                    dr.Read();
                    cx.Close_Conexao();
                    belStatic.codEmpresaNFe = sEmpresa;
                    belStatic.tpAmb = Convert.ToInt16(dr["tpAmb"].ToString());
                    belStatic.sNomeEmpresaCompleto = dr["nm_empresa"].ToString();
                    belStatic.CNPJ_Empresa = dr["cd_cgc"].ToString();
                    if (belStatic.RAMO != "TRANSPORTE" && belStatic.RAMO != "")
                    {
                        belStatic.cUF = Convert.ToInt32(dr["nr_ufnfe"].ToString());
                        belStatic.tpAmbNFse = Convert.ToInt16(dr["st_ambiente_nfse"].ToString());
                    }
                    else
                    {
                        belUF objUf = new belUF();
                        objUf.SiglaUF = dr["cd_ufnor"].ToString();
                        belStatic.cUF = Convert.ToInt32(objUf.CUF);
                        belStatic.CodEmpresaCte = sEmpresa;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
           
        }
    }
}
