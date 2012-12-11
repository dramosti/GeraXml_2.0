using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HLP.bel.CTe;
using FirebirdSql.Data.FirebirdClient;
using HLP.bel.Static;

namespace HLP.Dao.CTe
{
    public class daoVPrest
    {
        public void PopulaVPrest(belinfCte objbelinfCte, FbConnection conn, string sCte)
        {
            try
            {
                StringBuilder sQuery = new StringBuilder();

                sQuery.Append("Select ");
                sQuery.Append("coalesce (conhecim.vl_total,'') vTPrest, ");
                sQuery.Append("coalesce (conhecim.vl_frcubagem,'') FRETECUBAGEM, ");
                sQuery.Append("coalesce (conhecim.vl_frpeso,'') FRETEPESO, ");
                sQuery.Append("coalesce (conhecim.vl_cat,'') CAT, ");
                sQuery.Append("coalesce (conhecim.vl_desp,'') DESPACHO, ");
                sQuery.Append("coalesce (conhecim.vl_pedagio,'') PEDAGIO, ");
                sQuery.Append("coalesce (conhecim.vl_outros,'') OUTROS, ");
                sQuery.Append("coalesce (conhecim.vl_adval,'') ADME, ");
                sQuery.Append("coalesce (conhecim.vl_entcole,'') ENTREGA, ");
                sQuery.Append("coalesce (conhecim.vl_frvalor,'') FRETEVALOR ");
                sQuery.Append("From conhecim  ");
                sQuery.Append("join  empresa on conhecim.cd_empresa = empresa.cd_empresa ");
                sQuery.Append("Where conhecim.nr_lanc = '" + sCte + "'");
                sQuery.Append("And empresa.cd_empresa = '" + belStatic.CodEmpresaCte + "'");


                FbCommand fbConn = new FbCommand(sQuery.ToString(), conn);
                fbConn.ExecuteNonQuery();
                FbDataReader dr = fbConn.ExecuteReader();
                objbelinfCte.vPrest = new belvPrest();
                objbelinfCte.vPrest.Comp = new List<belComp >();

                while (dr.Read())
                {
                    objbelinfCte.vPrest.vTPrest = dr["vTPrest"].ToString();
                    objbelinfCte.vPrest.vRec = dr["vTPrest"].ToString();

                    belComp Comp = new belComp();
                    if (dr["FRETEVALOR"].ToString() != "0.00")
                    {
                        Comp = new belComp();
                        Comp.xNome = "FRETE VALOR";
                        Comp.vComp = dr["FRETEVALOR"].ToString();
                        objbelinfCte.vPrest.Comp.Add(Comp);
                    }
                    if (dr["FRETECUBAGEM"].ToString() != "0.00")
                    {
                        Comp = new belComp();
                        Comp.xNome = "FRETE CUBAGEM";
                        Comp.vComp = dr["FRETECUBAGEM"].ToString();
                        objbelinfCte.vPrest.Comp.Add(Comp);
                    }
                    if (dr["FRETEPESO"].ToString() != "0.00")
                    {
                        Comp = new belComp();
                        Comp.xNome = "FRETE PESO";
                        Comp.vComp = dr["FRETEPESO"].ToString();
                        objbelinfCte.vPrest.Comp.Add(Comp);
                    }
                    if (dr["CAT"].ToString() != "0.00")
                    {
                        Comp = new belComp();
                        Comp.xNome = "CAT";
                        Comp.vComp = dr["CAT"].ToString();
                        objbelinfCte.vPrest.Comp.Add(Comp);
                    }
                    if (dr["DESPACHO"].ToString() != "0.00")
                    {
                        Comp = new belComp();
                        Comp.xNome = "DESPACHO";
                        Comp.vComp = dr["DESPACHO"].ToString();
                        objbelinfCte.vPrest.Comp.Add(Comp);
                    }
                    if (dr["PEDAGIO"].ToString() != "0.00")
                    {
                        Comp = new belComp();
                        Comp.xNome = "PEDAGIO";
                        Comp.vComp = dr["PEDAGIO"].ToString();
                        objbelinfCte.vPrest.Comp.Add(Comp);
                    }
                    if (dr["OUTROS"].ToString() != "0.00")
                    {
                        Comp = new belComp();
                        Comp.xNome = "OUTROS";
                        Comp.vComp = dr["OUTROS"].ToString();
                        objbelinfCte.vPrest.Comp.Add(Comp);
                    }
                    if (dr["ADME"].ToString() != "0.00")
                    {
                        Comp = new belComp();
                        Comp.xNome = "ADME";
                        Comp.vComp = dr["ADME"].ToString();
                        objbelinfCte.vPrest.Comp.Add(Comp);
                    }
                    if (dr["ENTREGA"].ToString() != "0.00")
                    {
                        Comp = new belComp();
                        Comp.xNome = "ENTREGA";
                        Comp.vComp = dr["ENTREGA"].ToString();
                        objbelinfCte.vPrest.Comp.Add(Comp);
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
