using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Origin_Building : MonoBehaviour
{
    [HideInInspector]
    public int id;
    [HideInInspector]
    public Vector2 pos = Vector2.one * -1;
	

	public Building_Type kind;
	[HideInInspector]
	public List<Vector2> position; // 추후 타일 등록
    [HideInInspector]
    public Vector2 mainTile;
	
	public int count {
		get {
			return people.Count;
		}
		set {

		}
	}
	public float gaugeRed
	{
		get {
			return infection_Gauge.GetComponent<Infection_Bar>().Return_Gauge() * 100;
		}
		set {

		}
	}
	public float gaugeSup{
		get {
			return Return_Dole() * 100;
		}
		set {

			Revice_Dole(value / 100);

		}
	}
	public bool isRed
	{
		get
		{
			return red;
		}
		set
		{
			red = value;
			if (isRed)
			{
				infection_Gauge.SetActive(true);
				//Button_Event += Out_Infection;
			}
			else
			{
				infection_Gauge.SetActive(false);
				//Button_Event -= Out_Infection;
			}
		}
	}
	private bool red = false;

	public float infection_Gaude_Pos;
    public Vector2 size;
    private Tile tile;
    protected Animator animator;

    protected ArrayList people = new ArrayList();
	private GameObject event_Button;
	protected int max_Count = 0;

	private int IN_PE = 0;
	protected int infecting_People {
		get {
			return IN_PE;
		}
		set {
			IN_PE = value;
			
			if (IN_PE <= 0 && isRed)
				isRed = false;

			else if(IN_PE > 0 && !isRed)
				isRed = true;

			infection_Gauge.GetComponent<Infection_Bar>().fill_Count_txt.text = "+" + IN_PE.ToString();
		}
	}

	protected GameObject infection_Gauge;
	public UnityAction<Origin_Building> Button_Event = null;

	#region 정보 은닉(Return_Dole(), Revice_Dole(float dole))

	protected virtual float Return_Dole()
	{
		
		return 0;

	}

	public virtual void Revice_Dole(float dole = 1)
	{

		return;

	}

	#endregion

	#region 감염 (Create_Infection_Guage(), Infecting_People())

	private void Create_Infection_Guage()
	{

		infection_Gauge = Instantiate(Resources.Load<GameObject>("UI/Infection_Gauge") as GameObject, transform.position, Quaternion.identity);

		infection_Gauge.GetComponent<Canvas>().worldCamera = FindObjectOfType<Camera>();

		infection_Gauge.transform.SetParent(transform);

		infection_Gauge.transform.localPosition = new Vector3(0f, infection_Gaude_Pos, 0f);

		infection_Gauge.GetComponent<Gauge_Bar>().Set_Parent(this);

		infection_Gauge.SetActive(false);

	}

	public void Infecting_People()
	{
	
		for(int i = 0; i < people.Count; i++)
		{
			
			if((people[i] as GameObject).GetComponent<Person>().GetCitizenColor() == CitizenColor.White)
			{

				(people[i] as GameObject).GetComponent<Person>().ChangeColor(CitizenColor.Red);

				infecting_People += 1;

				return;

			}
			
		}
	}

    #endregion

    #region 출입

    public virtual void Summon_People(GameObject unit, NodeType node = (NodeType)10)
	{
        int index = people.IndexOf(unit);

        //건물 위치로 바꾸기
        Person citizen = (people[index] as GameObject).GetComponent<Person>();

        (people[index] as GameObject).transform.position = tile.transform.position;

        (people[index] as GameObject).SetActive(true);
        citizen.StartFSM();

		if (citizen.GetCitizenColor() == CitizenColor.Red)
		{

			infecting_People -= 1;

		}


        if (node != (NodeType)10)
        {
            unit.GetComponent<Person>().GoBulding(BuildingManager.Instance.Get_Building(node));
        }
		animator.SetTrigger("exit");

		SoundManager.Instance.PlaySe(SeEnum.Building_exit);

		people.RemoveAt(index);
		
	}

	
	public virtual void Come_In(GameObject unit)
	{
		people.Add(unit);

        unit.SetActive(false);

		animator.SetTrigger("enter");

		SoundManager.Instance.PlaySe(SeEnum.Building_enter);

		if (unit.GetComponent<Person>().GetCitizenColor() == CitizenColor.Red)
		{

			infecting_People += 1;

		}
	}

	#endregion
		
	#region 버튼

	public void Test(Origin_Building building = null) { }

	public void Call_Action()
	{
        if (Button_Event != null)
        {
			Button_Event(this);
		}
	}
	

	private void Create_Event_Button()
	{

		event_Button = Instantiate(Resources.Load<GameObject>("UI/Event_Button") as GameObject, transform.position, Quaternion.identity);

		event_Button.GetComponent<Canvas>().worldCamera = FindObjectOfType<Camera>();

		event_Button.transform.SetParent(transform);

		event_Button.transform.localPosition = new Vector3(0f, 0f, 0f);

		event_Button.transform.Find("Event").GetComponent<Button>().onClick.AddListener(Call_Action);

	}

	public void Out_Infection(Origin_Building building = null)
	{
		Debug.Log("Out!");
        
		if ((int)Random.Range(0,10) == 0) {

			for (int i = 0; i < people.Count; i++)
			{

				if ((people[i] as GameObject).GetComponent<Person>().GetCitizenColor() == CitizenColor.Red)
				{

					Summon_People(people[i] as GameObject);

					infecting_People -= 1;

					return;

				}

			}
		}
		
	}

	#endregion

	public int Call_Students()
	{
		int count = 0;

		for (int i = people.Count - 1; i >= 0; i--) { 
			
			if ((people[i] as GameObject).GetComponent<Person>().GetCitizenKind() == Citizen_Type.Young)
			{

				Summon_People(people[i] as GameObject,NodeType.School);

				count += 1;
			}
		}

		return count;
	}

    public void ResetPos(Tile tile)
    {
        transform.position = tile.transform.position + new Vector3(size.x * 49.4f / 4, 0f, 0f);
        this.tile = tile;
        pos = tile.pos;
        mainTile = tile.pos;
    }

    protected void Start()
	{

		Create_Infection_Guage();

		Create_Event_Button();

		Button_Event += Test;

        animator = transform.Find("Build").Find("Build_Anim").GetComponent<Animator>();

		BuildingManager buildingManager = FindObjectOfType<BuildingManager>();

        if (buildingManager != null)
		{
			buildingManager.arrayList.Add(this);
		}

		

        //GetComponent<Image>().sortingOrder = -(int)transform.position.y;

	}

}
