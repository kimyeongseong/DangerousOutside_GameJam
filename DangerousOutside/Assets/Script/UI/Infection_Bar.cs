using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Infection_Bar : Gauge_Bar
{
	public int fill_Speed = 10;

	private int fill_count = 0;
    [HideInInspector]
	public Text fill_Count_txt;

	/* private new void Start()
	{

		fill.fillAmount = 0f;

		StartCoroutine(Filling());

	}*/

	private void OnEnable()
	{
		base.Start();

		fill_Count_txt = back.transform.Find("Count").GetComponent<Text>();

		fill_count = 0;

		fill.fillAmount = 0f;

		StartCoroutine(Filling());
	}

	IEnumerator Filling()
	{

		for (int i = 0; i < 20; i++)
		{
			fill.fillAmount += 0.25f / 20;
			yield return new WaitForSeconds(0.5f / fill_Speed);
		}

		fill_count += 1;

		if (fill_count >= 4) {
			fill_count = 0;
			fill.fillAmount = 0f;
			parent_Building.Infecting_People();
		}
		
		yield return new WaitForSeconds(10.0f / fill_Speed);

		StartCoroutine(Filling());

	}


}
