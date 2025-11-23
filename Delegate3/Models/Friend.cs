using Seido.Utilities.SeedGenerator;

namespace Delegate3;

public enum FriendLevel { Friend, ClassMate, Family, BestFriend }
public class Friend
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }

    public string Email { get; set; }
    public FriendLevel Level { get; set; }

    public Car Car { get; set; }
        

    public override string ToString()
    {
        var sRet = $"{FirstName} {LastName} is my {Level} and can be reached at {Email}. {Age} years old.";
        if (Car != null)
        {
            sRet += $"\n -{FirstName}'s car is a {Car.Color} {Car.Brand} {Car.Model} from year {Car.YearModel}";
        }
        return sRet;
    }

    public string SayHello(Func<string, int, string> greeting)
    {
        return greeting(FirstName, Age);
    }

    public void DoTransform (Action <Friend> transform)
    {
        transform(this);
    }

    public string SendEmail(Func<Friend, string> composeEmail)
    {
        return composeEmail(this);
    }

    public Friend Seed(SeedGenerator seeder)
    {
        string _firstName = seeder.FirstName;
        string _lastName = seeder.LastName;

        return new Friend() {
            FirstName = _firstName,
            LastName = _lastName,

            Email = seeder.Email(_firstName, _lastName),
            Level = seeder.FromEnum<FriendLevel>(),
            Age = seeder.Next(5, 35),
            Car = new Car().Seed(seeder) };
    }
}
