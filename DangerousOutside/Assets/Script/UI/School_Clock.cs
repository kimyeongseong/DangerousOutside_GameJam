using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class School_Clock : Gauge_Bar
{
	private float max_Time = 10f;

    private Image back;

	private float gauge = 0;

	private new void Start()
	{
		base.Start();

		fill.fillAmount = 1f;

        back = transform.Find("LeftTime").GetComponent<Image>();

		StartCoroutine(Time_Goes_On());
	}

	public void Revice_Time(float time)
	{

		max_Time = time;

	}

	public IEnumerator Time_Goes_On()
	{

		yield return new WaitForSeconds(max_Time / 36);

		gauge += 1.00f / 36;

		fill.transform.localRotation = Quaternion.Euler(0f, 0f, -gauge * 360f);

        back.fillAmount = gauge;

		if (gauge > 1f)
		{
			gauge = 0f;

			StartCoroutine(parent_Building.GetComponent<Origin_School>().School_In_Out());

			fill.transform.localRotation = Quaternion.Euler(0f, 0f, 0);

            back.fillAmount = 0;
        }
		else
			StartCoroutine(Time_Goes_On());

	}
}
