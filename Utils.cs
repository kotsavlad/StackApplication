using System;
using System.Collections.Generic;

namespace StackApplication
{
    public static class Utils
    {
        public static string Stringify<T>(this IEnumerable<T> collection, char leftBracket = '[',
            char rightBracket = ']') =>
            $"{leftBracket}{string.Join(", ", collection)}{rightBracket}";

        public static void Print<T>(this IEnumerable<T> collection) =>
            Console.WriteLine(collection.Stringify());
    }
}