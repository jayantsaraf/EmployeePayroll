create database Payroll_Service;
use Payroll_Service;
select DB_NAME();
create table employee_payroll
(
id int identity(1,1),
name varchar(25) not null,
salary money not null,
start date not null
);
select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = 'employee_payroll';
insert into employee_payroll values
('Bill',100000.00,'2018-01-03'),
('Terissa',200000.00,'2019-11-13'),
('Charlie',300000.00,'2020-05-21');
select * from employee_payroll;
select salary from employee_payroll where name = 'Bill';
select * from employee_payroll where start between '2018-01-01' and GETDATE();

--UC6
select * from employee_payroll
alter table employee_payroll add gender varchar(1)
update employee_payroll set gender = 'M' where name = 'Bill' 
or name = 'Charlie' 
update employee_payroll set gender = 'F' where name = 'Terissa' 

--UC7 Operations - SUM, AVERAGE, MIN-MAX, COUNT
select SUM(salary) from employee_payroll
where gender = 'M'
group by gender;
select AVG(salary), gender from employee_payroll
group by gender;
select MIN(salary), gender from employee_payroll
group by gender;
select MAX(salary), gender from employee_payroll
group by gender;
select COUNT(gender), gender from employee_payroll
group by gender;

-- UC8 Additional Infor addition to table
alter table employee_payroll add phone_number varchar(13)
alter table employee_payroll add address varchar(250), department varchar(20)

--Add department for existing enteries
select * from employee_payroll
update employee_payroll set department = 'Sales' where name = 'Bill'
update employee_payroll set department = 'HR' where name = 'Terissa'
update employee_payroll set department = 'Marketing' where name = 'Charlie'
update employee_payroll set department = 'Marketing' where id=2
------Adding constraints
----alter table employee_payroll add constraint default_address default 'India' for address
----alter table employee_payroll alter column department varchar(20) Not null
--Add salary divisions
alter table employee_payroll add deduction float, taxable_pay real, income_tax real, net_pay real
--Rename salary column
EXEC sp_rename 'employee_payroll.salary', 'basic_pay', 'COLUMN';
--Redundant data for Terissa added with department change 
insert into employee_payroll 
(name, basic_pay,start,gender,department)
values('Terissa',200000.00,'2019-11-13','F','Sales')
select * from employee_payroll
--Update information for every employee
Update employee_payroll 
set phone_number = '9926707344', address = 'Damoh Naka', deduction = 1000, taxable_pay = 99000, income_tax = 5000, net_pay = 94000 where id = 1
Update employee_payroll
set phone_number = '8529631478', address = 'PNB Colony', deduction = 3000, taxable_pay = 297000, income_tax = 10000, net_pay = 287000 where id = 3
Update employee_payroll
set phone_number = '9586942335', address = 'Shanti Nagar', deduction = 2000, taxable_pay = 198000, income_tax = 8000, net_pay = 190000 where name = 'Terissa';

-- Department Table
create table Department
(
deptId int not null,
deptName varchar(25),
empId int not null foreign key references employee(id)
)
select * from Department
alter table Department
add constraint empId_fkey2 
foreign key(empId) references employee(id)
create table employee
(
id int not null,
name varchar(25),
gender varchar(2),
phone_number varchar(15),
address varchar(25)
)
select * from employee
alter table employee
add primary key(id)
alter table employee 
add id int not null identity(1,1)
alter table employee
add primary key (id)
create table company
(
compId int not null,
start date not null,
basic_pay money,
deduction money,
taxable_pay money,
income_tax money,
net_pay money,
empId int not null constraint empId_fkey foreign key(empId) references employee(id)
)
select * from company
alter table company
add constraint empId_fkey 
foreign key(empId) references employee(id)
-- Adding values
insert into employee values
('Bill', 'M', '9424787443', 'Shanti Nagar'),
('Terissa', 'F', '8109322276', 'Damoh Naka'),
('Charlie', 'M', '9926707344', 'Panchsheel Nagar');
insert into Department values
(
(101, 'Sales',1),
(102, 'Sales',2),
(103, 'HR', 3),
(104,'Marketting',2);
insert into company values
(1,'2018-01-03', 100000, 10000, 90000, 1000, 89000,1),
(2, '2019-11-13', 200000, 10000, 190000,3000,187000,2),
(3, '2020-05-21', 300000, 20000, 280000, 5000, 275000,3);

select * from employee_payroll
--stored procedure to add employee details
create procedure SpAddEmployeeDetails
(
@EmployeeName varchar(255),
@BasicPay float,
@StartDate Date,
@Gender char(1),
@PhoneNumber varchar(255),
@Department varchar(255),
@Address varchar(255),
@Deductions float,
@TaxablePay float,
@Tax float,
@NetPay float
)
as
begin
insert into employee_payroll values
(
@EmployeeName,@BasicPay,@StartDate,@Gender,@PhoneNumber,@Address,@Department,@Deductions,@TaxablePay,@Tax,@NetPay
)
end

--stored procedure to update salary
GO
create procedure spUpdateEmployeeDetails
(
@NewSalary money,
@EmployeeName varchar(25)
)
as
begin
update employee_payroll
set basic_pay = @NewSalary where name = @EmployeeName
end