using Delegate3;

Console.WriteLine("Hello, Friends!");

var friends = new FriendList(10);
//System.Console.WriteLine(friends);


string s = Greetings("Sammy", 5);
Console.WriteLine(s);


Func<string, int, string> f = Greetings;
var s1 = f("Baggins", 25);
Console.WriteLine(s1);


foreach (var friend in friends.MyFriends)
{
    //Console.WriteLine(friend.SayHello(Greetings));
    //Console.WriteLine(friend.SayHello(Greetings1));
    //friend.DoTransform(TransformToSally);
    //friend.DoTransform(TransformToFord);

    var message = friend.SendEmail(CarRecall);
    Console.WriteLine(message);
}

string CarRecall(Friend friend)
{
    if (friend.Car.Color == CarColor.Green)
    {
        return $"Dear {friend.FirstName}, your car is ugly {friend.Car.Color}, pls come to the shop for a paint job!";
    }
    return $"Dear {friend.FirstName}. Congratulations, to an excellent choice of a {friend.Car.Color} car!";
}


void TransformToSally(Friend friend)
{
    if (friend.Car.Color == CarColor.Green)
    {
        friend.FirstName = "Sally";
    }
}

void TransformToFord(Friend friend)
{
    if (friend.Email.Contains("gmail"))
    {
        friend.Car.Brand = CarBrand.Ford;
    }
}

string Greetings(string name, int age)
{
    return $"Hello {name}, happy {age} birthday!";
}
string Greetings1(string name, int age)
{
    return $"{name}, happy {age}!";
}
