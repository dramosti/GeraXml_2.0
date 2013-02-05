using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HLP.bel;
using System.IO;
using System.Collections;
using FirebirdSql.Data.FirebirdClient;
using ComponentFactory.Krypton.Toolkit;
using HLP.bel.NFe.GeraXml;

namespace NfeGerarXml
{
    public partial class frmImportaEscritorNfe : KryptonForm
    {
        struct strucArquivo
        {
            public string Arquivo { get; set; }
            public String NFE { get; set; }
            public string Nota { get; set; }
            public string Emitente { get; set; }
            public string Destinatario { get; set; }
            public DateTime Emissao { get; set; }
        }
        struct strucXmlValidacao
        {
            public string Xml { get; set; }
            public string Motivo { get; set; }
        }

        bool bMarcar = false;

        public FbConnection fbConexao { get; set; }

        public frmImportaEscritorNfe()
        {
            InitializeComponent();
        }

        public FbConnection MontaConexaoEscritor()
        {
            FbConnection Conn = new FbConnection();
            try
            {

                Globais MontaStringConexao = new Globais();
                StringBuilder sbConexao = new StringBuilder();

                sbConexao.Append("User =");
                sbConexao.Append(MontaStringConexao.LeRegConfig("Usuario"));
                sbConexao.Append(";");
                sbConexao.Append("Password=");
                sbConexao.Append(MontaStringConexao.LeRegConfig("Senha"));
                sbConexao.Append(";");
                sbConexao.Append("Database=");
                string sdatabase = MontaStringConexao.LeRegConfig("BancoEscritor");
                sbConexao.Append(sdatabase);
                sbConexao.Append(";");
                sbConexao.Append("DataSource=");
                sbConexao.Append(MontaStringConexao.LeRegConfig("ServidorEscritor"));
                sbConexao.Append(";");
                sbConexao.Append("Port=3050;Dialect=1; Charset=NONE;Role=;Connection lifetime=15;Pooling=true; MinPoolSize=0;MaxPoolSize=50;Packet Size=8192;ServerType=0;");


                Conn = new FbConnection(sbConexao.ToString());
                Conn.Open();
                return Conn;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possivel se conectar ao banco de dados do Escritor, Verifique as Configurações do Sistema, Erro.: {0}",
                                                  ex.Message));
            }
            finally
            {
                Conn.Close();
            }

            return Conn;
        }

        private void frmImportaEscritor_Load(object sender, EventArgs e)
        {


            belInfNFe objInfNFe = new belInfNFe();
            belConnection cx = new belConnection();
            fbConexao = cx.get_Conexao();

            belEscrituracao objEscrituracao = new belEscrituracao(objInfNFe, fbConexao);

            cbxEmpresas.DisplayMember = "Descricao";
            cbxEmpresas.ValueMember = "Codigo";

            cbxEmpresas.DataSource = objEscrituracao.RetornaEmpresa();

        }




        private List<strucArquivo> MontaStrucArquivos(string[] Arquivos)
        {
            List<strucArquivo> objArquivos = new List<strucArquivo>();
            List<strucXmlValidacao> objLisXmlNaoValidado = new List<strucXmlValidacao>();

            string sNomearq = string.Empty;

            try
            {
                string sDoc = "";

                foreach (string sArquivo in Arquivos)
                {
                    strucArquivo objArquivo = new strucArquivo();
                    objArquivo.Arquivo = sArquivo.ToString();
                    sNomearq = sArquivo;
                    belImportaXmlNFe xmlEscritor = new belImportaXmlNFe(sNomearq);
                    belEscrituracao objbelEscrituracao = new belEscrituracao(fbConexao);
                    string sCD_EMPRESA = cbxEmpresas.SelectedValue.ToString();
                    string sCD_CLIFOR = "";
                    string sTipo = "";
                    bool bValida = true;
                    try
                    {
                        belInfNFe objInfNFe = xmlEscritor.MontaInfNFe();


                        if (objInfNFe != null)
                        {
                            string sTipoLanc = TipoLancamento(objInfNFe);
                            bool bSaida = true; //  NotaSaida();
                            if (sTipoLanc == "E")
                            {
                                bSaida = false;
                            }
                            sDoc = "";

                            if (!bSaida)
                            {
                                if (objInfNFe.BelEmit.Cnpj != null)
                                {
                                    sDoc = objInfNFe.BelEmit.Cnpj.ToString();
                                }
                                else
                                {
                                    sDoc = objInfNFe.BelEmit.Cpf.ToString();
                                }
                            }
                            else
                            {
                                if (objInfNFe.BelDest.Cnpj != null)
                                {
                                    sDoc = objInfNFe.BelDest.Cnpj.ToString();
                                }
                                else
                                {
                                    sDoc = objInfNFe.BelDest.Cpf.ToString();
                                }
                            }

                            sTipo = (objInfNFe.BelDest.Cnpj != null ? "CNPJ" : "CPF");
                            sCD_CLIFOR = objbelEscrituracao.BuscaCodigoClifor(objbelEscrituracao.FormataString(sDoc, sTipo), objInfNFe, bSaida);
                            if (sCD_CLIFOR != "")
                            {
                                
                               // if (bSaida)
                                {
                                    bValida = objbelEscrituracao.ValidaNotaJaEscriturada(sCD_EMPRESA, objInfNFe.BelIde.Nnf,
                                       objInfNFe.BelIde.Serie,
                                       sCD_CLIFOR,
                                       objInfNFe.BelIde.Mod,
                                       bSaida
                                       );
                                }
                            }

                            if (bValida)
                            {
                                objArquivo.Nota = objInfNFe.BelIde.Nnf;
                                objArquivo.Emitente = objInfNFe.BelEmit.Xnome;
                                objArquivo.Destinatario = objInfNFe.BelDest.Xnome;
                                objArquivo.Emissao = objInfNFe.BelIde.Demi;
                                objArquivo.NFE = objInfNFe.Id;

                                objArquivos.Add(objArquivo);
                            }
                        }
                        else
                        {
                            strucXmlValidacao obj = new strucXmlValidacao();
                            obj.Xml = sNomearq;
                            obj.Motivo = "Não foi encontrado a Tag 'protNFe'.  XML NÃO É VÁLIDO!! ";
                            objLisXmlNaoValidado.Add(obj);
                        }
                    }
                    catch (Exception ex)
                    {
                        strucXmlValidacao obj = new strucXmlValidacao();
                        obj.Xml = sNomearq;
                        obj.Motivo = ex.Message;
                        objLisXmlNaoValidado.Add(obj);
                    }
                }

            }
            catch (Exception ex)
            {
                //throw new Exception(string.Format("Não foi possivel ler o arquivo {0}, devido ao Erro.: {1}",
                //                                  sNomearq,
                //                                  ex.Message));
                strucXmlValidacao obj = new strucXmlValidacao();
                obj.Xml = sNomearq;
                obj.Motivo = ex.Message;// "Não foi encontrado a Tag 'protNFe'.  XML NÃO É VÁLIDO!! ";
                objLisXmlNaoValidado.Add(obj);
            }
            dgvXmlNaoValidado.DataSource = objLisXmlNaoValidado;
            dgvXmlNaoValidado.Columns[0].Width = 500;
            dgvXmlNaoValidado.Columns[1].Width = 200;
            if (objLisXmlNaoValidado.Count > 0)
            {
                lblXmlNaoValidado.Text = "| " + objLisXmlNaoValidado.Count.ToString() + " registros não válidos para Escrituração";
            }
            else
            {
                lblXmlNaoValidado.Text = "";
            }

            return objArquivos;
        }


        public string TipoLancamento(belInfNFe objInfNFe)
        {
            try
            {
                string sTipoLancamento = string.Empty;
                string sCNPJempresa = "";
                bool bProdutorRural = false;
                using (FbCommand cmd = new FbCommand("select cd_cgc from empresa where cd_empresa ='" + cbxEmpresas.SelectedValue.ToString() + "'", fbConexao))
                {
                    if (fbConexao.State != ConnectionState.Open)
                    {
                        fbConexao.Open();
                    }
                    GeraXMLExp objGerarXMLExp = new GeraXMLExp();
                    sCNPJempresa = belUtil.TiraSimbolo(cmd.ExecuteScalar().ToString(), "");
                    fbConexao.Close();
                }

                //--->  0-Entrada / 1-Saída


                if (objInfNFe.BelIde.Tpnf == "0")
                {
                    sTipoLancamento = "E";
                    if (objInfNFe.BelEmit.Cnpj != null)
                    {
                        if (objInfNFe.BelEmit.Cnpj.ToString() == sCNPJempresa)
                        {
                            bProdutorRural = true;
                        }
                    }
                    else
                    {
                        string sInstrucao = string.Empty;
                        sInstrucao = "select cd_cpf from empresa where cd_empresa ='" + objInfNFe.Empresa + "'";
                        using (FbCommand cmd = new FbCommand(sInstrucao, fbConexao))
                        {
                            if (fbConexao.State != ConnectionState.Open)
                            {
                                fbConexao.Open();
                            }
                            GeraXMLExp objGerarXMLExp = new GeraXMLExp();

                            string sCPF = belUtil.TiraSimbolo(cmd.ExecuteScalar().ToString(), "");

                            if (sCPF == objInfNFe.BelEmit.Cpf.ToString())
                            {
                                sCPF = objInfNFe.BelEmit.Cpf;
                                bProdutorRural = true;
                            }
                        }
                    }
                }
                else
                {

                    if (objInfNFe.BelEmit.Cnpj != null)
                    {
                        if (objInfNFe.BelEmit.Cnpj.ToString() == sCNPJempresa)
                        {
                            sTipoLancamento = "S";
                        }
                    }
                    if (objInfNFe.BelDest.Cnpj != null)
                    {
                        if (objInfNFe.BelDest.Cnpj.ToString() == sCNPJempresa)
                        {
                            sTipoLancamento = "E";
                        }
                    }
                }
                return sTipoLancamento;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possivel definir o Tipo de Lançamento, Erro.: {0}",
                                                   ex.Message));
            }
        }



        private void dgvXmls_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (((e.ColumnIndex == 0)) && (dgvXmls.DataSource != null))
            {
                if (bMarcar == false)
                {
                    MarcadesmarcaTodos(true, e.ColumnIndex);
                    bMarcar = true;
                    SendKeys.Send("{ENTER}");
                }
                else
                {
                    MarcadesmarcaTodos(false, e.ColumnIndex);
                    bMarcar = false;
                    SendKeys.Send("{ENTER}");
                }
            }

        }

        private void MarcadesmarcaTodos(bool Marca, int coluna)
        {
            for (int i = 0; i < dgvXmls.RowCount; i++)
            {
                dgvXmls.Rows[i].Cells[coluna].Value = Marca;
            }

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (fbdImportar.ShowDialog() == DialogResult.OK)
            {
                txtXml.Text = fbdImportar.SelectedPath;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            lblarquivosEscriturados.Text = "";
            List<strucArquivo> objArquivos = new List<strucArquivo>();
            try
            {
                if (txtXml.Text == "")
                {
                    throw new Exception("Arquivo XML não foi selecionado !");
                }

                if (cbxEmpresas.SelectedValue.ToString() == "")
                {
                    throw new Exception("Empresa para Importção não foi selecionada !");
                }

                string[] Arquivos = Directory.GetFiles(@txtXml.Text, "*.xml");

                objArquivos = MontaStrucArquivos(Arquivos);
                lblStatusScrituracao.Text = objArquivos.Count.ToString() + "  registros validos";

            }
            catch (Exception ex)
            {

                KryptonMessageBox.Show(ex.Message);
            }

            dgvXmls.DataSource = objArquivos;
            for (int i = 0; i < dgvXmls.RowCount; i++)
            {
                dgvXmls[0, i].Value = false;
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            string sArquivoXML = string.Empty;
            try
            {
                if (txtXml.Text == "")
                {
                    throw new Exception("Arquivo XML não foi selecionado !");
                }

                if (cbxEmpresas.SelectedValue.ToString() == "")
                {
                    throw new Exception("Empresa para Importção não foi selecionada !");
                }

                List<string> lsXmls = new List<string>();

                for (int i = 0; i < dgvXmls.RowCount; i++)
                {
                    if (((dgvXmls["Selecionar", i].Value != null) && (dgvXmls["Selecionar", i].Value.ToString().Equals("True"))))
                    {
                        lsXmls.Add((string)dgvXmls["Arquivo", i].Value);
                    }

                }

                if (lsXmls.Count > 0)
                {
                    pgbNF.Step = 1;
                    pgbNF.Minimum = 0;
                    pgbNF.Maximum = lsXmls.Count;
                    pgbNF.MarqueeAnimationSpeed = lsXmls.Count;
                    pgbNF.Value = 0;

                    for (int i = 0; i < lsXmls.Count; i++)
                    {
                        try
                        {
                            lblStatusScrituracao.Text = (i + 1).ToString() + " de " + lsXmls.Count.ToString();
                            pgbNF.PerformStep();
                            statusStrip1.Refresh();
                            this.Refresh();
                            sArquivoXML = lsXmls[i];
                            belImportaXmlNFe xmlEscritor = new belImportaXmlNFe(lsXmls[i]);
                            belInfNFe objInfNFe = xmlEscritor.MontaInfNFe();
                            objInfNFe.Empresa = cbxEmpresas.SelectedValue.ToString();
                            belEscrituracao objEscrituracao = new belEscrituracao(objInfNFe, fbConexao);
                        }
                        catch (Exception ex)
                        {
                            KryptonMessageBox.Show(null, "Ocorreu uma excessão não esperada, Verifique a Messagem abaixo e Informe o Suporte." + Environment.NewLine + Environment.NewLine + ex.Message, "E R R 0", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    KryptonMessageBox.Show(null,
                                    string.Format("Importação efetuada com Sucesso!"),
                                    "Importação de XML",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    pgbNF.Value = 0;
                    //toolStripButton2_Click(sender, e);
                    lblarquivosEscriturados.Text = "Escriturado " + lsXmls.Count.ToString() + " registro(s) de " + lsXmls.Count.ToString();



                }
                else
                {
                    KryptonMessageBox.Show(null,
                                    string.Format("Nenhum XML Foi selecionado!"),
                                    "Importação de XML",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                }




            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null,
                                string.Format("Não foi possivel importar o XML. Erro {0}, Lendo o Arquivo {1}",
                                              ex.Message,
                                              sArquivoXML),
                                "Erro",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvXmls_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex > -1) && (e.ColumnIndex == 0))
            {
                if (dgvXmls[0, e.RowIndex].Value.ToString() == "False")
                {
                    dgvXmls[0, e.RowIndex].Value = true;
                    SendKeys.Send("{right}");
                    SendKeys.Send("{left}");

                }
                else
                {
                    dgvXmls[0, e.RowIndex].Value = false;
                    SendKeys.Send("{right}");
                    SendKeys.Send("{left}");
                    //SendKeys.Send("{LEFT}");
                }
            }


        }

        private void label1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void selecionarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SelecionaRows(true);
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(ex.Message);
            }
        }

        private void SelecionaRows(bool bseleciona)
        {
            foreach (DataGridViewRow row in dgvXmls.SelectedRows)
            {
                row.Cells["Selecionar"].Value = bseleciona;
            }
            dgvXmls.Refresh();
        }


    }
}
