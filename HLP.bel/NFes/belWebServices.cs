using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using HLP.bel.Static;
using System.Security.Cryptography.X509Certificates;
using HLP.bel.NFe.GeraXml;

namespace HLP.bel.NFes
{
    public class belWebServices
    {

        public string EnviarLoteRpsEnvio(XmlDocument xmlLote) 
        {
            try
            {
                string sMesssage = "";
                if (belStatic.tpAmbNFse == 1) // Produção
                {
                    HLP.WebService.Itu_servicos_Producao.ServiceGinfesImplService objWebServices = new HLP.WebService.Itu_servicos_Producao.ServiceGinfesImplService();
                    AssinaNFeXml objbuscanome = new AssinaNFeXml();
                    X509Certificate2 cert = new X509Certificate2();
                    cert = objbuscanome.BuscaNome("");
                    objWebServices.ClientCertificates.Add(cert);
                }
                else//Homologação
                {
                    HLP.WebService.Itu_servicos_Homologacao.ServiceGinfesImplService objWebServices = new HLP.WebService.Itu_servicos_Homologacao.ServiceGinfesImplService();

                }
               

               

                    



                return sMesssage;
            }
            catch (Exception)
            {                
                throw;
            }
           
        }
    }
}
