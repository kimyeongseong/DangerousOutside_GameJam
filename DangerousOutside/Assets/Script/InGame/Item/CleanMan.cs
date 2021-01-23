using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CleanMan : MonoBehaviour
{
    Tile currentTile;

    private float delayTime;
    private bool moveOn;

    bool initOn = false;
    public bool stopOn = false;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Init(Tile currentTile)
    {
        if (GameManager.Ins.vibrationOn)
        {
#if UNITY_ANDROID
            Handheld.Vibrate();
#endif
        }

        this.currentTile = currentTile;
        delayTime = 2;
        TileReset();
        initOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (initOn == false || stopOn)
            return;

        MoveTimeCheck();
    }

    void MoveTimeCheck()
    {
        if (moveOn)
            return;

        delayTime -= Time.deltaTime;

        if (delayTime < 0)
        {
            moveOn = true;
            delayTime = 2;
            MoveDoing();
        }
    }

    void MoveDoing()
    {
        Tile tempTile = TargetTileGet();
        if (tempTile == null)
        {
            moveOn = false;
            return;
        }

        currentTile = tempTile;

        float curentY = transform.transform.position.y;
        float targetY = currentTile.transform.position.y;

        bool moveUpOn = targetY > curentY;

        anim.SetBool("back", moveUpOn);
        anim.SetBool("side", !moveUpOn);
        
        transform.DOMove(currentTile.transform.position, 1).OnComplete(() =>
        {
            TileReset();
            moveOn = false;
        });
    }

    Tile TargetTileGet()
    {
        Vector2 pos = currentTile.pos;

        List<Vector2> nodePosList = new List<Vector2>()
        {
            new Vector2(pos.x +1 , pos.y),
            new Vector2(pos.x -1, pos.y),
            new Vector2(pos.x , pos.y +1),
            new Vector2(pos.x , pos.y -1)
        };

        List<Vector2> checkList = new List<Vector2>();

        foreach (var nodePos in nodePosList)
        {
            if (nodePos.x >= 0 && nodePos.y >= 0 && nodePos.x < TileController.x_max_value && nodePos.y < TileController.y_max_value &&
                GameManager.Ins.tileController.IsWall(nodePos) == false)
            {
                checkList.Add(nodePos);
            }
        }

        if (checkList.Count == 0)
        {
            return GameManager.Ins.tileController.GetTile(pos);
        }
        else
        {
            int ranIndex = Random.Range(0, checkList.Count);
            Vector2 targetPos = checkList[ranIndex];
            return GameManager.Ins.tileController.GetTile(targetPos);
        }
    }

    public void TileReset()
    {
        if (currentTile.tile_Type == Tile_Type.Red)
        {
            currentTile.TileChange(Tile_Type.White);
        }
        else if (currentTile.tile_Type == Tile_Type.White)
        {
            currentTile.TileChange(Tile_Type.Blue);
        }
    }

}
