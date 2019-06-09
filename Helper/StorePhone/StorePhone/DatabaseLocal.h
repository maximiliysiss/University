#pragma once
#include <fstream>
#include <string>
#include "CustomList.cpp"
#include "Libs/tinyxml.h"

/*Хранилище*/
/*Работает по принципу Singleton, то есть только 1 экземпляр на всю программу*/
template<typename T>
class DatabaseLocal
{
	/*путь файла*/
	std::string path;
	/*данные*/
	CustomList<T> data;
private:
	/*определение типа указателя на функцию типа bool name(T& elem)*/
	typedef bool(*selectFunc)(T& elem);
	/*единственный экземпляр*/
	static DatabaseLocal<T> * database;
	/*приватный коснтурктор*/
	DatabaseLocal(std::string);
public:
	/*загрузка*/
	void load();
	/*выгрузка*/
	void upload();
	/*получить едиснтвенный экземпляр*/
	static DatabaseLocal<T> & instance(std::string = "");
	/*добавить*/
	void insert(T elem);
	/*обновить*/
	void update(T elem);
	/*получить данные*/
	CustomList<T>& getData();
	/*получить данные из файла с применением функции отобора*/
	CustomList<T> select(selectFunc func = nullptr);
	/*удалить*/
	void remove(T elem);
	/*деструктор*/
	~DatabaseLocal();

};
