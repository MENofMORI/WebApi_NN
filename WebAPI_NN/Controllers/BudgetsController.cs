using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_NN.Model;
using WebAPI_NN.Repositories;

namespace WebApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class BudgetsController : Controller
    {
        private readonly IBudgetRepository _repository;

        public BudgetsController(IBudgetRepository repository)
        {
            _repository = repository;
        }

        // GET: api/budget
        [HttpGet("budget")]
        public async Task<IEnumerable<Budget>> GetBudgets()
        {
            return await _repository.GetBudgets();
        }

        // GET: api/budget/{id}
        [HttpGet("budget/{id}")]
        public async Task<Budget> GetBudget(int id)
        {
            return await _repository.GetBudget(id);
        }

        // GET: api/budget/contents/{id}
        [HttpGet("budget/type/{id}")]
        public async Task<IEnumerable<Budget>> GetSpecyficBudgets(int id)
        {
            return await _repository.GetBudgetsWithSpecyficBudgetType(id);
        }

        // POST: api/budget/add/{id}
        [HttpPost("budget/add/{id}")]
        public async Task<IActionResult> PostBudget(int id, Budget budget)
        {
            await _repository.CreateBudget(budget, id);

            return CreatedAtAction("GetBudget", new { id = budget.Id }, budget);
        }

        // DELETE: api/budget/{id}
        [HttpDelete("budget/{id}")]
        public async Task<IActionResult> DeleteBudget(int id)
        {
            var budget = await _repository.GetBudget(id);
            if (budget == null) return NotFound();

            await _repository.DeleteBudget(id);
            return NoContent();
        }

        // GET: api/budgetType
        [HttpGet("budgetType")]
        public async Task<IEnumerable<BudgetType>> GetBudgetTypes()
        {
            return await _repository.GetBudgetTypes();
        }

        // GET: api/prediction
        [HttpGet("prediction")]
        public async Task<double> GetPrediction(int id, Budget budget)
        {
            return await _repository.GetPrediction(id, budget);
        }
    }
}
