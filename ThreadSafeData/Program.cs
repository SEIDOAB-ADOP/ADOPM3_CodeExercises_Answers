using System;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSafeData
{
    internal class Program
    {
        public class SafeData
        {
            object _locker = new object();
            string SafeFile;
            int iSafeResult1 = 0;
            int iSafeResult2 = 0;
            Random rnd = new Random();


            public void SetData(string fname, int sf1, int sf2)
            {
                lock (_locker)
                {
                    SafeFile = fname;
                    iSafeResult1 = sf1;
                    Thread.Sleep(rnd.Next(1, 5));
                    iSafeResult2 = sf2;

                    if (iSafeResult1 != iSafeResult2 || SafeFile != $"file{iSafeResult1}.txt")
                        Console.WriteLine($"mySafe mismatch: iSafeResult1:{iSafeResult1}, iSafeResult2:{iSafeResult2}, SafeFile:{SafeFile}");
                }
            }
            public (string, int, int) GetData()
            {
                lock (_locker) 
                { 
                    return (SafeFile, iSafeResult1, iSafeResult2); 
                }
            }
        }

        static void Main(string[] args)
        {
            var SafeStorage = new SafeData();

            var t1 = Task.Run(() =>
            {
                var rnd = new Random();
                for (int i = 0; i < 1_000; i++)
                {
                    SafeStorage.SetData("file8888.txt", 8888, 8888);
                    (string fname, int i1, int i2) = SafeStorage.GetData();
                    if (i1 != i2 || fname != $"file{i1}.txt")
                        Console.WriteLine($"mySafe mismatch: i1:{i1}, i2:{i2}, fname:{fname}");
                }
                Console.WriteLine("t1 Finished");
            });

            var t2 = Task.Run(() =>
            {
                var rnd = new Random();
                for (int i = 0; i < 1_000; i++)
                {
                    SafeStorage.SetData("file1111.txt", 1111, 1111);
                    (string fname, int i1, int i2) = SafeStorage.GetData();
                    if (i1 != i2 || fname != $"file{i1}.txt")
                        Console.WriteLine($"mySafe mismatch: i1:{i1}, i2:{i2}, fname:{fname}");
                }
                Console.WriteLine("t2 Finished");
            });

            Task.WaitAll(t1, t2);
            Console.WriteLine("All Finished");
        }
    }  }
/*  Exercise
    1. Make class Vehicle Thread safe using lock(...)
    2.  - Have task t1 write 1000 times "ABC 123", "Kalle Anka" to myCar
        - Have task t2 write 1000 times "HKL 556", "Musse Pigg" to myCar
        - Verify data consistency
        - Discuss in the group what is data consistency in case of class Vehicle. Is your code living up to it?
*/
