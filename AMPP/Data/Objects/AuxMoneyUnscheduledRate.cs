using System;

namespace AMPP.Data.Objects
{
    public class AuxMoneyUnscheduledRate
    {
        public int Interest { get; set; }
        public int Acquittance { get; set; }
        public DateTime Date { get; set; }
        public int PayedFee { get; set; }
        public int SumPayed { get; set; }
        public string Type { get; set; }

        public override string ToString()
        {
            return
                $"{nameof(Interest)}: {Interest}, {nameof(Acquittance)}: {Acquittance}, {nameof(Date)}: {Date}, {nameof(PayedFee)}: {PayedFee}, {nameof(SumPayed)}: {SumPayed}, {nameof(Type)}: {Type}";
        }
    }
}