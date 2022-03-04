using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace NavigationBar
{

    class WpisDoRaportu
    {

        public WpisDoRaportu()
        {
          //  ZmienneGlobalne.path_To_STAT_Folder;

        }

        int iloscPlikowSTAT;
        string path = "C:\\copy_sodim\\data_" + ZmienneGlobalne.numer_sodimatu + ".xml"; // Plik do zapisu raportu 

        public void WpisRaportDoStatystykTXT()
        {
            int fileSTATcount = Directory.EnumerateFiles(ZmienneGlobalne.path_To_STAT_Folder).Count();

            string zmiana = "";
            if (DateTime.Now.Hour > 22 || DateTime.Now.Hour <= 6)
            {
                zmiana = "1";
            }
            else if (DateTime.Now.Hour > 6 && DateTime.Now.Hour <= 14)
            {
                zmiana = "2";
            }
            else if (DateTime.Now.Hour > 14 && DateTime.Now.Hour <= 22)
            {
                zmiana = "3";
            }

            string pattth = ZmienneGlobalne.path_saveRaport8h;
            pattth += ZmienneGlobalne.numer_sodimatu + "_raport.txt";
            try
            {
                StreamWriter writer = new StreamWriter(pattth, true);
                string dane = ZmienneGlobalne.numer_sodimatu +";"+ZmienneGlobalne.numCig+";"+ZmienneGlobalne.numCycle+";"+ fileSTATcount+";"+
                Convert.ToString(System.DateTime.Now.Date.ToString("dd/MM/yyyy"))+";"+ zmiana;
                writer.WriteLine(dane);
                writer.Dispose();
                //File.AppendAllLines("file.txt", new[] { DateTime.Now.ToString() });
                //File.WriteAllLines();
                //return true;
            }
            catch
            {

            }
        }

        public void WykonajWpis()
        {
            string zmiana = "";
            if (DateTime.Now.Hour > 22 || DateTime.Now.Hour <= 6)
            {
                zmiana = "1";
            }
            else if (DateTime.Now.Hour > 6 && DateTime.Now.Hour <= 14)
            {
                zmiana = "2";
            }
            else if(DateTime.Now.Hour > 14 && DateTime.Now.Hour <= 22)
            {
                zmiana = "3";
            }

            int fileSTATcount = Directory.EnumerateFiles(ZmienneGlobalne.path_To_STAT_Folder).Count();


            XDocument xdoc = new XDocument();
            xdoc = XDocument.Load(path);
            try
            {
                xdoc.Element("data").Element("RaportZmianowy").Add(
                    new XElement("raport",
                    new XElement("data", Convert.ToString(System.DateTime.Now.Date.ToString("dd/MM/yyyy"))),
                    new XElement("zmiana", zmiana),
                    new XElement("ilosc_plikow_STAT", fileSTATcount),
                     new XElement("ilosc_zmierzonych_probek", ZmienneGlobalne.numCig),
                    new XElement("ilosc_zainicjowanych_badan", ZmienneGlobalne.numCycle)));
                xdoc.Save(path);

                GC.Collect();
            }
            catch { }

            WpisRaportDoStatystykTXT();
            GC.Collect();

        }
    }
}
