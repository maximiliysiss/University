#pragma once
#include "CurrentClientViewModel.h"

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
		ViewModels::CurrentViewModel^ vm;
	public:
		ClientControl(ViewModels::CurrentViewModel^ vm)
		{
			this->vm = vm;
			InitializeComponent();

			accountIdentityOutputLabel->Text = vm->AccountIdStr;
			vm->set_reload(gcnew Action(this, &ClientControl::reload));

			vm->load();
		}

		/// <summary>
		/// Перезагрузить данные
		/// </summary>
		void reload() {
			transactionsDataGridView->DataSource = vm->get_transactions();
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
	private: System::Windows::Forms::DataGridView^ transactionsDataGridView;

	private: System::Windows::Forms::Label^ transactionsTableLabel;






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
			this->accountIdentityLabel = (gcnew System::Windows::Forms::Label());
			this->accountIdentityOutputLabel = (gcnew System::Windows::Forms::Label());
			this->transactionsDataGridView = (gcnew System::Windows::Forms::DataGridView());
			this->transactionsTableLabel = (gcnew System::Windows::Forms::Label());
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->transactionsDataGridView))->BeginInit();
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
			// transactionsDataGridView
			// 
			this->transactionsDataGridView->AllowUserToAddRows = false;
			this->transactionsDataGridView->AllowUserToDeleteRows = false;
			this->transactionsDataGridView->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((((System::Windows::Forms::AnchorStyles::Top | System::Windows::Forms::AnchorStyles::Bottom)
				| System::Windows::Forms::AnchorStyles::Left)
				| System::Windows::Forms::AnchorStyles::Right));
			this->transactionsDataGridView->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
			this->transactionsDataGridView->Location = System::Drawing::Point(0, 105);
			this->transactionsDataGridView->Name = L"transactionsDataGridView";
			this->transactionsDataGridView->ReadOnly = true;
			this->transactionsDataGridView->Size = System::Drawing::Size(572, 311);
			this->transactionsDataGridView->TabIndex = 2;
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
			// ClientControl
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->Controls->Add(this->transactionsTableLabel);
			this->Controls->Add(this->transactionsDataGridView);
			this->Controls->Add(this->accountIdentityOutputLabel);
			this->Controls->Add(this->accountIdentityLabel);
			this->Name = L"ClientControl";
			this->Size = System::Drawing::Size(572, 416);
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->transactionsDataGridView))->EndInit();
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion
	};
}
