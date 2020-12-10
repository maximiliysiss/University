#include "Commons.h"

std::string toStdString(String^ str) {
	using namespace System::Runtime::InteropServices;
	const char* chars = (const char*)(Marshal::StringToHGlobalAnsi(str)).ToPointer();
	std::string tmp = chars;
	Marshal::FreeHGlobal(IntPtr((void*)chars));
	return tmp;
}

std::string strjoin(const char* separator)
{
	return std::string();
}
