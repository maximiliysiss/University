#pragma once

// Данные о приложении (Singleton)
class MetaApplicationInfo
{
	static MetaApplicationInfo* appInfo;

	int wndWidht;
	int wndHeight;
public:
	static MetaApplicationInfo& getInstance();

	// Установить размеры
	void setSize(int width, int height);
	// получить размеры
	inline int getWidht() const { return wndWidht; }
	inline int getHeight() const { return wndHeight; }
};

