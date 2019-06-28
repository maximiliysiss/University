#include "Field.h"
#include <ctime>

LRESULT CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM); //Обработчик

const char * AppClassName = "CriminalSystem"; // Имя класса
const char * WndName = "Criminal System";

Field field(10, 10, 2, 700, 700); // Поле

HBITMAP BackGround; // Картинка фона

const UINT TIMER_FRAME = 10; // Обновление

int WINAPI WinMain(HINSTANCE hInst, HINSTANCE hprev, LPSTR cmd, int nCmd) // Главная функция
{
	MSG msg; // Собщения
	HWND hwnd; // Окно
	BackGround = LoadBitmap(GetModuleHandle(NULL), MAKEINTRESOURCE(IDB_BK)); // Загрзка из ресурсов фона
	WNDCLASS wc; // Класс окна
	wc.cbClsExtra = wc.cbWndExtra = 0;
	wc.hbrBackground = (HBRUSH)(WHITE_BRUSH); // Заливка
	wc.hCursor = LoadCursor(NULL, IDC_ARROW); // Курсор
	wc.hIcon = LoadIcon(hInst, IDI_APPLICATION); // Иконка
	wc.hInstance = hInst; // АЙДИ
	wc.lpfnWndProc = WndProc; // Функция обработки
	wc.lpszClassName = AppClassName; // Имя класса
	wc.lpszMenuName = NULL; // Меню
	wc.style = CS_VREDRAW | CS_HREDRAW; // Стиль
	RegisterClass(&wc); // Регитсрация класса
	// Создание окна
	hwnd = CreateWindow(AppClassName, WndName, WS_OVERLAPPEDWINDOW, 0, 0, 700, 700, NULL, NULL, hInst, NULL);
	SetTimer(hwnd, TIMER_FRAME, 10, NULL); // Устанвока таймера обновления
	ShowWindow(hwnd, SW_SHOW); // Показать окно
	UpdateWindow(hwnd); //Обновить окно
	while (GetMessage(&msg, 0, 0, 0)) // Обработка всех сообщений
	{
		if (!IsDialogMessage(hwnd, &msg))
		{
			TranslateMessage(&msg);
			DispatchMessage(&msg);
		}
	}
	return msg.message;
}

void OnPaint(PAINTSTRUCT & ps) // Отрисовка
{
	HDC hMemDC, hTempDC; // Теневой буффер
	HGDIOBJ hMemBmp, hSysBmp;
	hMemDC = CreateCompatibleDC(ps.hdc); // Создать конекст схожии с основным
	hMemBmp = CreateCompatibleBitmap(ps.hdc, 700, 700); // Создание битмап для фона
	SelectObject((HDC)hMemBmp, (HBRUSH)WHITE_BRUSH);
	hSysBmp = SelectObject(hMemDC, hMemBmp);
	hTempDC = CreateCompatibleDC(hMemDC);
	SelectObject(hTempDC, BackGround);
	BitBlt(hMemDC, 0, 0, 700, 700, hTempDC, 0, 0, SRCCOPY); // Копироание одного контекста в другой контекст
	DeleteDC(hTempDC); // Удаление временного контекста
	field.OnPaint(hMemDC); // Отрисовка поля

	BitBlt(ps.hdc, 0, 0, 700, 700, hMemDC, 0, 0, SRCCOPY); // Отправка в основной контекст
	SelectObject(hMemDC, hSysBmp);
	DeleteObject(hMemBmp); // Удаление Битмапа
	DeleteObject(hSysBmp);
	DeleteDC(hMemDC); // Удаление контекстов
}

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wparam, LPARAM lparam) // Обработчик
{
	HDC hdc; // контекст
	PAINTSTRUCT ps; // Структура для рисования
	switch (msg)
	{
	case WM_TIMER: // Обработка таймера
		switch (wparam)
		{
		case TIMER_FRAME:
			field.Math(); // Просчеты
			InvalidateRect(hwnd, NULL, true); // Перерисовка
			break; 
		}
		break;
	case WM_PAINT: // Отрисовка
		hdc = BeginPaint(hwnd, &ps); // Начало рисовки
		OnPaint(ps);
		EndPaint(hwnd, &ps); //  Конец рисовки
		break;
	case WM_CLOSE: // Закрытие приложения
		PostQuitMessage(NULL);
		return 0;
	default:
		return DefWindowProc(hwnd, msg, wparam, lparam); // Если ничего не произошлож
	}
	return 0;
}