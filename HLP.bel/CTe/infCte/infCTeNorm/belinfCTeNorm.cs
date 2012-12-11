using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLP.bel.CTe
{
    public class belinfCTeNorm
    {
        /// <summary>
        /// 1:1
        /// </summary>
        public belinfCarga infCarga { get; set; }

        /// <summary>
        /// 0:N
        /// </summary>
        public List<belcontQt> contQt { get; set; }

        /// <summary>
        /// 0:1
        /// </summary>
        public beldocAnt docAnt { get; set; }

        /// <summary>
        /// 0:N
        /// </summary>
        public List<belseg> Lseg { get; set; }

        public belseg seg { get; set; }

        /// <summary>
        /// 1:1
        /// </summary>
        public belrodo rodo { get; set; }

        /// <summary>
        /// 1:1
        /// </summary>
        public belaereo aereo { get; set; }

        /// <summary>
        /// 1:1
        /// </summary>
        public belaquav aquav { get; set; }

        /// <summary>
        /// 1:1
        /// </summary>
        public belferrov ferrov { get; set; }










    }
}
