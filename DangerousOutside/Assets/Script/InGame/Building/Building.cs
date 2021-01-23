using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Building : MonoBehaviour
{
    [System.NonSerialized] public Tile tile;
    public Building_Type building_Type;
    [SerializeField] protected Animator anim;
    protected BuildingSaveData buildingSaveData;
    protected bool initOn = false;
    [System.NonSerialized] public List<Citizen> citizenList = new List<Citizen>();
    protected int maxCitizenCnt;
    public UnityAction<Building> buildingClickOn;

    public void ResetPos(Tile tile)
    {
        this.tile = tile;
        Vector3 tilepos = tile.transform.position;
        transform.position = tilepos;
    }

    public virtual void Init(BuildingSaveData buildingSaveData)
    {
        this.buildingSaveData = buildingSaveData;
        CitizenSet();
    }

    void CitizenSet()
    {
        citizenList = new List<Citizen>();

        for (int i = 0; i < this.buildingSaveData.chitizen_normal_num; i++)
        {
            citizenList.Add(new Citizen(Citizen_Type.Normal,this));
        }

        for (int i = 0; i < this.buildingSaveData.chitizen_old_num; i++)
        {
            citizenList.Add(new Citizen(Citizen_Type.Old, this));
        }

        for (int i = 0; i < this.buildingSaveData.chitizen_The_num; i++)
        {
            citizenList.Add(new Citizen(Citizen_Type.The, this));
        }

        for (int i = 0; i < this.buildingSaveData.chitizen_young_num; i++)
        {
            citizenList.Add(new Citizen(Citizen_Type.Young, this));
        }

    }

    public virtual bool CheckCanInCitizen()
    {
        return false;
    }
    public virtual void ReviceFood()
    {
    }

    public virtual void Enter(Citizen citizen)
    {
        citizenList.Add(citizen);
        anim.SetTrigger("enter");
    }

    public virtual void Exit(Citizen citizen)
    {
        GameManager.Ins.tileController.OutCitizenCreate(citizen,this);
        citizenList.Remove(citizen);
        anim.SetTrigger("exit");
    }


    public virtual void ButtonClick()
    {
        if (buildingClickOn != null)
        {
            buildingClickOn(this);
        }

    }
}
