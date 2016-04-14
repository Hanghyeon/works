using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour, IChangeColor
{
    private EnemyGenerator enemyGenerator;

    private float elapsedTime = 0f;
    private Vector2 startPos;
    private SpriteRenderer spr;
    ColorType enemyColor;
    private float currMoveTime;
    private bool isMixedColor;

    void Awake()
    {
        enemyGenerator  =   GameObject.Find("EnemyGenerator").GetComponent<EnemyGenerator>();
        spr             =   GetComponent<SpriteRenderer>();
        currMoveTime    =   GameLogic.Instance.enemyMoveTime;
    }

    public bool Init()
    {
        elapsedTime     =   0f;
        startPos        =   transform.position;
        enemyColor      =   GameLogic.Instance.spawnColorList[Random.Range(0, GameLogic.Instance.spawnColorList.Count)];
        isMixedColor    =   ColorManager.Instance.IsMixedColor(enemyColor);


        ChangeColor(enemyColor);
        StartCoroutine(MoveRoutine());

        return isMixedColor;
    }

    IEnumerator MoveRoutine()
    {
        while(true)
        {
            transform.position = Vector2.Lerp(startPos, Vector2.zero, elapsedTime / currMoveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            DestoryEvent();
            GameLogic.Instance.Hit(20);
        }
        if (collision.transform.CompareTag("Bullet"))
        {
            if (ColorManager.Instance.IsColorCollision(collision.GetComponent<Bullet>().myColor,enemyColor))
            {
                ColorType temp;
                temp = ColorManager.Instance.CheckMixColor(enemyColor,collision.GetComponent<Bullet>().myColor);
                GameLogic.Instance.AddScore(10);
                Debug.Log(temp);
                if (temp == enemyColor)
                {
                    DestoryEvent();
                }
                else
                {
                    enemyColor = temp;
                    ChangeColor(enemyColor);
                }
                
            }
            else
                GameLogic.Instance.Hit(10);


        }
    }


    void DestoryEvent()
    {

        enemyGenerator.DestroyEnemy(gameObject);
    }


    #region IChangeColor 멤버

    public void ChangeColor(ColorType color)
    {
        spr.color = ColorManager.Instance.GetColor(color);
    }

    #endregion
}
