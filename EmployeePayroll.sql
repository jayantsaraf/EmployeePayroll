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
