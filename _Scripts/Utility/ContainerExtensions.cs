using System.Collections;
using System.Collections.Generic;
using Godot;
using Godot.Collections;

public static class ContainerExtensions
{
    public static bool IsNullOrEmpty(this Array array)
    {
        return array == null || array.Count == 0;
    }

    public static bool IsNullOrEmpty(this ArrayList arrayList)
    {
        return arrayList == null || arrayList.Count == 0;
    }
    
    public static bool IsNullOrEmpty(this Dictionary dictionary)
    {
        return dictionary == null || dictionary.Count == 0;
    }
    
    public static bool IsNullOrEmpty<T>(this HashSet<T> set)
    {
        return set == null || set.Count == 0;
    }
    
    public static bool IsNullOrEmpty<[MustBeVariant]T>(this Array<T> array)
    {
        return array == null || array.Count == 0;
    }

    public static T PopBack<[MustBeVariant]T>(this Array<T> arr)
    {
        var last = arr[^1];
        arr.RemoveAt(arr.Count-1);
        return last;
    }
    
    public static T PopBack<T>(this List<T> arr)
    {
        var last = arr[^1];
        arr.RemoveAt(arr.Count-1);
        return last;
    }
    
    public static object PopBack(this ArrayList arr)
    {
        var last = arr[^1];
        arr.RemoveAt(arr.Count-1);
        return last;
    }

    public static Array<Node> GetAllChildren(this Node root, bool includeInternal=false)
    {
        var children = root.GetChildren(includeInternal);
        var results = children;
        foreach (var n in children)
        {
            results.AddRange(n.GetAllChildren(includeInternal));
        }
        return results;
    }
}