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
        List<string> lines = null;

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

        void getValues()
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

