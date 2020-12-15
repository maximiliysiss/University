#include "Config.h"
#include "IOContainer.h"
#include "Repositories.h"
#include <iostream>
#include "UserContext.h"
#include "AuthForm.h"

using namespace System;
using namespace BankProject::Commons;
using namespace BankProject::Data;

void initIOC(Config& config);

[STAThreadAttribute]
int __stdcall WinMain() {

	Config* config;

	try {
		config = Config::readConfig("config.json");
		initIOC(*config);

		BankProject::AuthForm^ authForm = gcnew BankProject::AuthForm();
		authForm->ShowDialog();
	}
	catch (std::exception ex) {
		std::cout << ex.what();
	}
	finally {
		if (config) {
			delete config;
		}
	}

	return 0;
}

void initIOC(Config& config) {
	IOContainer& ioc = IOContainer::getInstance();
	ioc.registerService<IDataContext, DataContext>(new DataContext(config.get_connectionString()));
	ioc.registerService<IUserRepository, UserRepository>(new UserRepository());
	ioc.registerService<ITransactionRepository, TransactionRepository>(new TransactionRepository());
	ioc.registerService<IAccountRepository, AccountRepository>(new AccountRepository());
	ioc.registerService<IDepartmentRepository, DeparatmentRepository>(new DeparatmentRepository());
	ioc.registerService(new UserContext());
}