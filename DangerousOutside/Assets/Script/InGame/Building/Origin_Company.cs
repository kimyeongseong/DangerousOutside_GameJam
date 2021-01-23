using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Origin_Company : OutSide
{
    TaxController tax;

    bool work = false;

    float image_Change = 0;

    //Image image;

    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();

		Button_Event += Show_Me_The_Money;

        tax = FindObjectOfType<TaxController>();

        //image = transform.Find("Image").GetComponent<Image>();

        StartCoroutine(Company_Light_Check());

        kind = Building_Type.Company;
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
			animator.SetBool("In", true);

		base.Come_In(unit);
	}

	private IEnumerator Company_Light_Check()
    {
        yield return new WaitForSeconds(0.1f);

        if (image_Change < 0 && work)
        {
            //image.sprite = Resources.Load<SpriteAtlas>("Building/Sprite/Buildings").GetSprite("Object_Company") as Sprite;

            work = false;
        }
        else
            image_Change -= 0.1f;

        StartCoroutine(Company_Light_Check());

    }

    private void Show_Me_The_Money(Origin_Building building = null)
	{

		animator.SetTrigger("touch");

		SoundManager.Instance.PlaySe(SeEnum.Office_Touch);

        work = true;

        image_Change = 1f;

        tax.TouchOffice();

	}
}
