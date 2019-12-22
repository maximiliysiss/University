#pragma once
class MetaApplicationInfo
{
	static MetaApplicationInfo* appInfo;

	int wndWidht;
	int wndHeight;
public:
	static MetaApplicationInfo& getInstance();

	void setSize(int width, int height);
	inline int getWidht() { return wndWidht; }
	inline int getHeight() { return wndHeight; }
};

