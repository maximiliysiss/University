#pragma once
#include "User.h"
#include "ClientControl.h"
#include "DirectorControl.h"
#include "ManagerControl.h"

using namespace System::Windows::Forms;

namespace BankProject::Forms::Common {

	/// <summary>
	/// Фабрика контролов для главного окна
	/// </summary>
	/// <param name="role"></param>
	/// <returns></returns>
	Control^ getMainControl(Models::Role role);

}