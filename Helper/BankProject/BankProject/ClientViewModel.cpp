#include "ClientViewModel.h"

void BankProject::ViewModels::ClientViewModel::createTransaction()
{
	auto form = gcnew BankProject::CreateTransaction(gcnew ViewModels::CreateTransactionViewModel(repo, trRepo, accRepo));
	form->ShowDialog();
	reloadData();
}
