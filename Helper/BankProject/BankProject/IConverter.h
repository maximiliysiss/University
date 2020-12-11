#pragma once
#include "Accounts.h"
#include "User.h"
#include "Transaction.h"
#include "Departament.h"
#include "Commons.h"

using namespace System::Data::SqlClient;
using namespace BankProject::Models;

namespace BankProject::Data::Converters {

	class IConverter {
	public:
		virtual void* convert(SqlDataReader^ reader) = 0;
	};

	template<typename T>
	class Converter : public IConverter {
	protected:
		virtual void convertImpl(T& obj, SqlDataReader^ reader) = 0;
	public:
		void* convert(SqlDataReader^ reader) {
			T* newObj = new T();
			this->convertImpl(*newObj, reader);
			return (void*)newObj;
		}
	};

	class UserConverter : public Converter<User> {
	protected:
		// Login, PasswordHash, RoleId, Name, Surname, Passport, Birthday, Birthplace
		void convertImpl(User& user, SqlDataReader^ reader) {
			user.set_birthdate(toStdString(reader->GetDateTime(6).ToString("dd-MM-yyyy")));
			user.set_birthplace(toStdString(reader->GetString(7)));
			user.set_id(reader->GetInt32(8));
			user.set_login(toStdString(reader->GetString(0)));
			user.set_name(toStdString(reader->GetString(3)));
			user.set_passport(toStdString(reader->GetString(5)));
			user.set_passwordHash(toStdString(reader->GetString(1)));
			user.set_role((BankProject::Models::Role)reader->GetInt32(2));
			user.set_surname(toStdString(reader->GetString(4)));
			user.set_departmentId(reader->GetInt32(9));
		}
	};

	class AccountConverter : public Converter<Account> {
	protected:
		void convertImpl(Account& acc, SqlDataReader^ reader) {
			acc.set_id(reader->GetInt32(0));
			acc.set_clientId(reader->GetInt32(1));
			acc.set_value(System::Convert::ToDouble(reader->GetDecimal(2)));
		}
	};

	class TransactionConverter : public Converter<Transaction> {
	protected:
		void convertImpl(Transaction& tr, SqlDataReader^ reader) {
			tr.set_id(reader->GetInt32(0));
			tr.set_datetime(toStdString(reader->GetDateTime(1).ToString("dd-MM-yyyy HH:mm")));
			tr.set_fromAccountId(reader->GetInt32(2));
			tr.set_toAccountId(reader->GetInt32(3));
			tr.set_value(System::Convert::ToDouble(reader->GetDecimal(4)));
			tr.set_executorId(reader->GetInt32(5));
		}
	};

	class DepartmentConverter : public Converter<Department> {
	protected:
		void convertImpl(Department& d, SqlDataReader^ reader) {
			d.set_id(reader->GetInt32(0));
			d.set_name(toStdString(reader->GetString(1)));
			d.set_adress(toStdString(reader->GetString(2)));
		}
	};

}