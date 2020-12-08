#include "DataContext.h"

template<typename T>
std::list<T> BankProject::Data::DataContext::select(std::string sql)
{
	SqlConnection^ connection = gcnew SqlConnection(gcnew System::String(this->connectionString.c_str()));
	connection->Open();

	SqlCommand^ scmd = gcnew SqlCommand(gcnew System::String(sql.c_str()), connection);
	auto reader = scmd->ExecuteReader();

	std::list<T> results;
	Converters::Converter<T> converter;

	while (reader->Read()) {
		results.push_back(converter.convert(reader));
	}

	reader->Close();
	connection->Close();
}

void BankProject::Data::DataContext::execute(std::string sql){
	SqlConnection^ connection = gcnew SqlConnection(gcnew System::String(this->connectionString.c_str()));
	connection->Open();

	SqlCommand^ scmd = gcnew SqlCommand(gcnew System::String(sql.c_str()), connection);
	scmd->ExecuteNonQuery();

	connection->Close();
}
