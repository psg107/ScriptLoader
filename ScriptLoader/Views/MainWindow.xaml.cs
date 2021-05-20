using MahApps.Metro.Controls;
using System;
using System.Windows.Interop;

namespace ScriptLoader
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            SearchTextBox.Focus();
        }
    }
}
