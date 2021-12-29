﻿using NavigationBar.KartaCIL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
using System.Xml.Linq;




namespace NavigationBar
{
    /// <summary>
    /// Logika interakcji dla klasy CILCard.xaml
    /// </summary>
    public partial class CILCard : Window
    {
        public CILCard()
        {
            InitializeComponent();
            GetModulesInfo();
        }

        
        string[] arrLine;
        string TMPlocationModule = "C:\\copy_sodim\\hollow.txt";
        string XMLCILTemplatePath = "C:\\Users\\Maciek\\Desktop\\E-Cil\\CIL_Template.xml";
        //var templates;
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

        public void GetModulesInfo()
        {

            //DataSet ds = new DataSet();
            //ds.ReadXml(XMLCILTemplatePath);
            //foreach(DataRow dr in ds.Tables["czynnosc"].Rows)
            //{
            //    for (int i = 0; i < dr.ItemArray.Length; i++)
            //    {
            //        MessageBox.Show(dr[i].ToString());
            //    }
            //}
            // LINQ 
            //aWhere(x => x.Elements().Last().Value == "HOLLOW")
            
            var xdoc = XDocument.Load(XMLCILTemplatePath);
            var templates = xdoc.Root.Descendants("czynnosc")  // trzeba załadować w listę obiektów 
                .Select(x => new Czynnosc(int.Parse(x.Attribute("id").Value),
                x.Element("module").Value,
                x.Element("duty").Value,
                int.Parse(x.Element("interwal").Value)));

            //foreach (var t in templates)
            //{
            //    //MessageBox.Show("ID:" + t.id + " MODÓŁ:" + t.module + " CZYNNOŚĆ DO WYKONANIA:" + t.duty + " INTERWAŁ:" + t.interwal);
            //}

            //Grid myGrid = new Grid();
            //myGrid.Width = 250;
            //myGrid.Height = 100;
            //myGrid.HorizontalAlignment = HorizontalAlignment.Left;
            //myGrid.VerticalAlignment = VerticalAlignment.Bottom;
            //myGrid.ShowGridLines = true;

            ColumnDefinition colDef1 = new ColumnDefinition();
            ColumnDefinition colDef2 = new ColumnDefinition();
            //Border border = new Border()
            //{
            //    BorderThickness = new Thickness()
            //    {
            //        Bottom = 1,
            //        Left = 1,
            //        Right = 1,
            //        Top = 1
            //    },
            //    BorderBrush = new SolidColorBrush(Colors.Black)
            //};
            //Border border2 = new Border()
            //{
            //    BorderThickness = new Thickness()
            //    {
            //        Bottom = 1,
            //        Left = 1,
            //        Right = 1,
            //        Top = 1
            //    },
            //    BorderBrush = new SolidColorBrush(Colors.Black)
            //};
      

            ///*** Dodawanie wierszy tytułowych 
            mainGridPanel.ColumnDefinitions.Add(colDef1);
            mainGridPanel.ColumnDefinitions.Add(colDef2);
         

            TextBlock tb1 = new TextBlock();
            TextBlock tb2 = new TextBlock();
            tb1.Text = "Moduł";
            tb2.Text = "Czynność";

            tb1.Background = Brushes.Gray;
            tb2.Background = Brushes.Gray;

            tb1.FontSize = 15;
            tb2.FontSize = 15;

            tb2.FontWeight = FontWeights.Bold;
            tb2.FontWeight = FontWeights.Bold;


            RowDefinition gridRow1 = new RowDefinition();
            RowDefinition gridRow2 = new RowDefinition();
            mainGridPanel.RowDefinitions.Add(gridRow1);
            mainGridPanel.RowDefinitions.Add(gridRow2);

            Grid.SetRow(tb1, 0);
            Grid.SetColumn(tb1, 0);

            Grid.SetRow(tb2, 0);
            Grid.SetColumn(tb2, 1);

            //Grid.SetColumn(border, 0);
            //Grid.SetRow(border, 0);

            //Grid.SetColumn(border2, 0);
            //Grid.SetRow(border2, 1);

            mainGridPanel.Children.Add(tb1);
            mainGridPanel.Children.Add(tb2);
            //mainGridPanel.Children.Add(border2);
            //mainGridPanel.Children.Add(border);

            int i = 1;

            // Dodawanie komórek z definicjami wykonywanych czynności 
            foreach (var item in templates)
            {
                TextBlock tb3 = new TextBlock();
                TextBlock tb4 = new TextBlock();
                RowDefinition gridRow3 = new RowDefinition();
                RowDefinition gridRow4 = new RowDefinition();
                //border = new Border()
                //{
                //    BorderThickness = new Thickness()
                //    {
                //        Bottom = 1,
                //        Left = 1,
                //        Right = 1,
                //        Top = 1
                //    },
                //    BorderBrush = new SolidColorBrush(Colors.Black)
                //};


           
                gridRow3.Height = new GridLength(30);
                gridRow4.Height = new GridLength(30);
                
                mainGridPanel.RowDefinitions.Add(gridRow3);
                mainGridPanel.RowDefinitions.Add(gridRow4);

                tb3.Text = item.module;
                tb4.Text = item.duty;

                tb3.Background = Brushes.Crimson;
                tb4.Background = Brushes.WhiteSmoke;

                tb3.TextWrapping = TextWrapping.Wrap;
                tb4.TextWrapping = TextWrapping.Wrap;

                tb3.FontSize = 10;
                tb4.FontSize = 10;

                tb3.FontWeight = FontWeights.Bold;
                tb4.FontWeight = FontWeights.Bold;

                Grid.SetRow(tb3, i);
                Grid.SetColumn(tb3, 0);

                Grid.SetRow(tb4, i);
                Grid.SetColumn(tb4, 1);

                //Grid.SetColumn(border, i);
                //Grid.SetRow(border, 0);

                //Grid.SetColumn(border, i);
                //Grid.SetRow(border, 1);

                //btn.Background = new SolidColorBrush(Colors.Azure);
                //btn.Foreground = new SolidColorBrush(Colors.Black);
                /* stackPanelContainer.Children.Add(btn)*/
                mainGridPanel.Children.Add(tb3);
                mainGridPanel.Children.Add(tb4);
                
                i++;
            }

            // Tworzenie komórek dat
            int days = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            MessageBox.Show("Ilość dni" + days);
            for (int j = 0; j < days; j++)
            {

                TextBlock tb12 = new TextBlock();
                ColumnDefinition columnDefinition = new ColumnDefinition();

                columnDefinition.Width = new GridLength(20);
                Grid.SetRow(tb12, 0);
                Grid.SetColumn(tb12, j+2);
                tb12.TextAlignment = TextAlignment.Center;
                tb12.FontWeight = FontWeights.Bold;
                tb12.Text = (j + 1).ToString();


         
                mainGridPanel.ColumnDefinitions.Add(columnDefinition);
                mainGridPanel.Children.Add(tb12);
            }





        }


    }
}
