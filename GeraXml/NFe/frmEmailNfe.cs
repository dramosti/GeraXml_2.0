using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HLP.bel;
using System.Text.RegularExpressions;
using ComponentFactory.Krypton.Toolkit;

namespace NfeGerarXml
{
    public partial class frmEmailNfe : KryptonForm
    {
        public List<belEmail> objLbelEmail;


        Regex remail = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
        bool benvia = true;

        /// <summary>
        /// Tipo de Email
        /// E = Envio
        /// C = Cancelamento
        /// </summary>
        public string sTipo = "E";
        public frmEmailNfe(List<belEmail> objLbelEmail)
        {
            InitializeComponent();
            this.objLbelEmail = objLbelEmail;
            PopulaDataGrid();
        }
        private void PopulaDataGrid()
        {
            try
            {
                for (int i = 0; i < objLbelEmail.Count; i++)
                {
                    dgvEmail.Rows.Add();

                    dgvEmail[0, i].Value = true;
                    dgvEmail[1, i].Value = objLbelEmail[i]._sSeq;
                    dgvEmail[2, i].Value = objLbelEmail[i]._para;
                    dgvEmail[3, i].Value = objLbelEmail[i]._paraTransp;

                    //Valida E - mails
                    if ((objLbelEmail[i]._para != "") && (!remail.IsMatch(objLbelEmail[i]._para)))
                    {
                        dgvEmail[2, i].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        dgvEmail[2, i].Style.BackColor = Color.White;
                    }

                    if ((objLbelEmail[i]._paraTransp != "") && (!remail.IsMatch(objLbelEmail[i]._paraTransp))) // 24776 - Diego
                    {
                        dgvEmail[3, i].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        dgvEmail[3, i].Style.BackColor = Color.White;
                    }

                }
                VerificaEmails();
                lblStatusEmail.Text = objLbelEmail.Count.ToString() + " E-mails.";
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(ex.Message);
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

        private void dgvEmail_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            VerificaEmails();
        }



        private void btnEnviaEmail_Click(object sender, EventArgs e)
        {

            try
            {
                int ierroDest = VerificaEmailDest();
                int ierroTransp = VerificaEmailTransp();

                if (ierroDest != 0 || ierroTransp != 0)
                {
                    KryptonMessageBox.Show(null,
                        "Existem erros de nomenclatura de Email, Favor verificar as tarjas vermelhas:" +
                        Environment.NewLine +
                        Environment.NewLine +
                        "Total de erros = " + (ierroTransp + ierroDest).ToString(),
                        "A V I S O",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    for (int i = 0; i < dgvEmail.RowCount; i++)
                    {
                        string sdest = (dgvEmail[2, i].Value == null ? "" : dgvEmail[2, i].Value.ToString());
                        string sdestTransp = (dgvEmail[3, i].Value == null ? "" : dgvEmail[3, i].Value.ToString());
                        if ((Convert.ToBoolean(dgvEmail[0, i].Value) == true))
                        {
                            objLbelEmail[i]._envia = true;
                            objLbelEmail[i]._para = sdest;
                            objLbelEmail[i]._paraTransp = sdestTransp;
                        }
                        else
                        {
                            objLbelEmail[i]._envia = true;
                        }
                    }
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
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

                    if (dgvEmail[3, i].Value == null)
                    {
                        dgvEmail[3, i].Value = "";
                    }
                    if ((!remail.IsMatch(dgvEmail[3, i].Value.ToString())) && (dgvEmail[3, i].Value.ToString() != ""))
                    {
                        dgvEmail[3, i].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        dgvEmail[3, i].Style.BackColor = Color.White;
                    }
                }
                else
                {
                    dgvEmail[2, i].Style.BackColor = Color.White;
                    dgvEmail[3, i].Style.BackColor = Color.White;
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

        private int VerificaEmailTransp()
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
                    if (dgvEmail[3, i].Value.ToString() != "")
                    {
                        if (!remail.IsMatch(dgvEmail[3, i].Value.ToString()))
                        {
                            icountInvalidos++;
                        }
                    }
                }
            }
            return icountInvalidos;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (dgvEmail.RowCount > 0)
            {
                if ((KryptonMessageBox.Show(null, "Atenção: Nenhum aviso de NFe emitida será enviado ao cliente, tomar providencia !"
                    + Environment.NewLine
                    + Environment.NewLine
                    + "deseja continuar ?", "A V I S O ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes))
                {
                    for (int i = 0; i < objLbelEmail.Count; i++)
                    {
                        objLbelEmail[i]._envia = false;
                    }
                    this.Close();
                }
            }
        }

        private void frmEmail_Load(object sender, EventArgs e)
        {
            if (sTipo.Equals("C"))
            {
                dgvEmail.Columns["transp"].Visible = false;
            }
        }

       



    }
}
