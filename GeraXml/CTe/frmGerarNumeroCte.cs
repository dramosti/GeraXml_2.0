using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HLP.bel.CTe;
using HLP.Dao.CTe;
using ComponentFactory.Krypton.Toolkit;
using HLP.bel.Static;

namespace NfeGerarXml
{
    public partial class frmGerarNumeroCte : KryptonForm
    {
        string sEmp = "";
        List<string> objlGerarConhec;

        public frmGerarNumeroCte(string sEmp, List<string> objlGerarConhec)
        {
            InitializeComponent();
            this.sEmp = sEmp;
            this.objlGerarConhec = objlGerarConhec;
        }

        private void frmGerarNumero_Load(object sender, EventArgs e)
        {
            try
            {

                
                daoGeraNumero objdaoGeraNumero = new daoGeraNumero();
                if (belStatic.sNomeEmpresa.ToUpper().Equals("SICUPIRA") || belStatic.sNomeEmpresa.ToUpper().Equals("TRANSLILO") || belStatic.sNomeEmpresa.ToUpper().Equals("GCA"))
                {
                    string sGenerator = "CONHECIM_CTE" + belStatic.CodEmpresaCte;
                    HLP.Dao.NFes.daoUtil util = new HLP.Dao.NFes.daoUtil();
                    if (!util.VerificaExistenciaGenerator(sGenerator))
                    {
                        util.CreateGenerator(sGenerator, 0);
                    }
                }
                txtNumeroUltNF.Text = objdaoGeraNumero.BuscaUltimoNumeroConhecimento(sEmp).PadLeft(6, '0');
                txtNumeroASerEmi.Text = (Convert.ToInt32(txtNumeroUltNF.Text) + 1).ToString().PadLeft(6, '0');
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnGerar_Click(object sender, EventArgs e)
        {
            daoGeraNumero objdaoGeraNumero = new daoGeraNumero();
            try
            {
                List<belNumeroConhec> objLbelConhec = objdaoGeraNumero.GeraNumerosConhecimentos(objlGerarConhec, txtNumeroASerEmi.Text, sEmp);

                pgbNF.Minimum = 0;
                pgbNF.Maximum = objLbelConhec.Count;

                for (int i = 0; i < objLbelConhec.Count; i++)
                {
                    objdaoGeraNumero.GravaConhec(sEmp, objLbelConhec[i]);
                    pgbNF.Value++;
                }

                if (belStatic.sNomeEmpresa.ToUpper().Equals("SICUPIRA") || belStatic.sNomeEmpresa.ToUpper().Equals("TRANSLILO") || belStatic.sNomeEmpresa.ToUpper().Equals("GCA"))
                {
                    objdaoGeraNumero.AtualizaGenerator(objLbelConhec[objLbelConhec.Count - 1].cdConhec);
                }

                KryptonMessageBox.Show(null, "Numeração dos Conhecimentos gerados com sucesso!", "Gerar Número Conhecimento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


    }
}
