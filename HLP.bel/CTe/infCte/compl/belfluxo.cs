using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLP.bel.CTe
{
    public class belfluxo
    {
        private string _xOrig = "";
        /// <summary>
        /// 0:1 C TAMANHO  1-15
        /// </summary>
        public string xOrig
        {
            get { return _xOrig; }
            set { _xOrig = value; }
        }

        /// <summary>
        /// 0:N
        /// </summary>
        public  List<belpass> pass { get; set; }

        private string _xDest = "";
        /// <summary>
        /// 0:1 C TAMANHO  1-15
        /// </summary>
        public string xDest
        {
            get { return _xDest; }
            set { _xDest = value; }
        }

        private string _xRota = "";
        /// <summary>
        /// 0:1 C TAMANHO  1-10
        /// </summary>
        public string xRota
        {
            get { return _xRota; }
            set { _xRota = value; }
        }
    }
}
