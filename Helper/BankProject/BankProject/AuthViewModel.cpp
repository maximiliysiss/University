#include "AuthViewModel.h"
#include "MainForm.h"
#include "Crypto.h"

void BankProject::ViewModels::AuthViewModel::login(std::string login, std::string password) {

	auto findUser = repo->getByLoginPassword(login, Services::Crypto::md5(password));
	if (findUser == nullptr) {
		System::Windows::Forms::MessageBox::Show("Login/Password is incorrect", "Error");
		return;
	}

	auto userContext = Commons::IOContainer::getInstance().resolve<Commons::UserContext>();
	userContext->set_role(findUser->get_role());
	userContext->set_userId(findUser->get_id());

	close();

	MainForm^ mainForm = gcnew MainForm();
	mainForm->ShowDialog();
}
