using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Deployment.Application;
using System.Windows.Forms;
using System.IO;
using HLP.bel.Static;

namespace HLP.bel
{
    public class belVersionamento
    {
        bool bHLP = false;

        public bool bstateInternet = false;

        public belVersionamento()
        {
            InternetCS objIcs = new InternetCS();
            bstateInternet = objIcs.Conexao();
            bHLP = belUtil.VerificaSeEstaNaHLP();
        }

        public bool VerificaAtualizacaoDisponivel()
        {
            try
            {
                HLP.WebService.Ws_versionamento_Cliente.Verisionamento ws = new HLP.WebService.Ws_versionamento_Cliente.Verisionamento();
                return ws.VerificaVersaoLiberadaParaCliente(belStatic.sNomeEmpresa);
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool VersaoDisponivelIgualLiberada(string sUltimaVersao)
        {
            string sUltimaVersaoDisp = "";

            HLP.WebService.Ws_versionamento_Cliente.Verisionamento ws = new HLP.WebService.Ws_versionamento_Cliente.Verisionamento();
            sUltimaVersaoDisp = ws.BuscaUltimaVersaoDisponivel();

            if (sUltimaVersao == sUltimaVersaoDisp)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string BuscaInformacaoAtualizacao(string sVersao)
        {
            try
            {
                HLP.WebService.Ws_versionamento_Cliente.Verisionamento ws = new HLP.WebService.Ws_versionamento_Cliente.Verisionamento();
                return ws.BuscaInforcoesSobreAtualizacao(sVersao);
            }
            catch (Exception)
            {
                return "";
            }

        }


        public bool VerificaPublicacaoDisponivel()
        {
            bool bReturn = false;
            try
            {
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;
                    UpdateCheckInfo info = null;
                    #region Check se tem update
                    try
                    {
                        info = ad.CheckForDetailedUpdate();
                    }
                    catch (DeploymentDownloadException dde)
                    {
                        throw new Exception("A nova versão não pode ser baixada agora. \n\nVerifique sua conexão com a Internet ou tente novamente mais tarde. Erro: " + dde.Message);

                    }
                    catch (InvalidDeploymentException ide)
                    {
                        throw new Exception("O arquivo está indisponível ou corrompido. Erro: " + ide.Message);

                    }
                    catch (InvalidOperationException ioe)
                    {
                        throw new Exception("This application cannot be updated. It is likely not a ClickOnce application. Error: " + ioe.Message);
                    }
                    #endregion

                    if (info.UpdateAvailable)
                    {
                        bReturn = true;
                    }
                }

            }
            catch (Exception)
            {
                bReturn = false;
            }

            return bReturn;
        }
    }
}
