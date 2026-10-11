
JobManager manager = new JobManager();

bool running = true;

while (running)
{
    Console.WriteLine("\n===== JOB APPLICATION TRACKER =====");
    Console.WriteLine("1. Add job application");
    Console.WriteLine("2. Show all applications");
    Console.WriteLine("3. Filter by status");
    Console.WriteLine("4. Sort by application date");
    Console.WriteLine("5. Show statistics");
    Console.WriteLine("6. Update application status");
    Console.WriteLine("7. Remove job application");
    Console.WriteLine("8. Exit");

    Console.Write("\nChoose an option (1-8): ");
    string choice = Console.ReadLine() ?? "";

    switch (choice)
    {
        case "1":
            Console.Write("Company name: ");
            string company = Console.ReadLine() ?? "";

            Console.Write("Position title: ");
            string position = Console.ReadLine() ?? "";

            Console.Write("Salary expectation (SEK): ");
            if (!int.TryParse(Console.ReadLine(), out int salary)
                || salary < 0)
            {
                Console.WriteLine("Invalid salary.");
                break;
            }

            JobApplication newJob = new JobApplication
            {
                CompanyName = company,
                PositionTitle = position,
                Status = Status.Applied,
                ApplicationDate = DateTime.Today,
                SalaryExpectation = salary
            };

            manager.AddJob(newJob);
            break;

        case "2":
            manager.ShowAll();
            break;

        case "3":
            Console.WriteLine("0. Applied");
            Console.WriteLine("1. Interview");
            Console.WriteLine("2. Offer");
            Console.WriteLine("3. Rejected");

            Console.Write("Choose status (0-3): ");

            if (int.TryParse(Console.ReadLine(), out int filterStatus)
                && Enum.IsDefined(typeof(Status), filterStatus))
            {
                manager.ShowByStatus((Status)filterStatus);
            }
            else
            {
                Console.WriteLine("Invalid status.");
            }
            break;

        case "4":
            manager.SortByDate();
            break;

        case "5":
            manager.ShowStatistics();
            break;

        case "6":
            Console.Write("Company name: ");
            string updateCompany = Console.ReadLine() ?? "";

            Console.WriteLine("0. Applied");
            Console.WriteLine("1. Interview");
            Console.WriteLine("2. Offer");
            Console.WriteLine("3. Rejected");

            Console.Write("New status (0-3): ");

            if (int.TryParse(Console.ReadLine(), out int newStatus)
                && Enum.IsDefined(typeof(Status), newStatus))
            {
                manager.UpdateStatus(
                    updateCompany,
                    (Status)newStatus
                );
            }
            else
            {
                Console.WriteLine("Invalid status.");
            }
            break;

        case "7":
            Console.Write("Company name to remove: ");
            string removeCompany = Console.ReadLine() ?? "";

            manager.RemoveJob(removeCompany);
            break;

        case "8":
            running = false;
            Console.WriteLine("Goodbye!");
            break;

        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }
}

