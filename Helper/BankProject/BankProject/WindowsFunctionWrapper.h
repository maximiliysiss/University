#pragma once

using namespace System::Windows::Forms;

// Генерация меню для таблиц
ContextMenuStrip^ generateContextMenuForDataView(System::EventHandler^ onAddNewElement);

// Функция закрытия окна
ref class WindowsCloseFunc {
	Form^ form;
public:
	WindowsCloseFunc(Form^ form) :form(form) {}

	void operator()() {
		form->Hide();
		form->Close();
	}
};