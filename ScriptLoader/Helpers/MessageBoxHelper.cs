using Prism.Mvvm;
using System.Windows;

namespace ScriptLoader.Helpers
{
    public static class MessageBoxHelper
    {
        private const string CAPTION = "알림";

        public static void ShowMessage(string message, BindableBase dataContext)
        {
            Window owner = null;

            if (dataContext != null)
            {
                foreach (Window win in App.Current.Windows)
                {
                    if (win.DataContext == dataContext)
                    {
                        owner = win;
                        break;
                    }
                }
            }

            MessageBox.Show(owner, message, CAPTION);
        }

        public static bool ShowQuestionMessage(string message, BindableBase dataContext)
        {
            Window owner = null;

            if (dataContext != null)
            {
                foreach (Window win in App.Current.Windows)
                {
                    if (win.DataContext == dataContext)
                    {
                        owner = win;
                        break;
                    }
                }
            }

            var result = MessageBox.Show(owner, message, CAPTION, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                return true;
            }
            return false;
        }
    }
}
