#include "IDataContext.h"

template<typename T>
BankProject::Data::IBaseRepository<T>::IBaseRepository() {
	this->context = BankProject::Commons::IOContainer::getInstance().resolve<IDataContext>();
}

void BankProject::Data::UserRepository::insert(User obj) {
}

void BankProject::Data::UserRepository::remove(User obj) {
}

void BankProject::Data::UserRepository::update(User obj) {
}

std::list<User> BankProject::Data::UserRepository::select() {
	return std::list<User>();
}
