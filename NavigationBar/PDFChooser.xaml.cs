using System;
using System.Collections.Generic;
using System.IO;
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
    /// Logika interakcji dla klasy PDFChooser.xaml
    /// </summary>
    public partial class PDFChooser : Window
    {
        public PDFChooser()
        {
            InitializeComponent();
            GetVlues();

        }

        List<string> pathList = new List<string>();
       // string pdfFilesPath = @"C:\Users\Maciek\Desktop\PDFfolder\";
        string locationTxtWithLocationOfSavePAth = @"C:\copy_sodim\PATH_TO_SAVE_CIGNUM.txt";
        string pdfFilesPath = "";
        string activatedFolder = "";

        private void GetVlues()
        {

            try
            {
                pathList = File.ReadAllLines(locationTxtWithLocationOfSavePAth).ToList();
                pdfFilesPath += pathList[23];
                pathList = null;

                pathList = Directory.EnumerateDirectories(pdfFilesPath).ToList();
                foreach (var item in pathList)
                {
                    Button btn = new Button();

                    btn.Height = 50;

                    btn.Width = 100;

                    btn.Name = "item";
                    DirectoryInfo di = new DirectoryInfo(item);
                    string path = di.Name;
                   
                    btn.Content = path;
                    //btn.Content = item;
                    Thickness margin = btn.Margin;
                    margin.Top = 8;
                    btn.Margin = margin;

                    btn.FontSize = 10;

                    btn.Background = new SolidColorBrush(Colors.Azure);

                    btn.Foreground = new SolidColorBrush(Colors.Black);

                    stackPanelFoldersContainer.Children.Add(btn);

                    btn.Click +=  OpenFolder;

                }
            }
            catch { MessageBox.Show("Wykryto błąd! Upewnij się że ścieżka lokalizacji insturkcji jest prawidłowa!", "Błąd!");
              
            }

            
        }

        private object DirectoryInfo(string v)
        {
            throw new NotImplementedException();
        }

        void changeColorFolders()
        {
            var buttons = stackPanelContainer.Children.Cast<Button>().ToList();
            foreach (var item in buttons)
            {
                item.Background = new SolidColorBrush(Colors.Azure);
            }
        }

        private void OpenFolder(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            changeColorFolders();
            button.Background = new SolidColorBrush(Colors.Blue);

            var folderPath = pdfFilesPath + button.Content.ToString() + "\\";
            pathList = null;
            stackPanelContainer.Children.Clear();
            GC.Collect();

            activatedFolder = button.Content.ToString();

            try
            {
                pathList = Directory.EnumerateFiles(folderPath).ToList();
                foreach (var item in pathList)
                {
                    Button btn = new Button();

                    btn.Height = 50;

                    btn.Width = 100;

                    btn.Name = "item";

                    btn.Content = System.IO.Path.GetFileName(item);
                    //btn.Content = item;
                    Thickness margin = btn.Margin;
                    margin.Top = 8;
                    btn.Margin = margin;

                    btn.FontSize = 16;

                    btn.Background = new SolidColorBrush(Colors.Orange);

                    btn.Foreground = new SolidColorBrush(Colors.Black);
                    
                    stackPanelContainer.Children.Add(btn);

                    btn.Click += RunPDF;

                }
            }
            catch
            {
                MessageBox.Show("Wykryto błąd! Upewnij się że ścieżka lokalizacji insturkcji jest prawidłowa!", "Błąd!");

            }
        }

        private void RunPDF(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            MessageBox.Show(pdfFilesPath + button.Content.ToString());
            System.Diagnostics.Process.Start(pdfFilesPath +"\\" + activatedFolder + "\\" + button.Content.ToString());
            GC.Collect();
            this.Close();
            GC.Collect();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            GC.Collect();
            this.Close();
            GC.Collect();
        }
    }
}
