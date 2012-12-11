using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Drawing;
using HLP.bel.NFe.GeraXml;
using FirebirdSql.Data.FirebirdClient;

namespace HLP.bel
{
    public static class belUtil
    {

        public static bool CampoExisteNaTabela(string sNomeCampo, string sTabela)
        {
            try
            {
                belConnection cx = new belConnection();
                cx.Open_Conexao();

                string sQuery = "SELECT " +
                "RDB$FIELD_NAME CAMPO " +
                "FROM " +
                "RDB$RELATION_FIELDS " +
                "WHERE " +
                " RDB$FIELD_NAME = '{0}' AND" +
                " RDB$RELATION_NAME = '{1}'";

                FbCommand cmd = new FbCommand(string.Format(sQuery, sNomeCampo, sTabela), cx.get_Conexao());

                object iResult = cmd.ExecuteScalar();

                if (iResult == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool VerificaSeEstaNaHLP()
        {
            try
            {
                DirectoryInfo dinfo = new DirectoryInfo(@"G:\CSharp\Desenvolvimento");
                DirectoryInfo dinfo2 = new DirectoryInfo(@"J:\D6\Industri");

                if ((dinfo.Exists) && (dinfo.Exists))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception EX)
            {
                throw EX;
            }
        }

        public static string TiraSimbolo(string sString, string sIgnorar)
        {
            sString = sString.Replace("\\viewkind4\\uc1\\pard\\f0\\fs16 ", "");
            sString = sString.Replace("\\f1\\'c7", "C");
            sString = sString.Replace("\\'c3", "A");
            sString = sString.Replace("\\f0 ", "");
            sString = sString.Replace("\\par", " ");
            sString = sString.Replace("}\0", "");
            sString = sString.Replace("\\f0", "");

            //Claudinei - o.s. 23615 - 07/08/2009
            sString = sString.Replace("{\\colortbl ;\\red0\\green0\\blue255;}\\viewkind4\\uc1 d\\cf1\\lang1046\\fs16   ", "");
            sString = sString.Replace("\\'ba", "o");
            sString = sString.Replace("\\f1", "");
            //Fim - Claudinei - o.s. 23615 - 07/08/2009

            //Claudinei - o.s. sem - 28-08-2009
            sString = sString.Replace("\\'cd", "I");
            //Fim Claudinei - o.s. sem - 28-08-2009

            //Claudinei - o.s. sem - 24/08/2009
            sString = sString.Replace("\\viewkind4\\uc1 d\\b\\fs16 ", "");
            sString = sString.Replace("\\'aa", "a");
            sString = sString.Replace("\\'e1", "a");
            sString = sString.Replace("\\'e7\\'e3", "ca");
            sString = sString.Replace("\\b0", ".");

            //Fim - Claudinei - o.s. sem - 24/08/2009

            //Claudinei - o.s. sem - 01/09/2009

            sString = sString.Replace("\\'e3", "a");
            sString = sString.Replace("\\'ea", "e");
            //Fim - Claudinei - o.s. sem - 01/09/2009

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

            //Claudinei - o.s. sem - 02/03/2010
            while (sString.Contains("  "))
            {
                sString = sString.Replace("  ", " ");

            }
            //Fim - Claudinei - o.s. sem - 02/03/2010

            Resultado = sString.Trim();

            return Resultado;
        }

        public static string TiraSimbolo(string sString, string sIgnorar_1, string sIgnorar_2)
        {
            sString = sString.Replace("\\viewkind4\\uc1\\pard\\f0\\fs16 ", "");
            sString = sString.Replace("\\f1\\'c7", "C");
            sString = sString.Replace("\\'c3", "A");
            sString = sString.Replace("\\f0 ", "");
            sString = sString.Replace("\\par", " ");
            sString = sString.Replace("}\0", "");
            sString = sString.Replace("\\f0", "");

            //Claudinei - o.s. 23615 - 07/08/2009
            sString = sString.Replace("{\\colortbl ;\\red0\\green0\\blue255;}\\viewkind4\\uc1 d\\cf1\\lang1046\\fs16   ", "");
            sString = sString.Replace("\\'ba", "o");
            sString = sString.Replace("\\f1", "");
            //Fim - Claudinei - o.s. 23615 - 07/08/2009

            //Claudinei - o.s. sem - 28-08-2009
            sString = sString.Replace("\\'cd", "I");
            //Fim Claudinei - o.s. sem - 28-08-2009

            //Claudinei - o.s. sem - 24/08/2009
            sString = sString.Replace("\\viewkind4\\uc1 d\\b\\fs16 ", "");
            sString = sString.Replace("\\'aa", "a");
            sString = sString.Replace("\\'e1", "a");
            sString = sString.Replace("\\'e7\\'e3", "ca");
            sString = sString.Replace("\\b0", ".");

            //Fim - Claudinei - o.s. sem - 24/08/2009

            //Claudinei - o.s. sem - 01/09/2009

            sString = sString.Replace("\\'e3", "a");
            sString = sString.Replace("\\'ea", "e");
            //Fim - Claudinei - o.s. sem - 01/09/2009

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
                    if ((sCar == sSimbolos[y, 0]) && ((sCar != sIgnorar_1) && (sCar != sIgnorar_2)))
                    {
                        sString = sString.Replace(sCar, sSimbolos[y, 1]);
                    }
                }

            }

            //Claudinei - o.s. sem - 02/03/2010
            while (sString.Contains("  "))
            {
                sString = sString.Replace("  ", " ");
            }
            //Fim - Claudinei - o.s. sem - 02/03/2010

            Resultado = sString;

            return Resultado;
        }

        public static bool ValidaCertificado(X509Certificate2 cert)
        {
            try
            {
                Convert.ToInt32(cert.Version);
                return true;
            }
            catch (Exception)
            {
                //throw new Exception("Nenhum Certificado Digital foi selecionado.", new Exception("Selecione um certificado digital para conseguir enviar as cartas de correções"));
                return false;
            }

        }

        public static Byte[] carregaImagem(string fileName)
        {
            FileStream fs = null;
            BinaryReader br = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                br = new BinaryReader(fs);
            }
            catch (Exception ex)
            {

                //KryptonMessageBox.Show(null,"Caminho do logotipo não encontrado ou arquivo não encontrado! " + ex.Message,"ERRO DE IMPRESSÃO DE DANFE", MessageBoxButtons.OK, MessageBoxIcon.Error);

                br = null;
            }

            if (br != null)
            {
                return (br.ReadBytes(Convert.ToInt32(br.BaseStream.Length)));
            }
            else
            {
                return null;
            }


        }
        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            Image returnImage = null;
            if (byteArrayIn != null)
            {
                MemoryStream ms = new MemoryStream(byteArrayIn);
                returnImage = Image.FromStream(ms);
            }
            return returnImage;
        }

        public static Byte[] SalvaCodBarras(string sValor)
        {
            BarcodeLib.Barcode barcod = new BarcodeLib.Barcode(sValor, BarcodeLib.TYPE.CODE128C);
            barcod.Encode(BarcodeLib.TYPE.CODE128, sValor, 300, 150);
            Globais LeRegWin = new Globais();
            string sCaminho = "";


            sCaminho = belStaticPastas.CBARRAS + "\\Barras_" + sValor + ".JPG";

            barcod.SaveImage(@sCaminho, BarcodeLib.SaveTypes.JPG);

            return carregaImagem(sCaminho);
        }

        public static bool ValidaCean13(string CodigoEAN)
        {
            try
            {
                bool result = (CodigoEAN.Length == 8 || CodigoEAN.Length == 12 || CodigoEAN.Length == 13 || CodigoEAN.Length == 14);
                if (result)
                {
                    string checkSum = "";

                    if (CodigoEAN.Length == 8)
                    {
                        checkSum = "3131313";
                    }
                    else if (CodigoEAN.Length == 12)
                    {
                        checkSum = "31313131313";
                    }
                    else if (CodigoEAN.Length == 13)
                    {
                        checkSum = "131313131313";
                    }
                    else if (CodigoEAN.Length == 14)
                    {
                        checkSum = "3131313131313";
                    }

                    int digito =
                    int.Parse(CodigoEAN[CodigoEAN.Length - 1].ToString());
                    string ean = CodigoEAN.Substring(0, CodigoEAN.Length - 1);

                    int sum = 0;
                    for (int i = 0; i <= ean.Length - 1; i++)
                    {
                        sum += int.Parse(ean[i].ToString()) *
                        int.Parse(checkSum[i].ToString());
                    }
                    int calculo = 10 - (sum % 10);
                    if (calculo == 10)
                    {
                        calculo = 0;
                    }

                    result = (digito == calculo);
                }
                else if (CodigoEAN.Length == 0)
                {
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static bool ValidaPastaExistente(string sCaminho)
        {

            try
            {
                DirectoryInfo dinfo = new DirectoryInfo(sCaminho);
                if (dinfo.Exists)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
