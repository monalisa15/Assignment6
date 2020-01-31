﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6
{
    class Program
    {
		IList<Employee> employeeList;
		IList<Salary> salaryList;

		public Program()
		{
			employeeList = new List<Employee>() {
			new Employee(){ EmployeeID = 1, EmployeeFirstName = "Rajiv", EmployeeLastName = "Desai", Age = 49},
			new Employee(){ EmployeeID = 2, EmployeeFirstName = "Karan", EmployeeLastName = "Patel", Age = 32},
			new Employee(){ EmployeeID = 3, EmployeeFirstName = "Sujit", EmployeeLastName = "Dixit", Age = 28},
			new Employee(){ EmployeeID = 4, EmployeeFirstName = "Mahendra", EmployeeLastName = "Suri", Age = 26},
			new Employee(){ EmployeeID = 5, EmployeeFirstName = "Divya", EmployeeLastName = "Das", Age = 20},
			new Employee(){ EmployeeID = 6, EmployeeFirstName = "Ridhi", EmployeeLastName = "Shah", Age = 60},
			new Employee(){ EmployeeID = 7, EmployeeFirstName = "Dimple", EmployeeLastName = "Bhatt", Age = 53}
		};

			salaryList = new List<Salary>() {
			new Salary(){ EmployeeID = 1, Amount = 1000, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 1, Amount = 500, Type = SalaryType.Performance},
			new Salary(){ EmployeeID = 1, Amount = 100, Type = SalaryType.Bonus},
			new Salary(){ EmployeeID = 2, Amount = 3000, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 2, Amount = 1000, Type = SalaryType.Bonus},
			new Salary(){ EmployeeID = 3, Amount = 1500, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 4, Amount = 2100, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 5, Amount = 2800, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 5, Amount = 600, Type = SalaryType.Performance},
			new Salary(){ EmployeeID = 5, Amount = 500, Type = SalaryType.Bonus},
			new Salary(){ EmployeeID = 6, Amount = 3000, Type = SalaryType.Monthly},
			new Salary(){ EmployeeID = 6, Amount = 400, Type = SalaryType.Performance},
			new Salary(){ EmployeeID = 7, Amount = 4700, Type = SalaryType.Monthly}
		};
		}

		public static void Main()
		{
			Program program = new Program();

			program.Task1();

			program.Task2();

			program.Task3();
		}

		public void Task1()
		{
			var result1 = from emp in employeeList
						  join sal in salaryList on emp.EmployeeID equals sal.EmployeeID
						  group new { sal, emp } by
	emp.EmployeeFirstName into salGroup
						  orderby salGroup.Sum(x => x.sal.Amount)
						  select new { Name = salGroup.Key, sal = salGroup.Sum(x => x.sal.Amount) };
			foreach (var i in result1)
			{
				Console.WriteLine($"{i.Name} : {i.sal}");
			}
		}

		public void Task2()
		{
			Console.WriteLine("************************");
			int item_no = 2;
			var result2 = (from e in employeeList
						   join sal in salaryList
							on e.EmployeeID equals sal.EmployeeID into grp
						   orderby e.Age descending
						   select new
						   {
							   Id = e.EmployeeID,
							   Fname = e.EmployeeFirstName,
							   Lname = e.EmployeeLastName,
							   Age = e.Age,
							   sal = grp.Sum(x => x.Amount)

						   }).ToList();
			Console.WriteLine("2nd Oldest Employee");
			Console.WriteLine("Id=" + result2[item_no - 1].Id);
			Console.WriteLine("Name=" + result2[item_no - 1].Fname + result2[item_no - 1].Lname);
			Console.WriteLine("Age=" + result2[item_no - 1].Age);
			Console.WriteLine("Salary=" + result2[item_no - 1].sal);
		}


		public void Task3()
		{
			Console.WriteLine("*************************************");
			Console.WriteLine("Average salary Of Employees age Greater than 30:");
			var result3 = from emp in employeeList
						  where emp.Age > 30
						  join sal in salaryList on emp.EmployeeID equals sal.EmployeeID into groups
						  select new
						  {
							  avg = groups.Average(x => x.Amount)
						  };
			foreach (var i in result3)
			{
				Console.WriteLine(i.avg);
			}
		}
	}



	public enum SalaryType
	{
		Monthly,
		Performance,
		Bonus
	}

	public class Employee
	{
		public int EmployeeID { get; set; }
		public string EmployeeFirstName { get; set; }
		public string EmployeeLastName { get; set; }
		public int Age { get; set; }
	}

	public class Salary
	{
		public int EmployeeID { get; set; }
		public int Amount { get; set; }
		public SalaryType Type { get; set; }
	}

}

