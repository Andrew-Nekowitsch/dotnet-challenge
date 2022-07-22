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
			if (employee.DirectReports == null)
				return 0;

			int count = employee.DirectReports.Count;
			foreach (Employee e in employee.DirectReports)
			{
				count += RecursiveCount(e);
			}
			return count;
		}
	}
}
