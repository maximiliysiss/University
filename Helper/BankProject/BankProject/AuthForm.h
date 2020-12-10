#pragma once
#include "AuthViewModel.h"

namespace BankProject {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	namespace ViewModels {
		class AuthViewModel;
	}

	/// <summary>
	/// Summary for AuthForm
	/// </summary>
	public ref class AuthForm : public System::Windows::Forms::Form
	{
	private:

		ViewModels::AuthViewModel* viewModel;

	public:
		AuthForm(void)
		{
			InitializeComponent();

			viewModel = new ViewModels::AuthViewModel();
		}

	protected:
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		~AuthForm()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::Label^ labelLogin;
	private: System::Windows::Forms::Label^ passwordLabel;
	private: System::Windows::Forms::TextBox^ loginTextBox;
	private: System::Windows::Forms::TextBox^ passwordTextBox;
	private: System::Windows::Forms::Button^ loginButton;

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
			this->labelLogin = (gcnew System::Windows::Forms::Label());
			this->passwordLabel = (gcnew System::Windows::Forms::Label());
			this->loginTextBox = (gcnew System::Windows::Forms::TextBox());
			this->passwordTextBox = (gcnew System::Windows::Forms::TextBox());
			this->loginButton = (gcnew System::Windows::Forms::Button());
			this->SuspendLayout();
			// 
			// labelLogin
			// 
			this->labelLogin->AutoSize = true;
			this->labelLogin->Location = System::Drawing::Point(13, 13);
			this->labelLogin->Name = L"labelLogin";
			this->labelLogin->Size = System::Drawing::Size(33, 13);
			this->labelLogin->TabIndex = 0;
			this->labelLogin->Text = L"Login";
			// 
			// passwordLabel
			// 
			this->passwordLabel->AutoSize = true;
			this->passwordLabel->Location = System::Drawing::Point(13, 65);
			this->passwordLabel->Name = L"passwordLabel";
			this->passwordLabel->Size = System::Drawing::Size(53, 13);
			this->passwordLabel->TabIndex = 1;
			this->passwordLabel->Text = L"Password";
			// 
			// loginTextBox
			// 
			this->loginTextBox->Location = System::Drawing::Point(13, 30);
			this->loginTextBox->Name = L"loginTextBox";
			this->loginTextBox->Size = System::Drawing::Size(179, 20);
			this->loginTextBox->TabIndex = 2;
			// 
			// passwordTextBox
			// 
			this->passwordTextBox->Location = System::Drawing::Point(12, 81);
			this->passwordTextBox->Name = L"passwordTextBox";
			this->passwordTextBox->PasswordChar = '*';
			this->passwordTextBox->Size = System::Drawing::Size(180, 20);
			this->passwordTextBox->TabIndex = 3;
			// 
			// loginButton
			// 
			this->loginButton->Location = System::Drawing::Point(12, 108);
			this->loginButton->Name = L"loginButton";
			this->loginButton->Size = System::Drawing::Size(180, 23);
			this->loginButton->TabIndex = 4;
			this->loginButton->Text = L"Login";
			this->loginButton->UseVisualStyleBackColor = true;
			this->loginButton->Click += gcnew System::EventHandler(this, &AuthForm::loginButton_Click);
			// 
			// AuthForm
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(208, 143);
			this->Controls->Add(this->loginButton);
			this->Controls->Add(this->passwordTextBox);
			this->Controls->Add(this->loginTextBox);
			this->Controls->Add(this->passwordLabel);
			this->Controls->Add(this->labelLogin);
			this->FormBorderStyle = System::Windows::Forms::FormBorderStyle::FixedSingle;
			this->MaximizeBox = false;
			this->MinimizeBox = false;
			this->Name = L"AuthForm";
			this->Text = L"AuthForm";
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion
	private: System::Void loginButton_Click(System::Object^ sender, System::EventArgs^ e) {
		viewModel->login(this, toStdString(loginTextBox->Text), toStdString(passwordTextBox->Text));
	}
	};
}
