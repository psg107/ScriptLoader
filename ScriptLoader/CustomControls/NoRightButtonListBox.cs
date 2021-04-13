using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ScriptLoader.CustomControls
{
    public class NoRightButtonListBox : ListBox
    {
        public NoRightButtonListBox()
        {
            PreviewMouseRightButtonDown += LeftButtonListBox_PreviewMouseRightButtonDown;
        }

        private void LeftButtonListBox_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ListBoxItemNoRightClickSelect();
        }
    }

    public class ListBoxItemNoRightClickSelect : ListBoxItem
    {
        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
        }
    }
}
