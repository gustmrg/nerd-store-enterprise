using NSE.Core.Messages;

namespace NSE.Core.DomainObjects;

public abstract class Entity
{
    protected Entity()
    {
        Id = Guid.NewGuid();
    }
    
    public Guid Id { get; set; }
    private List<Event> _events;
    public IReadOnlyCollection<Event> Events => _events?.AsReadOnly();

    public void AddEvent(Event @event)
    {
        _events ??= new List<Event>();
        _events.Add(@event);
    }

    public void RemoveEvent(Event @event)
    {
        _events?.Remove(@event);
    }

    public void ClearEvents()
    {
        _events?.Clear();
    }

    public override bool Equals(object? obj)
    {
        var compareTo = obj as Entity;

        if (ReferenceEquals(this, compareTo)) return true;
        if (ReferenceEquals(null, compareTo)) return false;
        
        return Id.Equals(compareTo.Id);
    }

    public static bool operator ==(Entity? left, Entity? right)
    {
        if (ReferenceEquals(left, null) && ReferenceEquals(right, null)) return true;
        if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;
        
        return left.Equals(right);
    }

    public static bool operator !=(Entity? left, Entity? right)
    {
        return !(left == right);
    }
    
    public override int GetHashCode() => (GetType().GetHashCode() * 907) + Id.GetHashCode();

    public override string ToString()
    {
        return $"{GetType().Name} [Id: {Id.ToString()}]";
    }
}