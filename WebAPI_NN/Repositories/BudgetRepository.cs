using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_NN.Model;

namespace WebAPI_NN.Repositories
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly CombinedContext _context;
        public BudgetRepository(CombinedContext context) { _context = context; }

        public async Task<Budget> CreateBudget(Budget budget, int id)
        {
            var BudgetTypeToEdit = _context.BudgetTypes.Find(id);
            if (BudgetTypeToEdit != null)
            {
                if (BudgetTypeToEdit.Budgets == null) BudgetTypeToEdit.Budgets = new List<Budget>();
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
            return await _context.Budgets.ToListAsync();
        }

        public async Task<IEnumerable<Budget>> GetBudgetsWithSpecyficBudgetType(int idBudgetType)
        {
            BudgetType SelectedBudgetType = await _context.BudgetTypes.FindAsync(idBudgetType);
            var SelectedBudgets = await _context.Budgets.Include(e => e.Type).Where(e => e.Type == SelectedBudgetType).ToListAsync();

            return SelectedBudgets;
        }

        public async Task<IEnumerable<BudgetType>> GetBudgetTypes()
        {
            return await _context.BudgetTypes.ToListAsync();
        }
    }
}
