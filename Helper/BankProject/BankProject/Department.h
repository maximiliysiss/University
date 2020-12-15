#pragma once
#include "DepartmentViewModel.h"

namespace BankProject {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;
	using namespace BankProject::ViewModels;

	/// <summary>
	/// Summary for Department
	/// </summary>
	public ref class DepartmentForm : public System::Windows::Forms::Form
	{
	private:
		DepartmentViewModel^ viewModel;

	public:
		DepartmentForm(DepartmentViewModel^ viewModel)
		{
			InitializeComponent();

			this->viewModel = viewModel;

			this->nameTextBox->Text = this->viewModel->Name;
			this->idTextBox->Text = Convert::ToString(this->viewModel->Id);
			this->adressTextBox->Text = this->viewModel->Adress;

			if (this->viewModel->isNew() || this->viewModel->isDefault()) {
				deleteButton->Visible = false;
			}
		}

	protected:
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		~DepartmentForm()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::TextBox^ idTextBox;
	protected:

	protected:
	private: System::Windows::Forms::Label^ label1;
	private: System::Windows::Forms::TextBox^ nameTextBox;

	private: System::Windows::Forms::Label^ label2;
	private: System::Windows::Forms::Button^ saveButton;
	private: System::Windows::Forms::Button^ deleteButton;
	private: System::Windows::Forms::TextBox^ adressTextBox;

	private: System::Windows::Forms::Label^ label3;




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
			this->idTextBox = (gcnew System::Windows::Forms::TextBox());
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->nameTextBox = (gcnew System::Windows::Forms::TextBox());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->saveButton = (gcnew System::Windows::Forms::Button());
			this->deleteButton = (gcnew System::Windows::Forms::Button());
			this->adressTextBox = (gcnew System::Windows::Forms::TextBox());
			this->label3 = (gcnew System::Windows::Forms::Label());
			this->SuspendLayout();
			// 
			// idTextBox
			// 
			this->idTextBox->Location = System::Drawing::Point(71, 6);
			this->idTextBox->Name = L"idTextBox";
			this->idTextBox->ReadOnly = true;
			this->idTextBox->Size = System::Drawing::Size(421, 20);
			this->idTextBox->TabIndex = 27;
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Location = System::Drawing::Point(12, 9);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(18, 13);
			this->label1->TabIndex = 26;
			this->label1->Text = L"ID";
			// 
			// nameTextBox
			// 
			this->nameTextBox->Location = System::Drawing::Point(71, 32);
			this->nameTextBox->Name = L"nameTextBox";
			this->nameTextBox->Size = System::Drawing::Size(421, 20);
			this->nameTextBox->TabIndex = 29;
			this->nameTextBox->TextChanged += gcnew System::EventHandler(this, &DepartmentForm::nameTextBox_TextChanged);
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Location = System::Drawing::Point(12, 35);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(29, 13);
			this->label2->TabIndex = 28;
			this->label2->Text = L"Имя";
			// 
			// saveButton
			// 
			this->saveButton->Location = System::Drawing::Point(12, 103);
			this->saveButton->Name = L"saveButton";
			this->saveButton->Size = System::Drawing::Size(75, 23);
			this->saveButton->TabIndex = 30;
			this->saveButton->Text = L"Save";
			this->saveButton->UseVisualStyleBackColor = true;
			this->saveButton->Click += gcnew System::EventHandler(this, &DepartmentForm::saveButton_Click);
			// 
			// deleteButton
			// 
			this->deleteButton->Location = System::Drawing::Point(417, 103);
			this->deleteButton->Name = L"deleteButton";
			this->deleteButton->Size = System::Drawing::Size(75, 23);
			this->deleteButton->TabIndex = 31;
			this->deleteButton->Text = L"Delete";
			this->deleteButton->UseVisualStyleBackColor = true;
			this->deleteButton->Click += gcnew System::EventHandler(this, &DepartmentForm::Delete_Click);
			// 
			// adressTextBox
			// 
			this->adressTextBox->Location = System::Drawing::Point(70, 59);
			this->adressTextBox->Name = L"adressTextBox";
			this->adressTextBox->Size = System::Drawing::Size(421, 20);
			this->adressTextBox->TabIndex = 33;
			this->adressTextBox->TextChanged += gcnew System::EventHandler(this, &DepartmentForm::adressTextBox_TextChanged);
			// 
			// label3
			// 
			this->label3->AutoSize = true;
			this->label3->Location = System::Drawing::Point(11, 62);
			this->label3->Name = L"label3";
			this->label3->Size = System::Drawing::Size(38, 13);
			this->label3->TabIndex = 32;
			this->label3->Text = L"Адрес";
			// 
			// DepartmentForm
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(502, 138);
			this->Controls->Add(this->adressTextBox);
			this->Controls->Add(this->label3);
			this->Controls->Add(this->deleteButton);
			this->Controls->Add(this->saveButton);
			this->Controls->Add(this->nameTextBox);
			this->Controls->Add(this->label2);
			this->Controls->Add(this->idTextBox);
			this->Controls->Add(this->label1);
			this->FormBorderStyle = System::Windows::Forms::FormBorderStyle::FixedSingle;
			this->MaximizeBox = false;
			this->Name = L"DepartmentForm";
			this->Text = L"Department";
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion
	private: System::Void saveButton_Click(System::Object^ sender, System::EventArgs^ e) {
		auto result = viewModel->onSave();
		if (result.size() > 0) {
			MessageBox::Show(gcnew String(strjoin("\n", result).c_str()), "Error");
			return;
		}
		Close();
	}
	private: System::Void Delete_Click(System::Object^ sender, System::EventArgs^ e) {
		viewModel->onDelete();
		Close();
	}
	private: System::Void nameTextBox_TextChanged(System::Object^ sender, System::EventArgs^ e) {
		viewModel->Name = nameTextBox->Text;
	}
	private: System::Void adressTextBox_TextChanged(System::Object^ sender, System::EventArgs^ e) {
		viewModel->Adress = adressTextBox->Text;
	}
	};
}
