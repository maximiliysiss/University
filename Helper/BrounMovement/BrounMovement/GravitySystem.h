#pragma once
#include "SceneObject.h"
#include <vector>

class SceneObject;

class GravitySystem
{
	static GravitySystem* gravitySystem;
	GravitySystem() {}

	std::vector<SceneObject*> sceneObjects;
public:
	static GravitySystem& getInstance();
	void addObject(SceneObject* obj);
	void clear();
	void calculate() const;
};

