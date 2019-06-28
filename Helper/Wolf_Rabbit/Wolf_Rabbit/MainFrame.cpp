#include "Objects.h"

const char * AppClassName = "MainFrame"; // класс окна

const  UINT TIMER_FRAME = 10; // Таймер

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam); // Обработчик сообщений

int WINAPI WinMain(HINSTANCE hInst, HINSTANCE hPrev, LPSTR cmd, int nCmd) // Главная функция
{
	srand(time(0));
	HWND hwnd; // Окно
	MSG msg; // Сообщение
	WNDCLASS wc; // Класс окна
	wc.cbClsExtra = wc.cbWndExtra = 0; // Настройки класса окна
	wc.hbrBackground = (HBRUSH)(BLACK_BRUSH);
	wc.hCursor = LoadCursor(NULL, IDC_ARROW); // Курсор
	wc.hIcon = LoadIcon(hInst, IDI_APPLICATION);
	wc.hInstance = hInst;
	wc.lpfnWndProc = WndProc;
	wc.lpszClassName = AppClassName;
	wc.lpszMenuName = NULL;
	wc.style = CS_VREDRAW | CS_HREDRAW;
	RegisterClass(&wc); // Регистрация окна
	hwnd = CreateWindow(AppClassName, "RoadCross", WS_POPUP, 0, 0, 600, 600, NULL, NULL, hInst, NULL); // Создание окна
	SetTimer(hwnd, TIMER_FRAME, 15, NULL); // Установка таймера
	GodManager::GetInstace().SetBackGround(IDB_BACKGROUND); // Добавить фон
	GodManager::GetInstace().AddElements(hwnd); // Добавить функц элементы
	GodManager::GetInstace().ActiveElements(hwnd); // Активировать элементы
	ShowWindow(hwnd, SW_SHOW);
	UpdateWindow(hwnd);
	while (GetMessage(&msg, 0, 0, 0)) // Обработка сообщений
	{
		if (!IsDialogMessage(hwnd, &msg))
		{
			TranslateMessage(&msg);
			DispatchMessage(&msg);
		}
	}
	return msg.message;
}

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	HDC hdc;
	PAINTSTRUCT ps;
	switch (msg)
	{
	case WM_COMMAND:
		switch (wParam)
		{
		case 7: // Обработка кнопки
			if (GodManager::GetInstace().GetInfoEdits(hwnd)) // Если удалось считать информацию с Editов
			{
				GodManager::GetInstace().StartGame(); // Начать игру
				GodManager::GetInstace().DisabledElements(hwnd); // Отключить элементы
			}
			break;
		}
		break;
	case WM_TIMER: // Сработал таймер
		switch (wParam)
		{
		case TIMER_FRAME:
			InvalidateRect(hwnd, NULL, true); // Перерисовать
			break;
		}
		break;
	case WM_ERASEBKGND:
		return FALSE;
	case WM_PAINT: // Обработка отрисовки
		hdc = BeginPaint(hwnd, &ps); // Начало отрисовки
		GodManager::GetInstace().Paint(ps, 600, 600, hwnd);
		EndPaint(hwnd, &ps); // Конец отрисовки
		break;
	case WM_DESTROY: // Закрытие
		PostQuitMessage(NULL);
		return 0;
	default:
		return DefWindowProc(hwnd, msg, wParam, lParam);
	}
	return 0;
}