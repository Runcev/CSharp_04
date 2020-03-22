using System.Windows.Controls;
using Keneyz_03.Tools.Navigation;
using Keneyz_03.ViewModel;

namespace Keneyz_03.View
{
    /// <summary>
    /// Interaction logic for AddPersonView.xaml
    /// </summary>
    public partial class AddView : UserControl, INavigatable
    {
        public AddView()
        {
            InitializeComponent();
            DataContext = new AddViewModel();
        }
    }
}