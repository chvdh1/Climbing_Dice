using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textSC : MonoBehaviour
{
    GameObject ch;
    Text tx;

    
    private void Awake()
    {
        ch = transform.GetChild(0).gameObject;
        tx = ch.GetComponent<Text>();
    }

    public void text(Vector3 pos,string st, float speed, float time,int fontsize)
    {
        transform.position = pos;
        tx.text = st;
        tx.fontSize = fontsize;
        ch.SetActive(true);
        ch.transform.position = transform.position;
        StartCoroutine(move(speed,time));
    }

    IEnumerator move(float sp,float t)
    {
        float y = transform.position.y;
        yield return new WaitForFixedUpdate();
        Debug.Log("sp = "+sp);
        Debug.Log("t = " + t);
        while (t >0)
        {
            ch.transform.position =
                new Vector3(transform.position.x, y * sp * Time.deltaTime, transform.position.z);
            t -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        tx.text = "";
        yield return new WaitForFixedUpdate();
        gameObject.SetActive(false);
    }
}
