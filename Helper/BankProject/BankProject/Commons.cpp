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

std::string strjoin(const char* separator, std::list<std::string> list)
{
	std::string result;
	for (auto tmp : list)
		result += tmp + separator;
	return result.substr(0, result.length() - strlen(separator));
}
