using UnityEditor;

public partial class StageAllSaveDataCreater
{
    [MenuItem("Assets/Create/StageAllSaveDataCreate")]
    public static void CreateDataSheetAssetFile()
    {
        StageAllSaveData asset = CustomAssetUtility.CreateAsset<StageAllSaveData>();
        EditorUtility.SetDirty(asset);
    }
}