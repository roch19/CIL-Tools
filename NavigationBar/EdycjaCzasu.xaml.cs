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
using System.Diagnostics;
using System.Threading;

namespace NavigationBar
{
    /// <summary>
    /// Logika interakcji dla klasy EdycjaCzasu.xaml
    /// </summary>
    public partial class EdycjaCzasu : Window
    {
        private DateTime stopDT;
        private DateTime startDT;
        public List<DateTime> dtList = new List<DateTime>();
        DateTime pomocnicza;
        public EdycjaCzasu()
        {
            InitializeComponent();
      
        }

        internal void IntData(DateTime start, DateTime stop)
        {
            stopDT = stop;
            startDT = start;
            DateTime temp = start.Date;
            int temp2;
            temp2 = start.Hour;
            Dispatcher.Invoke(new Action(() => { Start_Data.Text = temp.ToString("dd/MM/yyyy"); ; }));
            Dispatcher.Invoke(new Action(() => { Start_Hours.Text = temp2.ToString() + " h"; ; }));
            temp2 = start.Minute;
            Dispatcher.Invoke(new Action(() => { Start_Minutes.Text = temp2.ToString() + " min"; ; }));

            temp = stop.Date;
            temp2 = stop.Hour;
            Dispatcher.Invoke(new Action(() => { Stop_Data.Text = temp.ToString("dd/MM/yyyy"); ; }));
            Dispatcher.Invoke(new Action(() => { Stop_Hours.Text = temp2.ToString() + " h"; ; }));
            temp2 = stop.Minute;
            Dispatcher.Invoke(new Action(() => { Stop_Minutes.Text = temp2.ToString() + " min"; ; }));

        }




        // Kontrolery daty startowej 
        private void UP_Start_Date_Click(object sender, RoutedEventArgs e)
        {
            startDT = startDT.AddDays(1);
            Dispatcher.Invoke(new Action(() => { Start_Data.Text = startDT.ToString("dd/MM/yyyy"); ; }));
        }

        private void UP_Start_Hours_Click(object sender, RoutedEventArgs e)
        {
            startDT = startDT.AddHours(1);
            Dispatcher.Invoke(new Action(() => { Start_Hours.Text = startDT.Hour.ToString()+" h"; ; }));
        }

        private void UP_Start_Minutes_Click(object sender, RoutedEventArgs e)
        {
            startDT = startDT.AddMinutes(1);
            Dispatcher.Invoke(new Action(() => { Start_Minutes.Text = startDT.Minute.ToString()+" min"; ; }));
        }

        private void Down_Start_Date_Click(object sender, RoutedEventArgs e)
        {
            startDT = startDT.AddDays(-1);
            Dispatcher.Invoke(new Action(() => { Start_Data.Text = startDT.ToString("dd/MM/yyyy"); ; }));
        }

        private void Down_Start_Hours_Click(object sender, RoutedEventArgs e)
        {
            startDT = startDT.AddHours(-1);
            Dispatcher.Invoke(new Action(() => { Start_Hours.Text = startDT.Hour.ToString() + " h"; ; ; }));
        }

        private void Down_Start_Minutes_Click(object sender, RoutedEventArgs e)
        {
            startDT = startDT.AddMinutes(-1);
            Dispatcher.Invoke(new Action(() => { Start_Minutes.Text = startDT.Minute.ToString() + " min"; ; }));
        }







        ////Kontrolery daty końcowej
        private void UP_Stop_Date_Click(object sender, RoutedEventArgs e)
        {
            stopDT = stopDT.AddDays(1);
            Dispatcher.Invoke(new Action(() => { Stop_Data.Text = stopDT.ToString("dd/MM/yyyy"); ; }));
        }

        private void UP_Stop_Hours_Click(object sender, RoutedEventArgs e)
        {
            stopDT = stopDT.AddHours(1);
            Dispatcher.Invoke(new Action(() => { Stop_Hours.Text = stopDT.Hour.ToString() + " h"; ; }));
        }

        private void UP_Stop_Minutes_Click(object sender, RoutedEventArgs e)
        {
            stopDT = stopDT.AddMinutes(1);
            Dispatcher.Invoke(new Action(() => { Stop_Minutes.Text = stopDT.Minute.ToString() + " min"; ; }));
        }

        private void Down_Stop_Date_Click(object sender, RoutedEventArgs e)
        {
            stopDT = stopDT.AddDays(-1);
            Dispatcher.Invoke(new Action(() => { Stop_Data.Text = stopDT.ToString("dd/MM/yyyy"); ; }));
        }

        private void Down_Stop_Hours_Click(object sender, RoutedEventArgs e)
        {
            stopDT = stopDT.AddHours(-1);
            Dispatcher.Invoke(new Action(() => { Stop_Hours.Text = stopDT.Hour.ToString() + " h"; ; }));
        }

        private void Down_Stop_Minutes_Click(object sender, RoutedEventArgs e)
        {
            stopDT = stopDT.AddMinutes(-1);
            Dispatcher.Invoke(new Action(() => { Stop_Minutes.Text = stopDT.Minute.ToString() + " min"; ; }));
        }






        private void Anuluj_LoginButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OK_LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (startDT > stopDT)
            {
                MessageBox.Show("Błąd daty!", "BŁĄD!");
            }
            else if (stopDT > startDT)
            {
                dtList.Add(startDT);
                dtList.Add(stopDT);
                this.Close();
            }
        }

        internal List<DateTime> throwDataToWpisAwarii()
        {
           
            return dtList;
        }

        internal List<DateTime> OK_LoginButton_Click()
        {
            throw new NotImplementedException();
        }

    }
}
