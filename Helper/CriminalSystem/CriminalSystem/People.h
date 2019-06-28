#pragma once
#include <Windows.h>
#include <iostream>
#include <string>
#include "resource.h"
#include <ctime>

enum ROLE { PEOPLE = 0, POLICEMAN = 1, THIEF = 2 };

class People
{
	int cash_count; // Кол кошельков
	float X_coord; // Позиция на поле
	float Y_coord;
	float sizeX_p, sizeY_p; // Размеры поля
	ROLE role; // Роль
	static HBITMAP hbitmap[3]; // Битмапы для отрисовки
	std::pair<float, float> Velocity; // Ускорение
public:
	void OnPaint(HDC hdc); // Отрисовка
	People() {}
	void SetCoord(int sizeX, int sizeY, int sizeX_f, int sizeY_f) // Установка координат
	{
		sizeX_p = sizeX_f;
		sizeY_p = sizeY_f;
		X_coord = sizeX;
		Y_coord = sizeY;
		Velocity.first = -100 + rand() % 99;
		Velocity.second = -100 + rand() % 99;
		Normal();
		cash_count = 0;
	}
	void Normal() // Вычсчитывание нормали для вектора перемещения
	{
		float l = sqrt(Velocity.first*Velocity.first + Velocity.second*Velocity.second);
		Velocity.first /= l;
		Velocity.second /= l;
	}
	void SetRole(ROLE r) // Установка роли
	{
		role = r;
		if (r == ROLE::PEOPLE)
			cash_count += 10 + rand() % 15;
	}
	void SetCash(int c) // Добавить кошельков
	{
		cash_count += c;
	}
	int GetCash() // Отобрать кошельки и получить их
	{
		int temp = cash_count;
		cash_count = 0;
		return temp;
	}
	void Move() // Движение
	{
		if (X_coord >= sizeX_p - 50 || X_coord <= 10)
			Velocity.first *= -1;
		if (Y_coord >= sizeY_p - 70 || Y_coord <= 10)
			Velocity.second *= -1;
		X_coord += Velocity.first;
		Y_coord += Velocity.second;
	}
	std::pair<float, float> GetCenter() // Получить координаты центра
	{
		return std::pair<int, int>(X_coord + 16, Y_coord + 16);
	}
	bool Connect(float X, float Y) // Проверка на пересечение
	{
		return sqrt((X - X_coord - 16)*(X - X_coord - 16) + (Y - Y_coord - 16)*(Y - Y_coord - 16)) - 32 <= 0;
	}
	ROLE GetRole() // Получить роль
	{
		return role;
	}
	int GetCashInfo() // Получить количество кошельков
	{
		return cash_count;
	}
};
