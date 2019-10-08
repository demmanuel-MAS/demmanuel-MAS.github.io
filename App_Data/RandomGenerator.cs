using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MASService1._0
{
    public class RandomGenerator
    {
        int result;
        public int generateRandomNumber()
        {
            Random rnd = new Random();
            result = rnd.Next(1000, 9999);
            return result;
        }
    }
}