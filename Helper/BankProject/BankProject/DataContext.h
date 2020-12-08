#pragma once
#include "defines.h"
#include <string>
#include <iostream>
#include <list>
#include "IConverter.h"
#include "IDataContext.h"

using namespace System::Data::SqlClient;

namespace BankProject::Data {

	class IDataContext;

	class DataContext : public IDataContext
	{
		readonly_property(std::string, connectionString);

	private:
		template<typename T>
		std::list<T> select(std::string sql);

	public:
		DataContext(std::string connectionString) : connectionString(connectionString) {}
		virtual void execute(std::string sql);

	};

}
