if not exists(select * from sys.databases d where d.name = 'chatpy')
	create database chatpy
GO

use chatpy
GO 

if not exists(select * from sys.tables t where t.name = 'users')
begin
	create table [dbo].[Users](
		Id int not null primary key identity(1,1),
		Login nvarchar(max) not null,
		PasswordHash nvarchar(max) not null
	)
end
GO

if not exists(select * from sys.tables t where t.name = 'messages')
BEGIN 
	create table [dbo].[Messages](
		Id int not null primary key identity(1,1),
		UserId int not null references [Users](Id),
		Content nvarchar(max) not null,
		[DateTime] datetime not null default(getdate())
	)
END
GO