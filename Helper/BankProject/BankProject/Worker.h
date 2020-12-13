#pragma once
#include "WorkerViewModel.h"

namespace BankProject {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	/// <summary>
	/// Summary for Worker
	/// </summary>
	public ref class Worker : public System::Windows::Forms::Form
	{
		ViewModels::WorkerViewModel^ viewModel;
	public:
		Worker(ViewModels::WorkerViewModel^ viewModel)
		{
			InitializeComponent();

			this->viewModel = viewModel;

			this->nameTextBox->Text = this->viewModel->Name;
			this->surnameTextBox->Text = this->viewModel->Surname;
			this->birthplaceTextBox->Text = this->viewModel->Birthplace;
			this->passportTextBox->Text = this->viewModel->Passport;
			this->birthdayDateTimePicker->Value = DateTime::Parse(this->viewModel->Birthday);
			this->loginTextBox->Text = this->viewModel->Login;

			if (this->viewModel->isNew())
				deleteButton->Visible = false;

			this->departmentComboBox->DataSource = this->viewModel->GetDeparaments();
			this->departmentComboBox->SelectedItem = this->viewModel->getSelectedDepartment();
		}

	protected:
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		~Worker()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::Button^ deleteButton;
	protected:

	protected:

	private: System::Windows::Forms::Button^ saveButton;




	private: System::Windows::Forms::DateTimePicker^ birthdayDateTimePicker;


	private: System::Windows::Forms::TextBox^ birthplaceTextBox;
	private: System::Windows::Forms::Label^ label5;
	private: System::Windows::Forms::Label^ label4;
	private: System::Windows::Forms::TextBox^ passportTextBox;
	private: System::Windows::Forms::Label^ label3;
	private: System::Windows::Forms::TextBox^ surnameTextBox;
	private: System::Windows::Forms::Label^ label2;
	private: System::Windows::Forms::TextBox^ nameTextBox;
	private: System::Windows::Forms::Label^ label1;
	private: System::Windows::Forms::TextBox^ passwordTextBox;

	private: System::Windows::Forms::Label^ label6;
	private: System::Windows::Forms::TextBox^ loginTextBox;
	private: System::Windows::Forms::Label^ label7;

	private: System::Windows::Forms::Label^ label8;
	private: System::Windows::Forms::ComboBox^ departmentComboBox;


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
			this->deleteButton = (gcnew System::Windows::Forms::Button());
			this->saveButton = (gcnew System::Windows::Forms::Button());
			this->birthdayDateTimePicker = (gcnew System::Windows::Forms::DateTimePicker());
			this->birthplaceTextBox = (gcnew System::Windows::Forms::TextBox());
			this->label5 = (gcnew System::Windows::Forms::Label());
			this->label4 = (gcnew System::Windows::Forms::Label());
			this->passportTextBox = (gcnew System::Windows::Forms::TextBox());
			this->label3 = (gcnew System::Windows::Forms::Label());
			this->surnameTextBox = (gcnew System::Windows::Forms::TextBox());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->nameTextBox = (gcnew System::Windows::Forms::TextBox());
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->passwordTextBox = (gcnew System::Windows::Forms::TextBox());
			this->label6 = (gcnew System::Windows::Forms::Label());
			this->loginTextBox = (gcnew System::Windows::Forms::TextBox());
			this->label7 = (gcnew System::Windows::Forms::Label());
			this->label8 = (gcnew System::Windows::Forms::Label());
			this->departmentComboBox = (gcnew System::Windows::Forms::ComboBox());
			this->SuspendLayout();
			// 
			// deleteButton
			// 
			this->deleteButton->Location = System::Drawing::Point(415, 240);
			this->deleteButton->Name = L"deleteButton";
			this->deleteButton->Size = System::Drawing::Size(75, 23);
			this->deleteButton->TabIndex = 40;
			this->deleteButton->Text = L"Delete";
			this->deleteButton->UseVisualStyleBackColor = true;
			this->deleteButton->Click += gcnew System::EventHandler(this, &Worker::deleteButton_Click);
			// 
			// saveButton
			// 
			this->saveButton->Location = System::Drawing::Point(10, 240);
			this->saveButton->Name = L"saveButton";
			this->saveButton->Size = System::Drawing::Size(75, 23);
			this->saveButton->TabIndex = 39;
			this->saveButton->Text = L"Save";
			this->saveButton->UseVisualStyleBackColor = true;
			this->saveButton->Click += gcnew System::EventHandler(this, &Worker::saveButton_Click);
			// 
			// birthdayDateTimePicker
			// 
			this->birthdayDateTimePicker->Location = System::Drawing::Point(108, 117);
			this->birthdayDateTimePicker->Name = L"birthdayDateTimePicker";
			this->birthdayDateTimePicker->Size = System::Drawing::Size(382, 20);
			this->birthdayDateTimePicker->TabIndex = 35;
			this->birthdayDateTimePicker->ValueChanged += gcnew System::EventHandler(this, &Worker::birthdayDateTimePicker_ValueChanged);
			// 
			// birthplaceTextBox
			// 
			this->birthplaceTextBox->Location = System::Drawing::Point(108, 143);
			this->birthplaceTextBox->Name = L"birthplaceTextBox";
			this->birthplaceTextBox->Size = System::Drawing::Size(382, 20);
			this->birthplaceTextBox->TabIndex = 32;
			this->birthplaceTextBox->TextChanged += gcnew System::EventHandler(this, &Worker::birthplaceTextBox_TextChanged);
			// 
			// label5
			// 
			this->label5->AutoSize = true;
			this->label5->Location = System::Drawing::Point(10, 146);
			this->label5->Name = L"label5";
			this->label5->Size = System::Drawing::Size(92, 13);
			this->label5->TabIndex = 31;
			this->label5->Text = L"Место рождения";
			// 
			// label4
			// 
			this->label4->AutoSize = true;
			this->label4->Location = System::Drawing::Point(10, 120);
			this->label4->Name = L"label4";
			this->label4->Size = System::Drawing::Size(86, 13);
			this->label4->TabIndex = 30;
			this->label4->Text = L"Дата рождения";
			// 
			// passportTextBox
			// 
			this->passportTextBox->Location = System::Drawing::Point(69, 91);
			this->passportTextBox->Name = L"passportTextBox";
			this->passportTextBox->Size = System::Drawing::Size(421, 20);
			this->passportTextBox->TabIndex = 29;
			this->passportTextBox->TextChanged += gcnew System::EventHandler(this, &Worker::passportTextBox_TextChanged);
			// 
			// label3
			// 
			this->label3->AutoSize = true;
			this->label3->Location = System::Drawing::Point(10, 94);
			this->label3->Name = L"label3";
			this->label3->Size = System::Drawing::Size(50, 13);
			this->label3->TabIndex = 28;
			this->label3->Text = L"Паспорт";
			// 
			// surnameTextBox
			// 
			this->surnameTextBox->Location = System::Drawing::Point(69, 65);
			this->surnameTextBox->Name = L"surnameTextBox";
			this->surnameTextBox->Size = System::Drawing::Size(421, 20);
			this->surnameTextBox->TabIndex = 27;
			this->surnameTextBox->TextChanged += gcnew System::EventHandler(this, &Worker::surnameTextBox_TextChanged);
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Location = System::Drawing::Point(10, 68);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(56, 13);
			this->label2->TabIndex = 26;
			this->label2->Text = L"Фамилия";
			// 
			// nameTextBox
			// 
			this->nameTextBox->Location = System::Drawing::Point(69, 39);
			this->nameTextBox->Name = L"nameTextBox";
			this->nameTextBox->Size = System::Drawing::Size(421, 20);
			this->nameTextBox->TabIndex = 25;
			this->nameTextBox->TextChanged += gcnew System::EventHandler(this, &Worker::nameTextBox_TextChanged);
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Location = System::Drawing::Point(10, 42);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(29, 13);
			this->label1->TabIndex = 24;
			this->label1->Text = L"Имя";
			// 
			// passwordTextBox
			// 
			this->passwordTextBox->Location = System::Drawing::Point(108, 169);
			this->passwordTextBox->Name = L"passwordTextBox";
			this->passwordTextBox->PasswordChar = '*';
			this->passwordTextBox->Size = System::Drawing::Size(382, 20);
			this->passwordTextBox->TabIndex = 42;
			this->passwordTextBox->TextChanged += gcnew System::EventHandler(this, &Worker::password_TextChanged);
			// 
			// label6
			// 
			this->label6->AutoSize = true;
			this->label6->Location = System::Drawing::Point(10, 172);
			this->label6->Name = L"label6";
			this->label6->Size = System::Drawing::Size(45, 13);
			this->label6->TabIndex = 41;
			this->label6->Text = L"Пароль";
			// 
			// loginTextBox
			// 
			this->loginTextBox->Location = System::Drawing::Point(69, 12);
			this->loginTextBox->Name = L"loginTextBox";
			this->loginTextBox->Size = System::Drawing::Size(421, 20);
			this->loginTextBox->TabIndex = 44;
			this->loginTextBox->TextChanged += gcnew System::EventHandler(this, &Worker::loginTextBox_TextChanged);
			// 
			// label7
			// 
			this->label7->AutoSize = true;
			this->label7->Location = System::Drawing::Point(10, 15);
			this->label7->Name = L"label7";
			this->label7->Size = System::Drawing::Size(38, 13);
			this->label7->TabIndex = 43;
			this->label7->Text = L"Логин";
			// 
			// label8
			// 
			this->label8->AutoSize = true;
			this->label8->Location = System::Drawing::Point(10, 198);
			this->label8->Name = L"label8";
			this->label8->Size = System::Drawing::Size(76, 13);
			this->label8->TabIndex = 45;
			this->label8->Text = L"Департамент";
			// 
			// departmentComboBox
			// 
			this->departmentComboBox->DisplayMember = L"Name";
			this->departmentComboBox->FormattingEnabled = true;
			this->departmentComboBox->Location = System::Drawing::Point(108, 195);
			this->departmentComboBox->Name = L"departmentComboBox";
			this->departmentComboBox->Size = System::Drawing::Size(382, 21);
			this->departmentComboBox->TabIndex = 46;
			this->departmentComboBox->SelectedIndexChanged += gcnew System::EventHandler(this, &Worker::departmentComboBox_SelectedIndexChanged);
			// 
			// Worker
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(502, 275);
			this->Controls->Add(this->departmentComboBox);
			this->Controls->Add(this->label8);
			this->Controls->Add(this->loginTextBox);
			this->Controls->Add(this->label7);
			this->Controls->Add(this->passwordTextBox);
			this->Controls->Add(this->label6);
			this->Controls->Add(this->deleteButton);
			this->Controls->Add(this->saveButton);
			this->Controls->Add(this->birthdayDateTimePicker);
			this->Controls->Add(this->birthplaceTextBox);
			this->Controls->Add(this->label5);
			this->Controls->Add(this->label4);
			this->Controls->Add(this->passportTextBox);
			this->Controls->Add(this->label3);
			this->Controls->Add(this->surnameTextBox);
			this->Controls->Add(this->label2);
			this->Controls->Add(this->nameTextBox);
			this->Controls->Add(this->label1);
			this->FormBorderStyle = System::Windows::Forms::FormBorderStyle::FixedSingle;
			this->MaximizeBox = false;
			this->Name = L"Worker";
			this->Text = L"Worker";
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion
	private: System::Void deleteButton_Click(System::Object^ sender, System::EventArgs^ e) {
		viewModel->Remove();
		Close();
	}
	private: System::Void saveButton_Click(System::Object^ sender, System::EventArgs^ e) {
		viewModel->Save();
		Close();
	}
	private: System::Void nameTextBox_TextChanged(System::Object^ sender, System::EventArgs^ e) {
		viewModel->Name = nameTextBox->Text->Trim();
	}
	private: System::Void surnameTextBox_TextChanged(System::Object^ sender, System::EventArgs^ e) {
		viewModel->Surname = surnameTextBox->Text->Trim();
	}
	private: System::Void passportTextBox_TextChanged(System::Object^ sender, System::EventArgs^ e) {
		viewModel->Passport = passportTextBox->Text->Trim();
	}
	private: System::Void birthplaceTextBox_TextChanged(System::Object^ sender, System::EventArgs^ e) {
		viewModel->Birthplace = birthplaceTextBox->Text->Trim();
	}
	private: System::Void birthdayDateTimePicker_ValueChanged(System::Object^ sender, System::EventArgs^ e) {
		viewModel->Birthday = birthdayDateTimePicker->Value.ToString("dd-MM-yyyy");
	}
	private: System::Void password_TextChanged(System::Object^ sender, System::EventArgs^ e) {
		viewModel->Password = passwordTextBox->Text->Trim();
	}
	private: System::Void loginTextBox_TextChanged(System::Object^ sender, System::EventArgs^ e) {
		viewModel->Login = loginTextBox->Text->ToString();
	}
	private: System::Void departmentComboBox_SelectedIndexChanged(System::Object^ sender, System::EventArgs^ e) {
		viewModel->setDepartment(((ViewModels::DepartmentViewModel^)departmentComboBox->SelectedItem)->Id);
	}
	};
}
