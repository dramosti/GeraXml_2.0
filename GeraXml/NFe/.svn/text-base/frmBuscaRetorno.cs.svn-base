using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using HLP.bel;
using HLP.bel.NFe.GeraXml;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace GeraXml.NFe
{
    public partial class frmBuscaRetorno : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        belBusRetFazenda _objbelBuscaRetFazendo;
        Thread WorkThread;
        public enum tipoBusca { ENVIO, RETORNO };

        public frmBuscaRetorno(belBusRetFazenda objbusretfazenda, tipoBusca tpbusca)
        {
            InitializeComponent();
            _objbelBuscaRetFazendo = objbusretfazenda;
            _objbelBuscaRetFazendo._lblQtde = this.lblTentativas;

            if (tpbusca == tipoBusca.ENVIO)
            {
                WorkThread = new Thread(_objbelBuscaRetFazendo.BusRetFazendaEnvio);
            }
            else
            {
                WorkThread = new Thread(_objbelBuscaRetFazendo.BusRetFazendaRetorno);
            }            
            _objbelBuscaRetFazendo.bStopRetorno = false;
            tempo.Start();
            WorkThread.Start();
            while (!WorkThread.IsAlive) ;
            Thread.Sleep(1);
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            _objbelBuscaRetFazendo.bStopRetorno = true;
            WorkThread.Join();
            this.Close();
        }

        private void tempo_Tick(object sender, EventArgs e)
        {
            if (_objbelBuscaRetFazendo.bStopRetorno)
            {
                WorkThread.Join();
                this.Close();
            }
        }
    }
}