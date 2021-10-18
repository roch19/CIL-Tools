using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NavigationBar.KartaCIL
{
    class SodimModules
    {
        public string Modul { get; set; }
        public string Opis_czynnosci { get; set; }
        public int Interwal { get; set; }

        public SodimModules(string modul, string opis_czynnosci, int interwal)
        {
            Modul = modul;
            Opis_czynnosci = opis_czynnosci;
            Interwal = interwal;
        }

        public SodimModules()
        {
        }
    }


}
