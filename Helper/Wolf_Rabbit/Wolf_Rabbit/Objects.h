#pragma once
#include <Windows.h>
#include <string>
#include <vector>
#include <chrono>
#pragma comment(lib,"MSIMG32.lib")
#pragma comment(lib, "Gdi32.lib")
#include "resource.h"

class Object;
class Wolf;
class Rabbit;

struct Settings // Настройки, получаемые из меню 
{
	int time_eat_wolf; // волк без еды
	int time_death_wolf; // смерть волка
	int time_death_rabbit; // смерть кролика
	int time_new_wolf; // новый волк
	int time_new_rabbit; // новый кролик
};

class GodManager // Главный класс управления
{
	friend Object;
	Settings settings; // Настройки
	int index; // индекс для индентификации объекта
	int count_wolf; // количество волков
	int count_rabbit; // количество кроликов
	bool game; // игра?
	HBITMAP* background; // фон
	static GodManager * manager; // сам элемент - Синглетон
	std::vector<Object*> objects; // объекты сцены
	GodManager() // конструктор
	{
		game = false;
		index = 0;
	}
public:
	static UINT IDCsEdit[7]; // Идентификаторы Editов
	static UINT Button_ID; // Идентификатор кнопки
	inline int GetID() // Получить ID для элемента
	{
		return index + 1;
	}
	inline void SetBackGround(WORD w_hb) // Установить фон
	{
		background = new HBITMAP();
		*background = LoadBitmap(GetModuleHandle(NULL), MAKEINTRESOURCE(w_hb));
	}
	void GenerateObjects(); // Сгенерировать в память все объекты сцены
	inline void AddObject(Object* obj) // Добавить объект
	{
		objects.push_back(obj);
	}
	void DeleteObjectofField(Object*); // Удалить объект
	void ActiveElements(HWND hwnd) // Активировать элементы меню
	{
		for (int i = 0; i < 7; i++)
			ShowWindow(GetDlgItem(hwnd, IDCsEdit[i]), SW_SHOW);
		ShowWindow(GetDlgItem(hwnd, Button_ID), SW_SHOW);
	}
	void DisabledElements(HWND hwnd) // Скрыть элементы
	{
		for (int i = 0; i < 7; i++)
			ShowWindow(GetDlgItem(hwnd, IDCsEdit[i]), SW_HIDE);
		ShowWindow(GetDlgItem(hwnd, Button_ID), SW_HIDE);
	}
	inline void StartGame()  // Старт 
	{
		game = true;
		this->GenerateObjects();
	}
	inline void StopGame() { game = false; } // Стоп игры
	bool GetInfoEdits(HWND hwnd) // Получить инфо о введенных данных
	{
		char settings[7][10]; // для получения информации
		for (int i = 0; i < 7; i++)
		{
			GetWindowText(GetDlgItem(hwnd, IDCsEdit[i]), settings[i], 10);
			try // Если вдруг пустота или бред введен
			{
				std::stoi(std::string(settings[i]));
			}
			catch (std::invalid_argument)
			{
				MessageBox(0, "Enter correct data", "Error", 0);
				return false;
			}
		}
		// Внесение данных
		this->count_wolf = std::stoi(std::string(settings[0]));
		this->count_rabbit = std::stoi(std::string(settings[1]));
		this->settings.time_death_wolf = std::stoi(std::string(settings[2]));
		this->settings.time_death_rabbit = std::stoi(std::string(settings[3]));
		this->settings.time_new_wolf = std::stoi(std::string(settings[4]));
		this->settings.time_new_rabbit = std::stoi(std::string(settings[5]));
		this->settings.time_eat_wolf = std::stoi(std::string(settings[6]));
		return true;
	}
	void AddElements(HWND hwnd) // Добавить элементы
	{
		int i;
		for (i = 0; i < 7; i++)
		{
			HWND hwnd_t = CreateWindowEx(0, "EDIT", "", WS_CHILD | WS_BORDER | ES_LEFT | ES_AUTOHSCROLL,
				300, 100 + i * 50, 50, 20, hwnd, (HMENU)IDCsEdit[i], GetModuleHandle(NULL), NULL);
		}
		HWND hwnd_t = CreateWindow("BUTTON", "Start", WS_CHILD, 300, 100 + i * 50, 70, 20, hwnd, (HMENU)Button_ID, GetModuleHandle(NULL), NULL);
	}
	void PaintSettings(HDC hdc) // Отрисовать настройки
	{
		std::string strs[7] = { "Count Wolf","Count Rabbit","Wolf's lifetime","Rabbit's lifetime","Wolf's new","Rabbit's new","Wolf's hungry", };
		for (int i = 0; i < 7; i++)
			TextOut(hdc, 150, 100 + i * 50, strs[i].c_str(), strs[i].length());
	}
	void PaintGame(HDC hdc, HWND hwnd); // Отрисовать игру
	void Paint(PAINTSTRUCT& ps, int width, int height, HWND hwnd) // Отрисовать 
	{
		HDC hMemDC, hTempDC; // Теневой буффер
		HGDIOBJ hMemBmp, hSysBmp;
		HBRUSH hBrush = (HBRUSH)WHITE_BRUSH;
		hMemDC = CreateCompatibleDC(ps.hdc); // Создать конекст схожии с основным
		hMemBmp = CreateCompatibleBitmap(ps.hdc, width, height); // Создание битмап для фона
		hSysBmp = SelectObject(hMemDC, hMemBmp);
		SelectObject(hMemDC, hBrush);
		hTempDC = CreateCompatibleDC(hMemDC);
		SelectObject(hTempDC, *background);
		BitBlt(hMemDC, 0, 0, width, height, hTempDC, 0, 0, SRCCOPY); // Копироание одного контекста в другой контекст
		DeleteDC(hTempDC); // Удаление временного контекста

		if (game) // Если игра
			this->PaintGame(hMemDC, hwnd);
		else
			this->PaintSettings(hMemDC);

		BitBlt(ps.hdc, 0, 0, width, height, hMemDC, 0, 0, SRCCOPY); // Отправка в основной контекст
		SelectObject(hMemDC, hSysBmp);
		DeleteObject(hMemBmp); // Удаление Битмапа
		DeleteObject(hSysBmp);
		DeleteDC(hMemDC); // Удаление контекстов
		DeleteObject(hBrush);
	}
	static GodManager & GetInstace() // Получить ссылку на объект
	{
		if (!manager)
			manager = new GodManager();
		return *manager;
	}
};

class Object // Объект
{
	friend GodManager;
private:
	float Lenght(float x1, float y1, float x2, float y2) // Длина вектора
	{
		return sqrt((x2 - x1)*(x2 - x1) + (y2 - y1)*(y2 - y1));
	}
protected:
	int id; // ИД
	static HBITMAP hbitmaps[2]; // битмапы
	int type_hb; // тип используемого битмапа
	std::pair<float, float> position; // позиция
	std::pair<float, float> velocity; // ускорение
	BITMAP info; // информация о битмапеы
	std::chrono::steady_clock::time_point last_nlife, startlife; // последнее рождение и начало жизни
	int age; // возраст
	int time_death; // смертельный возраст
	int time_new; // время рождения
	virtual void Action(Object*) = 0; // Действие
	Object* Contact(std::vector<Object*>& objects) // Проверка на пересечение
	{
		for (UINT i = 0; i < objects.size(); i++)
			if (objects[i]->id != id)
			{
				int len = Lenght(position.first + info.bmWidth / 2, position.second + info.bmHeight / 2,
					objects[i]->position.first + objects[i]->info.bmWidth / 2,
					objects[i]->position.second + objects[i]->info.bmHeight / 2);
				len -= (this->info.bmWidth / 2);
				if (len < 0)
					return objects[i];
			}
		return NULL;
	}
	virtual void AddAction() {} // Дополнительно действие
	virtual Object* AddNext() = 0; // Создать нового себя
public:
	virtual ~Object() // Деструктор
	{
		GodManager::GetInstace().DeleteObjectofField(this);
	}
	Object(int type, int index, std::pair<float, float> pos) :type_hb(type), id(index) // Констурктор
	{
		GetObject(hbitmaps[type_hb], sizeof(BITMAP), &info); // Получить информацию о битмапе
		startlife = last_nlife = std::chrono::high_resolution_clock::now();
		if (pos.first == -1)
		{
			position.first = 50 + rand() % 500;
			position.second = 50 + rand() % 500;
		}
		else
		{
			position.first = pos.first;
			position.second = pos.second;
		}
		velocity.first = -3 + rand() % 7;
		velocity.second = -3 + rand() % 7;
		float size = sqrt(velocity.first*velocity.first + velocity.second*velocity.second);
		velocity.first /= size;
		velocity.second /= size;
	}
	inline int GetID() { return id; } // Получить ИД
	void Show(HDC hdc) // Отобразить
	{
		HDC hMem = CreateCompatibleDC(hdc); // Создать схожий контекст
		SelectObject(hMem, hbitmaps[type_hb]); // Выбрать в него изображение
		// Скпировать с исключением черного цвета
		TransparentBlt(hdc, position.first, position.second, info.bmWidth, info.bmHeight, hMem, 0, 0,
			info.bmWidth, info.bmHeight, RGB(0, 0, 0));
		DeleteDC(hMem); // Удалить ненужный контекст
	}
	void Moving(std::vector<Object*>& objects) // движение
	{
		// Проверка на старость
		if (std::chrono::duration_cast<std::chrono::seconds>(std::chrono::high_resolution_clock::now() - startlife).count() > time_death - age)
		{
			this->~Object();
			return;
		}
		// Проверка на рождение
		if (std::chrono::duration_cast<std::chrono::seconds>(std::chrono::high_resolution_clock::now() - last_nlife).count() > time_new)
		{
			GodManager::GetInstace().AddObject(this->AddNext());
			last_nlife = std::chrono::high_resolution_clock::now();
		}
		// Удар о стены
		if (position.first >= 600 - info.bmWidth || position.first <= 10)
			velocity.first *= -1;
		if (position.second >= 600 - info.bmHeight || position.second <= 10)
			velocity.second *= -1;
		position.first += velocity.first * 5;
		position.second += velocity.second * 5;
		// Проверка на контакт
		Object * temp = Contact(objects);
		if (temp != NULL)
			Action(temp);
		AddAction();
	}
};

class Wolf :public Object // Волк
{
	std::chrono::steady_clock::time_point last_eat; // время есть
	int time_eat; // максимальный голод
public:
	// Конструктор
	Wolf(int index, int _time_eat, int _time_death, int _time_new, std::pair<float, float> pos = { -1,-1 }) :Object(0, index, pos)
	{
		last_eat = std::chrono::high_resolution_clock::now();
		this->time_eat = _time_eat;
		this->time_death = _time_death;
		this->time_new = _time_new;
		age = 1 + rand() % time_death;
	}
	// Действие
	void Action(Object* obj);
	Object* AddNext() // Создать себеподобного
	{
		return new Wolf(GodManager::GetInstace().GetID(), time_eat, time_death, time_new, position);
	}
	void AddAction() // Доп действие - проверка на голод
	{
		if (std::chrono::duration_cast<std::chrono::seconds>(std::chrono::high_resolution_clock::now() - last_eat).count() > time_eat)
			this->~Wolf();
	}
};

class Rabbit :public Object // Кролик
{
public:
	// Конструктор
	Rabbit(int index, int _time_death, int _time_new, std::pair<float, float> pos = { -1,-1 }) :Object(1, index, pos)
	{
		this->time_death = _time_death;
		this->time_new = _time_new;
		age = 1 + rand() % time_death;
	}
	void Action(Object* obj) {} // Нет никаких действий
	Object* AddNext() // Создать нового потомка
	{
		return new Rabbit(GodManager::GetInstace().GetID(), time_death, time_new, position);
	}
};