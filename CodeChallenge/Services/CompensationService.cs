using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;

namespace CodeChallenge.Services
{
	public class CompensationService : ICompensationService
	{
		private readonly ICompensationRepository _compensationRepository;
		private readonly ILogger<CompensationService> _logger;

		public CompensationService(ILogger<CompensationService> logger, ICompensationRepository CompensationRepository)
		{
			_compensationRepository = CompensationRepository;
			_logger = logger;
		}

		public Compensation Create(Compensation Compensation)
		{
			_logger.LogDebug($"Received Compensation create request for '{Compensation.EmployeeId}'");

			if (Compensation != null)
			{
				_compensationRepository.Add(Compensation);
				_compensationRepository.SaveAsync().Wait();
			}

			return Compensation;
		}

		public async Task<List<Compensation>> GetById(string id)
		{
			_logger.LogDebug($"Received Compensation Get request for '{id}'");

			if (!String.IsNullOrEmpty(id))
			{
				return await _compensationRepository.GetById(id);
			}

			return null;
		}
	}
}
