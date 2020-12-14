#pragma once
#include "User.h"
#include "Repositories.h"
#include "ClientViewModel.h"
#include "Client.h"

namespace BankProject::ViewModels {

	ref class ManagerViewModel {
	private:
		Data::IUserRepository* repo;
		readonly_property(System::Collections::Generic::List<ViewModels::ClientViewModel^>^, clientViewModels);

		set_property(System::Action^, reload);

		void reloadData() {

			clientViewModels = gcnew System::Collections::Generic::List<ViewModels::ClientViewModel^>();

			for (auto cl : repo->selectClients()) {
				clientViewModels->Add(gcnew ViewModels::ClientViewModel(cl, repo));
			}
			reload();
		}

		void openClient(ViewModels::ClientViewModel^ vm) {
			Client^ form = gcnew Client(vm);
			form->ShowDialog();
		}

	public:
		ManagerViewModel(Data::IUserRepository* repo) :repo(repo) {
		}

		void load() {
			reloadData();
		}

		void onAddNewClient() {
			openClient(gcnew ViewModels::ClientViewModel(Models::UserFactory::createClient(), repo));
			reloadData();
		}

		void onOpenClient(int index) {
			openClient(clientViewModels[index]);
			reloadData();
		}

	};

}