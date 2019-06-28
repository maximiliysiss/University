#pragma once
#include <Windows.h> // библиотеки
#include <string>
#include "resource.h"
#include <chrono>
#pragma comment(lib,"MSIMG32.lib")
#pragma comment(lib, "Gdi32.lib")

class Operator;
class Object;
class Semaphore;
class Bus;
class Car;

typedef Object* BackGround; // определение фона

class SystemManage // Системный менеджер - бог управления всем
{
	int counter; // количество объектов
	std::chrono::steady_clock::time_point time_span; // время последнего переключения светофора
	enum { RELOAD = 5 }; // переключение светофора через 
	Semaphore ** semaphore; // светофоры
	Object ** objects; // все машины
	BackGround background; // фон
	int count_object; // количество объектов (доп)
	int startpos[8]; // стартовые позиции
	SystemManage(int s_count, int c_count, int b_count); // конструктор
	static SystemManage * systemmanager; // Синглетон
	void Movement(); // Движение
public:
	void TurnSph(int num); // Вкл светофор номер num
	void OnPaint(PAINTSTRUCT& ps, int width, int height); // Отрисовать
	static SystemManage & GetInstace() // Получить ссылку
	{
		if (!systemmanager)
			systemmanager = new SystemManage(4, 6, 4);
		return *systemmanager;
	}
	int * GetStartPosInfo() { return startpos; }
};


class Object // класс объекта
{
	friend Operator;
public:
	bool HavePoint(std::pair<float, float> point, Object & obj) // Проверка на принадлежность точки объекту
	{
		return point.first >= obj.position.first && point.first <= obj.position.first + obj.bm.bmWidth
			&& point.second >= obj.position.second && point.second <= obj.position.second + obj.bm.bmHeight;
	}
	Object(int type_) :type(type_) // конструткор
	{
		GetObject(bitmaps[type], sizeof(BITMAP), &bm); // получение информации об объекте
		DeleteObject(hbitmap); // удаление
		hbitmap = (HBITMAP)CopyImage(bitmaps[type], IMAGE_BITMAP, bm.bmWidth, bm.bmHeight, NULL); // копирование из статичного поля
		velocity.first = velocity.second = position.first = position.second = 0;
	}
	HBITMAP * GetHB() { return &hbitmap; }
	virtual void Move(Semaphore ** sems, Object ** objects, int count_obj, int extra) {} // Перемещение
	virtual void OnPaint(HDC hdc) {} // Отрисовка
	static HBITMAP bitmaps[5]; // Статичные битмапы
	int line; // линия перемещения
protected:
	void ReCenter()
	{
		center.first = position.first + bm.bmWidth / 2;
		center.second = position.second + bm.bmHeight / 2;
	} // Пересчитать центр
	bool Collision(Object& obj, int coef) // Просчитать наличие коллизии (пересечения)
	{
		if (velocity.first > 0)
			return Object::HavePoint(std::pair<float, float>(this->center.first + this->bm.bmWidth / 2 + coef, this->center.second), obj);
		if (velocity.first < 0)
			return Object::HavePoint(std::pair<float, float>(this->center.first - this->bm.bmWidth / 2 - coef, this->center.second), obj);
		if (velocity.second > 0)
			return Object::HavePoint(std::pair<float, float>(this->center.first, this->center.second + this->bm.bmHeight / 2 + coef), obj);
		if (velocity.second < 0)
			return Object::HavePoint(std::pair<float, float>(this->center.first, this->center.second - this->bm.bmHeight / 2 - coef), obj);
		return false;
	}
	std::pair<float, float> position; // позиция
	std::pair<float, float> velocity; // ускорение
	std::pair<float, float> center; // центр
	void Show(HDC hdc) // Отобразить
	{
		HDC hMem = CreateCompatibleDC(hdc);
		HGDIOBJ hOld = SelectObject(hMem, hbitmap);
		BitBlt(hdc, (int)position.first, (int)position.second, bm.bmWidth, bm.bmWidth, hMem, 0, 0, SRCCOPY);
		DeleteDC(hMem);
		DeleteObject(hOld);
	}
	void ShowTransparent(HDC hdc, COLORREF color_erase) // Отобразить с исключением цвета
	{
		HDC hMem = CreateCompatibleDC(hdc); // Создать схожий контекст
		HGDIOBJ hOld = SelectObject(hMem, hbitmap); // Выбрать в контекст битмап
		// Рисовать им, пропуская цвет color_erase
		TransparentBlt(hdc, (int)position.first, (int)position.second, bm.bmWidth, bm.bmHeight,
			hMem, 0, 0, bm.bmWidth, bm.bmHeight, color_erase);
		DeleteDC(hMem); // Удалть ненужный контекст
		DeleteObject(hOld); // Удалить нунжный объект
	}
	void RotateHBITMAP(float angle); // Поворот битмапа на любой угол
	inline std::pair<float, float> GetXYMatrix(float posSX, float posSY, float x, float y, float angle) // Поворот вектора на угол - матрица поворота в основе
	{
		return std::pair<float, float>(posSX + (x - posSX)*cos(angle*3.1415 / 180) - (y - posSY)*sin(angle*3.1415 / 180), posSY + (x - posSX)*sin(angle*3.1415 / 180) + (y - posSY)*cos(angle*3.1415 / 180));
	}
	int type; // тип битмапа
	bool Go(Semaphore ** sems); // Проверка на возможность движения
	bool OutFrame()
	{
		return (velocity.first > 0 && position.first > 600) || (velocity.second > 0 && position.second > 600) ||
			(velocity.first < 0 && position.first < -75) || (velocity.second < 0 && position.second < -75);
	} // ПРоверка на выход за пределы кадра
	BITMAP bm; // информациия о битмапе
	HBITMAP hbitmap; // битмап
};

class Semaphore // Светофор
{
public:
	static HBITMAP condition[2]; // состояния
	Semaphore(std::pair<float, float> pos) :position(pos) // конструктор
	{
		cond = 0;
	}
	void Show(HDC hdc) // Отобразить
	{
		BITMAP bm;
		GetObject(condition[cond], sizeof(BITMAP), &bm);
		HDC hMem = CreateCompatibleDC(hdc);
		HGDIOBJ hOld = SelectObject(hMem, condition[cond]);
		TransparentBlt(hdc, (int)position.first, (int)position.second, bm.bmWidth, bm.bmHeight, hMem, 0, 0,
			bm.bmWidth, bm.bmHeight, RGB(0, 0, 0));
		DeleteDC(hMem);
		DeleteObject(hOld);
	}
	inline void TurnOn() //вкл
	{
		cond = 1;
	}
	inline void TurnOff()//выкл
	{
		cond = 0;
	}
	inline bool GetInfoTurn() { return cond == 0 ? false : true; }//получить информацию о состоянии
private:
	int cond; // состояние
	std::pair<float, float> position; // позиция
};

class Operator // доп класс оператор - хранит вспомогательные данные
{
public:
	static int un_bus_line[4]; // линии не для автобусов
	static int bus_mass[4]; // линии автобусов
	static std::pair<float, float> startpos_values[8]; // старторые координаты
	static std::pair<float, float> startpos_values_move[4]; // стартовые направления
	// Сгенерировать позицию и установить направления
	static std::pair<float, float> GenerateStart(Object * obj, int * startpos, bool bus, bool start = false)
	{
		if (!start)
			startpos[obj->line]--;
		int k;
		if (!bus)
			k = rand() % 8;
		else
			k = Operator::bus_mass[rand() % 4];
		startpos[k]++;
		obj->line = k;
		obj->velocity = startpos_values_move[k / 2];
		float size = sqrt(obj->velocity.first*obj->velocity.first + obj->velocity.second*obj->velocity.second);
		obj->velocity.first /= -size;
		obj->velocity.second /= -size;
		return std::pair<float, float>(startpos_values[k].first + startpos_values_move[k / 2].first*startpos[k],
			startpos_values[k].second + startpos_values_move[k / 2].second*startpos[k]);
	}
};

class Car : public Object // Машина
{
	enum { OFFSET = 25 }; // Отступ
	std::pair<float, float>* getnormals(int coef) // получить нормали
	{
		std::pair<float, float> normals[2];
		if (velocity.first > 0)
		{
			normals[0] = std::pair<float, float>(position.first, center.second + coef * bm.bmHeight / 2 + OFFSET);
			normals[1] = std::pair<float, float>(position.first + bm.bmWidth, center.second + coef * bm.bmHeight / 2 + OFFSET);
		}
		if (velocity.first < 0)
		{
			normals[0] = std::pair<float, float>(position.first, center.second - coef * bm.bmHeight / 2 - OFFSET);
			normals[1] = std::pair<float, float>(position.first + bm.bmWidth, center.second - coef * bm.bmHeight / 2 - OFFSET);
		}
		if (velocity.second > 0)
		{
			normals[0] = std::pair<float, float>(center.first - coef * bm.bmWidth / 2 - OFFSET, position.second);
			normals[1] = std::pair<float, float>(center.first - coef * bm.bmWidth / 2 - OFFSET, position.second + bm.bmHeight);
		}
		if (velocity.second < 0)
		{
			normals[0] = std::pair<float, float>(center.first + coef * bm.bmWidth / 2 + OFFSET, position.second);
			normals[1] = std::pair<float, float>(center.first + coef * bm.bmWidth / 2 + OFFSET, position.second + bm.bmHeight);
		}
		return normals;
	}
	friend Operator;
	int * stpos; // указатель на количество элементов на определенной дороге
	void ChangeMovement(int line, bool rotate = false) // Сменить вектор движения
	{
		this->stpos[this->line]--;
		this->line = line;
		stpos[this->line]++;
		type = type == 0 ? 3 : 0;
		DeleteObject(hbitmap);
		GetObject(bitmaps[type], sizeof(BITMAP), &bm);
		hbitmap = (HBITMAP)CopyImage(bitmaps[type], IMAGE_BITMAP, bm.bmWidth, bm.bmHeight, NULL);
		if (rotate)
			this->RotateHBITMAP(180);
	}
	bool change_move; // Сменила ли машина движение
public:
	Car(int* startpos) :Object(0), stpos(startpos) // Конструктор
	{
		position = Operator::GenerateStart(this, startpos, false, true);
		this->ReCenter();
		type = velocity.first != 0 ? 0 : 3;
		DeleteObject(hbitmap);
		GetObject(bitmaps[type], sizeof(BITMAP), &bm);
		hbitmap = (HBITMAP)CopyImage(bitmaps[type], IMAGE_BITMAP, bm.bmWidth, bm.bmHeight, NULL);
		if (velocity.first == -1 || velocity.second == 1)
			this->RotateHBITMAP(180.0f);
		change_move = true;
	}
	void Rotate() // Поворот
	{
		if (!change_move)
			return;
		int k = rand() % 3;
		if (k == 0)
			return;
		if (velocity.first != 0)
		{
			if (this->position.first >= Operator::startpos_values[4].first - 5 && this->position.first <= Operator::startpos_values[4].first + 5 && k == 1)
			{
				this->position.first = Operator::startpos_values[4].first;
				this->ChangeMovement(4, true);
				this->velocity = std::pair<float, float>(0, 1);
				ReCenter();
				change_move = false;
			}
			if (this->position.first >= Operator::startpos_values[6].first - 3 && this->position.first <= Operator::startpos_values[6].first + 3 && k == 2)
			{
				this->position.first = Operator::startpos_values[6].first;
				this->ChangeMovement(6, false);
				this->velocity = std::pair<float, float>(0, -1);
				ReCenter();
				change_move = false;
			}
			return;
		}
		if (velocity.second != 0)
		{
			if (this->position.second >= Operator::startpos_values[4].first - 5 && this->position.second <= Operator::startpos_values[4].first + 5 && k == 1)
			{
				this->position.second = Operator::startpos_values[4].first;
				this->ChangeMovement(0, true);
				this->velocity = std::pair<float, float>(-1, 0);
				ReCenter();
				change_move = false;
			}
			if (this->position.second >= Operator::startpos_values[6].first - 5 && this->position.second <= Operator::startpos_values[6].first + 5 && k == 2)
			{
				this->position.second = Operator::startpos_values[6].first;
				this->ChangeMovement(2, false);
				this->velocity = std::pair<float, float>(1, 0);
				ReCenter();
				change_move = false;
			}
			return;
		}
	}
	void ChangeLine(bool bus) // Сменить линию движения
	{
		for (int i = 0; i < 4 && !bus; i++)
			if (this->line == Operator::bus_mass[i])
				return;
		int k = this->line % 2 == 0 ? 1 : -1;
		if (velocity.first != 0)
			this->position.second += k * 37;
		if (velocity.second != 0)
			this->position.first += k * 37;
		if (bus)
		{
			this->position.first += velocity.first*SPEED * 3;
			this->position.second += velocity.second*SPEED * 3;
		}
		stpos[line]--;
		this->line = line + k;
		stpos[line]++;
		ReCenter();
	}
	// Реакция на событие в виде автобуса или попытки смены полосы
	void Reaction(Object ** objects, int count_object, int extra, bool bus = false)
	{
		for (int i = 0; i < 4; i++)
			if (Operator::bus_mass[i] == line && !bus)
				return;
		int line_neig = line % 2 == 0 ? line + 1 : line - 1;
		std::pair<float, float> normal[2] = { getnormals(bus ? -1 : 1)[0], getnormals(bus ? -1 : 1)[1] };
		for (int i = 0; i < count_object; i++)
			if (objects[i]->line == line_neig)
				if (HavePoint(normal[0], *objects[i]) || HavePoint(normal[1], *objects[i]))
					return;
		ChangeLine(bus);
	}
	void Reload() // Перезагрузка состояния
	{
		change_move = true;
		std::pair<float, float> temp = velocity;
		this->position = Operator::GenerateStart(this, stpos, true);
		if (velocity == temp)
			return;
		type = velocity.first != 0 ? 0 : 3;
		DeleteObject(hbitmap);
		GetObject(bitmaps[type], sizeof(BITMAP), &bm);
		hbitmap = (HBITMAP)CopyImage(bitmaps[type], IMAGE_BITMAP, bm.bmWidth, bm.bmHeight, NULL);
		if (velocity.first == -1 || velocity.second == 1)
			this->RotateHBITMAP(180.0f);
	}
	void Move(Semaphore ** sems, Object ** objects, int count_obj, int extra); // Перемещение
	void OnPaint(HDC hdc) // Отрисовать
	{
		this->ShowTransparent(hdc, RGB(0, 0, 0));
	}
private:
	enum { SPEED = 9 }; // Скорость
};

class Bus : public Object // Автобус
{
	int * stpos; // указатель на заполненость линий
public:
	Bus(int* startpos) : Object(1), stpos(startpos) // конструктор
	{
		position = Operator::GenerateStart(this, startpos, true, true);
		this->ReCenter();
		type = velocity.first != 0 ? 1 : 4;
		DeleteObject(hbitmap);
		GetObject(bitmaps[type], sizeof(BITMAP), &bm);
		hbitmap = (HBITMAP)CopyImage(bitmaps[type], IMAGE_BITMAP, bm.bmWidth, bm.bmHeight, NULL);
		if (velocity.first == -1 || velocity.second == 1)
			this->RotateHBITMAP(180.0f);
	}
	void Move(Semaphore ** sems, Object ** objects, int count_obj, int extra) // Перемещение
	{
		if (!this->Go(sems))
			return;
		for (int i = 0; i < count_obj; i++)
			if (i != extra)
				if (this->Collision(*objects[i], 20))
					return;
		this->position.first += velocity.first * SPEED;
		this->position.second += velocity.second * SPEED;
		ReCenter();
		if (this->OutFrame())
			this->Reload();
	}
	void Reload() // Перезагрузка состояния
	{
		std::pair<float, float> temp = velocity;
		this->position = Operator::GenerateStart(this, stpos, true);
		if (velocity == temp)
			return;
		type = velocity.first != 0 ? 1 : 4;
		DeleteObject(hbitmap);
		GetObject(bitmaps[type], sizeof(BITMAP), &bm);
		hbitmap = (HBITMAP)CopyImage(bitmaps[type], IMAGE_BITMAP, bm.bmWidth, bm.bmHeight, NULL);
		if (velocity.first == -1 || velocity.second == 1)
			this->RotateHBITMAP(180.0f);
	}
	void OnPaint(HDC hdc) // Отрисовка
	{
		this->ShowTransparent(hdc, RGB(0, 0, 0));
	}
private:
	enum { SPEED = 5 }; // Скорость
};