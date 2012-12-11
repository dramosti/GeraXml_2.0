using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//Danner o.s. 23722 30/09/2009 --+ Criação da classe 
namespace HLP.bel
{
    public class belEmit
    {
        /// <summary>
        /// CNPJ do emitente
        /// </summary>
        private string _cnpj;

        public string Cnpj
        {
            get { return _cnpj; }
            set { _cnpj = Util.Util.TiraSimbolo(value, ""); }
        }
        /// <summary>
        /// CPF do emitente
        /// </summary>
        private string _cpf;

        public string Cpf
        {
            get { return _cpf; }
            set { _cpf = Util.Util.TiraSimbolo(value, ""); }
        }
        /// <summary>
        /// Razão Social ou Nome do emitente
        /// </summary>
        private string _xnome;

        public string Xnome
        {
            get { return _xnome; }
            set { _xnome = Util.Util.TiraSimbolo(value,""); }
        }
        /// <summary>
        /// Nome Fantazia
        /// </summary>
        private string _xfant;

        public string Xfant
        {
            get { return _xfant; }
            set { _xfant = value; }
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
        /// Numero da Residência
        /// </summary>
        private string _nro;

        public string Nro
        {
            get { return _nro; }
            set
            {
                _nro = value;
            //    bool ctrl = true;
            //    for (int i = 0; i < value.Length; i++)
            //    {
            //        if ((value[i] != '0') &&
            //            (value[i] != '1') &&
            //            (value[i] != '2') &&
            //            (value[i] != '3') &&
            //            (value[i] != '4') &&
            //            (value[i] != '5') &&
            //            (value[i] != '6') &&
            //            (value[i] != '7') &&
            //            (value[i] != '8') &&
            //            (value[i] != '9'))
            //        {
            //            ctrl = false;
            //            break;
            //        }

            //    }
            //    if (ctrl == true)
            //        _nro = value;
            //    else
            //        throw new Exception("Nro contém Letras ou Caracteres Especiais");
                    
            }
        }
        /// <summary>
        /// Complemento
        /// </summary>
        private string _xcpl;

        public string Xcpl
        {
            get { return _xcpl; }
            set { _xcpl = Util.Util.TiraSimbolo(value, ""); }
        }
        /// <summary>
        /// Bairro
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
            set
            {
                bool ctrl = true;
                for (int i = 0; i < value.Length; i++)
                {
                    if ((value[i] != '0') &&
                        (value[i] != '1') &&
                        (value[i] != '2') &&
                        (value[i] != '3') &&
                        (value[i] != '4') &&
                        (value[i] != '5') &&
                        (value[i] != '6') &&
                        (value[i] != '7') &&
                        (value[i] != '8') &&
                        (value[i] != '9'))
                    {
                        ctrl = false;
                        break;
                    }

                }
                if (ctrl == true)
                    _cmun = value;
                else
                    throw new Exception("Cmun contém Letras ou Caracteres Especiais");
            }
        }
        /// <summary>
        /// Nome do município
        /// </summary>
        private string _xmun;

        public string Xmun
        {
            get { return _xmun; }
            set { _xmun = Util.Util.TiraSimbolo(value,""); }
        }
        /// <summary>
        /// Sigla do UF
        /// </summary>
        private string _uf;

        public string Uf
        {
            get { return _uf; }
            set
            {
                if ((value.Length > 2) || (value == null) || (value == ""))
                    throw new Exception("UF com Valor Incorreto, Null ou Vazio");
                else
                    _uf = value;
            }
                    
        }
        /// <summary>
        /// Código do CEP
        /// </summary>
        private string _cep;

        public string Cep
        {
            get { return _cep; }
            set { _cep = Util.Util.TiraSimbolo(value,""); }
        }
        /// <summary>
        /// Codigo do pais - 1058 Brasil
        /// </summary>
        private string _cpais;

        public string Cpais
        {
            get { return _cpais; }
            set
            {
                bool ctrl = true;
                for (int i = 0; i < value.Length; i++)
                {
                    if ((value[i] != '0') &&
                        (value[i] != '1') &&
                        (value[i] != '2') &&
                        (value[i] != '3') &&
                        (value[i] != '4') &&
                        (value[i] != '5') &&
                        (value[i] != '6') &&
                        (value[i] != '7') &&
                        (value[i] != '8') &&
                        (value[i] != '9'))
                    {
                        ctrl = false;
                        break;
                    }

                }
                if (ctrl == true)
                    _cpais = value;
                else
                    throw new Exception("Cpais contém Letras ou Caracteres Especiais");

            }
        }
        /// <summary>
        /// Brasil ou BRASIL
        /// </summary>
        private string _xpais;

        public string Xpais
        {
            get { return _xpais; }
            set { _xpais = value; }
        }
        /// <summary>
        /// Telefone
        /// </summary>
        private string _fone;

        public string Fone
        {
            get { return _fone; }
            set { _fone = value; }
        }
        /// <summary>
        /// Inscrição Estadual
        /// </summary>
        private string _ie;

        public string Ie
        {
            get { return _ie; }
            set { _ie = Util.Util.TiraSimbolo(value,""); }
        }
        /// <summary>
        /// Inscrição Estadual  do Subistituto Tributario
        /// </summary>
        private string _iest;

        public string Iest
        {
            get { return _iest; }
            set { _iest = value; }
        }
        /// <summary>
        /// Inscrição Municipal
        /// </summary>
        private string _im;

        public string Im
        {
            get { return _im; }
            set { _im = value; }
        }
        /// <summary>
        /// CNAE fiscal (esse campo só deve ser informado quando o campo de Inscrição Municipal estiver preenchido)
        /// </summary>
        private string _cnae;

        public string Cnae
        {
            get { return _cnae; }
            set { _cnae = value; }
        }
        public int CRT  { get; set; }
    }
    // Danner -Fim 30/09/2009
}
