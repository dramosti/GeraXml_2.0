using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLP.bel.CTe
{
    public class belEntrega
    {
        /// <summary>
        /// 1:1
        /// </summary>
        public belsemData semData { get; set; }

        /// <summary>
        /// 1:1 
        /// </summary>
        public belcomData comData { get; set; }

        /// <summary>
        /// 1:1  
        /// </summary>
        public belnoPeriodo noPeriodo { get; set; }

        /// <summary>
        /// 1:1  
        /// </summary>
        public belcomData comHora { get; set; }

        /// <summary>
        /// 1:1  
        /// </summary>
        public belnoInter noInter { get; set; }


    }
    
}
