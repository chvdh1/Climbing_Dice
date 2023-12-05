using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class net : MonoBehaviour
{
    GameObject cam;

    float pos = 0;

    [SerializeField] SpriteRenderer SPR;
    [HideInInspector]
    public int count;
    [SerializeField]GameObject child;

    private void Awake()
    {
        if(SPR == null)
            SPR = GetComponent<SpriteRenderer>();
    }

    public void scale(GameObject CM, float scalex,float scaley,int c,int mapint)
    {
        cam = CM;
        transform.localScale = new Vector3(scalex, scaley, 1);
        count = c;
        SPR.color = new Color(1, 1,1, (count + 1) * 0.2f);
       
        StartCoroutine(setfales());
    }


    public void countCK()
    {
        count--;

        SPR.color = new Color(0.5f, 0.5f, 0.5f, (count + 1) * 0.2f);

        if (count < 0)
            gameObject.SetActive(false);
    }
    private void Update()
    {
        if (child == null)
            return;

        child.transform.Rotate(Vector3.forward * Time.deltaTime);
    }


    public IEnumerator setfales()
    {
        pos = cam.transform.position.y - 10;
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
