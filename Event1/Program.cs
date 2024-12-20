// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System;

namespace Event1 // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
          Console.WriteLine("\nHuge friendlist");
          
          //Create an instance of FriendList
          //Your code
          var friendlist = new FriendList();

          //Assign your event handler to FriendList 
          // Your code
          friendlist.CreationProgress += WriteProgress;
          friendlist.CreationProgress += HalfTime;
          
          //Seed the instance of friendlist with 1_000_000 friends
          friendlist.Seed(1_000_000);

        }

        //Declare your Eventhandler
        //Your code
        static void WriteProgress (object sender, int e)
        {
            System.Console.WriteLine($"{e} friends created");
        }
        static void HalfTime (object sender, int e)
        {
            if (e == 500_000)
            System.Console.WriteLine($"HalfTime: {e} friends created!!!");
        }
    }
}
//Exercise
//1. In Friendlist implement the firing of an event, 
//2. In Program implement the event handler and assign it to the event CreationProgress