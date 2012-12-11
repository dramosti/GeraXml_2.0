using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HLP.bel.Static;
using System.Xml;
using System.IO;

namespace HLP.bel.NFe.GeraXml
{
    public class Globais
    {
        public string LeRegConfig(string NomeChave)
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

        public void CarregaInfStaticas()
        {
            try
            {
                string sCaminhoPadrao = LeRegConfig("CaminhoPadrao");

                if (sCaminhoPadrao != "" && belUtil.ValidaPastaExistente(sCaminhoPadrao))
                {
                    belStaticPastas.CAMINHO = sCaminhoPadrao;
                }
                else
                {
                    belStaticPastas.CANCELADOS = LeRegConfig("PastaXmlCancelados");
                    belStaticPastas.CONTINGENCIA = LeRegConfig("PastaContingencia");
                    belStaticPastas.ENVIADOS = LeRegConfig("PastaXmlEnviado");
                    belStaticPastas.ENVIO = LeRegConfig("PastaXmlEnvio");
                    belStaticPastas.PROTOCOLOS = LeRegConfig("PastaProtocolos");
                    belStaticPastas.CBARRAS = LeRegConfig("CodBarras");
                    belStaticPastas.RETORNO = LeRegConfig("PastaXmlRetorno");
                    belStaticPastas.CCe = LeRegConfig("PastaCCe");
                }

                string sFuso = LeRegConfig("Fuso");
                belStatic.sFuso = (sFuso != "" ? sFuso : "-02:00");
            }
            catch (Exception ex)
            {

                throw new Exception(string.Format("Erro ao tentar abrir o xml de configuração do sistema.: {0}",
                                    ex.Message));
            }
        }
    }
}
