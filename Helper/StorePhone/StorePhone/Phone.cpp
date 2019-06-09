#include "Phone.h"

InstrumentsForPhones::filterPhone InstrumentsForPhones::all = [](Phone& phone) {
	return true;
};

std::string(*InstrumentsForPhones::getName)(Phone& phone) = [](Phone& phone) {
	return phone.getname();
};

std::string(*InstrumentsForPhones::getProcessor)(Phone& phone) = [](Phone& phone) {
	return phone.getprocessor();
};

std::string(*InstrumentsForPhones::getModel)(Phone& phone) = [](Phone& phone) {
	return phone.getmodel();
};

float(*InstrumentsForPhones::getCamera)(Phone& phone) = [](Phone& phone) {
	return phone.getmp();
};

float(*InstrumentsForPhones::getDiagonal)(Phone& phone) = [](Phone& phone) {
	return phone.getdiagonal();
};

float(*InstrumentsForPhones::getRam)(Phone& phone) = [](Phone& phone) {
	return phone.getram();
};

float(*InstrumentsForPhones::getPrice)(Phone& phone) = [](Phone& phone) {
	return phone.getprice();
};

Phone::Phone()
{
}


Phone::Phone(string model, float ram, string os, float diagonal, float mp, size_t width, size_t height, float price)
{
	this->model = model;
	this->ram = ram;
	this->os = os;
	this->diagonal = diagonal;
	this->mp = mp;
	this->width = width;
	this->height = height;
	this->price = price;
}

bool Phone::operator==(const Phone & phone)
{
	return this->id == phone.id;
}

bool Phone::operator<(const Phone & phone)
{
	return this->id < phone.id;
}

bool Phone::operator>(const Phone & phone)
{
	return this->id > phone.id;
}

TiXmlElement * Phone::convertToTinyXML()
{
	TiXmlElement * xml = new TiXmlElement("Phone");
	xml->SetAttribute("Diagonal", std::to_string(this->diagonal).c_str());
	xml->SetAttribute("Height", std::to_string(this->height).c_str());
	xml->SetAttribute("Id", std::to_string(this->id).c_str());
	xml->SetAttribute("Model", this->model.c_str());
	xml->SetAttribute("MP", std::to_string(this->mp).c_str());
	xml->SetAttribute("OS", this->os.c_str());
	xml->SetAttribute("Price", std::to_string(this->price).c_str());
	xml->SetAttribute("RAM", std::to_string(this->ram).c_str());
	xml->SetAttribute("Width", std::to_string(this->width).c_str());
	xml->SetAttribute("Name", this->name.c_str());
	xml->SetAttribute("Processor", this->processor.c_str());
	return xml;
}

void Phone::loadFromTinyXML(TiXmlElement * xml)
{
	this->diagonal = std::stof(xml->Attribute("Diagonal"));
	this->height = std::stoi(xml->Attribute("Height"));
	this->id = std::stoi(xml->Attribute("Id"));
	this->model = xml->Attribute("Model");
	this->mp = std::stof(xml->Attribute("MP"));
	this->os = xml->Attribute("OS");
	this->price = std::stof(xml->Attribute("Price"));
	this->ram = std::stof(xml->Attribute("RAM"));
	this->width = std::stoi(xml->Attribute("Width"));
	this->name = xml->Attribute("Name");
	this->processor = xml->Attribute("Processor");
}
