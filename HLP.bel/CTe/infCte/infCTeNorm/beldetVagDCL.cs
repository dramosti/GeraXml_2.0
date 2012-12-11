using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLP.bel.CTe
{
    public class beldetVagDCL
    {
        /// <summary>
        /// 1:1 N TAMANHO 8
        /// </summary>
        public int nVag { get; set; }

        /// <summary>
        /// 0:1 N TAMANHO 3,2
        /// </summary>
        public decimal cap { get; set; }

        private string _tpVag = "";
        /// <summary>
        /// 0:1 c TAMANHO 3 
        /// </summary>
        public string tpVag
        {
            get { return _tpVag; }
            set { _tpVag = value; }
        }

        /// <summary>
        /// 0:1 N TAMANHO 3,2
        /// </summary>
        public decimal pesoR { get; set; }

        /// <summary>
        /// 0:1 N TAMANHO 3,2
        /// </summary>
        public decimal pesoBC { get; set; }

        /// <summary>
        /// 0:N
        /// </summary>
        public List<bellacDetVagDCL> detVagDCL { get; set; }

        /// <summary>
        /// O:N
        /// </summary>
        public List<belcontDCL> contDCL { get; set; } 

    }
}
