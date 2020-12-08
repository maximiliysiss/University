#include "Config.h"
#include "IOContainer.h"
#include "DataContext.h"
#include "IDataContext.h"
#include <iostream>

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
	auto ioc = IOContainer::getInstance();
	ioc.registerService<IDataContext, DataContext>(new DataContext(config.get_connectionString()));
	ioc.registerService<IBaseRepository<User>, UserRepository>(new UserRepository());
}