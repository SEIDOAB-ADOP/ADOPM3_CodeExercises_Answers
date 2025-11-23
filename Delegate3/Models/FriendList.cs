using Seido.Utilities.SeedGenerator;

namespace Delegate3;

public class FriendList
{
    public  List<Friend> MyFriends = new List<Friend>();

    public override string ToString()
    {
        string sRet = "";
        foreach (var item in MyFriends)
        {
            sRet += item.ToString() + "\n";
        }
        return sRet;
    }

    public FriendList(int nrOfFriends)
    {
        var rnd = new SeedGenerator();
        for (int i = 0; i < nrOfFriends; i++)
        {
            MyFriends.Add(new Friend().Seed(rnd));
        }
    }
}
