﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeChallenge.Models
{
    public class Employee
    {
        [Key]
        public String EmployeeId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Position { get; set; }
        public String Department { get; set; }
        public List<Employee> DirectReports { get; set; }
    }
}
