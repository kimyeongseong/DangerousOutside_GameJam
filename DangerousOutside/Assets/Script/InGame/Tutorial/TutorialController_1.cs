using System.Collections;
using System.Collections.Generic;
using Mobcast.Coffee.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class TutorialController_1 : BaseTutorialController
{
    Apartment building;
    CityhallController cityhallController;

    [SerializeField] AtlasImage handImage;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        SetEvent();
    }

    void SetEvent()
    {
        chatController.chatEndEvent = ChatEndEvent;
    }

    protected override void Init()
    {
        base.Init();

        cityhallController = FindObjectOfType<CityhallController>();
        cityhallController.GiftCntAdd(1);

        StartCoroutine(TutorialStart());
    }

    void BuildingSet()
    {
        BuildingSaveData buildingSaveData = new BuildingSaveData();
        buildingSaveData.buildingType = Building_Type.Small;
        buildingSaveData.pos = new Vector2(7, 11);
        buildingSaveData.chitizen_normal_num = 1;

        building = (Apartment)tileController.BuildingCreate(buildingSaveData);
    }

    IEnumerator TutorialStart()
    {
        BuildingSet();

        chatController.ChatPopupActive(false);
        tutorialObjPanel.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);

        chatController.DataSet(TutorialChatDataManager.Ins.GetData("1_0"));
        yield return new WaitUntil(()=> nextOn);
        nextOn = false;

        tutorialObjPanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        ShowTutorialObj(0);
        yield return new WaitForSeconds(2);

        tutorialObjPanel.gameObject.SetActive(false);
        chatController.DataSet(TutorialChatDataManager.Ins.GetData("1_1"));
        yield return new WaitUntil(() => nextOn);
        nextOn = false;

        //집에서 나오는 로직 구현
        building.AddFood(-5);
        yield return new WaitForSeconds(2);

        building.OutCitizen();
        yield return new WaitForSeconds(2);

        chatController.DataSet(TutorialChatDataManager.Ins.GetData("1_2"));
        nextOn = false;

        yield return new WaitUntil(() => nextOn);

        nextOn = false;
        tutorialObjPanel.gameObject.SetActive(true);
        ShowTutorialObj(1);

        handImage.transform.DOLocalMoveY(-50,1).SetLoops(-1, LoopType.Yoyo);

        yield return new WaitUntil(() => nextOn);
        nextOn = false;

        tutorialObjPanel.gameObject.SetActive(false);
        cityhallController.BuildingClick(building);
        yield return new WaitForSeconds(4);
        
        chatController.DataSet(TutorialChatDataManager.Ins.GetData("1_3"));
        yield return new WaitUntil(() => nextOn);

        nextOn = false;
        tutorialObjPanel.gameObject.SetActive(true);
        ShowTutorialObj(2);
        
        yield return new WaitForSeconds(2);

        tutorialObjPanel.gameObject.SetActive(false);

        chatController.DataSet(TutorialChatDataManager.Ins.GetData("1_4"));

        yield return new WaitUntil(() => nextOn);

        GameManager.Ins.SetTutorialState(1);
        GameManager.Ins.tutorialOn = false;
        GameManager.Ins.StageStartOn(GameManager.Ins.selectStageId,true);
    }

    void ShowTutorialObj(int index)
    {
        for (int i = 0; i < tutorialObjList.Count; i++)
        {
            tutorialObjList[i].gameObject.SetActive(i == index);
        }
    }

    public void ChatEndEvent()
    {
        chatController.ChatPopupActive(false);
        nextOn = true;
    }

    public void BuildingClickEvent()
    {
        nextOn = true;
    }

}


