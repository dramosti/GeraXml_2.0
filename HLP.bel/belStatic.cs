using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLP.bel
{
    public class belStatic
    {

        public static string sConfig;
      
        protected static string SConfig
        {
            get { return sConfig; }
            set { sConfig = value; }
        }

        public static string sEmpresa = "";

        protected static string SEmpresa
        {
            get { return sEmpresa; }
            set { sEmpresa = value; }
        }


    }
}
