#include "Repositories.h"

using namespace std;

std::list<User*> BankProject::Data::UserRepository::select(std::string sql)
{
	BankProject::Data::Converters::UserConverter converter;
	auto data = this->context->select(sql, &converter);
	std::list<User*> result;
	std::transform(data.begin(), data.end(), std::back_inserter(result), [](auto x) { return (User*)x; });
	return result;
}

void BankProject::Data::UserRepository::insert(User obj) {
	auto query = "insert into [dbo].[Users](Login, PasswordHash, RoleId, Name, Surname, Passport, Birthday, Birthplace, DepartmentId) values (" +
		(sqlstring(obj.get_login()) + ",") +
		(sqlstring(obj.get_passwordHash()) + ",") +
		(str((int)obj.get_role()) + ",") +
		(sqlstring(obj.get_name()) + ",") +
		(sqlstring(obj.get_surname()) + ",") +
		(sqlstring(obj.get_passport()) + ",") +
		(tosqldatetime(obj.get_birthdate()) + ",") +
		(sqlstring(obj.get_birthplace()) + ",") +
		(str(obj.get_departmentId())) + ")";
	this->context->execute(query);
}

void BankProject::Data::UserRepository::remove(User obj) {
	this->context->execute("update [dbo].[Users] set GC = 1 where Id = " + str(obj.get_id()));
}

void BankProject::Data::UserRepository::update(User obj) {
	auto query = "update [dbo].[Users] set " +
		("Login = " + sqlstring(obj.get_login()) + ",") +
		("PasswordHash = " + sqlstring(obj.get_passwordHash()) + ",") +
		("RoleId = " + str((int)obj.get_role()) + ",") +
		("Name = " + sqlstring(obj.get_name()) + ",") +
		("Surname = " + sqlstring(obj.get_surname()) + ",") +
		("Passport = " + sqlstring(obj.get_passport()) + ",") +
		("Birthday = " + tosqldatetime(obj.get_birthdate()) + ",") +
		("Birthplace = " + sqlstring(obj.get_birthplace()) + ",") +
		("DepartmentId = " + str(obj.get_departmentId())) +
		" where Id = " + str(obj.get_id());
	this->context->execute(query);
}

std::list<User*> BankProject::Data::UserRepository::select() {
	return select("select Login, PasswordHash, RoleId, Name, Surname, Passport, Birthday, Birthplace, Id, DepartmentId from [dbo].[Users] where GC is null");
}

User* BankProject::Data::UserRepository::getByLoginPassword(std::string login, std::string password)
{
	BankProject::Data::Converters::UserConverter converter;
	auto data = this->context->select("select Login, PasswordHash, RoleId, Name, Surname, Passport, Birthday, Birthplace, Id, DepartmentId from [dbo].[Users] where GC is null and Login = "
		+ sqlstring(login) + " and PasswordHash = " + sqlstring(password), &converter);
	if (data.size() == 0)
		return nullptr;
	return (User*)*data.begin();
}

std::list<User*> BankProject::Data::UserRepository::selectWorkers()
{
	return select("select Login, PasswordHash, RoleId, Name, Surname, Passport, Birthday, Birthplace, Id, DepartmentId from [dbo].[Users] where RoleId = 2 and GC is null");
}

std::list<User*> BankProject::Data::UserRepository::selectClients()
{
	return select("select Login, PasswordHash, RoleId, Name, Surname, Passport, Birthday, Birthplace, Id, DepartmentId from [dbo].[Users] where RoleId = 1 and GC is null");
}

void BankProject::Data::UserRepository::createClient(User obj)
{
	auto query = "exec sp_create_user " +
		("@Login = " + sqlstring(obj.get_login()) + ",") +
		("@PasswordHash = " + sqlstring(obj.get_passwordHash()) + ",") +
		("@RoleId = " + str((int)obj.get_role()) + ",") +
		("@Name = " + sqlstring(obj.get_name()) + ",") +
		("@Surname = " + sqlstring(obj.get_surname()) + ",") +
		("@Passport = " + sqlstring(obj.get_passport()) + ",") +
		("@Birthday = " + sqlstring(obj.get_birthdate()) + ",") +
		("@Birthplace = " + sqlstring(obj.get_birthplace()) + ",") +
		("@DepartmentId = " + str(obj.get_departmentId()));
	this->context->execute(query);
}

User* BankProject::Data::UserRepository::getUserByAccountId(int id)
{
	auto res = select("select u.Login, u.PasswordHash, u.RoleId, u.Name, u.Surname, u.Passport, u.Birthday, u.Birthplace, u.Id, u.DepartmentId from [dbo].[Users] u inner join [dbo].[Accounts] a on a.ClientId = u.Id where a.Id = " + str(id) + " and u.RoleId = 1");
	if (res.size() == 0)
		return nullptr;
	return *res.begin();
}

void BankProject::Data::AccountRepository::update(Account obj)
{
	this->context->execute("update [dbo].[Accounts] set Value = " + str(obj.get_value()) + " where Id = " + str(obj.get_id()));
}

Account* BankProject::Data::AccountRepository::getById(int id)
{
	BankProject::Data::Converters::AccountConverter converter;
	auto select = context->select("select Id, ClientId, Value from [dbo].[Accounts] where ClientId = " + str(id), &converter);
	if (select.size() == 0)
		return nullptr;
	return (Account*)*select.begin();
}

void BankProject::Data::AccountRepository::addMoney(int id, double value)
{
	this->context->execute("update [dbo].[Accounts] set Value = Value + " + str(value) + " where Id = " + str(id));
}

std::list<Transaction*> BankProject::Data::TransactionRepository::getUserTransactions(int userId)
{
	BankProject::Data::Converters::TransactionConverter converter;
	auto data = context->select("select Id, DateTime, FromAccountId, ToAccountId, (case when FromAccountId = " + str(userId) + " then Value * -1 else Value end) Value, ExecutorId from [dbo].[Transactions] where FromAccountId = " + str(userId) + " or ToAccountId = " + str(userId), &converter);
	std::list<Transaction*> result;
	std::transform(data.begin(), data.end(), std::back_inserter(result), [](auto x) { return (Transaction*)x; });
	return result;
}

void BankProject::Data::TransactionRepository::createTransaction(int from, int to, int manager, double value)
{
	context->execute("exec sp_create_transaction @fromAccountId = " + str(from) + ", @toAccountId = " + str(to) + ", @value = " + str(value) + ",  @executor = " + str(manager));
}

std::list<Department*> BankProject::Data::DeparatmentRepository::select(std::string sql)
{
	DepartmentConverter converter;
	auto data = context->select(sql, &converter);
	std::list<Department*> result;
	std::transform(data.begin(), data.end(), std::back_inserter(result), [](auto x) { return (Department*)x; });
	return result;
}

void BankProject::Data::DeparatmentRepository::insert(Department obj) {
	context->execute("insert into [dbo].[Department](Name, Adress) values (" + sqlstring(obj.get_name()) + "," + sqlstring(obj.get_adress()) + ")");
}

void BankProject::Data::DeparatmentRepository::remove(Department obj) {
	context->execute("update [dbo].[Department] set GC = 1 where Id = " + str(obj.get_id()));
}

void BankProject::Data::DeparatmentRepository::update(Department obj) {
	context->execute("update [dbo].[Department] set Name = " + sqlstring(obj.get_name()) + ", Adress = " + sqlstring(obj.get_adress()) + " where id = " + str(obj.get_id()));
}

std::list<Department*> BankProject::Data::DeparatmentRepository::select() {
	return select("select Id, Name, Adress from [dbo].[Department] where GC is null");
}

std::list<Department*> BankProject::Data::DeparatmentRepository::selectWithouDefault()
{
	return select("select Id, Name, Adress from [dbo].[Department] where Id != 1 and GC is null");
}
