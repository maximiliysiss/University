#include "StoreForm.h"

using namespace System;

[STAThreadAttribute]
int __stdcall WinMain() {
	auto form = gcnew StorePhone::StoreForm();
	form->ShowDialog();
	return 0;
}