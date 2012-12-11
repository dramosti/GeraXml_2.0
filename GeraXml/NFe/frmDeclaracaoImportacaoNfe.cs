using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HLP.bel;
using ComponentFactory.Krypton.Toolkit;

namespace NfeGerarXml.NFe
{
    public partial class frmDeclaracaoImportacaoNfe : KryptonForm
    {
        List<belDI> objLisbelAdi = null;
        public List<belDet> objListaProd;

        public frmDeclaracaoImportacaoNfe(List<belDet> objListaProd, bool bPopula)
        {
            InitializeComponent();
            this.objListaProd = objListaProd;
            lblObs.Text = "Obs.: É Obrigatório" + Environment.NewLine + "no mínimo" + Environment.NewLine + " uma Declaração e uma" + Environment.NewLine + " adição por Produto.";          

            if (bPopula)
            {
                for (int i = 0; i < this.objListaProd.Count; i++)
                {
                    List<beladi> objlistadi = new List<beladi>();
                    objlistadi.Add(new beladi { nSeqAdic = 1, nAdicao = 1, cFabricante = "1", vDescDI = 1 });
                    this.objListaProd[i].belProd.belDI = new List<belDI>();
                    this.objListaProd[i].belProd.belDI.Add(new belDI
                    {
                        cExportador = "1",
                        dDesemb = HLP.Util.Util.GetDateServidor().Date,
                        DDI = HLP.Util.Util.GetDateServidor().Date,
                        nDI = "1",
                        UFDesemb = "SP",
                        xLocDesemb = "são paulo",
                        adi = objlistadi
                    });
                }
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if ((bsDI.Current as belDI) != null)
                {
                    if ((bsDI.Current as belDI).adi != null)
                    {
                        bsAdicoes.DataSource = (bsDI.Current as belDI).adi;
                    }
                    else
                    {
                        (bsDI.Current as belDI).adi = new List<beladi>();
                        bsAdicoes.DataSource = (bsDI.Current as belDI).adi;
                        for (int i = 0; i < dgvADI.Rows.Count; i++)
                        {
                            dgvADI.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        }
                    }
                }

            }
        }

        private bool ValidaDI(belDI obj)
        {
            try
            {
                int icoun = 0;

                if (obj.nDI == null) { icoun++; }
                if (obj.DDI == null) { icoun++; }
                if (obj.xLocDesemb == null) { icoun++; }
                if (obj.UFDesemb == null) { icoun++; }
                if (obj.dDesemb == null) { icoun++; }
                if (obj.cExportador == null) { icoun++; }

                if (icoun > 0)
                {
                    KryptonMessageBox.Show(null, "Verifique os Campos em Vermelho da DI selecionada, pois são obrigatórios!!", "A T E N Ç Ã O", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if ((e.FormattedValue.ToString() != ""))
                {
                    if ((e.ColumnIndex == 1) || (e.ColumnIndex == 4))
                    {
                        Convert.ToDateTime(e.FormattedValue.ToString());
                    }
                }
            }
            catch (Exception)
            {
                KryptonMessageBox.Show(null, "Data Inválida!!", "A V I S O", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }

        }

        private void frmDeclaracaoImportacao_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < objListaProd.Count; i++)
            {
                listBoxProdutos.Items.Add(objListaProd[i].belProd);
            }
            listBoxProdutos.SelectedIndex = 0;
        }

        private void listBoxProdutos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((listBoxProdutos.SelectedItem as belProd).belDI == null)
            {
                (listBoxProdutos.SelectedItem as belProd).belDI = new List<belDI>();
                bsDI.DataSource = (listBoxProdutos.SelectedItem as belProd).belDI;
            }
            else
            {
                bsDI.DataSource = (listBoxProdutos.SelectedItem as belProd).belDI;
            }
            if ((bsDI.Current as belDI) != null)
            {
                bsAdicoes.DataSource = (bsDI.Current as belDI).adi;
            }
            else
            {
                List<beladi> obj = new List<beladi>();
                bsAdicoes.DataSource = obj;
            }
        }

        private void frmDeclaracaoImportacao_FormClosed(object sender, FormClosedEventArgs e)
        {
            KryptonMessageBox.Show(null, "As Declarações de Importação foram salvas com sucesso.", "A V I S O", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvADI_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if ((e.FormattedValue.ToString() != ""))
                {
                    if ((e.ColumnIndex == 0) || (e.ColumnIndex == 3))
                    {
                        Convert.ToDecimal(e.FormattedValue.ToString());
                    }
                }
            }
            catch (Exception)
            {
                KryptonMessageBox.Show(null, "Campo Numérico!!", "A V I S O", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }


    }
}
