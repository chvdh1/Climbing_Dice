using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loading : MonoBehaviour
{
    public static loading ld;

    void Awake()
    {
        ld = this;
    }

    [SerializeField] GameObject[] loadingOBJ;



    [SerializeField] GameObject m_dice;
    [SerializeField] Text txt;
    [SerializeField] RectTransform gauge;
    float gaugeint;


    [HideInInspector] public player pl;
    [HideInInspector] public GameObject plobj;
    [HideInInspector] public StartGauge SG;
    [HideInInspector] public pool item;
    [HideInInspector] public GameObject cam;
    [HideInInspector] public GameObject stageupline;
    [HideInInspector] public Score sc;
    [HideInInspector] public deadline dd;
    [HideInInspector] public Shop shop;
    [HideInInspector] public pool tt;
    gamemanager gm;
    // Start is called before the first frame update
    void Start()
    {
        SetResolution(); // 초기에 게임 해상도 고정
        gm = gamemanager.gm;
        m_dice = GameObject.Find("a_dice").gameObject;
        txt = GameObject.Find("loadingtxt").gameObject.GetComponent<Text>();
        gauge = GameObject.Find("loadinggauge").gameObject.GetComponent<RectTransform>();
        StartCoroutine(loadinggauge());
        StartCoroutine(loadingtext());

    }
    /* 해상도 설정하는 함수 */
    public void SetResolution()
    {
        int setWidth = 720; // 사용자 설정 너비
        int setHeight = 1280; // 사용자 설정 높이

        int deviceWidth = Screen.width; // 기기 너비 저장
        int deviceHeight = Screen.height; // 기기 높이 저장

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution 함수 제대로 사용하기

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // 기기의 해상도 비가 더 큰 경우
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // 새로운 너비
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // 새로운 Rect 적용
        }
        else // 게임의 해상도 비가 더 큰 경우
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // 새로운 높이
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // 새로운 Rect 적용
        }
    }
    IEnumerator loadinggauge()
    {
        gaugeint = 0;
        gauge.sizeDelta = new Vector2(0, 50);
        yield return new WaitForSeconds(1);

        plobj = GameObject.Find("pl").gameObject; 
        while (plobj == null)
            yield return new WaitForFixedUpdate();

        pl = plobj.GetComponent<player>();
        while(pl == null)
            yield return new WaitForFixedUpdate();
        gauge.sizeDelta = new Vector2(500*1/10, 50);

        SG = StartGauge.SG;
        while (SG == null)
            yield return new WaitForFixedUpdate();
        gauge.sizeDelta = new Vector2(500 * 2 / 10, 50);

        item = GameObject.Find("itemG").gameObject.GetComponent<pool>();
        while (item == null)
            yield return new WaitForFixedUpdate();
        gauge.sizeDelta = new Vector2(500 * 3 / 10, 50);

        cam = GameObject.Find("Main Camera");
        while (cam == null)
            yield return new WaitForFixedUpdate();
        gauge.sizeDelta = new Vector2(500 * 4 / 10, 50);

        stageupline = GameObject.Find("stageup");
        while (stageupline == null)
            yield return new WaitForFixedUpdate();
        gauge.sizeDelta = new Vector2(500 * 5 / 10, 50);

        tt = GameObject.Find("text_G").GetComponent<pool>();
        while (tt == null)
            yield return new WaitForFixedUpdate();
        gauge.sizeDelta = new Vector2(500 * 6 / 10, 50);

        shop = GameObject.Find("ShopSC").GetComponent<Shop>();
        while (shop == null)
            yield return new WaitForFixedUpdate();
        gauge.sizeDelta = new Vector2(500 * 7 / 10, 50);



        yield return new WaitForFixedUpdate();
        gaugeint = 100;
        gauge.sizeDelta = new Vector2(500, 50);
    }
    IEnumerator loadingtext()
    {
        int i = 0;
        while(gaugeint < 100)
        {
            if(i == 0)
                txt.text = "로딩 중.";
            else if(i == 1)
                txt.text = "로딩 중..";
            else if (i == 2)
                txt.text = "로딩 중...";

            i = i == 2 ? 0 : i+1; 
           yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.1f);
        txt.text = "로딩 완료!";
        gm.setvariable();
        yield return new WaitForFixedUpdate();
        StartCoroutine(fadeinout());

    }
    IEnumerator fadeinout()
    {
        Image im = loadingOBJ[1].GetComponent<Image>();
        for (int i = 0; i < 51; i++)
        {

            im.color = new Color(0, 0, 0, i/50f);
            yield return new WaitForSeconds(1.5f/150f);
        }
        yield return new WaitForSeconds(0.1f);
        loadingOBJ[0].SetActive(false);
        yield return new WaitForSeconds(0.1f);
        for (int i = 50; i > 0; i--)
        {

            im.color = new Color(0, 0, 0, i / 50f);
            yield return new WaitForSeconds(1.5f / 150f);
        }
        yield return new WaitForSeconds(0.1f);
        loadingOBJ[1].SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (m_dice != null)
        m_dice.transform.localEulerAngles += new Vector3(0, 0, Time.deltaTime * 50);
    }
}
