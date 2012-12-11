using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLP.bel.CTe
{
    public class belferrov
    {
        /// <summary>
        /// 1:1 N TAMANHO 1
        /// 0-Próprio, 1-Mútuo, 2- rodoferroviário ou 3-rodoviário.
        /// </summary>
        public int tpTraf { get; set; }

        private string _fluxo = "";
        /// <summary>
        /// 1:1 C TAMANHO 1-10
        /// </summary>
        public string fluxo
        {
            get { return _fluxo; }
            set { _fluxo = value; }
        }

        private string _idTrem = "";
        /// <summary>
        /// 0:1 C TAMANHO 1-7
        /// </summary>
        public string idTrem
        {
            get { return _idTrem; }
            set { _idTrem = value; }
        }

        /// <summary>
        /// 1:1 N TAMANHO 13,2
        /// </summary>
        public decimal vFrete { get; set; }

        /// <summary>
        /// 0:1
        /// </summary>
        public belferroSub ferroSub { get; set; }

        /// <summary>
        /// 0:N
        /// </summary>
        public List<belDCL> DCL { get; set; }

        /// <summary>
        /// 1:N
        /// </summary>
        public List<beldetVag> detVag { get; set; }



    }
}

