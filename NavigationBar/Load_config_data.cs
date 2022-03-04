using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using static NavigationBar.CILCard;

namespace NavigationBar
{
    public class Load_config_data
    {
        class Config
        {
            public Config(int przebieg_KS_papierosowy, int przebieg_DS_papierosowy, int przebieg_SS_papierosowy, int przebieg_KS_filtrowy, int przebieg_DS_filtrowy, 
                int przebieg_SS_filtrowy, int przebieg_CIL_filtrowy, int przebieg_CIL_papierosowy, int przebieg_Konserwacja_filtrowy, int przebieg_Konserwacja_papierosowy, 
                int termin_KS_papierosowy, int termin_DS_papierosowy, int termin_SS_papierosowy, int termin_KS_filtrowy, int termin_DS_filtrowy, int termin_SS_filtrowy, 
                int termin_CIL_papierosowy, int termin_CIL_filtrowy, int termin_Konserwacja_papierosowy, int termin_Konserwacja_filtrowy, string path_zapis_wymian_gum, 
                string path_zapis_awarii, string path_zapis_cila, string path_instukcje, string path_autodestruct, string path_remoteDesktop, string path_template_CIL,
                string path_saveBackup , string path_saveRaport8h)
            {
                this.przebieg_KS_papierosowy = przebieg_KS_papierosowy;
                this.przebieg_DS_papierosowy = przebieg_DS_papierosowy;
                this.przebieg_SS_papierosowy = przebieg_SS_papierosowy;
                this.przebieg_KS_filtrowy = przebieg_KS_filtrowy;
                this.przebieg_DS_filtrowy = przebieg_DS_filtrowy;
                this.przebieg_SS_filtrowy = przebieg_SS_filtrowy;
                this.przebieg_CIL_filtrowy = przebieg_CIL_filtrowy;
                this.przebieg_CIL_papierosowy = przebieg_CIL_papierosowy;
                this.przebieg_Konserwacja_filtrowy = przebieg_Konserwacja_filtrowy;
                this.przebieg_Konserwacja_papierosowy = przebieg_Konserwacja_papierosowy;
                this.termin_KS_papierosowy = termin_KS_papierosowy;
                this.termin_DS_papierosowy = termin_DS_papierosowy;
                this.termin_SS_papierosowy = termin_SS_papierosowy;
                this.termin_KS_filtrowy = termin_KS_filtrowy;
                this.termin_DS_filtrowy = termin_DS_filtrowy;
                this.termin_SS_filtrowy = termin_SS_filtrowy;
                this.termin_CIL_papierosowy = termin_CIL_papierosowy;
                this.termin_CIL_filtrowy = termin_CIL_filtrowy;
                this.termin_Konserwacja_papierosowy = termin_Konserwacja_papierosowy;
                this.termin_Konserwacja_filtrowy = termin_Konserwacja_filtrowy;
                this.path_zapis_wymian_gum = path_zapis_wymian_gum;
                this.path_zapis_awarii = path_zapis_awarii;
                this.path_zapis_cila = path_zapis_cila;
                this.path_instukcje = path_instukcje;
                this.path_autodestruct = path_autodestruct;
                this.path_remoteDesktop = path_remoteDesktop;
                this.path_template_CIL = path_template_CIL;
                this.path_saveBackup = path_saveBackup;
                this.path_saveRaport8h = path_saveRaport8h;
            }

            public  int przebieg_KS_papierosowy { get; set; }
            public  int przebieg_DS_papierosowy { get; set; }
            public  int przebieg_SS_papierosowy { get; set; }
            public  int przebieg_KS_filtrowy{get; set;}
            public  int przebieg_DS_filtrowy
            {
                get; set;
            }
            public  int przebieg_SS_filtrowy
            {
                get; set;
            }
            public  int przebieg_CIL_filtrowy
            {
                get; set;
            }
            public  int przebieg_CIL_papierosowy
            {
                get; set;
            }
            public  int przebieg_Konserwacja_filtrowy
            {
                get; set;
            }
            public  int przebieg_Konserwacja_papierosowy
            {
                get; set;
            }
            public  int termin_KS_papierosowy
            {
                get; set;
            }
            public  int termin_DS_papierosowy
            {
                get; set;
            }
            public  int termin_SS_papierosowy
            {
                get; set;
            }
            public  int termin_KS_filtrowy
            {
                get; set;
            }
            public  int termin_DS_filtrowy
            {
                get; set;
            }
            public  int termin_SS_filtrowy
            {
                get; set;
            }
            public  int termin_CIL_papierosowy
            {
                get; set;
            }
            public  int termin_CIL_filtrowy
            {
                get; set;
            }
            public  int termin_Konserwacja_papierosowy{get; set;}
            public  int termin_Konserwacja_filtrowy{get; set;}


            //info nt. ścierzek zapisu/odczytu etc
            public  string path_zapis_wymian_gum
            {
                get; set;
            }

            public  string path_zapis_awarii
            {
                get; set;
            }

            public  string path_zapis_cila
            {
                get; set;
            }

            public  string path_instukcje
            {
                get; set;
            }

            public  string path_autodestruct
            {
                get; set;
            }

            public  string path_remoteDesktop
            {
                get; set;
            }

            public  string path_template_CIL
            {
                get; set;
            }

            public  string path_saveBackup
            {
                get; set;
            }
            public string path_saveRaport8h
            {
                get; set;
            }
            

        }
        public void load()
        {
            try
            {

          
                var xdoc = XDocument.Load(ZmienneGlobalne.path_to_Config_file); // na razie zmienna lokalna, później globalna
                var soidm = xdoc.Root.Descendants("config")  // trzeba załadować w listę obiektów 
             .Select(x => new Config(int.Parse(x.Element("przebieg_KS_papierosowy").Value),
                    int.Parse(x.Element("przebieg_DS_papierosowy").Value),
              
                    int.Parse(x.Element("przebieg_SS_papierosowy").Value),
            int.Parse(x.Element("przebieg_KS_filtrowy").Value),
            int.Parse(x.Element("przebieg_DS_filtrowy").Value),
            int.Parse(x.Element("przebieg_SS_filtrowy").Value),
            int.Parse(x.Element("przebieg_CIL_filtrowy").Value),
            int.Parse(x.Element("przebieg_CIL_papierosowy").Value),
            int.Parse(x.Element("przebieg_Konserwacja_filtrowy").Value),
            int.Parse(x.Element("przebieg_Konserwacja_papierosowy").Value),
            int.Parse(x.Element("termin_KS_papierosowy").Value),
            int.Parse(x.Element("termin_DS_papierosowy").Value),
            int.Parse(x.Element("termin_SS_papierosowy").Value),
            int.Parse(x.Element("termin_KS_filtrowy").Value),

            int.Parse(x.Element("termin_DS_filtrowy").Value),
            int.Parse(x.Element("termin_SS_filtrowy").Value),
            int.Parse(x.Element("termin_CIL_papierosowy").Value),
            int.Parse(x.Element("termin_CIL_filtrowy").Value),
            int.Parse(x.Element("termin_Konserwacja_papierosowy").Value),
            int.Parse(x.Element("termin_Konserwacja_filtrowy").Value),


               x.Element("path_zapis_wymian_gum").Value,
               x.Element("path_zapis_awarii").Value,
               x.Element("path_zapis_cila").Value,
               x.Element("path_instukcje").Value,
               x.Element("path_autodestruct").Value,
               x.Element("path_remoteDesktop").Value,
               x.Element("path_template_CIL").Value,
               x.Element("path_saveBackup").Value,
               x.Element("path_saveRaport8h").Value)); ;

                foreach (var item in soidm)
                {
                    ZmienneGlobalne.przebieg_KS_papierosowy = item.przebieg_KS_papierosowy;
                    ZmienneGlobalne.przebieg_DS_papierosowy = item.przebieg_DS_papierosowy;
                    ZmienneGlobalne.przebieg_SS_papierosowy = item.przebieg_SS_papierosowy;
                    ZmienneGlobalne.przebieg_KS_filtrowy = item.przebieg_KS_filtrowy;
                    ZmienneGlobalne.przebieg_DS_filtrowy = item.przebieg_DS_filtrowy;
                    ZmienneGlobalne.przebieg_SS_filtrowy = item.przebieg_SS_filtrowy;
                    ZmienneGlobalne.przebieg_CIL_filtrowy = item.przebieg_CIL_filtrowy;
                    ZmienneGlobalne.przebieg_CIL_papierosowy = item.przebieg_CIL_papierosowy;
                    ZmienneGlobalne.przebieg_Konserwacja_filtrowy = item.przebieg_Konserwacja_filtrowy;
                    ZmienneGlobalne.przebieg_Konserwacja_papierosowy = item.przebieg_Konserwacja_papierosowy;
                    ZmienneGlobalne.termin_KS_papierosowy = item.termin_KS_papierosowy;
                    ZmienneGlobalne.termin_DS_papierosowy = item.termin_DS_papierosowy;
                    ZmienneGlobalne.termin_SS_papierosowy = item.termin_SS_papierosowy;
                    ZmienneGlobalne.termin_KS_filtrowy = item.termin_KS_filtrowy;
                    ZmienneGlobalne.termin_DS_filtrowy = item.termin_DS_filtrowy;
                    ZmienneGlobalne.termin_SS_filtrowy = item.termin_SS_filtrowy;
                    ZmienneGlobalne.termin_CIL_papierosowy = item.termin_CIL_papierosowy;
                    ZmienneGlobalne.termin_CIL_filtrowy = item.termin_CIL_filtrowy;
                    ZmienneGlobalne.termin_Konserwacja_papierosowy = item.termin_Konserwacja_papierosowy;
                    ZmienneGlobalne.termin_Konserwacja_filtrowy = item.termin_Konserwacja_filtrowy;
                    ZmienneGlobalne.path_zapis_wymian_gum = item.path_zapis_wymian_gum;
                    ZmienneGlobalne.path_zapis_awarii = item.path_zapis_awarii;
                    ZmienneGlobalne.path_zapis_cila = item.path_zapis_cila;
                    ZmienneGlobalne.path_instukcje = item.path_instukcje;
                    ZmienneGlobalne.path_autodestruct = item.path_autodestruct;
                    ZmienneGlobalne.path_remoteDesktop = item.path_remoteDesktop;
                    ZmienneGlobalne.path_template_CIL = item.path_template_CIL;
                    ZmienneGlobalne.path_saveBackup = item.path_saveBackup;
                    ZmienneGlobalne.path_saveRaport8h = item.path_saveRaport8h;
                }
            }
            catch { }




        }
       
    }
}
