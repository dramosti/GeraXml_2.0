using System;
using System.IO;
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
    public partial class frmCancJustNfe : KryptonForm
    {
        private List<string> _snotacanc;
        private string _emp;
        private int _tpamb;
        frmArquivosXmlNfe objfrmArquivos = null;
        private string UF_Empresa;
        public string status;
        belConnection cx = new belConnection();

        public frmCancJustNfe(List<string> sNotaCanc, string sEmp, string _UF_Empresa, frmArquivosXmlNfe objfrmArquivos)//Danner - o.s. 23984 - 07/01/2010
        {
            this.objfrmArquivos = objfrmArquivos;
            _snotacanc = sNotaCanc;
            _emp = sEmp;
            _tpamb = belStatic.tpAmb;
            //Danner - o.s. 23984 - 07/01/2010
            this.UF_Empresa = _UF_Empresa;
            //Fim - Danner - o.s. 23984 - 07/01/2010
            InitializeComponent();
        }

        private void btnCanc_Click(object sender, EventArgs e)
        {
            Globais pega = new Globais();
            //Danner - o.s. 23851 - 19/11/2009
            AssinaNFeXml bc = new AssinaNFeXml();
            X509Certificate2 cert = new X509Certificate2();
            cert = bc.BuscaNome("");
            //Fim - Danner - o.s. 23851 - 19/11/2009
            try
            {

                if (txtJust.Text.Length < 15)
                {
                    KryptonMessageBox.Show(null, "Justificativa Insuficiente, Mínimo de 15 Caracteres", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    txtJust.Focus();
                }
                else
                {
                    belNfeCancelamento nfecanc = new belNfeCancelamento("1.02", "2", "CANCELAR", txtJust.Text, "1.07", _emp, _snotacanc[0].ToString(), cert, UF_Empresa, belStatic.bModoSCAN, belStatic.iStatusAtualSistema);//Danner - o.s. 23984 - 07/01/2010
                    XmlDocument xmlret = new XmlDocument();
                    xmlret.LoadXml(nfecanc.Sret);
                    string cstat = xmlret.GetElementsByTagName("cStat")[0].InnerText;
                    string xmotivo = xmlret.GetElementsByTagName("xMotivo")[0].InnerText;
                    if (cstat != "101")
                    {
                        throw new Exception("Erro " + cstat + ": " + xmotivo);
                    }
                    else
                    {
                        string nprot = xmlret.GetElementsByTagName("nProt")[0].InnerText;
                        string chnfe = xmlret.GetElementsByTagName("chNFe")[0].InnerText;


                        StringBuilder sSql = new StringBuilder();

                        sSql.Append("update nf ");
                        sSql.Append("set cd_recibocanc = '");
                        sSql.Append(nprot);
                        sSql.Append("' ");
                        sSql.Append("where ");
                        sSql.Append("cd_empresa = '");
                        sSql.Append(_emp);
                        sSql.Append("' ");
                        sSql.Append("and ");
                        sSql.Append("cd_nfseq = '");
                        sSql.Append(_snotacanc[0]);
                        sSql.Append("'");

                        using (FbCommand cmdUpdate = new FbCommand(sSql.ToString(), cx.get_Conexao()))
                        {
                            cx.Open_Conexao();
                            cmdUpdate.ExecuteNonQuery();
                        }

                        geraNFeCanc(chnfe, pega);
                        KryptonMessageBox.Show(null, "Nota Cancelada com Sucesso", "Cancelamento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception x)
            {
                throw new Exception(x.Message + " - Nota não foi cancelada com sucesso!");
            }
            finally { cx.Close_Conexao(); }

        }

        private void btnParar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Globais pega = new Globais();
            //Danner - o.s. 23851 - 19/11/2009
            AssinaNFeXml bc = new AssinaNFeXml();
            X509Certificate2 cert = new X509Certificate2();
            cert = bc.BuscaNome("");
            //Fim - Danner - o.s. 23851 - 19/11/2009
            try
            {

                if (txtJust.Text.Length < 15)
                {
                    KryptonMessageBox.Show(null, "Justificativa Insuficiente, Mínimo de 15 Caracteres", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    txtJust.Focus();
                }
                else
                {
                    belNfeCancelamento nfecanc = new belNfeCancelamento("1.02", "2", "CANCELAR", txtJust.Text, "1.07", _emp, _snotacanc[0].ToString(), cert, UF_Empresa, belStatic.bModoSCAN, belStatic.iStatusAtualSistema);
                    XmlDocument xmlret = new XmlDocument();
                    xmlret.LoadXml(nfecanc.Sret);
                    string cstat = xmlret.GetElementsByTagName("cStat")[0].InnerText;
                    string xmotivo = xmlret.GetElementsByTagName("xMotivo")[0].InnerText;
                    if (cstat != "101")
                    {
                        throw new Exception("Erro " + cstat + ": " + xmotivo);
                    }
                    else
                    {
                        string nprot = xmlret.GetElementsByTagName("nProt")[0].InnerText;
                        string chnfe = xmlret.GetElementsByTagName("chNFe")[0].InnerText;


                        StringBuilder sSql = new StringBuilder();

                        sSql.Append("update nf ");
                        sSql.Append("set cd_recibocanc = '");
                        sSql.Append(nprot);
                        sSql.Append("' ");
                        sSql.Append("where ");
                        sSql.Append("cd_empresa = '");
                        sSql.Append(_emp);
                        sSql.Append("' ");
                        sSql.Append("and ");
                        sSql.Append("cd_nfseq = '");
                        sSql.Append(_snotacanc[0]);
                        sSql.Append("'");

                        using (FbCommand cmdUpdate = new FbCommand(sSql.ToString(), cx.get_Conexao()))
                        {
                            cx.Open_Conexao();
                            cmdUpdate.ExecuteNonQuery();
                        }

                        geraNFeCanc(chnfe, pega);
                        KryptonMessageBox.Show(null, "Nota Cancelada com Sucesso", "Cancelamento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
            }
            catch (Exception x)
            {
                KryptonMessageBox.Show(x.Message + " - Nota não foi cancelada com sucesso!");
            }
            finally { cx.Close_Conexao(); }

        }
        //Danner - o.s. 23851 - 19/11/2009
        private void geraNFeCanc(string sChnfe, Globais glob)
        {
            DirectoryInfo dinfo = new DirectoryInfo(belStaticPastas.ENVIADOS + "\\" + sChnfe.Substring(2, 4));
            string path = "";
            string nome = "";
            try
            {
                FileInfo[] finfo = dinfo.GetFiles();

                foreach (var item in finfo)
                {
                    if (item.Name.Contains(sChnfe))
                    {
                        path = item.FullName;
                        nome = item.Name;
                        break;
                    }

                }
                DirectoryInfo dinfoPasta = new DirectoryInfo(belStaticPastas.CANCELADOS + "\\" + HLP.Util.Util.GetDateServidor().Date.ToString("yyMM"));
                if (!dinfoPasta.Exists) { dinfoPasta.Create(); }

                File.Move(path, dinfoPasta.FullName + "\\" + nome.Replace("nfe", "can") + ".xml");
            }
            catch (Exception x)
            {

                throw new Exception("Erro ao tentar mover o arquivo - " + x.Message);
            }
            File.Delete(path);
        }
        //Fim - Danner - o.s. 23851 - 19/11/2009

        private void button1_Click(object sender, EventArgs e)
        {
            status = "0";
            this.Close();
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void btnSelAll_Click(object sender, EventArgs e)
        {
            Globais pega = new Globais();
            AssinaNFeXml bc = new AssinaNFeXml();
            X509Certificate2 cert = new X509Certificate2();
            cert = bc.BuscaNome("");
            try
            {

                if (txtJust.Text.Length < 15)
                {
                    KryptonMessageBox.Show(null, "Justificativa Insuficiente, Mínimo de 15 Caracteres", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    txtJust.Focus();
                }
                else
                {
                    belNfeCancelamento nfecanc = new belNfeCancelamento("1.02", _tpamb.ToString(), "CANCELAR", txtJust.Text, "2.00", _emp, _snotacanc[0].ToString(), cert, UF_Empresa, belStatic.bModoSCAN, belStatic.iStatusAtualSistema);//Danner - o.s. 23984 - 07/01/2010
                    XmlDocument xmlret = new XmlDocument();
                    xmlret.LoadXml(nfecanc.Sret);
                    string cstat = xmlret.GetElementsByTagName("cStat")[0].InnerText;
                    string xmotivo = xmlret.GetElementsByTagName("xMotivo")[0].InnerText;
                    status = cstat;
                    if (cstat != "101")
                    {
                        throw new Exception("Erro " + cstat + ": " + xmotivo);
                    }
                    else
                    {
                        string nprot = xmlret.GetElementsByTagName("nProt")[0].InnerText;
                        string chnfe = xmlret.GetElementsByTagName("chNFe")[0].InnerText;
                        StringBuilder sSql = new StringBuilder();
                        sSql.Append("update nf ");
                        sSql.Append("set cd_recibocanc = '");
                        sSql.Append(nprot);
                        sSql.Append("' ");
                        sSql.Append("where ");
                        sSql.Append("cd_empresa = '");
                        sSql.Append(_emp);
                        sSql.Append("' ");
                        sSql.Append("and ");
                        sSql.Append("cd_nfseq = '");
                        sSql.Append(_snotacanc[0]);
                        sSql.Append("'");
                        using (FbCommand cmdUpdate = new FbCommand(sSql.ToString(), cx.get_Conexao()))
                        {
                            cx.Open_Conexao();
                            cmdUpdate.ExecuteNonQuery();
                        }
                        geraNFeCanc(chnfe, pega);
                        this.Close();
                    }
                }
            }
            catch (Exception x)
            {
                cx.Close_Conexao();
                KryptonMessageBox.Show(x.Message + " - Nota não foi cancelada com sucesso!");
            }
            finally { cx.Close_Conexao(); ; }

            //Danner - o.s. 23851 - 19/11/2009

        }


    }
}
