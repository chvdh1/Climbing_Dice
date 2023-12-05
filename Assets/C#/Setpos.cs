using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setpos : MonoBehaviour
{
    gamemanager gm;

    [SerializeField] float posx;
    [SerializeField] float posy;

    private void Start()
    {
        gm = gamemanager.gm;
    }


}
