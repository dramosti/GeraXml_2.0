using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ImportacaoClientes.bel;
using ImportacaoClientes.dao;

namespace ImportacaoClientes
{
    public partial class frmGeraArquivo : Form
    {
        daoCliente objdao = new daoCliente();
        List<Identificacao> objListIdent = new List<Identificacao>();
        List<belLogErro> objListErro = new List<belLogErro>();
        belConexão objBel = new belConexão();


        public frmGeraArquivo()
        {
            InitializeComponent();
            txtBancoDados.Focus();
        }

        private bool VerificaCampos()
        {
            errorProvider1.Dispose();
            bool Retorno = true;

            if (txtBancoDados.Text == "")
            {
                errorProvider1.SetError(txtBancoDados, "Campo Obrigatório!");
                Retorno = false;
            }
            if (txtServidor.Text == "")
            {
                errorProvider1.SetError(txtServidor, "Campo Obrigatório!");
                Retorno = false;
            }

            return Retorno;
        }


        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            if (tabClientes.SelectedIndex == 0)
            {
                FiltroPesquisaCliente();
            }
            else if (tabClientes.SelectedIndex == 1)
            {
                FiltroPesquisaErro();
            }

        }
        private void cboFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFiltro.Text = "";
        }

        private void FiltroPesquisaErro()
        {
            if (txtFiltro.Text != "" && cboFiltro.SelectedIndex != -1)
            {
                try
                {
                    if (objListErro.Count() > 0)
                    {
                        if (cboParametro.SelectedIndex != -1)
                        {
                            switch (cboParametro.SelectedIndex)
                            {

                                case 0: if (cboFiltro.SelectedIndex == 0)
                                    {
                                        gridErros.DataSource = objListErro.FindAll(C => C.CodCliente.ToString() == txtFiltro.Text);
                                    }
                                    else if (cboFiltro.SelectedIndex == 1)
                                    {
                                        gridErros.DataSource = objListErro.FindAll(C => C.Razao.ToUpper() == txtFiltro.Text.ToUpper());
                                    }
                                    break;


                                case 1: if (cboFiltro.SelectedIndex == 0)
                                    {
                                        gridErros.DataSource = objListErro.FindAll(C => C.CodCliente.ToString().Contains(txtFiltro.Text));
                                    }
                                    else if (cboFiltro.SelectedIndex == 1)
                                    {
                                        gridErros.DataSource = objListErro.FindAll(C => C.Razao.ToUpper().Contains(txtFiltro.Text.ToUpper()));
                                    }
                                    break;

                                case 2: if (cboFiltro.SelectedIndex == 0)
                                    {
                                        gridErros.DataSource = objListErro.FindAll(C => C.CodCliente.ToString().StartsWith(txtFiltro.Text));
                                    }
                                    else if (cboFiltro.SelectedIndex == 1)
                                    {
                                        gridErros.DataSource = objListErro.FindAll(C => C.Razao.ToUpper().StartsWith(txtFiltro.Text.ToUpper()));
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            switch (cboFiltro.SelectedIndex)
                            {
                                case 0: gridErros.DataSource = objListErro.FindAll(C => C.CodCliente.ToString().Contains(txtFiltro.Text));
                                    break;

                                case 1: gridErros.DataSource = objListErro.FindAll(C => C.Razao.ToUpper().Contains(txtFiltro.Text.ToUpper()));
                                    break;

                            }
                        }
                        lblErro.Text = "Total de Erros: " + gridErros.Rows.Count;

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(null, string.Format("Ocorreu uma Exceção ao Manipular essa Ação : {0}{0}Verifique a Mensagem abaixo: {0}________________________________{0}{0}",
                   Environment.NewLine) + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "AVISO ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }
        private void FiltroPesquisaCliente()
        {
            if (txtFiltro.Text != "" && cboFiltro.SelectedIndex != -1)
            {
                try
                {
                    if (objListIdent.Count() > 0)
                    {
                        if (cboParametro.SelectedIndex != -1)
                        {
                            switch (cboParametro.SelectedIndex)
                            {

                                case 0: if (cboFiltro.SelectedIndex == 0)
                                    {
                                        gridClientes.DataSource = objListIdent.FindAll(C => C.Cod_Cliente.ToString() == txtFiltro.Text);
                                    }
                                    else if (cboFiltro.SelectedIndex == 1)
                                    {
                                        gridClientes.DataSource = objListIdent.FindAll(C => C.DsRazaoSocial.ToUpper() == txtFiltro.Text.ToUpper());
                                    }
                                    else if (cboFiltro.SelectedIndex == 2)
                                    {
                                        gridClientes.DataSource = objListIdent.FindAll(C => C.Nome_Cidade.ToUpper() == txtFiltro.Text.ToUpper());
                                    }
                                    break;


                                case 1: if (cboFiltro.SelectedIndex == 0)
                                    {
                                        gridClientes.DataSource = objListIdent.FindAll(C => C.Cod_Cliente.ToString().Contains(txtFiltro.Text));
                                    }
                                    else if (cboFiltro.SelectedIndex == 1)
                                    {
                                        gridClientes.DataSource = objListIdent.FindAll(C => C.DsRazaoSocial.ToUpper().Contains(txtFiltro.Text.ToUpper()));
                                    }
                                    else if (cboFiltro.SelectedIndex == 2)
                                    {
                                        gridClientes.DataSource = objListIdent.FindAll(C => C.Nome_Cidade.ToUpper().Contains(txtFiltro.Text.ToUpper()));
                                    }

                                    break;

                                case 2: if (cboFiltro.SelectedIndex == 0)
                                    {
                                        gridClientes.DataSource = objListIdent.FindAll(C => C.Cod_Cliente.ToString().StartsWith(txtFiltro.Text));
                                    }
                                    else if (cboFiltro.SelectedIndex == 1)
                                    {
                                        gridClientes.DataSource = objListIdent.FindAll(C => C.DsRazaoSocial.ToUpper().StartsWith(txtFiltro.Text.ToUpper()));
                                    }
                                    else if (cboFiltro.SelectedIndex == 2)
                                    {
                                        gridClientes.DataSource = objListIdent.FindAll(C => C.Nome_Cidade.ToUpper().StartsWith(txtFiltro.Text.ToUpper()));
                                    }

                                    break;
                            }
                        }
                        else
                        {
                            switch (cboFiltro.SelectedIndex)
                            {
                                case 0: gridClientes.DataSource = objListIdent.FindAll(C => C.Cod_Cliente.ToString().Contains(txtFiltro.Text));
                                    break;

                                case 1: gridClientes.DataSource = objListIdent.FindAll(C => C.DsRazaoSocial.ToUpper().Contains(txtFiltro.Text.ToUpper()));
                                    break;

                                case 2: gridClientes.DataSource = objListIdent.FindAll(C => C.Nome_Cidade.ToUpper().Contains(txtFiltro.Text.ToUpper()));
                                    break;
                            }


                        }
                        lblClientes.Text = "Total de Clientes: " + gridClientes.Rows.Count;

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(null, string.Format("Ocorreu uma Exceção ao Manipular essa Ação : {0}{0}Verifique a Mensagem abaixo: {0}________________________________{0}{0}",
                   Environment.NewLine) + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "AVISO ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }



        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (VerificaCampos() == true)
            {
                try
                {
                    string sConexao = objBel.sMontaStringConexao(txtBancoDados.Text, txtServidor.Text);

                    objListIdent = objdao.RetornaTodosClientes(sConexao, progressBar1);
                    gridClientes.DataSource = objListIdent;
                    progressBar1.Value = 0;

                    if (objdao.objListBelErros.Count > 0)
                    {
                        objListErro = objdao.objListBelErros;
                        gridErros.DataSource = objListErro;
                        lblErro.Text = "Total de Erros: " + objdao.objListBelErros.Count();
                        lblErro.Visible = true;
                        MessageBox.Show("Alguns Clientes não podem ser incluídos pois contém erros no Cadastro!", "A V I S O", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }

                    lblClientes.Text = "Total de Clientes: " + objListIdent.Count;
                    lblClientes.Visible = true;

                    panelBanco.Enabled = false;
                    panelFiltro.Enabled = true;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(null, string.Format("Ocorreu uma Exceção ao Manipular essa Ação : {0}{0}Verifique a Mensagem abaixo: {0}________________________________{0}{0}",
                   Environment.NewLine) + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "AVISO ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }
        private void btnBancoDados_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtBancoDados.Text = openFileDialog1.FileName.ToString();
            }

        }
        private void btnGerarXml_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridClientes.Rows.Count > 0)
                {
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string sNomeArquivo = saveFileDialog1.FileName.ToString();
                        List<Identificacao> objList = new List<Identificacao>();

                        progressBar1.Maximum = gridClientes.Rows.Count;
                        for (int i = 0; i < gridClientes.Rows.Count; i++)
                        {
                            progressBar1.PerformStep();
                            Identificacao objIdentificacao = new Identificacao();

                            //Identificacao
                            objIdentificacao.CdConsumidor = gridClientes.Rows[i].Cells["CdConsumidor"].Value.ToString();
                            objIdentificacao.DsIm = gridClientes.Rows[i].Cells["DsIm"].Value.ToString();
                            objIdentificacao.DsRazaoSocial = gridClientes.Rows[i].Cells["DsRazaoSocial"].Value.ToString();
                            objIdentificacao.DsEmail = gridClientes.Rows[i].Cells["DsEmail"].Value.ToString();
                            objIdentificacao.CdTipoEmpresa = gridClientes.Rows[i].Cells["CdTipoEmpresa"].Value.ToString();

                            //Endereco
                            objIdentificacao.DsLogradouro = gridClientes.Rows[i].Cells["DsLogradouro"].Value.ToString();
                            objIdentificacao.DsNumero = gridClientes.Rows[i].Cells["DsNumero"].Value.ToString();
                            objIdentificacao.DsComplemento = gridClientes.Rows[i].Cells["DsComplemento"].Value.ToString();
                            objIdentificacao.DsBairro = gridClientes.Rows[i].Cells["DsBairro"].Value.ToString();
                            objIdentificacao.CdMunicipioIbge = gridClientes.Rows[i].Cells["CdMunicipioIbge"].Value.ToString();
                            objIdentificacao.CdUfIbge = gridClientes.Rows[i].Cells["CdUfIbge"].Value.ToString();
                            objIdentificacao.DsTelefone = gridClientes.Rows[i].Cells["DsTelefone"].Value.ToString();
                            objIdentificacao.DsContato = gridClientes.Rows[i].Cells["DsContato"].Value.ToString();
                            objIdentificacao.CdCepPrefixo = gridClientes.Rows[i].Cells["CdCepPrefixo"].Value.ToString();
                            objIdentificacao.CdCepSufixo = gridClientes.Rows[i].Cells["CdCepSufixo"].Value.ToString();
                            objIdentificacao.CdPais = gridClientes.Rows[i].Cells["CdPais"].Value.ToString();
                            objIdentificacao.DsTelefoneDdd = gridClientes.Rows[i].Cells["DsTelefoneDdd"].Value.ToString();
                            objIdentificacao.DsTelefoneDdi = gridClientes.Rows[i].Cells["DsTelefoneDdi"].Value.ToString();

                            objList.Add(objIdentificacao);
                        }
                        progressBar1.Value = 0;


                        bool bXmlCriado = objdao.CriarXml(objList, sNomeArquivo, progressBar1);

                        if (bXmlCriado == true)
                        {
                            MessageBox.Show(null, "Arquivo Gerado com Sucesso!", "A V I S O", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(null, "Arquivo não foi Gerado!", "A V I S O", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        progressBar1.Value = 0;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(null, string.Format("Ocorreu uma Exceção ao Manipular essa Ação : {0}{0}Verifique a Mensagem abaixo: {0}________________________________{0}{0}",
                                  Environment.NewLine) + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "AVISO ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void tabClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFiltro.Text = "";
            if (tabClientes.SelectedIndex == 0)
            {
                lblErro.Text = "Total de Erros: " + objListErro.Count();

                gridClientes.DataSource = objListIdent;
                if (!cboFiltro.Items.Contains("Cidade"))
                {
                    cboFiltro.Items.Add("Cidade");
                }
            }
            else if (tabClientes.SelectedIndex == 1)
            {
                lblClientes.Text = "Total de Clientes: " + objListIdent.Count;

                gridErros.DataSource = objListErro;
                if (cboFiltro.Items.Contains("Cidade"))
                {
                    cboFiltro.Items.Remove("Cidade");
                }
            }

        }



    }
}
