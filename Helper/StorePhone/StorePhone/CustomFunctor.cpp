#include "CustomFunctor.h"

template<typename T>
bool PhoneFunctor<T>::operator()(Phone & in)
{
	return get(in) == val;
}

template<typename T>
bool PhoneBiFunctor<T>::operator()(Phone& p1, Phone& p2) {
	if (direction)
		return get(p1) < get(p2);
	return get(p1) > get(p2);
}

template<typename T>
bool PhoneFunctorCondPrice<T>::operator()(Phone & p) {
	return p.getprice() >= this->from && p.getprice() <= this->to;
}