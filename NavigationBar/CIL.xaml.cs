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
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace NavigationBar
{
    /// <summary>
    /// Logika interakcji dla klasy CIL.xaml
    /// </summary>
    public partial class CIL : Window
    {
        public CIL()
        {
            InitializeComponent();
            getValues();
            getInfoOnTable();
        }

        string locationTxtWithLocationOfSavePAth = @"C:\copy_sodim\PATH_TO_SAVE_CIGNUM.txt";
        string pathCopySodim = @"C:\copy_sodim\copy_sodim.vbs";
        string strSodimFolder = "";
        string sodimat_name = "";
        string savePath = "";
        string aktualnyNumcig = "";
        string aktualnyNumcycle = "";
        DateTime editTime;
        string iloscWykonanychBadanExportArch = "";
        //data_201_70.xml
        string sodDataPath = @"C:\copy_sodim\data_" + ZmienneGlobalne.numer_sodimatu + ".xml";
        List<string> lines = null;

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
        void getInfoOnTable()
        {
                string[] arrLine;

                try
                {
                editTime = DateTime.Now;
                arrLine = File.ReadAllLines(locationTxtWithLocationOfSavePAth); // replacment dla temp data (data+numcig+numcycle)
                    dataOstatniegoCila.Text = arrLine[13];
                    DateTime dt = Convert.ToDateTime(arrLine[13]);
                    iloscDniOdCila.Text = ((DateTime.Now - dt).Days).ToString();

                    int iloscZmiezonychProbek = Convert.ToInt32(arrLine[15]);
                    int temp = Int32.Parse(aktualnyNumcig);
                    iloscZmierzonychProbekOdOstatniegoCILA.Text = (temp - iloscZmiezonychProbek).ToString();

                   changeDateLabel.Content = DateTime.Now.ToShortDateString(); 

                    //zainicjowane badania od sotatniego cila
                int iloscZainicjowanychBadan = Convert.ToInt32(arrLine[17]);
                    temp = Int32.Parse(aktualnyNumcycle);
                    iloscCykli.Text = (temp - iloscZainicjowanychBadan).ToString();

                    ////wykonane badania od ostatniego cila
                    //int iloscWykonanychBadanOdCILA = Convert.ToInt32(arrLine[21]);
                    //temp = Int32.Parse(iloscWykonanychBadanExportArch);
                    //iloscWykonanychBadan.Text = (temp - iloscWykonanychBadanOdCILA).ToString();

                    ////[ilosc zainicjowanych badań - wykonanych] 
                    //var x = Int32.Parse(iloscCykli.Text);
                    //var y = Int32.Parse(iloscWykonanychBadan.Text);
                    //iloscPrzerwanychBadan.Text = (x-y).ToString();
                }
                catch { }
        }

        public void getValues()
        {
            try
            {
                lines = File.ReadAllLines(locationTxtWithLocationOfSavePAth).ToList();
                lines = File.ReadAllLines(pathCopySodim).ToList();
            }
            catch { MessageBox.Show("Brak pliku zawierającego ścieżkę zapisu który powinien znajdować się w ścieżce: C:\\copy_sodim\\PATH_TO_SAVE_CIGNUM.txt, dodaj plik do ścieżki", "UWAGA!"); };


            string tmp = lines[1];
            bool flag = true;



            //extract sodimat name
            for (int i = 0; i < tmp.Length; i++)
            {
                if (tmp[i].ToString().Equals("\"") && flag)
                {
                    flag = false;
                }
                else if (!tmp[i].ToString().Equals("\"") && flag.Equals(false))
                {
                    sodimat_name += tmp[i];
                }

            }
            Console.WriteLine("Nazwa sodimatu: " + sodimat_name);

            tmp = lines[2];
            flag = true;
            //extract start sodim folder
            for (int i = 0; i < tmp.Length; i++)
            {
                if (tmp[i].ToString().Equals("\"") && flag)
                {
                    flag = false;
                }
                else if (!tmp[i].ToString().Equals("\"") && flag.Equals(false))
                {
                    strSodimFolder += tmp[i];
                }

            }


            //get numcig from history
            string numcigPath = strSodimFolder + "HISTORY\\NUMCIG.TXT";

            lines.Clear();
            lines = File.ReadAllLines(numcigPath).ToList();

            string abc = "";

            string tmp1 = lines[0];
            int k = 0;

            while (Char.IsWhiteSpace(tmp1[k]))
            {
                k++;
            }

            while (!Char.IsWhiteSpace(tmp1[k]))
            {
                abc += tmp1[k];
                k++;
            }
            aktualnyNumcig = abc;

            ZmienneGlobalne.numCig = int.Parse(aktualnyNumcig);
         
            

            //            int requestCount = filePath.EnumerateFiles()
            //.Count(file => file.LastWriteTime < maxDate && file.LastWriteTime >= minDate);

            //get numcycle from history
            string numcyclePath = strSodimFolder + "HISTORY\\NUMCYCLE.TXT";
            lines.Clear();
            lines = File.ReadAllLines(numcyclePath).ToList();

            tmp1 = lines[0];
            k = 0;

            abc = "";

            while (Char.IsWhiteSpace(tmp1[k]))
            {
                k++;
            }

            while (!Char.IsWhiteSpace(tmp1[k]))
            {
                abc += tmp1[k];
                k++;
            }
            aktualnyNumcycle = abc;


            ZmienneGlobalne.numCycle = int.Parse(aktualnyNumcycle);

        }


        internal bool wpisCila()
        {
            savePath = "";
            try
            {
                lines = File.ReadAllLines(locationTxtWithLocationOfSavePAth).ToList();
                savePath += lines[21];
                lines = File.ReadAllLines(pathCopySodim).ToList();
            }
            catch { MessageBox.Show("Brak pliku zawierającego ścieżkę zapisu który powinien znajdować się w ścieżce: C:\\copy_sodim\\PATH_TO_SAVE_CIGNUM.txt, dodaj plik do ścieżki", "UWAGA!"); return false; };
            //Const Sodimat_name 
            //  Console.WriteLine(lines[1]);
            //Const strSodimFolder
            //  Console.WriteLine(lines[2]);
            sodimat_name = "";
            string tmp = lines[1];
            bool flag = true;

            string[] arrLine = File.ReadAllLines(locationTxtWithLocationOfSavePAth); // replacment dla temp data (data+numcig+numcycle)

            if (checkBoxStatus.IsChecked == true)
            {
                arrLine[13] = changeDateLabel.Content.ToString();
            }
            else
            {
                arrLine[13] = DateTime.Now.ToShortDateString();
            }

            //extract sodimat name
            for (int i = 0; i < tmp.Length; i++)
            {
                if (tmp[i].ToString().Equals("\"") && flag)
                {
                    flag = false;
                }
                else if (!tmp[i].ToString().Equals("\"") && flag.Equals(false))
                {
                    sodimat_name += tmp[i];
                }

            }
            Console.WriteLine("Nazwa sodimatu: " + sodimat_name);
            savePath += sodimat_name +"_CIL"+ ".txt";
            tmp = lines[2];
            flag = true;
            //extract start sodim folder
            strSodimFolder = "";
            for (int i = 0; i < tmp.Length; i++)
            {
                if (tmp[i].ToString().Equals("\"") && flag)
                {
                    flag = false;
                }
                else if (!tmp[i].ToString().Equals("\"") && flag.Equals(false))
                {
                    strSodimFolder += tmp[i];
                }

            }
            //Console.WriteLine("Ścierzka startowa Sodimatu: "+strSodimFolder);

            //get numcig from history
            string numcigPath = strSodimFolder + "HISTORY\\NUMCIG.TXT";

            lines.Clear();
            lines = File.ReadAllLines(numcigPath).ToList();
            // Console.WriteLine("Numcigaret:");
            // Console.WriteLine(lines[0]);
            // Console.WriteLine(lines[1]);
            string abc = "";
            abc += sodimat_name + ";";
            string tmp1 = lines[0];
            int k = 0;
            arrLine[15] = "";
            while (Char.IsWhiteSpace(tmp1[k]))
            {
                k++;
            }

            while (!Char.IsWhiteSpace(tmp1[k]))
            {
                abc += tmp1[k];
                arrLine[15] += tmp1[k];
                k++;
            }



            //get numcycle from history
            string numcyclePath = strSodimFolder + "HISTORY\\NUMCYCLE.TXT";
            lines.Clear();
            lines = File.ReadAllLines(numcyclePath).ToList();

            arrLine[17] = "";
            tmp1 = lines[0];
            k = 0;
            abc += ";";

            while (Char.IsWhiteSpace(tmp1[k]))
            {
                k++;
            }

            while (!Char.IsWhiteSpace(tmp1[k]))
            {
                abc += tmp1[k];
                arrLine[17] += tmp1[k];
                k++;
            }

            //Console.WriteLine("ilość cykli: "+abc);

            abc += ";";

            if (checkBoxStatus.IsChecked == true)
            {
                abc += changeDateLabel.Content.ToString();
            }
            else
            {
                abc += DateTime.Now.ToString();
            }


            //MessageBox.Show(path);

            string path = "C:\\copy_sodim\\data_" + ZmienneGlobalne.numer_sodimatu + ".xml";

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


            //string path = "C:\\copy_sodim\\data_"+ZmienneGlobalne.numer_sodimatu+".xml";
            //XDocument xd = new XDocument();

            //xd.Add(new XElement("ABC"));
            //xd.Save("C:\\copy_sodim\\blabla.xml");

            //if (!File.Exists(path))
            //{
            //    XDocument xd = new XDocument();
            //    xd = new XDocument();
            //    xd.Add(new XElement("ListaWpisow"));
            //    //xd.Save(templatePath + nameOfXMLFile);
            //    xd.Save(path); //
            //}



            //// xdoc = XDocument.Load(templatePath + nameOfXMLFile);




            //zapisywanie danych tymczasowych (data_wymiany_gumki + numcig + numcycle)

            try
            {
                StreamWriter writer = new StreamWriter(savePath, true);
                writer.WriteLine(abc);
                writer.Dispose();
                File.WriteAllLines(locationTxtWithLocationOfSavePAth, arrLine);
                return true;
            }

            catch
            {
                MessageBox.Show("Wykryto błąd. Upewnij się że wskazana ścierzka istnieje", "BŁĄD");
                return false;
            }


           
            //var xdoc = XDocument.Load(XMLSodimData);
            //var soidm = xdoc.Root.Descendants("Sodimat")  // trzeba załadować w listę obiektów 

        }


        bool makeShureWindow()
        {
            var Result = MessageBox.Show("Czy chcesz wykonać wpis CIL?", "Wpis CIL", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (Result == MessageBoxResult.Yes) return true;
            else return false;
        }


        private void WpisCIL_Click(object sender, RoutedEventArgs e)
        {
            var statment = makeShureWindow();
            if (statment == true)
            {
                bool b = wpisCila();
                if (b == true) 
                {
                  //  MessageBox.Show("Plik wykonania CILA zapisano w danej ścierzce: " + savePath, "Informacja");
                    GC.Collect();
                    this.Close();
                }
                else MessageBox.Show("Błąd podczas zapisu ", "Uwaga!");
            }
        }

        private void Anuluj_Click(object sender, RoutedEventArgs e)
        {
            GC.Collect();
            this.Close();
        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            if (backDateOption.IsEnabled == false) Dispatcher.Invoke(new Action(() => { backDateOption.IsEnabled = true; ; }));
            else if (backDateOption.IsEnabled == true) Dispatcher.Invoke(new Action(() => { backDateOption.IsEnabled = false; ; }));
        }

        private void subtractDay_Click(object sender, RoutedEventArgs e)
        {
            editTime = editTime.AddDays(-1);
            Dispatcher.Invoke(new Action(() => { changeDateLabel.Content = editTime.ToString("dd/MM/yyyy"); ; }));
            int result = DateTime.Compare(editTime, DateTime.Now);

            if (result < 0)
            {
                Dispatcher.Invoke(new Action(() => { addDay.IsEnabled = true; ; }));
            }

        }

        private void addDay_Click(object sender, RoutedEventArgs e)
        {
            editTime = editTime.AddDays(1);
            Dispatcher.Invoke(new Action(() => { changeDateLabel.Content = editTime.ToString("dd/MM/yyyy"); ; }));

            int result = DateTime.Compare(editTime.AddDays(1), DateTime.Now);

            if (result > 0)
            {
                Dispatcher.Invoke(new Action(() => { addDay.IsEnabled = false; ; }));
            }

        }
    }
}

