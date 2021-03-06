﻿#pragma once
#include "Commons.h"

// Обернуть в кавычки
#define sqlstring(type) ("'" + type + "'")
// Конвертация в дату
#define tosqldatetime(type) ("convert(datetime,'" + type + "',103)")
// В строку
#define str(any) std::to_string(any)

#define auto_property(type, name) \
	private: \
		type name; \
	public: \
		type get_##name(){ \
			return name; \
		} \
		void set_##name(type name){ \
			this->name = name; \
		} \
	private: 

#define readonly_property(type, name) \
	private: \
		type name; \
	public: \
		type get_##name(){ \
			return name; \
		} \
	private: 

#define set_property(type, name) \
	private: \
		type name; \
	public: \
		void set_##name(type name){ \
			this->name = name; \
		} \
	private: 