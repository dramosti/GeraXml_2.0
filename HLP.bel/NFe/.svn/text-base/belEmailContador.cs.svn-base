using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using HLP.bel.NFe.GeraXml;

namespace HLP.bel.NFe
{
    public class belEmailContador
    {
        public bool bSelect { get; set; }
        private string _mes;
        public string Mes
        {
            get { return _mes; }
            set { _mes = RetMes(Convert.ToInt32(value)); }
        }
        public string sAno { get; set; }
        public int iId { get; set; }
        public string sCaminhoEnviado { get; set; }
        public string sCaminhoCancelado { get; set; }
        public string sCaminhoCCe { get; set; }
        /// <summary>
        /// AAMMDD
        /// </summary>
        public string sCaminhoZip { get; set; }
      
        public System.Drawing.Image Ienviado { get; set; }
        private string _sName;
        public string sName
        {
            get { return _sName; }
            set
            {
                _sName = value;
                sCaminhoZip = dinfo.FullName + "\\" + value.ToString() + HLP.Util.Util.GetDateServidor().Date.Day.ToString().PadLeft(2, '0') + ".zip";
            }
        }

        public int iFaltantes { get; set; }

        private int _iEnviadoContador;

        public int iEnviadoContador
        {
            get { return _iEnviadoContador; }
            set
            {
                _iEnviadoContador = value;
                iFaltantes = iFaltantes - _iEnviadoContador;
            }
        }

        private System.Xml.Linq.XDocument _xmlArqEnviados;

        public System.Xml.Linq.XDocument xmlArqEnviados
        {
            get { return _xmlArqEnviados; }
            set { _xmlArqEnviados = value; }
        }


        public DirectoryInfo dinfo;

        public belEmailContador()
        {
            Globais objGlobais = new Globais();
            dinfo = new DirectoryInfo(belStaticPastas.ENVIADOS + "\\Contador_xml");
            if (!dinfo.Exists)
            {
                dinfo.Create();
            }
        }


        struct strucMeses
        {
            public string sMes { get; set; }
            public int iMes { get; set; }
        }
        public string RetMes(int iMes)
        {
            List<strucMeses> objListaMes = new List<strucMeses>();
            objListaMes.Add(new strucMeses { iMes = 1, sMes = "Janeiro" });
            objListaMes.Add(new strucMeses { iMes = 2, sMes = "Fevereiro" });
            objListaMes.Add(new strucMeses { iMes = 3, sMes = "Março" });
            objListaMes.Add(new strucMeses { iMes = 4, sMes = "Abril" });
            objListaMes.Add(new strucMeses { iMes = 5, sMes = "Maio" });
            objListaMes.Add(new strucMeses { iMes = 6, sMes = "Junho" });
            objListaMes.Add(new strucMeses { iMes = 7, sMes = "Julho" });
            objListaMes.Add(new strucMeses { iMes = 8, sMes = "Agosto" });
            objListaMes.Add(new strucMeses { iMes = 9, sMes = "Setembro" });
            objListaMes.Add(new strucMeses { iMes = 10, sMes = "Outubro" });
            objListaMes.Add(new strucMeses { iMes = 11, sMes = "Novembro" });
            objListaMes.Add(new strucMeses { iMes = 12, sMes = "Dezembro" });

            return objListaMes.FirstOrDefault(m => m.iMes == iMes).sMes;
        }
    }

}
