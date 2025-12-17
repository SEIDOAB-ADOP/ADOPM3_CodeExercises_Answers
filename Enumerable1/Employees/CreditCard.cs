using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Seido.Utilities.SeedGenerator;

namespace Models.Employees;

public class CreditCard : ICreditCard, ISeed<CreditCard>
{
    public Guid CreditCardId { get; set; }

    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public CardIssuer Issuer { get; set; }
    
    public string Number { get; set; }
    public string ExpirationYear { get; set; }
    public string ExpirationMonth { get; set; }

    public CreditCard() {}
    public CreditCard(ICreditCard original)
    {
        CreditCardId = original.CreditCardId;
        Issuer = original.Issuer;
        Number = original.Number;
        ExpirationYear = original.ExpirationYear;
        ExpirationMonth = original.ExpirationMonth;
    }

    #region randomly seed this instance
    public bool Seeded { get; set; } = false;

    public CreditCard Seed (SeedGenerator seeder)
    {
        Seeded = true;
        CreditCardId = Guid.NewGuid();
        
        Issuer = seeder.FromEnum<CardIssuer>();

        Number = $"{seeder.Next(2222, 9999)}-{seeder.Next(2222, 9999)}-{seeder.Next(2222, 9999)}-{seeder.Next(2222, 9999)}";
        ExpirationYear = $"{seeder.Next(25, 32)}";
        ExpirationMonth = $"{seeder.Next(01, 13):D2}";
        return this;
    }
    #endregion
}