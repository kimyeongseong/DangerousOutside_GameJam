using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

		StartCoroutine(Move());

		StartCoroutine(Remove());

	}

	IEnumerator Remove()
	{

		yield return new WaitForSeconds(16.0f);

		Destroy(this.gameObject);

	}
	
	IEnumerator Move()
	{
		Vector3 direction = new Vector3();

		switch (Random.Range(0, 4))
		{

			case 0:
				direction.x = 1;

				break;

			case 1:
				direction.x = -1;

				break;

			case 2:
				direction.y = 1;

				break;

			case 3:
				direction.y = -1;

				break;

		}

		for (int i = 0; i < 20; i++)
		{
			transform.Translate(direction / 20);
			yield return new WaitForSeconds(0.05f);
		}

		yield return new WaitForSeconds(2.0f);

		StartCoroutine(Move());

	}
}
