#pragma once
#include <exception>
#include <iostream>
#include "CustomFunctor.h"

/*Вмещающий класс*/
template<typename T>
class CustomList
{
	/*Определеение типа указателя на фукнцию типа bool name(T& elem)*/
	typedef bool(*filter)(T& elem);
	/*Определение типа указател на функицию тпиа bool name(const T& f, const T&)*/
	typedef bool(*order)(T& first, T& two);
	/*Определение типа указателя на функцию типа void name(T& elem)*/
	typedef void(foreach)(T& elem);
	/*Просто добавление типа аля как */
	typedef order comparer;

	/*Узел списка*/
	template<typename T>
	struct Node {
		/*Пустой конструктор*/
		Node() {
			this->next = nullptr;
		}
		/*Коснтурктор с данными*/
		Node(T value) {
			this->next = nullptr;
			this->value = value;
		}
		/*Конструктор, который скопирует все изнутри и дальше*/
		Node(const Node<T>& node) {
			this->value = node.value;
			this->next = nullptr;
			/*если есть информация о следующем элементе, то надо копировать*/
			if (node.next) {
				Node<T> & tmp = *(node.next);
				this->next = new Node<T>(tmp);
			}
		}
		/*указатель на следующий*/
		Node * next;
		/*значение*/
		T value;
		/*деструктор*/
		~Node() {
			if (next)
				delete next;
		}
	};

	/*указатель на первый элемент*/
	Node<T> * top;
	/*размер*/
	size_t size{ 0 };
	/*быстрая сортировка по алгоритму QSort*/
	void quickSort(int left, int right, order order);
	template<typename T1, typename T2>
	void quickSort(int left, int right, BiDirectalFunctor<T1,T2> * order);
	int partition(int low, int high, order order);
	template<typename T1, typename T2>
	int partition(int low, int high, BiDirectalFunctor<T1,T2> * order);
	/*удалить элемент*/
	void removeNode(Node<T> * node, Node<T> * prev);
	void swap(T& f, T& s);
public:
	/*определение внутреннего типа узла*/
	typedef CustomList<T>::Node<T> NodeType;
	/*конструктор*/
	CustomList();
	/*копирующий конструктор*/
	CustomList(const CustomList<T>& list);
	/*деструктор*/
	~CustomList();
	/*добавить элемент*/
	void add(T elem);
	/*доабвить элемент по индексу*/
	void add(size_t index, T elem);
	/*удалить*/
	void remove(T elem);
	/*удалить по индексу*/
	void remove(size_t index);
	/*удалить все*/
	void removeAll(T elem);
	/*поиск*/
	T * find(T elem);
	/*обновить*/
	void update(T elem);
	/*обновить, используя функцию сравниватель*/
	void update(T elem, comparer filter);
	/*размер*/
	size_t length();
	/*вывод*/
	friend std::ostream  & operator<<(std::ostream& out, CustomList<T> list);
	/*перегрузка опертора скобок для индекса*/
	T& operator[](int index);
	/*операция где*/
	CustomList<T> where(filter filter);
	template<typename T1, typename T2>
	CustomList<T> where(CustomFunctor<T1, T2>* functor);
	/*операция сортировки по условию*/
	CustomList<T> orderBy(order order);
	template<typename T1, typename T2>
	CustomList<T> orderBy(BiDirectalFunctor<T1, T2> * order);
	/*операция для каждого*/
	CustomList<T> forEach(foreach foreach);
	/*убрать все повторения с применение операции ==*/
	CustomList<T> distinct();
	/*превраитить внутренние данные в новые, тип, перегнать int -> string*/
	template<typename R>
	CustomList<R> convert(R(*converter)(T& elem));
};
