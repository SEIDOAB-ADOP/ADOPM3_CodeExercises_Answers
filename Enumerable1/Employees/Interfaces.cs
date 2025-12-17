using Newtonsoft.Json;
using Seido.Utilities.SeedGenerator;

namespace Models.Employees;

public enum WorkRole {AnimalCare, Veterinarian, ProgramCoordinator, Maintenance, Management}

public interface IEmployeeList :IEnumerable<IEmployee>
{
    public IEmployee this[int index] { get; }
    public int Count { get; }
    public IEnumerable<IEmployee> Filter(Func<ICreditCard, IEmployee, bool> predicate);
}


public interface IEmployee
{
    public Guid EmployeeId { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime HireDate { get; set; }
    public WorkRole Role { get; set; }

    public List<ICreditCard> CreditCards { get; set; }
}

public enum CardIssuer {AmericanExpress, Visa, MasterCard, DinersClub}

public interface ICreditCard
{
    public Guid CreditCardId { get; set; }

    public CardIssuer Issuer { get; set; }
    public string Number { get; set; }
    public string ExpirationYear { get; set; }
    public string ExpirationMonth { get; set; }
}