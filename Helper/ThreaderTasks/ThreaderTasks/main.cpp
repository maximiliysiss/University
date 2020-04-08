#include <omp.h>
#include <algorithm>
#include <numeric>
#include <chrono>
#include <iostream>
#include <functional>
#include <mpi.h>

/*сокращение*/
namespace ch = std::chrono;
/*размер*/
const unsigned N = 3000000;
/*минимум для вывода массива в консоль, чтобы не выводить огромыне числа*/
const unsigned MIN = 1000;

/*напечатать массив*/
void printArray(std::string name, int* arr) {
	if (N > MIN)
		return;
	std::cout << name << ": ";
	for (int i = 0; i < N; i++)
		std::cout << arr[i] << " ";
	std::cout << std::endl;
}

/*начать задачу (выполнение алгоритма)*/
void startTask(std::string name, std::function<long long()> code) {
#ifndef MPI
	std::cout << "Start '" << name << "'" << std::endl;
	/*замерим время начала*/
	auto start = ch::high_resolution_clock::now();
#endif // !MPI
	auto result = code();
#ifndef MPI
	/*время конца*/
	auto end = ch::high_resolution_clock::now();
	std::cout << "Result: " << result << std::endl;
	std::cout << "End '" << name << "' Time: " << ch::duration_cast<ch::microseconds>(end - start).count() << " mcs" << std::endl << std::endl;
#endif // !MPI
}

/*создание массива*/
void fillArray(int* arr) {
	/*заполним 0 1 2 3 4 5 6*/
	std::iota(arr, arr + N, 0);
	/*перемешаем*/
	std::random_shuffle(arr, arr + N);
	std::for_each(arr, arr + N, [](int& v) { v *= rand() % 2 ? -1 : 1; });
}

#ifdef LIN

/*линейное сложение*/
long long linearTask(int* vec1, int* vec2) {
	long long result = 0;
	for (int i = 0; i < N; i++)
		result += vec1[i] * vec2[i];
	return result;
}

#endif

#ifdef OMP

/*использование технологии openmp*/
long long ompTask(int* vec1, int* vec2) {
	long long result = 0;

#pragma omp parallel for reduction(+: result)
	for (int i = 0; i < N; i++)
		result += vec1[i] * vec2[i];
	return result;
}

#endif // OMP

#ifdef MPI

long long mpiTask(int* vec1, int* vec2, int* argc, char*** argv) {
	/*создание MP контекста*/
	MPI_Init(argc, argv);
	int size, rank;
	MPI_Comm_size(MPI_COMM_WORLD, &size);
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);

	/*расчитаем индексы для этого потока*/
	unsigned firstIndex = N / size * rank;
	unsigned lastIndex = rank == size - 1 ? N : (N / size) * (rank + 1);

	ch::high_resolution_clock::time_point start;

	/*если первый поток, то в нем создадим все данные и отправим дальше*/
	if (rank == 0) {

		fillArray(vec1);
		fillArray(vec2);

		for (int i = 1; i < size; i++) {
			MPI_Send(vec1, N, MPI_INT, i, 0, MPI_COMM_WORLD);
			MPI_Send(vec2, N, MPI_INT, i, 1, MPI_COMM_WORLD);
		}

		std::cout << "Count: " << N << std::endl;

		printArray("Vec1", vec1);
		printArray("Vec2", vec2);

		std::cout << std::endl;

		std::cout << "Start 'MPI'\n";
		start = ch::high_resolution_clock::now();
	}
	/*если нет, то начитаем свои данные*/
	else {
		MPI_Status state;
		MPI_Recv(vec1, N, MPI_INT, 0, 0, MPI_COMM_WORLD, &state);
		MPI_Recv(vec2, N, MPI_INT, 0, 1, MPI_COMM_WORLD, &state);
	}

	long long result = 0;
	/*расчет своего куска*/
	for (int i = firstIndex; i < lastIndex; i++) {
		result += vec1[i] * vec2[i];
	}

	/*отправим главному потоку все данные*/
	if (rank != 0) {
		MPI_Send(&result, 1, MPI_LONG_LONG, 0, 0, MPI_COMM_WORLD);
	}

	/*главный поток считает все*/
	if (rank == 0) {
		for (int i = 1; i < size; i++) {
			long long tmpRes = 0;
			MPI_Status state;
			MPI_Recv(&tmpRes, 1, MPI_LONG_LONG, i, 0, MPI_COMM_WORLD, &state);
			result += tmpRes;
		}

		auto resTime = ch::duration_cast<ch::microseconds>(ch::high_resolution_clock::now() - start).count();
		std::cout << "Result: " << result << std::endl;
		std::cout << "End 'MPI' Time: " << resTime << " mcs" << std::endl << std::endl;
	}

	MPI_Finalize();


	return result;
}

#endif // MPI

int main(int argc, char** argv) {

	srand((unsigned)time(0));

#ifndef MPI
	std::cout << "Count: " << N << std::endl;
#endif

	int* vec1 = new int[N];
	int* vec2 = new int[N];

#ifndef MPI

	fillArray(vec1);
	fillArray(vec2);

	printArray("Vec1", vec1);
	printArray("Vec2", vec2);

	std::cout << std::endl;
#endif

	/*запускаем задачи*/
#ifdef LIN
	startTask("linear task", std::bind(linearTask, vec1, vec2));
#endif
#ifdef OMP
	startTask("omp task", std::bind(ompTask, vec1, vec2));
#endif // OMP
#ifdef MPI
	startTask("mpi task", std::bind(mpiTask, vec1, vec2, &argc, &argv));
#endif // MPI

	return 0;
}