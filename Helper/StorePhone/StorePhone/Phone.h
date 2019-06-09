#pragma once
#include <string>
#include <iostream>
#include "XMLModel.h"

#ifndef PHONE_H
#define PHONE_H
/*автосвойства*/
/*чисто генерация кода*/
#define autoProperty(name, type)\
	private: \
		type name; \
	public: \
		inline type get##name(){ \
			return name; \
		} \
		inline type set##name(type name){ \
			this->##name = name; \
		} \
	private:

using namespace std;

class Phone : public XMLModel
{
private:
	/*определение полей*/
	autoProperty(id, int)
		autoProperty(name, string)
		autoProperty(processor, string)
		autoProperty(model, string)
		autoProperty(ram, float)
		autoProperty(os, string)
		autoProperty(diagonal, float)
		autoProperty(mp, float)
		autoProperty(width, size_t)
		autoProperty(height, size_t)
		autoProperty(price, float)
public:
	/*конструктор*/
	Phone();
	/*констурктор с аргументами*/
	Phone(string model, float ram, string os, float diagonal, float mp, size_t width, size_t height, float price);
	/*перегрузка оператора сравнения, нужно потом для списков*/
	bool operator==(const Phone& phone);
	/*перегрузка операторов сравнения*/
	bool operator<(const Phone& phone);
	bool operator>(const Phone& phone);
	// Inherited via XMLModel
	/*превратить элемент в элемент для XML документа*/
	virtual TiXmlElement * convertToTinyXML() override;
	/*превратитть XML элемент в объект*/
	virtual void loadFromTinyXML(TiXmlElement * xml) override;
};

/*всякие лямбды для Phones*/
struct InstrumentsForPhones {
	/*фильтры*/
	typedef bool(*filterPhone)(Phone& phone);
	/*сортировки*/
	typedef bool(*order)(Phone& phone, Phone& phone1);
	/*отобрать все*/
	static filterPhone all;
	/*получить различные части объекта телефона*/
	static std::string(*getName)(Phone& phone);
	static std::string(*getModel)(Phone& phone);
	static std::string(*getProcessor)(Phone& phone);
	static float(*getRam)(Phone& phone);
	static float(*getCamera)(Phone& phone);
	static float(*getDiagonal)(Phone& phone);
	static float(*getPrice)(Phone& phone);
};

#endif // !PHONE_H
