using System;

namespace PricebookDigitalAnalysis.Models
{
    public class DealerPart
    {
        public string ModelNumber { get; set; }
        public decimal Cost { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int PartCount { get; set; }
    }
}