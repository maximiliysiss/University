#pragma once
#include <cmath>

struct Vector2D
{
	float x, y;

	Vector2D();
	Vector2D(float x, float y);
	Vector2D(float x1, float y1, float x2, float y2);
	Vector2D(Vector2D vec1, Vector2D vec2);

	void rotate(Vector2D center, float angle);
	void normalize();
	float length();

	Vector2D& operator=(const Vector2D& vec);
	Vector2D operator-(const Vector2D& vec);
	Vector2D operator+(const Vector2D& vec);
	Vector2D operator*(int i);
	Vector2D operator*(float i);
	void operator-=(const Vector2D& vec);
	void operator+=(const Vector2D& vec);
	bool operator==(const Vector2D& vec);
};

