﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLP.bel
{
    public class belIcms30
    {
        /// <summary>
        /// Origem da Mercadoria; 0 - Nacional; 1 - Estrangeira, Importação direta; 2 - Estrangeira, Adquirida no merdado interno.
        /// </summary>
        private string _orig;

        public string Orig
        {
            get { return _orig; }
            set { _orig = value; }
        }

        /// <summary>
        /// Tributação do ICMS; 00 - Atribuida Integralmente.
        /// </summary>
        private string _cst;

        public string Cst
        {
            get { return _cst; }
            set { _cst = value; }
        }

        /// <summary>
        /// Modalidade de determinação da BC do ICMAS ST; 0 - Preço tabelado ou máximo sugerido; 1 - Lsta Negativa (valor); 2 - Lista Positiva (valor); Lista Neutra (valor); 4 - Margem Valor agregado (%); 5 - Pauta (valor).
        /// </summary>
        private decimal _modbcst;

        public decimal Modbcst
        {
            get { return _modbcst; }
            set { _modbcst = value; }
        }

        /// <summary>
        /// Percentual da margem de valor adicionado do ICMS ST;
        /// </summary>
        private decimal _pmvast;

        public decimal Pmvast
        {
            get { return _pmvast; }
            set { _pmvast = value; }
        }

        /// <summary>
        /// Precentual da Redução de BC do  ICMS ST;
        /// </summary>
        private decimal _predbcst;

        public decimal Predbcst
        {
            get { return _predbcst; }
            set { _predbcst = value; }
        }

        /// <summary>
        /// Valor da BC do ICMS ST.
        /// </summary>
        private decimal _vbcst;

        public decimal Vbcst
        {
            get { return _vbcst; }
            set { _vbcst = value; }
        }

        /// <summary>
        /// Alíquita do imposto do ICMS ST.
        /// </summary>
        private decimal _picmsst;

        public decimal Picmsst
        {
            get { return _picmsst; }
            set { _picmsst = value; }
        }

        /// <summary>
        /// Valor do ICMS ST
        /// </summary>
        private decimal _vicmsst;

        public decimal Vicmsst
        {
            get { return _vicmsst; }
            set { _vicmsst = value; }
        }
    }
}
