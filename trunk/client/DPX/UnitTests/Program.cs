using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using QuickReader;
using DPXDatabase;

namespace UnitTests
{
    class Program
    {
        //public static OleDbConnection aConnection;

        static void Main(string[] args)
        {
            Console.WriteLine("DyKnow Panel eXtractor Unit Tests");

            //String dbfile = "C:\\Users\\Jared\\Desktop\\testdatabase.accdb";
            String dyknowfiled = "C:\\Users\\Jared\\Desktop\\samplefile.dyz";

            //Database db = new Database(dbfile);
            DyKnowReader dr = new DyKnowReader(dyknowfiled);

            List<ImageData> id = dr.ImageInformation;
            for (int i = 0; i < id.Count; i++)
            {
                Console.WriteLine(id[i].ToString());
            }

            /*
            for (int i = 0; i < dr.NumOfPages(); i++)
            {
                DyKnowPage dp = dr.GetDyKnowPage(i);
                Console.WriteLine("Panel " + i.ToString());
                List<DyKnowImage> imgs = dp.Images;
                for (int k = 0; k < imgs.Count; k++)
                {
                    Console.Write(imgs[k].ToString());
                }
            }
            */

            /*
            List<Classdate> cd = db.GetClassdates();
            for (int i = 0; i < cd.Count; i++)
            {
                Console.WriteLine(cd[i].ClassDate.Date);
            }

            List<Reason> r = db.GetReasons();
            for (int i = 0; i < r.Count; i++)
            {
                Console.WriteLine(r[i].ToString());
            }
            
             */
            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }

        public static void sortfile(String inputfile, String outputfile)
        {
            PanelSorter ps = new PanelSorter(inputfile, outputfile);
            ps.ProcessSort();
        }


    }
}
