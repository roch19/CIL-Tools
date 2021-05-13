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
       
        public SpisAwarii(List<string> listaAwarii, bool val)
        {
            InitializeComponent();
         
            if (val) Dispatcher.Invoke(new Action(() => { titleTextBox.Text = "Spis kalibracji z ostatnich 12 dni"; ; }));
            else Dispatcher.Invoke(new Action(() => { titleTextBox.Text = "Spis awarii z ostatnich 12 godzin"; ; }));
            List<string> aa = new List<string>();
            for(int i=0; i<100; i++)
            {
                aa.Add(i.ToString());
            }
            TextBlockListaAwarii.Text = String.Join(Environment.NewLine, listaAwarii);
            //TextBlockListaAwarii.Text = listaAwarii.Count().ToString();
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
