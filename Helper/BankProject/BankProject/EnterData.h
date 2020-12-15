#pragma once

namespace BankProject {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	/// <summary>
	/// Summary for EnterData
	/// </summary>
	public ref class EnterData : public System::Windows::Forms::Form
	{
		/// <summary>
		/// Результат ввода
		/// </summary>
		double val;
	public:
		EnterData(void)
		{
			InitializeComponent();
		}

	public:
		property double Value {
			double get() {
				return val;
			}
		}

	protected:
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		~EnterData()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::TextBox^ dataTextBox;
	protected:

	private: System::Windows::Forms::Button^ okButton;
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
			this->dataTextBox = (gcnew System::Windows::Forms::TextBox());
			this->okButton = (gcnew System::Windows::Forms::Button());
			this->SuspendLayout();
			// 
			// dataTextBox
			// 
			this->dataTextBox->Location = System::Drawing::Point(13, 13);
			this->dataTextBox->Name = L"dataTextBox";
			this->dataTextBox->Size = System::Drawing::Size(284, 20);
			this->dataTextBox->TabIndex = 0;
			// 
			// okButton
			// 
			this->okButton->Location = System::Drawing::Point(174, 40);
			this->okButton->Name = L"okButton";
			this->okButton->Size = System::Drawing::Size(122, 23);
			this->okButton->TabIndex = 1;
			this->okButton->Text = L"Ok";
			this->okButton->UseVisualStyleBackColor = true;
			this->okButton->Click += gcnew System::EventHandler(this, &EnterData::okButton_Click);
			// 
			// EnterData
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(307, 72);
			this->Controls->Add(this->okButton);
			this->Controls->Add(this->dataTextBox);
			this->FormBorderStyle = System::Windows::Forms::FormBorderStyle::FixedSingle;
			this->MaximizeBox = false;
			this->MinimizeBox = false;
			this->Name = L"EnterData";
			this->ShowInTaskbar = false;
			this->Text = L"EnterData";
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion
	private: System::Void okButton_Click(System::Object^ sender, System::EventArgs^ e) {
		double data = 0;
		if (Double::TryParse(dataTextBox->Text->Trim(), data)) {
			val = data;
			Close();
			return;
		}
		MessageBox::Show("Incorrect data", "Error");
	}
	};
}
