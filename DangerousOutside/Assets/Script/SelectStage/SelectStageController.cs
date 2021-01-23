using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectStageController : MonoBehaviour
{
    public ChallengeCntController challengeCntController;
    public StagePageController stagePageController;
    public SettingPopup settingPopup;

    // Start is called before the first frame update
    void Start()
    {
        EventSet();
        SoundManager.Instance.PlayBGM(BGMEnum.StageSelect);
    }

    void EventSet()
    {
        stagePageController.startEvent += GameStartOn;
    }

    public void SettingBtnClickOn()
    {
        SoundManager.Instance.PlaySe(SeEnum.Touch);
        settingPopup.gameObject.SetActive(true);
    }

    void GameStartOn()
    {
        if (GameManager.Ins.challengeCurrentCnt <= 0)
            return;

        challengeCntController.CountAddOn(-1);

        GameManager.Ins.StageStartOn(stagePageController.stageData.id);        
    }

    public void GameExitOn()
    {
        SoundManager.Instance.PlaySe(SeEnum.Touch);
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
