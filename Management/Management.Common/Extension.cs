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
        public static long ToInt64(this object o)
        {
            try
            {
                return Convert.ToInt64(o);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static string IsNullOrEmpty(this string s, string s2)
        {
            if(String.IsNullOrWhiteSpace(s))
            {
                return s2;
            }
            else
            {
                return s;
            }
        }
        public static string IsNullOrWhiteSpace(this string s, string s2)
        {
            if (String.IsNullOrWhiteSpace(s))
            {
                return s2;
            }
            else
            {
                return s;
            }
        }
        public static string ToCommaSeparatedString(this List<string> strList)
        {
            var str = String.Empty;
            if(strList.Count > 0)
            {
                foreach(var _str in strList)
                {
                    var index = strList.FindIndex(x => x == _str);
                    if(index < (strList.Count - 1))
                    {
                        str = _str + _str + ",";
                    }
                    else if(index == (strList.Count - 1))
                    {
                        str= _str + ",";
                    }
                }
            }
            return str;
        }
    }
}