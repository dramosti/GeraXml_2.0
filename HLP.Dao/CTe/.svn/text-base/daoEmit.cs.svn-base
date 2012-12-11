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
    public class daoEmit
    {

        public void PopulaEmit(belinfCte objbelinfCte, FbConnection conn)
        {
            try
            {
                StringBuilder sQuery = new StringBuilder();

                sQuery.Append("Select ");
                sQuery.Append("coalesce (empresa.cd_cgc,'')CNPJ, ");
                sQuery.Append("coalesce (empresa.cd_insest,'') IE, ");
                sQuery.Append("empresa.nm_empresa xNome, ");
                sQuery.Append("empresa.nm_guerra xFant, ");
                sQuery.Append("coalesce (empresa.ds_endnor,'') xLgr, ");
                sQuery.Append("coalesce (empresa.ds_endcomp,'')xCpl, ");
                sQuery.Append("coalesce (empresa.nr_end,'')nro, ");
                sQuery.Append("empresa.nm_bairronor xBairro, ");
                sQuery.Append("coalesce (cidades.cd_municipio,'')cMun, ");
                sQuery.Append("empresa.nm_cidnor xMun, ");
                sQuery.Append("coalesce (empresa.cd_cepnor,'')CEP, ");
                sQuery.Append("coalesce (empresa.cd_ufnor,'') UF, ");
                sQuery.Append("coalesce (empresa.cd_fonenor,'')fone ");
                sQuery.Append("from empresa ");
                sQuery.Append("left join cidades on empresa.nm_cidnor = cidades.nm_cidnor  and cidades.cd_ufnor = empresa.cd_ufnor  ");
                sQuery.Append("where empresa.cd_empresa = '" + belStatic.CodEmpresaCte + "'");



                FbCommand fbConn = new FbCommand(sQuery.ToString(), conn);
                fbConn.ExecuteNonQuery();
                FbDataReader dr = fbConn.ExecuteReader();
                dr.Read();

                objbelinfCte.emit = new belemit();
                objbelinfCte.emit.enderEmit = new belenderEmit();


                objbelinfCte.emit.CNPJ = util.TiraSimbolo(dr["CNPJ"].ToString());
                objbelinfCte.emit.IE = util.TiraSimbolo(dr["IE"].ToString());
                objbelinfCte.emit.xNome = belUtil.TiraSimbolo(dr["xNome"].ToString(), "");
                objbelinfCte.emit.xFant = belUtil.TiraSimbolo(dr["xFant"].ToString(), "");
                objbelinfCte.emit.enderEmit.xLgr = belUtil.TiraSimbolo(dr["xLgr"].ToString(), "");
                objbelinfCte.emit.enderEmit.nro = dr["nro"].ToString();
                objbelinfCte.emit.enderEmit.xCpl = dr["xCpl"].ToString();
                objbelinfCte.emit.enderEmit.xBairro = belUtil.TiraSimbolo(dr["xBairro"].ToString(), "");
                objbelinfCte.emit.enderEmit.UF = dr["UF"].ToString();
                objbelinfCte.emit.enderEmit.xMun = belUtil.TiraSimbolo(dr["xMun"].ToString(), "");
                objbelinfCte.emit.enderEmit.cMun = dr["cMun"].ToString();
                objbelinfCte.emit.enderEmit.CEP = dr["CEP"].ToString() != "" ? util.TiraSimbolo(dr["CEP"].ToString()) : "";
                if (dr["fone"].ToString() != "")
                {
                    objbelinfCte.emit.enderEmit.fone = util.TiraSimbolo(dr["fone"].ToString());
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }



        }


    }
}
