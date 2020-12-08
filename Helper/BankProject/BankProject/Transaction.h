#pragma once
#include "defines.h"
#include <string>

namespace BankProject::Models {

	class Transaction {
		auto_property(int, id);
		auto_property(int, fromAccountId);
		auto_property(int, toAccountId);
		auto_property(double, value);
		auto_property(std::string, datetime);
	};

}