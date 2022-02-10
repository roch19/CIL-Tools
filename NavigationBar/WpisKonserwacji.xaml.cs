using System;
using System.Collections.Generic;
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
using System.Xml;
using System.Xml.Linq;

namespace NavigationBar
{
    /// <summary>
    /// Logika interakcji dla klasy WpisKonserwacji.xaml
    /// </summary>
    public partial class WpisKonserwacji : Window
    {
        public WpisKonserwacji()
        {
            InitializeComponent();
            getValues();
        }

 
        //data_201_70.xml
       // string sodDataPath = @"C:\copy_sodim\data_" + ZmienneGlobalne.numer_sodimatu + ".xml";
        List<string> lines = null;
        string path = "C:\\copy_sodim\\data_" + ZmienneGlobalne.numer_sodimatu + ".xml";

        public class Sodim
        {
            public string nazwa_sodimatu { get; set; }
            public string lokalizacja { get; set; }
            public string wlasciciel { get; set; }
            public string typ_sodimatu_gumka { get; set; }
            public string data_wymiany_gumki { get; set; }
            public string format_gumki { get; set; }
            public int numcig_z_wymiany_g { get; set; }
            public int numcycle_z_wymiany_g { get; set; }
            public string data_wykonanego_cila { get; set; }
            public int numcig_z_cila { get; set; }
            public int numcycle_z_cila { get; set; }
            public string data_wykonanej_konserwacji { get; set; }
            public int numcig_z_konserwacji { get; set; }
            public int numcycle_z_konserwacji { get; set; }

            public Sodim(string nazwa_sodimatu, string lokalizacja, string wlasciciel, string typ_sodimatu_gumka, string data_wymiany_gumki, string format_gumki,
                int numcig_z_wymiany_g, int numcycle_z_wymiany_g, string data_wykonanego_cila, int numcig_z_cila, int numcycle_z_cila, string data_wykonanej_konserwacji,
                int numcig_z_konserwacji, int numcycle_z_konserwacji)
            {
                this.nazwa_sodimatu = nazwa_sodimatu;
                this.lokalizacja = lokalizacja;
                this.wlasciciel = wlasciciel;
                this.typ_sodimatu_gumka = typ_sodimatu_gumka;
                this.data_wymiany_gumki = data_wymiany_gumki;
                this.format_gumki = format_gumki;
                this.numcig_z_wymiany_g = numcig_z_wymiany_g;
                this.numcycle_z_wymiany_g = numcycle_z_wymiany_g;
                this.data_wykonanego_cila = data_wykonanego_cila;
                this.numcig_z_cila = numcig_z_cila;
                this.numcycle_z_cila = numcycle_z_cila;
                this.data_wykonanej_konserwacji = data_wykonanej_konserwacji;
                this.numcig_z_konserwacji = numcig_z_konserwacji;
                this.numcycle_z_konserwacji = numcycle_z_konserwacji;
            }
        }
 

        public void getValues()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);
            XmlNode xmlNode = xmlDocument.SelectSingleNode("data/Sodimat/data_wykonanej_konserwacji");
            dataOstatniejKonserwacji.Text = xmlNode.InnerText.ToString();

            MessageBox.Show(xmlNode.InnerText.ToString());
            DateTime dt = Convert.ToDateTime(xmlNode.InnerText.ToString());
            iloscDniOdKonserwacji.Text = ((DateTime.Now - dt).Days).ToString();

            xmlNode = xmlDocument.SelectSingleNode("data/Sodimat/numcig_z_konserwacji");
            int tmp = int.Parse(xmlNode.InnerText.ToString());
            iloscProbek.Text = (ZmienneGlobalne.numCig - tmp).ToString();


            xmlNode = xmlDocument.SelectSingleNode("data/Sodimat/numcycle_z_konserwacji");
            tmp = int.Parse(xmlNode.InnerText.ToString());
            iloscCykli.Text = (ZmienneGlobalne.numCycle - tmp).ToString();

            xmlDocument = null;
            xmlNode = null;

            GC.Collect();
        }


        public void wpisKonserwacji()
        {



            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);
            XmlNode xmlNode = xmlDocument.SelectSingleNode("data/Sodimat/data_wykonanego_cila");
            xmlNode.InnerText = DateTime.Now.ToString();
            XmlNode xmlNode1 = xmlDocument.SelectSingleNode("data/Sodimat/numcig_z_cila");
            xmlNode1.InnerText = ZmienneGlobalne.numCig.ToString();
            xmlNode = xmlDocument.SelectSingleNode("data/Sodimat/numcycle_z_cila");
            xmlNode.InnerText = ZmienneGlobalne.numCycle.ToString();

            xmlDocument.Save(path);

            XDocument xdoc = new XDocument();
            xdoc = XDocument.Load(path);
            try
            {
                xdoc.Element("data").Element("CILe").Add(
                    new XElement("cil",
                    new XElement("data", Convert.ToString(System.DateTime.Now.Date.ToString("dd/MM/yyyy"))),
                    //new XElement("data", Convert.ToString(System.DateTime.Now.Date.ToString())),
                    new XElement("przebieg", ZmienneGlobalne.numCig)));
                xdoc.Save(path);

            }
            catch { MessageBox.Show("Podczas próby zapisu obiekt do zapisu nie został wykryty"); }


        }


        bool makeShureWindow()
        {
            var Result = MessageBox.Show("Czy chcesz wykonać wpis Konserwacji", "Wpis Konserwacji", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (Result == MessageBoxResult.Yes) return true;
            else return false;
        }


        private void WpisCIL_Click(object sender, RoutedEventArgs e)
        {
            var statment = makeShureWindow();
            if (statment == true)
            {
                wpisKonserwacji();
            }
        }

        private void Anuluj_Click(object sender, RoutedEventArgs e)
        {
            GC.Collect();
            this.Close();
        }

      
    }
}
