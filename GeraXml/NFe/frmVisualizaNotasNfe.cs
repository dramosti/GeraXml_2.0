using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HLP.bel;
using System.Text.RegularExpressions;
using FirebirdSql.Data.FirebirdClient;
using System.Data.Entity;
using System.Linq;
using NfeGerarXml.NFe;
using ComponentFactory.Krypton.Toolkit;
using HLP.bel.Static;
using HLP.bel.NFe.GeraXml;
using HLP.bel.CTe;

namespace NfeGerarXml
{
    public partial class frmVisualizaNotasNfe : KryptonForm
    {
        belIde objide = new belIde();
        List<List<object>> lObjTotNotas;
        public List<List<object>> lObjTotNotasFinal;
        int notAtual = 1;
        int notTotal = 0;
        public bool Cancel = false;
        private string _sEmp;
        belCampoPesquisa objbelCampoPesquisa;
        belConnection cx = new belConnection();
        string sCasasVlUnit = "";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bAlteraDados">Usuario pode alterar dados na Visualização?</param>
        /// <param name="_lObjTotNotas">Lista de Notas</param>daoInfAdic
        /// <param name="sEmp">Empresa</param>daoInfAdic
        public frmVisualizaNotasNfe(List<List<object>> _lObjTotNotas, string sEmp)//Danner - o.s. 24184 - 25/02/2010
        {
            this.lObjTotNotas = _lObjTotNotas;
            this.notTotal = lObjTotNotas.Count;
            lObjTotNotasFinal = lObjTotNotas;
            this._sEmp = sEmp;//Danner - o.s. 24184 - 25/02/2010
            InitializeComponent();
            lblMsgReferencia.Text = "Esta informação será utilizada" +
                 Environment.NewLine +
                "nas hipóteses previstas na" +
                 Environment.NewLine +
                "legislação. Ex.: " +
                 Environment.NewLine +
                "Devolução de Mercadorias; " +
                 Environment.NewLine +
                "Substituição de NF cancelada; " +
                 Environment.NewLine +
                "Complementação de NF, etc";
            errorProvider1.SetError(lblMsgReferencia, "I M P O R T A N T E");

        }
        private void frmVisualizaNotas_Load(object sender, EventArgs e)
        {
            dtpHSaiEnt.Value = HLP.Util.Util.GetDateServidor();
            Globais glob = new Globais();
            sCasasVlUnit = glob.LeRegConfig("QtdeCasasVlUnit");
            sCasasVlUnit = (sCasasVlUnit == "" ? "1" : sCasasVlUnit);

            geraDgvDup();
            geraDgvProd();
            lblCtrNota.Text = notAtual + " de " + notTotal;
            belUF objuf = new belUF();
            cbxUF.DisplayMember = "SiglaUF";
            cbxUF.ValueMember = "CUF";
            cbxUF.DataSource = objuf.retListaUF();

            cbxUF_embarque.DisplayMember = "SiglaUF";
            cbxUF_embarque.ValueMember = "CUF";
            cbxUF_embarque.DataSource = objuf.retListaUF();
            cbxUF_embarque.SelectedIndex = -1;

            populaNF(this.lObjTotNotas[notAtual - 1]);

            HabilitaCampos(this.Controls, false);
            desabilitaBotoes(false);
            EmEdicao(false);
            ControlaSeta();
            HabilitaGrids(false);
            // btnAtualizar_Click(sender, e);

            if (belStatic.bDentroHlp == false)
            {
                tsHlpTeste.Visible = false;
            }


        }



        private void populaDet(List<belDet> lObj)
        {
            dgvDet.Rows.Clear();
            List<belDet> lObjDet = lObj;
            try
            {

                for (int i = 0; i < lObjDet.Count; i++)
                {
                    dgvDet.Rows.Add();

                    #region Prod


                    dgvDet["Cean", i].Value = lObjDet[i].belProd.Cean;
                    dgvDet["Ceantrib", i].Value = lObjDet[i].belProd.Ceantrib;
                    dgvDet["Cfop", i].Value = lObjDet[i].belProd.Cfop;
                    dgvDet["Cprod", i].Value = lObjDet[i].belProd.Cprod;
                    dgvDet["Extipi", i].Value = Convert.ToString(lObjDet[i].belProd.Extipi);
                    dgvDet["Genero", i].Value = Convert.ToString(lObjDet[i].belProd.Genero);
                    dgvDet["NCM", i].Value = lObjDet[i].belProd.Ncm;
                    dgvDet["Qcom", i].Value = lObjDet[i].belProd.Qcom;
                    dgvDet["Qtrib", i].Value = lObjDet[i].belProd.Qtrib;
                    dgvDet["Ucom", i].Value = lObjDet[i].belProd.Ucom;
                    dgvDet["Utrib", i].Value = lObjDet[i].belProd.Utrib;
                    dgvDet["Vdesc", i].Value = Convert.ToString(lObjDet[i].belProd.Vdesc);
                    dgvDet["VOutro", i].Value = Convert.ToString(lObjDet[i].belProd.VOutro); // NFe_2.0'
                    dgvDet["indTot", i].Value = Convert.ToString(lObjDet[i].belProd.IndTot); // NFe_2.0'
                    dgvDet["Vfrete", i].Value = Convert.ToString(lObjDet[i].belProd.Vfrete);
                    dgvDet["Vprod", i].Value = lObjDet[i].belProd.Vprod;
                    dgvDet["Vseg", i].Value = Convert.ToString(lObjDet[i].belProd.Vseg);
                    dgvDet["Vuncom", i].Value = lObjDet[i].belProd.Vuncom;
                    dgvDet["Vuntrib", i].Value = lObjDet[i].belProd.Vuntrib;
                    dgvDet["Xprod", i].Value = lObjDet[i].belProd.Xprod;
                    dgvDet["xPed", i].Value = lObjDet[i].belProd.XPed;
                    dgvDet["nItemPed", i].Value = lObjDet[i].belProd.NItemPed;

                    #endregion

                    #region Imposto

                    belImposto objimposto = new belImposto();

                    #region ICMS
                    if (lObjDet[i].belImposto.belIcms.belICMSSN101 != null)
                    {
                        dgvDet["CstIcms", i].Value = lObjDet[i].belImposto.belIcms.belICMSSN101.CSOSN;
                        dgvDet["OrigIcms", i].Value = lObjDet[i].belImposto.belIcms.belICMSSN101.orig;
                        dgvDet["pCredSN", i].Value = lObjDet[i].belImposto.belIcms.belICMSSN101.pCredSN;
                        dgvDet["vCredICMSSN", i].Value = lObjDet[i].belImposto.belIcms.belICMSSN101.vCredICMSSN;
                    }
                    else if (lObjDet[i].belImposto.belIcms.belICMSSN102 != null)
                    {
                        dgvDet["CstIcms", i].Value = lObjDet[i].belImposto.belIcms.belICMSSN102.CSOSN;
                        dgvDet["OrigIcms", i].Value = lObjDet[i].belImposto.belIcms.belICMSSN102.orig;
                    }
                    else if (lObjDet[i].belImposto.belIcms.belICMSSN201 != null)
                    {
                        dgvDet["CstIcms", i].Value = lObjDet[i].belImposto.belIcms.belICMSSN201.CSOSN;
                        dgvDet["OrigIcms", i].Value = lObjDet[i].belImposto.belIcms.belICMSSN201.orig;
                        dgvDet["ModbcstIcms", i].Value = lObjDet[i].belImposto.belIcms.belICMSSN201.modBCST;
                        dgvDet["PmvastIcms", i].Value = lObjDet[i].belImposto.belIcms.belICMSSN201.pMVAST;
                        dgvDet["PredbcstIcms", i].Value = lObjDet[i].belImposto.belIcms.belICMSSN201.pRedBCST;
                        dgvDet["VbcstIcms", i].Value = lObjDet[i].belImposto.belIcms.belICMSSN201.vBCST;
                        dgvDet["PicmsstIcms", i].Value = lObjDet[i].belImposto.belIcms.belICMSSN201.pICMSST;
                        dgvDet["VicmsstIcms", i].Value = lObjDet[i].belImposto.belIcms.belICMSSN201.vICMSST;
                        if (lObjDet[i].belImposto.belIcms.belICMSSN201.CSOSN.Equals("101"))
                        {
                            dgvDet["pCredSN", i].Value = lObjDet[i].belImposto.belIcms.belICMSSN201.pCredSN;
                            dgvDet["vCredICMSSN", i].Value = lObjDet[i].belImposto.belIcms.belICMSSN201.vCredICMSSN;
                        }
                    }
                    else if (lObjDet[i].belImposto.belIcms.belICMSSN500 != null)
                    {
                        dgvDet["CstIcms", i].Value = lObjDet[i].belImposto.belIcms.belICMSSN500.CSOSN;
                        dgvDet["OrigIcms", i].Value = lObjDet[i].belImposto.belIcms.belICMSSN500.orig;
                        dgvDet["VbcstIcms", i].Value = lObjDet[i].belImposto.belIcms.belICMSSN500.vBCSTRet;
                        dgvDet["VicmsstIcms", i].Value = lObjDet[i].belImposto.belIcms.belICMSSN500.vICMSSTRet;
                    }
                    else if (lObjDet[i].belImposto.belIcms.belICMSSN900 != null)
                    {
                        dgvDet["CstIcms", i].Value = lObjDet[i].belImposto.belIcms.belICMSSN900.CSOSN;
                        dgvDet["OrigIcms", i].Value = lObjDet[i].belImposto.belIcms.belICMSSN900.orig;
                        dgvDet["ModbcIcms", i].Value = lObjDet[i].belImposto.belIcms.belICMSSN900.modBC;
                        dgvDet["VbcIcms", i].Value = lObjDet[i].belImposto.belIcms.belICMSSN900.vBC;
                        dgvDet["VicmsstIcms", i].Value = lObjDet[i].belImposto.belIcms.belICMSSN900.pRedBC;
                        dgvDet["PicmsIcms", i].Value = lObjDet[i].belImposto.belIcms.belICMSSN900.pICMS;
                        dgvDet["VicmsIcms", i].Value = lObjDet[i].belImposto.belIcms.belICMSSN900.vICMS;
                        dgvDet["ModbcstIcms", i].Value = lObjDet[i].belImposto.belIcms.belICMSSN900.modBCST;
                        dgvDet["PmvastIcms", i].Value = lObjDet[i].belImposto.belIcms.belICMSSN900.pMVAST;
                        dgvDet["PredbcstIcms", i].Value = lObjDet[i].belImposto.belIcms.belICMSSN900.pRedBCST;
                        dgvDet["VbcstIcms", i].Value = lObjDet[i].belImposto.belIcms.belICMSSN900.vBCST;
                        dgvDet["PicmsstIcms", i].Value = lObjDet[i].belImposto.belIcms.belICMSSN900.pICMSST;
                        dgvDet["VicmsstIcms", i].Value = lObjDet[i].belImposto.belIcms.belICMSSN900.vICMSST;
                        dgvDet["VbcstIcms", i].Value = lObjDet[i].belImposto.belIcms.belICMSSN900.vBCSTRet;
                        dgvDet["VicmsstIcms", i].Value = lObjDet[i].belImposto.belIcms.belICMSSN900.vICMSSTRet;

                        dgvDet["pCredSN", i].Value = lObjDet[i].belImposto.belIcms.belICMSSN900.pCredSN;
                        dgvDet["vCredICMSSN", i].Value = lObjDet[i].belImposto.belIcms.belICMSSN900.vCredICMSSN;
                    }
                    else if (lObjDet[i].belImposto.belIcms.belIcms00 != null)
                    {
                        dgvDet["CstIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms00.Cst;
                        dgvDet["ModbcIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms00.Modbc;
                        dgvDet["OrigIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms00.Orig;
                        dgvDet["PicmsIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms00.Picms;
                        dgvDet["VbcIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms00.Vbc;
                        dgvDet["VicmsIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms00.Vicms;
                    }
                    else if (lObjDet[i].belImposto.belIcms.belIcms10 != null)
                    {
                        dgvDet["CstIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms10.Cst;
                        dgvDet["ModbcIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms10.Modbc;
                        dgvDet["ModbcstIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms10.Modbcst;
                        dgvDet["OrigIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms10.Orig;
                        dgvDet["PicmsIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms10.Picms;
                        dgvDet["PicmsstIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms10.Picmsst;
                        dgvDet["PmvastIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms10.Pmvast;
                        dgvDet["PredbcstIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms10.Predbcst;
                        dgvDet["VbcIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms10.Vbc;
                        dgvDet["VbcstIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms10.Vbcst;
                        dgvDet["VicmsIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms10.Vicms;
                        dgvDet["VicmsstIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms10.Vicmsst;

                    }
                    else if (lObjDet[i].belImposto.belIcms.belIcms20 != null)
                    {

                        dgvDet["CstIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms20.Cst;
                        dgvDet["ModbcIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms20.Modbc;
                        dgvDet["OrigIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms20.Orig;
                        dgvDet["PicmsIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms20.Picms;
                        dgvDet["PredbcIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms20.Predbc;
                        dgvDet["VbcIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms20.Vbc;
                        dgvDet["VicmsIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms20.Vicms;

                    }
                    else if (lObjDet[i].belImposto.belIcms.belIcms30 != null)
                    {

                        dgvDet["CstIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms30.Cst;
                        dgvDet["ModbcstIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms30.Modbcst;
                        dgvDet["OrigIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms30.Orig;
                        dgvDet["PicmsstIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms30.Picmsst;
                        dgvDet["PmvastIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms30.Pmvast;
                        dgvDet["PredbcstIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms30.Predbcst;
                        dgvDet["VbcstIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms30.Vbcst;
                        dgvDet["VicmsIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms30.Vicmsst;

                    }
                    else if (lObjDet[i].belImposto.belIcms.belIcms40 != null)
                    {
                        dgvDet["CstIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms40.Cst;
                        dgvDet["OrigIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms40.Orig;
                        dgvDet["VicmsIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms40.Vicms; // NFe_2.0
                        dgvDet["motDesICMS", i].Value = lObjDet[i].belImposto.belIcms.belIcms40.motDesICMS;// NFe_2.0


                    }
                    else if (lObjDet[i].belImposto.belIcms.belIcms41 != null)
                    {
                        dgvDet["CstIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms41.Cst;
                        dgvDet["OrigIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms41.Orig;
                        dgvDet["VicmsIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms41.Vicms;// NFe_2.0
                        dgvDet["motDesICMS", i].Value = lObjDet[i].belImposto.belIcms.belIcms41.motDesICMS;// NFe_2.0
                    }
                    else if (lObjDet[i].belImposto.belIcms.belIcms50 != null)
                    {
                        dgvDet["CstIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms50.Cst;
                        dgvDet["OrigIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms50.Orig;
                        dgvDet["VicmsIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms50.Vicms;// NFe_2.0
                        dgvDet["motDesICMS", i].Value = lObjDet[i].belImposto.belIcms.belIcms50.motDesICMS;// NFe_2.0
                    }
                    else if (lObjDet[i].belImposto.belIcms.belIcms51 != null)
                    {
                        dgvDet["CstIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms51.Cst;
                        dgvDet["OrigIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms51.Orig;
                        dgvDet["ModbcIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms51.Modbc;
                        dgvDet["PredbcIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms51.Predbc;
                        dgvDet["PicmsIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms51.Picms;
                        dgvDet["VicmsIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms51.Vicms;
                        dgvDet["VbcIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms51.Vbc;

                    }
                    else if (lObjDet[i].belImposto.belIcms.belIcms60 != null)
                    {

                        dgvDet["CstIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms60.Cst;
                        dgvDet["OrigIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms60.Orig;
                        dgvDet["VbcstIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms60.Vbcst;
                        dgvDet["VicmsstIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms60.Vicmsst;

                    }
                    else if (lObjDet[i].belImposto.belIcms.belIcms70 != null)
                    {

                        dgvDet["CstIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms70.Cst;
                        dgvDet["ModbcIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms70.Modbc;
                        dgvDet["ModbcstIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms70.Modbcst;
                        dgvDet["OrigIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms70.Orig;
                        dgvDet["PicmsIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms70.Picms;
                        dgvDet["PicmsstIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms70.Picmsst;
                        dgvDet["PmvastIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms70.Pmvast;
                        dgvDet["PredbcIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms70.Predbc;
                        dgvDet["PredbcstIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms70.Predbcst;
                        dgvDet["VbcIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms70.Vbc;
                        dgvDet["VbcstIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms70.Vbcst;
                        dgvDet["VicmsIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms70.Vicms;
                        dgvDet["VicmsstIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms70.Vicmsst;

                    }
                    else if (lObjDet[i].belImposto.belIcms.belIcms90 != null)
                    {


                        dgvDet["CstIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms90.Cst;
                        dgvDet["ModbcIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms90.Modbc;
                        dgvDet["ModbcstIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms90.Modbcst;
                        dgvDet["OrigIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms90.Orig;
                        dgvDet["PicmsIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms90.Picms;
                        dgvDet["PicmsstIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms90.Picmsst;
                        dgvDet["PmvastIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms90.Pmvast;
                        dgvDet["PredbcIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms90.Predbc;
                        dgvDet["PredbcstIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms90.Predbcst;
                        dgvDet["VbcIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms90.Vbc;
                        dgvDet["VbcstIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms90.Vbcst;
                        dgvDet["VicmsIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms90.Vicms;
                        dgvDet["VicmsstIcms", i].Value = lObjDet[i].belImposto.belIcms.belIcms90.Vicmsst;

                    }


                    #endregion

                    #region IPI


                    dgvDet["CenqIpi", i].Value = lObjDet[i].belImposto.belIpi.Cenq;

                    if (lObjDet[i].belImposto.belIpi.belIpitrib != null)
                    {

                        dgvDet["CstIpi", i].Value = lObjDet[i].belImposto.belIpi.belIpitrib.Cst;
                        dgvDet["PipiTrib", i].Value = lObjDet[i].belImposto.belIpi.belIpitrib.Pipi;
                        dgvDet["QunidIpiTrib", i].Value = lObjDet[i].belImposto.belIpi.belIpitrib.Qunid;
                        dgvDet["VbcIpiTrib", i].Value = lObjDet[i].belImposto.belIpi.belIpitrib.Vbc;
                        dgvDet["VipiTrib", i].Value = lObjDet[i].belImposto.belIpi.belIpitrib.Vipi;//Danner - o.s. 24154 - 19/02/2010
                        dgvDet["VunidTrib", i].Value = lObjDet[i].belImposto.belIpi.belIpitrib.Vunid;

                    }
                    else
                    {
                        dgvDet["CstIpi", i].Value = lObjDet[i].belImposto.belIpi.belIpint.Cst;

                    }

                    #endregion

                    #region II



                    if (lObjDet[i].belImposto.belIi != null)
                    {

                        dgvDet["VbcIi", i].Value = lObjDet[i].belImposto.belIi.Vbc;
                        dgvDet["VdespaduIi", i].Value = lObjDet[i].belImposto.belIi.Vdespadu;
                        dgvDet["Vii", i].Value = lObjDet[i].belImposto.belIi.Vii;
                        dgvDet["ViofIi", i].Value = lObjDet[i].belImposto.belIi.Viof;

                    }
                    #endregion

                    #region PIS

                    if (lObjDet[i].belImposto.belPis.belPisaliq != null)
                    {
                        dgvDet["CstPis", i].Value = lObjDet[i].belImposto.belPis.belPisaliq.Cst;
                        dgvDet["Ppis", i].Value = lObjDet[i].belImposto.belPis.belPisaliq.Ppis;
                        dgvDet["VbcPis", i].Value = lObjDet[i].belImposto.belPis.belPisaliq.Vbc;
                        dgvDet["Vpis", i].Value = lObjDet[i].belImposto.belPis.belPisaliq.Vpis;

                    }
                    else if (lObjDet[i].belImposto.belPis.belPisqtde != null)
                    {

                        dgvDet["CstPis", i].Value = lObjDet[i].belImposto.belPis.belPisqtde.Cst;
                        dgvDet["ValiqprodPis", i].Value = lObjDet[i].belImposto.belPis.belPisqtde.Valiqprod;
                        dgvDet["QbcprodPis", i].Value = lObjDet[i].belImposto.belPis.belPisqtde.Qbcprod;
                        dgvDet["Vpis", i].Value = lObjDet[i].belImposto.belPis.belPisqtde.Vpis;

                    }
                    else if (lObjDet[i].belImposto.belPis.belPisnt != null)
                    {
                        dgvDet["CstPis", i].Value = lObjDet[i].belImposto.belPis.belPisnt.Cst;

                    }
                    else if (lObjDet[i].belImposto.belPis.belPisoutr != null)
                    {

                        dgvDet["CstPis", i].Value = lObjDet[i].belImposto.belPis.belPisoutr.Cst;
                        dgvDet["Ppis", i].Value = lObjDet[i].belImposto.belPis.belPisoutr.Ppis;
                        dgvDet["VbcPis", i].Value = lObjDet[i].belImposto.belPis.belPisoutr.Vbc;
                        dgvDet["ValiqprodPis", i].Value = lObjDet[i].belImposto.belPis.belPisoutr.Valiqprod;
                        dgvDet["QbcprodPis", i].Value = lObjDet[i].belImposto.belPis.belPisoutr.Qbcprod;
                        dgvDet["Vpis", i].Value = lObjDet[i].belImposto.belPis.belPisoutr.Vpis;

                    }

                    #endregion

                    #region COFINS

                    if (lObjDet[i].belImposto.belCofins.belCofinsaliq != null)
                    {
                        dgvDet["CstCofins", i].Value = lObjDet[i].belImposto.belCofins.belCofinsaliq.Cst;
                        dgvDet["Pcofins", i].Value = lObjDet[i].belImposto.belCofins.belCofinsaliq.Pcofins;
                        dgvDet["VbcCofins", i].Value = lObjDet[i].belImposto.belCofins.belCofinsaliq.Vbc;
                        dgvDet["Vconfins", i].Value = lObjDet[i].belImposto.belCofins.belCofinsaliq.Vcofins;
                    }
                    else if (lObjDet[i].belImposto.belCofins.belCofinsqtde != null)
                    {
                        dgvDet["CstCofins", i].Value = lObjDet[i].belImposto.belCofins.belCofinsqtde.Cst;
                        dgvDet["QbcprodCofins", i].Value = lObjDet[i].belImposto.belCofins.belCofinsqtde.Qbcprod;
                        dgvDet["ValiqprodCofins", i].Value = lObjDet[i].belImposto.belCofins.belCofinsqtde.Valiqprod;
                        dgvDet["Vconfins", i].Value = lObjDet[i].belImposto.belCofins.belCofinsqtde.Vcofins;

                    }
                    else if (lObjDet[i].belImposto.belCofins.belCofinsnt != null)
                    {
                        dgvDet["CstCofins", i].Value = lObjDet[i].belImposto.belCofins.belCofinsnt.Cst;
                    }
                    else if (lObjDet[i].belImposto.belCofins.belCofinsoutr != null)
                    {
                        dgvDet["CstCofins", i].Value = lObjDet[i].belImposto.belCofins.belCofinsoutr.Cst;
                        dgvDet["Pcofins", i].Value = lObjDet[i].belImposto.belCofins.belCofinsoutr.Pcofins;
                        dgvDet["VbcCofins", i].Value = lObjDet[i].belImposto.belCofins.belCofinsoutr.Vbc; ;
                        dgvDet["QbcprodCofins", i].Value = lObjDet[i].belImposto.belCofins.belCofinsoutr.Qbcprod;
                        dgvDet["ValiqprodCofins", i].Value = lObjDet[i].belImposto.belCofins.belCofinsoutr.Valiqprod;
                        dgvDet["Vconfins", i].Value = lObjDet[i].belImposto.belCofins.belCofinsoutr.Vcofins;

                    }

                    #endregion

                    #region ISSQN

                    if (lObjDet[i].belImposto.belIss != null)
                    {
                        dgvDet["ClistservIss", i].Value = lObjDet[i].belImposto.belIss.Clistserv;
                        dgvDet["CmunfgIss", i].Value = lObjDet[i].belImposto.belIss.Cmunfg;
                        dgvDet["ValiqIss", i].Value = lObjDet[i].belImposto.belIss.Valiq;
                        dgvDet["VbcIss", i].Value = lObjDet[i].belImposto.belIss.Vbc;
                        dgvDet["VissqnIss", i].Value = lObjDet[i].belImposto.belIss.Vissqn;

                    }

                    #endregion

                    #endregion

                    #region InfadProd
                    if (lObjDet[i].belInfadprod != null)
                    {

                        dgvDet["Infcpl", i].Value = Convert.ToString(lObjDet[i].belInfadprod.Infadprid);

                    }

                    #endregion
                }

                //Desabilita Campos Não utilizados
                for (int c = 0; c < dgvDet.Columns.Count; c++)
                {
                    bool bcoluna = false;
                    for (int i = 0; i < dgvDet.RowCount; i++)
                    {
                        if (dgvDet[c, i].Value != null)
                        {
                            bcoluna = true;
                            break;
                        }
                    }
                    dgvDet.Columns[c].Visible = bcoluna;
                }

                int IcountExport = lObj.Where(p => (p.belProd.Cfop == "7201") || (p.belProd.Cfop == "7101") || (p.belProd.Cfop == "7949") || (p.belProd.Cfop == "7930") || (p.belProd.Cfop == "7102")).Count(); //25679


                if (IcountExport > 0)
                {
                    groupExportacao.Visible = true;
                    errorProvider1.SetError(cbxUF_embarque, "Campo obrigatório para Exportação");
                    errorProvider1.SetError(txtLocalEntrega, "Campo obrigatório para Exportação");
                }
                else
                {
                    groupExportacao.Visible = false;
                }

            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(ex.Message);
            }
        }
        private void populaDup(List<belDup> lObjDup)
        {
            dgvDup.Rows.Clear();
            try
            {
                if (lObjDup != null)
                {
                    for (int i = 0; i < lObjDup.Count; i++)
                    {
                        dgvDup.Rows.Add();
                        dgvDup[0, i].Value = lObjDup[i].Ndup;
                        dgvDup[1, i].Value = lObjDup[i].Dvenc;
                        dgvDup[2, i].Value = lObjDup[i].Vdup;

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void geraDgvDup()
        {
            List<DataGridViewColumn> dgvDupColumns = new List<DataGridViewColumn>();

            DataGridViewTextBoxColumn nDup = new DataGridViewTextBoxColumn();
            nDup.MaxInputLength = 60;
            nDup.Name = "nDup";
            nDup.DataPropertyName = "Ndup";
            nDup.HeaderText = "Número";
            dgvDupColumns.Add(nDup);

            MaskedEditColumn dVenc = new MaskedEditColumn();
            dVenc.Name = "Dvenc";
            dVenc.DataPropertyName = "Dvenc";
            dVenc.HeaderText = "Data Venc.";
            dVenc.Mask = "00/00/0000";
            dVenc.ValidatingType = typeof(DateTime);
            dgvDupColumns.Add(dVenc);

            DataGridViewTextBoxColumn vDup = new DataGridViewTextBoxColumn();
            vDup.Name = "Vdup";
            vDup.DataPropertyName = "Vdup";
            vDup.HeaderText = "Valor";
            vDup.MaxInputLength = 15;
            dgvDupColumns.Add(vDup);

            dgvDup.Columns.AddRange(dgvDupColumns.ToArray());
        }
        private void geraDgvProd()
        {
            List<DataGridViewColumn> lColumn = new List<DataGridViewColumn>();

            #region Prod

            DataGridViewTextBoxColumn cProd = new DataGridViewTextBoxColumn();
            cProd.MaxInputLength = 7;
            cProd.Name = "Cprod";
            cProd.HeaderText = "Código";
            cProd.DataPropertyName = "Cprod";
            lColumn.Add(cProd);

            DataGridViewTextBoxColumn cEAN = new DataGridViewTextBoxColumn();
            cEAN.MaxInputLength = 14;
            cEAN.Name = "Cean";
            cEAN.HeaderText = "Cod. EAN";
            cEAN.DataPropertyName = "Cean";
            lColumn.Add(cEAN);

            DataGridViewTextBoxColumn xProd = new DataGridViewTextBoxColumn();
            xProd.MaxInputLength = 120;
            xProd.Name = "Xprod";
            xProd.HeaderText = "Nome";
            xProd.DataPropertyName = "Xprod";
            xProd.Width = 200;
            lColumn.Add(xProd);

            DataGridViewTextBoxColumn NCM = new DataGridViewTextBoxColumn();
            NCM.MaxInputLength = 8;
            NCM.Name = "NCM";
            NCM.HeaderText = "NCM";
            NCM.DataPropertyName = "NCM";
            lColumn.Add(NCM);

            DataGridViewTextBoxColumn EXTIPI = new DataGridViewTextBoxColumn();
            EXTIPI.MaxInputLength = 3;
            EXTIPI.Name = "EXTIPI";
            EXTIPI.HeaderText = "EX_TIPI";
            EXTIPI.DataPropertyName = "Extipi";
            lColumn.Add(EXTIPI);

            DataGridViewTextBoxColumn genero = new DataGridViewTextBoxColumn();
            genero.MaxInputLength = 2;
            genero.Name = "Genero";
            genero.HeaderText = "Genero";
            genero.DataPropertyName = "Genero";
            lColumn.Add(genero);

            DataGridViewTextBoxColumn CFOP = new DataGridViewTextBoxColumn();
            CFOP.MaxInputLength = 4;
            CFOP.Name = "CFOP";
            CFOP.HeaderText = "CFOP";
            CFOP.DataPropertyName = "Cfop";
            lColumn.Add(CFOP);

            DataGridViewTextBoxColumn uCom = new DataGridViewTextBoxColumn();
            uCom.MaxInputLength = 6;
            uCom.Name = "uCom";
            uCom.HeaderText = "Unidade Comercial";
            uCom.DataPropertyName = "Ucom";
            lColumn.Add(uCom);

            DataGridViewTextBoxColumn qCom = new DataGridViewTextBoxColumn();
            qCom.MaxInputLength = 12;
            qCom.Name = "Qcom";
            qCom.HeaderText = "Qtde Comercial";
            qCom.DataPropertyName = "Qcom";
            lColumn.Add(qCom);

            DataGridViewTextBoxColumn vUnCom = new DataGridViewTextBoxColumn();
            vUnCom.MaxInputLength = 16;
            vUnCom.Name = "Vuncom";
            vUnCom.HeaderText = "Valor Unit. Comerc.";
            vUnCom.DataPropertyName = "Vuncom";
            vUnCom.DefaultCellStyle.Format = "n" + sCasasVlUnit;

            lColumn.Add(vUnCom);

            DataGridViewTextBoxColumn vProd = new DataGridViewTextBoxColumn();
            vProd.MaxInputLength = 15;
            vProd.Name = "Vprod";
            vProd.HeaderText = "Valor Tot. Prod";
            vProd.DataPropertyName = "Vprod";
            vProd.DefaultCellStyle.Format = "n2";
            lColumn.Add(vProd);

            DataGridViewTextBoxColumn cEANTrib = new DataGridViewTextBoxColumn();
            cEANTrib.MaxInputLength = 14;
            cEANTrib.Name = "Ceantrib";
            cEANTrib.HeaderText = "Cod. EAN Trib.";
            cEANTrib.DataPropertyName = "Ceantrib";
            lColumn.Add(cEANTrib);

            DataGridViewTextBoxColumn uTrib = new DataGridViewTextBoxColumn();
            uTrib.MaxInputLength = 6;
            uTrib.Name = "Utrib";
            uTrib.HeaderText = "Unid. Tributavel";
            uTrib.DataPropertyName = "Utrib";
            lColumn.Add(uTrib);

            DataGridViewTextBoxColumn qTrib = new DataGridViewTextBoxColumn();
            qTrib.MaxInputLength = 12;
            qTrib.Name = "Qtrib";
            qTrib.HeaderText = "Qtde Tributavel";
            qTrib.DataPropertyName = "Qtrib";
            lColumn.Add(qTrib);

            DataGridViewTextBoxColumn vUnTrib = new DataGridViewTextBoxColumn();
            vUnTrib.MaxInputLength = 16;
            vUnTrib.Name = "Vuntrib";
            vUnTrib.HeaderText = "Valor Unit. Tribut.";
            vUnTrib.DataPropertyName = "Vuntrib";
            vUnCom.DefaultCellStyle.Format = "n" + sCasasVlUnit;
            lColumn.Add(vUnTrib);

            DataGridViewTextBoxColumn vFrete = new DataGridViewTextBoxColumn();
            vFrete.MaxInputLength = 15;
            vFrete.Name = "vFrete";
            vFrete.HeaderText = "Valor Frete";
            vFrete.DataPropertyName = "Vfrete";
            vFrete.DefaultCellStyle.Format = "n5";
            lColumn.Add(vFrete);

            DataGridViewTextBoxColumn vSeg = new DataGridViewTextBoxColumn();
            vSeg.MaxInputLength = 15;
            vSeg.Name = "Vseg";
            vSeg.HeaderText = "Valor Seguro";
            vSeg.DataPropertyName = "Vseg";
            lColumn.Add(vSeg);

            DataGridViewTextBoxColumn vDesc = new DataGridViewTextBoxColumn();
            vDesc.MaxInputLength = 15;
            vDesc.Name = "Vdesc";
            vDesc.HeaderText = "Valor Desconto";
            vDesc.DataPropertyName = "Vdesc";
            vDesc.DefaultCellStyle.Format = "n5";
            lColumn.Add(vDesc);

            DataGridViewTextBoxColumn VOutro = new DataGridViewTextBoxColumn(); // NFe_2.0
            VOutro.MaxInputLength = 15;
            VOutro.Name = "VOutro";
            VOutro.HeaderText = "Outros Valores";
            VOutro.DataPropertyName = "VOutro";
            VOutro.DefaultCellStyle.Format = "n5";
            lColumn.Add(VOutro);

            DataGridViewTextBoxColumn indTot = new DataGridViewTextBoxColumn(); // NFe_2.0
            indTot.MaxInputLength = 1;
            indTot.Name = "indTot";
            indTot.HeaderText = "Compõe Total da NF";
            indTot.DataPropertyName = "indTot";
            lColumn.Add(indTot);

            DataGridViewTextBoxColumn xPed = new DataGridViewTextBoxColumn(); // NFe_2.0
            xPed.MaxInputLength = 15;
            xPed.Name = "xPed";
            xPed.HeaderText = "Nr_Ped_Compra";
            xPed.DataPropertyName = "xPed";
            lColumn.Add(xPed);

            DataGridViewTextBoxColumn nItemPed = new DataGridViewTextBoxColumn(); // NFe_2.0
            nItemPed.MaxInputLength = 6;
            nItemPed.Name = "nItemPed";
            nItemPed.HeaderText = "Nr_Item_Ped_Compra";
            nItemPed.DataPropertyName = "nItemPed";
            lColumn.Add(nItemPed);

            #endregion

            #region ICMS

            DataGridViewTextBoxColumn CstIcms = new DataGridViewTextBoxColumn();
            CstIcms.MaxInputLength = 2;
            CstIcms.Name = "CstIcms";
            CstIcms.HeaderText = "CST ICMS";
            CstIcms.DataPropertyName = "CstIcms";
            lColumn.Add(CstIcms);

            DataGridViewTextBoxColumn OrigIcms = new DataGridViewTextBoxColumn();
            OrigIcms.MaxInputLength = 1;
            OrigIcms.Name = "OrigIcms";
            OrigIcms.HeaderText = "Origem";
            OrigIcms.DataPropertyName = "OrigIcms";
            lColumn.Add(OrigIcms);

            DataGridViewTextBoxColumn pCredSN = new DataGridViewTextBoxColumn();
            pCredSN.MaxInputLength = 8;
            pCredSN.Name = "pCredSN";
            pCredSN.HeaderText = "Alíq. Crédito";
            pCredSN.DataPropertyName = "pCredSN";
            lColumn.Add(pCredSN);

            DataGridViewTextBoxColumn vCredICMSSN = new DataGridViewTextBoxColumn();
            vCredICMSSN.MaxInputLength = 1;
            vCredICMSSN.Name = "vCredICMSSN";
            vCredICMSSN.HeaderText = "Valor. Crédito";
            vCredICMSSN.DataPropertyName = "vCredICMSSN";
            vCredICMSSN.DefaultCellStyle.Format = "n5";
            lColumn.Add(vCredICMSSN);

            DataGridViewTextBoxColumn ModbcIcms = new DataGridViewTextBoxColumn();
            ModbcIcms.MaxInputLength = 1;
            ModbcIcms.Name = "ModbcIcms";
            ModbcIcms.HeaderText = "Mod. BC";
            ModbcIcms.DataPropertyName = "ModbcIcms";
            lColumn.Add(ModbcIcms);

            DataGridViewTextBoxColumn VbcIcms = new DataGridViewTextBoxColumn();
            VbcIcms.MaxInputLength = 15;
            VbcIcms.Name = "VbcIcms";
            VbcIcms.HeaderText = "Base Calc. ICMS";
            VbcIcms.DataPropertyName = "VbcIcms";
            VbcIcms.DefaultCellStyle.Format = "n5";
            lColumn.Add(VbcIcms);

            DataGridViewTextBoxColumn PredbcIcms = new DataGridViewTextBoxColumn();
            PredbcIcms.MaxInputLength = 5;
            PredbcIcms.Name = "PredbcIcms";
            PredbcIcms.HeaderText = "Redução BC(%)";
            PredbcIcms.DataPropertyName = "PredbcIcms";
            lColumn.Add(PredbcIcms);

            DataGridViewTextBoxColumn PicmsIcms = new DataGridViewTextBoxColumn();
            PicmsIcms.MaxInputLength = 5;
            PicmsIcms.Name = "PicmsIcms";
            PicmsIcms.HeaderText = "Aliq. ICMS";
            PicmsIcms.DataPropertyName = "PicmsIcms";
            lColumn.Add(PicmsIcms);

            DataGridViewTextBoxColumn VicmsIcms = new DataGridViewTextBoxColumn();
            VicmsIcms.MaxInputLength = 15;
            VicmsIcms.Name = "VicmsIcms";
            VicmsIcms.HeaderText = "Valor ICMS";
            VicmsIcms.DataPropertyName = "VicmsIcms";
            VicmsIcms.DefaultCellStyle.Format = "n5";
            lColumn.Add(VicmsIcms);

            DataGridViewTextBoxColumn ModbcstIcms = new DataGridViewTextBoxColumn();
            ModbcstIcms.MaxInputLength = 1;
            ModbcstIcms.Name = "ModbcstIcms";
            ModbcstIcms.HeaderText = "Mod. BC ST";
            ModbcstIcms.DataPropertyName = "ModbcstIcms";
            lColumn.Add(ModbcstIcms);

            DataGridViewTextBoxColumn motDesICMS = new DataGridViewTextBoxColumn();
            motDesICMS.MaxInputLength = 1;
            motDesICMS.Name = "motDesICMS";
            motDesICMS.HeaderText = "Mot.Des.ICMS";
            motDesICMS.DataPropertyName = "motDesICMS";
            lColumn.Add(motDesICMS);

            DataGridViewTextBoxColumn PmvastIcms = new DataGridViewTextBoxColumn();
            PmvastIcms.MaxInputLength = 5;
            PmvastIcms.Name = "PmvastIcms";
            PmvastIcms.HeaderText = "MVAST";
            PmvastIcms.DataPropertyName = "PmvastIcms";
            lColumn.Add(PmvastIcms);

            DataGridViewTextBoxColumn PredbcstIcms = new DataGridViewTextBoxColumn();
            PredbcstIcms.MaxInputLength = 5;
            PredbcstIcms.Name = "PredbcstIcms";
            PredbcstIcms.HeaderText = "Redução BC ST(%)";
            PredbcstIcms.DataPropertyName = "PredbcstIcms";
            lColumn.Add(PredbcstIcms);

            DataGridViewTextBoxColumn VbcstIcms = new DataGridViewTextBoxColumn();
            VbcstIcms.MaxInputLength = 15;
            VbcstIcms.Name = "VbcstIcms";
            VbcstIcms.HeaderText = "Valor BC ST";
            VbcstIcms.DataPropertyName = "VbcstIcms";
            VbcstIcms.DefaultCellStyle.Format = "n5";
            lColumn.Add(VbcstIcms);


            DataGridViewTextBoxColumn PicmsstIcms = new DataGridViewTextBoxColumn();
            PicmsstIcms.MaxInputLength = 5;
            PicmsstIcms.Name = "PicmsstIcms";
            PicmsstIcms.HeaderText = "Aliq. ICMS ST(%)";
            PicmsstIcms.DataPropertyName = "PicmsstIcms";
            lColumn.Add(PicmsstIcms);

            DataGridViewTextBoxColumn VicmsstIcms = new DataGridViewTextBoxColumn();
            VicmsstIcms.MaxInputLength = 15;
            VicmsstIcms.Name = "VicmsstIcms";
            VicmsstIcms.HeaderText = "Valor ICMS ST";
            VicmsstIcms.DataPropertyName = "VicmsstIcms";
            VicmsstIcms.DefaultCellStyle.Format = "n5";
            lColumn.Add(VicmsstIcms);

            #endregion

            #region IPI

            DataGridViewTextBoxColumn CenqIpi = new DataGridViewTextBoxColumn();
            CenqIpi.MaxInputLength = 3;
            CenqIpi.Name = "CenqIpi";
            CenqIpi.HeaderText = "Enquadramento Legal";
            CenqIpi.DataPropertyName = "CenqIpi";
            lColumn.Add(CenqIpi);

            DataGridViewTextBoxColumn CstIpi = new DataGridViewTextBoxColumn();
            CstIpi.MaxInputLength = 2;
            CstIpi.Name = "CstIpi";
            CstIpi.HeaderText = "Situação Trib. IPI";
            CstIpi.DataPropertyName = "CstIpi";
            lColumn.Add(CstIpi);

            DataGridViewTextBoxColumn QunidIpiTrib = new DataGridViewTextBoxColumn();
            QunidIpiTrib.MaxInputLength = 16;
            QunidIpiTrib.Name = "QunidIpiTrib";
            QunidIpiTrib.HeaderText = "Qtde Tot. p/ Trib.";
            QunidIpiTrib.DataPropertyName = "QunidIpiTrib";
            lColumn.Add(QunidIpiTrib);

            DataGridViewTextBoxColumn VunidTrib = new DataGridViewTextBoxColumn();
            VunidTrib.MaxInputLength = 15;
            VunidTrib.Name = "VunidTrib";
            VunidTrib.HeaderText = "Val. Unid. Trib.";
            VunidTrib.DataPropertyName = "VunidTrib";
            VunidTrib.DefaultCellStyle.Format = "n5";
            lColumn.Add(VunidTrib);

            DataGridViewTextBoxColumn PipiTrib = new DataGridViewTextBoxColumn();
            PipiTrib.MaxInputLength = 5;
            PipiTrib.Name = "PipiTrib";
            PipiTrib.HeaderText = "Aliq IPI (%)";
            PipiTrib.DataPropertyName = "PipiTrib";
            lColumn.Add(PipiTrib);

            DataGridViewTextBoxColumn VipiTrib = new DataGridViewTextBoxColumn();
            VipiTrib.MaxInputLength = 15;
            VipiTrib.Name = "VipiTrib";
            VipiTrib.HeaderText = "Valor IPI";
            VipiTrib.DataPropertyName = "VipiTrib";
            VipiTrib.DefaultCellStyle.Format = "n5";
            lColumn.Add(VipiTrib);

            DataGridViewTextBoxColumn VbcIpiTrib = new DataGridViewTextBoxColumn();
            VbcIpiTrib.MaxInputLength = 15;
            VbcIpiTrib.Name = "VbcIpiTrib";
            VbcIpiTrib.HeaderText = "Base BC IPI";
            VbcIpiTrib.DataPropertyName = "VbcIpiTrib";
            VbcIpiTrib.DefaultCellStyle.Format = "n5";
            lColumn.Add(VbcIpiTrib);

            //Fim - IpiTrib


            #endregion

            #region II

            DataGridViewTextBoxColumn VbcIi = new DataGridViewTextBoxColumn();
            VbcIi.MaxInputLength = 15;
            VbcIi.Name = "VbcIi";
            VbcIi.HeaderText = "Valor BC II";
            VbcIi.DataPropertyName = "VbcIi";
            lColumn.Add(VbcIi);

            DataGridViewTextBoxColumn VdespaduIi = new DataGridViewTextBoxColumn();
            VdespaduIi.MaxInputLength = 15;
            VdespaduIi.Name = "VdespaduIi";
            VdespaduIi.HeaderText = "Val. Disp. Aduaneiras";
            VdespaduIi.DataPropertyName = "VdespaduIi";
            lColumn.Add(VdespaduIi);

            DataGridViewTextBoxColumn Vii = new DataGridViewTextBoxColumn();
            Vii.MaxInputLength = 15;
            Vii.Name = "Vii";
            Vii.HeaderText = "Valor II";
            Vii.DataPropertyName = "Vii";
            lColumn.Add(Vii);

            DataGridViewTextBoxColumn ViofIi = new DataGridViewTextBoxColumn();
            ViofIi.MaxInputLength = 15;
            ViofIi.Name = "ViofIi";
            ViofIi.HeaderText = "Val. IOF";
            ViofIi.DataPropertyName = "ViofIi";
            lColumn.Add(ViofIi);

            #endregion

            #region PIS

            DataGridViewTextBoxColumn CstPis = new DataGridViewTextBoxColumn();
            CstPis.MaxInputLength = 2;
            CstPis.Name = "CstPis";
            CstPis.HeaderText = "Cod. ST PIS";
            CstPis.DataPropertyName = "CstPis";
            lColumn.Add(CstPis);

            DataGridViewTextBoxColumn VbcPis = new DataGridViewTextBoxColumn();
            VbcPis.MaxInputLength = 15;
            VbcPis.Name = "VbcPis";
            VbcPis.HeaderText = "Valor BC PIS";
            VbcPis.DataPropertyName = "VbcPis";
            VbcPis.DefaultCellStyle.Format = "n5";
            lColumn.Add(VbcPis);

            DataGridViewTextBoxColumn Ppis = new DataGridViewTextBoxColumn();
            Ppis.MaxInputLength = 5;
            Ppis.Name = "Ppis";
            Ppis.HeaderText = "Aliq PIS";
            Ppis.DataPropertyName = "Ppis";
            lColumn.Add(Ppis);

            DataGridViewTextBoxColumn QbcprodPis = new DataGridViewTextBoxColumn();
            QbcprodPis.MaxInputLength = 16;
            QbcprodPis.Name = "QbcprodPis";
            QbcprodPis.HeaderText = "Qtde Vendida";
            QbcprodPis.DataPropertyName = "QbcprodPis";
            lColumn.Add(QbcprodPis);

            DataGridViewTextBoxColumn ValiqprodPis = new DataGridViewTextBoxColumn();
            ValiqprodPis.MaxInputLength = 15;
            ValiqprodPis.Name = "ValiqprodPis";
            ValiqprodPis.HeaderText = "Aliq. PIS Qtde (%)";
            ValiqprodPis.DataPropertyName = "ValiqprodPis";
            lColumn.Add(ValiqprodPis);

            DataGridViewTextBoxColumn Vpis = new DataGridViewTextBoxColumn();
            Vpis.MaxInputLength = 15;
            Vpis.Name = "Vpis";
            Vpis.HeaderText = "Valor PIS";
            Vpis.DataPropertyName = "Vpis";
            Vpis.DefaultCellStyle.Format = "n5";
            lColumn.Add(Vpis);

            #endregion

            #region COFINS

            DataGridViewTextBoxColumn CstCofins = new DataGridViewTextBoxColumn();
            CstCofins.MaxInputLength = 2;
            CstCofins.Name = "CstCofins";
            CstCofins.HeaderText = "Cod. ST COFINS";
            CstCofins.DataPropertyName = "CstCofins";
            lColumn.Add(CstCofins);

            DataGridViewTextBoxColumn VbcCofins = new DataGridViewTextBoxColumn();
            VbcCofins.MaxInputLength = 15;
            VbcCofins.Name = "VbcCofins";
            VbcCofins.HeaderText = "Val. BC COFINS";
            VbcCofins.DataPropertyName = "VbcCofins";
            VbcCofins.DefaultCellStyle.Format = "n5";
            lColumn.Add(VbcCofins);


            DataGridViewTextBoxColumn Pcofins = new DataGridViewTextBoxColumn();
            Pcofins.MaxInputLength = 5;
            Pcofins.Name = "Pcofins";
            Pcofins.HeaderText = "Aliq. COFINS (%)";
            Pcofins.DataPropertyName = "Pcofins";
            lColumn.Add(Pcofins);

            DataGridViewTextBoxColumn QbcprodCofins = new DataGridViewTextBoxColumn();
            QbcprodCofins.MaxInputLength = 16;
            QbcprodCofins.Name = "QbcprodCofins";
            QbcprodCofins.HeaderText = "Qtde Vendida";
            QbcprodCofins.DataPropertyName = "QbcprodCofins";
            lColumn.Add(QbcprodCofins);

            DataGridViewTextBoxColumn ValiqprodCofins = new DataGridViewTextBoxColumn();
            ValiqprodCofins.MaxInputLength = 15;
            ValiqprodCofins.Name = "ValiqprodCofins";
            ValiqprodCofins.HeaderText = "Aliq. COFINS QTDE (%)";
            ValiqprodCofins.DataPropertyName = "ValiqprodCofins";
            lColumn.Add(ValiqprodCofins);

            DataGridViewTextBoxColumn Vconfins = new DataGridViewTextBoxColumn();
            Vconfins.MaxInputLength = 15;
            Vconfins.Name = "Vconfins";
            Vconfins.HeaderText = "Valor COFINS";
            Vconfins.DataPropertyName = "Vconfins";
            Vconfins.DefaultCellStyle.Format = "n5";
            lColumn.Add(Vconfins);

            #endregion

            #region ISSQN
            DataGridViewTextBoxColumn VbcIss = new DataGridViewTextBoxColumn();
            VbcIss.MaxInputLength = 15;
            VbcIss.Name = "VbcIss";
            VbcIss.HeaderText = "Valor BC ISS";
            VbcIss.DataPropertyName = "VbcIss";
            lColumn.Add(VbcIss);

            DataGridViewTextBoxColumn ValiqIss = new DataGridViewTextBoxColumn();
            ValiqIss.MaxInputLength = 5;
            ValiqIss.Name = "ValiqIss";
            ValiqIss.HeaderText = "Aliq. ISSQN";
            ValiqIss.DataPropertyName = "ValiqIss";
            lColumn.Add(ValiqIss);

            DataGridViewTextBoxColumn VissqnIss = new DataGridViewTextBoxColumn();
            VissqnIss.MaxInputLength = 15;
            VissqnIss.Name = "VissqnIss";
            VissqnIss.HeaderText = "Valor ISSQN";
            VissqnIss.DataPropertyName = "VissqnIss";
            VissqnIss.DefaultCellStyle.Format = "n5";
            lColumn.Add(VissqnIss);

            DataGridViewTextBoxColumn CmunfgIss = new DataGridViewTextBoxColumn();
            CmunfgIss.MaxInputLength = 7;
            CmunfgIss.Name = "CmunfgIss";
            CmunfgIss.HeaderText = "Cod. Município";
            CmunfgIss.DataPropertyName = "CmunfgIss";
            lColumn.Add(CmunfgIss);

            DataGridViewTextBoxColumn ClistservIss = new DataGridViewTextBoxColumn();
            ClistservIss.MaxInputLength = 4;
            ClistservIss.Name = "ClistservIss";
            ClistservIss.HeaderText = "Cod. Lista Serv.";
            ClistservIss.DataPropertyName = "ClistservIss";
            lColumn.Add(ClistservIss);

            #endregion

            #region InfAdic

            DataGridViewTextBoxColumn Infcpl = new DataGridViewTextBoxColumn();
            Infcpl.MaxInputLength = 500;
            Infcpl.Name = "Infcpl";
            Infcpl.HeaderText = "Informações";
            Infcpl.DataPropertyName = "Infcpl";
            Infcpl.Width = 300;
            lColumn.Add(Infcpl);

            #endregion



            dgvDet.Columns.AddRange(lColumn.ToArray());
        }
        private void HabilitaCampos(Control.ControlCollection ctrltela, bool pHabilita)
        {
            foreach (Control ctr in ctrltela)
            {
                if (ctr.HasChildren == true)
                {
                    HabilitaCampos(ctr.Controls, pHabilita);
                }
                else
                {
                    if (ctr is TextBox)
                    {
                        if (((TextBox)ctr).Name.ToString() != "txtCodigo")
                        {
                            ctr.Enabled = pHabilita;
                        }
                    }
                    if (ctr is MaskedTextBox) { ctr.Enabled = pHabilita; }
                    if (ctr is ComboBox) { ((ComboBox)ctr).Enabled = pHabilita; }
                    if (ctr is NumericUpDown) { ((NumericUpDown)ctr).Enabled = pHabilita; }
                    if (ctr is DateTimePicker) { ((DateTimePicker)ctr).Enabled = pHabilita; }
                    if (ctr is RadioButton) { ((RadioButton)ctr).Enabled = pHabilita; }
                    if (ctr is Button) { ((Button)ctr).Enabled = pHabilita; }
                    if (ctr is ListBox) { ((ListBox)ctr).Enabled = pHabilita; }
                    if (ctr is CheckBox) { ((CheckBox)ctr).Enabled = pHabilita; }
                    if (ctr is RichTextBox) { ((RichTextBox)ctr).Enabled = pHabilita; }
                    if (ctr is NumericUpDown) { ((NumericUpDown)ctr).Enabled = pHabilita; }
                    if (ctr.GetType().Name == "UpDownButtons")
                    {
                        ctr.Enabled = false;
                    }

                }
            }
        }
        private void HabilitaGroups(List<object> lObj)
        {
            if (((belIde)lObj[0]) == null)
            {
                tabPageIDE.Enabled = false;
            }
            if (((belEmit)lObj[1]) == null)
            {
                tabPageEmit.Enabled = false;
            }
            if (((belDest)lObj[2]) == null)
            {
                tabPageDest.Enabled = false;
            }
            if (((belEndEnt)lObj[3]) == null)
            {
                tabPageEndEnt.Enabled = false;
            }
            if (((belTotal)lObj[5]) != null)
            {
                if (((belTotal)lObj[5]).belIssqntot == null)
                {
                    flpISSNQ.Enabled = false;
                }
                if (((belTotal)lObj[5]).belRetTrib == null)
                {
                    flpRetTrib.Enabled = false;
                }
            }
            else
            {
                tabPageTotais.Enabled = false;
            }
            if (((belTransp)lObj[6]).belTransportadora == null)
            {
                flpTransportador.Enabled = false;
            }
            if (((belTransp)lObj[6]).belVeicTransp == null)
            {
                flpVeicTransp.Enabled = false;
            }
            if (((belTransp)lObj[6]).belReboque == null)
            {
                flpReboque.Enabled = false;
            }
            if (((belTransp)lObj[6]).belRetTransp == null)
            {
                flpRetICMS.Enabled = false;
            }
            if (((belTransp)lObj[6]).belVol == null)
            {
                flpVolumes.Enabled = false;
            }
            if (((belCobr)lObj[7]).belFat == null)
            {
                tabPageCobr.Enabled = false;
            }
            if (((belInfAdic)lObj[8]) == null)
            {
                tabPageInfAdic.Enabled = false;
            }



        }
        private void HabilitaGrids(bool pHabil)
        {
            dgvDet.ReadOnly = !pHabil;
            dgvDup.ReadOnly = !pHabil;
        }
        private void populaNF(List<object> lNFe)
        {
            try
            {
                #region IDE
                belIde objide = lNFe[0] as belIde;

                cbxUF.SelectedValue = objide.Cuf;
                txtSeq.Text = objide.Cnf;
                txtNatOp.Text = objide.Natop;
                cbxIndPag.SelectedIndex = Convert.ToInt32(objide.Indpag);
                txtMod.Text = objide.Mod;
                txtSerie.Text = objide.Serie;
                txtNNF.Text = objide.Nnf;
                mtbDEmi.Text = objide.Demi.ToString("dd/MM/yyyy");
                mtbDSaiEnt.Text = objide.Dsaient.ToString("dd/MM/yyyy");
                cbxTpNF.SelectedIndex = Convert.ToInt32(objide.Tpnf);
                txtCMunFG.Text = objide.Cmunfg;
                cbxtpImp.SelectedIndex = Convert.ToInt32(objide.Tpimp) - 1;
                cbcxTpEmis.SelectedIndex = Convert.ToInt32(objide.Tpemis) - 1;
                txtCDV.Text = objide.Cdv;
                cbxTpAmb.SelectedIndex = Convert.ToInt32(objide.Tpamb) - 1;
                cbxFinNFe.SelectedIndex = Convert.ToInt32(objide.Finnfe) - 1;
                txtProcEmi.Text = objide.Procemi;
                txtVerProc.Text = objide.Verproc;

                #endregion

                #region Emitente
                belEmit objemit = lNFe[1] as belEmit;

                if (objemit.Cnpj != null)
                {
                    cbxPessoaEmit.SelectedIndex = 1;
                    mtbCpfCnpjEmit.Mask = "00.000.000/0000-00";
                    mtbCpfCnpjEmit.Text = objemit.Cnpj;
                }
                else
                {
                    cbxPessoaEmit.SelectedIndex = 0;
                    mtbCpfCnpjEmit.Mask = "000.000.000-00";
                    mtbCpfCnpjEmit.Text = objemit.Cpf; ;
                }


                switch (objemit.CRT) // NFe_2.0
                {
                    case 1: cmbCRT.SelectedIndex = 0;
                        break;

                    case 2: cmbCRT.SelectedIndex = 1;
                        break;

                    case 3: cmbCRT.SelectedIndex = 2;
                        break;
                }
                txtXNomeEmit.Text = objemit.Xnome;
                txtXFantEmit.Text = objemit.Xfant;
                txtIEEmit.Text = objemit.Ie;
                txtIESTEmit.Text = objemit.Iest;
                txtIM.Text = objemit.Im;
                txtCNAE.Text = objemit.Cnae;

                //Endereço

                txtEnderEmitXlgr.Text = objemit.Xlgr;
                txtEnderEmitNum.Text = objemit.Nro;
                txtEnderEmitCompl.Text = objemit.Xcpl;
                txtEnderEmitXbairro.Text = objemit.Xbairro;
                txtEnderEmitCmun.Text = objemit.Cmun;
                txtEnderEmitXmun.Text = objemit.Xmun;
                txtEnderEmitUF.Text = objemit.Uf;
                txtEnderEmitCpais.Text = objemit.Cpais;
                txtEnderEmitXpais.Text = objemit.Xpais;
                mtbEnderEmitCep.Text = objemit.Cep;
                mtbEnderEmitFone.Text = objemit.Fone;

                //Fim - Endereço

                #endregion

                #region Destinatario

                belDest objdest = lNFe[2] as belDest;

                if (objdest.Cnpj == "EXTERIOR")
                {
                    cbxPessoaDest.SelectedIndex = 2;
                    mtbCpfCnpjDest.Mask = "";
                    mtbCpfCnpjDest.Text = objdest.Cnpj;
                }
                else if (objdest.Cnpj != null)
                {
                    mtbCpfCnpjDest.Mask = "00.000.000/0000-00";
                    mtbCpfCnpjDest.Text = objdest.Cnpj;
                }
                else
                {
                    mtbCpfCnpjDest.Mask = "000.000.000-00";
                    mtbCpfCnpjDest.Text = objdest.Cpf;
                }

                if (objdest.tpEmit == 1)
                {
                    cbxPessoaDest.SelectedIndex = 1;
                }
                else
                {
                    cbxPessoaDest.SelectedIndex = 0;
                }

                txtXnomeDest.Text = objdest.Xnome;
                txtIEDest.Text = objdest.Ie;
                txtISUFDest.Text = objdest.Isuf;

                //Endereço

                txtEnderDestXlgr.Text = objdest.Xlgr;
                txtEnderDestCpl.Text = objdest.Xcpl; //OS_26347
                txtEnderDestNro.Text = objdest.Nro;
                txtEnderDestXbairro.Text = objdest.Xbairro;
                txtEnderDestCmun.Text = objdest.Cmun;
                txtEnderDestXmun.Text = objdest.Xmun;
                txtEnderDestUF.Text = objdest.Uf;
                txtEnderDestCpais.Text = objdest.Cpais;
                txtEnderDestXpais.Text = objdest.Xpais;
                mtbEnderDestCEP.Text = objdest.Cep;
                mtbEnderDestFone.Text = objdest.Fone;
                txtEmaildest.Text = objdest.email;

                //Fim - Endereço

                #endregion

                #region Endereço de Entrega

                belEndEnt objendent = lNFe[3] as belEndEnt;

                mtbEndEntCNPJ.Text = objendent.Cnpj;
                txtEndEntXlgr.Text = objendent.Xlgr;
                txtEndEntNro.Text = objendent.Nro;
                txtEndEntCmun.Text = objendent.Cmun;
                txtEndEntXmun.Text = objendent.Xmun;
                txtEndEntUF.Text = objendent.Uf;
                txtEndEntXbairro.Text = objendent.Xbairro; // OS_25185
                txtEndEntCpl.Text = objendent.Xcpl;

                #endregion

                #region Detalhes

                List<belDet> lObjDet = lNFe[4] as List<belDet>;
                // List<prodDgv> lProdDgv = new List<prodDgv>();

                populaDet(lObjDet);

                #endregion

                #region Totais

                belTotal objtotal = lNFe[5] as belTotal;

                //Totais
                nudVBC.Value = objtotal.belIcmstot.Vbc;
                nudVICMS.Value = objtotal.belIcmstot.Vicms;
                nudVBCICMSST.Value = objtotal.belIcmstot.Vbcst;
                nudVST.Value = objtotal.belIcmstot.Vst;
                nudVProd.Value = objtotal.belIcmstot.Vprod;
                //Danner - o.s. 24154 - 18/02/2010
                nudVFrete.Value = objtotal.belIcmstot.Vfrete;
                //Fim - Danner - o.s. 24154 - 18/02/2010
                nudVSEG.Value = objtotal.belIcmstot.Vseg;
                nudVDesc.Value = objtotal.belIcmstot.Vdesc;
                nudVII.Value = objtotal.belIcmstot.Vii;
                nudVIPI.Value = objtotal.belIcmstot.Vipi;
                nudVPIS.Value = objtotal.belIcmstot.Vpis;
                nudVCOFINS.Value = objtotal.belIcmstot.Vcofins;
                nudVOutro.Value = objtotal.belIcmstot.Voutro;
                nudVNF.Value = objtotal.belIcmstot.Vnf;

                //Fim - Totais

                //ISSQNtot
                if (objtotal.belIssqntot != null)
                {
                    nudVServ.Value = objtotal.belIssqntot.Vserv;
                    nudVBCISS.Value = objtotal.belIssqntot.Vbc;
                    nudVISS.Value = objtotal.belIssqntot.Viss;
                    nudVPISISS.Value = objtotal.belIssqntot.Vpis;
                    nudVCOFINSISS.Value = objtotal.belIssqntot.Vcofins;
                }

                //Fin - ISSQNtot;

                //retTrib
                if (objtotal.belRetTrib != null)
                {
                    nudVPISRet.Value = objtotal.belRetTrib.Vretpis;
                    nudVCOFINSRet.Value = objtotal.belRetTrib.Vretcofins;
                    nudVCSLLRet.Value = objtotal.belRetTrib.Vretcsll;
                    nudVBCIRRFRet.Value = objtotal.belRetTrib.Vbcirrf;
                    nudVIRRFRet.Value = objtotal.belRetTrib.Virrf;
                    nudVBCRetPrev.Value = objtotal.belRetTrib.Vbcretprev;
                    nudVRetPrev.Value = objtotal.belRetTrib.Vretprev;
                }

                //Fim - retTrib



                #endregion

                #region Transporte

                belTransp objtransp = lNFe[6] as belTransp;

                switch (objtransp.Modfrete)
                {
                    case "0": cbxModFrete.SelectedIndex = 0;
                        break;
                    case "1": cbxModFrete.SelectedIndex = 1;
                        break;
                    case "2": cbxModFrete.SelectedIndex = 2;
                        break;
                    case "9": cbxModFrete.SelectedIndex = 3;
                        break;
                }


                //cbxModFrete.SelectedIndex = Convert.ToInt32(objtransp.Modfrete);

                //Transportadora

                if (objtransp.belTransportadora.Cnpj != null)
                {
                    cbxPessoaTranp.SelectedIndex = 1;
                    mtbCPJCNPJTransp.Mask = "00.000.000/0000-00";
                    mtbCPJCNPJTransp.Text = objtransp.belTransportadora.Cnpj;
                }
                else
                {
                    cbxPessoaTranp.SelectedIndex = 0;
                    mtbCPJCNPJTransp.Mask = "000.000.000-00";
                    mtbCPJCNPJTransp.Text = objtransp.belTransportadora.Cpf;
                }

                txtXnomeTransp.Text = objtransp.belTransportadora.Xnome;
                txtIETransp.Text = objtransp.belTransportadora.Ie;
                txtEnderTransp.Text = objtransp.belTransportadora.Xender;
                txtUFTransp.Text = objtransp.belTransportadora.Uf;
                txtXmunTransp.Text = objtransp.belTransportadora.Xmun;

                //Fim - Transportadora

                //VeicTransp
                if (objtransp.belVeicTransp != null)
                {
                    mtbPlacaVeicTransp.Text = objtransp.belVeicTransp.Placa;
                    txtUFVeicTransp.Text = objtransp.belVeicTransp.Uf;
                    txtRNTCVeicTransp.Text = Convert.ToString(objtransp.belVeicTransp.Rntc);//Danner - o.s. sem - 05/03/2010 
                }
                //Fim -  VeicTransp

                //Reboque
                if (objtransp.belReboque != null)
                {
                    mtbPlacaReboque.Text = objtransp.belReboque.Placa;
                    txtUFReboque.Text = objtransp.belReboque.Uf;
                    txtRNTCReboque.Text = objtransp.belReboque.Rntc;
                }
                //Fim - Reboque 

                //RetTransp
                if (objtransp.belRetTransp != null)
                {
                    nudVBCICMSTransp.Value = objtransp.belRetTransp.Vbvret;
                    nudVServTransp.Value = objtransp.belRetTransp.Vserv;
                    nudPICMSTRetTransp.Value = objtransp.belRetTransp.Picmsret;
                    nudVICMSRet.Value = objtransp.belRetTransp.Vicmsret;
                    txtCmunFGTransp.Text = objtransp.belRetTransp.Cmunfg;
                    txtCFOPTransp.Text = objtransp.belRetTransp.Cfop;
                }
                //Fim - RetTransp

                //Vol
                if (objtransp.belVol != null)
                {
                    nudQvol.Value = objtransp.belVol.Qvol;
                    txtEsp.Text = objtransp.belVol.Esp;
                    txtMarca.Text = objtransp.belVol.Marca;
                    //Danner - o.s. 24385 - 26/04/2010
                    txtNVol.Text = (Convert.ToString(objtransp.belVol.Nvol));
                    //Danner - o.s. 24385 - 26/04/2010
                    nudPesoL.Value = objtransp.belVol.PesoL;
                    nudPesoB.Value = objtransp.belVol.PesoB;
                }
                //Fim - Vol

                #endregion

                #region Cobrança

                belCobr objcobr = lNFe[7] as belCobr;

                txtNFat.Text = objcobr.belFat.Nfat;
                nudVOrigFat.Value = objcobr.belFat.Vorig;
                nudVDescFat.Value = objcobr.belFat.Vdesc;
                nudVLiqFat.Value = objcobr.belFat.Vliq;

                populaDup(objcobr.belFat.belDup);

                #endregion

                #region Inf Adicionais

                belInfAdic objinfadic = lNFe[8] as belInfAdic;

                string sInfAdd = "";

                if (objinfadic.Infcpl != null)
                {
                    for (int i = 0; i < objinfadic.Infcpl.Length; i++)
                    {
                        sInfAdd = sInfAdd + objinfadic.Infcpl[i];
                        if (objinfadic.Infcpl[i].ToString().Equals(";"))
                        {
                            sInfAdd = sInfAdd + Environment.NewLine;
                        }
                    }
                }
                txtInfAdic.Text = sInfAdd;
                lblCaracter.Text = "Total Caracter: " + sInfAdd.Length.ToString();

                #endregion



            }
            catch (Exception ex)
            {

                KryptonMessageBox.Show(ex.Message);
            }
        }
        private void EmEdicao(bool bEmEdit)
        {
            btnAtualizar.Enabled = !bEmEdit;
            btnSalvar.Enabled = bEmEdit;
            btnCancelar.Enabled = bEmEdit;
            btnEnviar.Enabled = !bEmEdit;
            btnCancelNFe.Enabled = !bEmEdit;
        }
        private void ControlaSeta()
        {

            lblCtrNota.Text = notAtual + " de " + notTotal;
            if (notTotal != 1)
            {
                if (notAtual == 1)
                {
                    btnPrimeiro.Enabled = false;
                    btnAnterior.Enabled = false;
                    btnProximo.Enabled = true;
                    btnUltimo.Enabled = true;
                }
                else if (notAtual > 1 && notAtual < notTotal)
                {
                    btnPrimeiro.Enabled = true;
                    btnAnterior.Enabled = true;
                    btnProximo.Enabled = true;
                    btnUltimo.Enabled = true;
                }
                else
                {
                    btnPrimeiro.Enabled = true;
                    btnAnterior.Enabled = true;
                    btnProximo.Enabled = false;
                    btnUltimo.Enabled = false;
                }

            }
            else
            {
                btnPrimeiro.Enabled = false;
                btnAnterior.Enabled = false;
                btnProximo.Enabled = false;
                btnUltimo.Enabled = false;
            }
        }
        private void desabilitaBotoes(bool h)
        {
            btnPrimeiro.Enabled = h;
            btnAnterior.Enabled = h;
            btnProximo.Enabled = h;
            btnUltimo.Enabled = h;
        }
        private void Gravar()
        {
            List<object> lObj = new List<object>();
            try
            {
                #region IDE
                objide = new belIde();

                objide.Cuf = Convert.ToString(cbxUF.SelectedValue).Trim();
                objide.Cnf = txtSeq.Text;
                objide.Natop = txtNatOp.Text;
                objide.Indpag = Convert.ToString(cbxIndPag.SelectedIndex).Trim();
                objide.Mod = txtMod.Text.Trim();
                objide.Serie = txtSerie.Text.Trim();
                objide.Nnf = txtNNF.Text.Trim();
                objide.Demi = Convert.ToDateTime(mtbDEmi.Text);
                objide.Dsaient = Convert.ToDateTime(mtbDSaiEnt.Text);
                objide.Tpnf = Convert.ToString(cbxTpNF.SelectedIndex);
                objide.Cmunfg = txtCMunFG.Text.Trim();
                objide.Tpimp = Convert.ToString((int)cbxtpImp.SelectedIndex + 1);
                objide.Tpemis = Convert.ToString((int)cbcxTpEmis.SelectedIndex + 1);
                objide.Cdv = txtCDV.Text.Trim();
                objide.Tpamb = Convert.ToString((int)cbxTpAmb.SelectedIndex + 1);
                objide.Finnfe = Convert.ToString((int)cbxFinNFe.SelectedIndex + 1);
                objide.Procemi = txtProcEmi.Text.Trim();
                objide.Verproc = txtVerProc.Text.Trim();
                if (belNFrefBindingSource.Count > 0) // 25360
                {
                    List<belNFref> lObjNFref = new List<belNFref>();

                    for (int i = 0; i < belNFrefBindingSource.Count; i++)
                    {
                        lObjNFref.Add((belNFref)belNFrefBindingSource[i]);
                        if (lObjNFref[i].cUF != null)
                        {
                            if (!HLP.Util.Util.IsNumeric(lObjNFref[i].cUF))
                            {
                                belUF objuf = new belUF();
                                lObjNFref[i].cUF = objuf.RetornaCUF((lObjNFref[i].cUF));
                                lObjNFref[i].CNPJ = (lObjNFref[i].CNPJ).Replace(",", "").Replace("/", "").Replace("-", "");
                                lObjNFref[i].nNF = (Convert.ToInt32(lObjNFref[i].nNF)).ToString();
                            }
                        }

                    }
                    objide.belNFref = lObjNFref;
                }
                objide.HSaiEnt = dtpHSaiEnt.Value; //NFe_2.0

                lObj.Add(objide);
                #endregion

                #region Emitente
                belEmit objemit = new belEmit();

                if (cbxPessoaEmit.SelectedIndex == 0)
                {
                    objemit.Cpf = mtbCpfCnpjEmit.Text;
                }
                else
                {
                    objemit.Cnpj = mtbCpfCnpjEmit.Text;
                }
                objemit.Xnome = txtXNomeEmit.Text.Trim();
                objemit.Xfant = txtXFantEmit.Text.Trim();
                if (txtIEEmit.Text != "")
                {
                    objemit.Ie = txtIEEmit.Text.Trim();
                }
                if (txtIESTEmit.Text != "")
                {
                    objemit.Iest = txtIESTEmit.Text.Trim();
                }
                if (txtIM.Text != "")
                {
                    objemit.Im = txtIM.Text.Trim();
                }
                if (txtCNAE.Text != "")
                {
                    objemit.Cnae = txtCNAE.Text;
                }

                //Endereço

                objemit.Xlgr = txtEnderEmitXlgr.Text.Trim();
                objemit.Nro = txtEnderEmitNum.Text.Trim();
                if (txtEnderEmitCompl.Text != "")
                {
                    objemit.Xcpl = txtEnderEmitCompl.Text.Trim();
                }
                objemit.Xbairro = txtEnderEmitXbairro.Text.Trim();
                objemit.Cmun = txtEnderEmitCmun.Text.Trim();
                objemit.Xmun = txtEnderEmitXmun.Text.Trim();
                objemit.Uf = txtEnderEmitUF.Text.Trim();
                objemit.Cpais = txtEnderEmitCpais.Text.Trim();
                objemit.Xpais = txtEnderEmitXpais.Text.Trim();
                objemit.Cep = mtbEnderEmitCep.Text.Trim();
                objemit.Fone = mtbEnderEmitFone.Text.Trim();

                switch (cmbCRT.SelectedIndex) // NFe_2.0
                {
                    case 0: objemit.CRT = 1;
                        break;

                    case 1: objemit.CRT = 2;
                        break;

                    case 2: objemit.CRT = 3;
                        break;
                }

                //Fim - Endereço
                lObj.Add(objemit);
                #endregion

                #region Destinatário

                belDest objdest = new belDest();

                if (mtbCpfCnpjDest.Mask.Equals("00.000.000/0000-00") || mtbCpfCnpjDest.Text.ToString().ToUpper().Equals("EXTERIOR"))
                {
                    objdest.Cnpj = mtbCpfCnpjDest.Text.Trim();
                }
                else
                {
                    objdest.Cpf = mtbCpfCnpjDest.Text.Trim();
                }

                objdest.Xnome = (belStatic.tpAmb == 2 ? "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL"
                    : txtXnomeDest.Text.Trim());

                objdest.Ie = txtIEDest.Text.Trim();// (belStatic.tpAmb == 2 ? "" : txtIEDest.Text.Trim());
                if (txtISUFDest.Text != "")
                {
                    objdest.Isuf = txtISUFDest.Text.Trim();
                }

                //Endereço

                objdest.Xlgr = txtEnderDestXlgr.Text.Trim();
                objdest.Nro = txtEnderDestNro.Text.Trim();
                objdest.Xcpl = txtEnderDestCpl.Text.Trim(); //OS_26347
                objdest.Xbairro = txtEnderDestXbairro.Text.Trim();
                objdest.Cmun = txtEnderDestCmun.Text.Trim();
                objdest.Xmun = txtEnderDestXmun.Text.Trim();
                objdest.Uf = txtEnderDestUF.Text.Trim();
                objdest.Cpais = txtEnderDestCpais.Text.Trim();
                objdest.Xpais = txtEnderDestXpais.Text.Trim();
                objdest.Cep = mtbEnderDestCEP.Text.Trim();
                if (mtbEnderDestFone.Text.Trim() != "")
                {
                    objdest.Fone = mtbEnderDestFone.Text.Trim();
                }
                objdest.email = txtEmaildest.Text.Trim(); // NFe_2.0

                //Fim - Endereço
                lObj.Add(objdest);
                #endregion

                #region Endereço de Entrega
                belEndEnt objendent = new belEndEnt();
                if (mtbEndEntCNPJ.Text != "")
                {
                    objendent.Cnpj = mtbEndEntCNPJ.Text.Trim();
                    objendent.Xlgr = txtEndEntXlgr.Text.Trim();
                    objendent.Nro = txtEndEntNro.Text.Trim();
                    objendent.Cmun = txtEndEntCmun.Text.Trim();
                    objendent.Xmun = txtEndEntXmun.Text.Trim();
                    objendent.Uf = txtEndEntUF.Text.Trim();
                    objendent.Xbairro = txtEndEntXbairro.Text.Trim(); //0S_25185
                    objendent.Xcpl = txtEndEntCpl.Text.Trim();//0S_25185
                }
                lObj.Add(objendent);

                #endregion

                #region Detatalhes

                List<belDet> lObjDet = new List<belDet>();

                for (int i = 0; i < dgvDet.RowCount; i++)
                {
                    belDet objDet = new belDet();

                    objDet.Nitem = i + 1;

                    #region Prod
                    belProd objprod = new belProd();

                    objprod.Cean = Convert.ToString(dgvDet["Cean", i].Value);
                    objprod.Ceantrib = Convert.ToString(dgvDet["Ceantrib", i].Value);
                    objprod.Cfop = Convert.ToString(dgvDet["Cfop", i].Value);
                    objprod.Cprod = Convert.ToString(dgvDet["Cprod", i].Value);
                    if (dgvDet["Extipi", i].Value != null)
                    {
                        objprod.Extipi = Convert.ToString(dgvDet["Extipi", i].Value);
                    }
                    if (dgvDet["Genero", i].Value != null)
                    {
                        objprod.Genero = Convert.ToString(dgvDet["Genero", i].Value);
                    }
                    if (Convert.ToString(dgvDet["NCM", i].Value) != "")
                    {
                        objprod.Ncm = Convert.ToString(dgvDet["NCM", i].Value);
                    }
                    objprod.Qcom = Convert.ToDecimal(dgvDet["Qcom", i].Value);
                    objprod.Qtrib = Convert.ToDecimal(dgvDet["Qtrib", i].Value);
                    objprod.Ucom = Convert.ToString(dgvDet["Ucom", i].Value);
                    objprod.Utrib = Convert.ToString(dgvDet["Utrib", i].Value);
                    objprod.Vdesc = Convert.ToDecimal(dgvDet["Vdesc", i].Value);
                    objprod.Vfrete = Convert.ToDecimal(dgvDet["Vfrete", i].Value);
                    objprod.Vprod = Convert.ToDecimal(dgvDet["Vprod", i].Value);
                    objprod.Vseg = Convert.ToDecimal(dgvDet["Vseg", i].Value);
                    objprod.Vuncom = Convert.ToDecimal(dgvDet["Vuncom", i].Value);
                    objprod.Vuntrib = Convert.ToDecimal(dgvDet["Vuntrib", i].Value);
                    objprod.Xprod = Convert.ToString(dgvDet["Xprod", i].Value);
                    objprod.VOutro = Convert.ToDecimal(dgvDet["vOutro", i].Value);// NFe_2.0
                    objprod.IndTot = Convert.ToInt16(dgvDet["indTot", i].Value); // NFe_2.0
                    objprod.XPed = dgvDet["xPed", i].Value.ToString();
                    objprod.NItemPed = dgvDet["nItemPed", i].Value.ToString();

                    objDet.belProd = objprod;

                    objDet.belProd.belDI = ((List<belDet>)lObjTotNotas[notAtual - 1][4])[i].belProd.belDI;
                    #endregion

                    #region Imposto

                    belImposto objimposto = new belImposto();


                    #region ICMS

                    belIcms objicms = new belIcms();

                    if (!HLP.Util.Util.VerificaNovaST(Convert.ToString(dgvDet["CstIcms", i].Value)))
                    {
                        #region cst_antigas
                        switch (Convert.ToString(dgvDet["CstIcms", i].Value))
                        {

                            case "00":
                                {
                                    belIcms00 obj00 = new belIcms00();

                                    obj00.Cst = Convert.ToString(dgvDet["CstIcms", i].Value);
                                    obj00.Modbc = Convert.ToString(dgvDet["ModbcIcms", i].Value);
                                    obj00.Orig = Convert.ToString(dgvDet["OrigIcms", i].Value);
                                    obj00.Picms = Convert.ToDecimal(dgvDet["PicmsIcms", i].Value);
                                    obj00.Vbc = Convert.ToDecimal(dgvDet["VbcIcms", i].Value);
                                    obj00.Vicms = Convert.ToDecimal(dgvDet["VicmsIcms", i].Value);
                                    objicms.belIcms00 = obj00;
                                    break;
                                }
                            case "10":
                                {
                                    belIcms10 obj10 = new belIcms10();

                                    obj10.Cst = Convert.ToString(dgvDet["CstIcms", i].Value);
                                    obj10.Modbc = Convert.ToString(dgvDet["ModbcIcms", i].Value);
                                    obj10.Modbcst = Convert.ToDecimal(dgvDet["ModbcstIcms", i].Value);
                                    obj10.Orig = Convert.ToString(dgvDet["OrigIcms", i].Value);
                                    obj10.Picms = Convert.ToDecimal(dgvDet["PicmsIcms", i].Value);
                                    obj10.Picmsst = Convert.ToDecimal(dgvDet["PicmsstIcms", i].Value);
                                    obj10.Pmvast = Convert.ToDecimal(dgvDet["PmvastIcms", i].Value);
                                    obj10.Predbcst = Convert.ToDecimal(dgvDet["PredbcstIcms", i].Value);
                                    obj10.Vbc = Convert.ToDecimal(dgvDet["VbcIcms", i].Value);
                                    obj10.Vbcst = Convert.ToDecimal(dgvDet["VbcstIcms", i].Value);
                                    obj10.Vicms = Convert.ToDecimal(dgvDet["VicmsIcms", i].Value);
                                    obj10.Vicmsst = Convert.ToDecimal(dgvDet["VicmsstIcms", i].Value);
                                    objicms.belIcms10 = obj10;
                                    break;
                                }
                            case "20":
                                {
                                    belIcms20 obj20 = new belIcms20();
                                    obj20.Cst = Convert.ToString(dgvDet["CstIcms", i].Value);
                                    obj20.Modbc = Convert.ToString(dgvDet["ModbcIcms", i].Value);
                                    obj20.Orig = Convert.ToString(dgvDet["OrigIcms", i].Value);
                                    obj20.Picms = Convert.ToDecimal(dgvDet["PicmsIcms", i].Value);
                                    obj20.Predbc = Convert.ToDecimal(dgvDet["PredbcIcms", i].Value);
                                    obj20.Vbc = Convert.ToDecimal(dgvDet["VbcIcms", i].Value);
                                    obj20.Vicms = Convert.ToDecimal(dgvDet["VicmsIcms", i].Value);
                                    objicms.belIcms20 = obj20;
                                    break;
                                }
                            case "30":
                                {
                                    belIcms30 obj30 = new belIcms30();

                                    obj30.Cst = Convert.ToString(dgvDet["CstIcms", i].Value);
                                    obj30.Modbcst = Convert.ToDecimal(dgvDet["ModbcstIcms", i].Value);
                                    obj30.Orig = Convert.ToString(dgvDet["OrigIcms", i].Value);
                                    obj30.Picmsst = Convert.ToDecimal(dgvDet["PicmsstIcms", i].Value);
                                    obj30.Pmvast = Convert.ToDecimal(dgvDet["PmvastIcms", i].Value);
                                    obj30.Predbcst = Convert.ToDecimal(dgvDet["PredbcstIcms", i].Value);
                                    obj30.Vbcst = Convert.ToDecimal(dgvDet["VbcstIcms", i].Value);
                                    obj30.Vicmsst = Convert.ToDecimal(dgvDet["VicmsIcms", i].Value);
                                    objicms.belIcms30 = obj30;

                                    break;
                                }
                            case "40":
                                {
                                    belIcms40 obj40 = new belIcms40();

                                    obj40.Cst = Convert.ToString(dgvDet["CstIcms", i].Value);
                                    obj40.Orig = Convert.ToString(dgvDet["OrigIcms", i].Value);

                                    obj40.Vicms = Convert.ToDecimal(dgvDet["VicmsIcms", i].Value); //NFe_2.0
                                    obj40.motDesICMS = Convert.ToInt16(dgvDet["motDesICMS", i].Value);//NFe_2.0

                                    objicms.belIcms40 = obj40;
                                    break;
                                }
                            case "41":
                                {
                                    belIcms40 obj40 = new belIcms40();

                                    obj40.Cst = Convert.ToString(dgvDet["CstIcms", i].Value);
                                    obj40.Orig = Convert.ToString(dgvDet["OrigIcms", i].Value);
                                    obj40.Vicms = Convert.ToDecimal(dgvDet["VicmsIcms", i].Value); //NFe_2.0
                                    obj40.motDesICMS = Convert.ToInt16(dgvDet["motDesICMS", i].Value);//NFe_2.0
                                    objicms.belIcms40 = obj40;
                                    break;
                                }
                            case "50":
                                {
                                    belIcms40 obj40 = new belIcms40();

                                    obj40.Cst = Convert.ToString(dgvDet["CstIcms", i].Value);
                                    obj40.Orig = Convert.ToString(dgvDet["OrigIcms", i].Value);
                                    obj40.Vicms = Convert.ToDecimal(dgvDet["VicmsIcms", i].Value); //NFe_2.0
                                    obj40.motDesICMS = Convert.ToInt16(dgvDet["motDesICMS", i].Value);//NFe_2.0
                                    objicms.belIcms40 = obj40;
                                    break;

                                }
                            case "51":
                                {
                                    belIcms51 obj51 = new belIcms51();

                                    obj51.Cst = Convert.ToString(dgvDet["CstIcms", i].Value);
                                    obj51.Modbc = Convert.ToString(dgvDet["ModbcIcms", i].Value);
                                    obj51.Orig = Convert.ToString(dgvDet["OrigIcms", i].Value);
                                    obj51.Picms = Convert.ToDecimal(dgvDet["PicmsIcms", i].Value);
                                    obj51.Predbc = Convert.ToDecimal(dgvDet["PredbcIcms", i].Value);
                                    obj51.Vbc = Convert.ToDecimal(dgvDet["VbcIcms", i].Value);
                                    obj51.Vicms = Convert.ToDecimal(dgvDet["VicmsIcms", i].Value);
                                    objicms.belIcms51 = obj51;
                                    break;
                                }
                            //Fim - Danner - o.s. 24189 - 26/02/2010
                            case "60":
                                {
                                    belIcms60 obj60 = new belIcms60();
                                    obj60.Cst = Convert.ToString(dgvDet["CstIcms", i].Value);
                                    obj60.Orig = Convert.ToString(dgvDet["OrigIcms", i].Value);
                                    obj60.Vbcst = Convert.ToDecimal(dgvDet["VbcstIcms", i].Value);
                                    obj60.Vicmsst = Convert.ToDecimal(dgvDet["VicmsstIcms", i].Value);
                                    objicms.belIcms60 = obj60;
                                    break;
                                }
                            case "70":
                                {
                                    belIcms70 obj70 = new belIcms70();
                                    obj70.Cst = Convert.ToString(dgvDet["CstIcms", i].Value);
                                    obj70.Modbc = Convert.ToString(dgvDet["ModbcIcms", i].Value);
                                    obj70.Modbcst = Convert.ToDecimal(dgvDet["ModbcstIcms", i].Value);
                                    obj70.Orig = Convert.ToString(dgvDet["OrigIcms", i].Value);
                                    obj70.Picms = Convert.ToDecimal(dgvDet["PicmsIcms", i].Value);
                                    obj70.Picmsst = Convert.ToDecimal(dgvDet["PicmsstIcms", i].Value);
                                    obj70.Pmvast = Convert.ToDecimal(dgvDet["PmvastIcms", i].Value);
                                    obj70.Predbc = Convert.ToDecimal(dgvDet["PredbcIcms", i].Value);
                                    obj70.Predbcst = Convert.ToDecimal(dgvDet["PredbcstIcms", i].Value);
                                    obj70.Vbc = Convert.ToDecimal(dgvDet["VbcIcms", i].Value);
                                    obj70.Vbcst = Convert.ToDecimal(dgvDet["VbcstIcms", i].Value);
                                    obj70.Vicms = Convert.ToDecimal(dgvDet["VicmsIcms", i].Value);
                                    obj70.Vicmsst = Convert.ToDecimal(dgvDet["VicmsstIcms", i].Value);
                                    objicms.belIcms70 = obj70;
                                    break;
                                }
                            case "90":
                                {
                                    belIcms90 obj90 = new belIcms90();

                                    obj90.Cst = Convert.ToString(dgvDet["CstIcms", i].Value);
                                    obj90.Modbc = Convert.ToString(dgvDet["ModbcIcms", i].Value);
                                    obj90.Modbcst = Convert.ToDecimal(dgvDet["ModbcstIcms", i].Value);
                                    obj90.Orig = Convert.ToString(dgvDet["OrigIcms", i].Value);
                                    obj90.Picms = Convert.ToDecimal(dgvDet["PicmsIcms", i].Value);
                                    obj90.Picmsst = Convert.ToDecimal(dgvDet["PicmsstIcms", i].Value);
                                    obj90.Pmvast = Convert.ToDecimal(dgvDet["PmvastIcms", i].Value);
                                    obj90.Predbc = Convert.ToDecimal(dgvDet["PredbcIcms", i].Value);
                                    obj90.Predbcst = Convert.ToDecimal(dgvDet["PredbcstIcms", i].Value);
                                    obj90.Vbc = Convert.ToDecimal(dgvDet["VbcIcms", i].Value);
                                    obj90.Vbcst = Convert.ToDecimal(dgvDet["VbcstIcms", i].Value);
                                    obj90.Vicms = Convert.ToDecimal(dgvDet["VicmsIcms", i].Value);
                                    obj90.Vicmsst = Convert.ToDecimal(dgvDet["VicmsstIcms", i].Value);

                                    objicms.belIcms90 = obj90;
                                    break;
                                }
                        }
                        #endregion
                    }
                    else
                    {
                        #region cst_novas
                        switch (HLP.Util.Util.RetornaSTnovaAserUsada(Convert.ToString(dgvDet["CstIcms", i].Value)))
                        {
                            case "101":
                                {
                                    belICMSSN101 obj101 = new belICMSSN101();

                                    obj101.CSOSN = Convert.ToString(dgvDet["CstIcms", i].Value);
                                    obj101.orig = Convert.ToString(dgvDet["OrigIcms", i].Value);
                                    obj101.pCredSN = Convert.ToDecimal(dgvDet["pCredSN", i].Value);
                                    obj101.vCredICMSSN = Convert.ToDecimal(dgvDet["vCredICMSSN", i].Value);
                                    objicms.belICMSSN101 = obj101;
                                    break;
                                }
                            case "102":
                                {
                                    belICMSSN102 obj102 = new belICMSSN102();
                                    obj102.CSOSN = Convert.ToString(dgvDet["CstIcms", i].Value);
                                    obj102.orig = Convert.ToString(dgvDet["OrigIcms", i].Value);
                                    objicms.belICMSSN102 = obj102;
                                    break;
                                }
                            case "201":
                                {
                                    belICMSSN201 obj201 = new belICMSSN201();

                                    obj201.CSOSN = Convert.ToString(dgvDet["CstIcms", i].Value);
                                    obj201.orig = Convert.ToString(dgvDet["OrigIcms", i].Value);
                                    obj201.modBCST = Convert.ToInt32(dgvDet["ModbcstIcms", i].Value);
                                    obj201.pMVAST = Convert.ToDecimal(dgvDet["PmvastIcms", i].Value);
                                    obj201.pRedBCST = Convert.ToDecimal(dgvDet["PredbcstIcms", i].Value);
                                    obj201.vBCST = Convert.ToDecimal(dgvDet["VbcstIcms", i].Value);
                                    obj201.pICMSST = Convert.ToDecimal(dgvDet["PicmsstIcms", i].Value);
                                    obj201.vICMSST = Convert.ToDecimal(dgvDet["VicmsstIcms", i].Value);

                                    if (Convert.ToString(dgvDet["CstIcms", i].Value).Equals("101"))
                                    {
                                        obj201.pCredSN = Convert.ToDecimal(dgvDet["pCredSN", i].Value);
                                        obj201.vCredICMSSN = Convert.ToDecimal(dgvDet["vCredICMSSN", i].Value);
                                    }
                                    objicms.belICMSSN201 = obj201;
                                    break;
                                }
                            case "500":
                                {
                                    belICMSSN500 obj500 = new belICMSSN500();
                                    obj500.CSOSN = Convert.ToString(dgvDet["CstIcms", i].Value);
                                    obj500.orig = Convert.ToString(dgvDet["OrigIcms", i].Value);
                                    obj500.vBCSTRet = Convert.ToDecimal(dgvDet["VbcstIcms", i].Value);
                                    obj500.vICMSSTRet = Convert.ToDecimal(dgvDet["VicmsstIcms", i].Value);
                                    objicms.belICMSSN500 = obj500;
                                    break;
                                }
                            case "900":
                                {
                                    belICMSSN900 obj900 = new belICMSSN900();
                                    obj900.CSOSN = Convert.ToString(dgvDet["CstIcms", i].Value);
                                    obj900.orig = Convert.ToString(dgvDet["OrigIcms", i].Value);
                                    //obj900.modBC = Convert.ToInt32(dgvDet["ModbcIcms", i].Value);
                                    //obj900.vBC = Convert.ToDecimal(dgvDet["VbcIcms", i].Value);
                                    //obj900.pRedBC = Convert.ToDecimal(dgvDet["VicmsstIcms", i].Value);
                                    //obj900.pICMS = Convert.ToDecimal(dgvDet["PicmsIcms", i].Value);
                                    //obj900.vICMS = Convert.ToDecimal(dgvDet["VicmsIcms", i].Value);
                                    //obj900.modBCST = Convert.ToInt32(dgvDet["ModbcstIcms", i].Value);
                                    //obj900.pMVAST = Convert.ToDecimal(dgvDet["PmvastIcms", i].Value);
                                    //obj900.pRedBCST = Convert.ToDecimal(dgvDet["PredbcstIcms", i].Value);
                                    //obj900.vBCST = Convert.ToDecimal(dgvDet["VbcstIcms", i].Value);
                                    //obj900.pICMSST = Convert.ToDecimal(dgvDet["PicmsstIcms", i].Value);
                                    //obj900.vICMSST = Convert.ToDecimal(dgvDet["VicmsstIcms", i].Value);
                                    //obj900.vBCSTRet = Convert.ToDecimal(dgvDet["VbcstIcms", i].Value);
                                    //obj900.vICMSSTRet = Convert.ToDecimal(dgvDet["VicmsstIcms", i].Value);
                                    //obj900.pCredSN = Convert.ToDecimal(dgvDet["pCredSN", i].Value);
                                    //obj900.vCredICMSSN = Convert.ToDecimal(dgvDet["vCredICMSSN", i].Value);
                                    objicms.belICMSSN900 = obj900;
                                    break;
                                }
                        }
                        #endregion
                    }


                    objimposto.belIcms = objicms;

                    #endregion

                    #region IPI

                    belIpi objipi = new belIpi();
                    objipi.Cenq = Convert.ToString(dgvDet["CenqIpi", i].Value);

                    string sCSTIPI = Convert.ToString(dgvDet["CstIpi", i].Value);


                    if (sCSTIPI == "00" || sCSTIPI == "49" || sCSTIPI == "50" || sCSTIPI == "99")
                    {
                        belIpitrib objipitrib = new belIpitrib();

                        objipitrib.Cst = sCSTIPI;
                        objipitrib.Pipi = Convert.ToDecimal(dgvDet["PipiTrib", i].Value);
                        if (Convert.ToDecimal(dgvDet["QunidIpiTrib", i].Value) != 0)
                        {
                            objipitrib.Qunid = Convert.ToString(dgvDet["QunidIpiTrib", i].Value);
                        }
                        objipitrib.Vbc = Convert.ToDecimal(dgvDet["VbcIpiTrib", i].Value);
                        objipitrib.Vipi = Convert.ToDecimal(dgvDet["VipiTrib", i].Value); //Claudinei - o.s. 24192 - 01/03/2010
                        objipitrib.Vunid = Convert.ToDecimal(dgvDet["VunidTrib", i].Value);
                        objipi.belIpitrib = objipitrib;
                    }
                    else
                    {
                        belIpint objipint = new belIpint();

                        objipint.Cst = sCSTIPI;
                        objipi.belIpint = objipint;
                    }
                    objimposto.belIpi = objipi;



                    #endregion

                    #region II



                    //if (Convert.ToDecimal(dgvDet["VbcIi", i].Value) != 0)
                    if (objdest.Uf.Equals("EX"))
                    {
                        belIi objii = new belIi();
                        objii.Vbc = Convert.ToDecimal(dgvDet["VbcIi", i].Value);
                        objii.Vdespadu = Convert.ToDecimal(dgvDet["VdespaduIi", i].Value);
                        objii.Vii = Convert.ToDecimal(dgvDet["Vii", i].Value);
                        objii.Viof = Convert.ToDecimal(dgvDet["ViofIi", i].Value);
                        objimposto.belIi = objii;
                    }
                    #endregion

                    #region PIS

                    belPis objpis = new belPis();
                    string sCstPis = Convert.ToString(dgvDet["CstPis", i].Value);

                    if (sCstPis == "01" || sCstPis == "02") // aqui
                    {
                        belPisaliq objpisaliq = new belPisaliq();

                        objpisaliq.Cst = sCstPis;
                        objpisaliq.Ppis = Convert.ToDecimal(dgvDet["Ppis", i].Value);
                        objpisaliq.Vbc = Convert.ToDecimal(dgvDet["VbcPis", i].Value);
                        objpisaliq.Vpis = Convert.ToDecimal(dgvDet["Vpis", i].Value);
                        objpis.belPisaliq = objpisaliq;
                    }
                    else if (sCstPis == "03")
                    {
                        belPisqtde objpisqtde = new belPisqtde();
                        objpisqtde.Cst = sCstPis;
                        objpisqtde.Valiqprod = Convert.ToDecimal(dgvDet["ValiqprodPis", i].Value);
                        objpisqtde.Qbcprod = Convert.ToDecimal(dgvDet["QbcprodPis", i].Value);
                        objpisqtde.Vpis = Convert.ToDecimal(dgvDet["Vpis", i].Value);
                        objpis.belPisqtde = objpisqtde;
                    }
                    else if (sCstPis == "04" || sCstPis == "06" || sCstPis == "07" || sCstPis == "08" || sCstPis == "09")
                    {
                        belPisnt objpisnt = new belPisnt();
                        objpisnt.Cst = sCstPis;
                        objpis.belPisnt = objpisnt;
                    }
                    else //if (sCstPis == "99")
                    {
                        belPisoutr objpisoutr = new belPisoutr();

                        objpisoutr.Cst = sCstPis;
                        objpisoutr.Ppis = Convert.ToDecimal(dgvDet["Ppis", i].Value);
                        objpisoutr.Vbc = Convert.ToDecimal(dgvDet["VbcPis", i].Value);
                        if (Convert.ToDecimal(dgvDet["ValiqprodPis", i].Value) != 0)
                        {
                            objpisoutr.Valiqprod = Convert.ToDecimal(dgvDet["ValiqprodPis", i].Value);
                            //Danner - o.s. 24167 - 22/01/2010
                            objpisoutr.Qbcprod = Convert.ToDecimal(dgvDet["QbcprodPis", i].Value);
                            //Fim - Danner - o.s. 24167 - 22/01/2010
                        }
                        //objpisoutr.Vbcprod = Convert.ToString(dgvDet["QbcprodPis", i].Value);//Danner - o.s. 24167 - 22/01/2010
                        objpisoutr.Vpis = Convert.ToDecimal(dgvDet["Vpis", i].Value);
                        objpis.belPisoutr = objpisoutr;

                    }

                    objimposto.belPis = objpis;



                    #endregion

                    #region COFINS

                    belCofins objcofins = new belCofins();
                    string sCstCofins = Convert.ToString(dgvDet["CstCofins", i].Value);

                    if (sCstCofins == "01" || sCstCofins == "02")
                    {
                        belCofinsaliq objconfinsaliq = new belCofinsaliq();

                        objconfinsaliq.Cst = sCstCofins;
                        objconfinsaliq.Pcofins = Convert.ToDecimal(dgvDet["Pcofins", i].Value);
                        objconfinsaliq.Vbc = Convert.ToDecimal(dgvDet["VbcCofins", i].Value);
                        objconfinsaliq.Vcofins = Convert.ToDecimal(dgvDet["Vconfins", i].Value);

                        objcofins.belCofinsaliq = objconfinsaliq;
                    }
                    else if (sCstCofins == "03")
                    {
                        belCofinsqtde objcofinsqtde = new belCofinsqtde();

                        objcofinsqtde.Cst = sCstCofins;
                        objcofinsqtde.Qbcprod = Convert.ToDecimal(dgvDet["QbcprodCofins", i].Value);
                        objcofinsqtde.Valiqprod = Convert.ToDecimal(dgvDet["ValiqprodCofins", i].Value);
                        objcofinsqtde.Vcofins = Convert.ToDecimal(dgvDet["Vconfins", i].Value);
                        objcofins.belCofinsqtde = objcofinsqtde;
                    }
                    else if (sCstCofins == "04" || sCstCofins == "06" || sCstCofins == "07" || sCstCofins == "08" || sCstCofins == "09")
                    {
                        belCofinsnt objcofinsnt = new belCofinsnt();
                        objcofinsnt.Cst = sCstCofins;
                        objcofins.belCofinsnt = objcofinsnt;
                    }
                    else //if (sCstCofins == "99")
                    {
                        belCofinsoutr objcofinsoutr = new belCofinsoutr();
                        objcofinsoutr.Cst = sCstCofins;
                        objcofinsoutr.Pcofins = Convert.ToDecimal(dgvDet["Pcofins", i].Value);
                        objcofinsoutr.Vbc = Convert.ToDecimal(dgvDet["VbcCofins", i].Value);
                        objcofinsoutr.Qbcprod = Convert.ToDecimal(dgvDet["QbcprodCofins", i].Value);
                        objcofinsoutr.Valiqprod = Convert.ToDecimal(dgvDet["ValiqprodCofins", i].Value);
                        objcofinsoutr.Vcofins = Convert.ToDecimal(dgvDet["Vconfins", i].Value);
                        objcofins.belCofinsoutr = objcofinsoutr;
                    }
                    objimposto.belCofins = objcofins;

                    #endregion

                    #region ISSQN

                    if (Convert.ToDecimal(dgvDet["VbcIss", i].Value) != 0)
                    {
                        belIss objiss = new belIss();
                        objiss.Clistserv = Convert.ToInt64(dgvDet["ClistservIss", i].Value);
                        objiss.Cmunfg = Convert.ToString(dgvDet["CmunfgIss", i].Value);
                        objiss.Valiq = Convert.ToDecimal(dgvDet["ValiqIss", i].Value);
                        objiss.Vbc = Convert.ToDecimal(dgvDet["VbcIss", i].Value);
                        objiss.Vissqn = Convert.ToDecimal(dgvDet["VissqnIss", i].Value);
                        objimposto.belIss = objiss;
                    }



                    #endregion

                    objDet.belImposto = objimposto;

                    #region InfadProd
                    belInfadprod objinfadprod = new belInfadprod();
                    if (Convert.ToString(dgvDet["Infcpl", i].Value) != "")
                    {

                        objinfadprod.Infadprid = Convert.ToString(dgvDet["Infcpl", i].Value);
                        objDet.belInfadprod = objinfadprod;
                    }

                    #endregion

                    #endregion
                    lObjDet.Add(objDet);
                }
                lObj.Add(lObjDet);
                #endregion

                #region Totais

                belTotal objtotal = new belTotal();

                //Totais
                belIcmstot objIcmsTot = new belIcmstot();

                objIcmsTot.Vbc = nudVBC.Value;
                objIcmsTot.Vicms = nudVICMS.Value;
                objIcmsTot.Vbcst = nudVBCICMSST.Value;
                objIcmsTot.Vst = nudVST.Value;
                objIcmsTot.Vprod = nudVProd.Value;
                //Danner - o.s. 24154 - 18/02/2010
                objIcmsTot.Vfrete = nudVFrete.Value;
                //Fim - Danner - o.s. 24154 - 18/02/2010
                objIcmsTot.Vseg = nudVSEG.Value;
                objIcmsTot.Vdesc = nudVDesc.Value;
                objIcmsTot.Vii = nudVII.Value;
                objIcmsTot.Vipi = nudVIPI.Value;
                objIcmsTot.Vpis = nudVPIS.Value;
                objIcmsTot.Vcofins = nudVCOFINS.Value;
                objIcmsTot.Voutro = nudVOutro.Value;
                objIcmsTot.Vnf = nudVNF.Value;
                objtotal.belIcmstot = objIcmsTot;

                //Fim - Totais

                //ISSQNtot
                if (flpISSNQ.Enabled != false)
                {
                    belIssqntot objissqnTot = new belIssqntot();

                    objissqnTot.Vserv = nudVServ.Value;
                    objissqnTot.Vbc = nudVBCISS.Value;
                    objissqnTot.Viss = nudVISS.Value;
                    objissqnTot.Vpis = nudVPISISS.Value;
                    objissqnTot.Vcofins = nudVCOFINSISS.Value;
                    objtotal.belIssqntot = objissqnTot;
                }

                //Fin - ISSQNtot;

                //retTrib
                if (flpRetTrib.Enabled != false)
                {
                    belRetTrib objRetTrib = new belRetTrib();

                    objRetTrib.Vretpis = nudVPISRet.Value;
                    objRetTrib.Vretcofins = nudVCOFINSRet.Value;
                    objRetTrib.Vretcsll = nudVCSLLRet.Value;
                    objRetTrib.Vbcretprev = nudVBCIRRFRet.Value;
                    objRetTrib.Virrf = nudVIRRFRet.Value;
                    objRetTrib.Vbcirrf = nudVBCIRRFRet.Value;
                    objRetTrib.Vbcretprev = nudVBCRetPrev.Value;
                    objRetTrib.Vretprev = nudVRetPrev.Value;
                    objtotal.belRetTrib = objRetTrib;

                }

                lObj.Add(objtotal);

                //Fim - retTrib
                #endregion

                #region Transporte

                belTransp objtransp = new belTransp();

                switch (cbxModFrete.SelectedIndex) //Nfe_2.0
                {
                    case 0: objtransp.Modfrete = "0";
                        break;

                    case 1: objtransp.Modfrete = "1";
                        break;

                    case 2: objtransp.Modfrete = "2";
                        break;

                    case 3: objtransp.Modfrete = "9";
                        break;
                }

                //objtransp.Modfrete = Convert.ToString(cbxModFrete.SelectedIndex);

                //Transportadora
                belTransportadora objtransportadora = new belTransportadora();

                if (cbxPessoaTranp.SelectedIndex == 1)
                {
                    if (mtbCPJCNPJTransp.Text.Trim() != "")
                    {
                        objtransportadora.Cnpj = mtbCPJCNPJTransp.Text.Trim();
                    }
                }
                else
                {
                    if (mtbCPJCNPJTransp.Text.Trim() != "")
                    {
                        objtransportadora.Cpf = mtbCPJCNPJTransp.Text.Trim();
                    }
                }

                if (txtXnomeTransp.Text.Trim() != "")
                {
                    objtransportadora.Xnome = txtXnomeTransp.Text.Trim();
                }
                if (txtIETransp.Text.Trim() != "")
                {
                    objtransportadora.Ie = txtIETransp.Text.Trim();
                }
                if (txtEnderTransp.Text.Trim() != "")
                {
                    objtransportadora.Xender = txtEnderTransp.Text.Trim();
                }
                if (txtUFTransp.Text.Trim() != "")
                {
                    objtransportadora.Uf = txtUFTransp.Text.Trim();
                }
                if (txtXmunTransp.Text.Trim() != "")
                {
                    objtransportadora.Xmun = txtXmunTransp.Text.Trim();
                }

                objtransp.belTransportadora = objtransportadora;

                //Fim - Transportadora

                //VeicTransp
                if (flpVeicTransp.Enabled != false)
                {
                    belVeicTransp objVeicTrasnp = new belVeicTransp();
                    objVeicTrasnp.Placa = mtbPlacaVeicTransp.Text.Trim();
                    objVeicTrasnp.Uf = txtUFVeicTransp.Text.Trim();

                    //Danner - o.s. sem - 05/03/2010
                    if (txtRNTCVeicTransp.Text.Trim() != "")
                    {
                        objVeicTrasnp.Rntc = txtRNTCVeicTransp.Text.Trim();
                    }
                    //Fim - Danner - o.s. sem - 05/03/2010
                    objtransp.belVeicTransp = objVeicTrasnp;

                }
                //Fim -  VeicTransp

                //Reboque
                if (flpReboque.Enabled != false)
                {
                    belReboque objReboque = new belReboque();
                    objReboque.Placa = mtbPlacaReboque.Text.Trim();
                    objReboque.Uf = txtUFReboque.Text.Trim();
                    objReboque.Rntc = txtRNTCReboque.Text.Trim();
                    objtransp.belReboque = objReboque;
                }
                //Fim - Reboque 

                //RetTransp
                if (flpRetICMS.Enabled != false)
                {
                    belRetTransp objRetTransp = new belRetTransp();
                    objRetTransp.Vbvret = nudVBCICMSTransp.Value;
                    objRetTransp.Vserv = nudVServTransp.Value;
                    objRetTransp.Picmsret = nudPICMSTRetTransp.Value;
                    objRetTransp.Vicmsret = nudVICMSRet.Value;
                    objRetTransp.Cmunfg = txtCmunFGTransp.Text.Trim();
                    objRetTransp.Cfop = txtCFOPTransp.Text.Trim();
                    objtransp.belRetTransp = objRetTransp;
                }
                //Fim - RetTransp

                if (flpVolumes.Enabled != false)
                {
                    belVol objVol = new belVol();
                    objVol.Esp = txtEsp.Text.Trim();
                    objVol.Marca = txtMarca.Text.Trim();
                    //Danner - o.s. 24385 - 26/04/2010
                    if (txtNVol.Text != "")
                    {
                        objVol.Nvol = txtNVol.Text;//Danner - o.s. 24432 - 04/05/2010
                    }
                    //Fim - Danner - o.s. 24385 - 26/04/2010
                    objVol.PesoB = nudPesoB.Value;
                    objVol.PesoL = nudPesoL.Value;
                    objVol.Qvol = nudQvol.Value;
                    objtransp.belVol = objVol;

                }

                lObj.Add(objtransp);
                #endregion

                #region Cobrança

                belCobr objcobr = new belCobr();

                belFat objFat = new belFat();

                objFat.Nfat = txtNFat.Text.Trim();
                objFat.Vorig = nudVOrigFat.Value;
                objFat.Vdesc = nudVDescFat.Value;
                objFat.Vliq = nudVLiqFat.Value;

                if (dgvDup.RowCount != 0)
                {
                    List<belDup> lObjDup = new List<belDup>();

                    for (int i = 0; i < dgvDup.RowCount; i++)
                    {
                        belDup objdup = new belDup();
                        objdup.Ndup = Convert.ToString(dgvDup[0, i].Value);
                        objdup.Dvenc = Convert.ToDateTime(dgvDup[1, i].Value);
                        objdup.Vdup = Convert.ToDecimal(dgvDup[2, i].Value);
                        lObjDup.Add(objdup);
                    }
                    objFat.belDup = lObjDup;

                }
                objcobr.belFat = objFat;

                lObj.Add(objcobr);
                #endregion

                #region Inf Adicionais
                belInfAdic objinfadic = new belInfAdic();
                if (txtInfAdic.Text != "")
                {


                    objinfadic.Infcpl = txtInfAdic.Text.Trim().Replace(Environment.NewLine, "");
                }
                lObj.Add(objinfadic);

                #endregion

                #region exporta
                belExporta objexporta = new belExporta();
                if (cbxUF_embarque.SelectedIndex > 0)
                {
                    objexporta.Ufembarq = cbxUF_embarque.Text.ToString();
                }
                if (txtLocalEntrega.Text != "")
                {
                    objexporta.Xlocembarq = txtLocalEntrega.Text;
                }

                lObj.Add(objexporta);

                #endregion

                lObjTotNotasFinal[notAtual - 1] = lObj;
                lObjTotNotas[notAtual - 1] = lObjTotNotasFinal[notAtual - 1];

            }

            catch (Exception ex)
            {

                KryptonMessageBox.Show(ex.Message); ;
            }

        }


        private void btnProximo_Click(object sender, EventArgs e)
        {
            if (notAtual != notTotal)
            {
                notAtual++;
            }
            ControlaSeta();
            populaNF(lObjTotNotas[notAtual - 1]);
            HabilitaGroups(lObjTotNotas[notAtual - 1]);




        }
        private void btnUltimo_Click(object sender, EventArgs e)
        {
            notAtual = notTotal;
            ControlaSeta();
            populaNF(lObjTotNotas[notAtual - 1]);
            HabilitaGroups(lObjTotNotas[notAtual - 1]);

        }
        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (notAtual != 1)
            {
                notAtual--;
            }
            ControlaSeta();
            populaNF(lObjTotNotas[notAtual - 1]);
            HabilitaGroups(lObjTotNotas[notAtual - 1]);

        }
        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            notAtual = 1;

            ControlaSeta();
            populaNF(lObjTotNotas[notAtual - 1]);

            HabilitaGroups(lObjTotNotas[notAtual - 1]);

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (dgvDet.IsCurrentCellInEditMode != true)
            {
                HabilitaCampos(this.Controls, false);
                HabilitaGrids(true);
                desabilitaBotoes(true);
                Gravar();
                EmEdicao(false);
                ControlaSeta();
            }
            else
            {
                KryptonMessageBox.Show("Alguma Grid estava em Edição, o que foi alterado não foi perdido", "A L E R T A", MessageBoxButtons.OK, MessageBoxIcon.Information);
                HabilitaCampos(this.Controls, false);
                HabilitaGrids(true);
                desabilitaBotoes(true);
                Gravar();
                EmEdicao(false);
                ControlaSeta();
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {

            HabilitaCampos(this.Controls, false);
            desabilitaBotoes(true);
            EmEdicao(false);
            populaNF(lObjTotNotas[notAtual - 1]);
            ControlaSeta();

        }
        private void btnCancelNFe_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < lObjTotNotas.Count; i++)
                {
                    belIde objbelIde = lObjTotNotas[i][0] as belIde;

                    string sSqlAtualizaNF = "update NF set st_contingencia = '" + "N" +
                                         "' where cd_empresa = '" + _sEmp +
                                         "' and cd_nfseq = '" + objbelIde.Cnf.Substring(2, 6) + "'";

                    using (FbCommand cmdUpdate = new FbCommand(sSqlAtualizaNF, cx.get_Conexao()))
                    {
                        cx.Open_Conexao();
                        cmdUpdate.ExecuteNonQuery();
                    }
                }
                Cancel = true;
                this.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally { cx.Close_Conexao(); }

        }
        private void btnEnviar_Click(object sender, EventArgs e)
        {
            HabilitaGroups(lObjTotNotas[notAtual - 1]);
            Gravar();


            this.Close();
        }
        private void btnPesquisarNF_Click(object sender, EventArgs e)
        {
            frmPesquisaNfe frm = new frmPesquisaNfe(_sEmp);
            frm.ShowDialog();
            objbelCampoPesquisa = frm.objbelCampoPesquisa;

            if (objbelCampoPesquisa != null)
            {
                if (objbelCampoPesquisa.ChaveAcesso != "")
                {
                    txtChaveAcesso.Text = objbelCampoPesquisa.ChaveAcesso;
                    tabControl1.SelectedTab = tabPageRefNfe;
                }
                else
                {
                    txtnNFref.Text = objbelCampoPesquisa.NumeroNF;
                    txtClifor.Text = objbelCampoPesquisa.sCli_For;
                    txtCNPJ.Text = objbelCampoPesquisa.CNPJ;
                    txtcUF.Text = objbelCampoPesquisa.cUF;
                    txtserieRef.Text = objbelCampoPesquisa.serie;
                    txtAAMM.Text = Convert.ToDateTime(objbelCampoPesquisa.AAMM).ToString("dd/MM/yyyy");
                    tabControl1.SelectedTab = tabPageRefNotaA1;
                }
                objbelCampoPesquisa = new belCampoPesquisa();
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            errorProvider2.Clear();
            try
            {
                belNFref obj = new belNFref();
                int icountErro = 0;
                if (txtChaveAcesso.Text != "")
                {
                    if (txtChaveAcesso.Text.Length != 44)
                    {
                        icountErro++;
                        errorProvider2.SetError(txtChaveAcesso, "Campo Obrigatório - Chave incompleta!!");
                    }
                    obj.RefNFe = txtChaveAcesso.Text;
                }
                else
                {
                    //Valida Campos.                    
                    if (txtcUF.Text == "")
                    {
                        icountErro++;
                        errorProvider2.SetError(txtcUF, "Campo Obrigatório!!");
                    }
                    if (txtAAMM.Text == "")
                    {
                        icountErro++;
                        errorProvider2.SetError(txtAAMM, "Campo Obrigatório!!");
                    }
                    if (txtCNPJ.Text == "")
                    {
                        icountErro++;
                        errorProvider2.SetError(txtCNPJ, "Campo Obrigatório!!");
                    }
                    if (txtserieRef.Text == "")
                    {
                        icountErro++;
                        errorProvider2.SetError(txtserieRef, "Campo Obrigatório!!");
                    }
                    if (txtnNFref.Text == "")
                    {
                        icountErro++;
                        errorProvider2.SetError(txtnNFref, "Campo Obrigatório!!");
                    }

                    obj.cUF = txtcUF.Text;
                    if (txtAAMM.Text != "")
                    {
                        obj.AAMM = Convert.ToDateTime(txtAAMM.Text).ToString("yyMM");
                    }
                    obj.CNPJ = txtCNPJ.Text;
                    obj.serie = txtserieRef.Text;
                    obj.nNF = txtnNFref.Text;
                    obj.mod = textBoxmod.Text;
                }
                if (icountErro == 0)
                {
                    belNFrefBindingSource.Add(obj);
                    errorProvider2.Clear();
                }
                else
                {
                    throw new Exception("Favor Verificar as Inconsistências !!");
                }
                txtChaveAcesso.Text = "";
                txtcUF.Text = "";
                txtAAMM.Text = "";
                txtCNPJ.Text = "";
                txtserieRef.Text = "";
                txtnNFref.Text = "";
                txtClifor.Text = "";
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, ex.Message, "E R R O", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void btnRemover_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvRefNFe.CurrentRow != null)
                {
                    if (dgvRefNFe[1, dgvRefNFe.CurrentRow.Index].Value != null) //Remove NFe A1
                    {
                        string sNF = dgvRefNFe[1, dgvRefNFe.CurrentRow.Index].Value.ToString();
                        for (int i = 0; i < belNFrefBindingSource.Count; i++)
                        {
                            belNFref belRemove = ((belNFrefBindingSource[i]) as belNFref);
                            if (belRemove.nNF != null)
                            {
                                if (belRemove.nNF.Equals(sNF))
                                {
                                    belNFrefBindingSource.Remove(belRemove);
                                    break;
                                }
                            }
                        }
                    }
                    else if (dgvRefNFe[0, dgvRefNFe.CurrentRow.Index].Value != null) //Remove NFe
                    {
                        string sChave = dgvRefNFe[0, dgvRefNFe.CurrentRow.Index].Value.ToString();
                        for (int i = 0; i < belNFrefBindingSource.Count; i++)
                        {
                            belNFref belRemove = ((belNFrefBindingSource[i]) as belNFref);
                            if (belRemove.RefNFe != null)
                            {
                                if (belRemove.RefNFe.Equals(sChave))
                                {
                                    belNFrefBindingSource.Remove(belRemove);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void btnLimparRef_Click(object sender, EventArgs e)
        {
            txtChaveAcesso.Text = "";
            txtcUF.Text = "";
            txtAAMM.Text = "";
            txtCNPJ.Text = "";
            txtserieRef.Text = "";
            txtnNFref.Text = "";
            txtClifor.Text = "";
        }
        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (belStatic.BAlteraDadosNfe == true)
            {
                HabilitaCampos(this.Controls, true);
                desabilitaBotoes(true);
                HabilitaGroups(lObjTotNotas[notAtual - 1]);
                HabilitaGrids(true);
                EmEdicao(true);
                ControlaSeta();
            }
            else
            {
                if (KryptonMessageBox.Show(null, "Usuário não tem Acesso para Alterar dados da Nota Fiscal" +
                     Environment.NewLine +
                     Environment.NewLine +
                     "Deseja entrar com a Permissão de um Outro Usuário? ", "A V I S O",
                      MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    frmLogin objfrm = new frmLogin();
                    objfrm.ShowDialog();
                    if (!objfrm.bFechaAplicativo)
                    {
                        if (belStatic.BAlteraDadosNfe == true)
                        {
                            HabilitaCampos(this.Controls, true);
                            desabilitaBotoes(true);
                            HabilitaGroups(lObjTotNotas[notAtual - 1]);
                            HabilitaGrids(true);
                            EmEdicao(true);
                            ControlaSeta();
                        }
                        else
                        {
                            KryptonMessageBox.Show(null, "Usuário também não tem Permissão Para Alterar Dados da Nota Fiscal", "A V I S O", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }



        private void cbxPessoaEmit_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbxPessoaEmit.SelectedIndex == 1)
            {
                mtbCpfCnpjEmit.Mask = "00.000.000/0000-00";
            }
            else
            {
                mtbCpfCnpjEmit.Mask = "000.000.000-00";
            }
        }
        private void cbxPessoaDest_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbxPessoaDest.SelectedIndex == 1)
            {
                mtbCpfCnpjDest.Mask = "00.000.000/0000-00";
            }
            else if (cbxPessoaDest.SelectedIndex == 0)
            {
                mtbCpfCnpjDest.Mask = "000.000.000-00";
            }
            else
            {
                mtbCpfCnpjDest.Mask = "";
                mtbCpfCnpjDest.Text = "EXTERIOR";
            }
        }
        private void cbxPessoaTranp_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbxPessoaTranp.SelectedIndex == 1)
            {
                mtbCPJCNPJTransp.Mask = "00.000.000/0000-00";
            }
            else
            {
                mtbCPJCNPJTransp.Mask = "000.000.000-00";
            }
        }
        private void dgvDet_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDet.IsCurrentCellInEditMode == true)
            {
                if (dgvDet[e.ColumnIndex, e.RowIndex].Value != null)
                {
                    if (dgvDet.Columns[e.ColumnIndex].DefaultCellStyle.Format.Equals("n5"))
                    {
                        dgvDet[e.ColumnIndex, e.RowIndex].Value = Convert.ToDecimal(dgvDet[e.ColumnIndex, e.RowIndex].Value.ToString()).ToString("n5");
                    }
                    else
                    {
                        dgvDet[e.ColumnIndex, e.RowIndex].Value = dgvDet[e.ColumnIndex, e.RowIndex].Value.ToString().ToUpper();
                    }
                }
            }
        }
        private void txtEnderDestCmun_Validating(object sender, CancelEventArgs e)
        {
            if (txtEnderDestCmun.Text != "")
            {
                Regex m = new Regex("^[0-9]+$");
                if (!m.IsMatch(txtEnderDestCmun.Text))
                {
                    e.Cancel = true;
                    KryptonMessageBox.Show("Digite Somente caracteres numéricos", "A L E R T A", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        private void txtEnderDestCpais_Validating(object sender, CancelEventArgs e)
        {
            if (txtEnderDestCpais.Text != "")
            {
                Regex m = new Regex("^[0-9]+$");
                if (!m.IsMatch(txtEnderDestCpais.Text))
                {
                    e.Cancel = true;
                    KryptonMessageBox.Show("Digite Somente caracteres numéricos", "A L E R T A", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        private void txtIEDest_Validating(object sender, CancelEventArgs e)
        {
            if (txtIEDest.Text != "")
            {
                Regex m = new Regex("^[0-9.]+$");
                if (!m.IsMatch(txtIEDest.Text))
                {
                    e.Cancel = true;
                    KryptonMessageBox.Show("Digite Somente caracteres numéricos", "A L E R T A", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        private void txtEndEntCmun_Validating(object sender, CancelEventArgs e)
        {
            if (txtEndEntCmun.Text != "")
            {
                Regex m = new Regex("^[0-9]+$");
                if (!m.IsMatch(txtEndEntCmun.Text))
                {
                    e.Cancel = true;
                    KryptonMessageBox.Show("Digite Somente caracteres numéricos", "A L E R T A", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        private void txtEnderEmitCmun_Validating(object sender, CancelEventArgs e)
        {
            if (txtEnderEmitCmun.Text != "")
            {
                Regex m = new Regex("^[0-9]+$");
                if (!m.IsMatch(txtEnderEmitCmun.Text))
                {
                    e.Cancel = true;
                    KryptonMessageBox.Show("Digite Somente caracteres numéricos", "A L E R T A", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        private void txtEnderEmitCpais_Validating(object sender, CancelEventArgs e)
        {
            if (txtEnderEmitCpais.Text != "")
            {
                Regex m = new Regex("^[0-9]+$");
                if (!m.IsMatch(txtEnderEmitCpais.Text))
                {
                    e.Cancel = true;
                    KryptonMessageBox.Show("Digite Somente caracteres numéricos", "A L E R T A", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        private void txtIEEmit_Validating(object sender, CancelEventArgs e)
        {
            if (txtIEEmit.Text != "")
            {
                Regex m = new Regex("^[0-9.]+$");
                if (!m.IsMatch(txtIEEmit.Text))
                {
                    e.Cancel = true;
                    KryptonMessageBox.Show("Digite Somente caracteres numéricos", "A L E R T A", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        private void txtSeq_Validating(object sender, CancelEventArgs e)
        {
            if (txtSeq.Text != "")
            {
                Regex m = new Regex("^[0-9]+$");
                if (!m.IsMatch(txtSeq.Text))
                {
                    e.Cancel = true;
                    KryptonMessageBox.Show("Digite Somente caracteres numéricos", "A L E R T A", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        private void txtNNF_Validating(object sender, CancelEventArgs e)
        {
            if (txtNNF.Text != "")
            {
                Regex m = new Regex("^[0-9]+$");
                if (!m.IsMatch(txtNNF.Text))
                {
                    e.Cancel = true;
                    KryptonMessageBox.Show("Digite Somente caracteres numéricos", "A L E R T A", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        private void txtCMunFG_Validating(object sender, CancelEventArgs e)
        {
            if (txtCMunFG.Text != "")
            {
                Regex m = new Regex("^[0-9]+$");
                if (!m.IsMatch(txtCMunFG.Text))
                {
                    e.Cancel = true;
                    KryptonMessageBox.Show("Digite Somente caracteres numéricos", "A L E R T A", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        private void txtCDV_Validating(object sender, CancelEventArgs e)
        {
            if (txtCDV.Text != "")
            {
                Regex m = new Regex("^[0-9]+$");
                if (!m.IsMatch(txtCDV.Text))
                {
                    e.Cancel = true;
                    KryptonMessageBox.Show("Digite Somente caracteres numéricos", "A L E R T A", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        private void txtProcEmi_Validating(object sender, CancelEventArgs e)
        {
            Regex n = new Regex("^([0]|[1]|[2]|[3])$");
            if (txtProcEmi.Text != "")
            {
                Regex m = new Regex("^[0-9]+$");

                if (!m.IsMatch(txtProcEmi.Text))
                {
                    e.Cancel = true;
                    KryptonMessageBox.Show("Digite Somente caracteres numéricos", "A L E R T A", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (!n.IsMatch(txtProcEmi.Text))
                {
                    e.Cancel = true;
                    KryptonMessageBox.Show("Digite somente valores entre 0 à 3", "A L E R T A", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

        }
        private void txtCmunFGTransp_Validating(object sender, CancelEventArgs e)
        {
            if (txtCmunFGTransp.Text != "")
            {
                Regex m = new Regex("^[0-9]+$");
                if (!m.IsMatch(txtCmunFGTransp.Text))
                {
                    e.Cancel = true;
                    KryptonMessageBox.Show("Digite Somente caracteres numéricos", "A L E R T A", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        private void dgvDet_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.FormattedValue.ToString() != "")
            {
                if (dgvDet.Columns["Genero"].Index == e.ColumnIndex ||
                    dgvDet.Columns["Qcom"].Index == e.ColumnIndex ||
                    dgvDet.Columns["Vdesc"].Index == e.ColumnIndex ||
                    dgvDet.Columns["Vfrete"].Index == e.ColumnIndex ||
                    dgvDet.Columns["Vseg"].Index == e.ColumnIndex ||
                    dgvDet.Columns["Vuntrib"].Index == e.ColumnIndex ||
                    dgvDet.Columns["Qtrib"].Index == e.ColumnIndex ||
                    dgvDet.Columns["Vuncom"].Index == e.ColumnIndex ||
                    dgvDet.Columns["ModbcIcms"].Index == e.ColumnIndex ||
                    dgvDet.Columns["ModbcstIcms"].Index == e.ColumnIndex ||
                    dgvDet.Columns["OrigIcms"].Index == e.ColumnIndex ||
                    dgvDet.Columns["PicmsIcms"].Index == e.ColumnIndex ||
                    dgvDet.Columns["PicmsstIcms"].Index == e.ColumnIndex ||
                    dgvDet.Columns["PmvastIcms"].Index == e.ColumnIndex ||
                    dgvDet.Columns["PredbcIcms"].Index == e.ColumnIndex ||
                    dgvDet.Columns["PredbcstIcms"].Index == e.ColumnIndex ||
                    dgvDet.Columns["VbcIcms"].Index == e.ColumnIndex ||
                    dgvDet.Columns["VbcstIcms"].Index == e.ColumnIndex ||
                    dgvDet.Columns["VicmsIcms"].Index == e.ColumnIndex ||
                    dgvDet.Columns["VicmsstIcms"].Index == e.ColumnIndex ||
                    dgvDet.Columns["PipiTrib"].Index == e.ColumnIndex ||
                    dgvDet.Columns["QunidIpiTrib"].Index == e.ColumnIndex ||
                    dgvDet.Columns["VbcIpiTrib"].Index == e.ColumnIndex ||
                    dgvDet.Columns["PipiTrib"].Index == e.ColumnIndex ||
                    dgvDet.Columns["VunidTrib"].Index == e.ColumnIndex ||
                    dgvDet.Columns["VbcIi"].Index == e.ColumnIndex ||
                    dgvDet.Columns["VdespaduIi"].Index == e.ColumnIndex ||
                    dgvDet.Columns["Vii"].Index == e.ColumnIndex ||
                    dgvDet.Columns["ViofIi"].Index == e.ColumnIndex ||
                    dgvDet.Columns["Ppis"].Index == e.ColumnIndex ||
                    dgvDet.Columns["VbcPis"].Index == e.ColumnIndex ||
                    dgvDet.Columns["ValiqprodPis"].Index == e.ColumnIndex ||
                    dgvDet.Columns["QbcprodPis"].Index == e.ColumnIndex ||
                    dgvDet.Columns["Vpis"].Index == e.ColumnIndex ||
                    dgvDet.Columns["Pcofins"].Index == e.ColumnIndex ||
                    dgvDet.Columns["VbcCofins"].Index == e.ColumnIndex ||
                    dgvDet.Columns["QbcprodCofins"].Index == e.ColumnIndex ||
                    dgvDet.Columns["ValiqprodCofins"].Index == e.ColumnIndex ||
                    dgvDet.Columns["Vconfins"].Index == e.ColumnIndex ||
                    dgvDet.Columns["ClistservIss"].Index == e.ColumnIndex ||
                    dgvDet.Columns["CmunfgIss"].Index == e.ColumnIndex ||
                    dgvDet.Columns["VbcIss"].Index == e.ColumnIndex ||
                    dgvDet.Columns["VissqnIss"].Index == e.ColumnIndex ||
                    dgvDet.Columns["ValiqIss"].Index == e.ColumnIndex)
                {
                    Regex m = new Regex(@"^[0-9\.\,]+$");

                    if (!m.IsMatch(e.FormattedValue.ToString()))
                    {
                        e.Cancel = true;
                        KryptonMessageBox.Show("Campo Numérico", "A L E R T A", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }

            }
        }
        private void dgvDup_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.FormattedValue.ToString() != "")
            {
                if (e.ColumnIndex == 2)
                {
                    Regex m = new Regex(@"^[0-9\.\,]+$");
                    if (!m.IsMatch(e.FormattedValue.ToString()))
                    {
                        e.Cancel = true;
                        KryptonMessageBox.Show("Campo Numérico", "A L E R T A", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
            }
        }




        private void tbcNFe_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tbcNFe.SelectedTab != null)
                {
                    if (tbcNFe.SelectedTab.Name.Equals("tabPageNFeRef"))
                    {
                        belIde objide = lObjTotNotas[(notAtual - 1)][0] as belIde;
                        if (!objide.bReferenciaNF)
                        {
                            KryptonMessageBox.Show(null, "Tipo de Documento dessa Nota não permite Referenciar outras Notas", "A V I S O", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            tbcNFe.SelectedTab = tabPageIDE;
                        }
                    }
                }
            }
            catch (Exception EX)
            {
                throw EX;
            }


        }

        private void txtInfAdic_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblCaracter.Text = "Total Caracter: " + txtInfAdic.Text.Length.ToString();
        }


        private void tsDI_Click_1(object sender, EventArgs e)
        {
            if (btnAtualizar.Enabled == false)
            {
                frmDeclaracaoImportacaoNfe objfrm = new frmDeclaracaoImportacaoNfe((List<belDet>)lObjTotNotas[notAtual - 1][4], false);
                objfrm.ShowDialog();
                Gravar();
            }
            else
            {
                KryptonMessageBox.Show(null, "Formulário não está em modo de alteração!", "A V I S O", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Close();
        }


        private void tsHlpTeste_Click(object sender, EventArgs e)
        {
            if (btnAtualizar.Enabled == false)
            {
                frmDeclaracaoImportacaoNfe objfrm = new frmDeclaracaoImportacaoNfe((List<belDet>)lObjTotNotas[notAtual - 1][4], true);
                objfrm.ShowDialog();
                Gravar();
            }
            else
            {
                KryptonMessageBox.Show(null, "Formulário não está em modo de alteração!", "A V I S O", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void kryptonGroupBox1_Panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void nudVII_ValueChanged(object sender, EventArgs e)
        {

        }

        private void nudVLiqFat_Validated(object sender, EventArgs e)
        {

        }

        private void cmbCRT_SelectedIndexChanged(object sender, EventArgs e)
        {

        }







    }
}