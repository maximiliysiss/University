#pragma once
#include "ClientViewModel.h"

namespace BankProject {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	/// <summary>
	/// Summary for Client
	/// </summary>
	public ref class Client : public System::Windows::Forms::Form
	{
	private:
		ViewModels::ClientViewModel^ viewModel;
	public:
		Client(ViewModels::ClientViewModel^ viewModel)
		{
			this->viewModel = viewModel;

			InitializeComponent();

			if (viewModel->isNew()) {
				transactionButton->Visible = false;
				deleteButton->Visible = false;
				addMoneyButton->Visible = false;
			}

			/// <summary>
			/// Заполнение данными
			/// </summary>
			/// <param name="viewModel"></param>
			this->nameTextBox->Text = viewModel->Name;
			this->surnameTextBox->Text = viewModel->Surname;
			this->passportTextBox->Text = viewModel->Passport;
			this->birthdayDateTimePicker->Value = DateTime::Parse(viewModel->Birthday);
			this->loginTextBox->Text = viewModel->Login;
			this->birthplaceTextBox->Text = viewModel->Birthplace;

			viewModel->set_reload(gcnew Action(this, &Client::reloadData));
			viewModel->load();
		}

		void reloadData() {
			this->transactionDataGridView->DataSource = viewModel->get_transactions();

			this->accountTextBox->Text = viewModel->AccountIdStr;
			this->valueTextBox->Text = viewModel->Value;
		}

	protected:
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		~Client()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::Label^ label1;
	protected:
	private: System::Windows::Forms::TextBox^ nameTextBox;
	private: System::Windows::Forms::TextBox^ surnameTextBox;
	private: System::Windows::Forms::Label^ label2;
	private: System::Windows::Forms::TextBox^ passportTextBox;

	private: System::Windows::Forms::Label^ label3;
	private: System::Windows::Forms::Label^ label4;
	private: System::Windows::Forms::TextBox^ birthplaceTextBox;

	private: System::Windows::Forms::Label^ label5;
	private: System::Windows::Forms::TextBox^ accountTextBox;

	private: System::Windows::Forms::Label^ label6;
	private: System::Windows::Forms::DateTimePicker^ birthdayDateTimePicker;
	private: System::Windows::Forms::TextBox^ valueTextBox;


	private: System::Windows::Forms::Label^ label7;
	private: System::Windows::Forms::Button^ saveButton;
	private: System::Windows::Forms::Button^ deleteButton;
	private: System::Windows::Forms::Button^ transactionButton;
	private: System::Windows::Forms::DataGridView^ transactionDataGridView;
	private: System::Windows::Forms::TextBox^ passwordTextBox;

	private: System::Windows::Forms::Label^ label8;
	private: System::Windows::Forms::TextBox^ loginTextBox;
	private: System::Windows::Forms::Label^ label9;
	private: System::Windows::Forms::Button^ addMoneyButton;






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
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->nameTextBox = (gcnew System::Windows::Forms::TextBox());
			this->surnameTextBox = (gcnew System::Windows::Forms::TextBox());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->passportTextBox = (gcnew System::Windows::Forms::TextBox());
			this->label3 = (gcnew System::Windows::Forms::Label());
			this->label4 = (gcnew System::Windows::Forms::Label());
			this->birthplaceTextBox = (gcnew System::Windows::Forms::TextBox());
			this->label5 = (gcnew System::Windows::Forms::Label());
			this->accountTextBox = (gcnew System::Windows::Forms::TextBox());
			this->label6 = (gcnew System::Windows::Forms::Label());
			this->birthdayDateTimePicker = (gcnew System::Windows::Forms::DateTimePicker());
			this->valueTextBox = (gcnew System::Windows::Forms::TextBox());
			this->label7 = (gcnew System::Windows::Forms::Label());
			this->saveButton = (gcnew System::Windows::Forms::Button());
			this->deleteButton = (gcnew System::Windows::Forms::Button());
			this->transactionButton = (gcnew System::Windows::Forms::Button());
			this->transactionDataGridView = (gcnew System::Windows::Forms::DataGridView());
			this->passwordTextBox = (gcnew System::Windows::Forms::TextBox());
			this->label8 = (gcnew System::Windows::Forms::Label());
			this->loginTextBox = (gcnew System::Windows::Forms::TextBox());
			this->label9 = (gcnew System::Windows::Forms::Label());
			this->addMoneyButton = (gcnew System::Windows::Forms::Button());
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->transactionDataGridView))->BeginInit();
			this->SuspendLayout();
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Location = System::Drawing::Point(12, 68);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(29, 13);
			this->label1->TabIndex = 0;
			this->label1->Text = L"Имя";
			// 
			// nameTextBox
			// 
			this->nameTextBox->Location = System::Drawing::Point(71, 65);
			this->nameTextBox->Name = L"nameTextBox";
			this->nameTextBox->Size = System::Drawing::Size(421, 20);
			this->nameTextBox->TabIndex = 2;
			this->nameTextBox->TextChanged += gcnew System::EventHandler(this, &Client::nameTextBox_TextChanged);
			// 
			// surnameTextBox
			// 
			this->surnameTextBox->Location = System::Drawing::Point(71, 91);
			this->surnameTextBox->Name = L"surnameTextBox";
			this->surnameTextBox->Size = System::Drawing::Size(421, 20);
			this->surnameTextBox->TabIndex = 4;
			this->surnameTextBox->TextChanged += gcnew System::EventHandler(this, &Client::surnameTextBox_TextChanged);
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Location = System::Drawing::Point(12, 94);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(56, 13);
			this->label2->TabIndex = 3;
			this->label2->Text = L"Фамилия";
			// 
			// passportTextBox
			// 
			this->passportTextBox->Location = System::Drawing::Point(71, 117);
			this->passportTextBox->Name = L"passportTextBox";
			this->passportTextBox->Size = System::Drawing::Size(421, 20);
			this->passportTextBox->TabIndex = 6;
			this->passportTextBox->TextChanged += gcnew System::EventHandler(this, &Client::passportTextBox_TextChanged);
			// 
			// label3
			// 
			this->label3->AutoSize = true;
			this->label3->Location = System::Drawing::Point(12, 120);
			this->label3->Name = L"label3";
			this->label3->Size = System::Drawing::Size(50, 13);
			this->label3->TabIndex = 5;
			this->label3->Text = L"Паспорт";
			// 
			// label4
			// 
			this->label4->AutoSize = true;
			this->label4->Location = System::Drawing::Point(12, 146);
			this->label4->Name = L"label4";
			this->label4->Size = System::Drawing::Size(86, 13);
			this->label4->TabIndex = 7;
			this->label4->Text = L"Дата рождения";
			// 
			// birthplaceTextBox
			// 
			this->birthplaceTextBox->Location = System::Drawing::Point(110, 169);
			this->birthplaceTextBox->Name = L"birthplaceTextBox";
			this->birthplaceTextBox->Size = System::Drawing::Size(382, 20);
			this->birthplaceTextBox->TabIndex = 10;
			this->birthplaceTextBox->TextChanged += gcnew System::EventHandler(this, &Client::birthplaceTextBox_TextChanged);
			// 
			// label5
			// 
			this->label5->AutoSize = true;
			this->label5->Location = System::Drawing::Point(12, 172);
			this->label5->Name = L"label5";
			this->label5->Size = System::Drawing::Size(92, 13);
			this->label5->TabIndex = 9;
			this->label5->Text = L"Место рождения";
			// 
			// accountTextBox
			// 
			this->accountTextBox->Location = System::Drawing::Point(71, 195);
			this->accountTextBox->Name = L"accountTextBox";
			this->accountTextBox->ReadOnly = true;
			this->accountTextBox->Size = System::Drawing::Size(421, 20);
			this->accountTextBox->TabIndex = 12;
			// 
			// label6
			// 
			this->label6->AutoSize = true;
			this->label6->Location = System::Drawing::Point(12, 198);
			this->label6->Name = L"label6";
			this->label6->Size = System::Drawing::Size(30, 13);
			this->label6->TabIndex = 11;
			this->label6->Text = L"Счет";
			// 
			// birthdayDateTimePicker
			// 
			this->birthdayDateTimePicker->Location = System::Drawing::Point(110, 143);
			this->birthdayDateTimePicker->Name = L"birthdayDateTimePicker";
			this->birthdayDateTimePicker->Size = System::Drawing::Size(382, 20);
			this->birthdayDateTimePicker->TabIndex = 17;
			this->birthdayDateTimePicker->ValueChanged += gcnew System::EventHandler(this, &Client::birthdayDateTimePicker_ValueChanged);
			// 
			// valueTextBox
			// 
			this->valueTextBox->Location = System::Drawing::Point(71, 221);
			this->valueTextBox->Name = L"valueTextBox";
			this->valueTextBox->ReadOnly = true;
			this->valueTextBox->Size = System::Drawing::Size(421, 20);
			this->valueTextBox->TabIndex = 19;
			// 
			// label7
			// 
			this->label7->AutoSize = true;
			this->label7->Location = System::Drawing::Point(12, 224);
			this->label7->Name = L"label7";
			this->label7->Size = System::Drawing::Size(44, 13);
			this->label7->TabIndex = 18;
			this->label7->Text = L"Баланс";
			// 
			// saveButton
			// 
			this->saveButton->Location = System::Drawing::Point(12, 456);
			this->saveButton->Name = L"saveButton";
			this->saveButton->Size = System::Drawing::Size(75, 23);
			this->saveButton->TabIndex = 21;
			this->saveButton->Text = L"Save";
			this->saveButton->UseVisualStyleBackColor = true;
			this->saveButton->Click += gcnew System::EventHandler(this, &Client::saveButton_Click);
			// 
			// deleteButton
			// 
			this->deleteButton->Location = System::Drawing::Point(93, 456);
			this->deleteButton->Name = L"deleteButton";
			this->deleteButton->Size = System::Drawing::Size(75, 23);
			this->deleteButton->TabIndex = 22;
			this->deleteButton->Text = L"Delete";
			this->deleteButton->UseVisualStyleBackColor = true;
			this->deleteButton->Click += gcnew System::EventHandler(this, &Client::deleteButton_Click);
			// 
			// transactionButton
			// 
			this->transactionButton->Location = System::Drawing::Point(343, 456);
			this->transactionButton->Name = L"transactionButton";
			this->transactionButton->Size = System::Drawing::Size(149, 23);
			this->transactionButton->TabIndex = 23;
			this->transactionButton->Text = L"Create transaction";
			this->transactionButton->UseVisualStyleBackColor = true;
			this->transactionButton->Click += gcnew System::EventHandler(this, &Client::transactionButton_Click);
			// 
			// transactionDataGridView
			// 
			this->transactionDataGridView->AllowUserToAddRows = false;
			this->transactionDataGridView->AllowUserToDeleteRows = false;
			this->transactionDataGridView->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
			this->transactionDataGridView->Location = System::Drawing::Point(12, 252);
			this->transactionDataGridView->Name = L"transactionDataGridView";
			this->transactionDataGridView->ReadOnly = true;
			this->transactionDataGridView->SelectionMode = System::Windows::Forms::DataGridViewSelectionMode::FullRowSelect;
			this->transactionDataGridView->Size = System::Drawing::Size(480, 198);
			this->transactionDataGridView->TabIndex = 24;
			// 
			// passwordTextBox
			// 
			this->passwordTextBox->Location = System::Drawing::Point(71, 39);
			this->passwordTextBox->Name = L"passwordTextBox";
			this->passwordTextBox->PasswordChar = '*';
			this->passwordTextBox->Size = System::Drawing::Size(421, 20);
			this->passwordTextBox->TabIndex = 26;
			this->passwordTextBox->TextChanged += gcnew System::EventHandler(this, &Client::passwordTextBox_TextChanged);
			// 
			// label8
			// 
			this->label8->AutoSize = true;
			this->label8->Location = System::Drawing::Point(12, 42);
			this->label8->Name = L"label8";
			this->label8->Size = System::Drawing::Size(45, 13);
			this->label8->TabIndex = 25;
			this->label8->Text = L"Пароль";
			// 
			// loginTextBox
			// 
			this->loginTextBox->Location = System::Drawing::Point(71, 13);
			this->loginTextBox->Name = L"loginTextBox";
			this->loginTextBox->Size = System::Drawing::Size(421, 20);
			this->loginTextBox->TabIndex = 28;
			this->loginTextBox->TextChanged += gcnew System::EventHandler(this, &Client::loginTextBox_TextChanged);
			// 
			// label9
			// 
			this->label9->AutoSize = true;
			this->label9->Location = System::Drawing::Point(12, 16);
			this->label9->Name = L"label9";
			this->label9->Size = System::Drawing::Size(38, 13);
			this->label9->TabIndex = 27;
			this->label9->Text = L"Логин";
			// 
			// addMoneyButton
			// 
			this->addMoneyButton->Location = System::Drawing::Point(188, 456);
			this->addMoneyButton->Name = L"addMoneyButton";
			this->addMoneyButton->Size = System::Drawing::Size(149, 23);
			this->addMoneyButton->TabIndex = 29;
			this->addMoneyButton->Text = L"Add money";
			this->addMoneyButton->UseVisualStyleBackColor = true;
			this->addMoneyButton->Click += gcnew System::EventHandler(this, &Client::addMoneyButton_Click);
			// 
			// Client
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(504, 489);
			this->Controls->Add(this->addMoneyButton);
			this->Controls->Add(this->loginTextBox);
			this->Controls->Add(this->label9);
			this->Controls->Add(this->passwordTextBox);
			this->Controls->Add(this->label8);
			this->Controls->Add(this->transactionDataGridView);
			this->Controls->Add(this->transactionButton);
			this->Controls->Add(this->deleteButton);
			this->Controls->Add(this->saveButton);
			this->Controls->Add(this->valueTextBox);
			this->Controls->Add(this->label7);
			this->Controls->Add(this->birthdayDateTimePicker);
			this->Controls->Add(this->accountTextBox);
			this->Controls->Add(this->label6);
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
			this->Name = L"Client";
			this->Text = L"Client";
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->transactionDataGridView))->EndInit();
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion
	private: System::Void saveButton_Click(System::Object^ sender, System::EventArgs^ e) {
		auto res = viewModel->Save();
		if (res.size() > 0) {
			MessageBox::Show(gcnew String(strjoin("\n", res).c_str()), "Error");
			return;
		}
		Close();
	}
	// Обработка кнопок и ввода

	private: System::Void deleteButton_Click(System::Object^ sender, System::EventArgs^ e) {
		viewModel->Remove();
		Close();
	}
	private: System::Void loginTextBox_TextChanged(System::Object^ sender, System::EventArgs^ e) {
		viewModel->Login = loginTextBox->Text->Trim();
	}
	private: System::Void passwordTextBox_TextChanged(System::Object^ sender, System::EventArgs^ e) {
		viewModel->Password = passwordTextBox->Text->Trim();
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
	private: System::Void transactionButton_Click(System::Object^ sender, System::EventArgs^ e) {
		viewModel->createTransaction();
	}
	private: System::Void addMoneyButton_Click(System::Object^ sender, System::EventArgs^ e) {
		viewModel->addMoney();
	}
	};
}
