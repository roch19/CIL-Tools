﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NavigationBar
{
    class ZmienneGlobalne
    {

        public static string path_to_Config_file = "";

        public static string numer_sodimatu = "";


        public static string path_to_data_file = "";

        //typ sodimatu 
        public static string rodzajSodimatu = "";

        //zapis danych Karta CIL
        public static string template = "";

        //Nazwa pliku SOdimData 

        //template aktualny 



        //aktualny przebieg
        public static int numCig = 0;
        public static int numCycle = 0;

        //info nt. max przebiegów dla CIL/Kons/Wymiany gum
        public static int przebieg_KS_papierosowy =0;
        public static int przebieg_DS_papierosowy;
        public static int przebieg_SS_papierosowy;
        public static int przebieg_KS_filtrowy;
        public static int przebieg_DS_filtrowy;
        public static int przebieg_SS_filtrowy;
        public static int przebieg_CIL_filtrowy;
        public static int przebieg_CIL_papierosowy;
        public static int przebieg_Konserwacja_filtrowy;
        public static int przebieg_Konserwacja_papierosowy;
        public static int termin_KS_papierosowy;
        public static int termin_DS_papierosowy;
        public static int termin_SS_papierosowy;
        public static int termin_KS_filtrowy;
        public static int termin_DS_filtrowy;
        public static int termin_SS_filtrowy;
        public static int termin_CIL_papierosowy;
        public static int termin_CIL_filtrowy;
        public static int termin_Konserwacja_papierosowy;
        public static int termin_Konserwacja_filtrowy;


        //info nt. ścierzek zapisu/odczytu etc
        public static string path_zapis_wymian_gum;
        public static string path_zapis_awarii;
        public static string path_zapis_cila;
        public static string path_instukcje;
        public static string path_autodestruct;
        public static string path_remoteDesktop;
        public static string path_template_CIL;
        public static string path_saveBackup;

        public static string path_zapis_Wpisow_Karty_E;
        public static string path_zapis_Wpisow_Karty_E_Bez_Roku_XML;

        public static string path_To_STAT_Folder;
        public static string path_saveRaport8h;
    }
}
