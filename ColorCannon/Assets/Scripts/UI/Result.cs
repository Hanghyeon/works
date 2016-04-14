using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Result : MonoBehaviour, IBackScene
{

    Text scoreText;
    float   currScore;
    float   gameScore;
    float currTime;
    [SerializeField]
    private float fillTime;

    void Awake()
    {
        transform.FindChild("Retry").GetComponent<Button>().onClick.AddListener(OnRetry);
        transform.FindChild("GoMain").GetComponent<Button>().onClick.AddListener(OnMainMenu);

        scoreText = transform.FindChild("Score").GetComponent<Text>();
    }


    void OnEnable()
    {
        gameScore = GameLogic.Instance.currScore;

        CheckHighScore();
        PlayerData.Instance.Save();

        currScore = 0;
        currTime = 0;
        StartCoroutine(FillScore());
    }

    void CheckHighScore()
    {
        switch (GameLogic.Instance.retryDiff)
        {
            case Difficulty.Easy:
                IsHighScore(0);
                break;
            case Difficulty.Normal:
                IsHighScore(1);
                break;
            case Difficulty.Hard:
                IsHighScore(2);
                break;
            default:
                break;
        }
    }

    void IsHighScore(int idx)
    {
        if(gameScore >= PlayerData.Instance.highScore[idx])
            PlayerData.Instance.highScore[idx] = (int)gameScore;

        Debug.Log(PlayerData.Instance.highScore[idx]);
    }

    IEnumerator FillScore()
    {
        Debug.Log(Time.timeScale);
        while (currTime <= fillTime)
        {
            currTime += Time.deltaTime;
            currScore =  EasingUtil.easeOutSine(0f, gameScore, currTime / fillTime);
            scoreText.text = ((int)currScore).ToString();
            yield return null;
        }
        scoreText.text = gameScore.ToString();
    }
    public void OnRetry()
    {
        UIManager.Instance.hide(UIpage.Result);
        UIManager.Instance.Show(UIpage.PlayRoom);
        GameLogic.Instance.RestartGame(GameLogic.Instance.retryDiff);
    }

    public void OnMainMenu()
    {
        UIManager.Instance.hide(UIpage.Result);
        UIManager.Instance.Show(UIpage.MainMenu);

        SoundManager.Instance.UpdateTitleBGM();
        SoundManager.Instance.PlayTitleBGM();
    }


    #region IBackScene 멤버

    public void OnBack()
    {
        SoundManager.Instance.PlayEffect(EffectSound.Button_Click);
        UIManager.Instance.hide(UIpage.Result);
        UIManager.Instance.Show(UIpage.MainMenu);
    }

    #endregion
}
