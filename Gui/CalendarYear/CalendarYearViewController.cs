using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceLayer.Messages;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using Gui.Navigation;
using Gui.Block;
using System.Windows.Input;

namespace Gui.CalendarYear {
    public class CalendarYearViewController : NavigationController<BlockItemView> {
        readonly CalendarYearMessage DataContext;
        readonly CalendarYearView View;

        public CalendarYearViewController(CalendarYearMessage dataContext, CalendarYearView view) {
            DataContext = dataContext;
            View = view;
            View.SetDataContext(DataContext);
        }

        public override void PopulateView() {
            foreach (var block in DataContext.Blocks) {
                var child = CreateChildElement();
                child.DataContext = block;
                View.BlockPanel.Children.Add(child);
            }
        }

        protected override NavigationEventArgs GetParentNavigationEventArgs(object sender, MouseButtonEventArgs args) {
            throw new NotImplementedException();
        }

        protected override NavigationEventArgs GetChildNavigationEventArgs(object sender, MouseButtonEventArgs args) {
            return new NavigationEventArgs(null, new NavigationObject(NavigationObjectTypes.Block, 1));
        }
    }
}
