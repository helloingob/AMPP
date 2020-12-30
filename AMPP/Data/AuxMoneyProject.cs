using System;
using System.Collections.Generic;
using System.Linq;

namespace AMPP.Data
{
    public class AuxMoneyProject
    {
        private readonly List<AuxMoneyRate> _rates = new();
        public List<AuxMoneyUnscheduledRate> UnscheduledRates = new();
        public int Id { get; set; }

        public void AddRate(AuxMoneyRate auxMoneyRate)
        {
            _rates.Add(auxMoneyRate);
        }

        public string ToCsv()
        {
            var result = string.Empty;

            var currentRate = 0;
            foreach (var auxMoneyRate in _rates)
            {
                currentRate++;
                if (auxMoneyRate.PayedFee > 0)
                    result += FormatRow(auxMoneyRate.Date, auxMoneyRate.PayedFee, OutputSettings.Fee, Id.ToString()) +
                              " - (" + currentRate + "/" + _rates.Count + ")" +
                              "\n";
                if (auxMoneyRate.PayedInterest > 0)
                    result += FormatRow(auxMoneyRate.Date, auxMoneyRate.PayedInterest, OutputSettings.Interest, Id.ToString()) +
                              " - (" + currentRate + "/" + _rates.Count + ")" +
                              "\n";
            }

            foreach (var auxMoneyUnscheduledRate in UnscheduledRates)
                result += FormatRow(auxMoneyUnscheduledRate.Date, auxMoneyUnscheduledRate.Interest,
                    OutputSettings.Interest,
                    Id + " - " + auxMoneyUnscheduledRate.Type + "\n");

            return result;
        }

        private string FormatRow(DateTime dateTime, int value, string typ, string notiz)
        {
            return dateTime.ToString(OutputSettings.DateFormat) + ";" + value / 100 + OutputSettings.NumberSeparator +
                   value % 100 + ";EUR;" + typ + ";" + notiz;
        }
    }
}