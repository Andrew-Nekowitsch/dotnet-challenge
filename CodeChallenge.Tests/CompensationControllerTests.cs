
using System.Net;
using System.Net.Http;
using System.Text;
using System;
using System.Collections.Generic;

using CodeChallenge.Models;

using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeCodeChallenge.Tests.Integration
{
	[TestClass]
	public class CompensationControllerTests
	{
		private static HttpClient _httpClient;
		private static TestServer _testServer;

		[ClassInitialize]
		// Attribute ClassInitialize requires this signature
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
		public static void InitializeClass(TestContext context)
		{
			_testServer = new TestServer();
			_httpClient = _testServer.NewClient();
		}

		[ClassCleanup]
		public static void CleanUpTest()
		{
			_httpClient.Dispose();
			_testServer.Dispose();
		}

		[TestMethod]
		public void CreateCompensation_Returns_Created()
		{
			int expectedSalary = 100;
			string expectedEmployeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";
			DateTime expectedDate = DateTime.Now;

			// Arrange
			var comp = new Compensation()
			{
				EmployeeId = expectedEmployeeId,
				Salary = expectedSalary,
				EffectiveDate = expectedDate
			};

			var requestContent = new JsonSerialization().ToJson(comp);

			// Execute
			var postRequestTask = _httpClient.PostAsync("api/compensation",
			   new StringContent(requestContent, Encoding.UTF8, "application/json"));
			var response = postRequestTask.Result;

			// Assert
			Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

			var newCompensation = response.DeserializeContent<Compensation>();
			Assert.IsNotNull(newCompensation.CompensationId);
			Assert.AreEqual(expectedSalary, newCompensation.Salary);
			Assert.AreEqual(expectedEmployeeId, newCompensation.EmployeeId);
			Assert.AreEqual(expectedDate, newCompensation.EffectiveDate);
		}

		[TestMethod]
		public void GetCompensationByEmployeeId_Returns_Ok()
		{
			// Arrange
			var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";

			// Execute
			var getRequestTask = _httpClient.GetAsync($"api/compensation/{employeeId}");
			var response = getRequestTask.Result;

			// Assert
			Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
			List<Compensation> compensation = response.DeserializeContent<List<Compensation>>();
			foreach (var comp in compensation)
			{
				Assert.IsNotNull(comp.CompensationId);
			}
		}

			[TestMethod]
			public void GetCompensationByEmployeeId_Returns_NotFound()
			{
				// Arrange
				var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86";

				// Execute
				var getRequestTask = _httpClient.GetAsync($"api/compensation/{employeeId}");
				var response = getRequestTask.Result;

				// Assert
				Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
			}
		}
}
