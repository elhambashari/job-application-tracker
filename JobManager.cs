
public class JobManager
{
    // List of all job applications
    public List<JobApplication> Applications { get; set; }
        = new List<JobApplication>();

    // Add a new job application
    public void AddJob(JobApplication job)
    {
        Applications.Add(job);
        Console.WriteLine("Job application added successfully!");
    }

    // Show a job application with a colored status
    private void PrintJob(JobApplication job)
    {
        Console.Write($"Company: {job.CompanyName}, ");
        Console.Write($"Position: {job.PositionTitle}, ");
        Console.Write("Status: ");

        ConsoleColor originalColor = Console.ForegroundColor;

        switch (job.Status)
        {
            case Status.Applied:
                Console.ForegroundColor = ConsoleColor.Blue;
                break;

            case Status.Interview:
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;

            case Status.Offer:
                Console.ForegroundColor = ConsoleColor.Green;
                break;

            case Status.Rejected:
                Console.ForegroundColor = ConsoleColor.Red;
                break;
        }

        Console.Write(job.Status);

        // Restore the original console color
        Console.ForegroundColor = originalColor;

        Console.WriteLine(
            $", Applied: {job.ApplicationDate:yyyy-MM-dd}, " +
            $"Salary: {job.SalaryExpectation} kr"
        );
    }

    // Show all job applications
    public void ShowAll()
    {
        if (Applications.Count == 0)
        {
            Console.WriteLine("No job applications found.");
            return;
        }

        foreach (JobApplication job in Applications)
        {
            PrintJob(job);
        }
    }

    // Update the status of a job application
    public void UpdateStatus(string companyName, Status newStatus)
    {
        JobApplication? job = Applications.FirstOrDefault(
            a => a.CompanyName.Equals(
                companyName,
                StringComparison.OrdinalIgnoreCase
            )
        );

        if (job == null)
        {
            Console.WriteLine("Job application not found.");
            return;
        }

        job.Status = newStatus;

        if (newStatus == Status.Applied)
        {
            job.ResponseDate = null;
        }
        else if (job.ResponseDate == null)
        {
            job.ResponseDate = DateTime.Today;
        }

        Console.WriteLine("Job application status updated successfully!");
    }

    // Remove a job application
    public void RemoveJob(string companyName)
    {
        JobApplication? job = Applications.FirstOrDefault(
            a => a.CompanyName.Equals(
                companyName,
                StringComparison.OrdinalIgnoreCase
            )
        );

        if (job == null)
        {
            Console.WriteLine("Job application not found.");
            return;
        }

        Applications.Remove(job);
        Console.WriteLine("Job application removed successfully!");
    }

    // Filter job applications by status using LINQ
    public void ShowByStatus(Status status)
    {
        var filteredJobs = Applications
            .Where(job => job.Status == status)
            .ToList();

        if (filteredJobs.Count == 0)
        {
            Console.WriteLine("No applications found with this status.");
            return;
        }

        foreach (JobApplication job in filteredJobs)
        {
            PrintJob(job);
        }
    }

    // Sort job applications by application date using LINQ
    public void SortByDate()
    {
        if (Applications.Count == 0)
        {
            Console.WriteLine("No job applications to sort.");
            return;
        }

        var sortedJobs = Applications
            .OrderBy(job => job.ApplicationDate)
            .ToList();

        foreach (JobApplication job in sortedJobs)
        {
            PrintJob(job);
        }
    }

    // Show job application statistics using LINQ
    public void ShowStatistics()
    {
        Console.WriteLine("\n--- Job Application Statistics ---");

        Console.WriteLine($"Total applications: {Applications.Count}");

        foreach (Status status in Enum.GetValues<Status>())
        {
            int count = Applications.Count(
                job => job.Status == status
            );

            Console.WriteLine($"{status}: {count}");
        }

        var respondedJobs = Applications
            .Where(job => job.ResponseDate.HasValue)
            .ToList();

        if (respondedJobs.Count > 0)
        {
            double averageDays = respondedJobs.Average(
                job => (job.ResponseDate!.Value -
                        job.ApplicationDate).TotalDays
            );

            Console.WriteLine(
                $"Average response time: {averageDays:F1} days"
            );
        }
        else
        {
            Console.WriteLine(
                "Average response time: No responses yet."
            );
        }
    }

    // Show applications with no response after more than 14 days
    public void ShowUnansweredApplications()
    {
        var unansweredJobs = Applications
            .Where(job =>
                job.Status == Status.Applied &&
                !job.ResponseDate.HasValue &&
                job.GetDaysSinceApplied() > 14)
            .ToList();

        if (unansweredJobs.Count == 0)
        {
            Console.WriteLine(
                "No unanswered applications older than 14 days."
            );
            return;
        }

        Console.WriteLine(
            "\n--- Unanswered Applications (More Than 14 Days) ---"
        );

        foreach (JobApplication job in unansweredJobs)
        {
            PrintJob(job);

            Console.WriteLine(
                $"Days since applied: {job.GetDaysSinceApplied()}"
            );
        }
    }
}
