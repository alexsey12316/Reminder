using MVVM;
using MyMVVM;
using MyMVVM.Commands;
using Notifications.Wpf;
using Reminder.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Reminder.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private static string NOWIFI = "none";

        private TimerModel timer = new TimerModel();

        private NotificationManager notificationManager = new NotificationManager();

        private string currentWifi;

        public string CurrentWifi
        {
            get => currentWifi != null ? currentWifi : NOWIFI;
            private set => currentWifi = value;
        }

        public string AddWifi { get; set; }

        public DateTime Interval { get; set; } = new DateTime(1, 1, 1, 12, 34, 15);
        public DateTime Start { get; set; } 
        public DateTime End { get; set; } 
        public TimeSpan TimeLeft { get; private set; } = new TimeSpan(1, 59, 59);
        public ObservableCollection<string> WifiNames { get; private set; }

        #region Commands
        private ICommand moveCurrentWifiCommand;
        public ICommand MoveCurrentWifiCommand
        {
            get => moveCurrentWifiCommand ?? (moveCurrentWifiCommand =
                new SimpleCommand(() =>
                {
                    if (CurrentWifi != NOWIFI)
                        AddWifi = CurrentWifi;
                }));
        }

        private ICommand addWifiCommand;
        public ICommand AddWifiCommand
        {
            get => addWifiCommand ?? (addWifiCommand =
                new SimpleCommand(() =>
                {
                    if (AddWifi != string.Empty && AddWifi != NOWIFI && !WifiNames.Contains(AddWifi))
                        AddWifiName(AddWifi);
                    AddWifi = "";
                }));
        }

        private ICommand deleteWifiCommand;
        public ICommand DeleteWifiCommand
        {
            get => deleteWifiCommand ?? (deleteWifiCommand =
                new RelayCommand(param => RemoveWifiNameByIndex((int)param)));
        }

        private ICommand applyCommand;

        public ICommand ApplyCommand
        {
            get => applyCommand ?? (applyCommand = new SimpleCommand(() =>
            {
                SettingsModel.Interval = Interval.TimeOfDay;
                SettingsModel.Start = Start.TimeOfDay;
                SettingsModel.End = End.TimeOfDay;
            }));
        }

        private ICommand restartCommand;

        public ICommand RestartCommand
        {
            get => restartCommand ?? (restartCommand = new SimpleCommand(() =>
            {
                timer.Restart(SettingsModel.Interval);
                TimeLeft = SettingsModel.Interval;
            }));
        }


        #endregion


        public MainViewModel()
        {
            WifiNames = SettingsModel.WifiNames;
            CurrentWifi = WifiModel.CurrentWifiName;
            Interval = new DateTime(1, 1, 1, SettingsModel.Interval.Hours, SettingsModel.Interval.Minutes, SettingsModel.Interval.Seconds);
            Start = new DateTime(1, 1, 1, SettingsModel.Start.Hours, SettingsModel.Start.Minutes, SettingsModel.Start.Seconds);
            End = new DateTime(1, 1, 1, SettingsModel.End.Hours, SettingsModel.End.Minutes, SettingsModel.End.Seconds);
            timer.Start(SettingsModel.Interval);
            TimeLeft = SettingsModel.Interval;
            timer.RegisterMainAction(() =>
            {
                if (WifiNames.Contains(WifiModel.CurrentWifiName) && Start.TimeOfDay <= DateTime.Now.TimeOfDay && DateTime.Now.TimeOfDay<=End.TimeOfDay)
                    notificationManager.Show(new NotificationContent()
                    {
                        Title = "Time's up",
                        Message = "Unless you puke, faint, or die, keep going!",
                        Type = NotificationType.Information
                    }) ;
            });
            timer.RegisterTickAction(() =>
            {
                TimeLeft -= TimeSpan.FromSeconds(1);
                if (TimeLeft <= TimeSpan.Zero)
                    TimeLeft = SettingsModel.Interval;
            });
        }


        private void AddWifiName(string wifiName)
        {
            SettingsModel.AddWifiName(wifiName);
            WifiNames.Add(wifiName);
        }

        private void RemoveWifiNameByIndex(int index)
        {
            SettingsModel.RemoveWifiNameByIndex(index);
            WifiNames.RemoveAt(index);
        }
    }
}
