#pragma once
#include "User.h"
#include "ClientControl.h"
#include "DirectorControl.h"
#include "ManagerControl.h"

using namespace System::Windows::Forms;

namespace BankProject::Forms::Common {

	Control^ getMainControl(Models::Role role);

}