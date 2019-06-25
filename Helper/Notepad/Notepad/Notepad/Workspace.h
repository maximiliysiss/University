#pragma once
#include <Windows.h>
#include "App.h"
#include <string>
#include <functional>
#include <fstream>

class App;

///
/// Класс рабочей области
///
class Workspace
{
	/// Приложение
	App * parent;
	/// Идентификатор приложения
	HINSTANCE hInst;
	/// Представление для EditBox
	HWND hwnd;
	/// ID
	UINT guid;
	/// Путь для сохранения
	std::wstring savePlace;
	/// Сохранено?
	bool isSaving{ false };
	/// Получить для сохранения
	std::wstring GetPathForSaving();
	/// Сохранить
	void SaveProcess();
	/// Имя файла
	std::wstring name{};

public:
	/// Сохранить
	void Save();
	/// Установить наполнение
	void SetContent(std::wstring content);
	/// Сохранить как
	void SaveAs();
	void AppendText(std::wstring text);
	/// Получить имя
	inline std::wstring GetName() { return name; };
	/// Получить представление
	inline HWND GetEditBox() { return hwnd; }
	/// Установить как измененное
	inline void SetChanging() { isSaving = false; }
	/// Установить путь сохранения
	inline void SetSavePlace(std::wstring path) { savePlace = path; }
	/// Получить область
	RECT GetWndRECT();
	/// Сделать активным
	void SetActive(HWND, bool = true);
	// Показать
	void ShowDialog();
	/// Изменить размер
	void Resize(RECT & rect);
	/// Конструктор
	Workspace(HINSTANCE, App*, std::wstring);
	/// Деструктор
	~Workspace();
};

