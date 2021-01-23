using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Editor_Mnr : MonoBehaviour
{
    public StageAllSaveData stageAllSaveData;

    private List<EditorStage> stageList = new List<EditorStage>();
    [SerializeField] Dropdown stageNameDropDown;
    private int currentStageIndex = 0;
    [SerializeField] List<Text> tileTextList = new List<Text>();

    [SerializeField] List<Button> tileBtnList = new List<Button>();
    [SerializeField] Image selectImage;
    int toggleSelectIndex = -1;

    public EditorBuildingInfo editorBuildingInfo;
    [SerializeField] List<Text> citizenTextList = new List<Text>();

    [SerializeField] InputField minClearTime;
    [SerializeField] InputField minRedCitizen;
    [SerializeField] InputField maxRedTile;
    public EditorCitizenInfo editorCitizenInfo;

    private void Start()
    {
        Init();
    }

    void Init()
    {
        LoadOn();
    }

    public void StageCreate()
    {
        EditorStage stage = Instantiate(Resources.Load<EditorStage>("EditorObj/EditorStage"), transform);
        stage.stage_ID = stageList.Count;
        stageList.Add(stage);

        StageNameSet();
        StageChange(stageList.Count - 1);
    }

    void StageNameSet()
    {
        List<Dropdown.OptionData> options = stageNameDropDown.options;
        options.Clear();

        for (int i = 0; i < stageList.Count; i++)
        {
            options.Add(new Dropdown.OptionData() { text = "스테이지 " + (i + 1) });
        }

        stageNameDropDown.options = options;
        stageNameDropDown.value = stageList.Count - 1;


    }

    public void StageChange(int id)
    {
        currentStageIndex = id;
        StageActiveSet();

        ChangeTileOn();
        stageList[currentStageIndex].SetSelectTileType((Tile_Type)toggleSelectIndex);

        BuildingCitizenCountReset();
        ResetCitizensCnt();

        editorBuildingInfo.gameObject.SetActive(false);
        editorCitizenInfo.gameObject.SetActive(false);

        minClearTime.text = stageList[currentStageIndex].minClearTime.ToString();
        minRedCitizen.text = stageList[currentStageIndex].minRedCitizen.ToString();
        maxRedTile.text = stageList[currentStageIndex].maxRedTile.ToString();
    }
    public void StageActiveSet()
    {
        foreach (var stageData in stageList)
        {
            stageData.gameObject.SetActive(stageData.stage_ID == (currentStageIndex));
        }
    }


    public void BuildingCreate(int type)
    {
        stageList[currentStageIndex].Build_Crt((Building_Type)type);
    }

    public void ChangeTileType(int tile_Type)
    {
        //토글 선택 이미지 세팅
        int tempToggleSelectIndex = (tile_Type);

        selectImage.gameObject.SetActive(toggleSelectIndex != tempToggleSelectIndex);

        if (toggleSelectIndex == tempToggleSelectIndex)
        {
            toggleSelectIndex = (int)Tile_Type.None;
        }
        else
        {
            toggleSelectIndex = tempToggleSelectIndex;

            Vector2 targetPos = tileBtnList[toggleSelectIndex - 1].transform.position;
            targetPos.y += 7.5f;
            selectImage.transform.position = targetPos;
        }

        stageList[currentStageIndex].SetSelectTileType((Tile_Type)toggleSelectIndex);
        ChangeTileOn();
    }

    public void ChangeTileOn()
    {
        int whiteCnt = 0;
        int redCnt = 0;
        int blueCnt = 0;

        foreach (var tile in stageList[currentStageIndex].tiles)
        {
            switch (tile.tile_Type)
            {
                case Tile_Type.White:
                    whiteCnt++;
                    break;
                case Tile_Type.Red:
                    redCnt++;
                    break;
                case Tile_Type.Blue:
                    blueCnt++;
                    break;
            }
        }

        tileTextList[(int)Tile_Type.White - 1].text = whiteCnt.ToString();
        tileTextList[(int)Tile_Type.Red - 1].text = redCnt.ToString();
        tileTextList[(int)Tile_Type.Blue - 1].text = blueCnt.ToString();
    }

    public void CurrentBuildChange()
    {
        BuildingCitizenCountReset();
    }
    public void BuildingCitizenCountReset()
    {
        editorCitizenInfo.gameObject.SetActive(false);
        editorBuildingInfo.gameObject.SetActive(true);
        editorBuildingInfo.ChangeBuilding(stageList[currentStageIndex].select_Build_Obj);
    }

    public void CurrentCitizenChange()
    {
        editorBuildingInfo.gameObject.SetActive(false);
        editorCitizenInfo.gameObject.SetActive(true);
        editorCitizenInfo.ChangeCitizen(stageList[currentStageIndex].select_Citizen_Obj);
    }

    public void Citizens_Create(int index)
    {
        stageList[currentStageIndex].Citizens_Crt((Citizen_Type)index);
        ResetCitizensCnt();
    }

    public void ResetCitizensCnt()
    {
        int normalCnt = 0;
        int youngCnt = 0;
        int oldCnt = 0;
        int theCnt = 0;

        foreach (var citizen in stageList[currentStageIndex].citizenList)
        {
            switch (citizen.citizen_Type)
            {
                case Citizen_Type.Normal:
                    normalCnt++;
                    break;
                case Citizen_Type.Young:
                    youngCnt++;
                    break;
                case Citizen_Type.Old:
                    oldCnt++;
                    break;
                case Citizen_Type.The:
                    theCnt++;
                    break;
            }
        }

        citizenTextList[(int)Citizen_Type.Normal].text = normalCnt.ToString();
        citizenTextList[(int)Citizen_Type.Young].text = youngCnt.ToString();
        citizenTextList[(int)Citizen_Type.Old].text = oldCnt.ToString();
        citizenTextList[(int)Citizen_Type.The].text = theCnt.ToString();
    }

    public void MinClearTimeSet()
    {
        string str = minClearTime.text;
        stageList[currentStageIndex].minClearTime = int.Parse(str);

    }

    public void MinRedCitizenSet()
    {
        string str = minRedCitizen.text;
        stageList[currentStageIndex].minRedCitizen = int.Parse(str);
    }

    public void MaxRedTileSet()
    {
        string str = maxRedTile.text;
        stageList[currentStageIndex].maxRedTile = int.Parse(str);
    }


    public void SaveOn()
    {
        //StageAllSaveData stageAllSaveData = Resources.Load<StageAllSaveData>("SaveData/StageAllSaveData");
        stageAllSaveData.stageList = new List<StageSaveData>();

        foreach (var stage in stageList)
        {
            StageSaveData stageSaveData = new StageSaveData();

            List<TileSaveData> tileSaveDataList = new List<TileSaveData>();
            foreach (var tile in stage.tiles)
            {
                tileSaveDataList.Add(new TileSaveData() { pos = tile.pos, tile_Type = tile.tile_Type });
            }

            List<CitizenSaveData> citizenSaveDataList = new List<CitizenSaveData>();
            foreach (var citizen in stage.citizenList)
            {
                if (citizen.pos == Vector2.one * -1)
                    continue;

                citizenSaveDataList.Add(new CitizenSaveData()
                {
                    pos = citizen.pos,
                    citizen_Type = citizen.citizen_Type,
                    id = citizen.id,
                    citizenColor = citizen.citizen_color
                });
            }

            List<BuildingSaveData> buildingSaveDataList = new List<BuildingSaveData>();
            foreach (var building in stage.buildingList)
            {
                if (building.pos == Vector2.one * -1)
                    continue;

                buildingSaveDataList.Add(new BuildingSaveData()
                {
                    id = building.id,
                    pos = building.pos,
                    buildingType = building.object_Build,
                    buildSize = building.buildSize,
                    chitizen_normal_num = building.chitizen_normal_num,
                    chitizen_young_num = building.chitizen_young_num,
                    chitizen_old_num = building.chitizen_old_num,
                    chitizen_The_num = building.chitizen_The_num
                });
            }

            stageSaveData.stageId = stage.stage_ID;
            stageSaveData.minClearTime = stage.minClearTime;
            stageSaveData.minRedCitizen = stage.minRedCitizen;
            stageSaveData.maxRedTile = stage.maxRedTile;
            stageSaveData.tileList = tileSaveDataList;
            stageSaveData.citizenList = citizenSaveDataList;
            stageSaveData.buildingList = buildingSaveDataList;

            stageAllSaveData.stageList.Add(stageSaveData);
        }
#if UNITY_EDITOR
        EditorUtility.SetDirty(stageAllSaveData);
        AssetDatabase.SaveAssets();
#endif
    }

    public void LoadOn()
    {
        //StageAllSaveData stageAllSaveData = Resources.Load<StageAllSaveData>("SaveData/StageAllSaveData");
        if (stageAllSaveData != null)
        {
            StartCoroutine(LoadDoing(stageAllSaveData));
        }
    }

    IEnumerator LoadDoing(StageAllSaveData stageAllSaveData)
    {
        int before_currentStageIndex = currentStageIndex;

        foreach (var stage in stageList)
        {
            Destroy(stage.gameObject, 0.1f);
        }
        stageList.Clear();

        foreach (var stageSaveData in stageAllSaveData.stageList)
        {
            StageCreate();

            stageList[currentStageIndex].minClearTime = stageSaveData.minClearTime;
            stageList[currentStageIndex].minRedCitizen = stageSaveData.minRedCitizen;
            stageList[currentStageIndex].maxRedTile = stageSaveData.maxRedTile;

            while (stageList[currentStageIndex].tiles.Count == 0)
            {
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForEndOfFrame();

            foreach (var tile in stageSaveData.tileList)
            {
                if (tile.tile_Type != Tile_Type.None && tile.tile_Type != Tile_Type.White)
                {
                    EditorTile editorTile = stageList[currentStageIndex].tiles.Find(tileobj => tileobj.pos == tile.pos);

                    if (editorTile != null)
                    {
                        editorTile.TileChange(tile.tile_Type);
                    }
                }
            }
            ChangeTileOn();

            foreach (var citizen in stageSaveData.citizenList)
            {
                EditorCitizen editorCitizen = stageList[currentStageIndex].Citizens_Crt(citizen.citizen_Type);
                editorCitizen.pos = citizen.pos;
                editorCitizen.citizen_Type = citizen.citizen_Type;
                editorCitizen.id = citizen.id;
                editorCitizen.SetColor(citizen.citizenColor);
                //editorCitizen.citizen_color = citizen.citizenColor;

                EditorTile editorTile = stageList[currentStageIndex].tiles.Find(tileobj => tileobj.pos == citizen.pos);
                if (editorTile != null)
                {
                    editorCitizen.ResetPos(editorTile);
                }
            }
            ResetCitizensCnt();

            foreach (var building in stageSaveData.buildingList)
            {
                EditorBuilding editorBuilding = stageList[currentStageIndex].Build_Crt(building.buildingType);
                editorBuilding.pos = building.pos;
                stageList[currentStageIndex].BuildIDReset();

                editorBuilding.chitizen_normal_num = building.chitizen_normal_num;
                editorBuilding.chitizen_young_num = building.chitizen_young_num;
                editorBuilding.chitizen_old_num = building.chitizen_old_num;
                editorBuilding.chitizen_The_num = building.chitizen_The_num;

                EditorTile editorTile = stageList[currentStageIndex].tiles.Find(tileobj => tileobj.pos == building.pos);
                if (editorTile != null)
                {
                    editorBuilding.ResetPos(editorTile);
                }
            }
        }

        stageNameDropDown.value = before_currentStageIndex;
    }
}
