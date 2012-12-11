using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLP.bel
{

    public class belIcms60
    {
        /// <summary>
        /// Tributação do ICMS; 00 - Atribuida Integralmente.
        /// </summary>
        private string _cst;

        /// <summary>
        /// Origem da Mercadoria; 0 - Nacional; 1 - Estrangeira, Importação direta; 2 - Estrangeira, Adquirida no merdado interno.
        /// </summary>
        private string _orig;

        /// <summary>
        /// Valor da BC do ICMS ST.
        /// </summary>
        private decimal _vbcst;

        /// <summary>
        /// Valor do ICMS ST
        /// </summary>
        private decimal _vicmsst;

        public string Cst
        {
            get { return _cst; }
            set { _cst = value; }
        }

        public string Orig
        {
            get { return _orig; }
            set { _orig = value; }
        }

        public decimal Vbcst
        {
            get { return _vbcst; }
            set { _vbcst = value; }
        }

        public decimal Vicmsst
        {
            get { return _vicmsst; }
            set { _vicmsst = value; }
        }
    }
}
