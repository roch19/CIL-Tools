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

namespace NavigationBar
{
    /// <summary>
    /// Logika interakcji dla klasy SpisAwarii.xaml
    /// </summary>
    public partial class SpisAwarii : Window
    {

        public class Awaria
        {
            public Awaria(string data, string godzina, string definicja)
            {
                Data = data;
                Godzina = godzina;
                Definicja = definicja;
            }

            public string Data { get; set; }
            public string Godzina { get; set; }
            public string Definicja { get; set; }

            
        }
        public SpisAwarii(List<string> listaAwarii, bool val)
        {
           
            InitializeComponent();
            List<Awaria> awariaList = new List<Awaria>();
            if (val) Dispatcher.Invoke(new Action(() => { titleTextBox.Text = "Spis kalibracji z ostatnich 12 dni"; ; }));
            else Dispatcher.Invoke(new Action(() => { titleTextBox.Text = "Spis awarii z ostatnich 12 godzin"; ; }));
            //List<string> aa = new List<string>();
            //for(int i=0; i<100; i++)
            //{
            //    aa.Add(i.ToString());
            //}
            string tmpData;
            string tmpGodzina;
            string tmpTresc;

            foreach (var item in listaAwarii)
            {
                tmpData = item.Substring(0, 10);
                tmpGodzina = item.Substring(11,8);
                tmpTresc = item.Substring(20);
                Awaria a = new Awaria(tmpData, tmpGodzina, tmpTresc);
                awariaList.Add(a);
            }


            dataGrid.ItemsSource = awariaList;
            //TextBlockListaAwarii.Text = String.Join(Environment.NewLine, listaAwarii);
       
            this.Left = 0;
            this.Top = 0;
            

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            GC.Collect();
        }
    }
}
