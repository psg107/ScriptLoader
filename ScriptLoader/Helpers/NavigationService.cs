using Prism.Mvvm;
using System.Windows;

namespace ScriptLoader.Helpers
{
    public class NavigationService
    {
        private readonly BindableBase dataContext;

        public NavigationService(BindableBase dataContext)
        {
            this.dataContext = dataContext;
        }

        public void OpenDialogWindow<T>() where T : Window, new()
        {
            var owner = WindowHelper.GetWindowFromBindableObject(this.dataContext);

            Window win = new T();
            win.Owner = owner;
            win.ShowDialog();
        }
    }
}
