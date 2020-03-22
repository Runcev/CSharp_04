using System.Windows;
using System.Windows.Controls;
using Keneyz_03.Tools.DataStorage;
using Keneyz_03.Tools.Managers;
using Keneyz_03.Tools.Navigation;
using Keneyz_03.ViewModel;

namespace Keneyz_03
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IContentOwner
    {
        public ContentControl ContentControl => _contentControl;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();

            StationManager.Initialize(new SerializedDataStorage());
            NavigationManager.Instance.Initialize(new InitializationNavigationModel(this));
            NavigationManager.Instance.Navigate(ViewType.ListView);

        }
    }
}
