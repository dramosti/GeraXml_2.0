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
    public class daorodo
    {
        public void PopulaRodo(belinfCte objbelinfCte, FbConnection conn, string sCte)
        {
            try
            {
                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("Select ");
                sQuery.Append("coalesce(conhecim.ds_respseg,'')respSeg, ");
                sQuery.Append("coalesce(empresa.CD_APOBRIG,'')nApol, ");
                sQuery.Append("coalesce(empresa.cd_rtb,'')RNTRC, ");
                sQuery.Append("coalesce(conhecim.DT_PREV,'')dPrev, ");
                sQuery.Append("coalesce(conhecim.CD_LOTA,'')lota ");
                sQuery.Append("from conhecim ");
                sQuery.Append("join empresa on conhecim.cd_empresa = empresa.cd_empresa ");
                sQuery.Append("where conhecim.nr_lanc ='" + sCte + "' ");
                sQuery.Append("and empresa.cd_empresa ='" + belStatic.CodEmpresaCte + "'");

                FbCommand fbConn = new FbCommand(sQuery.ToString(), conn);
                fbConn.ExecuteNonQuery();
                FbDataReader dr = fbConn.ExecuteReader();



                objbelinfCte.infCTeNorm.seg = new belseg();
                objbelinfCte.infCTeNorm.rodo = new belrodo();

                while (dr.Read())
                {
                    objbelinfCte.infCTeNorm.seg.respSeg = dr["respSeg"].ToString() != "" ? dr["respSeg"].ToString().Substring(0, 1) : "";
                    objbelinfCte.infCTeNorm.seg.nApol = dr["nApol"].ToString() != "" ? util.TiraSimbolo(dr["nApol"].ToString()) : "";
                    objbelinfCte.infCTeNorm.rodo.RNTRC = dr["RNTRC"].ToString();
                    objbelinfCte.infCTeNorm.rodo.dPrev = dr["dPrev"].ToString();
                    objbelinfCte.infCTeNorm.rodo.lota = dr["lota"].ToString();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public void PopulaVeiculo(belinfCte objbelinfCte, FbConnection conn, string sCte)
        {
            try
            {
                objbelinfCte.infCTeNorm.rodo.veic = new List<belveic>();
                if (objbelinfCte.infCTeNorm.rodo.lota == "1")
                {
                    for (int tot = 0; tot < objbelinfCte.ide.Veiculo.Count; tot++)
                    {
                        StringBuilder sQuery = new StringBuilder();
                        sQuery.Append("Select ");
                        sQuery.Append("coalesce(veiculo.cd_renavam,'')RENAVAM, ");
                        sQuery.Append("coalesce(veiculo.cd_placa,'')placa, ");
                        sQuery.Append("coalesce(veiculo.cd_tara,'0.00')tara, ");
                        sQuery.Append("coalesce(veiculo.cd_tonela,'0.00')capKG, ");
                        sQuery.Append("coalesce(veiculo.cd_m3,'0.00')capM3, ");
                        sQuery.Append("coalesce(veiculo.st_tpproprietario,'')tpProp, ");
                        sQuery.Append("coalesce(veiculo.cd_tipo,'')tpVeic, ");
                        sQuery.Append("coalesce(veiculo.st_rodado,'')tpRod, ");
                        sQuery.Append("coalesce(veiculo.st_carroceria,'')tpCar, ");
                        sQuery.Append("coalesce(veiculo.cd_uf,'')UF ");
                        sQuery.Append("from veiculo ");
                        sQuery.Append("where veiculo.cd_veiculo ='" + objbelinfCte.ide.Veiculo[tot] + "' ");


                        FbCommand fbConn = new FbCommand(sQuery.ToString(), conn);
                        fbConn.ExecuteNonQuery();
                        FbDataReader dr = fbConn.ExecuteReader();

                        while (dr.Read())
                        {
                            belveic veic = new belveic();
                            veic.RENAVAM = dr["RENAVAM"].ToString();
                            veic.placa = dr["placa"].ToString();
                            veic.tara = dr["tara"].ToString().Substring(0, dr["tara"].ToString().IndexOf('.'));
                            veic.capKG = dr["capKG"].ToString().Substring(0, dr["capKG"].ToString().IndexOf('.'));
                            veic.capM3 = dr["capM3"].ToString().Substring(0, dr["capM3"].ToString().IndexOf('.'));
                            veic.tpProp = dr["tpProp"].ToString();
                            veic.tpVeic = dr["tpVeic"].ToString();
                            veic.tpRod = dr["tpRod"].ToString();
                            veic.tpCar = dr["tpCar"].ToString();
                            veic.UF = dr["UF"].ToString();

                            objbelinfCte.infCTeNorm.rodo.veic.Add(veic);
                        }

                        dr.Dispose();

                        if (objbelinfCte.infCTeNorm.rodo.veic[tot].tpProp == "T")
                        {
                            sQuery = new StringBuilder();
                            sQuery.Append("Select ");
                            sQuery.Append("coalesce(veiculo.cd_cgc,'')CPF, ");
                            sQuery.Append("coalesce(veiculo.cd_rtb,'')RNTRC, ");
                            sQuery.Append("coalesce(veiculo.nm_proprie,'')xNome, ");
                            sQuery.Append("coalesce(veiculo.cd_ie,'ISENTO')IE, ");
                            sQuery.Append("coalesce(veiculo.cd_uf,'')UF, ");
                            sQuery.Append("coalesce(veiculo.ST_TPPROP,'')tpProp ");
                            sQuery.Append("from veiculo ");
                            sQuery.Append("where veiculo.cd_veiculo ='" + objbelinfCte.ide.Veiculo[tot] + "' ");

                            fbConn = new FbCommand(sQuery.ToString(), conn);
                            fbConn.ExecuteNonQuery();
                            dr = fbConn.ExecuteReader();

                            objbelinfCte.infCTeNorm.rodo.veic[tot].prop = new belprop();
                            while (dr.Read())
                            {
                                objbelinfCte.infCTeNorm.rodo.veic[tot].prop.CPFCNPJ = util.TiraSimbolo(dr["CPF"].ToString());
                                objbelinfCte.infCTeNorm.rodo.veic[tot].prop.RNTRC = dr["RNTRC"].ToString();
                                objbelinfCte.infCTeNorm.rodo.veic[tot].prop.xNome = belUtil.TiraSimbolo(dr["xNome"].ToString(), "");
                                objbelinfCte.infCTeNorm.rodo.veic[tot].prop.IE = util.TiraSimbolo(dr["IE"].ToString());
                                objbelinfCte.infCTeNorm.rodo.veic[tot].prop.UF = dr["UF"].ToString();
                                objbelinfCte.infCTeNorm.rodo.veic[tot].prop.tpProp = dr["tpProp"].ToString();
                            }
                            dr.Dispose();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public void PopulaMotorista(belinfCte objbelinfCte, FbConnection conn, string sCte)
        {
            try
            {
                if (objbelinfCte.infCTeNorm.rodo.veic.Count() > 0)
                {
                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append("Select ");
                    sQuery.Append("coalesce(motorista.nm_motoris,'')xNome, ");
                    sQuery.Append("coalesce(motorista.cd_cgc,'')CPF ");
                    sQuery.Append("from motorista ");
                    sQuery.Append("where motorista.cd_motoris ='" + objbelinfCte.ide.Motorista + "' ");


                    FbCommand fbConn = new FbCommand(sQuery.ToString(), conn);
                    fbConn.ExecuteNonQuery();
                    FbDataReader dr = fbConn.ExecuteReader();

                    if (objbelinfCte.infCTeNorm.rodo.lota == "1")
                    {
                        objbelinfCte.infCTeNorm.rodo.moto = new belmoto();
                    }
                    while (dr.Read())
                    {
                        objbelinfCte.infCTeNorm.rodo.moto = new belmoto();
                        objbelinfCte.infCTeNorm.rodo.moto.xNome = belUtil.TiraSimbolo(dr["xNome"].ToString(), "");
                        objbelinfCte.infCTeNorm.rodo.moto.CPF = util.TiraSimbolo(dr["CPF"].ToString());
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
