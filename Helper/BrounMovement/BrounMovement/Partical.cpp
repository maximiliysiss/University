#include "Partical.h"
#define PI 3.14159265f 

Partical::Partical(HBITMAP hb, Vector2D position, float mass)
	:SceneObject(hb, position), mass(mass)
{
	velocity = Vector2D(-5 + (float)(rand() % 10), -5 + (float)(rand() % 10));
	velocity.normalize();
}

Partical::Partical(WORD hb_name, Vector2D position, float mass)
	: SceneObject(hb_name, position), mass(mass)
{
	velocity = Vector2D(-5 + (float)(rand() % 10), -5 + (float)(rand() % 10));
	velocity.normalize();
}

void Partical::collisionAction(const SceneObject* sc)
{
	// Если это Partical
	auto partical = dynamic_cast<const Partical*>(sc);
	if (partical) {
		// Угол между скоростями
		auto cosAngle = sc->getVelocity().cosAngleBetween(velocity);
		auto resAngle = (90.0f - acos(cosAngle) * 180.0f / PI) * 2;
		// Повернем вектор
		velocity.rotate(resAngle * PI / 180.0f);
		// Новая скорость
		speed += getMass() - partical->getMass();
		speed = 1 + (int)round(speed) % 10;
	}
}

// Есть коллизия между кругами
bool Partical::isCollision(const SceneObject& sc)
{
	return Vector2D(center, sc.getCenter()).length() - sc.getWidth() <= 0;
}

// Двжиение
void Partical::movement()
{
	auto metaData = MetaApplicationInfo::getInstance();
	// Следующая позиция
	auto nextPosition = center + velocity * (1 / mass * speed);
	// Если выходить за пределы, то изменим направление
	if (nextPosition.x < 0 || nextPosition.y < 0 || nextPosition.x >= metaData.getWidht() || nextPosition.y >= metaData.getHeight()) {
		if (nextPosition.x < 0 || nextPosition.x >= metaData.getWidht())
			velocity.x *= -1;
		if (nextPosition.y < 0 || nextPosition.y >= metaData.getHeight())
			velocity.y *= -1;
	}
	// Новая позиция
	center = center + velocity * (1 / mass * speed);
	startPos = center - Vector2D((float)getWidth() / 2, (float)getHeight() / 2);
}
