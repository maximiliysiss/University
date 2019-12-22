#pragma once
#include "SceneObject.h"
#include <vector>

class SceneObject;

// Система гравитации (Singleton)
class GravitySystem
{
	static GravitySystem* gravitySystem;
	GravitySystem() {}

	// Список объектов
	std::vector<SceneObject*> sceneObjects;
public:
	static GravitySystem& getInstance();
	// Добавить объект
	void addObject(SceneObject* obj);
	// Очистим
	void clear();
	// Расчет
	void calculate() const;
};

