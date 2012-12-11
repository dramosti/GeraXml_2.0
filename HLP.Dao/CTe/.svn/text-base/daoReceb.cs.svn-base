using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HLP.bel.CTe;
using FirebirdSql.Data.FirebirdClient;
using HLP.bel.Static;
using HLP.bel;

namespace HLP.Dao.CTe
{
    public class daoReceb
    {
        public void PopulaReceb(belinfCte objbelinfCte, FbConnection conn, string sCte)
        {
            try
            {
                StringBuilder sQuery = new StringBuilder();


                sQuery.Append("Select ");
                sQuery.Append("coalesce(conhecim.cd_consignat,'')cd_consignat ");
                sQuery.Append("from conhecim ");
                sQuery.Append("join empresa  on  conhecim.cd_empresa = empresa.cd_empresa ");
                sQuery.Append("where conhecim.nr_lanc ='" + sCte + "'");
                sQuery.Append("and empresa.cd_empresa='" + belStatic.CodEmpresaCte + "'");

                FbCommand fbConn = new FbCommand(sQuery.ToString(), conn);
                fbConn.ExecuteNonQuery();
                FbDataReader dr = fbConn.ExecuteReader();
                dr.Read();

                string sCodReceb = dr["cd_consignat"].ToString();
                dr.Close();

                if (sCodReceb != "")
                {
                    sQuery = new StringBuilder();

                    sQuery.Append("Select ");
                    sQuery.Append("coalesce(remetent.cd_cgc,'')CNPJ, ");
                    sQuery.Append("coalesce(remetent.cd_cpf,'')CPF, ");
                    sQuery.Append("coalesce(remetent.cd_insest,'')IE, ");
                    sQuery.Append("coalesce(remetent.nm_guerra,'')xNome, ");
                    sQuery.Append("coalesce(remetent.cd_fone,'')fone, ");
                    sQuery.Append("coalesce(remetent.ds_ende,'')xLgr, ");
                    sQuery.Append("coalesce(remetent.nr_end,'')nro, ");
                    sQuery.Append("coalesce(remetent.ds_bairro,'')xBairro, ");
                    sQuery.Append("coalesce (cidades.cd_municipio,'')cMun, ");
                    sQuery.Append("coalesce(remetent.nm_cida,'')xMun, ");
                    sQuery.Append("coalesce(remetent.cd_cep,'')CEP, ");
                    sQuery.Append("coalesce(remetent.cd_uf,'')UF, ");
                    sQuery.Append("coalesce(remetent.cd_pais,'')cPais ");
                    sQuery.Append("from remetent ");
                    sQuery.Append("left join cidades on remetent.nm_cida = cidades.nm_cidnor  and cidades.cd_ufnor = remetent.cd_uf  ");
                    sQuery.Append("where remetent.cd_remetent ='" + sCodReceb + "'");

                    fbConn = new FbCommand(sQuery.ToString(), conn);
                    fbConn.ExecuteNonQuery();
                    dr = fbConn.ExecuteReader();


                    objbelinfCte.receb = new belreceb();
                    objbelinfCte.receb.enderReceb = new belenderReceb();

                    while (dr.Read())
                    {
                        objbelinfCte.ide.tpServ = 2;

                        objbelinfCte.receb.CNPJ = util.TiraSimbolo(dr["CNPJ"].ToString());
                        objbelinfCte.receb.CPF = util.TiraSimbolo(dr["CPF"].ToString());
                        objbelinfCte.receb.IE = util.TiraSimbolo(dr["IE"].ToString());
                        objbelinfCte.receb.xNome = belUtil.TiraSimbolo(dr["xNome"].ToString(), "");
                        objbelinfCte.receb.fone = util.TiraSimbolo(dr["fone"].ToString());

                        objbelinfCte.receb.enderReceb.xLgr = belUtil.TiraSimbolo(dr["xLgr"].ToString(), "");
                        objbelinfCte.receb.enderReceb.nro = dr["nro"].ToString();
                        objbelinfCte.receb.enderReceb.xBairro = belUtil.TiraSimbolo(dr["xBairro"].ToString(), "");
                        objbelinfCte.receb.enderReceb.xMun = belUtil.TiraSimbolo(dr["xMun"].ToString(), "");
                        objbelinfCte.receb.enderReceb.CEP = util.TiraSimbolo(dr["CEP"].ToString());
                        objbelinfCte.receb.enderReceb.UF = dr["UF"].ToString();
                        objbelinfCte.receb.enderReceb.cMun = dr["cMun"].ToString();
                        objbelinfCte.receb.enderReceb.xPais = "Brasil";
                        objbelinfCte.receb.enderReceb.cPais = "1058";
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
