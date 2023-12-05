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
    //소비탬 관리
    public int[] oneoffint;

    //다이스 관리
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
    void coinupdate() //코인 업데이트
    {
        coin.text = gm.coin.ToString();
        Redcoin.text = gm.Redcoin.ToString();
    }
    public void HomeBtn() //g홈 버튼 클릭
    {
        for (int i = 0; i < otherobj.Length; i++)
            otherobj[i].SetActive(true);
        shopobj.SetActive(false);
    }

    public void shopBtn() //샵 버튼 클릭
    {
        shopobj.SetActive(true);
        choiceobj.SetActive(true);
        for (int i = 0; i < otherobj.Length; i++)
            otherobj[i].SetActive(false);

        coinupdate();
    }

    //--------- 상점 선택--------------

    public void oneoffitem()//소비템 상점
    {
        oneoffshop.SetActive(true);
        diceshop.SetActive(false);
        changepointshop.SetActive(false);

        ////소비탬 수량 최신화
        //for (int i = 0; i < oneoffint.Length; i++)
        //{
        //    oneoffinttext[i].text = oneoffint[i].ToString();
        //}
    }

    public void dice() //주사위 상점
    {
        oneoffshop.SetActive(false);
        diceshop.SetActive(true);
        changepointshop.SetActive(false);

        //다이스 수량 최신화
        for (int i = 0; i < diceint.Length; i++)
        {
            diceinttext[i].text = diceint.ToString();
        }
    }
    public void changepoint() //주사위 상점
    {
        oneoffshop.SetActive(false);
        diceshop.SetActive(false);
        changepointshop.SetActive(true);

    }

    //구매 버튼
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
                ts.text(vec, "구매 완료!", 10, 2, 75);
            }
            else
            {
                textSC ts = shoptext.Get(0).GetComponent<textSC>();
                ts.text(vec, "코인이\n부족합니다.", 10, 2, 75);
            }
        }




    }

}
