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
        public static OleDbConnection aConnection;
        static void Main(string[] args)
        {
            Console.WriteLine("DyKnow Panel eXtractor Unit Tests");

            //String dbfile = "C:\\Users\\Jared\\Desktop\\testdatabase.accdb";
            String dyknowfiled = "C:\\Users\\Jared\\Desktop\\samplefile.dyz";

            //Database db = new Database(dbfile);
            DyKnowReader dr = new DyKnowReader(dyknowfiled);

            for (int i = 0; i < dr.NumOfPages(); i++)
            {
                if (i == 6)
                {
                    DyKnowPage dp = dr.getDyKnowPage(i);
                    List<DyKnowPenStroke> dps = dp.Pens;
                    for (int k = 0; k < dps.Count; k++)
                    {
                        Console.Write(dps[k].PH + " - " + dps[k].PW + " / ");
                    }
                }
            }


            /*
            List<Classdate> cd = db.getClassdates();
            for (int i = 0; i < cd.Count; i++)
            {
                Console.WriteLine(cd[i].ClassDate.Date);
            }

            List<Reason> r = db.getReasons();
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
            ps.processSort();
        }


    }
}
