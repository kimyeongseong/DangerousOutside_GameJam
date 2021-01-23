using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    /// <summary> 타임 오버가 됬을시 실행 </summary>
    public UnityAction timeOverEvent;

    public Text timeText;
    public bool timeOn = false;
    public int secTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        EventSet();
    }

    /// <summary>
    /// 이벤트 설정
    /// </summary>
    void EventSet()
    {
        TimeManager.Ins.secOn += TimeSecOn;
    }
    private void OnDestroy()
    {
        TimeManager.Ins.secOn -= TimeSecOn;
    }

    /// <summary>
    /// 초기화
    /// </summary>
    void Init()
    {
        TimeReset();
    }

    public void TimeStart(int secTime)
    {
        this.secTime = secTime;
        timeOn = true;
    }

    /// <summary>
    /// 시간값 체크
    /// </summary>
    void TimeSecOn()
    {
        if (timeOn == false)
            return;

        secTime--;
        TimeReset();

        if (secTime <= 0)
        {
            secTime = 0;
            timeOn = false;

            if (timeOverEvent != null)
            {
                timeOverEvent();
            }
        }
    }

    /// <summary>
    /// 시간 텍스트 리셋
    /// </summary>
    void TimeReset()
    {
        timeText.text = GetTime();
    }

    public string GetTime()
    {
        int min = (int)(secTime / 60);
        int sec = (int)(secTime % 60);

        return string.Format("{0:D2}:{1:D2}", min, sec);
    }
}
