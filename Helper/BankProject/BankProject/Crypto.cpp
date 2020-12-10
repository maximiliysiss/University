#include "Crypto.h"

std::string BankProject::Services::Crypto::md5(std::string input)
{
	unsigned char result[MD5_DIGEST_LENGTH];
	MD5((unsigned char*)input.c_str(), input.length(), result);
	std::string res;
	for (int i = 0; i < MD5_DIGEST_LENGTH; i++) {
		res += std::to_string(result[i]);
	}
	return res;
}
