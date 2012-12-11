using System;
using System.Collections.Generic;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using System.Data;
using System.Linq;
using HLP.bel.NFe.GeraXml;
using HLP.bel.Static;


namespace HLP.bel
{
    public class belDuplicata : HLP.bel.IbelDuplicata
    {
        private string _empresa;

        public string Empresa
        {
            get { return _empresa; }
            set { _empresa = value; }
        }
        private string _nrseq;

        public string Nrseq
        {
            get { return _nrseq; }
            set { _nrseq = value; }
        }
        private string _numerodocumento;

        public string Numerodocumento
        {
            get { return _numerodocumento; }
            set { _numerodocumento = value; }
        }
        private DateTime _vencto;

        public DateTime Vencto
        {
            get { return _vencto; }
            set { _vencto = value; }
        }

        private string _cdduplic;

        public string Cdduplic
        {
            get { return _cdduplic; }
            set { _cdduplic = value; }
        }


       public struct NotasDuplicatas
        {
            public string sNF { get; set; }
            public string sNrDoc { get; set; }
        }

        public void BuscaVencto()
        {
            string ssGravarCdDupli = string.Empty;
            using (FbCommand cmd = new FbCommand("Select control.cd_conteud from control where cd_nivel = '1355'", Conn))
            {

                if (ConnectionState.Open != Conn.State)
                {
                    Conn.Open();
                }

                ssGravarCdDupli = cmd.ExecuteScalar().ToString();
                Conn.Close();
            }

            string ssGravardteminf = string.Empty;
            using (FbCommand cmd = new FbCommand("Select control.cd_conteud from control where cd_nivel = '1363'", Conn))
            {

                if (ConnectionState.Open != Conn.State)
                {
                    Conn.Open();
                }
                ssGravardteminf = cmd.ExecuteScalar().ToString();
                Conn.Close();
            }
            
            string sNMCliente = belStatic.sNomeEmpresa;
            if (ssGravarCdDupli == "S")
            {
                StringBuilder sSqlTeste = new StringBuilder();
                //Campos do Select
                sSqlTeste.Append("Select ");
                sSqlTeste.Append("doc_ctr.nr_doc, ");
                sSqlTeste.Append("doc_ctr.cd_documento, ");
                sSqlTeste.Append("doc_ctr.cd_dupli, ");
                sSqlTeste.Append("'S' Nota ");
                //Tabela
                sSqlTeste.Append("From Doc_ctr ");


                //Where
                sSqlTeste.Append("Where ");
                sSqlTeste.Append("(doc_ctr.cd_empresa ='");
                sSqlTeste.Append(_empresa);
                sSqlTeste.Append("') ");
                sSqlTeste.Append("and ");
                sSqlTeste.Append("(doc_ctr.cd_nfseq = '");
                sSqlTeste.Append(_nrseq);
                sSqlTeste.Append("') ");

                #region MASTERFEW
                if (sNMCliente == "MASTERFEW")
                {
                    StringBuilder sPedseq = new StringBuilder();
                    sPedseq.Append("Select ");
                    sPedseq.Append("pedseq.cd_pedido, ");
                    sPedseq.Append("pedseq.cd_seqped ");
                    sPedseq.Append("from pedseq ");
                    sPedseq.Append("where ");
                    sPedseq.Append("(pedseq.cd_empresa = '");
                    sPedseq.Append(_empresa);
                    sPedseq.Append("') ");
                    sPedseq.Append("and ");
                    sPedseq.Append("(pedseq.cd_nfseq = '");
                    sPedseq.Append(_nrseq);
                    sPedseq.Append("') ");
                    //Claudinei - o.s. sem - 15/03/2010
                    if (ConnectionState.Open != Conn.State)
                    {
                        Conn.Open();
                    }
                    //Fim - Claudinei - o.s. sem - 15/03/2010
                    FbCommand cmdPedseq = new FbCommand(sPedseq.ToString(), Conn);
                    cmdPedseq.ExecuteNonQuery();

                    FbDataReader drPedseq = cmdPedseq.ExecuteReader();

                    drPedseq.Read();
                    int iSeq = Convert.ToInt16(drPedseq["cd_seqped"]);
                    iSeq++;

                    //União
                    sSqlTeste.Append("Union ");
                    //Campos do Select
                    sSqlTeste.Append("Select ");
                    sSqlTeste.Append("doc_ctr.nr_doc, ");
                    sSqlTeste.Append("doc_ctr.cd_documento, ");
                    sSqlTeste.Append("doc_ctr.cd_dupli, ");
                    sSqlTeste.Append("'N' Nota ");
                    //Tabela
                    sSqlTeste.Append("From Doc_ctr ");
                    //Where
                    sSqlTeste.Append("Where ");
                    sSqlTeste.Append("(doc_ctr.cd_empresa ='");
                    sSqlTeste.Append(_empresa);
                    sSqlTeste.Append("') ");
                    sSqlTeste.Append("and ");
                    sSqlTeste.Append("(doc_ctr.CD_PEDIDO = '");
                    sSqlTeste.Append(drPedseq["cd_pedido"].ToString());
                    sSqlTeste.Append("') ");
                    sSqlTeste.Append("and ");
                    sSqlTeste.Append("(doc_ctr.CD_SEQPED = '");
                    sSqlTeste.Append(iSeq.ToString().PadLeft(2, '0'));
                    sSqlTeste.Append("') ");
                }
                #endregion
                
                sSqlTeste.Append("order by 1 ");
                //Clauidnei - o.s. 23801 - 04/11/2009

                if (ConnectionState.Open != Conn.State)
                {
                    Conn.Open();
                }
                FbCommand cmdEmit = new FbCommand(sSqlTeste.ToString(), Conn);
                cmdEmit.ExecuteNonQuery();

                FbDataReader drEmit = cmdEmit.ExecuteReader();
                int iParcela = 0;
                int iSemNota = 0;
                List<NotasDuplicatas> objListNfs = new List<NotasDuplicatas>();
                NotasDuplicatas objNFs = new NotasDuplicatas();
                while (drEmit.Read())
                {
                    objNFs = new NotasDuplicatas();
                    
                    if (!(belStatic.sNomeEmpresa.Equals("LORENZON")))
                    {
                        if (drEmit["NOTA"].ToString() == "S")
                        {
                            iParcela++;
                            objNFs.sNF = string.Format("{0}{1}",
                                                _cdnotafis.Trim(),
                                                Convert.ToChar((64 + iParcela)));
                            objNFs.sNrDoc = drEmit["NR_DOC"].ToString();
                        }
                        else
                        {
                            iSemNota++;
                            objNFs.sNF = string.Format("{0}/{1}",
                                                Convert.ToInt64(_cdnotafis).ToString().PadLeft(5, '0'),
                                                iSemNota.ToString().Trim());
                            objNFs.sNrDoc = drEmit["NR_DOC"].ToString();

                        }
                        objListNfs.Add(objNFs);
                    }
                    else
                    {
                        if (drEmit["cd_dupli"].ToString().Contains("/NUM"))
                        {
                            if (drEmit["NOTA"].ToString() == "S")
                            {
                                iParcela++;
                                objNFs.sNF = string.Format("{0}{1}",
                                                    _cdnotafis.Trim(),
                                                    Convert.ToChar((64 + iParcela)));
                                objNFs.sNrDoc = drEmit["NR_DOC"].ToString();
                            }
                            else
                            {
                                iSemNota++;
                                objNFs.sNF = string.Format("{0}/{1}",
                                                    Convert.ToInt64(_cdnotafis).ToString().PadLeft(5, '0'),
                                                    iSemNota.ToString().Trim());
                                objNFs.sNrDoc = drEmit["NR_DOC"].ToString();

                            }
                            objListNfs.Add(objNFs);
                        }
                    }
                }

                if (objListNfs.Count == 1)
                {
                    objNFs = objListNfs[0];
                    objNFs.sNF = objListNfs[0].sNF.Replace("A", "").PadLeft(7, '0'); //Claudinei - o.s. 24103 - 05/02/2010
                    objListNfs = new List<NotasDuplicatas>();
                    objListNfs.Add(objNFs);
                }

                for (int i = 0; i < objListNfs.Count; i++)
                {

                    StringBuilder sSql = new StringBuilder();

                    sSql.Append("update ");
                    sSql.Append("doc_ctr ");
                    sSql.Append("set cd_dupli = '");
                    sSql.Append(objListNfs[i].sNF);
                    sSql.Append("' Where ");
                    sSql.Append("(cd_empresa = '");
                    sSql.Append(_empresa);
                    sSql.Append("')");
                    sSql.Append(" and ");
                    sSql.Append("(nr_doc = '");
                    sSql.Append(objListNfs[i].sNrDoc);
                    sSql.Append("')");

                    using (FbCommand cmd = new FbCommand(sSql.ToString(), Conn))
                    {
                        if (Conn.State != ConnectionState.Open)
                        {
                            Conn.Open();
                        }

                        cmd.ExecuteNonQuery();
                    }

                }
            }

        }

        private string _cdnotafis;

        public string Cdnotafis
        {
            get { return _cdnotafis; }
            set { _cdnotafis = value; }
        }

        private string _nrdoc;

        public string Nrdoc
        {
            get { return _nrdoc; }
            set { _nrdoc = value; }
        }

        private DateTime _dtemi;

        public DateTime Dtemi
        {
            get { return _dtemi; }
            set { _dtemi = value; }
        }
        private FbConnection _conn;

        public FbConnection Conn
        {
            get { return _conn; }
            set { _conn = value; }
        }

    }
}
