#include "People.h"

HBITMAP People::hbitmap[3] = { LoadBitmap(GetModuleHandle(NULL),MAKEINTRESOURCE(IDB_PEOPLE)),
LoadBitmap(GetModuleHandle(NULL),MAKEINTRESOURCE(IDB_POLICE)),
LoadBitmap(GetModuleHandle(NULL),MAKEINTRESOURCE(IDB_THIEF)) }; // Предзадание битмапов



void People::OnPaint(HDC hdc) // Рисование
{
	HDC hMem = CreateCompatibleDC(hdc); // Создание схожего конткста
	HGDIOBJ hOld = SelectObject(hMem, hbitmap[role]); // Выбор в него битмапа
	TransparentBlt(hdc, X_coord, Y_coord, 32, 32, hMem,
		0, 0, 32, 32, RGB(255, 255, 255)); // Отрисовка с исплючением белого
	DeleteDC(hMem);
	DeleteObject(hOld);
	// Вывод числа кошельков
	TextOut(hdc, X_coord + 8, Y_coord + 8, std::to_string(cash_count).c_str(), std::to_string(cash_count).length());
}
