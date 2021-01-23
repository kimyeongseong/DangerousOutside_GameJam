using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GiftCntAddItem : BaseItem , IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.Instance.PlaySe(SeEnum.Touch);
        if (!CheckCost())
            return;

        AddCost();

        //구호품 증가 수치
        int addValue = 1;
        cityhallController.GiftCntAdd(addValue);
    }
}
