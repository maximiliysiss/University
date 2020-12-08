#pragma once
#include "User.h"
#include "Commons.h"

using namespace System::Data::SqlClient;
using namespace BankProject::Models;

namespace BankProject::Data::Converters {

	template<typename T>
	class Converter {
	public:
		T convert(SqlDataReader^ reader) {
			return T();
		}
	};

	template<>
	class Converter<User> {
	public:
		User convert(SqlDataReader^ reader) {
			User user;
			user.set_birthdate("");
			user.set_birthplace(toStdString(reader->GetString(0)));
			user.set_id(reader->GetInt32(1));
			user.set_login(toStdString(reader->GetString(0)));
			user.set_name(toStdString(reader->GetString(0)));
			user.set_passport(toStdString(reader->GetString(0)));
			user.set_passwordHash(toStdString(reader->GetString(0)));
			user.set_role((BankProject::Models::Role)reader->GetInt32(0));
			user.set_surname(toStdString(reader->GetString(0)));
			return user;
		}
	};

}