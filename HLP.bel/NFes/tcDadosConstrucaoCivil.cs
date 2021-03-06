﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLP.bel.NFes
{
    /// <summary>
    /// Representa dados para identificação de construção civil
    /// </summary>
   public class tcDadosConstrucaoCivil
    {
        /// <summary>
        /// </summary>
       private string _codigoObra = "";

        /// <summary>
        /// (1-1)S-15
        /// </summary>
        public string CodigoObra
        {
            get { return _codigoObra; }
            set { _codigoObra = Util.Util.ValidaTamanhoMaximo(15, value); }
        }
        /// <summary>
        /// </summary>
        private string _art = "";

        /// <summary>
        /// (1-1)S-15
        /// </summary>
        public string Art
        {
            get { return _art; }
            set { _art = Util.Util.ValidaTamanhoMaximo(15, value); }
        }
    }
}
