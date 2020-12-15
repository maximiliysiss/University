#pragma once
#include "User.h"
#include "Repositories.h"
#include "DepartmentViewModel.h"
#include "TransactionViewModel.h"
#include "CreateTransaction.h"
#include "EnterData.h"

namespace BankProject::ViewModels {

	ref class ClientViewModel {
	private:
		Models::User* model;
		Data::IUserRepository* repo;
		Data::IAccountRepository* accRepo;
		Data::ITransactionRepository* trRepo;

		readonly_property(System::Collections::Generic::List<ViewModels::TransactionViewModel^>^, transactions);
		set_property(System::Action^, reload);

		/// <summary>
		/// Обновить данные
		/// </summary>
		void reloadData() {
			transactions = gcnew System::Collections::Generic::List<ViewModels::TransactionViewModel^>();

			for (auto tr : trRepo->getUserTransactions(AccountId)) {
				transactions->Add(gcnew TransactionViewModel(tr, this->repo));
			}

			reload();
		}

		Account* getAccount() {
			return accRepo->getById(Id);
		}

	public:

		ClientViewModel(Models::User* model, Data::IUserRepository* repo) : model(model), repo(repo) {
			accRepo = Commons::IOContainer::getInstance().resolve<Data::IAccountRepository>();
			trRepo = Commons::IOContainer::getInstance().resolve<Data::ITransactionRepository>();
		}

		/// <summary>
		/// Создать транзакцию
		/// </summary>
		void createTransaction();

		bool isNew() {
			return Id == 0;
		}

		/// <summary>
		/// Сохранить
		/// </summary>
		/// <returns></returns>
		std::list<std::string> Save() {
			if (Name->Length == 0 || Surname->Length == 0 || Passport->Length == 0 || Birthplace->Length == 0) {
				return { "Not all information wrote" };
			}

			if (isNew())
				repo->createClient(*model);
			else
				repo->update(*model);
			return {};
		}

		/// <summary>
		/// Удалить
		/// </summary>
		void Remove() {
			repo->remove(*model);
		}

		/// <summary>
		/// Загрузить
		/// </summary>
		void load() {
			reloadData();
		}

		/// <summary>
		/// Добавить денег
		/// </summary>
		void addMoney() {
			EnterData^ entryData = gcnew EnterData();
			entryData->ShowDialog();
			if (entryData->Value > 0) {
				accRepo->addMoney(AccountId, entryData->Value);
				reloadData();
			}
		}

		// Свойства

		property String^ Password {
			void set(String^ pass) {
				model->set_passwordHash(Services::Crypto::md5(toStdString(pass)));
			}
		}

		property int AccountId {
			int get() {
				auto acc = getAccount();
				if (acc)
					return acc->get_id();
				return 0;
			}
		}

		property String^ AccountIdStr {
			String^ get() {
				auto acc = getAccount();
				if (acc)
					return Convert::ToString(acc->get_id());
				return nullptr;
			}
		}

		property String^ Value {
			String^ get() {
				auto acc = getAccount();
				if (acc)
					return Convert::ToString(acc->get_value());
				return nullptr;
			}
		}

		property String^ Birthday {
			String^ get() {
				return gcnew String(model->get_birthdate().c_str());
			}
			void set(String^ name) {
				model->set_birthdate(toStdString(name));
			}
		}

		property String^ Birthplace {
			String^ get() {
				return gcnew String(model->get_birthplace().c_str());
			}
			void set(String^ name) {
				model->set_birthplace(toStdString(name));
			}
		}

		property String^ Passport {
			String^ get() {
				return gcnew String(model->get_passport().c_str());
			}
			void set(String^ name) {
				model->set_passport(toStdString(name));
			}
		}

		property String^ Name {
			String^ get() {
				return gcnew String(model->get_name().c_str());
			}
			void set(String^ name) {
				model->set_name(toStdString(name));
			}
		}

		property String^ Surname {
			String^ get() {
				return gcnew String(model->get_surname().c_str());
			}
			void set(String^ name) {
				model->set_surname(toStdString(name));
			}
		}

		property String^ Login {
			String^ get() {
				return gcnew String(model->get_login().c_str());
			}
			void set(String^ name) {
				model->set_login(toStdString(name));
			}
		}

		property int Id {
			int get() {
				return model->get_id();
			}
		}

		~ClientViewModel() {
			delete model;
		}
	};

}