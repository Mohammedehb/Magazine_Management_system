Create Table employess 
(Employee_ID number(15)  primary key, Employee_name varchar2(20),gender varchar2 (1), Phone_number number(30), address varchar2(30), name_magazine varchar2(15),number_magazine number(15) , user_account varchar2 (30), Date_work Date ,sales_report number(15)) ; 
 
Create Table customer 
(customer_ID number(15) primary key, customer_Name varchar2(20), gender varchar2 (1), account_bank varchar2(15) , bank_balance number(15),user_account varchar2 (30),name_magazine varchar2(15), View_feedback varchar2 (15),Date_borrow Date) ; 
 
Create table manger 
(Manger_ID number(15) primary key,customer_ID number(15), Manger_Name varchar2 (20) not null, gender varchar2 (1),address varchar2(30), namemagazine varchar2(15)  ,Phone_number number(15), user_account varchar2 (30), Date_workm Date,Employee_ID number(15)) ; 
 
 
Alter table manger
add foreign key (Employee_ID) references employess (Employee_ID)  ;
Alter table manger
add foreign key (customer_ID) references customer (customer_ID)  ;
 
 
Insert into employess values 
(1,'Kristen Stewart','F',01224673311,'America_london','tamerashor',70,'Kristen_Stewart@yahoo.com', '16-Nov-2012' , 1000); 
Insert into employess values 
(2,'Robert Pattinson','M',01024228911,'America_mxico','times',80,'Robert_Pattinson@gmail.com', '18-Nov-2013' , 2000); 
Insert into employess values 
(3,'Taylor Lautner','M',01114673311,'Egypt_helwan','eco',130,'Taylor_Lautner@yahoo.com', '25-Dec-2011' , 3000); 
Insert into employess values 
(4,'Rupert Grint','M',01022678333,'saudi arbia_elmdina','tamerhosny',50,'Rupert_Grint@gmail.com', '03-Mar-2010' , 4000); 
Insert into employess values 
(5,'Daniel Radcliffe','M',01233228911,'america_USA','nor_elha',0,'Daniel_Radcliffe@gmail.com', '10-Oct-2018' , 5000); 

commit;

Insert into customer values 
(1,'Hank Azaria','M','Elhaly',1000,'Hank_Azaria@yahoo.com','tamerashor','good','20-Nov-2022'); 
Insert into customer values 
(2,'Neil Patrick','M','Elhaly',2000,'Neil_Patrick@yahoo.com','times','good','17-Dec-2023'); 
Insert into customer values 
(3,'Jayma Mays','F','Masr',1500,'Jayma_Mays@gmail.com','eco','bad','15-Mar-2023'); 
Insert into customer values 
(4,'Sofia Vergara','F','Masr',3900,'Sofia_Vergara@gmail.com','tamerhosny','not bad','23-Oct-2021'); 
Insert into customer values 
(5,'John Cleese','M','Elhaly',8090,'Sofia_Vergara@gmail.com','nor_elha','so good','20-Oct-2020');

commit;
insert into manger values 
(1, 1, 'ahmed','M' ,'Helwan','tamerashor',01124678911,'ahmedyyy@gmail.com', '23-Nov-2000',1); 
insert into manger values 
(2, 2, 'yasmin','F' ,'Ein_shams','times',01223374911,'yasmin_ahmed@yhoo.com', '16-Oct-2002',2); 
insert into manger values 
(3, 3,'zyad','M','EL_marg' ,'eco',01123248892,'zyad_khled@gmail.com', '16-Jan-2001',3); 
insert into manger values 
(4, 4,'zeke', 'M','Giza','tamerhosny',01125901189,'zeke_zaza@yhoo.com', '01-Mar-2008',4); 
insert into manger values 
(5, 5,'nada', 'F','Matria','nor_elha',01033271231,'nada@gmail.com', '12-Dec-2007',5); 


commit;



create or replace PROCEDURE viewfeedback
(cus_id IN NUMBER, feedback OUT VARCHAR2)
AS 
BEGIN 
  SELECT view_feedback
  INTO feedback
  FROM customer
  WHERE customer_id = cus_id;
END;

create or replace procedure information
(id_emp in number,name_emp out varchar2,gender_emp out varchar2,Phn_num out number,addr out varchar2,Dat_work out Date)
AS 
BEGIN
 select emp.Employee_name, emp.gender, emp.Phone_number, emp.address, emp.Date_work
 into name_emp, gender_emp, Phn_num, addr, Dat_work
 from employess emp 
 where id_emp = emp.Employee_ID;
End;


create or replace procedure number_magazines
(nam_magazine in varchar2,num_magazine out number)
as 
begin 
select emp.number_magazine
into num_magazine
from employess emp
where nam_magazine = emp.name_magazine;
End;

create or replace
procedure Get_rep_sales
  (nam_magazine in varchar2,sale_report out number)
as 
begin 
select emp.sales_report
into sale_report
from employess emp
where nam_magazine = emp.name_magazine;
End;
