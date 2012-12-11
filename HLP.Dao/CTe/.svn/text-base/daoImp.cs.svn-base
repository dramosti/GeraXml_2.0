using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HLP.bel.CTe;
using FirebirdSql.Data.FirebirdClient;
using HLP.bel.Static;

namespace HLP.Dao.CTe
{
    public class daoImp
    {
        public void PopulaImp(belinfCte objbelinfCte, FbConnection conn, string sCte)
        {


            try
            {
                StringBuilder sQuery = new StringBuilder();

                sQuery.Append("Select ");
                sQuery.Append("coalesce (conhecim.cd_sittrib,'') CST, ");
                sQuery.Append("coalesce (conhecim.vl_redbase,'') pRedBC, ");
                sQuery.Append("coalesce (conhecim.vl_base,'') vBC, ");
                sQuery.Append("coalesce (conhecim.vl_aliq,'') pICMS, ");
                sQuery.Append("coalesce (conhecim.vl_icms,'') vICMS ");
                sQuery.Append("From conhecim  ");
                sQuery.Append("join  empresa on conhecim.cd_empresa = empresa.cd_empresa ");
                sQuery.Append("Where conhecim.nr_lanc = '" + sCte + "'");
                sQuery.Append("And empresa.cd_empresa = '" + belStatic.CodEmpresaCte + "'");


                FbCommand fbConn = new FbCommand(sQuery.ToString(), conn);
                fbConn.ExecuteNonQuery();
                FbDataReader dr = fbConn.ExecuteReader();

                while (dr.Read())
                {
                    objbelinfCte.imp = new belimp();
                    objbelinfCte.imp.ICMS = new belICMS();
                    switch (dr["CST"].ToString())
                    {
                        case "000":
                            objbelinfCte.imp.ICMS.ICMS00 = new belICMS00();
                            objbelinfCte.imp.ICMS.ICMS00.CST = "00";
                            objbelinfCte.imp.ICMS.ICMS00.vBC = dr["vBC"].ToString().Replace(",", ".");
                            objbelinfCte.imp.ICMS.ICMS00.pICMS = dr["pICMS"].ToString().Replace(",", ".");
                            objbelinfCte.imp.ICMS.ICMS00.vICMS = dr["vICMS"].ToString().Replace(",", ".");
                            break;

                        case "020":
                            objbelinfCte.imp.ICMS.ICMS20 = new belICMS20();
                            objbelinfCte.imp.ICMS.ICMS20.CST = "20";
                            objbelinfCte.imp.ICMS.ICMS20.pRedBC = dr["pRedBC"].ToString().Replace(",", ".");
                            objbelinfCte.imp.ICMS.ICMS20.vBC = dr["vBC"].ToString().Replace(",", ".");
                            objbelinfCte.imp.ICMS.ICMS20.pICMS = dr["pICMS"].ToString().Replace(",", ".");
                            objbelinfCte.imp.ICMS.ICMS20.vICMS = dr["vICMS"].ToString().Replace(",", ".");
                            break;

                        case "040":
                            PopulaCst45(objbelinfCte, "40");
                            break;

                        case "041":
                            PopulaCst45(objbelinfCte, "41");
                            break;

                        case "051":
                            PopulaCst45(objbelinfCte, "51");
                            break;

                        case "060":
                            objbelinfCte.imp.ICMS.ICMS60 = new belICMS60();
                            objbelinfCte.imp.ICMS.ICMS60.CST = "60";
                            objbelinfCte.imp.ICMS.ICMS60.vBCSTRet = dr["vBC"].ToString().Replace(",", ".");
                            objbelinfCte.imp.ICMS.ICMS60.vICMSSTRet = dr["vICMS"].ToString().Replace(",", ".");
                            objbelinfCte.imp.ICMS.ICMS60.pICMSSTRet = dr["pICMS"].ToString().Replace(",", ".");

                            break;


                        case "090":
                            objbelinfCte.imp.ICMS = new belICMS();
                            objbelinfCte.imp.ICMS.ICMS90 = new belICMS90();
                            objbelinfCte.imp.ICMS.ICMS90.CST = "90";
                            objbelinfCte.imp.ICMS.ICMS90.pRedBC = dr["pRedBC"].ToString().Replace(",", ".");
                            objbelinfCte.imp.ICMS.ICMS90.vBC = dr["vBC"].ToString().Replace(",", ".");
                            objbelinfCte.imp.ICMS.ICMS90.pICMS = dr["pICMS"].ToString().Replace(",", ".");
                            objbelinfCte.imp.ICMS.ICMS90.vICMS = dr["vICMS"].ToString().Replace(",", ".");
                            break;


                        default:
                            throw new Exception("O Conhecimento " + objbelinfCte.ide.nCT + " não tem Situação Tributária Válida!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private static void PopulaCst45(belinfCte objbelinfCte, string CST)
        {
            objbelinfCte.imp.ICMS.ICMS45 = new belICMS45();
            objbelinfCte.imp.ICMS.ICMS45.CST = CST;
        }
    }
}
