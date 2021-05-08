using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Reminder.Models
{
    public class TimerModel
    {
        private Timer mainTimer = new Timer();

        private Timer tickTimer = new Timer();

        public void RegisterMainAction(Action action) => mainActions.Add(action);
        public void RegisterTickAction(Action action) => tickActions.Add(action);

        private List<Action> mainActions = new List<Action>();

        private List<Action> tickActions=new List<Action>();

        public TimerModel()
        {
            mainTimer.Elapsed += MainTimer_Elapsed;
            tickTimer.Elapsed += TickTimer_Elapsed;
        }

        public void Start(TimeSpan timeSpan)
        {
            mainTimer.Interval = timeSpan.TotalMilliseconds;
            tickTimer.Interval = 1000d;
            mainTimer.Start();
            tickTimer.Start();
        }

        private void TickTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            foreach (var a in tickActions)
            {
                a.Invoke();
            }
        }

        private void MainTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            foreach (var a in mainActions)
            {
                a.Invoke();
            }
        }

        public void Stop()
        {
            mainTimer.Stop();
            tickTimer.Stop();
        }

        public void Restart(TimeSpan timeSpan)
        {
            Stop();
            Start(timeSpan);
        }
    }
}
