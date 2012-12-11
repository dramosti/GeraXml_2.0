using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AC.ExtendedRenderer.Toolkit;
using System.Windows.Forms;

namespace NfeGerarXml
{
    public class HLPexception : Exception
    {
        public HLPexception(string message, Exception inner)
            : base(message, inner)
        {
            KryptonMessageBox.Show(
                string.Format(sEx, Environment.NewLine) + message,
                              "MAGNIFICUS - AVISO ",
                              (inner.InnerException != null ? inner.InnerException.Message : "Sem detalhes..."),
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
        }

        public static string sEx = "Ocorreu uma Exceção ao Manipular essa Ação : {0}{0}Verifique a Mensagem abaixo: {0}________________________________{0}{0}";

    }
}
