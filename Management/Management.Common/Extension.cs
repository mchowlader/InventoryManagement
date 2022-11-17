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
        public static string Take(this string s, int i)
        {
            if(i >= s.Length) return s; return s.Substring(0,i);
        }
        public static long ToInt32(this object o)
        {
            try
            {
                return Convert.ToInt32(o);
            }
            catch (Exception)
            {

                return 0;
            }
        }
    }
}