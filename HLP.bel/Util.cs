﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using HLP.bel;
namespace HLP.Util
{
    //Danner o.s. 23722 30/09/2009 -- + Criaçao da classe para utilização de métodos sem instância

    public static class Util
    {
        public static string etapa;
        //Danner - o.s. 23732 - 11/11/2009
        public static int retonarTpamb(string sEmp)
        {

            belGerarXML BuscaConexao = new belGerarXML();
            int tp_amb;
            try
            {
                StringBuilder sSql = new StringBuilder();

                sSql.Append("select ");
                sSql.Append("empresa.st_ambiente ");
                sSql.Append("uf.nr_ufnfe cd_ufnor, ");
                sSql.Append("from empresa ");
                sSql.Append("left join uf on (uf.cd_uf = empresa.cd_ufnor) ");
                sSql.Append("where ");
                sSql.Append("cd_empresa ='");
                sSql.Append(sEmp);
                sSql.Append("'");

                FbConnection Conn = BuscaConexao.Conn;

                Conn.Open();

                FbCommand cmdIde = new FbCommand(sSql.ToString(), Conn);
                cmdIde.ExecuteNonQuery();



                FbDataReader dr = cmdIde.ExecuteReader();
                dr.Read();

                tp_amb = Convert.ToInt16((dr["st_ambiente"].ToString() != "" ? dr["st_ambiente"].ToString() : "2"));

                Conn.Close();
            }
            catch (Exception x)
            {
                throw new Exception("Erro de " + x.Source + Environment.NewLine + x.Message);
            }

            return tp_amb;

        }
        //Fim - Danner - o.s. 23732 - 11/11/2009
        //Danner - o.s. 23732 - 13/11/2009
        public static string retornaCdfunor(string sEmp)
        {

            belGerarXML BuscaConexao = new belGerarXML();
            string cd_ufnor;
            try
            {
                StringBuilder sSql = new StringBuilder();

                sSql.Append("select ");

                sSql.Append("uf.nr_ufnfe cd_ufnor, ");
                sSql.Append("from empresa ");
                sSql.Append("left join uf on (uf.cd_uf = empresa.cd_ufnor) ");
                sSql.Append("where ");
                sSql.Append("cd_empresa ='");
                sSql.Append(sEmp);
                sSql.Append("'");

                FbConnection Conn = BuscaConexao.Conn;

                Conn.Open();

                FbCommand cmdIde = new FbCommand(sSql.ToString(), Conn);
                cmdIde.ExecuteNonQuery();



                FbDataReader dr = cmdIde.ExecuteReader();
                dr.Read();

                cd_ufnor = dr["cd_ufnor"].ToString();

                Conn.Close();
            }
            catch (Exception x)
            {
                throw new Exception("Erro de " + x.Source + Environment.NewLine + x.Message);
            }

            return cd_ufnor;

        }
        //Fim - Danner - o.s. 23732 - 13/11/2009
        public static string TiraSimbolo(string sString, string sIgnorar)
        {
            sString = sString.Replace("\\viewkind4\\uc1\\pard\\f0\\fs16 ", "");
            sString = sString.Replace("{\\colortbl ;\\red0\\green0\\blue255;}\\viewkind4\\uc1 d\\cf1\\lang1046\\fs16   ", "");
            sString = sString.Replace("\\viewkind4\\uc1 d\\b\\fs16 ", "");
            sString = sString.Replace("\\f1\\'c7", "C");
            sString = sString.Replace("\\'c3", "A");
            sString = sString.Replace("\\f0 ", "");
            sString = sString.Replace("\\par", " ");
            sString = sString.Replace("}\0", "");
            sString = sString.Replace("\\f0", "");
            sString = sString.Replace("\\'ba", "o");
            sString = sString.Replace("\\f1", "");
            sString = sString.Replace("\\'cd", "I");
            sString = sString.Replace("\\'aa", "a");
            sString = sString.Replace("\\'e1", "a");
            sString = sString.Replace("\\'e7\\'e3", "ca");
            sString = sString.Replace("\\b0", ".");
            sString = sString.Replace("\\'e3", "a");
            sString = sString.Replace("\\'ea", "e");
            sString = sString.Replace("\\c9", "E");
            string[,] sSimbolos = {   
                                    { "á", "a" },
                                    { "Á", "A" },
                                    { "é", "e" },
                                    { "É", "E" },
                                    { "í", "i" },
                                    { "Í", "I" },
                                    { "ó", "o" },
                                    { "Ó", "O" },
                                    { "ú", "u" },
                                    { "Ú", "U" },
                                    { "ã", "a" },
                                    { "Ã", "A" },
                                    { "õ", "o" },
                                    { "Õ", "O" },
                                    { "â", "a" },
                                    { "Â", "A" },
                                    { "ê", "e" },
                                    { "Ê", "E" },
                                    { "ô", "o" },
                                    { "Ô", "O" },
                                    { "/", "" },
                                    { "ç", "c" },
                                    { "Ç", "C" },
                                    { "-", "" },
                                    { "  ", "" },
                                    { ".", "" },
                                    { "(", "" },
                                    { ")", "" },
                                    { "°", "o" },
                                    { "�", " "},
                                    { "&", "E"},
                                    { "*", ""},
                                    { "º", "o"},
                                    { "\"", ""},
                                    { "Ø", ""},
                                    { "'", ""}
                                };

            string Resultado = "";
            string sCar = "";

            for (int i = 0; i <= (sString.Length - 1); i++)
            {
                sCar = sString[i].ToString();
                for (int y = 0; y <= (sSimbolos.GetLength(0) - 1); y++)
                {
                    if ((sCar == sSimbolos[y, 0]) && (sCar != sIgnorar))
                    {
                        sString = sString.Replace(sCar, sSimbolos[y, 1]);
                    }
                }

            }
            Resultado = sString;
            return Resultado;
        }

        public static bool ValidacEAN(string scodigo)
        {
            try
            {
                Globais objGlobais = new Globais();
                if (((scodigo.Length == 8) || (scodigo.Length == 12) || (scodigo.Length == 13) || (scodigo.Length == 14))
                    && (Convert.ToBoolean(objGlobais.LeRegConfig("CodBarrasXml"))))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool IsNumeric(object Expression)
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

        public static bool VerificaNovaST(string scst)
        {
            List<NovasCst> objList = new List<NovasCst>()
                    {
                        new NovasCst{ nsct = "101",sgrupo="101"},
                        new NovasCst{ nsct = "102",sgrupo="102"},
                        new NovasCst{ nsct = "103",sgrupo="102"},
                        new NovasCst{ nsct = "300",sgrupo="102"},
                        new NovasCst{ nsct = "400",sgrupo="102"},
                        new NovasCst{ nsct = "201",sgrupo="201"},
                        new NovasCst{ nsct = "201",sgrupo="202"},
                        new NovasCst{ nsct = "201",sgrupo="203"},
                        new NovasCst{ nsct = "500",sgrupo="500"},
                        new NovasCst{ nsct = "900",sgrupo="900"}
                    };

            return (objList.Count(b => b.nsct == scst) > 0 ? true : false);
        }

        public static string RetornaSTnovaAserUsada(string scst)
        {
            List<NovasCst> objList = new List<NovasCst>()
                    {
                        new NovasCst{ nsct = "101",sgrupo="101"},
                        new NovasCst{ nsct = "102",sgrupo="102"},
                        new NovasCst{ nsct = "103",sgrupo="102"},
                        new NovasCst{ nsct = "300",sgrupo="102"},
                        new NovasCst{ nsct = "400",sgrupo="102"},
                        new NovasCst{ nsct = "201",sgrupo="201"},
                        new NovasCst{ nsct = "201",sgrupo="202"},
                        new NovasCst{ nsct = "201",sgrupo="203"},
                        new NovasCst{ nsct = "500",sgrupo="500"},
                        new NovasCst{ nsct = "900",sgrupo="900"}
                    };

            return (objList.FirstOrDefault(b => b.nsct == scst)).sgrupo;

        }

        private struct NovasCst
        {
            public string nsct { get; set; }
            public string sgrupo { get; set; }
        }

        public static string TiraCaracterEstranho(string sString)
        {
            sString = sString.Replace("\\viewkind4\\uc1\\pard\\f0\\fs16 ", "");
            sString = sString.Replace("\\viewkind4\\uc1 d\\lang1046 ", "");
            sString = sString.Replace("\\f1\\'c7", "C");
            sString = sString.Replace("\\'c3", "A");
            sString = sString.Replace("\\f0 ", "");
            sString = sString.Replace("\\par", " ");
            sString = sString.Replace("}\0", "");
            sString = sString.Replace("\\f0", "");
            sString = sString.Replace("{\\colortbl ;\\red0\\green0\\blue255;}\\viewkind4\\uc1 d\\cf1\\lang1046\\fs16   ", "");
            sString = sString.Replace("\\'ba", "o");
            sString = sString.Replace("\\f1", "");
            sString = sString.Replace("\\'cd", "I");
            sString = sString.Replace("\\viewkind4\\uc1 d\\b\\fs16 ", "");
            sString = sString.Replace("\\'aa", "a");
            sString = sString.Replace("\\'e1", "a");
            sString = sString.Replace("\\'e7\\'e3", "ca");
            sString = sString.Replace("\\b0", ".");
            sString = sString.Replace("\\'e3", "a");
            sString = sString.Replace("\\'ea", "e");
            sString = sString.Replace("\\c9", "E");
             sString = sString.Replace("\\c9", "E");
            sString = sString.Replace("\\fs52", "");
            sString = sString.Replace("\\fs16", "");

            while (sString.Contains("  "))
            {
                sString = sString.Replace("  ", " ");

            }

            return sString;
        }


        public static string RetiraCaracterCNPJ(string sCNPJ)
        {
            return (sCNPJ != null ? sCNPJ.Substring(0, 2) +
                       "." +
                       sCNPJ.Substring(2, 3) +
                       "." +
                       sCNPJ.Substring(5, 3) +
                       "/" +
                       sCNPJ.Substring(8, 4) +
                       "-" +
                       sCNPJ.Substring(12, 2) : "");
        }
    }
    //Danner -Fim 30/09/2009
}
