using System.Collections.Generic;
using UnityEngine;

public enum EmoticonType
{
    House,
    BathHouse,
    Church,
    Karaoke,
    School,
    Company,
    Walk,
    Question,
}

public class EmoticonManager : MonoSingleton<EmoticonManager>
{
    [System.Serializable]
    public class EmoticonInfo
    {
        public EmoticonType Emoticon;
        public Sprite Image;
    }

    public List<EmoticonInfo> EmoticonList = new List<EmoticonInfo>();

    public Sprite GetSprite(EmoticonType emoticon)
    {
        EmoticonInfo info = EmoticonList.Find(x => x.Emoticon == emoticon);
        if (info != null && info.Image != null)
            return info.Image;
        return null;
    }
}