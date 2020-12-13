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
			//
			//TODO: Add the constructor code here
			//
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

	private: System::Windows::Forms::Button^ button1;
	private: System::Windows::Forms::Button^ button2;
	private: System::Windows::Forms::Button^ button3;
	private: System::Windows::Forms::DataGridView^ dataGridView1;

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
			this->button1 = (gcnew System::Windows::Forms::Button());
			this->button2 = (gcnew System::Windows::Forms::Button());
			this->button3 = (gcnew System::Windows::Forms::Button());
			this->dataGridView1 = (gcnew System::Windows::Forms::DataGridView());
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->dataGridView1))->BeginInit();
			this->SuspendLayout();
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Location = System::Drawing::Point(12, 16);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(29, 13);
			this->label1->TabIndex = 0;
			this->label1->Text = L"Имя";
			// 
			// nameTextBox
			// 
			this->nameTextBox->Location = System::Drawing::Point(71, 13);
			this->nameTextBox->Name = L"nameTextBox";
			this->nameTextBox->Size = System::Drawing::Size(421, 20);
			this->nameTextBox->TabIndex = 2;
			// 
			// surnameTextBox
			// 
			this->surnameTextBox->Location = System::Drawing::Point(71, 39);
			this->surnameTextBox->Name = L"surnameTextBox";
			this->surnameTextBox->Size = System::Drawing::Size(421, 20);
			this->surnameTextBox->TabIndex = 4;
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Location = System::Drawing::Point(12, 42);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(56, 13);
			this->label2->TabIndex = 3;
			this->label2->Text = L"Фамилия";
			// 
			// passportTextBox
			// 
			this->passportTextBox->Location = System::Drawing::Point(71, 65);
			this->passportTextBox->Name = L"passportTextBox";
			this->passportTextBox->Size = System::Drawing::Size(421, 20);
			this->passportTextBox->TabIndex = 6;
			// 
			// label3
			// 
			this->label3->AutoSize = true;
			this->label3->Location = System::Drawing::Point(12, 68);
			this->label3->Name = L"label3";
			this->label3->Size = System::Drawing::Size(50, 13);
			this->label3->TabIndex = 5;
			this->label3->Text = L"Паспорт";
			// 
			// label4
			// 
			this->label4->AutoSize = true;
			this->label4->Location = System::Drawing::Point(12, 94);
			this->label4->Name = L"label4";
			this->label4->Size = System::Drawing::Size(86, 13);
			this->label4->TabIndex = 7;
			this->label4->Text = L"Дата рождения";
			// 
			// birthplaceTextBox
			// 
			this->birthplaceTextBox->Location = System::Drawing::Point(110, 117);
			this->birthplaceTextBox->Name = L"birthplaceTextBox";
			this->birthplaceTextBox->Size = System::Drawing::Size(382, 20);
			this->birthplaceTextBox->TabIndex = 10;
			// 
			// label5
			// 
			this->label5->AutoSize = true;
			this->label5->Location = System::Drawing::Point(12, 120);
			this->label5->Name = L"label5";
			this->label5->Size = System::Drawing::Size(92, 13);
			this->label5->TabIndex = 9;
			this->label5->Text = L"Место рождения";
			// 
			// accountTextBox
			// 
			this->accountTextBox->Location = System::Drawing::Point(71, 143);
			this->accountTextBox->Name = L"accountTextBox";
			this->accountTextBox->ReadOnly = true;
			this->accountTextBox->Size = System::Drawing::Size(421, 20);
			this->accountTextBox->TabIndex = 12;
			// 
			// label6
			// 
			this->label6->AutoSize = true;
			this->label6->Location = System::Drawing::Point(12, 146);
			this->label6->Name = L"label6";
			this->label6->Size = System::Drawing::Size(30, 13);
			this->label6->TabIndex = 11;
			this->label6->Text = L"Счет";
			// 
			// birthdayDateTimePicker
			// 
			this->birthdayDateTimePicker->Location = System::Drawing::Point(110, 91);
			this->birthdayDateTimePicker->Name = L"birthdayDateTimePicker";
			this->birthdayDateTimePicker->Size = System::Drawing::Size(382, 20);
			this->birthdayDateTimePicker->TabIndex = 17;
			// 
			// valueTextBox
			// 
			this->valueTextBox->Location = System::Drawing::Point(71, 169);
			this->valueTextBox->Name = L"valueTextBox";
			this->valueTextBox->ReadOnly = true;
			this->valueTextBox->Size = System::Drawing::Size(421, 20);
			this->valueTextBox->TabIndex = 19;
			// 
			// label7
			// 
			this->label7->AutoSize = true;
			this->label7->Location = System::Drawing::Point(12, 172);
			this->label7->Name = L"label7";
			this->label7->Size = System::Drawing::Size(44, 13);
			this->label7->TabIndex = 18;
			this->label7->Text = L"Баланс";
			// 
			// button1
			// 
			this->button1->Location = System::Drawing::Point(12, 404);
			this->button1->Name = L"button1";
			this->button1->Size = System::Drawing::Size(75, 23);
			this->button1->TabIndex = 21;
			this->button1->Text = L"Save";
			this->button1->UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this->button2->Location = System::Drawing::Point(204, 404);
			this->button2->Name = L"button2";
			this->button2->Size = System::Drawing::Size(75, 23);
			this->button2->TabIndex = 22;
			this->button2->Text = L"Delete";
			this->button2->UseVisualStyleBackColor = true;
			// 
			// button3
			// 
			this->button3->Location = System::Drawing::Point(343, 404);
			this->button3->Name = L"button3";
			this->button3->Size = System::Drawing::Size(149, 23);
			this->button3->TabIndex = 23;
			this->button3->Text = L"Create transaction";
			this->button3->UseVisualStyleBackColor = true;
			// 
			// dataGridView1
			// 
			this->dataGridView1->AllowUserToAddRows = false;
			this->dataGridView1->AllowUserToDeleteRows = false;
			this->dataGridView1->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
			this->dataGridView1->Location = System::Drawing::Point(12, 200);
			this->dataGridView1->Name = L"dataGridView1";
			this->dataGridView1->ReadOnly = true;
			this->dataGridView1->Size = System::Drawing::Size(480, 198);
			this->dataGridView1->TabIndex = 24;
			// 
			// Client
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(504, 441);
			this->Controls->Add(this->dataGridView1);
			this->Controls->Add(this->button3);
			this->Controls->Add(this->button2);
			this->Controls->Add(this->button1);
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
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->dataGridView1))->EndInit();
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion
	};
}
