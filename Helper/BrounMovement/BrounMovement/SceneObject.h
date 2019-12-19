#pragma once
#include <Windows.h>
#include "Vector2D.h"
#include "GravitySystem.h"
#pragma comment(lib,"MSIMG32.lib")
#pragma comment(lib, "Gdi32.lib")

class GravitySystem;

class SceneObject
{
public:
	SceneObject(HBITMAP hb, Vector2D position);
	SceneObject(WORD hb_name, Vector2D position);

	virtual void movement();
	virtual void setPosition(Vector2D vec);
	virtual void paint(HDC hdc);
	virtual void collisionAction(const SceneObject&);
	virtual bool isCollision(const SceneObject&);

	SceneObject(const SceneObject&) = delete;
	SceneObject(SceneObject&&) = delete;
	SceneObject& operator=(const SceneObject& sc) = delete;
	SceneObject& operator=(SceneObject&& sc) = delete;

	virtual ~SceneObject() {}
protected:
	Vector2D velocity;
	Vector2D startPos;
	Vector2D center;
	HBITMAP hBitmap;
	BITMAP bm_info;
};

