#pragma once
#include "defines.h"
#include <string>

namespace BankProject::Models {

	enum Role {
		Client = 1,
		Manager = 2,
		TechDirector = 3
	};

	class User {
		auto_property(int, id);
		auto_property(std::string, login);
		auto_property(std::string, passwordHash);
		auto_property(Role, role);
		auto_property(std::string, name);
		auto_property(std::string, surname);
		auto_property(std::string, passport);
		auto_property(std::string, birthplace);
		auto_property(std::string, birthdate);
	};

}