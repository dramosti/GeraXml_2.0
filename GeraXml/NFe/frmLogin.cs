using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HLP.bel;
using FirebirdSql.Data.FirebirdClient;
using System.IO;
using ComponentFactory.Krypton.Toolkit;
using HLP.bel.Static;
using HLP.bel.NFe.GeraXml;
using HLP.bel.CTe;

namespace NfeGerarXml
{
    public partial class frmLogin : KryptonForm
    {
        public bool bFechaAplicativo = false;
        belConnection cx = new belConnection();
        public frmLogin()
        {

            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (belStatic.RAMO != "TRANSPORTE")
            {
                HLP.bel.NFe.belConfigInicial.CarregaConfiguracoesIniciais();
            }
            try
            {
                Globais objglobal = new Globais();
                string sEmp = objglobal.LeRegConfig("Empresa");
                belStatic.SUsuario = txtUsuario.Text.Trim().PadLeft(10, '0');
                belStatic.BAlteraDadosNfe = false;
                string sSenha = txtSenha.Text.Trim();
                string sTipoUsuario = "";
                StringBuilder sQueryUsuario = new StringBuilder();
                sQueryUsuario.Append("select count(acesso.CD_OPERADO) Total from acesso ");
                sQueryUsuario.Append("where acesso.CD_OPERADO = '" + belStatic.SUsuario + "'");
                FbCommand cmd = new FbCommand(sQueryUsuario.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                cmd.ExecuteNonQuery();
                FbDataReader dr = cmd.ExecuteReader();
                dr.Read();
                if (Convert.ToInt16(dr["Total"]) > 0)
                {
                    StringBuilder sQuery = new StringBuilder();
                    if (belStatic.RAMO != "TRANSPORTE")
                    {
                        sQuery.Append("select acesso.tp_operado, acesso.cd_senha, COALESCE(acesso.st_altera_dados_nfe,'S') st_altera_dados_nfe ");
                        sQuery.Append("from acesso ");
                        sQuery.Append("where acesso.cd_senha = '" + belCriptografia.Encripta(sSenha) + "' ");
                        sQuery.Append("and acesso.CD_OPERADO = '" + belStatic.SUsuario + "'");
                    }
                    else
                    {
                        sQuery.Append("select acesso.tp_operado, acesso.cd_senha ");
                        sQuery.Append("from acesso ");
                        sQuery.Append("where acesso.cd_senha = '" + belCriptografia.Encripta(sSenha) + "' ");
                        sQuery.Append("and acesso.CD_OPERADO = '" + belStatic.SUsuario + "'");
                    }

                    cmd = new FbCommand(sQuery.ToString(), cx.get_Conexao());
                    cmd.ExecuteNonQuery();
                    dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        sTipoUsuario = dr["tp_operado"].ToString();
                        if (belStatic.RAMO != "TRANSPORTE")
                        {
                            belStatic.BAlteraDadosNfe = (dr["st_altera_dados_nfe"].ToString().Equals("N") ? false : true);
                        }
                    }
                    if (sTipoUsuario != "")
                    {
                        belStatic.IPrimeiroLoad = 0;
                        this.Close();
                    }
                    else
                    {
                        KryptonMessageBox.Show(null, "SENHA INCORRETA", "A V I S O ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSenha.Focus();
                        txtSenha.Text = "";
                    }
                }
                else
                {
                    KryptonMessageBox.Show(null, "USUÁRIO INCORRETO", "A V I S O ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUsuario.Focus();
                    txtUsuario.Text = "";
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, ex.Message, "A V I S O", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.bFechaAplicativo = true;
            }
            finally
            {
                cx.Close_Conexao();
            }
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button1_Click(sender, e);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            try
            {
                if (belStatic.IPrimeiroLoad == 1)
                {
                    Environment.Exit(1);
                }
                else
                {
                    bFechaAplicativo = true;
                    this.Close();
                }
            }
            catch (Exception)
            {

            }
        }

        private void frmInicializaGeraXML_Load(object sender, EventArgs e)
        {
            try
            {
                string sQuery = "Select count(acesso.CD_OPERADO)total from acesso ";
                FbCommand command = new FbCommand(sQuery, cx.get_Conexao());
                cx.Open_Conexao();
                command.ExecuteNonQuery();
                FbDataReader dr = command.ExecuteReader();
                dr.Read();
                int itotal = Convert.ToInt32(dr["total"].ToString());

                if (!(itotal > 0))
                {
                    belStatic.BAlteraDadosNfe = true;
                    belStatic.SUsuario = "MASTER";

                    txtSenha.Enabled = false;
                    txtUsuario.Enabled = false;
                }
                else
                {
                    txtUsuario.Focus();
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally { cx.Close_Conexao(); }

        }

        private void txtUsuario_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                SendKeys.Send("{tab}");
            }
        }







    }
}
