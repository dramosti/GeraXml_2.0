using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLP.bel
{
    public class belTransp
    {
        private string _modfrete;
        private belTransportadora _belTransportadora;
        private belRetTransp _belRetTransp;
        private belVeicTransp _belVeicTransp;
        private belLacres _belLacres;
        private belVol _belVol;
        private belReboque _belReboque;

        public belReboque belReboque
        {
            get
            {
                return _belReboque;
            }
            set
            {
                _belReboque = value;
            }
        }
        public string Modfrete
        {
            get { return _modfrete; }
            set { _modfrete = value; }
        }

        public belTransportadora belTransportadora
        {
            get
            {
                return _belTransportadora ;
            }
            set
            {
                _belTransportadora = value;
            }
        }

        public belRetTransp belRetTransp
        {
            get
            {
                return _belRetTransp;
            }
            set
            {
                _belRetTransp = value;
            }
        }

        public belVeicTransp belVeicTransp
        {
            get
            {
                return _belVeicTransp;
            }
            set
            {
                _belVeicTransp = value;
            }
        }

        public belVol belVol
        {
            get
            {
                return _belVol;
            }
            set
            {
                _belVol = value;
            }
        }

        public belLacres belLacres
        {
            get
            {
                return _belLacres;
            }
            set
            {
                _belLacres = value;
            }
        }
    }
}
