using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CodeChallenge.Data;

namespace CodeChallenge.Repositories
{
	public class CompensationRespository : ICompensationRepository
	{
		private readonly CompensationContext _compensationContext;
		private readonly ILogger<ICompensationRepository> _logger;

		public CompensationRespository(ILogger<ICompensationRepository> logger, CompensationContext CompensationContext)
		{
			_compensationContext = CompensationContext;
			_logger = logger;
		}

		public Compensation Add(Compensation compensation)
		{
			compensation.CompensationId = Guid.NewGuid().ToString();
			if (compensation.EffectiveDate == DateTime.MinValue)
			{
				compensation.EffectiveDate = DateTime.Now;
			}
			_compensationContext.Compensations.Add(compensation);
			return compensation;
		}

		public async Task<List<Compensation>> GetById(string id)
		{
			IEnumerable<Compensation> result = await _compensationContext.Compensations.ToListAsync<Compensation>();
			return result.Where(e => e.EmployeeId == id).ToList();
		}

		public Task SaveAsync()
		{
			return _compensationContext.SaveChangesAsync();
		}

		public Compensation Remove(Compensation Compensation)
		{
			return _compensationContext.Remove(Compensation).Entity;
		}
	}
}
