using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Origin_BathHouse : OutSide
{
    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();

        kind = Building_Type.BathHouse;
    }

	public override void Come_In(GameObject unit)
	{
		base.Come_In(unit);

		unit.GetComponent<Person>().ChangeColor(CitizenColor.Blue);
	}
}
