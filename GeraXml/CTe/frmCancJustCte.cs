using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HLP.bel.CTe;
using HLP.bel;
using HLP.Dao.CTe;
using System.Security.Cryptography.X509Certificates;
using ComponentFactory.Krypton.Toolkit;

namespace NfeGerarXml
{
    public partial class frmCancJustCte : KryptonForm
    {
        public string _sMessageException = string.Format("Ocorreu uma Exceção ao Manipular essa Ação : {0}{0}Verifique a Mensagem abaixo: {0}________________________________{0}{0}", Environment.NewLine);

        X509Certificate2 cert = null;
        daoCancelaCte objCancelaCte = new daoCancelaCte();
        daoGravaDadosRetorno objDadosRetorno = new daoGravaDadosRetorno();
        daoBuscaDadosGerais objdaoDadosGerais = new daoBuscaDadosGerais();
        public string sCodConhecimento = "";


        public frmCancJustCte(List<string> sListCodConhec)
        {
            InitializeComponent();
            this.sCodConhecimento = sListCodConhec[0];
        }



        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtJust.Text.Length < 15)
                {
                    KryptonMessageBox.Show("Justificativa insuficiente, Mínimo de 15 Caracteres", "CT-e - AVISO ", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                }
                else
                {
                    CancelaCte();
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, _sMessageException + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "CT-e - AVISO ", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            }
        }
        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
            throw new Exception("Cancelamento não foi Realizado.");
        }


        private void CancelaCte()
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
                else
                {
                    string sJustificativa = txtJust.Text;
                    objCancelaCte.PopulaDadosCancelamento(sCodConhecimento, sJustificativa);

                    belCriaXml objXml = new belCriaXml(cert);
                    List<belStatusCte> ListaStatus = objXml.GerarXmlCancelamento(objCancelaCte.objBelCancelaCte);

                    foreach (belStatusCte cte in ListaStatus)
                    {
                        if (cte.CodRetorno == "101")
                        {
                            objDadosRetorno.GravarReciboCancelamento(sCodConhecimento, cte.Protocolo, sJustificativa);
                            objXml.SalvaArquivoPastaCancelado(objdaoDadosGerais.BuscaChaveRetornoCteSeq(cte.NumeroSeq));
                        }
                    }

                    KryptonMessageBox.Show(belTrataMensagem.RetornaMensagem(ListaStatus, belTrataMensagem.Tipo.Cancelamento), "CT-e - RETORNO WEBSERVICE ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, _sMessageException + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "CT-e - AVISO ", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            }
        }



    }
}
