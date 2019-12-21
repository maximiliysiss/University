#include "Vector2D.h"

Vector2D::Vector2D()
	:x(0), y(0)
{
}

Vector2D::Vector2D(float x, float y)
	: x(x), y(y)
{
}

Vector2D::Vector2D(float x1, float y1, float x2, float y2)
	: x(x2 - x1), y(y2 - y1)
{
}

Vector2D::Vector2D(Vector2D vec1, Vector2D vec2)
{
	x = vec2.x - vec1.x;
	y = vec2.y - vec1.y;
}

void Vector2D::rotate(Vector2D center, float angle)
{
	//angle = angle * 3.14f / 180.0f;
	Vector2D t(center, *this);
	float xt = t.x;
	t.x = xt * cos(angle) - t.y * sin(angle);
	t.y = xt * sin(angle) + t.y * cos(angle);
	this->x = center.x + t.x;
	this->y = center.y + t.y;
}

void Vector2D::rotate(Vector2D center, float cos, float sin)
{
	Vector2D t(center, *this);
	float xt = t.x;
	t.x = xt * cos - t.y * sin;
	t.y = xt * sin + t.y * cos;
	this->x = center.x + t.x;
	this->y = center.y + t.y;
}

float Vector2D::cosAngleBetween(Vector2D vec) {
	return this->scalar(vec) / (this->length() * vec.length());
}

float Vector2D::scalar(Vector2D vec) {
	return this->x * vec.x + this->y * vec.y;
}

void Vector2D::normalize()
{
	auto l = length();
	x /= l;
	y /= l;
}

float Vector2D::length()
{
	return sqrt(x * x + y * y);;
}

Vector2D& Vector2D::operator=(const Vector2D& vec)
{
	x = vec.x;
	y = vec.y;
	return *this;
}

Vector2D Vector2D::operator-(const Vector2D& vec)
{
	return Vector2D(x - vec.x, y - vec.y);
}

Vector2D Vector2D::operator+(const Vector2D& vec)
{
	return Vector2D(x + vec.x, y + vec.y);;
}

Vector2D Vector2D::operator*(int i)
{
	return Vector2D(x * i, y * i);
}

Vector2D Vector2D::operator*(float i)
{
	return Vector2D(x * i, y * i);
}

void Vector2D::operator-=(const Vector2D& vec)
{
	x -= vec.x;
	y -= vec.y;
}

void Vector2D::operator+=(const Vector2D& vec)
{
	x += vec.x;
	y += vec.y;
}

bool Vector2D::operator==(const Vector2D& vec)
{
	return x == vec.x && y == vec.y;
}
