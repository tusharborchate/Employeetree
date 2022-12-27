using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeTree.Modal.ViewModal
{
    public  class EmployeeViewModal
    {
        public Employee Employee { get; set; }

        public List<Employee> SubOrdinates { get; set; }

        public Employee Parent { get; set; }

        public List<Employee> Children { get; set; }


    }
}
