using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HLP.bel.NFes;
using ComponentFactory.Krypton.Toolkit;

namespace NfeGerarXml.NFes
{
    public partial class frmCancelamentoNfs : KryptonForm
    {
        belCancelamentoNFse objbelCanc = new belCancelamentoNFse();
        List<belCancelamentoNFse> objListaAll = new List<belCancelamentoNFse>();
        public string sErro = "";

        public frmCancelamentoNfs()
        {
            InitializeComponent();
            objListaAll = objbelCanc.RetListaErros();
            bsCancelamento.DataSource = objListaAll;
            cbxFiltro.SelectedIndex = 0;
            txtFiltro.Focus();
        }


        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            KryptonTextBox txt = (KryptonTextBox)sender;
            if (cbxFiltro.SelectedIndex == 0)
            {
                bsCancelamento.DataSource = objListaAll.FindAll(l => l.cod.ToUpper().Contains(txt.Text.ToUpper())).ToList();
            }
            else
            {
                bsCancelamento.DataSource = objListaAll.FindAll(l => l.msg.ToUpper().Contains(txt.Text.ToUpper())).ToList();
            }

            if (bsCancelamento.Count==0)
            {
                lblErro.Text = "''";
            }
        }

        private void cbxFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFiltro.Text = "";
        }

        private void dgvTabErros_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (bsCancelamento.Count > 0)
                {
                    txtSolucao.Text = dgvTabErros[2, e.RowIndex].Value.ToString();
                    lblErro.Text = "'" + dgvTabErros[0, e.RowIndex].Value.ToString() + "'";
                }
                else
                {
                    lblErro.Text = "' '";
                }
            }
            else
            {
                lblErro.Text = "' '";
            }
        }

        private void frmCancelamento_Load(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (bsCancelamento.Count == 0)
            {
                KryptonMessageBox.Show(null, "É necessário selecionar um Erro para cancelar a NFe-Serviço", "A V I S O", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                sErro = lblErro.Text.Replace("'", "").Trim();
                this.Close();
            }
        }




    }
}
