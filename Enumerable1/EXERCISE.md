# Exercise: IEnumerable Implementation with Employees and Credit Cards

## Overview
In this exercise, you will implement the `Employee` and `EmployeeList` classes, work with collections, and create custom filtering functionality using LINQ and IEnumerable.

## Learning Objectives
- Implement the `IEnumerable<T>` interface
- Work with custom collections
- Use the SeedGenerator to create random test data
- Implement indexer properties
- Create filter methods using predicates and lambda expressions
- Query complex object hierarchies

## Part 1: Implement the Employee Class

### Instructions
1. Navigate to [Employees/Employee.cs](Employees/Employee.cs)
2. Implement the `Employee` class that:
   - Implements the `IEmployee` interface
   - Implements the `ISeed<Employee>` interface for random data generation
   - Has a copy constructor `Employee(IEmployee original)`

### Requirements
- **Properties** (from `IEmployee` interface):
  - `Guid EmployeeId` - Unique identifier
  - `string FirstName` - Employee's first name
  - `string LastName` - Employee's last name
  - `DateTime HireDate` - Date the employee was hired
  - `WorkRole Role` - Employee's work role (enum: AnimalCare, Veterinarian, ProgramCoordinator, Maintenance, Management)
  - `List<ICreditCard> CreditCards` - List of credit cards (0 to 4 cards)

- **Seed Method**:
  - Set `Seeded = true`
  - Generate a new `Guid` for `EmployeeId`
  - Use `seeder.FirstName` and `seeder.LastName` for names
  - Use `seeder.DateAndTime` for `HireDate`
  - Use `seeder.FromEnum<WorkRole>()` for `Role`
  - Generate 0 to 4 credit cards using `seeder.Next(0, 5)`
  - For each credit card, create a new `CreditCard()` and call its `Seed()` method

### Example Structure
```csharp
public class Employee : IEmployee, ISeed<Employee>
{
    public Guid EmployeeId { get; set; }
    // ... other properties
    
    public bool Seeded { get; set; } = false;
    
    public Employee() { }
    
    public Employee(IEmployee original)
    {
        // Deep copy implementation
    }
    
    public Employee Seed(SeedGenerator seeder)
    {
        // Seeding implementation
        return this;
    }
}
```

## Part 2: Implement the EmployeeList Class

### Instructions
1. Create a new file `EmployeeList.cs` in the `Employees` folder
2. Implement the `EmployeeList` class that:
   - Implements the `IEmployeeList` interface
   - Implements `ISeed<EmployeeList>` for generating test data
   - Maintains an internal `List<IEmployee>`

### Requirements
- **Properties**:
  - `int Count` - Returns the number of employees
  - Indexer `this[int index]` - Allows accessing employees by index

- **IEnumerable Implementation**:
  - Implement `GetEnumerator()` to iterate through employees
  - Implement `IEnumerable.GetEnumerator()` (non-generic version)

- **Filter Method**:
  - Signature: `public IEnumerable<IEmployee> Filter(Func<ICreditCard, IEmployee, bool> predicate)`
  - Return employees where the predicate returns true for at least one credit card

- **Seed Method**:
  - Create exactly 100 employees
  - Seed each employee using the provided `SeedGenerator`

### Example Structure
```csharp
public class EmployeeList : IEmployeeList, ISeed<EmployeeList>
{
    private List<IEmployee> _employees = new List<IEmployee>();
    
    public IEmployee this[int index] => _employees[index];
    public int Count => _employees.Count;
    
    public bool Seeded { get; set; } = false;
    
    public IEnumerator<IEmployee> GetEnumerator()
    {
        // Return enumerator for _employees
    }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        // Return non-generic enumerator
    }
    
    public IEnumerable<IEmployee> Filter(Func<ICreditCard, IEmployee, bool> predicate)
    {
        // Filter implementation
    }
    
    public EmployeeList Seed(SeedGenerator seeder)
    {
        // Create 100 employees
        return this;
    }
}
```

## Part 3: Test Your Implementation in Program.cs

### Instructions
Update [Program.cs](Program.cs) to test your implementation:

1. **Create and seed an EmployeeList**:
```csharp
var seeder = new SeedGenerator();
var employeeList = new EmployeeList().Seed(seeder);
```

2. **Iterate through all employees** (demonstrates IEnumerable):
```csharp
Console.WriteLine($"Total employees: {employeeList.Count}\n");

foreach (var employee in employeeList)
{
    Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.Role}");
    Console.WriteLine($"  Credit Cards: {employee.CreditCards.Count}");
    foreach (var card in employee.CreditCards)
    {
        Console.WriteLine($"    - {card.Issuer}: {card.Number}");
    }
}
```

3. **Iterate through all employees using the indexer[]**

4. **Test the Filter method** - Find all Management employees with American Express:
```csharp
var managementWithAmex = employeeList.Filter(
    (card, employee) => employee.Role == WorkRole.Management && 
                        card.Issuer == CardIssuer.AmericanExpress
);

Console.WriteLine("\n=== Management Employees with American Express ===");
foreach (var employee in managementWithAmex)
{
    Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.Role}");
    var amexCards = employee.CreditCards.Where(c => c.Issuer == CardIssuer.AmericanExpress);
    foreach (var card in amexCards)
    {
        Console.WriteLine($"  AMEX: {card.Number} (Exp: {card.ExpirationMonth}/{card.ExpirationYear})");
    }
}
```

4. **Additional Tests** (optional):
   - Count employees in each role
   - Find employees with no credit cards
   - Find employees with 4 credit cards
   - List all unique credit card issuers

## Expected Output
Your program should output:
- A list of all 100 employees with their roles and credit cards
- A filtered list showing only Management employees who have at least one American Express card
- The filtered results should demonstrate that your `Filter` method works correctly

## Tips
- Remember to add `using System.Collections;` for `IEnumerable`
- Use `yield return` in the Filter method for efficient iteration
- The predicate in Filter takes both a credit card and an employee, allowing flexible filtering
- Make sure to handle the case where an employee has no credit cards

## Bonus Challenges
1. Add a method to EmployeeList that returns employees grouped by WorkRole
2. Create a LINQ query to find the average number of credit cards per role
3. Implement a method to find employees hired in a specific year
4. Add statistics methods (e.g., which role has the most American Express cards)

## Validation
Your implementation is correct when:
- ✓ The program compiles without errors
- ✓ You can iterate through all 100 employees using `foreach` (Enumerable) and `for` (Indexer)
- ✓ The Filter method returns only Management employees with AmericanExpress cards
- ✓ Each employee has between 0 and 4 credit cards
- ✓ All employees have valid randomly generated data
