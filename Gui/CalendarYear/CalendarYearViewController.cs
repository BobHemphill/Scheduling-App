using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceLayer.Messages;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using Gui.Navigation;

namespace Gui.CalendarYear {
    public class CalendarYearViewController : NavigationController {
        readonly CalendarYearMessage DataContext;
        readonly CalendarYearView View;

        public CalendarYearViewController(CalendarYearMessage dataContext, CalendarYearView view) {
            DataContext = dataContext;
            View = view;
        }

        public void PopulateView() {
            foreach (var block in DataContext.Blocks) {
                var textBlock = new TextBlock() { Text = block.Name };
                var border = new Border() { Background = new SolidColorBrush(Colors.AliceBlue), Margin = new System.Windows.Thickness(5) };
                border.Child = textBlock;
                border.MouseUp += new System.Windows.Input.MouseButtonEventHandler(border_MouseUp);
                View.BlockPanel.Children.Add(border);
            }
        }

        void border_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            FireNavigate(GetNavigationEventArgs());
        }

        protected override NavigationEventArgs GetNavigationEventArgs() {
            return new NavigationEventArgs(null, new NavigationObject(NavigationObjectTypes.Block, 1));
        }
    }
}
