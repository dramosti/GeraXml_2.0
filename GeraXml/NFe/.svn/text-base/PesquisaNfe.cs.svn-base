using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;
using HLP.bel;
using System.Text.RegularExpressions;
using ComponentFactory.Krypton.Toolkit;
using HLP.bel.CTe;


//Danner - o.s. 24184 - 25/02/2010
namespace NfeGerarXml
{

    public partial class frmPesquisaNfe : KryptonForm
    {
        private string _campoPesquisa = "nf.cd_notafis";
        private string _sEmp;
        public belCampoPesquisa objbelCampoPesquisa { get; set; }
        belConnection cx = new belConnection();

        /// <summary>
        /// Pesquisa Notas para Referenciar
        /// </summary>
        /// <param name="sEmp">Cod Empresa</param>       
        public frmPesquisaNfe(string sEmp)
        {
            _sEmp = sEmp;
            InitializeComponent();
            //cbxTipoNf.SelectedIndex = iTipPesquisa;
        }
        private void PesquisaNF_Load(object sender, EventArgs e)
        {
            cbxCampoPesquisa.SelectedIndex = 0;
            cbxOperador.SelectedIndex = 0;
            cbxModelo.SelectedIndex = 0;
            cbxTipoNf.SelectedIndex = 0;

            dgvPesquisa.Columns["cUF"].Visible = false;
            dgvPesquisa.Columns["AAMM"].Visible = false;
            dgvPesquisa.Columns["CNPJ"].Visible = false;
            dgvPesquisa.Columns["serie"].Visible = false;
        }
        private void cbxCampoPesquisa_SelectionChangeCommitted(object sender, EventArgs e)
        {
            lblCampoValor.Text = cbxCampoPesquisa.Text + ":";

            switch (cbxCampoPesquisa.SelectedIndex)
            {
                case 0:
                    {
                        txtPesquisa.MaxLength = 7;
                        _campoPesquisa = "nf.cd_notafis";
                        break;
                    }
                case 1:
                    {
                        txtPesquisa.MaxLength = 7;
                        _campoPesquisa = "nf.cd_nfseq";
                        break;
                    }
                case 2:
                    {
                        txtPesquisa.MaxLength = 44;
                        _campoPesquisa = " nf.cd_chavenfe";
                        break;
                    }
            }

        }

        private void cbxCampoPesquisaEntrada_SelectionChangeCommitted(object sender, EventArgs e)
        {
            lblCampoValor.Text = cbxCampoPesquisaEntrada.Text + ":";

            switch (cbxCampoPesquisaEntrada.SelectedIndex)
            {
                case 0:
                    {
                        txtPesquisa.MaxLength = 10;
                        _campoPesquisa = " movensai.cd_doc ";
                        break;
                    }
                case 1:
                    {
                        txtPesquisa.MaxLength = 9;
                        _campoPesquisa = " movensai.cd_mov ";
                        break;
                    }
                case 2:
                    {
                        txtPesquisa.MaxLength = 44;
                        _campoPesquisa = " movensai.cd_chave_nfe ";
                        break;
                    }
            }

        }

        private void Pesquisar()
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                if (txtPesquisa.Text == "")
                {
                    KryptonMessageBox.Show(lblCampoValor.Text + " está Vazio");
                }
                else if (cbxCampoPesquisa.SelectedIndex == -1)
                {
                    KryptonMessageBox.Show("Campo de Pesquisa está vazio");
                }
                else if (cbxOperador.SelectedIndex == -1)
                {
                    KryptonMessageBox.Show("Campo Operador esta Vazio");
                }
                else
                {
                    sSql.Append("select ");
                    sSql.Append("nf.cd_nfseq NFSEQ, ");
                    sSql.Append("nf.cd_notafis nNF, ");
                    sSql.Append("nf.cd_chavenfe CHAVE, ");
                    sSql.Append("nf.nm_clifornor, ");
                    sSql.Append("nf.cd_ufnor cUF, ");
                    sSql.Append("nf.dt_emi AAMM, ");
                    sSql.Append(" nf.cd_cgc CNPJ, ");
                    sSql.Append("coalesce(nf.cd_serie,'') serie ");
                    sSql.Append("from nf ");
                    sSql.Append("where cd_empresa = '");
                    sSql.Append(_sEmp);
                    sSql.Append("' ");
                    sSql.Append("and ");
                    sSql.Append(_campoPesquisa);
                    switch (cbxOperador.SelectedIndex)
                    {
                        case 0:
                            {
                                sSql.Append(" = ");
                                sSql.Append("'");
                                sSql.Append(txtPesquisa.Text.Trim());
                                break;
                            }
                        case 1:
                            {
                                sSql.Append(" > ");
                                sSql.Append("'");
                                sSql.Append(txtPesquisa.Text.Trim());
                                break;
                            }
                        case 2:
                            {
                                sSql.Append(" >= ");
                                sSql.Append("'");
                                sSql.Append(txtPesquisa.Text.Trim());
                                break;
                            }
                        case 3:
                            {
                                sSql.Append(" < ");
                                sSql.Append("'");
                                sSql.Append(txtPesquisa.Text.Trim());
                                break;
                            }
                        case 4:
                            {
                                sSql.Append(" <= ");
                                sSql.Append("'");
                                sSql.Append(txtPesquisa.Text.Trim());
                                break;
                            }
                        case 5:
                            {
                                sSql.Append(" like ");
                                sSql.Append("'%");
                                sSql.Append(txtPesquisa.Text.Trim());
                                sSql.Append("%");
                                break;
                            }
                    }
                    sSql.Append("' ");
                    if (cbxModelo.SelectedIndex == 0)
                    {
                        sSql.Append("and  ");
                        sSql.Append("nf.st_nfe = 'S'");
                    }

                    FbCommand cmd = new FbCommand(sSql.ToString(), cx.get_Conexao());
                    cx.Open_Conexao();
                    cmd.ExecuteNonQuery();

                    FbDataReader dr = cmd.ExecuteReader();
                    List<belCampoPesquisa> lCampoPesquisa = new List<belCampoPesquisa>();

                    while (dr.Read())
                    {
                        belCampoPesquisa objCampo = new belCampoPesquisa();
                        objCampo.ChaveAcesso = dr["CHAVE"].ToString();
                        objCampo.NumeroNF = dr["nNF"].ToString();
                        objCampo.SeqNF = dr["NFSEQ"].ToString();
                        objCampo.sCli_For = dr["nm_clifornor"].ToString();
                        objCampo.cUF = dr["cUF"].ToString();
                        objCampo.AAMM = dr["AAMM"].ToString();
                        objCampo.CNPJ = dr["CNPJ"].ToString();
                        objCampo.serie = dr["serie"].ToString();
                        lCampoPesquisa.Add(objCampo);

                    }
                    dgvPesquisa.DataSource = lCampoPesquisa;
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(ex.Message); ;
            }
            finally
            {
                cx.Close_Conexao();
            }
        }

        private void PesquisarEntrada()
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                if (txtPesquisa.Text == "")
                {
                    KryptonMessageBox.Show(lblCampoValor.Text + " está Vazio");
                }
                else if (cbxCampoPesquisa.SelectedIndex == -1)
                {
                    KryptonMessageBox.Show("Campo de Pesquisa está vazio");
                }
                else if (cbxOperador.SelectedIndex == -1)
                {
                    KryptonMessageBox.Show("Campo Operador esta Vazio");
                }
                else
                {
                    sSql.Append("select ");
                    sSql.Append("movensai.cd_doc nNF, ");
                    sSql.Append("movensai.cd_mov Lancamento, ");
                    sSql.Append("movensai.cd_chave_nfe CHAVE, ");
                    sSql.Append("clifor.nm_clifor  nm_clifornor, ");
                    sSql.Append("clifor.cd_ufnor cUF, ");
                    sSql.Append("movensai.dt_emi AAMM, ");
                    sSql.Append("clifor.cd_cgc CNPJ, ");
                    sSql.Append("movensai.cd_serienf serie ");
                    sSql.Append("from movensai  inner join clifor on movensai.cd_clifor = clifor.cd_clifor ");
                    sSql.Append("where movensai.cd_empresa = '");
                    sSql.Append(_sEmp);
                    sSql.Append("' ");
                    sSql.Append("and ");
                    sSql.Append(_campoPesquisa);
                    switch (cbxOperador.SelectedIndex)
                    {
                        case 0:
                            {
                                sSql.Append(" = ");
                                sSql.Append("'");
                                sSql.Append(txtPesquisa.Text.Trim());
                                break;
                            }
                        case 1:
                            {
                                sSql.Append(" > ");
                                sSql.Append("'");
                                sSql.Append(txtPesquisa.Text.Trim());
                                break;
                            }
                        case 2:
                            {
                                sSql.Append(" >= ");
                                sSql.Append("'");
                                sSql.Append(txtPesquisa.Text.Trim());
                                break;
                            }
                        case 3:
                            {
                                sSql.Append(" < ");
                                sSql.Append("'");
                                sSql.Append(txtPesquisa.Text.Trim());
                                break;
                            }
                        case 4:
                            {
                                sSql.Append(" <= ");
                                sSql.Append("'");
                                sSql.Append(txtPesquisa.Text.Trim());
                                break;
                            }
                        case 5:
                            {
                                sSql.Append(" like ");
                                sSql.Append("'%");
                                sSql.Append(txtPesquisa.Text.Trim());
                                sSql.Append("%");
                                break;
                            }
                    }

                    sSql.Append("' ");
                    if (cbxModelo.SelectedIndex == 0)
                    {
                        sSql.Append("and  ");
                        sSql.Append("movensai.cd_chave_nfe is not null ");
                    }

                    FbCommand cmd = new FbCommand(sSql.ToString(), cx.get_Conexao());
                    cx.Open_Conexao();
                    cmd.ExecuteNonQuery();

                    FbDataReader dr = cmd.ExecuteReader();
                    List<belCampoPesquisa> lCampoPesquisa = new List<belCampoPesquisa>();

                    while (dr.Read())
                    {
                        belCampoPesquisa objCampo = new belCampoPesquisa();
                        objCampo.ChaveAcesso = dr["CHAVE"].ToString();
                        objCampo.NumeroNF = dr["nNF"].ToString();
                        objCampo.SeqNF = dr["Lancamento"].ToString();
                        objCampo.sCli_For = dr["nm_clifornor"].ToString();
                        objCampo.cUF = dr["cUF"].ToString();
                        objCampo.AAMM = dr["AAMM"].ToString();
                        objCampo.CNPJ = dr["CNPJ"].ToString();
                        objCampo.serie = dr["serie"].ToString();
                        lCampoPesquisa.Add(objCampo);

                    }
                    dgvPesquisa.DataSource = lCampoPesquisa;
                }
            }
            catch (Exception ex)
            {

                KryptonMessageBox.Show(ex.Message); ;
            }
            finally
            {
                cx.Close_Conexao();
            }
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            if (cbxTipoNf.SelectedIndex == 0)
            {
                Pesquisar();
            }
            else
            {
                PesquisarEntrada();
            }



        }
        private void dgvPesquisa_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex > -1)
            {
                objbelCampoPesquisa = new belCampoPesquisa();
                objbelCampoPesquisa = objbelCampoPesquisa.RetornaCampoSelecionado((dgvPesquisa.DataSource as List<belCampoPesquisa>), dgvPesquisa[1, e.RowIndex].Value.ToString());
                this.Close();
            }

        }
        private void txtPesquisa_Validated(object sender, EventArgs e)
        {
            switch (cbxCampoPesquisa.SelectedIndex)
            {
                case 0:
                    {
                        if (cbxTipoNf.SelectedIndex == 0)
                        {
                            txtPesquisa.Text = txtPesquisa.Text.PadLeft(6, '0');
                        }
                        break;
                    }
                case 1:
                    {
                        if (cbxTipoNf.SelectedIndex == 0)
                        {
                            txtPesquisa.Text = txtPesquisa.Text.PadLeft(6, '0');
                        }
                        else
                        {
                            txtPesquisa.Text = txtPesquisa.Text.PadLeft(7, '0');
                        }
                        break;
                    }
                case 2:
                    {
                        txtPesquisa.Text = txtPesquisa.Text.PadLeft(44, '0');
                        break;
                    }
            }
        }

        private void txtPesquisa_Validating(object sender, CancelEventArgs e)
        {
            if (txtPesquisa.Text != "")
            {
                Regex m = new Regex("^[0-9]+$");
                if (!m.IsMatch(txtPesquisa.Text))
                {
                    KryptonMessageBox.Show("Campo Numérico, não é aceito letras");
                    e.Cancel = true;
                }
            }
        }

        private void txtPesquisa_Enter(object sender, EventArgs e)
        {
            txtPesquisa.SelectionLength = txtPesquisa.Text.Length;

        }

        private void cbxModelo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxModelo.SelectedIndex == 0)
            {
                dgvPesquisa.Columns["ChaveAcesso"].Visible = true;
            }
            else
            {
                dgvPesquisa.Columns["ChaveAcesso"].Visible = false;
            }
        }

        private void cbxTipoNf_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxTipoNf.SelectedIndex == 0) // Saída
            {
                cbxCampoPesquisa.Enabled = true;
                cbxCampoPesquisaEntrada.Enabled = false;
                cbxCampoPesquisa.SelectedIndex = 0;
                cbxCampoPesquisa_SelectionChangeCommitted(sender, e);
                dgvPesquisa.Columns["SeqNF"].HeaderText = "Sequencia";

            }
            else //Entrada
            {
                cbxCampoPesquisa.Enabled = false;
                cbxCampoPesquisaEntrada.Enabled = true;
                cbxCampoPesquisaEntrada.SelectedIndex = 0;
                cbxCampoPesquisaEntrada_SelectionChangeCommitted(sender, e);
                dgvPesquisa.Columns["SeqNF"].HeaderText = "Lançamento";
            }
        }

        private void label3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
//Fim - Danner - o.s. 24184 - 25/02/2010
