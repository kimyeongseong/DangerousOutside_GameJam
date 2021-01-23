using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ForceGoHomeItem : BaseItem, IPointerClickHandler
{
    bool forceGoHomeOn = false;
    [SerializeField] GameObject image;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        needCost = 3;
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //SoundManager.Instance.PlaySe(SeEnum.Touch);
        //if (!CheckCost())
        //    return;

        //if (forceGoHomeOn == false)
        //{
            
        //}
        //else
        //{
            
        //}

        //forceGoHomeOn = !forceGoHomeOn;
        //ImageReset();
    }

    public void ImageReset()
    {
        image.gameObject.SetActive(forceGoHomeOn);
    }

    public void CitizenClickOn(Person person)
    {
        SoundManager.Instance.PlaySe(SeEnum.Touch);
        if (forceGoHomeOn)
        {
            forceGoHomeOn = false;
            ImageReset();

            if (!CheckCost())
                return;

            //강제 귀가 시키기
//            person.GoBulding(person.Home);

            AddCost();
        }
    }

}
