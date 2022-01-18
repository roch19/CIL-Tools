using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.IO;

using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace NavigationBar
{
    /// <summary>
    /// Logika interakcji dla klasy NowyWpisKartyCIL.xaml
    /// </summary>
    public partial class NowyWpisKartyCIL : Window
    {
        public NowyWpisKartyCIL()
        {
            InitializeComponent();
            GetModulesInfo(1);
        }

      

        string[] arrLine;
        string TMPlocationModule = "C:\\copy_sodim\\hollow.txt";
        string XMLCILTemplatePath = "C:\\Users\\Maciek\\Desktop\\E-Cil\\CIL_Template.xml";
        string templatePath = "C:\\copy_sodim\\";
        string nameOfXMLFile = ZmienneGlobalne.numer_sodimatu.ToString() + "_wpisKartyCIL_" + System.DateTime.Now.Year.ToString()+".r.xml";


        public class Kalibracje
        {
            public int PD { get; set; }
            public int WG { get; set; }
            public int CIR { get; set; }
            public int HAR { get; set; }
            public int HOLLOW { get; set; }

            public Kalibracje(int pd, int wg, int cir, int har, int hollow)
            {
                PD = pd;
                WG = wg;
                CIR = cir;
                HAR = har;
                HOLLOW = hollow;
            }

        }
        public class Czynnosc
        {
            public int id { get; set; }
            public string module { get; set; }
            public string duty { get; set; }
            public int interwal { get; set; }

            public Czynnosc(int _id, string _module, string _duty, int _interwal)
            {
                id = _id;
                module = _module;
                duty = _duty;
                interwal = _interwal;
            }
        }

        
        public class ListaWpisow
        {
            
            public string data { get; set; } 
            public string module { get; set; }
            public string duty { get; set; }
            public string status { get; set; }

            //public WpisCzynnosci(string _data, string _module, string _duty, string _status)
            //{
            //    data = _data;
            //    module = _module;
            //    duty = _duty;
            //    status = _status;
            //}
        }


        public List<Czynnosc> czynnosc = new List<Czynnosc>();
        public List<Czynnosc> toSave = new List<Czynnosc>();
        public void GetModulesInfo(int interwal)
        {

            var xdoc = XDocument.Load(XMLCILTemplatePath);
            var templates = xdoc.Root.Descendants("czynnosc")  // trzeba załadować w listę obiektów 
                .Select(x => new Czynnosc(int.Parse(x.Attribute("id").Value),
                x.Element("module").Value,
                x.Element("duty").Value,
                int.Parse(x.Element("interwal").Value)));

            czynnosc.Clear();

            mainGridPanel.RowDefinitions.Clear();
            mainGridPanel.Children.Clear();
            mainGridPanel.ColumnDefinitions.Clear();
            GC.Collect();

            ColumnDefinition colDef1 = new ColumnDefinition();
            ColumnDefinition colDef2 = new ColumnDefinition();
            ColumnDefinition colDef3 = new ColumnDefinition();
            ColumnDefinition colDef4 = new ColumnDefinition();
            ColumnDefinition colDef5 = new ColumnDefinition();

            colDef1.Width = new GridLength(80);
            colDef2.Width = new GridLength(120);
            colDef3.Width = new GridLength(120);
            colDef4.Width = new GridLength(120);
            colDef5.Width = new GridLength(120);

            mainGridPanel.ColumnDefinitions.Add(colDef1);
            mainGridPanel.ColumnDefinitions.Add(colDef2);
            mainGridPanel.ColumnDefinitions.Add(colDef3);
            mainGridPanel.ColumnDefinitions.Add(colDef4);
            mainGridPanel.ColumnDefinitions.Add(colDef5);


            TextBlock tb1 = new TextBlock();
            TextBlock tb2 = new TextBlock();
            TextBlock tb3 = new TextBlock();
            TextBlock tb4 = new TextBlock();
            TextBlock tb5 = new TextBlock();
            tb1.Text = "Moduł";
            tb2.Text = "Czynność";
            tb3.Text = "Sprawdzono,\n stan OK";
            tb4.Text = "Wymiana,\n regulacja";
            tb5.Text = "Brak czynności";


            tb1.Background = Brushes.Transparent;
            tb2.Background = Brushes.Transparent;
            tb3.Background = Brushes.Transparent;
            tb4.Background = Brushes.Transparent;
            tb5.Background = Brushes.Transparent;

            tb1.FontSize = 15;
            tb2.FontSize = 15;
            tb3.FontSize = 15;
            tb4.FontSize = 15;
            tb5.FontSize = 15;

            tb1.FontWeight = FontWeights.Bold;
            tb2.FontWeight = FontWeights.Bold;
            tb3.FontWeight = FontWeights.Bold;
            tb4.FontWeight = FontWeights.Bold;
            tb5.FontWeight = FontWeights.Bold;

            tb1.TextAlignment = TextAlignment.Center;
            tb2.TextAlignment = TextAlignment.Center;
            tb3.TextAlignment = TextAlignment.Center;
            tb4.TextAlignment = TextAlignment.Center;
            tb5.TextAlignment = TextAlignment.Center;

            RowDefinition gridRow1 = new RowDefinition();
            RowDefinition gridRow2 = new RowDefinition();

            mainGridPanel.RowDefinitions.Add(gridRow1);
            mainGridPanel.RowDefinitions.Add(gridRow2);


            Grid.SetRow(tb1, 0);
            Grid.SetColumn(tb1, 0);

            Grid.SetRow(tb2, 0);
            Grid.SetColumn(tb2, 1);

            Grid.SetRow(tb3, 0);
            Grid.SetColumn(tb3, 2);

            Grid.SetRow(tb4, 0);
            Grid.SetColumn(tb4, 3);

            Grid.SetRow(tb5, 0);
            Grid.SetColumn(tb5, 4);

            //Grid.SetColumn(border, 0);
            //Grid.SetRow(border, 0);

            //Grid.SetColumn(border2, 0);
            //Grid.SetRow(border2, 1);

            mainGridPanel.Children.Add(tb1);
            mainGridPanel.Children.Add(tb2);
            mainGridPanel.Children.Add(tb3);
            mainGridPanel.Children.Add(tb4);
            mainGridPanel.Children.Add(tb5);


            int i = 1;

            // Dodawanie komórek z definicjami wykonywanych czynności 
            foreach (var item in templates)
            {
                if (item.interwal == interwal || interwal==0)
                {
                    tb3 = new TextBlock();
                    tb4 = new TextBlock();
                    RowDefinition gridRow3 = new RowDefinition();
                    RowDefinition gridRow4 = new RowDefinition();

                    gridRow3.Height = new GridLength(30);
                    gridRow4.Height = new GridLength(30);

                    mainGridPanel.RowDefinitions.Add(gridRow3);
                    mainGridPanel.RowDefinitions.Add(gridRow4);

                    tb3.Text = item.module;
                    tb4.Text = item.duty;

                    switch (item.module.ToString())
                    {
                        case "PD":
                            tb3.Background = Brushes.Green;
                            break;

                        case "HAR":
                            tb3.Background = Brushes.Red;
                            break;
                        case "ALL":
                            tb3.Background = Brushes.Transparent;
                            break;
                        case "WG":
                            tb3.Background = Brushes.Orange;
                            break;
                        case "DIA":
                            tb3.Background = Brushes.Yellow;
                            break;

                        default:
                            tb3.Background = Brushes.Aqua;
                            break;
                    }


                    tb4.Background = Brushes.Transparent;

                    tb3.TextAlignment = TextAlignment.Center;
                    tb4.TextAlignment = TextAlignment.Center;
                    tb4.TextWrapping = TextWrapping.Wrap;
                    tb3.FontSize = 10;
                    tb4.FontSize = 10;

                    tb3.FontWeight = FontWeights.Bold;
                    tb4.FontWeight = FontWeights.Bold;

                    czynnosc.Add(item);

                    Grid.SetRow(tb3, i);
                    Grid.SetColumn(tb3, 0);

                    Grid.SetRow(tb4, i);
                    Grid.SetColumn(tb4, 1);

                    mainGridPanel.Children.Add(tb3);
                    mainGridPanel.Children.Add(tb4);

                    i++;
                }
               
            }



            // Rysowanie siatki
            int j=5;
     
        
            for (int a = 0; a < j; a++)
            {
                for (int y = 0; y < i; y++)
                {
                    Border border = new Border()
                    {
                        BorderThickness = new Thickness()
                        {
                            Bottom = 1,
                            Left = 1,
                            Right = 1,
                            Top = 1
                        },
                        BorderBrush = new SolidColorBrush(Colors.Black)
                    };
          
                    Grid.SetColumn(border, a);
                    Grid.SetRow(border, y);
                
                    mainGridPanel.Children.Add(border);
                }
            }

            j = 4;

                for (int y = 1; y < i; y++)
                {

                RadioButton rb1 = new RadioButton();
                RadioButton rb2 = new RadioButton();
                RadioButton rb3 = new RadioButton();

                rb1.GroupName =  y.ToString() ;
                rb2.GroupName =  y.ToString() ;
                rb3.GroupName =  y.ToString();

                rb1.Name = "A";
                rb2.Name = "B";
                rb3.Name = "C";

                rb1.VerticalAlignment = VerticalAlignment.Center;
                rb2.VerticalAlignment = VerticalAlignment.Center;
                rb3.VerticalAlignment = VerticalAlignment.Center;

                rb1.HorizontalAlignment = HorizontalAlignment.Center;
                rb2.HorizontalAlignment = HorizontalAlignment.Center;
                rb3.HorizontalAlignment = HorizontalAlignment.Center;



                //rb1.Content = " \n ";

                Grid.SetColumn(rb1, 2);
                Grid.SetRow(rb1, y);

                Grid.SetColumn(rb2, 3);
                Grid.SetRow(rb2, y);


                Grid.SetColumn(rb3, 4);
                Grid.SetRow(rb3, y);


                mainGridPanel.Children.Add(rb1);
                mainGridPanel.Children.Add(rb2);
                mainGridPanel.Children.Add(rb3);
            }
            



        }

        private void ChangeDutyViewButton(object sender, RoutedEventArgs e)
        {
            SolidColorBrush hide = new SolidColorBrush(Color.FromRgb(3, 122, 122));
            SolidColorBrush top = new SolidColorBrush(Color.FromRgb(9, 207, 207));
          
            try
            {
                var button = (Button)sender;
                switch (button.Content.ToString())
                {
                    case "Dzienna":
                        miesiecznaButton.Foreground = hide;
                        tygodniowaButton.Foreground = hide;
                        dwaTygodniowaButton.Foreground = hide;
                        rocznaButton.Foreground = hide;
                        dziennaButton.Foreground = top;
                        GetModulesInfo(1);
                        break;

                    case "Tygodniowa":
                        miesiecznaButton.Foreground = hide;
                        tygodniowaButton.Foreground = top;
                        dwaTygodniowaButton.Foreground = hide;
                        rocznaButton.Foreground = hide;
                        dziennaButton.Foreground = hide;
                        GetModulesInfo(7);
                        break;
                    case "2 Tygodniowa":
                        miesiecznaButton.Foreground = hide;
                        tygodniowaButton.Foreground = hide;
                        dwaTygodniowaButton.Foreground = top;
                        rocznaButton.Foreground = hide;
                        dziennaButton.Foreground = hide;
                        GetModulesInfo(14);
                        break;
                    case "Miesięczna":
                        miesiecznaButton.Foreground = top;
                        tygodniowaButton.Foreground = hide;
                        dwaTygodniowaButton.Foreground = hide;
                        rocznaButton.Foreground = hide;
                        dziennaButton.Foreground = hide;
                        GetModulesInfo(30);
                        break;
                    case "Roczna":
                        miesiecznaButton.Foreground = hide;
                        tygodniowaButton.Foreground = hide;
                        dwaTygodniowaButton.Foreground = hide;
                        rocznaButton.Foreground = top;
                        dziennaButton.Foreground = hide;
                        GetModulesInfo(0);
                        break;

                    default:
                        break;
                }
            }
            catch { MessageBox.Show("Wykryto bład podczas próby wykonywania operacji."); }         

            }

        public void SerializeDataToXML()
        {

            var list = this.mainGridPanel.Children.OfType<RadioButton>().Where(x => x.IsChecked == true);
            if (!list.Any())
            {
                MessageBox.Show("Nie wprowadzono żadnych zmian, zapis nie przyniósł żadnych efektów.");
            }
            RadioButton rb = new RadioButton();
            XDocument xdoc = new XDocument();
            string path = templatePath + nameOfXMLFile;
            //XDocument xd = new XDocument();

            //xd.Add(new XElement("ABC"));
            //xd.Save("C:\\copy_sodim\\blabla.xml");

            if (!File.Exists(path))
            {
                XDocument xd = new XDocument();
                xd = new XDocument();
                xd.Add(new XElement("ListaWpisow"));
                //xd.Save(templatePath + nameOfXMLFile);
                xd.Save(path); //
            }



            //// xdoc = XDocument.Load(templatePath + nameOfXMLFile);


            foreach (var item in list)
            {

                // group name -> y -> nr wiersza w kolumnie
                rb = item;
                var a = czynnosc.ElementAt(int.Parse(rb.GroupName) - 1);
                xdoc = XDocument.Load(path);
                try
                {
                   // MessageBox.Show(a.duty.ToString());
                    xdoc.Element("ListaWpisow").Add(
                    new XElement("Wpis",
                    new XElement("data", Convert.ToString(System.DateTime.Now.Date.ToString("dd/MM/yyyy"))),
                    new XElement("modul", Convert.ToString(a.module)),
                    new XElement("czynnosc", Convert.ToString(a.duty)),
                    new XElement("status", Convert.ToString(rb.Name.ToString()))
                    ));
                   // xdoc.Save(templatePath + nameOfXMLFile);
                    xdoc.Save(path);
                }
                catch { MessageBox.Show("Podczas próby zapisu obiekt do zapisu nie został wykryty"); }

            }



            //    else
            //    {
            //        xdoc = new XDocument();
            //        xdoc.Add(new XElement("ListaWpisow"));

            //        MessageBox.Show("Hello");
            //        xdoc.Element("ListaWpisow").Add(
            //        new XElement("Wpis",
            //        new XElement("data", Convert.ToString(System.DateTime.Now.ToLongDateString()),
            //        new XElement("modul", Convert.ToString(a.module)),
            //        new XElement("czynnosc", Convert.ToString(a.duty)),
            //        new XElement("status", Convert.ToString(rb.Name.ToString()))
            //        )));
            //        xdoc.Save(templatePath + nameOfXMLFile);
            //    }
            //    xdoc.Save(templatePath + nameOfXMLFile);

            //}
            //}

            //else
            //{

            //    foreach (var item in list)
            //    {
            //        rb = item;
            //        var a = czynnosc.ElementAt(int.Parse(rb.GroupName) - 1);


            //XDocument xdoc = new XDocument();
            //xdoc.Add(new XElement("ListaWpisow"));

            //xdoc.Element("ListaWpisow").Add(
            //new XElement("Wpis",
            //new XElement("data", Convert.ToString(System.DateTime.Now.ToLongDateString()),
            //new XElement("modul", Convert.ToString(a.module)),
            //new XElement("czynnosc", Convert.ToString(a.duty)),
            //new XElement("status", Convert.ToString(rb.Name.ToString()))
            //)));
            //xdoc.Save(templatePath + nameOfXMLFile);
            ////    }


            //    ;
            //}
            //else
            //{
            //    XmlSerializer serializer = new XmlSerializer(typeof(ListaWpisow));
            //    TextWriter writer = new StreamWriter(templatePath + nameOfXMLFile);
            //    ListaWpisow wc = new ListaWpisow();

            //    //   List<WpisCzynnosci> wpis = new List<WpisCzynnosci>();

            //    foreach (var item in list)
            //    {
            //        // group name -> y -> nr wiersza w kolumnie
            //        rb = item;
            //      
            //         MessageBox.Show("Zaznaczono" + a.duty.ToString() + "Z kolumny: " + rb.Name.ToString());
            //        wc.data = System.DateTime.Now.ToLongDateString();
            //        wc.module = a.module;
            //        wc.duty = a.duty;
            //        wc.status = rb.Name.ToString();
            //        // wpis.Add(wc);
            //    }

            //    serializer.Serialize(writer, wc);
            //    writer.Close();
            //}



            //// Serialize the purchase order, and close the TextWriter.
            //serializer.Serialize(writer, po);

        }

       


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SerializeDataToXML();
            //   SerializeDataToXMLNEW();
            GC.Collect();
            this.Close();
            GC.Collect();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GC.Collect();
            this.Close();
            GC.Collect();
        }
    }
}


