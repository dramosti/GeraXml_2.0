using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HLP.bel.CTe;
using System.IO;
using FirebirdSql.Data.FirebirdClient;
using System.Windows.Forms;
using HLP.bel.Static;
using HLP.bel;

namespace HLP.Dao.CTe
{
    public class daoInfCte
    {
        belPopulaObjetos objbelObjetos = null;

        belConnection cx = new belConnection();

        public void ImportaConhecInfCte(belPopulaObjetos objbelObjetos, string sEmp)
        {
            this.objbelObjetos = objbelObjetos;
            try
            {

                if (File.Exists(objbelObjetos.sPath))
                {
                    File.Delete(objbelObjetos.sPath);
                }

                foreach (string sCte in objbelObjetos.objlConhec)
                {

                    string sNFe = "CTe" + GeraChave(sEmp, sCte);
                    belinfCte objbelinfCte = new belinfCte();
                    daoIde objDaoide = new daoIde();
                    daoComp objdaoComp = new daoComp();
                    daoEmit objDaoemit = new daoEmit();
                    daoRem objDaorem = new daoRem();
                    daoDest objDaodest = new daoDest();
                    daoNf objDaonf = new daoNf();
                    daoExped objDaoExped = new daoExped();
                    daoReceb objDaoReceb = new daoReceb();
                    daoVPrest objDaovPrest = new daoVPrest();
                    daoImp objDaoImp = new daoImp();
                    daoinfCarga objDaoinfCTeNorm = new daoinfCarga();
                    daoinfQ objDaoInfQ = new daoinfQ();
                    daorodo objDaoRodo = new daorodo();

                    cx.Open_Conexao();
                    objDaoide.PopulaIde(sCte, sNFe[sNFe.Length - 1].ToString(), cx.get_Conexao(), objbelinfCte, sNFe);
                    objdaoComp.PopulaComp(objbelinfCte, cx.get_Conexao(), sCte);
                    objDaoemit.PopulaEmit(objbelinfCte, cx.get_Conexao());
                    objDaorem.PopulaRem(objbelinfCte, cx.get_Conexao(), sCte);
                    objDaodest.PopulaDest(objbelinfCte, cx.get_Conexao(), sCte);
                    objDaonf.PopulaNf(objbelinfCte, cx.get_Conexao(), sCte);
                    objDaoExped.PopulaExped(objbelinfCte, cx.get_Conexao(), sCte);
                    objDaoReceb.PopulaReceb(objbelinfCte, cx.get_Conexao(), sCte);
                    objDaovPrest.PopulaVPrest(objbelinfCte, cx.get_Conexao(), sCte);
                    objDaoImp.PopulaImp(objbelinfCte, cx.get_Conexao(), sCte);
                    objDaoinfCTeNorm.PopulainfCarga(objbelinfCte, cx.get_Conexao(), sCte);
                    objDaoInfQ.PopulainfQ(objbelinfCte, cx.get_Conexao(), sCte);
                    objDaoRodo.PopulaRodo(objbelinfCte, cx.get_Conexao(), sCte);
                    objDaoRodo.PopulaVeiculo(objbelinfCte, cx.get_Conexao(), sCte);
                    objDaoRodo.PopulaMotorista(objbelinfCte, cx.get_Conexao(), sCte);

                    objbelObjetos.objLinfCte.Add(objbelinfCte);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cx.Close_Conexao();
            }
        }



        public string GeraChave(string sEmp, string sNF)
        {
            try
            {
                StringBuilder sSql = new StringBuilder();
                sSql.Append("Select ");
                sSql.Append("coalesce(empresa.cd_serie, 1) serie, c.cd_conheci nCT, ");
                sSql.Append("empresa.cd_cgc CNPJ, c.nr_lanc cCT, empresa.cd_ufnor sUF ");
                sSql.Append("From ");
                sSql.Append("conhecim c inner join empresa on (empresa.cd_empresa = c.cd_empresa) ");
                sSql.Append("where ");
                sSql.Append("(c.cd_empresa ='");
                sSql.Append(sEmp);
                sSql.Append("')");
                sSql.Append(" and ");
                sSql.Append("(c.nr_lanc = '");
                sSql.Append(sNF);
                sSql.Append("')");
                FbCommand sqlConsulta = new FbCommand(sSql.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                sqlConsulta.ExecuteNonQuery();
                FbDataReader drChave = sqlConsulta.ExecuteReader();
                drChave.Read();
                bel.belUF objbelUf = new bel.belUF();
                objbelUf.SiglaUF = drChave["sUF"].ToString();


                string sData = HLP.Util.Util.GetDateServidor().Date.ToString("dd-MM-yyyy");
                string scUF, sAAmM, sCNPJ, sMod, sSerie, snCT, scCT;

                scUF = objbelUf.CUF;
                sAAmM = sData.Substring(8, 2) + sData.Substring(3, 2);

                sCNPJ = util.TiraSimbolo(drChave["CNPJ"].ToString());
                sCNPJ = sCNPJ.PadLeft(14, '0');
                sMod = "57";


                if (IsNumeric(drChave["serie"].ToString()))
                {
                    sSerie = belStatic.bModoContingencia == true ? "900" : drChave["serie"].ToString().PadLeft(3, '0');
                }
                else
                {
                    sSerie = belStatic.bModoContingencia == true ? "900" : "001";
                }
                snCT = drChave["nCT"].ToString().PadLeft(9, '0');
                scCT = (belStatic.bModoContingencia == true ? "5" : "1") + drChave["cCT"].ToString().PadLeft(8, '0');


                string sChaveantDig = "";
                string sChave = "";
                string sDig = "";

                sChaveantDig = scUF.Trim() + sAAmM.Trim() + sCNPJ.Trim() + sMod.Trim() + sSerie.Trim() + snCT.Trim() + scCT.Trim();
                sDig = CalculaDig11(sChaveantDig).ToString();

                sChave = sChaveantDig + sDig;

                return sChave;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { cx.Close_Conexao(); }
        }
        public int CalculaDig11(string sChave)
        {

            int iDig = 0;
            int iMult = 4;
            int iTotal = 0;

            for (int i = 0; i < sChave.Length; i++)
            {
                if (iMult < 2)
                {
                    iMult = 9;
                }
                iTotal += Convert.ToInt32(sChave[i].ToString()) * iMult;
                iMult--;

            }


            int iresto = (iTotal % 11);
            if ((iresto == 0) || (iresto == 1))
            {
                iDig = 0;
            }
            else
            {
                iDig = 11 - iresto;
            }
            return iDig;
        }

        static bool IsNumeric(object Expression)
        {
            // Variable to collect the Return value of the TryParse method.
            bool isNum;

            // Define variable to collect out parameter of the TryParse method. If the conversion fails, the out parameter is zero.
            double retNum;

            // The TryParse method converts a string in a specified style and culture-specific format to its double-precision floating point number equivalent.
            // The TryParse method does not generate an exception if the conversion fails. If the conversion passes, True is returned. If it does not, False is returned.
            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

    }


}
