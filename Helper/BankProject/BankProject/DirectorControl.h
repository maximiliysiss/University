#pragma once
#include "DirectorViewModel.h"
#include "WorkerViewModel.h"
#include "WindowsFunctionWrapper.h"

using namespace System;
using namespace System::ComponentModel;
using namespace System::Collections;
using namespace System::Windows::Forms;
using namespace System::Data;
using namespace System::Drawing;
using namespace BankProject::ViewModels;


namespace BankProject {

	/// <summary>
	/// Summary for DirectorControl
	/// </summary>
	public ref class DirectorControl : public System::Windows::Forms::UserControl
	{
		DirectorViewModel^ viewModel;

	public:
		DirectorControl(DirectorViewModel^ vm)
		{
			InitializeComponent();
			viewModel = vm;
			viewModel->set_onReload(gcnew Action(this, &DirectorControl::ReloadData));

			this->usersDataGridView->ContextMenuStrip = generateContextMenuForDataView(gcnew EventHandler(this, &DirectorControl::onAddNewWorker));
			this->departmentDataGridView->ContextMenuStrip = generateContextMenuForDataView(gcnew EventHandler(this, &DirectorControl::onAddNewDepartment));

			viewModel->load();
		}

	private:
	private: System::Void onAddNewDepartment(System::Object^ sender, System::EventArgs^ e) {
		viewModel->onAddNewDepartment();
	}
	private: System::Void onAddNewWorker(System::Object^ sender, System::EventArgs^ e) {
		viewModel->onAddNewWorker();
	}

	private: System::Void ReloadData() {
		this->departmentDataGridView->DataSource = viewModel->GetDepRepos();
		this->usersDataGridView->DataSource = viewModel->GetWorkerRepos();
	}

	protected:
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		~DirectorControl()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::DataGridView^ usersDataGridView;
	protected:







	private: System::Windows::Forms::TabControl^ tabControl1;
	private: System::Windows::Forms::TabPage^ workerTabPage;
	private: System::Windows::Forms::TabPage^ tabPage1;
	private: System::Windows::Forms::DataGridView^ departmentDataGridView;


























	private:
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System::ComponentModel::Container^ components;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		void InitializeComponent(void)
		{
			this->usersDataGridView = (gcnew System::Windows::Forms::DataGridView());
			this->tabControl1 = (gcnew System::Windows::Forms::TabControl());
			this->workerTabPage = (gcnew System::Windows::Forms::TabPage());
			this->tabPage1 = (gcnew System::Windows::Forms::TabPage());
			this->departmentDataGridView = (gcnew System::Windows::Forms::DataGridView());
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->usersDataGridView))->BeginInit();
			this->tabControl1->SuspendLayout();
			this->workerTabPage->SuspendLayout();
			this->tabPage1->SuspendLayout();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->departmentDataGridView))->BeginInit();
			this->SuspendLayout();
			// 
			// usersDataGridView
			// 
			this->usersDataGridView->AllowUserToAddRows = false;
			this->usersDataGridView->AllowUserToDeleteRows = false;
			this->usersDataGridView->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
			this->usersDataGridView->Dock = System::Windows::Forms::DockStyle::Fill;
			this->usersDataGridView->Location = System::Drawing::Point(3, 3);
			this->usersDataGridView->Name = L"usersDataGridView";
			this->usersDataGridView->ReadOnly = true;
			this->usersDataGridView->SelectionMode = System::Windows::Forms::DataGridViewSelectionMode::FullRowSelect;
			this->usersDataGridView->Size = System::Drawing::Size(586, 368);
			this->usersDataGridView->TabIndex = 0;
			this->usersDataGridView->CellDoubleClick += gcnew System::Windows::Forms::DataGridViewCellEventHandler(this, &DirectorControl::usersDataGridView_CellDoubleClick);
			// 
			// tabControl1
			// 
			this->tabControl1->Controls->Add(this->workerTabPage);
			this->tabControl1->Controls->Add(this->tabPage1);
			this->tabControl1->Dock = System::Windows::Forms::DockStyle::Fill;
			this->tabControl1->Location = System::Drawing::Point(0, 0);
			this->tabControl1->Name = L"tabControl1";
			this->tabControl1->SelectedIndex = 0;
			this->tabControl1->Size = System::Drawing::Size(600, 400);
			this->tabControl1->TabIndex = 1;
			// 
			// workerTabPage
			// 
			this->workerTabPage->Controls->Add(this->usersDataGridView);
			this->workerTabPage->Location = System::Drawing::Point(4, 22);
			this->workerTabPage->Name = L"workerTabPage";
			this->workerTabPage->Padding = System::Windows::Forms::Padding(3);
			this->workerTabPage->Size = System::Drawing::Size(592, 374);
			this->workerTabPage->TabIndex = 0;
			this->workerTabPage->Text = L"Workers";
			this->workerTabPage->UseVisualStyleBackColor = true;
			// 
			// tabPage1
			// 
			this->tabPage1->Controls->Add(this->departmentDataGridView);
			this->tabPage1->Location = System::Drawing::Point(4, 22);
			this->tabPage1->Name = L"tabPage1";
			this->tabPage1->Padding = System::Windows::Forms::Padding(3);
			this->tabPage1->Size = System::Drawing::Size(592, 374);
			this->tabPage1->TabIndex = 1;
			this->tabPage1->Text = L"Departments";
			this->tabPage1->UseVisualStyleBackColor = true;
			// 
			// departmentDataGridView
			// 
			this->departmentDataGridView->AllowUserToAddRows = false;
			this->departmentDataGridView->AllowUserToDeleteRows = false;
			this->departmentDataGridView->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
			this->departmentDataGridView->Dock = System::Windows::Forms::DockStyle::Fill;
			this->departmentDataGridView->Location = System::Drawing::Point(3, 3);
			this->departmentDataGridView->Name = L"departmentDataGridView";
			this->departmentDataGridView->ReadOnly = true;
			this->departmentDataGridView->SelectionMode = System::Windows::Forms::DataGridViewSelectionMode::FullRowSelect;
			this->departmentDataGridView->Size = System::Drawing::Size(586, 368);
			this->departmentDataGridView->TabIndex = 0;
			this->departmentDataGridView->CellDoubleClick += gcnew System::Windows::Forms::DataGridViewCellEventHandler(this, &DirectorControl::departmentDataGridView_CellDoubleClick);
			// 
			// DirectorControl
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->Controls->Add(this->tabControl1);
			this->Name = L"DirectorControl";
			this->Size = System::Drawing::Size(600, 400);
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->usersDataGridView))->EndInit();
			this->tabControl1->ResumeLayout(false);
			this->workerTabPage->ResumeLayout(false);
			this->tabPage1->ResumeLayout(false);
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->departmentDataGridView))->EndInit();
			this->ResumeLayout(false);

		}
#pragma endregion
	private: System::Void departmentDataGridView_CellDoubleClick(System::Object^ sender, System::Windows::Forms::DataGridViewCellEventArgs^ e) {
		viewModel->onOpenDepartment(e->RowIndex);
	}
	private: System::Void usersDataGridView_CellDoubleClick(System::Object^ sender, System::Windows::Forms::DataGridViewCellEventArgs^ e) {
		viewModel->onOpenWorker(e->RowIndex);
	}
	};
}
