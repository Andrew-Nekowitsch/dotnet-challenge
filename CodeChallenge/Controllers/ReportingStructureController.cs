using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Services;

namespace CodeChallenge.Controllers
{
	[Route("api/reportingStructure")]
	[ApiController]
	public class ReportingStructureController : ControllerBase
	{
		private readonly ILogger _logger;
		private readonly IReportingStructureService _reportingStructureService;

		public ReportingStructureController(ILogger<EmployeeController> logger, IReportingStructureService reportingStructureService)
		{
			_logger = logger;
			_reportingStructureService = reportingStructureService;
		}

		// Converted to async because data was returning without directReports.
		[HttpGet("{id}", Name = "getReportingStructureByEmployeeId")]
		public async Task<IActionResult> GetEmployeeById(String id)
		{
			_logger.LogDebug($"Received reporting structure get request for '{id}'");

			var reportingStructure = await _reportingStructureService.GetById(id);

			if (reportingStructure == null)
				return NotFound();

			return Ok(reportingStructure);
		}
	}
}
