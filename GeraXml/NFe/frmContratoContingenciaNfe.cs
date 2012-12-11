using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using NfeGerarXml.NFe;
using ComponentFactory.Krypton.Toolkit;
using GeraXml.NFe;

namespace NfeGerarXml
{
    public partial class frmContratoContingenciaNfe : KryptonForm
    {
        public string sNomeEmpresa;
        public frmContratoContingenciaNfe(string sNomeEmpresa)
        {
            InitializeComponent();
            this.sNomeEmpresa = sNomeEmpresa;
            relAvisoContingencia rpt = new relAvisoContingencia();
            rpt.DataDefinition.FormulaFields["Empresa"].Text = "'" + sNomeEmpresa + "'";

            crystalReportViewer1.ReportSource = rpt;
            rpt.Refresh();


   

            
        }
        public bool bImprime { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            bImprime = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bImprime = false;
            this.Close();
        }

        private void frmContratoContingencia_Load(object sender, EventArgs e)
        {
        }
       
    }
}
