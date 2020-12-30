using System;
using AMPP.Data;

namespace AMPP
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var auxMoneyManager = new AuxMoneyManager(args[0], args[1]);
            if (auxMoneyManager.IsLoggedIn)
            {
                Console.WriteLine("Successfully logged in.");
                var auxMoneyProjects = auxMoneyManager.Get();
                Console.WriteLine("---------------------------------------------------");
                CsvHandler.Export(auxMoneyProjects);
            }
            else
            {
                Console.WriteLine("Could not log in. Exit!");
            }

            Console.WriteLine("===================================================");
            Console.WriteLine("Finished.");
        }
    }
}