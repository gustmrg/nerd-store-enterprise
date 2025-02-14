using MediatR;

namespace NSE.Core.Messages;

public class Event : Message, INotification
{
    protected Event()
    {
        Timestamp = DateTime.Now;
    }

    public DateTime Timestamp { get; private set; }
}