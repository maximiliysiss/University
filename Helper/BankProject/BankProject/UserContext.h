#pragma once
#include "defines.h"
#include "User.h"

namespace BankProject::Commons {

	struct UserContext {
		auto_property(int, userId);
		auto_property(Models::Role, role);
	};

}