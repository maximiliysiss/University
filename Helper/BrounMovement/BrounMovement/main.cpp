﻿#include <iostream>
#include <Windows.h>
#include <list>
#include "GravitySystem.h"
#include "MetaApplicationInfo.h"
#include "resource.h"
#include "Partical.h"
#include <algorithm>
#include <functional>
#include <ctime>

// Тамеры и частота обновления
// Для графики
#define TIMER_FRAME 42
#define TIMER_ELAPSE (1.0f/60.0f * 100)
// Для математики
#define TIMER_MATH 43
#define TIMER_MATH_ELAPSE (1.0f/24.0f*100)

// Размеры окна
const UINT wndWidth = 640;
const UINT wndHeight = 480;

// Название приложение
const char appName[] = "Broun Movement";
// Изображение для заднего фона
HBITMAP backGround;

// Создать окно
HWND createWindow();
// Обработчик для окна
LRESULT CALLBACK loop(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam);
// Отрисовать окно
void paintFrame(PAINTSTRUCT& ps);
// освободить ресурсы
void freeResources();

// объекты сцены
std::list<SceneObject*> sceneObjects;
// массы
float* mass = nullptr;

int main() {
	srand((UINT)time(0));
	std::cout << "Hello! It's program Broun Movement" << std::endl;
	try {

		//Получим задний фон
		backGround = LoadBitmap(GetModuleHandle(NULL), MAKEINTRESOURCE(IDB_BG));

		// цикл для создания разных симуляций
		while (true) {
			std::cout << "Enter count of partical: ";
			// количество
			int n;  std::cin >> n;
			if (n == 0)
				break;
			mass = new float[n] {0};
			// ввод масс
			for (int i = 0; i < n; ++i) {
				std::cout << "Mass of partiacal #" << i + 1 << ": ";
				std::cin >> mass[i];
			}

			//for (int i = 0; i < n; ++i) {
			//	mass[i] = rand() % 10;
			//}

			std::cout << std::endl << "End of init data\n" << "Start create simulation\n";

			// Создадим окно
			HWND hwnd = createWindow();

			// Размеры окна
			RECT wndRect;
			GetClientRect(hwnd, &wndRect);

			std::cout << "Create particals\n";

			// Система столкновений
			auto grSystem = GravitySystem::getInstance();

			// Получим размеры матрицы
			const int offsetSize = 13;
			const int colSize = wndRect.right / offsetSize;
			const int rowSize = wndRect.bottom / offsetSize;
			// Установить матрицу
			MetaApplicationInfo::getInstance().setSize(wndRect.right, wndRect.bottom);
			const int size = colSize * rowSize;
			int* matrixScreen = new int[size] { 0 };
			// Заполним первые n 
			std::fill(matrixScreen, matrixScreen + (n > size ? size : n), 1);
			// Перемещаем
			std::random_shuffle(matrixScreen, matrixScreen + size);
			// Проход по матрице
			for (int i = 0, k = 0; i < size; i++) {
				// Если тут есть данные
				if (matrixScreen[i]) {
					std::cout << "#" << k + 1 << " " << (i % colSize) * offsetSize << " " << (i / colSize) * offsetSize << std::endl;
					// Добавим новую частицу
					Partical* newPartical = new Partical(IDB_PART, Vector2D((float)(i % colSize) * offsetSize, (float)(i / colSize) * offsetSize), mass[k++]);
					sceneObjects.push_back(newPartical);
					grSystem.addObject(newPartical);
				}
			}

			// Удалим лишнее
			delete[] matrixScreen;

			std::cout << "End create particals\n";

			// Цикл для запуска окна
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

			// Освободим ресурсы
			freeResources();
			std::cout << "End simulation\n\n";
		}
	}
	catch (std::exception ex) {
		std::cout << "Error: " << ex.what() << std::endl;
	}

	// Освободим ресурсы
	freeResources();
	std::cout << "Success end of program\n";
	return 0;
}

// Создадим окно
HWND createWindow()
{
	// Получим обработчик для приложения
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

// Цикл окна
LRESULT CALLBACK loop(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam) {
	HDC hdc;
	PAINTSTRUCT ps;
	switch (msg)
	{
	case WM_TIMER: // Сработал таймер
		switch (wParam)
		{
		case TIMER_FRAME: // Графика
			// Перерисуем
			InvalidateRect(hwnd, NULL, true);
			break;
		case TIMER_MATH: // Математика
			// Пересчитаем движение
			std::for_each(sceneObjects.begin(), sceneObjects.end(), [](SceneObject* obj) {obj->movement(); });
			// Столкновение
			GravitySystem::getInstance().calculate();
			break;
		}
		break;
		// Корректный вызов для пересчета
	case WM_ERASEBKGND:
		return TRUE;
		// Отрисовка
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

// Отрисуем окно
void paintFrame(PAINTSTRUCT& ps)
{
	HDC hMemDC, hTempDC; // Теневой буффер
	HGDIOBJ hMemBmp, hSysBmp;
	HBRUSH hBrush = (HBRUSH)WHITE_BRUSH;
	hMemDC = CreateCompatibleDC(ps.hdc); // Создать конекст схожии с основным
	hMemBmp = CreateCompatibleBitmap(ps.hdc, wndWidth, wndHeight); // Создание битмап для фона
	hSysBmp = SelectObject(hMemDC, hMemBmp);
	SelectObject(hMemDC, hBrush);
	hTempDC = CreateCompatibleDC(hMemDC);
	SelectObject(hTempDC, backGround);
	BitBlt(hMemDC, 0, 0, wndWidth, wndHeight, hTempDC, 0, 0, SRCCOPY); // Копироание одного контекста в другой контекст
	DeleteDC(hTempDC); // Удаление временного контекста

	// Отрисуем частицы
	std::for_each(sceneObjects.begin(), sceneObjects.end(), [&](SceneObject* obj) {obj->paint(hMemDC); });

	BitBlt(ps.hdc, 0, 0, wndWidth, wndHeight, hMemDC, 0, 0, SRCCOPY); // Отправка в основной контекст
	SelectObject(hMemDC, hSysBmp);
	DeleteObject(hMemBmp); // Удаление Битмапа
	DeleteObject(hSysBmp);
	DeleteDC(hMemDC); // Удаление контекстов
	DeleteObject(hBrush);
}

// Освободим ресурсы
void freeResources()
{
	std::cout << "Free resources\n";
	// Удалим объекты
	for (auto p : sceneObjects)
		delete p;
	sceneObjects.clear();
	GravitySystem::getInstance().clear();

	// Удалим массы
	if (mass) {
		delete[] mass;
		mass = nullptr;
	}
}

