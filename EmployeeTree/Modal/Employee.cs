using EmployeeTree.Modal.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeTree.Modal
{
    public class Employee
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public Level Level { get; set; }
        public Employee Parent { get; set; }
        public List<Employee> childEmployees { get; set; }


    }
}
