namespace Keneyz_03.Tools.Navigation
{
    internal enum ViewType
    {
        ListView,
        AddView,
        EditView
    }

    interface INavigationModel
    {
        void Navigate(ViewType viewType);
    }
}