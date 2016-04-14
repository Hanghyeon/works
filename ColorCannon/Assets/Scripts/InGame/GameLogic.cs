using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public enum ColorType : int
{
    Red     =   1,      // 0000 0001
    Yellow  =   2,      // 0000 0010
    Blue    =   4,      // 0000 0100
    
    Orange  =   3,      // 0000 0011
    Green   =   6,      // 0000 0110
    Purple  =   5,      // 0000 0101
    Black   =   7       // 0000 0111
}

public class GameLogic : Singleton<GameLogic> 
{
    public List<ColorType> giveColorList { get; private set; }
    public List<ColorType> spawnColorList { get; private set; }


    public int currLevel { get; private set; }
    public int currScore { get; private set; }
    public float hp { get; private set; }

    public int combo { get; private set; }

    public float enemyMoveTime { get; private set; }
    public float generateTime { get; private set; }

    public readonly float   maxHP = 100;
    public readonly int     maxLV = 6;

    public Difficulty retryDiff { get; private set; }

    EnemyGenerator enemyGene;

    void Awake()
    {
        giveColorList   =   new List<ColorType>();
        spawnColorList  =   new List<ColorType>();

        enemyGene       =   GameObject.Find("EnemyGenerator").GetComponent<EnemyGenerator>();

        TableDataManager.Instance.LoadStage();
        AddGiveColorList(ColorType.Red);
        AddSpawnColorList(ColorType.Red);
    }

    public void RestartGame(Difficulty diff)
    {
        SoundManager.Instance.UpdateBGMClip(diff);
        SoundManager.Instance.PlayBGM(diff);

        ResetList();


        hp          =   100;
        currLevel   =   0;
        currScore   =   0;
        retryDiff   =   diff;
        combo       =   0;

        enemyMoveTime   = 0;
        generateTime    = 0;

        switch (diff)
        {
            case Difficulty.Easy:
                AddDifficulty(3.4f, 3);
                break;
            case Difficulty.Normal:
                AddDifficulty(3, 2);
                break;
            case Difficulty.Hard:
                AddDifficulty(1.9f, 2);
                break;
            default:
                break;
        }


        AddScore(0);    
        enemyGene.Restart();
    }

    void AddDifficulty(float moveTime, float geneTime)
    {
        enemyMoveTime += moveTime;
        generateTime += geneTime;
    }

    public void DiffControl()
    {
        switch (retryDiff)
        {
            case Difficulty.Easy:
                AddDifficulty(-0.2f, -0.05f);
                break;
            case Difficulty.Normal:
                AddDifficulty(-0.1f, -0.05f);
                break;
            case Difficulty.Hard:
                AddDifficulty(-0.05f, -0.05f);
                break;
            default:
                break;
        }
    }
    // 게임이 끝났을때 처리
    public void GameOver()
    {
        enemyGene.Stop();
        ResetList();

        UIManager.Instance.Show(UIpage.Result);
        UIManager.Instance.hide(UIpage.PlayRoom);
    }

    // 게임 중간에 나왔을때
    public void ForceStop()
    {
        enemyGene.Stop();
        ResetList();
    }


    public void AddGiveColorList(ColorType color)
    {
        giveColorList.Add(color);
    }

    public void AddSpawnColorList(ColorType color)
    {
        spawnColorList.Add(color);
    }


    public void AddScore(int addValue)
    {
        if(addValue <= 0)
            combo = 0;
        else
            combo++;

        currScore += addValue + combo;

        UIManager.Instance.GetUIpage(UIpage.PlayRoom).
            GetComponent<PlayRoom>().RefreshScore(currScore);

        UIManager.Instance.GetUIpage(UIpage.PlayRoom).
            GetComponent<PlayRoom>().RefreshCombo(combo);
    }


    /// <summary>
    /// 적에게 맞았을때
    /// </summary>
    /// <param name="damage"></param>
    public void Hit(int damage)
    {
        this.hp -= damage;

        if (damage > 0)
        {
            combo = 0;

        }

        hp = Mathf.Clamp(hp, 0, maxHP);

        if (hp <= 0)
            GameOver();

        UIManager.Instance.GetUIpage(UIpage.PlayRoom)
            .GetComponent<PlayRoom>().RefreshHpBar(this.hp);
        
    }


    public void LevelUp()
    {
        currLevel++;

        for (int i = 0; i < TableDataManager.Instance.stages[currLevel].giveColor.Count; i++)
            AddGiveColorList(TableDataManager.Instance.stages[currLevel].giveColor[i]);

        for (int i = 0; i < TableDataManager.Instance.stages[currLevel].addSpawnColor.Count; i++)
            AddSpawnColorList(TableDataManager.Instance.stages[currLevel].addSpawnColor[i]);
        
        Hit(-30);
    }



    public bool IsLevelUP()
    {
        if(currLevel == maxLV)
            return false;

        if (currScore >= TableDataManager.Instance.stages[currLevel+1].needScore)
        {
            return true;
        }
        return false;

    }

    void ResetList()
    {
        giveColorList.Clear();
        spawnColorList.Clear();

        AddGiveColorList(ColorType.Red);
        AddSpawnColorList(ColorType.Red);

        GameObject.Find("Cannon").GetComponent<Cannon>().Reset();
    }
}
