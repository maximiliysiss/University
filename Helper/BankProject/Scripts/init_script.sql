USE master
GO

IF NOT EXISTS(SELECT 1 FROM sys.databases WHERE name = 'bank_cpp')
    CREATE DATABASE [bank_cpp]
GO

USE bank_cpp
GO

IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE name = 'Roles')
BEGIN 
	CREATE TABLE [dbo].[Roles](
		Id int not null primary key identity(1,1),
		Name nvarchar(255) not null,
	)
END
GO

IF NOT EXISTS(SELECT * from [dbo].[Roles])
BEGIN
	INSERT INTO [dbo].[Roles](Name) values
		('Client'),
		('Manager'),
		('TechDirector')	
END
GO

IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE name = 'Users')
BEGIN 
	CREATE TABLE [dbo].[Users](
		Id int not null primary key identity(1,1),
		Login nvarchar(255) not null,
		PasswordHash nvarchar(128) not null,
		RoleId int not null references [dbo].[Roles]([Id]),
		Name nvarchar(255) not null,
		Surname nvarchar(255) not null,
		Passport nvarchar(255) not null,
		Birthday datetime not null,
		Birthplace nvarchar(255) not null
	)
END
GO

IF NOT EXISTS(SELECT * FROM [dbo].[Users])
BEGIN
	INSERT INTO [dbo].[Users](Login, PasswordHash, RoleId, Name, Surname, Passport, Birthday, Birthplace) values
	('admin', '33354741122871651676713774147412831195', 3, 'admin', 'admin', 'admin', '01-01-1990', 'admin')
END

IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE name = 'Accounts')
BEGIN 
	CREATE TABLE [dbo].[Accounts](
		Id int not null primary key identity(1,1),
		ClientId int not null references [dbo].[Users]([Id]),
		Value decimal not null default 0
	)
END
GO

IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE name = 'Transactions')
BEGIN 
	CREATE TABLE [dbo].[Transactions](
		Id int not null primary key identity(1,1),
		FromAccountId int not null references [dbo].[Accounts]([Id]),
		ToAccountId int not null references [dbo].[Accounts]([Id]),
		Value decimal not null,
		DateTime datetime not null default GetDate(),
		ExecutorId int not null references [dbo].[Users]([Id])
	)
END
GO

IF EXISTS(SELECT 1 FROM sys.procedures where name = 'sp_create_transaction')
BEGIN 
	DROP PROCEDURE sp_create_transaction
END
GO

IF EXISTS(SELECT 1 FROM sys.procedures where name = 'sp_create_user')
BEGIN 
	DROP PROCEDURE sp_create_user
END
GO

CREATE PROCEDURE sp_create_transaction(
		@fromAccountId int,
		@toAccountId int,
		@value decimal,
		@executor int
	)
AS
BEGIN  
	BEGIN TRY
		BEGIN TRANSACTION	
		INSERT INTO [dbo].[Transactions](FromAccountId, ToAccountId, Value, ExecutorId) values (@fromAccountId, @toAccountId, @value, @executor)
		UPDATE [dbo].[Accounts] set Value = Value - @value WHERE Id = @fromAccountId
		UPDATE [dbo].[Accounts] set Value = Value + @value WHERE Id = @toAccountId
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH  
	    ROLLBACK TRANSACTION
	END CATCH
END
GO

CREATE PROCEDURE sp_create_user(
		@Login nvarchar(255),
		@PasswordHash nvarchar(128),
		@RoleId int,
		@Name nvarchar(255),
		@Surname nvarchar(255),
		@Passport nvarchar(255),
		@Birthday datetime,
		@Birthplace nvarchar(255)
	)
AS
BEGIN  
	BEGIN TRY
		BEGIN TRANSACTION	
		INSERT INTO [dbo].[Users](Login, PasswordHash, RoleId, Name, Surname, Passport, Birthday, Birthplace) values 
			(@Login, @PasswordHash, @RoleId, @Name, @Surname, @Passport, @Birthday, @Birthplace)
		INSERT INTO [dbo].[Accounts](ClientId) values (@@Identity)
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH  
	    ROLLBACK TRANSACTION
	END CATCH
END
GO

