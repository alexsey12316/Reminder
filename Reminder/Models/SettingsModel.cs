using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reminder.Models
{
    public static class SettingsModel
    {
        static SettingsModel()
        {
            if (Reminder.Properties.Settings.Default.WifiNames == null)
                Reminder.Properties.Settings.Default.WifiNames = new System.Collections.Specialized.StringCollection();
            if (Interval < TimeSpan.FromSeconds(10))
                Interval = new TimeSpan(1, 0, 0);
        }


        public static ObservableCollection<string> WifiNames
        {
            get => new ObservableCollection<string>(Reminder.Properties.Settings.Default.WifiNames.Cast<string>());
        }

        public static void AddWifiName(string wifiName)
        {
            Reminder.Properties.Settings.Default.WifiNames.Add(wifiName);
            Reminder.Properties.Settings.Default.Save();
        }

        public static void RemoveWifiNameByIndex(int index)
        {
            Reminder.Properties.Settings.Default.WifiNames.RemoveAt(index);
            Reminder.Properties.Settings.Default.Save();
        }

        public static TimeSpan Interval
        {
            get => Reminder.Properties.Settings.Default.Interval;
            set
            {
                Reminder.Properties.Settings.Default.Interval = value;
                Reminder.Properties.Settings.Default.Save();
            }
        }
        public static TimeSpan Start
        {
            get => Reminder.Properties.Settings.Default.Start;
            set
            {
                Reminder.Properties.Settings.Default.Start = value;
                Reminder.Properties.Settings.Default.Save();
            }
        }
        public static TimeSpan End
        {
            get => Reminder.Properties.Settings.Default.End;
            set
            {
                Reminder.Properties.Settings.Default.End = value;
                Reminder.Properties.Settings.Default.Save();
            }
        }




    }
}
