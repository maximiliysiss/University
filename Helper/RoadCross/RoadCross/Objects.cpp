#include "Objects.h"

void SystemManage::OnPaint(PAINTSTRUCT& ps, int width, int height)
{
	HDC hMemDC, hTempDC; // Теневой буффер
	HGDIOBJ hMemBmp, hSysBmp;
	HBRUSH hBrush = (HBRUSH)WHITE_BRUSH;
	hMemDC = CreateCompatibleDC(ps.hdc); // Создать конекст схожии с основным
	hMemBmp = CreateCompatibleBitmap(ps.hdc, width, height); // Создание битмап для фона
	hSysBmp = SelectObject(hMemDC, hMemBmp);
	SelectObject(hMemDC, hBrush);
	hTempDC = CreateCompatibleDC(hMemDC);
	SelectObject(hTempDC, *background->GetHB());
	BitBlt(hMemDC, 0, 0, width, height, hTempDC, 0, 0, SRCCOPY); // Копироание одного контекста в другой контекст
	DeleteDC(hTempDC); // Удаление временного контекста
	for (int i = 0; i < count_object; i++) // Отрисовка всех объектов
		objects[i]->OnPaint(hMemDC);
	for (int i = 0; i < 4; i++)
		semaphore[i]->Show(hMemDC);
	BitBlt(ps.hdc, 0, 0, width, height, hMemDC, 0, 0, SRCCOPY); // Отправка в основной контекст
	SelectObject(hMemDC, hSysBmp);
	DeleteObject(hMemBmp); // Удаление Битмапа
	DeleteObject(hSysBmp);
	DeleteDC(hMemDC); // Удаление контекстов
	DeleteObject(hBrush);
	this->Movement(); // Движение всего
	// Обработка включения по таймеру светофоров
	if (std::chrono::duration_cast<std::chrono::seconds>(std::chrono::high_resolution_clock::now() - time_span).count() > RELOAD)
	{
		this->TurnSph(rand() % 4);
		time_span = std::chrono::high_resolution_clock::now();
	}
}

SystemManage::SystemManage(int s_count, int c_count, int b_count) :counter(c_count + b_count) // Конструктор
{
	for (int i = 0; i < 8; i++)
		startpos[i] = 0;
	background = new Object(2);
	count_object = c_count + b_count;
	semaphore = new Semaphore*[s_count];
	semaphore[0] = new Semaphore(std::pair<float, float>(80, 80));
	semaphore[1] = new Semaphore(std::pair<float, float>(480, 480));
	semaphore[2] = new Semaphore(std::pair<float, float>(80, 480));
	semaphore[3] = new Semaphore(std::pair<float, float>(480, 80));
	objects = new Object*[c_count + b_count];
	for (int i = 0; i < c_count; i++)
		objects[i] = new Car(startpos);
	for (int i = c_count; i < c_count + b_count; i++)
		objects[i] = new Bus(startpos);
	this->TurnSph(rand() % 4);
	time_span = std::chrono::high_resolution_clock::now();
}

SystemManage* SystemManage::systemmanager = NULL; // статическая переменная синглетона


// задание готовых координат начала движения
std::pair<float, float> Operator::startpos_values[8]{ std::pair<float,float>(0,304),std::pair<float,float>(0,341),
std::pair<float,float>(525,217),std::pair<float,float>(525,254),
std::pair<float,float>(217,0),std::pair<float,float>(254,0),
std::pair<float,float>(304,525),std::pair<float,float>(341,525) };

// готовая информация о линия для автобуса
int Operator::bus_mass[4]{ 1,2,4,7 };

// готовая информация о линиях не для автобуса
int Operator::un_bus_line[4]{ 0,3,5,6 };

// информация о направлениях
std::pair<float, float> Operator::startpos_values_move[4]{ std::pair<float,float>(-100,0),std::pair<float,float>(100,0),
std::pair<float,float>(0,-100) ,std::pair<float,float>(0,100) };

// битмапы
HBITMAP Object::bitmaps[5]{ LoadBitmap(GetModuleHandle(NULL),MAKEINTRESOURCE(IDB_CAR)),
LoadBitmap(GetModuleHandle(NULL),MAKEINTRESOURCE(IDB_BUS)),
LoadBitmap(GetModuleHandle(NULL),MAKEINTRESOURCE(IDB_BACKGROUND)),
LoadBitmap(GetModuleHandle(NULL),MAKEINTRESOURCE(IDB_CAR_ROT)),
LoadBitmap(GetModuleHandle(NULL),MAKEINTRESOURCE(IDB_BUS_R)) };

void Object::RotateHBITMAP(float angle) // Поворот битмапа на любой угол
{
	POINT point[3]; // Указатель на 3 точки, формирующие полигон с битмапом
	point[0].x = (LONG)GetXYMatrix((float)bm.bmWidth / 2, (float)bm.bmHeight / 2, 0, 0, angle).first;
	point[0].y = (LONG)GetXYMatrix((float)bm.bmWidth / 2, (float)bm.bmHeight / 2, 0, 0, angle).second;
	point[1].x = (LONG)GetXYMatrix((float)bm.bmWidth / 2, (float)bm.bmHeight / 2, (float)bm.bmWidth, 0, angle).first;
	point[1].y = (LONG)GetXYMatrix((float)bm.bmWidth / 2, (float)bm.bmHeight / 2, (float)bm.bmWidth, 0, angle).second;
	point[2].x = (LONG)GetXYMatrix((float)bm.bmWidth / 2, (float)bm.bmHeight / 2, 0, (float)bm.bmHeight, angle).first;
	point[2].y = (LONG)GetXYMatrix((float)bm.bmWidth / 2, (float)bm.bmHeight / 2, 0, (float)bm.bmHeight, angle).second;
	HDC hdc = CreateCompatibleDC(NULL); // Создание простого контекста для хранения
	DeleteObject(hbitmap);
	hbitmap = (HBITMAP)CopyImage(bitmaps[type], IMAGE_BITMAP, bm.bmWidth, bm.bmHeight, LR_COPYRETURNORG);
	HGDIOBJ hOld = NULL;
	hOld = SelectObject(hdc, hbitmap);
	PlgBlt(hdc, point, hdc, 0, 0, bm.bmWidth, bm.bmHeight, NULL, 0, 0); // Поворот к контексте
	DeleteObject(hbitmap);
	hbitmap = (HBITMAP)CopyImage(GetCurrentObject(hdc, OBJ_BITMAP), IMAGE_BITMAP, bm.bmWidth, bm.bmHeight, LR_COPYRETURNORG); // Копирование изображения из контекста
	DeleteDC(hdc);
	DeleteObject(hOld);
	GetObject(hbitmap, sizeof(BITMAP), &bm);
}

// Битмапы для светофора
HBITMAP Semaphore::condition[2] = { LoadBitmap(GetModuleHandle(NULL), MAKEINTRESOURCE(IDB_REDSIGNAL)),
LoadBitmap(GetModuleHandle(NULL), MAKEINTRESOURCE(IDB_GREENSIGNAL)) };

// Включить светофор и выключить остальные
void SystemManage::TurnSph(int num)
{
	(*semaphore[num]).TurnOn();
	for (int i = 0; i < 4; i++)
		if (i != num)
			(*semaphore[i]).TurnOff();
}

// Движение всех объектов
void SystemManage::Movement()
{
	for (int i = 0; i < counter; i++)
		(*objects[i]).Move(semaphore, objects, counter, i);
}


// Проверка на доступность движения
bool Object::Go(Semaphore ** sems)
{
	if (this->velocity.first > 0) // Пред и после движение (тип, уже пересек черту и надо доезжать перекресток)
		if (this->position.first < 90 || this->position.first > 190)
			return true;
	if (this->velocity.second > 0)
		if (this->position.second < 90 || this->position.second > 190)
			return true;
	if (this->velocity.first < 0)
		if (this->position.first > 460 || this->position.first < 370)
			return true;
	if (this->velocity.second < 0)
		if (this->position.second > 460 || this->position.second < 370)
			return true;
	// Проверка светофоров
	if (this->velocity.first < 0 && !sems[0]->GetInfoTurn())
		return false;
	if (this->velocity.first > 0 && !sems[1]->GetInfoTurn())
		return false;
	if (this->velocity.second > 0 && !sems[2]->GetInfoTurn())
		return false;
	if (this->velocity.second < 0 && !sems[3]->GetInfoTurn())
		return false;
	return true;
}

// Движение машины
void Car::Move(Semaphore ** sems, Object ** objects, int count_obj, int extra)
{
	if (!this->Go(sems)) // Если нельзя двигаться, то нет
		return;
	for (int i = 0; i < count_obj; i++) // Проверка на пересечение с другими объектами
		if (i != extra)
			if (this->Collision(*objects[i], 30))
			{
				if (typeid(Bus) == typeid(*objects[i])) // Если перед машиной автобус - объехать
					this->Reaction(objects, count_obj, extra, true);
				return;
			}
	this->Reaction(objects, count_obj, extra, false); // Проверка на возможность ехать по полосе автобуса
	this->position.first += velocity.first * SPEED; // Движение
	this->position.second += velocity.second * SPEED;
	ReCenter(); // Пересчет центра
	if (this->OutFrame()) // Выход за пределы - перезагрузка
		this->Reload();
	Rotate();
}