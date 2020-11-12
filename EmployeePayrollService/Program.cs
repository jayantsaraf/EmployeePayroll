using System;

namespace EmployeePayrollService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //Get data of all employees from database
            EmployeeRepo employeeRepo = new EmployeeRepo();
            //employeeRepo.GetAllEmployee();
            //Adding employee to database
            //EmployeeModel employee = new EmployeeModel();
            //employee.EmployeeName = "Indal";
            //employee.Department = "Tech";
            //employee.PhoneNumber = "6302907918";
            //employee.Address = "02-Khajauli";
            //employee.Gender = 'M';
            //employee.BasicPay = 10000.00M;
            //employee.Deductions = 1500;
            //employee.StartDate = employee.StartDate = Convert.ToDateTime("2020-11-03");

            //if (employeeRepo.AddEmployee(employee))
            //    Console.WriteLine("Records added successfully");
            if(employeeRepo.UpdateSalary("Indal", 200000))
                Console.WriteLine("Salary Updated successfully");
        }

    }
}
