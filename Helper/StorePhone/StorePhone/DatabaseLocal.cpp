#include "DatabaseLocal.h"

template<typename T>
DatabaseLocal<T> * DatabaseLocal<T>::database = nullptr;

template<typename T>
void DatabaseLocal<T>::load()
{
	TiXmlDocument doc;
	TiXmlDeclaration* decl = new TiXmlDeclaration("1.0", "", "");
	doc.LinkEndChild(decl);
	doc.LoadFile(path.c_str(), TIXML_ENCODING_UTF8);

	if (doc.Error())
		throw std::exception(doc.ErrorDesc());

	TiXmlElement * global = doc.FirstChildElement();

	for (auto tag = global->FirstChildElement(); tag != nullptr; tag = tag->NextSiblingElement("Phone")) {
		T elem;
		elem.loadFromTinyXML(tag);
		data.add(elem);
	}
}

template<typename T>
void DatabaseLocal<T>::upload()
{
	TiXmlElement * root = new TiXmlElement("Phones");
	data.forEach([&](T& elem) {
		root->LinkEndChild(elem.convertToTinyXML());
	});

	TiXmlDocument doc;
	TiXmlDeclaration* decl = new TiXmlDeclaration("1.0", "", "");
	doc.LinkEndChild(decl);
	doc.LinkEndChild(root);
	doc.SaveFile(path);
}

template<typename T>
DatabaseLocal<T>& DatabaseLocal<T>::instance(std::string path)
{
	if (!database) {
		database = new DatabaseLocal<T>(path);
	}
	return *database;
}

template<typename T>
void DatabaseLocal<T>::insert(T elem)
{
	data.add(elem);
}

template<typename T>
void DatabaseLocal<T>::update(T elem)
{
	auto element = data.update(elem);
}

template<typename T>
CustomList<T>& DatabaseLocal<T>::getData()
{
	return data;
}

template<typename T>
CustomList<T> DatabaseLocal<T>::select(selectFunc func)
{
	if (func)
		return data.where(func);
	return data;
}

template<typename T>
void DatabaseLocal<T>::remove(T elem)
{
	data.remove(elem);
}

template<typename T>
DatabaseLocal<T>::DatabaseLocal(std::string path) : path(path)
{
}

template<typename T>
DatabaseLocal<T>::~DatabaseLocal()
{
}
