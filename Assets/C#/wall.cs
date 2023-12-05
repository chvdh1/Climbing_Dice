using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall : MonoBehaviour
{
    [SerializeField] GameObject Cam;
    public bool start;
    SpriteRenderer SP;
    gamemanager gm;
    [SerializeField]Sprite[] sprites;



    void Start()
    {
        gm = gamemanager.gm;
    }

    public void setvariable()
    {
        Cam = loading.ld.cam;
        SP = GetComponent<SpriteRenderer>();

        StartCoroutine(repos());
    }

    IEnumerator repos()
    {
        yield return new WaitForSeconds(0.1f);
        while(start)
        {
            if(transform.position.y <= Cam.transform.position.y-25)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 100, 0);
                if (transform.position.y < 250)
                    SP.sprite = sprites[0];
                else if (transform.position.y >= 250 && transform.position.y < 750)
                    SP.sprite = sprites[1];
                else if (transform.position.y >= 750 && transform.position.y < 2750)
                    SP.sprite = sprites[2];
                else if (transform.position.y >= 2750 && transform.position.y < 7750)
                    SP.sprite = sprites[3];
                else if (transform.position.y >= 7750 && transform.position.y < 27750)
                    SP.sprite = sprites[4];
                else if (transform.position.y >= 27750)
                {
                    SP.sprite = sprites[5];
                    SP.color = new Color(0.5f, 0.5f, 0.5f, 1);
                }
                  

            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}
