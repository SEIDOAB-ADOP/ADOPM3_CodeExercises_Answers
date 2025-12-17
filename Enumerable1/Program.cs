// See https://aka.ms/new-console-template for more information
using Models.Employees;
using Seido.Utilities.SeedGenerator;

Console.WriteLine("Hello, Employees!");


var seeder = new SeedGenerator();
var employeeList = new EmployeeList().Seed(seeder);

Console.WriteLine($"Total employees: {employeeList.Count}\n");
System.Console.WriteLine("\n=== Employee Details using foreach ===");
foreach (var employee in employeeList)
{
    Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.Role}");
    Console.WriteLine($"  Credit Cards: {employee.CreditCards.Count}");
    foreach (var card in employee.CreditCards)
    {
        Console.WriteLine($"    - {card.Issuer}: {card.Number}");
    }
}

System.Console.WriteLine("\n=== Employee Details using for ===");
for (int i = 0; i < employeeList.Count; i++)
{
    var employee = employeeList[i];
    Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.Role}");
    Console.WriteLine($"  Credit Cards: {employee.CreditCards.Count}");
    for (int j = 0; j < employee.CreditCards.Count; j++)
    {
        Console.WriteLine($"    - {employee.CreditCards[j].Issuer}: {employee.CreditCards[j].Number}");
    }
}

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