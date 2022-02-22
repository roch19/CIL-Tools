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
using System.Xml.Linq;



namespace NavigationBar
{
    /// <summary>
    /// Logika interakcji dla klasy Statistics.xaml
    /// </summary>
    public partial class Statistics : Window
    {
        public Statistics()

        {
            InitializeComponent();
            LoadData();
          
        }



        public class Raport
        {
            public string Data { get; set; }
            public string Zmiana { get; set; }
            public int Ilosc_Plikow_STAT { get; set; }
            public int NumCig { get; set; }
            public int NumCycle { get; set; }

            public Raport(string data, string zmiana, int iloscPlikowStat, int iloscNumCig, int iloscNumCycle)
            {
                this.Data = data;
                this.Zmiana = zmiana;
                this.Ilosc_Plikow_STAT = iloscPlikowStat;
                this.NumCig = iloscNumCig;
                this.NumCycle = iloscNumCycle;

            }
        }

        public class DataToShow
        {
            public string Data { get; set; }
            public string Zmiana { get; set; }
            public int Zmierzone_Probki { get; set; }
            public int Rozpoczete_Cykle { get; set; }
            public int Zakonczone_Badania { get; set; }
            public int Przerwane_Badania { get; set; }
            public string Procent_Przerwanych { get; set; }

            public DataToShow(string data, string zmiana, int ilosc_zmierzonych_probek, int ilosc_rozpoczetych_badan, int ilosc_zakonczonych_badan, int ilosc_przerwanych_badan, string procent_przerwanych)
            {
                Data = data;
                Zmiana = zmiana;
                Zmierzone_Probki = ilosc_zmierzonych_probek;
                Rozpoczete_Cykle = ilosc_rozpoczetych_badan;
                Zakonczone_Badania = ilosc_zakonczonych_badan;
                Przerwane_Badania = ilosc_przerwanych_badan;
                Procent_Przerwanych = procent_przerwanych;
            }
        }



        string XMLSodimData = "C:\\copy_sodim\\data_" + ZmienneGlobalne.numer_sodimatu + ".xml";
        public List<Raport> raportList = new List<Raport>();
        public List<DataToShow> dataToShowList = new List<DataToShow>();

        public void LoadData()
        {
            sodimNameTB.Text += ZmienneGlobalne.numer_sodimatu.ToString();

            var xdoc = XDocument.Load(XMLSodimData);
            var raports = xdoc.Root.Descendants("raport")  // trzeba załadować w listę obiektów 
                .Select(x => new Raport(x.Element("data").Value,
                x.Element("zmiana").Value,
                int.Parse(x.Element("ilosc_plikow_STAT").Value),
                int.Parse(x.Element("ilosc_zmierzonych_probek").Value),
                int.Parse(x.Element("ilosc_zainicjowanych_badan").Value)));

            foreach (var item in raports)
            {
                Raport r = new Raport(item.Data, item.Zmiana, item.Ilosc_Plikow_STAT, item.NumCig, item.NumCycle);
                raportList.Add(r);

            }

            for (int i = 1; i < raportList.Count(); i++)
            {
                Raport r0 = raportList.ElementAt(i - 1);
                Raport r1 = raportList.ElementAt(i);
                int przerwane = (r1.NumCycle - r0.NumCycle) - (r1.Ilosc_Plikow_STAT - r0.Ilosc_Plikow_STAT);
                int zainicjonowane = r1.NumCycle - r0.NumCycle;

                double procent = przerwane * 100 / zainicjonowane;

                DataToShow dtShow = new DataToShow(r1.Data, r1.Zmiana, r1.NumCig - r0.NumCig, r1.NumCycle - r0.NumCycle, r1.Ilosc_Plikow_STAT - r0.Ilosc_Plikow_STAT, 
                    (r1.NumCycle - r0.NumCycle) - (r1.Ilosc_Plikow_STAT - r0.Ilosc_Plikow_STAT), procent.ToString() + "%");

                dataToShowList.Add(dtShow);
             }

            try
            {
                dataToShowList.Reverse();
                this.dataGrid.ItemsSource = dataToShowList;
            }
            catch 
            {

            }
            InitializeComponent();
        }
    }


   

   

}
