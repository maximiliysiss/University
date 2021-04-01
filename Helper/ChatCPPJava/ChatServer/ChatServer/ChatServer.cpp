#include "Server.h"
#include <iostream>

int main(int argc, char** args)
{
	if (argc < 4) {
		std::cout << "Enter port,ip and max pool\n";
		return EXIT_FAILURE;
	}

	Server server(args[2], args[1], std::cout, std::wcout, std::stoi(args[3]));
	server.start();
}
