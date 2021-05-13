using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Policy;

namespace NavigationBar
{
    class WymianaGumek
    {
        string locationTxtWithLocationOfSavePAth = @"C:\copy_sodim\PATH_TO_SAVE_CIGNUM.txt";
        string pathCopySodim = @"C:\copy_sodim\copy_sodim.vbs";
        string strSodimFolder = "";
        string sodimat_name = "";
        string savePath = "";
        List<string> lines = null;

        internal bool wpisWymiany()
        {

            lines = File.ReadAllLines(locationTxtWithLocationOfSavePAth).ToList();
            savePath += lines[0];
        //    Console.WriteLine(savePath);
         //   Console.WriteLine(lines[0]);
            lines = File.ReadAllLines(pathCopySodim).ToList();
            //Const Sodimat_name 
       //     Console.WriteLine(lines[1]);
            //Const strSodimFolder
        //    Console.WriteLine(lines[2]);

            string tmp = lines[1];
            bool flag = true;

            //extract sodimat name
            for (int i = 0; i < tmp.Length; i++)
            {
                if (tmp[i].ToString().Equals("\"") && flag)
                {
                    flag = false;
                }
                else if (!tmp[i].ToString().Equals("\"") && flag.Equals(false))
                {
                    sodimat_name += tmp[i];
                }

            }
           // Console.WriteLine("Nazwa sodimatu: " + sodimat_name);
            savePath += sodimat_name + ".txt";
            tmp = lines[2];
            flag = true;
            //extract start sodim folder
            for (int i = 0; i < tmp.Length; i++)
            {
                if (tmp[i].ToString().Equals("\"") && flag)
                {
                    flag = false;
                }
                else if (!tmp[i].ToString().Equals("\"") && flag.Equals(false))
                {
                    strSodimFolder += tmp[i];
                }

            }
            //Console.WriteLine("Ścierzka startowa Sodimatu: "+strSodimFolder);

            //get numcycle from history
            string numcigPath = strSodimFolder + "HISTORY\\NUMCIG.TXT";

            lines.Clear();
            lines = File.ReadAllLines(numcigPath).ToList();
            // Console.WriteLine("Numcigaret:");
            // Console.WriteLine(lines[0]);
            // Console.WriteLine(lines[1]);
            string abc = "";
            string tmp1 = lines[0];
            int k = 0;

            while (Char.IsWhiteSpace(tmp1[k]))
            {
                k++;
            }

            while (!Char.IsWhiteSpace(tmp1[k]))
            {
                abc += tmp1[k];
                k++;
            }




            string numcyclePath = strSodimFolder + "HISTORY\\NUMCYCLE.TXT";
            lines.Clear();
            lines = File.ReadAllLines(numcyclePath).ToList();
            // Console.WriteLine("Numcycle");
            // Console.WriteLine(lines[0]);
            // Console.WriteLine(lines[1]);

            tmp1 = lines[0];
            k = 0;
            abc += ";";


            while (Char.IsWhiteSpace(tmp1[k]))
            {
                k++;
            }

            while (!Char.IsWhiteSpace(tmp1[k]))
            {
                abc += tmp1[k];
                k++;
            }

            //Console.WriteLine("ilość cykli: "+abc);


            abc += ";";
                abc += DateTime.Now.ToString();




                StreamWriter writer = new StreamWriter(savePath, true);

                writer.WriteLine(abc);
                writer.Dispose();
            return true;

            
        }
            
        
            
    }
}
