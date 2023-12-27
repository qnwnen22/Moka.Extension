using System;
using System.Collections.Generic;
using System.Linq;

namespace Moka.Extension
{
    public static class CollectionsExpansion
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> iEnumerable)
        {
            if (iEnumerable == null) return true;
            return iEnumerable.Count() <= 0;
        }
        public static string Join<T>(this IEnumerable<T> iEnumerable, string separator = "")
        {
            return string.Join(separator, iEnumerable);
        }
        public static bool Exists<T>(this IEnumerable<T> iEnumerable, Predicate<T> match)
        {
            return iEnumerable.ToList().Exists(match);
        }
    }
}
