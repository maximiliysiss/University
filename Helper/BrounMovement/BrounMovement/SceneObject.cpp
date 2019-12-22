#include "SceneObject.h"

SceneObject::SceneObject(HBITMAP hb, Vector2D position)
	:hBitmap(hb), startPos(position)
{
	GetObject(hBitmap, sizeof(BITMAP), &bm_info);
	center.x = startPos.x + bm_info.bmWidth / 2;
	center.y = startPos.y + bm_info.bmHeight / 2;
	GravitySystem::getInstance().addObject(this);
}

SceneObject::SceneObject(WORD hb_name, Vector2D position)
	: SceneObject(LoadBitmap(GetModuleHandle(NULL), MAKEINTRESOURCE(hb_name)), position)
{
}

void SceneObject::movement()
{
	startPos += velocity;
}

void SceneObject::setPosition(Vector2D vec)
{
	startPos = vec;
}

void SceneObject::paint(HDC hdc)
{
	HDC hMem = CreateCompatibleDC(hdc); // Доп контекст
	HGDIOBJ hOld = SelectObject(hMem, hBitmap); // Выбор в него битмапа
	TransparentBlt(hdc, (int)startPos.x, (int)startPos.y, bm_info.bmWidth, bm_info.bmHeight, hMem, 0, 0, bm_info.bmWidth, bm_info.bmHeight, RGB(255, 255, 255)); // Отрисовка с исключением белого
	SelectObject(hMem, hOld);
	DeleteObject(hOld);
	DeleteDC(hMem);
}

void SceneObject::collisionAction(const SceneObject* obj)
{
}

bool SceneObject::isCollision(const SceneObject& obj)
{
	return false;
}
