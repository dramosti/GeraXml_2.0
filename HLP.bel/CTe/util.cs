using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HLP.bel.CTe
{
   public static class util
    {
        /// <summary>
        /// Envio - Cancelados - Enviados - Protocolos - Retorno
        /// </summary>
        /// <param name="sPasta"></param>
        /// <returns></returns>
        public static string RetCaminhoPastaPadrao(string sPasta)
        {
            try
            {
                string sCaminho = "";
                switch (sPasta)
                {
                    case "Envio":
                        {
                            sCaminho = Application.StartupPath + @"\Pastas\Envio\";
                        }
                        break;
                    case "Cancelados":
                        {
                            sCaminho = Application.StartupPath + @"\Pastas\Cancelados\";
                        }
                        break;
                    case "Enviados":
                        {
                            sCaminho = Application.StartupPath + @"\Pastas\Enviados\";
                        }
                        break;
                    case "Protocolos":
                        {
                            sCaminho = Application.StartupPath + @"\Pastas\Protocolos\";
                        }
                        break;
                    case "Retorno":
                        {
                            sCaminho = Application.StartupPath + @"\Pastas\Retorno\";
                        }
                        break;
                }
                return sCaminho;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static string TiraSimbolo(string sString)
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
            sString = sString.Replace(" ", "");
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
                    if ((sCar == sSimbolos[y, 0]))
                    {
                        sString = sString.Replace(sCar, sSimbolos[y, 1]);
                    }
                }

            }
            Resultado = sString;
            return Resultado;
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
