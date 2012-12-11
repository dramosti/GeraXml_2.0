using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HLP.bel.CTe;
using System.Text.RegularExpressions;
using ComponentFactory.Krypton.Toolkit;
using HLP.bel.Static;

namespace NfeGerarXml
{
    public partial class frmVisualizaCte : KryptonForm
    {
        public string _sMessageException = string.Format("Ocorreu uma Exceção ao Manipular essa Ação : {0}{0}Verifique a Mensagem abaixo: {0}________________________________{0}{0}", Environment.NewLine);
        belPopulaObjetos objObjetos = null;
        public belPopulaObjetos objObjetosAlter = null;
        public bool bCancela = false;
        bool bEnviar = false;
        int iIndex = 0;
        int iCountObj = 0;




        public frmVisualizaCte(belPopulaObjetos objObjetos)
        {
            InitializeComponent();
            this.objObjetos = objObjetos;
            CriaObjAlter();
            PopulaForm(iIndex);
            VerificaCte();
            HabilitaCampos(false);
            EmEdicao(false);
            iCountObj = objObjetos.objLinfCte.Count() - 1;
            lblContagemNotas.Text = "1 de " + (iCountObj + 1);
            tabCte.ItemSize = new Size(25, 125);
        }

        struct ErrosNotas
        {
            public string sErro { get; set; }
            public string sNumCte { get; set; }
        }
        List<ErrosNotas> objListaErroAtual;


        private void LimpaCampos(Control.ControlCollection ctrTela)
        {
            foreach (Control ctr in ctrTela)
            {
                if (ctr.HasChildren == true)
                {
                    LimpaCampos(ctr.Controls);
                }
                else
                {
                    foreach (Control ctrsub in ctrTela)
                    {
                        if (ctrsub is KryptonTextBox)
                        {
                            ctrsub.Text = "";
                        }
                        if (ctrsub is KryptonMaskedTextBox) { ctrsub.Text = ""; }
                        if (ctrsub is KryptonComboBox)
                        {
                            ((KryptonComboBox)ctrsub).SelectedIndex = -1;
                        }
                        if (ctrsub is KryptonNumericUpDown)
                        {
                            ((KryptonNumericUpDown)ctrsub).Value = 0;
                        }
                        if (ctrsub is KryptonNumericUpDown)
                        {
                            ((KryptonNumericUpDown)ctrsub).Text = "0.00";
                        }
                        if (ctrsub is KryptonDateTimePicker) { ((KryptonDateTimePicker)ctrsub).Value = HLP.Util.Util.GetDateServidor(); }
                        if (ctrsub is KryptonListBox) { ((KryptonListBox)ctrsub).Items.Clear(); }
                        if (ctrsub is KryptonCheckBox) { ((KryptonCheckBox)ctrsub).Checked = false; }
                        if (ctrsub is KryptonRichTextBox) { ((KryptonRichTextBox)ctrsub).Text = ""; }

                    }
                }
            }
        }
        private void HabilitaCampos(bool pHabilita)
        {
            panelIdentificacao.Enabled = pHabilita;
            panelTomador.Enabled = pHabilita;
            panelEmitente.Enabled = pHabilita;
            panelRemetente.Enabled = pHabilita;
            panelDestinatario.Enabled = pHabilita;
            panelExpedidor.Enabled = pHabilita;
            panelRecebedor.Enabled = pHabilita;
            panelNotas.Enabled = pHabilita;
            panelDocumentos.Enabled = pHabilita;
            panelValores.Enabled = pHabilita;
            panelInformacoes.Enabled = pHabilita;
            panelRodoviario.Enabled = pHabilita;
            panelVeiculo.Enabled = pHabilita;
        }
        private void EmEdicao(bool bEmEdit)
        {
            btnAtualizar.Enabled = !bEmEdit;
            btnSalvar.Enabled = bEmEdit;
            btnCancelar.Enabled = bEmEdit;
            btnSair.Enabled = !bEmEdit;
            btnEnviar.Enabled = !bEmEdit;
        }



        private void CriaObjAlter()
        {
            try
            {
                List<belinfCte> objList = new List<belinfCte>();


                for (int i = 0; i < this.objObjetos.objLinfCte.Count; i++)
                {
                    belinfCte objbelinfCte = new belinfCte();
                    belinfCte obj = this.objObjetos.objLinfCte[i];

                    #region Identificacao
                    objbelinfCte.id = obj.id;
                    objbelinfCte.ide = new belide();

                    objbelinfCte.ide.cUF = obj.ide.cUF;
                    objbelinfCte.ide.cCT = obj.ide.cCT;
                    objbelinfCte.ide.CFOP = obj.ide.CFOP;
                    objbelinfCte.ide.natOp = obj.ide.natOp;
                    objbelinfCte.ide.forPag = obj.ide.forPag;
                    objbelinfCte.ide.mod = obj.ide.mod;
                    objbelinfCte.ide.serie = obj.ide.serie;
                    objbelinfCte.ide.nCT = obj.ide.nCT;
                    objbelinfCte.ide.tpImp = obj.ide.tpImp;
                    objbelinfCte.ide.tpEmis = belStatic.bModoContingencia == true ? "2" : "1";
                    objbelinfCte.ide.cDV = obj.ide.cDV;
                    objbelinfCte.ide.tpAmb = obj.ide.tpAmb;
                    objbelinfCte.ide.tpCTe = obj.ide.tpCTe;
                    objbelinfCte.ide.procEmi = obj.ide.procEmi;
                    objbelinfCte.ide.verProc = obj.ide.verProc;
                    objbelinfCte.ide.cMunEnv = obj.ide.cMunEnv;
                    objbelinfCte.ide.xMunEnv = obj.ide.xMunEnv;
                    objbelinfCte.ide.UFEnv = obj.ide.UFEnv;
                    objbelinfCte.ide.modal = obj.ide.modal;
                    objbelinfCte.ide.tpServ = obj.ide.tpServ;
                    objbelinfCte.ide.cMunIni = obj.ide.cMunIni;
                    objbelinfCte.ide.xMunIni = obj.ide.xMunIni;
                    objbelinfCte.ide.UFIni = obj.ide.UFIni;
                    objbelinfCte.ide.cMunFim = obj.ide.cMunFim;
                    objbelinfCte.ide.xMunFim = obj.ide.xMunFim;
                    objbelinfCte.ide.UFFim = obj.ide.UFFim;
                    objbelinfCte.ide.retira = obj.ide.retira;
                    objbelinfCte.ide.xDetRetira = obj.ide.xDetRetira;

                    #endregion

                    #region compl
                    if (obj.compl != null)
                    {
                        objbelinfCte.compl = new belcompl();
                        objbelinfCte.compl.ObsCont.Xcampo = obj.compl.ObsCont.Xcampo;
                        objbelinfCte.compl.ObsCont.Xtexto = obj.compl.ObsCont.Xtexto;                        
                    }
                    #endregion

                    #region Tomador
                    if (obj.ide.toma03 != null)
                    {
                        objbelinfCte.ide.toma03 = new beltoma03();
                        objbelinfCte.ide.toma03.toma = obj.ide.toma03.toma;
                    }
                    else if (obj.ide.toma04 != null)
                    {
                        objbelinfCte.ide.toma04 = new beltoma04();
                        objbelinfCte.ide.toma04.toma = obj.ide.toma04.toma;
                        objbelinfCte.ide.toma04.CNPJ = obj.ide.toma04.CNPJ;
                        objbelinfCte.ide.toma04.CPF = obj.ide.toma04.CPF;
                        objbelinfCte.ide.toma04.IE = obj.ide.toma04.IE;
                        objbelinfCte.ide.toma04.xNome = obj.ide.toma04.xNome;
                        objbelinfCte.ide.toma04.xFant = obj.ide.toma04.xFant;
                        objbelinfCte.ide.toma04.fone = obj.ide.toma04.fone;

                        objbelinfCte.ide.toma04.enderToma = new belenderToma();
                        objbelinfCte.ide.toma04.enderToma.xLgr = obj.ide.toma04.enderToma.xLgr;
                        objbelinfCte.ide.toma04.enderToma.nro = obj.ide.toma04.enderToma.nro;
                        objbelinfCte.ide.toma04.enderToma.xCpl = obj.ide.toma04.enderToma.xCpl;
                        objbelinfCte.ide.toma04.enderToma.xBairro = obj.ide.toma04.enderToma.xBairro;
                        objbelinfCte.ide.toma04.enderToma.cMun = obj.ide.toma04.enderToma.cMun;
                        objbelinfCte.ide.toma04.enderToma.xMun = obj.ide.toma04.enderToma.xMun;
                        objbelinfCte.ide.toma04.enderToma.CEP = obj.ide.toma04.enderToma.CEP;
                        objbelinfCte.ide.toma04.enderToma.UF = obj.ide.toma04.enderToma.UF;
                        objbelinfCte.ide.toma04.enderToma.cPais = obj.ide.toma04.enderToma.cPais;
                        objbelinfCte.ide.toma04.enderToma.xPais = obj.ide.toma04.enderToma.xPais;

                    }

                    #endregion

                    #region Emitente
                    objbelinfCte.emit = new belemit();

                    objbelinfCte.emit.CNPJ = obj.emit.CNPJ;
                    objbelinfCte.emit.IE = obj.emit.IE;
                    objbelinfCte.emit.xNome = obj.emit.xNome;
                    objbelinfCte.emit.xFant = obj.emit.xFant;

                    objbelinfCte.emit.enderEmit = new belenderEmit();

                    objbelinfCte.emit.enderEmit.xLgr = obj.emit.enderEmit.xLgr;
                    objbelinfCte.emit.enderEmit.nro = obj.emit.enderEmit.nro;
                    objbelinfCte.emit.enderEmit.xCpl = obj.emit.enderEmit.xCpl;
                    objbelinfCte.emit.enderEmit.xBairro = obj.emit.enderEmit.xBairro;
                    objbelinfCte.emit.enderEmit.cMun = obj.emit.enderEmit.cMun;
                    objbelinfCte.emit.enderEmit.xMun = obj.emit.enderEmit.xMun;
                    objbelinfCte.emit.enderEmit.CEP = obj.emit.enderEmit.CEP;
                    objbelinfCte.emit.enderEmit.UF = obj.emit.enderEmit.UF;
                    objbelinfCte.emit.enderEmit.fone = obj.emit.enderEmit.fone;

                    #endregion

                    #region Remetente
                    objbelinfCte.rem = new belrem();

                    objbelinfCte.rem.CNPJ = obj.rem.CNPJ;
                    objbelinfCte.rem.CPF = obj.rem.CPF;
                    objbelinfCte.rem.IE = obj.rem.IE;
                    objbelinfCte.rem.xNome = obj.rem.xNome;
                    objbelinfCte.rem.xFant = obj.rem.xFant;
                    objbelinfCte.rem.fone = obj.rem.fone;

                    objbelinfCte.rem.enderReme = new belenderReme();

                    objbelinfCte.rem.enderReme.xLgr = obj.rem.enderReme.xLgr;
                    objbelinfCte.rem.enderReme.nro = obj.rem.enderReme.nro;
                    objbelinfCte.rem.enderReme.xCpl = obj.rem.enderReme.xCpl;
                    objbelinfCte.rem.enderReme.xBairro = obj.rem.enderReme.xBairro;
                    objbelinfCte.rem.enderReme.cMun = obj.rem.enderReme.cMun;
                    objbelinfCte.rem.enderReme.xMun = obj.rem.enderReme.xMun;
                    objbelinfCte.rem.enderReme.CEP = obj.rem.enderReme.CEP;
                    objbelinfCte.rem.enderReme.UF = obj.rem.enderReme.UF;
                    objbelinfCte.rem.enderReme.xPais = obj.rem.enderReme.xPais;
                    objbelinfCte.rem.enderReme.cPais = obj.rem.enderReme.cPais;

                    #endregion

                    #region Destinatario

                    objbelinfCte.dest = new beldest();

                    objbelinfCte.dest.CNPJ = obj.dest.CNPJ;
                    objbelinfCte.dest.CPF = obj.dest.CPF;
                    objbelinfCte.dest.IE = obj.dest.IE;
                    objbelinfCte.dest.xNome = obj.dest.xNome;
                    objbelinfCte.dest.fone = obj.dest.fone;
                    objbelinfCte.dest.ISUF = obj.dest.ISUF;

                    objbelinfCte.dest.enderDest = new belenderDest();

                    objbelinfCte.dest.enderDest.xLgr = obj.dest.enderDest.xLgr;
                    objbelinfCte.dest.enderDest.nro = obj.dest.enderDest.nro;
                    objbelinfCte.dest.enderDest.xCpl = obj.dest.enderDest.xCpl;
                    objbelinfCte.dest.enderDest.xBairro = obj.dest.enderDest.xBairro;
                    objbelinfCte.dest.enderDest.cMun = obj.dest.enderDest.cMun;
                    objbelinfCte.dest.enderDest.xMun = obj.dest.enderDest.xMun;
                    objbelinfCte.dest.enderDest.CEP = obj.dest.enderDest.CEP;
                    objbelinfCte.dest.enderDest.UF = obj.dest.enderDest.UF;
                    objbelinfCte.dest.enderDest.xPais = obj.dest.enderDest.xPais;
                    objbelinfCte.dest.enderDest.cPais = obj.dest.enderDest.cPais;

                    #endregion

                    #region Expedidor
                    if (obj.exped != null)
                    {
                        objbelinfCte.exped = new belexped();

                        objbelinfCte.exped.CNPJ = obj.exped.CNPJ;
                        objbelinfCte.exped.CPF = obj.exped.CPF;
                        objbelinfCte.exped.IE = obj.exped.IE;
                        objbelinfCte.exped.xNome = obj.exped.xNome;
                        objbelinfCte.exped.fone = obj.exped.fone;

                        objbelinfCte.exped.enderExped = new belenderExped();

                        objbelinfCte.exped.enderExped.xLgr = obj.exped.enderExped.xLgr;
                        objbelinfCte.exped.enderExped.nro = obj.exped.enderExped.nro;
                        objbelinfCte.exped.enderExped.xCpl = obj.exped.enderExped.xCpl;
                        objbelinfCte.exped.enderExped.xBairro = obj.exped.enderExped.xBairro;
                        objbelinfCte.exped.enderExped.cMun = obj.exped.enderExped.cMun;
                        objbelinfCte.exped.enderExped.xMun = obj.exped.enderExped.xMun;
                        objbelinfCte.exped.enderExped.CEP = obj.exped.enderExped.CEP;
                        objbelinfCte.exped.enderExped.UF = obj.exped.enderExped.UF;
                        objbelinfCte.exped.enderExped.xPais = obj.exped.enderExped.xPais;
                        objbelinfCte.exped.enderExped.cPais = obj.exped.enderExped.cPais;


                    }


                    #endregion

                    #region Recebedor
                    if (obj.receb != null)
                    {
                        objbelinfCte.receb = new belreceb();

                        objbelinfCte.receb.CNPJ = obj.receb.CNPJ;
                        objbelinfCte.receb.CPF = obj.receb.CPF;
                        objbelinfCte.receb.IE = obj.receb.IE;
                        objbelinfCte.receb.xNome = obj.receb.xNome;
                        objbelinfCte.receb.fone = obj.receb.fone;

                        objbelinfCte.receb.enderReceb = new belenderReceb();

                        objbelinfCte.receb.enderReceb.xLgr = obj.receb.enderReceb.xLgr;
                        objbelinfCte.receb.enderReceb.nro = obj.receb.enderReceb.nro;
                        objbelinfCte.receb.enderReceb.xCpl = obj.receb.enderReceb.xCpl;
                        objbelinfCte.receb.enderReceb.xBairro = obj.receb.enderReceb.xBairro;
                        objbelinfCte.receb.enderReceb.cMun = obj.receb.enderReceb.cMun;
                        objbelinfCte.receb.enderReceb.xMun = obj.receb.enderReceb.xMun;
                        objbelinfCte.receb.enderReceb.CEP = obj.receb.enderReceb.CEP;
                        objbelinfCte.receb.enderReceb.UF = obj.receb.enderReceb.UF;
                        objbelinfCte.receb.enderReceb.xPais = obj.receb.enderReceb.xPais;
                        objbelinfCte.receb.enderReceb.cPais = obj.receb.enderReceb.cPais;


                    }


                    #endregion

                    #region Informacoes da NF

                    objbelinfCte.rem.infNF = new List<belinfNF>();
                    for (int j = 0; j < obj.rem.infNF.Count; j++)
                    {
                        belinfNF nf = new belinfNF();
                        nf.mod = obj.rem.infNF[j].mod;
                        nf.nDoc = obj.rem.infNF[j].nDoc;
                        nf.serie = obj.rem.infNF[j].serie;
                        nf.dEmi = obj.rem.infNF[j].dEmi;
                        nf.vBC = obj.rem.infNF[j].vBC;
                        nf.vICMS = obj.rem.infNF[j].vICMS;
                        nf.vBCST = obj.rem.infNF[j].vBCST;
                        nf.vST = obj.rem.infNF[j].vST;
                        nf.vProd = obj.rem.infNF[j].vProd;
                        nf.vNF = obj.rem.infNF[j].vNF;
                        nf.nCFOP = Convert.ToInt32(obj.rem.infNF[j].nCFOP).ToString();

                        objbelinfCte.rem.infNF.Add(nf);
                    }


                    objbelinfCte.rem.infNFe = new List<belinfNFe>();
                    for (int n = 0; n < obj.rem.infNFe.Count; n++)
                    {
                        belinfNFe nfe = new belinfNFe();
                        nfe.chave = obj.rem.infNFe[n].chave;

                        objbelinfCte.rem.infNFe.Add(nfe);
                    }

                    #endregion

                    #region Outros Documentos
                    objbelinfCte.rem.infOutros = new List<belinfOutros>();
                    for (int j = 0; j < obj.rem.infOutros.Count; j++)
                    {
                        belinfOutros infOutros = new belinfOutros();
                        infOutros.tpDoc = obj.rem.infOutros[j].tpDoc;
                        infOutros.descOutros = obj.rem.infOutros[j].descOutros;
                        infOutros.nDoc = obj.rem.infOutros[j].nDoc;
                        infOutros.dEmi = obj.rem.infOutros[j].dEmi;
                        infOutros.vDocFisc = obj.rem.infOutros[j].vDocFisc;

                        objbelinfCte.rem.infOutros.Add(infOutros);
                    }


                    #endregion

                    #region Valores

                    objbelinfCte.vPrest = new belvPrest();
                    objbelinfCte.vPrest.vTPrest = obj.vPrest.vTPrest;
                    objbelinfCte.vPrest.vRec = obj.vPrest.vRec;

                    objbelinfCte.vPrest.Comp = obj.vPrest.Comp;



                    objbelinfCte.imp = new belimp();
                    objbelinfCte.imp.ICMS = new belICMS();

                    if (obj.imp.ICMS.ICMS00 != null)
                    {
                        objbelinfCte.imp.ICMS.ICMS00 = new belICMS00();
                        objbelinfCte.imp.ICMS.ICMS00.CST = obj.imp.ICMS.ICMS00.CST;
                        objbelinfCte.imp.ICMS.ICMS00.vBC = obj.imp.ICMS.ICMS00.vBC;
                        objbelinfCte.imp.ICMS.ICMS00.pICMS = obj.imp.ICMS.ICMS00.pICMS;
                        objbelinfCte.imp.ICMS.ICMS00.vICMS = obj.imp.ICMS.ICMS00.vICMS;
                    }
                    else if (obj.imp.ICMS.ICMS20 != null)
                    {
                        objbelinfCte.imp.ICMS.ICMS20 = new belICMS20();
                        objbelinfCte.imp.ICMS.ICMS20.CST = obj.imp.ICMS.ICMS20.CST;
                        objbelinfCte.imp.ICMS.ICMS20.pRedBC = obj.imp.ICMS.ICMS20.pRedBC;
                        objbelinfCte.imp.ICMS.ICMS20.vBC = obj.imp.ICMS.ICMS20.vBC;
                        objbelinfCte.imp.ICMS.ICMS20.pICMS = obj.imp.ICMS.ICMS20.pICMS;
                        objbelinfCte.imp.ICMS.ICMS20.vICMS = obj.imp.ICMS.ICMS20.vICMS;
                    }
                    else if (obj.imp.ICMS.ICMS45 != null)
                    {
                        objbelinfCte.imp.ICMS.ICMS45 = new belICMS45();
                        objbelinfCte.imp.ICMS.ICMS45.CST = obj.imp.ICMS.ICMS45.CST;
                    }
                    else if (obj.imp.ICMS.ICMS60 != null)
                    {
                        objbelinfCte.imp.ICMS.ICMS60 = new belICMS60();
                        objbelinfCte.imp.ICMS.ICMS60.CST = obj.imp.ICMS.ICMS60.CST;
                        objbelinfCte.imp.ICMS.ICMS60.vBCSTRet = obj.imp.ICMS.ICMS60.vBCSTRet;
                        objbelinfCte.imp.ICMS.ICMS60.vICMSSTRet = obj.imp.ICMS.ICMS60.vICMSSTRet;
                        objbelinfCte.imp.ICMS.ICMS60.pICMSSTRet = obj.imp.ICMS.ICMS60.pICMSSTRet;
                        objbelinfCte.imp.ICMS.ICMS60.vCred = obj.imp.ICMS.ICMS60.vCred;
                    }
                    else if (obj.imp.ICMS.ICMS90 != null)
                    {
                        objbelinfCte.imp.ICMS.ICMS90 = new belICMS90();
                        objbelinfCte.imp.ICMS.ICMS90.CST = obj.imp.ICMS.ICMS90.CST;
                        objbelinfCte.imp.ICMS.ICMS90.pRedBC = obj.imp.ICMS.ICMS90.pRedBC;
                        objbelinfCte.imp.ICMS.ICMS90.vBC = obj.imp.ICMS.ICMS90.vBC;
                        objbelinfCte.imp.ICMS.ICMS90.pICMS = obj.imp.ICMS.ICMS90.pICMS;
                        objbelinfCte.imp.ICMS.ICMS90.vICMS = obj.imp.ICMS.ICMS90.vICMS;
                        objbelinfCte.imp.ICMS.ICMS90.vCred = obj.imp.ICMS.ICMS90.vCred;
                    }
                    else if (obj.imp.ICMS.ICMSOutraUF != null)
                    {
                        objbelinfCte.imp.ICMS.ICMSOutraUF = new belICMSOutraUF();
                        objbelinfCte.imp.ICMS.ICMSOutraUF.CST = obj.imp.ICMS.ICMSOutraUF.CST;
                        objbelinfCte.imp.ICMS.ICMSOutraUF.pRedBCOutraUF = obj.imp.ICMS.ICMSOutraUF.pRedBCOutraUF;
                        objbelinfCte.imp.ICMS.ICMSOutraUF.vBCOutraUF = obj.imp.ICMS.ICMSOutraUF.vBCOutraUF;
                        objbelinfCte.imp.ICMS.ICMSOutraUF.pICMSOutraUF = obj.imp.ICMS.ICMSOutraUF.pICMSOutraUF;
                        objbelinfCte.imp.ICMS.ICMSOutraUF.vICMSOutraUF = obj.imp.ICMS.ICMSOutraUF.vICMSOutraUF;
                    }


                    #endregion

                    #region InformacoesCarga
                    objbelinfCte.infCTeNorm = new belinfCTeNorm();
                    objbelinfCte.infCTeNorm.infCarga = new belinfCarga();

                    objbelinfCte.infCTeNorm.infCarga.vCarga = obj.infCTeNorm.infCarga.vCarga;
                    objbelinfCte.infCTeNorm.infCarga.proPred = obj.infCTeNorm.infCarga.proPred;
                    objbelinfCte.infCTeNorm.infCarga.xOutCat = obj.infCTeNorm.infCarga.xOutCat;

                    objbelinfCte.infCTeNorm.infCarga.infQ = new List<belinfQ>();
                    for (int j = 0; j < obj.infCTeNorm.infCarga.infQ.Count; j++)
                    {
                        belinfQ objInfQ = new belinfQ();
                        objInfQ.cUnid = obj.infCTeNorm.infCarga.infQ[j].cUnid;
                        objInfQ.tpMed = obj.infCTeNorm.infCarga.infQ[j].tpMed;
                        objInfQ.qCarga = obj.infCTeNorm.infCarga.infQ[j].qCarga;

                        objbelinfCte.infCTeNorm.infCarga.infQ.Add(objInfQ);
                    }

                    #endregion

                    #region Rodoviario
                    objbelinfCte.infCTeNorm.seg = new belseg();
                    objbelinfCte.infCTeNorm.rodo = new belrodo();

                    objbelinfCte.infCTeNorm.seg.respSeg = obj.infCTeNorm.seg.respSeg;
                    objbelinfCte.infCTeNorm.seg.nApol = obj.infCTeNorm.seg.nApol;
                    objbelinfCte.infCTeNorm.rodo.RNTRC = obj.infCTeNorm.rodo.RNTRC;
                    objbelinfCte.infCTeNorm.rodo.dPrev = obj.infCTeNorm.rodo.dPrev;
                    objbelinfCte.infCTeNorm.rodo.lota = obj.infCTeNorm.rodo.lota;

                    #endregion

                    #region Veiculo

                    objbelinfCte.infCTeNorm.rodo.veic = new List<belveic>();
                    for (int v = 0; v < obj.infCTeNorm.rodo.veic.Count; v++)
                    {
                        belveic veic = new belveic();

                        veic.RENAVAM = obj.infCTeNorm.rodo.veic[v].RENAVAM;
                        veic.placa = obj.infCTeNorm.rodo.veic[v].placa;
                        veic.tara = obj.infCTeNorm.rodo.veic[v].tara;
                        veic.capKG = obj.infCTeNorm.rodo.veic[v].capKG;
                        veic.capM3 = obj.infCTeNorm.rodo.veic[v].capM3;
                        veic.tpProp = obj.infCTeNorm.rodo.veic[v].tpProp;
                        veic.tpVeic = obj.infCTeNorm.rodo.veic[v].tpVeic;
                        veic.tpRod = obj.infCTeNorm.rodo.veic[v].tpRod;
                        veic.tpCar = obj.infCTeNorm.rodo.veic[v].tpCar;
                        veic.UF = obj.infCTeNorm.rodo.veic[v].UF;

                        if (obj.infCTeNorm.rodo.veic[v].prop != null)
                        {
                            veic.prop = new belprop();

                            veic.prop.CPFCNPJ = obj.infCTeNorm.rodo.veic[v].prop.CPFCNPJ;
                            veic.prop.RNTRC = obj.infCTeNorm.rodo.veic[v].prop.RNTRC;
                            veic.prop.xNome = obj.infCTeNorm.rodo.veic[v].prop.xNome;
                            veic.prop.IE = obj.infCTeNorm.rodo.veic[v].prop.IE;
                            veic.prop.UF = obj.infCTeNorm.rodo.veic[v].prop.UF;
                            veic.prop.tpProp = obj.infCTeNorm.rodo.veic[v].prop.tpProp;
                        }

                        objbelinfCte.infCTeNorm.rodo.veic.Add(veic);
                    }
                    if (obj.infCTeNorm.rodo.moto != null)
                    {
                        objbelinfCte.infCTeNorm.rodo.moto = new belmoto();
                        objbelinfCte.infCTeNorm.rodo.moto.xNome = obj.infCTeNorm.rodo.moto.xNome;
                        objbelinfCte.infCTeNorm.rodo.moto.CPF = obj.infCTeNorm.rodo.moto.CPF;
                    }
                    #endregion

                    objList.Add(objbelinfCte);

                }
                this.objObjetosAlter = new belPopulaObjetos(objObjetos.sEmp, objObjetos.objlConhec, objObjetos.cUf, objObjetos.cert);
                this.objObjetosAlter.objLinfCte = objList;
                this.objObjetosAlter.sFormEmiss = objObjetos.sFormEmiss;
                this.objObjetosAlter.sNomeArq = objObjetos.sNomeArq;
                this.objObjetosAlter.sPath = objObjetos.sPath;

            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, _sMessageException + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            }
        }
        private void PopulaForm(int index)
        {
            try
            {
                LimpaCampos(this.Controls);
                gridinfQ.Rows.Clear();
                belinfCte objInfCte = this.objObjetosAlter.objLinfCte[index];

                #region Identificacao

                txtcUF.Text = objInfCte.ide.cUF;
                txtcCT.Text = objInfCte.ide.cCT;
                txtCFOP.Text = objInfCte.ide.CFOP;
                txtnatOp.Text = objInfCte.ide.natOp;
                cboforPag.SelectedIndex = objInfCte.ide.forPag;
                txtmod.Text = objInfCte.ide.mod;
                txtserie.Text = objInfCte.ide.serie;
                txtnCT.Text = objInfCte.ide.nCT;
                if (objInfCte.ide.tpEmis != "")
                {
                    switch (objInfCte.ide.tpEmis)
                    {
                        case "1": cbotpEmis.SelectedIndex = 0;
                            break;

                        case "5": cbotpEmis.SelectedIndex = 1;
                            break;
                    }
                }
                else { cbotpEmis.SelectedIndex = -1; }
                txtcDV.Text = objInfCte.ide.cDV;
                cbotpCTe.SelectedIndex = objInfCte.ide.tpCTe;
                txtverProc.Text = objInfCte.ide.verProc;
                txtcMunEmi.Text = objInfCte.ide.cMunEnv;
                txtxMunEmi.Text = objInfCte.ide.xMunEnv;
                txtUFEmi.Text = objInfCte.ide.UFEnv;
                cbomodal.SelectedIndex = objInfCte.ide.modal != "" ? Convert.ToInt32(objInfCte.ide.modal) - 1 : -1;
                cbotpServ.SelectedIndex = objInfCte.ide.tpServ;
                txtcMunIni.Text = objInfCte.ide.cMunIni;
                txtxMunIni.Text = objInfCte.ide.xMunIni;
                txtUFIni.Text = objInfCte.ide.UFIni;
                txtcMunFim.Text = objInfCte.ide.cMunFim;
                txtxMunFim.Text = objInfCte.ide.xMunFim;
                txtUFFim.Text = objInfCte.ide.UFFim;
                cboretira.SelectedIndex = objInfCte.ide.retira;


                #endregion

                #region Compl
                if (objInfCte.compl != null)
                {
                    txtObs.Text = objInfCte.compl.ObsCont.Xtexto;
                }
                #endregion

                #region Tomador
                if (objInfCte.ide.toma03 != null)
                {
                    cbotoma.SelectedIndex = Convert.ToInt32(objInfCte.ide.toma03.toma);
                }
                if (objInfCte.ide.toma04 != null)
                {
                    cbotoma.SelectedIndex = Convert.ToInt32(objInfCte.ide.toma04.toma);
                    if (objInfCte.ide.toma04.CNPJ != "")
                    {

                        txtCNPJtoma.Text = objInfCte.ide.toma04.CNPJ.ToString();
                    }
                    else if (objInfCte.ide.toma04.CPF != "")
                    {
                        txtCNPJtoma.Text = objInfCte.ide.toma04.CPF.ToString();
                    }

                    txtIEToma.Text = objInfCte.ide.toma04.IE;
                    txtxNomeToma.Text = objInfCte.ide.toma04.xNome;
                    txtxFantToma.Text = objInfCte.ide.toma04.xFant;
                    txtfoneToma.Text = objInfCte.ide.toma04.fone.ToString();

                    txtxLgrToma.Text = objInfCte.ide.toma04.enderToma.xLgr;
                    txtnroToma.Text = objInfCte.ide.toma04.enderToma.nro;
                    txtxCplToma.Text = objInfCte.ide.toma04.enderToma.xCpl;
                    txtxBairroToma.Text = objInfCte.ide.toma04.enderToma.xBairro;
                    txtcMunToma.Text = objInfCte.ide.toma04.enderToma.cMun.ToString();
                    txtxMunToma.Text = objInfCte.ide.toma04.enderToma.xMun;
                    mskCEPToma.Text = objInfCte.ide.toma04.enderToma.CEP.ToString();
                    txtUFToma.Text = objInfCte.ide.toma04.enderToma.UF;
                    txtcPaisToma.Text = objInfCte.ide.toma04.enderToma.cPais.ToString();
                    txtxPaisToma.Text = objInfCte.ide.toma04.enderToma.xPais;

                }
                HabilitaCamposToma();
                #endregion

                #region Emitente


                txtCNPJEmit.Text = objInfCte.emit.CNPJ;
                txtIEEmit.Text = objInfCte.emit.IE;
                txtxNomeEmit.Text = objInfCte.emit.xNome;
                txtxFantEmit.Text = objInfCte.emit.xFant;

                txtxLgrEmit.Text = objInfCte.emit.enderEmit.xLgr;
                txtnroEmit.Text = objInfCte.emit.enderEmit.nro;
                txtxCplEmit.Text = objInfCte.emit.enderEmit.xCpl;
                txtxBairroEmit.Text = objInfCte.emit.enderEmit.xBairro;
                txtcMunEmit.Text = objInfCte.emit.enderEmit.cMun.ToString();
                txtxMunEmit.Text = objInfCte.emit.enderEmit.xMun;
                mskCEPEmit.Text = objInfCte.emit.enderEmit.CEP;
                txtUFEmit.Text = objInfCte.emit.enderEmit.UF;
                txtfoneEmit.Text = objInfCte.emit.enderEmit.fone.ToString();

                #endregion

                #region Remetente

                if (objInfCte.rem.CNPJ != "")
                {
                    txtCNPJrem.Text = objInfCte.rem.CNPJ;
                }
                else if (objInfCte.rem.CPF != "")
                {
                    txtCNPJrem.Text = objInfCte.rem.CPF;
                }

                txtIErem.Text = objInfCte.rem.IE;
                txtxNomerem.Text = objInfCte.rem.xNome;
                txtxFantrem.Text = objInfCte.rem.xFant;
                txtfonerem.Text = objInfCte.rem.fone;


                txtxLgrrem.Text = objInfCte.rem.enderReme.xLgr;
                txtnrorem.Text = objInfCte.rem.enderReme.nro;
                txtxCplrem.Text = objInfCte.rem.enderReme.xCpl;
                txtxBairrorem.Text = objInfCte.rem.enderReme.xBairro;
                txtcMunrem.Text = objInfCte.rem.enderReme.cMun;
                txtxMunrem.Text = objInfCte.rem.enderReme.xMun;
                mskCEPrem.Text = objInfCte.rem.enderReme.CEP;
                txtUFrem.Text = objInfCte.rem.enderReme.UF;
                xPaisrem.Text = objInfCte.rem.enderReme.xPais;
                txtcPaisrem.Text = objInfCte.rem.enderReme.cPais;

                #endregion

                #region Destinatario

                if (objInfCte.dest.CNPJ != "")
                {
                    txtCNPJdest.Text = objInfCte.dest.CNPJ;
                }
                else if (objInfCte.dest.CPF != "")
                {
                    txtCNPJdest.Text = objInfCte.dest.CPF;
                }

                txtIEdest.Text = objInfCte.dest.IE;
                txtxNomedest.Text = objInfCte.dest.xNome;
                txtfonedest.Text = objInfCte.dest.fone;
                txtISUFdest.Text = objInfCte.dest.ISUF;



                txtxLgrdest.Text = objInfCte.dest.enderDest.xLgr;
                txtnrodest.Text = objInfCte.dest.enderDest.nro;
                txtxCpldest.Text = objInfCte.dest.enderDest.xCpl;
                txtxBairrodest.Text = objInfCte.dest.enderDest.xBairro;
                txtcMundest.Text = objInfCte.dest.enderDest.cMun;
                txtxMundest.Text = objInfCte.dest.enderDest.xMun;
                mskCEPdest.Text = objInfCte.dest.enderDest.CEP;
                txtUFdest.Text = objInfCte.dest.enderDest.UF;
                txtxPaisdest.Text = objInfCte.dest.enderDest.xPais;
                txtcPaisdest.Text = objInfCte.dest.enderDest.cPais;

                #endregion

                #region Expedidor
                if (objInfCte.exped != null)
                {

                    if (objInfCte.exped.CNPJ != "")
                    {
                        txtCNPJexped.Text = objInfCte.exped.CNPJ;
                    }
                    else if (objInfCte.exped.CPF != "")
                    {
                        txtCNPJexped.Text = objInfCte.exped.CPF;
                    }

                    txtIEexped.Text = objInfCte.exped.IE;
                    txtxNomeexped.Text = objInfCte.exped.xNome;
                    txtfoneexped.Text = objInfCte.exped.fone;


                    txtxLgrexped.Text = objInfCte.exped.enderExped.xLgr;
                    txtnroexped.Text = objInfCte.exped.enderExped.nro;
                    txtxBairroexped.Text = objInfCte.exped.enderExped.xBairro;
                    txtxCplexped.Text = objInfCte.exped.enderExped.xCpl;
                    txtcMunexped.Text = objInfCte.exped.enderExped.cMun;
                    txtxMunexped.Text = objInfCte.exped.enderExped.xMun;
                    mskCEPexped.Text = objInfCte.exped.enderExped.CEP;
                    txtUFexped.Text = objInfCte.exped.enderExped.UF;
                    txtxPaisexped.Text = objInfCte.exped.enderExped.xPais;
                    txtcPaisexped.Text = objInfCte.exped.enderExped.cPais;


                }


                #endregion

                #region Recebedor
                if (objInfCte.receb != null)
                {

                    if (objInfCte.receb.CNPJ != "")
                    {
                        txtCpfCnpfreceb.Text = objInfCte.receb.CNPJ;
                    }
                    else if (objInfCte.exped.CPF != "")
                    {
                        txtCpfCnpfreceb.Text = objInfCte.receb.CPF;
                    }

                    txtIEreceb.Text = objInfCte.receb.IE;
                    txtxNomereceb.Text = objInfCte.receb.xNome;
                    txtfonereceb.Text = objInfCte.receb.fone;


                    txtxLgrreceb.Text = objInfCte.receb.enderReceb.xLgr;
                    txtnroreceb.Text = objInfCte.receb.enderReceb.nro;
                    txtxCplreceb.Text = objInfCte.receb.enderReceb.xCpl;
                    txtxBairroreceb.Text = objInfCte.receb.enderReceb.xBairro;
                    txtcMunreceb.Text = objInfCte.receb.enderReceb.cMun;
                    txtxMunreceb.Text = objInfCte.receb.enderReceb.xMun;
                    mskCEPreceb.Text = objInfCte.receb.enderReceb.CEP;
                    txtUFreceb.Text = objInfCte.receb.enderReceb.UF;
                    txtxPaisreceb.Text = objInfCte.receb.enderReceb.xPais;
                    txtcPaisreceb.Text = objInfCte.receb.enderReceb.cPais;


                }


                #endregion

                #region Informacoes da NF

                gridNfNormal.Rows.Clear();
                for (int j = 0; j < objInfCte.rem.infNF.Count; j++)
                {
                    gridNfNormal.Rows.Add();

                    switch (objInfCte.rem.infNF[j].mod)
                    {
                        case "01": gridNfNormal.Rows[j].Cells["mod"].Value = mod.Items[0];
                            break;
                        case "04": gridNfNormal.Rows[j].Cells["mod"].Value = mod.Items[1];
                            break;
                    }
                    gridNfNormal.Rows[j].Cells["nDoc"].Value = objInfCte.rem.infNF[j].nDoc;
                    gridNfNormal.Rows[j].Cells["serie"].Value = objInfCte.rem.infNF[j].serie;
                    gridNfNormal.Rows[j].Cells["dEmi"].Value = Convert.ToDateTime(objInfCte.rem.infNF[j].dEmi);
                    gridNfNormal.Rows[j].Cells["vBC"].Value = Convert.ToDecimal(objInfCte.rem.infNF[j].vBC.Replace(".", ","));
                    gridNfNormal.Rows[j].Cells["vICMS"].Value = Convert.ToDecimal(objInfCte.rem.infNF[j].vICMS.Replace(".", ","));
                    gridNfNormal.Rows[j].Cells["vBCST"].Value = Convert.ToDecimal(objInfCte.rem.infNF[j].vBCST.Replace(".", ","));
                    gridNfNormal.Rows[j].Cells["vST"].Value = Convert.ToDecimal(objInfCte.rem.infNF[j].vST.Replace(".", ","));
                    gridNfNormal.Rows[j].Cells["vProd"].Value = Convert.ToDecimal(objInfCte.rem.infNF[j].vProd.Replace(".", ","));
                    gridNfNormal.Rows[j].Cells["vNF"].Value = Convert.ToDecimal(objInfCte.rem.infNF[j].vNF.Replace(".", ","));
                    gridNfNormal.Rows[j].Cells["nCFOP"].Value = Convert.ToInt32(objInfCte.rem.infNF[j].nCFOP.Replace(".", ","));

                }

                gridNfe.Rows.Clear();
                for (int n = 0; n < objInfCte.rem.infNFe.Count; n++)
                {
                    gridNfe.Rows.Add();
                    gridNfe.Rows[n].Cells[0].Value = objInfCte.rem.infNFe[n].chave;
                }


                #endregion

                #region Outros Documentos
                gridDocumentos.Rows.Clear();
                for (int j = 0; j < objInfCte.rem.infOutros.Count; j++)
                {
                    gridDocumentos.Rows.Add();
                    switch (objInfCte.rem.infOutros[j].tpDoc)
                    {
                        case "00":
                            gridDocumentos.Rows[j].Cells["tpDoc"].Value = tpDoc.Items[0];
                            break;
                        case "10":
                            gridDocumentos.Rows[j].Cells["tpDoc"].Value = tpDoc.Items[1];
                            break;
                        case "99":
                            gridDocumentos.Rows[j].Cells["tpDoc"].Value = tpDoc.Items[2];
                            break;
                    }
                    gridDocumentos.Rows[j].Cells["descOutros"].Value = objInfCte.rem.infOutros[j].descOutros;
                    gridDocumentos.Rows[j].Cells["nDoc_"].Value = objInfCte.rem.infOutros[j].nDoc;
                    gridDocumentos.Rows[j].Cells["dEmi_"].Value = Convert.ToDateTime(objInfCte.rem.infOutros[j].dEmi);
                    gridDocumentos.Rows[j].Cells["vDocFisc"].Value = Convert.ToDecimal(objInfCte.rem.infOutros[j].vDocFisc.Replace(".", ","));

                }


                #endregion

                #region Valores

                nudvTPrest.Value = Convert.ToDecimal(objInfCte.vPrest.vTPrest.Replace(".", ","));
                nudvRec.Value = Convert.ToDecimal(objInfCte.vPrest.vRec.Replace(".", ","));

                if (objInfCte.vPrest.Comp != null)
                {
                    for (int i = 0; i < objInfCte.vPrest.Comp.Count; i++)
                    {
                        switch (objInfCte.vPrest.Comp[i].xNome)
                        {
                            case "FRETE VALOR":
                                nudvFrete.Value = Convert.ToDecimal(objInfCte.vPrest.Comp[i].vComp.Replace(".", ","));
                                break;

                            case "FRETE CUBAGEM":
                                nudFreteCubagem.Value = Convert.ToDecimal(objInfCte.vPrest.Comp[i].vComp.Replace(".", ","));
                                break;

                            case "FRETE PESO":
                                nudFretePeso.Value = Convert.ToDecimal(objInfCte.vPrest.Comp[i].vComp.Replace(".", ","));
                                break;

                            case "CAT":
                                nudCat.Value = Convert.ToDecimal(objInfCte.vPrest.Comp[i].vComp.Replace(".", ","));
                                break;

                            case "DESPACHO":
                                nudDespacho.Value = Convert.ToDecimal(objInfCte.vPrest.Comp[i].vComp.Replace(".", ","));
                                break;

                            case "PEDAGIO":
                                nudPedagio.Value = Convert.ToDecimal(objInfCte.vPrest.Comp[i].vComp.Replace(".", ","));
                                break;

                            case "OUTROS":
                                nudOutros.Value = Convert.ToDecimal(objInfCte.vPrest.Comp[i].vComp.Replace(".", ","));
                                break;

                            case "ADME":
                                nudAdme.Value = Convert.ToDecimal(objInfCte.vPrest.Comp[i].vComp.Replace(".", ","));
                                break;

                            case "ENTREGA":
                                nudEntrega.Value = Convert.ToDecimal(objInfCte.vPrest.Comp[i].vComp.Replace(".", ","));
                                break;

                        }
                    }
                }


                if (objInfCte.imp.ICMS.ICMS00 != null)
                {
                    cboCST.SelectedIndex = 0;
                    nudvBC.Value = Convert.ToDecimal(objInfCte.imp.ICMS.ICMS00.vBC.Replace(".", ","));
                    nudpICMS.Value = Convert.ToDecimal(objInfCte.imp.ICMS.ICMS00.pICMS.Replace(".", ","));
                    nudvICMS.Value = Convert.ToDecimal(objInfCte.imp.ICMS.ICMS00.vICMS.Replace(".", ","));

                    HabilitaCamposValores(0);
                }
                else if (objInfCte.imp.ICMS.ICMS20 != null)
                {
                    cboCST.SelectedIndex = 1;
                    nudpRedBC.Value = Convert.ToDecimal(objInfCte.imp.ICMS.ICMS20.pRedBC.Replace(".", ","));
                    nudvBC.Value = Convert.ToDecimal(objInfCte.imp.ICMS.ICMS20.vBC.Replace(".", ","));
                    nudpICMS.Value = Convert.ToDecimal(objInfCte.imp.ICMS.ICMS20.pICMS.Replace(".", ","));
                    nudvICMS.Value = Convert.ToDecimal(objInfCte.imp.ICMS.ICMS20.vICMS.Replace(".", ","));


                    HabilitaCamposValores(1);
                }
                else if (objInfCte.imp.ICMS.ICMS45 != null)
                {
                    switch (objInfCte.imp.ICMS.ICMS45.CST)
                    {
                        case "40": cboCST.SelectedIndex = 2;
                            HabilitaCamposValores(2);
                            break;

                        case "41": cboCST.SelectedIndex = 3;
                            HabilitaCamposValores(3);
                            break;

                        case "51": cboCST.SelectedIndex = 4;
                            HabilitaCamposValores(4);
                            break;
                    }



                }
                else if (objInfCte.imp.ICMS.ICMS60 != null)
                {
                    cboCST.SelectedIndex = 5;
                    nudvBC.Value = Convert.ToDecimal(objInfCte.imp.ICMS.ICMS60.vBCSTRet.Replace(".", ","));
                    nudvICMS.Value = Convert.ToDecimal(objInfCte.imp.ICMS.ICMS60.vICMSSTRet.Replace(".", ","));
                    nudpICMS.Value = Convert.ToDecimal(objInfCte.imp.ICMS.ICMS60.pICMSSTRet.Replace(".", ","));

                    HabilitaCamposValores(5);
                }
                else if (objInfCte.imp.ICMS.ICMS90 != null)
                {
                    cboCST.SelectedIndex = 6;
                    nudpRedBC.Value = Convert.ToDecimal(objInfCte.imp.ICMS.ICMS90.pRedBC.Replace(".", ","));
                    nudvBC.Value = Convert.ToDecimal(objInfCte.imp.ICMS.ICMS90.vBC.Replace(".", ","));
                    nudpICMS.Value = Convert.ToDecimal(objInfCte.imp.ICMS.ICMS90.pICMS.Replace(".", ","));
                    nudvICMS.Value = Convert.ToDecimal(objInfCte.imp.ICMS.ICMS90.vICMS.Replace(".", ","));

                    HabilitaCamposValores(6);
                }
                else if (objInfCte.imp.ICMS.ICMSOutraUF != null)
                {
                    cboCST.SelectedIndex = 7;
                    nudpRedBC.Value = Convert.ToDecimal(objInfCte.imp.ICMS.ICMSOutraUF.pRedBCOutraUF.Replace(".", ","));
                    nudvBC.Value = Convert.ToDecimal(objInfCte.imp.ICMS.ICMSOutraUF.vBCOutraUF.Replace(".", ","));
                    nudpICMS.Value = Convert.ToDecimal(objInfCte.imp.ICMS.ICMSOutraUF.pICMSOutraUF.Replace(".", ","));
                    nudvICMS.Value = Convert.ToDecimal(objInfCte.imp.ICMS.ICMSOutraUF.vICMSOutraUF.Replace(".", ","));

                    HabilitaCamposValores(7);
                }

                #endregion

                #region InformacoesCarga

                nudvMerc.Value = objInfCte.infCTeNorm.infCarga.vCarga;
                txtproPred.Text = objInfCte.infCTeNorm.infCarga.proPred;
                txtxOutCat.Text = objInfCte.infCTeNorm.infCarga.xOutCat;

                gridinfQ.Rows.Clear();
                for (int j = 0; j < objInfCte.infCTeNorm.infCarga.infQ.Count; j++)
                {
                    gridinfQ.Rows.Add();
                    switch (objInfCte.infCTeNorm.infCarga.infQ[j].cUnid)
                    {
                        case "00": gridinfQ.Rows[j].Cells[0].Value = cUnid.Items[0];
                            break;
                        case "01": gridinfQ.Rows[j].Cells[0].Value = cUnid.Items[1];
                            break;
                        case "02": gridinfQ.Rows[j].Cells[0].Value = cUnid.Items[2];
                            break;
                        case "03": gridinfQ.Rows[j].Cells[0].Value = cUnid.Items[3];
                            break;
                        case "04": gridinfQ.Rows[j].Cells[0].Value = cUnid.Items[4];
                            break;
                    }
                    gridinfQ.Rows[j].Cells[1].Value = objInfCte.infCTeNorm.infCarga.infQ[j].tpMed;
                    gridinfQ.Rows[j].Cells[2].Value = objInfCte.infCTeNorm.infCarga.infQ[j].qCarga;
                }

                #endregion

                #region Rodoviario

                cborespSeg.SelectedIndex = objInfCte.infCTeNorm.seg.respSeg != "" ? Convert.ToInt32(objInfCte.infCTeNorm.seg.respSeg) : -1;
                txtnApol.Text = objInfCte.infCTeNorm.seg.nApol;
                txtRNTRC.Text = objInfCte.infCTeNorm.rodo.RNTRC;
                mskdPrev.Text = objInfCte.infCTeNorm.rodo.dPrev != "" ? Convert.ToDateTime(objInfCte.infCTeNorm.rodo.dPrev).ToString("dd-MM-yyyy") : "";
                if (objInfCte.infCTeNorm.rodo.lota != "")
                {
                    cbolota.SelectedIndex = objInfCte.infCTeNorm.rodo.lota != "" ? Convert.ToInt32(objInfCte.infCTeNorm.rodo.lota) : -1;
                }
                #endregion

                #region Veiculo

                lblTotalVeiculo.Text = "0 de " + objInfCte.infCTeNorm.rodo.veic.Count().ToString();
                bsVeiculos.DataSource = objInfCte.infCTeNorm.rodo.veic;
                if (objInfCte.infCTeNorm.rodo.veic.Count() > 0)
                {
                    lblTotalVeiculo.Text = "1 de " + objInfCte.infCTeNorm.rodo.veic.Count().ToString();
                    txtRENAVAM.Text = objInfCte.infCTeNorm.rodo.veic[0].RENAVAM;
                    txtplaca.Text = objInfCte.infCTeNorm.rodo.veic[0].placa;
                    nudtara.Value = Convert.ToInt32(objInfCte.infCTeNorm.rodo.veic[0].tara);
                    nudcapKG.Value = Convert.ToInt32(objInfCte.infCTeNorm.rodo.veic[0].capKG);
                    nudcapM3.Value = Convert.ToInt32(objInfCte.infCTeNorm.rodo.veic[0].capM3);
                    switch (objInfCte.infCTeNorm.rodo.veic[0].tpProp)
                    {
                        case "P":
                            cbotpProp.SelectedIndex = 0;
                            break;
                        case "T":
                            cbotpProp.SelectedIndex = 1;
                            break;
                        default:
                            cbotpProp.SelectedIndex = -1;
                            break;
                    }
                    cbotpVeic.SelectedIndex = objInfCte.infCTeNorm.rodo.veic[0].tpVeic != "" ? Convert.ToInt32(objInfCte.infCTeNorm.rodo.veic[0].tpVeic) : -1;
                    cbotpRod.SelectedIndex = objInfCte.infCTeNorm.rodo.veic[0].tpRod != "" ? Convert.ToInt32(objInfCte.infCTeNorm.rodo.veic[0].tpRod) : -1;
                    cbotpCar.SelectedIndex = objInfCte.infCTeNorm.rodo.veic[0].tpCar != "" ? Convert.ToInt32(objInfCte.infCTeNorm.rodo.veic[0].tpCar) : -1;
                    txtUF.Text = objInfCte.infCTeNorm.rodo.veic[0].UF;

                    if (objInfCte.infCTeNorm.rodo.veic[0].prop != null)
                    {
                        txtCPFprop.Text = objInfCte.infCTeNorm.rodo.veic[0].prop.CPFCNPJ;
                        txtRNTRCprop.Text = objInfCte.infCTeNorm.rodo.veic[0].prop.RNTRC;
                        txtxNomeprop.Text = objInfCte.infCTeNorm.rodo.veic[0].prop.xNome;
                        txtIEprop.Text = objInfCte.infCTeNorm.rodo.veic[0].prop.IE;
                        txtUFprop.Text = objInfCte.infCTeNorm.rodo.veic[0].prop.UF;
                        cbotpPropprop.SelectedIndex = Convert.ToInt32(objInfCte.infCTeNorm.rodo.veic[0].prop.tpProp);
                    }

                }
                if (objInfCte.infCTeNorm.rodo.moto != null)
                {
                    txtxNomemoto.Text = objInfCte.infCTeNorm.rodo.moto.xNome;
                    txtCPFmoto.Text = objInfCte.infCTeNorm.rodo.moto.CPF;
                }
                #endregion

                lblNumCte.Text = "Número CT-e: " + objInfCte.ide.nCT;
                VerificaCampos(index);

            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, _sMessageException + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            }
        }
        private void SalvarAlteracao(int index)
        {
            try
            {
                belinfCte objInfCte = this.objObjetosAlter.objLinfCte[index];

                #region Identificacao
                objInfCte.ide.cUF = txtcUF.Text;
                objInfCte.ide.cCT = txtcCT.Text;
                objInfCte.ide.CFOP = txtCFOP.Text;
                objInfCte.ide.natOp = txtnatOp.Text;
                objInfCte.ide.forPag = cboforPag.SelectedIndex;
                objInfCte.ide.mod = txtmod.Text;
                objInfCte.ide.serie = txtserie.Text;
                objInfCte.ide.nCT = txtnCT.Text;
                switch (cbotpEmis.SelectedIndex)
                {
                    case 0: objInfCte.ide.tpEmis = "1";
                        break;
                    case 1: objInfCte.ide.tpEmis = "5";
                        break;

                    case 2: objInfCte.ide.tpEmis = "7";
                        break;

                    case 3: objInfCte.ide.tpEmis = "8";
                        break;

                    default: objInfCte.ide.tpEmis = "";
                        break;
                }

                objInfCte.ide.cDV = txtcDV.Text;
                objInfCte.ide.tpCTe = cbotpCTe.SelectedIndex;
                objInfCte.ide.verProc = txtverProc.Text;
                objInfCte.ide.cMunEnv = txtcMunEmi.Text;
                objInfCte.ide.xMunEnv = txtxMunEmi.Text;
                objInfCte.ide.UFEnv = txtUFEmi.Text;
                objInfCte.ide.modal = "0" + (cbomodal.SelectedIndex + 1).ToString();
                objInfCte.ide.tpServ = cbotpServ.SelectedIndex;
                objInfCte.ide.cMunIni = txtcMunIni.Text;
                objInfCte.ide.xMunIni = txtxMunIni.Text;
                objInfCte.ide.UFIni = txtUFIni.Text;
                objInfCte.ide.cMunFim = txtcMunFim.Text;
                objInfCte.ide.xMunFim = txtxMunFim.Text;
                objInfCte.ide.UFFim = txtUFFim.Text;
                objInfCte.ide.retira = cboretira.SelectedIndex;


                #endregion

                #region Compl
                if (txtObs.Text != "")
                {
                    belcompl objcompl = new belcompl();
                    objcompl.ObsCont.Xtexto = txtObs.Text;
                    objcompl.ObsCont.Xcampo = "OBSERVACAO";
                    objInfCte.compl = objcompl;
                }

                #endregion

                #region Tomador
                objInfCte.ide.toma03 = null;

                objInfCte.ide.toma04 = null;

                if (cbotoma.SelectedIndex != -1)
                {
                    if (cbotoma.SelectedIndex != 4)
                    {
                        objInfCte.ide.toma03 = new beltoma03();
                        objInfCte.ide.toma03.toma = cbotoma.SelectedIndex.ToString();
                    }
                    else
                    {
                        objInfCte.ide.toma04 = new beltoma04();
                        objInfCte.ide.toma04.enderToma = new belenderToma();
                        objInfCte.ide.toma04.toma = cbotoma.SelectedIndex.ToString();

                        string sCnpj = txtCNPJtoma.Text;
                        if (sCnpj != "")
                        {
                            if (txtCNPJtoma.Text.Length == 14)
                            {
                                objInfCte.ide.toma04.CNPJ = sCnpj;
                            }
                            else if (txtCNPJtoma.Text.Length == 11)
                            {
                                objInfCte.ide.toma04.CPF = sCnpj;
                            }
                        }
                        objInfCte.ide.toma04.IE = txtIEToma.Text;
                        objInfCte.ide.toma04.xNome = txtxNomeToma.Text;
                        objInfCte.ide.toma04.xFant = txtxFantToma.Text;
                        if (txtfoneToma.Text != "")
                        {
                            objInfCte.ide.toma04.fone = txtfoneToma.Text;
                        }
                        objInfCte.ide.toma04.enderToma.xLgr = txtxLgrToma.Text;
                        objInfCte.ide.toma04.enderToma.nro = txtnroToma.Text;
                        objInfCte.ide.toma04.enderToma.xCpl = txtxCplToma.Text;
                        objInfCte.ide.toma04.enderToma.xBairro = txtxBairroToma.Text;
                        if (txtcMunToma.Text != "")
                        {
                            objInfCte.ide.toma04.enderToma.cMun = txtcMunToma.Text;
                        }
                        objInfCte.ide.toma04.enderToma.xMun = txtxMunToma.Text;

                        string sCep = mskCEPToma.Text.Replace(" ", "").Replace("-", "");
                        objInfCte.ide.toma04.enderToma.CEP = sCep;

                        objInfCte.ide.toma04.enderToma.UF = txtUFToma.Text;
                        if (txtcPaisToma.Text != "")
                        {
                            objInfCte.ide.toma04.enderToma.cPais = txtcPaisToma.Text;
                        }
                        objInfCte.ide.toma04.enderToma.xPais = txtxPaisToma.Text;

                    }
                }
                #endregion

                #region Emitente

                objInfCte.emit.CNPJ = txtCNPJEmit.Text.Replace(" ", "").Replace("-", "").Replace("/", "").Replace(".", ""); ;
                objInfCte.emit.IE = txtIEEmit.Text;
                objInfCte.emit.xNome = txtxNomeEmit.Text;
                objInfCte.emit.xFant = txtxFantEmit.Text;

                objInfCte.emit.enderEmit.xLgr = txtxLgrEmit.Text;
                objInfCte.emit.enderEmit.nro = txtnroEmit.Text;
                objInfCte.emit.enderEmit.xCpl = txtxCplEmit.Text;
                objInfCte.emit.enderEmit.xBairro = txtxBairroEmit.Text;
                if (txtcMunEmit.Text != "")
                {
                    objInfCte.emit.enderEmit.cMun = txtcMunEmit.Text;
                }
                objInfCte.emit.enderEmit.xMun = txtxMunEmit.Text;

                string sCepemit = mskCEPEmit.Text.Replace(" ", "").Replace("-", "");
                if (sCepemit != "")
                {
                    objInfCte.emit.enderEmit.CEP = sCepemit;
                }
                objInfCte.emit.enderEmit.UF = txtUFEmit.Text;
                if (txtfoneEmit.Text != "")
                {
                    objInfCte.emit.enderEmit.fone = txtfoneEmit.Text;
                }

                #endregion

                #region Remetente
                objInfCte.rem = new belrem();
                objInfCte.rem.enderReme = new belenderReme();

                if (objInfCte.rem.CNPJ != "")
                {
                    if (txtCNPJrem.Text.Length == 14)
                    {
                        objInfCte.rem.CNPJ = txtCNPJrem.Text;
                    }
                }
                else if (objInfCte.rem.CPF != "")
                {
                    if (txtCNPJrem.Text.Length == 11)
                    {
                        objInfCte.rem.CPF = txtCNPJrem.Text;
                    }
                }
                else
                {
                    if (txtCNPJrem.Text.Length == 14)
                    {
                        objInfCte.rem.CNPJ = txtCNPJrem.Text;
                    }
                    else if (txtCNPJrem.Text.Length == 11)
                    {
                        objInfCte.rem.CPF = txtCNPJrem.Text;
                    }
                }
                objInfCte.rem.IE = txtIErem.Text;
                objInfCte.rem.xNome = txtxNomerem.Text;
                objInfCte.rem.xFant = txtxFantrem.Text;
                objInfCte.rem.fone = txtfonerem.Text;


                objInfCte.rem.enderReme.xLgr = txtxLgrrem.Text;
                objInfCte.rem.enderReme.nro = txtnrorem.Text;
                objInfCte.rem.enderReme.xCpl = txtxCplrem.Text;
                objInfCte.rem.enderReme.xBairro = txtxBairrorem.Text;
                objInfCte.rem.enderReme.cMun = txtcMunrem.Text;
                objInfCte.rem.enderReme.xMun = txtxMunrem.Text;
                objInfCte.rem.enderReme.CEP = mskCEPrem.Text;
                objInfCte.rem.enderReme.UF = txtUFrem.Text;
                objInfCte.rem.enderReme.xPais = xPaisrem.Text;
                objInfCte.rem.enderReme.cPais = txtcPaisrem.Text;

                #endregion

                #region Destinatario

                objInfCte.dest = new beldest();
                objInfCte.dest.enderDest = new belenderDest();

                if (objInfCte.dest.CNPJ != "")
                {
                    if (txtCNPJdest.Text.Length == 14)
                    {
                        objInfCte.dest.CNPJ = txtCNPJdest.Text;
                    }
                }
                else if (objInfCte.dest.CPF != "")
                {
                    if (txtCNPJdest.Text.Length == 11)
                    {
                        objInfCte.dest.CPF = txtCNPJdest.Text;
                    }
                }
                else
                {
                    if (txtCNPJdest.Text.Length == 14)
                    {
                        objInfCte.dest.CNPJ = txtCNPJdest.Text;
                    }
                    else if (txtCNPJdest.Text.Length == 11)
                    {
                        objInfCte.dest.CPF = txtCNPJdest.Text;
                    }
                }

                objInfCte.dest.IE = txtIEdest.Text;
                objInfCte.dest.xNome = txtxNomedest.Text;
                objInfCte.dest.fone = txtfonedest.Text;
                objInfCte.dest.ISUF = txtISUFdest.Text;


                objInfCte.dest.enderDest.xLgr = txtxLgrdest.Text;
                objInfCte.dest.enderDest.nro = txtnrodest.Text;
                objInfCte.dest.enderDest.xCpl = txtxCpldest.Text;
                objInfCte.dest.enderDest.xBairro = txtxBairrodest.Text;
                objInfCte.dest.enderDest.cMun = txtcMundest.Text;
                objInfCte.dest.enderDest.xMun = txtxMundest.Text;
                objInfCte.dest.enderDest.CEP = mskCEPdest.Text;
                objInfCte.dest.enderDest.UF = txtUFdest.Text;
                objInfCte.dest.enderDest.xPais = txtxPaisdest.Text;
                objInfCte.dest.enderDest.cPais = txtcPaisdest.Text;

                #endregion

                #region Expedidor
                if (objInfCte.exped != null)
                {
                    if (objInfCte.exped.CNPJ != "")
                    {
                        if (txtCNPJexped.Text.Length == 14)
                        {
                            objInfCte.exped.CNPJ = txtCNPJexped.Text;
                        }
                    }
                    else if (objInfCte.exped.CPF != "")
                    {
                        if (txtCNPJexped.Text.Length == 11)
                        {
                            objInfCte.exped.CPF = txtCNPJexped.Text;
                        }
                    }
                    else
                    {
                        if (txtCNPJexped.Text.Length == 14)
                        {
                            objInfCte.exped.CNPJ = txtCNPJexped.Text;
                        }
                        else if (txtCNPJexped.Text.Length == 11)
                        {
                            objInfCte.exped.CPF = txtCNPJexped.Text;
                        }
                    }

                    objInfCte.exped.IE = txtIEexped.Text;
                    objInfCte.exped.xNome = txtxNomeexped.Text;
                    objInfCte.exped.fone = txtfoneexped.Text;


                    objInfCte.exped.enderExped.xLgr = txtxLgrexped.Text;
                    objInfCte.exped.enderExped.nro = txtnroexped.Text;
                    objInfCte.exped.enderExped.xBairro = txtxBairroexped.Text;
                    objInfCte.exped.enderExped.xCpl = txtxCplexped.Text;
                    objInfCte.exped.enderExped.cMun = txtcMunexped.Text;
                    objInfCte.exped.enderExped.xMun = txtxMunexped.Text;
                    objInfCte.exped.enderExped.CEP = mskCEPexped.Text;
                    objInfCte.exped.enderExped.UF = txtUFexped.Text;
                    objInfCte.exped.enderExped.xPais = txtxPaisexped.Text;
                    objInfCte.exped.enderExped.cPais = txtcPaisexped.Text;

                }


                #endregion

                #region Recebedor
                if (objInfCte.receb != null)
                {
                    if (objInfCte.receb.CNPJ != "")
                    {
                        if (txtCpfCnpfreceb.Text.Length == 14)
                        {
                            objInfCte.receb.CNPJ = txtCpfCnpfreceb.Text;
                        }
                    }
                    else if (objInfCte.receb.CPF != "")
                    {
                        if (txtCpfCnpfreceb.Text.Length == 11)
                        {
                            objInfCte.receb.CPF = txtCpfCnpfreceb.Text;
                        }
                    }
                    else
                    {
                        if (txtCpfCnpfreceb.Text.Length == 14)
                        {
                            objInfCte.receb.CNPJ = txtCpfCnpfreceb.Text;
                        }
                        else if (txtCpfCnpfreceb.Text.Length == 11)
                        {
                            objInfCte.receb.CPF = txtCpfCnpfreceb.Text;
                        }
                    }

                    objInfCte.receb.IE = txtIEreceb.Text;
                    objInfCte.receb.xNome = txtxNomereceb.Text;
                    objInfCte.receb.fone = txtfonereceb.Text;

                    objInfCte.receb.enderReceb.xLgr = txtxLgrreceb.Text;
                    objInfCte.receb.enderReceb.nro = txtnroreceb.Text;
                    objInfCte.receb.enderReceb.xBairro = txtxBairroreceb.Text;
                    objInfCte.receb.enderReceb.xCpl = txtxCplreceb.Text;
                    objInfCte.receb.enderReceb.cMun = txtcMunreceb.Text;
                    objInfCte.receb.enderReceb.xMun = txtxMunreceb.Text;
                    objInfCte.receb.enderReceb.CEP = mskCEPreceb.Text;
                    objInfCte.receb.enderReceb.UF = txtUFreceb.Text;
                    objInfCte.receb.enderReceb.xPais = txtxPaisreceb.Text;
                    objInfCte.receb.enderReceb.cPais = txtcPaisreceb.Text;

                }


                #endregion

                #region Informacoes da NF

                objInfCte.rem.infNF = new List<belinfNF>();
                for (int j = 0; j < gridNfNormal.RowCount; j++)
                {
                    belinfNF nf = new belinfNF();

                    switch (gridNfNormal.Rows[j].Cells["mod"].Value.ToString())
                    {
                        case "01 - NF Modelo 01/1A e Avulsa": nf.mod = "01";
                            break;
                        case "04 - NF de Produtor": nf.mod = "04";
                            break;
                    }
                    nf.nDoc = gridNfNormal.Rows[j].Cells["nDoc"].Value == null ? "" : gridNfNormal.Rows[j].Cells["nDoc"].Value.ToString();
                    nf.serie = gridNfNormal.Rows[j].Cells["serie"].Value == null ? "" : gridNfNormal.Rows[j].Cells["serie"].Value.ToString();
                    nf.dEmi = gridNfNormal.Rows[j].Cells["dEmi"].Value == null ? "" : gridNfNormal.Rows[j].Cells["dEmi"].Value.ToString();
                    nf.vBC = gridNfNormal.Rows[j].Cells["vBC"].Value == null ? "" : gridNfNormal.Rows[j].Cells["vBC"].Value.ToString().Replace(",", ".");
                    nf.vICMS = gridNfNormal.Rows[j].Cells["vICMS"].Value == null ? "" : gridNfNormal.Rows[j].Cells["vICMS"].Value.ToString().Replace(",", ".");
                    nf.vBCST = gridNfNormal.Rows[j].Cells["vBCST"].Value == null ? "" : gridNfNormal.Rows[j].Cells["vBCST"].Value.ToString().Replace(",", ".");
                    nf.vST = gridNfNormal.Rows[j].Cells["vST"].Value == null ? "" : gridNfNormal.Rows[j].Cells["vST"].Value.ToString().Replace(",", ".");
                    nf.vProd = gridNfNormal.Rows[j].Cells["vProd"].Value == null ? "" : gridNfNormal.Rows[j].Cells["vProd"].Value.ToString().Replace(",", ".");
                    nf.vNF = gridNfNormal.Rows[j].Cells["vNF"].Value == null ? "" : gridNfNormal.Rows[j].Cells["vNF"].Value.ToString().Replace(",", ".");
                    nf.nCFOP = gridNfNormal.Rows[j].Cells["nCFOP"].Value == null ? "" : gridNfNormal.Rows[j].Cells["nCFOP"].Value.ToString();

                    objInfCte.rem.infNF.Add(nf);
                }

                objInfCte.rem.infNFe = new List<belinfNFe>();
                for (int n = 0; n < gridNfe.RowCount; n++)
                {
                    belinfNFe nfe = new belinfNFe();
                    nfe.chave = gridNfe.Rows[n].Cells[0].Value == null ? "" : gridNfe.Rows[n].Cells[0].Value.ToString();

                    objInfCte.rem.infNFe.Add(nfe);
                }



                #endregion

                #region Outros Documentos

                objInfCte.rem.infOutros = new List<belinfOutros>();
                for (int j = 0; j < gridDocumentos.RowCount; j++)
                {
                    belinfOutros infOutros = new belinfOutros();
                    switch (gridDocumentos.Rows[j].Cells["tpDoc"].Value.ToString())
                    {
                        case "00 - Declaração":
                            infOutros.tpDoc = "00";
                            break;
                        case "10 - Dutoviário":
                            infOutros.tpDoc = "10";
                            break;
                        case "99 - Outros":
                            infOutros.tpDoc = "99";
                            break;
                    }
                    infOutros.descOutros = gridDocumentos.Rows[j].Cells["descOutros"].Value == null ? "" : gridDocumentos.Rows[j].Cells["descOutros"].Value.ToString();
                    infOutros.nDoc = gridDocumentos.Rows[j].Cells["nDoc_"].Value == null ? "" : gridDocumentos.Rows[j].Cells["nDoc_"].Value.ToString();
                    infOutros.dEmi = gridDocumentos.Rows[j].Cells["dEmi_"].Value == null ? "" : gridDocumentos.Rows[j].Cells["dEmi_"].Value.ToString();
                    infOutros.vDocFisc = gridDocumentos.Rows[j].Cells["vDocFisc"].Value == null ? "" : gridDocumentos.Rows[j].Cells["vDocFisc"].Value.ToString().Replace(",", ".");

                    objInfCte.rem.infOutros.Add(infOutros);
                }

                #endregion

                #region Valores

                objInfCte.vPrest.vTPrest = nudvTPrest.Value.ToString().Replace(",", ".");
                objInfCte.vPrest.vRec = nudvRec.Value.ToString().Replace(",", ".");

                objInfCte.vPrest.Comp = new List<belComp>();
                belComp Comp = new belComp();

                #region Componentes
                if (nudvFrete.Value > 0)
                {
                    Comp.xNome = "FRETE VALOR";
                    Comp.vComp = nudvFrete.Text.Replace(",", ".");
                    objInfCte.vPrest.Comp.Add(Comp);
                }
                if (nudFreteCubagem.Value > 0)
                {
                    Comp = new belComp();
                    Comp.xNome = "FRETE CUBAGEM";
                    Comp.vComp = nudFreteCubagem.Text.Replace(",", ".");
                    objInfCte.vPrest.Comp.Add(Comp);
                }

                if (nudFretePeso.Value > 0)
                {
                    Comp = new belComp();
                    Comp.xNome = "FRETE PESO";
                    Comp.vComp = nudFretePeso.Text.Replace(",", ".");
                    objInfCte.vPrest.Comp.Add(Comp);
                }

                if (nudCat.Value > 0)
                {
                    Comp = new belComp();
                    Comp.xNome = "CAT";
                    Comp.vComp = nudCat.Text.Replace(",", ".");
                    objInfCte.vPrest.Comp.Add(Comp);
                }

                if (nudDespacho.Value > 0)
                {
                    Comp = new belComp();
                    Comp.xNome = "DESPACHO";
                    Comp.vComp = nudDespacho.Text.Replace(",", ".");
                    objInfCte.vPrest.Comp.Add(Comp);
                }

                if (nudPedagio.Value > 0)
                {
                    Comp = new belComp();
                    Comp.xNome = "PEDAGIO";
                    Comp.vComp = nudPedagio.Text.Replace(",", ".");
                    objInfCte.vPrest.Comp.Add(Comp);
                }

                if (nudOutros.Value > 0)
                {
                    Comp = new belComp();
                    Comp.xNome = "OUTROS";
                    Comp.vComp = nudOutros.Text.Replace(",", ".");
                    objInfCte.vPrest.Comp.Add(Comp);
                }

                if (nudAdme.Value > 0)
                {
                    Comp = new belComp();
                    Comp.xNome = "ADME";
                    Comp.vComp = nudAdme.Text.Replace(",", ".");
                    objInfCte.vPrest.Comp.Add(Comp);
                }

                if (nudEntrega.Value > 0)
                {
                    Comp = new belComp();
                    Comp.xNome = "ENTREGA";
                    Comp.vComp = nudEntrega.Text.Replace(",", ".");
                    objInfCte.vPrest.Comp.Add(Comp);
                }
                #endregion




                objInfCte.imp.ICMS = new belICMS();
                if (cboCST.SelectedIndex == 0)
                {
                    objInfCte.imp.ICMS.ICMS00 = new belICMS00();
                    objInfCte.imp.ICMS.ICMS00.CST = "00";
                    objInfCte.imp.ICMS.ICMS00.vBC = nudvBC.Value.ToString().Replace(",", ".");
                    objInfCte.imp.ICMS.ICMS00.pICMS = nudpICMS.Value.ToString().Replace(",", ".");
                    objInfCte.imp.ICMS.ICMS00.vICMS = nudvICMS.Value.ToString().Replace(",", ".");
                }
                else if (cboCST.SelectedIndex == 1)
                {
                    objInfCte.imp.ICMS.ICMS20 = new belICMS20();
                    objInfCte.imp.ICMS.ICMS20.CST = "20";
                    objInfCte.imp.ICMS.ICMS20.pRedBC = nudpRedBC.Value.ToString().Replace(",", ".");
                    objInfCte.imp.ICMS.ICMS20.vBC = nudvBC.Value.ToString().Replace(",", ".");
                    objInfCte.imp.ICMS.ICMS20.pICMS = nudpICMS.Value.ToString().Replace(",", ".");
                    objInfCte.imp.ICMS.ICMS20.vICMS = nudvICMS.Value.ToString().Replace(",", ".");
                }
                else if (cboCST.SelectedIndex == 2 || cboCST.SelectedIndex == 3 || cboCST.SelectedIndex == 4)
                {
                    objInfCte.imp.ICMS.ICMS45 = new belICMS45();
                    switch (cboCST.SelectedIndex)
                    {
                        case 2: objInfCte.imp.ICMS.ICMS45.CST = "40";
                            break;
                        case 3: objInfCte.imp.ICMS.ICMS45.CST = "41";
                            break;
                        case 4: objInfCte.imp.ICMS.ICMS45.CST = "51";
                            break;
                    }
                }
                else if (cboCST.SelectedIndex == 5)
                {
                    objInfCte.imp.ICMS.ICMS60 = new belICMS60();
                    objInfCte.imp.ICMS.ICMS60.CST = "60";
                    objInfCte.imp.ICMS.ICMS60.vBCSTRet = nudvBC.Value.ToString().Replace(",", ".");
                    objInfCte.imp.ICMS.ICMS60.vICMSSTRet = nudvICMS.Value.ToString().Replace(",", ".");
                    objInfCte.imp.ICMS.ICMS60.pICMSSTRet = nudpICMS.Value.ToString().Replace(",", ".");
                }
                else if (cboCST.SelectedIndex == 6)
                {
                    objInfCte.imp.ICMS.ICMS90 = new belICMS90();
                    objInfCte.imp.ICMS.ICMS90.CST = "90";
                    objInfCte.imp.ICMS.ICMS90.pRedBC = nudpRedBC.Value.ToString().Replace(",", ".");
                    objInfCte.imp.ICMS.ICMS90.vBC = nudvBC.Value.ToString().Replace(",", ".");
                    objInfCte.imp.ICMS.ICMS90.pICMS = nudpICMS.Value.ToString().Replace(",", ".");
                    objInfCte.imp.ICMS.ICMS90.vICMS = nudvICMS.Value.ToString().Replace(",", ".");
                }
                else if (cboCST.SelectedIndex == 7)
                {
                    objInfCte.imp.ICMS.ICMSOutraUF = new belICMSOutraUF();
                    objInfCte.imp.ICMS.ICMSOutraUF.CST = "90";
                    objInfCte.imp.ICMS.ICMSOutraUF.pRedBCOutraUF = nudpRedBC.Value.ToString().Replace(",", ".");
                    objInfCte.imp.ICMS.ICMSOutraUF.vBCOutraUF = nudvBC.Value.ToString().Replace(",", ".");
                    objInfCte.imp.ICMS.ICMSOutraUF.pICMSOutraUF = nudpICMS.Value.ToString().Replace(",", ".");
                    objInfCte.imp.ICMS.ICMSOutraUF.vICMSOutraUF = nudvICMS.Value.ToString().Replace(",", ".");
                }



                #endregion

                #region InformacoesCarga

                objInfCte.infCTeNorm.infCarga.vCarga = nudvMerc.Value;
                objInfCte.infCTeNorm.infCarga.proPred = txtproPred.Text;
                objInfCte.infCTeNorm.infCarga.xOutCat = txtxOutCat.Text;


                objInfCte.infCTeNorm.infCarga.infQ = new List<belinfQ>();
                for (int j = 0; j < gridinfQ.RowCount; j++)
                {
                    belinfQ obj = new belinfQ();
                    if (gridinfQ.Rows[j].Cells[0].Value == null)
                    {
                        obj.cUnid = "";
                    }
                    else
                    {
                        switch (gridinfQ.Rows[j].Cells[0].Value.ToString())
                        {
                            case "00-M3": obj.cUnid = "00";
                                break;
                            case "01-KG": obj.cUnid = "01";
                                break;
                            case "02-Ton": obj.cUnid = "02";
                                break;
                            case "03-Unidade": obj.cUnid = "03";
                                break;
                            case "04-Litros": obj.cUnid = "04";
                                break;
                        }
                    }
                    if (gridinfQ.Rows[j].Cells[1].Value == null)
                    {
                        obj.tpMed = "";
                    }
                    else
                    {
                        obj.tpMed = gridinfQ.Rows[j].Cells[1].Value.ToString();
                    }
                    if (gridinfQ.Rows[j].Cells[2].Value == null)
                    {
                        obj.qCarga = 0;
                    }
                    else
                    {
                        obj.qCarga = Convert.ToDecimal(gridinfQ.Rows[j].Cells[2].Value.ToString());
                    }

                    objInfCte.infCTeNorm.infCarga.infQ.Add(obj);
                }

                #endregion

                #region Rodoviario

                objInfCte.infCTeNorm.seg.respSeg = cborespSeg.SelectedIndex != -1 ? cborespSeg.SelectedIndex.ToString() : "";
                objInfCte.infCTeNorm.rodo.dPrev = mskdPrev.Text.Replace(" ", "").Replace("/", "") != "" ? Convert.ToDateTime(mskdPrev.Text).ToShortDateString() : "";
                objInfCte.infCTeNorm.seg.nApol = txtnApol.Text;
                objInfCte.infCTeNorm.rodo.RNTRC = txtRNTRC.Text;
                objInfCte.infCTeNorm.rodo.lota = cbolota.SelectedIndex != -1 ? cbolota.SelectedIndex.ToString() : "";

                #endregion

                #region Veiculo

                if (objInfCte.infCTeNorm.rodo.veic.Count() > 0)
                {
                    if (VerificaCamposVeiculo() || objInfCte.infCTeNorm.rodo.lota == "1")
                    {
                        belveic veic = (belveic)bsVeiculos.Current;

                        veic.RENAVAM = txtRENAVAM.Text;
                        veic.placa = txtplaca.Text;
                        veic.tara = nudtara.Value.ToString();
                        veic.capKG = nudcapKG.Value.ToString();
                        veic.capM3 = nudcapM3.Value.ToString();
                        switch (cbotpProp.SelectedIndex)
                        {
                            case 0:
                                veic.tpProp = "P";
                                break;
                            case 1:
                                veic.tpProp = "T";
                                break;
                            default:
                                veic.tpProp = "";
                                break;
                        }
                        veic.tpVeic = cbotpVeic.SelectedIndex != -1 ? cbotpVeic.SelectedIndex.ToString() : "";
                        veic.tpRod = cbotpRod.SelectedIndex != -1 ? "0" + cbotpRod.SelectedIndex.ToString() : "";
                        veic.tpCar = cbotpCar.SelectedIndex != -1 ? "0" + cbotpCar.SelectedIndex.ToString() : "";
                        veic.UF = txtUF.Text;

                        if (veic.tpProp == "T")
                        {
                            veic.prop = new belprop();
                            veic.prop.CPFCNPJ = txtCPFprop.Text;
                            veic.prop.RNTRC = txtRNTRCprop.Text;
                            veic.prop.xNome = txtxNomeprop.Text;
                            veic.prop.IE = txtIEprop.Text;
                            veic.prop.UF = txtUFprop.Text;
                            veic.prop.tpProp = cbotpPropprop.SelectedIndex != -1 ? cbotpCar.SelectedIndex.ToString() : "";
                        }
                        else
                        {
                            veic.prop = null;
                        }
                    }
                }

                if (VerificaCamposProprietarioVeiculo() || objInfCte.infCTeNorm.rodo.lota == "1")
                {
                    objInfCte.infCTeNorm.rodo.moto = new belmoto();
                    objInfCte.infCTeNorm.rodo.moto.xNome = txtxNomemoto.Text;
                    objInfCte.infCTeNorm.rodo.moto.CPF = txtCPFmoto.Text;
                }
                else
                {
                    objInfCte.infCTeNorm.rodo.moto = null;
                }

                #endregion


                VerificaCte();

            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, _sMessageException + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            }
        }


        private void btnNavegacao(object sender, EventArgs e)
        {
            try
            {
                ToolStripButton btn = (ToolStripButton)sender;
                switch (btn.Name)
                {
                    case "btnProximo":
                        if (iIndex < iCountObj)
                        {
                            SalvarAlteracao(iIndex);
                            iIndex++;
                            PopulaForm(iIndex);
                            VerificaCte();
                        }
                        break;

                    case "btnAnterior":
                        if (iIndex > 0)
                        {
                            SalvarAlteracao(iIndex);
                            iIndex--;
                            PopulaForm(iIndex);
                            VerificaCte();
                        }
                        break;

                    case "btnUltimo":
                        SalvarAlteracao(iIndex);
                        iIndex = iCountObj;
                        PopulaForm(iIndex);
                        VerificaCte();
                        break;

                    case "btnPrimeiro":
                        SalvarAlteracao(iIndex);
                        iIndex = 0;
                        PopulaForm(iIndex);
                        VerificaCte();
                        break;

                }
                lblContagemNotas.Text = (iIndex + 1) + " de " + (iCountObj + 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            HabilitaCampos(true);
            EmEdicao(true);
        }
        private void btnSair_Click(object sender, EventArgs e)
        {
            if (KryptonMessageBox.Show("Nenhum Conhecimento será Enviado. Deseja realmente Sair?", "A L E R T A", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bCancela = true;
                this.Close();
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                if (KryptonMessageBox.Show("Deseja Cancelar as Alterações Realizadas?", "A L E R T A", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    HabilitaCampos(false);
                    EmEdicao(false);

                    CriaObjAlter();
                    iIndex = 0;

                    PopulaForm(iIndex);
                    VerificaCte();
                    lblContagemNotas.Text = (iIndex + 1) + " de " + (iCountObj + 1);
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, _sMessageException + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            }
        }
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                SalvarAlteracao(iIndex);

                if (VerificaCampos(iIndex) == true && VerificaCte() == true)
                {
                    EmEdicao(false);
                    HabilitaCampos(false);
                }
                else
                {
                    KryptonMessageBox.Show("Ainda existem Erros Pendentes!" + Environment.NewLine + "Verifique Todos antes de Enviar o CT-e.", "A T E N Ç Ã O", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, _sMessageException + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            }
        }
        private void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                if (VerificaCampos(iIndex) == true)
                {
                    if (VerificaCte() == true)
                    {
                        SalvarAlteracao(iIndex);
                        bCancela = false;
                        bEnviar = true;
                        this.Close();
                    }
                    else
                    {
                        KryptonMessageBox.Show("Verifique Todos os Erros antes de Enviar o CT-e!", "A T E N Ç Ã O", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    KryptonMessageBox.Show("Verifique Todos os Erros antes de Enviar o CT-e!", "A T E N Ç Ã O", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, _sMessageException + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            }
        }


        #region Tratamentos

        private void HabilitaCamposToma()
        {
            if (cbotoma.SelectedIndex == 4)
            {
                flpDadosTomador.Enabled = true;
                flpEndTomador.Enabled = true;
            }
            else
            {
                flpDadosTomador.Enabled = false;
                flpEndTomador.Enabled = false;
            }
        }
        private void cbotoma_SelectedIndexChanged(object sender, EventArgs e)
        {
            HabilitaCamposToma();
        }

        /// <summary>
        ///(0) 00 - Tributação normal ICMS
        ///(1) 20 - Tributação com BC reduzida do ICMS
        ///(2) 40 - ICMS isenção
        ///(3) 41 - ICMS não tributada
        ///(4) 51 - ICMS diferido
        ///(5) 60 - ICMS cobrado anteriormente por substituição tributária
        ///(6) 90 - ICMS outros
        ///(7) 90 - ICMS outros (ICMS devido à UF de origem da prestação, quando diferente da UF do emitente)
        /// </summary>
        /// <param name="index"></param>
        private void HabilitaCamposValores(int index)
        {
            switch (index)
            {
                //(0) 00 - Tributação normal ICMS
                case 0:
                    nudvBC.Enabled = true;
                    nudpICMS.Enabled = true;
                    nudvICMS.Enabled = true;
                    nudpRedBC.Enabled = false;
                    break;

                //(1) 20 - Tributação com BC reduzida do ICMS
                case 1:
                    nudvBC.Enabled = true;
                    nudpICMS.Enabled = true;
                    nudvICMS.Enabled = true;
                    nudpRedBC.Enabled = true;
                    break;

                //(2) 40 - ICMS isenção
                case 2:
                    nudvBC.Enabled = false;
                    nudpICMS.Enabled = false;
                    nudvICMS.Enabled = false;
                    nudpRedBC.Enabled = false;
                    break;

                //(3) 41 - ICMS não tributada
                case 3:
                    nudvBC.Enabled = false;
                    nudpICMS.Enabled = false;
                    nudvICMS.Enabled = false;
                    nudpRedBC.Enabled = false;
                    break;

                //(4) 51 - ICMS diferido
                case 4:
                    nudvBC.Enabled = false;
                    nudpICMS.Enabled = false;
                    nudvICMS.Enabled = false;
                    nudpRedBC.Enabled = false;
                    break;


                //(5) 60 - ICMS cobrado anteriormente por substituição tributária
                case 5:
                    nudvBC.Enabled = true;
                    nudpICMS.Enabled = true;
                    nudvICMS.Enabled = true;
                    nudpRedBC.Enabled = false;
                    break;

                //(6) 90 - ICMS outros
                case 6:
                    nudvBC.Enabled = true;
                    nudpICMS.Enabled = true;
                    nudvICMS.Enabled = true;
                    nudpRedBC.Enabled = true;
                    break;

                //(7) 90 - ICMS outros (ICMS devido à UF de origem da prestação, quando diferente da UF do emitente)
                case 7:
                    nudvBC.Enabled = true;
                    nudpICMS.Enabled = true;
                    nudvICMS.Enabled = true;
                    nudpRedBC.Enabled = true;
                    break;
            }

        }

        private bool VerificaCampos(int index)
        {
            errErro.Dispose();
            bool Retorno = true;
            string sObrigatorio = "Campo Obrigatório!";
            string sMascara = "Valor do Campo Inválido!";
            string sCpf = "CPF Inválido!";
            string sCnpj = "CNPJ Inválido!";

            #region CamposObrigatorios

            #region Identificacao

            if (txtcUF.Text == "") { errErro.SetError(txtcUF, sObrigatorio); Retorno = false; }
            if (txtcCT.Text == "") { errErro.SetError(txtcCT, sObrigatorio); Retorno = false; }
            if (txtCFOP.Text == "") { errErro.SetError(txtCFOP, sObrigatorio); Retorno = false; }
            if (txtnatOp.Text == "") { errErro.SetError(txtnatOp, sObrigatorio); Retorno = false; }
            if (cboforPag.SelectedIndex == -1) { errErro.SetError(cboforPag, sObrigatorio); Retorno = false; }
            if (txtmod.Text == "") { errErro.SetError(txtmod, sObrigatorio); Retorno = false; }
            if (txtserie.Text == "") { errErro.SetError(txtserie, sObrigatorio); Retorno = false; }
            if (txtnCT.Text == "") { errErro.SetError(txtnCT, sObrigatorio); Retorno = false; }
            if (cbotpEmis.SelectedIndex == -1) { errErro.SetError(cbotpEmis, sObrigatorio); Retorno = false; }
            if (txtcDV.Text == "") { errErro.SetError(txtcDV, sObrigatorio); Retorno = false; }
            if (cbotpCTe.SelectedIndex == -1) { errErro.SetError(cbotpCTe, sObrigatorio); Retorno = false; }
            if (txtverProc.Text == "") { errErro.SetError(txtverProc, sObrigatorio); Retorno = false; }
            if (txtxMunEmi.Text == "") { errErro.SetError(txtxMunEmi, sObrigatorio); Retorno = false; }
            if (txtcMunEmi.Text == "") { errErro.SetError(txtcMunEmi, sObrigatorio); Retorno = false; }
            if (txtUFEmi.Text == "") { errErro.SetError(txtUFEmi, sObrigatorio); Retorno = false; }
            if (cbomodal.SelectedIndex == -1) { errErro.SetError(cbomodal, sObrigatorio); Retorno = false; }
            if (cbotpServ.SelectedIndex == -1) { errErro.SetError(cbotpServ, sObrigatorio); Retorno = false; }
            if (txtxMunIni.Text == "") { errErro.SetError(txtxMunIni, sObrigatorio); Retorno = false; }
            if (txtcMunIni.Text == "") { errErro.SetError(txtcMunIni, sObrigatorio); Retorno = false; }
            if (txtUFIni.Text == "") { errErro.SetError(txtUFIni, sObrigatorio); Retorno = false; }
            if (txtxMunFim.Text == "") { errErro.SetError(txtxMunFim, sObrigatorio); Retorno = false; }
            if (txtcMunFim.Text == "") { errErro.SetError(txtcMunFim, sObrigatorio); Retorno = false; }
            if (txtUFFim.Text == "") { errErro.SetError(txtUFFim, sObrigatorio); Retorno = false; }
            if (cboretira.SelectedIndex == -1) { errErro.SetError(cboretira, sObrigatorio); Retorno = false; }

            #endregion

            #region Tomador
            if (cbotoma.SelectedIndex == -1) { errErro.SetError(cbotoma, sObrigatorio); Retorno = false; }
            if (cbotoma.SelectedIndex == 4)
            {
                if (txtCNPJtoma.Text == "") { errErro.SetError(txtCNPJtoma, sObrigatorio); Retorno = false; }
                if (txtxNomeToma.Text == "") { errErro.SetError(txtxNomeToma, sObrigatorio); Retorno = false; }

                if (txtxLgrToma.Text == "") { errErro.SetError(txtxLgrToma, sObrigatorio); Retorno = false; }
                if (txtnroToma.Text == "") { errErro.SetError(txtnroToma, sObrigatorio); Retorno = false; }
                if (txtxBairroToma.Text == "") { errErro.SetError(txtxBairroToma, sObrigatorio); Retorno = false; }
                if (txtxMunToma.Text == "") { errErro.SetError(txtxMunToma, sObrigatorio); Retorno = false; }
                if (txtcMunToma.Text == "") { errErro.SetError(txtcMunToma, sObrigatorio); Retorno = false; }
                if (txtUFToma.Text == "") { errErro.SetError(txtUFToma, sObrigatorio); Retorno = false; }

            }

            #endregion

            #region Emitente

            if (txtCNPJEmit.Text == "") { errErro.SetError(txtCNPJEmit, sObrigatorio); Retorno = false; }
            if (txtIEEmit.Text == "") { errErro.SetError(txtIEEmit, sObrigatorio); Retorno = false; }
            if (txtxNomeEmit.Text == "") { errErro.SetError(txtxNomeEmit, sObrigatorio); Retorno = false; }

            if (txtxLgrEmit.Text == "") { errErro.SetError(txtxLgrEmit, sObrigatorio); Retorno = false; }
            if (txtnroEmit.Text == "") { errErro.SetError(txtnroEmit, sObrigatorio); Retorno = false; }
            if (txtxBairroEmit.Text == "") { errErro.SetError(txtxBairroEmit, sObrigatorio); Retorno = false; }
            if (txtxMunEmit.Text == "") { errErro.SetError(txtxMunEmit, sObrigatorio); Retorno = false; }
            if (txtcMunEmit.Text == "") { errErro.SetError(txtcMunEmit, sObrigatorio); Retorno = false; }
            if (txtUFEmit.Text == "") { errErro.SetError(txtUFEmit, sObrigatorio); Retorno = false; }


            #endregion

            #region Remetente
            if (txtCNPJrem.Text == "") { errErro.SetError(txtCNPJrem, sObrigatorio); Retorno = false; }
            if (txtIErem.Text == "") { errErro.SetError(txtIErem, sObrigatorio); Retorno = false; }
            if (txtxNomerem.Text == "") { errErro.SetError(txtxNomerem, sObrigatorio); Retorno = false; }
            if (txtxLgrrem.Text == "") { errErro.SetError(txtxLgrrem, sObrigatorio); Retorno = false; }
            if (txtnrorem.Text == "") { errErro.SetError(txtnrorem, sObrigatorio); Retorno = false; }
            if (txtxBairrorem.Text == "") { errErro.SetError(txtxBairrorem, sObrigatorio); Retorno = false; }
            if (txtcMunrem.Text == "") { errErro.SetError(txtcMunrem, sObrigatorio); Retorno = false; }
            if (txtxMunrem.Text == "") { errErro.SetError(txtxMunrem, sObrigatorio); Retorno = false; }
            if (txtUFrem.Text == "") { errErro.SetError(txtUFrem, sObrigatorio); Retorno = false; }

            #endregion

            #region Destinatario

            if (txtCNPJdest.Text == "") { errErro.SetError(txtCNPJdest, sObrigatorio); Retorno = false; }
            if (txtxNomedest.Text == "") { errErro.SetError(txtxNomedest, sObrigatorio); Retorno = false; }
            if (txtxLgrdest.Text == "") { errErro.SetError(txtxLgrdest, sObrigatorio); Retorno = false; }
            if (txtnrodest.Text == "") { errErro.SetError(txtnrodest, sObrigatorio); Retorno = false; }
            if (txtxBairrodest.Text == "") { errErro.SetError(txtxBairrodest, sObrigatorio); Retorno = false; }
            if (txtcMundest.Text == "") { errErro.SetError(txtcMundest, sObrigatorio); Retorno = false; }
            if (txtxMundest.Text == "") { errErro.SetError(txtxMundest, sObrigatorio); Retorno = false; }
            if (txtUFdest.Text == "") { errErro.SetError(txtUFdest, sObrigatorio); Retorno = false; }

            #endregion

            #region Expedidor
            if (this.objObjetosAlter.objLinfCte[index].exped != null)
            {
                if (txtCNPJexped.Text == "") { errErro.SetError(txtCNPJexped, sObrigatorio); Retorno = false; }
                if (txtIEexped.Text == "") { errErro.SetError(txtIEexped, sObrigatorio); Retorno = false; }
                if (txtxNomeexped.Text == "") { errErro.SetError(txtxNomeexped, sObrigatorio); Retorno = false; }
                if (txtxLgrexped.Text == "") { errErro.SetError(txtxLgrexped, sObrigatorio); Retorno = false; }
                if (txtnroexped.Text == "") { errErro.SetError(txtnroexped, sObrigatorio); Retorno = false; }
                if (txtxBairroexped.Text == "") { errErro.SetError(txtxBairroexped, sObrigatorio); Retorno = false; }
                if (txtcMunexped.Text == "") { errErro.SetError(txtcMunexped, sObrigatorio); Retorno = false; }
                if (txtxMunexped.Text == "") { errErro.SetError(txtxMunexped, sObrigatorio); Retorno = false; }
                if (txtUFexped.Text == "") { errErro.SetError(txtUFexped, sObrigatorio); Retorno = false; }
            }


            #endregion

            #region Recebedor
            if (this.objObjetosAlter.objLinfCte[index].receb != null)
            {
                if (txtCpfCnpfreceb.Text == "") { errErro.SetError(txtCpfCnpfreceb, sObrigatorio); Retorno = false; }
                if (txtIEreceb.Text == "") { errErro.SetError(txtIEreceb, sObrigatorio); Retorno = false; }
                if (txtxNomereceb.Text == "") { errErro.SetError(txtxNomereceb, sObrigatorio); Retorno = false; }
                if (txtxLgrreceb.Text == "") { errErro.SetError(txtxLgrreceb, sObrigatorio); Retorno = false; }
                if (txtnroreceb.Text == "") { errErro.SetError(txtnroreceb, sObrigatorio); Retorno = false; }
                if (txtxBairroreceb.Text == "") { errErro.SetError(txtxBairroreceb, sObrigatorio); Retorno = false; }
                if (txtcMunreceb.Text == "") { errErro.SetError(txtcMunreceb, sObrigatorio); Retorno = false; }
                if (txtxMunreceb.Text == "") { errErro.SetError(txtxMunreceb, sObrigatorio); Retorno = false; }
                if (txtUFreceb.Text == "") { errErro.SetError(txtUFreceb, sObrigatorio); Retorno = false; }
            }


            #endregion

            #region Valores
            if (nudvTPrest.Value.Equals(0)) { errErro.SetError(nudvTPrest, sObrigatorio); Retorno = false; }
            if (nudvRec.Value.Equals(0)) { errErro.SetError(nudvRec, sObrigatorio); Retorno = false; }

            if (cboCST.SelectedIndex == -1) { errErro.SetError(cboCST, sObrigatorio); Retorno = false; }

            #endregion

            #region InformacoesCarga
            if (nudvMerc.Value.Equals(0)) { errErro.SetError(nudvMerc, sObrigatorio); Retorno = false; }
            if (txtproPred.Text == "") { errErro.SetError(txtproPred, sObrigatorio); Retorno = false; }

            if (gridinfQ.RowCount > 1)
            {
                for (int i = 0; i < gridinfQ.RowCount - 1; i++)
                {
                    if (gridinfQ.Rows[i].Cells[0].Value == null) { errErro.SetError(gridinfQ, "Preencha Todos os Campos!"); Retorno = false; }
                    if (gridinfQ.Rows[i].Cells[1].Value == null) { errErro.SetError(gridinfQ, "Preencha Todos os Campos!"); Retorno = false; }
                    if (gridinfQ.Rows[i].Cells[2].Value == null) { errErro.SetError(gridinfQ, "Preencha Todos os Campos!"); Retorno = false; }
                }
            }
            else if (gridinfQ.RowCount == 1)
            {
                if (gridinfQ.Rows[0].Cells[0].Value == null) { errErro.SetError(gridinfQ, "Preencha Todos os Campos!"); Retorno = false; }
                if (gridinfQ.Rows[0].Cells[1].Value == null) { errErro.SetError(gridinfQ, "Preencha Todos os Campos!"); Retorno = false; }
                if (gridinfQ.Rows[0].Cells[2].Value == null) { errErro.SetError(gridinfQ, "Preencha Todos os Campos!"); Retorno = false; }
            }

            #endregion

            #region Rodoviario

            if (cborespSeg.SelectedIndex == -1) { errErro.SetError(cborespSeg, sObrigatorio); Retorno = false; }
            if (txtnApol.Text == "") { errErro.SetError(txtnApol, sObrigatorio); Retorno = false; }
            if (mskdPrev.Text.Replace(" ", "").Replace("/", "") == "") { errErro.SetError(mskdPrev, sObrigatorio); Retorno = false; }
            if (txtRNTRC.Text == "") { errErro.SetError(txtRNTRC, sObrigatorio); Retorno = false; }
            if (cbolota.SelectedIndex == -1) { errErro.SetError(cbolota, sObrigatorio); Retorno = false; }

            #endregion

            #region Veiculo

            Retorno = VerificaCamposObrigatoriosVeiculo(Retorno, sObrigatorio);

            #endregion

            if (txtxNomemoto.Text != "" || txtCPFmoto.Text != "" || cbolota.SelectedIndex == 1)
            {
                if (txtxNomemoto.Text == "") { errErro.SetError(txtxNomemoto, sObrigatorio); Retorno = false; }
                if (txtCPFmoto.Text == "") { errErro.SetError(txtCPFmoto, sObrigatorio); Retorno = false; }
            }
            #endregion

            #region ValidaMascaras

            Match valida;

            #region Tomador

            string CNPJToma = txtCNPJtoma.Text;
            if (CNPJToma != "")
            {
                if (txtCNPJtoma.Text.Length == 14)
                {
                    valida = Regex.Match(CNPJToma, Expressoes.ER6);
                    if (!valida.Success)
                    {
                        errErro.SetError(txtCNPJtoma, sCnpj);
                        Retorno = false;
                    }
                }
                else
                {
                    valida = Regex.Match(CNPJToma, Expressoes.ER7);
                    if (!valida.Success)
                    {
                        errErro.SetError(txtCNPJtoma, sCpf);
                        Retorno = false;
                    }
                }
            }
            if (txtIEToma.Text != "")
            {
                valida = Regex.Match(txtIEToma.Text, Expressoes.ER26);
                if (!valida.Success)
                {
                    errErro.SetError(txtIEToma, sMascara);
                    Retorno = false;
                }
            }

            if (txtfoneToma.Text != "")
            {
                valida = Regex.Match(txtfoneToma.Text, Expressoes.ER36);
                if (!valida.Success)
                {
                    errErro.SetError(txtfoneToma, sMascara);
                    Retorno = false;
                }
            }

            if (mskCEPToma.Text != "")
            {
                valida = Regex.Match(mskCEPToma.Text, Expressoes.ER33);
                if (!valida.Success)
                {
                    errErro.SetError(mskCEPToma, sMascara);
                    Retorno = false;
                }
            }



            #endregion

            #region emit
            if (txtCNPJEmit.Text != "")
            {
                valida = Regex.Match(txtCNPJEmit.Text, Expressoes.ER4);
                if (!valida.Success)
                {
                    errErro.SetError(txtCNPJEmit, sCnpj);
                    Retorno = false;
                }
            }
            if (txtIEEmit.Text != "")
            {
                valida = Regex.Match(txtIEEmit.Text, Expressoes.ER25);
                if (!valida.Success)
                {
                    errErro.SetError(txtIEEmit, sMascara);
                    Retorno = false;
                }
            }

            if (mskCEPEmit.Text != "")
            {
                valida = Regex.Match(mskCEPEmit.Text, Expressoes.ER33);
                if (!valida.Success)
                {
                    errErro.SetError(mskCEPEmit, sMascara);
                    Retorno = false;
                }
            }
            if (txtfoneEmit.Text != "")
            {
                valida = Regex.Match(txtfoneEmit.Text, Expressoes.ER36);
                if (!valida.Success)
                {
                    errErro.SetError(txtfoneEmit, sMascara);
                    Retorno = false;
                }
            }
            #endregion

            #region rem

            string CNPJrem = txtCNPJrem.Text;
            if (this.objObjetosAlter.objLinfCte[index].rem.CNPJ != "")
            {
                valida = Regex.Match(CNPJrem, Expressoes.ER6);
                if (!valida.Success)
                {
                    errErro.SetError(txtCNPJrem, sCnpj);
                    Retorno = false;
                }
            }
            else if (this.objObjetosAlter.objLinfCte[index].rem.CPF != "")
            {
                valida = Regex.Match(CNPJrem, Expressoes.ER7);
                if (!valida.Success)
                {
                    errErro.SetError(txtCNPJrem, sCpf);
                    Retorno = false;
                }
            }
            if (txtIErem.Text != "")
            {
                valida = Regex.Match(txtIErem.Text, Expressoes.ER26);
                if (!valida.Success)
                {
                    errErro.SetError(txtIErem, sMascara);
                    Retorno = false;
                }
            }

            if (mskCEPrem.Text != "")
            {
                valida = Regex.Match(mskCEPrem.Text, Expressoes.ER33);
                if (!valida.Success)
                {
                    errErro.SetError(mskCEPrem, sMascara);
                    Retorno = false;
                }
            }

            if (txtfonerem.Text != "")
            {
                valida = Regex.Match(txtfonerem.Text, Expressoes.ER36);
                if (!valida.Success)
                {
                    errErro.SetError(txtfonerem, sMascara);
                    Retorno = false;
                }
            }
            #endregion

            #region exped
            if (this.objObjetosAlter.objLinfCte[index].exped != null)
            {
                if (this.objObjetosAlter.objLinfCte[index].exped.CNPJ != "")
                {
                    valida = Regex.Match(txtCNPJexped.Text, Expressoes.ER6);
                    if (!valida.Success)
                    {
                        errErro.SetError(txtCNPJdest, sCnpj);
                        Retorno = false;
                    }
                }

                else if (this.objObjetosAlter.objLinfCte[index].exped.CPF != "")
                {
                    valida = Regex.Match(txtCNPJexped.Text, Expressoes.ER7);
                    if (!valida.Success)
                    {
                        errErro.SetError(txtCNPJexped, sCpf);
                        Retorno = false;
                    }
                }
                if (txtIEexped.Text != "")
                {
                    valida = Regex.Match(txtIEexped.Text, Expressoes.ER26);
                    if (!valida.Success)
                    {
                        errErro.SetError(txtIEexped, sMascara);
                        Retorno = false;
                    }
                }

                if (mskCEPexped.Text != "")
                {
                    valida = Regex.Match(mskCEPexped.Text, Expressoes.ER33);
                    if (!valida.Success)
                    {
                        errErro.SetError(mskCEPexped, sCpf);
                        Retorno = false;
                    }
                }

                if (txtfoneexped.Text != "")
                {
                    valida = Regex.Match(txtfoneexped.Text, Expressoes.ER36);
                    if (!valida.Success)
                    {
                        errErro.SetError(txtfoneexped, sMascara);
                        Retorno = false;
                    }
                }
            }


            #endregion

            #region receb

            if (this.objObjetosAlter.objLinfCte[index].receb != null)
            {
                if (this.objObjetosAlter.objLinfCte[index].receb.CNPJ != "")
                {
                    valida = Regex.Match(txtCpfCnpfreceb.Text, Expressoes.ER6);
                    if (!valida.Success)
                    {
                        errErro.SetError(txtCpfCnpfreceb, sCnpj);
                        Retorno = false;
                    }
                }

                else if (this.objObjetosAlter.objLinfCte[index].receb.CPF != "")
                {
                    valida = Regex.Match(txtCpfCnpfreceb.Text, Expressoes.ER7);
                    if (!valida.Success)
                    {
                        errErro.SetError(txtCpfCnpfreceb, sCpf);
                        Retorno = false;
                    }
                }

                if (txtIEreceb.Text != "")
                {
                    valida = Regex.Match(txtIEreceb.Text, Expressoes.ER26);
                    if (!valida.Success)
                    {
                        errErro.SetError(txtIEreceb, sMascara);
                        Retorno = false;
                    }
                }
                if (mskCEPreceb.Text != "")
                {
                    valida = Regex.Match(mskCEPreceb.Text, Expressoes.ER33);
                    if (!valida.Success)
                    {
                        errErro.SetError(mskCEPreceb, sCpf);
                        Retorno = false;
                    }
                }
                if (txtfonereceb.Text != "")
                {
                    valida = Regex.Match(txtfonereceb.Text, Expressoes.ER36);
                    if (!valida.Success)
                    {
                        errErro.SetError(txtfonereceb, sMascara);
                        Retorno = false;
                    }
                }
            }


            #endregion

            #region dest

            string CNPJdest = txtCNPJdest.Text;
            if (this.objObjetosAlter.objLinfCte[index].dest.CNPJ != "")
            {
                valida = Regex.Match(CNPJdest, Expressoes.ER6);
                if (!valida.Success)
                {
                    errErro.SetError(txtCNPJdest, sCnpj);
                    Retorno = false;
                }
            }
            else if (this.objObjetosAlter.objLinfCte[index].dest.CPF != "")
            {
                valida = Regex.Match(CNPJdest, Expressoes.ER7);
                if (!valida.Success)
                {
                    errErro.SetError(txtCNPJdest, sCpf);
                    Retorno = false;
                }
            }
            if (txtIEdest.Text != "")
            {
                valida = Regex.Match(txtIEdest.Text, Expressoes.ER26);
                if (!valida.Success)
                {
                    errErro.SetError(txtIEdest, sMascara);
                    Retorno = false;
                }
            }

            if (txtISUFdest.Text != "")
            {
                valida = Regex.Match(txtISUFdest.Text, Expressoes.ER38);
                if (!valida.Success)
                {
                    errErro.SetError(txtISUFdest, sMascara);
                    Retorno = false;
                }
            }
            if (txtfonedest.Text != "")
            {
                valida = Regex.Match(txtfonedest.Text, Expressoes.ER36);
                if (!valida.Success)
                {
                    errErro.SetError(txtfonedest, sMascara);
                    Retorno = false;
                }
            }
            if (mskCEPdest.Text != "")
            {
                valida = Regex.Match(mskCEPdest.Text, Expressoes.ER33);
                if (!valida.Success)
                {
                    errErro.SetError(mskCEPdest, sMascara);
                    Retorno = false;
                }
            }


            #endregion

            #region Modal Rodoviario

            if (txtnApol.Text != "")
            {
                valida = Regex.Match(txtnApol.Text, Expressoes.ER32);
                if (!valida.Success)
                {
                    errErro.SetError(txtnApol, sMascara);
                    Retorno = false;
                }
            }
            valida = Regex.Match(txtRNTRC.Text, Expressoes.ER33);
            if (!valida.Success)
            {
                errErro.SetError(txtRNTRC, sMascara);
                Retorno = false;
            }

            #region Veiculos
            if (txtRENAVAM.Text != "")
            {
                valida = Regex.Match(txtRENAVAM.Text, Expressoes.ER32);
                if (!valida.Success)
                {
                    errErro.SetError(txtRENAVAM, sMascara);
                    Retorno = false;
                }
            }
            if (txtplaca.Text != "")
            {
                valida = Regex.Match(txtplaca.Text, Expressoes.ER50);
                if (!valida.Success)
                {
                    errErro.SetError(txtplaca, sMascara);
                    Retorno = false;
                }
            }
            if (nudtara.Value > 0)
            {
                valida = Regex.Match(nudtara.Value.ToString(), Expressoes.ER53);
                if (!valida.Success)
                {
                    errErro.SetError(nudtara, sMascara);
                    Retorno = false;
                }
            }
            if (nudcapKG.Value > 0)
            {
                valida = Regex.Match(nudcapKG.Value.ToString(), Expressoes.ER53);
                if (!valida.Success)
                {
                    errErro.SetError(nudcapKG, sMascara);
                    Retorno = false;
                }
            }
            if (nudcapM3.Value > 0)
            {
                valida = Regex.Match(nudcapM3.Value.ToString(), Expressoes.ER30);
                if (!valida.Success)
                {
                    errErro.SetError(nudcapM3, sMascara);
                    Retorno = false;
                }
            }
            #endregion


            #region Proprietario Veiculo

            if (txtRNTRCprop.Text != "")
            {
                valida = Regex.Match(txtRNTRCprop.Text, Expressoes.ER33);
                if (!valida.Success)
                {
                    errErro.SetError(txtRNTRCprop, sMascara);
                    Retorno = false;
                }
            }
            if (txtIEprop.Text != "")
            {
                valida = Regex.Match(txtIEprop.Text, Expressoes.ER26);
                if (!valida.Success)
                {
                    errErro.SetError(txtIEprop, sMascara);
                    Retorno = false;
                }
            }
            if (txtCPFprop.Text != "")
            {
                valida = Regex.Match(txtCPFprop.Text, Expressoes.ER7);
                if (!valida.Success)
                {
                    valida = Regex.Match(txtCPFprop.Text, Expressoes.ER6);
                    if (!valida.Success)
                    {
                        errErro.SetError(txtCPFprop, sMascara);
                        Retorno = false;
                    }
                }
            }
            if (txtCPFmoto.Text != "")
            {
                valida = Regex.Match(txtCPFmoto.Text, Expressoes.ER7);
                if (!valida.Success)
                {
                    errErro.SetError(txtCPFmoto, sCpf);
                    Retorno = false;
                }
            }



            #endregion





            #endregion



            #endregion



            return Retorno;
        }

        private bool VerificaCamposObrigatoriosVeiculo(bool Retorno, string sObrigatorio)
        {
            if (cbolota.SelectedIndex == 1 || VerificaCamposVeiculo())
            {
                errVeiculo.Dispose();
                if (txtRENAVAM.Text == "") { errVeiculo.SetError(txtRENAVAM, sObrigatorio); Retorno = false; }
                if (txtplaca.Text == "") { errVeiculo.SetError(txtplaca, sObrigatorio); Retorno = false; }
                if (nudtara.Value == 0) { errVeiculo.SetError(nudtara, sObrigatorio); Retorno = false; }
                if (nudcapKG.Value == 0) { errVeiculo.SetError(nudcapKG, sObrigatorio); Retorno = false; }
                if (nudcapM3.Value == 0) { errVeiculo.SetError(nudcapM3, sObrigatorio); Retorno = false; }
                if (cbotpProp.SelectedIndex == -1) { errVeiculo.SetError(cbotpProp, sObrigatorio); Retorno = false; }
                if (cbotpVeic.SelectedIndex == -1) { errVeiculo.SetError(cbotpVeic, sObrigatorio); Retorno = false; }
                if (cbotpRod.SelectedIndex == -1) { errVeiculo.SetError(cbotpRod, sObrigatorio); Retorno = false; }
                if (cbotpCar.SelectedIndex == -1) { errVeiculo.SetError(cbotpCar, sObrigatorio); Retorno = false; }
                if (txtUF.Text == "") { errVeiculo.SetError(txtUF, sObrigatorio); Retorno = false; }


                if (cbotpProp.SelectedIndex == 1 || VerificaCamposProprietarioVeiculo())
                {
                    if (txtCPFprop.Text == "") { errVeiculo.SetError(txtCPFprop, sObrigatorio); Retorno = false; }
                    if (txtRNTRC.Text == "") { errVeiculo.SetError(txtRNTRC, sObrigatorio); Retorno = false; }
                    if (txtxNomeprop.Text == "") { errVeiculo.SetError(txtxNomeprop, sObrigatorio); Retorno = false; }
                    if (txtIEprop.Text == "") { errVeiculo.SetError(txtIEprop, sObrigatorio); Retorno = false; }
                    if (txtUFprop.Text == "") { errVeiculo.SetError(txtUFprop, sObrigatorio); Retorno = false; }
                    if (cbotpPropprop.SelectedIndex == -1) { errVeiculo.SetError(cbotpPropprop, sObrigatorio); Retorno = false; }
                }

            }
            return Retorno;
        }
        private bool VerificaCamposVeiculo()
        {
            try
            {
                if (txtRENAVAM.Text != "" || txtplaca.Text != "" || nudtara.Value != 0 || nudcapKG.Value != 0 || nudcapM3.Value != 0 ||
                    cbotpProp.SelectedIndex != -1 || cbotpVeic.SelectedIndex != -1 || cbotpRod.SelectedIndex != -1 || cbotpCar.SelectedIndex != -1 || txtUF.Text != "")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool VerificaCamposProprietarioVeiculo()
        {
            try
            {

                if (txtCPFprop.Text != "" || txtRNTRCprop.Text != "" || txtxNomeprop.Text != "" || txtIEprop.Text != "" || txtUFprop.Text != "" || cbotpPropprop.SelectedIndex != -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private bool VerificaCte()
        {
            try
            {


                bool Retorno = true;
                int iCountErros = 0;
                objListaErroAtual = new List<ErrosNotas>();


                for (int i = 0; i < objObjetosAlter.objLinfCte.Count; i++)
                {
                    ErrosNotas objErro = new ErrosNotas();
                    objObjetosAlter.objLinfCte[i].ide.nCT = Convert.ToInt32(objObjetosAlter.objLinfCte[i].ide.nCT).ToString();
                    objErro.sNumCte = objObjetosAlter.objLinfCte[i].ide.nCT;

                    belinfCte objbelinfCte = objObjetosAlter.objLinfCte[i];
                    #region Identificacao

                    if (objbelinfCte.ide.cUF == "") { objErro.sErro = objErro.sErro + "            UF Emitente é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.ide.cCT == "") { objErro.sErro = objErro.sErro + "            Código Numérico que compõe a Chave é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.ide.CFOP == "") { objErro.sErro = objErro.sErro + "            CFOP é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.ide.natOp == "") { objErro.sErro = objErro.sErro + "            Natureza da Operação é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.ide.mod == "") { objErro.sErro = objErro.sErro + "            Modelo do Documento Fiscal é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.ide.serie == "") { objErro.sErro = objErro.sErro + "            Série do CT-e é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.ide.nCT == "") { objErro.sErro = objErro.sErro + "            Número do CT-e é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.ide.tpImp == "") { objErro.sErro = objErro.sErro + "            Formato de Impressão é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.ide.tpEmis == "") { objErro.sErro = objErro.sErro + "            Formato de Emissão é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.ide.cDV == "") { objErro.sErro = objErro.sErro + "            Digito Verificador da Chave de Acesso é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.ide.tpAmb == "") { objErro.sErro = objErro.sErro + "            Tipo de Ambiente é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.ide.cMunEnv == "") { objErro.sErro = objErro.sErro + "            Código do Município onde o CT-e está sendo emitido é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.ide.xMunEnv == "") { objErro.sErro = objErro.sErro + "            Nome do Município onde o CT-e está sendo emitido é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.ide.UFEnv == "") { objErro.sErro = objErro.sErro + "            Sigla da UF onde o CT-e está sendo emitido é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.ide.modal == "") { objErro.sErro = objErro.sErro + "            Modal é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.ide.cMunIni == "") { objErro.sErro = objErro.sErro + "            Código do Município de início da prestação é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.ide.xMunIni == "") { objErro.sErro = objErro.sErro + "            Nome do Município do início da prestação é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.ide.UFIni == "") { objErro.sErro = objErro.sErro + "            UF do início da prestação é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.ide.cMunFim == "") { objErro.sErro = objErro.sErro + "            Código do Município de término da prestação é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.ide.xMunFim == "") { objErro.sErro = objErro.sErro + "            Nome do Município do término da prestação é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.ide.UFFim == "") { objErro.sErro = objErro.sErro + "            UF do término da prestação é Obrigatório!" + Environment.NewLine; iCountErros++; }

                    #endregion

                    #region Tomador
                    if (objbelinfCte.ide.toma03 != null)
                    {
                        if (objbelinfCte.ide.toma03.toma == "") { objErro.sErro = objErro.sErro + "            Tomador do Serviço é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    }
                    else if (objbelinfCte.ide.toma04 != null)
                    {
                        if (objbelinfCte.ide.toma04.toma == "") { objErro.sErro = objErro.sErro + "            Tomador do Serviço é Obrigatório!" + Environment.NewLine; iCountErros++; }

                        if (objbelinfCte.ide.toma04.CNPJ != "")
                        {
                            string sCnpj = util.TiraSimbolo(objbelinfCte.ide.toma04.CNPJ);
                            if (ValidaCnpj(sCnpj) == false) { objErro.sErro = objErro.sErro + "             CNPJ Tomador do Serviço Inválido!" + Environment.NewLine; iCountErros++; }
                        }
                        else if (objbelinfCte.ide.toma04.CPF != "")
                        {
                            string sCpf = util.TiraSimbolo(objbelinfCte.ide.toma04.CPF);
                            if (ValidaCpf(sCpf) == false) { objErro.sErro = objErro.sErro + "             CPF Tomador do Serviço Inválido!" + Environment.NewLine; iCountErros++; }
                        }
                        else { objErro.sErro = objErro.sErro + "             CPF ou CNPJ Tomador do Serviço Inválido!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.ide.toma04.xNome == "") { objErro.sErro = objErro.sErro + "             Razão Social ou Nome Tomador do Serviço é Obrigatório!" + Environment.NewLine; iCountErros++; }

                        if (objbelinfCte.ide.toma04.enderToma.xLgr == "") { objErro.sErro = objErro.sErro + "             Logradouro Tomador do Serviço é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.ide.toma04.enderToma.nro == "") { objErro.sErro = objErro.sErro + "             Número Tomador do Serviço é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.ide.toma04.enderToma.xBairro == "") { objErro.sErro = objErro.sErro + "             Bairro Tomador do Serviço é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.ide.toma04.enderToma.cMun == "") { objErro.sErro = objErro.sErro + "             Código do Município Tomador do Serviço é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.ide.toma04.enderToma.xMun == "") { objErro.sErro = objErro.sErro + "             Nome do Município Tomador do Serviço é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.ide.toma04.enderToma.CEP != "")
                        {
                            string sCep = util.TiraSimbolo(objbelinfCte.ide.toma04.enderToma.CEP);
                            if (ValidaCep(sCep) == false) { objErro.sErro = objErro.sErro + "             CEP Tomador do Serviço Inválido!" + Environment.NewLine; iCountErros++; }
                        }
                        if (objbelinfCte.ide.toma04.enderToma.UF == "") { objErro.sErro = objErro.sErro + "             UF Tomador do Serviço é Obrigatório!" + Environment.NewLine; iCountErros++; }

                    }

                    #endregion

                    #region Emitente
                    if (objbelinfCte.emit.CNPJ == "") { objErro.sErro = objErro.sErro + "             Cnpj do Emitente é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    else
                    {
                        string sCpnj = util.TiraSimbolo(objbelinfCte.emit.CNPJ);
                        if (ValidaCnpj(sCpnj) == false) { objErro.sErro = objErro.sErro + "             Cnpj do Emitente Inválido!" + Environment.NewLine; iCountErros++; }
                    }
                    if (objbelinfCte.emit.IE == "") { objErro.sErro = objErro.sErro + "             Inscrição Estadual do Emitente é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.emit.xNome == "") { objErro.sErro = objErro.sErro + "             Razão Social ou Nome do Emitente é Obrigatório!" + Environment.NewLine; iCountErros++; }

                    if (objbelinfCte.emit.enderEmit.xLgr == "") { objErro.sErro = objErro.sErro + "             Logradouro do Emitente é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.emit.enderEmit.nro == "") { objErro.sErro = objErro.sErro + "             Número do Emitente é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.emit.enderEmit.xBairro == "") { objErro.sErro = objErro.sErro + "             Bairro do Emitente é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.emit.enderEmit.cMun == "") { objErro.sErro = objErro.sErro + "             Código do Município do Emitente é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.emit.enderEmit.xMun == "") { objErro.sErro = objErro.sErro + "             Nome do Município do Emitente é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.emit.enderEmit.CEP != "")
                    {
                        string sCep = util.TiraSimbolo(objbelinfCte.emit.enderEmit.CEP);
                        if (ValidaCep(sCep) == false) { objErro.sErro = objErro.sErro + "             CEP do Emitente Inválido!" + Environment.NewLine; iCountErros++; }
                    }
                    if (objbelinfCte.emit.enderEmit.UF == "") { objErro.sErro = objErro.sErro + "             UF do Emitente é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.emit.enderEmit.fone != "")
                    {
                        string sFone = util.TiraSimbolo(objbelinfCte.emit.enderEmit.fone);
                        if (ValidaFone(sFone) == false) { objErro.sErro = objErro.sErro + "             Telefone do Emitente Inválido!" + Environment.NewLine; iCountErros++; }
                    }
                    #endregion

                    #region Remetente
                    if (objbelinfCte.rem.CNPJ != "")
                    {
                        string sCnpj = util.TiraSimbolo(objbelinfCte.rem.CNPJ);
                        if (ValidaCnpj(sCnpj) == false) { objErro.sErro = objErro.sErro + "             CNPJ Remetente Inválido!" + Environment.NewLine; iCountErros++; }
                    }
                    else if (objbelinfCte.rem.CPF != "")
                    {
                        string sCpf = util.TiraSimbolo(objbelinfCte.rem.CPF);
                        if (ValidaCpf(sCpf) == false) { objErro.sErro = objErro.sErro + "             CPF Remetente Inválido!" + Environment.NewLine; iCountErros++; }
                    }
                    else { objErro.sErro = objErro.sErro + "             CPF ou CNPJ Remetente é Obrigatório!" + Environment.NewLine; iCountErros++; }


                    if (objbelinfCte.rem.IE == "") { objErro.sErro = objErro.sErro + "             Inscrição Estadual Remetente é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.rem.xNome == "") { objErro.sErro = objErro.sErro + "             Nome Remetente é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.rem.enderReme.xLgr == "") { objErro.sErro = objErro.sErro + "             Logradouro Remetente é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.rem.enderReme.nro == "") { objErro.sErro = objErro.sErro + "             Número Endereço Remetente é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.rem.enderReme.xBairro == "") { objErro.sErro = objErro.sErro + "             Bairro Remetente é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.rem.enderReme.cMun == "") { objErro.sErro = objErro.sErro + "             Código Município Remetente é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.rem.enderReme.xMun == "") { objErro.sErro = objErro.sErro + "             Município Remetente é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.rem.enderReme.CEP != "")
                    {
                        string sCep = util.TiraSimbolo(objbelinfCte.rem.enderReme.CEP);
                        if (ValidaCep(sCep) == false) { objErro.sErro = objErro.sErro + "             CEP Remetente Inválido!" + Environment.NewLine; iCountErros++; }
                    }
                    if (objbelinfCte.rem.enderReme.UF == "") { objErro.sErro = objErro.sErro + "             UF Remetente é Obrigatório!" + Environment.NewLine; iCountErros++; }

                    #endregion

                    #region Destinatario

                    if (objbelinfCte.dest.CNPJ != "")
                    {
                        string sCnpj = util.TiraSimbolo(objbelinfCte.dest.CNPJ);
                        if (ValidaCnpj(sCnpj) == false) { objErro.sErro = objErro.sErro + "             CNPJ Destinatário Inválido!" + Environment.NewLine; iCountErros++; }
                    }
                    else if (objbelinfCte.dest.CPF != "")
                    {
                        string sCpf = util.TiraSimbolo(objbelinfCte.dest.CPF);
                        if (ValidaCpf(sCpf) == false) { objErro.sErro = objErro.sErro + "             CPF Destinatário Inválido!" + Environment.NewLine; iCountErros++; }
                    }
                    else { objErro.sErro = objErro.sErro + "             CPF ou CNPJ Destinatário é Obrigatório!" + Environment.NewLine; iCountErros++; }

                    if (objbelinfCte.dest.xNome == "") { objErro.sErro = objErro.sErro + "             Nome Destinatário é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.dest.enderDest.xLgr == "") { objErro.sErro = objErro.sErro + "             Logradouro Destinatário é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.dest.enderDest.nro == "") { objErro.sErro = objErro.sErro + "             Número Destinatário é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.dest.enderDest.xBairro == "") { objErro.sErro = objErro.sErro + "             Bairro Destinatário é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.dest.enderDest.cMun == "") { objErro.sErro = objErro.sErro + "             Código Município Destinatário é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.dest.enderDest.xMun == "") { objErro.sErro = objErro.sErro + "             Município Destinatário é Obrigatório!" + Environment.NewLine; iCountErros++; }


                    if (objbelinfCte.dest.enderDest.CEP != "")
                    {
                        string sCep = util.TiraSimbolo(objbelinfCte.dest.enderDest.CEP);
                        if (ValidaCep(sCep) == false) { objErro.sErro = objErro.sErro + "             CEP Destinatário Inválido!" + Environment.NewLine; iCountErros++; }
                    }

                    if (objbelinfCte.dest.enderDest.UF == "") { objErro.sErro = objErro.sErro + "             UF Destinatário é Obrigatório!" + Environment.NewLine; iCountErros++; }

                    #endregion

                    #region Expedidor
                    if (objbelinfCte.exped != null)
                    {
                        if (objbelinfCte.exped.CNPJ != "")
                        {
                            string sCnpj = util.TiraSimbolo(objbelinfCte.exped.CNPJ);
                            if (ValidaCnpj(sCnpj) == false) { objErro.sErro = objErro.sErro + "             CNPJ Expedidor Inválido!" + Environment.NewLine; iCountErros++; }
                        }
                        else if (objbelinfCte.exped.CPF != "")
                        {
                            string sCpf = util.TiraSimbolo(objbelinfCte.exped.CPF);
                            if (ValidaCpf(sCpf) == false) { objErro.sErro = objErro.sErro + "             CPF Expedidor Inválido!" + Environment.NewLine; iCountErros++; }
                        }
                        else { objErro.sErro = objErro.sErro + "             CPF ou CNPJ Expedidor é Obrigatório!" + Environment.NewLine; iCountErros++; }

                        if (objbelinfCte.exped.IE == "") { objErro.sErro = objErro.sErro + "             Inscrição Estadual Expedidor é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.exped.xNome == "") { objErro.sErro = objErro.sErro + "             Nome Expedidor é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.exped.enderExped.xLgr == "") { objErro.sErro = objErro.sErro + "             Logradouro Expedidor é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.exped.enderExped.nro == "") { objErro.sErro = objErro.sErro + "             Número Endereço Expedidor é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.exped.enderExped.xBairro == "") { objErro.sErro = objErro.sErro + "             Bairro Expedidor é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.exped.enderExped.cMun == "") { objErro.sErro = objErro.sErro + "             Código Município Expedidor é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.exped.enderExped.xMun == "") { objErro.sErro = objErro.sErro + "             Município Expedidor é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.exped.enderExped.CEP != "")
                        {
                            string sCep = util.TiraSimbolo(objbelinfCte.exped.enderExped.CEP);
                            if (ValidaCep(sCep) == false) { objErro.sErro = objErro.sErro + "             CEP Expedidor Inválido!" + Environment.NewLine; iCountErros++; }
                        }
                        if (objbelinfCte.exped.enderExped.UF == "") { objErro.sErro = objErro.sErro + "             UF Expedidor é Obrigatório!" + Environment.NewLine; iCountErros++; }

                    }


                    #endregion

                    #region Recebedor
                    if (objbelinfCte.receb != null)
                    {
                        if (objbelinfCte.receb.CNPJ != "")
                        {
                            string sCnpj = util.TiraSimbolo(objbelinfCte.receb.CNPJ);
                            if (ValidaCnpj(sCnpj) == false) { objErro.sErro = objErro.sErro + "             CNPJ Recebedor Inválido!" + Environment.NewLine; iCountErros++; }
                        }
                        else if (objbelinfCte.receb.CPF != "")
                        {
                            string sCpf = util.TiraSimbolo(objbelinfCte.receb.CPF);
                            if (ValidaCpf(sCpf) == false) { objErro.sErro = objErro.sErro + "             CPF Recebedor Inválido!" + Environment.NewLine; iCountErros++; }
                        }
                        else { objErro.sErro = objErro.sErro + "             CPF ou CNPJ Recebedor é Obrigatório!" + Environment.NewLine; iCountErros++; }

                        if (objbelinfCte.receb.IE == "") { objErro.sErro = objErro.sErro + "             Inscrição Estadual Recebedor é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.receb.xNome == "") { objErro.sErro = objErro.sErro + "             Nome Recebedor é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.receb.enderReceb.xLgr == "") { objErro.sErro = objErro.sErro + "             Logradouro Recebedor é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.receb.enderReceb.nro == "") { objErro.sErro = objErro.sErro + "             Número Endereço Recebedor é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.receb.enderReceb.xBairro == "") { objErro.sErro = objErro.sErro + "             Bairro Recebedor é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.receb.enderReceb.cMun == "") { objErro.sErro = objErro.sErro + "             Código Município Recebedor é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.receb.enderReceb.xMun == "") { objErro.sErro = objErro.sErro + "             Município Recebedor é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.receb.enderReceb.CEP != "")
                        {
                            string sCep = util.TiraSimbolo(objbelinfCte.receb.enderReceb.CEP);
                            if (ValidaCep(sCep) == false) { objErro.sErro = objErro.sErro + "             CEP Recebedor Inválido!" + Environment.NewLine; iCountErros++; }
                        }
                        if (objbelinfCte.receb.enderReceb.UF == "") { objErro.sErro = objErro.sErro + "             UF Recebedor é Obrigatório!" + Environment.NewLine; iCountErros++; }

                    }


                    #endregion

                    #region Informacoes da NF

                    for (int j = 0; j < objbelinfCte.rem.infNF.Count; j++)
                    {
                        if (objbelinfCte.rem.infNF[j].nDoc == "") { objErro.sErro = objErro.sErro + "             Número NF é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.rem.infNF[j].serie == "") { objErro.sErro = objErro.sErro + "             Série NF é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.rem.infNF[j].dEmi == "") { objErro.sErro = objErro.sErro + "             Data Emissão NF é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.rem.infNF[j].vBC == "") { objErro.sErro = objErro.sErro + "             Valor da Base de Cálculo do ICMS NF é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.rem.infNF[j].vICMS == "") { objErro.sErro = objErro.sErro + "             Valor Total do ICMS NF é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.rem.infNF[j].vBCST == "") { objErro.sErro = objErro.sErro + "             Valor da Base de Cálculo do ICMS ST NF é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.rem.infNF[j].vST == "") { objErro.sErro = objErro.sErro + "             Valor Total do ICMS ST NF é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.rem.infNF[j].vProd == "") { objErro.sErro = objErro.sErro + "             Valor Total dos Produtos NF é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.rem.infNF[j].vNF == "") { objErro.sErro = objErro.sErro + "             Valor Total da NF NF é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.rem.infNF[j].nCFOP == "") { objErro.sErro = objErro.sErro + "             CFOP Predominante NF é Obrigatório!" + Environment.NewLine; iCountErros++; }

                    }
                    for (int k = 0; k < objbelinfCte.rem.infNFe.Count; k++)
                    {
                        if (objbelinfCte.rem.infNFe[k].chave == "") { objErro.sErro = objErro.sErro + "             Chave NF-e é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    }

                    #endregion

                    #region Outros Documentos
                    for (int j = 0; j < objbelinfCte.rem.infOutros.Count; j++)
                    {
                        if (objbelinfCte.rem.infOutros[j].tpDoc == "") { objErro.sErro = objErro.sErro + "             Tipo do Documento é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.rem.infOutros[j].dEmi == "") { objErro.sErro = objErro.sErro + "             Data Emissão do Documento é Obrigatório!" + Environment.NewLine; iCountErros++; }

                    }
                    #endregion

                    #region Valores

                    if (objbelinfCte.vPrest.vTPrest == "") { objErro.sErro = objErro.sErro + "             Valor Total da Prestação do Serviço é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.vPrest.vRec == "") { objErro.sErro = objErro.sErro + "             Valor a Receber é Obrigatório!" + Environment.NewLine; iCountErros++; }


                    if (objbelinfCte.imp.ICMS.ICMS00 != null)
                    {
                        if (objbelinfCte.imp.ICMS.ICMS00.vBC == "") { objErro.sErro = objErro.sErro + "             Valor da BC do ICMS é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.imp.ICMS.ICMS00.pICMS == "") { objErro.sErro = objErro.sErro + "             Alíquota do ICMS é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.imp.ICMS.ICMS00.vICMS == "") { objErro.sErro = objErro.sErro + "             Valor do ICMS é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    }
                    else if (objbelinfCte.imp.ICMS.ICMS20 != null)
                    {
                        if (objbelinfCte.imp.ICMS.ICMS20.CST == "") { objErro.sErro = objErro.sErro + "             CST é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.imp.ICMS.ICMS20.pRedBC == "") { objErro.sErro = objErro.sErro + "             Percentual de redução da BC é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.imp.ICMS.ICMS20.vBC == "") { objErro.sErro = objErro.sErro + "             Valor da BC do ICMS é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.imp.ICMS.ICMS20.pICMS == "") { objErro.sErro = objErro.sErro + "             Alíquota do ICMS é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.imp.ICMS.ICMS20.vICMS == "") { objErro.sErro = objErro.sErro + "             Valor do ICMS é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    }
                    else if (objbelinfCte.imp.ICMS.ICMS45 != null)
                    {
                        if (objbelinfCte.imp.ICMS.ICMS45.CST == "") { objErro.sErro = objErro.sErro + "             CST é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    }
                    else if (objbelinfCte.imp.ICMS.ICMS60 != null)
                    {
                        if (objbelinfCte.imp.ICMS.ICMS60.CST == "") { objErro.sErro = objErro.sErro + "             CST é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.imp.ICMS.ICMS60.vBCSTRet == "") { objErro.sErro = objErro.sErro + "             Valor da BC do ICMS Retido é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.imp.ICMS.ICMS60.vICMSSTRet == "") { objErro.sErro = objErro.sErro + "             Valor do ICMS Retido é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.imp.ICMS.ICMS60.pICMSSTRet == "") { objErro.sErro = objErro.sErro + "             Alíquota do ICMS é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    }
                    else if (objbelinfCte.imp.ICMS.ICMS90 != null)
                    {
                        if (objbelinfCte.imp.ICMS.ICMS90.CST == "") { objErro.sErro = objErro.sErro + "             CST é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.imp.ICMS.ICMS90.vBC == "") { objErro.sErro = objErro.sErro + "             Valor da BC do ICMS é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.imp.ICMS.ICMS90.pICMS == "") { objErro.sErro = objErro.sErro + "             Alíquota do ICMS é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.imp.ICMS.ICMS90.vICMS == "") { objErro.sErro = objErro.sErro + "             Valor do ICMS é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    }
                    else if (objbelinfCte.imp.ICMS.ICMSOutraUF != null)
                    {
                        if (objbelinfCte.imp.ICMS.ICMSOutraUF.CST == "") { objErro.sErro = objErro.sErro + "             CST é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.imp.ICMS.ICMSOutraUF.vBCOutraUF == "") { objErro.sErro = objErro.sErro + "             Valor da BC do ICMS é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.imp.ICMS.ICMSOutraUF.pICMSOutraUF == "") { objErro.sErro = objErro.sErro + "             Alíquota do ICMS é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.imp.ICMS.ICMSOutraUF.vICMSOutraUF == "") { objErro.sErro = objErro.sErro + "             Valor do ICMS é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    }



                    #endregion

                    #region InformacoesCarga

                    if (objbelinfCte.infCTeNorm.infCarga.vCarga == 0) { objErro.sErro = objErro.sErro + "             Valor total da mercadoria é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.infCTeNorm.infCarga.proPred == "") { objErro.sErro = objErro.sErro + "             Produto predominante é Obrigatório!" + Environment.NewLine; iCountErros++; }

                    for (int j = 0; j < objbelinfCte.infCTeNorm.infCarga.infQ.Count; j++)
                    {
                        if (objbelinfCte.infCTeNorm.infCarga.infQ[j].cUnid == "") { objErro.sErro = objErro.sErro + "             Código da Unidade de Medida é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.infCTeNorm.infCarga.infQ[j].tpMed == "") { objErro.sErro = objErro.sErro + "             Tipo da Medida é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.infCTeNorm.infCarga.infQ[j].qCarga == 0) { objErro.sErro = objErro.sErro + "             Quantidade de Produtos é Obrigatório!" + Environment.NewLine; iCountErros++; }

                    }

                    #endregion

                    #region Rodoviario


                    if (objbelinfCte.infCTeNorm.seg.respSeg == "") { objErro.sErro = objErro.sErro + "             Responsável pelo Seguro é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.infCTeNorm.rodo.dPrev == "") { objErro.sErro = objErro.sErro + "             Previsão de Entrega é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.infCTeNorm.seg.nApol == "") { objErro.sErro = objErro.sErro + "             Número da Apólice é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.infCTeNorm.rodo.RNTRC == "") { objErro.sErro = objErro.sErro + "             RNTRC é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    if (objbelinfCte.infCTeNorm.rodo.lota == "") { objErro.sErro = objErro.sErro + "             Lotação é Obrigatório!" + Environment.NewLine; iCountErros++; }

                    #endregion

                    #region Veiculo

                    if (objbelinfCte.infCTeNorm.rodo.lota == "1" || objbelinfCte.infCTeNorm.rodo.veic.Count > 0)
                    {
                        for (int v = 0; v < objbelinfCte.infCTeNorm.rodo.veic.Count; v++)
                        {
                            if (objbelinfCte.infCTeNorm.rodo.veic[v].RENAVAM == "") { objErro.sErro = objErro.sErro + "             RENAVAM do Veículo é Obrigatório!" + Environment.NewLine; iCountErros++; }
                            if (objbelinfCte.infCTeNorm.rodo.veic[v].placa == "") { objErro.sErro = objErro.sErro + "             Placa do Veículo é Obrigatório!" + Environment.NewLine; iCountErros++; }
                            if (objbelinfCte.infCTeNorm.rodo.veic[v].tara == "0") { objErro.sErro = objErro.sErro + "             Tara do Veículo é Obrigatório!" + Environment.NewLine; iCountErros++; }
                            if (objbelinfCte.infCTeNorm.rodo.veic[v].capKG == "0") { objErro.sErro = objErro.sErro + "             Capacidade do Veículo é Obrigatório!" + Environment.NewLine; iCountErros++; }
                            if (objbelinfCte.infCTeNorm.rodo.veic[v].capM3 == "0") { objErro.sErro = objErro.sErro + "             Capacidade(M3) do Veículo é Obrigatório!" + Environment.NewLine; iCountErros++; }
                            if (objbelinfCte.infCTeNorm.rodo.veic[v].tpProp == "") { objErro.sErro = objErro.sErro + "             Tipo de Proprietário do Veículo é Obrigatório!" + Environment.NewLine; iCountErros++; }
                            if (objbelinfCte.infCTeNorm.rodo.veic[v].tpVeic == "") { objErro.sErro = objErro.sErro + "             Tipo de Veículo é Obrigatório!" + Environment.NewLine; iCountErros++; }
                            if (objbelinfCte.infCTeNorm.rodo.veic[v].tpRod == "") { objErro.sErro = objErro.sErro + "             Tipo de Rodado é Obrigatório!" + Environment.NewLine; iCountErros++; }
                            if (objbelinfCte.infCTeNorm.rodo.veic[v].tpCar == "") { objErro.sErro = objErro.sErro + "             Tipo de Carroceria é Obrigatório!" + Environment.NewLine; iCountErros++; }
                            if (objbelinfCte.infCTeNorm.rodo.veic[v].UF == "") { objErro.sErro = objErro.sErro + "             Uf do Veículo é Obrigatório!" + Environment.NewLine; iCountErros++; }



                            if (objbelinfCte.infCTeNorm.rodo.veic[v].tpProp == "T")
                            {
                                if (objbelinfCte.infCTeNorm.rodo.veic[v].prop.CPFCNPJ == "") { objErro.sErro = objErro.sErro + "             Cpf/Cnpj do Dono do Veículo é Obrigatório!" + Environment.NewLine; iCountErros++; }
                                if (objbelinfCte.infCTeNorm.rodo.veic[v].prop.RNTRC == "") { objErro.sErro = objErro.sErro + "             RNTRC do Dono do Veículo é Obrigatório!" + Environment.NewLine; iCountErros++; }
                                if (objbelinfCte.infCTeNorm.rodo.veic[v].prop.xNome == "0") { objErro.sErro = objErro.sErro + "             Nome do Dono do Veículo é Obrigatório!" + Environment.NewLine; iCountErros++; }
                                if (objbelinfCte.infCTeNorm.rodo.veic[v].prop.IE == "0") { objErro.sErro = objErro.sErro + "             Inscrição Estadual do Dono do Veículo é Obrigatório!" + Environment.NewLine; iCountErros++; }
                                if (objbelinfCte.infCTeNorm.rodo.veic[v].prop.UF == "0") { objErro.sErro = objErro.sErro + "             Uf do Dono do Veículo é Obrigatório!" + Environment.NewLine; iCountErros++; }
                                if (objbelinfCte.infCTeNorm.rodo.veic[v].prop.tpProp == "") { objErro.sErro = objErro.sErro + "             Tipo de Proprietário do Dono do Veículo é Obrigatório!" + Environment.NewLine; iCountErros++; }
                            }
                        }
                    }
                    if (objbelinfCte.infCTeNorm.rodo.moto != null || objbelinfCte.infCTeNorm.rodo.lota == "1")
                    {
                        if (objbelinfCte.infCTeNorm.rodo.moto.xNome == "") { objErro.sErro = objErro.sErro + "             Nome do Motorista é Obrigatório!" + Environment.NewLine; iCountErros++; }
                        if (objbelinfCte.infCTeNorm.rodo.moto.CPF == "") { objErro.sErro = objErro.sErro + "             Cpf do Motorista é Obrigatório!" + Environment.NewLine; iCountErros++; }
                    }
                    #endregion


                    if (objErro.sErro != null)
                    {
                        objListaErroAtual.Add(objErro);
                    }
                }
                txtErros.Text = "";

                if (iCountErros > 0)
                {
                    Retorno = false;
                    StringBuilder sErro = new StringBuilder();
                    for (int i = 0; i < objListaErroAtual.Count; i++)
                    {
                        sErro.Append("Número CT-e " + objListaErroAtual[i].sNumCte + Environment.NewLine);
                        sErro.Append(objListaErroAtual[i].sErro + Environment.NewLine);
                    }
                    txtErros.Text = sErro.ToString();
                }

                lblErro.Text = "Erros Encontrados: " + iCountErros;


                return Retorno;
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, _sMessageException + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                return false;
            }
        }

        private bool ValidaCnpj(string sCnpj)
        {
            Match valida = Regex.Match(sCnpj, @"^[0-9]{14}$");
            if (!valida.Success) { return false; } else { return true; }
        }
        private bool ValidaCpf(string sCpf)
        {
            Match valida = Regex.Match(sCpf, @"^[0-9]{11}$");
            if (!valida.Success) { return false; } else { return true; }
        }
        private bool ValidaCep(string sCep)
        {
            Match valida = Regex.Match(sCep, @"^[0-9]{8}$");
            if (!valida.Success) { return false; } else { return true; }
        }
        private bool ValidaFone(string sFone)
        {
            Match valida = Regex.Match(sFone, @"^[0-9]{7,12}$");
            if (!valida.Success) { return false; } else { return true; }
        }
        #endregion

        private void NumericUpDown_Enter(object sender, EventArgs e)
        {
            KryptonNumericUpDown nud = (KryptonNumericUpDown)sender;
            nud.Select(0, nud.ToString().Length);
        }
        private void SomenteNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void frmVisualizaCte_Load(object sender, EventArgs e)
        {

        }

        private void cbotpProp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbotpProp.SelectedIndex == 1)
                {
                    flpPropVeiculo.Enabled = true;
                }
                else
                {
                    flpPropVeiculo.Enabled = false;
                    txtCPFprop.Text = "";
                    txtRNTRCprop.Text = "";
                    txtxNomeprop.Text = "";
                    txtIEprop.Text = "";
                    txtUFprop.Text = "";
                    cbotpPropprop.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, _sMessageException + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            }
        }
        private void cboCST_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboCST.SelectedIndex != -1)
                {
                    HabilitaCamposValores(cboCST.SelectedIndex);
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, _sMessageException + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            }
        }

        private void frmVisualizaCte_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bCancela == false && bEnviar == false)
            {
                if (KryptonMessageBox.Show("Nenhum Conhecimento será Enviado. Deseja realmente Sair?", "A L E R T A", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    bCancela = true;
                    this.Close();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }



        private void btnVeiculo_Click(object sender, EventArgs e)
        {
            if (bsVeiculos.Count > 0)
            {
                ToolStripButton btn = (ToolStripButton)sender;
                SalvaAlteracaoVeiculo();
                switch (btn.Name)
                {
                    case "btnProximoVeiculo":
                        bsVeiculos.MoveNext();
                        break;

                    case "btnAnteriorVeiculo":
                        bsVeiculos.MovePrevious();
                        break;

                    case "btnUltimoVeiculo":
                        bsVeiculos.MoveLast();
                        break;

                    case "btnPrimeiroVeiculo":
                        bsVeiculos.MoveFirst();
                        break;

                }
                PopulaVeiculo();
                VerificaCamposObrigatoriosVeiculo(true, "Campo Obrigatório!");
                lblTotalVeiculo.Text = (bsVeiculos.IndexOf(bsVeiculos.Current) + 1).ToString() + " de " + (bsVeiculos.Count.ToString());
            }
        }
        private void PopulaVeiculo()
        {
            try
            {
                belveic veic = (belveic)bsVeiculos.Current;
                txtRENAVAM.Text = veic.RENAVAM;
                txtplaca.Text = veic.placa;
                nudtara.Value = Convert.ToInt32(veic.tara);
                nudcapKG.Value = Convert.ToInt32(veic.capKG);
                nudcapM3.Value = Convert.ToInt32(veic.capM3);
                switch (veic.tpProp)
                {
                    case "P":
                        cbotpProp.SelectedIndex = 0;
                        break;
                    case "T":
                        cbotpProp.SelectedIndex = 1;
                        break;
                    default:
                        cbotpProp.SelectedIndex = -1;
                        break;
                }
                cbotpVeic.SelectedIndex = veic.tpVeic != "" ? Convert.ToInt32(veic.tpVeic) : -1;
                cbotpRod.SelectedIndex = veic.tpRod != "" ? Convert.ToInt32(veic.tpRod) : -1;
                cbotpCar.SelectedIndex = veic.tpCar != "" ? Convert.ToInt32(veic.tpCar) : -1;
                txtUF.Text = veic.UF;

                if (veic.prop != null)
                {
                    txtCPFprop.Text = veic.prop.CPFCNPJ;
                    txtRNTRCprop.Text = veic.prop.RNTRC;
                    txtxNomeprop.Text = veic.prop.xNome;
                    txtIEprop.Text = veic.prop.IE;
                    txtUFprop.Text = veic.prop.UF;
                    cbotpPropprop.SelectedIndex = Convert.ToInt32(veic.prop.tpProp);
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, _sMessageException + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            }
        }
        private void SalvaAlteracaoVeiculo()
        {
            try
            {
                belveic veic = (belveic)bsVeiculos.Current;

                veic.RENAVAM = txtRENAVAM.Text;
                veic.placa = txtplaca.Text;
                veic.tara = nudtara.Value.ToString();
                veic.capKG = nudcapKG.Value.ToString();
                veic.capM3 = nudcapM3.Value.ToString();
                switch (cbotpProp.SelectedIndex)
                {
                    case 0:
                        veic.tpProp = "P";
                        break;
                    case 1:
                        veic.tpProp = "T";
                        break;
                    default:
                        veic.tpProp = "";
                        break;
                }
                veic.tpVeic = cbotpVeic.SelectedIndex != -1 ? cbotpVeic.SelectedIndex.ToString() : "";
                veic.tpRod = cbotpRod.SelectedIndex != -1 ? "0" + cbotpRod.SelectedIndex.ToString() : "";
                veic.tpCar = cbotpCar.SelectedIndex != -1 ? "0" + cbotpCar.SelectedIndex.ToString() : "";
                veic.UF = txtUF.Text;

                if (veic.tpProp == "T")
                {
                    veic.prop = new belprop();
                    veic.prop.CPFCNPJ = txtCPFprop.Text;
                    veic.prop.RNTRC = txtRNTRCprop.Text;
                    veic.prop.xNome = txtxNomeprop.Text;
                    veic.prop.IE = txtIEprop.Text;
                    veic.prop.UF = txtUFprop.Text;
                    veic.prop.tpProp = cbotpPropprop.SelectedIndex != -1 ? cbotpCar.SelectedIndex.ToString() : "";
                }
                else
                {
                    veic.prop = null;
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(null, _sMessageException + (ex.InnerException != null ? ex.InnerException.Message : ex.Message).ToString(), "CT-e - AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            }
        }









    }
}
