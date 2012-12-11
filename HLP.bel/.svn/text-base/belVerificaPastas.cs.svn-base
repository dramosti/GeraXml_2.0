using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLP.bel
{
    public class belVerificaPastas
    {
        private string pastaenvio;
        private string pastaenviados;
        private string pastacancelados;
        private string pastaprotocolos;
        private string pastaretorno;
        private string pastaschemas;
        
        public belVerificaPastas()
        {
            Globais objLeregwis = new Globais();
            this.pastaenvio = objLeregwis.LeRegConfig("PastaXmlEnvio").ToString();
            this.pastaenviados = objLeregwis.LeRegConfig("PastaXmlEnviado").ToString();
            this.pastacancelados = objLeregwis.LeRegConfig("PastaXmlCancelados").ToString();
            this.pastaprotocolos = objLeregwis.LeRegConfig("PastaProtocolos").ToString();
            this.pastaretorno = objLeregwis.LeRegConfig("PastaXmlRetorno").ToString();
            this.pastaschemas = objLeregwis.LeRegConfig("PastaSchema").ToString();
            VerificaConsistencia();
        }

        public void VerificaConsistencia()
        {
            #region Tratamento para pasta envio

            if (pastaenvio.Equals(pastaenviados))
            {
                throw new Exception("Pasta Enviados está igual a pasta Envio");
            }
            else if (pastaenvio.Equals(pastacancelados))
            {
                throw new Exception("Pasta Cancelados está igual a pasta Envio");
            }
            else if (pastaenvio.Equals(pastaprotocolos))
            {
                throw new Exception("Pasta Protocolos está igual a pasta Envio");
            }
            else if (pastaenvio.Equals(pastaretorno))
            {
                throw new Exception("Pasta Retorno está igual a pasta Envio");
            }
            else if (pastaenvio.Equals(pastaschemas))
            {
                throw new Exception("Pasta XmlSchema está igual a pasta Envio");
            }
            #endregion

            #region Tratamento para pasta enviado

            if (pastaenviados.Equals(pastaenvio))
            {
                throw new Exception("Pasta Enviados está igual a pasta Enviados");
            }
            else if (pastaenviados.Equals(pastacancelados))
            {
                throw new Exception("Pasta Cancelados está igual a pasta Enviados");
            }
            else if (pastaenviados.Equals(pastaprotocolos))
            {
                throw new Exception("Pasta Protocolos está igual a pasta Enviados");
            }
            else if (pastaenviados.Equals(pastaretorno))
            {
                throw new Exception("Pasta Retorno está igual a pasta Enviados");
            }
            else if (pastaenviados.Equals(pastaschemas))
            {
                throw new Exception("Pasta XmlSchema está igual a pasta Enviados");
            }
            #endregion

            #region Tratamento para pasta Cancelados
            if (pastacancelados.Equals(pastaenviados))
            {
                throw new Exception("Pasta Enviados está igual a pasta Cancelados");
            }
            else if (pastacancelados.Equals(pastaenvio))
            {
                throw new Exception("Pasta Envio está igual a pasta Cancelados");
            }
            else if (pastacancelados.Equals(pastaprotocolos))
            {
                throw new Exception("Pasta Protocolos está igual a pasta Cancelados");
            }
            else if (pastacancelados.Equals(pastaretorno))
            {
                throw new Exception("Pasta Retorno está igual a pasta Cancelados");
            }
            else if (pastacancelados.Equals(pastaschemas))
            {
                throw new Exception("Pasta XmlSchema está igual a pasta Cancelados");
            }

            #endregion

            #region Tratamento para pasta Protocolo
            if (pastaprotocolos.Equals(pastaenviados))
            {
                throw new Exception("Pasta Enviados está igual a pasta Protocolos");
            }
            else if (pastaprotocolos.Equals(pastaenvio))
            {
                throw new Exception("Pasta Envio está igual a pasta Protocolos");
            }
            else if (pastaprotocolos.Equals(pastacancelados))
            {
                throw new Exception("Pasta Cancelados está igual a pasta Protocolos");
            }
            else if (pastaprotocolos.Equals(pastaretorno))
            {
                throw new Exception("Pasta Retorno está igual a pasta Protocolos");
            }
            else if (pastaprotocolos.Equals(pastaschemas))
            {
                throw new Exception("Pasta XmlSchema está igual a pasta Protocolos");
            }
            #endregion

            #region Tratamento para pasta Retorno

            if (pastaretorno.Equals(pastaenviados))
            {
                throw new Exception("Pasta Enviados está igual a pasta Retorno");
            }
            else if (pastaretorno.Equals(pastaenvio))
            {
                throw new Exception("Pasta Envio está igual a pasta Retorno");
            }
            else if (pastaretorno.Equals(pastacancelados))
            {
                throw new Exception("Pasta Cancelados está igual a pasta Retorno");
            }
            else if (pastaretorno.Equals(pastaprotocolos))
            {
                throw new Exception("Pasta Protocolos está igual a pasta Retorno");
            }
            else if (pastaretorno.Equals(pastaschemas))
            {
                throw new Exception("Pasta XmlSchema está igual a pasta Retorno");
            }

            #endregion

            #region Tratamento para pasta XmlSchemas

            if (pastaschemas.Equals(pastaenviados))
            {
                throw new Exception("Pasta Enviados está igual a pasta XmlSchema");
            }
            else if (pastaschemas.Equals(pastacancelados))
            {
                throw new Exception("Pasta Cancelados está igual a pasta XmlSchema");
            }
            else if (pastaschemas.Equals(pastaprotocolos))
            {
                throw new Exception("Pasta Protocolos está igual a pasta XmlSchema");
            }
            else if (pastaschemas.Equals(pastaretorno))
            {
                throw new Exception("Pasta Retorno está igual a pasta XmlSchema");
            }
            else if (pastaschemas.Equals(pastaenvio))
            {
                throw new Exception("Pasta Envio está igual a pasta XmlSchema");
            }

            #endregion
        }
    }
}
