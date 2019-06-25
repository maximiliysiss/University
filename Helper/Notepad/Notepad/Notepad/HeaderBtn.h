#pragma once
#include <Windows.h>
#include "Workspace.h"

class Workspace;
///
/// Класс кнопки в заголовке
///
class HeaderBtn
{
	/// Область кнопки
	RECT rect;
	/// Открытый документ для этой кнопки
	Workspace * workspace;
public:
	/// Обработать нажатие
	bool OnClick(POINT point, HWND);
	/// Установить размер
	void SetRect(RECT rect);
	/// Получить рабочую область
	Workspace * GetWorkspace();
	/// Конструктор
	HeaderBtn(Workspace * workspace);
	/// Деструктор
	~HeaderBtn();
};

