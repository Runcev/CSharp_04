using System;
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
    class EditViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public EditViewModel()
        {
            StationManager.EditVM = this;
        }
        #region Fields

        private Person _person = StationManager.CurrentPerson;
        private Person _testPerson = StationManager.TestPerson;

        private RelayCommand<object> _confirmCommand;
        private RelayCommand<object> _cancelCommand;
        #endregion

        #region Properties
        public Person TestPerson
        {
            get => _testPerson;
            set
            {
                _testPerson = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<Object> ConfirmCommand
        {
            get
            {
                return _confirmCommand ??= new RelayCommand<object>(
                    ConfirmImplementation, CanExecuteProceed);
            }
        }

        public RelayCommand<Object> CancelCommand
        {
            get
            {
                return _cancelCommand ??= new RelayCommand<object>(
                    CancelImplementation);
            }
        }

        #endregion

        private async void ConfirmImplementation(object obj)
        {
            LoaderManeger.Instance.ShowLoader();
            var res = await Task.Run(() => {
                try
                {
                    _testPerson.Validate();
                }
                catch (NotBornException e)
                {
                    MessageBox.Show($"Error: {e.Message}");
                    return false;
                }
                catch (TooOldException e)
                {
                    MessageBox.Show($"Error: {e.Message}");
                    return false;
                }
                catch (InvalidEmailExceptions e)
                {
                    MessageBox.Show($"Error: {e.Message}");
                    return false;
                }
                return true;
            });
            LoaderManeger.Instance.HideLoader();
            if (res)
            {
                _person.Name = _testPerson.Name;
                _person.Surname = _testPerson.Surname;
                _person.Email = _testPerson.Email;
                _person.Birthday = _testPerson.Birthday;
                StationManager.TestPerson = null;
                StationManager.DataStorage.ApplyChanges();
                StationManager.DataVM.UpdateInfo();
                NavigationManager.Instance.Navigate(ViewType.ListView);
            }
        }

        private bool CanExecuteProceed(Object obj)
        {
            return !string.IsNullOrWhiteSpace(TestPerson.Email) && !string.IsNullOrWhiteSpace(TestPerson.Name) && !string.IsNullOrWhiteSpace(TestPerson.Surname);
        }

        private void CancelImplementation(object obj)
        {
            //   StationManager.TestPerson = null;
            NavigationManager.Instance.Navigate(ViewType.ListView);

        }

        public void UpdateAll()
        {
            TestPerson = StationManager.TestPerson;
            _person = StationManager.CurrentPerson;
            OnPropertyChanged(nameof(TestPerson));
        }
    }
}
