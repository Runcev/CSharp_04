using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using Keneyz_03.Model;
using Keneyz_03.Tools;
using Keneyz_03.Tools.Exceptions;
using Keneyz_03.Tools.Managers;
using Keneyz_03.Tools.Navigation;

namespace Keneyz_03.ViewModel
{
    class AddViewModel : BaseViewModel, INotifyPropertyChanged
    {
        #region Fields

        private Person _person = StationManager.CurrentPerson;

        private RelayCommand<object> _proceedCommand;
        private RelayCommand<object> _cancelCommand;
        #endregion

        public AddViewModel()
        {
        }

        #region Properties
        public Person MyPerson
        {
            get { return _person; }
            set
            {
                _person = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<Object> ProceedCommand
        {
            get
            {
                return _proceedCommand ?? (_proceedCommand = new RelayCommand<object>(
                           ProceedImplementation, CanExecuteProceed));
            }
        }

        public RelayCommand<Object> CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new RelayCommand<object>(
                           CancelImplementation));
            }
        }

        #endregion

        private async void ProceedImplementation(object obj)
        {
            LoaderManeger.Instance.ShowLoader();
            bool res = await Task.Run(() => {
                try
                {
                    _person.Validate();
                }
                catch (NotBornException e)
                {
                    MessageBox.Show($"Mistake with age: {e.Message}");
                    return false;
                }
                catch (TooOldException e)
                {
                    MessageBox.Show($"Mistake with age: {e.Message}");
                    return false;
                }
                catch (InvalidEmailExceptions e)
                {
                    MessageBox.Show($"Mistake with email: {e.Message}");
                    return false;
                }
                return true;
            });
            LoaderManeger.Instance.HideLoader();
            if (res)
            {
                StationManager.DataStorage.AddPerson(_person);
                _person = new Person("", "", "");
                MyPerson = _person;
            }
        }

        private bool CanExecuteProceed(Object obj)
        {
            return !String.IsNullOrWhiteSpace(MyPerson.Email) && !String.IsNullOrWhiteSpace(MyPerson.Name) && !String.IsNullOrWhiteSpace(MyPerson.Surname);
        }

        private void CancelImplementation(object obj)
        {
            StationManager.DataVM.UpdateInfo();
            NavigationManager.Instance.Navigate(ViewType.ListView);
        }
    }
}
