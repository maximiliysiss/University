#include "HeaderBtn.h"


void HeaderBtn::SetRect(RECT rect)
{
	this->rect = rect;
}

Workspace * HeaderBtn::GetWorkspace()
{
	return workspace;
}

HeaderBtn::HeaderBtn(Workspace * workspace)
	: workspace(workspace)
{
}

bool HeaderBtn::OnClick(POINT point, HWND hwnd)
{
	bool f = point.x >= rect.left && point.x <= rect.right && point.y >= rect.top && point.y <= rect.bottom;
	if (f)
	{
		SIZE size{ 0 };
		GetTextExtentPoint32A(GetDC(hwnd), "X", 1, &size);
		bool isDelete = point.x >= rect.right - 4 - size.cx && point.x <= rect.right - 4 && point.y >= rect.top + 2 && point.y <= rect.top + 2 + size.cy;
		if (!isDelete) {
			workspace->SetActive(hwnd);
		}
		else {
			App::Instance().DeleteFileFromApp();
		}
	}
	return f;
}

HeaderBtn::~HeaderBtn()
{
}
