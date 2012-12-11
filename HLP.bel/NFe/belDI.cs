using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLP.bel
{
    public class belDI
    {
        /// <summary>
        /// 1-10 Número do Documento de Importação (DI/DSI/DA)
        /// </summary>
        public string _nDI;

        public string nDI
        {
            get { return _nDI; }
            set { _nDI = value.ToUpper(); }
        }

        /// <summary>
        /// Data de Registro da DI/DSI/DA / Formato “AAAA-MM-DD”
        /// </summary>
        public DateTime DDI { get; set; }
        
        /// <summary>
        /// 1-60 Local de desembaraço
        /// </summary>
        public string _xLocDesemb;

        public string xLocDesemb
        {
            get { return _xLocDesemb; }
            set { _xLocDesemb = value.ToUpper(); }
        }

        /// <summary>
        /// 2 Sigla da UF onde ocorreu o Desembaraço Aduaneiro
        /// </summary>
        private string _uFDesemb ;

        public string UFDesemb
        {
            get { return _uFDesemb; }
            set { _uFDesemb = value.ToUpper(); }
        }
        /// <summary>
        /// Data do Desembaraço Aduaneiro / Formato “AAAA-MM-DD”
        /// </summary>
        public DateTime dDesemb { get; set; }
        /// <summary>
        /// 1-60 Código do exportador
        /// </summary>
        private string _cExportador;

        public string cExportador
        {
            get { return _cExportador; }
            set { _cExportador = value.ToUpper(); }
        }
        
        public List<beladi> adi { get; set; }

    }
}
