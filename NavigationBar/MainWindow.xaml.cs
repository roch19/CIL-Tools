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


namespace NavigationBar
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {   //Thuersday , Monday
        string theDIADay = "Monday"; //dzień w którym kalibrowana jest średnica
        AwarieKalibracje kalibracje = new AwarieKalibracje();
        public bool ifCllicked = false;
        public bool zalogowany = false;
        public string remoteDesktopPath = @"\\10.177.11.130\Sodimats\Remote Desktop List\";
        string locationTxtWithLocationOfSavePAth = @"C:\copy_sodim\PATH_TO_SAVE_CIGNUM.txt";
        public string shutDownProgramFilePath = "";
        private string shutDownProgramContent = "";
        public bool ifWXP = false;
        bool awariaButtonStatus = false;
        bool statusPD = false;
        bool statusDIA = false;
        
        private string watchedFolder = "";
        System.Timers.Timer aTimer;
        DateTime startAwaria;
        List<string> calibrationsList = new List<string>();
  
        public object DataTime { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            this.Left = SystemParameters.PrimaryScreenWidth - this.Width;
            GetPathOfAutoDestructFolder();
            MainWindow2_Loaded();
            ChceckIfAnyCalibrationWasTodayMaken();
            SearchForExecutionFileToShutDownProgram();
    
            //App_Deactivated_LostFocus();
            this.Focus();
            ShittyFunctionToChceckIfAppIsOnTopOnWinows7();
            MidnightNotifier.DayChanged += (s, e) => { OnTimedEvent(); };
        }

        public void ShittyFunctionToChceckIfAppIsOnTopOnWinows7()
        {
            string operatingSystem = System.Environment.OSVersion.ToString();
            if (operatingSystem.Contains("6.1"))
            {
                Thread myThread = new Thread(() =>
                {
                    fun();
                });

                myThread.IsBackground = true; // <-- Set your thread to background
                myThread.Start();
            }
            else if (operatingSystem.Contains("5.1")) ifWXP = true;
          }

        private void GetPathOfAutoDestructFolder()
        {
            calibrationsList = File.ReadAllLines(locationTxtWithLocationOfSavePAth).ToList();
            shutDownProgramFilePath = calibrationsList[26];
            shutDownProgramContent = calibrationsList[28];
            calibrationsList.Clear();
            GC.Collect();
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

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private void SearchForExecutionFileToShutDownProgram()
        {

            watchedFolder = shutDownProgramFilePath;

            FileSystemWatcher fsw = new FileSystemWatcher(watchedFolder);

            fsw.NotifyFilter = NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.FileName
                                 | NotifyFilters.DirectoryName;


            //fsw.Changed += OnChanged;
            fsw.Created += OnChanged2;
            //fsw.Deleted += OnChanged;
            //fsw.Renamed += OnRenamed;

            fsw.EnableRaisingEvents = true;
        }

        private void OnChanged2(object source, FileSystemEventArgs e)
        {
                if (e.FullPath.ToString().Contains(shutDownProgramContent))
                {
                    Environment.Exit(Environment.ExitCode);
                    Application.Current.Shutdown();
                }
            
            calibrationsList.Clear();
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private void MainWindow2_Loaded()
        {

            watchedFolder = kalibracje.getPaths();

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

        //Po uruchomieniu programu sprawdza czy była robiona kalibraca w dniu dzisiejszym
      

        // Define the event handlers.
        private void OnChanged(object source, FileSystemEventArgs e)
        {
                if (e.FullPath.ToString().Contains("PD"))
                {
                    if(statusPD == false)
                    {
                        statusPD = true;
                        Dispatcher.Invoke(new Action(() => { PDTextBlock.Background = Brushes.Green; ; }));
                    }
                }
                  
            else if (e.FullPath.ToString().Contains("DIA"))
            {
                if (DateTime.Now.DayOfWeek.ToString() == theDIADay && statusDIA == false)
                {
                    statusDIA = true;
                    Dispatcher.Invoke(new Action(() => { DIATextBlock.Background = Brushes.Green; ; }));
                }
            } 
        }

        void ChceckIfAnyCalibrationWasTodayMaken()
        {
            try
            {

                GC.Collect();
                calibrationsList = kalibracje.ChceckIfWasAnyCalibrationToday();
                

                if (!calibrationsList.Any() && DateTime.Now.DayOfWeek.ToString() == theDIADay) // jeżeli nie ma żadnej kalibracji i jest dzień średnicy
                {
                    Dispatcher.Invoke(new Action(() => { DIATextBlock.Visibility = Visibility.Visible; ; }));
                    Dispatcher.Invoke(new Action(() => { DIATextBlock.Background = Brushes.Red; ; }));
                }
                else if (calibrationsList.Any() && DateTime.Now.DayOfWeek.ToString() == theDIADay) // Sunday.. Jeżeli jest dzień śednicy i jest i jest wpis kalibracji
                {
                    Dispatcher.Invoke(new Action(() => { DIATextBlock.Visibility = Visibility.Visible; ; }));

                    foreach (var item in calibrationsList)
                    {
                        if (item.Contains("PD"))
                        {
                            Dispatcher.Invoke(new Action(() => { PDTextBlock.Background = Brushes.Green; ; }));
                        }
                        else if (item.Contains("DIA"))
                        {
                            Dispatcher.Invoke(new Action(() => { DIATextBlock.Background = Brushes.Green; ; }));
                            statusDIA = true;
                        }
                    }
                }
                else if (calibrationsList.Any() && DateTime.Now.DayOfWeek.ToString() != theDIADay) // Sunday
                {
                    foreach (var item in calibrationsList)
                    {
                        if (item.Contains("PD"))
                        {
                            statusPD = true;
                            Dispatcher.Invoke(new Action(() => { PDTextBlock.Background = Brushes.Green; ; }));
                        }
                    }
                }
                GC.Collect();
            }
            catch (Exception e)
            {
                MessageBox.Show("Brak folderu coppy_sodim lub oprogramowania SODIM! Sprawdź czy folder copy_sodim wraz z zawartośćią znajduje sie we wskazanej ścieżce: C:\\copy_sodim", "BŁĄD KRYTYCZNY!");
                Environment.Exit(Environment.ExitCode);
                Application.Current.Shutdown();
            }

        }

        //void timeToReset()
        //{
        //    var now = DateTime.Now;   // pobieranie daty dzisiejszej (data + godzina)
        //    var remaining = TimeSpan.FromHours(24) - now.TimeOfDay; // czas do następnego dnia, tzn. ile pozostało do końca obecnego
        //    MessageBox.Show(remaining.ToString("hh\\:mm\\:ss\\.fff"));
        //    GC.Collect();
        //    aTimer = new System.Timers.Timer();
        //    aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
        //    aTimer.Interval = remaining.TotalMilliseconds;
        //    aTimer.AutoReset = true;
        //    aTimer.Enabled = true;
            
        //    GC.Collect();
        //}

        private void OnTimedEvent()
        {
            statusPD = false;
            statusDIA = false;
            if (DateTime.Now.DayOfWeek.ToString() == theDIADay)
            {
                Dispatcher.Invoke(new Action(() => { DIATextBlock.Visibility = Visibility.Visible; ; }));
                Dispatcher.Invoke(new Action(() => { DIATextBlock.Background = Brushes.Red; ; }));
               
            }
            else if (DateTime.Now.DayOfWeek.ToString() != theDIADay)
            {
                Dispatcher.Invoke(new Action(() => { DIATextBlock.Visibility = Visibility.Hidden ; ; }));
                
            }

            Dispatcher.Invoke(new Action(() => { PDTextBlock.Background = Brushes.Red; ; }));          

        }

        private void VisibilityButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeVisibilityBar();
        }

        void ChangeVisibilityBar()
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
                ChangeVisibilityBar();
                Dispatcher.Invoke(new Action(() => { LoginOutButton.Content = "Zaloguj"; ; }));
                Dispatcher.Invoke(new Action(() => { AwarieButton.IsEnabled = false; ; }));
                Dispatcher.Invoke(new Action(() => { KalibracjeButton.IsEnabled = false; ; }));
                Dispatcher.Invoke(new Action(() => { WymianaGumButton.IsEnabled = false; ; }));
                Dispatcher.Invoke(new Action(() => { Awaria_StartButton.IsEnabled = false; ; }));
                Dispatcher.Invoke(new Action(() => { TurnOffButton.IsEnabled = false; ; }));
                Dispatcher.Invoke(new Action(() => { BlokowanieEkranuButton.IsEnabled = false; ; }));
                Dispatcher.Invoke(new Action(() => { CmdButton.IsEnabled = false; ; }));
                Dispatcher.Invoke(new Action(() => { CILcardButton.IsEnabled = false; ; }));
                Dispatcher.Invoke(new Action(() => { RemoteDesktopButton.IsEnabled = false; ; }));
                Dispatcher.Invoke(new Action(() => { CILcardViewButton.IsEnabled = false; ; }));
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
                Dispatcher.Invoke(new Action(() => { CILcardButton.IsEnabled = true; ; }));
                Dispatcher.Invoke(new Action(() => { RemoteDesktopButton.IsEnabled = true; ; }));
                Dispatcher.Invoke(new Action(() => { CILcardViewButton.IsEnabled = true; ; }));
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
                Dispatcher.Invoke(new Action(() => { CILcardButton.IsEnabled = false; ; }));
                Dispatcher.Invoke(new Action(() => { RemoteDesktopButton.IsEnabled = false; ; }));
                Dispatcher.Invoke(new Action(() => { CILcardViewButton.IsEnabled = false; ; }));
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

            PDFChooser pDFChooser = new PDFChooser("pdf");
            pDFChooser.ShowDialog();
            GC.Collect();
        }


        private void CILcardButton_Click(object sender, RoutedEventArgs e)
        {
            CIL cil = new CIL();
            cil.ShowDialog();
            GC.Collect();           
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

        private void CILcardViewButton_Click(object sender, RoutedEventArgs e)
        {
            CILCard cc = new CILCard();
            cc.ShowDialog();
            cc = null;
            GC.Collect();
        }

        private void RemoteDesktopButton_Click(object sender, RoutedEventArgs e)
        {
            PDFChooser pDFChooser = new PDFChooser("remote");
            pDFChooser.ShowDialog();
            GC.Collect();
        }
    }
}
