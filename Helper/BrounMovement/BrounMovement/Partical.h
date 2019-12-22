#pragma once
#include "SceneObject.h"
#include "MetaApplicationInfo.h"

// Частица
class Partical :
	public SceneObject
{
	// Масса
	float mass;
	// Скорость
	float speed{ 5 };
public:

	Partical(HBITMAP hb, Vector2D position, float mass);
	Partical(WORD hb_name, Vector2D position, float mass);

	// Обработка коллизии
	void collisionAction(const SceneObject*) override;
	// Есть ли коллизия
	bool isCollision(const SceneObject&) override;

	// Движение
	virtual void movement() override;
	// Получить массу
	inline float getMass() const { return mass; }
};

