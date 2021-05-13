using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Logika interakcji dla klasy BlockerWindow.xaml
    /// </summary>
    public partial class BlockerWindow : Window
    {
        public BlockerWindow()
        {
            InitializeComponent();
        }

        private void odblokujButton_Click(object sender, RoutedEventArgs e)
        {
            Login l = new Login();
            l.ShowDialog();
            if (l.loginStatus() == null)
            {
                MessageBox.Show("UPS null");
            }
            else if (l.loginStatus() == true)
            {
                this.Close();
                l = null;
                GC.Collect();
            }

        }
    }
}