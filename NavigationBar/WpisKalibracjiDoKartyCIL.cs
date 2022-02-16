using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Xml.Linq;

namespace NavigationBar
{
    class WpisKalibracjiDoKartyCIL
    {
        string[] arrLine;

        string templatePath = "C:\\copy_sodim\\";
        //string nameOfXMLFile = ZmienneGlobalne.numer_sodimatu.ToString() + "_wpisKartyCIL_" + System.DateTime.Now.Year.ToString() + ".r.xml";
        string nameOfXMLFile = ZmienneGlobalne.path_zapis_Wpisow_Karty_E;


        public void WpisKalibracjiToXML()
        {

       
            XDocument xdoc = new XDocument();
           // string path = templatePath + nameOfXMLFile;

            if (!File.Exists(ZmienneGlobalne.path_zapis_Wpisow_Karty_E))
            {
                XDocument xd = new XDocument();
                xd = new XDocument();
                xd.Add(new XElement("ListaWpisow"));
                //xd.Save(templatePath + nameOfXMLFile);
                xd.Save(ZmienneGlobalne.path_zapis_Wpisow_Karty_E); //
            }



            //// xdoc = XDocument.Load(templatePath + nameOfXMLFile);

            xdoc = XDocument.Load(ZmienneGlobalne.path_zapis_Wpisow_Karty_E);
            try
            {
                // MessageBox.Show(a.duty.ToString());
                xdoc.Element("ListaWpisow").Add(
                new XElement("Wpis",
                new XElement("data", Convert.ToString(System.DateTime.Now.Date.ToString("dd/MM/yyyy"))),
                new XElement("modul", "PD"),
                new XElement("czynnosc", "Init, kontrola gumek, kalibracja"),
                new XElement("status", "A")
                ));
                // xdoc.Save(templatePath + nameOfXMLFile);
                xdoc.Save(ZmienneGlobalne.path_zapis_Wpisow_Karty_E);
            }
            catch { /*MessageBox.Show("Podczas próby zapisu obiekt do zapisu nie został wykryty");*/ }
            xdoc = XDocument.Load(ZmienneGlobalne.path_zapis_Wpisow_Karty_E);

            try
            {
                // MessageBox.Show(a.duty.ToString());
                xdoc.Element("ListaWpisow").Add(
                new XElement("Wpis",
                new XElement("data", Convert.ToString(System.DateTime.Now.Date.ToString("dd/MM/yyyy"))),
                new XElement("modul", "ALL"),
                new XElement("czynnosc", "Czyszczenie toru pomiarowego"),
                new XElement("status", "A")
                ));
                // xdoc.Save(templatePath + nameOfXMLFile);
                xdoc.Save(ZmienneGlobalne.path_zapis_Wpisow_Karty_E);
            }

            catch { /*MessageBox.Show("Podczas próby zapisu obiekt do zapisu nie został wykryty");*/ }

        }

    }
}
