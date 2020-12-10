#include "MainFormFactory.h"

Control^ BankProject::Forms::Common::getMainControl(Models::Role role) {
	switch (role) {
	case Models::Role::Client:
		return gcnew ClientControl();
	case Models::Role::Manager:
		return gcnew ManagerControl();
	case Models::Role::TechDirector:
		return gcnew DirectorControl();
	}
	throw new std::exception("Cannot create main form");
}
