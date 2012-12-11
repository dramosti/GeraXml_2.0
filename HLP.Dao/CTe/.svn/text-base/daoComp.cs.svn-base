using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HLP.bel.CTe;
using FirebirdSql.Data.FirebirdClient;
using System.IO;
using HLP.bel.Static;

namespace HLP.Dao.CTe
{
    public class daoComp
    {
        public void PopulaComp(belinfCte objbelinfCte, FbConnection conn, string sCte)
        {
            StringBuilder sQuery = new StringBuilder();

            sQuery.Append("select  conhecim.ds_obs from conhecim ");
            sQuery.Append("where conhecim.nr_lanc = '" + sCte + "' and conhecim.cd_empresa = '" + belStatic.CodEmpresaCte + "' ");

            FbCommand comando = new FbCommand(sQuery.ToString(), conn);
            FbDataReader Reader = comando.ExecuteReader();
            Byte[] blob = null;
            MemoryStream ms = new MemoryStream();
            string texto = "";
            while (Reader.Read())
            {
                blob = new Byte[(Reader.GetBytes(0, 0, null, 0, int.MaxValue))];
                try
                {
                    Reader.GetBytes(0, 0, blob, 0, blob.Length);
                }
                catch
                {
                    texto = "";
                }
                ms = new MemoryStream(blob);
            }
            StreamReader Ler = new StreamReader(ms);
            Ler.ReadLine();
            while (Ler.Peek() != -1)
            {
                texto += Ler.ReadLine();
            }

            belcompl objcomp = null;
            if (texto != "")
            {
                objcomp = new belcompl();
                objcomp.ObsCont.Xcampo = "OBSERVACAO";
                objcomp.ObsCont.Xtexto = texto;
            }
            objbelinfCte.compl = objcomp;
        }
    }
}

