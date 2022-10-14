using PricebookDigitalAnalysis.Models;
using System.Collections.Generic;
using System.Linq;

namespace PricebookDigitalAnalysis
{
    public class Dealer
    {
        private List<DealerModelLines> _modelCounts;

        public string DealerGUID { get; set; }
        public string DealerName { get; set; }
        public List<DealerPart> Models { get; set; }
        public List<DealerModelLines> ModelCounts
        {
            get
            {
                return _modelCounts ??= Models
                    .GroupBy(x => x.ModelNumber)
                    .Select(x => new DealerModelLines
                    {
                        ModelNumber = x.Key,
                        LineCount = x.Count()
                    }).ToList();
            }
            set => _modelCounts = value;
        }
    }
}