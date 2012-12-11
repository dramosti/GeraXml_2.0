using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLP.bel.NFes
{
   public class belTabelaErros
    {
       public string sCodigo { get; set; }

       public string sDescricao { get; set; }

       public string sSolucao { get; set; }


       public List<belTabelaErros> RetornaListaCompleta() 
       {
           try
           {
               List<belTabelaErros> objLista = new List<belTabelaErros>();
               return objLista;
           }
           catch (Exception)
           {               
               throw;
           }
       }

    }
}
