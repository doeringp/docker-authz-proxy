using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthzProxy.Extensions
{
    public static class StringCsvExtensions
    {
        /// <summary>
        /// Convert a comma seperated string to a list of strings.
        /// </summary>
        public static IList<string> ToList(this string csvString, string separator)
        {
            return (csvString ?? "")
                .Split(separator)
                .Select(s => s.Trim())
                .Where(s => s.Length > 0)
                .ToList();
        }
    }
}