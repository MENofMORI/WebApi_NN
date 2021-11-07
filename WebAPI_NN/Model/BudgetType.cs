using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_NN.Model
{
    public class BudgetType
    {
        public int Id { get; set; }

        [StringLength(maximumLength: 30)]
        public string Type { get; set; }
        public List<Budget> Budgets { get; set; }
    }
}
