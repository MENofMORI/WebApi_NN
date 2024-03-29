﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_NN.Model;

namespace WebAPI_NN.Repositories
{
    public interface IBudgetRepository
    {
        Task<IEnumerable<Budget>> GetBudgets();
        Task<Budget> GetBudget(int id);
        Task<IEnumerable<Budget>> GetBudgetsWithSpecyficBudgetType(int idBudgetType);
        Task<Budget> CreateBudget(Budget budget, int id);
        Task DeleteBudget(int id);
        Task<IEnumerable<BudgetType>> GetBudgetTypes();
        Task<double> GetLastSebastian();
        
        Task<double> GetAvailableForALastIncomeBudget();
        Task<List<double>> GetListOfAvailableBudgetByTypeForALastIncomeBudget();
        Task<double> GetPrediction(int id, Budget budget);
    }
}
