using MediatR;

namespace CharityPlatform.DAL.Models
{
    public class StoredEvent : INotification
    {
        public string EventStreamId { get; set; }

        public string EventType { get; set; }

        public int EventPosition { get; set; }

        public string EventJson { get; set; }
    }
}
