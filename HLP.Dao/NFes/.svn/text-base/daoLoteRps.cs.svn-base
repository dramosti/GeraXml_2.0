using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HLP.bel;
using HLP.bel.NFes;
using FirebirdSql.Data.FirebirdClient;
using HLP.bel.Static;
using HLP.bel.CTe;

namespace HLP.Dao.NFes
{
    public class daoLoteRps
    {
        tcLoteRps objLoteRps;

        public daoLoteRps() { }

        public tcLoteRps BuscaDadosNFes(List<string> sListaNotas)
        {
            belConnection cx = new belConnection();            
            try
            {                
                objLoteRps = new tcLoteRps();

                objLoteRps.Rps = new List<TcRps>();
                foreach (string sNota in sListaNotas)
                {
                    TcRps objTcRps = new TcRps();

                    //IdentificacaoRps - TcIdentificacaoRps
                    daotcIdentificacaoRps objdaotcIdentificacaoRps = new daotcIdentificacaoRps();
                    objTcRps.InfRps = new TcInfRps();

                    StringBuilder sQuery = new StringBuilder();
                    sQuery.Append("SELECT   coalesce (tpdoc.cd_natureza_oper_nfse,'1')cd_natureza_oper_nfse , ");
                    sQuery.Append("coalesce (empresa.st_simples,'')st_simples , ");
                    sQuery.Append("coalesce (empresa.cd_regime_trib_especial,'0')RegimeEspecialTributacao , ");
                    sQuery.Append("coalesce (empresa.st_insentivador_cultural,'N')st_insentivador_cultural from nf ");
                    sQuery.Append("inner join tpdoc on nf.cd_tipodoc = tpdoc.cd_tipodoc ");
                    sQuery.Append("inner join empresa on empresa.cd_empresa = nf.cd_empresa ");
                    sQuery.Append(" where nf.cd_nfseq = '" + sNota + "' and ");
                    sQuery.Append(" nf.cd_empresa = '" + belStatic.codEmpresaNFe + "'");

                    FbCommand Command = new FbCommand(sQuery.ToString(), cx.get_Conexao());
                    cx.Open_Conexao();
                    Command.ExecuteNonQuery();
                    FbDataReader dr = Command.ExecuteReader();
                    dr.Read();

                    
                    objTcRps.InfRps.DataEmissao = HLP.Util.Util.GetDateServidor();
                    objTcRps.InfRps.NaturezaOperacao = Convert.ToInt16(dr["cd_natureza_oper_nfse"].ToString());

                    objTcRps.InfRps.OptanteSimplesNacional = (dr["st_simples"].ToString().Equals("S") ? 1 : 2);
                    objTcRps.InfRps.IncentivadorCultural = (dr["st_insentivador_cultural"].ToString().Equals("S")?1:2);

                    objTcRps.InfRps.Status = 1;//Normal;

                    if (objTcRps.InfRps.OptanteSimplesNacional == 1)
                    {
                        objTcRps.InfRps.RegimeEspecialTributacao = Convert.ToInt16(dr["RegimeEspecialTributacao"].ToString());
                    }
                    else
                    {
                        objTcRps.InfRps.RegimeEspecialTributacao = 0;
                    }


                    objTcRps.InfRps.IdentificacaoRps = objdaotcIdentificacaoRps.BuscatcIdentificacaoRps(cx.get_Conexao(), sNota);


                    //RpsSubstituido - TcIdentificacaoRps // Método tratado na visualização da Nota;
                    //daoRpsSubstituido objdaoRpsSubstituido = new daoRpsSubstituido();
                    //objTcRps.InfRps.RpsSubstituido = objdaoRpsSubstituido.RetornaIdentificacaoRpds(Conn, sNota);

                    //Servico - TcDadosServico
                    daoServico objdaoServico = new daoServico();
                    objTcRps.InfRps.Servico = objdaoServico.RetornaDadosServico(cx.get_Conexao(), sNota, objTcRps.InfRps.NaturezaOperacao);


                    //Prestador - tcIdentificacaoPrestador
                    daoPrestador objdaoPrestador = new daoPrestador();
                    objTcRps.InfRps.Prestador = objdaoPrestador.RettcIdentificacaoPrestador(cx.get_Conexao(), sNota);
                    objLoteRps.Cnpj = objTcRps.InfRps.Prestador.Cnpj; // Tag Pai;
                    objLoteRps.InscricaoMunicipal = objTcRps.InfRps.Prestador.InscricaoMunicipal; //Tag Pai;


                    //Tomador - TcDadosTomador
                    daoTomador objdaoTomador = new daoTomador();
                    objTcRps.InfRps.Tomador = objdaoTomador.RettcDadosTomador(cx.get_Conexao(), sNota);

                    //IntermediarioServico - tcIdentificacaoIntermediarioServico  //Tratado na visualização da Nota
                    //daoIntermediarioServico objdaoIntermediarioServico = new daoIntermediarioServico();
                    //objTcRps.InfRps.IntermediarioServico = objdaoIntermediarioServico.RettcIdentificacaoIntermediarioServico(Conn, sNota);

                    //ConstrucaoCivil - TcDadosContrucaoCivil - Tratado na Visualização da Nota
                    if (belStatic.sNomeEmpresa.Equals("AENGE"))
                    {
                        daoConstrucaoCivil objdaoConstrucaoCivil = new daoConstrucaoCivil();
                        objTcRps.InfRps.ConstrucaoCivil = objdaoConstrucaoCivil.RettcDadosConstrucaoCivil(cx.get_Conexao(), sNota);                       
                    }
                    objLoteRps.Rps.Add(objTcRps);
                }
                daoUtil objdaoUtil = new daoUtil();
                objLoteRps.NumeroLote = objdaoUtil.RetornaProximoValorGenerator("GEN_LOTE_NFES", 15);
                objLoteRps.QuantidadeRps = objLoteRps.Rps.Count;

                return objLoteRps;
            }
            catch (Exception ex)
            {              
                throw ex;
            }
            finally
            {
                cx.Close_Conexao();
            }
        }

    }
}
