
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeChallenge.Models
{
	public class Compensation
	{
		[Key]
		public string CompensationId { get; set; }
		public String EmployeeId { get; set; }
		public double Salary { get; set; }
		public DateTime EffectiveDate { get; set; }
	}
}
