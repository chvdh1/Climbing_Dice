using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back : MonoBehaviour
{
    gamemanager gm;
    [SerializeField] Transform campos;

    [SerializeField] float camposm;
    [SerializeField] float dirpos;
    [SerializeField] float activef;

    public void setvariable()
    {
        gm = gamemanager.gm;
        campos = gm.cam.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (campos != null)
        {
            transform.position = new Vector2(0, dirpos+campos.position.y * camposm);

            if (transform.position.y < activef)
                gameObject.SetActive(false);
        }
    }
}
