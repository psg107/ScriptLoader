using Prism.Mvvm;
using System.Windows;

namespace ScriptLoader.Helpers
{
    public class MessageBoxHelper
    {
        private const string CAPTION = "알림";
        private readonly BindableBase dataContext;

        public MessageBoxHelper(BindableBase dataContext)
        {
            this.dataContext = dataContext;
        }

        public void ShowMessage(string message)
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

        public bool ShowQuestionMessage(string message)
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
