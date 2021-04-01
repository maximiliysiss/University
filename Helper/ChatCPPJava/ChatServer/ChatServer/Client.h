#pragma once
#define _WIN32_WINNT 0x501
#include <WinSock2.h>
#include <WS2tcpip.h>
#pragma comment(lib, "Ws2_32.lib")

#include "Server.h"
#include <string>

class Server;

class Client
{
private:
	SOCKET socket;
	Server& server;
	std::string name;
	std::ostream& log;

	void clientLoop();
	void stop();
public:

	Client(SOCKET socker, Server& server, std::string name, std::ostream& log);
	void start();
	inline SOCKET getSocket() {
		return socket;
	}
	inline std::string getName() {
		return name;
	}
};

