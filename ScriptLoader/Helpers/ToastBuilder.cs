using System;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;

namespace ScriptLoader.Helpers
{
    public class ToastBuilder
    {
        public bool TopMost { get; set; } = false;

        /// <summary>
        /// 단위 (초)
        /// </summary>
        public double LifeTime { get; set; } = 1;

        /// <summary>
        /// 
        /// </summary>
        public double Width { get; set; } = 500;

        /// <summary>
        /// 
        /// </summary>
        public Window ParentWindow { get; set; } = Application.Current.MainWindow;

        public Notifier BuildNotifier()
        {
            return new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: this.ParentWindow,
                    corner: Corner.BottomCenter,
                    offsetX: 0,
                    offsetY: 0);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(LifeTime),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                cfg.DisplayOptions.TopMost = this.TopMost;
                cfg.DisplayOptions.Width = Width;

                cfg.Dispatcher = Application.Current.Dispatcher;
            });
        }
    }
}
