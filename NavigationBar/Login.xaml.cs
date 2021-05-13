using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Logika interakcji dla klasy Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            this.Left = 0;

            this.Top = SystemParameters.PrimaryScreenHeight/2 ;
        }

        string password = "1234";
        bool val = false;
        //MainWindow m = new MainWindow();
        Process keyboard; 
 
        private void KlawiaturaButton_Click(object sender, RoutedEventArgs e)
        {
            keyboard = System.Diagnostics.Process.Start("osk.exe");
            Keyboard.Focus(Haslo);

        }

        private void OK_LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (password == Haslo.Password)
            {
                //if(keyboard!= null) keyboard.Kill();
                this.Close();
                // m.ChangeStatus(true);
                val = true;
                 loginStatus();
    
            }
            else
            {
                MessageBox.Show("Błędne hasło!");
                Haslo.Password = "";
            }

        }
 
        internal bool loginStatus()
        {
            return val;
        }

        private void Anuluj_LoginButton_Click(object sender, RoutedEventArgs e)
        {
          //  if (keyboard != null) keyboard.Kill();
                   this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Haslo.Password += (sender as Button).Content;
        }
    

        private void btDEL_Click(object sender, RoutedEventArgs e)
        {
            if(Haslo.Password.Length > 0) Haslo.Password = Haslo.Password.Remove(Haslo.Password.Length - 1);
        }
    }
}
