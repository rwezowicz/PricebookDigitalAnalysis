using PricebookDigitalAnalysis.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PricebookDigitalAnalysis.Services
{
    public class DealerService
    {
        public List<Dealer> BuildDealers(List<string[]> fileLines)
        {
            var dealers = new List<Dealer>();
            
            foreach (var lineValues in fileLines)
            {
                if (lineValues[0] == "Dealer GUID") continue;

                var dealer = dealers.Find(x => x.DealerGUID == lineValues[0] && x.DealerName == lineValues[1]);
                if (dealer == null)
                {
                    dealer = new Dealer
                    {
                        DealerGUID = lineValues[0],
                        DealerName = lineValues[1],
                        Models = new List<DealerPart>()
                    };

                    dealers.Add(dealer);
                }

                dealer.Models.Add(
                    new DealerPart()
                    {
                        ModelNumber = lineValues[2],
                        Cost = Convert.ToDecimal(lineValues[3]),
                        UpdatedDate = Convert.ToDateTime(lineValues[4])
                    }
                );
            }

            return dealers;
        }

        public void CalculateModelCounts(List<Dealer> dealers)
        {
            foreach (var dealer in dealers)
            {
                dealer.ModelCounts = dealer.Models
                    .GroupBy(x => x.ModelNumber)
                    .Select(x => new DealerModelLines
                    {
                        ModelNumber = x.Key,
                        LineCount = x.Count()
                    }).ToList();
            }
        }
    }
}
