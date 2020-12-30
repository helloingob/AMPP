using System.Collections.Generic;
using System.Linq;
using AMPP.Data.Objects;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AMPP.Data
{
    public class Parser
    {
        public static AuxMoneyProject ExtractTransactions(string content)
        {
            var jObject = JObject.Parse(ExtractJson(content));

            var creditNumber = (int) jObject["creditNumber"];
            var duration = (int) jObject["duration"];

            var auxMoneyProject = new AuxMoneyProject {Id = creditNumber};

            for (var i = 1; i <= duration; i++)
            {
                var rate = jObject["schedule"][i.ToString()];
                if (rate == null) continue;
                var auxMoneyRate = ((JObject) rate).ToObject<AuxMoneyRate>();
                auxMoneyProject.AddRate(auxMoneyRate);
            }

            auxMoneyProject.UnscheduledRates = GetUnscheduledRates((JArray) jObject["unscheduled"]);

            return auxMoneyProject;
        }

        private static List<AuxMoneyUnscheduledRate> GetUnscheduledRates(JToken unscheduledRates)
        {
            var jsonSerializer = new JsonSerializer();
            jsonSerializer.Converters.Add(new DateConverter());
            return unscheduledRates.ToObject<List<AuxMoneyUnscheduledRate>>(jsonSerializer);
        }

        private static string ExtractJson(string content)
        {
            const string dataIdentifier = "var creditData = ";

            var result = content.Replace("\r\n", string.Empty).Split(";").ToList();

            foreach (var line in result.Where(line => line.Contains(dataIdentifier)))
                return line.Replace(dataIdentifier, string.Empty).Trim();

            return string.Empty;
        }

        public static List<string> ExtractDetailUrls(string content)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(content);

            var foundDetailUrls = new List<string>();
            foreach (var node in htmlDocument.DocumentNode.SelectNodes("//a[@class='details']"))
                foundDetailUrls.Add(node.GetAttributeValue("href", string.Empty));

            return foundDetailUrls;
        }
    }
}