#pragma once
#include "defines.h"
#include <string>
#include <iostream>
#include <list>
#include "IConverter.h"

using namespace System::Data::SqlClient;
using namespace BankProject::Data::Converters;

namespace BankProject::Data {

	class IDataContext {
	public:
		virtual void execute(std::string sql) = 0;
		virtual std::list<void*> select(std::string sql, IConverter* converter) = 0;
	};

	class DataContext : public IDataContext
	{
		readonly_property(std::string, connectionString);

	public:
		DataContext(std::string connectionString) : connectionString(connectionString) {}

		virtual std::list<void*> select(std::string sql, IConverter* converter) override;
		virtual void execute(std::string sql);
	};

}
