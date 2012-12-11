using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HLP.bel;
using System.Xml;
using FirebirdSql.Data.FirebirdClient;
using System.Security.Cryptography.X509Certificates;
using ComponentFactory.Krypton.Toolkit;
using HLP.bel.Static;
using HLP.bel.NFe.GeraXml;
using HLP.bel.CTe;

namespace NfeGerarXml
{
    public partial class frmInutilizaNFecs : KryptonForm
    {
        private int _tpamp;
        private string _cnpj;
        private string _emp;
        private string _mod;
        private string _cuf;
        private string _uf_empresa;
        private frmArquivosXmlNfe objfrmArquivos = null;
        belConnection cx = new belConnection();

        public frmInutilizaNFecs(string sEmp, string uf_empresa, frmArquivosXmlNfe objfrmArquivos)
        {
            _uf_empresa = uf_empresa;
            _emp = sEmp;
            _tpamp = belStatic.tpAmb;
            this.objfrmArquivos = objfrmArquivos;
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInutilizar_Click(object sender, EventArgs e)
        {
            XmlDocument xmldoc = new XmlDocument();
            try
            {


                if (txtNNFini.Text == "")
                {
                    KryptonMessageBox.Show("Número Inicial Não Preenchido!", "A T E N Ç Ã O", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                }
                else if (txtNNFfim.Text == "")
                {
                    KryptonMessageBox.Show("Número Final Não Preenchido!", "A T E N Ç Ã O", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if (txtXjust.Text == "")
                {
                    KryptonMessageBox.Show("Justificativa Não Preenchido!", "A T E N Ç Ã O", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                }
                else if (txtXjust.Text.Length < 15)
                {
                    KryptonMessageBox.Show("Justificativa Não Preenchido!", "A T E N Ç Ã O", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    retInfempresa();
                    AssinaNFeXml assina = new AssinaNFeXml();
                    X509Certificate2 cert = assina.BuscaNome("");
                    belNfeInutilizacao objinutilizacao = new belNfeInutilizacao(_tpamp, _cuf, _cnpj, _mod, cbxSerie.Text, txtXjust.Text,
                                                                                txtNNFini.Text, txtNNFfim.Text, cert, _uf_empresa, belStatic.bModoSCAN, belStatic.iStatusAtualSistema);


                    xmldoc.LoadXml(objinutilizacao.RetWs);
                    Globais LeRegWin = new Globais();

                    if (xmldoc.GetElementsByTagName("cStat")[0].InnerText == "102")
                    {
                        KryptonMessageBox.Show(null, "Tipo Ambiente: " + xmldoc.GetElementsByTagName("tpAmb")[0].InnerText + Environment.NewLine +
                                              "Status: " + xmldoc.GetElementsByTagName("cStat")[0].InnerText + Environment.NewLine +
                                              "Descrição: " + xmldoc.GetElementsByTagName("xMotivo")[0].InnerText + Environment.NewLine +
                                              "Ano: " + xmldoc.GetElementsByTagName("ano")[0].InnerText + Environment.NewLine +
                                              "Modelo da NF-e: " + xmldoc.GetElementsByTagName("mod")[0].InnerText + Environment.NewLine +
                                              "Serie da NF-e: " + xmldoc.GetElementsByTagName("serie")[0].InnerText + Environment.NewLine +
                                              "Número Inicial: " + xmldoc.GetElementsByTagName("nNFIni")[0].InnerText + Environment.NewLine +
                                              "Número Final: " + xmldoc.GetElementsByTagName("nNFFin")[0].InnerText + Environment.NewLine +
                                              "Data do Recbto: " + xmldoc.GetElementsByTagName("dhRecbto")[0].InnerText.Replace('T', ' ') + Environment.NewLine +
                                              "Número do Protocolo: " + xmldoc.GetElementsByTagName("nProt")[0].InnerText, "INUTILIZAÇÃO DE NUMERACAO",
                                              MessageBoxButtons.OK,
                                              MessageBoxIcon.Information);
                        xmldoc.Save(belStaticPastas.PROTOCOLOS + "\\" + xmldoc.GetElementsByTagName("nProt")[0].InnerText + "_inu.xml");
                    }
                    else
                    {
                        KryptonMessageBox.Show(null, "Tipo Ambiente: " + xmldoc.GetElementsByTagName("tpAmb")[0].InnerText + Environment.NewLine +
                                              "Status: " + xmldoc.GetElementsByTagName("cStat")[0].InnerText + Environment.NewLine +
                                              "Descrição: " + xmldoc.GetElementsByTagName("xMotivo")[0].InnerText,
                                              "INUTILIZAÇÃO DE NUMERACAO",
                                              MessageBoxButtons.OK,
                                              MessageBoxIcon.Information);
                    }
                    this.Close();
                }
            }
            catch (Exception ex)
            {

                KryptonMessageBox.Show(ex.Message);
            }

        }
        private void PesquisaDadosInutilizacao()
        {

        }

        private void txtNNFini_Validated(object sender, EventArgs e)
        {

            txtNNFini.Text = txtNNFini.Text.PadLeft(9, '0'); // Nfe_2.0
            txtNNFfim.Text = txtNNFini.Text;
        }

        private void txtNNFfim_Validated(object sender, EventArgs e)
        {
            txtNNFfim.Text = txtNNFfim.Text.PadLeft(9, '0');// Nfe_2.0
        }

        private void frmInutilizaNFecs_Load(object sender, EventArgs e)
        {

            try
            {
                StringBuilder sSql = new StringBuilder();
                sSql.Append("select distinct ");
                sSql.Append("coalesce ( empresa.cd_serie,'1') Serie ");
                sSql.Append("from ");
                sSql.Append("empresa ");
                //sSql.Append("left join tpdoc on (tpdoc.cd_tipodoc = nf.cd_tipodoc )");
                sSql.Append("where ");
                sSql.Append("empresa.cd_empresa ='");
                sSql.Append(_emp);
                sSql.Append("'");

                FbCommand cmdSerie = new FbCommand(sSql.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                FbDataReader drSerie = cmdSerie.ExecuteReader();
                string sSerie = string.Empty;
                while (drSerie.Read())
                {
                    cbxSerie.Items.Add(drSerie["Serie"].ToString().PadLeft(3, '0'));
                }
                if (cbxSerie.Items.Count <= 0)
                {
                    cbxSerie.Items.Add("001");
                }
                if (belStatic.bModoSCAN)
                {
                    cbxSerie.Items.Add(belStatic.iSerieSCAN);
                }
                cbxSerie.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                cx.Close_Conexao();
                throw new Exception(ex.Message);
            }
            finally
            {
                cx.Close_Conexao();
            }
        }
        private void retInfempresa()
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.Append("select ");
                sSql.Append("uf.nr_ufnfe cUF, ");
                sSql.Append("empresa.cd_cgc CNPJ, ");
                sSql.Append("'55' mod ");
                sSql.Append("from empresa ");
                sSql.Append("left join uf on (uf.cd_uf = empresa.cd_ufnor ) ");
                sSql.Append("where empresa.cd_empresa = '");
                sSql.Append(_emp);
                sSql.Append("'");
                FbCommand cmd = new FbCommand(sSql.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                FbDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    _cnpj = dr["CNPJ"].ToString();
                    _mod = dr["mod"].ToString();
                    _cuf = dr["cUF"].ToString();
                }

            }
            catch (Exception ex)
            {
                cx.Close_Conexao();
                throw new Exception(ex.Message);
            }
            finally
            {
                cx.Close_Conexao();
            }
        }

        private void btnSair_Click_1(object sender, EventArgs e)
        {

        }
    }
}
