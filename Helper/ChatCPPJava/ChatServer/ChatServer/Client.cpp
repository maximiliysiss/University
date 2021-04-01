#include "Client.h"
#include <iostream>

void Client::clientLoop() {
	while (server.getIsWork()) {

		int len;
		recv(socket, (char*)&len, sizeof(int), 0);
		len = ntohl(len);
		if (socket == SOCKET_ERROR || socket == INVALID_SOCKET || len <= 0)
			break;
		char* data = new char[len + 1]{ 0 };
		recv(socket, (char*)data, len, 0);
		if (socket == SOCKET_ERROR || socket == INVALID_SOCKET || strlen(data) == 0)
			break;

		log << std::string(data) << std::endl;

		delete[] data;
	}

	stop();
}

void Client::stop() {
	server.closeClient(this);
	if (socket != SOCKET_ERROR) {
		closesocket(socket);
	}
}

Client::Client(SOCKET socker, Server& server, std::string name, std::ostream& log)
	:socket(socker), server(server), name(name), log(log) {
}

void Client::start() {
	std::thread(&Client::clientLoop, this).detach();
}
