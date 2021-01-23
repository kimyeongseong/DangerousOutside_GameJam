using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Origin_School : Origin_Building
{
	public float school_Time = 10;

	private GameObject school_Clock;

	private bool inClass = false;

	private int current = 0;

	private int max = 0;

    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();

		Create_School_Clock();

		school_Clock.GetComponent<School_Clock>().Revice_Time(school_Time);

       kind = Building_Type.School;
    }

	private void Create_School_Clock()
	{
        school_Clock = animator.gameObject.transform.Find("School_body").gameObject;

        school_Clock.GetComponent<School_Clock>().Set_Parent(this);
	}

	public override void Summon_People(GameObject unit, NodeType node = (NodeType)10)
	{

		base.Summon_People(unit, node);

		if (unit.GetComponent<Person>().GetCitizenKind() == Citizen_Type.Young)
			current -= 1;

	}

	public override void Come_In(GameObject unit)
	{
		base.Come_In(unit);

		if (unit.GetComponent<Person>().GetCitizenKind() == Citizen_Type.Young)
			current += 1;
	}

	IEnumerator Recall(Person[] out_Students)
	{
		if(max != current)
		{
			yield return new WaitForSeconds(5.0f);

			foreach (Person person in out_Students)
				if (person.GetCitizenKind() == Citizen_Type.Young)
					person.GoBulding(BuildingManager.Instance.Get_Building(NodeType.School));

			StartCoroutine(Recall(out_Students));
		}
		
	}

	public IEnumerator School_In_Out()
	{
		if (inClass)
		{
			for (int i = people.Count - 1; i >= 0; i--)
				Summon_People(people[i] as GameObject);
					
			Debug.Log("Students Out!");

			animator.SetTrigger("spread");

			inClass = false;

			current = 0;

			StartCoroutine(school_Clock.GetComponent<School_Clock>().Time_Goes_On());
		}
		else
		{
			max = 0;

			animator.SetBool("collect", true);

			Person[] students = FindObjectsOfType<Person>();

			Origin_Building[] buildings = FindObjectsOfType<Origin_Building>();

			foreach (Person student in students)
			{
				if (student.GetCitizenKind() == Citizen_Type.Young)
				{
					max += 1;
					student.GoBulding(this);
				}
			}

			foreach (Origin_Building building in buildings)
			{
				
				max += building.Call_Students();

			}

			Person[] out_Students = FindObjectsOfType<Person>();

			StartCoroutine(Recall(out_Students));

			yield return new WaitUntil(() => max == current);

			Debug.Log("Students In!");

			inClass = true;

			animator.SetBool("collect", false);

			StartCoroutine(school_Clock.GetComponent<School_Clock>().Time_Goes_On());
		}
	}
}
