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

        private void GetVlues()
        {

            try
            {
                pathList = File.ReadAllLines(locationTxtWithLocationOfSavePAth).ToList();
                pdfFilesPath += pathList[23];
                pathList = null;

                pathList = Directory.EnumerateFiles(pdfFilesPath).ToList();
                foreach (var item in pathList)
                {
                    Button btn = new Button();

                    btn.Height = 50;

                    btn.Width = 350;

                    btn.Name = "item";

                    btn.Content = System.IO.Path.GetFileName(item);
                    //btn.Content = item;
                    Thickness margin = btn.Margin;
                    margin.Top = 8;
                    btn.Margin = margin;

                    btn.FontSize = 16;

                    btn.Background = new SolidColorBrush(Colors.Azure);

                    btn.Foreground = new SolidColorBrush(Colors.Black);

                    stackPanelContainer.Children.Add(btn);

                    btn.Click += RunPDF;

                }
            }
            catch { MessageBox.Show("Wykryto błąd! Upewnij się że ścieżka lokalizacji insturkcji jest prawidłowa!", "Błąd!");
              
            }

            
        }


        private void RunPDF(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            System.Diagnostics.Process.Start(pdfFilesPath + button.Content.ToString());
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
