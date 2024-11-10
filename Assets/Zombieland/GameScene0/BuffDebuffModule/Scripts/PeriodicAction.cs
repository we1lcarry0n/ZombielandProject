using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Zombieland.GameScene0.BuffDebuffModule
{
    public class PeriodicAction
    {
        public event Action OnFinished;
        
        private int _lifeTimer;
        private int _interval;
        private System.Timers.Timer _timer;
        private ElapsedEventHandler _elapsedEventHandler;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public PeriodicAction(float lifeTimer, float interval, ElapsedEventHandler elapsedEventHandler)
        {
            _lifeTimer = (int)(lifeTimer * 1000);
            _interval = (int)(interval * 1000);
            _elapsedEventHandler = elapsedEventHandler;
        }

        public void Start()
        {
            if (_interval > 0)
            {
                _timer = new System.Timers.Timer(_interval);
                _timer.SynchronizingObject = null;
                _timer.Elapsed += _elapsedEventHandler;
                _timer.Start();

                Task.Delay(_lifeTimer, _cancellationTokenSource.Token).ContinueWith(task =>
                {
                    Stop();
                });
            }
            else
            {
                Task.Delay(_lifeTimer, _cancellationTokenSource.Token).ContinueWith(task =>
                {
                    _elapsedEventHandler.Invoke(null, null);

                    Stop();
                });
            }
        }

        public void Stop()
        {
            _timer?.Stop();
            _timer?.Dispose();
            _cancellationTokenSource.Cancel();
            OnFinished?.Invoke();
        }           
    }
}