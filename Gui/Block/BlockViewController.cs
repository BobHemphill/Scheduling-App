using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceLayer.Messages;
using System.Windows.Controls;
using System.Windows.Media;
using Gui.Navigation;
using Gui.Rotation;
using System.Windows.Input;

namespace Gui.Block {
    public class BlockViewController : NavigationController<RotationItemView> {
        readonly BlockMessage DataContext;
        readonly BlockView View;

        public BlockViewController(BlockMessage dataContext, BlockView view) {
            DataContext = dataContext;
            View = view;
            View.SetDataContext(DataContext);
        }

        public override void PopulateView() {
            AttachParentNavigation(View.Parent);
            foreach (var rotation in DataContext.Rotations) {
                var child = CreateChildElement();
                child.DataContext = rotation;
                View.RotationPanel.Children.Add(child);
            }
        }

        protected override NavigationEventArgs GetParentNavigationEventArgs(object sender, MouseButtonEventArgs args) {
            return new NavigationEventArgs(null, new NavigationObject(NavigationObjectTypes.CalendarYear, 1));
        }

        protected override NavigationEventArgs GetChildNavigationEventArgs(object sender, MouseButtonEventArgs args) {
            return new NavigationEventArgs(null, new NavigationObject(NavigationObjectTypes.Rotation, 1));
        }
    }
}
