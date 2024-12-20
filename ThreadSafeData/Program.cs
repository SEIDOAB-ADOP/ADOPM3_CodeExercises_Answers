using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSafeData
{
    internal class Program
    {
        public class Vehicle
        {
            public string Owner  {get; set;}
            public string CarBrand  {get; set;}
            public string RegNr {get; set;}
        }
        
        public class VechicleStorage
        {
            object _locker = new object();
            string Owner;
            string CarBrand;
            string RegNr;

            Random rnd = new Random();
            Vehicle _vehicle;
            List<Vehicle> _vehicles = new List<Vehicle>();

            public int Count()
            {
                lock (_locker)
                {
                    return _vehicles.Count;
                }
            }
            public void SetData(string owner, string carBrand, string regNr)
            {
                lock (_locker)
                {
                    //I somehow manipulate the data
                    Owner = owner;
                    CarBrand = carBrand;
                    Thread.Sleep(rnd.Next(1, 5));
                    RegNr = regNr;

                    //Finally, create the instance
                    _vehicle = new Vehicle(){ Owner = Owner,  CarBrand = CarBrand, RegNr = RegNr};
                    
                    //Place it into the list
                    _vehicles.Add(_vehicle);

                    //Check data consistency
                    // if ((_vehicle.Owner, _vehicle.CarBrand, _vehicle.RegNr) != ("Donald Duck", "Volvo", "JKF 345") &&
                    //     (_vehicle.Owner, _vehicle.CarBrand, _vehicle.RegNr) != ("Mickey Mouse", "Jaguar", "EFD 235"))
                    //     Console.WriteLine($"mySafe mismatch: Owner:{_vehicle.Owner}, CarBrand:{_vehicle.CarBrand}, RegNr:{_vehicle.RegNr}");
                }
                
            }
            public (string, string, string) GetData()
            {
                lock (_locker) 
                { 
                    return (_vehicle.Owner, _vehicle.CarBrand, _vehicle.RegNr); 
                }
            }

            public bool CheckConsistency()
            {
                lock (_locker) 
                { 
                    foreach (var v in _vehicles)
                    {
                        if ((v.Owner, v.CarBrand, v.RegNr) != ("Donald Duck", "Volvo", "JKF 345") &&
                            (v.Owner, v.CarBrand, v.RegNr) != ("Mickey Mouse", "Jaguar", "EFD 235"))
                        {
                            Console.WriteLine($"mySafe mismatch: Owner:{v.Owner}, CarBrand:{v.CarBrand}, RegNr:{v.RegNr}");
                            return false;
                        }
                    }
                    return true;
                }
            }            
        }

        static void Main(string[] args)
        {
            var vechicleStorage = new VechicleStorage();

            var t1 = Task.Run(() =>
            {
                var rnd = new Random();
                for (int i = 0; i < 1_000; i++)
                {
                    vechicleStorage.SetData("Mickey Mouse", "Jaguar", "EFD 235");

                    (string owner, string carBrand, string regNr) = vechicleStorage.GetData();
                    // if ((owner, carBrand, regNr) != ("Donald Duck", "Volvo", "JKF 345") &&
                    //     (owner, carBrand, regNr) != ("Mickey Mouse", "Jaguar", "EFD 235"))
                    //     Console.WriteLine($"mySafe mismatch: Owner:{owner}, CarBrand:{carBrand}, RegNr:{regNr}");
                }
                Console.WriteLine("t1 Finished");
            });

            var t2 = Task.Run(() =>
            {
                var rnd = new Random();
                for (int i = 0; i < 1_000; i++)
                {
                    vechicleStorage.SetData("Donald Duck", "Volvo", "JKF 345");

                    (string owner, string carBrand, string regNr) = vechicleStorage.GetData();
                    // if ((owner, carBrand, regNr) != ("Donald Duck", "Volvo", "JKF 345") &&
                    //     (owner, carBrand, regNr) != ("Mickey Mouse", "Jaguar", "EFD 235"))
                    //     Console.WriteLine($"mySafe mismatch: Owner:{owner}, CarBrand:{carBrand}, RegNr:{regNr}");
                }
                Console.WriteLine("t2 Finished");
            });

            Task.WaitAll(t1, t2);
            Console.WriteLine("All Finished");

            System.Console.WriteLine(vechicleStorage.Count());
            System.Console.WriteLine(vechicleStorage.CheckConsistency());
        }
    }  
}
/*  Exercise
    1.  - Have task t1 write 1000 times "Mickey Mouse", "Jaguar", "EFD 235" to vechicleStorage.SetData
        - Have task t2 write 1000 times "Donald Duck", "Volvo", "JKF 345" to vechicleStorage.SetData
        - Verify data consistency
        - Discuss in the group what is data consistency means here and why is it wrong
    
    2.  - in both tasks, use vechicleStorage.GetData() to read the data right after writing it with vechicleStorage.SetData
        - The data read could be either of below.  Both are correct. Do you understand why?
             (owner, carBrand, regNr) == ("Donald Duck", "Volvo", "JKF 345") or
             (owner, carBrand, regNr) == ("Mickey Mouse", "Jaguar", "EFD 235")
        - in both tasks, write an if statement after vechicleStorage.GetData() to test data consistency. Write an error if not consistent

    3. Make class Vehicle Thread safe using lock(...)
        - Verify data consistency is now correct

    4. Add another tasks that writes 100 times "Scrooge McDuck", "Lamborghini", "SCROOGE" to vechicleStorage.SetData
        - Verify data consistency is still correct
*/
