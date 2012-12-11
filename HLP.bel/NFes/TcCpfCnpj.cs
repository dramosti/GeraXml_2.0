using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLP.bel.NFes
{
    /// <summary>
    /// Número de CPF ou CNPJ
    /// </summary>
    public class TcCpfCnpj
    {
        /// <summary>
        /// </summary>
        private string _cnpj = "";

        /// <summary>
        /// (1-1) N-14
        /// </summary>
        public string Cnpj
        {
            get { return _cnpj; }
            set { _cnpj = Util.Util.TiraSimbolo(value, ""); }
        }
        /// <summary>
        /// </summary>
        private string _cpf = "";

        /// <summary>
        /// (1-1) N-11
        /// </summary>
        public string Cpf
        {
            get { return _cpf; }
            set { _cpf = Util.Util.TiraSimbolo(value, ""); }
        }
    }
}
