using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                SaveCSV(auxMoneyProjects);
            }
            else
            {
                Console.WriteLine("Could not log in. Exit!");
            }

            Console.WriteLine("===================================================");
            Console.WriteLine("Finished.");
        }

        private static void SaveCSV(List<AuxMoneyProject> auxMoneyProjects)
        {
            var currentDate = DateTime.Now.ToString("ddMMyyyyHHmmss");
            var csvOutput = "Datum;Wert;Buchungswährung;Typ;Notiz\n";
            foreach (var csvContent in auxMoneyProjects.Select(auxMoneyProject => auxMoneyProject.ToCsv()))
                if (!string.IsNullOrEmpty(csvContent))
                {
                    csvOutput += csvContent;
                    Console.WriteLine(csvContent);
                }

            Console.WriteLine("---------------------------------------------------");
            var filename = "ampp_" + currentDate + ".txt";
            File.WriteAllText("ampp_" + currentDate + ".csv", csvOutput);
            Console.WriteLine("Exported to: " + filename);
        }
    }
}