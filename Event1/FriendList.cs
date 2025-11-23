using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Event1
{
    public class FriendList
    {
        /// <summary>
        /// Event is fired every 10_000 friend created.
        /// Parameter object? sender - list being created
        /// parameter TEventArgs e - int, being the number of friends created
        /// </summary>
        public event EventHandler<int> CreationProgress;


        public  List<Friend> myFriends = new List<Friend>();

        public override string ToString()
        {
            string sRet = "";
            foreach (var item in myFriends)
            {
                sRet += item.ToString() + "\n";
            }
            return sRet;
        }

        public  void Seed (int NrOfItems)
        {

            for (int i = 0; i < NrOfItems; i++)
            {
                var afriend = Friend.Factory.CreateRandom();

                myFriends.Add(afriend);

                if (i%10_000 == 0)
                {
                    //Invoke the event
                    //Your Code
                    CreationProgress?.Invoke(this, i);
                }
            }
        }
    }
}
