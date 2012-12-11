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
    public class daoDest
    {
        public void PopulaDest(belinfCte objbelinfCte, FbConnection conn, string sCte)
        {
            try
            {
                StringBuilder sQuery = new StringBuilder();


                sQuery.Append("Select ");
                sQuery.Append("coalesce(remetent.cd_cgc,'')CNPJ, ");
                sQuery.Append("coalesce(remetent.cd_cpf,'')CPF, ");
                sQuery.Append("coalesce(remetent.cd_insest,'')IE, ");
                sQuery.Append("coalesce(remetent.nm_social,'')xNome, ");
                sQuery.Append("coalesce(remetent.cd_fone,'')fone, ");
                sQuery.Append("coalesce(remetent.cd_isuf,'')ISUF, ");
                sQuery.Append("coalesce (cidades.cd_municipio,'')cMun, ");
                sQuery.Append("coalesce(remetent.ds_ende,'')xLgr, ");
                sQuery.Append("coalesce(remetent.nr_end,'')nro, ");
                sQuery.Append("coalesce(remetent.ds_bairro,'')xBairro, ");
                sQuery.Append("coalesce(remetent.nm_cida,'')xMun, ");
                sQuery.Append("coalesce(remetent.cd_cep,'')CEP, ");
                sQuery.Append("coalesce(remetent.cd_uf,'')UF, ");
                sQuery.Append("coalesce(remetent.cd_pais,'')cPais ");
                sQuery.Append("from remetent ");
                sQuery.Append("join conhecim on  conhecim.cd_destinat  = remetent.cd_remetent ");
                sQuery.Append("join empresa  on  conhecim.cd_empresa = empresa.cd_empresa ");
                sQuery.Append("left join cidades on remetent.nm_cida = cidades.nm_cidnor  and cidades.cd_ufnor = remetent.cd_uf  ");
                sQuery.Append("where conhecim.nr_lanc ='" + sCte + "'");
                sQuery.Append("and empresa.cd_empresa='" + belStatic.CodEmpresaCte + "'");




                FbCommand fbConn = new FbCommand(sQuery.ToString(), conn);
                fbConn.ExecuteNonQuery();
                FbDataReader dr = fbConn.ExecuteReader();
                dr.Read();

                objbelinfCte.dest = new beldest();
                objbelinfCte.dest.enderDest = new belenderDest();


                objbelinfCte.dest.CNPJ = util.TiraSimbolo(dr["CNPJ"].ToString());
                objbelinfCte.dest.CPF = util.TiraSimbolo(dr["CPF"].ToString());
                objbelinfCte.dest.IE = util.TiraSimbolo(dr["IE"].ToString());
                objbelinfCte.dest.xNome = belUtil.TiraSimbolo(dr["xNome"].ToString(), "");
                objbelinfCte.dest.fone = util.TiraSimbolo(dr["fone"].ToString());
                objbelinfCte.dest.ISUF = dr["ISUF"].ToString();

                objbelinfCte.dest.enderDest.xLgr = belUtil.TiraSimbolo(dr["xLgr"].ToString(), "");
                objbelinfCte.dest.enderDest.nro = dr["nro"].ToString();
                objbelinfCte.dest.enderDest.xBairro = belUtil.TiraSimbolo(dr["xBairro"].ToString(), "");
                objbelinfCte.dest.enderDest.xMun = belUtil.TiraSimbolo(dr["xMun"].ToString(), "");
                objbelinfCte.dest.enderDest.UF = dr["UF"].ToString();
                objbelinfCte.dest.enderDest.cMun = dr["cMun"].ToString();
                objbelinfCte.dest.enderDest.CEP = util.TiraSimbolo(dr["CEP"].ToString());
                objbelinfCte.dest.enderDest.xPais = "Brasil";
                objbelinfCte.dest.enderDest.cPais = "1058";



            }
            catch (Exception ex)
            {
                throw ex;
            }



        }



    }
}
