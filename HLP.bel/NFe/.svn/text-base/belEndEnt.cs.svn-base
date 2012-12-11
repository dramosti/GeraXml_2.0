using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLP.bel
{
    public class belEndEnt
    {
        /// <summary>
        /// CNPJ, informar os zeros não signifacitvos
        /// </summary>
        private string _cnpj;

        public string Cnpj
        {
            get { return _cnpj; }
            set { _cnpj = Util.Util.TiraSimbolo(value,""); }
        }
        /// <summary>
        /// Logradouro
        /// </summary>
        private string _xlgr;

        public string Xlgr
        {
            get { return _xlgr; }
            set { _xlgr = Util.Util.TiraSimbolo(value,""); }
        }
        /// <summary>
        /// Numero do Estabelecimento
        /// </summary>
        private string _nro;

        public string Nro
        {
            get { return _nro; }
            set { _nro = Util.Util.TiraSimbolo(value,""); }
        }
        /// <summary>
        /// Complemento
        /// </summary>
        private string _xcpl;

        public string Xcpl
        {
            get { return _xcpl; }
            set { _xcpl = value; }
        }
        /// <summary>
        /// Nome do Bairro
        /// </summary>
        private string _xbairro;

        public string Xbairro
        {
            get { return _xbairro; }
            set { _xbairro = Util.Util.TiraSimbolo(value,""); }
        }
        /// <summary>
        /// Código do município, Utilizar a Tabela do IBGE (Anexo IV - Tabela de UF, Município e País).
        /// </summary>
        private string _cmun;

        public string Cmun
        {
            get { return _cmun; }
            set { _cmun = Util.Util.TiraSimbolo(value,""); }
        }
        /// <summary>
        /// Nome do Município, Informar 'EXTERIOR' para operações com o exterior
        /// </summary>
        private string _xmun;

        public string Xmun
        {
            get { return _xmun; }
            set { _xmun = Util.Util.TiraSimbolo(value,""); }
        }
        /// <summary>
        /// UF, Informar 'EX' para operações com o exterior
        /// </summary>
        private string _uf;

        public string Uf
        {
            get { return _uf; }
            set { _uf = Util.Util.TiraSimbolo(value,""); }
        }
    }
}
