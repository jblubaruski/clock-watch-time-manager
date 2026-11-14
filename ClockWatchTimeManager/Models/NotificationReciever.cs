using System;
using System.Collections.Generic;
using System.Text;

namespace ClockWatchTimeManager.Models
{
    /*
     * Reciever allowing an object to recieve notifications from a provider
     */
    public sealed class NotificationReciever<T>(Action<T> onNext, Action onCompleted, Action<Exception> onError) : IObserver<T>
    {
        private IDisposable? _unsubscriber;
        private T? _previous;

        // the method called in OnNext
        private Action<T> Next { get; } = onNext;
        // the method called in OnCompleted
        private Action Completed { get; } = onCompleted;
        // the method called in OnError
        private Action<Exception> Error { get; } = onError;

        /*
         * Subscribes to a provider
         * 
         * provider the notification provider this reviever is subscribing to
         * 
         * Throws an ArgumentNullException if provider is null
         */
        public void Subscribe(IObservable<T> provider)
        {
            ArgumentNullException.ThrowIfNull(provider);

            _unsubscriber = provider.Subscribe(this);
        }

        public void Unsubscribe() => _unsubscriber?.Dispose();

        /*
         * Called by the provider to send data to the reciever
         */
        public void OnNext(T data)
        {
            Next(data);
            _previous = data;
        }

        /*
         * Called by the provider to tell the reciever its done sending data
         */
        public void OnCompleted()
        {
            Completed();
            Unsubscribe();
        }

        /*
         * Called by the provider to inform the reciever of an error
         */
        public void OnError(Exception ex)
        {
            Error(ex);
            Unsubscribe();
        }
    }
}
