#include "CustomList.h"


template<typename T>
CustomList<T>::CustomList()
{
	this->top = new Node<T>();
}

template<typename T>
CustomList<T>::CustomList(const CustomList<T>& list)
{
	this->size = list.size;
	Node<T>& tmp = *list.top;
	this->top = new Node<T>(tmp);
}

template<typename T>
CustomList<T>::~CustomList()
{
}

template<typename T>
void CustomList<T>::add(T elem)
{
	size++;

	if (!top->next) {
		top->next = new Node<T>(elem);
		return;
	}

	CustomList<T>::Node<T> * topCpy = this->top->next;
	while (topCpy->next) {
		topCpy = topCpy->next;
	}

	topCpy->next = new Node<T>(elem);
}

template<typename T>
void CustomList<T>::add(size_t index, T elem)
{
	if (index > size)
		throw new std::exception("index out of bound");

	size++;

	if (!top->next) {
		top->next = new Node<T>(elem);
		return;
	}

	CustomList<T>::Node<T> * topCpy = this->top;
	CustomList<T>::Node<T> * nextCpy = this->top->next;

	while (nextCpy && index > 0) {
		index--;
		topCpy = nextCpy;
		nextCpy = topCpy->next;
	}

	if (!topCpy->next) {
		topCpy->next = new Node<T>(elem);
	}
	else
	{
		Node<T> * nodeTmp = new Node<T>(elem);
		nodeTmp->next = topCpy->next;
		topCpy->next = nodeTmp;
	}
}

template<typename T>
void CustomList<T>::remove(T elem)
{
	CustomList<T>::Node<T> * topCpy = top->next;
	CustomList<T>::Node<T> * prevNode = top;
	while (topCpy) {
		if (topCpy->value == elem) {
			removeNode(topCpy, prevNode);
			return;
		}
		prevNode = topCpy;
		topCpy = topCpy->next;
	}
}

template<typename T>
void CustomList<T>::remove(size_t index)
{
	if (index >= size)
		throw std::exception("index out of bound");
	CustomList<T>::Node<T> * topCpy = top.next;
	CustomList<T>::Node<T> * prevNode = top;
	while (index >= 0) {
		prevNode = topCpy;
		topCpy = topCpy->next;
		index--;
	}
	removeNode(topCpy, prevNode);
}

template<typename T>
void CustomList<T>::removeAll(T elem)
{
	CustomList<T>::Node<T> * topCpy = top->next;
	CustomList<T>::Node<T> * prevNode = top;
	while (topCpy) {
		if (topCpy->value == elem) {
			removeNode(topCpy, prevNode);
			topCpy = prevNode->next;
			continue;
		}
		prevNode = topCpy;
		topCpy = topCpy->next;
	}
}

template<typename T>
T * CustomList<T>::find(T elem)
{
	NodeType * topCpy = this->top->next;
	while (topCpy) {
		if (topCpy->value == elem)
			return &topCpy->value;
		topCpy = topCpy->next;
	}
	return nullptr;
}

template<typename T>
void CustomList<T>::update(T elem)
{
	NodeType * topCpy = this->top->next;
	while (topCpy) {
		if (topCpy->value == elem) {
			topCpy->value = elem;
			return;
		}
		topCpy = topCpy->next;
	}
}

template<typename T>
void CustomList<T>::update(T elem, comparer filter)
{
	NodeType * topCpy = this->top;
	while (topCpy) {
		if (filter(elem, topCpy->value)) {
			topCpy->value = update;
			return;
		}
		topCpy = topCpy->next;
	}
}

template<typename T>
size_t CustomList<T>::length()
{
	return size;
}

template<typename T>
std::ostream & operator<<(std::ostream & out, CustomList<T> list)
{
	return out;
}

template<typename T>
template<typename T1, typename T2>
void CustomList<T>::quickSort(int low, int high, BiDirectalFunctor<T1, T2> * order)
{
	if (low < high)
	{
		int pi = partition(low, high, order);

		quickSort(low, pi - 1, order);
		quickSort(pi + 1, high, order);
	}
}

template<typename T>
template<typename T1, typename T2>
int CustomList<T>::partition(int low, int high, BiDirectalFunctor<T1, T2> * order)
{
	T pivot = (*this)[high];    // pivot 
	int i = (low - 1);  // Index of smaller element 

	for (int j = low; j <= high - 1; j++)
	{
		if ((*order)((*this)[j], pivot))
		{
			i++;    // increment index of smaller element 
			swap((*this)[i], (*this)[j]);
		}
	}
	swap((*this)[i + 1], (*this)[high]);
	return (i + 1);
}

template<typename T>
template<typename T1, typename T2>
CustomList<T> CustomList<T>::where(CustomFunctor<T1, T2> * functor)
{
	CustomList<T> tmp;
	CustomList<T>::Node<T> * tmpTop = top->next;
	while (tmpTop) {
		if ((*functor)(tmpTop->value))
			tmp.add(tmpTop->value);
		tmpTop = tmpTop->next;
	}
	return tmp;
}

template<typename T>
template<typename T1, typename T2>
CustomList<T> CustomList<T>::orderBy(BiDirectalFunctor<T1, T2> * order)
{
	CustomList<T> cpy(*this);
	cpy.quickSort(0, cpy.size - 1, order);
	return cpy;
}

template<typename T>
template<typename R>
CustomList<R> CustomList<T>::convert(R(*converter)(T &elem))
{
	CustomList<R> * newList = new CustomList<R>();
	NodeType * node = top->next;
	while (node) {
		newList->add(converter(node->value));
		node = node->next;
	}
	return *newList;
}

template<typename T>
T& CustomList<T>::operator[](int index)
{
	CustomList<T>::Node<T> * tmp = top->next;
	if (index >= size)
		throw new std::exception("index out of bound");
	while (index > 0) {
		tmp = tmp->next;
		index--;
	}
	return tmp->value;
}

template<typename T>
CustomList<T> CustomList<T>::where(filter filter)
{
	CustomList<T> tmp;
	CustomList<T>::Node<T> * tmpTop = top->next;
	while (tmpTop) {
		if (filter(tmpTop->value))
			tmp.add(tmpTop->value);
		tmpTop = tmpTop->next;
	}
	return tmp;
}

template<typename T>
void CustomList<T>::quickSort(int left, int right, order order) {
	if (low < high)
	{
		int pi = partition(low, high, order);

		quickSort(low, pi - 1, order);
		quickSort(pi + 1, high, order);
	}
}

template<typename T>
int CustomList<T>::partition(int low, int high, order order)
{
	T pivot = (*this)[high];    // pivot 
	int i = (low - 1);  // Index of smaller element 

	for (int j = low; j <= high - 1; j++)
	{
		if (order((*this)[j], pivot))
		{
			i++;    // increment index of smaller element 
			swap((*this)[i], (*this)[j]);
		}
	}
	swap((*this)[i + 1], (*this)[high]);
	return (i + 1);
}

template<typename T>
void CustomList<T>::removeNode(Node<T> * node, Node<T> * prev)
{
	if (node->next) {
		prev->next = node->next;
		node->next = nullptr;
		delete node;
	}
	else {
		prev->next = nullptr;
		delete node;
	}
	size--;
}

template<typename T>
void CustomList<T>::swap(T & f, T & s)
{
	T tmp = f;
	f = s;
	s = tmp;
}

template<typename T>
CustomList<T> CustomList<T>::orderBy(order order)
{
	CustomList<T> cpy(*this);
	cpy.quickSort(0, cpy.size - 1, order);
	return cpy;
}

template<typename T>
CustomList<T> CustomList<T>::forEach(foreach foreach)
{
	CustomList<T>::Node<T> * topCpy = this->top.next;
	while (topCpy) {
		foreach(topCpy->value);
		topCpy = topCpy->next;
	}
}

template<typename T>
CustomList<T> CustomList<T>::distinct()
{
	CustomList<T> cpy;
	for (size_t i = 0; i < length(); i++) {
		if (!cpy.find((*this)[i]))
			cpy.add((*this)[i]);
	}
	return cpy;
}
