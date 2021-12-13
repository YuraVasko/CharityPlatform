using MediatR;

namespace CharityPlatform.SharedKernel
{
    public abstract class Event : INotification
    {
        public abstract string EventName { get; }
    }
}
