#pragma once
#include "defines.h"
#include <string>

namespace BankProject::Models {

	class Department {
		auto_property(int, id);
		auto_property(std::string, adress);
		auto_property(std::string, name);
	};

}