﻿#pragma once
#include <string>
#include <list>

using namespace System;

// Конвертация System::String -> std::string
std::string toStdString(String^ str);

// join strings to one with separator
std::string strjoin(const char* separator);

template<typename T, typename... Targs>
T strjoin(const char* separator, T value, Targs... Fargs) // recursive variadic function
{
	auto res = value + separator + strjoin(separator, Fargs...);
	return res.substr(0, res.length() - strlen(separator));
}


std::string strjoin(const char* separator, std::list<std::string> list);