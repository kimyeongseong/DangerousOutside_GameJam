using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    public Vector2 pos;
    public Tile_Type tile_Type;
    public Animator anim;
    public UnityAction tileChangeOn;

    public void TileChange(Tile_Type type, bool setOn = false)
    {
        if (type == Tile_Type.None && setOn == false)
            return;

        tile_Type = type;

        if (anim != null)
        {
            bool whiteOn = false;
            bool red = false;
            bool blue = false;

            switch (type)
            {
                case Tile_Type.White:
                    whiteOn = true;
                    red = false;
                    blue = false;
                    break;
                case Tile_Type.Red:
                    whiteOn = false;
                    red = true;
                    blue = false;
                    break;
                case Tile_Type.Blue:
                    whiteOn = false;
                    red = false;
                    blue = true;
                    break;
            }

            anim.SetBool("white", whiteOn);
            anim.SetBool("red", red);
            anim.SetBool("blue", blue);


            if (type == Tile_Type.None)
            {
                anim.gameObject.SetActive(false);
            }
        }

        if (tileChangeOn != null)
        {
            tileChangeOn();
        }
    }

}
