using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking
{
    class Program
    {
        public static void Main()
        {
            var service = new VideoService();
            //var title = service.ReadVideoTitle(new FileReader()); // This is done through Dependency Injection Frameworks (for making objects for these...)
            var title = service.ReadVideoTitle();
        }
    }
}
