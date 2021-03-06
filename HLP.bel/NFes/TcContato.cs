﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLP.bel.NFes
{
    /// <summary>
    /// Representa forma de contato com a pessoa (física/jurídica)
    /// </summary>
    public class TcContato
    {
        /// <summary>
        /// </summary>
        private string _telefone = "";

        /// <summary>
        /// (0-1)S-10
        /// </summary>
        public string Telefone
        {
            get { return _telefone; }
            set { _telefone = (Util.Util.ValidaTamanhoMaximo(10, Util.Util.TiraSimbolo(value, "").Replace(" ", ""))); }
        }
        /// <summary>
        /// </summary>
        private string _email = "";

        /// <summary>
        /// (0-1)S-80
        /// </summary>
        public string Email
        {
            get { return _email; }
            set { _email = Util.Util.ValidaTamanhoMaximo(80, value); }
        }
    }
}
