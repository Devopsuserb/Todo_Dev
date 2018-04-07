
--Creating task table and inserting data into the tables
create database ToDo;
use todo;

drop table tasktable;

create table tasktable (task_id int primary key identity(1,1),task_name varchar(200),task_startdate date not null default(getdate()), task_status Char(9) not null default('active'));

insert into tasktable(task_name) values ('Have a cup of coffee'),('Have healthy breakfast');
insert into tasktable(task_name, task_status) values ('Get up early in the morning','Completed');
insert into tasktable(task_name) values ('Have a cup of coffee'),('Have healthy breakfast');
insert into tasktable(task_name, task_status) values ('Get up early in the morning','Completed');
insert into tasktable(task_name) values ('Have a cup of coffee'),('Have healthy breakfast');
insert into tasktable(task_name, task_status) values ('Get up early in the morning','Completed');
insert into tasktable(task_name) values ('Have a cup of coffee'),('Have healthy breakfast');
insert into tasktable(task_name, task_status) values ('Get up early in the morning','Completed');
insert into tasktable(task_name) values ('Have a cup of coffee'),('Have healthy breakfast');
insert into tasktable(task_name, task_status) values ('Get up early in the morning','Completed');
insert into tasktable(task_name) values ('Have a cup of coffee'),('Have healthy breakfast');
insert into tasktable(task_name, task_status) values ('Get up early in the morning','Completed');

select * from tasktable;

--Stored procedures to perform CRUD operations

drop proc InsertTask;
Create proc InsertTask(@TaskName varchar(200))
as
begin
Insert into tasktable (task_name) values (@TaskName);
end

Exec InsertTask 'GoodNight';

drop proc DeleteSingleTask;

Create proc DeleteSingleTask(@TaskID int)
as 
begin
Delete from tasktable where task_ID = @TaskID;
end

drop proc UpdateSingleTask;

Create proc UpdateSingleTask(@TaskStatus char(9),@TaskID int)
as 
begin
update tasktable set task_status = @TaskStatus where task_ID = @TaskID;
end
EXEC UpdateSingleTask 'COMPLETED', 292

exec DeleteSingleTask 6;

drop proc GetChartData;

create proc GetChartData
as
begin
select count(task_id) as CountOfRecords, task_status from tasktable group by task_status;
end

Exec GetChartData '2018-03-17'
