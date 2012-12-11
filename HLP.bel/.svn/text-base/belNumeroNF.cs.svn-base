using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HLP.Dao;
using FirebirdSql.Data.FirebirdClient;
using System.Data;

namespace HLP.bel
{
    public class belNumeroNF 
    {
        private string _nfseq;

        public string Nfseq
        {
            get { return _nfseq; }
            set { _nfseq = value; }
        }
        private string _cdnotafis;

        public string Cdnotafis
        {
            get { return _cdnotafis; }
            set { _cdnotafis = value; }
        }

        public bool  bNfSeqValida { get; set; }

        public List<belNumeroNF> GeraNumeroNF(List<string> lsNFSEq, string sProximaNF, string sEmpresa)
        {
            StringBuilder sSql = new StringBuilder();

            sSql.Append("select ");
            sSql.Append("nf.cd_nfseq ");
            sSql.Append("From nf ");
            sSql.Append("where ");
            sSql.Append("((nf.cd_notafis is null) or (nf.cd_notafis = '')) and (");
            sSql.Append("nf.cd_empresa ='");
            sSql.Append(sEmpresa);
            sSql.Append("') and (");
            sSql.Append("nf.cd_nfseq in('");
            int iCont = 0;
            foreach (var sNfseq in lsNFSEq)
            {
                iCont++;
                sSql.Append(sNfseq);
                if (lsNFSEq.Count > iCont)
                {
                    sSql.Append("','");
                }

                
            }
            sSql.Append("')) ");
            sSql.Append("Order by nf.cd_empresa, nf.cd_nfseq ");

            //Claudinei - o.s. sem - 11/03/2010
            belGerarXML BuscaConexao = new belGerarXML();

            FbConnection Conn = BuscaConexao.Conn;

            if (Conn.State != ConnectionState.Open)
            {
                Conn.Open();
            }
            //Fim - Claudinei - o.s. sem - 11/03/2010
            FbCommand cmd = new FbCommand(sSql.ToString(), Conn);
            cmd.ExecuteNonQuery();

            FbDataReader dr = cmd.ExecuteReader();


            Int32 iNumeroNF = Convert.ToInt32(sProximaNF);

            List<belNumeroNF> objNumeroNFs = new List<belNumeroNF>();
            //dr = AcessoDados.ExecuteReader(sSql.ToString(), CommandType.Text);                  
            

            while (dr.Read())
            {
                
                belNumeroNF objNumeroNF = new belNumeroNF();
                objNumeroNF._nfseq = dr["cd_nfseq"].ToString();
                objNumeroNF.Cdnotafis = iNumeroNF.ToString().PadLeft(6, '0');
                objNumeroNFs.Add(objNumeroNF);

                iNumeroNF++;

            }

            return objNumeroNFs;
            
        }
    }
}
