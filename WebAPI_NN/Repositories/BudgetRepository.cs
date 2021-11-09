﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_NN.ArtificialNeuralNetwork;
using WebAPI_NN.Model;

namespace WebAPI_NN.Repositories
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly BudgetContext _context;
        public BudgetRepository(BudgetContext context) { _context = context; }

        public async Task<Budget> CreateBudget(Budget budget, int id)
        {
            BudgetType BudgetTypeToEdit = _context.BudgetTypes.Find(id);
            if (BudgetTypeToEdit != null)
            {
                //if (BudgetTypeToEdit.Budgets == null) BudgetTypeToEdit.Budgets = new List<Budget>();
                budget.Type = BudgetTypeToEdit;
                _context.Budgets.Add(budget);

                await _context.SaveChangesAsync();
                return budget;
            }

            return null;
        }

        public async Task DeleteBudget(int id)
        {
            var ListContentToDelete = await _context.Budgets.FindAsync(id);
            if (ListContentToDelete != null)
            {
                _context.Budgets.Remove(ListContentToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Budget> GetBudget(int id)
        {
            return await _context.Budgets.FindAsync(id);
        }

        public async Task<IEnumerable<Budget>> GetBudgets()
        {
            return await _context.Budgets
                .Include(i => i.Type)
                .ToListAsync();
        }

        public async Task<IEnumerable<Budget>> GetBudgetsWithSpecyficBudgetType(int idBudgetType)
        {
            BudgetType SelectedBudgetType = await _context.BudgetTypes.FindAsync(idBudgetType);
            var SelectedBudgets = await _context.Budgets.
                Include(e => e.Type).
                Where(e => e.Type == SelectedBudgetType).
                ToListAsync();

            return SelectedBudgets;
        }

        public async Task<IEnumerable<BudgetType>> GetBudgetTypes()
        {
            return await _context.BudgetTypes.ToListAsync();
        }

        public async Task<double> GetPrediction(int id, Budget newBudget)
        {
            Budget NewBudget = await CreateBudget(newBudget, id);

            BudgetType SelectedBudgetType = await _context.BudgetTypes.FindAsync(10);
            Budget Budget_Month_income = await _context.Budgets
                .Include(i => i.Type)
                .Where(w => w.Type == SelectedBudgetType)
                .OrderBy(o => o.AddedData)
                .FirstOrDefaultAsync();
            double IncomeBudget;
            if (Budget_Month_income != null)
            {
                IncomeBudget = Budget_Month_income.Amount;
                if (IncomeBudget == 0) IncomeBudget = 1;
            }
            else
            {
                IncomeBudget = 1;
            }

            SelectedBudgetType = await _context.BudgetTypes.FindAsync(11);
            Budget Budget_Budget = await _context.Budgets
                .Include(i => i.Type)
                .Where(w => w.Type == SelectedBudgetType)
                .OrderBy(o => o.AddedData)
                .FirstOrDefaultAsync();
            double TotalBudget;
            if (Budget_Budget != null)
            {
                TotalBudget = Budget_Budget.Amount;
            }
            else
            {
                TotalBudget = 0;
            }

            List<double> InpuData = new List<double>(9);
            int i = 0;
            while(true)
            {
                InpuData[i] = 0;
                List<Budget> f = (List<Budget>) await GetBudgetsWithSpecyficBudgetType(i+1);
                foreach (Budget budget in f)
                {
                    if (budget.AddedData < Budget_Month_income.AddedData) continue;
                    InpuData[i] += budget.Amount;
                }
                InpuData[i] /= IncomeBudget;
                if (++i >= InpuData.Count) break;
            }

            InpuData.Add(IncomeBudget / 10000D);
            InpuData.Add(TotalBudget / 200000D);

            await DeleteBudget(NewBudget.Id);

            ANN_Builder ANNBuilder = new ANN_Builder();
            NeuralNetworkEngin ANN = ANNBuilder.GetANN();

            List<double> ListAnsware = ANN.WyliczOdpowiedz(InpuData);
            return ListAnsware[0];
        }
    }
}
