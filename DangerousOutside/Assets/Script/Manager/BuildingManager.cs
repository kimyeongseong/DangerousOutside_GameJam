using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoSingleton<BuildingManager>
{
	public ArrayList arrayList = new ArrayList();

	public Origin_Building Get_Building(NodeType type, Origin_Building Home = null)
	{

		if (type == NodeType.Home && Home != null)
		{
			foreach (Origin_Building building in arrayList)
				if (building == Home)
					return building;
		}

		else

		{
			Building_Type building_Type = Building_Type.Nothing;

			switch (type)
			{
				case NodeType.BathHouse:
					building_Type = Building_Type.BathHouse;
					break;

				case NodeType.Church:
					building_Type = Building_Type.Church;

					break;

				case NodeType.Company:
					building_Type = Building_Type.Company;
					break;

				case NodeType.Karaoke:
					building_Type = Building_Type.Karaoke;
					break;

				case NodeType.School:
					building_Type = Building_Type.School;
					break;

			}

			foreach(Origin_Building building in arrayList)
			
				if(building.kind == building_Type)
				
					return building;
				
				
			

		}

		return null; 
	}
}
