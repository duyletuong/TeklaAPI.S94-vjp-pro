using System;
using System.Collections;
using System.Collections.Generic;

namespace DLT.Plugin.Core.Extension
{
    public static class IEnumeratorExtension
    {
        /// <summary>
        /// Convet enumerator to enumerable for LINQ queries
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerator"></param>
        /// <returns></returns>
        public static IEnumerable<T> ToIEnumerable<T>(this IEnumerator enumerator) where T : class
        {
            enumerator.Reset();
            while (enumerator.MoveNext())
            {
                T @object = null;
                try
                {
                    @object = enumerator.Current as T; //Only this way working with broken models in Tekla
                }
                catch (Exception)
                { }

                if (@object != null)
                {
                    yield return @object;
                }
            }
        }
    }
}
