using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ScriptLoader.AttachedProperties
{
    /// <summary>
    /// 팝업윈도우 ESC로 종료 기능 추가
    /// </summary>
    public class CloseWindowWithESCButtonAttachedProperty
    {
        public static bool GetCanClose(DependencyObject obj)
        {
            return (bool)obj.GetValue(CanCloseProperty);
        }

        public static void SetCanClose(DependencyObject obj, bool value)
        {
            obj.SetValue(CanCloseProperty, value);
        }

        public static readonly DependencyProperty CanCloseProperty =
            DependencyProperty.RegisterAttached(
                name: "CanClose", 
                propertyType: typeof(bool), 
                ownerType: typeof(Window), 
                defaultMetadata: new PropertyMetadata(CanCloseChanged));

        private static void CanCloseChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window)
            {
                window.PreviewKeyDown += (s, ev) =>
                {
                    if (ev.Key == System.Windows.Input.Key.Escape)
                    {
                        window.Close();
                        window.Owner?.Focus();
                    }
                };
            }
        }
    }
}
