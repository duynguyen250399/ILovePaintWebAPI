using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ILovePaintWebAPI.Helpers
{
    public class Utils
    {
        public static string ImagePathToLink(int id)
        {
            return $"https://localhost:44385/api/images/product/{id}";
        }
    }
}
