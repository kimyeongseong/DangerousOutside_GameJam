using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EditorTile : MonoBehaviour , IPointerClickHandler
{
    public Vector2 pos;
    public Tile_Type tile_Type;
    /// <summary> 클릭이 되었을때 리스너에게 호출 </summary>
    public UnityAction<EditorTile> clickEvent;
    public EditorStage Tile_mng;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickEvent != null)
        {
            clickEvent(this);
        }
    }

    public void TileChange(Tile_Type type, bool setOn = false)
    {
        if (type == Tile_Type.None && setOn == false)
            return;

        tile_Type = type;

        Color cor = Color.white;
        switch (type)
        {
            case Tile_Type.None:
                cor = new Color(1, 1, 1, 0);
                break;
            case Tile_Type.White:
                cor = Color.white;
                break;
            case Tile_Type.Red:
                cor = Color.red;
                break;
            case Tile_Type.Blue:
                cor = Color.blue;
                break;
        }
        ColorChange(cor);
    }

    private void ColorChange(Color cor)
    {
        GetComponent<Image>().color = cor;
    }
}
