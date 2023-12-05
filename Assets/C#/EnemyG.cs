using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyG : MonoBehaviour
{
    gamemanager gm;

    [SerializeField] pool enemy1;
    [SerializeField] pool enemy2;
    [SerializeField] pool enemy3;
    [SerializeField] pool enemy4;

    float time;
    int m50 = 0;
    int RAN;
    // Start is called before the first frame update
    void Start()
    {
        gm = gamemanager.gm;
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gamestate == 0 || gm.gamestate == 4)
            return;

        int a = 0;
        int b = 0;
        if (gm.cam != null && gm.gamestate == 2)
        {
            a = (int)gm.cam.transform.position.y;
            b = a / 15;
            if (b != m50)
            {
                m50 = b;
                RAN = Random.Range(0, gm.mapint + 1 +gm.difficulty);
                RAN = RAN > 3 ? 3 : RAN;
                SpwanEnemy(RAN);
              
            }
        }
        time += Time.deltaTime;
        if (time > gm.coltime)
        {
            time = 0;
            RAN = Random.Range(0, gm.mapint + 1 + gm.difficulty);
            RAN = RAN > 3 ? 3 : RAN;
            if (RAN == 3)
                SpwanEnemy(RAN);
        }
    }
    void SpwanEnemy(int enemynum) // 장애물 소환
    {
        GameObject enemyobj;
        // 난이도에 따른 오젝 소환 제어문
        switch (enemynum)
        {
            case 0: //무빙벽
                float ranpos = Random.Range(-0.5f, 0.5f);
                enemyobj = enemy1.Get(gm.mapint);
                enemyobj.transform.position = new Vector3(ranpos, gm.cam.transform.position.y + 10, 0);
                movewall movewalls = enemyobj.GetComponent<movewall>();
                movewalls.scale(gm.cam, Random.Range(5, 7), gm.mapint);
                break;
            case 1:// 그물
                float ranpos1 = Random.Range(-1f, 1f);
                enemyobj = enemy2.Get(gm.mapint);
                net nets = enemyobj.GetComponent<net>();
                enemyobj.transform.position = new Vector3(ranpos1, gm.cam.transform.position.y + 10, 0);
                float ransiz = Random.Range(1, 2);
                int ranC = Random.Range(0, 3);
                nets.scale(gm.cam, ransiz, ransiz, ranC,gm. mapint);
                break;
            case 2:// 트렙
                float ranpos2 = Random.Range(-2f, 2f);
                enemyobj = enemy3.Get(gm.mapint + 1);
                trap traps = enemyobj.GetComponent<trap>();
                enemyobj.transform.position = new Vector3(ranpos2, gm.cam.transform.position.y + 10, 0);
                if (gm.mapint == 0)
                {
                    GameObject enemyobjC = enemy3.Get(0);
                    enemyobjC.transform.position = new Vector3(ranpos2, gm.cam.transform.position.y + 10 + 0.17f, 0);
                }
                float ransiz1 = Random.Range(0.5f, 1.5f);
                float ransiz2 = Random.Range(0.5f, 1.5f);
                traps.scale(gm.cam, ransiz1, ransiz2, gm.mapint);
                break;
            case 3:// 드롭트렉
                float ranpos3 = Random.Range(-2f, 2f);
                enemyobj = enemy4.Get(gm.mapint);
                droptrap droptraps = enemyobj.GetComponent<droptrap>();
                enemyobj.transform.position = new Vector3(ranpos3, gm.cam.transform.position.y + 10, 0);
                float ransiz11 = Random.Range(0.5f, 1.5f);
                float ransiz22 = Random.Range(0.5f, 1.5f);
                float ransiz33 = Random.Range(0.5f, 1.5f);
                droptraps.scale(gm.cam, ransiz11, ransiz22, 5 + ransiz33, gm.mapint);
                break;
        }
    }
}
