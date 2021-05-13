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
using System.Windows.Threading;

using System.IO;
using System.Security.Permissions;
using System.Timers;
using System.Diagnostics;
using System.Threading;
using WindowsInput;

namespace NavigationBar
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string theDIADay = "Monday"; //dzień w którym kalibrowana jest średnica
        AwarieKalibracje kalibracje = new AwarieKalibracje();
        public bool ifCllicked = false;
        public bool zalogowany = false;
        public string remoteDesktopPath = @"\\10.177.11.130\Sodimats\Remote Desktop List\";
        public string logoutRemoteDekstopPath = @"C:\ProgramData\Microsoft\Windows\Strat Menu\Programs\Accessories\RemoteDesktop\";
        public bool ifWXP = false;
        bool awariaButtonStatus = false;
        private string watchedFolder = "";
        DateTime startAwaria;
        List<string> calibrationsList = new List<string>();

        public object DataTime { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            this.Left = SystemParameters.PrimaryScreenWidth - this.Width;
            ChceckIfAnyCalibrationWasTodayMaken();
            MainWindow2_Loaded();
            timeToReset();
            App_Deactivated_LostFocus();
            this.Focus();
            ShittyFunctionToChceckIfAppIsOnTopOnWinows7();
        }

        public void ShittyFunctionToChceckIfAppIsOnTopOnWinows7()
        {
            string operatingSystem = System.Environment.OSVersion.ToString();
            if (operatingSystem.Contains("6.1"))
            {
                MessageBox.Show("To windows 7!");

                Thread myThread = new Thread(() =>
                {
                    fun();
                });

                myThread.IsBackground = true; // <-- Set your thread to background
                myThread.Start();
            }
            else if (operatingSystem.Contains("5.1")) ifWXP = true;
          }
        
        public void fun()
        {
            while (true)
            {
                Thread.Sleep(3000);
                Dispatcher.Invoke(new Action(() => { MainWindow2.Topmost = false; ; }));
                Dispatcher.Invoke(new Action(() => { MainWindow2.Topmost = true; ; }));
               
            }
        }

        public delegate void WindowLostFocusEventHandler(object source, EventArgs args);

        public event WindowLostFocusEventHandler LostFocus;

        void App_Deactivated_LostFocus()
        {

            Dispatcher.Invoke(new Action(() => { this.Focus(); ; }));

            OnFousLosted();
        }

        protected virtual void OnFousLosted()
        {
            LostFocus?.Invoke(this, EventArgs.Empty);
        }

        //bool isapplicationactive;
        //private void App_Deactivated(object sender, EventHandler e)
        //{
        //    // application deactivated
        //    this.isapplicationactive = false;
        //    Dispatcher.Invoke(new Action(() => { this.Focus(); ; }));
        //}


        private void Widow_LostFocus(object sender, EventArgs e)
        {
            MainWindow window = (MainWindow)sender;
            window.Topmost = true;
            window.Focus();
        }

        private void abc()
        {
            Console.WriteLine("ABC");
        }
       
        //Po uruchomieniu programu sprawdza czy była robiona kalibraca w danym dniu

        void ChceckIfAnyCalibrationWasTodayMaken()
        {
            try
            {
                calibrationsList = kalibracje.ChceckIfWasAnyCalibrationToday();
            }
            catch(Exception e)
            {
                MessageBox.Show("Brak folderu coppy_sodim lub oprogramowania SODIM! Sprawdź czy folder copy_sodim wraz z zawartośćią znajduje sie we wskazanej ścieżce: C:\\copy_sodim","BŁĄD KRYTYCZNY!");
                Environment.Exit(Environment.ExitCode);
                Application.Current.Shutdown();

            }
            //if (kalibracje.ChceckIfWasAnyCalibrationToday())
            if(!calibrationsList.Any()&& DateTime.Now.DayOfWeek.ToString() == theDIADay)
            {
                Dispatcher.Invoke(new Action(() => { DIATextBlock.Visibility = Visibility.Visible; ; }));
                Dispatcher.Invoke(new Action(() => { DIATextBlock.Background = Brushes.Red; ; }));
            }
            else if (calibrationsList.Any()&& DateTime.Now.DayOfWeek.ToString() == theDIADay) // Sunday
            {
                Dispatcher.Invoke(new Action(() => { DIATextBlock.Visibility = Visibility.Visible; ; }));

                foreach (var item in calibrationsList)
                {
                    if (item.Contains("PD"))
                    {
                        Dispatcher.Invoke(new Action(() => { VisibilityButton.Background = Brushes.Green; ; }));
                    }
                    else if(item.Contains("DIA"))
                    {
                        Dispatcher.Invoke(new Action(() => { DIATextBlock.Background = Brushes.Green; ; }));
                    }
                }
            }
            else if (calibrationsList.Any() && DateTime.Now.DayOfWeek.ToString() != theDIADay) // Sunday
            {
                foreach (var item in calibrationsList)
                {
                    if (item.Contains("PD"))
                    {
                        Dispatcher.Invoke(new Action(() => { VisibilityButton.Background = Brushes.Green; ; }));
                    }
                }
            }
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private void MainWindow2_Loaded()
        {

            //string watchedFolder = System.IO.Path.GetFileName("C:\\SODIM\\Sodim Instrumentation\\Sodimax-line-lab V4\\HISTORY.ARCH\\");
            //string watchedFolder = System.IO.Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), "Pictures");
            
            watchedFolder = kalibracje.getPaths();

            // TODO: get file name from copy sodim
            // TODO: set timer from time when the 
            FileSystemWatcher fsw = new FileSystemWatcher(watchedFolder);

            fsw.NotifyFilter = NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.FileName
                                 | NotifyFilters.DirectoryName;


            //fsw.Changed += OnChanged;
            fsw.Created += OnChanged;
            //fsw.Deleted += OnChanged;
            //fsw.Renamed += OnRenamed;

            

            fsw.EnableRaisingEvents = true;
    
        }


        // Define the event handlers.
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            //if(DateTime.Now.DayOfWeek.ToString() == theDIADay)
            //{
            //    Dispatcher.Invoke(new Action(() => { DIATextBlock.Background = Brushes.Green; ; }));
            //}
            //Dispatcher.Invoke(new Action(() => { VisibilityButton.Background = Brushes.Green; ; }));
            ChceckIfAnyCalibrationWasTodayMaken();
            timeToReset();
        }


        void timeToReset()
        {
            var now = DateTime.Now;
            var tomorrow = now.AddDays(1);
            var durationUntilMidnight = tomorrow.Date - now;
            MessageBox.Show("Czas do resetu greenKalibButtona: " + durationUntilMidnight);

            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = durationUntilMidnight.TotalMilliseconds;
            aTimer.Enabled = true;
            aTimer = null;
            GC.Collect();

        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            if (DateTime.Now.DayOfWeek.ToString() == theDIADay)
            {
                Dispatcher.Invoke(new Action(() => { DIATextBlock.Visibility = Visibility.Visible; ; }));
                Dispatcher.Invoke(new Action(() => { DIATextBlock.Background = Brushes.Red; ; }));

            }
            else if (DateTime.Now.DayOfWeek.ToString() != theDIADay)
            {
                Dispatcher.Invoke(new Action(() => { DIATextBlock.Visibility = Visibility.Hidden ; ; }));
            }

            Dispatcher.Invoke(new Action(() => { VisibilityButton.Background = Brushes.Red; ; }));
        }

        private void VisibilityButton_Click(object sender, RoutedEventArgs e)
        {
            if (ifCllicked)
            {
                ifCllicked = false;
                StackPanelWithButtons.Visibility = Visibility.Hidden;
            }
            else
            {
                ifCllicked = true;
                StackPanelWithButtons.Visibility = Visibility.Visible;
            }
        }

        private void LogInOutButton(object sender, RoutedEventArgs e)
        {
            if(!zalogowany)
            {
                Login login = new Login();
                GC.Collect();
                login.ShowDialog();
                //login = null;
                //GC.Collect();
                //this.Close();
                if (login.loginStatus()==true)
                {
                    ChangeStatus(true);
                }
            }
            else
            {
                Dispatcher.Invoke(new Action(() => { LoginOutButton.Content = "Zaloguj"; ; }));
                Dispatcher.Invoke(new Action(() => { AwarieButton.IsEnabled = false; ; }));
                Dispatcher.Invoke(new Action(() => { KalibracjeButton.IsEnabled = false; ; }));
                Dispatcher.Invoke(new Action(() => { WymianaGumButton.IsEnabled = false; ; }));
                Dispatcher.Invoke(new Action(() => { Awaria_StartButton.IsEnabled = false; ; }));
                Dispatcher.Invoke(new Action(() => { TurnOffButton.IsEnabled = false; ; }));
                Dispatcher.Invoke(new Action(() => { BlokowanieEkranuButton.IsEnabled = false; ; }));
                Dispatcher.Invoke(new Action(() => { CmdButton.IsEnabled = false; ; }));
 
                //Dispatcher.Invoke(new Action(() => { LogoutRemoteDesktopButton.IsEnabled = false; ; }));
                zalogowany = false;
            }
        }

       internal  void ChangeStatus(bool val)
        {
            if (val)
            {
                this.Show();
                StackPanelWithButtons.Visibility = Visibility.Visible;
                Dispatcher.Invoke(new Action(() => { LoginOutButton.Content = "Wyloguj"; ; }));
                Dispatcher.Invoke(new Action(() => { AwarieButton.IsEnabled = true; ; }));
                Dispatcher.Invoke(new Action(() => { WymianaGumButton.IsEnabled = true; ; }));
                Dispatcher.Invoke(new Action(() => { KalibracjeButton.IsEnabled = true; ; }));
                Dispatcher.Invoke(new Action(() => { Awaria_StartButton.IsEnabled = true; ; }));
                Dispatcher.Invoke(new Action(() => { TurnOffButton.IsEnabled = true; ; }));
                Dispatcher.Invoke(new Action(() => { BlokowanieEkranuButton.IsEnabled = true; ; }));
                Dispatcher.Invoke(new Action(() => { CmdButton.IsEnabled = true; ; }));
             //    Dispatcher.Invoke(new Action(() => { LogoutRemoteDesktopButton.IsEnabled = true; ; }));
               
                zalogowany = true;
            }
       
            else
            {
                this.Show();
                StackPanelWithButtons.Visibility = Visibility.Visible;
                Dispatcher.Invoke(new Action(() => { LoginOutButton.Content = "Zaloguj"; ; }));
                Dispatcher.Invoke(new Action(() => { AwarieButton.IsEnabled = false; ; }));
                Dispatcher.Invoke(new Action(() => { WymianaGumButton.IsEnabled = false; ; }));
                Dispatcher.Invoke(new Action(() => { KalibracjeButton.IsEnabled = false; ; }));
                Dispatcher.Invoke(new Action(() => { Awaria_StartButton.IsEnabled = false; ; }));
                Dispatcher.Invoke(new Action(() => { TurnOffButton.IsEnabled = false; ; }));
                Dispatcher.Invoke(new Action(() => { BlokowanieEkranuButton.IsEnabled = false; ; }));
                Dispatcher.Invoke(new Action(() => { CmdButton.IsEnabled = false; ; }));
        
               // Dispatcher.Invoke(new Action(() => { LogoutRemoteDesktopButton.IsEnabled = false; ; }));
                zalogowany = false;
            }
        }
        private void TurnOffButton_Click(object sender, RoutedEventArgs e)
        {

            if(MessageBox.Show("Czy chcesz zamknąć aplikację?", "UWAGA", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Environment.Exit(Environment.ExitCode);
                Application.Current.Shutdown();
            }

        }

        private void AwarieButton_Click(object sender, RoutedEventArgs e)
        {
            AwarieKalibracje awarie = new AwarieKalibracje();

            List<string> listaawarii;
            listaawarii = awarie.ListaAwarii();
            SpisAwarii sa = new SpisAwarii(listaawarii, false);
            sa.ShowDialog();
            sa = null;
            GC.Collect();
        }

        private void KalibracjeButton_Click(object sender, RoutedEventArgs e)
        {
            AwarieKalibracje kalibracje = new AwarieKalibracje();

            List<string> listaKalibracji;
            listaKalibracji = kalibracje.ListaKalibracji();

            SpisAwarii sa = new SpisAwarii(listaKalibracji, true);

            sa.ShowDialog();
            sa = null;
            GC.Collect();
        }

        private void Awaria_StartButton_Click(object sender, RoutedEventArgs e)
        {
            WpisAwari wa = new WpisAwari();

          //  DateTime = DateTime.Now;
            if (!awariaButtonStatus)
            {
                startAwaria = DateTime.Now;
                Dispatcher.Invoke(new Action(() => { Awaria_StartButton.Content = "AWARIA STOP"; ; }));
                Dispatcher.Invoke(new Action(() => { Awaria_StartButton.Background =  Brushes.Red; ; }));
                awariaButtonStatus = true;
            }
            else
            {
                var Result = MessageBox.Show("Czy chcesz zakończyć proces trwania awarii?", "Zakończenie awarii", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (Result == MessageBoxResult.Yes)
                {
                    wa.clockStop(startAwaria);
                    wa.ShowDialog();
                    Dispatcher.Invoke(new Action(() => { Awaria_StartButton.Content = "AWARIA START"; ; }));
                    Awaria_StartButton.ClearValue(Button.BackgroundProperty);
                    awariaButtonStatus = false;
                }
                else if (Result == MessageBoxResult.No)
                {

                }
           
            } 

        }

        private void OPLButton_Click(object sender, RoutedEventArgs e)
        {
       
            MessageBox.Show("Na razie nic do pokazania :)");
        }


        private void CILcardButton_Click(object sender, RoutedEventArgs e)
        {
            GC.Collect();
            MessageBox.Show("Na razie nic do pokazania :)");
           
        }

        private void WymianaGumButton_Click(object sender, RoutedEventArgs e)
        {
            WymianaGum wg = new WymianaGum();
            wg.ShowDialog();
            wg = null;
            GC.Collect();
        }

        private void BlokowanieEkranuButton_Click(object sender, RoutedEventArgs e)
        {
            var Result = MessageBox.Show("Czy chcesz zablokować ekran?", "Blokowanie ekranu", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (Result == MessageBoxResult.Yes)
            {
                BlockerWindow bw = new BlockerWindow();
                  StackPanelWithButtons.Visibility = Visibility.Hidden;
                this.Hide();
                bw.ShowDialog();
                this.Show();
                bw = null;
                GC.Collect();
            }
            else if (Result == MessageBoxResult.No)
            {

            }
            
        }

        private void CmdButton_Click(object sender, RoutedEventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = "taskmgr";
            p.StartInfo.CreateNoWindow = true;
            p.Start();
       
            GC.Collect();
        }

        private void ShowRemoteDesktopListButton_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(remoteDesktopPath))
            {
                Process.Start(remoteDesktopPath);
            }
             else
             {
                 MessageBox.Show(string.Format("{0} Directory does not exist!", remoteDesktopPath));
            }
        }

        private void LogoutRemoteDesktopButton_Click(object sender, RoutedEventArgs e)
        {
            //InputSimulator sim = new InputSimulator();
            // enter username: QAUser01 
            // sim.Keyboard.TextEntry("QAUser01
            // press Tab key 
          //  sim.Keyboard.KeyPress((WindowsInput.Native.VirtualKeyCode)Key.B);
            // Enter Password 
         //   sim.Keyboard.TextEntry("acb@123");
            // submit enter 
            //sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            //Robot robot
            //AccessKeyPressedEventArgs k = Key.LWin;
            //Process.Start("rundll32.exe", "^{ESC}");
            //  string file="";
            //  //LogoutRemoteDesktop7.bat
            //  //LogoutRemoteDesktopXP.bat
            //  //
            //  string operatingSystem = System.Environment.OSVersion.ToString();
            //  if (operatingSystem.Contains("5.1"))
            //  {
            // //     file = "LogoutRemoteDesktopXP.bat";
            //  }
            //  else if (operatingSystem.Contains("6.1"))
            //  {
            //   //   file = "LogoutRemoteDesktop7.bat";
            //  }
            //  else 
            //  {
            //      MessageBox.Show(string.Format("{0} Brak obsługi tego systemu! Proszę skontaktuj się z Adamem Osewskim aby zrobił na ten system wersję RemoteDesktop Logout. Pozdrawiam Maciej.", "Wersja systemu:"+operatingSystem));
            //  }

            //  if (Directory.Exists(remoteDesktopPath))
            //  {
            ////      Process.Start(remoteDesktopPath+file);
            //  }
            //  else
            //  {
            //      MessageBox.Show(string.Format("{0} Directory does not exist!", remoteDesktopPath));
            //  }
        }
    }
}
