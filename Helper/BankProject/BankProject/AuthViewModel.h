#pragma once
#include "defines.h"
#include "UserContext.h"
#include "Repositories.h"
#include "WindowsFunctionWrapper.h"



namespace BankProject::ViewModels {

	/// <summary>
	/// Вьюмодель для входа
	/// </summary>
	ref class AuthViewModel {
	private:
		set_property(WindowsCloseFunc^, close);
		Data::IUserRepository* repo;
	public:
		AuthViewModel() : repo(Commons::IOContainer::getInstance().resolve<Data::IUserRepository>()) {}
		/// <summary>
		/// Вход
		/// </summary>
		/// <param name="login"></param>
		/// <param name="password"></param>
		void login(std::string login, std::string password);
	};

}