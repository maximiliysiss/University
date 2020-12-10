#pragma once

using namespace System;
using namespace System::ComponentModel;
using namespace System::Collections;
using namespace System::Windows::Forms;
using namespace System::Data;
using namespace System::Drawing;


namespace BankProject {

	/// <summary>
	/// Summary for ManagerControl
	/// </summary>
	public ref class ManagerControl : public System::Windows::Forms::UserControl
	{
	public:
		ManagerControl(void)
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
		~ManagerControl()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::TabControl^ tabControl1;
	private: System::Windows::Forms::TabPage^ clientTabPage;
	private: System::Windows::Forms::DataGridView^ usersDataGridView;

	private: System::Windows::Forms::DataGridViewTextBoxColumn^ Id;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ NameColumn;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ Surname;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ Birthdate;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ Birthplace;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ Account;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ Passport;
	protected:



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
			this->tabControl1 = (gcnew System::Windows::Forms::TabControl());
			this->clientTabPage = (gcnew System::Windows::Forms::TabPage());
			this->usersDataGridView = (gcnew System::Windows::Forms::DataGridView());
			this->Id = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->NameColumn = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->Surname = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->Birthdate = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->Birthplace = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->Account = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->Passport = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->tabControl1->SuspendLayout();
			this->clientTabPage->SuspendLayout();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->usersDataGridView))->BeginInit();
			this->SuspendLayout();
			// 
			// tabControl1
			// 
			this->tabControl1->Controls->Add(this->clientTabPage);
			this->tabControl1->Dock = System::Windows::Forms::DockStyle::Fill;
			this->tabControl1->Location = System::Drawing::Point(0, 0);
			this->tabControl1->Name = L"tabControl1";
			this->tabControl1->SelectedIndex = 0;
			this->tabControl1->Size = System::Drawing::Size(566, 403);
			this->tabControl1->TabIndex = 0;
			// 
			// clientTabPage
			// 
			this->clientTabPage->Controls->Add(this->usersDataGridView);
			this->clientTabPage->Location = System::Drawing::Point(4, 22);
			this->clientTabPage->Name = L"clientTabPage";
			this->clientTabPage->Padding = System::Windows::Forms::Padding(3);
			this->clientTabPage->Size = System::Drawing::Size(558, 377);
			this->clientTabPage->TabIndex = 0;
			this->clientTabPage->Text = L"Clients";
			this->clientTabPage->UseVisualStyleBackColor = true;
			// 
			// usersDataGridView
			// 
			this->usersDataGridView->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
			this->usersDataGridView->Columns->AddRange(gcnew cli::array< System::Windows::Forms::DataGridViewColumn^  >(7) {
				this->Id,
					this->NameColumn, this->Surname, this->Birthdate, this->Birthplace, this->Account, this->Passport
			});
			this->usersDataGridView->Dock = System::Windows::Forms::DockStyle::Fill;
			this->usersDataGridView->Location = System::Drawing::Point(3, 3);
			this->usersDataGridView->Name = L"usersDataGridView";
			this->usersDataGridView->Size = System::Drawing::Size(552, 371);
			this->usersDataGridView->TabIndex = 0;
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
			// Birthdate
			// 
			this->Birthdate->HeaderText = L"Birthdate";
			this->Birthdate->Name = L"Birthdate";
			this->Birthdate->ReadOnly = true;
			// 
			// Birthplace
			// 
			this->Birthplace->HeaderText = L"Birthplace";
			this->Birthplace->Name = L"Birthplace";
			this->Birthplace->ReadOnly = true;
			// 
			// Account
			// 
			this->Account->HeaderText = L"Account";
			this->Account->Name = L"Account";
			this->Account->ReadOnly = true;
			// 
			// Passport
			// 
			this->Passport->HeaderText = L"Passport";
			this->Passport->Name = L"Passport";
			this->Passport->ReadOnly = true;
			// 
			// ManagerControl
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->Controls->Add(this->tabControl1);
			this->Name = L"ManagerControl";
			this->Size = System::Drawing::Size(780, 403);
			this->tabControl1->ResumeLayout(false);
			this->clientTabPage->ResumeLayout(false);
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->usersDataGridView))->EndInit();
			this->ResumeLayout(false);

		}
#pragma endregion
	};
}
