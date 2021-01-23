using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorBuildingInfo : MonoBehaviour
{
    private EditorBuilding building;
    [SerializeField] Text buildNameText;
    [SerializeField] InputField normalInput;
    [SerializeField] InputField youngInput;
    [SerializeField] InputField oldInput;
    [SerializeField] InputField theInput;

    public void ChangeBuilding(EditorBuilding building)
    {
        this.building = building;
        CountReset();
        BuildNameReset();
    }
    public void CountReset()
    {
        InputReset();
    }

    public void Chitizen_Normal_Num_ReSet(bool up)
    {
        Chitizen_Num_Reset(Citizen_Type.Normal, up);
    }
    public void Chitizen_Young_Num_ReSet(bool up)
    {
        Chitizen_Num_Reset(Citizen_Type.Young, up);
    }
    public void Chitizen_Old_Num_ReSet(bool up)
    {
        Chitizen_Num_Reset(Citizen_Type.Old, up);
    }
    public void Chitizen_The_Num_ReSet(bool up)
    {
        Chitizen_Num_Reset(Citizen_Type.The, up);
    }

    public void Chitizen_Num_Reset(Citizen_Type citizen_Type , bool up)
    {
        int addValue = up ? 1 : -1;
        switch (citizen_Type)
        {
            case Citizen_Type.Normal:
                building.chitizen_normal_num = Mathf.Max(building.chitizen_normal_num += addValue, 0);
                break;
            case Citizen_Type.Young:
                building.chitizen_young_num = Mathf.Max(building.chitizen_young_num += addValue, 0);
                break;
            case Citizen_Type.Old:
                building.chitizen_old_num = Mathf.Max(building.chitizen_old_num += addValue, 0);
                break;
            case Citizen_Type.The:
                building.chitizen_The_num = Mathf.Max(building.chitizen_The_num += addValue, 0);
                break;
        }

        InputReset();
    }

    void InputReset()
    {
        if (building == null)
        {
            normalInput.text = string.Empty;
            youngInput.text = string.Empty;
            oldInput.text = string.Empty;
            theInput.text = string.Empty;
            return;
        }
        
        normalInput.text = building.chitizen_normal_num.ToString();
        youngInput.text = building.chitizen_young_num.ToString();
        oldInput.text = building.chitizen_old_num.ToString();
        theInput.text = building.chitizen_The_num.ToString();
    }


    public void Chitizen_Normal_InputOn()
    {
        building.chitizen_normal_num = int.Parse(normalInput.text);
    }
    public void Chitizen_Young_InputOn()
    {
        building.chitizen_young_num = int.Parse(youngInput.text);
    }
    public void Chitizen_Old_InputOn()
    {
        building.chitizen_old_num = int.Parse(oldInput.text);
    }
    public void Chitizen_The_InputOn()
    {
        building.chitizen_The_num = int.Parse(theInput.text);
    }

    public void BuildNameReset()
    {
        if (this.building == null)
        {
            buildNameText.text = string.Empty;
            return;
        }

        buildNameText.text = this.building.GetBuildName();
    }

    public void DeleteBuildOn()
    {
        if (building != null)
        {
            building.DeleteOn();
        }

        gameObject.SetActive(false);
    }

}
