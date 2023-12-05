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

        foreach (GameObject item in pools[i]) //0... ������ Ǯ�� ��� �ִ� ���ӿ�����Ʈ ����
            if (!item.activeSelf)
            {
                // �߰��ϸ� sel������ �Ҵ�
                sel = item;
                sel.SetActive(true);
                break;
            }

        if (!sel) // ���࿡ ��� ���̰� �ִٸ� 
        {
            // ���Ӱ� �����ؼ� sel ������ �Ҵ�
            sel = Instantiate(prefabs[i], transform);
            pools[i].Add(sel);
        }


        return sel;
    }

}
