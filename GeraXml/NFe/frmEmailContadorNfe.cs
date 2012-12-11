using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using HLP.bel.NFe;
using FirebirdSql.Data.FirebirdClient;
using HLP.bel;
using System.Text.RegularExpressions;
using Ionic.Zip;
using System.Xml.Linq;
using Ionic.Zlib;
using System.Threading;
using ComponentFactory.Krypton.Toolkit;
using HLP.bel.Static;
using HLP.bel.NFe.GeraXml;


namespace NfeGerarXml.NFe
{

    public partial class frmEmailContadorNfe : KryptonForm
    {
        HLP.Dao.daoEmailContador objdaoEmailConatador = new HLP.Dao.daoEmailContador();
        List<HLP.bel.NFe.belEmailContador> objListaEmailContador = new List<HLP.bel.NFe.belEmailContador>();
        List<DiasSemana> objListaSemanas = new List<DiasSemana>();
        string sArqConfigDia = "";

        public frmEmailContadorNfe()
        {
            InitializeComponent();
            VerificaCamposEmail();
            Globais objGlobais = new Globais();
            sArqConfigDia = belStaticPastas.ENVIADOS + "\\Contador_xml\\" + "ConfigDia.txt";

            objListaSemanas.Add(new DiasSemana { Display = "Domingo", Valor = "Sunday" });
            objListaSemanas.Add(new DiasSemana { Display = "Segunda", Valor = "Monday" });
            objListaSemanas.Add(new DiasSemana { Display = "Terça", Valor = "Tuesday" });
            objListaSemanas.Add(new DiasSemana { Display = "Quarta", Valor = "Wednesday" });
            objListaSemanas.Add(new DiasSemana { Display = "Quinta", Valor = "Thursday" });
            objListaSemanas.Add(new DiasSemana { Display = "Sexta", Valor = "Friday" });
            objListaSemanas.Add(new DiasSemana { Display = "Sabado", Valor = "Saturday" });

            cbxDia.DisplayMember = "Display";
            cbxDia.ValueMember = "Valor";
            cbxDia.DataSource = objListaSemanas;

            FileInfo finfo = new FileInfo(sArqConfigDia);
            if (finfo.Exists)
            {
                FileStream theFile = File.Open(sArqConfigDia, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(theFile);
                string sDia = reader.ReadToEnd().Trim();
                if (sDia == "month")
                {
                    cbxDia.Enabled = false;
                    cbxDia.SelectedIndex = -1;
                    chkMensal.Checked = true;
                }
                else
                {
                    cbxDia.SelectedValue = sDia;
                    chkSemanal.Checked = true;
                    cbxDia.Enabled = true;
                }
                reader.Close();
            }
            else
            {
                KryptonMessageBox.Show("Para utilizar essa Rotina, primeiro Acerte a Configuração de Alerta!", "A V I S O", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tabControl1.SelectedTab = tabPage2;
            }
        }

        private void frmEmailContador_Load(object sender, EventArgs e)
        {
            try
            {
                if (objdaoEmailConatador.VerificaEmailContador())
                {
                    txtContador.Text = belStatic.sEmailContador;
                    RefreshGrid();
                    ExcluirArquivosZip();
                }
                else
                {
                    KryptonMessageBox.Show("O E-mail do contador não está salvo no cadastro de empresa.", "A V I S O", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    errorProvider1.SetError(txtContador, "E-mail contador inválido");
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, string.Format(Msg_Padrao.CException, Environment.NewLine) + (ex.InnerException != null ? ex.InnerException.Message + Environment.NewLine + ex.Message : ex.Message).ToString(), "AVISO ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEnviarEmail_Click(object sender, EventArgs e)
        {
            ProcessoEmail();
        }

        private void ProcessoEmail()
        {
            try
            {
                if (objListaEmailContador.Where(c => c.bSelect == true).Count() > 0)
                {
                    lblStatus.Text = "Compactando os Arquivos e Enviando Email...";
                    lblStatus.Refresh();
                    CampactaZip();
                    EnviaEmail();
                    KryptonMessageBox.Show("E-mail enviado com sucesso!", "A V I S O", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblStatus.Text = "";
                    this.RefreshGrid();
                }
                else
                {
                    KryptonMessageBox.Show("Nenhum Mês foi selecionado", "A V I S O", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, string.Format(Msg_Padrao.CException, Environment.NewLine) + (ex.InnerException != null ? ex.InnerException.Message + Environment.NewLine + ex.Message : ex.Message).ToString(), "AVISO ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtCopia_Validating(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            if (txtCopia.Text != "")
            {
                Regex remail = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                if (!remail.IsMatch(txtCopia.Text))
                {
                    errorProvider1.SetError(txtCopia, "E-mail Inválido");
                    e.Cancel = true;
                }
            }
        }


        //Metodos
        private void EnviaEmail()
        {
            HLP.bel.NFe.GeraXml.Globais LeRegWin = new HLP.bel.NFe.GeraXml.Globais();
            string hostservidor = LeRegWin.LeRegConfig("HostServidor").ToString().Trim();
            string porta = LeRegWin.LeRegConfig("PortaServidor").ToString().Trim();
            string remetente = LeRegWin.LeRegConfig("EmailRemetente").ToString().Trim();
            string senha = LeRegWin.LeRegConfig("SenhaRemetente").ToString().Trim();
            bool autentica = Convert.ToBoolean(LeRegWin.LeRegConfig("RequerSSL").ToString().Trim());

            if (remetente == "" || senha == "" || hostservidor == "")
            {
                throw new Exception("As configurações de e-mail não estão corretas!");
            }


            belEmail objbelEmail = new belEmail(objListaEmailContador.Where(c => c.bSelect == true).ToList(),
                hostservidor, porta, remetente, senha, txtContador.Text, autentica, txtCopia.Text);

            objbelEmail.enviaEmail();

        }
        private void VerificaCamposEmail()
        {
            Globais LeRegWin = new Globais();
            string hostservidor = LeRegWin.LeRegConfig("HostServidor").ToString().Trim();
            string porta = LeRegWin.LeRegConfig("PortaServidor").ToString().Trim();
            string remetente = LeRegWin.LeRegConfig("EmailRemetente").ToString().Trim();
            string senha = LeRegWin.LeRegConfig("SenhaRemetente").ToString().Trim();
            bool autentica = Convert.ToBoolean(LeRegWin.LeRegConfig("RequerSSL").ToString().Trim());

            if (remetente == "" || senha == "" || hostservidor == "")
            {
                throw new Exception("As configurações de e-mail não estão corretas!");
            }
        }
        private void CampactaZip()
        {
            HLP.bel.NFe.GeraXml.Globais objGlobais = new Globais();
            try
            {
                foreach (belEmailContador item in objListaEmailContador.Where(c => c.bSelect == true).ToList())
                {
                    using (ZipFile zip = new ZipFile())
                    {
                        zip.StatusMessageTextWriter = zip.StatusMessageTextWriter;

                        // Compacta arquivos Enviados
                        DirectoryInfo dinfoArq = new DirectoryInfo(item.sCaminhoEnviado);
                        if (dinfoArq.Exists)
                        {
                            foreach (FileSystemInfo arq in dinfoArq.GetFileSystemInfos())
                            {
                                try
                                {
                                    List<XElement> xListElem = item.xmlArqEnviados.Descendants("Email").Elements("Envio").Where(c => c.Attribute("Tipo").Value.ToString() == "enviados").ToList();
                                    if (xListElem.Where(x => x.Value.ToString() == arq.Name).Count() == 0)
                                    {
                                        zip.AddFile(arq.FullName);
                                        item.xmlArqEnviados.Element("Email").Add(new XElement("Envio", new XAttribute("Tipo", "enviados"), arq.Name));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }

                            }
                        }

                        //  Compacta arquivos Cancelados
                        dinfoArq = new DirectoryInfo(item.sCaminhoCancelado);
                        if (dinfoArq.Exists)
                        {
                            foreach (FileSystemInfo arq in dinfoArq.GetFileSystemInfos())
                            {
                                List<XElement> xListElem = item.xmlArqEnviados.Descendants("Email").Elements("Envio").Where(c => c.Attribute("Tipo").Value.ToString() == "cancelados").ToList();
                                if (xListElem.Where(x => x.Value.ToString() == arq.Name.ToString()).Count() == 0)
                                {
                                    zip.AddFile(arq.FullName);
                                    item.xmlArqEnviados.Element("Email").Add(new XElement("Envio", new XAttribute("Tipo", "cancelados"), arq.Name));
                                }
                            }
                        }

                        //  Compacta arquivos CCe
                        if (!string.IsNullOrEmpty(item.sCaminhoCCe))
                        {
                            dinfoArq = new DirectoryInfo(item.sCaminhoCCe);
                            if (dinfoArq.Exists)
                            {
                                foreach (FileSystemInfo arq in dinfoArq.GetFileSystemInfos())
                                {
                                    List<XElement> xListElem = item.xmlArqEnviados.Descendants("Email").Elements("Envio").Where(c => c.Attribute("Tipo").Value.ToString() == "cce").ToList();
                                    if (xListElem.Where(x => x.Value.ToString() == arq.Name.ToString()).Count() == 0)
                                    {
                                        zip.AddFile(arq.FullName);
                                        item.xmlArqEnviados.Element("Email").Add(new XElement("Envio", new XAttribute("Tipo", "cce"), arq.Name));
                                    }
                                }
                            }
                        }


                        if (File.Exists(item.sCaminhoZip))
                        {
                            File.Delete(item.sCaminhoZip);
                        }
                        zip.Save(item.sCaminhoZip);
                        item.xmlArqEnviados.Save(item.dinfo.FullName + "\\" + item.sName + HLP.Util.Util.GetDateServidor().Date.Day.ToString().PadLeft(2, '0') + ".xml");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void RefreshGrid()
        {
            HLP.bel.NFe.GeraXml.Globais objGlobais = new Globais();
            DirectoryInfo dEnviados = new DirectoryInfo(belStaticPastas.ENVIADOS);
            int icount = 0;
            objListaEmailContador = new List<belEmailContador>();

            foreach (FileSystemInfo pasta in dEnviados.GetFileSystemInfos())
            {
                if (belStatic.RAMO == "TRANSPORTE")
                {
                    if (pasta.Name.Length == 5)
                    {
                        belEmailContador objEmailCont = new belEmailContador();
                        objEmailCont.iId = icount++;
                        objEmailCont.Mes = pasta.Name.Substring(0, 2);
                        objEmailCont.sAno = "20" + pasta.Name.Substring(3, 2);
                        objEmailCont.sName = pasta.Name;
                        objEmailCont.sCaminhoEnviado = pasta.FullName;
                        objEmailCont.sCaminhoCancelado = belStaticPastas.CANCELADOS + "\\" + pasta.Name;
                        DirectoryInfo dinfoEnviados = new DirectoryInfo(objEmailCont.sCaminhoEnviado);
                        DirectoryInfo dinfoCancelados = new DirectoryInfo(objEmailCont.sCaminhoCancelado);
                        objEmailCont.iFaltantes = (dinfoEnviados.Exists ? dinfoEnviados.GetFileSystemInfos().Count() : 0) + (dinfoCancelados.Exists ? dinfoCancelados.GetFileSystemInfos().Count() : 0);

                        string sCaminho = belStaticPastas.ENVIADOS + "\\Contador_xml\\" + pasta.Name + HLP.Util.Util.GetDateServidor().Date.Day.ToString().PadLeft(2, '0') + ".xml";
                        if (File.Exists(sCaminho))
                        {
                            XDocument xmlDoc = XDocument.Load(sCaminho);
                            objEmailCont.xmlArqEnviados = xmlDoc;
                            //Count nos arquivos que já foram enviados q estão no Xml.
                            objEmailCont.iEnviadoContador = xmlDoc.Descendants("Email").Elements("Envio").Count();
                        }
                        else
                        {
                            objEmailCont.xmlArqEnviados = new XDocument();
                            objEmailCont.xmlArqEnviados.Add(new XElement("Email"));
                            objEmailCont.xmlArqEnviados.Save(sCaminho);
                        }

                        objListaEmailContador.Add(objEmailCont);
                    }
                }
                else if (pasta.Name.Length == 4)
                {
                    belEmailContador objEmailCont = new belEmailContador();
                    objEmailCont.iId = icount++;
                    objEmailCont.Mes = pasta.Name.Substring(2, 2);
                    objEmailCont.sAno = "20" + pasta.Name.Substring(0, 2);
                    objEmailCont.sName = pasta.Name;

                    objEmailCont.sCaminhoEnviado = pasta.FullName;
                    objEmailCont.sCaminhoCancelado = belStaticPastas.CANCELADOS + "\\" + pasta.Name;
                    objEmailCont.sCaminhoCCe = belStaticPastas.CCe + "\\" + pasta.Name;

                    DirectoryInfo dinfoEnviados = new DirectoryInfo(objEmailCont.sCaminhoEnviado);
                    DirectoryInfo dinfoCancelados = new DirectoryInfo(objEmailCont.sCaminhoCancelado);
                    DirectoryInfo dCartaCorrecao = new DirectoryInfo(objEmailCont.sCaminhoCCe);

                    objEmailCont.iFaltantes = (dinfoEnviados.Exists ? dinfoEnviados.GetFileSystemInfos().Count() : 0) + (dinfoCancelados.Exists ? dinfoCancelados.GetFileSystemInfos().Count() : 0) +
                                              (dCartaCorrecao.Exists ? dCartaCorrecao.GetFileSystemInfos().Count() : 0);

                    string sCaminho = belStaticPastas.ENVIADOS + "\\Contador_xml\\" + pasta.Name + HLP.Util.Util.GetDateServidor().Date.Day.ToString().PadLeft(2, '0') + ".xml";
                    if (File.Exists(sCaminho))
                    {
                        XDocument xmlDoc = XDocument.Load(sCaminho);
                        objEmailCont.xmlArqEnviados = xmlDoc;
                        //Count nos arquivos que já foram enviados q estão no Xml.
                        objEmailCont.iEnviadoContador = xmlDoc.Descendants("Email").Elements("Envio").Count();
                    }
                    else
                    {
                        objEmailCont.xmlArqEnviados = new XDocument();
                        objEmailCont.xmlArqEnviados.Add(new XElement("Email"));
                        objEmailCont.xmlArqEnviados.Save(sCaminho);
                    }

                    objListaEmailContador.Add(objEmailCont);
                }
            }
            dataGridView1.Rows.Clear();
            belEmailContadorBindingSource.DataSource = new object();
            belEmailContadorBindingSource.DataSource = objListaEmailContador.OrderByDescending(c => c.iId);

            string sCaminhoXml = objListaEmailContador.First().dinfo.FullName + "\\" + "emailContador.xml";

            foreach (HLP.bel.NFe.belEmailContador obj in objListaEmailContador)
            {
                if (obj.iFaltantes == 0)
                {
                    obj.Ienviado = GeraXml.Properties.Resources.confirmar;
                }
                else
                {
                    obj.Ienviado = GeraXml.Properties.Resources.cancel__3_;
                }
            }
        }

        private void ExcluirArquivosZip()
        {
            try
            {
                if (objListaEmailContador != null)
                {
                    if (objListaEmailContador.Count > 0)
                    {
                        if (objListaEmailContador.First().dinfo.Exists)
                        {
                            foreach (FileInfo item in objListaEmailContador.First().dinfo.GetFiles("*.zip"))
                            {
                                item.Delete();
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnSalvarDia_Click(object sender, EventArgs e)
        {
            try
            {

                string sValorSave = "";

                if (chkSemanal.Checked)
                {

                    sValorSave = cbxDia.SelectedValue.ToString();

                }
                else
                {
                    sValorSave = "month";
                }
                FileStream FileDia = File.Create(sArqConfigDia);
                StreamWriter sw = new StreamWriter(FileDia);
                sw.WriteLine(sValorSave);
                sw.Close();
                KryptonMessageBox.Show("Configuração de Alerta Salvo com Sucesso", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {

                throw;
            }





        }


        public struct DiasSemana
        {
            public string Display { get; set; }
            public string Valor { get; set; }
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            KryptonCheckBox chk = (KryptonCheckBox)sender;

            cbxDia.Enabled = chkSemanal.Checked;

            if (chk == chkSemanal)
            {
                chkMensal.Checked = !chkSemanal.Checked;

                if (cbxDia.SelectedIndex == -1)
                {
                    cbxDia.SelectedValue = "Friday";
                }
            }
            else
            {
                chkSemanal.Checked = !chkMensal.Checked;
                cbxDia.Enabled = false;
            }
        }

        private void frmEmailContadorNfe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

    }
}
