using System;
using System.Linq;

namespace SwEngHomework.DescriptiveStatistics
{
    public class StatsCalculator : IStatsCalculator
    {
        public Stats Calculate(string semicolonDelimitedContributions)
        {
            string[] contributions = semicolonDelimitedContributions.Split(';');
            if (contributions.Length == 0)
                return new Stats { Average = 0, Median = 0, Range = 0 };

            decimal[] values = new decimal[contributions.Length];
            Stats result = new Stats();

            for (int i = 0; i < contributions.Length; i++)
            {
                decimal value = 0m;
                if (Decimal.TryParse(contributions[i].Replace("$", "").Trim(), out value))
                    values[i] = value;
            }

            values = values.Where(val => val != 0).ToArray();
            if (values.Length == 0)
                return new Stats { Average = 0, Median = 0, Range = 0 };

            Array.Sort(values);

            result.Average = (double) Math.Round(values.Sum() / values.Length, 2);
            result.Range = (double) Math.Round(values.Max() - values.Min(), 2);

            if (values.Length % 2 == 0)
            {
                var middleHigh = (int) values.Length / 2;
                var middleLow = middleHigh - 1;
                result.Median = (double) Math.Round((values[middleLow] + values[middleHigh]) / 2, 2);
            }
            else
                result.Median = (double) Math.Round(values[values.Length / 2], 2);

            return result;
        }
    }
}
