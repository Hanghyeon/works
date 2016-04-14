using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public enum Difficulty 
{
    Easy,
    Normal,
    Hard
}

public class StageSelect : MonoBehaviour, IBackScene
{
    Difficulty diff;

    Text highScore_Easy;
    Text highScore_Normal;
    Text highScore_Hard;

    void Awake()
    {
        transform.FindChild("Easy").GetComponent<Button>().onClick.AddListener(() => On_GameStart(Difficulty.Easy));
        transform.FindChild("Normal").GetComponent<Button>().onClick.AddListener(() => On_GameStart(Difficulty.Normal));
        transform.FindChild("Hard").GetComponent<Button>().onClick.AddListener(() => On_GameStart(Difficulty.Hard));

        highScore_Easy      =   transform.FindChild("High_Easy").GetComponent<Text>();
        highScore_Normal    =   transform.FindChild("High_Normal").GetComponent<Text>();
        highScore_Hard      =   transform.FindChild("High_Hard").GetComponent<Text>();

    }

    void OnEnable()
    {
        highScore_Easy.text     = PlayerData.Instance.highScore[0].ToString();
        highScore_Normal.text   = PlayerData.Instance.highScore[1].ToString();
        highScore_Hard.text     = PlayerData.Instance.highScore[2].ToString();
    }

    public void On_GameStart(Difficulty diff)
    {
        SoundManager.Instance.PlayEffect(EffectSound.Button_Click);
        UIManager.Instance.hide(UIpage.StageSelect);
        UIManager.Instance.Show(UIpage.PlayRoom); 
        switch (diff)
        {
            case Difficulty.Easy:
                GameLogic.Instance.RestartGame(diff);
                break;
            case Difficulty.Normal:
                GameLogic.Instance.RestartGame(diff);
                break;
            case Difficulty.Hard:
                GameLogic.Instance.RestartGame(diff);
                break;
            default:
                break;
        }



    }

    #region IBackScene 멤버

    public void OnBack()
    {
        SoundManager.Instance.PlayEffect(EffectSound.Button_Click);
        UIManager.Instance.hide(UIpage.StageSelect);
        UIManager.Instance.Show(UIpage.MainMenu); 
    }

    #endregion
}
