using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutSide : Origin_Building
{
    public float min_Time = 8;

	public float max_Time = 15;

	private new void Start()
    {
        base.Start();
    }

	public override void Come_In(GameObject unit)
	{
		base.Come_In(unit);

		StartCoroutine(Out_Time(unit));
	}

	IEnumerator Out_Time(GameObject unit)
    {

        yield return new WaitForSeconds(Random.Range(min_Time, max_Time + 1));

        Summon_People(unit);
    }
}
