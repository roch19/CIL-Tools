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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace NavigationBar
{
    /// <summary>
    /// Logika interakcji dla klasy WymianaGum.xaml
    /// </summary>
    public partial class WymianaGum : Window
    {
        public WymianaGum()
        {
            InitializeComponent();
            getValuesOnStart();
            this.Left = 0;
            this.Top = SystemParameters.PrimaryScreenHeight / 2;
            GetInfoAboutGum();
        }
        string locationTxtWithLocationOfSavePAth = @"C:\copy_sodim\PATH_TO_SAVE_CIGNUM.txt";
        string pathCopySodim = @"C:\copy_sodim\copy_sodim.vbs";
        string strSodimFolder = "";
        string sodimat_name = "";
        string savePath = "";
        string aktualnyNumcig = "";
        string aktualnyNumcycle = "";
        DateTime editTime;
        List<string> lines = null;

        void GetInfoAboutGum()
        {
            string[] arrLine;

            try
            {
                editTime = DateTime.Now;
                changeDateLabel.Content = DateTime.Now.ToShortDateString();
                arrLine = File.ReadAllLines(locationTxtWithLocationOfSavePAth); // replacment dla temp data (data+numcig+numcycle)
                dataOstatniejWymianyGumki.Text = arrLine[5];
                formatObecniejGumki.Text = arrLine[7];
                int x = Int32.Parse(aktualnyNumcycle);
                int y = Int32.Parse(arrLine[11]);
                iloscCykli.Text = (x-y).ToString();
                x = Int32.Parse(aktualnyNumcig);
                y = Int32.Parse(arrLine[9]);
                iloscZbadanychProbek.Text = (x - y).ToString();
                DateTime dt = Convert.ToDateTime(arrLine[5]);
                iloscDniBezZmianyGumki.Text = ((DateTime.Now - dt).Days).ToString(); 

            }
            catch { }

          
        }


        void getValuesOnStart()
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

        internal bool wpisWymiany(string format, string reason)
        {
            savePath = "";
            try
            {
                lines = File.ReadAllLines(locationTxtWithLocationOfSavePAth).ToList();
                savePath += lines[1];
                lines = File.ReadAllLines(pathCopySodim).ToList();
            }
            catch { MessageBox.Show("Brak pliku zawierającego ścieżkę zapisu który powinien znajdować się w ścieżce: C:\\copy_sodim\\PATH_TO_SAVE_CIGNUM.txt, dodaj plik do ścieżki", "UWAGA!"); return false; };
            //Const Sodimat_name 
          //  Console.WriteLine(lines[1]);
            //Const strSodimFolder
          //  Console.WriteLine(lines[2]);

            string tmp = lines[1];
            bool flag = true;

            string[] arrLine = File.ReadAllLines(locationTxtWithLocationOfSavePAth); // replacment dla temp data (data+numcig+numcycle)
           
            if(checkBoxStatus.IsChecked == true)
            {
                arrLine[5] = changeDateLabel.Content.ToString();
            }
            else
            {
                arrLine[5] = DateTime.Now.ToShortDateString();
            }
             
            arrLine[7] = format;

            sodimat_name = "";
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
            savePath += sodimat_name + ".txt";
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
            abc += sodimat_name+";";
            string tmp1 = lines[0];
            int k = 0;
            arrLine[9] = "";
            while (Char.IsWhiteSpace(tmp1[k]))
            {
                k++;
            }

            while (!Char.IsWhiteSpace(tmp1[k]))
            {
                abc += tmp1[k];
                arrLine[9] += tmp1[k];
                k++;
            }



            //get numcycle from history
            string numcyclePath = strSodimFolder + "HISTORY\\NUMCYCLE.TXT";
            lines.Clear();
            lines = File.ReadAllLines(numcyclePath).ToList();

            arrLine[11] = "";
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
                arrLine[11] += tmp1[k];
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
                abc += ";" + format +";"+ reason;
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

        private void OK_LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var statment = makeShureWindow();
            //if (gumRadioButtonStackPanel.Children.OfType<RadioButton>().Any(rb => rb.IsChecked == true) == true)
            //{
            //    MessageBox.Show("Określiłeś format gumki!");
            //}
            //else if (gumRadioButtonStackPanel.Children.OfType<RadioButton>().All(rb => rb.IsChecked == false))
            //{
            //    MessageBox.Show("Nie określiłeś formatu gumki!");
            //}
            RadioButton gumCheckedButton = null;
            RadioButton reasonCheckedButton = null;

            try
            {
                gumCheckedButton = gumRadioButtonGrid.Children.OfType<RadioButton>().First(r => r.IsChecked == true);
                reasonCheckedButton = reasonRadioButtonGrid.Children.OfType<RadioButton>().First(r => r.IsChecked == true);

                if (statment == true)
                {
                    bool b = wpisWymiany(gumCheckedButton.Content.ToString(), reasonCheckedButton.Content.ToString());
                    if (b == true)
                    {
                       // MessageBox.Show("Plik wymiany gumki zapisano w danej ścierzce: " + savePath, "Informacja");
                        this.Close();
                        GC.Collect();
                    }

                    else MessageBox.Show("Błąd podczas zapisu ", "Uwaga!");
                }

            }

            catch
            {
                MessageBox.Show("Należy wybrać rodzaj gumki oraz powód wymiany!", "Bład wprowadzania danych!");
            }


           

            //if (KsRB.IsChecked == true)
            //{
            //    if (statment == true)
            //    {
            //        bool b = wpisWymiany("KS");
            //        if (b == true)
            //        {
            //            MessageBox.Show("Plik wymiany gumki zapisano w danej ścierzce: " + savePath, "Informacja");
            //            this.Close();
            //            GC.Collect();
            //        }

            //        else MessageBox.Show("Błąd podczas zapisu ", "Uwaga!");
            //    }
            //}
            //else if (DsRB.IsChecked == true)
            //{
            //    bool b = wpisWymiany("DS");
            //    if (b == true)
            //    {
            //        MessageBox.Show("Plik wymiany gumki zapisano w danej ścierzce: " + savePath, "Informacja");
            //        this.Close();
            //        GC.Collect();
            //    }

            //    else MessageBox.Show("Błąd podczas zapisu ", "Uwaga!");
            //}
            //else if (SsRB.IsChecked == true)
            //{
            //    bool b = wpisWymiany("SS");
            //    if (b == true)
            //    {
            //        MessageBox.Show("Plik wymiany gumki zapisano w danej ścierzce: " + savePath, "Informacja");
            //        this.Close();
            //        GC.Collect();
            //    }

            //    else MessageBox.Show("Błąd podczas zapisu ", "Uwaga!");
            //}

            //else MessageBox.Show("Musisz określić format gumki!");
        }

  
        bool makeShureWindow()
        {
            var Result = MessageBox.Show("Czy chcesz wymienić gumkę?", "Wymiana Gumki", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (Result == MessageBoxResult.Yes) return true;
            else return false;
        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            if(backDateOption.IsEnabled == false) Dispatcher.Invoke(new Action(() => { backDateOption.IsEnabled = true; ; }));
            else if (backDateOption.IsEnabled == true) Dispatcher.Invoke(new Action(() => { backDateOption.IsEnabled = false; ; }));
        }

        private void Anuluj_LoginButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void subtractDay_Click(object sender, RoutedEventArgs e)
        {
            editTime = editTime.AddDays(-1);
            Dispatcher.Invoke(new Action(() => { changeDateLabel.Content = editTime.ToString("dd/MM/yyyy"); ; }));
            int result = DateTime.Compare(editTime, DateTime.Now);

            if (result < 0 )
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
