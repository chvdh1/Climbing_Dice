using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageUpBar : MonoBehaviour
{
    gamemanager gm;
    player pl;

    private void Start()
    {
        gm = gamemanager.gm;
        pl = player.pl;
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gamestate != 2 && gm.gamestate != 3)
            return;


        if (transform.position.y < pl.ply && !gm.Check)
        {
            gm.Check = true;
            gm.stageup();
        }
    }
}
