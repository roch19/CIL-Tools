using NavigationBar.KartaCIL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
using System.Xml.Linq;




namespace NavigationBar
{
    /// <summary>
    /// Logika interakcji dla klasy CILCard.xaml
    /// </summary>
    public partial class CILCard : Window
    {
        public CILCard()
        {
            WymianaGum wg = new WymianaGum();
            wg = null;
            GC.Collect();


            InitializeComponent();
            LoadSodimData();
            GetModulesInfo();
            DrawDataGrid(0);
            LoadData(0);
     

       
            //load();
        }

        
       
        //string TMPlocationModule = "C:\\copy_sodim\\hollow.txt";

        string XMLCILTemplatePath = ZmienneGlobalne.path_template_CIL;
        string XMLSodimData = "C:\\copy_sodim\\data_"+ZmienneGlobalne.numer_sodimatu+".xml";


        string dataPath = "C:\\copy_sodim\\201_70_wpisKartyCIL_2022.r.xml";
        public List<string> czynnosc = new List<string>();
        int templatesNumb = 0;
        int numberOfChildren = 0;

        public class Kalibracje
        {
            public int PD { get; set; }
            public int WG { get; set; }
            public int CIR { get; set; }
            public int HAR { get; set; }
            public int HOLLOW { get; set; }

            public Kalibracje(int pd, int wg, int cir, int har, int hollow)
            {
                PD = pd;
                WG = wg;
                CIR = cir;
                HAR = har;
                HOLLOW = hollow;
            }

        }
        public class Czynnosc
        {
            public int id { get; set; }
            public string module { get; set; }
            public string duty { get; set; }
            public int interwal { get; set; }

            public Czynnosc(int _id, string _module, string _duty, int _interwal)
            {
                id = _id;
                module = _module;
                duty = _duty;
                interwal = _interwal;
            }
        }
        public class Wpis
        {
            public string date { get; set; }
            public string modul { get; set; }
            public string czynnosc { get; set; }
            public string status { get; set; }

            public Wpis(string _date, string _modul, string _czynnosc, string _status)
            {
                this.date = _date;
                this.modul = _modul;
                this.czynnosc = _czynnosc;
                this.status = _status;
            }
        }

        public class Sodim
        { 
            public string nazwa_sodimatu { get; set; }
            public string lokalizacja { get; set; }
            public string wlasciciel { get; set; }
            public string typ_sodimatu_karta_CIL { get; set; }
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

            public Sodim(string nazwa_sodimatu, string lokalizacja, string wlasciciel, string typ_sodimatu_karta_CIL, string typ_sodimatu_gumka, string data_wymiany_gumki, string format_gumki,
                int numcig_z_wymiany_g, int numcycle_z_wymiany_g, string data_wykonanego_cila, int numcig_z_cila, int numcycle_z_cila, string data_wykonanej_konserwacji, 
                int numcig_z_konserwacji, int numcycle_z_konserwacji)
            {
                this.nazwa_sodimatu = nazwa_sodimatu;
                this.lokalizacja = lokalizacja;
                this.wlasciciel = wlasciciel;
                this.typ_sodimatu_karta_CIL = typ_sodimatu_karta_CIL;
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

      
        internal int ChceckIfPD()
        {
            var xdoc = XDocument.Load(XMLCILTemplatePath);
            var templates = xdoc.Root.Descendants("kalibracje")  // trzeba załadować w listę obiektów 
                .Select(x => new Kalibracje(int.Parse(x.Element("PD").Value),
                int.Parse(x.Element("CIR").Value),
                int.Parse(x.Element("HAR").Value),
                int.Parse(x.Element("WG").Value),
                int.Parse(x.Element("VI").Value)));
            int pd=0; 
            foreach (var item in templates)
            {
                pd = int.Parse(item.PD.ToString());
            }
            return pd;
        }


        public void LoadSodimData()
        {
            var xdoc = XDocument.Load(XMLSodimData);
            var soidm = xdoc.Root.Descendants("Sodimat")  // trzeba załadować w listę obiektów 
                .Select(x => new Sodim(x.Element("nazwa_sodimatu").Value,
                x.Element("lokalizacja").Value,
                x.Element("wlasciciel").Value,
                x.Element("typ_sodimatu_karta_CIL").Value,
                x.Element("typ_sodimatu_gumka").Value,
                x.Element("data_wymiany_gumki").Value,
                x.Element("format_gumki").Value,
                int.Parse(x.Element("numcig_z_wymiany_g").Value),
                int.Parse(x.Element("numcycle_z_wymiany_g").Value),
                x.Element("data_wykonanego_cila").Value,
                int.Parse(x.Element("numcig_z_cila").Value),
                int.Parse(x.Element("numcycle_z_cila").Value),
                x.Element("data_wykonanej_konserwacji").Value,
                int.Parse(x.Element("numcig_z_konserwacji").Value),
                int.Parse(x.Element("numcycle_z_konserwacji").Value)));

            foreach (var item in soidm)
            {
                sodimatNametextBlock.Text = item.nazwa_sodimatu.ToString();
                lokalizacjaTextBlock.Text = item.lokalizacja.ToString();
                typSodimatuTextBlock.Text = "Typ: " + item.typ_sodimatu_karta_CIL.ToString().ToUpper();
                formatTextBlock.Text = item.format_gumki.ToString();
                ownerTextBlock.Text = item.wlasciciel.ToString();
                XMLCILTemplatePath += "template_"+ item.typ_sodimatu_karta_CIL.ToString() + ".xml";

                if (item.typ_sodimatu_karta_CIL.ToString().Contains("hollow"))
                {
                    gumkiButt.Visibility = Visibility.Hidden;
                    gumProgBar.Visibility = Visibility.Hidden;
                    gumkiTextBlock.Visibility = Visibility.Hidden;
                    gumContent.Visibility = Visibility.Hidden;
                    formatTextBlock.Visibility = Visibility.Hidden;
                    borderFormatGum.Visibility = Visibility.Hidden;
                }


                switch (item.typ_sodimatu_gumka)
                {
                    case "KS_papierosowy":
                        gumProgBar.Maximum = ZmienneGlobalne.przebieg_KS_papierosowy;
                        var a = ZmienneGlobalne.numCig - item.numcig_z_wymiany_g;
                        gumProgBar.Value = a;
                        gumContent.Content = (ZmienneGlobalne.numCig - item.numcig_z_wymiany_g).ToString() + " / " + ZmienneGlobalne.przebieg_KS_papierosowy.ToString();

                        konsProgBar.Maximum = ZmienneGlobalne.przebieg_Konserwacja_papierosowy;
                        var b = ZmienneGlobalne.numCig - item.numcig_z_konserwacji;
                        konsProgBar.Value = b;
                        konsContent.Content = (ZmienneGlobalne.numCig - item.numcig_z_konserwacji).ToString() + " / " + ZmienneGlobalne.przebieg_Konserwacja_filtrowy.ToString();

                        cilProgBar.Maximum = ZmienneGlobalne.przebieg_CIL_papierosowy;
                        var c = ZmienneGlobalne.numCig - item.numcig_z_cila;
                        cilProgBar.Value = c;
                        cilContent.Content = (ZmienneGlobalne.numCig - item.numcig_z_cila).ToString() + " / " + ZmienneGlobalne.przebieg_CIL_papierosowy.ToString();

                        gumkiTextBlock.Text = (ZmienneGlobalne.termin_KS_papierosowy - ((DateTime.Now - DateTime.Parse(item.data_wymiany_gumki)).Days)).ToString();
                        cilTextBlock.Text = (ZmienneGlobalne.termin_CIL_papierosowy - ((DateTime.Now - DateTime.Parse(item.data_wykonanego_cila)).Days)).ToString();
                        konserwacjaTextBlock.Text = (ZmienneGlobalne.termin_Konserwacja_papierosowy - ((DateTime.Now - DateTime.Parse(item.data_wykonanej_konserwacji)).Days)).ToString();

                                              

                            break;
                    case "DS_papierosowy":
                        gumProgBar.Maximum = ZmienneGlobalne.przebieg_DS_papierosowy;
                        gumProgBar.Value = ZmienneGlobalne.numCig - item.numcig_z_wymiany_g;
                        gumContent.Content = (ZmienneGlobalne.numCig - item.numcig_z_wymiany_g).ToString() + " / " + ZmienneGlobalne.przebieg_DS_papierosowy.ToString();

                        konsProgBar.Maximum = ZmienneGlobalne.przebieg_Konserwacja_papierosowy;
                        konsProgBar.Value = ZmienneGlobalne.numCig - item.numcig_z_konserwacji;
                        konsContent.Content = (ZmienneGlobalne.numCig - item.numcig_z_konserwacji).ToString() + " / " + ZmienneGlobalne.przebieg_Konserwacja_filtrowy.ToString();


                        cilProgBar.Maximum = ZmienneGlobalne.przebieg_CIL_papierosowy;
                        cilProgBar.Value = ZmienneGlobalne.numCig - item.numcig_z_cila;
                        cilContent.Content = (ZmienneGlobalne.numCig - item.numcig_z_cila).ToString() + " / " + ZmienneGlobalne.przebieg_CIL_papierosowy.ToString();

                gumkiTextBlock.Text = (ZmienneGlobalne.termin_DS_papierosowy - ((DateTime.Now - DateTime.Parse(item.data_wymiany_gumki)).Days)).ToString();
                cilTextBlock.Text = (ZmienneGlobalne.termin_CIL_papierosowy - ((DateTime.Now - DateTime.Parse(item.data_wykonanego_cila)).Days)).ToString();
                konserwacjaTextBlock.Text = (ZmienneGlobalne.termin_Konserwacja_papierosowy - ((DateTime.Now - DateTime.Parse(item.data_wykonanej_konserwacji)).Days)).ToString();

                        break;
                    case "SS_papierosowy":
                        gumProgBar.Maximum = ZmienneGlobalne.przebieg_SS_papierosowy;
                        gumProgBar.Value = ZmienneGlobalne.numCig - item.numcig_z_wymiany_g;
                        gumContent.Content = (ZmienneGlobalne.numCig - item.numcig_z_wymiany_g).ToString() + " / " + ZmienneGlobalne.przebieg_SS_papierosowy.ToString();

                        konsProgBar.Maximum = ZmienneGlobalne.przebieg_Konserwacja_papierosowy;
                        konsProgBar.Value = ZmienneGlobalne.numCig - item.numcig_z_konserwacji;
                        konsContent.Content = (ZmienneGlobalne.numCig - item.numcig_z_konserwacji).ToString() + " / " + ZmienneGlobalne.przebieg_Konserwacja_filtrowy.ToString();


                        cilProgBar.Maximum = ZmienneGlobalne.przebieg_CIL_papierosowy;
                        cilProgBar.Value = ZmienneGlobalne.numCig - item.numcig_z_cila;
                        cilContent.Content = (ZmienneGlobalne.numCig - item.numcig_z_cila).ToString() + " / " + ZmienneGlobalne.przebieg_CIL_papierosowy.ToString();

                        gumkiTextBlock.Text = (ZmienneGlobalne.termin_SS_papierosowy - ((DateTime.Now - DateTime.Parse(item.data_wymiany_gumki)).Days)).ToString();
                        cilTextBlock.Text = (ZmienneGlobalne.termin_CIL_papierosowy - ((DateTime.Now - DateTime.Parse(item.data_wykonanego_cila)).Days)).ToString();
                        konserwacjaTextBlock.Text = (ZmienneGlobalne.termin_Konserwacja_papierosowy - ((DateTime.Now - DateTime.Parse(item.data_wykonanej_konserwacji)).Days)).ToString();
                        break;
                    case "KS_filtrowy":
                        gumProgBar.Maximum = ZmienneGlobalne.przebieg_KS_filtrowy;
                        gumProgBar.Value = ZmienneGlobalne.numCig - item.numcig_z_wymiany_g;
                        gumContent.Content = (ZmienneGlobalne.numCig - item.numcig_z_wymiany_g).ToString() + " / " + ZmienneGlobalne.przebieg_KS_filtrowy.ToString();

                        konsProgBar.Maximum = ZmienneGlobalne.przebieg_Konserwacja_filtrowy;
                        konsProgBar.Value = ZmienneGlobalne.numCig - item.numcig_z_konserwacji;
                        konsContent.Content = (ZmienneGlobalne.numCig - item.numcig_z_konserwacji).ToString() + " / " + ZmienneGlobalne.przebieg_Konserwacja_filtrowy.ToString();


                        cilProgBar.Maximum = ZmienneGlobalne.przebieg_CIL_filtrowy;
                        cilProgBar.Value = ZmienneGlobalne.numCig - item.numcig_z_cila;
                        cilContent.Content = (ZmienneGlobalne.numCig - item.numcig_z_cila).ToString() + " / " + ZmienneGlobalne.przebieg_CIL_filtrowy.ToString();

                        gumkiTextBlock.Text = (ZmienneGlobalne.termin_KS_filtrowy - ((DateTime.Now - DateTime.Parse(item.data_wymiany_gumki)).Days)).ToString();
                        cilTextBlock.Text = (ZmienneGlobalne.przebieg_CIL_filtrowy - ((DateTime.Now - DateTime.Parse(item.data_wykonanego_cila)).Days)).ToString();
                        konserwacjaTextBlock.Text = (ZmienneGlobalne.termin_Konserwacja_filtrowy - ((DateTime.Now - DateTime.Parse(item.data_wykonanej_konserwacji)).Days)).ToString();
                        break;
                    case "DS_filtrowy":
                        gumProgBar.Maximum = ZmienneGlobalne.przebieg_DS_filtrowy;
                        gumProgBar.Value = ZmienneGlobalne.numCig - item.numcig_z_wymiany_g;
                        gumContent.Content = (ZmienneGlobalne.numCig - item.numcig_z_wymiany_g).ToString() + " / " + ZmienneGlobalne.przebieg_DS_filtrowy.ToString();

                        konsProgBar.Maximum = ZmienneGlobalne.przebieg_Konserwacja_filtrowy;
                        konsProgBar.Value = ZmienneGlobalne.numCig - item.numcig_z_konserwacji;
                        konsContent.Content = (ZmienneGlobalne.numCig - item.numcig_z_konserwacji).ToString() + " / " + ZmienneGlobalne.przebieg_Konserwacja_filtrowy.ToString();

                        cilProgBar.Maximum = ZmienneGlobalne.przebieg_CIL_filtrowy;
                        cilProgBar.Value = ZmienneGlobalne.numCig - item.numcig_z_cila;
                        cilContent.Content = (ZmienneGlobalne.numCig - item.numcig_z_cila).ToString() + " / " + ZmienneGlobalne.przebieg_CIL_filtrowy.ToString();

                        gumkiTextBlock.Text = (ZmienneGlobalne.termin_DS_filtrowy - ((DateTime.Now - DateTime.Parse(item.data_wymiany_gumki)).Days)).ToString();
                        cilTextBlock.Text = (ZmienneGlobalne.przebieg_CIL_filtrowy - ((DateTime.Now - DateTime.Parse(item.data_wykonanego_cila)).Days)).ToString();
                        konserwacjaTextBlock.Text = (ZmienneGlobalne.termin_Konserwacja_filtrowy - ((DateTime.Now - DateTime.Parse(item.data_wykonanej_konserwacji)).Days)).ToString();

                        break;
                    case "SS_filtrowy":
                        gumProgBar.Maximum = ZmienneGlobalne.przebieg_SS_filtrowy;
                        gumProgBar.Value = ZmienneGlobalne.numCig - item.numcig_z_wymiany_g;
                        gumContent.Content = (ZmienneGlobalne.numCig - item.numcig_z_wymiany_g).ToString() + " / " + ZmienneGlobalne.przebieg_SS_filtrowy.ToString();

                        konsProgBar.Maximum = ZmienneGlobalne.przebieg_Konserwacja_filtrowy;
                        konsProgBar.Value = ZmienneGlobalne.numCig - item.numcig_z_konserwacji;
                        konsContent.Content = (ZmienneGlobalne.numCig - item.numcig_z_konserwacji).ToString() + " / " + ZmienneGlobalne.przebieg_Konserwacja_filtrowy.ToString();

                        cilProgBar.Maximum = ZmienneGlobalne.przebieg_CIL_filtrowy;
                        cilProgBar.Value = ZmienneGlobalne.numCig - item.numcig_z_cila;
                        cilContent.Content = (ZmienneGlobalne.numCig - item.numcig_z_cila).ToString() + " / " + ZmienneGlobalne.przebieg_CIL_filtrowy.ToString();

                        gumkiTextBlock.Text = (ZmienneGlobalne.termin_SS_filtrowy - ((DateTime.Now - DateTime.Parse(item.data_wymiany_gumki)).Days)).ToString();
                        cilTextBlock.Text = (ZmienneGlobalne.przebieg_CIL_filtrowy - ((DateTime.Now - DateTime.Parse(item.data_wykonanego_cila)).Days)).ToString();
                        konserwacjaTextBlock.Text = (ZmienneGlobalne.termin_Konserwacja_filtrowy - ((DateTime.Now - DateTime.Parse(item.data_wykonanej_konserwacji)).Days)).ToString();
                        break;

                    default:
                        break;
                }

                var avrg = (konsProgBar.Value * 100) / konsProgBar.Maximum;

                if (avrg >= 0 && avrg < 50)
                {
                    konsProgBar.Foreground = Brushes.Green; ; // zieleń
                }

                else if (avrg >= 50 && avrg < 80)
                {
                    konsProgBar.Foreground = Brushes.Yellow; // żółć
                }

                else if (avrg >= 80 && avrg < 97)
                {
                    konsProgBar.Foreground = Brushes.Orange; // pomarańcz
                }

                else if (avrg >= 97) //czerwo
                {
                    konsProgBar.Foreground = Brushes.Red;
                }


                avrg = (cilProgBar.Value * 100) / cilProgBar.Maximum;

                if (avrg >= 0 && avrg < 50)
                {
                    cilProgBar.Foreground = Brushes.Green;
                }

                else if (avrg >= 50 && avrg < 80)
                {
                    cilProgBar.Foreground = Brushes.Yellow;
                }

                else if (avrg >= 80 && avrg < 95)
                {
                    cilProgBar.Foreground = Brushes.Orange;
                }

                else if (avrg >=  99)
                {
                    cilProgBar.Foreground = Brushes.Red;
                }


                avrg = (gumProgBar.Value * 100) / gumProgBar.Maximum;

                if (avrg >= 0 && avrg < 50)
                {
                    gumProgBar.Foreground = Brushes.Green;
                }

                else if (avrg >= 50 && avrg < 80)
                {
                    gumProgBar.Foreground = Brushes.Yellow;
                }

                else if (avrg >= 80 && avrg < 95)
                {
                    gumProgBar.Foreground = Brushes.Orange;
                }

                else if (avrg > 99)
                {
                    gumProgBar.Foreground = Brushes.Red ;
                }


                if(int.Parse(cilTextBlock.Text)<= 6 && int.Parse(cilTextBlock.Text)>=1 )
                {
                    cilTextBlock.Background = Brushes.Yellow;
                    cilTextBlock.Text += " dni";
                }
                else if (int.Parse(cilTextBlock.Text) <1)
                {
                    cilTextBlock.Background = Brushes.Red;
                    cilTextBlock.Text += " dni";
                }

                if (int.Parse(gumkiTextBlock.Text) <= 5 && int.Parse(gumkiTextBlock.Text) >= 1)
                {
                    gumkiTextBlock.Background = Brushes.Yellow;
                    gumkiTextBlock.Text += " dni";
                }
                else if (int.Parse(gumkiTextBlock.Text) < 1)
                {
                    gumkiTextBlock.Background = Brushes.Red;
                    gumkiTextBlock.Text += " dni";
                }

                if (int.Parse(konserwacjaTextBlock.Text) < 15 && int.Parse(konserwacjaTextBlock.Text) >= 1)
                {
                    konserwacjaTextBlock.Background = Brushes.Yellow;
                    konserwacjaTextBlock.Text += " dni";
                }
                else if (int.Parse(konserwacjaTextBlock.Text) < 1)
                {
                    konserwacjaTextBlock.Background = Brushes.Red;
                    konserwacjaTextBlock.Text += " dni";
                }


                //  if()

                //switch (avrg)
                //{
                //    case avrg >= 50:

                //    default:
                //        break;
                //}
                //gumProgBar.Maximum = ZmienneGlobalne.przebieg_KS_papierosowy;

                //gumProgBar.Value = ZmienneGlobalne.numCig - item.numcig_z_wymiany_g;

                //gumContent.Content = (ZmienneGlobalne.numCig - item.numcig_z_wymiany_g).ToString() + " / " + ZmienneGlobalne.przebieg_KS_papierosowy.ToString();

            }
        }


        internal bool LoadSodimDataBool()
        {
            bool ifHollow = false;
            var xdoc = XDocument.Load(XMLSodimData);
            var soidm = xdoc.Root.Descendants("Sodimat")  // trzeba załadować w listę obiektów 
                .Select(x => new Sodim(x.Element("nazwa_sodimatu").Value,
                x.Element("lokalizacja").Value,
                x.Element("wlasciciel").Value,
                x.Element("typ_sodimatu_karta_CIL").Value,
                x.Element("typ_sodimatu_gumka").Value,
                x.Element("data_wymiany_gumki").Value,
                x.Element("format_gumki").Value,
                int.Parse(x.Element("numcig_z_wymiany_g").Value),
                int.Parse(x.Element("numcycle_z_wymiany_g").Value),
                x.Element("data_wykonanego_cila").Value,
                int.Parse(x.Element("numcig_z_cila").Value),
                int.Parse(x.Element("numcycle_z_cila").Value),
                x.Element("data_wykonanej_konserwacji").Value,
                int.Parse(x.Element("numcig_z_konserwacji").Value),
                int.Parse(x.Element("numcycle_z_konserwacji").Value)));

            foreach (var item in soidm)
            {
                sodimatNametextBlock.Text = item.nazwa_sodimatu.ToString();
                lokalizacjaTextBlock.Text = item.lokalizacja.ToString();
                ownerTextBlock.Text = item.wlasciciel.ToString();




                if (item.typ_sodimatu_karta_CIL.ToString().Contains("hollow"))
                {
                    ifHollow = true;
                }


                switch (item.typ_sodimatu_gumka)
                {
                    case "KS_papierosowy":

                        if(!ifHollow)
                        {
                            gumProgBar.Maximum = ZmienneGlobalne.przebieg_KS_papierosowy;
                            var a = ZmienneGlobalne.numCig - item.numcig_z_wymiany_g;
                            gumProgBar.Value = a;
                            gumContent.Content = (ZmienneGlobalne.numCig - item.numcig_z_wymiany_g).ToString() + " / " + ZmienneGlobalne.przebieg_KS_papierosowy.ToString();
                            gumkiTextBlock.Text = (ZmienneGlobalne.termin_KS_papierosowy - ((DateTime.Now - DateTime.Parse(item.data_wymiany_gumki)).Days)).ToString();

                        }

                        konsProgBar.Maximum = ZmienneGlobalne.przebieg_Konserwacja_papierosowy;
                        var b = ZmienneGlobalne.numCig - item.numcig_z_konserwacji;
                        konsProgBar.Value = b;
                        konsContent.Content = (ZmienneGlobalne.numCig - item.numcig_z_konserwacji).ToString() + " / " + ZmienneGlobalne.przebieg_Konserwacja_filtrowy.ToString();

                        cilProgBar.Maximum = ZmienneGlobalne.przebieg_CIL_papierosowy;
                        var c = ZmienneGlobalne.numCig - item.numcig_z_cila;
                        cilProgBar.Value = c;
                        cilContent.Content = (ZmienneGlobalne.numCig - item.numcig_z_cila).ToString() + " / " + ZmienneGlobalne.przebieg_CIL_papierosowy.ToString();


                            cilTextBlock.Text = (ZmienneGlobalne.termin_CIL_papierosowy - ((DateTime.Now - DateTime.Parse(item.data_wykonanego_cila)).Days)).ToString();
                        konserwacjaTextBlock.Text = (ZmienneGlobalne.termin_Konserwacja_papierosowy - ((DateTime.Now - DateTime.Parse(item.data_wykonanej_konserwacji)).Days)).ToString();



                        break;
                    case "DS_papierosowy":
                        if (!ifHollow)
                        {
                            gumProgBar.Maximum = ZmienneGlobalne.przebieg_DS_papierosowy;
                            gumProgBar.Value = ZmienneGlobalne.numCig - item.numcig_z_wymiany_g;
                            gumContent.Content = (ZmienneGlobalne.numCig - item.numcig_z_wymiany_g).ToString() + " / " + ZmienneGlobalne.przebieg_DS_papierosowy.ToString();
                            gumkiTextBlock.Text = (ZmienneGlobalne.termin_DS_papierosowy - ((DateTime.Now - DateTime.Parse(item.data_wymiany_gumki)).Days)).ToString();

                        }

                        konsProgBar.Maximum = ZmienneGlobalne.przebieg_Konserwacja_papierosowy;
                        konsProgBar.Value = ZmienneGlobalne.numCig - item.numcig_z_konserwacji;
                        konsContent.Content = (ZmienneGlobalne.numCig - item.numcig_z_konserwacji).ToString() + " / " + ZmienneGlobalne.przebieg_Konserwacja_filtrowy.ToString();


                        cilProgBar.Maximum = ZmienneGlobalne.przebieg_CIL_papierosowy;
                        cilProgBar.Value = ZmienneGlobalne.numCig - item.numcig_z_cila;
                        cilContent.Content = (ZmienneGlobalne.numCig - item.numcig_z_cila).ToString() + " / " + ZmienneGlobalne.przebieg_CIL_papierosowy.ToString();

                      

                        cilTextBlock.Text = (ZmienneGlobalne.termin_CIL_papierosowy - ((DateTime.Now - DateTime.Parse(item.data_wykonanego_cila)).Days)).ToString();
                        konserwacjaTextBlock.Text = (ZmienneGlobalne.termin_Konserwacja_papierosowy - ((DateTime.Now - DateTime.Parse(item.data_wykonanej_konserwacji)).Days)).ToString();

                        break;
                    case "SS_papierosowy":
                        if(!ifHollow)
                        {
                            gumProgBar.Maximum = ZmienneGlobalne.przebieg_SS_papierosowy;
                            gumProgBar.Value = ZmienneGlobalne.numCig - item.numcig_z_wymiany_g;
                            gumContent.Content = (ZmienneGlobalne.numCig - item.numcig_z_wymiany_g).ToString() + " / " + ZmienneGlobalne.przebieg_SS_papierosowy.ToString();
                            gumkiTextBlock.Text = (ZmienneGlobalne.termin_SS_papierosowy - ((DateTime.Now - DateTime.Parse(item.data_wymiany_gumki)).Days)).ToString();

                        }

                        konsProgBar.Maximum = ZmienneGlobalne.przebieg_Konserwacja_papierosowy;
                        konsProgBar.Value = ZmienneGlobalne.numCig - item.numcig_z_konserwacji;
                        konsContent.Content = (ZmienneGlobalne.numCig - item.numcig_z_konserwacji).ToString() + " / " + ZmienneGlobalne.przebieg_Konserwacja_filtrowy.ToString();


                        cilProgBar.Maximum = ZmienneGlobalne.przebieg_CIL_papierosowy;
                        cilProgBar.Value = ZmienneGlobalne.numCig - item.numcig_z_cila;
                        cilContent.Content = (ZmienneGlobalne.numCig - item.numcig_z_cila).ToString() + " / " + ZmienneGlobalne.przebieg_CIL_papierosowy.ToString();

                      
                        cilTextBlock.Text = (ZmienneGlobalne.termin_CIL_papierosowy - ((DateTime.Now - DateTime.Parse(item.data_wykonanego_cila)).Days)).ToString();
                        konserwacjaTextBlock.Text = (ZmienneGlobalne.termin_Konserwacja_papierosowy - ((DateTime.Now - DateTime.Parse(item.data_wykonanej_konserwacji)).Days)).ToString();
                        break;
                    case "KS_filtrowy":
                        if(!ifHollow)
                        {
                            gumProgBar.Maximum = ZmienneGlobalne.przebieg_KS_filtrowy;
                            gumProgBar.Value = ZmienneGlobalne.numCig - item.numcig_z_wymiany_g;
                            gumContent.Content = (ZmienneGlobalne.numCig - item.numcig_z_wymiany_g).ToString() + " / " + ZmienneGlobalne.przebieg_KS_filtrowy.ToString();
                            gumkiTextBlock.Text = (ZmienneGlobalne.termin_KS_filtrowy - ((DateTime.Now - DateTime.Parse(item.data_wymiany_gumki)).Days)).ToString();
                        }

                        konsProgBar.Maximum = ZmienneGlobalne.przebieg_Konserwacja_filtrowy;
                        konsProgBar.Value = ZmienneGlobalne.numCig - item.numcig_z_konserwacji;
                        konsContent.Content = (ZmienneGlobalne.numCig - item.numcig_z_konserwacji).ToString() + " / " + ZmienneGlobalne.przebieg_Konserwacja_filtrowy.ToString();


                        cilProgBar.Maximum = ZmienneGlobalne.przebieg_CIL_filtrowy;
                        cilProgBar.Value = ZmienneGlobalne.numCig - item.numcig_z_cila;
                        cilContent.Content = (ZmienneGlobalne.numCig - item.numcig_z_cila).ToString() + " / " + ZmienneGlobalne.przebieg_CIL_filtrowy.ToString();

                   
                     
                        cilTextBlock.Text = (ZmienneGlobalne.przebieg_CIL_filtrowy - ((DateTime.Now - DateTime.Parse(item.data_wykonanego_cila)).Days)).ToString();
                        konserwacjaTextBlock.Text = (ZmienneGlobalne.termin_Konserwacja_filtrowy - ((DateTime.Now - DateTime.Parse(item.data_wykonanej_konserwacji)).Days)).ToString();
                        break;
                    case "DS_filtrowy":

                        if(!ifHollow)
                        {
                            gumProgBar.Maximum = ZmienneGlobalne.przebieg_DS_filtrowy;
                            gumProgBar.Value = ZmienneGlobalne.numCig - item.numcig_z_wymiany_g;
                            gumContent.Content = (ZmienneGlobalne.numCig - item.numcig_z_wymiany_g).ToString() + " / " + ZmienneGlobalne.przebieg_DS_filtrowy.ToString();
                            gumkiTextBlock.Text = (ZmienneGlobalne.termin_DS_filtrowy - ((DateTime.Now - DateTime.Parse(item.data_wymiany_gumki)).Days)).ToString();
                        }
                 
                        konsProgBar.Maximum = ZmienneGlobalne.przebieg_Konserwacja_filtrowy;
                        konsProgBar.Value = ZmienneGlobalne.numCig - item.numcig_z_konserwacji;
                        konsContent.Content = (ZmienneGlobalne.numCig - item.numcig_z_konserwacji).ToString() + " / " + ZmienneGlobalne.przebieg_Konserwacja_filtrowy.ToString();

                        cilProgBar.Maximum = ZmienneGlobalne.przebieg_CIL_filtrowy;
                        cilProgBar.Value = ZmienneGlobalne.numCig - item.numcig_z_cila;
                        cilContent.Content = (ZmienneGlobalne.numCig - item.numcig_z_cila).ToString() + " / " + ZmienneGlobalne.przebieg_CIL_filtrowy.ToString();

              
                        cilTextBlock.Text = (ZmienneGlobalne.przebieg_CIL_filtrowy - ((DateTime.Now - DateTime.Parse(item.data_wykonanego_cila)).Days)).ToString();
                        konserwacjaTextBlock.Text = (ZmienneGlobalne.termin_Konserwacja_filtrowy - ((DateTime.Now - DateTime.Parse(item.data_wykonanej_konserwacji)).Days)).ToString();

                        break;
                    case "SS_filtrowy":
                        if(!ifHollow)
                        {
                            gumProgBar.Maximum = ZmienneGlobalne.przebieg_SS_filtrowy;
                            gumProgBar.Value = ZmienneGlobalne.numCig - item.numcig_z_wymiany_g;
                            gumContent.Content = (ZmienneGlobalne.numCig - item.numcig_z_wymiany_g).ToString() + " / " + ZmienneGlobalne.przebieg_SS_filtrowy.ToString();
                            gumkiTextBlock.Text = (ZmienneGlobalne.termin_SS_filtrowy - ((DateTime.Now - DateTime.Parse(item.data_wymiany_gumki)).Days)).ToString();
                        }

                        konsProgBar.Maximum = ZmienneGlobalne.przebieg_Konserwacja_filtrowy;
                        konsProgBar.Value = ZmienneGlobalne.numCig - item.numcig_z_konserwacji;
                        konsContent.Content = (ZmienneGlobalne.numCig - item.numcig_z_konserwacji).ToString() + " / " + ZmienneGlobalne.przebieg_Konserwacja_filtrowy.ToString();

                        cilProgBar.Maximum = ZmienneGlobalne.przebieg_CIL_filtrowy;
                        cilProgBar.Value = ZmienneGlobalne.numCig - item.numcig_z_cila;
                        cilContent.Content = (ZmienneGlobalne.numCig - item.numcig_z_cila).ToString() + " / " + ZmienneGlobalne.przebieg_CIL_filtrowy.ToString();

                       
                        cilTextBlock.Text = (ZmienneGlobalne.przebieg_CIL_filtrowy - ((DateTime.Now - DateTime.Parse(item.data_wykonanego_cila)).Days)).ToString();
                        konserwacjaTextBlock.Text = (ZmienneGlobalne.termin_Konserwacja_filtrowy - ((DateTime.Now - DateTime.Parse(item.data_wykonanej_konserwacji)).Days)).ToString();
                        break;

                    default:
                        break;
                }

                var avrg = (konsProgBar.Value * 100) / konsProgBar.Maximum;

                if (avrg >= 0 && avrg < 50)
                {
                }

                else if (avrg >= 91 && avrg < 98)
                {
                
                    return true;
                }

                else if (avrg >= 99) //czerwo
                {
                   
                    return true;
                }


                avrg = (cilProgBar.Value * 100) / cilProgBar.Maximum;

                if (avrg >= 0 && avrg < 50)
                {
             
                }

                else if (avrg >= 50 && avrg < 80)
                {
                 
                }

                else if (avrg >= 80 && avrg < 95)
                {
                  
                    return true;
                }

                else if (avrg >= 99)
                {
               
                    return true;
                }



                if (!ifHollow)
                {
                    avrg = (gumProgBar.Value * 100) / gumProgBar.Maximum;

                    if (avrg >= 0 && avrg < 50)
                    {

                    }

                    else if (avrg >= 50 && avrg < 80)
                    {


                    }

                    else if (avrg >= 80 && avrg < 95)
                    {

                        return true;
                    }

                    else if (avrg > 99)
                    {

                        return true;
                    }

                }


                if (int.Parse(cilTextBlock.Text) < 5 && int.Parse(cilTextBlock.Text) >= 1)
                {

                    return true;
                }
                else if (int.Parse(cilTextBlock.Text) < 1)
                { 

                    return true;
                }
                if(!ifHollow)
                {
                    if (int.Parse(gumkiTextBlock.Text) < 5 && int.Parse(gumkiTextBlock.Text) >= 1)
                    {

                        return true;
                    }
                    else if (int.Parse(gumkiTextBlock.Text) < 1)
                    {

                        return true;
                    }

                }

                if (int.Parse(konserwacjaTextBlock.Text) < 15 && int.Parse(konserwacjaTextBlock.Text) >= 1)
                {
 
                    return true;
                }
                else if (int.Parse(konserwacjaTextBlock.Text) < 1)
                {
  
                    return true;
                }
                return false;
            }
            return false;
        }

        public void GetModulesInfo()
        {
            //Load_config_data load = new Load_config_data();
            //load.load();



         //   MessageBox.Show("NUMCIG" + ZmienneGlobalne.numCig) ;
          //  ZmienneGlobalne.numCig zm = new ZmienneGlobalne();
         
            //DataSet ds = new DataSet();
            //ds.ReadXml(XMLCILTemplatePath);
            //foreach(DataRow dr in ds.Tables["czynnosc"].Rows)
            //{
            //    for (int i = 0; i < dr.ItemArray.Length; i++)
            //    {
            //        MessageBox.Show(dr[i].ToString());
            //    }
            //}
            // LINQ 
            //aWhere(x => x.Elements().Last().Value == "HOLLOW")

            var xdoc = XDocument.Load(XMLCILTemplatePath);
            var templates = xdoc.Root.Descendants("czynnosc")  // trzeba załadować w listę obiektów 
                .Select(x => new Czynnosc(int.Parse(x.Attribute("id").Value),
                x.Element("module").Value,
                x.Element("duty").Value,
                int.Parse(x.Element("interwal").Value)));
             //xdoc = XDocument.Load(XMLCILTemplatePath);
            //var kalala = xdoc.Root.Descendants("kalibracje").Select(x => new Kalibracje(int.Parse(x.Element("PD").Value),
            //    int.Parse(x.Element("CIR").Value),
            //    int.Parse(x.Element("HAR").Value),
            //    int.Parse(x.Element("WG").Value),
            //    int.Parse(x.Element("HOLLOW").Value)));

            //foreach (var item in kalala)
            //{
            //    MessageBox.Show("HAR" + item.HAR +"PD"+ item.PD );
            //}

            templatesNumb = templates.Count() +1 ;

            foreach (var item in templates)
            {
                czynnosc.Add(item.duty);
            }

            dataTextBlock.Text = DateTime.Now.ToString("Y");

            ColumnDefinition colDef1 = new ColumnDefinition();
            ColumnDefinition colDef2 = new ColumnDefinition();

            ///*** Dodawanie wierszy tytułowych 
            ///

            colDef1.Width = new GridLength(80);
            colDef2.Width = new GridLength(150);
            mainGridPanel.ColumnDefinitions.Add(colDef1);
            mainGridPanel.ColumnDefinitions.Add(colDef2);
         

            TextBlock tb1 = new TextBlock();
            TextBlock tb2 = new TextBlock();
            tb1.Text = "Moduł";
            tb2.Text = "Czynność";

            tb1.Background = Brushes.Gray;
            tb2.Background = Brushes.Gray;

            tb1.FontSize = 15;
            tb2.FontSize = 15;

            tb2.FontWeight = FontWeights.Bold;
            tb2.FontWeight = FontWeights.Bold;


            RowDefinition gridRow1 = new RowDefinition();
            RowDefinition gridRow2 = new RowDefinition();
            mainGridPanel.RowDefinitions.Add(gridRow1);
            mainGridPanel.RowDefinitions.Add(gridRow2);

            Grid.SetRow(tb1, 0);
            Grid.SetColumn(tb1, 0);

            Grid.SetRow(tb2, 0);
            Grid.SetColumn(tb2, 1);

            //Grid.SetColumn(border, 0);
            //Grid.SetRow(border, 0);

            //Grid.SetColumn(border2, 0);
            //Grid.SetRow(border2, 1);

            mainGridPanel.Children.Add(tb1);
            mainGridPanel.Children.Add(tb2);
            //mainGridPanel.Children.Add(border2);
            //mainGridPanel.Children.Add(border);

            int i = 1;

            templates.ToList<Czynnosc>().Sort((p, q) => p.interwal.CompareTo(q.interwal));

            // Dodawanie komórek z definicjami wykonywanych czynności 
            foreach (var item in templates)
            {
                TextBlock tb3 = new TextBlock();
                TextBlock tb4 = new TextBlock();
                RowDefinition gridRow3 = new RowDefinition();
                RowDefinition gridRow4 = new RowDefinition();
           
                gridRow3.Height = new GridLength(30);
                
                gridRow4.Height = new GridLength(30);
                
                mainGridPanel.RowDefinitions.Add(gridRow3);
                mainGridPanel.RowDefinitions.Add(gridRow4);

                tb3.Text = item.module;
                tb4.Text = item.duty ;

                switch(item.interwal.ToString())
                {
                    case "1":
                        tb4.Background = Brushes.Orange;
                        break;

                    case "7":
                        tb4.Background = Brushes.SkyBlue;
                        break;

                    case "14":
                        tb4.Background = Brushes.SandyBrown;
                        break;

                    case "30":
                        tb4.Background = Brushes.IndianRed;
                        break;

                    default:
                        break;

                }

                switch (item.module.ToString())
                {
                    case "PD":
                        tb3.Background = Brushes.Green;
                        break;

                    case "HAR":
                        tb3.Background = Brushes.Red;
                        break;
                    case "ALL":
                        tb3.Background = Brushes.Gray;
                        break;
                    case "WG":
                        tb3.Background = Brushes.Orange;
                        break;
                    case "DIA":
                        tb3.Background = Brushes.Yellow;
                        break;

                    default:
                        tb3.Background = Brushes.Aqua;
                        break;
                }

                
                //tb4.Background = Brushes.WhiteSmoke;

                tb3.TextAlignment = TextAlignment.Center;
                tb4.TextAlignment = TextAlignment.Center;
                tb4.TextWrapping = TextWrapping.Wrap;
                tb3.FontSize = 10;
                tb4.FontSize = 10;

                tb3.FontWeight = FontWeights.Bold;
                tb4.FontWeight = FontWeights.Bold;

                Grid.SetRow(tb3, i);
                Grid.SetColumn(tb3, 0);

                Grid.SetRow(tb4, i);
                Grid.SetColumn(tb4, 1);

                //Grid.SetColumn(border, i);
                //Grid.SetRow(border, 0);

                //Grid.SetColumn(border, i);
                //Grid.SetRow(border, 1);

                //btn.Background = new SolidColorBrush(Colors.Azure);
                //btn.Foreground = new SolidColorBrush(Colors.Black);
                /* stackPanelContainer.Children.Add(btn)*/
                mainGridPanel.Children.Add(tb3);
                mainGridPanel.Children.Add(tb4);
                numberOfChildren = mainGridPanel.Children.Count; 
                i++;
            }
        }

        public void DrawDataGrid(int way)
        {
            DateTime tmp;
            tmp = DateTime.Parse(dataTextBlock.Text);
           // dataTextBlock.Text = tmp.AddMonths(way).ToString("Y");
            // Tworzenie komórek dat
           
            int days = DateTime.DaysInMonth(tmp.Year, tmp.Month);
            int widthOfDataBox = 670 / days; 

            int j;
            // MessageBox.Show("Ilość dni" + days);
            for (j = 0; j < days; j++)
            {

                TextBlock tb12 = new TextBlock();
                ColumnDefinition columnDefinition = new ColumnDefinition();

                columnDefinition.Width = new GridLength(widthOfDataBox);
                Grid.SetRow(tb12, 0);
                Grid.SetColumn(tb12, j + 2);
                tb12.TextAlignment = TextAlignment.Center;
                tb12.FontWeight = FontWeights.Bold;
                tb12.Text = (j + 1).ToString();



                mainGridPanel.ColumnDefinitions.Add(columnDefinition);
                mainGridPanel.Children.Add(tb12);
            }
         
            j = j + 2;
            for (int a = 0; a < j; a++)
            {
                for (int y = 0; y < templatesNumb; y++)
                {
                    Border border = new Border()
                    {
                        BorderThickness = new Thickness()
                        {
                            Bottom = 1,
                            Left = 1,
                            Right = 1,
                            Top = 1
                        },
                        BorderBrush = new SolidColorBrush(Colors.Black)
                    };
                    TextBlock tb3 = new TextBlock();

                    if (y > 0 && a > 1)
                    {
                        if(DateTime.Now.Month == tmp.Month && DateTime.Now.Year == tmp.Year)
                        {
                            if (a <= DateTime.Now.Day)
                            {
                                tb3.Background = new SolidColorBrush(Color.FromRgb(210, 210, 210));
                            }
                            else if (a - 1 == DateTime.Now.Day)
                            {
                                tb3.Background = new SolidColorBrush(Color.FromRgb(172, 255, 214));
                            }
                        }
                        else tb3.Background = new SolidColorBrush(Color.FromRgb(210, 210, 210));

                        tb3.FontWeight = FontWeights.Bold;
                        Grid.SetColumn(tb3, a);
                        Grid.SetRow(tb3, y);
                        mainGridPanel.Children.Add(tb3);
                    }

                    Grid.SetColumn(border, a);
                    Grid.SetRow(border, y);

                    mainGridPanel.Children.Add(border);

                }
            }
        }

        public void RemoveChildren()
        {
            try
            {
                mainGridPanel.Children.RemoveRange(numberOfChildren, mainGridPanel.Children.Count);
                GC.Collect();
            }
            catch (Exception e) { }
            
        }

        public void LoadData(int way)
        {
            //Get actual data month + year np maj 2022
            DateTime tmp;
            tmp = DateTime.Parse(dataTextBlock.Text); 
            tmp = tmp.AddMonths(way);
        
            try
            {
                var xdoc = XDocument.Load(dataPath);
                var wpisy = xdoc.Root.Descendants("Wpis")  // trzeba załadować w listę obiektów 
                    .Select(x => new Wpis(x.Element("data").Value,
                    x.Element("modul").Value,
                    x.Element("czynnosc").Value,
                    x.Element("status").Value));
                
                foreach (var item in wpisy)
                {
                   // MessageBox.Show(item.date.ToString());

                    if(DateTime.Parse(item.date).Month == DateTime.Parse(dataTextBlock.Text).Month &&
                        DateTime.Parse(item.date).Year == DateTime.Parse(dataTextBlock.Text).Year)
                    {
                        int IDdzien;
                        int IDmod;
                        //if(czynnosc.Contains(item.czynnosc))
                        //{
                        //    MessageBox.Show("HERE IS JOHNY!");
                        //}
                      //  IDmod = czynnosc.Contains(item.czynnosc); // 
                        IDmod = czynnosc.IndexOf(item.czynnosc) +1; // 
                        IDdzien = int.Parse(DateTime.Parse(item.date).Day.ToString())+1;
                        TextBlock tb = new TextBlock();
                        switch (item.status.ToString())
                        {
                            case "A":
                                tb.Text = "V";
                                break;
                            case "B":
                                tb.Text = "X";
                                break;
                            case "C":
                                tb.Text = "-";
                                break;
                            default:
                                break;
                        }
                

                        tb.VerticalAlignment = VerticalAlignment.Center; 
                        tb.HorizontalAlignment = HorizontalAlignment.Center;
                        tb.FontSize = 15;
                        tb.FontWeight = FontWeights.Bold;
                        tb.Foreground = new SolidColorBrush(Color.FromRgb(0, 176, 31));
                        Grid.SetColumn(tb, IDdzien);
                        Grid.SetRow(tb, IDmod);

                        mainGridPanel.Children.Add(tb);
                    }
                }

                    
            }

            catch
            {
                MessageBox.Show("Błąd podczas próby ładowania danych do karty CIL");
            }
        }

        private void newDataPutButton_Click(object sender, RoutedEventArgs e)
        {
            NowyWpisKartyCIL nowyWpis = new NowyWpisKartyCIL();
            nowyWpis.ShowDialog();
            GC.Collect();
            RemoveChildren();
            DrawDataGrid(0);
            LoadData(0);
            GC.Collect();
            
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            if(dataTextBlock.Text != DateTime.Now.ToString("Y"))
            {
                var time = DateTime.Parse(dataTextBlock.Text); 
                time = time.AddMonths(1);
                dataTextBlock.Text = time.ToString("Y");
                RemoveChildren();
                DrawDataGrid(1);
                LoadData(1);
                GC.Collect();

            }   
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
           
            var time = DateTime.Parse(dataTextBlock.Text);
            time = time.AddMonths(-1);
            dataTextBlock.Text = time.ToString("Y");
            RemoveChildren();
            DrawDataGrid(-1);
            LoadData(-1);
            GC.Collect();


        }

        private void gumkiButt_Click(object sender, RoutedEventArgs e)
        {
            WymianaGum wg = new WymianaGum();
            wg.ShowDialog();
            wg = null;
            GC.Collect();
            LoadSodimData();
        }

        private void konsButt_Click(object sender, RoutedEventArgs e)
        {
            WpisKonserwacji wpis = new WpisKonserwacji();
            wpis.ShowDialog();
            wpis = null;
            GC.Collect();
            LoadSodimData();
        }

        private void cilButt_Click(object sender, RoutedEventArgs e)
        {
            CIL cil = new CIL();
            cil.ShowDialog();
            GC.Collect();
            LoadSodimData();
        }
    }
}
