#include "Workspace.h"
#include <memory>

std::wstring Workspace::GetPathForSaving()
{
	OPENFILENAME ofn;
	char szFile[260];
	ZeroMemory(&ofn, sizeof(ofn));
	ofn.lStructSize = sizeof(ofn);
	ofn.hwndOwner = NULL;
	ofn.lpstrFile = (LPWSTR)szFile;
	ofn.lpstrFile[0] = '\0';
	ofn.nMaxFile = sizeof(szFile);
	ofn.lpstrFilter = L"txt\0*.txt\0";
	ofn.nFilterIndex = 1;
	ofn.lpstrFileTitle = NULL;
	ofn.nMaxFileTitle = 0;
	ofn.lpstrInitialDir = NULL;
	ofn.Flags = OFN_PATHMUSTEXIST | OFN_FILEMUSTEXIST;
	if (!GetSaveFileName(&ofn))
		return std::wstring();
	this->savePlace = std::wstring(ofn.lpstrFile);
	std::wstring nameFile = savePlace.substr(savePlace.find_last_of(L'\\') + 1);
	if (nameFile.find('.') == std::wstring::npos) {
		savePlace += L".txt";
	}

	this->name = nameFile.substr(0, nameFile.find('.'));
	return savePlace;
}

void Workspace::SaveProcess()
{
	int len = GetWindowTextLength(hwnd) + 1;
	std::wstring content;
	content.reserve(len);
	content.resize(len);
	GetWindowText(hwnd, (LPWSTR)const_cast<wchar_t*>(content.c_str()), len);
	std::ofstream file(savePlace);
	if (!file.is_open())
		throw std::exception("Error open file");
	auto res = std::convertToString(content);
	file << res;
	file.close();
	this->isSaving = true;
}

void Workspace::Save()
{
	if (this->isSaving)
		return;
	if (this->savePlace.length() == 0) {
		auto tmp = GetPathForSaving();
		if (tmp.length() == 0)
			return;
	}
	SaveProcess();
}

void Workspace::SetContent(std::wstring content)
{
	SetWindowTextW(hwnd, content.c_str());
	this->isSaving = true;
}

void Workspace::SaveAs()
{
	auto tmp = GetPathForSaving();
	if (tmp.length() == 0)
		return;
	SaveProcess();
}

void Workspace::AppendText(std::wstring text)
{
	int len = GetWindowTextLength(hwnd) + 1;
	std::wstring content = L"";
	content.reserve(len);
	content.resize(len);
	GetWindowText(hwnd, (LPWSTR)const_cast<wchar_t*>(content.c_str()), len - 1);
	std::trim(content);
	content += text;
	this->SetContent(std::wstring(content.begin(), content.end()));
}

RECT Workspace::GetWndRECT()
{
	RECT rect;
	GetClientRect(hwnd, &rect);
	return rect;
}

void Workspace::SetActive(HWND hwnd, bool isRepaint)
{
	parent->SetActiveWS(hwnd, this, isRepaint);
}

void Workspace::ShowDialog()
{
	MSG msg;

	while (GetMessage(&msg, nullptr, 0, 0))
	{
		if (!TranslateAccelerator(msg.hwnd, 0, &msg))
		{
			TranslateMessage(&msg);
			DispatchMessage(&msg);
		}
	}
}

void Workspace::Resize(RECT & rect)
{
	MoveWindow(hwnd, parent->Left(),
		parent->Top() + parent->HeaderHeight(),
		parent->GetWndSizeX(),
		parent->GetWndSizeY() - parent->HeaderHeight(), true);
}

Workspace::Workspace(HINSTANCE hInst, App* parent, std::wstring wndName)
	: hInst(hInst), parent(parent), guid(std::GetGuid()), name(wndName)
{
	hwnd = CreateWindowEx(
		0, L"EDIT",
		NULL,
		WS_CHILD | WS_VISIBLE | WS_VSCROLL |
		ES_LEFT | ES_MULTILINE | ES_AUTOVSCROLL | ES_WANTRETURN,
		parent->Left(),
		parent->Top() + parent->HeaderHeight(),
		parent->GetWndSizeX(),
		parent->GetWndSizeY() - parent->HeaderHeight(),
		parent->GetWnd(),
		(HMENU)guid,
		hInst,
		NULL);
}

Workspace::~Workspace()
{
	DestroyWindow(hwnd);
}
