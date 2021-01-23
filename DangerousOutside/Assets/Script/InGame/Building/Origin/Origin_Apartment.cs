using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Origin_Apartment : Origin_Building
{
	public enum Level { Small = 0, Middle = 1, Large = 2} // 아파트 크기 분류

	public Level started_Level;

	private Level current_Level;

	//private Image sprite;

	private Dole_Bar dole_Bar;

	public Animator[] animators = new Animator[3];
	
	
	new protected void Start()
	{
		base.Start();

		
		GameObject clone = transform.Find("Build").gameObject;

		animators[0] = clone.transform.Find("Build_Anim").GetComponent<Animator>();
		animators[1] = clone.transform.Find("Build_Anim_m").GetComponent<Animator>();
		animators[2] = clone.transform.Find("Build_Anim_l").GetComponent<Animator>();

		//초기화
		current_Level = started_Level;

        StartCoroutine(Create_Delay());

		Create_Dole_Guage();

		if (SceneManager.GetActiveScene().name != "InGame")
		{
			Button_Event += Revice_Dole;
		}
	}

    IEnumerator Create_Delay()
    {
        yield return new WaitForSeconds(0.1f);

        Level_Up();

		BuildingManager buildingManager = FindObjectOfType<BuildingManager>();

		if (buildingManager != null)
		{
			buildingManager.Get_Building(NodeType.Company);
		}
    }

	public void Level_Up()
	{

		switch ((int)current_Level)
		{
			case 0:
				//max_Count = Random.Range(5, 11);
				animators[0].gameObject.SetActive(true);
				animators[1].gameObject.SetActive(false);
				animators[2].gameObject.SetActive(false);
				animator = animators[0];
				kind = Building_Type.Small;
				break;

			case 1:
				//max_Count = Random.Range(10, 21);
				animators[1].gameObject.SetActive(true);
				animators[0].gameObject.SetActive(false);
				animators[2].gameObject.SetActive(false);
				animator = animators[1];
				kind = Building_Type.Middle;
				break;

			case 2:
				//max_Count = Random.Range(20, 31);
				animators[2].gameObject.SetActive(true);
				animators[0].gameObject.SetActive(false);
				animators[1].gameObject.SetActive(false);
				animator = animators[2];
				kind = Building_Type.Big;
				break;

			default:
				Debug.Log("Max_Level_Already");
				break;
		}

		current_Level = (int)current_Level < 3 ? (Level)((int)current_Level + 1) : current_Level;
        
		
        //SpriteAtlas[] spriteAtlas = Resources.LoadAll<SpriteAtlas>("Buildings");

        //sprite.sprite = Resources.Load<SpriteAtlas>("Building/Sprite/Buildings").GetSprite("Object_House_" + ((int)current_Level)) as Sprite;

		//sprite.SetNativeSize();

		Debug.Log("Max People : " + max_Count);

	}

     GameObject CreatePeople(string name)
    {
        TileController clone = FindObjectOfType<TileController>();

        GameObject person = Instantiate(Resources.Load<GameObject>("InGame/Citizen/" + name), transform) as GameObject;
        person.transform.position = mainTile;
		Tile editorTile = clone.tiles[(int)pos.x, (int)pos.y];
		Citizen person_Con = person.GetComponent<Citizen>();
		//clone.CitizenBtnEventAdd(person_Con);

        //person_Con.pos = pos;
        person_Con.ResetPos(editorTile);
		//person_Con.CreateSetting(clone);
		//person_Con.SetCitizenHome(this);
        clone.citizenList.Add(person_Con);
		
		max_Count += 1;

		return person;
    }

	public void Summon_People()
	{
		if (people.Count <= 0)
		{
			//더 이상의 인원이 없습니다.

			return;
		}
		
		Summon_People((people[0] as GameObject));

		//for (int i = 0; i < people.Count; i++)
		//	Debug.Log(i + " " + people[i] == null + "wn");
		//추후 유닛의 외출 속성이 가장 높은 사람을 찾아 소환
	}

	private void Create_Dole_Guage()
	{
		dole_Bar = Instantiate(Resources.Load<Dole_Bar>("UI/Dole_Gauge") as Dole_Bar, transform.position, Quaternion.identity);

		dole_Bar.GetComponent<Canvas>().worldCamera = FindObjectOfType<Camera>();

		dole_Bar.transform.SetParent(transform);

		dole_Bar.transform.localPosition = new Vector3(0f, 98.0f, 0f);

		dole_Bar.GetComponent<Gauge_Bar>().Set_Parent(this);

		StartCoroutine(Apartment_Spawn());

	}

	public void Create_Set(int nom, int young, int old, int the)
	{

		for (int i = 0; i < nom; i++)
		{
			GameObject clone = CreatePeople("Normal_Citizen");

			people.Add(clone);

			clone.SetActive(false);
		}

		for (int i = 0; i < young; i++)
		{
			GameObject clone = CreatePeople("Young_Citizen");

			people.Add(clone);

			clone.SetActive(false);
		}

		for (int i = 0; i < old; i++)
		{
			GameObject clone = CreatePeople("Old_Citizen");

			people.Add(clone);

			clone.SetActive(false);
		}

		for (int i = 0; i < the; i++)
		{
			GameObject clone = CreatePeople("The_Citizen");

			people.Add(clone);

			clone.SetActive(false);
		}

	}
	
	private IEnumerator Apartment_Spawn()
	{

		if((float)people.Count/max_Count - 1.0f/max_Count > dole_Bar.Return_Gauge())
			Summon_People();

		yield return new WaitForSeconds(0.5f);

		StartCoroutine(Apartment_Spawn());

	}

    public void Revice_Dole(Origin_Building building = null)
    {

        dole_Bar.Revice_Gauge(dole_Bar.Return_Gauge() + 0.05f);

    }

	public override void Revice_Dole(float fill = 1f)
	{

		dole_Bar.Revice_Gauge(fill);

	}

	protected override float Return_Dole()
	{
	
		return dole_Bar.Return_Gauge();

	}
}
