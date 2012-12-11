using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLP.bel.CTe
{
    public class belaereo
    {
        /// <summary>
        /// 0:1 N TAMANHO 9 
        /// </summary>
        public int nMinu { get; set; }

        /// <summary>
        /// 1:1 N TAMANHO 14 
        /// </summary>
        public int nOCA { get; set; }

        /// <summary>
        /// 1:1 D TAMANHO 10 
        /// </summary>
        public DateTime  dPrev { get; set; }

    
        private string _xLAgEmi = "";
        /// <summary>
        /// 0:1 C TAMANHO 1-20 
        /// </summary>
        public string xLAgEmi
        {
            get { return _xLAgEmi; }
            set { _xLAgEmi = value; }
        }

        
        private string _cIATA = "";
        /// <summary>
        /// 0:1 C TAMANHO 1-14
        /// </summary>
        public string cIATA
        {
            get { return _cIATA; }
            set { _cIATA = value; }
        }

        /// <summary>
        /// 1:1
        /// </summary>
        public beltarifa tarifa { get; set; }

    }
}
