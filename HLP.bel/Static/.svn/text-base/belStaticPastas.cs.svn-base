using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HLP.bel
{
    public static class belStaticPastas
    {
        public static string Pasta_StartupPath { get; set; }

        public static string CAMINHO = "";

        private static string _CANCELADOS = "Cancelados\\";
        public static string CANCELADOS
        {
            get
            {
                string sCaminho = (CAMINHO != "" ? CAMINHO + "\\" : "") + belStaticPastas._CANCELADOS;
                DirectoryInfo info = new DirectoryInfo(sCaminho);
                if (!info.Exists)
                {
                    info.Create();
                }
                return sCaminho.Trim();
            }
            set { belStaticPastas._CANCELADOS = value; }
        }

        private static string _CONTINGENCIA = "Contingencia\\";
        public static string CONTINGENCIA
        {
            get
            {
                string sCaminho = (CAMINHO != "" ? CAMINHO + "\\" : "") + belStaticPastas._CONTINGENCIA;
                DirectoryInfo info = new DirectoryInfo(sCaminho);
                if (!info.Exists)
                {
                    info.Create();
                }
                return sCaminho.Trim();
            }
            set { belStaticPastas._CONTINGENCIA = value; }
        }

        private static string _ENVIADOS = "Enviados\\";
        public static string ENVIADOS
        {
            get
            {
                string sCaminho = (CAMINHO != "" ? CAMINHO + "\\" : "") + belStaticPastas._ENVIADOS;
                DirectoryInfo info = new DirectoryInfo(sCaminho);
                if (!info.Exists)
                {
                    info.Create();
                }
                return sCaminho.Trim();
            }
            set { belStaticPastas._ENVIADOS = value; }
        }

        private static string _ENVIO = "Envio\\";
        public static string ENVIO
        {
            get
            {
                string sCaminho = (CAMINHO != "" ? CAMINHO + "\\" : "") + belStaticPastas._ENVIO;
                DirectoryInfo info = new DirectoryInfo(sCaminho);
                if (!info.Exists)
                {
                    info.Create();
                }
                return sCaminho.Trim();
            }
            set { belStaticPastas._ENVIO = value; }
        }

        private static string _PROTOCOLOS = "Protocolos\\";
        public static string PROTOCOLOS
        {
            get
            {
                string sCaminho = (CAMINHO != "" ? CAMINHO + "\\" : "") + belStaticPastas._PROTOCOLOS;
                DirectoryInfo info = new DirectoryInfo(sCaminho);
                if (!info.Exists)
                {
                    info.Create();
                }
                return sCaminho.Trim();
            }
            set { belStaticPastas._PROTOCOLOS = value; }
        }

        private static string _CBARRAS = "CBarras\\";
        public static string CBARRAS
        {
            get
            {
                string sCaminho = (CAMINHO != "" ? CAMINHO + "\\" : "") + belStaticPastas._CBARRAS;
                DirectoryInfo info = new DirectoryInfo(sCaminho);
                if (!info.Exists)
                {
                    info.Create();
                }
                return sCaminho.Trim();
            }
            set { belStaticPastas._CBARRAS = value; }
        }

        private static string _RETORNO = "Retorno\\";
        public static string RETORNO
        {
            get
            {
                string sCaminho = (CAMINHO != "" ? CAMINHO + "\\" : "") + belStaticPastas._RETORNO;
                DirectoryInfo info = new DirectoryInfo(sCaminho);
                if (!info.Exists)
                {
                    info.Create();
                }
                return sCaminho.Trim();
            }
            set { belStaticPastas._RETORNO = value; }
        }

        private static string _CCe = "CCe\\";
        public static string CCe
        {
            get
            {
                try
                {
                    string sCaminho = (CAMINHO != "" ? CAMINHO + "\\" : "") + belStaticPastas._CCe;
                    DirectoryInfo info = new DirectoryInfo(sCaminho);
                    if (!info.Exists)
                    {
                        info.Create();
                    }
                    return sCaminho.Trim();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            set { belStaticPastas._CCe = value; }
        }

        public static string SCHEMA_NFE { get { return Pasta_StartupPath + "\\Schema\\NFe\\"; } }
        public static string SCHEMA_NFSE { get { return Pasta_StartupPath + "\\Schema\\NFe-s\\"; } }
        public static string SCHEMA_CCE { get { return Pasta_StartupPath + "\\Schema\\CCe\\"; } }
        public static string SCHEMA_CTE { get { return Pasta_StartupPath + "\\Schema\\CTe\\"; } }


    }
}
