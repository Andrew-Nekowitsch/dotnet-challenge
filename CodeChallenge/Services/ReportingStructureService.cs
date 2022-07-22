using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;

namespace CodeChallenge.Services
{
	public class ReportingStructureService : IReportingStructureService
	{
		private readonly IEmployeeRepository _employeeRepository;
		private readonly ILogger<EmployeeService> _logger;

		public ReportingStructureService(ILogger<EmployeeService> logger, IEmployeeRepository employeeRepository)
		{
			_employeeRepository = employeeRepository;
			_logger = logger;
		}

		public async Task<ReportingStructure> GetById(string id)
		{
			if (!String.IsNullOrEmpty(id))
			{
				var x = await _employeeRepository.GetById(id);
				var y = new ReportingStructure() { Employee = x };
				return y;
			}

			return null;
		}
	}
}
