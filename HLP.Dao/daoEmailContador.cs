using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using HLP.bel;
using System.IO;
using System.Xml.Linq;
using HLP.bel.Static;
using HLP.bel.CTe;

namespace HLP.Dao
{
    public class daoEmailContador
    {
        public bool VerificaEmailContador()
        {
            belConnection cx = new belConnection();
            try
            {
                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("select coalesce(empresa.cd_emailcont,'')cd_emailcont ,  empresa.nm_empresa from empresa ");
                sQuery.Append("where empresa.cd_empresa = '" + belStatic.codEmpresaNFe + "'");
                FbCommand fbCom = new FbCommand(sQuery.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                FbDataReader dr = fbCom.ExecuteReader();
                dr.Read();
                string sEmailCont = dr["cd_emailcont"].ToString();

                if (sEmailCont.Equals(""))
                {
                    return false;
                    throw new Exception("Não existe nenhum e-mail de contador cadastrado nesse empresa");
                }
                else
                {
                    belStatic.sEmailContador = sEmailCont;
                    belStatic.sNomeEmpresaCompleto = dr["nm_empresa"].ToString();
                    return true;
                }

            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
            finally
            {
                cx.Close_Conexao();
            }

        }

        public bool VerificaDiaParaEnviarEmail()
        {
            bool bAvisa = false;
            DirectoryInfo dinfo = new DirectoryInfo(belStaticPastas.ENVIADOS + "\\Contador_xml");
            if (dinfo.Exists)
            {
                string sCaminhoXml = dinfo.FullName + "\\" + "ConfigDia.txt";
                if (File.Exists(sCaminhoXml))
                {
                    FileStream arquivo = File.Open(sCaminhoXml, FileMode.Open);

                    StreamReader reader = new StreamReader(arquivo);

                    string sDia = reader.ReadToEnd().Trim();

                    reader.Close();

                    if (sDia == "month")
                    {
                        if (HLP.Util.Util.GetDateServidor().Date.Day.ToString().Equals("1"))
                        {
                            bAvisa = true;
                        }
                    }
                    else if (HLP.Util.Util.GetDateServidor().Date.DayOfWeek.ToString().Equals(sDia))
                    {
                        bAvisa = true;
                    }

                }
            }
            return bAvisa;
        }
    }
}


