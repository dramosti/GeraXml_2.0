using System;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using System.Xml;
using System.Text.RegularExpressions;
using HLP.bel.NFes;
using System.Windows.Forms;
using System.IO;
using HLP.bel.CCe;
using HLP.bel.Static;
using HLP.bel.NFe.GeraXml;
using HLP.bel.CTe;

namespace HLP.bel
{
    public class belEmail
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

        public string sNum { get; set; }

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

        /// <summary>
        /// Email de Cancelamento
        /// </summary>      
        public belEmail(string sSeq, string sEmp, string sHost, string sPorta, string sDe, string sSenha, string sPara, bool bAutentica)//NFe_2.0
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                Globais objGlobais = new Globais();
                xml.Load(objGlobais.LeRegConfig("PastaProtocolos") + "\\" + sSeq + "_ped-can.xml");
                _anexo2 = objGlobais.LeRegConfig("PastaProtocolos") + "\\" + sSeq + "_ped-can.xml";
                string sPath = belStaticPastas.CANCELADOS + "\\" + HLP.Util.Util.GetDateServidor().Date.ToString("yyMM") + "\\" + xml.GetElementsByTagName("chNFe")[0].InnerText + "-can.xml.xml";

                //DirectoryInfo dinfo = new DirectoryInfo(objGlobais.LeRegConfig("PastaXmlCancelados"));
                //string sArq = xml.GetElementsByTagName("chNFe")[0].InnerText + "-can.xml.xml";

                //foreach (FileInfo arq in dinfo.GetFiles("*.xml"))
                //{

                //}

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
                    throw new Exception("Arquivo ref. a Nota Fiscal " + xml.GetElementsByTagName("chNFe")[0].InnerText.Substring(25, 9) + " não se Encontra na Pasta de Cancelados" +
                    Environment.NewLine
                    + Environment.NewLine
                    + "Arquivo : " + xml.GetElementsByTagName("chNFe")[0].InnerText);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        /// <summary>
        /// Email de CCe
        /// </summary>
        /// <param name="sSeq"></param>
        /// <param name="sNum"></param>
        /// <param name="sEmp"></param>
        /// <param name="sHost"></param>
        /// <param name="sPorta"></param>
        /// <param name="sDe"></param>
        /// <param name="sSenha"></param>
        /// <param name="sPath"></param>
        /// <param name="sPara"></param>
        /// <param name="bAutentica"></param>
        public belEmail(belPesquisaCCe cce, string sSeq, string sNum, string sEmp, string sHost, string sPorta, string sDe, string sSenha, string sPara, bool bAutentica)
        {
            this.sNum = sNum;
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
            _corpo = geraCorpoEmail(cce);
            Globais LeRegWin = new Globais();
            string sPath = belStaticPastas.ENVIADOS + "\\Servicos" + "\\PDF\\" + cce.CD_NOTAFIS + ".pdf";
            _anexo = sPath;
            _assunto = "Mensagem Automática 'Carta de Correção Eletrônica de " + belStatic.sNomeEmpresaCompleto + "'";

        }


        public belEmail(string sSeq, string sNum, string sEmp, string sHost, string sPorta, string sDe, string sSenha, string sPath, string sPara, bool bAutentica)//Danner - o.s. 24329 - 08/04/2010
        {
            this.sNum = sNum;
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
            _corpo = geraCorpoEmail(sPath);
            _anexo = sPath;
            _assunto = "Mensagem Automática de Nota Fiscal Eletrônica de " + _razaoemit;

        }

        /// <summary>
        /// Email NFe-Serviço
        /// </summary>      
        public belEmail(TcInfNfse objInfNfse, string sMsgPrestador, string sHost, string sPorta, string sDe, string sSenha, string sPara, bool bAutentica)
        {
            bCorpoHTML = true;
            if (sPara == "")
            {
                _para = retEmailDestinatario(objInfNfse.IdentificacaoRps.Nfseq, belStatic.codEmpresaNFe);
            }
            else
            {
                _para = sPara;
            }

            _envia = true;
            _sSeq = objInfNfse.IdentificacaoRps.Nfseq;
            _paraTransp = "";
            _autentica = bAutentica;
            _de = sDe;
            _porta = Convert.ToInt16(sPorta);
            _host = sHost;
            _senha = sSenha;
            _corpo = geraCorpoEmail(objInfNfse, sMsgPrestador);
            _assunto = "Notificação de Emissão de Nota Fiscal Eletrônica";

        }

        /// <summary>
        /// E-mail arquivos Xml para Contador;
        /// </summary>
        /// <param name="sEmailContador"></param>
        /// <param name="sEmailCc"></param>
        /// <param name="sMsgPrestador"></param>
        /// <param name="sHost"></param>
        /// <param name="sPorta"></param>
        /// <param name="sDe"></param>
        /// <param name="sSenha"></param>
        /// <param name="sPara"></param>
        /// <param name="bAutentica"></param>
        public belEmail(List<HLP.bel.NFe.belEmailContador> objListaEmailContador, string sHost, string sPorta, string sDe, string sSenha, string sPara, bool bAutentica, string sEmailCc)
        {
            bCorpoHTML = true;
            _para = sPara;
            _envia = true;
            _paraTransp = sEmailCc; //será usado como a Cc
            _autentica = bAutentica;
            _de = sDe;
            _porta = Convert.ToInt16(sPorta);
            _host = sHost;
            _senha = sSenha;
            this.objListaEmailContador = objListaEmailContador;
            _corpo = belStatic.RAMO != "TRANSPORTE" ? GeraCorpoEmailContador() : GeraCorpoEmailContadorCte();
            _assunto = (belStatic.RAMO != "TRANSPORTE" ? "Nota Fiscal Eletrônica ( XML ) - Empresa: " : "Conhecimento de Transporte Eletrônico - Empresa: ") + belStatic.sNomeEmpresaCompleto;
        }

        private string GeraCorpoEmailContador()
        {
            StringBuilder corpo = new StringBuilder();
            corpo.Append("<H3>Caros Srs,</H3>");
            corpo.Append("Segue anexo os Xml's de envio ref. aos meses citados abaixo");
            corpo.Append("<P>");
            foreach (HLP.bel.NFe.belEmailContador item in objListaEmailContador)
            {
                corpo.Append(item.Mes + " / " + item.sAno + "<P>");
            }
            corpo.Append("Esse é um e-mail automático gerado por nosso sistema transmissor de NF-e; <P>");
            corpo.Append("Att.: " + belStatic.sNomeEmpresaCompleto);
            corpo.Append("<br><br><br><I><font color = " + "\"" + "#c0c0c0" + "\"" + " size = 5>HLP - Estratégia em Sistemas</font></I><br>");
            corpo.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href=" + "\"" + "http://www.hlp.com.br" + "\"" + ">www.hlp.com.br</a>");
            return corpo.ToString();
        }

        private string GeraCorpoEmailContadorCte()
        {
            StringBuilder corpo = new StringBuilder();
            corpo.Append("<H3>Caros Srs,</H3>");
            corpo.Append("Segue anexo os Xml's de envio ref. aos meses citados abaixo");
            corpo.Append("<P>");
            foreach (HLP.bel.NFe.belEmailContador item in objListaEmailContador)
            {
                corpo.Append(item.Mes + " / " + item.sAno + "<P>");
            }
            corpo.Append("Esse é um e-mail automático gerado por nosso sistema transmissor de CT-e; <P>");
            corpo.Append("Att: " + belStatic.sNomeEmpresaCompleto);
            corpo.Append("<br><br><br><I><font color = " + "\"" + "#c0c0c0" + "\"" + " size = 5>HLP - Estratégia em Software</font></I><br>");
            corpo.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href=" + "\"" + "http://www.hlp.com.br" + "\"" + ">www.hlp.com.br</a>");
            return corpo.ToString();
        }
        /// <summary>
        /// NFE- SERVIÇO
        /// </summary>     
        private string geraCorpoEmail(TcInfNfse objInfNfse, string sMsgPrestador)
        {
            StringBuilder corpo = new StringBuilder();
            try
            {
                corpo.Append("<H3>Sr. Contribuinte,</H3>");
                corpo.Append("Esta mensagem refere-se à Nota Fiscal Eletrônica de Serviços N°" + objInfNfse.Numero);
                corpo.Append(" emitida pelo prestador de serviços: <P>");
                corpo.Append(sMsgPrestador + Environment.NewLine);
                corpo.Append("Para visualizá-la acesse o link a seguir: " + Environment.NewLine);

                string scaminhoLink = "http://" + "itu" + ".ginfes" + (belStatic.tpAmbNFse == 2 ? "h" : "") + ".com.br/birt/frameset?__report=nfs_ver4.rptdesign&cdVerificacao=" + objInfNfse.CodigoVerificacao + "&numNota=" + objInfNfse.Numero;

                corpo.Append("<a href=" + scaminhoLink + ">Visualize a NFe-serviço aqui!</a>");
                corpo.Append("<br><br><br><I><font color = " + "\"" + "#c0c0c0" + "\"" + " size = 5>HLP - Estratégia em Sistemas</font></I><br>");
                corpo.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href=" + "\"" + "http://www.hlp.com.br" + "\"" + ">www.hlp.com.br</a>");
            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }
            return corpo.ToString();
        }

        private string geraCorpoEmailCanc(string sPath)//NFe_2.0
        {
            XmlDocument xmldoc = new XmlDocument();
            string corpo;
            try
            {
                xmldoc.Load(@sPath);
                this.sNum = xmldoc.GetElementsByTagName("nNF")[0].InnerText;
                string cnpj = xmldoc.GetElementsByTagName("CNPJ")[0].InnerText;
                _razaoemit = xmldoc.GetElementsByTagName("xNome")[0].InnerText;
                string chave = xmldoc.GetElementsByTagName("infNFe")[0].Attributes["Id"].Value.Replace("NFe", "");
                string protocolo = xmldoc.GetElementsByTagName("nProt")[0].InnerText;
                corpo = string.Format("Esta mensagem refere-se ao CANCELAMENTO da Nota Fiscal Eletronica Nacional de serie/numero {5} emitida pela:{0}{0}Razao Social: {1}{0}CNPJ: {2}{0}{0} Para verificar o status da NFe na SEFAZ, acesse o  ( https://www.nfe.fazenda.gov.br/portal/consulta.aspx?tipoConsulta=completa&tipoConteudo=XbSeqxE8pl8= ) {0}{0}Chave de Acesso: {3}{0}Protocolo: {4}{0}{0}Este e-mail foi enviado automaticamente pelo Sistema de Nota Fiscal Eletronica (NF-e) da HLP - ESTRATEGIA EM SISTEMAS ({6})", Environment.NewLine, _razaoemit, cnpj, chave, protocolo, sNum, "www.hlp.com.br");
            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }
            return corpo; ;
        }

        /// <summary>
        /// Gera Corpo para CCe
        /// </summary>
        /// <returns></returns>
        private string geraCorpoEmail(belPesquisaCCe cce)
        {
            bCorpoHTML = true;
            string corpo;
            StringBuilder scorpo = new StringBuilder();
            try
            {
                scorpo.Append("<H3>Sr. Contribuinte,</H3>");
                scorpo.Append("Esta mensagem refere-se a Carta de Correção Eletrônica efetuada na NFe Nacional de número {4} emitida pela: <P>");
                scorpo.Append("Razao Social: {1}{0}");
                scorpo.Append("CNPJ: {2}{0}");
                scorpo.Append("Visualize a NF-eletrônica ");
                string scaminhoLink = "https://www.nfe.fazenda.gov.br/portal/consulta.aspx?tipoConsulta=completa&tipoConteudo=XbSeqxE8pl8=";
                scorpo.Append("<a href=" + scaminhoLink + ">aqui!</a> <P>");
                scorpo.Append("Chave de Acesso: {3}{0}");
                scorpo.Append("<I><font color = " + "\"" + "#c0c0c0" + "\"" + " size = 5>HLP - Estratégia em Sistemas</font></I>{0}");
                scorpo.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href=" + "\"" + "http://www.hlp.com.br" + "\"" + ">www.hlp.com.br</a>");


                corpo = string.Format(scorpo.ToString(), "<br>", belStatic.sNomeEmpresaCompleto, cce.CNPJ, cce.CHNFE, sNum);
            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }
            return corpo; ;
        }

        private string geraCorpoEmail(string sPath)
        {
            bCorpoHTML = true;
            XmlDocument xmldoc = new XmlDocument();
            string corpo;
            StringBuilder scorpo = new StringBuilder();
            try
            {
                xmldoc.Load(sPath);

                scorpo.Append("<H3>Sr. Contribuinte,</H3>");
                scorpo.Append("Esta mensagem refere-se a Nota Fiscal Eletronica Nacional de serie/numero {5} emitida pela: <P>");
                scorpo.Append("Razao Social: {1}{0}");
                scorpo.Append("CNPJ: {2}{0}");
                scorpo.Append("Visualize a NF-eletrônica ");
                string scaminhoLink = "https://www.nfe.fazenda.gov.br/portal/consulta.aspx?tipoConsulta=completa&tipoConteudo=XbSeqxE8pl8=";
                scorpo.Append("<a href=" + scaminhoLink + ">aqui!</a> <P>");
                scorpo.Append("Chave de Acesso: {3}{0}");
                scorpo.Append("Protocolo: {4}{0}{0}");
                scorpo.Append("<I><font color = " + "\"" + "#c0c0c0" + "\"" + " size = 5>HLP - Estratégia em Sistemas</font></I>{0}");
                scorpo.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href=" + "\"" + "http://www.hlp.com.br" + "\"" + ">www.hlp.com.br</a>");

                string cnpj = xmldoc.GetElementsByTagName("CNPJ")[0].InnerText;
                _razaoemit = xmldoc.GetElementsByTagName("xNome")[0].InnerText;
                string chave = xmldoc.GetElementsByTagName("infNFe")[0].Attributes["Id"].Value.Replace("NFe", "");
                string protocolo = "";
                if (xmldoc.GetElementsByTagName("nProt")[0] != null)
                {
                    protocolo = xmldoc.GetElementsByTagName("nProt")[0].InnerText;
                }
                corpo = string.Format(scorpo.ToString(), "<br>", _razaoemit, cnpj, chave, protocolo, sNum);
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
                cliente.Timeout = 200000;
                MailAddress remetente = new MailAddress(_de);
                MailAddress destinatario = new MailAddress((_para != "" ? _para : _paraTransp));
                MailMessage mensagem = new MailMessage(remetente, destinatario);
                // Diego - 24776
                if ((_paraTransp != "") && (_para != ""))
                {
                    MailAddress dest_Trasnp = new MailAddress(_paraTransp);
                    mensagem.To.Add(dest_Trasnp);
                }
                // Diego - 24776 - FIM
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
                Globais objGlobais = new Globais();
                string sPDF_cancelado = belStaticPastas.ENVIADOS + "\\PDF\\" + this.sNum + "_cancelado.pdf";
                string sPDF_danfe = belStaticPastas.ENVIADOS + "\\PDF\\" + this.sNum + "_enviado.pdf";
                string sTemp = belStaticPastas.CBARRAS;
                string sArquivo = "";
                sArquivo = string.Format("_{0}{1}{2}", HLP.Util.Util.GetDateServidor().Hour, HLP.Util.Util.GetDateServidor().Minute, HLP.Util.Util.GetDateServidor().Second);

                if (File.Exists(sPDF_cancelado))
                {
                    sArquivo = sTemp + "\\" + this.sNum + sArquivo + "_cancelado.pdf";
                    File.Copy(sPDF_cancelado, sArquivo, true);
                    Attachment anexo = new Attachment(sArquivo);
                    mensagem.Attachments.Add(anexo);
                }
                else if (File.Exists(sPDF_danfe))
                {
                    sArquivo = sTemp + "\\" + this.sNum + sArquivo + "_enviado.pdf";
                    File.Copy(sPDF_danfe, sArquivo, true);
                    Attachment anexo = new Attachment(sArquivo);
                    mensagem.Attachments.Add(anexo);
                }

                if (objListaEmailContador.Count > 0)
                {
                    DirectoryInfo dinfo = new DirectoryInfo(belStaticPastas.ENVIADOS + "\\Contador_xml");
                    foreach (HLP.bel.NFe.belEmailContador item in objListaEmailContador)
                    {
                        Attachment anexo = new Attachment(item.sCaminhoZip);
                        mensagem.Attachments.Add(anexo);
                    }
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
            belConnection cx = new belConnection();

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



                FbCommand sqlConsulta = new FbCommand(sSql.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                sqlConsulta.ExecuteNonQuery();

                FbDataReader drDest = sqlConsulta.ExecuteReader();
                drDest.Read();

                string[] split = drDest["cd_email"].ToString().Split(';');

                foreach (var i in split)
                {
                    email = i;
                    break;
                }

            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }
            finally { cx.Close_Conexao(); }

            return email;
        }

        private string retEmailTransportador(string sSeq, string sEmp)
        {
            StringBuilder sSql = new StringBuilder();
            string email = "";
            belConnection cx = new belConnection();
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


                FbCommand sqlConsulta = new FbCommand(sSql.ToString(), cx.get_Conexao());
                cx.Open_Conexao();
                sqlConsulta.ExecuteNonQuery();

                FbDataReader drDest = sqlConsulta.ExecuteReader();
                drDest.Read();

                string[] split = drDest["cd_email"].ToString().Split(';');

                foreach (var i in split)
                {
                    email = i;
                    break;
                }
            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }
            finally { cx.Close_Conexao(); }

            return email;
        }
    }
}
