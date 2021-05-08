using NativeWifi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Reminder.Models
{
    public static class WifiModel
    {
        public static string CurrentWifiName
        {
            get
            {
                try
                {
                    WlanClient client = new WlanClient();
                    Wlan.Dot11Ssid ssid = client.Interfaces[0].CurrentConnection.wlanAssociationAttributes.dot11Ssid;
                    return Encoding.ASCII.GetString(ssid.SSID, 0, (int)ssid.SSIDLength);
                }
                catch (Exception)
                {
                    return null;
                }

            }
        }
    }
}
