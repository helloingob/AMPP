using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AMPP.Data.Objects;

namespace AMPP.Data
{
    public static class CsvHandler
    {
        public static void Export(List<AuxMoneyProject> auxMoneyProjects)
        {
            var currentDate = DateTime.Now.ToString("ddMMyyyyHHmmss");
            var csvOutput = OutputSettings.CsvHeader;
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