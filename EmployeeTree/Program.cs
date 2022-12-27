using EmployeeTree.Modal;
using EmployeeTree.Modal.Enums;
using System.Collections.Generic;
using System;
using System.Linq;
using EmployeeTree.Modal.ViewModal;

namespace EmployeeTree
{
    public class Program
    {
        static Employee employee = new Employee();
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Organization Chart");
            BindData(ref employee, CreateData(), Level.VP.ToString());
            Console.WriteLine("please enter employee name to search");
            var empName = Console.ReadLine();
            EmployeeViewModal employeeViewModal = new EmployeeViewModal();
            employeeViewModal = GetData(employee, empName);
            if (employeeViewModal != null)
            {
                Console.WriteLine("employee found\n");
                Console.WriteLine("----------------------------\n");
                Console.WriteLine($"Employee Parent: - {employeeViewModal.Parent?.Name}\n");
                Console.WriteLine($"Employee Parent Id: - {employeeViewModal.Parent?.Id}\n");
                Console.WriteLine($"Employee Name: - {employeeViewModal.Employee.Name}\n");
                Console.WriteLine($"Employee Id: - {employeeViewModal.Employee.Id}\n");
                Console.WriteLine($"Employee Level: - {employeeViewModal.Employee.Level}\n");
                Console.WriteLine("----------------------------\n");

                if (employeeViewModal.SubOrdinates?.Count > 0)
                {
                    foreach (var item in employeeViewModal.SubOrdinates)
                    {
                        Console.WriteLine($"subordinate Name: - {item.Name}\n");
                        Console.WriteLine($"subordinate Id: - {item.Id}");

                    }
                }
            }
            else
            {
                Console.WriteLine("no employee found");

            }
            Console.Read();
        }


        private static List<Employee> CreateData()
        {

            List<Employee> employees = new List<Employee>();
            employees.Add(new Employee { Id = Level.VP.ToString(), Level = Level.VP, Name = "Hari", ParentId = null });
            employees.Add(new Employee { Id = "1.1", Level = Level.AVP, Name = "Tushar", ParentId = Level.VP.ToString() });
            employees.Add(new Employee { Id = "1.1.1", Level = Level.SRMNGR, Name = "Vaibhav", ParentId = "1.1" });
            employees.Add(new Employee { Id = "1.1.2", Level = Level.SRMNGR, Name = "Swapnil", ParentId = "1.1" });
            employees.Add(new Employee { Id = "1.1.2.1", Level = Level.MNGR, Name = "Priyanka", ParentId = "1.1.2" });
            employees.Add(new Employee { Id = "1.1.2.1.1", Level = Level.SE, Name = "Priya", ParentId = "1.1.2.1" });
            employees.Add(new Employee { Id = "1.1.2.1.2", Level = Level.SE, Name = "Sankalpa", ParentId = "1.1.2.1" });
            return employees;
        }

        private static void BindData(ref Employee employee, List<Employee> employees, string parentId)
        {

            if (parentId == null || parentId == Level.VP.ToString())
            {
                employee = employees.Where(a => a.Level == Level.VP).FirstOrDefault();
            }
            else
            {
                employee = employees.Where(a => a.Id == parentId).FirstOrDefault();

            }
            List<Employee> childEmployees = employees.Where(a => a.ParentId == parentId).ToList();
            employee.childEmployees = childEmployees;

            foreach (var employee1 in childEmployees)
            {
                var emp = employee1;
                emp.Parent = employee;
                BindData(ref emp, employees, employee1.Id);
            }
        }

        private static EmployeeViewModal GetData(Employee employee, string name)
        {
            if (employee.Name.ToLower() == name.ToLower())
            {
                return GetEmployeeViewModal(employee);
            }


            foreach (var item in employee.childEmployees)
            {
                if (item.Name.ToLower() == name.ToLower())
                {
                    return GetEmployeeViewModal(item);

                }
                var data= GetData(item, name);
                if(data != null)
                { return data; }
            }

            return null;
        }

        private static EmployeeViewModal GetEmployeeViewModal(Employee employee)
        {
            EmployeeViewModal employeeViewModal = new EmployeeViewModal();
            employeeViewModal.Employee = employee;
            employeeViewModal.Parent = employee.Parent;
            employeeViewModal.Children = employee.childEmployees;
            employeeViewModal.SubOrdinates = employee.Parent?.childEmployees.Where(a => a.Id != employee.Id).ToList();
            return employeeViewModal;
        }
    }

    




}
