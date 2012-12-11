using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLP.bel
{
    public class belInfAdic
    {
        /// <summary>
        /// Informaçoes dicionais de Inderesse do Fisco
        /// </summary>
        private string _infadfisco;

        public string Infadfisco
        {
            get { return _infadfisco; }
            set { _infadfisco = value; }
        }
        /// <summary>
        /// Informações Complementares de Interesses do Contribuintes
        /// </summary>
        private string _infcpl;

        public string Infcpl
        {
            get { return _infcpl; }
            set { _infcpl = value; }
        }

        public belObsCont belObsCont
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public belObsFisco belObsFisco
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public belProcRef belProcRef
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }
}
