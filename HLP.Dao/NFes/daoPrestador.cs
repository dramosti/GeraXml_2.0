using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HLP.bel.NFes;
using FirebirdSql.Data.FirebirdClient;
using HLP.bel;
using HLP.bel.Static;

namespace HLP.Dao.NFes
{
    public class daoPrestador
    {
        tcIdentificacaoPrestador objtcIdentificacaoPrestador;

        public tcIdentificacaoPrestador RettcIdentificacaoPrestador(FbConnection Conn, string sNota)
        {
            try
            {
                StringBuilder sQuery = new StringBuilder();
                sQuery.Append(" select empresa.cd_cgc, empresa.cd_inscrmu from empresa ");
                sQuery.Append(" where empresa.cd_empresa = '" + belStatic.codEmpresaNFe + "'");

                FbCommand Comand = new FbCommand(sQuery.ToString(), Conn);
                if (Conn.State == System.Data.ConnectionState.Closed)
                {
                    Conn.Open();
                }
                Comand.ExecuteNonQuery();
                FbDataReader dr = Comand.ExecuteReader();
                dr.Read();
                objtcIdentificacaoPrestador = new tcIdentificacaoPrestador();

                if (dr["cd_cgc"] != null)
                {
                    objtcIdentificacaoPrestador.Cnpj = dr["cd_cgc"].ToString();
                }
                else
                {
                    throw new Exception("Prestador cadastrado sem CNPJ, Item é obrigatório!");
                }
                if (dr["cd_inscrmu"] != null)
                {
                    objtcIdentificacaoPrestador.InscricaoMunicipal = dr["cd_inscrmu"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally { Conn.Close(); }


            return objtcIdentificacaoPrestador;
        }

        public string RetPrestadorEmail()
        {
            belConnection cx = new belConnection();

            try
            {
                string sMsgPadraoEmail = "{5}Razão Social:{1}{0}{5}E-mail: {2}{0}{5}CCM :{3}{0}{5}CNPJ:{4}{0}{0}";
                StringBuilder sQuery = new StringBuilder();
                sQuery.Append(" select empresa.cd_cgc, empresa.cd_inscrmu, empresa.cd_email, empresa.nm_empresa from empresa ");
                sQuery.Append(" where empresa.cd_empresa = '" + belStatic.codEmpresaNFe + "'");
                FbCommand Comand = new FbCommand(sQuery.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                Comand.ExecuteNonQuery();
                FbDataReader dr = Comand.ExecuteReader();
                dr.Read();
                objtcIdentificacaoPrestador = new tcIdentificacaoPrestador();

                return string.Format(sMsgPadraoEmail, "<br>",
                                            dr["nm_empresa"].ToString(),
                                            dr["cd_email"].ToString(),
                                            dr["cd_inscrmu"].ToString(),
                                            dr["cd_cgc"].ToString(), "    ");
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cx.Close_Conexao();
            }
        }


    }
}
