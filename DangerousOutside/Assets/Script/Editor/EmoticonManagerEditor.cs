using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(EmoticonManager))]
public class EmoticonManagerEditor : Editor
{
    EmoticonManager component;

    void OnEnable()
    {
        component = target as EmoticonManager;

        foreach (EmoticonType emoticon in Enum.GetValues(typeof(EmoticonType)))
        {
            if (component.EmoticonList.Find(x => x.Emoticon == emoticon) == null)
                component.EmoticonList.Add(new EmoticonManager.EmoticonInfo { Emoticon = emoticon });
        }
        component.EmoticonList.Sort((x, y) => x.Emoticon.CompareTo(y.Emoticon));
    }

    public override void OnInspectorGUI()
    {
        OnEnable();

        foreach (EmoticonManager.EmoticonInfo item in component.EmoticonList)
        {
            item.Image = (Sprite)EditorGUILayout.ObjectField(item.Emoticon.ToString(), item.Image, typeof(Sprite), false);
        }

        serializedObject.ApplyModifiedProperties();
        if (GUI.changed)
            EditorUtility.SetDirty(component);
    }
}
