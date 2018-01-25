﻿using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.EventSourcing.Exceptions;
using StockTradingAnalysis.Interfaces.Common;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.EventSourcing.Storage
{
    /// <summary>
    /// The EventStoreInitializer initializes the event store
    /// </summary>
    public class EventStoreInitializer : IEventStoreInitializer
    {
        /// <summary>
        /// The event store
        /// </summary>
        private readonly IEventStore _eventStore;

        /// <summary>
        /// The event bus
        /// </summary>
        private readonly IEventBus _eventBus;

        /// <summary>
        /// The logging service
        /// </summary>
        private readonly ILoggingService _loggingService;

        /// <summary>
        /// The is initialized
        /// </summary>
        private static bool _isInitialized;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="eventStore">The event store.</param>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="loggingService">The logging service.</param>
        public EventStoreInitializer(IEventStore eventStore, IEventBus eventBus, ILoggingService loggingService)
        {
            _eventStore = eventStore;
            _eventBus = eventBus;
            _loggingService = loggingService;
        }

        /// <summary>
        /// Instructs the eventstore <seealso cref="IEventStore"/> to load all events, 
        /// processes them and publish them on the eventbus <seealso cref="IEventBus"/>
        /// </summary>
        public void Replay()
        {
            if (_isInitialized)
                throw new EventStoreInitializeException(
                    "The eventstore is already initialized. This can only be done once at startup");

            _loggingService.Debug("Start replay");

            var count = 0;
            long msec = 0;
            using (new TimeMeasure(t => msec = t))
            {
                foreach (var @event in _eventStore.GetEvents())
                {
                    count++;
                    _eventBus.Publish(@event);
                }
            }

            _loggingService.Debug($"Replayed {count} events in {msec / 1000} seconds");

            _isInitialized = true;
        }
    }
}