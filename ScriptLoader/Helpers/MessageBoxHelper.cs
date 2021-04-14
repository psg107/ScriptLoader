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
            var owner = WindowHelper.GetWindowFromBindableObject(this.dataContext);

            MessageBox.Show(owner, message, CAPTION);
        }

        public bool ShowQuestionMessage(string message)
        {
            var owner = WindowHelper.GetWindowFromBindableObject(this.dataContext);

            var result = MessageBox.Show(owner, message, CAPTION, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                return true;
            }
            return false;
        }
    }
}
