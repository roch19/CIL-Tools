using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Policy;
namespace NavigationBar
{
    public class AwarieKalibracje
    {

        string sourceDirectoryErrorLog = "ERRLOG.ARCH\\";
        string sourceDirectoryCalibrationLog = "HISTORY.ARCH\\";

        string pathCopySodim = @"C:\copy_sodim\copy_sodim.vbs";
        string strSodimFolder = "";
        string sodimat_name = "";
        string numcigPath = "";
        string savePath = "";
        List<string> lines;
        List<string> filesList;
        List<string> listaInformacji = new List<string>();
        List<string> calibs = new List<string>();

        internal List<string> ChceckIfWasAnyCalibrationToday()
        {

            getPaths();

            ///////// Lista Kalibracji
            listaInformacji.Clear();
       
            
            //Lista plików znajdujących się w folderze o podanej ścierzce 
            filesList = (Directory.EnumerateFiles(strSodimFolder + sourceDirectoryCalibrationLog).ToList());
            FileInfo fi;
            filesList.Reverse();

            var now = DateTime.Now;
            var yesterday = now.AddDays(-1);
            var durationUntilMidnight = yesterday.Date - now;
   
            foreach (string currentFile in filesList)
            {
                fi = new FileInfo(currentFile);
                if (fi.LastAccessTime.Date == DateTime.Now.Date) //get files where create TIme is less than 14 days
                {
                    calibs.Add(fi.Name.ToString());
                   // return true;
                }
            }
            // return false;
            GC.Collect();
            return calibs;


        }

        internal List<string> ListaKalibracji()
        {

            relaseTheMemory();
            //lines = File.ReadAllLines(pathCopySodim).ToList();

            //string tmp = lines[1];
            //bool flag = true;

            ////extract sodimat name
            //for (int i = 0; i < tmp.Length; i++)
            //{
            //    if (tmp[i].ToString().Equals("\"") && flag)
            //    {
            //        flag = false;
            //    }
            //    else if (!tmp[i].ToString().Equals("\"") && flag.Equals(false))
            //    {
            //        sodimat_name += tmp[i];
            //    }

            //}
            //Console.WriteLine("Nazwa sodimatu: " + sodimat_name);
            //savePath += sodimat_name + ".txt";
            //tmp = lines[2];
            //flag = true;

            ////extract start sodim folder
            //for (int i = 0; i < tmp.Length; i++)
            //{
            //    if (tmp[i].ToString().Equals("\"") && flag)
            //    {
            //        flag = false;
            //    }
            //    else if (!tmp[i].ToString().Equals("\"") && flag.Equals(false))
            //    {
            //        strSodimFolder += tmp[i];
            //    }

            //}

            ////get numcycle from history
            //string numcigPath = strSodimFolder + "HISTORY\\NUMCIG.TXT";



            //get multiple errors from ERRORLOG.ARCH 

            //lines.Clear();

            getPaths();

          ///////// Lista Kalibracji
            listaInformacji.Clear();
            // GET Error list 
            //Lista plików znajdujących się w folderze o podanej ścierzce 
            filesList = (Directory.EnumerateFiles(strSodimFolder + sourceDirectoryCalibrationLog).ToList());
            FileInfo fi;
            filesList.Reverse();
            foreach (string currentFile in filesList)
            {
                fi = new FileInfo(currentFile);
                if (fi.LastAccessTime > DateTime.Now.AddDays(-12)) //get files where create TIme is less than 14 days
                {
                    lines = File.ReadAllLines(currentFile).ToList();

                    foreach (var VARIABLE in lines)
                    {
                        listaInformacji.Add(VARIABLE);
                    }
                }
            }
            if (listaInformacji.Count < 1)
            {
                listaInformacji.Add("Brak kalibracji do wyświetlenia.");
            }
            relaseTheMemory();
            return listaInformacji;
            //return listaInformacji = listaInformacji.OrderBy(q => q.Substring(0, 10)).ToList();

        }

        //internal string getCalibPath()
        //{
        //    getPaths();

        //    return strSodimFolder;
        //}

        internal string getPaths()
        {
            relaseTheMemory();
            strSodimFolder = "";
            try
            {
                lines = File.ReadAllLines(pathCopySodim).ToList();
            }
            catch
            {
                return "AC";
            }
            

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
            Console.WriteLine("Nazwa sodimatu: " + sodimat_name);
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
            relaseTheMemory();
            //get numcycle from history
            return strSodimFolder + "HISTORY.ARCH\\";
        }


        internal List<string> ListaAwarii()
        {
            relaseTheMemory();
            // lines = File.readalllines(pathcopysodim).tolist();
            //string tmp = lines[1];
            // bool flag = true;

            //extract sodimat name
            //for (int i = 0; i < tmp.length; i++)
            //{
            //    if (tmp[i].tostring().equals("\"") && flag)
            //    {
            //        flag = false;
            //    }
            //    else if (!tmp[i].tostring().equals("\"") && flag.equals(false))
            //    {
            //        sodimat_name += tmp[i];
            //    }

            //}
            //   console.writeline("nazwa sodimatu: " + sodimat_name);
            //savepath += sodimat_name + ".txt";
            //    tmp = lines[2];
            //   flag = true;


            //extract start sodim folder
            //for (int i = 0; i < tmp.length; i++)
            //{
            //    if (tmp[i].tostring().equals("\"") && flag)
            //    {
            //        flag = false;
            //    }
            //    else if (!tmp[i].tostring().equals("\"") && flag.equals(false))
            //    {
            //        strsodimfolder += tmp[i];
            //    }

            //}

            ////get numcycle from history
            //string numcigpath = strsodimfolder + "history\\numcig.txt";

            //get multiple errors from errorlog.arch 

            getPaths();

            // GET Calibration list 
            //Lista plików znajdujących się w folderze o podanej ścierzce 
            filesList = (Directory.EnumerateFiles(strSodimFolder + sourceDirectoryCalibrationLog).ToList());
            FileInfo fi;
           




            Console.WriteLine("-----------------------!!!!! AWARIE !!!!----------------------------");

            listaInformacji.Clear();
            // GET Error list 
            //Lista plików znajdujących się w folderze o podanej ścierzce 
            filesList = (Directory.EnumerateFiles(strSodimFolder + sourceDirectoryErrorLog).ToList());

            foreach (string currentFile in filesList)
            {
                fi = new FileInfo(currentFile);
           
                    if (fi.LastWriteTimeUtc > DateTime.Now.AddHours(-12)) //get files where create TIme is less than 24h
                  {
                    lines = File.ReadAllLines(currentFile).ToList();

                    foreach (var VARIABLE in lines)
                    {
                        listaInformacji.Add(VARIABLE);
                    }
                }  
             }
              if(listaInformacji.Count<1)
                 {

                       listaInformacji.Add("Brak awarii do wyświetlenia.");          
                 }
              relaseTheMemory();
              return listaInformacji; 


        }

        public void relaseTheMemory()//the Cracen
        {
          lines=null;
          filesList= null; ;
         // listaInformacji= null;
        }

    }
}

