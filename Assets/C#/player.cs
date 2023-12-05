using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    public static player pl;

    void Awake()
    {
        pl = this;
    }
    gamemanager gm;
    [HideInInspector] public float ply = 0;

   public int diceint;
    [HideInInspector] public bool isdead = false;
    [HideInInspector] public bool burning = false;
    bool click = false;
    bool touchwall = false;
    public float pw = 80;
    [SerializeField] float rospeed;
    int dir = -1, dicenum = 0, Jumpnum = 0;// 0 =��, 1=1������, 2= 2������. 3 = �뽬 
    public float gauge =0;
    [SerializeField] float dragdown;
    [SerializeField] float dragup;

    Vector2 dirvec;
    GameObject numbeobj;
    SpriteRenderer numSR;
    //Animator numanim;

    GameObject dice;
    SpriteRenderer plSR;
    //Animator planim;

    [HideInInspector] public Rigidbody2D rg;
    Collider2D box;

    public Sprite[] plstat;
    public Sprite[] dicenumstat;

    //���װ����� UI
    [SerializeField] RectTransform gaugebar;
    [SerializeField] Text gaugetext;
    public pool pl_Shadow;


    void Start()
    {
        Application.targetFrameRate = 60; //���� ������ �ӵ� 60���������� ���� ��Ű��.. �ڵ�
        QualitySettings.vSyncCount = 0;
        //����� �ֻ���(�÷�����)�� �ٸ� ��ǻ���� ��� ĳ���� ���۽� ������ ������ �� �ִ�.

        dice = transform.GetChild(0).gameObject;
        plSR = dice.GetComponent<SpriteRenderer>();
        //planim = dice.GetComponent<Animator>();

        numbeobj = dice.transform.GetChild(0).gameObject;
        box = numbeobj.GetComponent<Collider2D>();
        numSR = numbeobj.GetComponent<SpriteRenderer>();
       // numanim = numbeobj.GetComponent<Animator>();

        gm = gamemanager.gm;

        rg = GetComponent<Rigidbody2D>();
        dirvec = new Vector2(-1, 1).normalized;
        rg.AddForce(dirvec * pw);
        //numanim.SetTrigger("roll");
        rg.bodyType = RigidbodyType2D.Kinematic;
    }

    void Update()
    {
        if (gm.gamestate != 2 || gm.pouse)
            return;

        ply = transform.position.y;

        if (Input.GetMouseButtonDown(0) && !isdead)
        {
            dragdown = Input.mousePosition.x;
        }
        if(Input.GetMouseButtonUp(0) && !isdead)
        {
            click = true;
            dragup = Input.mousePosition.x;

            float length = Mathf.Abs((dragdown - dragup));
          
            if (length > 200f)
            {
                int dirpoint = dragdown < dragup ? 1 : -1;
                Jumpnum = 3;
                dir = dirpoint;
            }
               

            else
                Jumpnum++;

            rg.velocity = Vector2.zero;
            switch (Jumpnum)
            {
                case 1:
                    dirvec = new Vector2(dir, 1).normalized;
                    rg.AddForce(dirvec * pw);
                    //planim.SetTrigger("jp");
                    break;
                case 2:
                    dirvec = new Vector2(dir* - 1, 1).normalized;
                    rg.AddForce(dirvec * pw);
                    // planim.SetTrigger("jp");
                    break;
                case 3:
                    dirvec = new Vector2(dir, 0).normalized;
                    rg.AddForce(dirvec * pw*3);
                    // planim.SetTrigger("ds");
                    break;
            }
        }
       if(Jumpnum != 0 && Jumpnum < 3)
        {
            dice.transform.Rotate(Vector3.forward * Time.deltaTime * rospeed);
        }
       else if (Jumpnum == 0)
        {
            transform.Translate(Vector3.down * Time.deltaTime);
        }

       //��� �ൿ ����
       if(transform.position.x > 2.3f || transform.position.x < -2.3f)
            transform.position = new Vector2(2.2f * dir * -1, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6) // L
        {
            dir = 1;
            transform.position = new Vector2(2 * dir * -1, transform.position.y);
            if (!touchwall)
                Enterwall();
        }
        if (collision.gameObject.layer == 7)//R
        {
            dir = -1;
            transform.position = new Vector2(2 * dir * -1, transform.position.y);
            if (!touchwall)
                Enterwall();
        }
        if (collision.gameObject.layer == 10)//movewall
        {
            dir = collision.gameObject.transform.position.x > transform.position.x ? -1:1;
            transform.position = new Vector2(0.4f * dir+ collision.gameObject.transform.position.x, transform.position.y);
            if (!touchwall)
                Enterwall();
        }
        if (collision.gameObject.layer == 11)//net
        {
            rg.velocity = Vector2.zero;
            dice.transform.localEulerAngles = new Vector2(0, 0);
            net net = collision.gameObject.GetComponent<net>();
            net.countCK();
        }
        if (collision.gameObject.layer == 8)
        {
            gm.gameover();
        }


        // ������ �ν�
        if (collision.gameObject.layer == 13)
        {
            coin iteminf = collision.gameObject.GetComponent<coin>();
            if(iteminf.itemnum ==0)
            {
                gm.getcoin += iteminf.getint;
            }
            else if(iteminf.itemnum == 1)
            {
                gm.getredcoin += iteminf.getint;
            }
            gm.textUpdate();
            collision.gameObject.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 6 || 
            collision.gameObject.layer == 7 )
            && Jumpnum != 0 && touchwall)
        {
            Exitwall();
        }
        if(collision.gameObject.layer == 10)
        {
            if(Jumpnum != 0 && touchwall)
                Exitwall();
            else
            {
                Jumpnum = 1;
                Exitwall();
            }
        }

    }
    void Enterwall()
    {
        touchwall = true;

        // ���� ��ȣ �ν� �� ���� Ȯ��
       // numanim.SetTrigger("end");
        int ran = Random.Range(1, 7);
        numSR.sprite = dicenumstat[ran - 1];
        dicenum = ran;
        gauge += ran;
        if (gauge >= 100)
            gauge = 100;

        gaugebar.sizeDelta = new Vector2(7.2f * gauge, 40);
        gaugetext.text = gauge.ToString("F0") + " / 100";
        if (gauge >= 100 && !burning)
        {
            burning = true;
            burningselect();
        }


        // ���� �ʱ�ȭ
        Jumpnum = 0;

        //�÷��̾� �ൿ���� �� ���̱�
        rg.velocity = Vector2.zero;
        dice.transform.localEulerAngles = new Vector2(0, 0);
       
        plSR.sprite = plstat[1];
        transform.localScale = new Vector3(dir*0.5f, 0.5f, 1);
        click = false;
        // planim.SetTrigger("id");
    }
    void Exitwall()
    {
        touchwall = false;

        plSR.sprite = plstat[0];
        StartCoroutine(rananim());
        //numanim.SetTrigger("roll");
    }
    IEnumerator rananim()
    {
        yield return new WaitForFixedUpdate();
        while(Jumpnum != 0 && click)
        {
            int ran = Random.Range(1, 7);
            numSR.sprite = dicenumstat[ran - 1];
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void burningselect()// �ֻ����� Ư��
    {
        switch(diceint)
        {
            case 0:// �⺻ ���̽�
                StartCoroutine(diec1());
                break;
        }

        StartCoroutine(burningEF());
    }
    IEnumerator burningEF()
    {
        yield return new WaitForFixedUpdate();
        if (gm.BN_back == 1)
            gm.BNUI[0].SetActive(true);
        Image ranimg = gm.BNUI[1].GetComponent<Image>();
        float i = 0;
        while (gauge > 0)
        {
            i += Time.deltaTime;
            if(Jumpnum >0 && gm.BN_tail ==1)
            {
               Transform SD = pl_Shadow.Get(0).transform;
                SD.position = transform.position;
                SD.eulerAngles = new Vector3(0,0, dice.transform.eulerAngles.z);
            }
            if (i > 0.2f && gm.BN_back ==1 && gm.BNUI[0].activeSelf)
            {
                i = 0;
                int ran = Random.Range(0, 6);
                ranimg.sprite = gm.ranBNUI[ran];
            }
            yield return new WaitForFixedUpdate();
        }
        gm.BNUI[0].SetActive(false);
        yield return new WaitForFixedUpdate();
    }
    IEnumerator diec1()
    {
        yield return new WaitForFixedUpdate();
        while (gauge > 0)
        {
            gauge -= Time.deltaTime*10;
            gaugebar.sizeDelta = new Vector2(7.2f * gauge, 40);
            gaugetext.text = gauge.ToString("F0") + " / 100";
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForFixedUpdate();
        burning = false;
    }
}
