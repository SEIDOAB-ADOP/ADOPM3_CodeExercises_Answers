using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Delegate2
{
    public class FriendList
    {
        public  List<Friend> myFriends = new List<Friend>();
        public Friend this[int idx]=> myFriends[idx];

        public override string ToString()
        {
            string sRet = "";
            foreach (var item in myFriends)
            {
                sRet += item.ToString() + "\n";
            }
            return sRet;
        }

        public void SayHello(Action<Friend> sayHello)
        {
            foreach (var item in myFriends)
            {
                sayHello(item);
            }       
        }


        public static class Factory
        {
            public static FriendList CreateRandom(int NrOfItems, Func<Friend,Friend> CustomInit = null)
            {

                var myList = new FriendList();
                for (int i = 0; i < NrOfItems; i++)
                {
                    var afriend = Friend.Factory.CreateRandom();
                    if (CustomInit != null)
                        afriend = CustomInit(afriend);

                    myList.myFriends.Add(afriend);
                }
                return myList;
            }
        }
    }
}
