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

namespace NavigationBar
{
    /// <summary>
    /// Logika interakcji dla klasy SpisKalibracji.xaml
    /// </summary>
    public partial class SpisKalibracji : Window
    {
        public SpisKalibracji(List<string> listaKalibracji)
        {
            InitializeComponent();
            List<string> aa = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                aa.Add(i.ToString());
            }
            TextBlockListaKalibracji.Text = String.Join(Environment.NewLine, listaKalibracji);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
