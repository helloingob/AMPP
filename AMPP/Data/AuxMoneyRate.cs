using System;

namespace AMPP.Data
{
    public class AuxMoneyRate
    {
        public int Interests { get; set; }
        public int Acquittance { get; set; }
        public DateTime Date { get; set; }
        public int PayedAquittance { get; set; }
        public int PayedInterest { get; set; }
        public int PayedFee { get; set; }
        public int SumPayed { get; set; }
        public bool Future { get; set; }

        public override string ToString()
        {
            return
                $"{nameof(Interests)}: {Interests}, {nameof(Acquittance)}: {Acquittance}, {nameof(Date)}: {Date}, {nameof(PayedAquittance)}: {PayedAquittance}, {nameof(PayedInterest)}: {PayedInterest}, {nameof(PayedFee)}: {PayedFee}, {nameof(SumPayed)}: {SumPayed}, {nameof(Future)}: {Future}";
        }
    }
}