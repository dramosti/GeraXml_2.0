﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HLP.bel.Static;

namespace HLP.bel.CTe
{
    public class beldest
    {
        /// <summary>
        /// 1:1 N TAMANHO 14
        /// </summary>
        private string _CNPJ = "";
        public string CNPJ
        {
            get { return _CNPJ; }
            set { _CNPJ = value; }
        }

        /// <summary>
        /// 1:1 C TAMANHO 11
        /// </summary>
        private string _CPF = "";
        public string CPF
        {
            get { return _CPF; }
            set { _CPF = value; }
        }

        private string _IE = "";
        /// <summary>
        /// 0:1 C TAMANHO 0-14
        /// </summary>
        public string IE
        {
            get { return _IE; }
            set { _IE = value; }
        }

        private string _xNome = "";
        /// <summary>
        /// 1:1 C TAMANHO 1-60
        /// </summary>
        public string xNome
        {
            get { return (belStatic.TpAmb == 2 ? "CT-E EMITIDO EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL" : _xNome); } //OS_29019
            set { _xNome = value; }
        }

        /// <summary>
        /// 0:1 N TAMANHO 8-9
        /// </summary>
        private string _fone = "";
        public string fone
        {
            get { return _fone; }
            set { _fone = value; }
        }

        /// <summary>
        /// 0:1 N TAMANHO 1-60
        /// </summary>
        private string _ISUF = "";
        public string ISUF
        {
            get { return _ISUF; }
            set { _ISUF = value; }
        }

        /// <summary>
        /// 1:1
        /// </summary>
        public belenderDest enderDest { get; set; }

        /// <summary>
        /// 0:1
        /// </summary>
        public bellocEnt locEmt { get; set; }
    }
}
