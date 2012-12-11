using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLP.bel.CTe
{
    public class belaquav
    {
        /// <summary>
        /// 1:1 N TAMANHO 13,2
        /// </summary>
        public decimal vPrest { get; set; }

        /// <summary>
        /// 1:1 N TAMANHO 13,2
        /// </summary>
        public decimal vAFRMM { get; set; }

        private string _nBooking = "";
        /// <summary>
        /// 0:1 C TAMANHO 1-10
        /// </summary>
        public string nBooking
        {
            get { return _nBooking; }
            set { _nBooking = value; }
        }

        private string _nCtrl = "";
        /// <summary>
        /// 0:1 C TAMANHO 1-10
        /// </summary>
        public string nCtrl
        {
            get { return _nCtrl; }
            set { _nCtrl = value; }
        }

        private string _xNavio = "";
        /// <summary>
        /// 0:1 C TAMANHO 1-60
        /// </summary>
        public string xNavio
        {
            get { return _xNavio; }
            set { _xNavio = value; }
        }

        private string _nViag = "";
        /// <summary>
        /// 0:1 C TAMANHO 1-10
        /// </summary>
        public string nViag
        {
            get { return _nViag; }
            set { _nViag = value; }
        }

        private string _direc = "";
        /// <summary>
        /// 1:1 C TAMANHO 1 
        /// N-Norte, L-Leste, S-Sul, O-Oeste
        /// </summary>
        public string direc
        {
            get { return _direc; }
            set { _direc = value; }
        }

        private string _prtEmb = "";
        /// <summary>
        /// 0:1 C TAMANHO 1-60
        /// </summary>
        public string prtEmb
        {
            get { return _prtEmb; }
            set { _prtEmb = value; }
        }

        private string _prtTrans = "";
        /// <summary>
        /// 0:1 C TAMANHO 1-60
        /// </summary>
        public string prtTrans
        {
            get { return _prtTrans; }
            set { _prtTrans = value; }
        }

        private string _prtDest = "";
        /// <summary>
        /// 0:1 C TAMANHO 1-60
        /// </summary>
        public string prtDest
        {
            get { return _prtDest; }
            set { _prtDest = value; }
        }

        /// <summary>
        /// 0:1 N TAMANHO 1 
        /// 0 - Interior
        ///1 - Cabotagem
        /// </summary>
        public int tpNav { get; set; }

        private string _irin = "";
        /// <summary>
        /// 1:1   TAMANHO 1-10
        /// </summary>
        public string irin
        {
            get { return _irin; }
            set { _irin = value; }
        }

        /// <summary>
        /// 0:3
        /// </summary>
        public bellacre lacre { get; set; }

    }
}

