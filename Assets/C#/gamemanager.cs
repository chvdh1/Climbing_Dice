using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class gamemanager : MonoBehaviour
{
    public static gamemanager gm;

    void Awake()
    {
        gm = this;
    }

    [HideInInspector] public  StartGauge SG;
    [HideInInspector] public Shop sh;

  [HideInInspector] public deadline dd;
    [HideInInspector] public Camcontroller camc;
    [HideInInspector] public pool textG;
    Back[] back = new Back[10];
    public GameObject wallP;
    wall[] wallsc = new wall[8];

    //게임의 상태 변수
    public int gamestate = 0; // 0 = 준비 / 1 = 출발 / 2 = 진행 / 종료 = 3 
    [SerializeField] GameObject gameoverui, gamestartui, startbuster;
    [SerializeField] Text goSc, gohi, gocoin, goRed, gomaxSc, gomaxhi;

    //플레이어 변수
    [HideInInspector] public player pl;
    [HideInInspector] public GameObject plobj;
    [HideInInspector] public Score sc;
    [HideInInspector] public int coin, Redcoin, getcoin, getredcoin;
    float maxSc, maxhi;
    [SerializeField] Text cointtext, Redtext, getcointtext, getredtext;

    //게이지 변수
    [HideInInspector] public float gaugeMax = 100f;
    [HideInInspector] public float gaugeCriMax;
    [HideInInspector] public float gaugeCriMin;

    //플레이어 버닝 ui
    [HideInInspector] public GameObject[] BNUI;
    [HideInInspector] public Sprite[] ranBNUI;
    [HideInInspector] public bool dice_tail;

    //일시정지 UI
    [HideInInspector] public bool pouse;
    [HideInInspector] public int BN_tail, BN_back;
    [SerializeField] Image BN_tail_im, BN_back_im;
    public GameObject[] pouseobj; //메뉴, 백, 일시정지, 셋팅
    [HideInInspector] public GameObject Fade;
    [SerializeField] Text counttext;
    [SerializeField] GameObject deletprefab, codepanel;
    [HideInInspector] public Text codetext;

    //게임 상태에 따른 UI관리
    [SerializeField] GameObject[] lobbyUI;
    [SerializeField] GameObject[] IngameUI;
    [SerializeField] GameObject[] GOUI;



    pool item;
    [HideInInspector] public GameObject cam;

    //높이에 따른 스테이지 업 변수
    [HideInInspector] public int stage = 0;
    [HideInInspector] public bool Check;
    GameObject stageupline;
    float high = 0;

    //장애물을 주기위한 변수
    [HideInInspector] public int difficulty = 0; // 0 = 이지 , 1 = 노말 .2=하드
    [SerializeField] Text difficultytext;
    [HideInInspector] public float coltime = 5; // 장애물 나오는 쿨타임

    //맵 변경을 위한 변수
    [HideInInspector] public int mapint = 0;

   

    void Start()
    {
        //pl = GameObject.Find("pl").gameObject.GetComponent<player>();
        //SG = StartGauge.SG;
        //item = GameObject.Find("itemG").gameObject.GetComponent<pool>();
        //cam = GameObject.Find("Main Camera");
        //stageupline = GameObject.Find("stageup");
        gamestate = 0;
        for (int i = 0; i < back.Length; i++)
        {
            back[i] = GameObject.Find("BACK").transform.GetChild(i).GetComponent<Back>();
        }
        for (int i = 0; i < wallsc.Length; i++)
        {
            if(i <= 3)
                wallsc[i] = wallP.transform.GetChild(0).transform.GetChild(i).GetComponent<wall>();
            else
                wallsc[i] = wallP.transform.GetChild(1).transform.GetChild(i-4).GetComponent<wall>();
        }
        if (PlayerPrefs.HasKey("maxSc"))
            maxSc = PlayerPrefs.GetFloat("maxSc");
        if (PlayerPrefs.HasKey("maxhi"))
            maxhi = PlayerPrefs.GetFloat("maxhi");
        if (PlayerPrefs.HasKey("coin"))
            coin = PlayerPrefs.GetInt("coin");
        if (PlayerPrefs.HasKey("Redcoin"))
            Redcoin = PlayerPrefs.GetInt("Redcoin");

        if (PlayerPrefs.HasKey("BN_tail"))
            BN_tail = PlayerPrefs.GetInt("BN_tail");
        if (PlayerPrefs.HasKey("BN_back"))
            BN_back = PlayerPrefs.GetInt("BN_back");
        cointtext.text = coin.ToString();
        Redtext.text = Redcoin.ToString();
        UIUpdate();
    }
    public void setvariable()
    {
        pl = loading.ld.pl;
        plobj = loading.ld.plobj;
        SG = loading.ld.SG;
        item = loading.ld.item;
        cam = loading.ld.cam;
        stageupline = loading.ld.stageupline;
        textG = loading.ld.tt;
        sh = loading.ld.shop;

        StartCoroutine(scriptsetvariable());
    }
    IEnumerator scriptsetvariable()
    {
        yield return new WaitForSecondsRealtime(0.2f);
       
        dd.setvariable();
        yield return new WaitForFixedUpdate();
        camc.setvariable();
        yield return new WaitForFixedUpdate();
        sc.setvariable();
        yield return new WaitForFixedUpdate();
        sh.setvariable();
        yield return new WaitForFixedUpdate();

        for (int i = 0; i < back.Length; i++)
        {
            back[i].setvariable();
            yield return new WaitForFixedUpdate();
        }


        for (int i = 0; i < wallsc.Length; i++)
        {
            wallsc[i].setvariable();
            yield return new WaitForFixedUpdate();
        }
    }

    void Update()
    {
        if (sc != null)
            high = sc.ply;
    }


    public void stageup() // 스테이지 업에 변화들 맵, 소환 주ㄱㅣ, 
    {
        stage++;
        int itemint = stage * 2;
        int stagehi = 0;
        if (stage < 5)
        {
            stagehi = 50;
            stageupline.transform.position = new Vector3(0, stageupline.transform.position.y + stagehi, 0);
            coltime = 4;
            mapint = 0;
        }
        else if (stage < 10)
        {
            stagehi = 100;
            stageupline.transform.position = new Vector3(0, stageupline.transform.position.y + stagehi, 0);
            coltime = 3;
            mapint = 1;
        }
        else if (stage < 20)
        {
            stagehi = 200;
            stageupline.transform.position = new Vector3(0, stageupline.transform.position.y + stagehi, 0);
            coltime = 2;
            mapint = 2;
        }
        else if (stage < 30)
        {
            stagehi = 500;
            stageupline.transform.position = new Vector3(0, stageupline.transform.position.y + stagehi, 0);
            coltime = 1.5f;
            mapint = 3;
        }
        else if (stage < 50)
        {
            stagehi = 1000;
            stageupline.transform.position = new Vector3(0, stageupline.transform.position.y + stagehi, 0);
            coltime = 1.2f;
            mapint = 4;
        }
        else if (stage >= 50)
        {
            stagehi = 1000;
            stageupline.transform.position = new Vector3(0, stageupline.transform.position.y + stagehi, 0);
            coltime = 1.2f;
            mapint = 4;
        }

        //스테이지 마다 랜덤한 위치에 아이템 소환
        StartCoroutine(spawnitem(itemint, stagehi));
       
        Check = false;
    }
    IEnumerator spawnitem(int itemint, int stagehi)
    {
        for (int i = 0; i < itemint; i++)
        {
            int percent = Random.Range(stage, 60);
            int ranitem = percent > 45 ? 1 : 0;
            GameObject itemobj = item.Get(ranitem);
            itemobj.transform.position = new Vector3(Random.Range(-1.7f, 1.7f),
                Random.Range(cam.transform.position.y + 10, cam.transform.position.y + stagehi));
            yield return new WaitForSeconds(0.02f);
        }
    }
    public void textUpdate()// 텍스트 업데이트
    {
        getcointtext.text = "+" + getcoin.ToString();
        getredtext.text = "+" + getredcoin.ToString();


        if (gm.gamestate == 3)
        {
            coin += getcoin;
            Redcoin += getredcoin;

            cointtext.text = coin.ToString();
            Redtext.text = Redcoin.ToString();

            PlayerPrefs.SetInt("coin", gm.coin);
            PlayerPrefs.SetInt("Redcoin", gm.Redcoin);
        }

    }
    public IEnumerator gaugefal() //출발시 ui등 행동제어
    {
        UIUpdate();
        sc.StartCoroutine(sc.CK());
        pl.rg.bodyType = RigidbodyType2D.Dynamic;
        pl.rg.AddForce(Vector2.up * (pl.pw + ((pl.pw * SG.power / 100) * SG.cripower)));
        SG.powerCktext.text = " ";
        SG.powertext.text = " ";
        yield return new WaitForSeconds(0.1f);
        startbuster.SetActive(true);
        while (pl.rg.velocity.y > 1)
        {
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForFixedUpdate();
        startbuster.SetActive(false);
        Debug.Log("gmae");
        gamestate = 2;
    }
    public void UIUpdate() // 게임 상태에 따른ui제어
    {
        bool lobby = gamestate == 0 ? true : false;
        bool ingame = gamestate == 1 || gamestate == 2 ? true : false;
        bool end = gamestate == 3 ? true : false;

        for (int i = 0; i < 50; i++)
        {
            if (i < lobbyUI.Length)
                lobbyUI[i].SetActive(lobby);
            if (i < IngameUI.Length)
                IngameUI[i].SetActive(ingame);
            if (i < GOUI.Length)
                GOUI[i].SetActive(end);
        }

    }
    public void gameover()//게임오버
    {
        gamestate = 3;
        gm.UIUpdate();
        gameoverui.SetActive(true);
        goSc.text = "점수 : " + sc.Sc.ToString("F0");
        gohi.text = "높이 : " + sc.ply.ToString("F1");
        gocoin.text = "획득 코인 : " + getcoin.ToString();
        goRed.text = "획득 코인 : " + getredcoin.ToString();

        coin += getcoin;
        Redcoin += getredcoin;

        cointtext.text = coin.ToString();
        Redtext.text = Redcoin.ToString();

        PlayerPrefs.SetInt("coin", gm.coin);
        PlayerPrefs.SetInt("Redcoin", gm.Redcoin);


        if (sc.Sc > maxSc)
        {
            maxSc = sc.Sc;
            gomaxSc.text = "최고 점수 : " + maxSc.ToString("F0");
            PlayerPrefs.SetFloat("maxSc", maxSc);
        }
        else
        {
            maxSc = PlayerPrefs.GetFloat("maxSc");
            gomaxSc.text = "최고 점수 : " + maxSc.ToString("F0");
        }


        if (sc.ply > maxhi)
        {
            maxhi = sc.ply;
            gomaxhi.text = "최고 높이 : " + maxhi.ToString("F1");
            PlayerPrefs.SetFloat("maxhi", maxhi);
        }
        else
        {
            maxhi = PlayerPrefs.GetFloat("maxhi");
            gomaxhi.text = "최고 높이 : " + maxhi.ToString("F1");
        }

        PlayerPrefs.Save();

        Time.timeScale = 0;
    }

    public void repl() //재도전!
    {
        SceneManager.LoadScene("game");
    }




    // UI

    // 항시
    public void Pouse() // 일시정지
    {
        pouse = true;
        Time.timeScale = 0;
        Fade.SetActive(true);
        pouseobj[0].SetActive(true);
        pouseobj[2].SetActive(false);

        for (int i = 1; i < pouseobj.Length; i++)
        {
            pouseobj[i].SetActive(false);
        }
    }
    public void Goback() // 돌아가기
    {
        StartCoroutine(GoCount());
        pouseobj[0].SetActive(false);
    }
    IEnumerator GoCount() // 돌아가기 행동제어
    {
        if(gamestate >= 1 && gamestate != 3)
        {
            counttext.fontSize = 150;
            counttext.text = "3";
            yield return new WaitForSecondsRealtime(1);
            counttext.text = "2";
            yield return new WaitForSecondsRealtime(1);
            counttext.text = "1";
            yield return new WaitForSecondsRealtime(1);
            counttext.text = "";
        }
        Fade.SetActive(false);
        pouseobj[2].SetActive(true);
        Time.timeScale = 1;
        pouse = false;
    }

    public void GameExit() // 게임 종료
    {
        Application.Quit();
    }

    //시작
    public void DifficultyChangeBT() // 난이도 변경버튼
    {
        if (StartGauge.SG.gaugestate != 0)
            return;

        difficulty = difficulty + 1 > 2 ? 0 : difficulty + 1;
        string D = difficulty == 0 ? "하" : difficulty == 1 ? "중" : "상";
        difficultytext.text = "난이도\n변경\n<" + D + ">";
    }


    public void SettingMenu() // 셋팅 메뉴
    {
        pouseobj[1].SetActive(true); // 백버튼
        pouseobj[3].SetActive(true);

        if (BN_tail == 1)
            BN_back_im.color = new Color(1, 1, 1, 1);
        else
            BN_tail_im.color = new Color(0.5f, 0.5f, 0.5f, 1);

        if (BN_back == 1)
            BN_back_im.color = new Color(1, 1, 1, 1);
        else
            BN_back_im.color = new Color(0.5f, 0.5f, 0.5f, 1);
    }
    public void Burning_tail_Impact() // 버닝시 주사위 효과 유무
    {
        if (BN_tail == 1)
        {
            BN_tail = 0;
            BN_tail_im.color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
        else
        {
            BN_tail = 1;
            BN_tail_im.color = new Color(1, 1, 1, 1);
        }

        PlayerPrefs.SetInt("BN_tail", BN_tail);
        PlayerPrefs.Save();

    }
    public void Burning_back_Impact() // 버닝시 배경효과 유무
    {
        if (BN_back == 1)
        {
            BN_back = 0;
            BN_back_im.color = new Color(0.5f, 0.5f, 0.5f, 1);
        }
        else
        {
            BN_back = 1;
            BN_back_im.color = new Color(1, 1, 1, 1);
        }
        PlayerPrefs.SetInt("BN_back", BN_back);
        PlayerPrefs.Save();
    }

    public void deletPrefab() // 데이터 삭제
    {
        if (StartGauge.SG.gaugestate != 0)
        {
            StartCoroutine(EFfalse());
            deletprefab.SetActive(false);
            return;
        }
        else if (deletprefab.activeSelf)
        {
            StartCoroutine(Delete());
        }
        else
            deletprefab.SetActive(true);
    }
    IEnumerator Delete()
    {
        PlayerPrefs.DeleteAll();
        deletprefab.SetActive(false);
        counttext.fontSize = 60;
        counttext.text = "데이터가 삭제되었습니다!\n게임을 재실행합니다!";
        yield return new WaitForSecondsRealtime(3);
        counttext.text = "";
        repl();
    }
    public void codeenter() //코드 입력
    {
        if (StartGauge.SG.gaugestate != 0)
        {
            StartCoroutine(EFfalse());
            return;
        }
        else if (codepanel.activeSelf)
            codecheck();
        else
            codepanel.SetActive(true);
    }
    void codecheck() // 코드 체크
    {
        if ( string.Compare(codetext.text, "안녕하세요?",true) == 0) 
        {
            Cdck(0, 500); // C,R
        }
        if (string.Compare(codetext.text, "데이터 삭제", true) == 0)
        {
            deletPrefab();
        }

       
    }
    void Cdck(int c,int r)  //코드 유무 확인
    {
        coin += c;
        Redcoin += r;

        cointtext.text = coin.ToString();
        Redtext.text = Redcoin.ToString();

        PlayerPrefs.SetInt("coin", gm.coin);
        PlayerPrefs.SetInt("Redcoin", gm.Redcoin);

        PlayerPrefs.Save();
        StartCoroutine(codepass(r, c));
        codepanel.SetActive(false);
        Pouse();
    }

    IEnumerator EFfalse()
    {
        counttext.fontSize = 60;
        counttext.text = "로비에서 실행시켜주세요!";
        yield return new WaitForSecondsRealtime(3);
        counttext.text = "";
    }
    IEnumerator codepass(int R,int C) // 코드 정보전달 // 나중에 다시
    {
        counttext.fontSize = 60;
        if (R != 0 && C != 0)
            counttext.text = "레드 코인 " + R + "개를 획득!\n코인 " + C + "개를 획득!";
        else if (R != 0)
            counttext.text = "레드 코인 " + R + "개를 획득!";
        else if (C != 0)
            counttext.text = "코인 " + C + "개를 획득!";
        else
        {
            counttext.text = "코드가 일치하지 않아요;;";
            Debug.Log("3");
        }
        yield return new WaitForSecondsRealtime(3);
        counttext.text = "";
    }
}
