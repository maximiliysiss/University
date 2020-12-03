if not exists(select * from sys.databases d where d.name = 'javachat')
	create database javachat
GO

use javachat
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

IF NOT EXISTS(SELECT * FROM sys.tables t where t.name = 'private_message')
BEGIN 
	create table [dbo].[Private_Message](
		Id int not null primary key identity(1,1),
		ReceiverId int not null references [Users](Id),
		MessageId int not null references [Messages](Id)
	)
END
GO

if exists(select * from sys.procedures p where p.name = 'sp_messages')
	drop proc sp_messages
GO

if exists(select * from sys.procedures p where p.name = 'sp_create_privatemessage')
	drop proc sp_create_privatemessage
GO

create proc sp_create_privatemessage(
	@userId int,
	@content nvarchar(MAX),
	@receiveId int
)
AS 
BEGIN 
	insert into [dbo].[Messages](UserId,Content) values
		(@userId,@content)
	insert into [dbo].[Private_Message](ReceiverId,MessageId) values
		(@receiveId,@@IDENTITY)
END
GO

create proc sp_messages (
	@id int,
	@userId int
) 
as
begin
	select
		m.Id,
		case 
			when pm.Id is not null then concat(u.Login, ': [private] ', m.Content)
			else concat(u.Login, ': ', m.Content)
		end
	from Messages m 
	left join Users u on u.Id = m.UserId
	left join Private_Message pm on pm.MessageId = m.Id
	where 
		m.Id > @id AND 
		(pm.Id is null or m.UserId = @userId or pm.ReceiverId = @userId)
	order by m.Id
end
GO