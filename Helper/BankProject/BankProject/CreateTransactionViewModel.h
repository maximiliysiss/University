#pragma once
#include "Repositories.h"
#include "UserContext.h"

namespace BankProject::Commons {
	struct UserContext;
}

namespace BankProject::ViewModels {

	/// <summary>
	/// Модель выпадающего списка
	/// </summary>
	ref class AccountComboBox {
	private:
		int id;
		String^ name;
	public:
		AccountComboBox(int id, String^ name) :id(id), name(name) {
		}
		property int Id {
			int get() {
				return id;
			}
		}
		property String^ Name {
			String^ get() {
				return name;
			}
		}
	};

	ref class CreateTransactionViewModel {
	private:
		Data::IUserRepository* userRepo;
		Data::ITransactionRepository* trRepo;
		Data::IAccountRepository* accRepo;

		int fromId, toId;
		double value;

	public:
		CreateTransactionViewModel(Data::IUserRepository* userRepo, Data::ITransactionRepository* trRepo, Data::IAccountRepository* accRepo) :accRepo(accRepo), userRepo(userRepo), trRepo(trRepo) {
		}

		/// <summary>
		/// Получить заполнение для выпадающих списков
		/// </summary>
		/// <returns></returns>
		System::Collections::Generic::List<AccountComboBox^>^ getAccounts();

		property int FromId {
			void set(int id) {
				fromId = id;
			}
		}

		property int ToId {
			void set(int id) {
				toId = id;
			}
		}

		property double Value {
			void set(double val) {
				value = val;
			}
		}

		/// <summary>
		/// Создать
		/// </summary>
		/// <returns></returns>
		std::list<std::string> create() {
			if (fromId == 0 || toId == 0 || value == 0)
				return { "Empty data" };
			if (fromId == toId)
				return { "Try send money to myself" };

			auto context = Commons::IOContainer::getInstance().resolve<Commons::UserContext>();

			trRepo->createTransaction(fromId, toId, context->get_userId(), value);

			return {};
		}
	};
}