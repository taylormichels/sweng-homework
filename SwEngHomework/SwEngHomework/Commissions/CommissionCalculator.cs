using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace SwEngHomework.Commissions
{
    public class CommissionCalculator : ICommissionCalculator
    {
        public IDictionary<string, double> CalculateCommissionsByAdvisor(string jsonInput)
        {
            var result = new Dictionary<string, double>();          
            var input = JsonConvert.DeserializeObject<CommissionData>(jsonInput);
            foreach (var advisor in input.Advisors)
            {
                var allCommmissionByAdvisor = input.Accounts.Where(a => a.Advisor == advisor.Name);
                decimal total = allCommmissionByAdvisor.Sum(a => a.PresentValue);
                decimal accountFee = 0;
                decimal commission = 0;

                if (total < 50000)
                    accountFee = total * .0005m;
                else if (total >= 50000 && total < 100000)
                    accountFee = total * .0006m;
                else
                    accountFee = total * .0007m;

                if (advisor.Level == "Junior")
                    commission = accountFee * .25m;
                else if (advisor.Level == "Experienced")
                    commission = accountFee * .5m;
                else
                    commission = accountFee;

                result.Add(advisor.Name, Math.Round((double)commission, 2));

            }

            return result;
        }

        [Serializable]
        public class Advisors
        {
            public string Name { get; set; }
            public string Level { get; set; }
        }

        [Serializable]
        public class Accounts
        {
            public string Advisor { get; set; }
            public decimal PresentValue { get; set; }
        }
        
        [Serializable]
        public class CommissionData
        {
            public List<Advisors> Advisors { get; set; }
            public List<Accounts> Accounts { get; set; }
        }
    }
}
