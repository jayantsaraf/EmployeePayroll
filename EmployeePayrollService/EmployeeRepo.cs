using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace EmployeePayrollService
{
    class EmployeeRepo
    {
        public static string connectionString = @"Data Source=LAPTOP-KB7EAQS7\SQLEXPRESS;Initial Catalog=payroll_service1;Integrated Security=True";
        SqlConnection connection = new SqlConnection(connectionString);
        /// <summary>
        /// Function to get data of all employees from the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void GetAllEmployee()
        {
            try
            {
                EmployeeModel employeeModel = new EmployeeModel();
                using (this.connection)
                {
                    string query = @"Select * from employee_payroll;";
                    SqlCommand cmd = new SqlCommand(query, this.connection);
                    this.connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            employeeModel.EmployeeID = dr.GetInt32(0);

                            employeeModel.EmployeeName = dr.GetString(1);
                            employeeModel.BasicPay = dr.GetDecimal(2);
                            employeeModel.StartDate = dr.GetDateTime(3);
                            employeeModel.Gender = Convert.ToChar(dr.GetString(4));
                            //employeeModel.PhoneNumber = dr.GetString(5);
                            //employeeModel.Address = dr.GetString(6);
                            //employeeModel.Department = dr.GetString(7);
                            //employeeModel.Deductions = dr.GetDouble(8);
                            //employeeModel.TaxablePay = dr.GetDouble(9);
                            //employeeModel.Tax = dr.GetDouble(10);
                            //employeeModel.NetPay = dr.GetDouble(11);
                            System.Console.WriteLine(employeeModel.EmployeeName + " " + employeeModel.BasicPay + " " + employeeModel.StartDate + " " + employeeModel.Gender + " " + employeeModel.PhoneNumber + " " + employeeModel.Address + " " + employeeModel.Department + " " + employeeModel.Deductions + " " + employeeModel.TaxablePay + " " + employeeModel.Tax + " " + employeeModel.NetPay);
                            System.Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("No data found");
                    }
                    this.connection.Close();
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Function to add employee to the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddEmployee(EmployeeModel model)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand command = new SqlCommand("SpAddEmployeeDetails", this.connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeName", model.EmployeeName);
                    command.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);
                    command.Parameters.AddWithValue("@Address", model.Address);
                    command.Parameters.AddWithValue("@Department", model.Department);
                    command.Parameters.AddWithValue("@Gender", model.Gender);
                    command.Parameters.AddWithValue("@BasicPay", model.BasicPay);
                    command.Parameters.AddWithValue("@Deductions", model.Deductions);
                    command.Parameters.AddWithValue("@TaxablePay", model.TaxablePay);
                    command.Parameters.AddWithValue("@Tax", model.Tax);
                    command.Parameters.AddWithValue("@NetPay", model.NetPay);
                    command.Parameters.AddWithValue("@StartDate", DateTime.Now);
                    //command.Parameters.AddWithValue("@City", model.City);
                    //command.Parameters.AddWithValue("@Country", model.Country);
                    this.connection.Open();
                    var result = command.ExecuteNonQuery();
                    this.connection.Close();
                    if (result != 0)
                    {

                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
            return false;
        }
        public bool UpdateSalary(string name, double newSalary)
        {
            try
            {
                using(this.connection)
                {
                    //SqlCommand command = new SqlCommand("SpUpdateEmployeeDetails", connection);
                    //command.CommandType = System.Data.CommandType.StoredProcedure;
                    //command.Parameters.AddWithValue("@EmployeeName", name);
                    //command.Parameters.AddWithValue("@NewSalary", newSalary);
                    string query = @"Update employee_payroll set basic_pay = '" + newSalary + "' where name = '" + name + "'";
                    SqlCommand command = new SqlCommand(query, this.connection);
                    this.connection.Open();
                    var result = command.ExecuteNonQuery();
                    this.connection.Close();
                    if (result != 0)
                    {

                        return true;
                    }
                    return false;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
            return false;
        }
        public void Retrieve_Employee_Within_Specified_Joining_Date(DateTime startDate, DateTime endDate)
        {
            try
            {
                using (this.connection)
                {
                    EmployeeModel employeeModel = new EmployeeModel();
                    string query = @"select * from employee_payroll where start between '" + startDate + "'and '" + endDate + "';"; 
                    SqlCommand command = new SqlCommand(query, this.connection);
                    this.connection.Open();
                    SqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            employeeModel.EmployeeID = dr.GetInt32(0);
                            employeeModel.EmployeeName = dr.IsDBNull(1) ? "NA" : dr.GetString(1);
                            employeeModel.BasicPay = dr.IsDBNull(2) ? 0 :  dr.GetDecimal(2);
                            employeeModel.StartDate = dr.IsDBNull(3) ? Convert.ToDateTime("01/01/0001") :  dr.GetDateTime(3);
                            employeeModel.Gender = dr.IsDBNull(4) ? ' '  :  Convert.ToChar(dr.GetString(4));
                            employeeModel.PhoneNumber = dr.IsDBNull(5) ? "NA" : dr.GetString(5);
                            employeeModel.Address = dr.IsDBNull(6) ? "NA" :  dr.GetString(6);
                            employeeModel.Department = dr.IsDBNull(7) ? "NA" :  dr.GetString(7);
                            employeeModel.Deductions = dr.IsDBNull(8) ? 0 :  dr.GetDouble(8);
                            employeeModel.TaxablePay = dr.IsDBNull(9) ? 0 :  dr.GetDouble(9);
                            employeeModel.Tax = dr.IsDBNull(10) ? 0 :  dr.GetDouble(10);
                            employeeModel.NetPay = dr.IsDBNull(11) ? 0 :  dr.GetDouble(11);
                            System.Console.WriteLine(employeeModel.EmployeeName + " " + employeeModel.BasicPay + " " + employeeModel.StartDate + " " + employeeModel.Gender + " " + employeeModel.PhoneNumber + " " + employeeModel.Address + " " + employeeModel.Department + " " + employeeModel.Deductions + " " + employeeModel.TaxablePay + " " + employeeModel.Tax + " " + employeeModel.NetPay);
                            System.Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("No data found");
                    }
                    this.connection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }


}
