using System;
using System.Collections.Generic;
using System.Text;

namespace OpsCore.Core.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Password {  get; set; } = string.Empty;
        public bool IsOnDuty { get; set; }
        public bool IsPresent { get; set; }
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
