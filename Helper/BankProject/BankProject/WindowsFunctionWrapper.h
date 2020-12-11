#pragma once

using namespace System::Windows::Forms;

ref class WindowsCloseFunc {
	Form^ form;
public:
	WindowsCloseFunc(Form^ form) :form(form) {}

	void operator()() {
		form->Hide();
		form->Close();
	}
};