using NavigationBar.KartaCIL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NavigationBar
{
    /// <summary>
    /// Logika interakcji dla klasy CILCard.xaml
    /// </summary>
    public partial class CILCard : Window
    {
        public CILCard()
        {
            InitializeComponent();
        }

        
        string[] arrLine;
        string TMPlocationModule = "C:\\copy_sodim\\hollow.txt";
      


        public void GetModulesInfo()
        {
            arrLine = File.ReadAllLines(TMPlocationModule);
            string modTMP;
            string opisTMP;
            int interwal;
            string line;
            List<SodimModules> sodim_modules = new List<SodimModules>();
            for (int i = 0; i < arrLine.Length; i++)
            {

                line = arrLine[i].ToString();

                


                sodim_modules.Add(
                    new SodimModules()
                    {
                     //   Modul = "", Interwal = , Opis_czynnosci = ""
                    }) ;
            }
        } 
    }
}
