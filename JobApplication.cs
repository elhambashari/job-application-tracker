public class JobApplication
{
    public string CompanyName { get; set; } = "";
    public string PositionTitle { get; set; } = "";
    public Status Status { get; set; }
    public DateTime ApplicationDate { get; set; }
    public DateTime? ResponseDate { get; set; }
    public int SalaryExpectation { get; set; }

    public int GetDaysSinceApplied()
    {
        return (DateTime.Today - ApplicationDate.Date).Days;
    }

    public string GetSummary()
    {
        return $"Company: {CompanyName}, " +
               $"Position: {PositionTitle}, " +
               $"Status: {Status}, " +
               $"Applied: {ApplicationDate:yyyy-MM-dd}, " +
               $"Salary: {SalaryExpectation} kr";
    }
}