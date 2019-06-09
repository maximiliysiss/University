#pragma once

#include "DatabaseLocal.cpp"
#include "Phone.h"
#include "CustomFunctor.cpp"

/*класс окна*/
namespace StorePhone {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	/// <summary>
	/// Summary for StoreForm
	/// </summary>
	public ref class StoreForm : public System::Windows::Forms::Form
	{
		/*доступ к БД*/
		DatabaseLocal<Phone> * datas = nullptr;
	public:

		/*создать сортировку*/
		template<typename T>
		static PhoneBiFunctor<T> createSort(T(*get)(Phone& ph), bool direction) {
			return PhoneBiFunctor<T>(get, direction);
		}
		/*создать фильтер*/
		template<typename T>
		static PhoneFunctor<T> createFilter(T(*get)(Phone& ph), T val) {
			return PhoneFunctor<T>(get, val);
		}
		/*создать фильтер для цен*/
		static PhoneFunctorCondPrice<float> createFilter(float from, float to) {
			return PhoneFunctorCondPrice<float>(from, to);
		}

		/*конструктор*/
		StoreForm(void)
		{
			InitializeComponent();
			datas = &DatabaseLocal<Phone>::instance("phones.xml");
			datas->load();
			dataLoad(InstrumentsForPhones::all);

			this->nameFilterCombobox->SelectedValueChanged += gcnew System::EventHandler(this, &StorePhone::StoreForm::ChangeComboBoxContext);
			this->diagonalFilterCombobox->SelectedValueChanged += gcnew System::EventHandler(this, &StorePhone::StoreForm::ChangeComboBoxContext);
			this->modelFilterCombobox->SelectedValueChanged += gcnew System::EventHandler(this, &StorePhone::StoreForm::ChangeComboBoxContext);
			this->mpFilterCombobox->SelectedValueChanged += gcnew System::EventHandler(this, &StorePhone::StoreForm::ChangeComboBoxContext);
			this->processorFilterCombobox->SelectedValueChanged += gcnew System::EventHandler(this, &StorePhone::StoreForm::ChangeComboBoxContext);
			this->ramFilterCombobox->SelectedValueChanged += gcnew System::EventHandler(this, &StorePhone::StoreForm::ChangeComboBoxContext);
			this->toFilterTextBox->TextChanged += gcnew System::EventHandler(this, &StorePhone::StoreForm::OnTextChanged);
			this->fromFilterTextBox->TextChanged += gcnew System::EventHandler(this, &StorePhone::StoreForm::OnTextChanged);

			auto names = datas->getData().convert<std::string>(InstrumentsForPhones::getName).distinct();
			names.add(0, "");
			for (size_t i = 0; i < names.length(); i++) {
				nameFilterCombobox->Items->Add(gcnew String(names[i].c_str()));
			}

			auto models = datas->getData().convert<std::string>(InstrumentsForPhones::getModel).distinct();
			models.add(0, "");
			for (size_t i = 0; i < models.length(); i++) {
				modelFilterCombobox->Items->Add(gcnew String(models[i].c_str()));
			}

			auto processor = datas->getData().convert<std::string>(InstrumentsForPhones::getProcessor).distinct();
			processor.add(0, "");
			for (size_t i = 0; i < processor.length(); i++) {
				processorFilterCombobox->Items->Add(gcnew String(processor[i].c_str()));
			}

			auto rams = datas->getData().convert<float>(InstrumentsForPhones::getRam).distinct();
			ramFilterCombobox->Items->Add(gcnew String(""));
			for (size_t i = 0; i < rams.length(); i++) {
				ramFilterCombobox->Items->Add(gcnew String(std::to_string(rams[i]).c_str()));
			}

			auto cameras = datas->getData().convert<float>(InstrumentsForPhones::getCamera).distinct();
			mpFilterCombobox->Items->Add(gcnew String(""));
			for (size_t i = 0; i < cameras.length(); i++) {
				mpFilterCombobox->Items->Add(gcnew String(std::to_string(cameras[i]).c_str()));
			}

			auto diagonals = datas->getData().convert<float>(InstrumentsForPhones::getDiagonal).distinct();
			diagonalFilterCombobox->Items->Add(gcnew String(""));
			for (size_t i = 0; i < diagonals.length(); i++) {
				diagonalFilterCombobox->Items->Add(gcnew String(std::to_string(diagonals[i]).c_str()));
			}
		}

		/*превратить System String -> std:: string*/
		std::string toStdString(String^ str) {
			using namespace System::Runtime::InteropServices;
			const char * chars = (const char*)(Marshal::StringToHGlobalAnsi(str)).ToPointer();
			std::string tmp = chars;
			Marshal::FreeHGlobal(IntPtr((void*)chars));
			return tmp;
		}

		/*создать фильтр*/
		CustomList<Phone> filterCreator() {
			CustomList<Phone> all = datas->getData();
			if (this->diagonalFilterCombobox->SelectedItem && this->diagonalFilterCombobox->SelectedItem->ToString()->Trim()->Length > 0) {
				all = all.where(&createFilter(InstrumentsForPhones::getDiagonal,
					std::stof(toStdString(this->diagonalFilterCombobox->SelectedItem->ToString()->Trim()))));
			}

			if (this->modelFilterCombobox->SelectedItem && this->modelFilterCombobox->SelectedItem->ToString()->Trim()->Length > 0) {
				all = all.where(&createFilter(InstrumentsForPhones::getModel,
					toStdString(this->modelFilterCombobox->SelectedItem->ToString()->Trim())));
			}

			if (this->mpFilterCombobox->SelectedItem && this->mpFilterCombobox->SelectedItem->ToString()->Trim()->Length > 0) {
				all = all.where(&createFilter(InstrumentsForPhones::getCamera,
					std::stof(toStdString(this->mpFilterCombobox->SelectedItem->ToString()->Trim()))));
			}

			if (this->nameFilterCombobox->SelectedItem && this->nameFilterCombobox->SelectedItem->ToString()->Trim()->Length > 0) {
				all = all.where(&createFilter(InstrumentsForPhones::getName,
					(toStdString(this->nameFilterCombobox->SelectedItem->ToString()->Trim()))));
			}

			if (this->processorFilterCombobox->SelectedItem && this->processorFilterCombobox->SelectedItem->ToString()->Trim()->Length > 0) {
				all = all.where(&createFilter(InstrumentsForPhones::getProcessor,
					(toStdString(this->processorFilterCombobox->SelectedItem->ToString()->Trim()))));
			}

			if (this->ramFilterCombobox->SelectedItem && this->ramFilterCombobox->SelectedItem->ToString()->Trim()->Length > 0) {
				all = all.where(&createFilter(InstrumentsForPhones::getRam,
					std::stof(toStdString(this->ramFilterCombobox->SelectedItem->ToString()->Trim()))));
			}

			if (this->fromFilterTextBox->Text && this->toFilterTextBox->Text
				&& this->fromFilterTextBox->Text->Trim()->Length > 0
				&& this->toFilterTextBox->Text->Trim()->Length > 0) {

				float from = std::stof(toStdString(this->fromFilterTextBox->Text->Trim()));
				float to = std::stof(toStdString(this->toFilterTextBox->Text->Trim()));

				if (from <= to)
					all = all.where(&createFilter(from, to));
			}

			return all;
		}

		/*создать сортировку*/
		template<typename T>
		CustomList<Phone> sortCreator(T(*getter)(Phone& p)) {
			CustomList<Phone> list = filterCreator();
			return list.orderBy(&createSort(getter, ascSortingRadio->Checked));
		}

		/*выгрузить данные в DataGridView*/
		void dataLoad(bool(*filter)(Phone& phone)) {

			CustomList<Phone> list = datas->getData().where(filter);
			printData(list);
		}

		/*напечатать данные в таблицу*/
		void printData(CustomList<Phone> list) {

			dataGridView1->Rows->Clear();

			for (size_t i = 0; i < list.length(); i++) {
				dataGridView1->Rows->Add();
				dataGridView1->Rows[i]->Cells[0]->Value = gcnew String(list[i].getname().c_str());
				dataGridView1->Rows[i]->Cells[1]->Value = gcnew String(list[i].getmodel().c_str());
				dataGridView1->Rows[i]->Cells[2]->Value = gcnew String(list[i].getprocessor().c_str());
				dataGridView1->Rows[i]->Cells[3]->Value = list[i].getram();
				dataGridView1->Rows[i]->Cells[4]->Value = list[i].getmp();
				dataGridView1->Rows[i]->Cells[5]->Value = list[i].getdiagonal();
				dataGridView1->Rows[i]->Cells[6]->Value = list[i].getprice();
			}
		}

	protected:
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		~StoreForm()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::DataGridView^  dataGridView1;
	protected:
	private: System::Windows::Forms::GroupBox^  groupBox1;
	private: System::Windows::Forms::RadioButton^  deskSortingRadio;

	private: System::Windows::Forms::RadioButton^  ascSortingRadio;

	private: System::Windows::Forms::Button^  PriceSortBtn;

	private: System::Windows::Forms::Button^  DiagonalSortBtn;

	private: System::Windows::Forms::Button^  CameraSortBtn;

	private: System::Windows::Forms::Button^  RAMSortBtn;

	private: System::Windows::Forms::GroupBox^  groupBox2;
	private: System::Windows::Forms::TextBox^  toFilterTextBox;

	private: System::Windows::Forms::Label^  label8;
	private: System::Windows::Forms::TextBox^  fromFilterTextBox;

	private: System::Windows::Forms::ComboBox^  diagonalFilterCombobox;

	private: System::Windows::Forms::ComboBox^  mpFilterCombobox;

	private: System::Windows::Forms::ComboBox^  ramFilterCombobox;

	private: System::Windows::Forms::ComboBox^  processorFilterCombobox;

	private: System::Windows::Forms::ComboBox^  modelFilterCombobox;

	private: System::Windows::Forms::ComboBox^  nameFilterCombobox;


	private: System::Windows::Forms::Label^  label7;
	private: System::Windows::Forms::Label^  label6;
	private: System::Windows::Forms::Label^  label5;
	private: System::Windows::Forms::Label^  label4;
	private: System::Windows::Forms::Label^  label3;
	private: System::Windows::Forms::Label^  label2;
	private: System::Windows::Forms::Label^  label1;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^  name;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^  model;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^  processor;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^  ram;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^  camera;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^  diagonal;
	private: System::Windows::Forms::DataGridViewTextBoxColumn^  price;

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
			this->dataGridView1 = (gcnew System::Windows::Forms::DataGridView());
			this->name = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->model = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->processor = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->ram = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->camera = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->diagonal = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->price = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->groupBox1 = (gcnew System::Windows::Forms::GroupBox());
			this->deskSortingRadio = (gcnew System::Windows::Forms::RadioButton());
			this->ascSortingRadio = (gcnew System::Windows::Forms::RadioButton());
			this->PriceSortBtn = (gcnew System::Windows::Forms::Button());
			this->DiagonalSortBtn = (gcnew System::Windows::Forms::Button());
			this->CameraSortBtn = (gcnew System::Windows::Forms::Button());
			this->RAMSortBtn = (gcnew System::Windows::Forms::Button());
			this->groupBox2 = (gcnew System::Windows::Forms::GroupBox());
			this->toFilterTextBox = (gcnew System::Windows::Forms::TextBox());
			this->label8 = (gcnew System::Windows::Forms::Label());
			this->fromFilterTextBox = (gcnew System::Windows::Forms::TextBox());
			this->diagonalFilterCombobox = (gcnew System::Windows::Forms::ComboBox());
			this->mpFilterCombobox = (gcnew System::Windows::Forms::ComboBox());
			this->ramFilterCombobox = (gcnew System::Windows::Forms::ComboBox());
			this->processorFilterCombobox = (gcnew System::Windows::Forms::ComboBox());
			this->modelFilterCombobox = (gcnew System::Windows::Forms::ComboBox());
			this->nameFilterCombobox = (gcnew System::Windows::Forms::ComboBox());
			this->label7 = (gcnew System::Windows::Forms::Label());
			this->label6 = (gcnew System::Windows::Forms::Label());
			this->label5 = (gcnew System::Windows::Forms::Label());
			this->label4 = (gcnew System::Windows::Forms::Label());
			this->label3 = (gcnew System::Windows::Forms::Label());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->label1 = (gcnew System::Windows::Forms::Label());
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->dataGridView1))->BeginInit();
			this->groupBox1->SuspendLayout();
			this->groupBox2->SuspendLayout();
			this->SuspendLayout();
			// 
			// dataGridView1
			// 
			this->dataGridView1->AllowUserToAddRows = false;
			this->dataGridView1->AllowUserToDeleteRows = false;
			this->dataGridView1->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
			this->dataGridView1->Columns->AddRange(gcnew cli::array< System::Windows::Forms::DataGridViewColumn^  >(7) {
				this->name, this->model,
					this->processor, this->ram, this->camera, this->diagonal, this->price
			});
			this->dataGridView1->Location = System::Drawing::Point(12, 12);
			this->dataGridView1->Name = L"dataGridView1";
			this->dataGridView1->ReadOnly = true;
			this->dataGridView1->Size = System::Drawing::Size(568, 247);
			this->dataGridView1->TabIndex = 0;
			// 
			// name
			// 
			this->name->HeaderText = L"Марка";
			this->name->Name = L"name";
			this->name->ReadOnly = true;
			// 
			// model
			// 
			this->model->HeaderText = L"Модель";
			this->model->Name = L"model";
			this->model->ReadOnly = true;
			// 
			// processor
			// 
			this->processor->HeaderText = L"Процессор";
			this->processor->Name = L"processor";
			this->processor->ReadOnly = true;
			// 
			// ram
			// 
			this->ram->HeaderText = L"ОЗУ";
			this->ram->Name = L"ram";
			this->ram->ReadOnly = true;
			// 
			// camera
			// 
			this->camera->HeaderText = L"Камера";
			this->camera->Name = L"camera";
			this->camera->ReadOnly = true;
			// 
			// diagonal
			// 
			this->diagonal->HeaderText = L"Диагональ";
			this->diagonal->Name = L"diagonal";
			this->diagonal->ReadOnly = true;
			// 
			// price
			// 
			this->price->HeaderText = L"Цена";
			this->price->Name = L"price";
			this->price->ReadOnly = true;
			// 
			// groupBox1
			// 
			this->groupBox1->Controls->Add(this->deskSortingRadio);
			this->groupBox1->Controls->Add(this->ascSortingRadio);
			this->groupBox1->Controls->Add(this->PriceSortBtn);
			this->groupBox1->Controls->Add(this->DiagonalSortBtn);
			this->groupBox1->Controls->Add(this->CameraSortBtn);
			this->groupBox1->Controls->Add(this->RAMSortBtn);
			this->groupBox1->Location = System::Drawing::Point(587, 13);
			this->groupBox1->Name = L"groupBox1";
			this->groupBox1->Size = System::Drawing::Size(200, 246);
			this->groupBox1->TabIndex = 1;
			this->groupBox1->TabStop = false;
			this->groupBox1->Text = L"Сортировка";
			// 
			// deskSortingRadio
			// 
			this->deskSortingRadio->AutoSize = true;
			this->deskSortingRadio->Location = System::Drawing::Point(7, 179);
			this->deskSortingRadio->Name = L"deskSortingRadio";
			this->deskSortingRadio->Size = System::Drawing::Size(93, 17);
			this->deskSortingRadio->TabIndex = 5;
			this->deskSortingRadio->TabStop = true;
			this->deskSortingRadio->Text = L"По убыванию";
			this->deskSortingRadio->UseVisualStyleBackColor = true;
			// 
			// ascSortingRadio
			// 
			this->ascSortingRadio->AutoSize = true;
			this->ascSortingRadio->Checked = true;
			this->ascSortingRadio->Location = System::Drawing::Point(7, 155);
			this->ascSortingRadio->Name = L"ascSortingRadio";
			this->ascSortingRadio->Size = System::Drawing::Size(109, 17);
			this->ascSortingRadio->TabIndex = 4;
			this->ascSortingRadio->TabStop = true;
			this->ascSortingRadio->Text = L"По возрастанию";
			this->ascSortingRadio->UseVisualStyleBackColor = true;
			// 
			// PriceSortBtn
			// 
			this->PriceSortBtn->Location = System::Drawing::Point(7, 107);
			this->PriceSortBtn->Name = L"PriceSortBtn";
			this->PriceSortBtn->Size = System::Drawing::Size(75, 23);
			this->PriceSortBtn->TabIndex = 3;
			this->PriceSortBtn->Text = L"Цена";
			this->PriceSortBtn->UseVisualStyleBackColor = true;
			this->PriceSortBtn->Click += gcnew System::EventHandler(this, &StoreForm::PriceSortBtn_Click);
			// 
			// DiagonalSortBtn
			// 
			this->DiagonalSortBtn->Location = System::Drawing::Point(6, 78);
			this->DiagonalSortBtn->Name = L"DiagonalSortBtn";
			this->DiagonalSortBtn->Size = System::Drawing::Size(75, 23);
			this->DiagonalSortBtn->TabIndex = 2;
			this->DiagonalSortBtn->Text = L"Диагональ";
			this->DiagonalSortBtn->UseVisualStyleBackColor = true;
			this->DiagonalSortBtn->Click += gcnew System::EventHandler(this, &StoreForm::DiagonalSortBtn_Click);
			// 
			// CameraSortBtn
			// 
			this->CameraSortBtn->Location = System::Drawing::Point(7, 49);
			this->CameraSortBtn->Name = L"CameraSortBtn";
			this->CameraSortBtn->Size = System::Drawing::Size(75, 23);
			this->CameraSortBtn->TabIndex = 1;
			this->CameraSortBtn->Text = L"Камера";
			this->CameraSortBtn->UseVisualStyleBackColor = true;
			this->CameraSortBtn->Click += gcnew System::EventHandler(this, &StoreForm::CameraSortBtn_Click);
			// 
			// RAMSortBtn
			// 
			this->RAMSortBtn->Location = System::Drawing::Point(7, 20);
			this->RAMSortBtn->Name = L"RAMSortBtn";
			this->RAMSortBtn->Size = System::Drawing::Size(75, 23);
			this->RAMSortBtn->TabIndex = 0;
			this->RAMSortBtn->Text = L"ОЗУ";
			this->RAMSortBtn->UseVisualStyleBackColor = true;
			this->RAMSortBtn->Click += gcnew System::EventHandler(this, &StoreForm::RAMSortBtn_Click);
			// 
			// groupBox2
			// 
			this->groupBox2->Controls->Add(this->toFilterTextBox);
			this->groupBox2->Controls->Add(this->label8);
			this->groupBox2->Controls->Add(this->fromFilterTextBox);
			this->groupBox2->Controls->Add(this->diagonalFilterCombobox);
			this->groupBox2->Controls->Add(this->mpFilterCombobox);
			this->groupBox2->Controls->Add(this->ramFilterCombobox);
			this->groupBox2->Controls->Add(this->processorFilterCombobox);
			this->groupBox2->Controls->Add(this->modelFilterCombobox);
			this->groupBox2->Controls->Add(this->nameFilterCombobox);
			this->groupBox2->Controls->Add(this->label7);
			this->groupBox2->Controls->Add(this->label6);
			this->groupBox2->Controls->Add(this->label5);
			this->groupBox2->Controls->Add(this->label4);
			this->groupBox2->Controls->Add(this->label3);
			this->groupBox2->Controls->Add(this->label2);
			this->groupBox2->Controls->Add(this->label1);
			this->groupBox2->Location = System::Drawing::Point(13, 266);
			this->groupBox2->Name = L"groupBox2";
			this->groupBox2->Size = System::Drawing::Size(774, 100);
			this->groupBox2->TabIndex = 2;
			this->groupBox2->TabStop = false;
			this->groupBox2->Text = L"Фильтрация";
			// 
			// toFilterTextBox
			// 
			this->toFilterTextBox->Location = System::Drawing::Point(597, 56);
			this->toFilterTextBox->Name = L"toFilterTextBox";
			this->toFilterTextBox->Size = System::Drawing::Size(100, 20);
			this->toFilterTextBox->TabIndex = 15;
			// 
			// label8
			// 
			this->label8->AutoSize = true;
			this->label8->Location = System::Drawing::Point(571, 59);
			this->label8->Name = L"label8";
			this->label8->Size = System::Drawing::Size(19, 13);
			this->label8->TabIndex = 14;
			this->label8->Text = L"до";
			// 
			// fromFilterTextBox
			// 
			this->fromFilterTextBox->Location = System::Drawing::Point(623, 21);
			this->fromFilterTextBox->Name = L"fromFilterTextBox";
			this->fromFilterTextBox->Size = System::Drawing::Size(100, 20);
			this->fromFilterTextBox->TabIndex = 13;
			// 
			// diagonalFilterCombobox
			// 
			this->diagonalFilterCombobox->FormattingEnabled = true;
			this->diagonalFilterCombobox->Location = System::Drawing::Point(444, 65);
			this->diagonalFilterCombobox->Name = L"diagonalFilterCombobox";
			this->diagonalFilterCombobox->Size = System::Drawing::Size(84, 21);
			this->diagonalFilterCombobox->TabIndex = 12;
			// 
			// mpFilterCombobox
			// 
			this->mpFilterCombobox->FormattingEnabled = true;
			this->mpFilterCombobox->Location = System::Drawing::Point(459, 20);
			this->mpFilterCombobox->Name = L"mpFilterCombobox";
			this->mpFilterCombobox->Size = System::Drawing::Size(69, 21);
			this->mpFilterCombobox->TabIndex = 11;
			// 
			// ramFilterCombobox
			// 
			this->ramFilterCombobox->FormattingEnabled = true;
			this->ramFilterCombobox->Location = System::Drawing::Point(274, 65);
			this->ramFilterCombobox->Name = L"ramFilterCombobox";
			this->ramFilterCombobox->Size = System::Drawing::Size(81, 21);
			this->ramFilterCombobox->TabIndex = 10;
			// 
			// processorFilterCombobox
			// 
			this->processorFilterCombobox->FormattingEnabled = true;
			this->processorFilterCombobox->Location = System::Drawing::Point(284, 20);
			this->processorFilterCombobox->Name = L"processorFilterCombobox";
			this->processorFilterCombobox->Size = System::Drawing::Size(101, 21);
			this->processorFilterCombobox->TabIndex = 9;
			// 
			// modelFilterCombobox
			// 
			this->modelFilterCombobox->FormattingEnabled = true;
			this->modelFilterCombobox->Location = System::Drawing::Point(74, 65);
			this->modelFilterCombobox->Name = L"modelFilterCombobox";
			this->modelFilterCombobox->Size = System::Drawing::Size(139, 21);
			this->modelFilterCombobox->TabIndex = 8;
			// 
			// nameFilterCombobox
			// 
			this->nameFilterCombobox->FormattingEnabled = true;
			this->nameFilterCombobox->Location = System::Drawing::Point(69, 20);
			this->nameFilterCombobox->Name = L"nameFilterCombobox";
			this->nameFilterCombobox->Size = System::Drawing::Size(120, 21);
			this->nameFilterCombobox->TabIndex = 7;
			// 
			// label7
			// 
			this->label7->AutoSize = true;
			this->label7->Location = System::Drawing::Point(555, 24);
			this->label7->Name = L"label7";
			this->label7->Size = System::Drawing::Size(62, 13);
			this->label7->TabIndex = 6;
			this->label7->Text = L"По цене от";
			// 
			// label6
			// 
			this->label6->AutoSize = true;
			this->label6->Location = System::Drawing::Point(6, 69);
			this->label6->Name = L"label6";
			this->label6->Size = System::Drawing::Size(62, 13);
			this->label6->TabIndex = 5;
			this->label6->Text = L"По модели";
			// 
			// label5
			// 
			this->label5->AutoSize = true;
			this->label5->Location = System::Drawing::Point(221, 69);
			this->label5->Name = L"label5";
			this->label5->Size = System::Drawing::Size(47, 13);
			this->label5->TabIndex = 4;
			this->label5->Text = L"По ОЗУ";
			// 
			// label4
			// 
			this->label4->AutoSize = true;
			this->label4->Location = System::Drawing::Point(391, 24);
			this->label4->Name = L"label4";
			this->label4->Size = System::Drawing::Size(62, 13);
			this->label4->TabIndex = 3;
			this->label4->Text = L"По камере";
			// 
			// label3
			// 
			this->label3->AutoSize = true;
			this->label3->Location = System::Drawing::Point(361, 69);
			this->label3->Name = L"label3";
			this->label3->Size = System::Drawing::Size(77, 13);
			this->label3->TabIndex = 2;
			this->label3->Text = L"По диагонали";
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Location = System::Drawing::Point(195, 24);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(83, 13);
			this->label2->TabIndex = 1;
			this->label2->Text = L"По процессору";
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Location = System::Drawing::Point(7, 24);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(56, 13);
			this->label1->TabIndex = 0;
			this->label1->Text = L"По марке";
			// 
			// StoreForm
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(797, 397);
			this->Controls->Add(this->groupBox2);
			this->Controls->Add(this->groupBox1);
			this->Controls->Add(this->dataGridView1);
			this->Name = L"StoreForm";
			this->Text = L"StoreForm";
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->dataGridView1))->EndInit();
			this->groupBox1->ResumeLayout(false);
			this->groupBox1->PerformLayout();
			this->groupBox2->ResumeLayout(false);
			this->groupBox2->PerformLayout();
			this->ResumeLayout(false);

		}
#pragma endregion
		void ChangeComboBoxContext(System::Object ^sender, System::EventArgs ^e) {
			printData(filterCreator());
		}
	private: System::Void RAMSortBtn_Click(System::Object^  sender, System::EventArgs^  e) {
		printData(sortCreator(InstrumentsForPhones::getRam));
	}
	private: System::Void CameraSortBtn_Click(System::Object^  sender, System::EventArgs^  e) {
		printData(sortCreator(InstrumentsForPhones::getCamera));
	}
	private: System::Void DiagonalSortBtn_Click(System::Object^  sender, System::EventArgs^  e) {
		printData(sortCreator(InstrumentsForPhones::getDiagonal));
	}
	private: System::Void PriceSortBtn_Click(System::Object^  sender, System::EventArgs^  e) {
		printData(sortCreator(InstrumentsForPhones::getPrice));
	}
			 void OnTextChanged(System::Object ^sender, System::EventArgs ^e) {
				 printData(filterCreator());
			 }
	};
}
