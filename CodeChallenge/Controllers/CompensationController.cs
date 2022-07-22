
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Services;
using CodeChallenge.Models;

namespace CodeChallenge.Controllers
{
	[ApiController]
	[Route("api/compensation")]
	public class CompensationController : ControllerBase
	{
		private readonly ILogger _logger;
		private readonly ICompensationService _compensationService;
		private readonly IEmployeeService _employeeService;

		public CompensationController(ILogger<CompensationController> logger, ICompensationService compensationService, IEmployeeService employeeService)
		{
			_logger = logger;
			_compensationService = compensationService;
			_employeeService = employeeService;
		}

		[HttpPost]
		public IActionResult CreateCompensation([FromBody] Compensation compensation)
		{
			_logger.LogDebug($"Received Compensation create request for '{compensation.EmployeeId} {compensation.Salary}'");

			if (_employeeService.GetById(compensation.EmployeeId) == null) 
				return NotFound();

			_compensationService.Create(compensation);

			return CreatedAtRoute("getCompensationById", new { id = compensation.EmployeeId }, compensation);
		}

		[HttpGet("{id}", Name = "getCompensationById")]
		public async Task<IActionResult> GetCompensationById(String id)
		{
			_logger.LogDebug($"Received Compensation get request for '{id}'");

			var Compensations = await _compensationService.GetById(id);

			if (Compensations == null || Compensations.Count <= 0)
				return NotFound();

			return Ok(Compensations);
		}
	}
}
