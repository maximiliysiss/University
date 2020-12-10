#pragma once
#include "IOContainer.h"
#include "DataContext.h"
#include "User.h"
#include "Transaction.h"
#include "Accounts.h"
#include "Crypto.h"
#include <algorithm>

using namespace BankProject::Models;

namespace BankProject::Data {

	template<typename T>
	class IBaseRepository {
	protected:
		IDataContext* context;
	public:
		IBaseRepository()
			:context(BankProject::Commons::IOContainer::getInstance().resolve<IDataContext>())
		{}

		virtual void insert(T obj) = 0;
		virtual void remove(T obj) = 0;
		virtual void update(T obj) = 0;
		virtual std::list<T*> select() = 0;

		virtual ~IBaseRepository() {}
	};

	class IUserRepository : public IBaseRepository<User> {
	public:
		virtual User* getByLoginPassword(std::string login, std::string password) = 0;
	};

	class UserRepository : public IUserRepository {
	public:

		virtual void insert(User obj) override;

		virtual void remove(User obj) override;

		virtual void update(User obj) override;

		virtual std::list<User*> select() override;

		virtual User* getByLoginPassword(std::string login, std::string password) override;

	};

	class IAccountRepository : public IBaseRepository<Account> {
	private:
		virtual void insert(Account obj) override {}
		virtual void remove(Account obj) override {}
		virtual std::list<Account*> select() override { return std::list<Account*>(); }
	public:
		virtual void update(Account obj) = 0;
		virtual Account* getById(int id) = 0;
	};

	class AccountRepository : public IAccountRepository {
	public:
		virtual void update(Account obj) override;
		virtual Account* getById(int id) override;
	};

	class ITransactionRepository : public IBaseRepository<Transaction> {
	private:
		virtual void insert(Transaction obj) override {}
		virtual void remove(Transaction obj) override {}
		virtual std::list<Transaction*> select() override { return std::list<Transaction*>(); }
		virtual void update(Transaction obj) {}
	public:
		virtual std::list<Transaction*> getUserTransactions(int userId) = 0;
		virtual void createTransaction(int from, int to, int manager, double value) = 0;
	};

	class TransactionRepository : public ITransactionRepository {
	public:
		// Inherited via ITransactionRepository
		virtual std::list<Transaction*> getUserTransactions(int userId) override;
		virtual void createTransaction(int from, int to, int manager, double value) override;
	};

}