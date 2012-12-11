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
    public class daoTomador
    {
        tcDadosTomador objtcDadosTomador;
        public tcDadosTomador RettcDadosTomador(FbConnection Conn, String sNota)
        {
            try
            {
                StringBuilder sQuery = new StringBuilder();
                sQuery.Append(" Select ");
                sQuery.Append("clifor.cd_cgc, ");
                sQuery.Append("clifor.cd_cpf, ");
                sQuery.Append("coalesce(clifor.cd_inscrmu,'') cd_inscrmu, ");
                sQuery.Append("clifor.nm_clifor RazaoSocial, ");
                sQuery.Append("clifor.ds_endnor Endereco, ");
                sQuery.Append("clifor.nr_endnor Numero, ");
                sQuery.Append("clifor.nm_bairronor Bairro, ");
                sQuery.Append("cidades.cd_municipio  CodigoMunicipio, ");
                sQuery.Append("clifor.cd_ufnor  Uf, ");
                sQuery.Append("clifor.cd_cepnor Cep, ");
                sQuery.Append("clifor.cd_fonenor Telefone, ");
                sQuery.Append("clifor.cd_email Email ");
                sQuery.Append(" from  nf inner join clifor on nf.cd_clifor = clifor.cd_clifor");
                sQuery.Append(" left join cidades on (cidades.nm_cidnor = clifor.nm_cidnor) ");
                sQuery.Append(" where nf.cd_nfseq = '" + sNota + "' and ");
                sQuery.Append(" nf.cd_empresa = '" + belStatic.codEmpresaNFe + "'");

                FbCommand Command = new FbCommand(sQuery.ToString(), Conn);
                Conn.Open();
                Command.ExecuteNonQuery();
                FbDataReader dr = Command.ExecuteReader();
                dr.Read();

                objtcDadosTomador = new tcDadosTomador();

                #region tcIdentificacaoTomador
                objtcDadosTomador.IdentificacaoTomador = new tcIdentificacaoTomador();
                if ((dr["cd_cgc"] != null) || (dr["cd_cpf"] != null))
                {
                    objtcDadosTomador.IdentificacaoTomador.CpfCnpj = new TcCpfCnpj();
                    if ((dr["cd_cgc"].ToString() != ""))
                    {
                        objtcDadosTomador.IdentificacaoTomador.CpfCnpj.Cnpj = dr["cd_cgc"].ToString();
                    }
                    else
                    {
                        objtcDadosTomador.IdentificacaoTomador.CpfCnpj.Cpf = dr["cd_cpf"].ToString();
                    }
                }
                if (dr["cd_inscrmu"] != null)
                {
                    objtcDadosTomador.IdentificacaoTomador.InscricaoMunicipal = dr["cd_inscrmu"].ToString();
                }
                #endregion

                objtcDadosTomador.RazaoSocial = dr["RazaoSocial"].ToString();

                #region TcEndereco
                objtcDadosTomador.Endereco = new TcEndereco();
                if (dr["Endereco"] != null) { objtcDadosTomador.Endereco.Endereco = dr["Endereco"].ToString(); }
                if (dr["Numero"] != null) { objtcDadosTomador.Endereco.Numero = dr["Numero"].ToString(); }
                if (dr["Bairro"] != null) { objtcDadosTomador.Endereco.Bairro = dr["Bairro"].ToString(); }
                if (dr["CodigoMunicipio"] != null) { objtcDadosTomador.Endereco.CodigoMunicipio = Convert.ToInt32(dr["CodigoMunicipio"].ToString()); }
                if (dr["Uf"] != null) { objtcDadosTomador.Endereco.Uf = dr["Uf"].ToString(); }
                if (dr["Cep"] != null) { objtcDadosTomador.Endereco.Cep = Util.Util.TiraSimbolo(dr["Cep"].ToString(), ""); }
                #endregion

                #region TcContato
                objtcDadosTomador.Contato = new TcContato();
                if (dr["Telefone"] != null) { objtcDadosTomador.Contato.Telefone = dr["Telefone"].ToString(); }
                if (dr["Email"] != null) { objtcDadosTomador.Contato.Email = dr["Email"].ToString(); }


                #endregion

                return objtcDadosTomador;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { Conn.Close(); }

        }
    }
}
