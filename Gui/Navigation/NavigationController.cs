using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gui.Navigation {
    public abstract class NavigationController {
        public event NavigationEventHandler Navigate;  

        protected void FireNavigate(NavigationEventArgs args){
            if(Navigate!=null) Navigate(this, args);
        }
        protected abstract NavigationEventArgs GetNavigationEventArgs();
    }
}
