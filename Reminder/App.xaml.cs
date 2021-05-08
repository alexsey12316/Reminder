using Reminder.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Reminder
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private System.Windows.Forms.NotifyIcon notifyIcon = new System.Windows.Forms.NotifyIcon();
        private MainViewModel viewModel = new MainViewModel();


        protected override void OnStartup(StartupEventArgs ev)
        {
            base.OnStartup(ev);

            notifyIcon.Icon = Reminder.Properties.Resources.notifyIcon;
            notifyIcon.DoubleClick += (s, e) => ShowWindow();
            notifyIcon.Visible = true;
            notifyIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            notifyIcon.ContextMenuStrip.Items.Add("Open").Click += (s, e) => ShowWindow();
            notifyIcon.ContextMenuStrip.Items.Add("Exit").Click += (s, e) => ExitApp();
            //ShowWindow(); //TODO remove
        }


        private void ShowWindow()
        {
            if (MainWindow == null)
                MainWindow = new MainWindow()
                {
                    DataContext = viewModel
                };
            MainWindow.Show();
            MainWindow.Activate();
        }

        private void ExitApp()
        {
            MainWindow?.Close();
            Application.Current.Shutdown();
            notifyIcon.Dispose();
        }
    }
}
