#include "WindowsFunctionWrapper.h"

ContextMenuStrip^ generateContextMenuForDataView(System::EventHandler^ onAddNewElement)
{
	ContextMenuStrip^ menu = gcnew ContextMenuStrip();
	ToolStripMenuItem^ menuItem = gcnew ToolStripMenuItem("Add");
	menuItem->Click += onAddNewElement;
	menu->Items->Add(menuItem);
	return menu;
}
