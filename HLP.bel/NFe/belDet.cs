using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLP.bel
{
    public class belDet
    {
        /// <summary>
        /// </summary>
        private belProd _belProd;
        private belImposto _belImposto;
        private belInfadprod _belInfadprod;
        /// <summary>
        /// Atributo referente ao index do produto
        /// </summary>
        private decimal _nitem;

        public decimal Nitem
        {
            get { return _nitem; }
            set { _nitem = value; }
        }

        /// <summary>
        /// QUANDO FOR MÃO DE OBRA O CAMPO ESTARÁ PREENCHIDO COM A LETRA “ M ”
        /// QUANDO FOR INSUMOS O CAMPO ESTARÁ PREENCHIDO COM A LETRA “ I ”
        /// </summary>
        public string tp_industrializacao { get; set; }

        public belProd belProd
        {
            get
            {
                return _belProd;
            }
            set
            {
                _belProd = value;
            }
        }

        public belImposto belImposto
        {
            get
            {
                return _belImposto;
            }
            set
            {
                _belImposto = value;
            }
        }

        public belInfadprod belInfadprod
        {
            get
            {
                return _belInfadprod;
            }
            set
            {
                _belInfadprod = value;
            }
        }
    }
}
