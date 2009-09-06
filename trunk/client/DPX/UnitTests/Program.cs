using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickReader;

namespace UnitTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("DyKnow Panel eXtractor Unit Tests");
            Console.Write("File Name: ");
            String file = Console.ReadLine();
            DyKnowReader dr = new DyKnowReader(file);

            for (int i = 0; i < dr.NumOfPages(); i++)
            {
                Console.WriteLine(dr.GetPageString(i));
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}
