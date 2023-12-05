using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pool : MonoBehaviour
{
    public GameObject[] prefabs;

    List<GameObject>[] pools;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < pools.Length; i++)
            pools[i] = new List<GameObject>();
    }

    public GameObject Get(int i)
    {
        GameObject sel = null;

        foreach (GameObject item in pools[i]) //0... 선택한 풀의 놀고 있는 게임오브젝트 접근
            if (!item.activeSelf)
            {
                // 발견하면 sel변수에 할당
                sel = item;
                sel.SetActive(true);
                break;
            }

        if (!sel) // 만약에 모두 쓰이고 있다면 
        {
            // 새롭게 생성해서 sel 변수에 할당
            sel = Instantiate(prefabs[i], transform);
            pools[i].Add(sel);
        }


        return sel;
    }

}
