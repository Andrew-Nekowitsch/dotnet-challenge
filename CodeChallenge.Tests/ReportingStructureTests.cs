
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using CodeChallenge.Models;
using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeCodeChallenge.Tests.Integration
{
	[TestClass]
	public class ReportingStructureTests
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
        public async Task GetReportingStructure_Returns_Ok()
        {
            // Arrange
            var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";
            var expectedFirstName = "John";
            var expectedLastName = "Lennon";

            // Execute
            var response = await _httpClient.GetAsync($"api/reportingStructure/{employeeId}");
            ReportingStructure reportingStructure = response.DeserializeContent<ReportingStructure>();

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(expectedFirstName, reportingStructure.Employee.FirstName);
            Assert.AreEqual(expectedLastName, reportingStructure.Employee.LastName);
            Assert.AreEqual(4, reportingStructure.NumberOfReports);
        }

        [TestMethod]
        public async Task GetReportingStructure_Returns_NumberOfReports()
        {
            // Arrange
            var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";

            // Execute
            var response = await _httpClient.GetAsync($"api/reportingStructure/{employeeId}");
            ReportingStructure reportingStructure = response.DeserializeContent<ReportingStructure>();

            // Assert
            Assert.AreEqual(4, reportingStructure.NumberOfReports);
        }
    }
}
