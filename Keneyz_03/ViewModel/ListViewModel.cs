using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Keneyz_03.Model;
using Keneyz_03.Tools;
using Keneyz_03.Tools.Managers;
using Keneyz_03.Tools.Navigation;

namespace Keneyz_03.ViewModel
{
    internal class ListViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public ListViewModel()
        {
            StationManager.DataVM = this;
        }

        #region Fields

        private readonly List<Person> _personList = StationManager.DataStorage.PersonsList;
        private readonly string[] _sortByList = { "Name", "Surname", "Email", "Birthday", "Western", "Chinese" };
        private readonly string[] _filterByList = { "Name", "Surname", "Email", "Western", "Chinese" };

        private RelayCommand<object> _addPersonCommand;
        private RelayCommand<object> _editPersonCommand;
        private RelayCommand<object> _refreshCommand;
        private RelayCommand<object> _removePersonCommand;
        private RelayCommand<object> _filterCommand;

        private int _sortByIndex = 0;
        private int _filterByIndex = 0;

        #endregion

        #region Properties

        public string FilterQuery { get; set; }

        public int SelectedSortByIndex
        {
            get => _sortByIndex;
            set
            {
                _sortByIndex = value;
                OnPropertyChanged(nameof(PersonList));
            }
        }

        public int SelectedFilterByIndex
        {
            get => _filterByIndex;
            set
            {
                _filterByIndex = value;
                OnPropertyChanged(nameof(PersonList));
            }
        }


        public object SelectedPerson { get; set; }

        public IEnumerable<Person> PersonList
        {
            get
            {
                IEnumerable<Person> l = _personList;

                switch (SelectedSortByIndex)
                {
                    case 0:
                        l = l.OrderBy(x => x.Name);
                        break;
                    case 1:
                        l = l.OrderBy(x => x.Surname);
                        break;
                    case 2:
                        l = l.OrderBy(x => x.Email);
                        break;
                    case 3:
                        l = l.OrderBy(x => x.Birthday);
                        break;
                    case 4:
                        l = l.OrderBy(x => x.WesternSign);
                        break;
                    case 5:
                        l = l.OrderBy(x => x.ChineseSign);
                        break;
                }

                if (String.IsNullOrWhiteSpace(FilterQuery))
                    return l;

                switch (SelectedFilterByIndex)
                {
                    case 0:
                        l = l.Where(x => x.Name.Contains(FilterQuery));
                        break;
                    case 1:
                        l = l.Where(x => x.Surname.Contains(FilterQuery));
                        break;
                    case 2:
                        l = l.Where(x => x.Email.Contains(FilterQuery));
                        break;
                    case 3:
                        l = l.Where(x => x.WesternSign.Contains(FilterQuery));
                        break;
                    case 4:

                        l = l.Where(x => x.ChineseSign.Contains(FilterQuery));

                        break;
                }

                return l;
            }
        }

        public IEnumerable<string> SortByList => _sortByList;

        public IEnumerable<string> FilterByList => _filterByList;

        public RelayCommand<object> AddPersonCommand
        {
            get
            {
                return _addPersonCommand ??= new RelayCommand<object>(
                    AddPersonImplementation);
            }
        }

        public RelayCommand<object> EditPersonCommand
        {
            get
            {
                return _editPersonCommand ??= new RelayCommand<object>(EditPersonImplementation, CanExecuteRemoveOrEdit);
            }
        }

        public RelayCommand<object> RefreshCommand
        {
            get
            {
                return _refreshCommand ??= new RelayCommand<object>(
                    (o => { OnPropertyChanged(nameof(PersonList)); }));
            }
        }

        public RelayCommand<object> RemovePersonCommand
        {
            get
            {
                return _removePersonCommand ??= new RelayCommand<object>(RemovePersonImplementation, CanExecuteRemoveOrEdit);
            }
        }

        public RelayCommand<object> FilterCommand
        {
            get
            {
                return _filterCommand ??= new RelayCommand<object>(
                    (o => { OnPropertyChanged(nameof(PersonList)); }));
            }
        }

        #endregion

        private void AddPersonImplementation(object obj)
        {
            StationManager.CurrentPerson = new Person("", "", "");
            NavigationManager.Instance.Navigate(ViewType.AddView);
        }

        private async void RemovePersonImplementation(object obj)
        {
            LoaderManeger.Instance.ShowLoader();
            if (SelectedPerson == null)
            {
                MessageBox.Show(
                    "Select someone first, please"
                );
                LoaderManeger.Instance.HideLoader();
                return;
            }

            await Task.Run(() =>
            {
                Person personToRemove = (Person)SelectedPerson;

                {
                    StationManager.DataStorage.RemovePerson(personToRemove);
                    OnPropertyChanged(nameof(PersonList));
                }
            });

            LoaderManeger.Instance.HideLoader();
        }

        private async void EditPersonImplementation(object obj)
        {
            LoaderManeger.Instance.ShowLoader();
            if (SelectedPerson == null)
            {
                MessageBox.Show(
                    "Select someone first, please!"
                );
                LoaderManeger.Instance.HideLoader();
                return;
            }

            await Task.Run(() =>
            {
                StationManager.CurrentPerson = (Person)SelectedPerson;

                StationManager.TestPerson = new Person(
                    StationManager.CurrentPerson.Name,
                    StationManager.CurrentPerson.Surname,
                    StationManager.CurrentPerson.Email,
                    StationManager.CurrentPerson.Birthday
                );
            });

            LoaderManeger.Instance.HideLoader();

            if (StationManager.EditVM != null)
            {
                StationManager.EditVM.UpdateAll();
            }

            NavigationManager.Instance.Navigate(ViewType.EditView);
        }

        private bool CanExecuteRemoveOrEdit(object obj)
        {
            return SelectedPerson != null;
        }

        public void UpdateInfo()
        {
            OnPropertyChanged(nameof(PersonList));
        }
    }
}
