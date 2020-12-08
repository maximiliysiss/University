#pragma once


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