using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeliveryItem : BaseItem, IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        needCost = 7;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!CheckCost())
            return;

        SoundManager.Instance.PlaySe(SeEnum.UseRocket);
        AddCost();

        cityhallController.AllHomeBuildingGiftOn();

    }
}
