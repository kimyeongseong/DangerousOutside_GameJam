using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Company : Building
{
    TaxController tax;

    // Start is called before the first frame update
    void Start()
    {
        tax = FindObjectOfType<TaxController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ButtonClick()
    {
        base.ButtonClick();
        anim.SetTrigger("touch");
        SoundManager.Instance.PlaySe(SeEnum.Office_Touch);
        tax.TouchOffice();
    }
}
