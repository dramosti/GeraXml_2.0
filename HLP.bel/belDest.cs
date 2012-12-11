using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLP.bel
{
    //Danner - o.s. 23722 - 30/09/2009
    public class belDest
    {
        /// <summary>
        /// Informar o CNPJ ou o CPF do destinatário, preenchendo os zeros não significativos. Não informar o conteúdo da TAG se a operação for realizada com o exterior.
        /// </summary>
        private string _cnpj;

        public string Cnpj
        {
            get { return _cnpj; }
            set { _cnpj = Util.Util.TiraSimbolo(value,""); }
        }
        /// <summary>
        /// Informar o CNPJ ou o CPF do destinatário, preenchendo os zeros não significativos. Não informar o conteúdo da TAG se a operação for realizada com o exterior.
        /// </summary>
        private string _cpf;

        public string Cpf
        {
            get { return _cpf; }
            set { _cpf = Util.Util.TiraSimbolo(value,""); }
        }
        /// <summary>
        /// Razão Social ou nome do Destinatario
        /// </summary>
        private string _xnome;

        public string Xnome
        {
            get { return _xnome; }
            set { _xnome = value; }
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
        /// Numero da Residência ou Estabelecimento
        /// </summary>
        private string _nro;

        public string Nro
        {
            get { return _nro; }
            set { _nro = value; }
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
        /// Código do Município
        /// </summary>
        private string _cmun;

        public string Cmun
        {
            get { return _cmun; }
            set { _cmun = value; }
        }
        /// <summary>
        /// Nome do Município
        /// </summary>
        private string _xmun;

        public string Xmun
        {
            get { return _xmun; }
            set { _xmun = value; }
        }
        /// <summary>
        /// Nome do Uf
        /// </summary>
        private string _uf;

        public string Uf
        {
            get { return _uf; }
            set { _uf = value; }
        }
        /// <summary>
        /// CEP
        /// </summary>
        private string _cep;

        public string Cep
        {
            get { return _cep; }
            set { _cep = Util.Util.TiraSimbolo(value,""); }
        }
        /// <summary>
        /// Código do Pais
        /// </summary>
        private string _cpais;

        public string Cpais
        {
            get { return _cpais; }
            set { _cpais = value; }
        }
        /// <summary>
        /// Nome do País
        /// </summary>
        private string _xpais;

        public string Xpais
        {
            get { return _xpais; }
            set { _xpais = value; }
        }
        /// <summary>
        /// Telefone do Destinatário
        /// </summary>
        private string _fone;

        public string Fone
        {
            get { return _fone; }
            set { _fone = value; }
        }
        /// <summary>
        /// Inscrição Estadual, caso pessoa fisica deixar tag em brando ex.  <IE/>
        /// </summary>
        private string _ie;

        public string Ie
        {
            get { return _ie; }
            set { _ie = Util.Util.TiraSimbolo(value,""); }
        }
        /// <summary>
        /// Inscrição do SUFRAMA, caso pessoa fisica deixar  a tag em branco ex. <IE/>
        /// </summary>
        private string _isuf;

        public string Isuf
        {
            get { return _isuf; }
            set { _isuf = Util.Util.TiraSimbolo(value,""); }
        }

        public string email { get; set; }
    }
    //Danner - Fim 30/09/2009
}
