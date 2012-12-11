using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using HLP.bel.CTe;
using ComponentFactory.Krypton.Toolkit;

namespace NfeGerarXml
{
    public partial class frmEmailCte : KryptonForm
    {
        public List<belEmailCte> objLbelEmailCte;


        Regex remail = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
        bool benvia = true;

        /// <summary>
        /// Tipo de Email
        /// E = Envio
        /// C = Cancelamento
        /// </summary>
        public string sTipo = "E";
        public frmEmailCte(List<belEmailCte> objLbelEmailCte)
        {
            InitializeComponent();
            this.objLbelEmailCte = objLbelEmailCte;
            PopulaDataGrid();
        }
        private void PopulaDataGrid()
        {
            try
            {
                for (int i = 0; i < objLbelEmailCte.Count; i++)
                {
                    dgvEmail.Rows.Add();

                    dgvEmail[0, i].Value = true;
                    dgvEmail[1, i].Value = objLbelEmailCte[i]._NumCte;
                    dgvEmail[2, i].Value = objLbelEmailCte[i]._para;

                    //Valida E - mails
                    if ((objLbelEmailCte[i]._para != "") && (!remail.IsMatch(objLbelEmailCte[i]._para)))
                    {
                        dgvEmail[2, i].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        dgvEmail[2, i].Style.BackColor = Color.White;
                    }



                }
                VerificaEmails();
                lblStatusEmail.Text = objLbelEmailCte.Count.ToString() + " E-mails.";
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(ex.Message);
            }
        }



        private void dgvEmail_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                for (int i = 0; i < dgvEmail.RowCount; i++)
                {
                    dgvEmail[0, i].Value = !benvia;
                    SendKeys.Send("{right}");
                    SendKeys.Send("{left}");
                }
                VerificaEmails();
                benvia = !benvia;
            }
        }

        private void btnEnviaEmail_Click(object sender, EventArgs e)
        {

            try
            {
                int ierroDest = VerificaEmailDest();

                if (ierroDest != 0)
                {
                    KryptonMessageBox.Show(null,
                        "Existem erros de nomenclatura de Email, Favor verificar as tarjas vermelhas:" +
                        Environment.NewLine +
                        Environment.NewLine +
                        "Total de erros = " + (ierroDest).ToString(),
                        "A V I S O",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    for (int i = 0; i < dgvEmail.RowCount; i++)
                    {
                        string sdest = (dgvEmail[2, i].Value == null ? "" : dgvEmail[2, i].Value.ToString());
                        if ((Convert.ToBoolean(dgvEmail[0, i].Value) == true))
                        {
                            objLbelEmailCte[i]._envia = true;
                            objLbelEmailCte[i]._para = sdest;
                        }
                        else
                        {
                            objLbelEmailCte[i]._envia = true;
                        }
                    }
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void dgvEmail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex > -1)
            {
                if (dgvEmail[0, e.RowIndex].Value == null)
                {
                    dgvEmail[0, e.RowIndex].Value = false;
                }
                dgvEmail[0, e.RowIndex].Value = !Convert.ToBoolean(dgvEmail[0, e.RowIndex].Value);
                SendKeys.Send("{right}");
                SendKeys.Send("{left}");

                VerificaEmails();
            }
        }

        private void VerificaEmails()
        {
            for (int i = 0; i < dgvEmail.RowCount; i++)
            {
                if (dgvEmail[0, i].Value == null)
                {
                    dgvEmail[0, i].Value = false;
                }
                if (Convert.ToBoolean(dgvEmail[0, i].Value))
                {
                    if (dgvEmail[2, i].Value == null)
                    {
                        dgvEmail[2, i].Value = "";
                    }
                    if (!remail.IsMatch(dgvEmail[2, i].Value.ToString()))
                    {
                        dgvEmail[2, i].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        dgvEmail[2, i].Style.BackColor = Color.White;
                    }


                }
                else
                {
                    dgvEmail[2, i].Style.BackColor = Color.White;


                }
            }
        }

        private int VerificaEmailDest()
        {
            int icountInvalidos = 0;
            for (int i = 0; i < dgvEmail.RowCount; i++)
            {
                if (dgvEmail[0, i].Value == null)
                {
                    dgvEmail[0, i].Value = false;
                }
                if (Convert.ToBoolean(dgvEmail[0, i].Value))
                {
                    if (!remail.IsMatch(dgvEmail[2, i].Value.ToString()))
                    {
                        icountInvalidos++;
                    }
                }
            }
            return icountInvalidos;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (dgvEmail.RowCount > 0)
            {
                if ((KryptonMessageBox.Show(null, "Atenção: Não será enviado arquivo XML ao Cliente!"
                    + Environment.NewLine
                    + Environment.NewLine
                    + "Deseja continuar ?", "A V I S O ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes))
                {
                    for (int i = 0; i < objLbelEmailCte.Count; i++)
                    {
                        objLbelEmailCte[i]._envia = false;
                    }
                    this.Close();
                }
            }
        }

        private void frmEmail_Load(object sender, EventArgs e)
        {

        }

        private void dgvEmail_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            VerificaEmails();
        }



    }
}
