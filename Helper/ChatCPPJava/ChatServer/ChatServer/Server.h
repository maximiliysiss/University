#pragma once
#include "Client.h"

#include <string>
#include <vector>
#include <thread>
#include <atomic>
#include <algorithm>
#include <mutex>

class Client;

std::string get_utf8(const std::wstring& wstr);
std::wstring get_utf16(const std::string& str);

class Server
{
	std::mutex m;
	typedef std::lock_guard<std::mutex> lkm;

private:
	SOCKET listen_socket;
	addrinfo* addr{ nullptr };

	std::string port, ip;
	unsigned max_pool{ 0 };
	std::ostream& log;
	std::wostream& wlog;
	std::atomic<bool> isWork{ false };
	int initWSAServer();
	int serverLoop();

	void readServerCommands();
	std::vector<Client*> clients;
public:
	Server(std::string port, std::string ip, std::ostream& log, std::wostream& wlog, int max_pool);
	int start();
	void close();
	void broadcastSend(char* msg, int len, Client* cl);
	inline bool getIsWork() {
		return isWork;
	}
	void closeClient(Client * cl);
};

