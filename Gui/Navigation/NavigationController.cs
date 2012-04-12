using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Gui.Navigation {
    public interface INavigationController {
        event NavigationEventHandler Navigate;
        void PopulateView();
        void UnLoad();
    }

    public abstract class NavigationController<T> : INavigationController where T : FrameworkElement, new() {
        public event NavigationEventHandler Navigate;

        List<FrameworkElement> childNavigationElements = new List<FrameworkElement>();
        List<FrameworkElement> parentNavigationElements = new List<FrameworkElement>();

        void FireNavigate(NavigationEventArgs args){
            if(Navigate!=null) Navigate(this, args);
        }
        protected abstract NavigationEventArgs GetChildNavigationEventArgs(object sender, MouseButtonEventArgs args);
        protected abstract NavigationEventArgs GetParentNavigationEventArgs(object sender, MouseButtonEventArgs args);

        protected void ParentElement_MouseUp(object sender, MouseButtonEventArgs e) {
            FireNavigate(GetParentNavigationEventArgs(sender, e));
        }

        void childElement_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            FireNavigate(GetChildNavigationEventArgs(sender, e));
        }

        protected T CreateChildElement() {
            var child = new T();
            child.MouseUp += new System.Windows.Input.MouseButtonEventHandler(childElement_MouseUp);
            childNavigationElements.Add(child);
            return child;
        }

        protected void AttachParentNavigation(FrameworkElement parent) {
            parentNavigationElements.Add(parent);
            parent.MouseUp += new MouseButtonEventHandler(ParentElement_MouseUp);
        }

        public abstract void PopulateView();
        public void UnLoad() {
            foreach (var child in childNavigationElements) {
                child.MouseUp -= new System.Windows.Input.MouseButtonEventHandler(childElement_MouseUp);
            }
            childNavigationElements.Clear();
            foreach (var parent in parentNavigationElements) {
                parent.MouseUp -= new System.Windows.Input.MouseButtonEventHandler(ParentElement_MouseUp);
            }
            parentNavigationElements.Clear();
        }
    }
}
