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
    public class daoExped
    {
        public void PopulaExped(belinfCte objbelinfCte, FbConnection conn, string sCte)
        {
            try
            {
                StringBuilder sQuery = new StringBuilder();


                sQuery.Append("Select ");
                sQuery.Append("coalesce(conhecim.cd_redes,'')cd_redes ");
                sQuery.Append("from conhecim ");
                sQuery.Append("join empresa  on  conhecim.cd_empresa = empresa.cd_empresa ");
                sQuery.Append("where conhecim.nr_lanc ='" + sCte + "'");
                sQuery.Append("and empresa.cd_empresa='" + belStatic.CodEmpresaCte + "'");

                FbCommand fbConn = new FbCommand(sQuery.ToString(), conn);
                fbConn.ExecuteNonQuery();
                FbDataReader dr = fbConn.ExecuteReader();
                dr.Read();

                string sCodRedes = dr["cd_redes"].ToString();
                dr.Close();

                if (sCodRedes != "")
                {
                    sQuery = new StringBuilder();

                    sQuery.Append("Select ");
                    sQuery.Append("coalesce(remetent.cd_cgc,'')CNPJ, ");
                    sQuery.Append("coalesce(remetent.cd_cpf,'')CPF, ");
                    sQuery.Append("coalesce(remetent.cd_insest,'')IE, ");
                    sQuery.Append("coalesce(remetent.nm_social,'')xNome, ");
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
                    sQuery.Append("where remetent.cd_remetent ='" + sCodRedes + "'");

                    fbConn = new FbCommand(sQuery.ToString(), conn);
                    fbConn.ExecuteNonQuery();
                    dr = fbConn.ExecuteReader();


                    objbelinfCte.exped = new belexped();
                    objbelinfCte.exped.enderExped = new belenderExped();

                    while (dr.Read())
                    {
                        objbelinfCte.ide.tpServ = 2;

                        objbelinfCte.exped.CNPJ = util.TiraSimbolo(dr["CNPJ"].ToString());
                        objbelinfCte.exped.CPF = util.TiraSimbolo(dr["CPF"].ToString());
                        objbelinfCte.exped.IE = util.TiraSimbolo(dr["IE"].ToString());
                        objbelinfCte.exped.xNome = belUtil.TiraSimbolo(dr["xNome"].ToString(), "");
                        objbelinfCte.exped.fone = util.TiraSimbolo(dr["fone"].ToString());
                        objbelinfCte.exped.enderExped.xLgr = belUtil.TiraSimbolo(dr["xLgr"].ToString(), "");
                        objbelinfCte.exped.enderExped.nro = dr["nro"].ToString();
                        objbelinfCte.exped.enderExped.xBairro = belUtil.TiraSimbolo(dr["xBairro"].ToString(), "");
                        objbelinfCte.exped.enderExped.xMun = belUtil.TiraSimbolo(dr["xMun"].ToString(), "");
                        objbelinfCte.exped.enderExped.CEP = util.TiraSimbolo(dr["CEP"].ToString());
                        objbelinfCte.exped.enderExped.UF = dr["UF"].ToString();
                        objbelinfCte.exped.enderExped.cMun = dr["cMun"].ToString();
                        objbelinfCte.exped.enderExped.xPais = "Brasil";
                        objbelinfCte.exped.enderExped.cPais = "1058";
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
