using System;
using Keneyz_03.View;

namespace Keneyz_03.Tools.Navigation
{
    internal class InitializationNavigationModel : BaseNavigationModel
    {
        public InitializationNavigationModel(IContentOwner contentOwner) : base(contentOwner)
        {

        }
        protected override void InitializeView(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.ListView:
                    ViewsDictionary.Add(viewType, new ListView());
                    break;
                case ViewType.AddView:
                    ViewsDictionary.Add(viewType, new AddView());
                    break;
                case ViewType.EditView:
                    ViewsDictionary.Add(viewType, new EditView());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }
    }
}