#pragma once
#include <fstream>
#include <json/json.h>
#include <exception>
#include "defines.h"

namespace BankProject::Commons {

	class Config {

		readonly_property(std::string, connectionString);
		Config() {}

	public:
		static Config* readConfig(std::string fileName) {
			std::ifstream configFile(fileName, std::ios::in);
			if (!configFile.is_open())
				throw std::exception("Not found file");

			Json::Value root;

			configFile >> root;
			configFile.close();

			Config* config = new Config();

			config->connectionString = root["ConnectionString"].asString().c_str();

			return config;
		}

	};

}