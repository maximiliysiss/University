#pragma once
#include <map>
#include <string>
#include <typeinfo>

namespace BankProject::Commons {

	class IOContainer {

	private:
		static IOContainer* ioc;
		std::map<std::string, void*> memoryContainer;

		IOContainer() = default;

	public:

		static IOContainer& getInstance() {
			if (ioc)
				return *ioc;
			ioc = new IOContainer();
			return *ioc;
		}

		template<typename I, typename T>
		void registerService(T* service) {
			memoryContainer[typeid(I).name()] = (void*)service;
		}

		template<typename I>
		I* resolve() {
			return (I*)memoryContainer[typeid(I).name()];
		}
	};

	IOContainer* IOContainer::ioc = nullptr;
}