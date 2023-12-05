using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap : MonoBehaviour
{
    GameObject cam;

    float pos = 0;
    SpriteRenderer SPR;

    private void Awake()
    {
        SPR = GetComponent<SpriteRenderer>();
    }

    public void scale(GameObject CM, float scalex, float scaley,int mapint)
    {
        cam = CM;
        transform.localScale = new Vector3(scalex, scaley, 1);
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
