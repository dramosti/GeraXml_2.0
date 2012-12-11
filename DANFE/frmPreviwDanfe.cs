using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DANFE;
using CrystalDecisions.CrystalReports.Engine;
using ComponentFactory.Krypton.Toolkit;


namespace DANFE
{
    public partial class frmPreviwDanfe : KryptonForm
    {
        public frmPreviwDanfe(ReportDocument rpt)
        {
            InitializeComponent();
            crystalReportViewer1.ReportSource = rpt;
            WindowState = FormWindowState.Maximized;
        }
    }
}
