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
	virtual void collisionAction(const SceneObject*);
	virtual bool isCollision(const SceneObject&);

	virtual ~SceneObject() {}

	inline void setVelocity(Vector2D velocity) {
		this->velocity = velocity;
	}

	inline int getWidth() const { return bm_info.bmWidth; }
	inline int getHeight() const { return bm_info.bmHeight; }
	inline Vector2D getCenter() const { return center; }
	inline Vector2D getVelocity() const { return velocity; }
protected:
	Vector2D velocity;
	Vector2D startPos;
	Vector2D center;
	HBITMAP hBitmap;
	BITMAP bm_info;
};

