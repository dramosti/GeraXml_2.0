using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using HLP.bel;
using NfeGerarXml.Config;
using ComponentFactory.Krypton.Toolkit;
using HLP.bel.Static;

namespace NfeGerarXml
{
    public partial class frmLoginConfig : KryptonForm
    {
        frmGerarXml objfrmPrincipal = null;
        public frmLoginConfig()
        {
            InitializeComponent();
        }

        public frmLoginConfig(frmGerarXml objfrmPrincipal)
        {
            InitializeComponent();
            this.objfrmPrincipal = objfrmPrincipal;

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSenha.Text.Equals("MASTERPC"))
                {
                    if ((chkNovo.Checked) && (txtNomeArquivo.Text == ""))
                    {
                        errorProvider1.SetError(txtNomeArquivo, "Insira um nome para o Arquivo");
                        txtNomeArquivo.Focus();
                        throw new Exception("Verifique as Pendencias");
                    }
                    if ((cbxArquivos.Items.Count == 0) && !(chkNovo.Checked))
                    {
                        errorProvider1.SetError(txtNomeArquivo, "Insira um nome para o Arquivo");
                        chkNovo.Checked = true;
                        txtNomeArquivo.Focus();
                        throw new Exception("Verifique as Pendencias");
                    }

                    Boolean ok = false;
                    if (objfrmPrincipal != null)
                    {
                        foreach (Form frm in this.objfrmPrincipal.MdiChildren)
                        {
                            if (frm is frmConfiguracao)
                            {
                                frm.BringToFront();
                                ok = true;
                            }
                        }
                    }
                    if (!ok)
                    {
                        this.Hide();
                        if (chkNovo.Checked)
                        {
                            belStatic.sConfig = txtNomeArquivo.Text + ".xml";

                        }
                        else
                        {
                            belStatic.sConfig = cbxArquivos.SelectedItem.ToString();
                        }
                        frmConfiguracao objfrm = new frmConfiguracao(0);
                        //objfrm.MdiParent =  objfrmPrincipal;
                        objfrm.ShowDialog();
                        this.Close();
                    }
                }
                else
                {
                    KryptonMessageBox.Show(null, "SENHA INCORRETA", "A V I S O ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSenha.Focus();
                    txtSenha.SelectAll();
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, ex.Message, "A V I S O", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                SendKeys.Send("{tab}");
            }
        }

        private void frmLoginConfig_Load(object sender, EventArgs e)
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
                if ((belStatic.sConfig != "") && ((belStatic.sConfig != null)))
                {
                    cbxArquivos.Text = belStatic.sConfig;
                }
                else if (cbxArquivos.Items.Count > 0)
                {
                    cbxArquivos.SelectedIndex = 0;
                }
                cbxArquivos.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void chkNovo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkNovo.Checked)
                {
                    txtNomeArquivo.Enabled = true;
                    cbxArquivos.Enabled = false;
                    txtNomeArquivo.Focus();
                }
                else
                {
                    txtNomeArquivo.Enabled = false;
                    cbxArquivos.Enabled = true;
                    cbxArquivos.Focus();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cbxArquivos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                SendKeys.Send("{tab}");
            }
        }

        private void frmLoginConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (belStatic.IPrimeiroLoad == 1)
            {
                Application.Exit();
            }
        }


    }
}
