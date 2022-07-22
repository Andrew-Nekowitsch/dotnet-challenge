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
				return new ReportingStructure() { Employee = await _employeeRepository.GetById(id) };
			}

			return null;
		}
	}
}
