#pragma once

using namespace System;
using namespace System::ComponentModel;
using namespace System::Collections;
using namespace System::Windows::Forms;
using namespace System::Data;
using namespace System::Drawing;


namespace BankProject {

	/// <summary>
	/// Summary for DirectorControl
	/// </summary>
	public ref class DirectorControl : public System::Windows::Forms::UserControl
	{
	public:
		DirectorControl(void)
		{
			InitializeComponent();
			//
			//TODO: Add the constructor code here
			//
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
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ Id;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ NameColumn;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ Surname;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ Passport;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ Birthday;

	private:
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System::ComponentModel::Container ^components;

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
			this->Id = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->NameColumn = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->Surname = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->Passport = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->Birthday = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->usersDataGridView))->BeginInit();
			this->tabControl1->SuspendLayout();
			this->workerTabPage->SuspendLayout();
			this->SuspendLayout();
			// 
			// usersDataGridView
			// 
			this->usersDataGridView->AllowUserToAddRows = false;
			this->usersDataGridView->AllowUserToDeleteRows = false;
			this->usersDataGridView->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
			this->usersDataGridView->Columns->AddRange(gcnew cli::array< System::Windows::Forms::DataGridViewColumn^  >(5) {
				this->Id,
					this->NameColumn, this->Surname, this->Passport, this->Birthday
			});
			this->usersDataGridView->Dock = System::Windows::Forms::DockStyle::Fill;
			this->usersDataGridView->Location = System::Drawing::Point(3, 3);
			this->usersDataGridView->Name = L"usersDataGridView";
			this->usersDataGridView->ReadOnly = true;
			this->usersDataGridView->Size = System::Drawing::Size(586, 368);
			this->usersDataGridView->TabIndex = 0;
			// 
			// tabControl1
			// 
			this->tabControl1->Controls->Add(this->workerTabPage);
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
			// Id
			// 
			this->Id->HeaderText = L"Id";
			this->Id->Name = L"Id";
			this->Id->ReadOnly = true;
			// 
			// Name
			// 
			this->NameColumn->HeaderText = L"Name";
			this->NameColumn->Name = L"Name";
			this->NameColumn->ReadOnly = true;
			// 
			// Surname
			// 
			this->Surname->HeaderText = L"Surname";
			this->Surname->Name = L"Surname";
			this->Surname->ReadOnly = true;
			// 
			// Passport
			// 
			this->Passport->HeaderText = L"Passport";
			this->Passport->Name = L"Passport";
			this->Passport->ReadOnly = true;
			// 
			// Birthday
			// 
			this->Birthday->HeaderText = L"Birthday";
			this->Birthday->Name = L"Birthday";
			this->Birthday->ReadOnly = true;
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
			this->ResumeLayout(false);

		}
#pragma endregion
};
}
