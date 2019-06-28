#include "Objects.h"

GodManager * GodManager::manager = nullptr; // определение статики

HBITMAP Object::hbitmaps[2]{ LoadBitmap(GetModuleHandle(NULL),MAKEINTRESOURCE(IDB_WOLF)),
LoadBitmap(GetModuleHandle(NULL),MAKEINTRESOURCE(IDB_RABBIT)) }; // статика битмапов

UINT GodManager::IDCsEdit[7]{ 0,1,2,3,4,5,6 }; // статика для ИД Editов
UINT GodManager::Button_ID = 7; // статика ИД кнопки


void GodManager::DeleteObjectofField(Object* obj) // Удалить объект со сцены
{
	int i;
	for (i = 0; i < objects.size(); i++)
		if (obj->GetID() == objects[i]->GetID())
			break;
	if (i == objects.size())
		return;
	objects.erase(objects.begin() + i);
}

void GodManager::GenerateObjects() // Сгенерировать объекты в память по количеству
{
	if (objects.size() != 0)
	{
		for (int i = 0; i < objects.size(); i++)
		{
			objects[i]->~Object();
			objects.erase(objects.begin() + i);
			i--;
		}
	}
	index = 0;
	for (int i = 0; i < count_wolf; i++)
		objects.push_back(new Wolf(index++, settings.time_eat_wolf, settings.time_death_wolf, settings.time_new_wolf));
	for (int i = 0; i < count_rabbit; i++)
		objects.push_back(new Rabbit(index++, settings.time_death_rabbit, settings.time_new_rabbit));
}

void GodManager::PaintGame(HDC hdc, HWND hwnd) // Отрисовать игру
{
	int count_r = 0, count_w = 0, a_r = 0, a_w = 0;
	for (int i = 0; i < objects.size(); i++) // Подсчет количества для вывод статистики
	{
		if (typeid(Rabbit) == typeid(*objects[i]))
		{
			count_r++;
			a_r += objects[i]->age;
		}
		else
		{
			count_w++;
			a_w += objects[i]->age;
		}
		objects[i]->Show(hdc);
		objects[i]->Moving(objects);
	}
	std::string info_r = "", info_w = "";
	if (count_r != 0)
		info_r = "Rabbit count/average age - " + std::to_string(count_r) + "/" + std::to_string((float)a_r / (float)count_r);
	if (count_w != 0)
		info_w = "Wolf count/average age - " + std::to_string(count_w) + "/" + std::to_string((float)a_w / (float)count_w);
	TextOut(hdc, 350, 20, info_r.c_str(), info_r.length());
	TextOut(hdc, 350, 40, info_w.c_str(), info_w.length());
	if (count_r + count_w == 0) // Если количество элементов =0
	{
		this->StopGame();
		this->ActiveElements(hwnd);
	}
}

void Wolf::Action(Object* obj) // Действия волков по отношению к зайцам
{
	if (typeid(Rabbit) == typeid(*obj))
	{
		GodManager::GetInstace().DeleteObjectofField(obj);
		this->last_eat = std::chrono::high_resolution_clock::now();
	}
}