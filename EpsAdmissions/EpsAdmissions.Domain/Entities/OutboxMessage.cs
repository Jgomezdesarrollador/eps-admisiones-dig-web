namespace EpsAdmissions.Domain.Entities;

public class OutboxMessage
{
    public Guid Id { get; private set; }

    public string EventType { get; private set; }

    public string Payload { get; private set; }

    public bool Processed { get; private set; }

    public int RetryCount { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? ProcessedAt { get; private set; }
    public string AggregateId { get; private set; }

    private OutboxMessage()
    {
    }

    public OutboxMessage(string aggregateId, string eventType, string payload)
    {
        Id = Guid.NewGuid();
        AggregateId = aggregateId;
        EventType = eventType;
        Payload = payload;
        CreatedAt = DateTime.UtcNow;
        RetryCount = 0;
        Processed = false;
    }

    public void MarkAsProcessed()
    {
        Processed = true;
        ProcessedAt = DateTime.UtcNow;
    }

    public void IncrementRetry()
    {
        RetryCount++;
    }
}