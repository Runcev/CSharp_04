using System.Windows.Controls;
using Keneyz_03.Tools.Managers;
using Keneyz_03.Tools.Navigation;
using Keneyz_03.ViewModel;

namespace Keneyz_03.View
{
    /// <summary>
    /// Interaction logic for ListView.xaml
    /// </summary>
    public partial class ListView : UserControl, INavigatable
    {
        
        public ListView()
        {
            InitializeComponent();
            DataContext = new ListViewModel();
            StationManager.PersonTable = DGname;
        }
    }
}
