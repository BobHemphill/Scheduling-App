﻿using System;
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

namespace Gui.CalendarYear {
    /// <summary>
    /// Interaction logic for CalendarYearView.xaml
    /// </summary>
    public partial class CalendarYearView : UserControl {
        public CalendarYearView() {
            InitializeComponent();
        }

        public void SetDataContext(object dc) {
            RootGrid.DataContext = dc;
        }
    }
}
