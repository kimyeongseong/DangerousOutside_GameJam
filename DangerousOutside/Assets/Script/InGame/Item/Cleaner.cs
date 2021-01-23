using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner
{
    /// <summary> 세탁기 인덱스 </summary>
    public int index;
    /// <summary> 세탁기에 들어있는 주민 </summary>
    public Citizen citizen = null;
    /// <summary> 세탁중인 시간(Citizen.cleanTime과 비교하여 takeTime이 더 크면 세탁기 탈출) </summary>
    public float takeTime;
    /// <summary> 세탁기 동작 상태 </summary>
    public CleanerState cleanerState = CleanerState.Idle;

    public void CitizenOn(Citizen citizen)
    {
        this.citizen = citizen;
        this.citizen.cleanerOn = true;

        cleanerState = CleanerState.CitizenReady;
    }

    public void SetState(CleanerState cleanerState)
    {
        this.cleanerState = cleanerState;

        if (cleanerState == CleanerState.Cleaning)
        {
            takeTime = 0;
        }
        else if (cleanerState == CleanerState.CleaningEnd)
        {
            
            citizen.cleanerOn = false;

            Tile tile = GameManager.Ins.tileController.tiles[0, 4];

            //var tile = GameObject.FindObjectOfType<TileController>().tiles[0, 4];
            //citizen.transform.position = tile.transform.position;
            //citizen.pos = tile.pos;

            citizen.gameObject.SetActive(true);
            citizen.ChangeColor(CitizenColor.White);
            citizen.ResetPos(tile);
            citizen.GoHome();

            

            //citizen.StartFSM();
            /*
            if (citizen.Home != null)
            {
                citizen.SetCitizenHome(citizen.Home);
            }
            else
            {
                citizen.ChangePattern();
            }
            */

            citizen = null;
            cleanerState = CleanerState.Idle;
        }
    }

}

public enum CleanerState
{
    /// <summary> 대기상태 </summary>
    Idle,
    /// <summary> 주민 픽업 하러 가는중 </summary>
    CitizenReady,
    /// <summary> 세탁중 </summary>
    Cleaning,
    /// <summary> 세탁 끝 </summary>
    CleaningEnd
}
