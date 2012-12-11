using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using ComponentFactory.Krypton.Toolkit;

namespace NfeGerarXml
{
    public partial class frmRelatorioCte : KryptonForm
    {
        public frmRelatorioCte(ReportDocument rpt, string sTexto)
        {
            InitializeComponent();
            this.Text = sTexto;
            crystalReportViewer1.ReportSource = rpt;
            crystalReportViewer1.Refresh();
        }
    }
}
