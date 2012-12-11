using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HLP.bel.CTe;
using FirebirdSql.Data.FirebirdClient;
using HLP.bel.Static;

namespace HLP.Dao.CTe
{
    public class daoNf
    {
        public void PopulaNf(belinfCte objbelinfCte, FbConnection conn, string sCte)
        {
            try
            {
                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("Select ");
                sQuery.Append("coalesce(nfconhec.st_tiponf,'')Tipo, ");
                sQuery.Append("coalesce(nfconhec.cd_mod,'01')mod, ");
                sQuery.Append("coalesce(nfconhec.cd_chave,'')chave, ");
                sQuery.Append("coalesce(nfconhec.desc_outros,'')descOutros, ");
                sQuery.Append("coalesce(nfconhec.cd_serie,'')serie, ");
                sQuery.Append("coalesce(nfconhec.cd_nf,'')nDoc, ");
                sQuery.Append("coalesce(nfconhec.dt_emi,'')dEmi, ");
                sQuery.Append("coalesce(nfconhec.vl_basecalc,'')vBC, ");
                sQuery.Append("coalesce(nfconhec.vl_totalicms,'')vICMS, ");
                sQuery.Append("coalesce(nfconhec.vl_basecalcst,'')vBCST, ");
                sQuery.Append("coalesce(nfconhec.vl_totalicmsst,'')vST, ");
                sQuery.Append("coalesce(nfconhec.vl_nf,'')vProd, ");
                sQuery.Append("coalesce(nfconhec.cd_cfop,'')nCFOP ");
                sQuery.Append("from nfconhec ");
                sQuery.Append("join empresa on nfconhec.cd_empresa = empresa.cd_empresa ");
                sQuery.Append("where nfconhec.nr_lancconhecim ='" + sCte + "'");
                sQuery.Append("and empresa.cd_empresa ='" + belStatic.CodEmpresaCte + "'");




                FbCommand fbConn = new FbCommand(sQuery.ToString(), conn);
                fbConn.ExecuteNonQuery();
                FbDataReader dr = fbConn.ExecuteReader();


                objbelinfCte.rem.infNF = new List<belinfNF>();
                objbelinfCte.rem.infNFe = new List<belinfNFe>();
                objbelinfCte.rem.infOutros = new List<belinfOutros>();
                while (dr.Read())
                {
                    switch (dr["Tipo"].ToString())
                    {
                        case "N":
                            belinfNF infNf = new belinfNF();
                            infNf.mod = dr["mod"].ToString();
                            infNf.serie = dr["serie"].ToString();
                            infNf.nDoc = dr["nDoc"].ToString();
                            infNf.dEmi = dr["dEmi"].ToString() != "" ? (Convert.ToDateTime(dr["dEmi"].ToString())).ToString("dd/MM/yyyy") : "";
                            infNf.vBC = dr["vBC"].ToString() != "" ? dr["vBC"].ToString().Replace(",", ".") : "0.00";
                            infNf.vICMS = dr["vICMS"].ToString() != "" ? dr["vICMS"].ToString().Replace(",", ".") : "0.00";
                            infNf.vBCST = dr["vBCST"].ToString() != "" ? dr["vBCST"].ToString().Replace(",", ".") : "0.00";
                            infNf.vST = dr["vST"].ToString() != "" ? dr["vST"].ToString().Replace(",", ".") : "0.00";
                            infNf.vProd = dr["vProd"].ToString() != "" ? dr["vProd"].ToString().Replace(",", ".") : "0.00";
                            infNf.vNF = dr["vProd"].ToString() != "" ? dr["vProd"].ToString().Replace(",", ".") : "0.00";
                            infNf.nCFOP = dr["nCFOP"].ToString() != "" ? Convert.ToInt32(dr["nCFOP"]).ToString() : "0";

                            objbelinfCte.rem.infNF.Add(infNf);
                            break;

                        case "E":
                            belinfNFe infNfe = new belinfNFe();
                            infNfe.chave = dr["chave"].ToString();
                            infNfe.nDoc = dr["nDoc"].ToString();
                            objbelinfCte.rem.infNFe.Add(infNfe);
                            break;

                        case "00":
                            belinfOutros infDeclaracao = new belinfOutros();
                            infDeclaracao.tpDoc = "00";
                            infDeclaracao.nDoc = dr["nDoc"].ToString();
                            infDeclaracao.dEmi = dr["dEmi"].ToString() != "" ? (Convert.ToDateTime(dr["dEmi"].ToString())).ToString("dd/MM/yyyy") : "";
                            infDeclaracao.vDocFisc = dr["vProd"].ToString() != "" ? dr["vProd"].ToString().Replace(",", ".") : "0.00";

                            objbelinfCte.rem.infOutros.Add(infDeclaracao);
                            break;

                        case "10":
                            belinfOutros infDutoviario = new belinfOutros();
                            infDutoviario.tpDoc = "00";
                            infDutoviario.nDoc = dr["nDoc"].ToString();
                            infDutoviario.dEmi = dr["dEmi"].ToString() != "" ? (Convert.ToDateTime(dr["dEmi"].ToString())).ToString("dd/MM/yyyy") : "";
                            infDutoviario.vDocFisc = dr["vProd"].ToString() != "" ? dr["vProd"].ToString().Replace(",", ".") : "0.00";

                            objbelinfCte.rem.infOutros.Add(infDutoviario);
                            break;

                        case "99":
                            belinfOutros infOutros = new belinfOutros();
                            infOutros.tpDoc = "99";
                            infOutros.descOutros = dr["descOutros"].ToString();
                            infOutros.nDoc = dr["nDoc"].ToString();
                            infOutros.dEmi = dr["dEmi"].ToString() != "" ? (Convert.ToDateTime(dr["dEmi"].ToString())).ToString("dd/MM/yyyy") : "";
                            infOutros.vDocFisc = dr["vProd"].ToString() != "" ? dr["vProd"].ToString().Replace(",", ".") : "0.00";

                            objbelinfCte.rem.infOutros.Add(infOutros);
                            break;

                        default:
                            throw new Exception("A nota " + dr["nDoc"].ToString() + " do Conhecimento " + objbelinfCte.ide.nCT + " não tem Tipo selecionado(NF, NF-e, Declaração, Dutoviário, Outros)");

                    }


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
}
