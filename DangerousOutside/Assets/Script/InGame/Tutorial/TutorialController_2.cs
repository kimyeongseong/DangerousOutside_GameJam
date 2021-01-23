using System.Collections;
using System.Collections.Generic;
using Mobcast.Coffee.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class TutorialController_2 : BaseTutorialController
{
    CleanerController cleanerController;

    List<Vector2> redTilePosList = new List<Vector2>();
    List<Citizen> citizenList = new List<Citizen>();

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

        cleanerController = FindObjectOfType<CleanerController>();

        RedTilePosSet();

        StartCoroutine(TutorialStart());
    }

    void RedTilePosSet()
    {
        redTilePosList = new List<Vector2>()
        {
            new Vector2(5, 14),
            new Vector2(5, 8),
            new Vector2(10, 14),
            new Vector2(10, 8)
        };
    }

    void TileSet()
    {
        foreach (var redTilePos in redTilePosList)
        {
            tileController.GetTile(redTilePos).TileChange(Tile_Type.Red);
        }
    }

    void CitizenSet()
    {
        for (int i = 0; i < redTilePosList.Count; i++)
        {
            CitizenSaveData citizenSaveData = new CitizenSaveData();
            citizenSaveData.citizen_Type = Citizen_Type.Normal;
            citizenSaveData.citizenColor = CitizenColor.White;

            Vector2 pos = redTilePosList[i];

            switch (i)
            {
                case 0:
                    pos.x -= 1;
                    break;
                case 1:
                    pos.y -= 1;
                    break;
                case 2:
                    pos.y += 1;
                    break;
                case 3:
                    pos.x += 1;
                    break;

            }

            citizenSaveData.pos = pos;

            citizenList.Add(tileController.CitizenCreate(citizenSaveData));
        }
    }

    IEnumerator TutorialStart()
    {
        TileSet();
        CitizenSet();

        handImage.gameObject.SetActive(false);
        chatController.ChatPopupActive(false);
        tutorialObjPanel.gameObject.SetActive(false);
        yield return new WaitForSeconds(2);


        CitizenMove();
        yield return new WaitForSeconds(2);

        CitizenMove();
        yield return new WaitForSeconds(2);

        chatController.DataSet(TutorialChatDataManager.Ins.GetData("2_0"));
        yield return new WaitUntil(()=> nextOn);
        nextOn = false;

        tutorialObjPanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        ShowTutorialObj(0);
        yield return new WaitForSeconds(2);

        tutorialObjPanel.gameObject.SetActive(false);
        nextOn = false;
        chatController.DataSet(TutorialChatDataManager.Ins.GetData("2_1"));
        yield return new WaitUntil(() => nextOn);
        nextOn = false;

        tutorialObjPanel.gameObject.SetActive(true);
        ShowTutorialObj(0);
        handImage.gameObject.SetActive(true);
        handImage.transform.DOLocalMoveY(-50, 1).SetLoops(-1, LoopType.Yoyo);

        yield return new WaitUntil(() => nextOn);
        tutorialObjPanel.gameObject.SetActive(false);

        cleanerController.CleanerClickOn();
        yield return new WaitForSeconds(2);

        chatController.DataSet(TutorialChatDataManager.Ins.GetData("2_2"));
        nextOn = false;

        yield return new WaitUntil(() => nextOn);

        GameManager.Ins.SetTutorialState(2);
        GameManager.Ins.tutorialOn = false;
        GameManager.Ins.StageStartOn(GameManager.Ins.selectStageId, true);
    }

    void CitizenMove()
    {
        for (int i = 0; i < citizenList.Count; i++)
        {
            Vector2 pos = citizenList[i].currentTile.pos;

            switch (i)
            {
                case 0:
                    pos.x += 1;
                    break;
                case 1:
                    pos.y += 1;
                    break;
                case 2:
                    pos.y -= 1;
                    break;
                case 3:
                    pos.x -= 1;
                    break;

            }

            citizenList[i].MoveDoing(tileController.GetTile(pos), null);
        }
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

    public void ButtonClickEvent()
    {
        nextOn = true;
    }

}


