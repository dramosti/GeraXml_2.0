using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;
using HLP.bel;
using NfeGerarXml.NFes;
using ComponentFactory.Krypton.Toolkit;
using HLP.bel.Static;
using HLP.bel.CTe;
using HLP.bel.NFe;


namespace NfeGerarXml
{

    public partial class frmGerarNumeroNfe : KryptonForm
    {
        public string psEmp { get; set; }
        public string psNM_Cliente { get; set; }
        public string psNM_Banco { get; set; }
        public List<string> plNotas { get; set; }
        public bool bValida = true;
        frmArquivosXmlNfe objfrm = null;
        frmEnviaNfs objfrmNFes = null;
        belRegras objbelRegras = new belRegras();
        belConnection cx = new belConnection();

        public frmGerarNumeroNfe(frmArquivosXmlNfe objfrm)
        {
            InitializeComponent();
            belStatic.bNotaServico = false;
            this.objfrm = objfrm;
            psEmp = belStatic.codEmpresaNFe;
        }
        public frmGerarNumeroNfe(frmEnviaNfs objfrmNFes)
        {
            InitializeComponent();
            this.Text = "Geração do Número de RPS";
            grdNumeroUltNF.Text = "Número do último RPS";
            grdNumeroASerEmi.Text = "Número do próximo RPS";
            btnGerar.Text = "Gerar Número de RPS";
            belStatic.bNotaServico = true;
            this.objfrmNFes = objfrmNFes;
            psEmp = (belStatic.bNotaServico == true ? belStatic.codEmpresaNFe : belStatic.codEmpresaNFe);
        }

        private void frmGerarNumeroNF_Load(object sender, EventArgs e)
        {
            GeraNumeroNF();
        }

        private void cmbGF_Validated(object sender, EventArgs e)
        {
            GeraNumeroNF();
        }

        public void GeraNumeroNF()
        {
            try
            {

                StringBuilder sSql = new StringBuilder();
                sSql.Append("Select ");
                sSql.Append("Distinct coalesce(nf.cd_gruponf, '') GrupoNF ");
                sSql.Append("From NF ");
                sSql.Append("Where ");
                sSql.Append("(nf.cd_empresa ='");
                sSql.Append(belStatic.codEmpresaNFe);
                sSql.Append("') ");
                sSql.Append("order by coalesce(nf.cd_gruponf, '') ");

                FbCommand cmdGF = new FbCommand(sSql.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                FbDataReader drGF = cmdGF.ExecuteReader();
                this.cmbGF.Items.Clear();
                while (drGF.Read())
                {
                    this.cmbGF.Items.Add(drGF["GrupoNF"].ToString());
                }
                cmbGF.Text = objbelRegras.retGrupoFaturamento(plNotas[0]);

                belProcessaNF BuscaNumeroNF = new belProcessaNF();
                BuscaNumeroNF.pConn = cx.get_Conexao();
                BuscaNumeroNF.psNM_Banco = this.psNM_Banco;
                BuscaNumeroNF.psNM_Cliente = this.psNM_Cliente;
                txtNumeroUltNF.Text = BuscaNumeroNF.GerarNumeroNF(psEmp, cmbGF.Text).PadLeft(6, '0');
                int iNumeroASerEmi = Convert.ToInt32(txtNumeroUltNF.Text); //Claudinei - o.s. 23971 - 07/01/2010
                iNumeroASerEmi++;
                txtNumeroASerEmi.Text = iNumeroASerEmi.ToString().PadLeft(6, '0');
            }
            catch (Exception)
            {

                throw;
            }
            finally { cx.Close_Conexao(); }
        }

        private void btnGerar_Click(object sender, EventArgs e)
        {
            try
            {
                belNumeroNF objNumeroNF = new belNumeroNF();

                List<belNumeroNF> objNumeroNfs = new List<belNumeroNF>();
                objNumeroNfs = objNumeroNF.GeraNumeroNF(plNotas, txtNumeroASerEmi.Text, psEmp);

                //Verifica se a as notas já não foram enviadas
                if (objNumeroNfs.Count == 0)
                {

                    throw new Exception("Selecione "
                        + (objNumeroNfs.Count > 1 ? "as " : "a ")
                        + (objNumeroNfs.Count > 1 ? "Notas " : "Nota ") + " Novamente pois já "
                        + (objNumeroNfs.Count > 1 ? "existem " : "existe ")
                        + (objNumeroNfs.Count > 1 ? "Numerações " : "Numeração ")
                        + (objNumeroNfs.Count > 1 ? "Geradas " : "Gerada ")
                        + " ou o número a ser emitido já foi utilizado!!");
                }

                //Verifica se Já não existe no banco a mesma numeração;              
                string sNumNotasInvalidas = objbelRegras.VerificaNumeracaoExistente(objNumeroNfs);

                if (sNumNotasInvalidas == "")
                {

                    string sSqlAtualizaNF = string.Empty;
                    //DataGeneric AcessoDados = new DataGeneric();

                    for (int i = 0; i < objNumeroNfs.Count; i++)
                    {
                        objbelRegras.AlteraDuplicatasNfe(objNumeroNfs, ref sSqlAtualizaNF, i);

                        if (HLP.bel.Static.belStatic.sNomeEmpresa.Equals("LORENZON"))
                        {
                            HLP.bel.NFe.belLorenzon objLorenzon = new HLP.bel.NFe.belLorenzon();
                            objLorenzon.AlteraDuplicataLorenzon(objNumeroNfs, i);
                        }

                        //Claudinei - o.s. 23929 - 10/12/2009
                        string sFiltro = string.Empty;
                        sFiltro = objbelRegras.OperacoesValidas(psEmp, objNumeroNfs[i].Nfseq);
                        bool bHe131 = false; ;

                        if (sFiltro.IndexOf("TIPO131") == -1)
                        {
                            sFiltro = objbelRegras.MontaFiltroOperacoesValidas(sFiltro);
                            bHe131 = false;
                        }
                        else
                        {
                            sFiltro = objbelRegras.MontaFiltroOperacoesValidas(sFiltro.Replace("TIPO131", ""));
                            bHe131 = true;

                        }
                        if (bHe131)
                        {
                            sSqlAtualizaNF = "update movitem set DT_DOC = '" + HLP.Util.Util.GetDateServidor().ToString("dd.MM.yyyy") +
                                                                 "' where cd_empresa = '" + psEmp +
                                                                 "' and cd_nfseq = '" + objNumeroNfs[i].Nfseq + "'" + sFiltro;
                        }
                        else
                        {
                            sSqlAtualizaNF = "update movitem set cd_doc = '" + objNumeroNfs[i].Cdnotafis +
                                             "', DT_DOC = '" + HLP.Util.Util.GetDateServidor().ToString("dd.MM.yyyy") +
                                             "' where cd_empresa = '" + psEmp +
                                             "' and cd_nfseq = '" + objNumeroNfs[i].Nfseq + "'" + sFiltro;
                        }

                        FbCommand cmdUpdatemovitem = new FbCommand(sSqlAtualizaNF, cx.get_Conexao());
                        cx.Open_Conexao();
                        cmdUpdatemovitem.ExecuteNonQuery();
                        cx.Close_Conexao();
                        //Fim - Claudinei - o.s. 23929 - 10/12/2009

                        belDuplicata objDuplicata = new belDuplicata();
                        objDuplicata.Empresa = psEmp;
                        objDuplicata.Nrseq = objNumeroNfs[i].Nfseq;
                        objDuplicata.Cdnotafis = objNumeroNfs[i].Cdnotafis;
                        objDuplicata.Dtemi = dtpEmissao.Value;
                        objDuplicata.Conn = cx.get_Conexao();

                        objDuplicata.BuscaVencto();

                    }
                    KryptonMessageBox.Show(null, "Numeração das notas geradas com sucesso!", "Gerar Numeros de Notas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GeraNumeroNF();
                    this.Hide();
                    this.bValida = true;
                }
                else
                {
                    throw new Exception("Selecione "
                        + (objNumeroNfs.Count > 1 ? "as " : "a ")
                        + (objNumeroNfs.Count > 1 ? "Notas " : "Nota ") + " Novamente pois já "
                        + (objNumeroNfs.Count > 1 ? "existem " : "existe ")
                        + (objNumeroNfs.Count > 1 ? "Numerações " : "Numeração ")
                        + (objNumeroNfs.Count > 1 ? "Geradas " : "Gerada ")
                        + " ou o número a ser emitido já foi utilizado!!");
                }

            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, ex.Message, "A V I S O", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.bValida = false;
                this.Close();
            }
            finally
            {
                cx.Close_Conexao();
            }
        }





        private void cmbGF_TabIndexChanged(object sender, EventArgs e)
        {
            GeraNumeroNF();
        }


        private void frmGerarNumeroNF_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (objfrm != null)
            {
                objfrm.PopulaDataGridView();
            }
            else
            {
                objfrmNFes.PopulaDataGridView();
            }

        }
        //Fim - Danner - o.s. 24099 - 09/02/2010
    }

}
