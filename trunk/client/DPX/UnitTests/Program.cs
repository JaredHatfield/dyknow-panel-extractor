using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using QuickReader;

namespace UnitTests
{
    class Program
    {
        public static OleDbConnection aConnection;
        static void Main(string[] args)
        {
            Console.WriteLine("DyKnow Panel eXtractor Unit Tests");

            //databaseTest();

            readdyknowfile();


            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }

        public static void sortfile(String inputfile, String outputfile)
        {
            PanelSorter ps = new PanelSorter(inputfile, outputfile);
            ps.processSort();
        }

        public static void readdyknowfile()
        {
            //Get the DyKnow file ready
            Console.Write("File Name: ");
            String file = Console.ReadLine();
            DyKnowReader dr = new DyKnowReader(file);

            for (int i = 0; i < dr.NumOfPages(); i++)
            {
                Console.WriteLine(dr.GetPageString(i));
            }

            Console.WriteLine();
            Console.WriteLine(dr.ToString());
            Console.WriteLine("Minimum: " + dr.MinStrokeCount);
            Console.WriteLine("Maximum: " + dr.MaxStrokeCount);
        }

        public static void databaseTest()
        {

            //Get the database ready
            Console.Write("Database File: ");
            String db = Console.ReadLine();
            aConnection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + db + ";Persist Security Info=False");

            //Get the DyKnow file ready
            Console.Write("File Name: ");
            String file = Console.ReadLine();
            DyKnowReader dr = new DyKnowReader(file);

            aConnection.Open();
            for (int i = 0; i < dr.NumOfPages(); i++)
            {
                Console.WriteLine(dr.GetPageString(i));
                InsertNewRecord(file, dr.getDyKnowPage(i));
            }
            aConnection.Close();
        }


        public static bool InsertNewRecord(String file, DyKnowPage page)
        {
            OleDbCommand cmdInsert = new OleDbCommand();
            string query = "INSERT INTO panels(file, panelnumber, username, fullname, strokes, complete) VALUES(@parm1, @parm2, @parm3, @parm4, @parm5, @parm6)";
            int status;

            cmdInsert.Parameters.Clear();
            try
            {
                //Give it the query
                cmdInsert.CommandText = query;
                //Type of query
                cmdInsert.CommandType = System.Data.CommandType.Text;
                //Add parameters to the query
                cmdInsert.Parameters.AddWithValue("@parm1", file);
                cmdInsert.Parameters.AddWithValue("@parm2", page.PageNumber);
                cmdInsert.Parameters.AddWithValue("@parm3", page.UserName);
                cmdInsert.Parameters.AddWithValue("@parm4", page.FullName);
                cmdInsert.Parameters.AddWithValue("@parm5", page.StrokeCount);
                cmdInsert.Parameters.AddWithValue("@parm6", page.Finished);
                
                //Specify connection
                cmdInsert.Connection = aConnection;

                // 0 = failed, 1 = success
                status = cmdInsert.ExecuteNonQuery();
                if (!(status == 0))
                {
                    //It Failed
                    return false;
                }
                else
                {
                    //It Worked!
                    return true;
                }
            }
            catch (Exception ex)
            {
                //Display the error
                Console.WriteLine(ex.Message, "Error");
            }
            finally
            {
                //All done
            }
            return true;
        }


    }
}
