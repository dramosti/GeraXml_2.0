using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using HLP.bel.CTe;
using HLP.bel.Static;
using HLP.bel;

namespace HLP.Dao.CTe
{
    public class daoIde
    {
        public void PopulaIde(string sCte, string sDigVerif, FbConnection conn, belinfCte objbelinfCte, string sId)
        {
            try
            {

                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("select ");
                sQuery.Append("coalesce(conhecim.cd_consignat,'')cd_consignat, ");
                sQuery.Append("coalesce(empresa.cd_ufnor,'') cUF,");
                sQuery.Append("coalesce(conhecim.cd_respons,'') Tomador,");
                sQuery.Append("coalesce(conhecim.nr_lanc,'') cCT,");
                sQuery.Append("coalesce(conhecim.cd_cfop,'') CFOP,");
                sQuery.Append("coalesce(natop.ds_natop,'') natOp ,");
                sQuery.Append("coalesce(conhecim.cd_serie,'') serie,");
                sQuery.Append("coalesce(conhecim.cd_conheci,'') nCT,");
                sQuery.Append("coalesce(empresa.nm_cidnor,'') xMunEmi,");
                sQuery.Append("coalesce(empresa.cd_ufnor,'') UFEmi,");
                sQuery.Append("coalesce(cidade1.cd_municipio,'') cMunIni,");
                sQuery.Append("coalesce(conhecim.ds_cidcole,'') xMunIni,");
                sQuery.Append("coalesce(conhecim.cd_ufcole,'') UFIni,");
                sQuery.Append("coalesce(destino.cd_uf,'') UFFim, ");
                sQuery.Append("coalesce(cidade2.cd_municipio,'') cMunFim,");
                sQuery.Append("coalesce(conhecim.ds_calc,'') xMunFim,");
                sQuery.Append("coalesce(conhecim.st_forpag,'1') forPag,");
                sQuery.Append("coalesce(conhecim.cd_veiculo,'') Veiculo,");
                sQuery.Append("coalesce(conhecim.cd_veiculo2,'') Veiculo2,");
                sQuery.Append("coalesce(conhecim.cd_veiculo3,'') Veiculo3,");
                sQuery.Append("coalesce(conhecim.cd_veiculo4,'') Veiculo4,");
                sQuery.Append("coalesce(conhecim.cd_motoris,'') Motorista ");
                sQuery.Append("from conhecim left join natop  on conhecim.cd_cfop = natop.cd_cfop ");
                sQuery.Append("left join empresa on conhecim.cd_empresa = empresa.cd_empresa ");
                sQuery.Append("left join remetent destino on (destino.cd_remetent = conhecim.cd_redes and conhecim.cd_redes is not null) ");
                sQuery.Append("or (destino.cd_remetent = conhecim.cd_destinat and conhecim.cd_redes is null) ");
                sQuery.Append("left join clifor on conhecim.cd_clifor = clifor.cd_clifor ");
                sQuery.Append("left join cidades cidade1 on  cidade1.nm_cidnor = conhecim.ds_cidcole  and cidade1.cd_ufnor = conhecim.cd_ufcole  ");
                sQuery.Append("left join cidades cidade2 on  cidade2.nm_cidnor = conhecim.ds_calc  and cidade2.cd_ufnor = destino.cd_uf  ");
                sQuery.Append("where empresa.cd_empresa ='" + belStatic.CodEmpresaCte);
                sQuery.Append("' and conhecim.nr_lanc ='" + sCte + "'");

                FbCommand fbConn = new FbCommand(sQuery.ToString(), conn);
                fbConn.ExecuteNonQuery();
                FbDataReader dr = fbConn.ExecuteReader();
                dr.Read();

                objbelinfCte.ide = new belide();
                bel.belUF objbelUF = new bel.belUF();

                objbelinfCte.id = sId;
                objbelinfCte.ide.cUF = objbelUF.RetornaCUF(dr["cUF"].ToString());
                objbelinfCte.ide.cCT = dr["cCT"].ToString();
                objbelinfCte.ide.CFOP = dr["CFOP"].ToString();
                objbelinfCte.ide.natOp = dr["natOp"].ToString().Length > 60 ? dr["natOp"].ToString().Substring(0, 60) : dr["natOp"].ToString();
                objbelinfCte.ide.forPag = Convert.ToInt32(dr["forPag"]);
                objbelinfCte.ide.mod = "57";
                objbelinfCte.ide.serie = belStatic.bModoContingencia == true ? "900" : "1";
                objbelinfCte.ide.nCT = dr["nCT"].ToString();
                objbelinfCte.ide.tpImp = "1";
                objbelinfCte.ide.tpEmis = belStatic.bModoContingencia == true ? "5" : "1";
                objbelinfCte.ide.cDV = sDigVerif;
                objbelinfCte.ide.tpAmb = Convert.ToString(belStatic.TpAmb);
                objbelinfCte.ide.tpCTe = 0;
                objbelinfCte.ide.procEmi = 0;
                objbelinfCte.ide.verProc = "1.04";
                objbelinfCte.ide.xMunEnv = dr["xMunEmi"].ToString();
                objbelinfCte.ide.UFEnv = dr["UFEmi"].ToString();
                objbelinfCte.ide.cMunEnv = RetornaCodigoCidade(objbelinfCte.ide.xMunEnv, objbelinfCte.ide.UFEnv, conn);
                objbelinfCte.ide.modal = "01";
                objbelinfCte.ide.tpServ = 0;
                objbelinfCte.ide.xMunIni = dr["xMunIni"].ToString();
                objbelinfCte.ide.UFIni = dr["UFIni"].ToString();
                objbelinfCte.ide.cMunIni = dr["cMunIni"].ToString();
                objbelinfCte.ide.xMunFim = dr["xMunFim"].ToString();
                objbelinfCte.ide.UFFim = dr["UFFim"].ToString();
                objbelinfCte.ide.cMunFim = dr["cMunFim"].ToString();
                objbelinfCte.ide.retira = 0;
                objbelinfCte.ide.xDetRetira = null;
                if (dr["Veiculo"].ToString() != "") { objbelinfCte.ide.Veiculo.Add(dr["Veiculo"].ToString()); }
                if (dr["Veiculo2"].ToString() != "") { objbelinfCte.ide.Veiculo.Add(dr["Veiculo2"].ToString()); }
                if (dr["Veiculo3"].ToString() != "") { objbelinfCte.ide.Veiculo.Add(dr["Veiculo3"].ToString()); }
                if (dr["Veiculo4"].ToString() != "") { objbelinfCte.ide.Veiculo.Add(dr["Veiculo4"].ToString()); }

                objbelinfCte.ide.Motorista = dr["Motorista"].ToString();

                string sTipoTomador = dr["Tomador"].ToString();
                switch (sTipoTomador)
                {
                    case "R": objbelinfCte.ide.toma03 = new beltoma03();
                        objbelinfCte.ide.toma03.toma = "0";
                        break;

                    case "D": objbelinfCte.ide.toma03 = new beltoma03();
                        objbelinfCte.ide.toma03.toma = "3";
                        break;

                    default: objbelinfCte.ide.toma04 = new beltoma04();
                        if (dr["cd_consignat"].ToString() != "")
                        {
                            CarregaToma4(dr["cd_consignat"].ToString(), objbelinfCte.ide.toma04, conn);
                        }
                        else
                        {
                            throw new Exception("Não foi selecionado um Consignatário no sistema");
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        void CarregaToma4(string sCD_REMENET, beltoma04 obj, FbConnection conn)
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
                sQuery.Append("left join cidades on remetent.nm_cida = cidades.nm_cidnor  and cidades.cd_ufnor = remetent.cd_uf ");
                sQuery.Append("WHERE remetent.cd_remetent = '" + sCD_REMENET + "'");

                FbCommand fbConn = new FbCommand(sQuery.ToString(), conn);
                fbConn.ExecuteNonQuery();
                FbDataReader dr = fbConn.ExecuteReader();
                dr.Read();

                obj.CNPJ = util.TiraSimbolo(dr["CNPJ"].ToString());
                obj.CPF = util.TiraSimbolo(dr["CPF"].ToString());
                obj.IE = util.TiraSimbolo(dr["IE"].ToString());
                obj.xNome = belUtil.TiraSimbolo(dr["xNome"].ToString(), "");
                obj.xFant = belUtil.TiraSimbolo(dr["xFant"].ToString(), "");
                obj.fone = util.TiraSimbolo(dr["fone"].ToString());
                obj.enderToma = new belenderToma();
                obj.enderToma.xLgr = belUtil.TiraSimbolo(dr["xLgr"].ToString(), "");
                obj.enderToma.nro = dr["nro"].ToString();
                obj.enderToma.xBairro = belUtil.TiraSimbolo(dr["xBairro"].ToString(), "");
                obj.enderToma.xMun = belUtil.TiraSimbolo(dr["xMun"].ToString(), "");
                obj.enderToma.UF = dr["UF"].ToString();
                obj.enderToma.cMun = dr["cMun"].ToString();
                obj.enderToma.CEP = util.TiraSimbolo(dr["CEP"].ToString());
                obj.enderToma.xPais = "Brasil";
                obj.enderToma.cPais = "1058";
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }


   

        public string RetornaCodigoCidade(string sCidade, string sUf, FbConnection conn)
        {
            try
            {
                string sCod = "";

                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("Select coalesce(cidades.cd_municipio,'') cd_municipio from cidades ");
                sQuery.Append("Where cidades.nm_cidnor ='" + sCidade + "'");
                sQuery.Append(" and cidades.cd_ufnor='" + sUf + "'");
                FbCommand fbConn = new FbCommand(sQuery.ToString(), conn);

                fbConn.ExecuteNonQuery();
                FbDataReader dr = fbConn.ExecuteReader();

                while (dr.Read())
                {
                    sCod = dr["cd_municipio"].ToString();
                }

                return sCod;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


    }
}
