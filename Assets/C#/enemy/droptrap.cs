using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droptrap : MonoBehaviour
{
    GameObject cam;
    float dropspeed = 0;
    float pos = 0;
    SpriteRenderer SPR;

    private void Awake()
    {
        SPR = GetComponent<SpriteRenderer>();
    }



    public void scale(GameObject CM,float scalex, float scaley,float DS,int stageint)
    {
        cam = CM;
        transform.localScale = new Vector3(scalex, scaley, 1);
        dropspeed = DS;

        StartCoroutine(setfales());
    }

    public IEnumerator setfales()
    {
        pos = cam.transform.position.y-10;
        yield return new WaitForFixedUpdate();
        while (transform.position.y > pos)
        {
            pos = cam.transform.position.y - 10;
            yield return new WaitForSeconds(1);
        }
        yield return new WaitForFixedUpdate();
        gameObject.SetActive(false);
    }
}
