using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace cpt.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        private ObservableCollection<Employee> _employees;
        public ObservableCollection<Employee> Employees
        {
            get => _employees;
            set => this.RaiseAndSetIfChanged(ref _employees, value);
        }

        private string _newEmployeeFirstName;
        public string NewEmployeeFirstName
        {
            get => _newEmployeeFirstName;
            set => this.RaiseAndSetIfChanged(ref _newEmployeeFirstName, value);
        }

        private string _newEmployeeLastName;
        public string NewEmployeeLastName
        {
            get => _newEmployeeLastName;
            set => this.RaiseAndSetIfChanged(ref _newEmployeeLastName, value);
        }

        private ReactiveCommand<Unit, Unit> _addEmployeeCommand;
        public ReactiveCommand<Unit, Unit> AddEmployeeCommand =>
            _addEmployeeCommand ??= ReactiveCommand.Create(AddEmployee);

        

        private void AddEmployee()
        {

            using (var context = new AppDbContext())
            {
                var newEmployee = new Employee
                {
                    FirstName = NewEmployeeFirstName,
                    LastName = NewEmployeeLastName
                };

                context.Employees.Add(newEmployee);
                context.SaveChanges();

                Employees.Add(newEmployee);
            }


            NewEmployeeFirstName = "";
            NewEmployeeLastName = "";
        }
    }
}




