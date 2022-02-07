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
        string TMPlocationModule = "C:\\copy_sodim\\hollow.txt";
        string XMLCILTemplatePath = "C:\\Users\\Maciek\\Desktop\\E-Cil\\CIL_Template.xml";
        string templatePath = "C:\\copy_sodim\\";
        string nameOfXMLFile = ZmienneGlobalne.numer_sodimatu.ToString() + "_wpisKartyCIL_" + System.DateTime.Now.Year.ToString() + ".r.xml";

        public void WpisKalibracjiToXML()
        {

       
            XDocument xdoc = new XDocument();
            string path = templatePath + nameOfXMLFile;

            if (!File.Exists(path))
            {
                XDocument xd = new XDocument();
                xd = new XDocument();
                xd.Add(new XElement("ListaWpisow"));
                //xd.Save(templatePath + nameOfXMLFile);
                xd.Save(path); //
            }



            //// xdoc = XDocument.Load(templatePath + nameOfXMLFile);

            xdoc = XDocument.Load(path);
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
                xdoc.Save(path);
            }
            catch { /*MessageBox.Show("Podczas próby zapisu obiekt do zapisu nie został wykryty");*/ }
            xdoc = XDocument.Load(path);

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
                xdoc.Save(path);
            }

            catch { /*MessageBox.Show("Podczas próby zapisu obiekt do zapisu nie został wykryty");*/ }

        }

    }
}
