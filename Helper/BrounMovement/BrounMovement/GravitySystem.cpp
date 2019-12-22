#include "GravitySystem.h"

GravitySystem* GravitySystem::gravitySystem = nullptr;

GravitySystem& GravitySystem::getInstance()
{
	if (gravitySystem)
		return *gravitySystem;
	gravitySystem = new GravitySystem();
	return *gravitySystem;
}

void GravitySystem::addObject(SceneObject* obj)
{
	sceneObjects.push_back(obj);
}

void GravitySystem::clear()
{
	sceneObjects.clear();
}

void GravitySystem::calculate() const
{
	for (unsigned i = 0; i < sceneObjects.size(); i++) {
		for (unsigned j = i + 1; j < sceneObjects.size(); j++) {
			if (sceneObjects[i]->isCollision(*sceneObjects[j])) {
				sceneObjects[i]->collisionAction(sceneObjects[j]);
				sceneObjects[j]->collisionAction(sceneObjects[i]);
			}
		}
	}
}
