using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Linq;
using Mobcast.Coffee.UI;

public class StagePageController : MonoBehaviour
{
    public StageAllSaveData stageAllSaveData;
    public UnityAction startEvent = null;
    public int currenPage = 0;
    public int selectStage;

    private List<StageIcon> stageIconObjList = new List<StageIcon>();
    public StageIconData stageData;

    [SerializeField] AtlasImage bg;
    [SerializeField] RectTransform rtBG;

    [SerializeField] AtlasImage leftBtn;
    [SerializeField] AtlasImage rightBtn;

    [SerializeField] StageInfoPopup stageInfoPopup;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        PageDataSet();
        BGSet();
    }

    public void PageDataSet()
    {
        StageIconDataManager.Ins.MaxStageReset();

        List<StageIconData> currentPageDataList = StageIconDataManager.Ins.GetPageData(currenPage);

        foreach (var stageIconObj in stageIconObjList)
        {
            stageIconObj.gameObject.SetActive(false);
        }

        if (stageIconObjList.Count < currentPageDataList.Count)
        {
            for (int i = stageIconObjList.Count; i < currentPageDataList.Count; i++)
            {
                StageIcon stageIcon = Instantiate(Resources.Load<StageIcon>("SelectStage/StageIcon"), this.transform);
                stageIcon.transform.localPosition = currentPageDataList[i].posData;
                stageIconObjList.Add(stageIcon);
                stageIcon.clickEvent += StageStartOn;
            }
        }

        for (int i = 0; i < currentPageDataList.Count; i++)
        {
            stageIconObjList[i].gameObject.SetActive(true);
            stageIconObjList[i].DataSet(currentPageDataList[i]);
            stageIconObjList[i].transform.localPosition = currentPageDataList[i].posData;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StageStartOn(StageIconData stageData)
    {
        SoundManager.Instance.PlaySe(SeEnum.Touch);
        this.stageData = stageData;

        //if (this.stageData.id >= 5)
        //{
        //    WarningManager.Instance.WarningSet("준비중 입니다. 다음 업데이트를 기다려주세요.");
        //}
        //else
        if (this.stageData.id <= StageIconDataManager.Ins.playMaxStageID + 1)
        {
            StageSaveData stageSaveData = stageAllSaveData.stageList.Find(data => data.stageId == this.stageData.id);
            stageInfoPopup.Init(this.stageData, stageSaveData);
            stageInfoPopup.gameObject.SetActive(true);
        }
        else
        {
            WarningManager.Instance.WarningSet("아직은 입장할수 없습니다.");
        }
    }

    public void StartOn()
    {
        if (startEvent != null)
        {
            SoundManager.Instance.PlaySe(SeEnum.Touch);
            startEvent();
        }
    }

    public void PageMove(bool leftOn)
    {
        SoundManager.Instance.PlaySe(SeEnum.Touch);
        if (leftOn)
        {
            currenPage = Mathf.Max(--currenPage,0);
        }
        else
        {
            currenPage = Mathf.Min(++currenPage, StageIconDataManager.Ins.maxPage);
            
        }

        BGSet();
        PageDataSet();
    }

    public void BGSet()
    {
        if (currenPage == 0)
        {
            bg.spriteName = "Img_Map_3";

            rtBG.transform.localPosition = new Vector3(-125.4f, -11.2f, 0);

            rightBtn.gameObject.SetActive(true);
            leftBtn.gameObject.SetActive(false);
        }
        else if (currenPage == 1)
        {
            bg.spriteName = "Img_Map_4";

            rtBG.transform.localPosition = new Vector3(120f, -11.2f, 0);

            leftBtn.gameObject.SetActive(true);
            rightBtn.gameObject.SetActive(false);
        }

        
    }

}
