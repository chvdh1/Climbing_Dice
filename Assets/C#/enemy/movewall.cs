using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movewall : MonoBehaviour
{
    GameObject cam;

    float pos = 0;
    public SpriteRenderer SPR;
    BoxCollider2D box;
    [SerializeField]GameObject child;

    private void Awake()
    {
        SPR = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
    }


    public void scale(GameObject CM, float scaley, int mapint)
    {
        if (mapint == 4)
        {
            box.size = new Vector2(1, scaley-0.3f);
        }
        else
        {
            box.size = new Vector2(1, scaley);
        }
        SPR.size = new Vector2(1, scaley);

        cam = CM;
        if(child != null)
        {
            child.transform.position = new Vector3(transform.position.x, transform.position.y + (scaley * 0.5f) + 0.1f);
        }
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
