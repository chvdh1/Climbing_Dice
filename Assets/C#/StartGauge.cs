using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartGauge : MonoBehaviour
{
    public static StartGauge SG;

    void Awake()
    {
        SG = this;
    }

    gamemanager gm;

    [HideInInspector] public float Max = 100f;
    [HideInInspector] public float power = 0;
    [HideInInspector] public float cripower = 2;

    public GameObject movepoint;
    [SerializeField] RectTransform gaugebar,Cripoint,Maxtext, movepointRC,Gaugebar;
    [SerializeField] float gaugeSpeed;
    Image movepointIMG;

    [HideInInspector] public Text powertext, powerCktext;

    // 버튼의 클릭을 인지하는 변수
    bool gaugeCL = false;
    bool maxTouch = false;
    public int gaugestate; // 0 누르기 전 / 1 다운 / 2업


    void Start()
    {
        gm = gamemanager.gm;
        power = 0;
        cripower = 2;
        movepointIMG = movepoint.GetComponent<Image>();
        movepointRC = movepoint.GetComponent<RectTransform>();
        Gaugebar = GetComponent<RectTransform>();
        Gaugebar.sizeDelta = new Vector2(60, 2 * Max);
        movepointRC.sizeDelta = new Vector2(50, 2*Max);
        movepointIMG.fillAmount = 0;
        Cripoint.sizeDelta = new Vector2(85, (gm.gaugeCriMax- gm.gaugeCriMin)*2);
        Cripoint.anchoredPosition = new Vector3(0, gm.gaugeCriMin*2, 0);
        gaugeCL = false;
        maxTouch = false;
        gaugestate = 0;
        Time.timeScale = 1;
    }

    public void gaugestart()
    {
        if (gaugestate != 0)
            return;

        gaugeCL = true;
        StartCoroutine(gaugeUpDown());
    }

    IEnumerator gaugeUpDown()
    {
        gaugestate = 1;
        yield return new WaitForFixedUpdate();
        Debug.Log("start1");
        while (gaugeCL)
        {
            Debug.Log("start2");
            if (!maxTouch)
            {
                power += Time.deltaTime * gaugeSpeed;
                if (power >= Max)
                    maxTouch = true;
            }
            else
            {
                power -= Time.deltaTime * gaugeSpeed;
                if (power <= 0)
                    maxTouch = false;
            }

            movepointIMG.fillAmount = power/ Max;
            yield return new WaitForFixedUpdate();
        }
    }

    public void gaugeEnd()
    {
        if (gaugestate != 1)
            return;
        gaugestate = 2;
        Debug.Log("end");
        gaugeCL = false;
        // 계산 모션
        StartCoroutine(gaugeCK());
    }
    IEnumerator gaugeCK()
    {
        yield return new WaitForFixedUpdate();
        powertext.text = gm.pl.pw.ToString("F1");
        //효과음 1
        yield return new WaitForSeconds(1);
        powerCktext.text = "+ (" + gm.pl.pw.ToString("F1") + " X " + (power / 100).ToString("F1") + ")";
        powertext.text = (gm.pl.pw+ (gm.pl.pw*power/100)).ToString("F1");
        //효과음 2
        yield return new WaitForSeconds(1);
        Debug.Log("gaugeCriMin = " + gm.gaugeCriMin);
        Debug.Log("gaugeCriMax = " + gm.gaugeCriMax);
        Debug.Log("power = " + power);
        if (power >= gm.gaugeCriMin && gm.gaugeCriMax >= power)
        {
            powerCktext.text = "Critical + (" + (gm.pl.pw + (power / 100)* cripower).ToString("F1") + ")";
            powertext.text = (gm.pl.pw + ((gm.pl.pw * (power / 100))* cripower)).ToString("F1");
            //효과음 3
            yield return new WaitForSeconds(1);
        }
        yield return new WaitForFixedUpdate();

        gm.gamestate = 1;// 출발
        gm.StartCoroutine(gm.gaugefal());
        gm.UIUpdate();
    }
}
