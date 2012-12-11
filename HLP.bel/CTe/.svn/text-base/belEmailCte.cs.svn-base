using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Net;
using System.Net.Mail;
using FirebirdSql.Data.FirebirdClient;
using System.IO;
using HLP.bel.Static;

namespace HLP.bel.CTe
{
    public class belEmailCte
    {
        List<HLP.bel.NFe.belEmailContador> objListaEmailContador = new List<HLP.bel.NFe.belEmailContador>();
        public bool bCorpoHTML = false;

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

        public string _NumCte;

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

        private bool _autentica;



        public belEmailCte(string sPath, string sNumCte, string sHost, string sPorta, string sDe, string sSenha, bool bAutentica)
        {

            _para = retEmailDestinatario(sNumCte);
            //_para = "andre@hlp.com.br";


            _envia = true;
            _NumCte = sNumCte;
            _autentica = bAutentica;
            _de = sDe;
            _porta = Convert.ToInt16(sPorta);
            _host = sHost;
            _senha = sSenha;
            _corpo = geraCorpoEmail(sPath, sNumCte);
            _anexo = sPath;
            _assunto = "Mensagem Automática de Conhecimento de Transporte Eletrônico de " + _razaoemit;

        }

        private string retEmailDestinatario(string sNumCte)
        {
            belConnection cx = new belConnection();
            StringBuilder sQuery = new StringBuilder();

            string email = "";

            try
            {
                sQuery.Append("Select ");
                sQuery.Append("coalesce(remetent.cd_email,'')email ");
                sQuery.Append("from remetent ");
                sQuery.Append("join conhecim on remetent.cd_remetent = conhecim.cd_remetent ");
                sQuery.Append("where conhecim.cd_conheci  ='" + sNumCte + "'");


                FbCommand fbConn = new FbCommand(sQuery.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                fbConn.ExecuteNonQuery();
                FbDataReader dr = fbConn.ExecuteReader();
                dr.Read();
                email = dr["email"].ToString();

            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }
            finally
            {
                cx.Close_Conexao();
            }

            return email;
        }

        private string geraCorpoEmail(string sPath, string sNumCte)
        {
            XmlDocument xmldoc = new XmlDocument();
            string corpo;
            string cnpj = "";

            try
            {
                xmldoc.Load(sPath);
                XmlNodeList emit = xmldoc.GetElementsByTagName("emit");
                for (int i = 0; i < emit.Count; i++)
                {
                    for (int j = 0; j < emit[i].ChildNodes.Count; j++)
                    {
                        switch (emit[i].ChildNodes[j].LocalName)
                        {
                            case "CNPJ": cnpj = emit[0].ChildNodes[j].InnerText;
                                break;
                            case "xNome": _razaoemit = emit[0].ChildNodes[j].InnerText;
                                break;
                        }
                    }
                }
                string chave = xmldoc.GetElementsByTagName("infCte")[0].Attributes["Id"].Value.Replace("CTe", "");

                string protocolo = "";
                if (xmldoc.GetElementsByTagName("nProt")[0] != null)
                {
                    protocolo = xmldoc.GetElementsByTagName("nProt")[0].InnerText;
                }

                corpo = string.Format("Esta mensagem refere-se ao Conhecimento de Transporte Eletrônico número {5} emitido por:{0}{0}Razão Social: {1}{0}CNPJ: {2}{0}{0} Para verificar o Conhecimento mencionado, acesse o site https://www.cte.fazenda.gov.br/FormularioDePesquisa.aspx?tipoconsulta=resumo {0}{0}Chave de Acesso: {3}{0}Protocolo: {4}{0}{0}Este e-mail foi enviado automaticamente pelo Sistema de Conhecimento de Transporte Eletrônico (CT-e) da HLP - ESTRATÉGIA EM SOFTWARE ({6})", Environment.NewLine, _razaoemit, cnpj, chave, protocolo, sNumCte, "www.hlp.com.br");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return corpo;
        }

        public void enviaEmail(string sNumCte)
        {
            try
            {
                SmtpClient cliente = new SmtpClient(_host, _porta);
                cliente.EnableSsl = _autentica;

                MailAddress remetente = new MailAddress(_de);
                MailAddress destinatario = new MailAddress(_para);
                MailMessage mensagem = new MailMessage(remetente, destinatario);


                mensagem.IsBodyHtml = bCorpoHTML;
                mensagem.Body = _corpo;
                mensagem.Subject = _assunto;
                if (_anexo != null)
                {
                    Attachment anexo = new Attachment(_anexo);
                    mensagem.Attachments.Add(anexo);
                }
                if ((_anexo2 != "") && (_anexo2 != null))
                {
                    Attachment anexo2 = new Attachment(_anexo2);
                    mensagem.Attachments.Add(anexo2);
                }

                string sPDF_cancelado = belStaticPastas.CANCELADOS + "\\PDF\\Cte_" + this._NumCte + ".pdf";
                string sPDF_normal = belStaticPastas.ENVIADOS + "\\PDF\\Cte_" + this._NumCte + ".pdf";
                string sTemp = belStaticPastas.CBARRAS;
                string sArquivo = "";
                sArquivo = string.Format("_{0}{1}{2}", HLP.Util.Util.GetDateServidor().Hour, HLP.Util.Util.GetDateServidor().Minute, HLP.Util.Util.GetDateServidor().Second);

                if (File.Exists(sPDF_cancelado))
                {
                    sArquivo = sTemp + "\\" + this._NumCte + sArquivo + ".pdf";
                    File.Copy(sPDF_cancelado, sArquivo, true);
                    Attachment anexo = new Attachment(sArquivo);
                    mensagem.Attachments.Add(anexo);
                }
                else if (File.Exists(sPDF_normal))
                {
                    sArquivo = sTemp + "\\" + this._NumCte + sArquivo + ".pdf";
                    File.Copy(sPDF_normal, sArquivo, true);
                    Attachment anexo = new Attachment(sArquivo);
                    mensagem.Attachments.Add(anexo);
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




    }
}
