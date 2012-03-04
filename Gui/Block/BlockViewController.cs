using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceLayer.Messages;
using System.Windows.Controls;
using System.Windows.Media;

namespace Gui.Block {
    public class BlockViewController {
        readonly BlockMessage DataContext;
        readonly BlockView View;

        public BlockViewController(BlockMessage dataContext, BlockView view) {
            DataContext = dataContext;
            View = view;
        }

        public void PopulateView() {
            foreach (var rotation in DataContext.Rotations) {
                var textBlock = new TextBlock() { Text = rotation.Name };
                var border = new Border() { Background = new SolidColorBrush(Colors.AliceBlue), Margin = new System.Windows.Thickness(5) };
                border.Child = textBlock;

                View.RotationPanel.Children.Add(border);
            }
        }
    }
}
