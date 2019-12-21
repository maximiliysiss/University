#pragma once
#include "SceneObject.h"
#include "MetaApplicationInfo.h"

class Partical :
	public SceneObject
{
	float mass;
	float speed{ 2 };
public:
	Partical(HBITMAP hb, Vector2D position, float mass);
	Partical(WORD hb_name, Vector2D position, float mass);

	virtual void movement() override;
};

