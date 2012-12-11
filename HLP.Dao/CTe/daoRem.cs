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
    public class daoRem
    {
        public void PopulaRem(belinfCte objbelinfCte, FbConnection conn, string sCte)
        {
            try
            {
                StringBuilder sQuery = new StringBuilder();


                sQuery.Append("Select ");
                sQuery.Append("coalesce(remetent.cd_cgc,'')CNPJ, ");
                sQuery.Append("coalesce(remetent.cd_cpf,'')CPF, ");
                sQuery.Append("coalesce(remetent.cd_insest,'')IE, ");
                sQuery.Append("coalesce(remetent.nm_social,'')xNome, ");
                sQuery.Append("coalesce(remetent.nm_guerra,'')xFant, ");
                sQuery.Append("coalesce(remetent.cd_fone,'')fone, ");
                sQuery.Append("coalesce (cidades.cd_municipio,'')cMun, ");
                sQuery.Append("coalesce(remetent.ds_ende,'')xLgr, ");
                sQuery.Append("coalesce(remetent.nr_end,'')nro, ");
                sQuery.Append("coalesce(remetent.ds_bairro,'')xBairro, ");
                sQuery.Append("coalesce(remetent.nm_cida,'')xMun, ");
                sQuery.Append("coalesce(remetent.cd_cep,'')CEP, ");
                sQuery.Append("coalesce(remetent.cd_uf,'')UF, ");
                sQuery.Append("coalesce(remetent.cd_pais,'')cPais ");
                sQuery.Append("from remetent ");
                sQuery.Append("join conhecim on  conhecim.cd_remetent  = remetent.cd_remetent ");
                sQuery.Append("join empresa  on  conhecim.cd_empresa = empresa.cd_empresa ");
                sQuery.Append("left join cidades on remetent.nm_cida = cidades.nm_cidnor  and cidades.cd_ufnor = remetent.cd_uf  ");
                sQuery.Append("where conhecim.nr_lanc ='" + sCte + "'");
                sQuery.Append("and empresa.cd_empresa='" + belStatic.CodEmpresaCte + "'");




                FbCommand fbConn = new FbCommand(sQuery.ToString(), conn);
                fbConn.ExecuteNonQuery();
                FbDataReader dr = fbConn.ExecuteReader();
                dr.Read();

                objbelinfCte.rem = new belrem();
                objbelinfCte.rem.enderReme = new belenderReme();


                objbelinfCte.rem.CNPJ = util.TiraSimbolo(dr["CNPJ"].ToString());
                objbelinfCte.rem.CPF = util.TiraSimbolo(dr["CPF"].ToString());
                objbelinfCte.rem.IE = util.TiraSimbolo(dr["IE"].ToString());
                objbelinfCte.rem.xNome = belUtil.TiraSimbolo(dr["xNome"].ToString(), "");
                objbelinfCte.rem.xFant = belUtil.TiraSimbolo(dr["xFant"].ToString(), "");
                objbelinfCte.rem.fone = util.TiraSimbolo(dr["fone"].ToString());

                objbelinfCte.rem.enderReme.xLgr = belUtil.TiraSimbolo(dr["xLgr"].ToString(), "");
                objbelinfCte.rem.enderReme.nro = dr["nro"].ToString();
                objbelinfCte.rem.enderReme.xBairro = belUtil.TiraSimbolo(dr["xBairro"].ToString(), "");
                objbelinfCte.rem.enderReme.xMun = belUtil.TiraSimbolo(dr["xMun"].ToString(), "");
                objbelinfCte.rem.enderReme.UF = dr["UF"].ToString();
                objbelinfCte.rem.enderReme.cMun = dr["cMun"].ToString();
                objbelinfCte.rem.enderReme.CEP = util.TiraSimbolo(dr["CEP"].ToString());
                objbelinfCte.rem.enderReme.xPais = "Brasil";
                objbelinfCte.rem.enderReme.cPais = "1058";



            }
            catch (Exception ex)
            {
                throw ex;
            }



        }


    }
}
