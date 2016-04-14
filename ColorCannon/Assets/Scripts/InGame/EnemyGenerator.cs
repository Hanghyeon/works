using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyGenerator : MonoBehaviour
{
    private GameObjectPool<GameObject> enemyPool;
    private GameObject enemyPrefab;

    

    [SerializeField]
    private short enemyCount;
    [SerializeField]
    private int checkScore;
    [SerializeField]
    private int addCheckScore;


    private bool isPlaying  = true; 


    void Awake()
    {
        enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy");


        enemyPool = new GameObjectPool<GameObject>(enemyCount,enemyPrefab,(GameObject obj) =>
        {
            GameObject clone = GameObject.Instantiate(enemyPrefab);
            clone.SetActive(false);
            return clone;
        });



        
    }

    public void Restart()
    {
        StopCoroutine(GenerateRoutine());
        StartCoroutine(GenerateRoutine());
    }

    public void Stop()
    {
        DestroyAll();
        StopAllCoroutines();
    }

    IEnumerator GenerateRoutine()
    {

        while (true)
        {
            UIManager.Instance.GetUIpage(UIpage.PlayRoom).
                GetComponent<PlayRoom>().PreparePlay(GameLogic.Instance.currLevel);

            DestroyAll();

            yield return new WaitForSeconds(2f);

            isPlaying = true;

            while (isPlaying)
            {
                yield return new WaitForSeconds(Generate());
            }

            // End Level -> event?

            yield return null;
        }
    }


    float Generate()
    {
        if (GameLogic.Instance.IsLevelUP())
        {
            isPlaying = false;

            GameLogic.Instance.LevelUp();

            return 0;
        }

        if (GameLogic.Instance.currScore > checkScore)
        {
            checkScore += addCheckScore;
            GameLogic.Instance.DiffControl();
        }

        GameObject enemy = enemyPool.pop();
        enemy.transform.position = GetSpawnPos();
        enemy.SetActive(true);

        float randTime = Random.Range(GameLogic.Instance.generateTime,
            GameLogic.Instance.generateTime+0.5f);

        if (enemy.GetComponent<Enemy>().Init() && GameLogic.Instance.retryDiff != Difficulty.Easy )
            return randTime + 1;

        return randTime;
    }


    bool orderFlag = false;
    float screenInterval = 100;
    Vector2 GetSpawnPos()
    {
        Vector2 spawnPos = Vector2.zero;
        int sideFlag = Random.Range(0, 2);

        if(orderFlag)
        {
            spawnPos.x = Random.Range(0, Screen.width);
            spawnPos.y = sideFlag == 0 ? -screenInterval : Screen.height + screenInterval;
        }
        else
        {
            spawnPos.y = Random.Range(0, Screen.height);
            spawnPos.x = sideFlag == 0 ? -screenInterval : Screen.width + screenInterval;
        }

        orderFlag = !orderFlag;
        return Camera.main.ScreenToWorldPoint(spawnPos);
    }

    public void DestroyEnemy(GameObject obj)
    {
        obj.SetActive(false);
        enemyPool.push(obj);
    }

    public void DestroyAll()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemys.Length; i++)
            DestroyEnemy(enemys[i]);
    }

}
