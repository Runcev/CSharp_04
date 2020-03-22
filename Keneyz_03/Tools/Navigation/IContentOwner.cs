using System.Windows.Controls;

namespace Keneyz_03.Tools.Navigation
{
    internal interface IContentOwner
    {
        ContentControl ContentControl { get; }
    }
}