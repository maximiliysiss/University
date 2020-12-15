#pragma once
#include "defines.h"

namespace BankProject::Models {

	enum class Role {
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
		auto_property(int, departmentId);
	};

	/// <summary>
	/// Фабрика пользователей
	/// </summary>
	class UserFactory {
	public:
		static User* createWorker() {
			User* worker = new User();
			worker->set_role(Role::Manager);
			worker->set_birthdate(toStdString(System::DateTime::Now.ToString("dd-MM-yyyy")));
			return worker;
		}
		static User* createClient() {
			User* client = new User();
			client->set_role(Role::Client);
			client->set_departmentId(1);
			client->set_birthdate(toStdString(System::DateTime::Now.ToString("dd-MM-yyyy")));
			return client;
		}
	};

}