using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using HLP.bel;
using ComponentFactory.Krypton.Toolkit;
using HLP.bel.Static;
using HLP.bel.NFe.GeraXml;

namespace NfeGerarXml
{
    public partial class frmProtocolosNfe : KryptonForm
    {
        string sPastaProtocolos = string.Empty;
        frmGerarXml objPrincipal;
        HLP.bel.NFe.GeraXml.Globais objbelGlobais = new HLP.bel.NFe.GeraXml.Globais();

        public frmProtocolosNfe(frmGerarXml objPrincipal)
        {
            InitializeComponent();
            this.objPrincipal = objPrincipal;

        }

        private void PopulaGridCancelados()
        {
            try
            {
                sPastaProtocolos = belStaticPastas.PROTOCOLOS;
                DirectoryInfo diretorio = new DirectoryInfo(sPastaProtocolos);
                FileSystemInfo[] itens = diretorio.GetFileSystemInfos("*.xml");
                int irow = 0;
                dgvCancelamentos.Rows.Clear();
                foreach (FileSystemInfo item in itens)
                {
                    if (item.Name.Contains("ped-can"))
                    {
                        XmlDocument xml = new XmlDocument();
                        xml.Load(item.FullName);
                        dgvCancelamentos.Rows.Add();
                        dgvCancelamentos[0, irow].Value = (xml.GetElementsByTagName("infCanc").Item(0).FirstChild.InnerText == "2" ? "Homologação" : "Produção");
                        dgvCancelamentos[1, irow].Value = (xml.GetElementsByTagName("chNFe").Item(0).InnerText.Equals("") ? "S/Nota" : xml.GetElementsByTagName("chNFe").Item(0).InnerText.Substring(25, 9));
                        dgvCancelamentos[2, irow].Value = (xml.GetElementsByTagName("chNFe").Item(0).InnerText.Equals("") ? "S/Sequencia" : xml.GetElementsByTagName("chNFe").Item(0).InnerText.Substring(34, 9));
                        dgvCancelamentos[3, irow].Value = (xml.GetElementsByTagName("nProt").Item(0).InnerText.Equals("") ? "S/Protocolo" : xml.GetElementsByTagName("nProt").Item(0).InnerText);
                        dgvCancelamentos[5, irow].Value = item.Name;
                        irow++;

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void PopulaGridInutilizados()
        {
            try
            {
                sPastaProtocolos = belStaticPastas.PROTOCOLOS;
                DirectoryInfo diretorio = new DirectoryInfo(sPastaProtocolos);
                FileSystemInfo[] itens = diretorio.GetFileSystemInfos("*.xml");
                int irow = 0;
                dgvInutilizacoes.Rows.Clear();

                foreach (FileSystemInfo item in itens)
                {
                    if ((item.Name.Contains("_inu")) && (!item.Name.Contains("_ped_inu")))
                    {
                        XmlDocument xml = new XmlDocument();
                        xml.Load(item.FullName);
                        dgvInutilizacoes.Rows.Add();
                        dgvInutilizacoes[0, irow].Value = (xml.GetElementsByTagName("tpAmb").Item(0).InnerText == "2" ? "Homologação" : "Produção");
                        dgvInutilizacoes[1, irow].Value = xml.GetElementsByTagName("nNFIni").Item(0).InnerText.PadLeft(9, '0');
                        dgvInutilizacoes[2, irow].Value = xml.GetElementsByTagName("nNFFin").Item(0).InnerText.PadLeft(9, '0');
                        dgvInutilizacoes[3, irow].Value = Convert.ToDateTime(xml.GetElementsByTagName("dhRecbto").Item(0).InnerText).ToString("dd/MM/yyyy");
                        dgvInutilizacoes[4, irow].Value = xml.GetElementsByTagName("nProt").Item(0).InnerText;
                        irow++;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void dgvCancelamentos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == 4)
                {
                    if (dgvCancelamentos[e.ColumnIndex, e.RowIndex].Value == null)
                    {
                        dgvCancelamentos[e.ColumnIndex, e.RowIndex].Value = false;
                    }
                    dgvCancelamentos[e.ColumnIndex, e.RowIndex].Value = !Convert.ToBoolean(dgvCancelamentos[e.ColumnIndex, e.RowIndex].Value);
                }
            }
        }

        private void btnEnviarEmail_Click(object sender, EventArgs e)
        {
            List<string> sCaminhos = new List<string>();
            for (int i = 0; i < dgvCancelamentos.RowCount; i++)
            {
                if (dgvCancelamentos[4, i].Value != null)
                {
                    if (Convert.ToBoolean(dgvCancelamentos[4, i].Value))
                    {
                        sCaminhos.Add(dgvCancelamentos[5, i].Value.ToString());
                    }
                }
            }
            EnviaEmailCancelamento(sCaminhos);
        }

        private void EnviaEmailCancelamento(List<string> objListaEmail) //NFe_2.0
        {
            try
            {
                Globais LeRegWin = new Globais();
                string hostservidor = LeRegWin.LeRegConfig("HostServidor").ToString().Trim();
                string porta = LeRegWin.LeRegConfig("PortaServidor").ToString().Trim();
                string remetente = LeRegWin.LeRegConfig("EmailRemetente").ToString().Trim();
                string senha = LeRegWin.LeRegConfig("SenhaRemetente").ToString().Trim();
                bool autentica = Convert.ToBoolean(LeRegWin.LeRegConfig("RequerSSL").ToString().Trim());
                List<belEmail> objlbelEmail = new List<belEmail>();
                int iCount = 0;

                if ((hostservidor != "") && (porta != "0") && (remetente != "") && (senha != ""))
                {
                    for (int i = 0; i < objListaEmail.Count; i++)
                    {
                        belEmail objemail = new belEmail(objListaEmail[i].Substring(0, 6), LeRegWin.LeRegConfig("Empresa").ToString().Trim(), hostservidor, porta, remetente, senha, "", autentica);
                        objlbelEmail.Add(objemail);
                    }
                }
                else
                {
                    if (KryptonMessageBox.Show(null, "Campos para o envio de e-Mail automático não estão preenchidos corretamente!" +
                                    Environment.NewLine + Environment.NewLine +
                                    "Deseja Preencher os campos corretamente agora ?", "E-Mail não pode ser enviado", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        Boolean ok = false;
                        foreach (Form frm in this.MdiChildren)
                        {
                            if (frm is frmLoginConfig)
                            {
                                frm.BringToFront();
                                ok = true;
                            }
                        }
                        if (!ok)
                        {
                            frmLoginConfig objfrm = new frmLoginConfig(objPrincipal);
                            objfrm.MdiParent = this;
                            objfrm.Show();
                        }
                    }
                }
                if (objlbelEmail.Count > 0)
                {
                    frmEmailNfe objfrmEmail = new frmEmailNfe(objlbelEmail);
                    objfrmEmail.sTipo = "C";
                    objfrmEmail.ShowDialog();
                    for (int i = 0; i < objfrmEmail.objLbelEmail.Count; i++)
                    {
                        if ((objfrmEmail.objLbelEmail[i]._envia == true) && (objfrmEmail.objLbelEmail[i]._para != "" || objfrmEmail.objLbelEmail[i]._paraTransp != ""))
                        {
                            try
                            {
                                objfrmEmail.objLbelEmail[i].enviaEmail();
                                iCount++;
                            }
                            catch (Exception ex)
                            {
                                KryptonMessageBox.Show(null, ex.Message + Environment.NewLine + Environment.NewLine + "E-mail: " + objfrmEmail.objLbelEmail[i]._para + "   - Seq: " + objfrmEmail.objLbelEmail[i]._sSeq, "E R R O - E N V I O", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    if (iCount > 0)
                    {
                        KryptonMessageBox.Show(null, "Procedimento de Envio de E-mail Finalizado!"
                            + Environment.NewLine
                            + Environment.NewLine,
                            "A V I S O",
                             MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, ex.Message, "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void frmProtocolos_Load(object sender, EventArgs e)
        {
            try
            {
                DirectoryInfo dinfo = new DirectoryInfo(belStatic.Pasta_xmls_Configs);
                FileInfo[] finfo = dinfo.GetFiles();

                foreach (FileInfo item in finfo)
                {
                    if (Path.GetExtension(item.FullName).ToUpper().Equals(".XML"))
                    {
                        cbxArquivos.Items.Add(item.Name);
                    }
                }
                if ((belStatic.sConfig != "") && (belStatic.sConfig != null))
                {
                    cbxArquivos.Text = belStatic.sConfig;
                }
                else if (cbxArquivos.Items.Count > 0)
                {
                    cbxArquivos.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cbxArquivos_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                belStatic.sConfig = cbxArquivos.SelectedItem.ToString();
                objbelGlobais = new Globais();
                PopulaGridCancelados();
                PopulaGridInutilizados();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
