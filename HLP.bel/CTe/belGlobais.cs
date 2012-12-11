using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using HLP.bel.Static;

namespace HLP.bel.CTe
{
    public class belGlobais
    {

        public string LeRegWin(string NomeChave)
        {
            string Retorno = "";
            try
            {
                string path = belStatic.Pasta_xmls_Configs + belStatic.sConfig;

                if (File.Exists(path))
                {
                    XmlTextReader reader = new XmlTextReader(path);
                    while (reader.Read())
                    {
                        if ((reader.NodeType != XmlNodeType.Element) || !(reader.Name == "nfe_configuracoes"))
                        {
                            continue;
                        }
                        while (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Element)
                            {
                                if (reader.Name == NomeChave)
                                {
                                    reader.Read();
                                    Retorno = reader.Value;
                                    continue;
                                }
                            }
                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro ao tentar abrir o xml de configuração do sistema.: {0}",
                                      ex.Message));
            }

            return Retorno;
        }
    }
}
