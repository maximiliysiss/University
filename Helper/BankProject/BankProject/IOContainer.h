#pragma once
#include <map>
#include <string>
#include <typeinfo>

namespace BankProject::Commons {

	// Контейнер для DIP
	class IOContainer {

	private:
		static IOContainer* ioc;
		std::map<std::string, void*> memoryContainer;

		IOContainer() = default;

	public:

		static IOContainer& getInstance();

		template<typename I, typename T>
		void registerService(T* service) {
			memoryContainer[typeid(I).name()] = (void*)service;
		}

		template<typename T>
		void registerService(T* service) {
			registerService<T, T>(service);
		}

		template<typename I>
		I* resolve() {
			return (I*)memoryContainer[typeid(I).name()];
		}
	};
}