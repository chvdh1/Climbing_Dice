using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class deadline : MonoBehaviour
{
    GameObject cam;
    GameObject pl;
    player pls;
    gamemanager gm;

    [SerializeField] GameObject distanceobj;
    [SerializeField] Text dis;
    public float miny = -10f;
    float vely = 1;
    // Start is called before the first frame update
    private void Start()
    {
        gm = gamemanager.gm;
        gm.dd = this;
    }

    public  void setvariable()
    {
        gm = gamemanager.gm;
        cam = gm.cam;
        pls = gm.pl;
        pl = gm.plobj;
        distanceobj.SetActive(false);

        StartCoroutine(rise());
    }

    public IEnumerator rise()
    {
        transform.position = new Vector2(0, cam.transform.position.y+ miny);
        while (gm.gamestate ==0)
        {
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForFixedUpdate();
        distanceobj.SetActive(true);
        while (!pls.isdead)
        {
            float disy = transform.position.y;
            float camy = cam.transform.position.y;
            float ply = pl.transform.position.y;

            if (disy < camy+ miny)
            {
                disy = camy + miny;
            }
            else
            {
                disy += Time.deltaTime * (vely+ gm.stage*0.5f);
            }

            if(distanceobj.activeSelf && camy - disy <= 5)
                distanceobj.SetActive(false);
            else if (!distanceobj.activeSelf && camy - disy > 5)
                distanceobj.SetActive(true);

            dis.text = (ply - disy).ToString("F2")+"M";
            transform.position = new Vector2(0,disy);

             yield return new WaitForFixedUpdate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            gm.gameover();
        }
    }
}
