﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLP.bel
{
    public class belProd
    {
        /// <summary>
        /// Código do Produto ou Serviço
        /// </summary>
        private string _cprod;

        public string Cprod
        {
            get { return _cprod; }
            set { _cprod = Util.Util.TiraSimbolo(value,""); }
        }

        /// <summary>
        /// GTIN (Global Trade Item Number) do produto, antigo código EAN ou código de barras, não importar o conteúdo da TAG, em caso do produto não  possuir este código.
        /// </summary>
        private string _cean;

        public string Cean
        {
            get { return _cean; }
            set { _cean = value; }
        }

        /// <summary>
        /// Descrição do Produto ou Serviço
        /// </summary>
        private string _xprod;

        public string Xprod
        {
            get { return _xprod; }
            set { _xprod = value; } //Util.Util.TiraSimbolo(value,""); }
        }
        /// <summary>
        /// Código NCM, Preencher de acordo com a tabela de Capítulos da NCM. Em caso de serviço, não incluir essa tag.
        /// </summary>
        private string _ncm;

        public string Ncm
        {
            get { return _ncm; }
            set { _ncm = Util.Util.TiraSimbolo(value,""); }
        }

        /// <summary>
        /// Preencher de acordo com o código Ex da TIPI. Em caso de serviço não Incluir a tag
        /// </summary>
        private string _extipi;

        public string Extipi
        {
            get { return _extipi; }
            set { _extipi = value; }
        }

        /// <summary>
        /// Gênero do produto ou Serviço. Preencher de acordo com a Tabela de Capítulos da NCM em caso de serviço não incluir a TAG
        /// </summary>
        private string _genero;

        public string Genero
        {
            get { return _genero; }
            set { _genero = value; }
        }

        /// <summary>
        /// Código Fiscal de Operações e Prestações, Utilizar a tabela do CFOP
        /// </summary>
        private string _cfop;

        public string Cfop
        {
            get { return _cfop; }
            set { _cfop = value; }
        }

        /// <summary>
        /// Unidade Comercial, Utilizar a Unidade Comercial do Produto.
        /// </summary>
        private string _ucom;

        public string Ucom
        {
            get { return _ucom; }
            set { _ucom = value; }
        }

        /// <summary>
        /// Quantidade Comercial, Informar a quantidade comercialização do Produto.
        /// </summary>
        private decimal _qcom;

        public decimal Qcom
        {
            get { return _qcom; }
            set { _qcom = value; }
        }

        /// <summary>
        /// Valor Unitário de Comercialização.
        /// </summary>
        private decimal _vuncom;

        public decimal Vuncom
        {
            get { return _vuncom; }
            set { _vuncom = value; }
        }

        /// <summary>
        /// Valor Total Bruto do Produtos ou Serviços
        /// </summary>
        private decimal _vprod;

        public decimal Vprod
        {
            get { return _vprod; }
            set { _vprod = value; }
        }

        /// <summary>
        /// Unidade Tributavel
        /// </summary>
        private string _utrib;

        public string Utrib
        {
            get { return _utrib; }
            set { _utrib = value; }
        }

        /// <summary>
        /// Quantidade Tributavel
        /// </summary>
        private decimal _qtrib;

        public decimal Qtrib
        {
            get { return _qtrib; }
            set { _qtrib = value; }
        }

        /// <summary>
        /// Valor Unitario de Tributação
        /// </summary>
        private decimal _vuntrib;

        public decimal Vuntrib
        {
            get { return _vuntrib; }
            set { _vuntrib = value; }
        }

        /// <summary>
        /// Valor Total do Frete
        /// </summary>
        private decimal _vfrete;

        /// <summary>
        /// Valor Total do Frete
        /// </summary>
        public decimal Vfrete
        {
            get { return _vfrete; }
            set { _vfrete = value; }
        }

        /// <summary>
        /// Valor Total do Seguro
        /// </summary>
        private decimal _vseg;

        /// <summary>
        /// Valor Total do Seguro
        /// </summary>
        public decimal Vseg
        {
            get { return _vseg; }
            set { _vseg = value; }
        }

        /// <summary>
        /// Valor Desconto
        /// </summary>
        private decimal _vdesc;

        /// <summary>
        /// Valor do Desconto
        /// </summary>
        public decimal Vdesc
        {
            get { return _vdesc; }
            set { _vdesc = value; }
        }
        /// <summary>
        /// Outras despesas acessórias
        /// </summary>
        public decimal VOutro { get; set; }

        /// <summary>
        /// Indica se valor do Item (vProd) entra no valor total da NF-e(vProd)
        /// </summary>
        public int IndTot { get; set; }

        public belVeicprod belVeicprod
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public belMed belMed
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public belArma belArma
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public belComp belComp
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        private string  _ceantrib;

        public string Ceantrib
        {
            get { return _ceantrib; }
            set { _ceantrib = value; }
        }
    }
}
