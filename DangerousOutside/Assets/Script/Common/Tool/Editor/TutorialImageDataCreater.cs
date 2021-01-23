using UnityEditor;

public partial class TutorialImageDataCreater
{
    [MenuItem("Assets/Create/TutorialImageDataCreate")]
    public static void CreateDataSheetAssetFile()
    {
        TutorialImageAllData asset = CustomAssetUtility.CreateAsset<TutorialImageAllData>();
        EditorUtility.SetDirty(asset);
    }
}