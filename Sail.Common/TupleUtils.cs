using System.Runtime.CompilerServices;

namespace Sail;

public class TupleUtils
{
    // can return a null only when Activator.CreateInstance can return null
    // in other words, only when T is Nullable<whatever>, and tuple is 0-element
    public T? ActivateWithTuple<T>(T tuple)
        where T : ITuple
    {
        var t = tuple as ITuple;
        var a = new object?[t.Length];
        for (int i = 0; i < t.Length; i++)
        {
            a[i] = t[i];
        }
        return (T?)Activator.CreateInstance(typeof(T), a);
    }
}