#pragma once
#include <string>
#include <algorithm>
#include <cctype>
#include <locale>
#include <codecvt>
#include <list>
#include <algorithm>
#include <sstream>


///
/// Дополнительные утилиты
///

namespace std {

	/// Цвета
	static auto WHITE_RGB = RGB(0, 0, 0);
	static auto WHITE_RGB_TEXT = RGB(255, 255, 255);
	static auto GREY_RGB = RGB(100, 100, 100);
	static auto ACTIVE_GREY_RGB = RGB(200, 200, 200);

	/// Конвертер для wstring/string
	static std::wstring_convert<std::codecvt_utf8_utf16<wchar_t>, wchar_t> converterToWString;

	/// Лист уникальных идентификаторов
	static std::list<unsigned int> guids;

	/// Сконвертировать строки
	static inline std::string convertToString(std::wstring input) {
		return converterToWString.to_bytes(input);
	}

	/// Получить уникальный ID
	static inline unsigned int GetGuid() {
		int attempt = 100;
		while (true) {
			if (attempt == 0)
				break;
			unsigned int tmp = rand() % UINT_MAX;
			tmp *= tmp < 0 ? -1 : 1;
			auto find = std::find(guids.begin(), guids.end(), tmp);
			if (find == guids.end()) {
				guids.push_back(tmp);
				return tmp;
			}
			attempt--;
		}
		return -1;
	}

	template<typename T>
	static inline void ltrim(T &s) {
		s.erase(s.begin(), std::find_if(s.begin(), s.end(), [](int ch) {
			return !std::isspace(ch);
		}));
	}

	template<typename T>
	static inline void rtrim(T &s) {
		s.erase(std::find_if(s.rbegin(), s.rend(), [](int ch) {
			return !std::isspace(ch);
		}).base(), s.end());
	}


	/// trim для строк
	template<typename T>
	static inline T trim(T& input) {
		ltrim(input);
		rtrim(input);
		return input;
	}

	static inline std::wstring trim(std::wstring & input) {
		while (input.size() && (isspace(input.front()) || input.front() == '\0'))
			input.erase(input.begin());
		while (input.size() && (isspace(input.back()) || input.back() == '\0'))
			input.pop_back();
		return input;
	}
}