#pragma once
#include <string>

using namespace System;

std::string toStdString(String^ str) {
	using namespace System::Runtime::InteropServices;
	const char* chars = (const char*)(Marshal::StringToHGlobalAnsi(str)).ToPointer();
	std::string tmp = chars;
	Marshal::FreeHGlobal(IntPtr((void*)chars));
	return tmp;
}