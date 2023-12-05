using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class item : MonoBehaviour
{
    public int itemtype; // 0 소비템, 1 다이스

    public int oneoffitem_count;
    public int diecitem_count;

    public int price;

    Shop sh;
    [SerializeField] Text pricetxt;
    [SerializeField] Text quantity;

    private void Start()
    {
        sh = Shop.sh;
        pricetxt.text = price.ToString();

        if(itemtype == 0)
        {
            quantity = transform.GetChild(3).GetComponent<Text>();
            quantity.text = sh.oneoffint[oneoffitem_count].ToString();
        }
          
        //else if(itemtype == 1)
        //    quantity.text = sh.oneoffint[oneoffitem_count].ToString();
    }

    public void quantityupdate()
    {
        if (itemtype == 0)
        {
            quantity.text = sh.oneoffint[oneoffitem_count].ToString();
        }

        //else if(itemtype == 1)
        //    quantity.text = sh.oneoffint[oneoffitem_count].ToString();
    }

}
