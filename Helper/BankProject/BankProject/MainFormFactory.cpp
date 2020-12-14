#include "MainFormFactory.h"

Control^ BankProject::Forms::Common::getMainControl(Models::Role role) {

	Commons::IOContainer& ioc = Commons::IOContainer::getInstance();

	switch (role) {
	case Models::Role::Client:
		return gcnew ClientControl(gcnew ViewModels::CurrentViewModel(ioc.resolve<Data::IUserRepository>(), ioc.resolve<Data::IAccountRepository>(), ioc.resolve<Data::ITransactionRepository>()));
	case Models::Role::Manager:
		return gcnew ManagerControl(gcnew ViewModels::ManagerViewModel(ioc.resolve<Data::IUserRepository>()));
	case Models::Role::TechDirector:
		return gcnew DirectorControl(gcnew ViewModels::DirectorViewModel(ioc.resolve<Data::IDepartmentRepository>(), ioc.resolve<Data::IUserRepository>()));
	}
	throw new std::exception("Cannot create main form");
}
