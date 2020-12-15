#pragma once
#include <string>

namespace BankProject::Service::QueryBuilder {

	std::string toSQLString(std::string in) {
		return "'" + in + "'";
	}

}