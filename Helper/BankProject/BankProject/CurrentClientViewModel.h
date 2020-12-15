#pragma once
#include "User.h"
#include "defines.h"
#include "Repositories.h"
#include "TransactionViewModel.h"
#include "UserContext.h"

namespace BankProject::ViewModels {

	ref class CurrentViewModel {

	private:
		Models::User* model;
		Data::IUserRepository* repo;
		Data::ITransactionRepository* trRepo;

		Models::Account* acc;

		readonly_property(System::Collections::Generic::List<ViewModels::TransactionViewModel^>^, transactions);
		set_property(System::Action^, reload);

		/// <summary>
		/// Обновить данные
		/// </summary>
		void reloadData() {
			transactions = gcnew System::Collections::Generic::List<ViewModels::TransactionViewModel^>();

			for (auto tr : trRepo->getUserTransactions(acc->get_id())) {
				transactions->Add(gcnew TransactionViewModel(tr, this->repo));
			}

			reload();
		}

	public:

		CurrentViewModel(Data::IUserRepository* repo, Data::IAccountRepository* accRepo, Data::ITransactionRepository* trRepo) : repo(repo), trRepo(trRepo) {
			auto context = Commons::IOContainer::getInstance().resolve<Commons::UserContext>();
			acc = accRepo->getById(context->get_userId());
		}

		property String^ AccountIdStr {
			String^ get() {
				return Convert::ToString(acc->get_id());
			}
		}

		void load() {
			reloadData();
		}

	};

}