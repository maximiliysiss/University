#pragma once
#include "defines.h"
#include "Departament.h"
#include "WindowsFunctionWrapper.h"
#include "Repositories.h"

namespace BankProject::ViewModels {

	ref class DepartmentViewModel {
	private:
		Models::Department* model;
		Data::IBaseRepository<Models::Department>* repo;
	public:
		DepartmentViewModel(Models::Department* model, Data::IBaseRepository<Models::Department>* repo) :model(model), repo(repo) {}

	public:
		property String^ Name {
			String^ get() {
				return gcnew String(model->get_name().c_str());
			}
			void set(String^ set) {
				model->set_name(toStdString(set));
			}
		}

		property String^ Adress {
			String^ get() {
				return gcnew String(model->get_adress().c_str());
			}
			void set(String^ set) {
				model->set_adress(toStdString(set));
			}
		}

		property int Id {
			int get() {
				return model->get_id();
			}
		}

	public:

		bool isNew() {
			return Id <= 0;
		}

		bool isDefault() {
			return Id == 1;
		}

		std::list<std::string> onSave() {
			if (this->Name->Length == 0) {
				return { "Name length must be > 0" };
			}

			if (isNew())
				repo->insert(*model);
			else
				repo->update(*model);

			return {};
		}

		void onDelete() {
			repo->remove(*model);
		}

		~DepartmentViewModel() {
			delete model;
		}
	};

}