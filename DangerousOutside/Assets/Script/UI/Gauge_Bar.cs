using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gauge_Bar : MonoBehaviour
{
	protected Origin_Building parent_Building;

	protected GameObject back;

	protected Image fill;

	public void Set_Parent(Origin_Building parent)
	{

		parent_Building = parent;

	}

	protected void Start()
	{

		back = transform.Find("BackGround").gameObject;

		fill = back.transform.Find("Fill").GetComponent<Image>();

	}

	public virtual float Return_Gauge()
	{

		return fill.fillAmount;

	}
}
