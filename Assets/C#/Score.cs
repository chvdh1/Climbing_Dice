using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    GameObject plobj;
    gamemanager gm;
    player pl;
    Text score;
    Text high;
    [SerializeField] Text stageint;

    [HideInInspector] public float ply = 0; // 높이
    float timesc = 0;
    [HideInInspector] public float Sc = 0;// 점수

    private void Start()
    {
        gm = gamemanager.gm;
        gm.sc = this;
    }

    public void setvariable()
    {
        score = transform.GetChild(0).gameObject.GetComponent<Text>();
        high = transform.GetChild(1).gameObject.GetComponent<Text>();
        stageint = transform.GetChild(2).gameObject.GetComponent<Text>();
        plobj = loading.ld.plobj;
        pl = loading.ld.pl;

    }

    public IEnumerator CK()
    {
        yield return new WaitForSeconds(0.5f);
        while(!pl.isdead)
        {
            float burningSC = pl.burning ? 2 : 0;
            if (ply < plobj.transform.position.y)
            {
                ply = plobj.transform.position.y;
            }
               

            timesc += 1+ burningSC;
            Sc = (pl.transform.position.y/10) + timesc + (gm.getcoin*10)+(gm.getredcoin*500);
            score.text = Sc.ToString("F0") + " : 점수";
            high.text = "높이 : " + ply.ToString("F1");
            stageint.text = gm.stage.ToString();
            yield return new WaitForSeconds(0.1f);
        }
    }
}
