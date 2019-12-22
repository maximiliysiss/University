#pragma once
#include <cmath>

// Вектор для 2Д
struct Vector2D
{
	// Данные
	float x, y;

	Vector2D();
	Vector2D(float x, float y);
	Vector2D(float x1, float y1, float x2, float y2);
	Vector2D(Vector2D vec1, Vector2D vec2);

	void rotate(float angle);
	void rotate(Vector2D center, float cos, float sin);
	float cosAngleBetween(Vector2D vec);
	float scalar(Vector2D vec);
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

