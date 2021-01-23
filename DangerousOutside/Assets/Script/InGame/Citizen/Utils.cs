using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static T RandomPick<T>(this List<T> list)
    {
        int rand = Random.Range(0, list.Count);
        T picked = list[rand];
        return picked;
    }

    public static T RandomPick<T>(this List<T> list, T exclusion)
    {
        var tempList = new List<T>(list);

        if (tempList.Contains(exclusion))
            tempList.Remove(exclusion);

        int rand = Random.Range(0, tempList.Count);
        T picked = tempList[rand];
        return picked;
    }

    public static Sprite Icon(this EmoticonType emoticon)
    {
        return EmoticonManager.Instance.GetSprite(emoticon);
    }
}
