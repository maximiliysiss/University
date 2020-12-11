#pragma once
#include "defines.h"
#include "UserContext.h"
#include "Repositories.h"
#include "WindowsFunctionWrapper.h"



namespace BankProject::ViewModels {

	ref class AuthViewModel {
	private:
		set_property(WindowsCloseFunc^, close);
		Data::IUserRepository* repo;
	public:
		AuthViewModel() : repo(Commons::IOContainer::getInstance().resolve<Data::IUserRepository>()) {}

		void login(std::string login, std::string password);
	};

}