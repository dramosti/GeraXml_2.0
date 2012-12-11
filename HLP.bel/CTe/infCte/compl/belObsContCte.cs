using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLP.bel.CTe
{
    public class belObsContCte
    {
        private string _xCampo = "";
        /// <summary>
        /// 1:1 C TAMANHO 1-20  
        /// </summary>
        public string xCampo
        {
            get { return _xCampo; }
            set { _xCampo = value; }
        }

        private string _xTexto = "";
        /// <summary>
        /// 1:1 C TAMANHO 1-160  
        /// </summary>
        public string xTexto
        {
            get { return _xTexto; }
            set { _xTexto = value; }
        }
    }
}
