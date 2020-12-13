#pragma once
#include "User.h"
#include "Repositories.h"
#include "DepartmentViewModel.h"

namespace BankProject::ViewModels {

	ref class ClientViewModel {
	private:
		Models::User* model;
		Data::IUserRepository* repo;
	public:

		ClientViewModel(Models::User* model, Data::IUserRepository* repo) : model(model), repo(repo) {
		}

		bool isNew() {
			return Id == 0;
		}

		std::list<std::string> Save() {
			if (Name->Length == 0 || Surname->Length == 0 || Passport->Length == 0 || Birthplace->Length == 0) {
				return { "Not all information wrote" };
			}

			if (isNew())
				repo->insert(*model);
			else
				repo->update(*model);
			return {};
		}

		void Remove() {
			repo->remove(*model);
		}


		property String^ Password {
			void set(String^ pass) {
				model->set_passwordHash(Services::Crypto::md5(toStdString(pass)));
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