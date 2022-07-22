using MovieLibrary.Business.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business.Helpers
{
    public static class RateHelper
    {
        public static decimal AvgRate(ICollection<RateViewModel> rates)
        {
            decimal avgRate = 0;
            if (rates.Count > 0)
            {
                foreach (var item in rates)
                {
                    avgRate += item.Rates;
                }

                avgRate = avgRate / rates.Count;
            }

            decimal rez = (decimal)Math.Round(avgRate, 2);
            return rez;
        }
    }
}
