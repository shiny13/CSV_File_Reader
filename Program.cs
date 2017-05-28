using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ERM_TestTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var currentDir = Directory.GetCurrentDirectory();
            //Console.WriteLine("Current Path: " + currentDir);
            //Console.WriteLine("Second Path: " + AppDomain.CurrentDomain.BaseDirectory);
            string[] extractedpath = currentDir.Split('\\');
            string path = extractedpath.First();
            foreach (var e in extractedpath)
            {
                if (e == path)
                {
                    path += "\\";
                    continue;
                }

                if (e == "bin")
                {
                    //path += "\\";
                    break;
                }

                path += e + "\\";
            }
            var assetsPath = path + "Assets\\";
            //Console.WriteLine("Assets Path: " + assetsPath);
            var reader = new Reader();

            DirectoryInfo d = new DirectoryInfo(assetsPath);
            FileInfo[] Files = d.GetFiles("*.csv");
            foreach (FileInfo file in Files)
            {
                //Console.WriteLine("Reading: " + file.Name);
                try
                {
                    reader.ReadCsv(assetsPath, file.Name);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            Console.WriteLine("Total LP Values: " + reader.LpValues.Count());
            Console.WriteLine("Total TOU Values: " + +reader.TouValues.Count());
            Console.WriteLine("Press any key to exit ...");
            Console.ReadLine();
        }
    }
}
