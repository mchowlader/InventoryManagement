using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Common
{
    public static class Extension
    {
        public static bool IsNullOrWhiteSpace(this string value)
        {
            if(String.IsNullOrWhiteSpace(value)) return true; return false;
        }

        public static bool IsNullOrEmpty(this string value)
        {
            if (String.IsNullOrEmpty(value)) return true; return false;
        }
    }
}