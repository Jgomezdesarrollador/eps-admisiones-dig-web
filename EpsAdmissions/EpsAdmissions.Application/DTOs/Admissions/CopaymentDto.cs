namespace EpsAdmissions.Application.DTOs.Admissions;

public class CopaymentDto
{
    public decimal Amount { get; set; }

    public string Currency { get; set; } = "COP";
}