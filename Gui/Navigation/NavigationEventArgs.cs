using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gui.Navigation {
    public enum NavigationObjectTypes {
        CalendarYear,
        Block,
        Rotation
    }

    public class NavigationObject {
        public NavigationObjectTypes NavigationObjectType { get; set; }
        public int Id { get; set; }

        public NavigationObject() { }
        public NavigationObject(NavigationObjectTypes navigationObjectType, int id) {
            NavigationObjectType = navigationObjectType;
            Id = id;
        }
    }

    public class NavigationEventArgs {
        public NavigationObject FromNavigationObject { get; set; }
        public NavigationObject ToNavigationObject { get; set; }

        public NavigationEventArgs() { }
        public NavigationEventArgs(NavigationObject fromNavigationObject, NavigationObject toNavigationObject) {
            FromNavigationObject = fromNavigationObject;
            ToNavigationObject = toNavigationObject;
        }
    }

    public delegate void NavigationEventHandler(object sender, NavigationEventArgs args);
}
