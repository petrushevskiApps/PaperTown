using System.Collections.Generic;

public static class ListUtils
{
    public static T Random<T>(this List<T> list)
    {
        return list[UnityEngine.Random.Range(0, list.Count)];
    }
}
