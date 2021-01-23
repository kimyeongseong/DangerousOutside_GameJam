using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dole_Bar : Gauge_Bar
{
	public int out_Speed = 10;

	private float gauge = 1f;

	private new void Start()
	{
		base.Start();

		fill.fillAmount = 1f;
	}

	public void FixedUpdate()
	{
        if(gauge > 0)
		    gauge -= out_Speed / 1000f * Time.deltaTime;

		fill.fillAmount = gauge;

	}

	public override float Return_Gauge()
	{

		return gauge;

	}

	public void Revice_Gauge(float fill)
	{
        if (gauge < 1)
            gauge = fill;
        else
            gauge = 1f;

		this.fill.fillAmount = fill;

	}
}
