using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NavigationBar
{
    class ZmienneGlobalne
    {
        public static string numer_sodimatu = "";


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
    }
}
