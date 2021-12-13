using CharityPlatform.SharedKernel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CharityPlatform.Integration.Infrastructure
{
    public class StoredEventSerializer
    {
        public StoredEventSerializer(params Assembly[] assemblies)
        {
            EventTypes = assemblies.SelectMany(x => x.GetTypes())
                .Where(x => typeof(Event)
                .IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => (EventName: GetEventName(x), Type: x))
                .ToList();
        }

        private List<(string EventName, Type Type)> EventTypes { get; }

        public string Serialize(Event storedEvent)
        {
            return JsonConvert.SerializeObject(storedEvent);
        }

        public Event Deserialize(string serializedEvent, string eventType)
        {
            var eventTypes = EventTypes.Single(t => string.Equals(t.EventName, eventType, StringComparison.OrdinalIgnoreCase));
            return (Event)JsonConvert.DeserializeObject(serializedEvent, eventTypes.Type);
        }

        private static string GetEventName(Type eventType)
        {
            return eventType.Name;
        }
    }
}
