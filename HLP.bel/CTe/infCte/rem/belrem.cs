using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HLP.bel.Static;

namespace HLP.bel.CTe
{
    public class belrem
    {
        private string _CNPJ = "";
        public string CNPJ
        {
            get { return _CNPJ; }
            set { _CNPJ = value; }
        }

        private string _CPF = "";
        public string CPF
        {
            get { return _CPF; }
            set { _CPF = value; }
        }

        private string _IE = "";
        public string IE
        {
            get { return _IE; }
            set { _IE = value; }
        }

        private string _xNome = "";
        public string xNome
        {
            get { return (belStatic.TpAmb == 2 ? "CT-E EMITIDO EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL" : _xNome); } //OS_29019
            set { _xNome = value; }
        }

        private string _xFant = "";
        public string xFant
        {
            get { return _xFant; }
            set { _xFant = value; }
        }

        private string _fone = "";
        public string fone
        {
            get { return _fone; }
            set { _fone = value; }
        }

        public belenderReme enderReme { get; set; }
        public List<belinfNF> infNF { get; set; }
        public List<belinfNFe> infNFe { get; set; }
        public List<belinfOutros> infOutros { get; set; }





    }
}
