using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using HLP.bel.NFes;
using HLP.bel;
using HLP.bel.Static;

namespace HLP.Dao.NFes
{
    public class daoIntermediarioServico
    {
        TcIdentificacaoIntermediarioServico objTcIdentificacaoIntermediarioServico;
        public TcIdentificacaoIntermediarioServico RettcIdentificacaoIntermediarioServico(FbConnection Conn, String sNota)
        {
            try
            {
                

                StringBuilder sQuery = new StringBuilder();
                sQuery.Append("select empresa.cd_cgc, empresa.cd_inscrmu, empresa.nm_empresa RazaoSocial from empresa ");
                sQuery.Append(" where empresa.cd_empresa = '" + belStatic.codEmpresaNFe + "'");

                FbCommand command = new FbCommand(sQuery.ToString(), Conn);
                command.ExecuteNonQuery();
                FbDataReader dr = command.ExecuteReader();
                dr.Read();

                objTcIdentificacaoIntermediarioServico = new TcIdentificacaoIntermediarioServico();
                objTcIdentificacaoIntermediarioServico.CpfCnpj = new TcCpfCnpj();
                                
                if (dr["cd_cgc"] != null)
                {
                    objTcIdentificacaoIntermediarioServico.CpfCnpj.Cnpj = dr["cd_cgc"].ToString();
                }
                else
                {
                    throw new Exception("Intermediário cadastrado sem CNPJ, Item é obrigatório!");
                }
                if (dr["cd_inscrmu"] != null) { objTcIdentificacaoIntermediarioServico.InscricaoMunicipal = dr["cd_inscrmu"].ToString(); }
                if (dr["RazaoSocial"] != null) { objTcIdentificacaoIntermediarioServico.RazaoSocial = dr["RazaoSocial"].ToString(); }

                return objTcIdentificacaoIntermediarioServico;
            }
            catch (Exception ex)
            {                
                throw;
            }
            
        }

    }
}
