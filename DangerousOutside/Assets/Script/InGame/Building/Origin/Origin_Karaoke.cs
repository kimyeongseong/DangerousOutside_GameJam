using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Origin_Karaoke : OutSide
{
    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();

        kind = Building_Type.Karaoke;
    }

	public override void Summon_People(GameObject unit, NodeType node = (NodeType)10)
	{

		base.Summon_People(unit, node);

		if (people.Count == 0)
			animator.SetBool("In", false);

	}

	public override void Come_In(GameObject unit)
	{
		if (people.Count == 0)
		{
			animator.SetBool("In", true);
			SoundManager.Instance.PlaySe(SeEnum.Sing_Sing);
		}
		base.Come_In(unit);
	}
}
