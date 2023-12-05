using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Camcontroller : MonoBehaviour
{
    GameObject pl;
    gamemanager gm;
    [SerializeField] float setposy;


    void Start()
    {
        gm = gamemanager.gm;
        gm.camc = this;
    }
    public void setvariable()
    {
        pl = gm.plobj;
    }

    void Update()
    {
        if ((gm.gamestate ==1|| gm.gamestate == 2)&& transform.position.y < pl.transform.position.y+ setposy && pl !=null)
        {
            transform.position = new Vector3(0, pl.transform.position.y+ setposy, -10);
        }
    }
}
