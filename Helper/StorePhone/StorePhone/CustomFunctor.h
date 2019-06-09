#pragma once
#include "Phone.h"

/*универсальный функтор для одного аргумента*/
template<typename Input, typename Output>
class CustomFunctor {
public:
	virtual Output operator()(Input) = 0;
};

/*универсальный функтор для двух аргументов (для сортировок)*/
template<typename Input, typename Output>
class BiDirectalFunctor {
public:
	virtual Output operator()(Input, Input) = 0;
};

/*функтор для телефона по получению определенного свойства*/
template<typename T>
class PhoneFunctor : public CustomFunctor<Phone&, bool> {
	typedef T(*getter)(Phone&);
	getter get;
	T val;
public:
	PhoneFunctor(getter get, T val) :get(get), val(val) {}
	virtual bool operator()(Phone& in) override;
};

/*функтор для цен*/
template<typename T>
class PhoneFunctorCondPrice : public CustomFunctor<Phone&, bool> {
	float from, to;
public:
	PhoneFunctorCondPrice(float from, float to) :from(from), to(to) {}
	virtual bool operator()(Phone& in) override;
};

/*функтор для сортировок телефона*/
template<typename T>
class PhoneBiFunctor : public BiDirectalFunctor<Phone&, bool> {
	typedef T(*getter)(Phone&);
	getter get;
	bool direction;
public:
	PhoneBiFunctor(getter get, bool direction) :get(get), direction(direction) {}
	virtual bool operator()(Phone&, Phone&) override;
};
