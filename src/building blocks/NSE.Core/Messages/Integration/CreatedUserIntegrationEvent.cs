namespace NSE.Core.Messages.Integration;

public class CreatedUserIntegrationEvent : IntegrationEvent
{
    public CreatedUserIntegrationEvent(Guid id, string name, string email, string documentNumber)
    {
        Id = id;
        Name = name;
        Email = email;
        DocumentNumber = documentNumber;
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string DocumentNumber { get; private set; }
}