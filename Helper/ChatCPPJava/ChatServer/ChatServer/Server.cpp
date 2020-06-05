#include "Server.h"
#include <iostream>

int Server::initWSAServer() {

	WSADATA wsaData;
	int result = WSAStartup(MAKEWORD(2, 2), &wsaData);

	if (result != 0) {
		log << "WSAStartup failed: " << result << "\n";
		return result;
	}

	struct addrinfo hints;
	ZeroMemory(&hints, sizeof(hints));

	hints.ai_family = AF_INET;
	hints.ai_socktype = SOCK_STREAM;
	hints.ai_protocol = IPPROTO_TCP;
	hints.ai_flags = AI_PASSIVE;

	result = getaddrinfo(ip.c_str(), port.c_str(), &hints, &addr);

	if (result != 0) {
		log << "getaddrinfo failed: " << result << "\n";
		WSACleanup();
		return EXIT_FAILURE;
	}


	listen_socket = socket(addr->ai_family, addr->ai_socktype,
		addr->ai_protocol);
	if (listen_socket == INVALID_SOCKET) {
		log << "Error at socket: " << WSAGetLastError() << "\n";
		close();
		return EXIT_FAILURE;
	}

	result = bind(listen_socket, addr->ai_addr, (int)addr->ai_addrlen);

	if (result == SOCKET_ERROR) {
		log << "bind failed with error: " << WSAGetLastError() << "\n";
		close();
		return EXIT_FAILURE;
	}

	if (listen(listen_socket, SOMAXCONN) == SOCKET_ERROR) {
		log << "listen failed with error: " << WSAGetLastError() << "\n";
		close();
		return EXIT_FAILURE;
	}

	return 0;
}

int Server::serverLoop() {
	while (isWork) {

		int client_socket = accept(listen_socket, NULL, NULL);
		if (client_socket == INVALID_SOCKET) {
			log << "accept failed: " << WSAGetLastError() << "\n";
			close();
			return EXIT_FAILURE;
		}

		if (clients.size() >= max_pool) {
			std::string msg = "Too many users";
			log << "Too many users\n";
			send(client_socket, msg.data(), msg.length(), 0);
			closesocket(client_socket);
		}
		else {
			try {
				int len = 0;
				recv(client_socket, (char*)&len, sizeof(int), 0);
				len = ntohl(len);
				char* name = new char[len + 1]{ 0 };
				recv(client_socket, name, len, 0);

				log << "New user: " << get_utf16(std::string(name)) << std::endl;
				Client* newCl = new Client(client_socket, *this, name, log);
				newCl->start();
				{
					lkm lg(m);
					clients.push_back(newCl);
				}

				std::string msg = std::string(name) + " log in";
				broadcastSend((char*)msg.data(), msg.length(), newCl);
			}
			catch (...) {
				log << "Client error connect\n";
			}
		}

		Sleep(50);
	}

	return 0;
}

void Server::readServerCommands() {

	std::string cmd;

	while (isWork) {
		std::getline(std::cin, cmd);
		std::transform(cmd.begin(), cmd.end(), cmd.begin(), std::tolower);

		if (cmd == "quit") {
			isWork = false;
		}
	}

	log << "Server was stoped" << std::endl;
	exit(0);
}

Server::Server(std::string port, std::string ip, std::ostream& log, std::wostream& wlog, int max_pool)
	:port(port), ip(ip), log(log), wlog(wlog), listen_socket(SOCKET_ERROR), max_pool(max_pool) {
}

int Server::start() {

	auto res = initWSAServer();
	isWork = true;
	std::thread(&Server::readServerCommands, this).detach();
	log << "Success start chat server " << ip << ":" << port << std::endl;
	serverLoop();
	close();
	log << "Server was stoped" << std::endl;
	return 0;
}

void Server::close() {
	closesocket(listen_socket);
	freeaddrinfo(addr);
	WSACleanup();
}

void Server::broadcastSend(char* msg, int len, Client* cl) {
	lkm lg(m);

	for (auto u : clients) {
		send(u->getSocket(), (char*)&len, sizeof(int), 0);
		send(u->getSocket(), msg, len, 0);
	}
}

void Server::closeClient(Client* cl) {
	auto msg = cl->getName() + " logout";
	log << msg << std::endl;
	broadcastSend((char*)msg.data(), msg.length(), cl);

	lkm lg(m);
	auto iter = std::find(clients.begin(), clients.end(), cl);
	if (iter != clients.end())
		clients.erase(iter);
}

std::string get_utf8(const std::wstring& wstr) {
	if (wstr.empty()) return std::string();
	int sz = WideCharToMultiByte(CP_UTF8, 0, &wstr[0], -1, 0, 0, 0, 0);
	std::string res(sz, 0);
	WideCharToMultiByte(CP_UTF8, 0, &wstr[0], -1, &res[0], sz, 0, 0);
	return res;
}

std::wstring get_utf16(const std::string& str) {
	if (str.empty()) return std::wstring();
	int sz = MultiByteToWideChar(CP_UTF8, 0, &str[0], -1, 0, 0);
	std::wstring res(sz, 0);
	MultiByteToWideChar(CP_UTF8, 0, &str[0], -1, &res[0], sz);
	return res;
}
