﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Async
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    internal class IOBoundAsync
    {
        public int GetDotNetCount()
        {
            using (var w = new WebClient())
            {
                string html = w.DownloadString("https://dotnetfoundation.org");

                //simulate a delay in communication
                Task.Delay(5000).Wait();
                return html.Length;
            }
        }

        public Task<int> GetDotNetCountAsync() => Task.Run(() => GetDotNetCount());
    }
    internal class CPUBoundAsync
    {
        public Task<int> GetPrimesCountAsync(int start, int count)
        {
            return Task.Run(() =>
               Enumerable.Range(start, count).Count(n =>
                 Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));
        }
        public int GetPrimesCount(int start, int count)
        {
            return Enumerable.Range(start, count).Count(n =>
                 Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0));
        }
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            myCounter.Content = (int.Parse((string)myCounter.Content) + 1).ToString();
        }
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            myGreetings.Text = "";
            int nrwords = new IOBoundAsync().GetDotNetCount();
            myGreetings.Text = nrwords.ToString();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            myGreetings.Text = "";
            int nrwords = await new IOBoundAsync().GetDotNetCountAsync();
            myGreetings.Text = nrwords.ToString();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            myGreetings.Text = "";
            int nrPrimes = new CPUBoundAsync().GetPrimesCount(2, 10_000_000);
            myGreetings.Text = nrPrimes.ToString();
        }
        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            myGreetings.Text = "";
            int nrPrimes = await new CPUBoundAsync().GetPrimesCountAsync(2, 10_000_000);
            myGreetings.Text = nrPrimes.ToString();
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            myGreetings.Text = "Counting....";
            int nrPrimes = await new CPUBoundAsync().GetPrimesCountAsync(2, 10_000_000);

            //another way
            /*
            var t1 = new CPUBoundAsync().GetPrimesCountAsync(2, 2_000_000);
            var t2 = new CPUBoundAsync().GetPrimesCountAsync(2_000_002, 2_000_000);
            var t3 = new CPUBoundAsync().GetPrimesCountAsync(4_000_002, 2_000_000);
            var t4 = new CPUBoundAsync().GetPrimesCountAsync(6_000_002, 2_000_000);
            var t5 = new CPUBoundAsync().GetPrimesCountAsync(8_000_002, 2_000_000);

            await Task.WhenAll(t1,t2, t3, t4, t5);
            int nrPrimes = t1.Result + t2.Result + t3.Result + t4.Result + t5.Result;
            */

            await WriteAllTextAsync(fname("Primes.txt"), nrPrimes.ToString());
            myGreetings.Text = nrPrimes.ToString();
        }

        private Task WriteAllTextAsync(string path, string content) => Task.Run(() => File.WriteAllText(path, content));

        static string fname(string name)
        {
            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            documentPath = System.IO.Path.Combine(documentPath, "ADOP", "Async");
            if (!Directory.Exists(documentPath)) Directory.CreateDirectory(documentPath);
            return System.IO.Path.Combine(documentPath, name);
        }
    }
    //Exercise:
    //1.    Use asyc/await pattern to write the nr of primes between 2, 10_000_000 to a text file
    //      - Use File.WriteAllText and make it Async according to the Task.Run pattern
    //      - is it Thread safe? how would you make it Thread safe
}
