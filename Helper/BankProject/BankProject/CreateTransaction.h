#pragma once
#include "CreateTransactionViewModel.h"

namespace BankProject {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;
	using namespace BankProject;
	using namespace BankProject::ViewModels;

	/// <summary>
	/// Summary for CreateTransaction
	/// </summary>
	public ref class CreateTransaction : public System::Windows::Forms::Form
	{
		ViewModels::CreateTransactionViewModel^ vm;
	public:
		CreateTransaction(ViewModels::CreateTransactionViewModel^ vm) :vm(vm)
		{
			InitializeComponent();

			fromComboBox->DataSource = vm->getAccounts();
			toComboBox->DataSource = vm->getAccounts();
		}

	protected:
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		~CreateTransaction()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::Label^ fromLabel;
	protected:

	protected:

	protected:
	private: System::Windows::Forms::Label^ label2;
	private: System::Windows::Forms::TextBox^ valueTextBox;

	private: System::Windows::Forms::Label^ label3;
	private: System::Windows::Forms::ComboBox^ fromComboBox;
	private: System::Windows::Forms::ComboBox^ toComboBox;


	private: System::Windows::Forms::Button^ createButton;


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
			this->fromLabel = (gcnew System::Windows::Forms::Label());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->valueTextBox = (gcnew System::Windows::Forms::TextBox());
			this->label3 = (gcnew System::Windows::Forms::Label());
			this->fromComboBox = (gcnew System::Windows::Forms::ComboBox());
			this->toComboBox = (gcnew System::Windows::Forms::ComboBox());
			this->createButton = (gcnew System::Windows::Forms::Button());
			this->SuspendLayout();
			// 
			// fromLabel
			// 
			this->fromLabel->AutoSize = true;
			this->fromLabel->Location = System::Drawing::Point(13, 13);
			this->fromLabel->Name = L"fromLabel";
			this->fromLabel->Size = System::Drawing::Size(30, 13);
			this->fromLabel->TabIndex = 0;
			this->fromLabel->Text = L"From";
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Location = System::Drawing::Point(13, 39);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(20, 13);
			this->label2->TabIndex = 2;
			this->label2->Text = L"To";
			// 
			// valueTextBox
			// 
			this->valueTextBox->Location = System::Drawing::Point(54, 62);
			this->valueTextBox->Name = L"valueTextBox";
			this->valueTextBox->Size = System::Drawing::Size(251, 20);
			this->valueTextBox->TabIndex = 5;
			this->valueTextBox->TextChanged += gcnew System::EventHandler(this, &CreateTransaction::valueTextBox_TextChanged);
			// 
			// label3
			// 
			this->label3->AutoSize = true;
			this->label3->Location = System::Drawing::Point(13, 65);
			this->label3->Name = L"label3";
			this->label3->Size = System::Drawing::Size(34, 13);
			this->label3->TabIndex = 4;
			this->label3->Text = L"Value";
			// 
			// fromComboBox
			// 
			this->fromComboBox->DisplayMember = L"Name";
			this->fromComboBox->FormattingEnabled = true;
			this->fromComboBox->Location = System::Drawing::Point(54, 10);
			this->fromComboBox->Name = L"fromComboBox";
			this->fromComboBox->Size = System::Drawing::Size(251, 21);
			this->fromComboBox->TabIndex = 6;
			this->fromComboBox->SelectedIndexChanged += gcnew System::EventHandler(this, &CreateTransaction::fromComboBox_SelectedIndexChanged);
			// 
			// toComboBox
			// 
			this->toComboBox->DisplayMember = L"Name";
			this->toComboBox->FormattingEnabled = true;
			this->toComboBox->Location = System::Drawing::Point(54, 36);
			this->toComboBox->Name = L"toComboBox";
			this->toComboBox->Size = System::Drawing::Size(251, 21);
			this->toComboBox->TabIndex = 7;
			this->toComboBox->SelectedIndexChanged += gcnew System::EventHandler(this, &CreateTransaction::toComboBox_SelectedIndexChanged);
			// 
			// createButton
			// 
			this->createButton->Location = System::Drawing::Point(12, 93);
			this->createButton->Name = L"createButton";
			this->createButton->Size = System::Drawing::Size(293, 23);
			this->createButton->TabIndex = 8;
			this->createButton->Text = L"Create";
			this->createButton->UseVisualStyleBackColor = true;
			this->createButton->Click += gcnew System::EventHandler(this, &CreateTransaction::createButton_Click);
			// 
			// CreateTransaction
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(313, 124);
			this->Controls->Add(this->createButton);
			this->Controls->Add(this->toComboBox);
			this->Controls->Add(this->fromComboBox);
			this->Controls->Add(this->valueTextBox);
			this->Controls->Add(this->label3);
			this->Controls->Add(this->label2);
			this->Controls->Add(this->fromLabel);
			this->FormBorderStyle = System::Windows::Forms::FormBorderStyle::FixedSingle;
			this->MaximizeBox = false;
			this->MinimizeBox = false;
			this->Name = L"CreateTransaction";
			this->Text = L"CreateTransaction";
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion
	private: System::Void createButton_Click(System::Object^ sender, System::EventArgs^ e) {
		auto res = vm->create();
		if (res.size() > 0) {
			MessageBox::Show(gcnew String(strjoin("\n", res).c_str()), "Error");
			return;
		}
		Close();
	}
	private: System::Void fromComboBox_SelectedIndexChanged(System::Object^ sender, System::EventArgs^ e) {
		vm->FromId = ((ViewModels::AccountComboBox^)fromComboBox->SelectedItem)->Id;
	}
	private: System::Void toComboBox_SelectedIndexChanged(System::Object^ sender, System::EventArgs^ e) {
		vm->ToId = ((ViewModels::AccountComboBox^)toComboBox->SelectedItem)->Id;
	}
	private: System::Void valueTextBox_TextChanged(System::Object^ sender, System::EventArgs^ e) {
		double res = 0;
		if (Double::TryParse(valueTextBox->Text->Trim(), res))
			vm->Value = res;
	}
	};
}
