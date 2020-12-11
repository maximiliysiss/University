#include "Repositories.h"

using namespace std;

void BankProject::Data::UserRepository::insert(User obj) {
	auto query = "exec sp_create_user " +
		("@Login = " + sqlstring(obj.get_login())) +
		("@PasswordHash = " + sqlstring(obj.get_passwordHash())) +
		("@RoleId = " + str((int)obj.get_role())) +
		("@Name = " + sqlstring(obj.get_name())) +
		("@Surname = " + sqlstring(obj.get_surname())) +
		("@Passport = " + sqlstring(obj.get_passport())) +
		("@Birthday = " + sqlstring(obj.get_birthdate())) +
		("@Birthplace = " + sqlstring(obj.get_birthplace())) +
		("@DepartmentId = " + str(obj.get_departmentId()));
	this->context->execute(query);
}

void BankProject::Data::UserRepository::remove(User obj) {
	this->context->execute("delete [dbo].[Users] where Id = " + str(obj.get_id()));
}

void BankProject::Data::UserRepository::update(User obj) {
	auto query = "update [dbo].[Users] set" +
		("Login = " + sqlstring(obj.get_login())) +
		("PasswordHash = " + sqlstring(obj.get_passwordHash())) +
		("RoleId = " + str((int)obj.get_role())) +
		("Name = " + sqlstring(obj.get_name())) +
		("Surname = " + sqlstring(obj.get_surname())) +
		("Passport = " + sqlstring(obj.get_passport())) +
		("Birthday = " + sqlstring(obj.get_birthdate())) +
		("Birthplace = " + sqlstring(obj.get_birthplace())) +
		("DepartmentId = " + str(obj.get_departmentId())) +
		"where Id = " + str(obj.get_id());
	this->context->execute(query);
}

std::list<User*> BankProject::Data::UserRepository::select() {
	BankProject::Data::Converters::UserConverter converter;
	auto data = this->context->select("select Login, PasswordHash, RoleId, Name, Surname, Passport, Birthday, Birthplace, Id, DepartmentId from [dbo].[Users]", &converter);
	std::list<User*> result;
	std::transform(data.begin(), data.end(), std::back_inserter(result), [](auto x) { return (User*)x; });
	return result;
}

User* BankProject::Data::UserRepository::getByLoginPassword(std::string login, std::string password)
{
	BankProject::Data::Converters::UserConverter converter;
	auto data = this->context->select("select Login, PasswordHash, RoleId, Name, Surname, Passport, Birthday, Birthplace, Id, DepartmentId from [dbo].[Users] where Login = "
		+ sqlstring(login) + " and PasswordHash = " + sqlstring(password), &converter);
	if (data.size() == 0)
		return nullptr;
	return (User*)*data.begin();
}

void BankProject::Data::AccountRepository::update(Account obj)
{
	this->context->execute("update [dbo].[Accounts](Value) set Value = " + str(obj.get_value()) + "where Id = " + str(obj.get_id()));
}

Account* BankProject::Data::AccountRepository::getById(int id)
{
	BankProject::Data::Converters::AccountConverter converter;
	auto select = context->select("select Id, ClientId, Value from [dbo].[Accounts] where Id = " + str(id), &converter);
	if (select.size() == 0)
		return nullptr;
	return (Account*)*select.begin();
}

std::list<Transaction*> BankProject::Data::TransactionRepository::getUserTransactions(int userId)
{
	BankProject::Data::Converters::TransactionConverter converter;
	auto data = context->select("select Id, DateTime, FromAccountId, ToAccountId, Value, ExecutorId from [dbo].[Transactions]", &converter);
	std::list<Transaction*> result;
	std::transform(data.begin(), data.end(), std::back_inserter(result), [](auto x) { return (Transaction*)x; });
	return result;
}

void BankProject::Data::TransactionRepository::createTransaction(int from, int to, int manager, double value)
{
	context->execute("exec sp_create_transaction @fromAccountId = " + str(from) + ", @toAccountId = " + str(to) + ", @value = " + str(value) + ",  @executor = " + str(manager));
}

void BankProject::Data::DeparatmentRepository::insert(Department obj) {
	context->execute("insert into [dbo].[Department](Name, Adress) values (" + sqlstring(obj.get_name()) + "," + sqlstring(obj.get_adress()) + ")");
}

void BankProject::Data::DeparatmentRepository::remove(Department obj) {
	context->execute("delete [dbo].[Department] where Id = " + str(obj.get_id()));
}

void BankProject::Data::DeparatmentRepository::update(Department obj) {
	context->execute("update [dbo].[Department] set Name = " + sqlstring(obj.get_name()) + ", Adress = " + sqlstring(obj.get_adress()) + " where id = " + str(obj.get_id()));
}

std::list<Department*> BankProject::Data::DeparatmentRepository::select() {
	DepartmentConverter converter;
	auto data = context->select("select Id, Name, Adress from [dbo].[Department]", &converter);
	std::list<Department*> result;
	std::transform(data.begin(), data.end(), std::back_inserter(result), [](auto x) { return (Department*)x; });
	return result;
}
