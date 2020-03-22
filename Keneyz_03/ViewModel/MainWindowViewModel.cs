using System.Windows;
using Keneyz_03.Tools;
using Keneyz_03.Tools.Managers;

namespace Keneyz_03.ViewModel
{
    internal class MainWindowViewModel : BaseViewModel, ILoaderOwner
    {
        #region Fields
        private Visibility _loaderVisibility = Visibility.Hidden;
        private bool _isControlEnabled = true;
        #endregion

        #region Properties
        public Visibility LoaderVisibility
        {
            get => _loaderVisibility;
            set
            {

                _loaderVisibility = value;
                OnPropertyChanged();
            }
        }
        public bool IsControlEnabled
        {
            get => _isControlEnabled;
            set
            {
                _isControlEnabled = value;
                OnPropertyChanged();
            }
        }
        #endregion

        internal MainWindowViewModel()
        {
            LoaderManeger.Instance.Initialize(this);
        }
    }
}
