using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLP.bel.CTe
{
    public class belcompl
    {
        private string _xCaracAd = "";
        /// <summary>
        /// 0:1 C TAMANHO  1-15
        /// </summary>
        /// 
        public string xCaracAd
        {
            get { return _xCaracAd; }
            set { _xCaracAd = value; }
        }


        private string _xCaracSer = "";
        /// <summary>
        /// 0:1 C TAMANHO  1-30
        /// </summary>
        public string xCaracSer
        {
            get { return _xCaracSer; }
            set { _xCaracSer = value; }
        }
        
        private string _xEmi = "";
        /// <summary>
        /// 0:1 C TAMANHO  1-20
        /// </summary>
        public string xEmi
        {
            get { return _xEmi; }
            set { _xEmi = value; }
        }

        /// <summary>
        /// 0:1  
        /// </summary>
        public belfluxo fluxo { get; set; }

        /// <summary>
        /// 0:1  
        /// </summary>
        public belEntrega entrega { get; set; }

        /// <summary>
        /// 0:10  
        /// </summary>
        public belObsCont ObsCont = new belObsCont();

        private string _origCalc = "";
        /// <summary>
        /// 0:1 C TAMANHO 1-40  
        /// </summary>
        public string origCalc
        {
            get { return _origCalc; }
            set { _origCalc = value; }
        }

        private string _destCalc = "";
        /// <summary>
        /// 0:1 C TAMANHO 1-40  
        /// </summary>
        public string destCalc
        {
            get { return _destCalc; }
            set { _destCalc = value; }
        }

        private string _xObs = "";
        /// <summary>
        /// 0:1 C TAMANHO 1-2000
        /// </summary>
        public string xObs
        {
            get { return _xObs; }
            set { _xObs = value; }
        }



    }
}
