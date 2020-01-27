using System;
using System.Linq;
using System.Threading.Tasks;

namespace QuickNSmart.ConApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Task.Run(() => Console.WriteLine("QuickNSmart"));
        }
    }
}
