using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLP.bel.NFes
{
    /// <summary>
    /// Representa dados para identificação do tomador de serviço
    /// </summary>
    public class tcIdentificacaoTomador
    {
        /// <summary>
        /// </summary>
        private string _inscricaoMunicipal = "";

        /// <summary>
        /// (0-1)S-15
        /// </summary>
        public string InscricaoMunicipal
        {
            get { return _inscricaoMunicipal; }
            set { _inscricaoMunicipal = Util.Util.TiraSimbolo(value,""); }
        }

        /// <summary>
        /// (0-1)
        /// </summary>
        public TcCpfCnpj CpfCnpj { get; set; }

    }
}
