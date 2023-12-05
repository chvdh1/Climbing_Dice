using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class Shop : MonoBehaviour
{
    public static Shop sh;

    void Awake()
    {
        sh = this;
    }


    gamemanager gm;
    [SerializeField] GameObject[] otherobj;

    [SerializeField] GameObject shopobj, choiceobj,oneoffshop, diceshop, changepointshop;
    [SerializeField] Text coin;
    [SerializeField] Text Redcoin;

    [SerializeField] pool shoptext;
    //�Һ��� ����
    public int[] oneoffint;

    //���̽� ����
    public int[] diceint;
    [SerializeField] Text[] diceinttext;


    void Start()
    {
        gm = gamemanager.gm;
      
        for (int i = 0; i < otherobj.Length; i++)
            otherobj[i].SetActive(true);
        shopobj.SetActive(false);
        coinupdate();
    }
    public void setvariable()
    {
        shoptext = loading.ld.tt;
    }
    void coinupdate() //���� ������Ʈ
    {
        coin.text = gm.coin.ToString();
        Redcoin.text = gm.Redcoin.ToString();
    }
    public void HomeBtn() //gȨ ��ư Ŭ��
    {
        for (int i = 0; i < otherobj.Length; i++)
            otherobj[i].SetActive(true);
        shopobj.SetActive(false);
    }

    public void shopBtn() //�� ��ư Ŭ��
    {
        shopobj.SetActive(true);
        choiceobj.SetActive(true);
        for (int i = 0; i < otherobj.Length; i++)
            otherobj[i].SetActive(false);

        coinupdate();
    }

    //--------- ���� ����--------------

    public void oneoffitem()//�Һ��� ����
    {
        oneoffshop.SetActive(true);
        diceshop.SetActive(false);
        changepointshop.SetActive(false);

        ////�Һ��� ���� �ֽ�ȭ
        //for (int i = 0; i < oneoffint.Length; i++)
        //{
        //    oneoffinttext[i].text = oneoffint[i].ToString();
        //}
    }

    public void dice() //�ֻ��� ����
    {
        oneoffshop.SetActive(false);
        diceshop.SetActive(true);
        changepointshop.SetActive(false);

        //���̽� ���� �ֽ�ȭ
        for (int i = 0; i < diceint.Length; i++)
        {
            diceinttext[i].text = diceint.ToString();
        }
    }
    public void changepoint() //�ֻ��� ����
    {
        oneoffshop.SetActive(false);
        diceshop.SetActive(false);
        changepointshop.SetActive(true);

    }

    //���� ��ư
    //coin, Redcoin
    public void buy_item()
    {
        item itemSC = EventSystem.current.currentSelectedGameObject.GetComponent<item>();
        Vector3 vec = new Vector3(360, 640, 0);
        if(itemSC.itemtype == 0)
        {
            int type = itemSC.oneoffitem_count;
            int price = itemSC.price;

            if(gm.coin >= price)
            {
                gm.coin -= price;
                oneoffint[type]++;

                //for (int i = 0; i < oneoffint.Length; i++)
                //    oneoffinttext[i].text = oneoffint[i].ToString();

                textSC ts = shoptext.Get(0).GetComponent<textSC>();
                ts.text(vec, "���� �Ϸ�!", 10, 2, 75);
            }
            else
            {
                textSC ts = shoptext.Get(0).GetComponent<textSC>();
                ts.text(vec, "������\n�����մϴ�.", 10, 2, 75);
            }
        }




    }

}
