#include "IOContainer.h"

namespace BankProject::Commons {

	IOContainer* IOContainer::ioc = nullptr;

	IOContainer& BankProject::Commons::IOContainer::getInstance()
	{
		if (ioc)
			return *ioc;
		ioc = new IOContainer();
		return *ioc;
	}

}
