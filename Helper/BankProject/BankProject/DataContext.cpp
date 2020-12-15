#include "DataContext.h"

std::list<void*> BankProject::Data::DataContext::select(std::string sql, IConverter* converter)
{
	SqlConnection^ connection = gcnew SqlConnection(gcnew System::String(this->connectionString.c_str()));
	connection->Open();

	SqlCommand^ scmd = gcnew SqlCommand(gcnew System::String(sql.c_str()), connection);
	auto reader = scmd->ExecuteReader();

	std::list<void*> results;

	while (reader->Read()) {
		results.push_back(converter->convert(reader));
	}

	reader->Close();
	connection->Close();

	return results;
}

void BankProject::Data::DataContext::execute(std::string sql) {
	SqlConnection^ connection = gcnew SqlConnection(gcnew System::String(this->connectionString.c_str()));
	connection->Open();

	SqlCommand^ scmd = gcnew SqlCommand(gcnew System::String(sql.c_str()), connection);
	scmd->ExecuteNonQuery();

	connection->Close();
}
