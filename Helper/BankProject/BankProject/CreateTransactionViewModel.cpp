#include "CreateTransactionViewModel.h"

System::Collections::Generic::List<BankProject::ViewModels::AccountComboBox^>^ BankProject::ViewModels::CreateTransactionViewModel::getAccounts()
{
	auto accs = gcnew System::Collections::Generic::List<BankProject::ViewModels::AccountComboBox^>();
	for (auto ac : userRepo->selectClients()) {
		auto accInfo = accRepo->getById(ac->get_id());
		accs->Add(gcnew ViewModels::AccountComboBox(accInfo->get_id(), gcnew String(ac->get_name().c_str())));
	}
	return accs;
}
