using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorCitizenInfo : MonoBehaviour
{
    public List<Toggle> selectToggleList = new List<Toggle>();
    public Text nameText;
    EditorCitizen citizen;
    int selectIndex;

    // Start is called before the first frame update
    void Start()
    {
        EventSet();
        Init();
    }

    void EventSet()
    {
        for (int i = 0; i < selectToggleList.Count; i++)
        {
            int index = i;
            selectToggleList[i].onValueChanged.RemoveAllListeners();
            selectToggleList[i].onValueChanged.AddListener(isOn =>
            {
                if (isOn == true)
                {
                    ToggleValueChange(index);
                }       
            });
        }
    }

    void Init()
    {
        
    }

    public void ChangeCitizen(EditorCitizen citizen)
    {
        this.citizen = citizen;
        nameText.text = this.citizen.GetName();
        selectToggleList[(int)citizen.citizen_color].isOn = true;
    }

    public void ToggleValueChange(int index)
    {
        selectIndex = index;

        if (this.citizen != null)
        {
            this.citizen.SetColor((CitizenColor)index);
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeleteCitizenOn()
    {
        if (citizen != null)
        {
            citizen.DeleteOn();
        }

        gameObject.SetActive(false);
    }
}
