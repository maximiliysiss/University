#include "MetaApplicationInfo.h"

MetaApplicationInfo* MetaApplicationInfo::appInfo = nullptr;

MetaApplicationInfo& MetaApplicationInfo::getInstance()
{
	if (appInfo)
		return *appInfo;
	appInfo = new MetaApplicationInfo();
	return *appInfo;
}

void MetaApplicationInfo::setSize(int width, int height)
{
	this->wndHeight = height;
	this->wndWidht = width;
}
