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

    //������ ���� ����
    public int gamestate = 0; // 0 = �غ� / 1 = ��� / 2 = ���� / ���� = 3 
    [SerializeField] GameObject gameoverui, gamestartui, startbuster;
    [SerializeField] Text goSc, gohi, gocoin, goRed, gomaxSc, gomaxhi;

    //�÷��̾� ����
    [HideInInspector] public player pl;
    [HideInInspector] public GameObject plobj;
    [HideInInspector] public Score sc;
    [HideInInspector] public int coin, Redcoin, getcoin, getredcoin;
    float maxSc, maxhi;
    [SerializeField] Text cointtext, Redtext, getcointtext, getredtext;

    //������ ����
    [HideInInspector] public float gaugeMax = 100f;
    [HideInInspector] public float gaugeCriMax;
    [HideInInspector] public float gaugeCriMin;

    //�÷��̾� ���� ui
    [HideInInspector] public GameObject[] BNUI;
    [HideInInspector] public Sprite[] ranBNUI;
    [HideInInspector] public bool dice_tail;

    //�Ͻ����� UI
    [HideInInspector] public bool pouse;
    [HideInInspector] public int BN_tail, BN_back;
    [SerializeField] Image BN_tail_im, BN_back_im;
    public GameObject[] pouseobj; //�޴�, ��, �Ͻ�����, ����
    [HideInInspector] public GameObject Fade;
    [SerializeField] Text counttext;
    [SerializeField] GameObject deletprefab, codepanel;
    [HideInInspector] public Text codetext;

    //���� ���¿� ���� UI����
    [SerializeField] GameObject[] lobbyUI;
    [SerializeField] GameObject[] IngameUI;
    [SerializeField] GameObject[] GOUI;



    pool item;
    [HideInInspector] public GameObject cam;

    //���̿� ���� �������� �� ����
    [HideInInspector] public int stage = 0;
    [HideInInspector] public bool Check;
    GameObject stageupline;
    float high = 0;

    //��ֹ��� �ֱ����� ����
    [HideInInspector] public int difficulty = 0; // 0 = ���� , 1 = �븻 .2=�ϵ�
    [SerializeField] Text difficultytext;
    [HideInInspector] public float coltime = 5; // ��ֹ� ������ ��Ÿ��

    //�� ������ ���� ����
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


    public void stageup() // �������� ���� ��ȭ�� ��, ��ȯ �֤���, 
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

        //�������� ���� ������ ��ġ�� ������ ��ȯ
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
    public void textUpdate()// �ؽ�Ʈ ������Ʈ
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
    public IEnumerator gaugefal() //��߽� ui�� �ൿ����
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
    public void UIUpdate() // ���� ���¿� ����ui����
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
    public void gameover()//���ӿ���
    {
        gamestate = 3;
        gm.UIUpdate();
        gameoverui.SetActive(true);
        goSc.text = "���� : " + sc.Sc.ToString("F0");
        gohi.text = "���� : " + sc.ply.ToString("F1");
        gocoin.text = "ȹ�� ���� : " + getcoin.ToString();
        goRed.text = "ȹ�� ���� : " + getredcoin.ToString();

        coin += getcoin;
        Redcoin += getredcoin;

        cointtext.text = coin.ToString();
        Redtext.text = Redcoin.ToString();

        PlayerPrefs.SetInt("coin", gm.coin);
        PlayerPrefs.SetInt("Redcoin", gm.Redcoin);


        if (sc.Sc > maxSc)
        {
            maxSc = sc.Sc;
            gomaxSc.text = "�ְ� ���� : " + maxSc.ToString("F0");
            PlayerPrefs.SetFloat("maxSc", maxSc);
        }
        else
        {
            maxSc = PlayerPrefs.GetFloat("maxSc");
            gomaxSc.text = "�ְ� ���� : " + maxSc.ToString("F0");
        }


        if (sc.ply > maxhi)
        {
            maxhi = sc.ply;
            gomaxhi.text = "�ְ� ���� : " + maxhi.ToString("F1");
            PlayerPrefs.SetFloat("maxhi", maxhi);
        }
        else
        {
            maxhi = PlayerPrefs.GetFloat("maxhi");
            gomaxhi.text = "�ְ� ���� : " + maxhi.ToString("F1");
        }

        PlayerPrefs.Save();

        Time.timeScale = 0;
    }

    public void repl() //�絵��!
    {
        SceneManager.LoadScene("game");
    }




    // UI

    // �׽�
    public void Pouse() // �Ͻ�����
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
    public void Goback() // ���ư���
    {
        StartCoroutine(GoCount());
        pouseobj[0].SetActive(false);
    }
    IEnumerator GoCount() // ���ư��� �ൿ����
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

    public void GameExit() // ���� ����
    {
        Application.Quit();
    }

    //����
    public void DifficultyChangeBT() // ���̵� �����ư
    {
        if (StartGauge.SG.gaugestate != 0)
            return;

        difficulty = difficulty + 1 > 2 ? 0 : difficulty + 1;
        string D = difficulty == 0 ? "��" : difficulty == 1 ? "��" : "��";
        difficultytext.text = "���̵�\n����\n<" + D + ">";
    }


    public void SettingMenu() // ���� �޴�
    {
        pouseobj[1].SetActive(true); // ���ư
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
    public void Burning_tail_Impact() // ���׽� �ֻ��� ȿ�� ����
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
    public void Burning_back_Impact() // ���׽� ���ȿ�� ����
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

    public void deletPrefab() // ������ ����
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
        counttext.text = "�����Ͱ� �����Ǿ����ϴ�!\n������ ������մϴ�!";
        yield return new WaitForSecondsRealtime(3);
        counttext.text = "";
        repl();
    }
    public void codeenter() //�ڵ� �Է�
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
    void codecheck() // �ڵ� üũ
    {
        if ( string.Compare(codetext.text, "�ȳ��ϼ���?",true) == 0) 
        {
            Cdck(0, 500); // C,R
        }
        if (string.Compare(codetext.text, "������ ����", true) == 0)
        {
            deletPrefab();
        }

       
    }
    void Cdck(int c,int r)  //�ڵ� ���� Ȯ��
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
        counttext.text = "�κ񿡼� ��������ּ���!";
        yield return new WaitForSecondsRealtime(3);
        counttext.text = "";
    }
    IEnumerator codepass(int R,int C) // �ڵ� �������� // ���߿� �ٽ�
    {
        counttext.fontSize = 60;
        if (R != 0 && C != 0)
            counttext.text = "���� ���� " + R + "���� ȹ��!\n���� " + C + "���� ȹ��!";
        else if (R != 0)
            counttext.text = "���� ���� " + R + "���� ȹ��!";
        else if (C != 0)
            counttext.text = "���� " + C + "���� ȹ��!";
        else
        {
            counttext.text = "�ڵ尡 ��ġ���� �ʾƿ�;;";
            Debug.Log("3");
        }
        yield return new WaitForSecondsRealtime(3);
        counttext.text = "";
    }
}
