#include "App.h"
#include <stdio.h>
#include <stdlib.h>

App * App::application = NULL;
const int App::headerHeight = 30;
int App::top = 0;
int App::bottom = 0;
int App::left = 0;
int App::right = 0;

inline void App::SetWndSize(RECT & rect)
{
	this->right = rect.right;
	this->left = rect.left;
	this->top = rect.top;
	this->bottom = rect.bottom;
}

inline std::vector<Workspace*> App::GetWorkspaces()
{
	return this->windows;
}

void App::InsertText(HWND hwnd)
{
	if (!OpenClipboard(nullptr))
		return;

	auto clipboardHandle = GetClipboardData(CF_TEXT);
	if (!clipboardHandle)
		return;

	char * pszText = static_cast<char*>(GlobalLock(clipboardHandle));
	CloseClipboard();
	std::string clipBoardContent(pszText);
	if (currentWindows == windows.end())
		return;
	(*currentWindows)->AppendText(std::wstring(clipBoardContent.begin(), clipBoardContent.end()));
}

void App::SetActiveWS(HWND hwnd, Workspace * ws, bool isPaint)
{
	auto newWnd = std::find(windows.begin(), windows.end(), ws);
	BringWindowToTop(ws->GetEditBox());
	if (currentWindows == newWnd)
		return;
	ShowWindow((*currentWindows)->GetEditBox(), SW_HIDE);
	currentWindows = newWnd;
	ShowWindow((*currentWindows)->GetEditBox(), SW_SHOWDEFAULT);
	if (isPaint)
		App::Paint(hwnd, GetDC(hwnd));
}

void App::Open(HWND hwnd)
{
	OPENFILENAME ofn = { 0 };
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
	if (!GetOpenFileName(&ofn))
		return;

	std::wstring path = std::wstring(ofn.lpstrFile);
	std::wstring name = path.substr(path.find_last_of(L'\\') + 1);
	Workspace * ws = new Workspace(this->hInst, this, name);

	std::ifstream file(path);
	if (!file.is_open())
		throw std::exception("File not found");
	std::string content((std::istreambuf_iterator<char>(file)),
		(std::istreambuf_iterator<char>()));
	setlocale(LC_ALL, "Russian");
	std::wstring wcontent(content.begin(), content.end());
	ws->SetContent(wcontent);
	ws->SetSavePlace(path);
	HeaderBtn * hBtn = new HeaderBtn(ws);
	if (windows.size() > 0) {
		ShowWindow((*currentWindows)->GetEditBox(), SW_HIDE);
	}
	this->windows.push_back(ws);
	this->headerBtns.push_back(hBtn);
	this->currentWindows = windows.end() - 1;
	this->Paint(hwnd, GetDC(hwnd));
}

void App::SaveFile(HWND hwnd)
{
	if (this->currentWindows != windows.end()) {
		(*currentWindows)->Save();
		this->Paint(hwnd, GetDC(hwnd));
	}
}

void App::SaveFileAs(HWND hwnd)
{
	if (this->currentWindows != windows.end()) {
		(*currentWindows)->SaveAs();
	}
	this->Paint(hwnd, GetDC(hwnd));
}

void App::SetChanged()
{
	if (this->currentWindows != windows.end()) {
		(*currentWindows)->SetChanging();
	}
}

void App::DeleteFileFromApp()
{
	for (int i = 0; i < headerBtns.size(); i++) {
		if (headerBtns[i]->GetWorkspace() == *currentWindows) {
			delete headerBtns[i];
			headerBtns.erase(headerBtns.begin() + i);
			break;
		}
	}
	(*currentWindows)->Save();
	delete *currentWindows;
	windows.erase(currentWindows);
	currentWindows = windows.size() == 0 ? windows.end() : windows.end() - 1;
	if (windows.size() != 0)
		ShowWindow((*currentWindows)->GetEditBox(), SW_SHOWDEFAULT);
	this->Paint(this->window, GetDC(window));
}

bool App::CreateNewWorkSpace(std::string name)
{
	if (std::trim(name) == "") {
		name = "new" + std::to_string(newCount);
		newCount++;
	}
	if (this->currentWindows != windows.end())
		ShowWindow((*currentWindows)->GetEditBox(), SW_HIDE);
	auto newWS = new Workspace(this->hInst, this, std::converterToWString.from_bytes(name.c_str()));
	auto hBtn = new HeaderBtn(newWS);
	windows.push_back(newWS);
	headerBtns.push_back(hBtn);
	currentWindows = windows.end() - 1;
	return true;
}

int App::GetCountWindows()
{
	return (int)windows.size();
}

App & App::Instance(int sX, int sY, std::wstring name, HINSTANCE hInst)
{
	if (App::application == NULL)
		App::application = new App(sX, sY, name, hInst);
	return *application;
}

void App::ShowDialog()
{
	window = CreateWindow(wndName.c_str(), wndName.c_str(), WS_OVERLAPPEDWINDOW, CW_USEDEFAULT, CW_USEDEFAULT, top,
		bottom, 0, 0, hInst, 0);

	ShowWindow(window, SW_SHOW);
	UpdateWindow(window);

	MSG msg;

	while (GetMessage(&msg, 0, 0, 0))
	{
		if (!IsDialogMessage(window, &msg))
		{
			TranslateMessage(&msg);
			DispatchMessage(&msg);
		}
	}
}

void App::Paint(HWND hwnd, HDC hdc)
{
	RECT rect;
	GetClientRect(hwnd, &rect);
	FillRect(hdc, &rect, (HBRUSH)std::WHITE_RGB);
	RECT header{ rect.left, rect.top, rect.right, App::headerHeight };
	App& app = App::Instance();
	app.SetWndSize(rect);
	int countWindows = app.GetCountWindows();
	if (countWindows > 0) {
		int widthMenu = (header.right - header.left) / countWindows - 4;
		auto workspaces = app.GetWorkspaces();
		for (unsigned int i = 0; i < workspaces.size(); i++) {
			RECT head{ rect.left + widthMenu * i + (i + 1) * 2, rect.top, rect.left + widthMenu * (i + 1), App::headerHeight };
			auto header = *std::find_if(app.headerBtns.begin(), app.headerBtns.end(), [&](HeaderBtn * btn) {
				return btn->GetWorkspace() == *(workspaces.begin() + i);
			});
			header->SetRect(head);
			auto color = app.windows.begin() + i == app.currentWindows ? std::ACTIVE_GREY_RGB : std::GREY_RGB;
			FillRect(hdc, &head, CreateSolidBrush(color));
			head.top += 5;
			SetBkColor(hdc, color);
			SetTextColor(hdc, std::WHITE_RGB_TEXT);
			int len = workspaces[i]->GetName().length() > 20 ? 20 : workspaces[i]->GetName().length();
			DrawText(hdc, (LPCWSTR)workspaces[i]->GetName().data(), len, &head, DT_CENTER);
			SetTextColor(GetDC(workspaces[i]->GetEditBox()), std::WHITE_RGB);
			head.right -= 4;
			head.top += 2;
			DrawText(hdc, L"X", 1, &head, DT_RIGHT);
		}
	}
	if (app.currentWindows != app.windows.end()) {
		auto ws = *app.currentWindows;
		ws->Resize(rect);
		ws->SetActive(hwnd, false);
	}
}

void App::Close()
{
	for (auto window : App::Instance().windows) {
		(*window).Save();
	}
}

int App::CommandHandler(WPARAM wParam, LPARAM lParam, HWND hwnd, UINT message)
{
	int mLow = LOWORD(wParam);
	int mHigh = HIWORD(wParam);
	switch (mLow)
	{
	case IDM_ABOUT:
		DialogBox(App::Instance().hInst, MAKEINTRESOURCE(IDD_ABOUTBOX), hwnd, About);
		break;
	case ID_FILE_OPEN:
		App::Instance().Open(hwnd);
		break;
	case ID_FILE_SAVE:
		App::Instance().SaveFile(hwnd);
		break;
	case ID_INSERT:
		App::Instance().InsertText(hwnd);
		break;
	case ID_FILE_NEW:
		App::Instance().CreateNewWorkSpace();
		App::Paint(hwnd, GetDC(hwnd));
		break;
	case ID_FILE_SAVEAS:
		App::Instance().SaveFileAs(hwnd);
		break;
	default: {
		switch (mHigh) {
		case EN_CHANGE:
			App::Instance().SetChanged();
			break;
		default:
			return DefWindowProc(hwnd, message, wParam, lParam);
		}
	}
	}
	return 0;
}

LRESULT App::WndProc(HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	switch (message) {
	case WM_ERASEBKGND:
		App::Paint(hwnd, (HDC)wParam);
		return TRUE;
	case WM_DESTROY:
		App::Close();
		PostQuitMessage(NULL);
		break;
	case WM_COMMAND: {
		App::CommandHandler(wParam, lParam, hwnd, message);
		break;
	}
	case WM_LBUTTONUP: {
		int x = LOWORD(lParam);
		int y = HIWORD(lParam);
		for (auto hBtn : App::Instance().headerBtns) {
			if (hBtn->OnClick({ x,y }, hwnd)) {
				break;
			}
		}
		break;
	}
	default:
		return DefWindowProc(hwnd, message, wParam, lParam);
	}
	return 0;
}

INT_PTR App::About(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
	UNREFERENCED_PARAMETER(lParam);
	switch (message)
	{
	case WM_INITDIALOG:
		return (INT_PTR)TRUE;

	case WM_COMMAND:
		if (LOWORD(wParam) == IDOK || LOWORD(wParam) == IDCANCEL)
		{
			EndDialog(hDlg, LOWORD(wParam));
			return (INT_PTR)TRUE;
		}
		break;
	}
	return (INT_PTR)FALSE;
}

App::App(int sX, int sY, std::wstring wndName, HINSTANCE hInst) :hInst(hInst), wndName(wndName)
{
	wndClass = new  WNDCLASS{ 0 };
	wndClass->cbClsExtra = wndClass->cbWndExtra = 0;
	wndClass->hbrBackground = (HBRUSH)WHITE_BRUSH;
	wndClass->hCursor = LoadCursor(NULL, IDC_ARROW);
	wndClass->hIcon = LoadIcon(hInst, IDI_APPLICATION);
	wndClass->hInstance = hInst;
	wndClass->lpfnWndProc = nullptr;
	wndClass->lpszClassName = wndName.c_str();
	wndClass->lpszMenuName = MAKEINTRESOURCEW(IDC_NOTEPAD);
	wndClass->style = CS_HREDRAW | CS_VREDRAW;
	wndClass->lpfnWndProc = App::WndProc;
	RegisterClass(wndClass);

	App::top = sX;
	App::bottom = sY;

	this->currentWindows = windows.end();
}


App::~App()
{
	delete wndClass;
}
