#pragma once
#include <openssl/md5.h>
#include <string>

namespace BankProject::Services {

	class Crypto
	{
	public:
		static std::string md5(std::string);
	};

}

