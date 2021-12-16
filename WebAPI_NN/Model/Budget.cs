using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_NN.Model
{
    public class Budget
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public DateTime AddedData { get; set; }
        public BudgetType Type { get; set; }
    }
}
