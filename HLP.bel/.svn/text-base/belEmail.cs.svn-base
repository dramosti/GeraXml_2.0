using System;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using System.Xml;
using System.Text.RegularExpressions;
//Danner - o.s. SEM - 05/11/2009
namespace HLP.bel
{
    public class belEmail
    {

        public bool _envia { get; set; }
        public string _sSeq { get; set; }


        /// <summary>
        /// Endereço de e-mail da pessoa que está mandando.
        /// </summary>
        private string _de;
        /// <summary>
        /// Senha do e-Mail Remetente.
        /// </summary>
        private string _senha;
        /// <summary>
        /// Endereço de e-mail da pessoa que está enviando.
        /// </summary>
        public string _para;

        /// <summary>
        /// Endereço de e-mail do Transportador
        /// </summary>
        public string _paraTransp;

        /// <summary>
        /// Corpo do e-Mail.
        /// </summary>
        private string _corpo;
        /// <summary>
        /// Endereço aonde se localiza o anexo á ser enviado.
        /// </summary>
        private string _anexo;
        /// <summary>
        /// Endereço aonde se localiza o anexo á ser enviado.
        /// </summary>
        private string _anexo2;
        /// <summary>
        /// Assunto do email.
        /// </summary>
        private string _assunto;
        /// <summary>
        /// Host do servidor de e-Mail.
        /// </summary>
        private string _host;
        /// <summary>
        /// Porta do servidor.
        /// </summary>
        private int _porta;
        /// <summary>
        /// Razão Social do Emitente.
        /// </summary>
        private string _razaoemit;
        /// <summary>
        /// Variavel que testa se o cara nao possui e-Mail.
        /// </summary>
        private int _sememail;
        public int Sememail
        {
            get { return _sememail; }

        }

        private bool _autentica;//Danner - o.s. 24329 - 08/04/2010


        public belEmail(string sSeq, string sEmp, string sHost, string sPorta, string sDe, string sSenha, string sPara, bool bAutentica)//NFe_2.0
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                Globais objGlobais = new Globais();
                xml.Load(objGlobais.LeRegConfig("PastaProtocolos") + "\\" + sSeq + "_ped-can.xml");
                _anexo2 = objGlobais.LeRegConfig("PastaProtocolos") + "\\" + sSeq + "_ped-can.xml";
                string sPath = objGlobais.LeRegConfig("PastaXmlCancelados") + "\\" + xml.GetElementsByTagName("chNFe")[0].InnerText + "-can.xml.xml";
                if (System.IO.File.Exists(sPath))
                {
                    if (sPara == "")
                    {
                        _para = retEmailDestinatario(sSeq, sEmp);
                    }
                    else
                    {
                        _para = sPara;
                    }

                    _paraTransp = "";
                    _envia = true;
                    _sSeq = sSeq;

                    _autentica = bAutentica;
                    _de = sDe;
                    _porta = Convert.ToInt16(sPorta);
                    _host = sHost;
                    _senha = sSenha;
                    _corpo = geraCorpoEmailCanc(sPath);
                    _anexo = sPath;
                    _assunto = "Mensagem Automática de Nota Fiscal Eletrônica de " + _razaoemit;
                }
                else
                {
                    throw new Exception("Arquivo ref. a Nota Fiscal " + xml.GetElementsByTagName("chNFe")[0].InnerText.Substring(25, 9) + " não se Encontra na Pasta de Cancelados"+
                    Environment.NewLine
                    +Environment.NewLine
                    + "Arquivo : " + xml.GetElementsByTagName("chNFe")[0].InnerText);
                }

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
            
        }
        /// <summary>
        /// Email de Cancelamento
        /// </summary>
        /// <param name="sSeq"></param>
        /// <param name="sNum"></param>
        /// <param name="sEmp"></param>
        /// <param name="sHost"></param>
        /// <param name="sPorta"></param>
        /// <param name="sDe"></param>
        /// <param name="sSenha"></param>
        /// <param name="sPara"></param>
        /// <param name="bAutentica"></param>
        public belEmail(string sSeq, string sNum, string sEmp, string sHost, string sPorta, string sDe, string sSenha, string sPath, string sPara, bool bAutentica)//Danner - o.s. 24329 - 08/04/2010
        {

            if (sPara == "")
            {
                _para = retEmailDestinatario(sSeq, sEmp);
            }
            else
            {
                _para = sPara;
            }

            _paraTransp = retEmailTransportador(sSeq, sEmp); // 24776 - Diego
            _envia = true;
            _sSeq = sSeq;

            _autentica = bAutentica;//Danner - o.s. 24329 - 08/04/2010
            _de = sDe;
            _porta = Convert.ToInt16(sPorta);
            _host = sHost;
            _senha = sSenha;
            _corpo = geraCorpoEmail(sPath, sNum);
            _anexo = sPath;
            _assunto = "Mensagem Automática de Nota Fiscal Eletrônica de " + _razaoemit;

        }


        private string geraCorpoEmailCanc(string sPath)//NFe_2.0
        {
            XmlDocument xmldoc = new XmlDocument();
            string corpo;
            try
            {
                xmldoc.Load(@sPath);
                string sNum = xmldoc.GetElementsByTagName("nNF")[0].InnerText;
                string cnpj = xmldoc.GetElementsByTagName("CNPJ")[0].InnerText;
                _razaoemit = xmldoc.GetElementsByTagName("xNome")[0].InnerText;
                string chave = xmldoc.GetElementsByTagName("infNFe")[0].Attributes["Id"].Value.Replace("NFe", "");
                string protocolo = xmldoc.GetElementsByTagName("nProt")[0].InnerText;
                corpo = string.Format("Esta mensagem refere-se ao CANCELAMENTO da Nota Fiscal Eletronica Nacional de serie/numero {5} emitida pela:{0}{0}Razao Social: {1}{0}CNPJ: {2}{0}{0} Para verificar o status da NFe na SEFAZ, acesse o site https://www.nfe.fazenda.gov.br/portal/FormularioDePesquisa.aspx?tipoconsulta=completa {0}{0}Chave de Acesso: {3}{0}Protocolo: {4}{0}{0}Este e-mail foi enviado automaticamente pelo Sistema de Nota Fiscal Eletronica (NF-e) da HLP - ESTRATEGIA EM SISTEMAS ({6})", Environment.NewLine, _razaoemit, cnpj, chave, protocolo, sNum, "www.hlp.com.br");
            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }
            return corpo; ;
        }

        private string geraCorpoEmail(string sPath, string sNum)
        {
            XmlDocument xmldoc = new XmlDocument();
            string corpo;
            try
            {
                xmldoc.Load(sPath);
                string cnpj = xmldoc.GetElementsByTagName("CNPJ")[0].InnerText;
                _razaoemit = xmldoc.GetElementsByTagName("xNome")[0].InnerText;
                string chave = xmldoc.GetElementsByTagName("infNFe")[0].Attributes["Id"].Value.Replace("NFe","");
                string protocolo = "";
                if (xmldoc.GetElementsByTagName("nProt")[0] != null)
                {
                    protocolo = xmldoc.GetElementsByTagName("nProt")[0].InnerText;                    
                }                
                corpo = string.Format("Esta mensagem refere-se a Nota Fiscal Eletronica Nacional de serie/numero {5} emitida pela:{0}{0}Razao Social: {1}{0}CNPJ: {2}{0}{0} Para verificar a autorizacao da SEFAZ referente a nota acima mencionada, acesse o site https://www.nfe.fazenda.gov.br/portal/FormularioDePesquisa.aspx?tipoconsulta=completa {0}{0}Chave de Acesso: {3}{0}Protocolo: {4}{0}{0}Este e-mail foi enviado automaticamente pelo Sistema de Nota Fiscal Eletronica (NF-e) da HLP - ESTRATEGIA EM SISTEMAS ({6})", Environment.NewLine, _razaoemit, cnpj, chave, protocolo, sNum, "www.hlp.com.br");
            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }
            return corpo; ;
        }
        public void enviaEmail()
        {
            try
            {
                SmtpClient cliente = new SmtpClient(_host, _porta);
                cliente.EnableSsl = _autentica;

                MailAddress remetente = new MailAddress(_de);
                MailAddress destinatario = new MailAddress((_para != "" ? _para : _paraTransp));
                MailMessage mensagem = new MailMessage(remetente, destinatario);

                //mensagem.BodyEncoding = Encoding.UTF8;
                // Diego - 24776
                if ((_paraTransp != "") && (_para != ""))
                {
                    MailAddress dest_Trasnp = new MailAddress(_paraTransp);
                    mensagem.To.Add(dest_Trasnp);
                }
                // Diego - 24776 - FIM

                mensagem.Body = _corpo;
                mensagem.Subject = _assunto;
                Attachment anexo = new Attachment(_anexo);
                mensagem.Attachments.Add(anexo);
                if ((_anexo2 != "")&&(_anexo2!= null))
                {
                    Attachment anexo2 = new Attachment(_anexo2);
                    mensagem.Attachments.Add(anexo2);
                }

                NetworkCredential credenciais = new NetworkCredential(_de, _senha);

                cliente.Credentials = credenciais;

                cliente.Send(mensagem);

            }
            catch (SmtpException x)
            {

                throw new Exception(x.Message);
            }

        }
        private string retEmailDestinatario(string sSeq, string sEmp)
        {
            StringBuilder sSql = new StringBuilder();
            string email = "";

            try
            {
                sSql.Append("select ");
                sSql.Append("clifor.cd_email ");
                sSql.Append("from ");
                sSql.Append("clifor ");
                sSql.Append("left join nf ");
                sSql.Append("on ");
                sSql.Append("(nf.cd_clifor = clifor.cd_clifor) ");
                sSql.Append("where ");
                sSql.Append("nf.cd_empresa = '");
                sSql.Append(sEmp);
                sSql.Append("' ");
                sSql.Append("and ");
                sSql.Append("nf.cd_nfseq = '");
                sSql.Append(sSeq);
                sSql.Append("'");

                belGerarXML BuscaConexao = new belGerarXML();

                FbConnection Conn = BuscaConexao.Conn;

                Conn.Open();

                FbCommand sqlConsulta = new FbCommand(sSql.ToString(), Conn);
                sqlConsulta.ExecuteNonQuery();

                FbDataReader drDest = sqlConsulta.ExecuteReader();
                drDest.Read();

                string[] split = drDest["cd_email"].ToString().Split(';');

                foreach (var i in split)
                {
                    email = i;
                    break;
                }


                Conn.Close();

            }
            catch (Exception x)
            {

                throw new Exception(x.Message);
            }

            return email;
        }

        private string retEmailTransportador(string sSeq, string sEmp)
        {
            StringBuilder sSql = new StringBuilder();
            string email = "";

            try
            {
                sSql.Append("select ");
                sSql.Append("transpor.cd_email ");
                sSql.Append("from ");
                sSql.Append("transpor inner join nf ");
                sSql.Append("on transpor.cd_trans = nf.cd_trans ");
                sSql.Append("where ");
                sSql.Append("nf.cd_empresa = '");
                sSql.Append(sEmp);
                sSql.Append("' ");
                sSql.Append("and ");
                sSql.Append("nf.cd_nfseq = '");
                sSql.Append(sSeq);
                sSql.Append("'");

                belGerarXML BuscaConexao = new belGerarXML();

                FbConnection Conn = BuscaConexao.Conn;

                Conn.Open();

                FbCommand sqlConsulta = new FbCommand(sSql.ToString(), Conn);
                sqlConsulta.ExecuteNonQuery();

                FbDataReader drDest = sqlConsulta.ExecuteReader();
                drDest.Read();

                string[] split = drDest["cd_email"].ToString().Split(';');

                foreach (var i in split)
                {
                    email = i;
                    break;
                }


                Conn.Close();

            }
            catch (Exception x)
            {

                throw new Exception(x.Message);
            }

            return email;
        }
    }
}
//Fim - Danner - o.s. SEM - 05/11/2009