using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Apartment obj = Instantiate(Resources.Load<Apartment>("InGame/Building/Apartment"), transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
