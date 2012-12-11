using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HLP.Dao.CTe;
using System.Security.Cryptography.X509Certificates;
using HLP.bel.CTe;
using HLP.bel;
using ComponentFactory.Krypton.Toolkit;

namespace NfeGerarXml
{
    public partial class frmInutilizaFaixaCte : KryptonForm
    {
        public string _sMessageException = string.Format("Ocorreu uma Exceção ao Manipular essa Ação : {0}{0}Verifique a Mensagem abaixo: {0}________________________________{0}{0}", Environment.NewLine);
        X509Certificate2 cert = null;
        daoInutilizaFaixaCte objInutiliza = new daoInutilizaFaixaCte();



        public frmInutilizaFaixaCte()
        {
            InitializeComponent();
        }


        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtnCTIni.Text == "")
                {
                    KryptonMessageBox.Show("Número final não Informado.", "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                }
                else if (txtnCTFin.Text == "")
                {
                    KryptonMessageBox.Show("Número inicial não Informado.", "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                }
                else if (txtJust.Text.Length < 15)
                {
                    KryptonMessageBox.Show("Justificativa insuficiente, Mínimo de 15 Caracteres", "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                }
                else
                {
                    InutilizaCte();
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, _sMessageException + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            }


        }
        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
            throw new Exception("Inutilização não foi Realizada.");
        }


        private void InutilizaCte()
        {
            try
            {
                cert = new X509Certificate2();
                cert = belCertificadoDigital.BuscaNome("");
                if (!belCertificadoDigital.ValidaCertificado(cert))
                {
                    this.Close();
                    throw new Exception("Certificado não Selecionado.");
                }
                objInutiliza.PopulaDadosInutilizacao(txtnCTIni.Text, txtnCTFin.Text, txtJust.Text);
                belCriaXml objXml = new belCriaXml(cert);
                List<belStatusCte> ListaStatus = objXml.GerarXmlInutilizacao(objInutiliza.objBelInutiliza);

                KryptonMessageBox.Show(belTrataMensagem.RetornaMensagem(ListaStatus, belTrataMensagem.Tipo.Inutilizacao), "CT-e - RETORNO WEBSERVICE ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SomenteNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }



    }
}
