using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TutorialImageAllData : ScriptableObject
{
    public List<TutorialImageData> tutorialImageDataList = new List<TutorialImageData>();
}

[Serializable]
public class TutorialImageData
{
    public int stageID;
    public List<Sprite> imageList = new List<Sprite>();
}
