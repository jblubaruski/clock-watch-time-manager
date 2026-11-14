using System;
using System.Collections.Generic;
using System.Text;

namespace ClockWatchTimeManager.Models
{
    /*
     * Provider allowing an object to send notifications containing data to multiple subscribers
     */
    public sealed class NotificationProvider<T> : IObservable<T>
    {
        private readonly List<IObserver<T>> _observers = [];
        private readonly Lock _sync = new();

        /*
         * Disposable used for unsubscribing from timeclock notifications
         */
        private sealed class Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer, Lock sync) : IDisposable
        {
            public void Dispose()
            {
                lock (sync)
                {
                    observers.Remove(observer);
                }
            }
        }

        /*
         * Adds an observer to the list of observers that recieve notifications and retuns an unsubscriber that the observer can use to unsubscribe from recieving notifications
         * 
         * observer is the observer that recieves notifications
         * 
         * Throws an exception if observer is null
         */
        public IDisposable Subscribe(IObserver<T> observer)
        {
            ArgumentNullException.ThrowIfNull(observer);

            lock (_sync)
            {
                if (!_observers.Contains(observer))
                    _observers.Add(observer);
            }

            return new Unsubscriber(_observers, observer, _sync);
        }

        /*
         * Sends a notification containing data to each observer
         */
        public void NotifyAll(T data)
        {
            IObserver<T>[] snapshot;
            lock (_sync)
            {
                snapshot = [.. _observers];
            }

            foreach (IObserver<T> observer in snapshot)
            {
                observer.OnNext(data);
            }
        }

        /*
         * Sends a notification telling each observer that the provider has finished sending information
         * 
         * Unsubscribes all observers from the provider
         */
        public void CompleteAll()
        {
            IObserver<T>[] snapshot;
            lock (_sync)
            {
                snapshot = [.. _observers];
                _observers.Clear();
            }

            foreach (IObserver<T> observer in snapshot)
            {
                observer.OnCompleted();
            }
        }

        /*
         * Sends a notification containing an exception informing each observer that an error has occured
         * 
         * Unsubscribes all observers from the provider
         */
        public void RaiseAll(Exception ex)
        {
            IObserver<T>[] snapshot;
            lock (_sync)
            {
                snapshot = [.. _observers];
                _observers.Clear();
            }

            foreach (IObserver<T> observer in snapshot)
            {
                observer.OnError(ex);
            }
        }
    }
}
