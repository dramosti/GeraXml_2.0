using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImportacaoClientes.bel
{
    public class Identificacao
    {
        #region Dados da Grid

        private string _nome_Cidade = "";
        public string Nome_Cidade
        {
            get { return _nome_Cidade; }
            set { _nome_Cidade = value; }
        }

        private string _cod_Cliente = "";
        public string Cod_Cliente
        {
            get { return _cod_Cliente; }
            set { _cod_Cliente = value; }
        }

        #endregion

        private string _cdConsumidor = "";
        public string CdConsumidor
        {
            get { return _cdConsumidor; }
            set { _cdConsumidor = value; }
        }

        private string _dsIm = "";
        public string DsIm
        {
            get { return _dsIm; }
            set { _dsIm = value; }
        }

        private string _dsRazaoSocial = "";
        public string DsRazaoSocial
        {
            get { return _dsRazaoSocial; }
            set { _dsRazaoSocial = value; }
        }

        private string _dsEmail = "";
        public string DsEmail
        {
            get { return _dsEmail; }
            set { _dsEmail = value; }
        }

        private string _cdTipoEmpresa = "";
        public string CdTipoEmpresa
        {
            get { return _cdTipoEmpresa; }
            set { _cdTipoEmpresa = value; }
        }

        #region Endereco
        private string _dsLogradouro = "";
        public string DsLogradouro
        {
            get { return _dsLogradouro; }
            set { _dsLogradouro = value; }
        }

        private string _dsNumero = "";
        public string DsNumero
        {
            get { return _dsNumero; }
            set { _dsNumero = value; }
        }

        private string _dsComplemento = "";
        public string DsComplemento
        {
            get { return _dsComplemento; }
            set { _dsComplemento = value; }
        }

        private string _dsBairro = "";
        public string DsBairro
        {
            get { return _dsBairro; }
            set { _dsBairro = value; }
        }

        private string _cdMunicipioIbge = "";
        public string CdMunicipioIbge
        {
            get { return _cdMunicipioIbge; }
            set { _cdMunicipioIbge = value; }
        }

        private string _cdUfIbge = "";
        public string CdUfIbge
        {
            get { return _cdUfIbge; }
            set { _cdUfIbge = value; }
        }

        private string _dsTelefone = "";
        public string DsTelefone
        {
            get { return _dsTelefone; }
            set { _dsTelefone = value; }
        }

        private string _dsContato = "";
        public string DsContato
        {
            get { return _dsContato; }
            set { _dsContato = value; }
        }

        private string cdCepPrefixo = "";
        public string CdCepPrefixo
        {
            get { return cdCepPrefixo; }
            set { cdCepPrefixo = value; }
        }

        private string _cdCepSufixo = "";
        public string CdCepSufixo
        {
            get { return _cdCepSufixo; }
            set { _cdCepSufixo = value; }
        }

        private string _cdPais = "";
        public string CdPais
        {
            get { return _cdPais; }
            set { _cdPais = value; }
        }

        private string _dsTelefoneDdd = "";
        public string DsTelefoneDdd
        {
            get { return _dsTelefoneDdd; }
            set { _dsTelefoneDdd = value; }
        }

        private string _dsTelefoneDdi = "";
        public string DsTelefoneDdi
        {
            get { return _dsTelefoneDdi; }
            set { _dsTelefoneDdi = value; }
        }
        #endregion
    }
}
