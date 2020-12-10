#pragma once

using namespace System;
using namespace System::ComponentModel;
using namespace System::Collections;
using namespace System::Windows::Forms;
using namespace System::Data;
using namespace System::Drawing;


namespace BankProject {

	/// <summary>
	/// Summary for ClientControl
	/// </summary>
	public ref class ClientControl : public System::Windows::Forms::UserControl
	{
	public:
		ClientControl(void)
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
		~ClientControl()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::Label^ accountIdentityLabel;
	protected:
	private: System::Windows::Forms::Label^ accountIdentityOutputLabel;
	private: System::Windows::Forms::DataGridView^ dataGridView1;
	private: System::Windows::Forms::Label^ transactionsTableLabel;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ DateTimeColumn;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ FromAccount;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ Value;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^ Manager;


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
			this->accountIdentityLabel = (gcnew System::Windows::Forms::Label());
			this->accountIdentityOutputLabel = (gcnew System::Windows::Forms::Label());
			this->dataGridView1 = (gcnew System::Windows::Forms::DataGridView());
			this->transactionsTableLabel = (gcnew System::Windows::Forms::Label());
			this->DateTimeColumn = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->FromAccount = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->Value = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->Manager = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->dataGridView1))->BeginInit();
			this->SuspendLayout();
			// 
			// accountIdentityLabel
			// 
			this->accountIdentityLabel->AutoSize = true;
			this->accountIdentityLabel->Location = System::Drawing::Point(14, 10);
			this->accountIdentityLabel->Name = L"accountIdentityLabel";
			this->accountIdentityLabel->Size = System::Drawing::Size(79, 13);
			this->accountIdentityLabel->TabIndex = 0;
			this->accountIdentityLabel->Text = L"Account Identy";
			// 
			// accountIdentityOutputLabel
			// 
			this->accountIdentityOutputLabel->AutoSize = true;
			this->accountIdentityOutputLabel->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 20, System::Drawing::FontStyle::Regular,
				System::Drawing::GraphicsUnit::Point, static_cast<System::Byte>(0)));
			this->accountIdentityOutputLabel->Location = System::Drawing::Point(14, 32);
			this->accountIdentityOutputLabel->Name = L"accountIdentityOutputLabel";
			this->accountIdentityOutputLabel->Size = System::Drawing::Size(195, 31);
			this->accountIdentityOutputLabel->TabIndex = 1;
			this->accountIdentityOutputLabel->Text = L"Account Identy";
			// 
			// dataGridView1
			// 
			this->dataGridView1->AllowUserToAddRows = false;
			this->dataGridView1->AllowUserToDeleteRows = false;
			this->dataGridView1->Anchor = static_cast<System::Windows::Forms::AnchorStyles>(((System::Windows::Forms::AnchorStyles::Bottom | System::Windows::Forms::AnchorStyles::Left)
				| System::Windows::Forms::AnchorStyles::Right));
			this->dataGridView1->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
			this->dataGridView1->Columns->AddRange(gcnew cli::array< System::Windows::Forms::DataGridViewColumn^  >(4) {
				this->DateTimeColumn,
					this->FromAccount, this->Value, this->Manager
			});
			this->dataGridView1->Location = System::Drawing::Point(0, 105);
			this->dataGridView1->Name = L"dataGridView1";
			this->dataGridView1->ReadOnly = true;
			this->dataGridView1->Size = System::Drawing::Size(572, 308);
			this->dataGridView1->TabIndex = 2;
			// 
			// transactionsTableLabel
			// 
			this->transactionsTableLabel->AutoSize = true;
			this->transactionsTableLabel->Location = System::Drawing::Point(14, 78);
			this->transactionsTableLabel->Name = L"transactionsTableLabel";
			this->transactionsTableLabel->Size = System::Drawing::Size(98, 13);
			this->transactionsTableLabel->TabIndex = 3;
			this->transactionsTableLabel->Text = L"Transactions Table";
			// 
			// DateTimeColumn
			// 
			this->DateTimeColumn->HeaderText = L"Date and time";
			this->DateTimeColumn->Name = L"DateTimeColumn";
			this->DateTimeColumn->ReadOnly = true;
			// 
			// FromAccount
			// 
			this->FromAccount->HeaderText = L"From Account";
			this->FromAccount->Name = L"FromAccount";
			this->FromAccount->ReadOnly = true;
			// 
			// Value
			// 
			this->Value->HeaderText = L"Value";
			this->Value->Name = L"Value";
			this->Value->ReadOnly = true;
			// 
			// Manager
			// 
			this->Manager->HeaderText = L"Manager";
			this->Manager->Name = L"Manager";
			this->Manager->ReadOnly = true;
			// 
			// ClientControl
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->Controls->Add(this->transactionsTableLabel);
			this->Controls->Add(this->dataGridView1);
			this->Controls->Add(this->accountIdentityOutputLabel);
			this->Controls->Add(this->accountIdentityLabel);
			this->Name = L"ClientControl";
			this->Size = System::Drawing::Size(572, 416);
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->dataGridView1))->EndInit();
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion
	};
}
