namespace EpsAdmissions.Blazor.Configuration;

public sealed class ApiSettings
{
    public const string SectionName = "ApiSettings";

    public string BaseUrl { get; init; } = string.Empty;
}