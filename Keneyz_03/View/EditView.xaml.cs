using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Keneyz_03.Tools.Navigation;
using Keneyz_03.ViewModel;

namespace Keneyz_03.View
{
    /// <summary>
    /// Interaction logic for EditView.xaml
    /// </summary>
    public partial class EditView : UserControl, INavigatable
    {
        public EditView()
        {
            InitializeComponent();
            DataContext = new EditViewModel();
        }
    }
}
