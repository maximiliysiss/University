#pragma once
#include "Department.h"
#include "Worker.h"
#include "Repositories.h"
#include "WorkerViewModel.h"

namespace BankProject::ViewModels {

	ref class DirectorViewModel {
	private:
		Data::IDepartmentRepository* depRepo;
		Data::IUserRepository* userRepo;

		System::Collections::Generic::List<DepartmentViewModel^>^ depoViewModels;
		System::Collections::Generic::List<WorkerViewModel^>^ workerViewModels;

		void reloadData() {
			depoViewModels = gcnew System::Collections::Generic::List<DepartmentViewModel^>();
			workerViewModels = gcnew System::Collections::Generic::List<WorkerViewModel^>();

			for (auto dep : depRepo->select()) {
				depoViewModels->Add(gcnew DepartmentViewModel(dep, depRepo));
			}

			for (auto w : userRepo->selectWorkers()) {
				workerViewModels->Add(gcnew WorkerViewModel(w, userRepo));
			}

			onReload();
		}

		set_property(System::Action^, onReload);

		void openDeparment(DepartmentViewModel^ vm) {
			DepartmentForm^ form = gcnew DepartmentForm(vm);
			form->ShowDialog();
		}

		void openWorker(WorkerViewModel^ vm) {
			Worker^ worker = gcnew Worker(vm);
			worker->ShowDialog();
		}

	public:

		DirectorViewModel(Data::IDepartmentRepository* depRepo, Data::IUserRepository* userRepo)
			: depRepo(depRepo), userRepo(userRepo) {}

		void onAddNewDepartment() {
			openDeparment(gcnew DepartmentViewModel(new Department(), depRepo));
			reloadData();
		}

		void onOpenDepartment(int index) {
			openDeparment(depoViewModels[index]);
			reloadData();
		}

		void onAddNewWorker() {
			User* worker = UserFactory::createWorker();
			openWorker(gcnew WorkerViewModel(worker, userRepo));
			reloadData();
		}

		void onOpenWorker(int index) {
			openWorker(workerViewModels[index]);
			reloadData();
		}

		System::Collections::Generic::List<DepartmentViewModel^>^ GetDepRepos() {
			return depoViewModels;
		}

		System::Collections::Generic::List<WorkerViewModel^>^ GetWorkerRepos() {
			return workerViewModels;
		}

		void load() {
			reloadData();
		}

	};

}