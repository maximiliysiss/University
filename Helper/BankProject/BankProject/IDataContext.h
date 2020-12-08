#pragma once
#include "IOContainer.h"
#include "DataContext.h"
#include "User.h"
#include <list>

using namespace BankProject::Models;

namespace BankProject::Data {

	class IDataContext {
	public:
		virtual void execute(std::string sql) = 0;
	};

	template<typename T>
	class IBaseRepository {
	protected:
		IDataContext* context;
	public:
		IBaseRepository();

		virtual void insert(T obj) = 0;
		virtual void remove(T obj) = 0;
		virtual void update(T obj) = 0;
		virtual std::list<T> select() = 0;

		virtual ~IBaseRepository() {}
	};

	class UserRepository : public IBaseRepository<User> {
	public:

		// Inherited via IBaseRepository
		virtual void insert(User obj) override;

		virtual void remove(User obj) override;

		virtual void update(User obj) override;

		virtual std::list<User> select() override;

	};

}