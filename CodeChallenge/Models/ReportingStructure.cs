namespace CodeChallenge.Models
{
	public class ReportingStructure
	{
		public Employee Employee { get; set; }
		public int NumberOfReports
		{
			get => RecursiveCount(Employee);
		}

		private int RecursiveCount(Employee employee)
		{
			int count = 0;
			if (employee.DirectReports != null)
			{
				count = employee.DirectReports.Count;
				foreach (Employee e in employee.DirectReports)
				{
					count += RecursiveCount(e);
				}
			}
			return count;
		}
	}
}
