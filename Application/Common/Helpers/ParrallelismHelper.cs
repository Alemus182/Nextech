﻿namespace Application.Common.Helpers
{
    public static class ParallelismHelper
    {
        public static async Task<IList<TResult>> SelectAsync<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, Task<TResult>> selector, int maxDegreesOfParallelism = 4)
        {
            var results = new List<TResult>();

            var activeTasks = new HashSet<Task<TResult>>();
            foreach (var item in source)
            {
                activeTasks.Add(selector(item));
                if (activeTasks.Count >= maxDegreesOfParallelism)
                {
                    var completed = await Task.WhenAny(activeTasks);
                    activeTasks.Remove(completed);
                    results.Add(completed.Result);
                }
            }

            results.AddRange(await Task.WhenAll(activeTasks));
            return results;
        }

    }
}
