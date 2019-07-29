using System;
using System.Collections.Generic;

#nullable enable
namespace CSharp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello Stalin!");
            WriteStalinSort(new[] { 4 });
            WriteStalinSort(new[] { 6, 2, 5, 7, 3, 8, 8, 4 });
            WriteStalinSort(new[] { 5, 3, 7, 8, 9, 5, 3, 5, 7 });
            /**
             * Hello Stalin!
             * Input: 4
             * StalinBy: 4
             * StalinByDescending: 4
             * Input: 6, 2, 5, 7, 3, 8, 8, 4
             * StalinBy: 6, 7, 8, 8
             * StalinByDescending: 6, 2
             * Input: 5, 3, 7, 8, 9, 5, 3, 5, 7
             * StalinBy: 5, 7, 8, 9
             * StalinByDescending: 5, 3, 3
             */
        }

        public static void WriteStalinSort(int[] source)
        {
            Console.WriteLine($"Input: {string.Join(", ", source)}");
            Console.WriteLine($"StalinBy: {string.Join(", ", source.StalinBy(x => x))}");
            Console.WriteLine($"StalinByDescending: {string.Join(", ", source.StalinByDescending(x => x))}");
        }
    }

    public static class StalinSort
    {
        public static IReadOnlyList<TSource> StalinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.StalinBy(keySelector, Comparer<TKey>.Default);
        }

        public static IReadOnlyList<TSource> StalinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            return stalinSort(source, keySelector, comparer, false);
        }

        public static IReadOnlyList<TSource> StalinByDescending<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.StalinByDescending(keySelector, Comparer<TKey>.Default);
        }

        public static IReadOnlyList<TSource> StalinByDescending<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            return stalinSort(source, keySelector, comparer, true);
        }

        private static IReadOnlyList<TSource> stalinSort<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            IList<TSource> result = new List<TSource>();
            using IEnumerator<TSource> enumerator = source.GetEnumerator();

            if (enumerator.MoveNext())
            {
                TSource element = enumerator.Current;
                TKey lastKey = keySelector(element);

                result.Add(element);

                while (enumerator.MoveNext())
                {
                    element = enumerator.Current;
                    TKey newKey = keySelector(element);
                    int compare = descending ? comparer.Compare(lastKey, newKey) : comparer.Compare(newKey, lastKey);

                    if (0 <= compare)
                    {
                        result.Add(element);
                        lastKey = newKey;
                    }
                }
            }

            return (IReadOnlyList<TSource>)result;
        }
    }
}
