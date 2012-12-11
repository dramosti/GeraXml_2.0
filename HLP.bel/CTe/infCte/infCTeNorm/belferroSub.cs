using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLP.bel.CTe
{
    public class belferroSub
    {
        /// <summary>
        /// 1:1 N TAMANHO 14
        /// </summary>
        public int CNPJ { get; set; }

        private string _cInt = "";
        /// <summary>
        /// 0:1 C TAMANHO 1-10
        /// </summary>
        public string cInt
        {
            get { return _cInt; }
            set { _cInt = value; }
        }

        private string _IE = "";
        /// <summary>
        /// 0:1 C TAMANHO 2-14
        /// </summary>
        public string IE
        {
            get { return _IE; }
            set { _IE = value; }
        }

        private string _xNome = "";
        /// <summary>
        /// 1:1 C TAMANHO 1-60
        /// </summary>
        public string xNome
        {
            get { return _xNome; }
            set { _xNome = value; }
        }

        /// <summary>
        /// 1:1
        /// </summary>
        public belenderFerro enderFerro { get; set; }



    }
}
