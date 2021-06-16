using System;

namespace Resources.Scripts.Utils
{
    public static class Functions
    {
        // Due to constraint of C# 7, R is not nullable
        public static R Let<T, R>(this T obj, Func<T, R> block)
        {
            return obj != null ? block(obj) : default;
        }

        public static void Let<T>(this T obj, Action<T> block)
        {
            if (obj != null)
            {
                block(obj);
            }
        }

        public static T Apply<T>(this T obj, Action<T> block)
        {
            if (obj != null)
            {
                block(obj);
            }
            return obj;
        }
    }
}