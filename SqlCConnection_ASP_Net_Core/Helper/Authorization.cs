using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlCConnection_ASP_Net_Core.Helper
{
    public class Authorization
    {
        public static bool decode(string authVal)
        {
            string[] splitVal = authVal.Split(" ");

            Console.WriteLine(splitVal[1]);
            byte[] data = Convert.FromBase64String(splitVal[1]);
            string decodedString = Encoding.UTF8.GetString(data);
            string[] retVal = decodedString.Split(":");
            Console.WriteLine(retVal[0]);
            Console.WriteLine(retVal[1]);
            if (retVal[0] == "admin" && retVal[1] == "12345")
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}
