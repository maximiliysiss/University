#pragma once
#include <Windows.h>
#include "Vector2D.h"
#include "GravitySystem.h"
#pragma comment(lib,"MSIMG32.lib")
#pragma comment(lib, "Gdi32.lib")

class GravitySystem;

// Объект сцены
class SceneObject
{
public:
	SceneObject(HBITMAP hb, Vector2D position);
	SceneObject(WORD hb_name, Vector2D position);

	// Движение
	virtual void movement();
	// Установить положение
	virtual void setPosition(Vector2D vec);
	// Отрисовать
	virtual void paint(HDC hdc);
	// Обработка коллизии
	virtual void collisionAction(const SceneObject*);
	// Есть ли коллизия
	virtual bool isCollision(const SceneObject&);

	virtual ~SceneObject() {}

	// Установить скорость
	inline void setVelocity(Vector2D velocity) {
		this->velocity = velocity;
	}

	// Получить размеры
	inline int getWidth() const { return bm_info.bmWidth; }
	inline int getHeight() const { return bm_info.bmHeight; }
	// Получить положение, скорость
	inline Vector2D getCenter() const { return center; }
	inline Vector2D getVelocity() const { return velocity; }
protected:
	// Поля данные
	Vector2D velocity;
	Vector2D startPos;
	Vector2D center;
	HBITMAP hBitmap;
	BITMAP bm_info;
};

