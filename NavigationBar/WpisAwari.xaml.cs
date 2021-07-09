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
using System.Timers;
using System.IO;

namespace NavigationBar
{
    /// <summary>
    /// Logika interakcji dla klasy WpisAwari.xaml
    /// </summary>
    public partial class WpisAwari : Window
    {
        public WpisAwari()
        {
            InitializeComponent();
            getPaths();
            this.Left = 0;
            this.Top = SystemParameters.PrimaryScreenHeight / 2;
            initializeChceckBoxList();
        }

        List<StackPanel> checkBoxes = new List<StackPanel>();
        DateTime timeStart;
        DateTime timeStop;
        string sodimat_name = "";


    

        private void Anuluj_LoginButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        internal void clockStop(DateTime start)
        {
            timeStop = DateTime.Now;
            timeStart = start;
            
            var timeSumarum = (timeStop - timeStart).TotalSeconds;
            var summaryTime = timeSumarum.ToString();
            TimeSpan t = TimeSpan.FromSeconds(timeSumarum);
            string summary = string.Format("{0:D2}h:{1:D2}m:{2:D2}s", 
                        t.Hours, 
                        t.Minutes, 
                        t.Seconds);
            Dispatcher.Invoke(new Action(() => { startAwariaTextBlock.Text = timeStart.ToString(); ; }));
            Dispatcher.Invoke(new Action(() => { stopAwariaTextBlock.Text = timeStop.ToString(); ; }));
            Dispatcher.Invoke(new Action(() => { czasTrwaniaAwariiTextBlock.Text = summary; ; }));
        }

        

        string getPaths()
        {      
            string savePath = "";
            List<string> lines;

            lines = File.ReadAllLines(pathCopySodim).ToList();

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
            savePath += sodimat_name + ".txt";
            Dispatcher.Invoke(new Action(() => { numerUrzadzeniaTextBox.Text = sodimat_name; ; }));
            tmp = null;
            lines = null;
            pathCopySodim = null;
            GC.Collect();
            return savePath;
        }

        string locationTxtWithLocationOfSavePAth = @"C:\copy_sodim\PATH_TO_SAVE_CIGNUM.txt";
        string pathCopySodim = @"C:\copy_sodim\copy_sodim.vbs";

        string savePath = "";
        List<string> lines = null;

        internal bool wpisWymiany()
        {
            try
            {
                lines = File.ReadAllLines(locationTxtWithLocationOfSavePAth).ToList();
                savePath += lines[3];
                lines = null;
            }

            catch
            {
                MessageBox.Show("Brak pliku zawierającego ścieżkę zapisu który powinien znajdować się w ścieżce: C:\\copy_sodim\\PATH_TO_SAVE_CIGNUM.txt, dodaj plik do ścieżki", "UWAGA!");
                return false;
            };



            savePath += sodimat_name + "_WpisAwarii.txt";

            string abc = "";
            abc += timeStart.ToString();
            abc += ";" + timeStop.ToString();
            abc += ";" + sodimat_name;
           
            if (ModułComboBox.Text.ToString() == "WG")
            {
                abc += ";WG;";
                var list = this.definicjaProblemuSP.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);
           
                foreach (var item in list)
                {
                    if (!item.Equals(list.Last())) abc += item.Content.ToString() + ", ";
                    else abc += item.Content.ToString();
                }

                abc += ";";

                list = this.rcWGCheckBox.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);
                foreach (var item in list)
                {
                    if (!item.Equals(list.Last())) abc += item.Content.ToString() + ", ";
                    else abc += item.Content.ToString();
                }

                abc += ";";

                list = this.caWGCheckBox.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);
                foreach (var item in list)
                {
                    if (!item.Equals(list.Last())) abc += item.Content.ToString() + ", ";
                    else abc += item.Content.ToString();
                }
            }

            else if (ModułComboBox.Text.ToString() == "Hopper")
            {
                abc += ";Hopper;";
                var list = this.definicjaProblemuSP.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);
                foreach (var item in list)
                {
                    if (!item.Equals(list.Last())) abc += item.Content.ToString() + ", ";
                    else abc += item.Content.ToString();
                }

                abc += ";";

                list = this.rcHopperCheckBox.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);
                foreach (var item in list)
                {
                    if (!item.Equals(list.Last())) abc += item.Content.ToString() + ", ";
                    else abc += item.Content.ToString();
                }

                abc += ";";

                list = this.caHopperCheckBox.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);
                foreach (var item in list)
                {
                    if (!item.Equals(list.Last())) abc += item.Content.ToString() + ", ";
                    else abc += item.Content.ToString();
                }
            }

            else if (ModułComboBox.Text.ToString() == "PD")
            {
                abc += ";PD;";
                var list = this.definicjaProblemuSP.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);
                foreach (var item in list)
                {
                    if (!item.Equals(list.Last())) abc += item.Content.ToString() + ", ";
                    else abc += item.Content.ToString();
                }

                abc += ";";

                list = this.rcPDCheckBox.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);
                foreach (var item in list)
                {
                    if (!item.Equals(list.Last())) abc += item.Content.ToString() + ", ";
                    else abc += item.Content.ToString();
                }

                abc += ";";

                list = this.caPDCheckBox.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);
                foreach (var item in list)
                {
                    if (!item.Equals(list.Last())) abc += item.Content.ToString() + ", ";
                    else abc += item.Content.ToString();
                }
            }

            else if (ModułComboBox.Text.ToString() == "CIR")
            {
                abc += ";CIR;";
                var list = this.definicjaProblemuSP.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);
                foreach (var item in list)
                {
                    if (!item.Equals(list.Last())) abc += item.Content.ToString() + ", ";
                    else abc += item.Content.ToString();
                }

                abc += ";";

                list = this.rcCIRCheckBox.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);
                foreach (var item in list)
                {
                    if (!item.Equals(list.Last())) abc += item.Content.ToString() + ", ";
                    else abc += item.Content.ToString();
                }

                abc += ";";

                list = this.caCIRCheckBox.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);
                foreach (var item in list)
                {
                    if (!item.Equals(list.Last())) abc += item.Content.ToString() + ", ";
                    else abc += item.Content.ToString();
                }
            }

            else if (ModułComboBox.Text.ToString() == "DL")
            {
                abc += ";DL;";
                var list = this.definicjaProblemuSP.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);
                foreach (var item in list)
                {
                    if (!item.Equals(list.Last())) abc += item.Content.ToString() + ", ";
                    else abc += item.Content.ToString();
                }

                abc += ";";

                list = this.rcDLCheckBox.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);
                foreach (var item in list)
                {
                    if (!item.Equals(list.Last())) abc += item.Content.ToString() + ", ";
                    else abc += item.Content.ToString();
                }

                abc += ";";

                list = this.caDLCheckBox.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);
                foreach (var item in list)
                {
                    if (!item.Equals(list.Last())) abc += item.Content.ToString() + ", ";
                    else abc += item.Content.ToString();
                }
            }

            else if (ModułComboBox.Text.ToString() == "Cutter")
            {
                abc += ";Cutter;";
                var list = this.definicjaProblemuSP.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);
                foreach (var item in list)
                {
                    if (!item.Equals(list.Last())) abc += item.Content.ToString() + ", ";
                    else abc += item.Content.ToString();
                }

                abc += ";";

                list = this.rcCutterCheckBox.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);
                foreach (var item in list)
                {
                    if (!item.Equals(list.Last())) abc += item.Content.ToString() + ", ";
                    else abc += item.Content.ToString();
                }

                abc += ";";

                list = this.caCutterCheckBox.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);
                foreach (var item in list)
                {
                    if (!item.Equals(list.Last())) abc += item.Content.ToString() + ", ";
                    else abc += item.Content.ToString();
                }
            }

            else if (ModułComboBox.Text.ToString() == "System")
            {
                abc += ";System;";
                var list = this.definicjaProblemuSP.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);
                foreach (var item in list)
                {
                    abc += item.Content.ToString() + ", ";
                }

                abc += ";";

                list = this.rcSystemCheckBox.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);
                foreach (var item in list)
                {
                    if (!item.Equals(list.Last())) abc += item.Content.ToString() + ", ";
                    else abc += item.Content.ToString();
                }

                abc += ";";

                list = this.caSystemCheckBox.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);
                foreach (var item in list)
                {
                    if (!item.Equals(list.Last())) abc += item.Content.ToString() + ", ";
                    else abc += item.Content.ToString();
                }
            }

            else if (ModułComboBox.Text.ToString() == "HT")
            {
                abc += ";PD;";
                var list = this.definicjaProblemuSP.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);
                foreach (var item in list)
                {
                    if (!item.Equals(list.Last())) abc += item.Content.ToString() + ", ";
                    else abc += item.Content.ToString();
                }

                abc += ";";

                list = this.rcHTCheckBox.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);
                foreach (var item in list)
                {
                    if (!item.Equals(list.Last())) abc += item.Content.ToString() + ", ";
                    else abc += item.Content.ToString();
                }

                abc += ";";

                list = this.caHTCheckBox.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);
                foreach (var item in list)
                {
                    if (!item.Equals(list.Last())) abc += item.Content.ToString() + ", ";
                    else abc += item.Content.ToString();
                }
            }

            else if (ModułComboBox.Text.ToString() == "HAR")
            {
                abc += ";PD;";
                var list = this.definicjaProblemuSP.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);
                foreach (var item in list)
                {
                    if (!item.Equals(list.Last())) abc += item.Content.ToString() + ", ";
                    else abc += item.Content.ToString();
                }

                abc += ";";

                list = this.rcHARCheckBox.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);
                foreach (var item in list)
                {
                    if (!item.Equals(list.Last())) abc += item.Content.ToString() + ", ";
                    else abc += item.Content.ToString();
                }

                abc += ";";

                list = this.caHARCheckBox.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);
                foreach (var item in list)
                {
                    if (!item.Equals(list.Last())) abc += item.Content.ToString() + ", ";
                    else abc += item.Content.ToString();
                }
            }


            else MessageBox.Show("Musisz wybrać typ awarii!");

            try
            {
                StreamWriter writer = new StreamWriter(savePath, true);
                writer.WriteLine(abc);
                writer.Dispose();
                return true;
            }
            catch
            {
                MessageBox.Show("Wykryto błąd. Upewnij się że wskazana ścierzka istnieje", "BŁĄD");
                return false;
            }
        }



        private void ModułComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combo = (ComboBox)sender;
            var value = (ComboBoxItem)combo.SelectedValue;

            changeValues((string)value.Content);
        }

        void changeValues(string mod)
        {
            if (mod == "WG")
            {
                clean();
                rcWGCheckBox.Visibility = Visibility.Visible;
                caWGCheckBox.Visibility = Visibility.Visible;
            }
            else if (mod == "Hopper")
            {
                clean();
                rcHopperCheckBox.Visibility = Visibility.Visible;
                caHopperCheckBox.Visibility = Visibility.Visible;
            }
            else if (mod == "PD")
            {
                clean();
                rcPDCheckBox.Visibility = Visibility.Visible;
                caPDCheckBox.Visibility = Visibility.Visible;
            }
            else if (mod == "CIR")
            {
                clean();
                rcCIRCheckBox.Visibility = Visibility.Visible;
                caCIRCheckBox.Visibility = Visibility.Visible;
            }
            else if (mod == "DL")
            {
                clean();
                rcDLCheckBox.Visibility = Visibility.Visible;
                caDLCheckBox.Visibility = Visibility.Visible;
            }
            else if (mod == "Cutter")
            {
                clean();
                rcCutterCheckBox.Visibility = Visibility.Visible;
                caCutterCheckBox.Visibility = Visibility.Visible;
            }
            else if (mod == "System")
            {
                clean();
                rcSystemCheckBox.Visibility = Visibility.Visible;
                caSystemCheckBox.Visibility = Visibility.Visible;
            }
            else if (mod == "HT")
            {
                clean();
                rcHTCheckBox.Visibility = Visibility.Visible;
                caHTCheckBox.Visibility = Visibility.Visible;
            }
            else if (mod == "HAR")
            {
                clean();
                rcHARCheckBox.Visibility = Visibility.Visible;
                caHARCheckBox.Visibility = Visibility.Visible;
            }
        }

        void initializeChceckBoxList()
        {
            checkBoxes.Add(rcHopperCheckBox);
            checkBoxes.Add(rcWGCheckBox);
            checkBoxes.Add(rcPDCheckBox);
            checkBoxes.Add(rcCIRCheckBox);
            checkBoxes.Add(rcDLCheckBox);
            checkBoxes.Add(rcCutterCheckBox);
            checkBoxes.Add(rcSystemCheckBox);
            checkBoxes.Add(rcHTCheckBox);
            checkBoxes.Add(rcHARCheckBox);
            checkBoxes.Add(caHopperCheckBox);
            checkBoxes.Add(caWGCheckBox);
            checkBoxes.Add(caPDCheckBox);
            checkBoxes.Add(caCIRCheckBox);;
            checkBoxes.Add(caDLCheckBox);
            checkBoxes.Add(caCutterCheckBox);
            checkBoxes.Add(caSystemCheckBox);
            checkBoxes.Add(caHTCheckBox);
            checkBoxes.Add(caHARCheckBox);
        }
       

        void clean()
        {
            foreach (var item in checkBoxes)
            {
                item.Visibility = Visibility.Hidden;
            }
        }

        private void EdytujCzasButton_Click(object sender, RoutedEventArgs e)
        {
            List<DateTime> dtList = new List<DateTime>();
            EdycjaCzasu ec = new EdycjaCzasu();
            ec.IntData(timeStart, timeStop);
             ec.ShowDialog();
            dtList = ec.throwDataToWpisAwarii();
            try {
            timeStart = dtList[0];
            timeStop = dtList[1];

                var timeSumarum = (timeStop - timeStart).TotalSeconds;
                var summaryTime = timeSumarum.ToString();
                TimeSpan t = TimeSpan.FromSeconds(timeSumarum);
                string summary = string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
                            t.Hours,
                            t.Minutes,
                            t.Seconds);
                Dispatcher.Invoke(new Action(() => { startAwariaTextBlock.Text = timeStart.ToString(); ; }));
                Dispatcher.Invoke(new Action(() => { stopAwariaTextBlock.Text = timeStop.ToString(); ; }));
                Dispatcher.Invoke(new Action(() => { czasTrwaniaAwariiTextBlock.Text = summary; ; }));

            }
            catch (Exception eqweq) {; }
        }

        private void OK_LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if(ModułComboBox.SelectedIndex <= -1 ) MessageBox.Show("Musisz wybrać moduł! " + savePath, "Uwaga!"); 
            else if(ModułComboBox.SelectedIndex > -1)
            {
                bool b = wpisWymiany();
                if (b == false) MessageBox.Show("Błąd podczas zapisywania awarii!", "UWAGA!");
                this.Close();
            }
        }
    }
}
