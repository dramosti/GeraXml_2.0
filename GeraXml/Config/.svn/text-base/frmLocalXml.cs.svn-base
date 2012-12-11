using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Security.AccessControl;
using ComponentFactory.Krypton.Toolkit;
using HLP.bel.Static;
using HLP.bel;


namespace NfeGerarXml.Config
{
    public partial class frmLocalXml : KryptonForm
    {
        public frmLocalXml(string sCaminho)
        {
            InitializeComponent();
            txtCaminho.Text = @sCaminho;
        }


        private static string _LOCAL_XML = "LOCAL_XML";

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtCaminho.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            try
            {
                DirectoryInfo dinfo = new DirectoryInfo(txtCaminho.Text);
                if (dinfo.Exists)
                {
                    if (!belStatic.bClickOnce)
                    {
                        belRegedit.SalvarRegistro("Config_xml", "Caminho_xmls", txtCaminho.Text);
                    }
                    else
                    {
                        belIsolated objIsolated = new belIsolated();
                        objIsolated.SalvarArquivo(_LOCAL_XML, txtCaminho.Text, belIsolated.Lugar.Local);
                    }
                    belStatic.Pasta_xmls_Configs = txtCaminho.Text;
                    this.Close();
                }
                else
                {
                    KryptonMessageBox.Show(null, "Caminho não encontrado", "A V I S O", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void frmLocalXml_Load(object sender, EventArgs e)
        {

        }
    }
}
