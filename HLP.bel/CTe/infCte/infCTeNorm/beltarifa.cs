using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLP.bel.CTe
{
    public class beltarifa
    {
        private string _trecho = "";
        /// <summary>
        /// 0:1 C TAMANHO 1-7
        /// </summary>
        public string trecho
        {
            get { return _trecho; }
            set { _trecho = value; }
        }

        private string _CL = "";
        /// <summary>
        /// 0:1 C TAMANHO 1-2
        /// </summary>
        public string CL
        {
            get { return _CL; }
            set { _CL = value; }
        }

        private string _cTar = "";
        /// <summary>
        /// 0:1 C TAMANHO 1-4
        /// </summary>
        public string cTar
        {
            get { return _cTar; }
            set { _cTar = value; }
        }

        /// <summary>
        /// 0:1 N TAMANHO 13,2
        /// </summary>
        public int vTar { get; set; }

    }
}
