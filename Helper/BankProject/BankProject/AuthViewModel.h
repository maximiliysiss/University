#pragma once
#include "defines.h"
#include "UserContext.h"
#include "Repositories.h"

namespace BankProject {
	ref class AuthForm;
}

namespace BankProject::ViewModels {

	class AuthViewModel {
	private:
		Data::IUserRepository* repo;
	public:
		AuthViewModel() : repo(Commons::IOContainer::getInstance().resolve<Data::IUserRepository>()) {}

		void login(BankProject::AuthForm^ form, std::string login, std::string password);
	};

}