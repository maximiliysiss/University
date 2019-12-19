#include <iostream>
#include <Windows.h>
#include <list>
#include "GravitySystem.h"
#include "SceneObject.h"
#include <algorithm>
#include <functional>

#define TIMER_FRAME 42
#define TIMER_ELAPSE (1.0f/60.0f * 100)
#define TIMER_MATH 43
#define TIMER_MATH_ELAPSE (1.0f/24.0f*100)

const UINT wndWidth = 640;
const UINT wndHeight = 480;

const char appName[] = "Broun Movement";

HWND createWindow();
LRESULT CALLBACK loop(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam);
void paintFrame(PAINTSTRUCT& ps);

std::list<SceneObject> sceneObjects;

int main() {
	std::cout << "Hello! It's program Broun Movement" << std::endl;
	float* mass = nullptr;
	try {
		std::cout << "Enter count of partical: ";
		int n; std::cin >> n;
		if (n == 0)
			throw std::exception("No have paricals");
		mass = new float[n] {0};
		for (int i = 0; i < n; ++i) {
			std::cout << "Mass of partiacal #" << i + 1 << ": ";
			std::cin >> mass[i];
		}

		std::cout << std::endl << "End of init data\n" << "Start create simulation\n";

		HWND hwnd = createWindow();
		ShowWindow(hwnd, SW_SHOW);
		UpdateWindow(hwnd);
		MSG msg; // Сообщение
		while (GetMessage(&msg, 0, 0, 0))
		{
			if (!IsDialogMessage(hwnd, &msg))
			{
				TranslateMessage(&msg);
				DispatchMessage(&msg);
			}
		}

		std::cout << "End simulation\n";
	}
	catch (std::exception ex) {
		std::cout << "Error: " << ex.what() << std::endl;
	}
	std::cout << "Free resources\n";
	if (mass)
		delete[] mass;
	std::cout << "Success end of program\n";
	return 0;
}

HWND createWindow()
{
	HINSTANCE hInstance = GetModuleHandle(0);
	WNDCLASS wc; // Класс окна
	wc.cbClsExtra = wc.cbWndExtra = 0;
	wc.hbrBackground = (HBRUSH)(BLACK_BRUSH);
	wc.hCursor = LoadCursor(NULL, IDC_ARROW); // Курсор
	wc.hIcon = LoadIcon(hInstance, IDI_APPLICATION);
	wc.hInstance = hInstance;
	wc.lpfnWndProc = loop;
	wc.lpszClassName = appName;
	wc.lpszMenuName = NULL;
	wc.style = CS_VREDRAW | CS_HREDRAW;
	RegisterClass(&wc); // Регистрация окна
	auto hwnd = CreateWindow(appName, appName, WS_OVERLAPPEDWINDOW ^ WS_SIZEBOX ^ WS_MAXIMIZEBOX, 0, 0, wndWidth, wndHeight, NULL, NULL, hInstance, NULL); // Создание окна
	SetTimer(hwnd, TIMER_FRAME, (UINT)TIMER_ELAPSE, NULL); // Установка таймера
	SetTimer(hwnd, TIMER_MATH, (UINT)TIMER_MATH_ELAPSE, NULL); // Установка таймера
	return hwnd;
}

LRESULT CALLBACK loop(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam) {
	HDC hdc;
	PAINTSTRUCT ps;
	switch (msg)
	{
	case WM_TIMER: // Сработал таймер
		switch (wParam)
		{
		case TIMER_FRAME:
			InvalidateRect(hwnd, NULL, true);
			break;
		case TIMER_MATH:
			std::for_each(sceneObjects.begin(), sceneObjects.end(), std::bind(&SceneObject::movement, std::placeholders::_1));
			GravitySystem::getInstance().calculate();
			break;
		}
		break;
	case WM_ERASEBKGND:
		return TRUE;
	case WM_PAINT:
		hdc = BeginPaint(hwnd, &ps);
		paintFrame(ps);
		EndPaint(hwnd, &ps);
		break;
	case WM_DESTROY: // Закрытие
		PostQuitMessage(NULL);
		return 0;
	default:
		return DefWindowProc(hwnd, msg, wParam, lParam);
	}
	return 0;
}

void paintFrame(PAINTSTRUCT& ps)
{
	HDC hMemDC; // Теневой буффер
	hMemDC = CreateCompatibleDC(ps.hdc); // Создать конекст схожии с основным

	HPEN pen = CreatePen(1, 1, WHITE_PEN);
	SelectObject(hMemDC, pen);
	LineTo(hMemDC, 50, 50);

	std::for_each(sceneObjects.begin(), sceneObjects.end(), std::bind(&SceneObject::paint, std::placeholders::_1, hMemDC));

	BitBlt(ps.hdc, 0, 0, wndWidth, wndHeight, hMemDC, 0, 0, SRCCOPY); // Отправка в основной контекст

	DeleteDC(hMemDC); // Удаление контекстов
}

