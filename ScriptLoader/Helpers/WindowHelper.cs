using Prism.Mvvm;
using System.Windows;

namespace ScriptLoader.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class WindowHelper
    {
        public static Window GetWindowFromBindableObject(BindableBase bindableBase)
        {
            Window owner = null;

            if (bindableBase != null)
            {
                foreach (Window win in App.Current.Windows)
                {
                    if (win.DataContext == bindableBase)
                    {
                        owner = win;
                        break;
                    }
                }
            }

            return owner;
        }
    }
}
