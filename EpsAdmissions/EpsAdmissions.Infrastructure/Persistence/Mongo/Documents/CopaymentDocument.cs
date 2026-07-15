namespace EpsAdmissions.Infrastructure.Persistence.Mongo.Documents;

public sealed class CopaymentDocument
{
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "COP";
}