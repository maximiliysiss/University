#pragma once
#include "defines.h"
#include "User.h"

namespace BankProject::Commons {

	// Контекст пользователя
	struct UserContext {
		auto_property(int, userId);
		auto_property(Models::Role, role);
	};

}