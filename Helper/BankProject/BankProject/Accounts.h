#pragma once
#include "defines.h"
#include <string>

namespace BankProject::Models {

	class Account {
		auto_property(int, id);
		auto_property(int, clientId);
		auto_property(double, value);
	};

}