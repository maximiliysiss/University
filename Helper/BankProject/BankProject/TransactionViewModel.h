#pragma once
#include "Transaction.h"
#include "User.h"

namespace BankProject::ViewModels {

	ref class TransactionViewModel {
		Models::Transaction* model;
		Data::IUserRepository* repo;
	public:
		TransactionViewModel(Models::Transaction* model, Data::IUserRepository* repo) :model(model), repo(repo) {
		}

		property String^ From {
			String^ get() {
				auto user = repo->getUserByAccountId(model->get_fromAccountId());
				return gcnew String((user->get_name() + " " + user->get_surname()).c_str());
			}
		}

		property String^ To {
			String^ get() {
				auto user = repo->getUserByAccountId(model->get_toAccountId());
				return gcnew String((user->get_name() + " " + user->get_surname()).c_str());
			}
		}

		property double Value {
			double get() {
				return model->get_value();
			}
		}

		property String^ DateTime {
			String^ get() {
				return gcnew String(model->get_datetime().c_str());
			}
		}

		~TransactionViewModel() {
			delete model;
		}
	};

}