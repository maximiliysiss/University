#pragma once
#include "SceneObject.h"
#include "MetaApplicationInfo.h"

class Partical :
	public SceneObject
{
	float mass;
	float speed{ 5 };
public:
	Partical(HBITMAP hb, Vector2D position, float mass);
	Partical(WORD hb_name, Vector2D position, float mass);

	void collisionAction(const SceneObject*) override;
	bool isCollision(const SceneObject&) override;

	virtual void movement() override;
	inline float getMass() const { return mass; }
};

