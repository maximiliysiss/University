#pragma once
#include <Windows.h>
#include "People.h"
#include <vector>
#pragma comment(lib,"MSIMG32.lib")
#pragma comment(lib, "Gdi32.lib")


class Field // Класс поля
{
private:
	int sizeX; // Размеры поля
	int sizeY;
	int count_pl; // Количество игроков
	int offset; // Отступ для генерации позииции, чтобы не пересекались при появлении
	People * people; // Массив людей
public:
	// Конструктор
	Field(int count_p, int count_pm, int count_t, int sizeX, int sizeY, int off = 40) :count_pl(count_p + count_pm + count_t), sizeX(sizeX),
		sizeY(sizeY)
	{
		people = new People[count_pl];
		int index = 0;
		srand((UINT)time(NULL));
		std::vector<std::pair<float, float>> pos;
		for (int i = 1; i < sizeX / off - 1; i++)
			for (int j = 1; j < sizeY / off - 1; j++)
				pos.push_back(std::pair<float, float>(i, j));
		for (int i = 0; i < count_p; i++, index++) // Задание людей
		{
			int k = rand() % pos.size();
			people[index].SetCoord(pos[k].first*off, pos[k].second*off, sizeX, sizeY);
			people[index].SetRole(ROLE::PEOPLE);
			pos.erase(pos.begin() + k);
		}
		for (int i = 0; i < count_pm; i++, index++) // Задание полциции
		{
			int k = rand() % pos.size();
			people[index].SetCoord(pos[k].first*off, pos[k].second*off, sizeX, sizeY);
			people[index].SetRole(ROLE::POLICEMAN);
			pos.erase(pos.begin() + k);
		}
		for (int i = 0; i < count_t; i++, index++) // Задание воров
		{
			int k = rand() % pos.size();
			people[index].SetCoord(pos[k].first*off, pos[k].second*off, sizeX, sizeY);
			people[index].SetRole(ROLE::THIEF);
			pos.erase(pos.begin() + k);
		}
	}
	void OnPaint(HDC hdc) // Отрисовка
	{
		int police = 0;
		int thief = 0;
		int peop = 0;
		for (int i = 0; i < count_pl; i++) // Отрисовка людей и одновременный счет кошельков
		{
			people[i].OnPaint(hdc);
			if (people[i].GetRole() == ROLE::PEOPLE)
				peop += people[i].GetCashInfo();
			if (people[i].GetRole() == ROLE::POLICEMAN)
				police += people[i].GetCashInfo();
			if (people[i].GetRole() == ROLE::THIEF)
				thief += people[i].GetCashInfo();
		}
		// Вывод доп информации
		TextOut(hdc, 550, 0, (std::string("People is ") + std::to_string(peop)).c_str(), (std::string("People is ") + std::to_string(peop)).length());
		TextOut(hdc, 550, 20, (std::string("Police is ") + std::to_string(police)).c_str(), (std::string("People is ") + std::to_string(police)).length());
		TextOut(hdc, 550, 40, (std::string("Thief is ") + std::to_string(thief)).c_str(), (std::string("People is ") + std::to_string(thief)).length());
	}
	void Math()
	{
		for (int i = 0; i < count_pl; i++)
		{
			people[i].Move();
			for (int j = 0; j < count_pl; j++)
			{
				if (j != i)
					if (people[i].Connect(people[j].GetCenter().first, people[j].GetCenter().second))
					{
						if (people[i].GetRole() == ROLE::PEOPLE && people[j].GetRole() == ROLE::POLICEMAN)
							people[i].SetCash(people[j].GetCash());
						if (people[i].GetRole() == ROLE::PEOPLE && people[j].GetRole() == ROLE::THIEF)
							people[j].SetCash(people[i].GetCash());
						if (people[i].GetRole() == ROLE::THIEF && people[j].GetRole() == ROLE::POLICEMAN)
							people[j].SetCash(people[i].GetCash());
					}
			}
		}
	}
};