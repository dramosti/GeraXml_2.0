using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;



namespace HLP.bel
{
    public class belFecharJanela
    {
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        private const int WM_CLOSE = 0x010;

        public static void FecharJanela(string sNomeJanela)
        {
            try
            {
                IntPtr h = FindWindow(null, sNomeJanela);
                if (h != IntPtr.Zero)
                {
                    PostMessage(h, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool IsFileOpen(string filePath)
        {
            bool fileOpened = false;
            try
            {
                System.IO.FileStream fs = System.IO.File.OpenWrite(filePath);
                fs.Close();
            }
            catch (System.IO.IOException ex)
            {
                fileOpened = true;
            }
            return fileOpened;
        }





    }
}
