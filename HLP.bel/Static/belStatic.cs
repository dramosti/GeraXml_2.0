using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLP.bel.Static
{
    public class belStatic
    {
        public static string sNomeEmpresa { get; set; }

        public static string psNM_Banco { get; set; }

        public static string RAMO = "";

        public static string sTipoIndustrializacao { get; set; }

        public static string sFuso { get; set; }

        private static string _sVersaoAtual;

        public static string sVersaoAtual
        {
            get { return belStatic._sVersaoAtual; }
            set { belStatic._sVersaoAtual = value; }
        }

        private static bool bSemArquivo;
        public static bool BSemArquivo
        {
            get { return belStatic.bSemArquivo; }
            set { belStatic.bSemArquivo = value; }
        }

        private static bool bAlteraDadosNfe;
        public static bool BAlteraDadosNfe
        {
            get { return belStatic.bAlteraDadosNfe; }
            set { belStatic.bAlteraDadosNfe = value; }
        }

        /// <summary>       
        /// 0 - Fecha Form
        /// </summary>
        private static string _pasta_xmls_Config;
        public static string Pasta_xmls_Configs
        {
            get { return belStatic._pasta_xmls_Config; }
            set
            {
                if (!value.ToString()[value.Length - 1].ToString().Equals(@"\"))
                {
                    value = value + "\\";
                }
                belStatic._pasta_xmls_Config = value;
            }
        }

        /// <summary>
        /// 1 - Fecha Tudo
        /// 0 - Fecha Form
        /// </summary>
        private static string sUsuario;
        public static string SUsuario
        {
            get { return belStatic.sUsuario; }
            set { belStatic.sUsuario = value; }
        }

        private static int iPrimeiroLoad;
        public static int IPrimeiroLoad
        {
            get { return belStatic.iPrimeiroLoad; }
            set { belStatic.iPrimeiroLoad = value; }
        }

        public static string sConfig;
        protected static string SConfig
        {
            get { return sConfig; }
            set { sConfig = value; }
        }
        
        public static string sNomeEmpresaCompleto { get; set; }

        public static string sEmailContador { get; set; }

        public static string codEmpresaNFe = "";
        protected static string CodEmpresaNFe
        {
            get { return codEmpresaNFe; }
            set { codEmpresaNFe = value; }
        }

        public static bool bModoSCAN = false;
        protected static bool BModoSCAN
        {
            get { return bModoSCAN; }
            set { bModoSCAN = value; }
        }

        public static bool bModoContingencia = false;
        protected static bool BModoContingencia
        {
            get { return bModoContingencia; }
            set { bModoContingencia = value; }
        }

        public static int iSerieSCAN = 0;
        /// <summary>
        /// 1 = Normal || 2 = Contingencia || 3 = SCAN
        /// </summary>
        protected static int ISerieSCAN
        {
            get { return iSerieSCAN; }
            set { iSerieSCAN = value; }
        }

        public static int iStatusAtualSistema = 0;
        protected static int IStatusAtualSistema
        {
            get { return iStatusAtualSistema; }
            set { iStatusAtualSistema = value; }
        }

        public static bool bNotaServico = false;
        protected static bool BNotaServico
        {
            get { return bNotaServico; }
            set { bNotaServico = value; }
        }

        /// <summary>
        /// 1 – Produção / 2 - Homologação
        /// </summary>
        public static int tpAmb;
        public static int TpAmb
        {
            get { return tpAmb; }
            set { tpAmb = value; }
        }

        /// <summary>
        /// 1 – Produção / 2 - Homologação
        /// </summary>
        public static int tpAmbNFse;
        protected static int TpAmbNFse
        {
            get { return tpAmbNFse; }
            set { tpAmbNFse = value; }
        }

        public static int cUF { get; set; }
        public static string sUF { get; set; }

        public static string CNPJ_Empresa { get; set; }

        public static bool bDentroHlp;
        protected static bool BDentroHlp
        {
            get { return bDentroHlp; }
            set { bDentroHlp = value; }
        }


        public static string sNmCidadeEmpresa { get; set; }

        public static bool bClickOnce { get; set; }

        private static string _codEmpresaCte;
        public static string CodEmpresaCte
        {
            get { return belStatic._codEmpresaCte; }
            set { belStatic._codEmpresaCte = value; }
        }




        private static string _sigla_uf;
        public static string Sigla_uf
        {
            get { return belStatic._sigla_uf; }
            set { belStatic._sigla_uf = value; }
        }


    }
}
