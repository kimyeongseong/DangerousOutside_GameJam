using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 시간을 관리하는 클래스
/// </summary>
public class TimeManager : MonoBehaviour
{
	private static TimeManager ins;
	public static TimeManager Ins
	{
		get
		{
			if (ins == null)
			{
				ins = FindObjectOfType<TimeManager>();

				if (ins == null)
				{
					ins = new GameObject("TimeManager").AddComponent<TimeManager>();
					DontDestroyOnLoad(ins.gameObject);
				}
			}
			return ins;
		}
	}

	private float timeValue = 0;
	private int timeIntValue = 0;


	public UnityAction secOn;
	public UnityAction minOn;

    // Update is called once per frame
    void Update()
    {
		TimeCheck();
	}

	void TimeCheck()
    {
		timeValue += Time.deltaTime;

		if (timeIntValue != (int)timeValue)
		{
			timeIntValue = (int)timeValue;

            //1초가 지날때마다 실행
			if (secOn != null)
            {
				secOn();
			}

            //1분이 지날때마다 실행
			if (timeIntValue%60 == 0 && minOn != null)
            {
				minOn();
			}
            
		}
	}
}
