#pragma once
#include <vector>
#include <cstdlib>
#include "Resource.h"
#include "Workspace.h"
#include "Utils.h"
#include "HeaderBtn.h"

class Workspace;
class HeaderBtn;
///
/// Класс приложения
///
class App
{
	/// Все открытые окна
	std::vector<Workspace*> windows;
	/// Активное окно
	std::vector<Workspace*>::iterator currentWindows;
	/// Кнопки сверху (через которые происходит переключение между документами)
	std::vector<HeaderBtn*> headerBtns;
	/// Представление для окна
	HWND window;
	// Класс окна
	WNDCLASS * wndClass;
	/// Инстанс окна
	HINSTANCE hInst;
	/// Имя окна
	std::wstring wndName;
	/// Количество открытых документов
	int newCount{ 0 };
	/// Координаты углов приложения (для визуала)
	static int top, bottom, left, right;
	/// Высота заголовков
	static const int headerHeight;
	/// Отрисовать окно
	static void Paint(HWND hwnd, HDC);
	/// Закрыть приложение
	static void Close();
	/// Обработчик команд
	static int CommandHandler(WPARAM wParam, LPARAM lParam, HWND hwnd, UINT message);
	/// Главный обработчик
	static LRESULT CALLBACK WndProc(HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam);
	/// Отображение окна "О программе"
	static INT_PTR CALLBACK About(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam);

	/// Конструктор
	App(int sX, int sY, std::wstring, HINSTANCE);
	/// Деструктор
	~App();

	/// Единственный экземпляр приложения
	static App * application;

public:
	/// Получить ширину
	inline int GetWndSizeX() { return right - left; }
	/// Получить высоту
	inline int GetWndSizeY() { return bottom - top; }
	/// Получить координаты
	inline int Top() { return top; }
	inline int Bottom() { return bottom; }
	inline int Right() { return right; }
	inline int Left() { return left; }
	/// Получить высоту заголовка
	inline int HeaderHeight() { return headerHeight; }
	/// Установить размер окна
	inline void SetWndSize(RECT &rect);
	/// Получить представление окна
	inline HWND GetWnd() { return window; }
	/// Получить все рабочие документы
	inline std::vector<Workspace*> GetWorkspaces();

	void InsertText(HWND);

	/// Установить рабочий документ
	void SetActiveWS(HWND hwnd, Workspace *, bool = true);
	/// Открыть документ
	void Open(HWND);
	/// Сохранить файл
	void SaveFile(HWND);
	/// Сохранить файл как
	void SaveFileAs(HWND);
	/// Отметить файл, как измененный
	void SetChanged();
	/// Удалить документ из приложения
	void DeleteFileFromApp();

	/// Создать новый пустой документ
	bool CreateNewWorkSpace(std::string name = "");
	/// Получить количество открытых документов
	inline int GetCountWindows();
	/// Получить объект приложения
	static App & Instance(int sX = 0, int sY = 0, std::wstring name = TEXT("Notepad"), HINSTANCE hInst = NULL);
	/// Показать окно
	void ShowDialog();
};

