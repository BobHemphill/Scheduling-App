using System;
using System.Collections.Generic;
using System.Linq;
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
using ServiceLayer;
using Gui.CalendarYear;
using Gui.Block;
using Gui.Navigation;

namespace Gui {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        INavigationController currentController;

        public MainWindow() {
            InitializeComponent();
            DisplayCalendar();
        }

        void DisplayCalendar() {
            var view = new CalendarYearView();
            this.Content = view;
            var calendarYear = MessageFactory.CreateCalendarYearMessage();
            var controller = new CalendarYearViewController(calendarYear, view);
            controller.Navigate += new Navigation.NavigationEventHandler(controller_Navigate);
            currentController = controller;
            controller.PopulateView();
        }

        void DispalyBlock() {
            var view = new BlockView();
            this.Content = view;
            var block = MessageFactory.CreateBlockMessage();
            var controller = new BlockViewController(block, view);
            controller.Navigate += new Navigation.NavigationEventHandler(controller_Navigate);
            currentController = controller;
            controller.PopulateView();    
        }

        void controller_Navigate(object sender, Navigation.NavigationEventArgs args) {
            currentController.Navigate -= new Navigation.NavigationEventHandler(controller_Navigate);
            currentController.UnLoad();
            switch (args.ToNavigationObject.NavigationObjectType) {
                case Navigation.NavigationObjectTypes.CalendarYear:
                    DisplayCalendar();
                    break;
                case Navigation.NavigationObjectTypes.Block:
                    DispalyBlock();
                    break;
            }
        }
    }
}
