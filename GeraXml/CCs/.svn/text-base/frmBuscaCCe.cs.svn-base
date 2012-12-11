using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using HLP.Dao.CCe;
using AC.ExtendedRenderer.Navigator;
using HLP.bel.CCe;
using System.Security.Cryptography.X509Certificates;
using HLP.bel;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using CrystalDecisions.Shared;
using System.Xml.Linq;
using HLP.bel.Static;
using HLP.bel.NFe.GeraXml;

namespace NfeGerarXml.CCe
{
    public partial class frmBuscaCCe : KryptonForm
    {
        public string sStatusPesquisa = "";
        daoPesquisaCCe objDaoPesquisaCCe = new daoPesquisaCCe();
        X509Certificate2 cert;

        public frmBuscaCCe()
        {
            try
            {
                InitializeComponent();
                HLP.bel.NFe.belConfigInicial.CarregaConfiguracoesIniciais();
                nmEmpresa.Text = belStatic.sNomeEmpresa + " - " + belStatic.codEmpresaNFe;
                nmAmbiente.Text = (belStatic.tpAmb == 1 ? "Produção" : "Homologação");
            }
            catch (Exception ex)
            {
                new HLPexception(ex.Message, ex);
            }

        }


        private void rdbData_CheckedChanged(object sender, EventArgs e)
        {
            KryptonRadioButton radio = (KryptonRadioButton)sender;
            if (radio.Name.Equals("rdbData"))
            {
                dtpFim.Visible = true;
                dtpIni.Visible = true;

                txtNfFim.Visible = false;
                txtNfIni.Visible = false;

                dtpIni.Focus();
            }
            else
            {
                dtpFim.Visible = false;
                dtpIni.Visible = false;

                txtNfFim.Visible = true;
                txtNfIni.Visible = true;
                txtNfIni.Focus();
            }
        }

        private void txtNfIni_Enter(object sender, EventArgs e)
        {
            KryptonTextBox txt = (KryptonTextBox)sender;
            txt.SelectAll();
        }

        private void txtNfIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                KryptonMessageBox.Show("Somente Números", "Campo Númerico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Handled = true;
            }
        }

        private void txtNfFim_Validated(object sender, EventArgs e)
        {
            KryptonTextBox txt = (KryptonTextBox)sender;
            txt.Text = txt.Text.PadLeft(6, '0');
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            try
            {
                CarregaGridView();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CarregaGridView()
        {
            bsGrid.DataSource = new List<belPesquisaCCe>();
            if (rdbData.Checked)
            {
                bsGrid.DataSource = objDaoPesquisaCCe.BuscaCCe(dtpIni.Value, dtpFim.Value, sStatusPesquisa);
            }
            else
            {
                if (belStatic.RAMO != "TRANSPORTE")
                {
                    bsGrid.DataSource = objDaoPesquisaCCe.BuscaCCe(txtNfIni.Text, txtNfFim.Text, (rdbNF.Checked ? daoPesquisaCCe.Campo.cd_notafis : daoPesquisaCCe.Campo.cd_nfseq), sStatusPesquisa);
                }
                else
                {
                    bsGrid.DataSource = objDaoPesquisaCCe.BuscaCCe(txtNfIni.Text, txtNfFim.Text, (rdbNF.Checked ? daoPesquisaCCe.Campo.cd_conheci : daoPesquisaCCe.Campo.nr_lanc), sStatusPesquisa);
                }
            }
            for (int i = 0; i < dgvItens.RowCount; i++)
            {
                if (dgvItens["QT_ENVIO", i].Value.ToString() == "0")
                {
                    dgvItens.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
                else
                {
                    dgvItens.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 222);
                }
            }
        }

        private void kryptonRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            KryptonRadioButton radio = (KryptonRadioButton)sender;
            sStatusPesquisa = radio.Tag.ToString();
        }

        private void btnEnvio_Click(object sender, EventArgs e)
        {
            try
            {
                AssinaNFeXml objbuscanome = new AssinaNFeXml();
                cert = new X509Certificate2();
                cert = objbuscanome.BuscaNome("");
                belUtil.ValidaCertificado(cert);
                if ((bsGrid.DataSource as List<belPesquisaCCe>).Where(c => c.bSeleciona).Count() > 0)
                {
                    belPesquisaCCe objbelPesqEnvio = (bsGrid.DataSource as List<belPesquisaCCe>).FirstOrDefault(c => c.bSeleciona);
                    List<belPesquisaCCe> objListaSelect = (bsGrid.DataSource as List<belPesquisaCCe>).Where(c => c.bSeleciona).ToList();
                    daoGeraCCe objDaoGeraCCe = new daoGeraCCe(objListaSelect, cert);
                    objDaoGeraCCe.GeraXmlEnvio();
                    string sRetorno = daoEnviaCCe.TransmiteLoteCCe(objDaoGeraCCe.sXMLfinal, cert);
                    string sMessage = objDaoGeraCCe.AnalisaRetornoEnvio(sRetorno);
                    hlpMessageBox.ShowAviso(sMessage);
                    CarregaGridView();
                }
            }
            catch (Exception ex)
            {
                new HLPexception(ex.Message, ex);
            }
        }

        private void kryptonDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SendKeys.Send("{right}");
            SendKeys.Send("{left}");
        }


        private void kryptonTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                AC.ExtendedRenderer.Navigator.KryptonTabControl tab = (AC.ExtendedRenderer.Navigator.KryptonTabControl)sender;

                if (tab.SelectedTab.Name.Equals("tabVisual"))
                {
                    if (bsGrid.Count > 0)
                    {
                        if ((bsGrid.DataSource as List<belPesquisaCCe>).Where(c => c.bSeleciona).Count() > 0)
                        {
                            List<belPesquisaCCe> objLfiltro = ((bsGrid.DataSource as List<belPesquisaCCe>).Where(c => c.bSeleciona).ToList());
                            daoGeraCCe objdaoGeraCCeVisual = new daoGeraCCe(objLfiltro, cert);
                            bsEvento.DataSource = objdaoGeraCCeVisual.objEnvEvento.evento;
                            if (bsEvento.Count > 0)
                            {
                                PoopulaTabVisualizacao(bsEvento.Current as belEvento);

                                if (bsEvento.Count == 1)
                                {
                                    btnFirst.Enabled = false;
                                    btnPrevious.Enabled = false;
                                    btnNext.Enabled = false;
                                    btnLast.Enabled = false;
                                }
                                else
                                {
                                    btnFirst.Enabled = true;
                                    btnPrevious.Enabled = true;
                                    btnNext.Enabled = true;
                                    btnLast.Enabled = true;
                                }
                            }
                        }
                        else
                        {
                            LimpaTabVisuaizacao();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new HLPexception(ex.Message, ex);
            }
        }

        private void PoopulaTabVisualizacao(belEvento objEvento)
        {
            try
            {
                txtNotaFis.Text = (bsGrid.DataSource as List<belPesquisaCCe>).FirstOrDefault(c => c.CHNFE == objEvento.infEvento.chNFe).CD_NOTAFIS;
                txtChaveNFe.Text = objEvento.infEvento.chNFe;
                txtRazaoSocial.Text = (bsGrid.DataSource as List<belPesquisaCCe>).FirstOrDefault(c => c.CHNFE == objEvento.infEvento.chNFe).NM_CLIFOR;
                txtCondUso.Text = objEvento.infEvento.detEvento.xCondUso;
                txtAjustes.Text = objEvento.infEvento.detEvento.xCorrecao.Replace("|", Environment.NewLine);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void LimpaTabVisuaizacao()
        {
            btnFirst.Enabled = false;
            btnPrevious.Enabled = false;
            btnNext.Enabled = false;
            btnLast.Enabled = false;

            txtNotaFis.Text = "";
            txtChaveNFe.Text = "";
            txtRazaoSocial.Text = "";
            txtCondUso.Text = "";
            txtAjustes.Text = "";
        }



        private void MoveFirst_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripButton btn = (ToolStripButton)sender;
                if (btn.Name.Equals("btnFirst"))
                {
                    bsEvento.MoveFirst();
                    PoopulaTabVisualizacao(bsEvento.Current as belEvento);
                }
                else if (btn.Name.Equals("btnLast"))
                {
                    bsEvento.MoveLast();
                    PoopulaTabVisualizacao(bsEvento.Current as belEvento);
                }
                else if (btn.Name.Equals("btnNext"))
                {
                    bsEvento.MoveNext();
                    PoopulaTabVisualizacao(bsEvento.Current as belEvento);
                }
                else if (btn.Name.Equals("btnPrevious"))
                {
                    bsEvento.MovePrevious();
                    PoopulaTabVisualizacao(bsEvento.Current as belEvento);
                }
            }
            catch (Exception ex)
            {
                new HLPexception(ex.Message, ex);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {

        }

        private void kryptonGroupBox2_Panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                Globais LeRegWin = new Globais();
                if ((bsGrid.DataSource as List<belPesquisaCCe>).Where(c => c.bSeleciona && c.QT_ENVIO > 0).Count() > 0)
                {
                    daoCarregaDataSet objdaoCarregaDataSet = new daoCarregaDataSet((bsGrid.DataSource as List<belPesquisaCCe>).Where(c => c.bSeleciona && c.QT_ENVIO > 0).ToList<belPesquisaCCe>());

                    ReportDocument rpt = new ReportDocument();
                    if (LeRegWin.LeRegConfig("UsaRelatorioEspecifico") == "True")
                    {
                        string sCaminho = LeRegWin.LeRegConfig("CaminhoRelatorioEspecifico") + "\\" + "CCe.rpt";
                        rpt.Load(sCaminho);
                    }
                    else
                    {
                        rpt.Load(Application.StartupPath + "\\Relatorios" + "\\" + "\\" + "CCe.rpt");
                    }

                    DirectoryInfo dinfo = new DirectoryInfo(belStaticPastas.ENVIADOS + "\\Servicos" + "\\PDF");
                    if (!dinfo.Exists)
                    {
                        dinfo.Create();
                    }
                    string sCaminhoSave;
                    foreach (DANFE.dsCCe ds in objdaoCarregaDataSet.objListaDS)
                    {
                        sCaminhoSave = dinfo.FullName + "\\" + ds.CCe[0].NFE.ToString() + ".pdf";
                        if (belFecharJanela.IsFileOpen(sCaminhoSave) == false)
                        {
                            rpt.SetDataSource(ds);
                            rpt.Refresh();
                            ExportPDF(rpt, sCaminhoSave);
                        }
                    }

                    EnviaEmailCCe((bsGrid.DataSource as List<belPesquisaCCe>).Where(c => c.bSeleciona && c.QT_ENVIO > 0).ToList<belPesquisaCCe>());

                    //Visualização
                    sCaminhoSave = dinfo.FullName + "\\" + Environment.MachineName + "_Grupo_CCe.pdf";
                    string[] processos = windows_net.EnumerateOpenedWindows.GetDesktopWindowsTitles();
                    foreach (string window in processos)
                    {
                        if (window.Contains("Grupo_CCe"))
                        {
                            belFecharJanela.FecharJanela(window);
                            File.Delete(sCaminhoSave);
                            break;
                        }
                    }
                    rpt.SetDataSource(objdaoCarregaDataSet.objDS);
                    rpt.Refresh();
                    ExportPDF(rpt, sCaminhoSave);
                    System.Diagnostics.Process.Start(sCaminhoSave);
                }
                else
                {
                    hlpMessageBox.ShowAviso("Não há Cartas de Correções válidas selecionadas!");
                }
            }
            catch (Exception ex)
            {
                new HLPexception(ex.Message, ex);
            }

        }



        private void EnviaEmailCCe(List<belPesquisaCCe> lsNotas)
        {
            Globais LeRegWin = new Globais();

            string hostservidor = LeRegWin.LeRegConfig("HostServidor").ToString().Trim();
            string porta = LeRegWin.LeRegConfig("PortaServidor").ToString().Trim();
            string remetente = LeRegWin.LeRegConfig("EmailRemetente").ToString().Trim();
            string senha = LeRegWin.LeRegConfig("SenhaRemetente").ToString().Trim();
            bool autentica = Convert.ToBoolean(LeRegWin.LeRegConfig("RequerSSL").ToString().Trim());

            List<belEmail> objlbelEmail = new List<belEmail>();

            if ((hostservidor != "") && (porta != "0") && (remetente != "") && (senha != ""))
            {
                for (int e = 0; e < lsNotas.Count; e++)
                {
                    // InformaStatusEnvio("Estruturando Email", e, lsNotas.Count);
                    belEmail objemail = new belEmail(lsNotas[e], lsNotas[e].CD_NFSEQ, lsNotas[e].CD_NOTAFIS, belStatic.codEmpresaNFe, hostservidor, porta, remetente, senha, "", autentica);
                    objlbelEmail.Add(objemail);
                }
            }
            else
            {
                if (KryptonMessageBox.Show(null, "Campos para o envio de e-Mail automático não estão preenchidos corretamente!" +
                                Environment.NewLine + Environment.NewLine +
                                "Deseja Preencher os campos corretamente agora ?", "E-Mail não pode ser enviado", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    frmConfiguracao objconfiguracao = new frmConfiguracao(2);
                    objconfiguracao.ShowDialog();
                }
            }


            if (objlbelEmail.Count > 0)
            {
                frmEmailNfe objfrmEmail = new frmEmailNfe(objlbelEmail);
                objfrmEmail.ShowDialog();
                int icount = 0;
                for (int i = 0; i < objfrmEmail.objLbelEmail.Count; i++)
                {
                    if ((objfrmEmail.objLbelEmail[i]._envia == true) && (objfrmEmail.objLbelEmail[i]._para != "" || objfrmEmail.objLbelEmail[i]._paraTransp != ""))
                    {
                        try
                        {
                            // InformaStatusEnvio("Enviando Email", i, lCaminhosXml.Count);
                            objfrmEmail.objLbelEmail[i].enviaEmail();
                            icount++;
                        }
                        catch (Exception ex)
                        {
                            KryptonMessageBox.Show(null, ex.Message + Environment.NewLine + Environment.NewLine + "E-mail: " + objfrmEmail.objLbelEmail[i]._para + "   - Seq: " + objfrmEmail.objLbelEmail[i]._sSeq, "E R R O - E N V I O", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                if (icount > 0)
                {
                    KryptonMessageBox.Show(null, "Procedimento de Envio de E-mail Finalizado!"
                           + Environment.NewLine
                           + Environment.NewLine,
                           "A V I S O",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        private static void ExportPDF(ReportDocument rpt, string sCaminhoSave)
        {
            CrystalDecisions.Windows.Forms.CrystalReportViewer cryView = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            ExportOptions CrExportOptions;
            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
            CrDiskFileDestinationOptions.DiskFileName = sCaminhoSave;
            CrExportOptions = rpt.ExportOptions;
            {
                CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                CrExportOptions.FormatOptions = CrFormatTypeOptions;
            }
            rpt.Export();
        }

        private int _cacheWidth;
        private void btnMinimiza_Click(object sender, EventArgs e)
        {
            try
            {
                kryptonSplitContainer1.SuspendLayout();
                if (kryptonSplitContainer1.FixedPanel == FixedPanel.None)
                {
                    kryptonSplitContainer1.FixedPanel = FixedPanel.Panel1;
                    kryptonSplitContainer1.IsSplitterFixed = true;

                    _cacheWidth = headerMenuLateral.Width;
                    int newWidth = headerMenuLateral.PreferredSize.Height;

                    kryptonSplitContainer1.Panel1MinSize = newWidth;
                    kryptonSplitContainer1.SplitterDistance = newWidth;

                    headerMenuLateral.HeaderPositionPrimary = VisualOrientation.Right;
                    headerMenuLateral.ButtonSpecs[0].Edge = PaletteRelativeEdgeAlign.Near;
                    for (int i = 0; i < kryptonSplitContainer1.Panel1.Controls.Count; i++)
                    {
                        if (kryptonSplitContainer1.Panel1.Controls[i].GetType() == typeof(KryptonButton) || kryptonSplitContainer1.Panel1.Controls[i].GetType() == typeof(KryptonSeparator))
                        {
                            kryptonSplitContainer1.Panel1.Controls[i].Visible = false;
                        }
                        else if (kryptonSplitContainer1.Panel1.Controls[i].GetType() == typeof(KryptonTextBox))
                        {
                            kryptonSplitContainer1.Panel1.Controls[i].Visible = false;
                        }
                        else if (kryptonSplitContainer1.Panel1.Controls[i].GetType() == typeof(KryptonHeader))
                        {
                            kryptonSplitContainer1.Panel1.Controls[i].Visible = false;
                        }
                    }
                }
                else
                {
                    kryptonSplitContainer1.FixedPanel = FixedPanel.None;
                    kryptonSplitContainer1.IsSplitterFixed = false;
                    kryptonSplitContainer1.Panel1MinSize = 25;
                    kryptonSplitContainer1.SplitterDistance = _cacheWidth;

                    headerMenuLateral.HeaderPositionPrimary = VisualOrientation.Top;
                    headerMenuLateral.ButtonSpecs[0].Edge = PaletteRelativeEdgeAlign.Far;

                    for (int i = 0; i < kryptonSplitContainer1.Panel1.Controls.Count; i++)
                    {
                        if (kryptonSplitContainer1.Panel1.Controls[i].GetType() == typeof(KryptonButton) || kryptonSplitContainer1.Panel1.Controls[i].GetType() == typeof(KryptonSeparator))
                        {
                            kryptonSplitContainer1.Panel1.Controls[i].Visible = true;
                        }
                        else if (kryptonSplitContainer1.Panel1.Controls[i].GetType() == typeof(KryptonTextBox))
                        {
                            kryptonSplitContainer1.Panel1.Controls[i].Visible = true;
                        }
                        else if (kryptonSplitContainer1.Panel1.Controls[i].GetType() == typeof(KryptonHeader))
                        {
                            kryptonSplitContainer1.Panel1.Controls[i].Visible = true;
                        }
                    }
                }
                kryptonSplitContainer1.ResumeLayout();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void frmBuscaCCe_Load(object sender, EventArgs e)
        {

        }
    }
}
