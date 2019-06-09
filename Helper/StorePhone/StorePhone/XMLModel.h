#pragma once
#include "Libs/tinyxml.h"

using namespace System::Windows::Forms;
/*Интерфейс для XML объектов*/
class XMLModel {
public:
	/*превратить объект в XML элемент*/
	virtual TiXmlElement * convertToTinyXML() = 0;
	/*превратить XML элемент в объект*/
	virtual void loadFromTinyXML(TiXmlElement * xml) = 0;
};